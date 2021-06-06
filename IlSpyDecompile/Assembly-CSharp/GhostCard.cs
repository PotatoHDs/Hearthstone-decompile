using System.Collections.Generic;
using Hearthstone.Extensions;
using UnityEngine;

public class GhostCard : MonoBehaviour
{
	public enum Type
	{
		NONE,
		MISSING_UNCRAFTABLE,
		MISSING,
		NOT_VALID,
		DORMANT
	}

	public Actor m_Actor;

	public Vector3 m_CardOffset = Vector3.zero;

	public RenderToTexture m_R2T_EffectGhost;

	public GameObject m_EffectRoot;

	public GameObject m_GlowPlane;

	public GameObject m_GlowPlaneElite;

	private static GhostStyleDef s_ghostStyles;

	private static MaterialManagerService s_MaterialManagerService;

	private bool m_isBigCard;

	private bool m_Init;

	private RenderToTexture m_R2T_BaseCard;

	private bool m_R2T_BaseCard_OrigHideRenderObject;

	private Type m_ghostType;

	private TAG_PREMIUM m_ghostPremium;

	private int m_renderQueue;

	private GameObject m_CardMesh;

	private int m_CardFrontIdx;

	private int m_PremiumRibbonIdx = -1;

	private GameObject m_PortraitMesh;

	private int m_PortraitFrameIdx;

	private GameObject m_NameMesh;

	private GameObject m_DescriptionMesh;

	private GameObject m_DescriptionTrimMesh;

	private GameObject m_RarityFrameMesh;

	private GameObject m_ManaCostMesh;

	private GameObject m_AttackMesh;

	private GameObject m_HealthMesh;

	private GameObject m_RacePlateMesh;

	private GameObject m_EliteMesh;

	private bool m_hasOriginalMaterialsStored;

	private Material m_OrgMat_CardFront;

	private Material m_OrgMat_PremiumRibbon;

	private Material m_OrgMat_PortraitFrame;

	private Material m_OrgMat_Name;

	private Material m_OrgMat_Description;

	private Material m_OrgMat_Description2;

	private Material m_OrgMat_DescriptionTrim;

	private Material m_OrgMat_RarityFrame;

	private Material m_OrgMat_ManaCost;

	private Material m_OrgMat_Attack;

	private Material m_OrgMat_Health;

	private Material m_OrgMat_RacePlate;

	private Material m_OrgMat_Elite;

