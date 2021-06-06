using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000809 RID: 2057
public class MyraPlaySpell : Spell
{
	// Token: 0x06006F6F RID: 28527 RVA: 0x0023EE60 File Offset: 0x0023D060
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.SetSuppressDeckEmotes(true);
		base.StartCoroutine(this.DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	// Token: 0x06006F70 RID: 28528 RVA: 0x0023EE7D File Offset: 0x0023D07D
	private void SetSuppressDeckEmotes(bool suppress)
	{
		ZoneDeck deckZone = base.GetSourceCard().GetController().GetDeckZone();
		deckZone.SetSuppressEmotes(suppress);
		deckZone.UpdateLayout();
	}

	// Token: 0x06006F71 RID: 28529 RVA: 0x0023EE9B File Offset: 0x0023D09B
	private IEnumerator DoEffectWithTiming()
	{
		this.ActivateBirth();
		this.FindEntitiesToDrawBeforeFX();
		yield return base.StartCoroutine(this.CompleteTasks());
		yield return base.StartCoroutine(this.WaitForDrawing());
		yield return base.StartCoroutine(this.ActivateAction());
		yield break;
	}

	// Token: 0x06006F72 RID: 28530 RVA: 0x0023EEAA File Offset: 0x0023D0AA
	private void ActivateBirth()
	{
		if (this.m_spell == null)
		{
			this.m_spell = UnityEngine.Object.Instantiate<Spell>(this.m_Spell);
			this.m_spell.SetSource(base.GetSource());
		}
		SpellUtils.ActivateBirthIfNecessary(this.m_spell);
	}

	// Token: 0x06006F73 RID: 28531 RVA: 0x0023EEE8 File Offset: 0x0023D0E8
	private void FindEntitiesToDrawBeforeFX()
	{
		Card sourceCard = base.GetSourceCard();
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
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

	// Token: 0x06006F74 RID: 28532 RVA: 0x0023EF70 File Offset: 0x0023D170
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
			entity.GetCard().SetSkipMilling(true);
			this.m_entitiesToDrawBeforeFX.Add(entity);
		}
	}

	// Token: 0x06006F75 RID: 28533 RVA: 0x0023F03C File Offset: 0x0023D23C
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
		if (histTagChange.Value == 4)
		{
			entity.GetCard().SetSkipMilling(true);
		}
		this.m_entitiesToDrawBeforeFX.Add(entity);
	}

	// Token: 0x06006F76 RID: 28534 RVA: 0x0023F0DC File Offset: 0x0023D2DC
	private void SetDrawTimeScale(float scale)
	{
		foreach (Entity entity in this.m_entitiesToDrawBeforeFX)
		{
			entity.GetCard().SetDrawTimeScale(scale);
		}
	}

	// Token: 0x06006F77 RID: 28535 RVA: 0x0023F134 File Offset: 0x0023D334
	private IEnumerator CompleteTasks()
	{
		this.SetDrawTimeScale(1f / this.m_DrawSpeedScale);
		bool complete = false;
		this.m_taskList.DoAllTasks(delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006F78 RID: 28536 RVA: 0x0023F143 File Offset: 0x0023D343
	private IEnumerator WaitForDrawing()
	{
		if (this.m_taskList.GetBlockEnd() == null)
		{
			yield break;
		}
		while (this.IsDrawing())
		{
			yield return null;
		}
		this.SetDrawTimeScale(1f);
		yield break;
	}

	// Token: 0x06006F79 RID: 28537 RVA: 0x0023F154 File Offset: 0x0023D354
	private bool IsDrawing()
	{
		foreach (Entity entity in this.m_entitiesToDrawBeforeFX)
		{
			Card card = entity.GetCard();
			if (entity.GetZone() == TAG_ZONE.HAND && !(card.GetZone() is ZonePlay) && !(card.GetZone() == null))
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

	// Token: 0x06006F7A RID: 28538 RVA: 0x0023F20C File Offset: 0x0023D40C
	private IEnumerator ActivateAction()
	{
		if (this.m_taskList.GetBlockEnd() == null)
		{
			this.OnSpellFinished();
			yield break;
		}
		this.m_spell.ActivateState(SpellStateType.ACTION);
		while (!this.m_spell.IsFinished())
		{
			yield return null;
		}
		this.OnSpellFinished();
		this.SetSuppressDeckEmotes(false);
		while (this.m_spell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(this.m_spell);
		this.m_spell = null;
		base.Deactivate();
		yield break;
	}

	// Token: 0x04005958 RID: 22872
	[SerializeField]
	private Spell m_Spell;

	// Token: 0x04005959 RID: 22873
	[SerializeField]
	private float m_DrawSpeedScale = 1f;

	// Token: 0x0400595A RID: 22874
	private Spell m_spell;

	// Token: 0x0400595B RID: 22875
	private List<Entity> m_entitiesToDrawBeforeFX = new List<Entity>();
}
