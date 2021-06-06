using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C9 RID: 1737
public class PowerSpellController : SpellController
{
	// Token: 0x0600614F RID: 24911 RVA: 0x001FC408 File Offset: 0x001FA608
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		Entity sourceEntity = taskList.GetSourceEntity(true);
		Card card = sourceEntity.GetCard();
		CardEffect orCreateEffect = PowerSpellController.GetOrCreateEffect(card, this.m_taskList);
		if (orCreateEffect == null)
		{
			return false;
		}
		if (sourceEntity.IsMinion() || sourceEntity.IsHero())
		{
			if (!this.InitPowerSpell(orCreateEffect, card))
			{
				if (!SpellUtils.CanAddPowerTargets(taskList))
				{
					this.Reset();
					return false;
				}
				if (this.GetActorBattlecrySpell(card) == null)
				{
					this.Reset();
					return false;
				}
			}
		}
		else
		{
			this.InitPowerSpell(orCreateEffect, card);
			this.InitPowerSounds(orCreateEffect, card);
			if (this.m_powerSpell == null && this.m_powerSoundSpells.Count == 0)
			{
				this.Reset();
				return false;
			}
		}
		base.SetSource(card);
		return true;
	}

	// Token: 0x06006150 RID: 24912 RVA: 0x001FC4BF File Offset: 0x001FA6BF
	protected override void OnProcessTaskList()
	{
		if (this.ActivateActorBattlecrySpell())
		{
			return;
		}
		if (this.ActivateCardEffects())
		{
			return;
		}
		base.OnProcessTaskList();
	}

	// Token: 0x06006151 RID: 24913 RVA: 0x001FC4D9 File Offset: 0x001FA6D9
	protected override void OnFinished()
	{
		if (this.m_processingTaskList)
		{
			this.m_pendingFinish = true;
			return;
		}
		base.StartCoroutine(this.WaitThenFinish());
	}

	// Token: 0x06006152 RID: 24914 RVA: 0x001FC4F8 File Offset: 0x001FA6F8
	public override bool ShouldReconnectIfStuck()
	{
		if (this.m_powerSpell != null)
		{
			return this.m_powerSpell.ShouldReconnectIfStuck();
		}
		return base.ShouldReconnectIfStuck();
	}

	// Token: 0x06006153 RID: 24915 RVA: 0x001FC51C File Offset: 0x001FA71C
	private void Reset()
	{
		if (this.m_powerSpell != null && this.m_powerSpell.GetPowerTaskList().GetId() == this.m_taskListId)
		{
			SpellUtils.PurgeSpell(this.m_powerSpell);
		}
		if (this.m_powerSoundSpells != null)
		{
			for (int i = 0; i < this.m_powerSoundSpells.Count; i++)
			{
				CardSoundSpell cardSoundSpell = this.m_powerSoundSpells[i];
				if (cardSoundSpell != null && cardSoundSpell.GetPowerTaskList().GetId() == this.m_taskListId)
				{
					SpellUtils.PurgeSpell(cardSoundSpell);
				}
			}
		}
		this.m_powerSpell = null;
		this.m_powerSoundSpells.Clear();
		this.m_cardEffectsBlockingFinish = 0;
		this.m_cardEffectsBlockingTaskListFinish = 0;
	}

	// Token: 0x06006154 RID: 24916 RVA: 0x001FC5C7 File Offset: 0x001FA7C7
	private IEnumerator WaitThenFinish()
	{
		yield return new WaitForSeconds(10f);
		this.Reset();
		base.OnFinished();
		yield break;
	}

	// Token: 0x06006155 RID: 24917 RVA: 0x001FC5D8 File Offset: 0x001FA7D8
	private Spell GetActorBattlecrySpell(Card card)
	{
		Spell actorSpell = card.GetActorSpell(SpellType.BATTLECRY, true);
		if (actorSpell == null)
		{
			return null;
		}
		if (!actorSpell.HasUsableState(SpellStateType.ACTION))
		{
			return null;
		}
		return actorSpell;
	}

	// Token: 0x06006156 RID: 24918 RVA: 0x001FC608 File Offset: 0x001FA808
	private bool ActivateActorBattlecrySpell()
	{
		Card source = base.GetSource();
		Entity entity = source.GetEntity();
		if (!this.CanActivateActorBattlecrySpell(entity))
		{
			return false;
		}
		Spell actorBattlecrySpell = this.GetActorBattlecrySpell(source);
		if (actorBattlecrySpell == null)
		{
			return false;
		}
		this.m_taskList.SetActivateBattlecrySpellState();
		base.StartCoroutine(this.WaitThenActivateActorBattlecrySpell(actorBattlecrySpell));
		return true;
	}

	// Token: 0x06006157 RID: 24919 RVA: 0x001FC65C File Offset: 0x001FA85C
	private bool CanActivateActorBattlecrySpell(Entity entity)
	{
		return this.m_taskList.ShouldActivateBattlecrySpell() && entity.GetZone() == TAG_ZONE.PLAY && !entity.HasTag(GAME_TAG.FAST_BATTLECRY) && (entity.HasBattlecry() || (entity.HasCombo() && entity.GetController().IsComboActive()));
	}

	// Token: 0x06006158 RID: 24920 RVA: 0x001FC6B4 File Offset: 0x001FA8B4
	private IEnumerator WaitThenActivateActorBattlecrySpell(Spell actorBattlecrySpell)
	{
		yield return new WaitForSeconds(0.2f);
		actorBattlecrySpell.ActivateState(SpellStateType.ACTION);
		if (!this.ActivateCardEffects())
		{
			base.OnProcessTaskList();
		}
		yield break;
	}

	// Token: 0x06006159 RID: 24921 RVA: 0x001FC6CC File Offset: 0x001FA8CC
	public static CardEffect GetOrCreateEffect(Card card, PowerTaskList taskList)
	{
		if (card == null)
		{
			return null;
		}
		CardEffect result = null;
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		string effectCardId = taskList.GetEffectCardId();
		int subOption = blockStart.SubOption;
		int num = blockStart.EffectIndex;
		Entity entity = card.GetEntity();
		string text = (entity != null) ? entity.GetCardId() : null;
		if (!string.IsNullOrEmpty(effectCardId) && !string.IsNullOrEmpty(text) && !(text == effectCardId))
		{
			using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(effectCardId, null))
			{
				CardEffectDef proxyEffectDef;
				if (subOption >= 0)
				{
					if (num > 0)
					{
						if (cardDef.CardDef.m_AdditionalSubOptionEffectDefs == null)
						{
							return null;
						}
						if (subOption >= cardDef.CardDef.m_AdditionalSubOptionEffectDefs.Count)
						{
							return null;
						}
						List<CardEffectDef> list = cardDef.CardDef.m_AdditionalSubOptionEffectDefs[subOption];
						num--;
						if (num >= list.Count)
						{
							return null;
						}
						proxyEffectDef = list[num];
					}
					else
					{
						if (cardDef.CardDef.m_SubOptionEffectDefs == null)
						{
							return null;
						}
						if (subOption >= cardDef.CardDef.m_SubOptionEffectDefs.Count)
						{
							return null;
						}
						proxyEffectDef = cardDef.CardDef.m_SubOptionEffectDefs[subOption];
					}
				}
				else if (num > 0)
				{
					if (cardDef.CardDef.m_AdditionalPlayEffectDefs == null)
					{
						return null;
					}
					num--;
					if (num >= cardDef.CardDef.m_AdditionalPlayEffectDefs.Count)
					{
						return null;
					}
					proxyEffectDef = cardDef.CardDef.m_AdditionalPlayEffectDefs[num];
				}
				else
				{
					proxyEffectDef = cardDef.CardDef.m_PlayEffectDef;
				}
				result = card.GetOrCreateProxyEffect(blockStart, proxyEffectDef);
			}
			return result;
		}
		if (subOption >= 0)
		{
			result = card.GetSubOptionEffect(subOption, num);
		}
		else
		{
			result = card.GetPlayEffect(num);
		}
		return result;
	}

	// Token: 0x0600615A RID: 24922 RVA: 0x001FC8C0 File Offset: 0x001FAAC0
	private bool ActivateCardEffects()
	{
		bool flag = this.ActivatePowerSpell();
		bool flag2 = this.ActivatePowerSounds();
		return flag || flag2;
	}

	// Token: 0x0600615B RID: 24923 RVA: 0x001FC8DC File Offset: 0x001FAADC
	private void OnCardSpellFinished(Spell spell, object userData)
	{
		this.CardSpellFinished();
	}

	// Token: 0x0600615C RID: 24924 RVA: 0x001FC8E4 File Offset: 0x001FAAE4
	private void OnCardSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.CardSpellNoneStateEntered();
	}

	// Token: 0x0600615D RID: 24925 RVA: 0x001FC8F5 File Offset: 0x001FAAF5
	private void CardSpellFinished()
	{
		this.m_cardEffectsBlockingTaskListFinish--;
		if (this.m_cardEffectsBlockingTaskListFinish > 0)
		{
			return;
		}
		this.OnFinishedTaskList();
	}

	// Token: 0x0600615E RID: 24926 RVA: 0x001FC915 File Offset: 0x001FAB15
	private void CardSpellNoneStateEntered()
	{
		this.m_cardEffectsBlockingFinish--;
		if (this.m_cardEffectsBlockingFinish > 0)
		{
			return;
		}
		this.OnFinished();
	}

	// Token: 0x0600615F RID: 24927 RVA: 0x001FC938 File Offset: 0x001FAB38
	private bool InitPowerSpell(CardEffect effect, Card card)
	{
		Spell spell = effect.GetSpell(true);
		if (spell == null)
		{
			return false;
		}
		if (!spell.HasUsableState(SpellStateType.ACTION))
		{
			Log.Power.PrintWarning("{0}.InitPowerSpell() - spell {1} for Card {2} has no {3} state", new object[]
			{
				base.name,
				spell,
				card,
				SpellStateType.ACTION
			});
			return false;
		}
		if (!spell.AttachPowerTaskList(this.m_taskList))
		{
			Log.Power.Print("{0}.InitPowerSpell() - FAILED to attach task list to spell {1} for Card {2}", new object[]
			{
				base.name,
				spell,
				card
			});
			return false;
		}
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			spell.ActivateState(SpellStateType.NONE);
		}
		this.m_powerSpell = spell;
		this.m_cardEffectsBlockingFinish++;
		this.m_cardEffectsBlockingTaskListFinish++;
		return true;
	}

	// Token: 0x06006160 RID: 24928 RVA: 0x001FC9FC File Offset: 0x001FABFC
	private bool ActivatePowerSpell()
	{
		if (this.m_powerSpell == null)
		{
			return false;
		}
		this.m_powerSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnCardSpellFinished));
		this.m_powerSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnCardSpellStateFinished));
		this.m_powerSpell.ActivateState(SpellStateType.ACTION);
		return true;
	}

	// Token: 0x06006161 RID: 24929 RVA: 0x001FCA54 File Offset: 0x001FAC54
	private bool InitPowerSounds(CardEffect effect, Card card)
	{
		List<CardSoundSpell> soundSpells = effect.GetSoundSpells(true);
		if (soundSpells == null)
		{
			return false;
		}
		if (soundSpells.Count == 0)
		{
			return false;
		}
		foreach (CardSoundSpell cardSoundSpell in soundSpells)
		{
			if (cardSoundSpell)
			{
				if (!cardSoundSpell.AttachPowerTaskList(this.m_taskList))
				{
					Log.Power.Print("{0}.InitPowerSounds() - FAILED to attach task list to PowerSoundSpell {1} for Card {2}", new object[]
					{
						base.name,
						cardSoundSpell,
						card
					});
				}
				else
				{
					this.m_powerSoundSpells.Add(cardSoundSpell);
				}
			}
		}
		if (this.m_powerSoundSpells.Count == 0)
		{
			return false;
		}
		this.m_cardEffectsBlockingFinish++;
		this.m_cardEffectsBlockingTaskListFinish++;
		return true;
	}

	// Token: 0x06006162 RID: 24930 RVA: 0x001FCB28 File Offset: 0x001FAD28
	private bool ActivatePowerSounds()
	{
		if (this.m_powerSoundSpells.Count == 0)
		{
			return false;
		}
		Card source = base.GetSource();
		foreach (CardSoundSpell cardSoundSpell in this.m_powerSoundSpells)
		{
			if (cardSoundSpell)
			{
				source.ActivateSoundSpell(cardSoundSpell);
			}
		}
		this.CardSpellFinished();
		this.CardSpellNoneStateEntered();
		return true;
	}

	// Token: 0x04005132 RID: 20786
	private Spell m_powerSpell;

	// Token: 0x04005133 RID: 20787
	private List<CardSoundSpell> m_powerSoundSpells = new List<CardSoundSpell>();

	// Token: 0x04005134 RID: 20788
	private int m_cardEffectsBlockingFinish;

	// Token: 0x04005135 RID: 20789
	private int m_cardEffectsBlockingTaskListFinish;
}
