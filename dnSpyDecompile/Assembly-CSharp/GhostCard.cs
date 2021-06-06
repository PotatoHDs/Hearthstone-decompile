using System;
using System.Collections.Generic;
using Hearthstone.Extensions;
using UnityEngine;

// Token: 0x02000A34 RID: 2612
public class GhostCard : MonoBehaviour
{
	// Token: 0x06008C8C RID: 35980 RVA: 0x002CF6EC File Offset: 0x002CD8EC
	public static GhostCard.Type GetGhostTypeFromSlot(CollectionDeck deck, CollectionDeckSlot slot)
	{
		if (deck == null || slot == null)
		{
			return GhostCard.Type.NONE;
		}
		CollectionDeck.SlotStatus slotStatus = deck.GetSlotStatus(slot);
		if (slotStatus == CollectionDeck.SlotStatus.NOT_VALID)
		{
			return GhostCard.Type.NOT_VALID;
		}
		if (slotStatus == CollectionDeck.SlotStatus.MISSING)
		{
			return GhostCard.Type.MISSING;
		}
		return GhostCard.Type.NONE;
	}

	// Token: 0x06008C8D RID: 35981 RVA: 0x002CF718 File Offset: 0x002CD918
	private void Awake()
	{
		this.m_R2T_BaseCard = base.GetComponent<RenderToTexture>();
		this.m_R2T_BaseCard_OrigHideRenderObject = this.m_R2T_BaseCard.m_HideRenderObject;
		if (GhostCard.s_ghostStyles == null && AssetLoader.Get() != null)
		{
			GhostCard.s_ghostStyles = AssetLoader.Get().InstantiatePrefab("GhostStyleDef.prefab:932fbc50238e04673aeb0f59c9cfaed1", AssetLoadingOptions.None).GetComponent<GhostStyleDef>();
		}
	}

	// Token: 0x06008C8E RID: 35982 RVA: 0x002CF775 File Offset: 0x002CD975
	private void OnDisable()
	{
		this.Disable();
	}

	// Token: 0x06008C8F RID: 35983 RVA: 0x002CF780 File Offset: 0x002CD980
	private void OnDestroy()
	{
		this.DropMaterialReferences();
		if (this.m_EffectRoot)
		{
			ParticleSystem componentInChildren = this.m_EffectRoot.GetComponentInChildren<ParticleSystem>();
			if (componentInChildren)
			{
				componentInChildren.Stop();
			}
		}
	}

	// Token: 0x06008C90 RID: 35984 RVA: 0x002CF7BA File Offset: 0x002CD9BA
	public void SetBigCard(bool isBigCard)
	{
		this.m_isBigCard = isBigCard;
	}

	// Token: 0x06008C91 RID: 35985 RVA: 0x002CF7C3 File Offset: 0x002CD9C3
	public void SetGhostType(GhostCard.Type ghostType)
	{
		this.m_ghostType = ghostType;
	}

	// Token: 0x06008C92 RID: 35986 RVA: 0x002CF7CC File Offset: 0x002CD9CC
	public void SetPremium(TAG_PREMIUM premium)
	{
		this.m_ghostPremium = premium;
	}

	// Token: 0x06008C93 RID: 35987 RVA: 0x002CF7D5 File Offset: 0x002CD9D5
	public void SetRenderQueue(int renderQueue)
	{
		this.m_renderQueue = renderQueue;
	}

	// Token: 0x06008C94 RID: 35988 RVA: 0x002CF7DE File Offset: 0x002CD9DE
	public void RenderGhostCard()
	{
		this.RenderGhostCard(false);
	}

	// Token: 0x06008C95 RID: 35989 RVA: 0x002CF7E7 File Offset: 0x002CD9E7
	public void RenderGhostCard(bool forceRender)
	{
		this.RenderGhost(forceRender);
	}

	// Token: 0x06008C96 RID: 35990 RVA: 0x002CF7F0 File Offset: 0x002CD9F0
	public void Reset()
	{
		this.m_Init = false;
	}

	// Token: 0x06008C97 RID: 35991 RVA: 0x002CF7F9 File Offset: 0x002CD9F9
	private void RenderGhost()
	{
		this.RenderGhost(false);
	}