	private readonly PlatformDependentValue<bool> PLATFORM_ALLOWS_RENDER_TO_TEXTURE = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = false,
		Android = true,
		PC = true,
		Mac = true
	};

	private readonly PlatformDependentValue<bool> MEMORY_ENABLES_RENDER_TO_TEXTURE = new PlatformDependentValue<bool>(PlatformCategory.Memory)
	{
		LowMemory = false,
		MediumMemory = false,
		HighMemory = true
	};

	public static Type GetGhostTypeFromSlot(CollectionDeck deck, CollectionDeckSlot slot)
	{
		if (deck == null || slot == null)
		{
			return Type.NONE;
		}
		return deck.GetSlotStatus(slot) switch
		{
			CollectionDeck.SlotStatus.MISSING => Type.MISSING, 
			CollectionDeck.SlotStatus.NOT_VALID => Type.NOT_VALID, 
			_ => Type.NONE, 
		};
	}

	private void Awake()
	{
		m_R2T_BaseCard = GetComponent<RenderToTexture>();
		m_R2T_BaseCard_OrigHideRenderObject = m_R2T_BaseCard.m_HideRenderObject;
		if (s_ghostStyles == null && AssetLoader.Get() != null)
		{
			s_ghostStyles = AssetLoader.Get().InstantiatePrefab("GhostStyleDef.prefab:932fbc50238e04673aeb0f59c9cfaed1").GetComponent<GhostStyleDef>();
		}
	}

	private void OnDisable()
	{
		Disable();
	}

	private void OnDestroy()
	{
		DropMaterialReferences();
		if ((bool)m_EffectRoot)
		{
			ParticleSystem componentInChildren = m_EffectRoot.GetComponentInChildren<ParticleSystem>();
			if ((bool)componentInChildren)
			{
				componentInChildren.Stop();
			}
		}
	}

	public void SetBigCard(bool isBigCard)
	{
		m_isBigCard = isBigCard;
	}

	public void SetGhostType(Type ghostType)
	{
		m_ghostType = ghostType;
	}

	public void SetPremium(TAG_PREMIUM premium)
	{
		m_ghostPremium = premium;
	}

	public void SetRenderQueue(int renderQueue)
	{
		m_renderQueue = renderQueue;
	}

	public void RenderGhostCard()
	{
		RenderGhostCard(forceRender: false);
	}

	public void RenderGhostCard(bool forceRender)
	{
		RenderGhost(forceRender);
	}

	public void Reset()
	{
		m_Init = false;
	}

	private void RenderGhost()
	{
		RenderGhost(forceRender: false);
	}

	private void RenderGhost(bool forceRender)
	{
		Init(forceRender);
		bool num = m_ghostType != Type.DORMANT && ((bool)PLATFORM_ALLOWS_RENDER_TO_TEXTURE || (bool)MEMORY_ENABLES_RENDER_TO_TEXTURE);
		if (num)
		{
			m_R2T_BaseCard.enabled = true;
			m_R2T_BaseCard.m_HideRenderObject = m_R2T_BaseCard_OrigHideRenderObject;
		}
		else
		{
			m_R2T_BaseCard.enabled = false;
			m_R2T_BaseCard.m_HideRenderObject = false;
		}
		m_R2T_BaseCard.m_RenderQueue = m_renderQueue;
		if ((bool)m_R2T_EffectGhost)
		{
			m_R2T_EffectGhost.enabled = true;
			m_R2T_EffectGhost.m_RenderQueue = m_renderQueue;
		}
		m_R2T_BaseCard.m_ObjectToRender = m_Actor.GetRootObject();
		m_Actor.GetRootObject().transform.localPosition = m_CardOffset;
		m_Actor.ShowAllText();
		ApplyGhostMaterials();
		if (num)
		{
			m_R2T_BaseCard.RenderNow();
		}
		Renderer renderer = null;
		if ((bool)m_GlowPlane)
		{
			renderer = m_GlowPlane.GetComponent<Renderer>();
			renderer.enabled = false;
		}
		Renderer renderer2 = null;
		if ((bool)m_GlowPlaneElite)
		{
			renderer2 = m_GlowPlaneElite.GetComponent<Renderer>();
			renderer2.enabled = false;
		}
		if ((bool)renderer && !m_Actor.IsElite())
		{
			renderer.enabled = true;
			renderer.GetMaterial().renderQueue = 3000 + GetGlowPlaneRenderOrderAdjustment();
			renderer.sortingOrder = GetGlowPlaneRenderOrderAdjustment();
		}
		if ((bool)renderer2 && m_Actor.IsElite())
		{
			renderer2.enabled = true;
			renderer2.GetMaterial().renderQueue = 3000 + GetGlowPlaneRenderOrderAdjustment();
			renderer2.sortingOrder = GetGlowPlaneRenderOrderAdjustment();
		}
		if (!m_EffectRoot)
		{
			return;
		}
		m_EffectRoot.transform.parent = null;
		m_EffectRoot.transform.position = new Vector3(-500f, -500f, -500f);
		m_EffectRoot.transform.localScale = Vector3.one;
		if ((bool)m_R2T_EffectGhost)
		{
			m_R2T_EffectGhost.enabled = true;
			RenderTexture renderTexture = m_R2T_EffectGhost.RenderNow();
			if (renderTexture != null)
			{
				m_R2T_BaseCard.GetRenderMaterial().SetTexture("_FxTex", renderTexture);
			}
		}
		ParticleSystem componentInChildren = m_EffectRoot.GetComponentInChildren<ParticleSystem>();
		if ((bool)componentInChildren)
		{
			Renderer component = componentInChildren.GetComponent<Renderer>();
			if ((bool)component)
			{
				component.enabled = true;
			}
			componentInChildren.Play();
		}
	}

	private int GetGlowPlaneRenderOrderAdjustment()
	{
		Type ghostType = m_ghostType;
		if (ghostType == Type.DORMANT)
		{
			return 51;
		}
		return m_renderQueue + 1;
	}

	public void DisableGhost()
	{
		Disable();
		base.enabled = false;
	}

	private void Init(bool forceRender)
	{
		if (m_Init && !forceRender)
		{
			return;
		}
		if (m_Actor == null)
		{
			m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
			if (m_Actor == null)
			{
				Debug.LogError($"{base.transform.root.name} Ghost card effect failed to find Actor!");
				base.enabled = false;
				return;
			}
		}
		m_CardMesh = m_Actor.m_cardMesh;
		m_CardFrontIdx = m_Actor.m_cardFrontMatIdx;
		m_PremiumRibbonIdx = m_Actor.m_premiumRibbon;
		m_PortraitMesh = m_Actor.m_portraitMesh;
		m_PortraitFrameIdx = m_Actor.m_portraitFrameMatIdx;
		m_NameMesh = m_Actor.m_nameBannerMesh;
		m_DescriptionMesh = m_Actor.m_descriptionMesh;
		m_DescriptionTrimMesh = m_Actor.m_descriptionTrimMesh;
		m_RarityFrameMesh = m_Actor.m_rarityFrameMesh;
		if ((bool)m_Actor.m_attackObject)
		{
			Renderer component = m_Actor.m_attackObject.GetComponent<Renderer>();
			if (component != null)
			{
				m_AttackMesh = component.gameObject;
			}
			if (m_AttackMesh == null)
			{
				Renderer[] componentsInChildren = m_Actor.m_attackObject.GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer in componentsInChildren)
				{
					if (!renderer.GetComponent<UberText>())
					{
						m_AttackMesh = renderer.gameObject;
					}
				}
			}
		}
		if ((bool)m_Actor.m_healthObject)
		{
			Renderer component2 = m_Actor.m_healthObject.GetComponent<Renderer>();
			if (component2 != null)
			{
				m_HealthMesh = component2.gameObject;
			}
			if (m_HealthMesh == null)
			{
				Renderer[] componentsInChildren = m_Actor.m_healthObject.GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer2 in componentsInChildren)
				{
					if (!renderer2.GetComponent<UberText>())
					{
						m_HealthMesh = renderer2.gameObject;
					}
				}
			}
		}
		m_ManaCostMesh = m_Actor.m_manaObject;
		m_RacePlateMesh = m_Actor.m_racePlateObject;
		m_EliteMesh = m_Actor.m_eliteObject;
		StoreOrgMaterials();
		m_R2T_BaseCard.m_ObjectToRender = m_Actor.GetRootObject();
		if ((bool)m_R2T_BaseCard.m_Material && m_R2T_BaseCard.m_Material.HasProperty("_Seed"))
		{
			m_R2T_BaseCard.m_Material.SetFloat("_Seed", Random.Range(0f, 1f));
		}
		if (m_Actor.UsesMultiClassBanner())
		{
			m_Actor.GetMultiClassBanner().TurnOffShadowsAndFX();
		}
		m_Init = true;
	}

	private void StoreOrgMaterials()
	{
		if (m_hasOriginalMaterialsStored)
		{
			DropMaterialReferences();
		}
		m_hasOriginalMaterialsStored = true;
		MaterialManagerService materialManagerService = GetMaterialManagerService();
		if ((bool)m_CardMesh)
		{
			if (m_CardFrontIdx > -1)
			{
				m_OrgMat_CardFront = m_CardMesh.GetComponent<Renderer>().GetMaterial(m_CardFrontIdx);
				materialManagerService?.KeepMaterial(m_OrgMat_CardFront);
			}
			if (m_PremiumRibbonIdx > -1)
			{
				m_OrgMat_PremiumRibbon = m_CardMesh.GetComponent<Renderer>().GetMaterial(m_PremiumRibbonIdx);
				materialManagerService?.KeepMaterial(m_OrgMat_PremiumRibbon);
			}
		}
		if ((bool)m_PortraitMesh && m_PortraitFrameIdx > -1)
		{
			m_OrgMat_PortraitFrame = m_PortraitMesh.GetComponent<Renderer>().GetMaterial(m_PortraitFrameIdx);
			materialManagerService?.KeepMaterial(m_OrgMat_PortraitFrame);
		}
		if ((bool)m_NameMesh)
		{
			m_OrgMat_Name = m_NameMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_Name);
		}
		if ((bool)m_ManaCostMesh)
		{
			m_OrgMat_ManaCost = m_ManaCostMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_ManaCost);
		}
		if ((bool)m_AttackMesh)
		{
			m_OrgMat_Attack = m_AttackMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_Attack);
		}
		if ((bool)m_HealthMesh)
		{
			m_OrgMat_Health = m_HealthMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_Health);
		}
		if ((bool)m_RacePlateMesh)
		{
			m_OrgMat_RacePlate = m_RacePlateMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_RacePlate);
		}
		if ((bool)m_RarityFrameMesh)
		{
			m_OrgMat_RarityFrame = m_RarityFrameMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_RarityFrame);
		}
		if ((bool)m_DescriptionMesh && m_DescriptionMesh.GetComponent<Renderer>() != null)
		{
			List<Material> materials = m_DescriptionMesh.GetComponent<Renderer>().GetMaterials();
			if (materials.Count > 0)
			{
				m_OrgMat_Description = materials[0];
				materialManagerService?.KeepMaterial(m_OrgMat_Description);
				if (materials.Count > 1)
				{
					m_OrgMat_Description2 = materials[1];
					materialManagerService?.KeepMaterial(m_OrgMat_Description2);
				}
			}
		}
		if ((bool)m_DescriptionTrimMesh)
		{
			m_OrgMat_DescriptionTrim = m_DescriptionTrimMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_DescriptionTrim);
		}
		if ((bool)m_EliteMesh)
		{
			m_OrgMat_Elite = m_EliteMesh.GetComponent<Renderer>().GetMaterial();
			materialManagerService?.KeepMaterial(m_OrgMat_Elite);
		}
	}

	private void RestoreOrgMaterials()
	{
		ApplyMaterialByIdx(m_CardMesh, m_OrgMat_CardFront, m_CardFrontIdx);
		ApplyMaterialByIdx(m_CardMesh, m_OrgMat_PremiumRibbon, m_PremiumRibbonIdx);
		ApplyMaterialByIdx(m_PortraitMesh, m_OrgMat_PortraitFrame, m_PortraitFrameIdx);
		ApplyMaterialByIdx(m_DescriptionMesh, m_OrgMat_Description, 0);
		ApplyMaterialByIdx(m_DescriptionMesh, m_OrgMat_Description2, 1);
		ApplyMaterial(m_NameMesh, m_OrgMat_Name);
		ApplyMaterial(m_ManaCostMesh, m_OrgMat_ManaCost);
		ApplyMaterial(m_AttackMesh, m_OrgMat_Attack);
		ApplyMaterial(m_HealthMesh, m_OrgMat_Health);
		ApplyMaterial(m_RacePlateMesh, m_OrgMat_RacePlate);
		ApplyMaterial(m_RarityFrameMesh, m_OrgMat_RarityFrame);
		ApplyMaterial(m_DescriptionTrimMesh, m_OrgMat_DescriptionTrim);
		ApplyMaterial(m_EliteMesh, m_OrgMat_Elite);
	}

	private void DropMaterialReferences()
	{
		MaterialManagerService materialManagerService = GetMaterialManagerService();
		materialManagerService?.DropMaterial(m_OrgMat_CardFront);
		materialManagerService?.DropMaterial(m_OrgMat_PremiumRibbon);
		materialManagerService?.DropMaterial(m_OrgMat_PortraitFrame);
		materialManagerService?.DropMaterial(m_OrgMat_Description);
		materialManagerService?.DropMaterial(m_OrgMat_Description2);
		materialManagerService?.DropMaterial(m_OrgMat_Name);
		materialManagerService?.DropMaterial(m_OrgMat_ManaCost);
		materialManagerService?.DropMaterial(m_OrgMat_Attack);
		materialManagerService?.DropMaterial(m_OrgMat_Health);
		materialManagerService?.DropMaterial(m_OrgMat_RacePlate);
		materialManagerService?.DropMaterial(m_OrgMat_RarityFrame);
		materialManagerService?.DropMaterial(m_OrgMat_DescriptionTrim);
		materialManagerService?.DropMaterial(m_OrgMat_Elite);
	}

	private void ApplyGhostMaterials()
	{
		GhostStyle ghostStyle = m_ghostType switch
		{
			Type.NOT_VALID => (m_ghostPremium == TAG_PREMIUM.DIAMOND) ? s_ghostStyles.m_invalidDiamond : s_ghostStyles.m_invalid, 
			Type.DORMANT => (m_ghostPremium == TAG_PREMIUM.DIAMOND) ? s_ghostStyles.m_dormantDiamond : s_ghostStyles.m_dormant, 
			_ => (m_ghostPremium == TAG_PREMIUM.DIAMOND) ? s_ghostStyles.m_missingDiamond : s_ghostStyles.m_missing, 
		};
		Material material = null;
		if (ghostStyle.m_GhostCardMaterial != null && !m_isBigCard)
		{
			material = Object.Instantiate(ghostStyle.m_GhostCardMaterial);
		}
		else if (ghostStyle.m_GhostBigCardMaterial != null && m_isBigCard)
		{
			material = Object.Instantiate(ghostStyle.m_GhostBigCardMaterial);
		}
		m_R2T_BaseCard.m_Material = material;
		if ((bool)m_R2T_EffectGhost)
		{
			m_R2T_EffectGhost.m_Material = material;
		}
		ApplyMaterialByIdx(m_CardMesh, ghostStyle.m_GhostMaterial, m_CardFrontIdx);
		ApplyMaterialByIdx(m_CardMesh, ghostStyle.m_GhostMaterial, m_PremiumRibbonIdx);
		ApplyMaterialByIdx(m_PortraitMesh, ghostStyle.m_GhostMaterial, m_PortraitFrameIdx);
		if ((bool)m_GlowPlane)
		{
			if (m_AttackMesh != null)
			{
				m_GlowPlane.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialGlowPlane);
			}
			else
			{
				m_GlowPlane.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialAbilityGlowPlane);
			}
		}
		if ((bool)m_GlowPlaneElite)
		{
			if (m_AttackMesh != null)
			{
				m_GlowPlaneElite.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialGlowPlane);
			}
			else
			{
				m_GlowPlaneElite.GetComponent<Renderer>().SetMaterial(ghostStyle.m_GhostMaterialAbilityGlowPlane);
			}
		}
		ApplyMaterialByIdx(m_DescriptionMesh, ghostStyle.m_GhostMaterialMod2x, 0);
		ApplyMaterialByIdx(m_DescriptionMesh, ghostStyle.m_GhostMaterial, 1);
		ApplyMaterial(m_NameMesh, ghostStyle.m_GhostMaterial);
		ApplyMaterial(m_ManaCostMesh, ghostStyle.m_GhostMaterial);
		ApplyMaterial(m_AttackMesh, ghostStyle.m_GhostMaterial);
		ApplyMaterial(m_HealthMesh, ghostStyle.m_GhostMaterial);
		ApplyMaterial(m_RacePlateMesh, ghostStyle.m_GhostMaterial);
		ApplyMaterial(m_RarityFrameMesh, ghostStyle.m_GhostMaterial);
		ApplyMaterial(m_DescriptionTrimMesh, ghostStyle.m_GhostMaterialTransparent);
		ApplyMaterial(m_EliteMesh, ghostStyle.m_GhostMaterial);
		SceneUtils.SetRenderQueue(base.gameObject, m_R2T_BaseCard.m_RenderQueueOffset + m_renderQueue, includeInactive: true);
	}

	private void ApplyMaterial(GameObject go, Material mat)
	{
		if (!(go == null) && !(mat == null))
		{
			Renderer component = go.GetComponent<Renderer>();
			Texture mainTexture = component.GetMaterial().mainTexture;
			component.SetMaterial(mat);
			component.GetMaterial().mainTexture = mainTexture;
		}
	}

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
		if (!(material == null))
		{
			if (material.HasProperty("_SecondTex"))
			{
				texture = material.GetTexture("_SecondTex");
			}
			Color value = Color.clear;
			bool num = material.HasProperty("_SecondTint");
			if (num)
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
			if (num)
			{
				material2.SetColor("_SecondTint", value);
			}
		}
	}

	private void ApplySharedMaterialByIdx(GameObject go, Material mat, int idx)
	{
		if (!(go == null) && !(mat == null) && idx >= 0)
		{
			Renderer component = go.GetComponent<Renderer>();
			List<Material> materials = component.GetMaterials();
			if (idx < materials.Count)
			{
				Texture mainTexture = materials[idx].mainTexture;
				component.SetMaterial(idx, mat);
				component.GetMaterial(idx).mainTexture = mainTexture;
			}
		}
	}

	private void Disable()
	{
		RestoreOrgMaterials();
		if ((bool)m_R2T_BaseCard)
		{
			m_R2T_BaseCard.enabled = false;
		}
		if ((bool)m_R2T_EffectGhost)
		{
			m_R2T_EffectGhost.enabled = false;
		}
		if ((bool)m_GlowPlane)
		{
			m_GlowPlane.GetComponent<Renderer>().enabled = false;
		}
		if ((bool)m_GlowPlaneElite)
		{
			m_GlowPlaneElite.GetComponent<Renderer>().enabled = false;
		}
		if ((bool)m_EffectRoot)
		{
			ParticleSystem componentInChildren = m_EffectRoot.GetComponentInChildren<ParticleSystem>();
			if ((bool)componentInChildren)
			{
				componentInChildren.Stop();
				componentInChildren.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	private static MaterialManagerService GetMaterialManagerService()
	{
		if (s_MaterialManagerService == null)
		{
			s_MaterialManagerService = HearthstoneServices.Get<MaterialManagerService>();
		}
		return s_MaterialManagerService;
	}
}
