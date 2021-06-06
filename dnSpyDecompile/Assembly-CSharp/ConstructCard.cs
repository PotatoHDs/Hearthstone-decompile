using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A1E RID: 2590
public class ConstructCard : MonoBehaviour
{
	// Token: 0x06008B93 RID: 35731 RVA: 0x002CA268 File Offset: 0x002C8468
	private void OnDisable()
	{
		this.Cancel();
	}

	// Token: 0x06008B94 RID: 35732 RVA: 0x002CA270 File Offset: 0x002C8470
	public void Construct()
	{
		base.StartCoroutine(this.DoConstruct());
	}

	// Token: 0x06008B95 RID: 35733 RVA: 0x002CA27F File Offset: 0x002C847F
	private IEnumerator DoConstruct()
	{
		this.m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (this.m_Actor == null)
		{
			Debug.LogError(string.Format("{0} Ghost card effect failed to find Actor!", base.transform.root.name));
			base.enabled = false;
			yield break;
		}
		this.m_Actor.HideAllText();
		this.m_GhostSpell = this.m_Actor.GetSpell(SpellType.GHOSTMODE);
		this.m_GhostSpell.ActivateState(SpellStateType.CANCEL);
		this.m_Actor.ActivateSpellDeathState(SpellType.GHOSTMODE);
		while (this.m_GhostSpell.IsActive())
		{
			yield return new WaitForEndOfFrame();
		}
		this.m_Actor.HideAllText();
		this.Init();
		this.CreateInstances();
		this.ApplyGhostMaterials();
		if (this.m_GhostGlow)
		{
			Renderer component = this.m_GhostGlow.GetComponent<Renderer>();
			if (this.m_Actor.IsElite() && this.m_GhostTextureUnique)
			{
				component.GetMaterial().mainTexture = this.m_GhostTextureUnique;
			}
			component.enabled = true;
			this.m_GhostGlow.GetComponent<Animation>().Play("GhostModeHot", PlayMode.StopAll);
		}
		if (this.m_RarityGemMesh)
		{
			this.m_RarityGemMesh.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_RarityFrameMesh)
		{
			this.m_RarityFrameMesh.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_ManaGemStartPosition && this.m_ManaGemInstance)
		{
			this.AnimateManaGem();
		}
		if (this.m_DescriptionStartPosition && this.m_DescriptionInstance)
		{
			this.AnimateDescription();
		}
		if (this.m_AttackStartPosition && this.m_AttackInstance)
		{
			this.AnimateAttack();
		}
		if (this.m_HealthStartPosition && this.m_HealthInstance)
		{
			this.AnimateHealth();
		}
		if (this.m_ArmorStartPosition && this.m_ArmorInstance)
		{
			this.AnimateArmor();
		}
		if (this.m_PortraitStartPosition && this.m_PortraitInstance)
		{
			this.AnimatePortrait();
		}
		if (this.m_NameStartPosition && this.m_NameInstance)
		{
			this.AnimateName();
		}
		if (this.m_RarityStartPosition)
		{
			this.AnimateRarity();
		}
		yield break;
	}

	// Token: 0x06008B96 RID: 35734 RVA: 0x002CA290 File Offset: 0x002C8490
	private void Init()
	{
		if (this.isInit)
		{
			return;
		}
		this.m_Actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (this.m_Actor == null)
		{
			Debug.LogError(string.Format("{0} Ghost card effect failed to find Actor!", base.transform.root.name));
			base.enabled = false;
			return;
		}
		this.m_CardMesh = this.m_Actor.m_cardMesh;
		this.m_CardFrontIdx = this.m_Actor.m_cardFrontMatIdx;
		this.m_PortraitMesh = this.m_Actor.m_portraitMesh;
		this.m_PortraitFrameIdx = this.m_Actor.m_portraitFrameMatIdx;
		this.m_NameMesh = this.m_Actor.m_nameBannerMesh;
		this.m_DescriptionMesh = this.m_Actor.m_descriptionMesh;
		this.m_DescriptionTrimMesh = this.m_Actor.m_descriptionTrimMesh;
		this.m_RarityGemMesh = this.m_Actor.m_rarityGemMesh;
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
		if (this.m_Actor.m_armorObject)
		{
			Renderer component3 = this.m_Actor.m_armorObject.GetComponent<Renderer>();
			if (component3 != null)
			{
				this.m_ArmorMesh = component3.gameObject;
			}
			if (this.m_ArmorMesh == null)
			{
				foreach (Renderer renderer3 in this.m_Actor.m_armorObject.GetComponentsInChildren<Renderer>())
				{
					if (!renderer3.GetComponent<UberText>())
					{
						this.m_ArmorMesh = renderer3.gameObject;
					}
				}
			}
		}
		this.m_ManaCostMesh = this.m_Actor.m_manaObject;
		this.m_RacePlateMesh = this.m_Actor.m_racePlateObject;
		this.m_EliteMesh = this.m_Actor.m_eliteObject;
		this.StoreOrgMaterials();
		switch (this.m_Actor.GetRarity())
		{
		case TAG_RARITY.RARE:
			this.m_AnimationScale = this.m_AnimationRarityScaleRare;
			break;
		case TAG_RARITY.EPIC:
			this.m_AnimationScale = this.m_AnimationRarityScaleEpic;
			break;
		case TAG_RARITY.LEGENDARY:
			this.m_AnimationScale = this.m_AnimationRarityScaleLegendary;
			break;
		default:
			this.m_AnimationScale = this.m_AnimationRarityScaleCommon;
			break;
		}
		this.isInit = true;
	}

	// Token: 0x06008B97 RID: 35735 RVA: 0x002CA5B4 File Offset: 0x002C87B4
	private void Cancel()
	{
		base.StopAllCoroutines();
		this.RestoreOrgMaterials();
		this.DisableManaGem();
		this.DisableDescription();
		this.DisableAttack();
		this.DisableHealth();
		this.DisableArmor();
		this.DisablePortrait();
		this.DisableName();
		this.DisableRarity();
		this.DestroyInstances();
		this.StopAllParticles();
		this.HideAllMeshObjects();
		if (this.m_Actor)
		{
			this.m_Actor.ShowAllText();
		}
		if (this.m_Actor != null)
		{
			iTween.StopByName(this.m_Actor.gameObject, "CardConstructImpactRotation");
		}
	}