	// Token: 0x06008C98 RID: 35992 RVA: 0x002CF804 File Offset: 0x002CDA04
	private void RenderGhost(bool forceRender)
	{
		this.Init(forceRender);
		bool flag = this.m_ghostType != GhostCard.Type.DORMANT && (this.PLATFORM_ALLOWS_RENDER_TO_TEXTURE || this.MEMORY_ENABLES_RENDER_TO_TEXTURE);
		if (flag)
		{
			this.m_R2T_BaseCard.enabled = true;
			this.m_R2T_BaseCard.m_HideRenderObject = this.m_R2T_BaseCard_OrigHideRenderObject;
		}
		else
		{
			this.m_R2T_BaseCard.enabled = false;
			this.m_R2T_BaseCard.m_HideRenderObject = false;
		}
		this.m_R2T_BaseCard.m_RenderQueue = this.m_renderQueue;
		if (this.m_R2T_EffectGhost)
		{
			this.m_R2T_EffectGhost.enabled = true;
			this.m_R2T_EffectGhost.m_RenderQueue = this.m_renderQueue;
		}
		this.m_R2T_BaseCard.m_ObjectToRender = this.m_Actor.GetRootObject();
		this.m_Actor.GetRootObject().transform.localPosition = this.m_CardOffset;
		this.m_Actor.ShowAllText();
		this.ApplyGhostMaterials();
		if (flag)
		{
			this.m_R2T_BaseCard.RenderNow();
		}
		Renderer renderer = null;
		if (this.m_GlowPlane)
		{
			renderer = this.m_GlowPlane.GetComponent<Renderer>();
			renderer.enabled = false;
		}
		Renderer renderer2 = null;
		if (this.m_GlowPlaneElite)
		{
			renderer2 = this.m_GlowPlaneElite.GetComponent<Renderer>();
			renderer2.enabled = false;
		}
		if (renderer && !this.m_Actor.IsElite())
		{
			renderer.enabled = true;
			renderer.GetMaterial().renderQueue = 3000 + this.GetGlowPlaneRenderOrderAdjustment();
			renderer.sortingOrder = this.GetGlowPlaneRenderOrderAdjustment();
		}
		if (renderer2 && this.m_Actor.IsElite())
		{
			renderer2.enabled = true;
			renderer2.GetMaterial().renderQueue = 3000 + this.GetGlowPlaneRenderOrderAdjustment();
			renderer2.sortingOrder = this.GetGlowPlaneRenderOrderAdjustment();
		}
		if (this.m_EffectRoot)
		{
			this.m_EffectRoot.transform.parent = null;
			this.m_EffectRoot.transform.position = new Vector3(-500f, -500f, -500f);
			this.m_EffectRoot.transform.localScale = Vector3.one;
			if (this.m_R2T_EffectGhost)
			{
				this.m_R2T_EffectGhost.enabled = true;
				RenderTexture renderTexture = this.m_R2T_EffectGhost.RenderNow();
				if (renderTexture != null)
				{
					this.m_R2T_BaseCard.GetRenderMaterial().SetTexture("_FxTex", renderTexture);
				}
			}
			ParticleSystem componentInChildren = this.m_EffectRoot.GetComponentInChildren<ParticleSystem>();
			if (componentInChildren)
			{
				Renderer component = componentInChildren.GetComponent<Renderer>();
				if (component)
				{
					component.enabled = true;
				}
				componentInChildren.Play();
			}
		}
	}

	// Token: 0x06008C99 RID: 35993 RVA: 0x002CFA98 File Offset: 0x002CDC98
	private int GetGlowPlaneRenderOrderAdjustment()
	{
		GhostCard.Type ghostType = this.m_ghostType;
		if (ghostType == GhostCard.Type.DORMANT)
		{
			return 51;
		}
		return this.m_renderQueue + 1;
	}

	// Token: 0x06008C9A RID: 35994 RVA: 0x002CFABB File Offset: 0x002CDCBB
	public void DisableGhost()
	{
		this.Disable();
		base.enabled = false;
	}

