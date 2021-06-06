using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006CE RID: 1742
public class SecretSpellController : SpellController
{
	// Token: 0x06006192 RID: 24978 RVA: 0x001FDE90 File Offset: 0x001FC090
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

	// Token: 0x06006193 RID: 24979 RVA: 0x001FDEF6 File Offset: 0x001FC0F6
	protected override void OnProcessTaskList()
	{
		base.GetSource().SetSecretTriggered(true);
		if (this.m_taskList.IsStartOfBlock())
		{
			this.FireSecretActorSpell();
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

	// Token: 0x06006194 RID: 24980 RVA: 0x001FDF30 File Offset: 0x001FC130
	private bool FireSecretActorSpell()
	{
		Card source = base.GetSource();
		if (!source.CanShowSecretTrigger())
		{
			return false;
		}
		source.ShowSecretTrigger();
		return true;
	}

	// Token: 0x06006195 RID: 24981 RVA: 0x001FDF58 File Offset: 0x001FC158
	private Spell GetTriggerSpell(Card card)
	{
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		return card.GetTriggerSpell(blockStart.EffectIndex, true);
	}

	// Token: 0x06006196 RID: 24982 RVA: 0x001FDF80 File Offset: 0x001FC180
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

	// Token: 0x06006197 RID: 24983 RVA: 0x001FDFD0 File Offset: 0x001FC1D0
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

	// Token: 0x06006198 RID: 24984 RVA: 0x001FDA9A File Offset: 0x001FBC9A
	private void OnTriggerSpellFinished(Spell triggerSpell, object userData)
	{
		this.OnFinishedTaskList();
	}

	// Token: 0x06006199 RID: 24985 RVA: 0x001FE03D File Offset: 0x001FC23D
	private void OnTriggerSpellStateFinished(Spell triggerSpell, SpellStateType prevStateType, object userData)
	{
		if (triggerSpell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.OnFinished();
	}

	// Token: 0x0600619A RID: 24986 RVA: 0x001FE050 File Offset: 0x001FC250
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
				SecretBannerDef secretBannerDef = this.m_BannerDefs[i];
				if (spellClassTag == secretBannerDef.m_HeroClass)
				{
					return secretBannerDef.m_SpellPrefab;
				}
			}
			Log.Asset.Print(string.Format("{0}.DetermineBannerSpellPrefab() - class type {1} has no Banner Def. Using default banner.", this, spellClassTag), Array.Empty<object>());
		}
		return this.m_DefaultBannerSpellPrefab;
	}

	// Token: 0x0600619B RID: 24987 RVA: 0x001FE100 File Offset: 0x001FC300
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

	// Token: 0x0600619C RID: 24988 RVA: 0x001FE13C File Offset: 0x001FC33C
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

	// Token: 0x0600619D RID: 24989 RVA: 0x001FE189 File Offset: 0x001FC389
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

	// Token: 0x0600619E RID: 24990 RVA: 0x001FE198 File Offset: 0x001FC398
	private void OnBannerSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_bannerSpell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_bannerSpell.gameObject);
		this.m_bannerSpell = null;
	}

	// Token: 0x04005161 RID: 20833
	public List<SecretBannerDef> m_BannerDefs;

	// Token: 0x04005162 RID: 20834
	public Spell m_DefaultBannerSpellPrefab;

	// Token: 0x04005163 RID: 20835
	private Spell m_bannerSpell;

	// Token: 0x04005164 RID: 20836
	private Spell m_triggerSpell;
}
