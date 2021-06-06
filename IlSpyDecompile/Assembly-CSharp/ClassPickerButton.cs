using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using UnityEngine;

public class ClassPickerButton : HeroPickerButton
{
	public GameObject m_questBang;

	private AssetHandle<Texture> m_portraitTexture;

	public override void UpdateDisplay(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		m_heroClass = ((def?.EntityDef != null) ? def.EntityDef.GetClass() : TAG_CLASS.INVALID);
		base.UpdateDisplay(def, premium);
		SetClassname(GameStrings.GetClassName(m_heroClass));
		SetClassIcon(GetClassIconMaterial(m_heroClass));
	}

	protected override void UpdatePortrait()
	{
		if (m_fullDef?.CardDef == null)
		{
			return;
		}
		AssetHandle.Set(ref m_portraitTexture, m_fullDef.CardDef.GetPortraitTextureHandle());
		if (!m_portraitTexture)
		{
			return;
		}
		Material premiumClassMaterial = m_fullDef.CardDef.GetPremiumClassMaterial();
		DeckPickerHero component = GetComponent<DeckPickerHero>();
		Renderer component2 = m_buttonFrame.GetComponent<Renderer>();
		List<Material> materials = component2.GetMaterials();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool num = mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsChallengeTavernBrawl()) || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode());
		bool flag = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO).Find((Achievement a) => a.ClassReward.Value == m_heroClass && a.IsCompleted()) == null;
		if (!num && flag && m_fullDef.CardDef.m_LockedClassPortrait != null)
		{
			materials[component.m_PortraitMaterialIndex] = m_fullDef.CardDef.m_LockedClassPortrait;
		}
		else if (m_premium == TAG_PREMIUM.GOLDEN && premiumClassMaterial != null)
		{
			materials[component.m_PortraitMaterialIndex] = premiumClassMaterial;
			if (!m_seed.HasValue)
			{
				m_seed = Random.value;
			}
			if (materials[component.m_PortraitMaterialIndex].HasProperty("_Seed"))
			{
				materials[component.m_PortraitMaterialIndex].SetFloat("_Seed", m_seed.Value);
			}
			if ((bool)m_fullDef.CardDef.GetPremiumPortraitAnimation())
			{
				UberShaderController uberShaderController = m_buttonFrame.GetComponent<UberShaderController>();
				if (uberShaderController == null)
				{
					uberShaderController = m_buttonFrame.AddComponent<UberShaderController>();
				}
				uberShaderController.UberShaderAnimation = Object.Instantiate(m_fullDef.CardDef.GetPremiumPortraitAnimation());
				uberShaderController.m_MaterialIndex = component.m_PortraitMaterialIndex;
			}
		}
		else
		{
			materials[component.m_PortraitMaterialIndex].mainTexture = m_portraitTexture;
		}
		component2.SetMaterials(materials);
	}

	public override void Lock()
	{
		base.Lock();
		ShowQuestBang(shown: true);
		m_heroClassIcon.SetActive(value: false);
		m_heroClassIconSepia.SetActive(value: true);
	}

	public override void Unlock()
	{
		bool num = IsLocked();
		base.Unlock();
		if (num)
		{
			UpdatePortrait();
		}
		ShowQuestBang(shown: false);
		m_heroClassIcon.SetActive(value: true);
		m_heroClassIconSepia.SetActive(value: false);
	}

	public void ShowQuestBang(bool shown)
	{
		m_questBang.SetActive(shown);
	}

	protected override void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_portraitTexture);
		base.OnDestroy();
	}
}