	// Token: 0x06008C9B RID: 35995 RVA: 0x002CFACC File Offset: 0x002CDCCC
	private void Init(bool forceRender)
	{
		if (this.m_Init && !forceRender)
		{
			return;
		}
		if (this.m_Actor == null)
		{
			this.m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
			if (this.m_Actor == null)
			{
				Debug.LogError(string.Format("{0} Ghost card effect failed to find Actor!", base.transform.root.name));
				base.enabled = false;
				return;
			}
		}
		this.m_CardMesh = this.m_Actor.m_cardMesh;
		this.m_CardFrontIdx = this.m_Actor.m_cardFrontMatIdx;
		this.m_PremiumRibbonIdx = this.m_Actor.m_premiumRibbon;
		this.m_PortraitMesh = this.m_Actor.m_portraitMesh;
		this.m_PortraitFrameIdx = this.m_Actor.m_portraitFrameMatIdx;
		this.m_NameMesh = this.m_Actor.m_nameBannerMesh;
		this.m_DescriptionMesh = this.m_Actor.m_descriptionMesh;
		this.m_DescriptionTrimMesh = this.m_Actor.m_descriptionTrimMesh;
		this.m_RarityFrameMesh = this.m_Actor.m_rarityFrameMesh;
		if (this.m_Actor.m_attackObject)
		{
			Renderer component = this.m_Actor.m_attackObject.GetComponent<Renderer>();
			if (component != null)
			{
				this.m_AttackMesh = component.gameObject;
			}
			if (this.m_AttackMesh == null)
			{
				foreach (Renderer renderer in this.m_Actor.m_attackObject.GetComponentsInChildren<Renderer>())
				{
					if (!renderer.GetComponent<UberText>())
					{
						this.m_AttackMesh = renderer.gameObject;
					}
				}
			}
		}
		if (this.m_Actor.m_healthObject)
		{
			Renderer component2 = this.m_Actor.m_healthObject.GetComponent<Renderer>();
			if (component2 != null)
			{
				this.m_HealthMesh = component2.gameObject;
			}
			if (this.m_HealthMesh == null)
			{
				foreach (Renderer renderer2 in this.m_Actor.m_healthObject.GetComponentsInChildren<Renderer>())
				{
					if (!renderer2.GetComponent<UberText>())
					{
						this.m_HealthMesh = renderer2.gameObject;
					}
				}
			}
		}
		this.m_ManaCostMesh = this.m_Actor.m_manaObject;
		this.m_RacePlateMesh = this.m_Actor.m_racePlateObject;
		this.m_EliteMesh = this.m_Actor.m_eliteObject;
		this.StoreOrgMaterials();
		this.m_R2T_BaseCard.m_ObjectToRender = this.m_Actor.GetRootObject();
		if (this.m_R2T_BaseCard.m_Material && this.m_R2T_BaseCard.m_Material.HasProperty("_Seed"))
		{
			this.m_R2T_BaseCard.m_Material.SetFloat("_Seed", UnityEngine.Random.Range(0f, 1f));
		}
		if (this.m_Actor.UsesMultiClassBanner())
		{
			this.m_Actor.GetMultiClassBanner().TurnOffShadowsAndFX();
		}
		this.m_Init = true;
	}

