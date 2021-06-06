using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000B04 RID: 2820
public class ClassPickerButton : HeroPickerButton
{
	// Token: 0x0600962C RID: 38444 RVA: 0x0030A080 File Offset: 0x00308280
	public override void UpdateDisplay(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		this.m_heroClass = ((((def != null) ? def.EntityDef : null) == null) ? TAG_CLASS.INVALID : def.EntityDef.GetClass());
		base.UpdateDisplay(def, premium);
		base.SetClassname(GameStrings.GetClassName(this.m_heroClass));
		base.SetClassIcon(base.GetClassIconMaterial(this.m_heroClass));
	}

	// Token: 0x0600962D RID: 38445 RVA: 0x0030A0DC File Offset: 0x003082DC
	protected override void UpdatePortrait()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (((fullDef != null) ? fullDef.CardDef : null) == null)
		{
			return;
		}
		AssetHandle.Set<Texture>(ref this.m_portraitTexture, this.m_fullDef.CardDef.GetPortraitTextureHandle());
		if (!this.m_portraitTexture)
		{
			return;
		}
		Material premiumClassMaterial = this.m_fullDef.CardDef.GetPremiumClassMaterial();
		DeckPickerHero component = base.GetComponent<DeckPickerHero>();
		Renderer component2 = this.m_buttonFrame.GetComponent<Renderer>();
		List<Material> materials = component2.GetMaterials();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		int num = (mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsChallengeTavernBrawl()) || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode())) ? 1 : 0;
		bool flag = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO).Find((global::Achievement a) => a.ClassReward.Value == this.m_heroClass && a.IsCompleted()) == null;
		if (num == 0 && flag && this.m_fullDef.CardDef.m_LockedClassPortrait != null)
		{
			materials[component.m_PortraitMaterialIndex] = this.m_fullDef.CardDef.m_LockedClassPortrait;
		}
		else if (this.m_premium == TAG_PREMIUM.GOLDEN && premiumClassMaterial != null)
		{
			materials[component.m_PortraitMaterialIndex] = premiumClassMaterial;
			if (this.m_seed == null)
			{
				this.m_seed = new float?(UnityEngine.Random.value);
			}
			if (materials[component.m_PortraitMaterialIndex].HasProperty("_Seed"))
			{
				materials[component.m_PortraitMaterialIndex].SetFloat("_Seed", this.m_seed.Value);
			}
			if (this.m_fullDef.CardDef.GetPremiumPortraitAnimation())
			{
				UberShaderController uberShaderController = this.m_buttonFrame.GetComponent<UberShaderController>();
				if (uberShaderController == null)
				{
					uberShaderController = this.m_buttonFrame.AddComponent<UberShaderController>();
				}
				uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(this.m_fullDef.CardDef.GetPremiumPortraitAnimation());
				uberShaderController.m_MaterialIndex = component.m_PortraitMaterialIndex;
			}
		}
		else
		{
			materials[component.m_PortraitMaterialIndex].mainTexture = this.m_portraitTexture;
		}
		component2.SetMaterials(materials);
	}

	// Token: 0x0600962E RID: 38446 RVA: 0x0030A2F1 File Offset: 0x003084F1
	public override void Lock()
	{
		base.Lock();
		this.ShowQuestBang(true);
		this.m_heroClassIcon.SetActive(false);
		this.m_heroClassIconSepia.SetActive(true);
	}

	// Token: 0x0600962F RID: 38447 RVA: 0x0030A318 File Offset: 0x00308518
	public override void Unlock()
	{
		bool flag = base.IsLocked();
		base.Unlock();
		if (flag)
		{
			this.UpdatePortrait();
		}
		this.ShowQuestBang(false);
		this.m_heroClassIcon.SetActive(true);
		this.m_heroClassIconSepia.SetActive(false);
	}

	// Token: 0x06009630 RID: 38448 RVA: 0x0030A34D File Offset: 0x0030854D
	public void ShowQuestBang(bool shown)
	{
		this.m_questBang.SetActive(shown);
	}

	// Token: 0x06009631 RID: 38449 RVA: 0x0030A35B File Offset: 0x0030855B
	protected override void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_portraitTexture);
		base.OnDestroy();
	}

	// Token: 0x04007DE3 RID: 32227
	public GameObject m_questBang;

	// Token: 0x04007DE4 RID: 32228
	private AssetHandle<Texture> m_portraitTexture;
}
