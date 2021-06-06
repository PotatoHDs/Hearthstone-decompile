using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D0 RID: 1744
public class SideQuestSpellController : SpellController
{
	// Token: 0x060061A2 RID: 24994 RVA: 0x001FE1C0 File Offset: 0x001FC3C0
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		Entity sourceEntity = taskList.GetSourceEntity(true);
		Card card = sourceEntity.GetCard();
		bool flag = false;
		if (taskList.IsStartOfBlock() && this.InitBannerSpell(sourceEntity))
		{
			flag = true;
		}
		Spell triggerSpell = this.GetTriggerSpell(card);
		if (triggerSpell != null && this.InitTriggerSpell(card, triggerSpell))
		{
			flag = true;
		}
		if (!flag)
		{
			return false;
		}
		base.SetSource(card);
		return true;
	}

	// Token: 0x060061A3 RID: 24995 RVA: 0x001FE226 File Offset: 0x001FC426
	protected override void OnProcessTaskList()
	{
		base.GetSource().SetSecretTriggered(true);
		if (this.m_taskList.IsStartOfBlock())
		{
			this.FireSideQuestActorSpell();
			if (this.FireBannerSpell())
			{
				return;
			}
		}
		if (this.FireTriggerSpell())
		{
			return;
		}
		base.OnProcessTaskList();
	}

	// Token: 0x060061A4 RID: 24996 RVA: 0x001FE260 File Offset: 0x001FC460
	private bool FireSideQuestActorSpell()
	{
		base.GetSource().UpdateSideQuestUI(true);
		return true;
	}

	// Token: 0x060061A5 RID: 24997 RVA: 0x001FE270 File Offset: 0x001FC470
	private Spell GetTriggerSpell(Card card)
	{
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		return card.GetTriggerSpell(blockStart.EffectIndex, true);
	}

	// Token: 0x060061A6 RID: 24998 RVA: 0x001FE298 File Offset: 0x001FC498
	private bool InitTriggerSpell(Card card, Spell triggerSpell)
	{
		if (!triggerSpell.AttachPowerTaskList(this.m_taskList))
		{
			Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
			Log.Power.Print(string.Format("{0}.InitTriggerSpell() - FAILED to attach task list to trigger spell {1} for {2}", this, blockStart.EffectIndex, card), Array.Empty<object>());
			return false;
		}
		return true;
	}

	// Token: 0x060061A7 RID: 24999 RVA: 0x001FE2E8 File Offset: 0x001FC4E8
	private bool FireTriggerSpell()
	{
		Card source = base.GetSource();
		Spell triggerSpell = this.GetTriggerSpell(source);
		if (triggerSpell == null)
		{
			return false;
		}
		if (triggerSpell.GetPowerTaskList() != this.m_taskList && !this.InitTriggerSpell(source, triggerSpell))
		{
			return false;
		}
		triggerSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnTriggerSpellFinished));
		triggerSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnTriggerSpellStateFinished));
		triggerSpell.SafeActivateState(SpellStateType.ACTION);
		return true;
	}

	// Token: 0x060061A8 RID: 25000 RVA: 0x001FDA9A File Offset: 0x001FBC9A
	private void OnTriggerSpellFinished(Spell triggerSpell, object userData)
	{
		this.OnFinishedTaskList();
	}

	// Token: 0x060061A9 RID: 25001 RVA: 0x001FE03D File Offset: 0x001FC23D
	private void OnTriggerSpellStateFinished(Spell triggerSpell, SpellStateType prevStateType, object userData)
	{
		if (triggerSpell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.OnFinished();
	}

	// Token: 0x060061AA RID: 25002 RVA: 0x001FE358 File Offset: 0x001FC558
	private Spell DetermineBannerSpellPrefab(Entity sourceEntity)
	{
		if (this.m_BannerDefs == null)
		{
			return null;
		}
		TAG_CLASS @class = sourceEntity.GetClass();
		SpellClassTag spellClassTag = SpellUtils.ConvertClassTagToSpellEnum(@class);
		if (spellClassTag == SpellClassTag.NONE)
		{
			Debug.LogWarning(string.Format("{0}.DetermineBannerSpellPrefab() - entity {1} has unknown class tag {2}. Using default banner.", this, sourceEntity, @class));
		}
		else if (this.m_BannerDefs != null && this.m_BannerDefs.Count > 0)
		{
			for (int i = 0; i < this.m_BannerDefs.Count; i++)
			{
				SideQuestBannerDef sideQuestBannerDef = this.m_BannerDefs[i];
				if (spellClassTag == sideQuestBannerDef.m_HeroClass)
				{
					return sideQuestBannerDef.m_SpellPrefab;
				}
			}
			Log.Asset.Print(string.Format("{0}.DetermineBannerSpellPrefab() - class type {1} has no Banner Def. Using default banner.", this, spellClassTag), Array.Empty<object>());
		}
		return this.m_DefaultBannerSpellPrefab;
	}

	// Token: 0x060061AB RID: 25003 RVA: 0x001FE408 File Offset: 0x001FC608
	private bool InitBannerSpell(Entity sourceEntity)
	{
		Spell spell = this.DetermineBannerSpellPrefab(sourceEntity);
		if (spell == null)
		{
			return false;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(spell.gameObject);
		this.m_bannerSpell = gameObject.GetComponent<Spell>();
		return true;
	}

	// Token: 0x060061AC RID: 25004 RVA: 0x001FE444 File Offset: 0x001FC644
	private bool FireBannerSpell()
	{
		if (this.m_bannerSpell == null)
		{
			return false;
		}
		base.StartCoroutine(this.ContinueWithSecretEvents());
		this.m_bannerSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnBannerSpellStateFinished));
		this.m_bannerSpell.Activate();
		return true;
	}

	// Token: 0x060061AD RID: 25005 RVA: 0x001FE491 File Offset: 0x001FC691
	private IEnumerator ContinueWithSecretEvents()
	{
		yield return new WaitForSeconds(1f);
		while (!HistoryManager.Get().HasBigCard())
		{
			yield return null;
		}
		HistoryManager.Get().NotifyOfSecretSpellFinished();
		yield return new WaitForSeconds(1f);
		if (this.FireTriggerSpell())
		{
			yield break;
		}
		base.OnProcessTaskList();
		yield break;
	}

	// Token: 0x060061AE RID: 25006 RVA: 0x001FE4A0 File Offset: 0x001FC6A0
	private void OnBannerSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_bannerSpell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_bannerSpell.gameObject);
		this.m_bannerSpell = null;
	}

	// Token: 0x04005167 RID: 20839
	public List<SideQuestBannerDef> m_BannerDefs;

	// Token: 0x04005168 RID: 20840
	public Spell m_DefaultBannerSpellPrefab;

	// Token: 0x04005169 RID: 20841
	private Spell m_bannerSpell;

	// Token: 0x0400516A RID: 20842
	private Spell m_triggerSpell;
}