	// Token: 0x06008B98 RID: 35736 RVA: 0x002CA64C File Offset: 0x002C884C
	private void StopAllParticles()
	{
		foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
		{
			if (particleSystem.isPlaying)
			{
				particleSystem.Stop();
			}
		}
	}

	// Token: 0x06008B99 RID: 35737 RVA: 0x002CA680 File Offset: 0x002C8880
	private void HideAllMeshObjects()
	{
		MeshRenderer[] componentsInChildren = base.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetComponent<Renderer>().enabled = false;
		}
	}

	// Token: 0x06008B9A RID: 35738 RVA: 0x002CA6B0 File Offset: 0x002C88B0
	private void CreateInstances()
	{
		Vector3 position = new Vector3(0f, -5000f, 0f);
		if (this.m_RarityGemMesh)
		{
			this.m_RarityGemMesh.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_RarityFrameMesh)
		{
			this.m_RarityFrameMesh.GetComponent<Renderer>().enabled = false;
		}
		if (this.m_ManaGemStartPosition && this.m_ManaCostMesh)
		{
			this.m_ManaGemInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_ManaCostMesh);
			this.m_ManaGemInstance.transform.parent = base.transform.parent;
			this.m_ManaGemInstance.transform.position = position;
		}
		if (this.m_DescriptionStartPosition && this.m_DescriptionMesh)
		{
			this.m_DescriptionInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_DescriptionMesh);
			this.m_DescriptionInstance.transform.parent = base.transform.parent;
			this.m_DescriptionInstance.transform.position = position;
		}
		if (this.m_AttackStartPosition && this.m_AttackMesh)
		{
			this.m_AttackInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_AttackMesh);
			this.m_AttackInstance.transform.parent = base.transform.parent;
			this.m_AttackInstance.transform.position = position;
		}
		if (this.m_HealthStartPosition && this.m_HealthMesh)
		{
			this.m_HealthInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_HealthMesh);
			this.m_HealthInstance.transform.parent = base.transform.parent;
			this.m_HealthInstance.transform.position = position;
		}
		if (this.m_ArmorStartPosition && this.m_ArmorMesh)
		{
			this.m_ArmorInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_ArmorMesh);
			this.m_ArmorInstance.transform.parent = base.transform.parent;
			this.m_ArmorInstance.transform.position = position;
		}
		if (this.m_PortraitStartPosition && this.m_PortraitMesh)
		{
			this.m_PortraitInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_PortraitMesh);
			this.m_PortraitInstance.transform.parent = base.transform.parent;
			this.m_PortraitInstance.transform.position = position;
		}
		if (this.m_NameStartPosition && this.m_NameMesh)
		{
			this.m_NameInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_NameMesh);
			this.m_NameInstance.transform.parent = base.transform.parent;
			this.m_NameInstance.transform.position = position;
		}
		if (this.m_RarityStartPosition && this.m_RarityGemMesh)
		{
			this.m_RarityInstance = UnityEngine.Object.Instantiate<GameObject>(this.m_RarityGemMesh);
			this.m_RarityInstance.transform.parent = base.transform.parent;
			this.m_RarityInstance.transform.position = position;
		}
	}

	// Token: 0x06008B9B RID: 35739 RVA: 0x002CA9C8 File Offset: 0x002C8BC8
	private void DestroyInstances()
	{
		if (this.m_ManaGemInstance)
		{
			UnityEngine.Object.Destroy(this.m_ManaGemInstance);
		}
		if (this.m_DescriptionInstance)
		{
			UnityEngine.Object.Destroy(this.m_DescriptionInstance);
		}
		if (this.m_AttackInstance)
		{
			UnityEngine.Object.Destroy(this.m_AttackInstance);
		}
		if (this.m_HealthInstance)
		{
			UnityEngine.Object.Destroy(this.m_HealthInstance);
		}
		if (this.m_ArmorInstance)
		{
			UnityEngine.Object.Destroy(this.m_ArmorInstance);
		}
		if (this.m_PortraitInstance)
		{
			UnityEngine.Object.Destroy(this.m_PortraitInstance);
		}
		if (this.m_NameInstance)
		{
			UnityEngine.Object.Destroy(this.m_NameInstance);
		}
		if (this.m_RarityInstance)
		{
			UnityEngine.Object.Destroy(this.m_RarityInstance);
		}
	}

	// Token: 0x06008B9C RID: 35740 RVA: 0x002CAA98 File Offset: 0x002C8C98
	private void AnimateManaGem()
	{
		GameObject manaGemInstance = this.m_ManaGemInstance;
		manaGemInstance.transform.parent = null;
		manaGemInstance.transform.localScale = this.m_ManaCostMesh.transform.lossyScale;
		manaGemInstance.transform.position = this.m_ManaGemStartPosition.transform.position;
		manaGemInstance.transform.parent = base.transform.parent;
		manaGemInstance.GetComponent<Renderer>().SetMaterial(this.m_OrgMat_ManaCost);
		float startDelay = UnityEngine.Random.Range(this.m_ManaGemStartDelay - this.m_ManaGemStartDelay * this.m_RandomDelayVariance, this.m_ManaGemStartDelay + this.m_ManaGemStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "ManaGem",
			AnimateTransform = manaGemInstance.transform,
			StartTransform = this.m_ManaGemStartPosition.transform,
			TargetTransform = this.m_ManaGemTargetPosition.transform,
			HitBlastParticle = this.m_ManaGemHitBlastParticle,
			AnimationTime = this.m_ManaGemAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_ManaGemGlow,
			ImpactRotation = this.m_ManaGemImpactRotation,
			OnComplete = "ManaGemOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008B9D RID: 35741 RVA: 0x002CABD1 File Offset: 0x002C8DD1
	private IEnumerator ManaGemOnComplete()
	{
		this.DisableManaGem();
		yield break;
	}

	// Token: 0x06008B9E RID: 35742 RVA: 0x002CABE0 File Offset: 0x002C8DE0
	private void DisableManaGem()
	{
		if (this.m_ManaGemGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_ManaGemGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008B9F RID: 35743 RVA: 0x002CAC1C File Offset: 0x002C8E1C
	private void AnimateDescription()
	{
		GameObject descriptionInstance = this.m_DescriptionInstance;
		descriptionInstance.transform.parent = null;
		descriptionInstance.transform.localScale = this.m_DescriptionMesh.transform.lossyScale;
		descriptionInstance.transform.position = this.m_DescriptionStartPosition.transform.position;
		descriptionInstance.transform.parent = base.transform.parent;
		descriptionInstance.GetComponent<Renderer>().SetMaterial(this.m_OrgMat_Description);
		float startDelay = UnityEngine.Random.Range(this.m_DescriptionStartDelay - this.m_DescriptionStartDelay * this.m_RandomDelayVariance, this.m_DescriptionStartDelay + this.m_DescriptionStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Description",
			AnimateTransform = descriptionInstance.transform,
			StartTransform = this.m_DescriptionStartPosition.transform,
			TargetTransform = this.m_DescriptionTargetPosition.transform,
			HitBlastParticle = this.m_DescriptionHitBlastParticle,
			AnimationTime = this.m_DescriptionAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_DescriptionGlow,
			ImpactRotation = this.m_DescriptionImpactRotation,
			OnComplete = "DescriptionOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BA0 RID: 35744 RVA: 0x002CAD55 File Offset: 0x002C8F55
	private IEnumerator DescriptionOnComplete()
	{
		this.DisableDescription();
		yield break;
	}

	// Token: 0x06008BA1 RID: 35745 RVA: 0x002CAD64 File Offset: 0x002C8F64
	private void DisableDescription()
	{
		if (this.m_DescriptionGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_DescriptionGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BA2 RID: 35746 RVA: 0x002CADA0 File Offset: 0x002C8FA0
	private void AnimateAttack()
	{
		GameObject attackInstance = this.m_AttackInstance;
		attackInstance.transform.parent = null;
		attackInstance.transform.localScale = this.m_AttackMesh.transform.lossyScale;
		attackInstance.transform.position = this.m_AttackStartPosition.transform.position;
		attackInstance.transform.parent = base.transform.parent;
		attackInstance.GetComponent<Renderer>().SetMaterial(this.m_OrgMat_Attack);
		float startDelay = UnityEngine.Random.Range(this.m_AttackStartDelay - this.m_AttackStartDelay * this.m_RandomDelayVariance, this.m_AttackStartDelay + this.m_AttackStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Attack",
			AnimateTransform = attackInstance.transform,
			StartTransform = this.m_AttackStartPosition.transform,
			TargetTransform = this.m_AttackTargetPosition.transform,
			HitBlastParticle = this.m_AttackHitBlastParticle,
			AnimationTime = this.m_AttackAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_AttackGlow,
			ImpactRotation = this.m_AttackImpactRotation,
			OnComplete = "AttackOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BA3 RID: 35747 RVA: 0x002CAED9 File Offset: 0x002C90D9
	private IEnumerator AttackOnComplete()
	{
		this.DisableAttack();
		yield break;
	}

	// Token: 0x06008BA4 RID: 35748 RVA: 0x002CAEE8 File Offset: 0x002C90E8
	private void DisableAttack()
	{
		if (this.m_AttackGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_AttackGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BA5 RID: 35749 RVA: 0x002CAF24 File Offset: 0x002C9124
	private void AnimateHealth()
	{
		GameObject healthInstance = this.m_HealthInstance;
		healthInstance.transform.parent = null;
		healthInstance.transform.localScale = this.m_HealthMesh.transform.lossyScale;
		healthInstance.transform.position = this.m_HealthStartPosition.transform.position;
		healthInstance.transform.parent = base.transform.parent;
		healthInstance.GetComponent<Renderer>().SetMaterial(this.m_OrgMat_Health);
		float startDelay = UnityEngine.Random.Range(this.m_HealthStartDelay - this.m_HealthStartDelay * this.m_RandomDelayVariance, this.m_HealthStartDelay + this.m_HealthStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Health",
			AnimateTransform = healthInstance.transform,
			StartTransform = this.m_HealthStartPosition.transform,
			TargetTransform = this.m_HealthTargetPosition.transform,
			HitBlastParticle = this.m_HealthHitBlastParticle,
			AnimationTime = this.m_HealthAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_HealthGlow,
			ImpactRotation = this.m_HealthImpactRotation,
			OnComplete = "HealthOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BA6 RID: 35750 RVA: 0x002CB05D File Offset: 0x002C925D
	private IEnumerator HealthOnComplete()
	{
		this.DisableHealth();
		yield break;
	}

	// Token: 0x06008BA7 RID: 35751 RVA: 0x002CB06C File Offset: 0x002C926C
	private void DisableHealth()
	{
		if (this.m_HealthGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_HealthGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BA8 RID: 35752 RVA: 0x002CB0A8 File Offset: 0x002C92A8
	private void AnimateArmor()
	{
		GameObject armorInstance = this.m_ArmorInstance;
		armorInstance.transform.parent = null;
		armorInstance.transform.localScale = this.m_ArmorMesh.transform.lossyScale;
		armorInstance.transform.position = this.m_ArmorStartPosition.transform.position;
		armorInstance.transform.parent = base.transform.parent;
		armorInstance.GetComponent<Renderer>().SetMaterial(this.m_OrgMat_Armor);
		float startDelay = UnityEngine.Random.Range(this.m_ArmorStartDelay - this.m_ArmorStartDelay * this.m_RandomDelayVariance, this.m_ArmorStartDelay + this.m_ArmorStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Armor",
			AnimateTransform = armorInstance.transform,
			StartTransform = this.m_ArmorStartPosition.transform,
			TargetTransform = this.m_ArmorTargetPosition.transform,
			HitBlastParticle = this.m_ArmorHitBlastParticle,
			AnimationTime = this.m_ArmorAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_ArmorGlow,
			ImpactRotation = this.m_ArmorImpactRotation,
			OnComplete = "ArmorOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BA9 RID: 35753 RVA: 0x002CB1E1 File Offset: 0x002C93E1
	private IEnumerator ArmorOnComplete()
	{
		this.DisableArmor();
		yield break;
	}

	// Token: 0x06008BAA RID: 35754 RVA: 0x002CB1F0 File Offset: 0x002C93F0
	private void DisableArmor()
	{
		if (this.m_ArmorGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_ArmorGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BAB RID: 35755 RVA: 0x002CB22C File Offset: 0x002C942C
	private void AnimatePortrait()
	{
		GameObject portraitInstance = this.m_PortraitInstance;
		portraitInstance.transform.parent = null;
		portraitInstance.transform.localScale = this.m_PortraitMesh.transform.lossyScale;
		portraitInstance.transform.position = this.m_PortraitStartPosition.transform.position;
		portraitInstance.transform.parent = base.transform.parent;
		float startDelay = UnityEngine.Random.Range(this.m_PortraitStartDelay - this.m_PortraitStartDelay * this.m_RandomDelayVariance, this.m_PortraitStartDelay + this.m_PortraitStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Portrait",
			AnimateTransform = portraitInstance.transform,
			StartTransform = this.m_PortraitStartPosition.transform,
			TargetTransform = this.m_PortraitTargetPosition.transform,
			HitBlastParticle = this.m_PortraitHitBlastParticle,
			AnimationTime = this.m_PortraitAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_PortraitGlow,
			GlowObjectStandard = this.m_PortraitGlowStandard,
			GlowObjectUnique = this.m_PortraitGlowUnique,
			ImpactRotation = this.m_PortraitImpactRotation,
			OnComplete = "PortraitOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BAC RID: 35756 RVA: 0x002CB36C File Offset: 0x002C956C
	private IEnumerator PortraitOnComplete()
	{
		this.DisablePortrait();
		yield break;
	}

	// Token: 0x06008BAD RID: 35757 RVA: 0x002CB37C File Offset: 0x002C957C
	private void DisablePortrait()
	{
		if (this.m_PortraitGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_PortraitGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BAE RID: 35758 RVA: 0x002CB3B8 File Offset: 0x002C95B8
	private void AnimateName()
	{
		GameObject nameInstance = this.m_NameInstance;
		nameInstance.transform.parent = null;
		nameInstance.transform.localScale = this.m_NameMesh.transform.lossyScale;
		nameInstance.transform.position = this.m_NameStartPosition.transform.position;
		nameInstance.transform.parent = base.transform.parent;
		nameInstance.GetComponent<Renderer>().SetMaterial(this.m_OrgMat_Name);
		float startDelay = UnityEngine.Random.Range(this.m_NameStartDelay - this.m_NameStartDelay * this.m_RandomDelayVariance, this.m_NameStartDelay + this.m_NameStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Name",
			AnimateTransform = nameInstance.transform,
			StartTransform = this.m_NameStartPosition.transform,
			TargetTransform = this.m_NameTargetPosition.transform,
			HitBlastParticle = this.m_NameHitBlastParticle,
			AnimationTime = this.m_NameAnimTime,
			StartDelay = startDelay,
			GlowObject = this.m_NameGlow,
			ImpactRotation = this.m_NameImpactRotation,
			OnComplete = "NameOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BAF RID: 35759 RVA: 0x002CB4F1 File Offset: 0x002C96F1
	private IEnumerator NameOnComplete()
	{
		this.DisableName();
		yield break;
	}

	// Token: 0x06008BB0 RID: 35760 RVA: 0x002CB500 File Offset: 0x002C9700
	private void DisableName()
	{
		if (this.m_NameGlow)
		{
			ParticleSystem[] componentsInChildren = this.m_NameGlow.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BB1 RID: 35761 RVA: 0x002CB53C File Offset: 0x002C973C
	private void AnimateRarity()
	{
		if (this.m_Actor.GetRarity() == TAG_RARITY.FREE)
		{
			return;
		}
		GameObject rarityInstance = this.m_RarityInstance;
		rarityInstance.transform.parent = null;
		rarityInstance.transform.localScale = this.m_RarityGemMesh.transform.lossyScale;
		rarityInstance.transform.position = this.m_RarityStartPosition.transform.position;
		rarityInstance.transform.parent = base.transform.parent;
		this.m_RarityInstance.GetComponent<Renderer>().enabled = true;
		GameObject glowObject = this.m_RarityGlowCommon;
		switch (this.m_Actor.GetRarity())
		{
		case TAG_RARITY.RARE:
			glowObject = this.m_RarityGlowRare;
			break;
		case TAG_RARITY.EPIC:
			glowObject = this.m_RarityGlowEpic;
			break;
		case TAG_RARITY.LEGENDARY:
			glowObject = this.m_RarityGlowLegendary;
			break;
		}
		float startDelay = UnityEngine.Random.Range(this.m_RarityStartDelay - this.m_RarityStartDelay * this.m_RandomDelayVariance, this.m_RarityStartDelay + this.m_RarityStartDelay * this.m_RandomDelayVariance);
		ConstructCard.AnimationData value = new ConstructCard.AnimationData
		{
			Name = "Rarity",
			AnimateTransform = rarityInstance.transform,
			StartTransform = this.m_RarityStartPosition.transform,
			TargetTransform = this.m_RarityTargetPosition.transform,
			HitBlastParticle = this.m_RarityHitBlastParticle,
			AnimationTime = this.m_RarityAnimTime,
			StartDelay = startDelay,
			GlowObject = glowObject,
			ImpactRotation = this.m_RarityImpactRotation,
			OnComplete = "RarityOnComplete"
		};
		base.StartCoroutine("AnimateObject", value);
	}

	// Token: 0x06008BB2 RID: 35762 RVA: 0x002CB6C3 File Offset: 0x002C98C3
	private IEnumerator RarityOnComplete()
	{
		this.DisableRarity();
		if (this.m_Actor.GetRarity() != TAG_RARITY.FREE)
		{
			if (this.m_RarityGemMesh)
			{
				this.m_RarityGemMesh.GetComponent<Renderer>().enabled = true;
			}
			if (this.m_RarityFrameMesh)
			{
				this.m_RarityFrameMesh.GetComponent<Renderer>().enabled = true;
			}
		}
		base.StartCoroutine(this.EndAnimation());
		yield break;
	}

	// Token: 0x06008BB3 RID: 35763 RVA: 0x002CB6D4 File Offset: 0x002C98D4
	private void DisableRarity()
	{
		if (this.m_RarityGlowCommon)
		{
			ParticleSystem[] componentsInChildren = this.m_RarityGlowCommon.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
	}

	// Token: 0x06008BB4 RID: 35764 RVA: 0x002CB710 File Offset: 0x002C9910
	private IEnumerator EndAnimation()
	{
		ParticleSystem particleSystem = this.m_RarityBurstCommon;
		TAG_RARITY rarity = this.m_Actor.GetRarity();
		switch (rarity)
		{
		case TAG_RARITY.RARE:
			particleSystem = this.m_RarityBurstRare;
			break;
		case TAG_RARITY.EPIC:
			particleSystem = this.m_RarityBurstEpic;
			break;
		case TAG_RARITY.LEGENDARY:
			particleSystem = this.m_RarityBurstLegendary;
			break;
		}
		if (particleSystem)
		{
			Renderer[] componentsInChildren = particleSystem.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = true;
			}
			particleSystem.Play(true);
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
		if (this.m_FuseGlow)
		{
			this.m_FuseGlow.GetComponent<Renderer>().enabled = true;
			this.m_FuseGlow.GetComponent<Animation>().Play(animation, PlayMode.StopAll);
		}
		yield return new WaitForSeconds(0.25f);
		this.DestroyInstances();
		this.m_Actor.ShowAllText();
		this.RestoreOrgMaterials();
		yield break;
	}

	// Token: 0x06008BB5 RID: 35765 RVA: 0x002CB71F File Offset: 0x002C991F
	private IEnumerator AnimateObject(ConstructCard.AnimationData animData)
	{
		yield return new WaitForSeconds(animData.StartDelay);
		float animPos = 0f;
		float rate = 1f / (animData.AnimationTime * this.m_AnimationScale);
		Quaternion rotation = this.m_Actor.transform.rotation;
		this.m_Actor.transform.rotation = Quaternion.identity;
		Vector3 startPosition = animData.StartTransform.position;
		Quaternion startRotation = animData.StartTransform.rotation;
		this.m_Actor.transform.rotation = rotation;
		if (animData.GlowObject)
		{
			GameObject glowObject = animData.GlowObject;
			glowObject.transform.parent = animData.AnimateTransform;
			glowObject.transform.localPosition = Vector3.zero;
			ParticleSystem[] componentsInChildren = glowObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
			if (animData.GlowObjectStandard && animData.GlowObjectUnique)
			{
				if (this.m_Actor.IsElite())
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
		if (animData.HitBlastParticle)
		{
			animData.HitBlastParticle.transform.position = animData.TargetTransform.position;
			animData.HitBlastParticle.GetComponent<Renderer>().enabled = true;
			animData.HitBlastParticle.Play();
		}
		animData.AnimateTransform.parent = animData.TargetTransform;
		animData.AnimateTransform.position = animData.TargetTransform.position;
		animData.AnimateTransform.rotation = animData.TargetTransform.rotation;
		if (animData.GlowObject)
		{
			ParticleSystem[] componentsInChildren = animData.GlowObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Stop();
			}
		}
		if (this.m_Actor.gameObject == null)
		{
			yield break;
		}
		this.m_Actor.gameObject.transform.localRotation = Quaternion.Euler(animData.ImpactRotation);
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			Vector3.zero,
			"time",
			this.m_ImpactRotationTime,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"space",
			Space.Self,
			"name",
			"CardConstructImpactRotation" + animData.Name
		});
		iTween.StopByName(this.m_Actor.gameObject, "CardConstructImpactRotation" + animData.Name);
		iTween.RotateTo(this.m_Actor.gameObject, args);
		CameraShakeMgr.Shake(Camera.main, this.IMPACT_CAMERA_SHAKE_AMOUNT, this.IMPACT_CAMERA_SHAKE_TIME);
		if (animData.OnComplete != string.Empty)
		{
			base.StartCoroutine(animData.OnComplete);
		}
		yield break;
	}

	// Token: 0x06008BB6 RID: 35766 RVA: 0x002CB738 File Offset: 0x002C9938
	private void StoreOrgMaterials()
	{
		if (this.m_CardMesh)
		{
			this.m_OrgMat_CardFront = this.m_CardMesh.GetComponent<Renderer>().GetMaterial(this.m_CardFrontIdx);
		}
		if (this.m_PortraitMesh)
		{
			this.m_OrgMat_PortraitFrame = this.m_PortraitMesh.GetComponent<Renderer>().GetSharedMaterial(this.m_PortraitFrameIdx);
		}
		if (this.m_NameMesh)
		{
			this.m_OrgMat_Name = this.m_NameMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_ManaCostMesh)
		{
			this.m_OrgMat_ManaCost = this.m_ManaCostMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_AttackMesh)
		{
			this.m_OrgMat_Attack = this.m_AttackMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_HealthMesh)
		{
			this.m_OrgMat_Health = this.m_HealthMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_ArmorMesh)
		{
			this.m_OrgMat_Armor = this.m_ArmorMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_RacePlateMesh)
		{
			this.m_OrgMat_RacePlate = this.m_RacePlateMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_RarityFrameMesh)
		{
			this.m_OrgMat_RarityFrame = this.m_RarityFrameMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_DescriptionMesh)
		{
			List<Material> materials = this.m_DescriptionMesh.GetComponent<Renderer>().GetMaterials();
			if (this.m_DescriptionMesh.GetComponent<Renderer>() != null)
			{
				if (materials.Count > 1)
				{
					this.m_OrgMat_Description = materials[0];
					this.m_OrgMat_Description2 = materials[1];
				}
				else
				{
					this.m_OrgMat_Description = this.m_DescriptionMesh.GetComponent<Renderer>().GetMaterial();
				}
			}
		}
		if (this.m_DescriptionTrimMesh)
		{
			this.m_OrgMat_DescriptionTrim = this.m_DescriptionTrimMesh.GetComponent<Renderer>().GetMaterial();
		}
		if (this.m_EliteMesh)
		{
			this.m_OrgMat_Elite = this.m_EliteMesh.GetComponent<Renderer>().GetMaterial();
		}
	}

	// Token: 0x06008BB7 RID: 35767 RVA: 0x002CB940 File Offset: 0x002C9B40
	private void RestoreOrgMaterials()
	{
		this.ApplyMaterialByIdx(this.m_CardMesh, this.m_OrgMat_CardFront, this.m_CardFrontIdx);
		this.ApplySharedMaterialByIdx(this.m_PortraitMesh, this.m_OrgMat_PortraitFrame, this.m_PortraitFrameIdx);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, this.m_OrgMat_Description, 0);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, this.m_OrgMat_Description2, 1);
		this.ApplyMaterial(this.m_NameMesh, this.m_OrgMat_Name);
		this.ApplyMaterial(this.m_ManaCostMesh, this.m_OrgMat_ManaCost);
		this.ApplyMaterial(this.m_AttackMesh, this.m_OrgMat_Attack);
		this.ApplyMaterial(this.m_HealthMesh, this.m_OrgMat_Health);
		this.ApplyMaterial(this.m_ArmorMesh, this.m_OrgMat_Armor);
		this.ApplyMaterial(this.m_RacePlateMesh, this.m_OrgMat_RacePlate);
		this.ApplyMaterial(this.m_RarityFrameMesh, this.m_OrgMat_RarityFrame);
		this.ApplyMaterial(this.m_DescriptionTrimMesh, this.m_OrgMat_DescriptionTrim);
		this.ApplyMaterial(this.m_EliteMesh, this.m_OrgMat_Elite);
	}

	// Token: 0x06008BB8 RID: 35768 RVA: 0x002CBA48 File Offset: 0x002C9C48
	private void ApplyGhostMaterials()
	{
		this.ApplyMaterialByIdx(this.m_CardMesh, this.m_GhostMaterial, this.m_CardFrontIdx);
		this.ApplySharedMaterialByIdx(this.m_PortraitMesh, this.m_GhostMaterial, this.m_PortraitFrameIdx);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, this.m_GhostMaterial, 0);
		this.ApplyMaterialByIdx(this.m_DescriptionMesh, this.m_GhostMaterial, 1);
		this.ApplyMaterial(this.m_NameMesh, this.m_GhostMaterial);
		this.ApplyMaterial(this.m_ManaCostMesh, this.m_GhostMaterial);
		this.ApplyMaterial(this.m_AttackMesh, this.m_GhostMaterial);
		this.ApplyMaterial(this.m_HealthMesh, this.m_GhostMaterial);
		this.ApplyMaterial(this.m_RacePlateMesh, this.m_GhostMaterial);
		this.ApplyMaterial(this.m_RarityFrameMesh, this.m_GhostMaterial);
		if (this.m_GhostMaterialTransparent)
		{
			this.ApplyMaterial(this.m_DescriptionTrimMesh, this.m_GhostMaterialTransparent);
		}
		this.ApplyMaterial(this.m_EliteMesh, this.m_GhostMaterial);
	}

	// Token: 0x06008BB9 RID: 35769 RVA: 0x002CBB48 File Offset: 0x002C9D48
	private void ApplyMaterial(GameObject go, Material mat)
	{
		if (go == null)
		{
			return;
		}
		Renderer component = go.GetComponent<Renderer>();
		Texture mainTexture = component.GetMaterial().mainTexture;
		component.SetMaterial(mat);
		component.GetMaterial().mainTexture = mainTexture;
	}

	// Token: 0x06008BBA RID: 35770 RVA: 0x002CBB84 File Offset: 0x002C9D84
	private void ApplyMaterialByIdx(GameObject go, Material mat, int idx)
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
		Texture mainTexture = component.GetMaterial(idx).mainTexture;
		component.SetMaterial(idx, mat);
		component.GetMaterial(idx).mainTexture = mainTexture;
	}

	// Token: 0x06008BBB RID: 35771 RVA: 0x002CBBE4 File Offset: 0x002C9DE4
	private void ApplySharedMaterialByIdx(GameObject go, Material mat, int idx)
	{
		if (go == null || mat == null || idx < 0)
		{
			return;
		}
		Renderer component = go.GetComponent<Renderer>();
		List<Material> sharedMaterials = component.GetSharedMaterials();
		if (idx >= sharedMaterials.Count)
		{
			return;
		}
		Texture mainTexture = component.GetSharedMaterial(idx).mainTexture;
		component.SetSharedMaterial(idx, mat);
		component.GetSharedMaterial(idx).mainTexture = mainTexture;
	}

	// Token: 0x04007425 RID: 29733
	private readonly Vector3 IMPACT_CAMERA_SHAKE_AMOUNT = new Vector3(0.35f, 0.35f, 0.35f);

	// Token: 0x04007426 RID: 29734
	private readonly float IMPACT_CAMERA_SHAKE_TIME = 0.25f;

	// Token: 0x04007427 RID: 29735
	public Material m_GhostMaterial;

	// Token: 0x04007428 RID: 29736
	public Material m_GhostMaterialTransparent;

	// Token: 0x04007429 RID: 29737
	public float m_ImpactRotationTime = 0.5f;

	// Token: 0x0400742A RID: 29738
	public float m_RandomDelayVariance = 0.2f;

	// Token: 0x0400742B RID: 29739
	public float m_AnimationRarityScaleCommon = 1f;

	// Token: 0x0400742C RID: 29740
	public float m_AnimationRarityScaleRare = 0.9f;

	// Token: 0x0400742D RID: 29741
	public float m_AnimationRarityScaleEpic = 0.8f;

	// Token: 0x0400742E RID: 29742
	public float m_AnimationRarityScaleLegendary = 0.7f;

	// Token: 0x0400742F RID: 29743
	public GameObject m_GhostGlow;

	// Token: 0x04007430 RID: 29744
	public Texture m_GhostTextureUnique;

	// Token: 0x04007431 RID: 29745
	public GameObject m_FuseGlow;

	// Token: 0x04007432 RID: 29746
	public ParticleSystem m_RarityBurstCommon;

	// Token: 0x04007433 RID: 29747
	public ParticleSystem m_RarityBurstRare;

	// Token: 0x04007434 RID: 29748
	public ParticleSystem m_RarityBurstEpic;

	// Token: 0x04007435 RID: 29749
	public ParticleSystem m_RarityBurstLegendary;

	// Token: 0x04007436 RID: 29750
	public Transform m_ManaGemStartPosition;

	// Token: 0x04007437 RID: 29751
	public Transform m_ManaGemTargetPosition;

	// Token: 0x04007438 RID: 29752
	public float m_ManaGemStartDelay;

	// Token: 0x04007439 RID: 29753
	public float m_ManaGemAnimTime = 1f;

	// Token: 0x0400743A RID: 29754
	public GameObject m_ManaGemGlow;

	// Token: 0x0400743B RID: 29755
	public ParticleSystem m_ManaGemHitBlastParticle;

	// Token: 0x0400743C RID: 29756
	public Vector3 m_ManaGemImpactRotation = new Vector3(20f, 0f, 20f);

	// Token: 0x0400743D RID: 29757
	public Transform m_DescriptionStartPosition;

	// Token: 0x0400743E RID: 29758
	public Transform m_DescriptionTargetPosition;

	// Token: 0x0400743F RID: 29759
	public float m_DescriptionStartDelay;

	// Token: 0x04007440 RID: 29760
	public float m_DescriptionAnimTime = 1f;

	// Token: 0x04007441 RID: 29761
	public GameObject m_DescriptionGlow;

	// Token: 0x04007442 RID: 29762
	public ParticleSystem m_DescriptionHitBlastParticle;

	// Token: 0x04007443 RID: 29763
	public Vector3 m_DescriptionImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x04007444 RID: 29764
	public Transform m_AttackStartPosition;

	// Token: 0x04007445 RID: 29765
	public Transform m_AttackTargetPosition;

	// Token: 0x04007446 RID: 29766
	public float m_AttackStartDelay;

	// Token: 0x04007447 RID: 29767
	public float m_AttackAnimTime = 1f;

	// Token: 0x04007448 RID: 29768
	public GameObject m_AttackGlow;

	// Token: 0x04007449 RID: 29769
	public ParticleSystem m_AttackHitBlastParticle;

	// Token: 0x0400744A RID: 29770
	public Vector3 m_AttackImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x0400744B RID: 29771
	public Transform m_HealthStartPosition;

	// Token: 0x0400744C RID: 29772
	public Transform m_HealthTargetPosition;

	// Token: 0x0400744D RID: 29773
	public float m_HealthStartDelay;

	// Token: 0x0400744E RID: 29774
	public float m_HealthAnimTime = 1f;

	// Token: 0x0400744F RID: 29775
	public GameObject m_HealthGlow;

	// Token: 0x04007450 RID: 29776
	public ParticleSystem m_HealthHitBlastParticle;

	// Token: 0x04007451 RID: 29777
	public Vector3 m_HealthImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x04007452 RID: 29778
	public Transform m_ArmorStartPosition;

	// Token: 0x04007453 RID: 29779
	public Transform m_ArmorTargetPosition;

	// Token: 0x04007454 RID: 29780
	public float m_ArmorStartDelay;

	// Token: 0x04007455 RID: 29781
	public float m_ArmorAnimTime = 1f;

	// Token: 0x04007456 RID: 29782
	public GameObject m_ArmorGlow;

	// Token: 0x04007457 RID: 29783
	public ParticleSystem m_ArmorHitBlastParticle;

	// Token: 0x04007458 RID: 29784
	public Vector3 m_ArmorImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x04007459 RID: 29785
	public Transform m_PortraitStartPosition;

	// Token: 0x0400745A RID: 29786
	public Transform m_PortraitTargetPosition;

	// Token: 0x0400745B RID: 29787
	public float m_PortraitStartDelay;

	// Token: 0x0400745C RID: 29788
	public float m_PortraitAnimTime = 1f;

	// Token: 0x0400745D RID: 29789
	public GameObject m_PortraitGlow;

	// Token: 0x0400745E RID: 29790
	public GameObject m_PortraitGlowStandard;

	// Token: 0x0400745F RID: 29791
	public GameObject m_PortraitGlowUnique;

	// Token: 0x04007460 RID: 29792
	public ParticleSystem m_PortraitHitBlastParticle;

	// Token: 0x04007461 RID: 29793
	public Vector3 m_PortraitImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x04007462 RID: 29794
	public Transform m_NameStartPosition;

	// Token: 0x04007463 RID: 29795
	public Transform m_NameTargetPosition;

	// Token: 0x04007464 RID: 29796
	public float m_NameStartDelay;

	// Token: 0x04007465 RID: 29797
	public float m_NameAnimTime = 1f;

	// Token: 0x04007466 RID: 29798
	public GameObject m_NameGlow;

	// Token: 0x04007467 RID: 29799
	public ParticleSystem m_NameHitBlastParticle;

	// Token: 0x04007468 RID: 29800
	public Vector3 m_NameImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x04007469 RID: 29801
	public Transform m_RarityStartPosition;

	// Token: 0x0400746A RID: 29802
	public Transform m_RarityTargetPosition;

	// Token: 0x0400746B RID: 29803
	public float m_RarityStartDelay;

	// Token: 0x0400746C RID: 29804
	public float m_RarityAnimTime = 1f;

	// Token: 0x0400746D RID: 29805
	public GameObject m_RarityGlowCommon;

	// Token: 0x0400746E RID: 29806
	public GameObject m_RarityGlowRare;

	// Token: 0x0400746F RID: 29807
	public GameObject m_RarityGlowEpic;

	// Token: 0x04007470 RID: 29808
	public GameObject m_RarityGlowLegendary;

	// Token: 0x04007471 RID: 29809
	public ParticleSystem m_RarityHitBlastParticle;

	// Token: 0x04007472 RID: 29810
	public Vector3 m_RarityImpactRotation = new Vector3(-15f, 0f, 0f);

	// Token: 0x04007473 RID: 29811
	private Actor m_Actor;

	// Token: 0x04007474 RID: 29812
	private Spell m_GhostSpell;

	// Token: 0x04007475 RID: 29813
	private float m_AnimationScale = 1f;

	// Token: 0x04007476 RID: 29814
	private bool isInit;

	// Token: 0x04007477 RID: 29815
	private GameObject m_ManaGemInstance;

	// Token: 0x04007478 RID: 29816
	private GameObject m_DescriptionInstance;

	// Token: 0x04007479 RID: 29817
	private GameObject m_AttackInstance;

	// Token: 0x0400747A RID: 29818
	private GameObject m_HealthInstance;

	// Token: 0x0400747B RID: 29819
	private GameObject m_ArmorInstance;

	// Token: 0x0400747C RID: 29820
	private GameObject m_PortraitInstance;

	// Token: 0x0400747D RID: 29821
	private GameObject m_NameInstance;

	// Token: 0x0400747E RID: 29822
	private GameObject m_RarityInstance;

	// Token: 0x0400747F RID: 29823
	private GameObject m_CardMesh;

	// Token: 0x04007480 RID: 29824
	private int m_CardFrontIdx;

	// Token: 0x04007481 RID: 29825
	private GameObject m_PortraitMesh;

	// Token: 0x04007482 RID: 29826
	private int m_PortraitFrameIdx;

	// Token: 0x04007483 RID: 29827
	private GameObject m_NameMesh;

	// Token: 0x04007484 RID: 29828
	private GameObject m_DescriptionMesh;

	// Token: 0x04007485 RID: 29829
	private GameObject m_DescriptionTrimMesh;

	// Token: 0x04007486 RID: 29830
	private GameObject m_RarityGemMesh;

	// Token: 0x04007487 RID: 29831
	private GameObject m_RarityFrameMesh;

	// Token: 0x04007488 RID: 29832
	private GameObject m_ManaCostMesh;

	// Token: 0x04007489 RID: 29833
	private GameObject m_AttackMesh;

	// Token: 0x0400748A RID: 29834
	private GameObject m_HealthMesh;

	// Token: 0x0400748B RID: 29835
	private GameObject m_ArmorMesh;

	// Token: 0x0400748C RID: 29836
	private GameObject m_RacePlateMesh;

	// Token: 0x0400748D RID: 29837
	private GameObject m_EliteMesh;

	// Token: 0x0400748E RID: 29838
	private GameObject m_ManaGemClone;

	// Token: 0x0400748F RID: 29839
	private Material m_OrgMat_CardFront;

	// Token: 0x04007490 RID: 29840
	private Material m_OrgMat_PortraitFrame;

	// Token: 0x04007491 RID: 29841
	private Material m_OrgMat_Name;

	// Token: 0x04007492 RID: 29842
	private Material m_OrgMat_Description;

	// Token: 0x04007493 RID: 29843
	private Material m_OrgMat_Description2;

	// Token: 0x04007494 RID: 29844
	private Material m_OrgMat_DescriptionTrim;

	// Token: 0x04007495 RID: 29845
	private Material m_OrgMat_RarityFrame;

	// Token: 0x04007496 RID: 29846
	private Material m_OrgMat_ManaCost;

	// Token: 0x04007497 RID: 29847
	private Material m_OrgMat_Attack;

	// Token: 0x04007498 RID: 29848
	private Material m_OrgMat_Health;

	// Token: 0x04007499 RID: 29849
	private Material m_OrgMat_Armor;

	// Token: 0x0400749A RID: 29850
	private Material m_OrgMat_RacePlate;

	// Token: 0x0400749B RID: 29851
	private Material m_OrgMat_Elite;

	// Token: 0x0200268F RID: 9871
	private class AnimationData
	{
		// Token: 0x0400F106 RID: 61702
		public string Name;

		// Token: 0x0400F107 RID: 61703
		public Transform AnimateTransform;

		// Token: 0x0400F108 RID: 61704
		public Transform StartTransform;

		// Token: 0x0400F109 RID: 61705
		public Transform TargetTransform;

		// Token: 0x0400F10A RID: 61706
		public float AnimationTime = 1f;

		// Token: 0x0400F10B RID: 61707
		public float StartDelay;

		// Token: 0x0400F10C RID: 61708
		public GameObject GlowObject;

		// Token: 0x0400F10D RID: 61709
		public GameObject GlowObjectStandard;

		// Token: 0x0400F10E RID: 61710
		public GameObject GlowObjectUnique;

		// Token: 0x0400F10F RID: 61711
		public ParticleSystem HitBlastParticle;

		// Token: 0x0400F110 RID: 61712
		public Vector3 ImpactRotation;

		// Token: 0x0400F111 RID: 61713
		public string OnComplete = string.Empty;
	}
}
