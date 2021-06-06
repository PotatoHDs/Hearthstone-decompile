using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007E2 RID: 2018
public class DeadManCopyHandToDeckSpell : SuperSpell
{
	// Token: 0x06006E5F RID: 28255 RVA: 0x002398D8 File Offset: 0x00237AD8
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
			if (!(targetCardFromPowerTask == null) && base.IsValidSpellTarget(targetCardFromPowerTask.GetEntity()) && !this.m_targets.Contains(targetCardFromPowerTask.gameObject))
			{
				this.AddTarget(targetCardFromPowerTask.gameObject);
				targetCardFromPowerTask.SuppressHandToDeckTransition();
				if (this.m_targets.Count == 1)
				{
					this.m_taskCountToRunFirst = i;
				}
			}
		}
		return this.m_targets.Count != 0;
	}

	// Token: 0x06006E60 RID: 28256 RVA: 0x00239994 File Offset: 0x00237B94
	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type == Network.PowerType.TAG_CHANGE)
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
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
				return null;
			}
			if (entity.GetZone() != TAG_ZONE.HAND && entity.GetZone() != TAG_ZONE.SETASIDE)
			{
				return null;
			}
			return entity.GetCard();
		}
		else
		{
			if (power.Type != Network.PowerType.HIDE_ENTITY)
			{
				return null;
			}
			Network.HistHideEntity histHideEntity = (Network.HistHideEntity)power;
			if (histHideEntity.Zone != 2)
			{
				return null;
			}
			Entity entity2 = GameState.Get().GetEntity(histHideEntity.Entity);
			if (entity2 == null)
			{
				return null;
			}
			if (entity2.GetZone() != TAG_ZONE.HAND)
			{
				return null;
			}
			return entity2.GetCard();
		}
	}

	// Token: 0x06006E61 RID: 28257 RVA: 0x00239A48 File Offset: 0x00237C48
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		this.FindEntitiesToDrawBeforeFX();
		this.DoTasks();
		base.StartCoroutine(this.DoActionWithTiming());
	}

	// Token: 0x06006E62 RID: 28258 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void DoActionNow()
	{
	}

	// Token: 0x06006E63 RID: 28259 RVA: 0x00239A78 File Offset: 0x00237C78
	private IEnumerator DoActionWithTiming()
	{
		if (this.m_ShuffleRealHandToDeck)
		{
			yield return base.StartCoroutine(this.WaitForPendingCardDraw());
		}
		yield return base.StartCoroutine(this.WaitForTasksAndDrawing());
		yield return base.StartCoroutine(this.LoadAssets());
		yield return base.StartCoroutine(this.DoEffects());
		yield break;
	}

	// Token: 0x06006E64 RID: 28260 RVA: 0x00239A88 File Offset: 0x00237C88
	private void FindEntitiesToDrawBeforeFX()
	{
		Card sourceCard = base.GetSourceCard();
		this.m_entitiesToDrawBeforeFX.Clear();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < this.m_taskCountToRunFirst; i++)
		{
			PowerTask powerTask = taskList[i];
			if (sourceCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				this.FindRevealedEntitiesToDrawBeforeFX(powerTask.GetPower());
			}
			else
			{
				this.FindRevealedEntitiesToDrawBeforeFX(powerTask.GetPower());
				this.FindHiddenEntitiesToDrawBeforeFX(powerTask.GetPower());
			}
		}
	}

	// Token: 0x06006E65 RID: 28261 RVA: 0x00239AFC File Offset: 0x00237CFC
	private void FindRevealedEntitiesToDrawBeforeFX(Network.PowerHistory power)
	{
		if (power.Type != Network.PowerType.SHOW_ENTITY)
		{
			return;
		}
		Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
		Entity entity = GameState.Get().GetEntity(histShowEntity.Entity.ID);
		if (entity == null)
		{
			return;
		}
		if (entity.GetZone() != TAG_ZONE.DECK)
		{
			return;
		}
		if (histShowEntity.Entity.Tags.Exists((Network.Entity.Tag tag) => tag.Name == 49 && tag.Value == 3))
		{
			this.m_entitiesToDrawBeforeFX.Add(entity);
			return;
		}
		if (histShowEntity.Entity.Tags.Exists((Network.Entity.Tag tag) => tag.Name == 49 && tag.Value == 4))
		{
			this.m_entitiesToDrawBeforeFX.Add(entity);
		}
	}

	// Token: 0x06006E66 RID: 28262 RVA: 0x00239BBC File Offset: 0x00237DBC
	private void FindHiddenEntitiesToDrawBeforeFX(Network.PowerHistory power)
	{
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return;
		}
		Network.HistTagChange histTagChange = (Network.HistTagChange)power;
		if (histTagChange.Tag != 49)
		{
			return;
		}
		if (histTagChange.Value != 3 && histTagChange.Value != 4)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarningFormat("{0}.FindOpponentEntitiesToDrawBeforeFX() - WARNING trying to target entity with id {1} but there is no entity with that id", new object[]
			{
				this,
				histTagChange.Entity
			});
			return;
		}
		if (entity.GetZone() != TAG_ZONE.DECK)
		{
			return;
		}
		this.m_entitiesToDrawBeforeFX.Add(entity);
	}

	// Token: 0x06006E67 RID: 28263 RVA: 0x00239C45 File Offset: 0x00237E45
	private void DoTasks()
	{
		if (this.m_taskCountToRunFirst <= 0)
		{
			this.m_waitForTasksToComplete = false;
			return;
		}
		this.m_waitForTasksToComplete = true;
		this.m_taskList.DoTasks(0, this.m_taskCountToRunFirst, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			this.m_waitForTasksToComplete = false;
		});
	}

	// Token: 0x06006E68 RID: 28264 RVA: 0x00239C7D File Offset: 0x00237E7D
	private IEnumerator LoadAssets()
	{
		this.m_numActorsInLoading = this.m_targets.Count;
		this.m_actors.Clear();
		this.m_friendlyActors.Clear();
		this.m_opposingActors.Clear();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			if (this.m_targets[i].GetComponent<Card>().GetEntity().IsControlledByFriendlySidePlayer())
			{
				num++;
			}
			else
			{
				num2++;
			}
		}
		int num3 = 0;
		int num4 = 0;
		for (int j = 0; j < this.m_targets.Count; j++)
		{
			Entity entity = this.m_targets[j].GetComponent<Card>().GetEntity();
			bool flag = entity.IsControlledByFriendlySidePlayer();
			this.m_actors.Add(null);
			if (flag)
			{
				this.m_friendlyActors.Add(null);
			}
			else
			{
				this.m_opposingActors.Add(null);
			}
			string zoneActor = ActorNames.GetZoneActor(entity, TAG_ZONE.HAND);
			DeadManCopyHandToDeckSpell.ActorCallbackData actorCallbackData = default(DeadManCopyHandToDeckSpell.ActorCallbackData);
			actorCallbackData.targetIndex = j;
			int handIndex;
			if (!flag)
			{
				num4 = (handIndex = num4) + 1;
			}
			else
			{
				num3 = (handIndex = num3) + 1;
			}
			actorCallbackData.handIndex = handIndex;
			actorCallbackData.handSize = (flag ? num : num2);
			DeadManCopyHandToDeckSpell.ActorCallbackData actorCallbackData2 = actorCallbackData;
			AssetLoader.Get().InstantiatePrefab(zoneActor, new PrefabCallback<GameObject>(this.OnActorLoaded), actorCallbackData2, AssetLoadingOptions.IgnorePrefabPosition);
		}
		while (this.m_numActorsInLoading > 0)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006E69 RID: 28265 RVA: 0x00239C8C File Offset: 0x00237E8C
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_numActorsInLoading--;
		DeadManCopyHandToDeckSpell.ActorCallbackData actorCallbackData = (DeadManCopyHandToDeckSpell.ActorCallbackData)callbackData;
		int targetIndex = actorCallbackData.targetIndex;
		int handIndex = actorCallbackData.handIndex;
		int handSize = actorCallbackData.handSize;
		if (go == null)
		{
			Error.AddDevFatal("DeadManCopyHandToDeckSpell.OnActorLoaded() - FAILED to load actor {0} (targetIndex {1})", new object[]
			{
				assetRef,
				targetIndex
			});
			return;
		}
		Actor component = go.GetComponent<Actor>();
		Card component2 = this.m_targets[targetIndex].GetComponent<Card>();
		Entity entity = component2.GetEntity();
		ZoneHand handZone = component2.GetController().GetHandZone();
		component2.SetDoNotSort(true);
		component.SetCard(component2);
		component.SetCardDefFromCard(component2);
		component.SetEntity(entity);
		component.SetEntityDef(entity.GetEntityDef());
		component.SetCardBackSideOverride(new Player.Side?(entity.GetControllerSide()));
		component.UpdateAllComponents();
		component2.transform.position = handZone.GetCardPosition(handIndex, handSize);
		component2.transform.localEulerAngles = handZone.GetCardRotation(handIndex, handSize);
		component2.transform.localScale = handZone.GetCardScale();
		component.Hide();
		this.m_actors[targetIndex] = component;
		if (entity.IsControlledByFriendlySidePlayer())
		{
			this.m_friendlyActors[handIndex] = component;
			return;
		}
		this.m_opposingActors[handIndex] = component;
	}

	// Token: 0x06006E6A RID: 28266 RVA: 0x00239DD2 File Offset: 0x00237FD2
	private IEnumerator WaitForPendingCardDraw()
	{
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			yield break;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			yield break;
		}
		if (entity.IsControlledByFriendlySidePlayer())
		{
			while (GameState.Get().GetFriendlyCardBeingDrawn())
			{
				yield return null;
			}
			while (GameState.Get().GetFriendlySidePlayer().GetHandZone().IsUpdatingLayout())
			{
				yield return null;
			}
		}
		else
		{
			while (GameState.Get().GetOpponentCardBeingDrawn())
			{
				yield return null;
			}
			while (GameState.Get().GetOpposingSidePlayer().GetHandZone().IsUpdatingLayout())
			{
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06006E6B RID: 28267 RVA: 0x00239DE4 File Offset: 0x00237FE4
	private bool IsDrawing()
	{
		foreach (Entity entity in this.m_entitiesToDrawBeforeFX)
		{
			Card card = entity.GetCard();
			TAG_ZONE zone = entity.GetZone();
			if (zone == TAG_ZONE.GRAVEYARD)
			{
				if (!card.IsActorReady())
				{
					return true;
				}
			}
			else if (zone == TAG_ZONE.HAND)
			{
				if (!(card.GetZone() is ZoneHand))
				{
					return true;
				}
				if (card.IsDoNotSort())
				{
					return true;
				}
				if (entity.IsControlledByFriendlySidePlayer())
				{
					if (!card.CardStandInIsInteractive())
					{
						return true;
					}
				}
				else if (card.IsBeingDrawnByOpponent())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06006E6C RID: 28268 RVA: 0x00239E98 File Offset: 0x00238098
	private IEnumerator WaitForTasksAndDrawing()
	{
		while (this.m_waitForTasksToComplete)
		{
			yield return null;
		}
		while (this.IsDrawing())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006E6D RID: 28269 RVA: 0x00239EA8 File Offset: 0x002380A8
	private void CheckHideOriginalHandActors()
	{
		if (!this.m_ShuffleRealHandToDeck)
		{
			return;
		}
		if (this.m_opposingActors.Count > 0)
		{
			foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetHandZone().GetCards())
			{
				card.GetActor().Hide();
			}
		}
		if (this.m_friendlyActors.Count > 0)
		{
			foreach (Card card2 in GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards())
			{
				card2.GetActor().Hide();
			}
		}
	}

	// Token: 0x06006E6E RID: 28270 RVA: 0x00239F84 File Offset: 0x00238184
	private IEnumerator DoEffects()
	{
		base.DoActionNow();
		this.CheckHideOriginalHandActors();
		this.AnimateSpread();
		Actor livingActor = null;
		do
		{
			livingActor = this.m_actors.Find((Actor currActor) => currActor);
			if (livingActor)
			{
				yield return null;
			}
		}
		while (livingActor);
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x06006E6F RID: 28271 RVA: 0x00239F94 File Offset: 0x00238194
	private void AnimateSpread()
	{
		int num = 0;
		while (num < this.m_opposingActors.Count || num < this.m_friendlyActors.Count)
		{
			if (num < this.m_opposingActors.Count)
			{
				float waitSec = (float)(this.m_opposingActors.Count - num - 1) * this.m_MoveToDeckInterval;
				base.StartCoroutine(this.AnimateActor(this.m_opposingActors[num], waitSec));
			}
			if (num < this.m_friendlyActors.Count)
			{
				float waitSec2 = (float)(this.m_friendlyActors.Count - num - 1) * this.m_MoveToDeckInterval;
				base.StartCoroutine(this.AnimateActor(this.m_friendlyActors[num], waitSec2));
			}
			num++;
		}
	}

	// Token: 0x06006E70 RID: 28272 RVA: 0x0023A04E File Offset: 0x0023824E
	private IEnumerator AnimateActor(Actor actor, float waitSec)
	{
		Card card = actor.GetCard();
		Player controller = card.GetController();
		ZoneDeck deck = controller.GetDeckZone();
		actor.Show();
		float num = controller.IsFriendlySide() ? this.m_MoveUpOffsetZ : (-this.m_MoveUpOffsetZ);
		Vector3 position = new Vector3(card.transform.position.x, card.transform.position.y, card.transform.position.z + num);
		iTween.MoveTo(card.gameObject, position, this.m_MoveUpTime);
		iTween.ScaleTo(card.gameObject, card.transform.localScale * this.m_MoveUpScale, this.m_MoveUpTime);
		yield return new WaitForSeconds(this.m_MoveUpTime + waitSec);
		bool hideBackSide = actor.GetEntityDef().GetCardType() == TAG_CARDTYPE.INVALID;
		yield return base.StartCoroutine(actor.GetCard().AnimatePlayToDeck(actor.gameObject, deck, hideBackSide, 1f));
		actor.Destroy();
		card.SetDoNotSort(false);
		yield break;
	}

	// Token: 0x04005895 RID: 22677
	public float m_MoveUpTime;

	// Token: 0x04005896 RID: 22678
	public float m_MoveUpOffsetZ;

	// Token: 0x04005897 RID: 22679
	public float m_MoveUpScale;

	// Token: 0x04005898 RID: 22680
	public float m_MoveToDeckInterval;

	// Token: 0x04005899 RID: 22681
	public bool m_ShuffleRealHandToDeck;

	// Token: 0x0400589A RID: 22682
	private int m_taskCountToRunFirst;

	// Token: 0x0400589B RID: 22683
	private bool m_waitForTasksToComplete;

	// Token: 0x0400589C RID: 22684
	private List<Entity> m_entitiesToDrawBeforeFX = new List<Entity>();

	// Token: 0x0400589D RID: 22685
	private List<Actor> m_actors = new List<Actor>();

	// Token: 0x0400589E RID: 22686
	private List<Actor> m_friendlyActors = new List<Actor>();

	// Token: 0x0400589F RID: 22687
	private List<Actor> m_opposingActors = new List<Actor>();

	// Token: 0x040058A0 RID: 22688
	private int m_numActorsInLoading;

	// Token: 0x0200237C RID: 9084
	private struct ActorCallbackData
	{
		// Token: 0x0400E6E9 RID: 59113
		public int targetIndex;

		// Token: 0x0400E6EA RID: 59114
		public int handIndex;

		// Token: 0x0400E6EB RID: 59115
		public int handSize;
	}
}
