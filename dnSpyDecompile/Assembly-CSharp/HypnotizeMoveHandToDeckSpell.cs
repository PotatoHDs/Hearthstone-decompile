using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FB RID: 2043
public class HypnotizeMoveHandToDeckSpell : SuperSpell
{
	// Token: 0x06006EFC RID: 28412 RVA: 0x0023C218 File Offset: 0x0023A418
	public override bool AddPowerTargets()
	{
		this.m_visualToTargetIndexMap.Clear();
		this.m_targetToMetaDataMap.Clear();
		this.m_targets.Clear();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			PowerTask task = taskList[i];
			Card targetCardFromPowerTask = this.GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && base.IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				this.AddTarget(targetCardFromPowerTask.gameObject);
			}
		}
		return true;
	}

	// Token: 0x06006EFD RID: 28413 RVA: 0x0023C298 File Offset: 0x0023A498
	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return null;
		}
		Network.HistTagChange histTagChange = power as Network.HistTagChange;
		if (histTagChange.Tag != 49)
		{
			return null;
		}
		if (histTagChange.Value != 2)
		{
			return null;
		}
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarningFormat("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", new object[]
			{
				this,
				histTagChange.Entity
			});
			return null;
		}
		if (entity.GetZone() != TAG_ZONE.HAND)
		{
			return null;
		}
		return entity.GetCard();
	}

	// Token: 0x06006EFE RID: 28414 RVA: 0x0023C31E File Offset: 0x0023A51E
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		this.SetActors();
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoActionWithTiming());
	}

	// Token: 0x06006EFF RID: 28415 RVA: 0x0023C348 File Offset: 0x0023A548
	private void SetActors()
	{
		this.m_friendlyActors.Clear();
		this.m_opponentActors.Clear();
		InputManager.Get().DisableInput();
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			Card component = this.m_targets[i].GetComponent<Card>();
			Entity entity = component.GetEntity();
			Actor actor = component.GetActor();
			if (entity.IsControlledByFriendlySidePlayer())
			{
				this.m_friendlyActors.Add(actor);
			}
			else
			{
				this.m_opponentActors.Add(actor);
			}
		}
	}

	// Token: 0x06006F00 RID: 28416 RVA: 0x0023C3CC File Offset: 0x0023A5CC
	private int FindTaskCountToRun()
	{
		int num = 0;
		using (List<PowerTask>.Enumerator enumerator = this.m_taskList.GetTaskList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetPower().Type == Network.PowerType.SHOW_ENTITY)
				{
					return num;
				}
				num++;
			}
		}
		return 0;
	}

	// Token: 0x06006F01 RID: 28417 RVA: 0x0023C438 File Offset: 0x0023A638
	private IEnumerator DoActionWithTiming()
	{
		yield return base.StartCoroutine(this.DoMoveEffects());
		yield return base.StartCoroutine(this.CompleteTasksUntilDraw());
		yield break;
	}

	// Token: 0x06006F02 RID: 28418 RVA: 0x0023C447 File Offset: 0x0023A647
	private IEnumerator DoMoveEffects()
	{
		if (this.m_friendlyActors.Count > 0)
		{
			while (GameState.Get().GetFriendlyCardBeingDrawn())
			{
				yield return null;
			}
			while (GameState.Get().GetFriendlySidePlayer().GetHandZone().IsUpdatingLayout())
			{
				yield return null;
			}
			this.AnimateSpread(this.m_friendlyActors);
		}
		if (this.m_opponentActors.Count > 0)
		{
			while (GameState.Get().GetOpponentCardBeingDrawn())
			{
				yield return null;
			}
			while (GameState.Get().GetOpposingSidePlayer().GetHandZone().IsUpdatingLayout())
			{
				yield return null;
			}
			this.AnimateSpread(this.m_opponentActors);
		}
		while (this.m_friendlyActors.Count > 0 || this.m_opponentActors.Count > 0)
		{
			yield return null;
		}
		InputManager.Get().EnableInput();
		yield break;
	}

	// Token: 0x06006F03 RID: 28419 RVA: 0x0023C458 File Offset: 0x0023A658
	private void AnimateSpread(List<Actor> actors)
	{
		for (int i = 0; i < actors.Count; i++)
		{
			float waitSec = (float)(actors.Count - i - 1) * this.m_MoveToDeckInterval;
			base.StartCoroutine(this.AnimateActor(actors, actors[i], waitSec));
		}
	}

	// Token: 0x06006F04 RID: 28420 RVA: 0x0023C49F File Offset: 0x0023A69F
	private IEnumerator AnimateActor(List<Actor> actors, Actor actor, float waitSec)
	{
		Card card = actor.GetCard();
		Player player = card.GetController();
		ZoneDeck deck = player.GetDeckZone();
		actor.Show();
		float num = player.IsFriendlySide() ? this.m_MoveUpOffsetZ : (-this.m_MoveUpOffsetZ);
		Vector3 position = new Vector3(card.transform.position.x, card.transform.position.y, card.transform.position.z + num);
		iTween.MoveTo(card.gameObject, position, this.m_MoveUpTime);
		iTween.ScaleTo(card.gameObject, card.transform.localScale * this.m_MoveUpScale, this.m_MoveUpTime);
		yield return new WaitForSeconds(this.m_MoveUpTime + waitSec);
		bool hideBackSide = !player.IsFriendlySide();
		yield return base.StartCoroutine(actor.GetCard().AnimatePlayToDeck(actor.gameObject, deck, hideBackSide, 1f));
		actors.Remove(actor);
		yield break;
	}

	// Token: 0x06006F05 RID: 28421 RVA: 0x0023C4C3 File Offset: 0x0023A6C3
	private IEnumerator CompleteTasksUntilDraw()
	{
		int num = this.FindTaskCountToRun();
		if (num <= 0)
		{
			this.m_effectsPendingFinish--;
			base.FinishIfPossible();
			yield break;
		}
		bool complete = false;
		this.m_taskList.DoTasks(0, num, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x04005905 RID: 22789
	public float m_MoveUpTime;

	// Token: 0x04005906 RID: 22790
	public float m_MoveUpOffsetZ;

	// Token: 0x04005907 RID: 22791
	public float m_MoveUpScale;

	// Token: 0x04005908 RID: 22792
	public float m_MoveToDeckInterval;

	// Token: 0x04005909 RID: 22793
	private List<Actor> m_friendlyActors = new List<Actor>();

	// Token: 0x0400590A RID: 22794
	private List<Actor> m_opponentActors = new List<Actor>();
}
