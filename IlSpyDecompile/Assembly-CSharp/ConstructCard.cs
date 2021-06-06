using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructCard : MonoBehaviour
{
	private class AnimationData
	{
		public string Name;

		public Transform AnimateTransform;

		public Transform StartTransform;

		public Transform TargetTransform;

		public float AnimationTime = 1f;

		public float StartDelay;

		public GameObject GlowObject;

		public GameObject GlowObjectStandard;

		public GameObject GlowObjectUnique;

		public ParticleSystem HitBlastParticle;

		public Vector3 ImpactRotation;

		public string OnComplete = string.Empty;
	}

	private readonly Vector3 IMPACT_CAMERA_SHAKE_AMOUNT = new Vector3(0.35f, 0.35f, 0.35f);

	private readonly float IMPACT_CAMERA_SHAKE_TIME = 0.25f;

	public Material m_GhostMaterial;

	public Material m_GhostMaterialTransparent;

	public float m_ImpactRotationTime = 0.5f;

	public float m_RandomDelayVariance = 0.2f;

	public float m_AnimationRarityScaleCommon = 1f;

	public float m_AnimationRarityScaleRare = 0.9f;

	public float m_AnimationRarityScaleEpic = 0.8f;

	public float m_AnimationRarityScaleLegendary = 0.7f;

	public GameObject m_GhostGlow;

	public Texture m_GhostTextureUnique;

	public GameObject m_FuseGlow;

	public ParticleSystem m_RarityBurstCommon;

	public ParticleSystem m_RarityBurstRare;

	public ParticleSystem m_RarityBurstEpic;

	public ParticleSystem m_RarityBurstLegendary;

	public Transform m_ManaGemStartPosition;

	public Transform m_ManaGemTargetPosition;

	public float m_ManaGemStartDelay;

	public float m_ManaGemAnimTime = 1f;

	public GameObject m_ManaGemGlow;

	public ParticleSystem m_ManaGemHitBlastParticle;

	public Vector3 m_ManaGemImpactRotation = new Vector3(20f, 0f, 20f);

	public Transform m_DescriptionStartPosition;

	public Transform m_DescriptionTargetPosition;

	public float m_DescriptionStartDelay;

	public float m_DescriptionAnimTime = 1f;

	public GameObject m_DescriptionGlow;

	public ParticleSystem m_DescriptionHitBlastParticle;

	public Vector3 m_DescriptionImpactRotation = new Vector3(-15f, 0f, 0f);

	public Transform m_AttackStartPosition;

	public Transform m_AttackTargetPosition;

	public float m_AttackStartDelay;

	public float m_AttackAnimTime = 1f;

	public GameObject m_AttackGlow;

	public ParticleSystem m_AttackHitBlastParticle;

	public Vector3 m_AttackImpactRotation = new Vector3(-15f, 0f, 0f);

	public Transform m_HealthStartPosition;

	public Transform m_HealthTargetPosition;

	public float m_HealthStartDelay;

	public float m_HealthAnimTime = 1f;

	public GameObject m_HealthGlow;

	public ParticleSystem m_HealthHitBlastParticle;

	public Vector3 m_HealthImpactRotation = new Vector3(-15f, 0f, 0f);

	public Transform m_ArmorStartPosition;

	public Transform m_ArmorTargetPosition;

	public float m_ArmorStartDelay;

	public float m_ArmorAnimTime = 1f;

	public GameObject m_ArmorGlow;

	public ParticleSystem m_ArmorHitBlastParticle;

	public Vector3 m_ArmorImpactRotation = new Vector3(-15f, 0f, 0f);

	public Transform m_PortraitStartPosition;

	public Transform m_PortraitTargetPosition;

	public float m_PortraitStartDelay;

	public float m_PortraitAnimTime = 1f;

	public GameObject m_PortraitGlow;

	public GameObject m_PortraitGlowStandard;

	public GameObject m_PortraitGlowUnique;

	public ParticleSystem m_PortraitHitBlastParticle;

	public Vector3 m_PortraitImpactRotation = new Vector3(-15f, 0f, 0f);

	public Transform m_NameStartPosition;

	public Transform m_NameTargetPosition;

	public float m_NameStartDelay;

	public float m_NameAnimTime = 1f;

	public GameObject m_NameGlow;

	public ParticleSystem m_NameHitBlastParticle;

	public Vector3 m_NameImpactRotation = new Vector3(-15f, 0f, 0f);

	public Transform m_RarityStartPosition;

	public Transform m_RarityTargetPosition;

	public float m_RarityStartDelay;

	public float m_RarityAnimTime = 1f;

	public GameObject m_RarityGlowCommon;

	public GameObject m_RarityGlowRare;

	public GameObject m_RarityGlowEpic;

	public GameObject m_RarityGlowLegendary;

	public ParticleSystem m_RarityHitBlastParticle;

	public Vector3 m_RarityImpactRotation = new Vector3(-15f, 0f, 0f);

	private Actor m_Actor;

	private Spell m_GhostSpell;

	private float m_AnimationScale = 1f;

	private bool isInit;

	private GameObject m_ManaGemInstance;

	private GameObject m_DescriptionInstance;

	private GameObject m_AttackInstance;

	private GameObject m_HealthInstance;

	private GameObject m_ArmorInstance;

	private GameObject m_PortraitInstance;

	private GameObject m_NameInstance;

	private GameObject m_RarityInstance;

	private GameObject m_CardMesh;

	private int m_CardFrontIdx;

	private GameObject m_PortraitMesh;

	private int m_PortraitFrameIdx;

	private GameObject m_NameMesh;

	private GameObject m_DescriptionMesh;

	private GameObject m_DescriptionTrimMesh;

	private GameObject m_RarityGemMesh;

	private GameObject m_RarityFrameMesh;

	private GameObject m_ManaCostMesh;

	private GameObject m_AttackMesh;

	private GameObject m_HealthMesh;

	private GameObject m_ArmorMesh;

	private GameObject m_RacePlateMesh;

	private GameObject m_EliteMesh;

	private GameObject m_ManaGemClone;

	private Material m_OrgMat_CardFront;

	private Material m_OrgMat_PortraitFrame;

	private Material m_OrgMat_Name;

	private Material m_OrgMat_Description;

	private Material m_OrgMat_Description2;

	private Material m_OrgMat_DescriptionTrim;

	private Material m_OrgMat_RarityFrame;

	private Material m_OrgMat_ManaCost;

	private Material m_OrgMat_Attack;

	private Material m_OrgMat_Health;

	private Material m_OrgMat_Armor;

	private Material m_OrgMat_RacePlate;

	private Material m_OrgMat_Elite;

	private void OnDisable()
	{
		Cancel();
	}

	public void Construct()
	{
		StartCoroutine(DoConstruct());
	}

	private IEnumerator DoConstruct()
	{
		m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (m_Actor == null)
		{
			Debug.LogError($"{base.transform.root.name} Ghost card effect failed to find Actor!");
			base.enabled = false;
			yield break;
		}
		m_Actor.HideAllText();
		m_GhostSpell = m_Actor.GetSpell(SpellType.GHOSTMODE);
		m_GhostSpell.ActivateState(SpellStateType.CANCEL);
		m_Actor.ActivateSpellDeathState(SpellType.GHOSTMODE);
		while (m_GhostSpell.IsActive())
		{
			yield return new WaitForEndOfFrame();
		}
		m_Actor.HideAllText();
		Init();
		CreateInstances();
		ApplyGhostMaterials();
		if ((bool)m_GhostGlow)
		{
			Renderer component = m_GhostGlow.GetComponent<Renderer>();
			if (m_Actor.IsElite() && (bool)m_GhostTextureUnique)
			{
				component.GetMaterial().mainTexture = m_GhostTextureUnique;
			}
			component.enabled = true;
			m_GhostGlow.GetComponent<Animation>().Play("GhostModeHot", PlayMode.StopAll);
		}
		if ((bool)m_RarityGemMesh)
		{
			m_RarityGemMesh.GetComponent<Renderer>().enabled = false;
		}
		if ((bool)m_RarityFrameMesh)
		{
			m_RarityFrameMesh.GetComponent<Renderer>().enabled = false;
		}
		if ((bool)m_ManaGemStartPosition && (bool)m_ManaGemInstance)
		{
			AnimateManaGem();
		}
		if ((bool)m_DescriptionStartPosition && (bool)m_DescriptionInstance)
		{
			AnimateDescription();
		}
		if ((bool)m_AttackStartPosition && (bool)m_AttackInstance)
		{
			AnimateAttack();
		}
		if ((bool)m_HealthStartPosition && (bool)m_HealthInstance)
		{
			AnimateHealth();
		}
		if ((bool)m_ArmorStartPosition && (bool)m_ArmorInstance)
		{
			AnimateArmor();
		}
		if ((bool)m_PortraitStartPosition && (bool)m_PortraitInstance)
		{
			AnimatePortrait();
		}
		if ((bool)m_NameStartPosition && (bool)m_NameInstance)
		{
			AnimateName();
		}
		if ((bool)m_RarityStartPosition)
		{
			AnimateRarity();
		}
	}

	private void Init()
	{
		if (isInit)
		{
			return;
		}
		m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (m_Actor == null)
		{
			Debug.LogError($"{base.transform.root.name} Ghost card effect failed to find Actor!");
			base.enabled = false;
			return;
		}
		m_CardMesh = m_Actor.m_cardMesh;
		m_CardFrontIdx = m_Actor.m_cardFrontMatIdx;
		m_PortraitMesh = m_Actor.m_portraitMesh;
		m_PortraitFrameIdx = m_Actor.m_portraitFrameMatIdx;
		m_NameMesh = m_Actor.m_nameBannerMesh;
		m_DescriptionMesh = m_Actor.m_descriptionMesh;
		m_DescriptionTrimMesh = m_Actor.m_descriptionTrimMesh;
		m_RarityGemMesh = m_Actor.m_rarityGemMesh;
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
		if ((bool)m_Actor.m_armorObject)
		{
			Renderer component3 = m_Actor.m_armorObject.GetComponent<Renderer>();
			if (component3 != null)
			{
				m_ArmorMesh = component3.gameObject;
			}
			if (m_ArmorMesh == null)
			{
				Renderer[] componentsInChildren = m_Actor.m_armorObject.GetComponentsInChildren<Renderer>();
				foreach (Renderer renderer3 in componentsInChildren)
				{
					if (!renderer3.GetComponent<UberText>())
					{
						m_ArmorMesh = renderer3.gameObject;
					}
				}
			}
		}
		m_ManaCostMesh = m_Actor.m_manaObject;
		m_RacePlateMesh = m_Actor.m_racePlateObject;
		m_EliteMesh = m_Actor.m_eliteObject;
		StoreOrgMaterials();
		switch (m_Actor.GetRarity())
		{
		case TAG_RARITY.RARE:
			m_AnimationScale = m_AnimationRarityScaleRare;
			break;
		case TAG_RARITY.EPIC:
			m_AnimationScale = m_AnimationRarityScaleEpic;
			break;
		case TAG_RARITY.LEGENDARY:
			m_AnimationScale = m_AnimationRarityScaleLegendary;
			break;
		default:
			m_AnimationScale = m_AnimationRarityScaleCommon;
			break;
		}
		isInit = true;
	}

	private void Cancel()
	{
		StopAllCoroutines();
		RestoreOrgMaterials();
		DisableManaGem();
		DisableDescription();
		DisableAttack();
		DisableHealth();
		DisableArmor();
		DisablePortrait();
		DisableName();
		DisableRarity();
		DestroyInstances();
		StopAllParticles();
		HideAllMeshObjects();
		if ((bool)m_Actor)
		{
			m_Actor.ShowAllText();
		}
		if (m_Actor != null)
		{
			iTween.StopByName(m_Actor.gameObject, "CardConstructImpactRotation");
		}
	}

	private void StopAllParticles()
	{
		ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			if (particleSystem.isPlaying)
			{
				particleSystem.Stop();
			}
		}
	}

	private void HideAllMeshObjects()
	{
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetComponent<Renderer>().enabled = false;
		}
	}

	private void CreateInstances()
	{
		Vector3 position = new Vector3(0f, -5000f, 0f);
		if ((bool)m_RarityGemMesh)
		{
			m_RarityGemMesh.GetComponent<Renderer>().enabled = false;
		}
		if ((bool)m_RarityFrameMesh)
		{
			m_RarityFrameMesh.GetComponent<Renderer>().enabled = false;
		}
		if ((bool)m_ManaGemStartPosition && (bool)m_ManaCostMesh)
		{
			m_ManaGemInstance = Object.Instantiate(m_ManaCostMesh);
			m_ManaGemInstance.transform.parent = base.transform.parent;
			m_ManaGemInstance.transform.position = position;
		}
		if ((bool)m_DescriptionStartPosition && (bool)m_DescriptionMesh)
		{
			m_DescriptionInstance = Object.Instantiate(m_DescriptionMesh);
			m_DescriptionInstance.transform.parent = base.transform.parent;
			m_DescriptionInstance.transform.position = position;
		}
		if ((bool)m_AttackStartPosition && (bool)m_AttackMesh)
		{
			m_AttackInstance = Object.Instantiate(m_AttackMesh);
			m_AttackInstance.transform.parent = base.transform.parent;
			m_AttackInstance.transform.position = position;
		}
		if ((bool)m_HealthStartPosition && (bool)m_HealthMesh)
		{
			m_HealthInstance = Object.Instantiate(m_HealthMesh);
			m_HealthInstance.transform.parent = base.transform.parent;
			m_HealthInstance.transform.position = position;
		}
		if ((bool)m_ArmorStartPosition && (bool)m_ArmorMesh)
		{
			m_ArmorInstance = Object.Instantiate(m_ArmorMesh);
			m_ArmorInstance.transform.parent = base.transform.parent;
			m_ArmorInstance.transform.position = position;
		}
		if ((bool)m_PortraitStartPosition && (bool)m_PortraitMesh)
		{
			m_PortraitInstance = Object.Instantiate(m_PortraitMesh);
			m_PortraitInstance.transform.parent = base.transform.parent;
			m_PortraitInstance.transform.position = position;
		}
		if ((bool)m_NameStartPosition && (bool)m_NameMesh)
		{
			m_NameInstance = Object.Instantiate(m_NameMesh);
			m_NameInstance.transform.parent = base.transform.parent;
			m_NameInstance.transform.position = position;
		}
		if ((bool)m_RarityStartPosition && (bool)m_RarityGemMesh)
		{
			m_RarityInstance = Object.Instantiate(m_RarityGemMesh);
			m_RarityInstance.transform.parent = base.transform.parent;
			m_RarityInstance.transform.position = position;
		}
	}

	private void DestroyInstances()
	{
		if ((bool)m_ManaGemInstance)
		{
			Object.Destroy(m_ManaGemInstance);
		}
		if ((bool)m_DescriptionInstance)
		{
			Object.Destroy(m_DescriptionInstance);
		}
		if ((bool)m_AttackInstance)
		{
			Object.Destroy(m_AttackInstance);
		}
		if ((bool)m_HealthInstance)
		{
			Object.Destroy(m_HealthInstance);
		}
		if ((bool)m_ArmorInstance)
		{
			Object.Destroy(m_ArmorInstance);
		}
		if ((bool)m_PortraitInstance)
		{
			Object.Destroy(m_PortraitInstance);
		}
		if ((bool)m_NameInstance)
		{
			Object.Destroy(m_NameInstance);
		}
		if ((bool)m_RarityInstance)
		{
			Object.Destroy(m_RarityInstance);
		}
	}

	private void AnimateManaGem()
	{
		GameObject manaGemInstance = m_ManaGemInstance;
		manaGemInstance.transform.parent = null;
		manaGemInstance.transform.localScale = m_ManaCostMesh.transform.lossyScale;
		manaGemInstance.transform.position = m_ManaGemStartPosition.transform.position;
		manaGemInstance.transform.parent = base.transform.parent;
		manaGemInstance.GetComponent<Renderer>().SetMaterial(m_OrgMat_ManaCost);
		float startDelay = Random.Range(m_ManaGemStartDelay - m_ManaGemStartDelay * m_RandomDelayVariance, m_ManaGemStartDelay + m_ManaGemStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "ManaGem",
			AnimateTransform = manaGemInstance.transform,
			StartTransform = m_ManaGemStartPosition.transform,
			TargetTransform = m_ManaGemTargetPosition.transform,
			HitBlastParticle = m_ManaGemHitBlastParticle,
			AnimationTime = m_ManaGemAnimTime,
			StartDelay = startDelay,
			GlowObject = m_ManaGemGlow,
			ImpactRotation = m_ManaGemImpactRotation,
			OnComplete = "ManaGemOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator ManaGemOnComplete()
	{
		DisableManaGem();
		yield break;
	}

	private void DisableManaGem()
	{
		if ((bool)m_ManaGemGlow)
		{
			ParticleSystem[] componentsInChildren = m_ManaGemGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimateDescription()
	{
		GameObject descriptionInstance = m_DescriptionInstance;
		descriptionInstance.transform.parent = null;
		descriptionInstance.transform.localScale = m_DescriptionMesh.transform.lossyScale;
		descriptionInstance.transform.position = m_DescriptionStartPosition.transform.position;
		descriptionInstance.transform.parent = base.transform.parent;
		descriptionInstance.GetComponent<Renderer>().SetMaterial(m_OrgMat_Description);
		float startDelay = Random.Range(m_DescriptionStartDelay - m_DescriptionStartDelay * m_RandomDelayVariance, m_DescriptionStartDelay + m_DescriptionStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "Description",
			AnimateTransform = descriptionInstance.transform,
			StartTransform = m_DescriptionStartPosition.transform,
			TargetTransform = m_DescriptionTargetPosition.transform,
			HitBlastParticle = m_DescriptionHitBlastParticle,
			AnimationTime = m_DescriptionAnimTime,
			StartDelay = startDelay,
			GlowObject = m_DescriptionGlow,
			ImpactRotation = m_DescriptionImpactRotation,
			OnComplete = "DescriptionOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator DescriptionOnComplete()
	{
		DisableDescription();
		yield break;
	}

	private void DisableDescription()
	{
		if ((bool)m_DescriptionGlow)
		{
			ParticleSystem[] componentsInChildren = m_DescriptionGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimateAttack()
	{
		GameObject attackInstance = m_AttackInstance;
		attackInstance.transform.parent = null;
		attackInstance.transform.localScale = m_AttackMesh.transform.lossyScale;
		attackInstance.transform.position = m_AttackStartPosition.transform.position;
		attackInstance.transform.parent = base.transform.parent;
		attackInstance.GetComponent<Renderer>().SetMaterial(m_OrgMat_Attack);
		float startDelay = Random.Range(m_AttackStartDelay - m_AttackStartDelay * m_RandomDelayVariance, m_AttackStartDelay + m_AttackStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "Attack",
			AnimateTransform = attackInstance.transform,
			StartTransform = m_AttackStartPosition.transform,
			TargetTransform = m_AttackTargetPosition.transform,
			HitBlastParticle = m_AttackHitBlastParticle,
			AnimationTime = m_AttackAnimTime,
			StartDelay = startDelay,
			GlowObject = m_AttackGlow,
			ImpactRotation = m_AttackImpactRotation,
			OnComplete = "AttackOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator AttackOnComplete()
	{
		DisableAttack();
		yield break;
	}

	private void DisableAttack()
	{
		if ((bool)m_AttackGlow)
		{
			ParticleSystem[] componentsInChildren = m_AttackGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimateHealth()
	{
		GameObject healthInstance = m_HealthInstance;
		healthInstance.transform.parent = null;
		healthInstance.transform.localScale = m_HealthMesh.transform.lossyScale;
		healthInstance.transform.position = m_HealthStartPosition.transform.position;
		healthInstance.transform.parent = base.transform.parent;
		healthInstance.GetComponent<Renderer>().SetMaterial(m_OrgMat_Health);
		float startDelay = Random.Range(m_HealthStartDelay - m_HealthStartDelay * m_RandomDelayVariance, m_HealthStartDelay + m_HealthStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "Health",
			AnimateTransform = healthInstance.transform,
			StartTransform = m_HealthStartPosition.transform,
			TargetTransform = m_HealthTargetPosition.transform,
			HitBlastParticle = m_HealthHitBlastParticle,
			AnimationTime = m_HealthAnimTime,
			StartDelay = startDelay,
			GlowObject = m_HealthGlow,
			ImpactRotation = m_HealthImpactRotation,
			OnComplete = "HealthOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator HealthOnComplete()
	{
		DisableHealth();
		yield break;
	}

	private void DisableHealth()
	{
		if ((bool)m_HealthGlow)
		{
			ParticleSystem[] componentsInChildren = m_HealthGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimateArmor()
	{
		GameObject armorInstance = m_ArmorInstance;
		armorInstance.transform.parent = null;
		armorInstance.transform.localScale = m_ArmorMesh.transform.lossyScale;
		armorInstance.transform.position = m_ArmorStartPosition.transform.position;
		armorInstance.transform.parent = base.transform.parent;
		armorInstance.GetComponent<Renderer>().SetMaterial(m_OrgMat_Armor);
		float startDelay = Random.Range(m_ArmorStartDelay - m_ArmorStartDelay * m_RandomDelayVariance, m_ArmorStartDelay + m_ArmorStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "Armor",
			AnimateTransform = armorInstance.transform,
			StartTransform = m_ArmorStartPosition.transform,
			TargetTransform = m_ArmorTargetPosition.transform,
			HitBlastParticle = m_ArmorHitBlastParticle,
			AnimationTime = m_ArmorAnimTime,
			StartDelay = startDelay,
			GlowObject = m_ArmorGlow,
			ImpactRotation = m_ArmorImpactRotation,
			OnComplete = "ArmorOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator ArmorOnComplete()
	{
		DisableArmor();
		yield break;
	}

	private void DisableArmor()
	{
		if ((bool)m_ArmorGlow)
		{
			ParticleSystem[] componentsInChildren = m_ArmorGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimatePortrait()
	{
		GameObject portraitInstance = m_PortraitInstance;
		portraitInstance.transform.parent = null;
		portraitInstance.transform.localScale = m_PortraitMesh.transform.lossyScale;
		portraitInstance.transform.position = m_PortraitStartPosition.transform.position;
		portraitInstance.transform.parent = base.transform.parent;
		float startDelay = Random.Range(m_PortraitStartDelay - m_PortraitStartDelay * m_RandomDelayVariance, m_PortraitStartDelay + m_PortraitStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "Portrait",
			AnimateTransform = portraitInstance.transform,
			StartTransform = m_PortraitStartPosition.transform,
			TargetTransform = m_PortraitTargetPosition.transform,
			HitBlastParticle = m_PortraitHitBlastParticle,
			AnimationTime = m_PortraitAnimTime,
			StartDelay = startDelay,
			GlowObject = m_PortraitGlow,
			GlowObjectStandard = m_PortraitGlowStandard,
			GlowObjectUnique = m_PortraitGlowUnique,
			ImpactRotation = m_PortraitImpactRotation,
			OnComplete = "PortraitOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator PortraitOnComplete()
	{
		DisablePortrait();
		yield break;
	}

	private void DisablePortrait()
	{
		if ((bool)m_PortraitGlow)
		{
			ParticleSystem[] componentsInChildren = m_PortraitGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimateName()
	{
		GameObject nameInstance = m_NameInstance;
		nameInstance.transform.parent = null;
		nameInstance.transform.localScale = m_NameMesh.transform.lossyScale;
		nameInstance.transform.position = m_NameStartPosition.transform.position;
		nameInstance.transform.parent = base.transform.parent;
		nameInstance.GetComponent<Renderer>().SetMaterial(m_OrgMat_Name);
		float startDelay = Random.Range(m_NameStartDelay - m_NameStartDelay * m_RandomDelayVariance, m_NameStartDelay + m_NameStartDelay * m_RandomDelayVariance);
		AnimationData value = new AnimationData
		{
			Name = "Name",
			AnimateTransform = nameInstance.transform,
			StartTransform = m_NameStartPosition.transform,
			TargetTransform = m_NameTargetPosition.transform,
			HitBlastParticle = m_NameHitBlastParticle,
			AnimationTime = m_NameAnimTime,
			StartDelay = startDelay,
			GlowObject = m_NameGlow,
			ImpactRotation = m_NameImpactRotation,
			OnComplete = "NameOnComplete"
		};
		StartCoroutine("AnimateObject", value);
	}

	private IEnumerator NameOnComplete()
	{
		DisableName();
		yield break;
	}

	private void DisableName()
	{
		if ((bool)m_NameGlow)
		{
			ParticleSystem[] componentsInChildren = m_NameGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private void AnimateRarity()
	{
		if (m_Actor.GetRarity() != TAG_RARITY.FREE)
		{
			GameObject rarityInstance = m_RarityInstance;
			rarityInstance.transform.parent = null;
			rarityInstance.transform.localScale = m_RarityGemMesh.transform.lossyScale;
			rarityInstance.transform.position = m_RarityStartPosition.transform.position;
			rarityInstance.transform.parent = base.transform.parent;
			m_RarityInstance.GetComponent<Renderer>().enabled = true;
			GameObject glowObject = m_RarityGlowCommon;
			switch (m_Actor.GetRarity())
			{
			case TAG_RARITY.RARE:
				glowObject = m_RarityGlowRare;
				break;
			case TAG_RARITY.EPIC:
				glowObject = m_RarityGlowEpic;
				break;
			case TAG_RARITY.LEGENDARY:
				glowObject = m_RarityGlowLegendary;
				break;
			}
			float startDelay = Random.Range(m_RarityStartDelay - m_RarityStartDelay * m_RandomDelayVariance, m_RarityStartDelay + m_RarityStartDelay * m_RandomDelayVariance);
			AnimationData value = new AnimationData
			{
				Name = "Rarity",
				AnimateTransform = rarityInstance.transform,
				StartTransform = m_RarityStartPosition.transform,
				TargetTransform = m_RarityTargetPosition.transform,
				HitBlastParticle = m_RarityHitBlastParticle,
				AnimationTime = m_RarityAnimTime,
				StartDelay = startDelay,
				GlowObject = glowObject,
				ImpactRotation = m_RarityImpactRotation,
				OnComplete = "RarityOnComplete"
			};
			StartCoroutine("AnimateObject", value);
		}
	}

	private IEnumerator RarityOnComplete()
	{
		DisableRarity();
		if (m_Actor.GetRarity() != TAG_RARITY.FREE)
		{
			if ((bool)m_RarityGemMesh)
			{
				m_RarityGemMesh.GetComponent<Renderer>().enabled = true;
			}
			if ((bool)m_RarityFrameMesh)
			{
				m_RarityFrameMesh.GetComponent<Renderer>().enabled = true;
			}
		}
		StartCoroutine(EndAnimation());
		yield break;
	}

	private void DisableRarity()
	{
		if ((bool)m_RarityGlowCommon)
		{
			ParticleSystem[] componentsInChildren = m_RarityGlowCommon.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	private IEnumerator EndAnimation()
	{
		ParticleSystem particleSystem = m_RarityBurstCommon;
		TAG_RARITY rarity = m_Actor.GetRarity();
		switch (rarity)
		{
		case TAG_RARITY.RARE:
			particleSystem = m_RarityBurstRare;
			break;
		case TAG_RARITY.EPIC:
			particleSystem = m_RarityBurstEpic;
			break;
		case TAG_RARITY.LEGENDARY:
			particleSystem = m_RarityBurstLegendary;
			break;
		}
		if ((bool)particleSystem)
		{
			Renderer[] componentsInChildren = particleSystem.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = true;
			}
			particleSystem.Play(withChildren: true);
		}
		string animation = "CardFuse_Common";
		switch (rarity)
		{
		case TAG_RARITY.RARE:
			animation = "CardFuse_Rare";
			break;
		case TAG_RARITY.EPIC:
			animation = "CardFuse_Epic";
			break;
		case TAG_RARITY.LEGENDARY:
			animation = "CardFuse_Legendary";
			break;
		}
		if ((bool)m_FuseGlow)
		{
			m_FuseGlow.GetComponent<Renderer>().enabled = true;
			m_FuseGlow.GetComponent<Animation>().Play(animation, PlayMode.StopAll);
		}
		yield return new WaitForSeconds(0.25f);
		DestroyInstances();
		m_Actor.ShowAllText();
		RestoreOrgMaterials();
	}

	private IEnumerator AnimateObject(AnimationData animData)
	{
		yield return new WaitForSeconds(animData.StartDelay);
		float animPos = 0f;
		float rate = 1f / (animData.AnimationTime * m_AnimationScale);
		Quaternion rotation = m_Actor.transform.rotation;
		m_Actor.transform.rotation = Quaternion.identity;
		Vector3 startPosition = animData.StartTransform.position;
		Quaternion startRotation = animData.StartTransform.rotation;
		m_Actor.transform.rotation = rotation;
		if ((bool)animData.GlowObject)
		{
			GameObject glowObject = animData.GlowObject;
			glowObject.transform.parent = animData.AnimateTransform;
			glowObject.transform.localPosition = Vector3.zero;
			ParticleSystem[] componentsInChildren = glowObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
			if ((bool)animData.GlowObjectStandard && (bool)animData.GlowObjectUnique)
			{
				if (m_Actor.IsElite())
				{
					animData.GlowObjectUnique.GetComponent<Renderer>().enabled = true;
				}
				else
				{
					animData.GlowObjectStandard.GetComponent<Renderer>().enabled = true;
				}
			}
			else
			{
				Renderer[] componentsInChildren2 = glowObject.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					componentsInChildren2[i].enabled = true;
				}
			}
		}
		while (animPos < 1f)
		{
			Vector3 position = animData.TargetTransform.position;
			Quaternion rotation2 = animData.TargetTransform.rotation;
			animPos += rate * Time.deltaTime;
			Vector3 position2 = Vector3.Lerp(startPosition, position, animPos);
			Quaternion rotation3 = Quaternion.Lerp(startRotation, rotation2, animPos);
			animData.AnimateTransform.position = position2;
			animData.AnimateTransform.rotation = rotation3;
			yield return null;
		}
		if ((bool)animData.HitBlastParticle)
		{
			animData.HitBlastParticle.transform.position = animData.TargetTransform.position;
			animData.HitBlastParticle.GetComponent<Renderer>().enabled = true;
			animData.HitBlastParticle.Play();
		}
		animData.AnimateTransform.parent = animData.TargetTransform;
		animData.AnimateTransform.position = animData.TargetTransform.position;
		animData.AnimateTransform.rotation = animData.TargetTransform.rotation;
		if ((bool)animData.GlowObject)
		{
			ParticleSystem[] componentsInChildren = animData.GlowObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
		if (!(m_Actor.gameObject == null))
		{
			m_Actor.gameObject.transform.localRotation = Quaternion.Euler(animData.ImpactRotation);
			Hashtable args = iTween.Hash("rotation", Vector3.zero, "time", m_ImpactRotationTime, "easetype", iTween.EaseType.easeOutQuad, "space", Space.Self, "name", "CardConstructImpactRotation" + animData.Name);
			iTween.StopByName(m_Actor.gameObject, "CardConstructImpactRotation" + animData.Name);
			iTween.RotateTo(m_Actor.gameObject, args);
			CameraShakeMgr.Shake(Camera.main, IMPACT_CAMERA_SHAKE_AMOUNT, IMPACT_CAMERA_SHAKE_TIME);
			if (animData.OnComplete != string.Empty)
			{
				StartCoroutine(animData.OnComplete);
			}
		}
	}

	private void StoreOrgMaterials()
	{
		if ((bool)m_CardMesh)
		{
			m_OrgMat_CardFront = m_CardMesh.GetComponent<Renderer>().GetMaterial(m_CardFrontIdx);
		}
		if ((bool)m_PortraitMesh)
		{
			m_OrgMat_PortraitFrame = m_PortraitMesh.GetComponent<Renderer>().GetSharedMaterial(m_PortraitFrameIdx);
		}
		if ((bool)m_NameMesh)
		{
			m_OrgMat_Name = m_NameMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_ManaCostMesh)
		{
			m_OrgMat_ManaCost = m_ManaCostMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_AttackMesh)
		{
			m_OrgMat_Attack = m_AttackMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_HealthMesh)
		{
			m_OrgMat_Health = m_HealthMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_ArmorMesh)
		{
			m_OrgMat_Armor = m_ArmorMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_RacePlateMesh)
		{
			m_OrgMat_RacePlate = m_RacePlateMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_RarityFrameMesh)
		{
			m_OrgMat_RarityFrame = m_RarityFrameMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_DescriptionMesh)
		{
			List<Material> materials = m_DescriptionMesh.GetComponent<Renderer>().GetMaterials();
			if (m_DescriptionMesh.GetComponent<Renderer>() != null)
			{
				if (materials.Count > 1)
				{
					m_OrgMat_Description = materials[0];
					m_OrgMat_Description2 = materials[1];
				}
				else
				{
					m_OrgMat_Description = m_DescriptionMesh.GetComponent<Renderer>().GetMaterial();
				}
			}
		}
		if ((bool)m_DescriptionTrimMesh)
		{
			m_OrgMat_DescriptionTrim = m_DescriptionTrimMesh.GetComponent<Renderer>().GetMaterial();
		}
		if ((bool)m_EliteMesh)
		{
			m_OrgMat_Elite = m_EliteMesh.GetComponent<Renderer>().GetMaterial();
		}
	}

	private void RestoreOrgMaterials()
	{
		ApplyMaterialByIdx(m_CardMesh, m_OrgMat_CardFront, m_CardFrontIdx);
		ApplySharedMaterialByIdx(m_PortraitMesh, m_OrgMat_PortraitFrame, m_PortraitFrameIdx);
		ApplyMaterialByIdx(m_DescriptionMesh, m_OrgMat_Description, 0);
		ApplyMaterialByIdx(m_DescriptionMesh, m_OrgMat_Description2, 1);
		ApplyMaterial(m_NameMesh, m_OrgMat_Name);
		ApplyMaterial(m_ManaCostMesh, m_OrgMat_ManaCost);
		ApplyMaterial(m_AttackMesh, m_OrgMat_Attack);
		ApplyMaterial(m_HealthMesh, m_OrgMat_Health);
		ApplyMaterial(m_ArmorMesh, m_OrgMat_Armor);
		ApplyMaterial(m_RacePlateMesh, m_OrgMat_RacePlate);
		ApplyMaterial(m_RarityFrameMesh, m_OrgMat_RarityFrame);
		ApplyMaterial(m_DescriptionTrimMesh, m_OrgMat_DescriptionTrim);
		ApplyMaterial(m_EliteMesh, m_OrgMat_Elite);
	}

	private void ApplyGhostMaterials()
	{
		ApplyMaterialByIdx(m_CardMesh, m_GhostMaterial, m_CardFrontIdx);
		ApplySharedMaterialByIdx(m_PortraitMesh, m_GhostMaterial, m_PortraitFrameIdx);
		ApplyMaterialByIdx(m_DescriptionMesh, m_GhostMaterial, 0);
		ApplyMaterialByIdx(m_DescriptionMesh, m_GhostMaterial, 1);
		ApplyMaterial(m_NameMesh, m_GhostMaterial);
		ApplyMaterial(m_ManaCostMesh, m_GhostMaterial);
		ApplyMaterial(m_AttackMesh, m_GhostMaterial);
		ApplyMaterial(m_HealthMesh, m_GhostMaterial);
		ApplyMaterial(m_RacePlateMesh, m_GhostMaterial);
		ApplyMaterial(m_RarityFrameMesh, m_GhostMaterial);
		if ((bool)m_GhostMaterialTransparent)
		{
			ApplyMaterial(m_DescriptionTrimMesh, m_GhostMaterialTransparent);
		}
		ApplyMaterial(m_EliteMesh, m_GhostMaterial);
	}

	private void ApplyMaterial(GameObject go, Material mat)
	{
		if (!(go == null))
		{
			Renderer component = go.GetComponent<Renderer>();
			Texture mainTexture = component.GetMaterial().mainTexture;
			component.SetMaterial(mat);
			component.GetMaterial().mainTexture = mainTexture;
		}
	}

	private void ApplyMaterialByIdx(GameObject go, Material mat, int idx)
	{
		if (!(go == null) && !(mat == null) && idx >= 0)
		{
			Renderer component = go.GetComponent<Renderer>();
			List<Material> materials = component.GetMaterials();
			if (idx < materials.Count)
			{
				Texture mainTexture = component.GetMaterial(idx).mainTexture;
				component.SetMaterial(idx, mat);
				component.GetMaterial(idx).mainTexture = mainTexture;
			}
		}
	}

	private void ApplySharedMaterialByIdx(GameObject go, Material mat, int idx)
	{
		if (!(go == null) && !(mat == null) && idx >= 0)
		{
			Renderer component = go.GetComponent<Renderer>();
			List<Material> sharedMaterials = component.GetSharedMaterials();
			if (idx < sharedMaterials.Count)
			{
				Texture mainTexture = component.GetSharedMaterial(idx).mainTexture;
				component.SetSharedMaterial(idx, mat);
				component.GetSharedMaterial(idx).mainTexture = mainTexture;
			}
		}
	}
}
