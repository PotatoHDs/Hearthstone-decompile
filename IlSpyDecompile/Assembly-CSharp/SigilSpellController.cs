using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilSpellController : SpellController
{
	public List<SigilBannerDef> m_BannerDefs;

	public Spell m_DefaultBannerSpellPrefab;

	private Spell m_bannerSpell;

	private Spell m_triggerSpell;

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!HasSourceCard(taskList))
		{
			return false;
		}
		Entity sourceEntity = taskList.GetSourceEntity();
		Card card = sourceEntity.GetCard();
		bool flag = false;
		if (taskList.IsStartOfBlock() && InitBannerSpell(sourceEntity))
		{
			flag = true;
		}
		Spell triggerSpell = GetTriggerSpell(card);
		if (triggerSpell != null && InitTriggerSpell(card, triggerSpell))
		{
			flag = true;
		}
		if (!flag)
		{
			return false;
		}
		SetSource(card);
		return true;
	}

	protected override void OnProcessTaskList()
	{
		GetSource().SetSecretTriggered(set: true);
		if (m_taskList.IsStartOfBlock())
		{
			FireSigilActorSpell();
			if (FireBannerSpell())
			{
				return;
			}
		}
		if (!FireTriggerSpell())
		{
			base.OnProcessTaskList();
		}
	}

	private bool FireSigilActorSpell()
	{
		Card source = GetSource();
		if (!source.CanShowSecretTrigger())
		{
			return false;
		}
		source.ShowSecretTrigger();
		return true;
	}

	private Spell GetTriggerSpell(Card card)
	{
		Network.HistBlockStart blockStart = m_taskList.GetBlockStart();
		return card.GetTriggerSpell(blockStart.EffectIndex);
	}

	private bool InitTriggerSpell(Card card, Spell triggerSpell)
	{
		if (!triggerSpell.AttachPowerTaskList(m_taskList))
		{
			Network.HistBlockStart blockStart = m_taskList.GetBlockStart();
			Log.Power.Print($"{this}.InitTriggerSpell() - FAILED to attach task list to trigger spell {blockStart.EffectIndex} for {card}");
			return false;
		}
		return true;
	}

	private bool FireTriggerSpell()
	{
		Card source = GetSource();
		Spell triggerSpell = GetTriggerSpell(source);
		if (triggerSpell == null)
		{
			return false;
		}
		if (triggerSpell.GetPowerTaskList() != m_taskList && !InitTriggerSpell(source, triggerSpell))
		{
			return false;
		}
		triggerSpell.AddFinishedCallback(OnTriggerSpellFinished);
		triggerSpell.AddStateFinishedCallback(OnTriggerSpellStateFinished);
		triggerSpell.SafeActivateState(SpellStateType.ACTION);
		return true;
	}

	private void OnTriggerSpellFinished(Spell triggerSpell, object userData)
	{
		OnFinishedTaskList();
	}

	private void OnTriggerSpellStateFinished(Spell triggerSpell, SpellStateType prevStateType, object userData)
	{
		if (triggerSpell.GetActiveState() == SpellStateType.NONE)
		{
			OnFinished();
		}
	}

	private Spell DetermineBannerSpellPrefab(Entity sourceEntity)
	{
		if (m_BannerDefs == null)
		{
			return null;
		}
		TAG_CLASS @class = sourceEntity.GetClass();
		SpellClassTag spellClassTag = SpellUtils.ConvertClassTagToSpellEnum(@class);
		if (spellClassTag == SpellClassTag.NONE)
		{
			Debug.LogWarning($"{this}.DetermineBannerSpellPrefab() - entity {sourceEntity} has unknown class tag {@class}. Using default banner.");
		}
		else if (m_BannerDefs != null && m_BannerDefs.Count > 0)
		{
			for (int i = 0; i < m_BannerDefs.Count; i++)
			{
				SigilBannerDef sigilBannerDef = m_BannerDefs[i];
				if (spellClassTag == sigilBannerDef.m_HeroClass)
				{
					return sigilBannerDef.m_SpellPrefab;
				}
			}
			Log.Asset.Print($"{this}.DetermineBannerSpellPrefab() - class type {spellClassTag} has no Banner Def. Using default banner.");
		}
		return m_DefaultBannerSpellPrefab;
	}

	private bool InitBannerSpell(Entity sourceEntity)
	{
		Spell spell = DetermineBannerSpellPrefab(sourceEntity);
		if (spell == null)
		{
			return false;
		}
		GameObject gameObject = Object.Instantiate(spell.gameObject);
		m_bannerSpell = gameObject.GetComponent<Spell>();
		return true;
	}

	private bool FireBannerSpell()
	{
		if (m_bannerSpell == null)
		{
			return false;
		}
		StartCoroutine(ContinueWithSecretEvents());
		m_bannerSpell.AddStateFinishedCallback(OnBannerSpellStateFinished);
		m_bannerSpell.Activate();
		return true;
	}

	private IEnumerator ContinueWithSecretEvents()
	{
		yield return new WaitForSeconds(1f);
		while (!HistoryManager.Get().HasBigCard())
		{
			yield return null;
		}
		HistoryManager.Get().NotifyOfSecretSpellFinished();
		yield return new WaitForSeconds(1f);
		if (!FireTriggerSpell())
		{
			base.OnProcessTaskList();
		}
	}

	private void OnBannerSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (m_bannerSpell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(m_bannerSpell.gameObject);
			m_bannerSpell = null;
		}
	}
}
