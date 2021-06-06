using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D2 RID: 1746
public class SigilSpellController : SpellController
{
	// Token: 0x060061B2 RID: 25010 RVA: 0x001FE4C8 File Offset: 0x001FC6C8
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

	// Token: 0x060061B3 RID: 25011 RVA: 0x001FE52E File Offset: 0x001FC72E
	protected override void OnProcessTaskList()
	{
		base.GetSource().SetSecretTriggered(true);
		if (this.m_taskList.IsStartOfBlock())
		{
			this.FireSigilActorSpell();
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

	// Token: 0x060061B4 RID: 25012 RVA: 0x001FE568 File Offset: 0x001FC768
	private bool FireSigilActorSpell()
	{
		Card source = base.GetSource();
		if (!source.CanShowSecretTrigger())
		{
			return false;
		}
		source.ShowSecretTrigger();
		return true;
	}

	// Token: 0x060061B5 RID: 25013 RVA: 0x001FE590 File Offset: 0x001FC790
	private Spell GetTriggerSpell(Card card)
	{
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		return card.GetTriggerSpell(blockStart.EffectIndex, true);
	}

	// Token: 0x060061B6 RID: 25014 RVA: 0x001FE5B8 File Offset: 0x001FC7B8
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

	// Token: 0x060061B7 RID: 25015 RVA: 0x001FE608 File Offset: 0x001FC808
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

	// Token: 0x060061B8 RID: 25016 RVA: 0x001FDA9A File Offset: 0x001FBC9A
	private void OnTriggerSpellFinished(Spell triggerSpell, object userData)
	{
		this.OnFinishedTaskList();
	}

	// Token: 0x060061B9 RID: 25017 RVA: 0x001FE03D File Offset: 0x001FC23D
	private void OnTriggerSpellStateFinished(Spell triggerSpell, SpellStateType prevStateType, object userData)
	{
		if (triggerSpell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.OnFinished();
	}

	// Token: 0x060061BA RID: 25018 RVA: 0x001FE678 File Offset: 0x001FC878
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
				SigilBannerDef sigilBannerDef = this.m_BannerDefs[i];
				if (spellClassTag == sigilBannerDef.m_HeroClass)
				{
					return sigilBannerDef.m_SpellPrefab;
				}
			}
			Log.Asset.Print(string.Format("{0}.DetermineBannerSpellPrefab() - class type {1} has no Banner Def. Using default banner.", this, spellClassTag), Array.Empty<object>());
		}
		return this.m_DefaultBannerSpellPrefab;
	}

	// Token: 0x060061BB RID: 25019 RVA: 0x001FE728 File Offset: 0x001FC928
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

	// Token: 0x060061BC RID: 25020 RVA: 0x001FE764 File Offset: 0x001FC964
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

	// Token: 0x060061BD RID: 25021 RVA: 0x001FE7B1 File Offset: 0x001FC9B1
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

	// Token: 0x060061BE RID: 25022 RVA: 0x001FE7C0 File Offset: 0x001FC9C0
	private void OnBannerSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_bannerSpell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_bannerSpell.gameObject);
		this.m_bannerSpell = null;
	}

	// Token: 0x0400516D RID: 20845
	public List<SigilBannerDef> m_BannerDefs;

	// Token: 0x0400516E RID: 20846
	public Spell m_DefaultBannerSpellPrefab;

	// Token: 0x0400516F RID: 20847
	private Spell m_bannerSpell;

	// Token: 0x04005170 RID: 20848
	private Spell m_triggerSpell;
}