	// Token: 0x06008C9C RID: 35996 RVA: 0x002CFD9C File Offset: 0x002CDF9C
	private void StoreOrgMaterials()
	{
		if (this.m_hasOriginalMaterialsStored)
		{
			this.DropMaterialReferences();
		}
		this.m_hasOriginalMaterialsStored = true;
		MaterialManagerService materialManagerService = GhostCard.GetMaterialManagerService();
		if (this.m_CardMesh)
		{
			if (this.m_CardFrontIdx > -1)
			{
				this.m_OrgMat_CardFront = this.m_CardMesh.GetComponent<Renderer>().GetMaterial(this.m_CardFrontIdx);
				if (materialManagerService != null)
				{
					materialManagerService.KeepMaterial(this.m_OrgMat_CardFront);
				}
			}
			if (this.m_PremiumRibbonIdx > -1)
			{
				this.m_OrgMat_PremiumRibbon = this.m_CardMesh.GetComponent<Renderer>().GetMaterial(this.m_PremiumRibbonIdx);
				if (materialManagerService != null)
				{
					materialManagerService.KeepMaterial(this.m_OrgMat_PremiumRibbon);
				}
			}
		}
		if (this.m_PortraitMesh && this.m_PortraitFrameIdx > -1)
		{
			this.m_OrgMat_PortraitFrame = this.m_PortraitMesh.GetComponent<Renderer>().GetMaterial(this.m_PortraitFrameIdx);
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_PortraitFrame);
			}
		}
		if (this.m_NameMesh)
		{
			this.m_OrgMat_Name = this.m_NameMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_Name);
			}
		}
		if (this.m_ManaCostMesh)
		{
			this.m_OrgMat_ManaCost = this.m_ManaCostMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_ManaCost);
			}
		}
		if (this.m_AttackMesh)
		{
			this.m_OrgMat_Attack = this.m_AttackMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_Attack);
			}
		}
		if (this.m_HealthMesh)
		{
			this.m_OrgMat_Health = this.m_HealthMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_Health);
			}
		}
		if (this.m_RacePlateMesh)
		{
			this.m_OrgMat_RacePlate = this.m_RacePlateMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_RacePlate);
			}
		}
		if (this.m_RarityFrameMesh)
		{
			this.m_OrgMat_RarityFrame = this.m_RarityFrameMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_RarityFrame);
			}
		}
		if (this.m_DescriptionMesh && this.m_DescriptionMesh.GetComponent<Renderer>() != null)
		{
			List<Material> materials = this.m_DescriptionMesh.GetComponent<Renderer>().GetMaterials();
			if (materials.Count > 0)
			{
				this.m_OrgMat_Description = materials[0];
				if (materialManagerService != null)
				{
					materialManagerService.KeepMaterial(this.m_OrgMat_Description);
				}
				if (materials.Count > 1)
				{
					this.m_OrgMat_Description2 = materials[1];
					if (materialManagerService != null)
					{
						materialManagerService.KeepMaterial(this.m_OrgMat_Description2);
					}
				}
			}
		}
		if (this.m_DescriptionTrimMesh)
		{
			this.m_OrgMat_DescriptionTrim = this.m_DescriptionTrimMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_DescriptionTrim);
			}
		}
		if (this.m_EliteMesh)
		{
			this.m_OrgMat_Elite = this.m_EliteMesh.GetComponent<Renderer>().GetMaterial();
			if (materialManagerService != null)
			{
				materialManagerService.KeepMaterial(this.m_OrgMat_Elite);
			}
		}
	}

	// Token: 0x06008C9D RID: 35997 RVA: 0x002D0088 File Offset: 0x002CE288
	private void RestoreOrgMaterials()
	{
		this.ApplyMaterialByIdx(this.m_CardMesh, this.m_OrgMat_CardFront, this.m_CardFrontIdx);
		this.ApplyMaterialByIdx(this.m_CardMesh, this.m_OrgMat_PremiumRibbon, this.m_PremiumRibbonIdx);
		this.ApplyMaterialByIdx(this.m_PortraitMesh, this.m_OrgMat_PortraitFrame, this.m_PortraitFrameIdx);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, this.m_OrgMat_Description, 0);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, this.m_OrgMat_Description2, 1);
		this.ApplyMaterial(this.m_NameMesh, this.m_OrgMat_Name);
		this.ApplyMaterial(this.m_ManaCostMesh, this.m_OrgMat_ManaCost);
		this.ApplyMaterial(this.m_AttackMesh, this.m_OrgMat_Attack);
		this.ApplyMaterial(this.m_HealthMesh, this.m_OrgMat_Health);
		this.ApplyMaterial(this.m_RacePlateMesh, this.m_OrgMat_RacePlate);
		this.ApplyMaterial(this.m_RarityFrameMesh, this.m_OrgMat_RarityFrame);
		this.ApplyMaterial(this.m_DescriptionTrimMesh, this.m_OrgMat_DescriptionTrim);
		this.ApplyMaterial(this.m_EliteMesh, this.m_OrgMat_Elite);
	}

	// Token: 0x06008C9E RID: 35998 RVA: 0x002D0194 File Offset: 0x002CE394
	private void DropMaterialReferences()
	{
		MaterialManagerService materialManagerService = GhostCard.GetMaterialManagerService();
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_CardFront);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_PremiumRibbon);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_PortraitFrame);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_Description);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_Description2);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_Name);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_ManaCost);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_Attack);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_Health);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_RacePlate);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_RarityFrame);
		}
		if (materialManagerService != null)
		{
			materialManagerService.DropMaterial(this.m_OrgMat_DescriptionTrim);
		}
		if (materialManagerService == null)
		{
			return;
		}
		materialManagerService.DropMaterial(this.m_OrgMat_Elite);
	}

	// Token: 0x06008C9F RID: 35999 RVA: 0x002D0290 File Offset: 0x002CE490
	private void ApplyGhostMaterials()
	{
		GhostCard.Type ghostType = this.m_ghostType;
		GhostStyle ghostStyle;
		if (ghostType != GhostCard.Type.NOT_VALID)
		{
			if (ghostType != GhostCard.Type.DORMANT)
			{
				ghostStyle = ((this.m_ghostPremium == TAG_PREMIUM.DIAMOND) ? GhostCard.s_ghostStyles.m_missingDiamond : GhostCard.s_ghostStyles.m_missing);
			}
			else
			{
				ghostStyle = ((this.m_ghostPremium == TAG_PREMIUM.DIAMOND) ? GhostCard.s_ghostStyles.m_dormantDiamond : GhostCard.s_ghostStyles.m_dormant);
			}
		}
		else
		{
			ghostStyle = ((this.m_ghostPremium == TAG_PREMIUM.DIAMOND) ? GhostCard.s_ghostStyles.m_invalidDiamond : GhostCard.s_ghostStyles.m_invalid);
		}
		Material material = null;
		if (ghostStyle.m_GhostCardMaterial != null && !this.m_isBigCard)
		{
			material = UnityEngine.Object.Instantiate<Material>(ghostStyle.m_GhostCardMaterial);
		}
		else if (ghostStyle.m_GhostBigCardMaterial != null && this.m_isBigCard)
		{
			material = UnityEngine.Object.Instantiate<Material>(ghostStyle.m_GhostBigCardMaterial);
		}
		this.m_R2T_BaseCard.m_Material = material;
		if (this.m_R2T_EffectGhost)
		{
			this.m_R2T_EffectGhost.m_Material = material;
		}
		this.ApplyMaterialByIdx(this.m_CardMesh, ghostStyle.m_GhostMaterial, this.m_CardFrontIdx);
		this.ApplyMaterialByIdx(this.m_CardMesh, ghostStyle.m_GhostMaterial, this.m_PremiumRibbonIdx);
		this.ApplyMaterialByIdx(this.m_PortraitMesh, ghostStyle.m_GhostMaterial, this.m_PortraitFrameIdx);
		if (this.m_GlowPlane)
		{
			if (this.m_AttackMesh != null)
			{
				this.m_GlowPlane.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialGlowPlane);
			}
			else
			{
				this.m_GlowPlane.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialAbilityGlowPlane);
			}
		}
		if (this.m_GlowPlaneElite)
		{
			if (this.m_AttackMesh != null)
			{
				this.m_GlowPlaneElite.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialGlowPlane);
			}
			else
			{
				this.m_GlowPlaneElite.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialAbilityGlowPlane);
			}
		}
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, ghostStyle.m_GhostMaterialMod2x, 0);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, ghostStyle.m_GhostMaterial, 1);
		this.ApplyMaterial(this.m_NameMesh, ghostStyle.m_GhostMaterial);
		this.ApplyMaterial(this.m_ManaCostMesh, ghostStyle.m_GhostMaterial);
		this.ApplyMaterial(this.m_AttackMesh, ghostStyle.m_GhostMaterial);
		this.ApplyMaterial(this.m_HealthMesh, ghostStyle.m_GhostMaterial);
		this.ApplyMaterial(this.m_RacePlateMesh, ghostStyle.m_GhostMaterial);
		this.ApplyMaterial(this.m_RarityFrameMesh, ghostStyle.m_GhostMaterial);
		this.ApplyMaterial(this.m_DescriptionTrimMesh, ghostStyle.m_GhostMaterialTransparent);
		this.ApplyMaterial(this.m_EliteMesh, ghostStyle.m_GhostMaterial);
		SceneUtils.SetRenderQueue(base.gameObject, this.m_R2T_BaseCard.m_RenderQueueOffset + this.m_renderQueue, true);
	}

	// Token: 0x06008CA0 RID: 36000 RVA: 0x002D0530 File Offset: 0x002CE730
	private void ApplyMaterial(GameObject go, Material mat)
	{
		if (go == null || mat == null)
		{
			return;
		}
		Renderer component = go.GetComponent<Renderer>();
		Texture mainTexture = component.GetMaterial().mainTexture;
		component.SetMaterial(mat);
		component.GetMaterial().mainTexture = mainTexture;
	}

	// Token: 0x06008CA1 RID: 36001 RVA: 0x002D0574 File Offset: 0x002CE774
	private void ApplyMaterialByIdx(GameObject go, Material mat, int idx)
	{
		if (go == null || mat == null || idx < 0)
		{
			return;
		}
		Renderer component = go.GetComponent<Renderer>();
		if (!component)
		{
			return;
		}
		List<Material> materials = component.GetMaterials();
		if (idx >= materials.Count)
		{
			return;
		}
		Texture mainTexture = materials[idx].mainTexture;
		Texture texture = null;
		Material material = materials[idx];
		if (material == null)
		{
			return;
		}
		if (material.HasProperty("_SecondTex"))
		{
			texture = material.GetTexture("_SecondTex");
		}
		Color value = Color.clear;
		bool flag = material.HasProperty("_SecondTint");
		if (flag)
		{
			value = material.GetColor("_SecondTint");
		}
		materials[idx] = mat;
		component.SetMaterials(materials);
		Material material2 = component.GetMaterial(idx);
		material2.mainTexture = mainTexture;
		if (texture != null)
		{
			material2.SetTexture("_SecondTex", texture);
		}
		if (flag)
		{
			material2.SetColor("_SecondTint", value);
		}
	}

	// Token: 0x06008CA2 RID: 36002 RVA: 0x002D0664 File Offset: 0x002CE864
	private void ApplySharedMaterialByIdx(GameObject go, Material mat, int idx)
	{
		if (go == null || mat == null || idx < 0)
		{
			return;
		}
		Renderer component = go.GetComponent<Renderer>();
		List<Material> materials = component.GetMaterials();
		if (idx >= materials.Count)
		{
			return;
		}
		Texture mainTexture = materials[idx].mainTexture;
		component.SetMaterial(idx, mat);
		component.GetMaterial(idx).mainTexture = mainTexture;
	}

	// Token: 0x06008CA3 RID: 36003 RVA: 0x002D06C4 File Offset: 0x002CE8C4
	private void Disable()
	{
		this.RestoreOrgMaterials();
		if (this.m_R2T_BaseCard)
		{
			this.m_R2T_BaseCard.enabled = false;
		}
		if (this.m_R2T_EffectGhost)
		{
			this.m_R2T_EffectGhost.enabled = false;
		}
		if (this.m_GlowPlane)
		{
			this.m_GlowPlane.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_GlowPlaneElite)
		{
			this.m_GlowPlaneElite.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_EffectRoot)
		{
			ParticleSystem componentInChildren = this.m_EffectRoot.GetComponentInChildren<ParticleSystem>();
			if (componentInChildren)
			{
				componentInChildren.Stop();
				componentInChildren.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	// Token: 0x06008CA4 RID: 36004 RVA: 0x002D0778 File Offset: 0x002CE978
	private static MaterialManagerService GetMaterialManagerService()
	{
		if (GhostCard.s_MaterialManagerService == null)
		{
			GhostCard.s_MaterialManagerService = HearthstoneServices.Get<MaterialManagerService>();
		}
		return GhostCard.s_MaterialManagerService;
	}

	// Token: 0x04007541 RID: 30017
	public Actor m_Actor;

	// Token: 0x04007542 RID: 30018
	public Vector3 m_CardOffset = Vector3.zero;

	// Token: 0x04007543 RID: 30019
	public RenderToTexture m_R2T_EffectGhost;

	// Token: 0x04007544 RID: 30020
	public GameObject m_EffectRoot;

	// Token: 0x04007545 RID: 30021
	public GameObject m_GlowPlane;

	// Token: 0x04007546 RID: 30022
	public GameObject m_GlowPlaneElite;

	// Token: 0x04007547 RID: 30023
	private static GhostStyleDef s_ghostStyles;

	// Token: 0x04007548 RID: 30024
	private static MaterialManagerService s_MaterialManagerService;

	// Token: 0x04007549 RID: 30025
	private bool m_isBigCard;

	// Token: 0x0400754A RID: 30026
	private bool m_Init;

	// Token: 0x0400754B RID: 30027
	private RenderToTexture m_R2T_BaseCard;

	// Token: 0x0400754C RID: 30028
	private bool m_R2T_BaseCard_OrigHideRenderObject;

	// Token: 0x0400754D RID: 30029
	private GhostCard.Type m_ghostType;

	// Token: 0x0400754E RID: 30030
	private TAG_PREMIUM m_ghostPremium;

	// Token: 0x0400754F RID: 30031
	private int m_renderQueue;

	// Token: 0x04007550 RID: 30032
	private GameObject m_CardMesh;

	// Token: 0x04007551 RID: 30033
	private int m_CardFrontIdx;

	// Token: 0x04007552 RID: 30034
	private int m_PremiumRibbonIdx = -1;

	// Token: 0x04007553 RID: 30035
	private GameObject m_PortraitMesh;

	// Token: 0x04007554 RID: 30036
	private int m_PortraitFrameIdx;

	// Token: 0x04007555 RID: 30037
	private GameObject m_NameMesh;

	// Token: 0x04007556 RID: 30038
	private GameObject m_DescriptionMesh;

	// Token: 0x04007557 RID: 30039
	private GameObject m_DescriptionTrimMesh;

	// Token: 0x04007558 RID: 30040
	private GameObject m_RarityFrameMesh;

	// Token: 0x04007559 RID: 30041
	private GameObject m_ManaCostMesh;

	// Token: 0x0400755A RID: 30042
	private GameObject m_AttackMesh;

	// Token: 0x0400755B RID: 30043
	private GameObject m_HealthMesh;

	// Token: 0x0400755C RID: 30044
	private GameObject m_RacePlateMesh;

	// Token: 0x0400755D RID: 30045
	private GameObject m_EliteMesh;

	// Token: 0x0400755E RID: 30046
	private bool m_hasOriginalMaterialsStored;

	// Token: 0x0400755F RID: 30047
	private Material m_OrgMat_CardFront;

	// Token: 0x04007560 RID: 30048
	private Material m_OrgMat_PremiumRibbon;

	// Token: 0x04007561 RID: 30049
	private Material m_OrgMat_PortraitFrame;

	// Token: 0x04007562 RID: 30050
	private Material m_OrgMat_Name;

	// Token: 0x04007563 RID: 30051
	private Material m_OrgMat_Description;

	// Token: 0x04007564 RID: 30052
	private Material m_OrgMat_Description2;

	// Token: 0x04007565 RID: 30053
	private Material m_OrgMat_DescriptionTrim;

	// Token: 0x04007566 RID: 30054
	private Material m_OrgMat_RarityFrame;

	// Token: 0x04007567 RID: 30055
	private Material m_OrgMat_ManaCost;

	// Token: 0x04007568 RID: 30056
	private Material m_OrgMat_Attack;

	// Token: 0x04007569 RID: 30057
	private Material m_OrgMat_Health;

	// Token: 0x0400756A RID: 30058
	private Material m_OrgMat_RacePlate;

	// Token: 0x0400756B RID: 30059
	private Material m_OrgMat_Elite;

	// Token: 0x0400756C RID: 30060
	private readonly PlatformDependentValue<bool> PLATFORM_ALLOWS_RENDER_TO_TEXTURE = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = false,
		Android = true,
		PC = true,
		Mac = true
	};

	// Token: 0x0400756D RID: 30061
	private readonly PlatformDependentValue<bool> MEMORY_ENABLES_RENDER_TO_TEXTURE = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = false,
		MediumMemory = false,
		HighMemory = true
	};

	// Token: 0x020026A2 RID: 9890
	public enum Type
	{
		// Token: 0x0400F153 RID: 61779
		NONE,
		// Token: 0x0400F154 RID: 61780
		MISSING_UNCRAFTABLE,
		// Token: 0x0400F155 RID: 61781
		MISSING,
		// Token: 0x0400F156 RID: 61782
		NOT_VALID,
		// Token: 0x0400F157 RID: 61783
		DORMANT
	}
}
