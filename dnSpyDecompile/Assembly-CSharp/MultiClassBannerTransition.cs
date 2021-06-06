using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A54 RID: 2644
public class MultiClassBannerTransition : MonoBehaviour
{
	// Token: 0x06008E8B RID: 36491 RVA: 0x002DFF38 File Offset: 0x002DE138
	public static int CompareClasses(TAG_CLASS classA, TAG_CLASS classB)
	{
		int num = Array.IndexOf<TAG_CLASS>(CollectionPageManager.CLASS_TAB_ORDER, classA);
		if (num < 0)
		{
			return 1;
		}
		int num2 = Array.IndexOf<TAG_CLASS>(CollectionPageManager.CLASS_TAB_ORDER, classB);
		if (num2 < 0)
		{
			return -1;
		}
		return num - num2;
	}

	// Token: 0x06008E8C RID: 36492 RVA: 0x002DFF6C File Offset: 0x002DE16C
	private MultiClassBannerTransition.ClassMaterials GetClassMaterials(TAG_CLASS cycleClass)
	{
		foreach (MultiClassBannerTransition.ClassMaterials classMaterials in this.m_ClassMaterials)
		{
			if (classMaterials.m_Class == cycleClass)
			{
				return classMaterials;
			}
		}
		return null;
	}

	// Token: 0x06008E8D RID: 36493 RVA: 0x002DFFC8 File Offset: 0x002DE1C8
	public void SetClasses(IEnumerable<TAG_CLASS> classes)
	{
		this.m_ClassList.Clear();
		this.m_ClassList.AddRange(classes);
		this.UpdateClassList();
		this.TransitionClass();
	}

	// Token: 0x06008E8E RID: 36494 RVA: 0x002DFFF0 File Offset: 0x002DE1F0
	public void SetMultiClassGroup(int groupID)
	{
		if (this.m_multiClassGroupID == groupID)
		{
			return;
		}
		this.m_multiClassGroupID = groupID;
		if (this.m_multiClassGroupIcon != null)
		{
			UnityEngine.Object.Destroy(this.m_multiClassGroupIcon);
		}
		MultiClassGroupDbfRecord record = GameDbf.MultiClassGroup.GetRecord(groupID);
		if (record == null || string.IsNullOrEmpty(record.IconAssetPath))
		{
			return;
		}
		this.m_multiClassGroupIcon = AssetLoader.Get().InstantiatePrefab(record.IconAssetPath, AssetLoadingOptions.None);
		if (this.m_multiClassGroupIcon == null)
		{
			return;
		}
		this.m_multiClassGroupIcon.transform.parent = this.m_BannerMesh.transform.parent;
		TransformUtil.Identity(this.m_multiClassGroupIcon);
	}

	// Token: 0x06008E8F RID: 36495 RVA: 0x002E009C File Offset: 0x002DE29C
	public void UpdateClassList()
	{
		if (this.m_bannerColors == null)
		{
			this.m_bannerColors = new List<Color>();
		}
		else
		{
			this.m_bannerColors.Clear();
		}
		if (this.m_glowColors == null)
		{
			this.m_glowColors = new List<Color>();
		}
		else
		{
			this.m_glowColors.Clear();
		}
		if (this.m_effectMaterials == null)
		{
			this.m_effectMaterials = new List<Material>();
		}
		else
		{
			this.m_effectMaterials.Clear();
		}
		foreach (TAG_CLASS cycleClass in this.m_ClassList)
		{
			MultiClassBannerTransition.ClassMaterials classMaterials = this.GetClassMaterials(cycleClass);
			this.m_bannerColors.Add(classMaterials.m_BannerColor);
			this.m_glowColors.Add(classMaterials.m_GlowColor);
			this.m_effectMaterials.Add(classMaterials.m_EffectMaterial);
		}
	}

	// Token: 0x06008E90 RID: 36496 RVA: 0x002E0184 File Offset: 0x002DE384
	public void Awake()
	{
		this.UpdateClassList();
		this.m_bannerMaterial = this.m_BannerMesh.GetComponent<Renderer>().GetMaterial();
		this.m_TransitionFXMat = this.m_TransitionFXParticles.GetComponent<ParticleSystem>().GetComponent<Renderer>();
		this.m_subFXSparksMat = this.m_subFXSparks.GetComponent<Renderer>();
		this.m_subFXMotes.main.startDelay = this.m_MoteDelayTime;
		this.m_bannerMaterial.SetFloat("_DissolveTime", 0f);
		this.UpdateMaterials();
	}

	// Token: 0x06008E91 RID: 36497 RVA: 0x002E0210 File Offset: 0x002DE410
	private void TransitionClass()
	{
		this.m_nextTransitionTime = Time.time + this.m_HoldTime;
		this.m_transitionEndTime = Time.time + this.m_TransitionTime;
		this.m_currentClassNum = (this.m_currentClassNum + 1) % this.m_ClassList.Count;
		this.UpdateMaterials();
		this.PlayParticles();
	}

	// Token: 0x06008E92 RID: 36498 RVA: 0x002E0268 File Offset: 0x002DE468
	public void TransitionClassImmediately()
	{
		this.m_currentClassNum = (this.m_currentClassNum + 1) % this.m_ClassList.Count;
		this.m_bannerMaterial.SetFloat("_DissolveTime", 0f);
		if (this.m_GoldenRibbonMesh != null && this.m_goldenRibbonMaterial != null)
		{
			this.m_goldenRibbonMaterial.SetFloat("_DissolveTime", 0f);
		}
		this.UpdateMaterials();
	}

	// Token: 0x06008E93 RID: 36499 RVA: 0x002E02DC File Offset: 0x002DE4DC
	public void Update()
	{
		if (this.m_ClassList.Count > 1 && Time.time > this.m_nextTransitionTime)
		{
			this.TransitionClass();
		}
		if ((double)Time.time < (double)this.m_transitionEndTime + (double)this.m_TransitionTime * 0.25)
		{
			this.m_materialDissolve = Mathf.Clamp((this.m_transitionEndTime - Time.time) / this.m_TransitionTime, 0f, 1f);
			this.m_bannerMaterial.SetFloat("_DissolveTime", this.m_materialDissolve);
			if (this.m_GoldenRibbonMesh != null && this.m_goldenRibbonMaterial != null)
			{
				this.m_goldenRibbonMaterial.SetFloat("_DissolveTime", this.m_materialDissolve);
			}
		}
	}

	// Token: 0x06008E94 RID: 36500 RVA: 0x002E039C File Offset: 0x002DE59C
	public void SetGoldenCardMesh(GameObject mesh, int matIdx)
	{
		if (matIdx < 0)
		{
			return;
		}
		this.m_GoldenRibbonMesh = mesh;
		if (this.m_GoldenRibbonMesh != null)
		{
			Renderer component = this.m_GoldenRibbonMesh.GetComponent<Renderer>();
			List<Material> materials = component.GetMaterials();
			if (matIdx >= materials.Count)
			{
				return;
			}
			component.SetMaterial(matIdx, this.m_GoldenRibbonMaterial);
			this.m_goldenRibbonMaterial = component.GetMaterial(matIdx);
			this.m_goldenRibbonMaterial.SetFloat("_DissolveTime", 0f);
			this.UpdateMaterials();
		}
	}

	// Token: 0x06008E95 RID: 36501 RVA: 0x002E0415 File Offset: 0x002DE615
	public void TurnOffShadowsAndFX()
	{
		this.m_ParticleFXRoot.SetActive(false);
		this.m_Shadow.gameObject.SetActive(false);
		this.m_multiClassGroupIcon.GetComponent<MultiClassIcon>().m_ShadowMesh.SetActive(false);
	}

	// Token: 0x06008E96 RID: 36502 RVA: 0x002E044A File Offset: 0x002DE64A
	private void PlayParticles()
	{
		this.UpdateParticleScale();
		this.m_TransitionFXParticles.Play();
		this.m_subFXSparks.Play();
		this.m_subFXAura.Play();
		this.m_subFXMotes.Play();
	}

	// Token: 0x06008E97 RID: 36503 RVA: 0x002E0480 File Offset: 0x002DE680
	private void UpdateParticleScale()
	{
		int nextClassNum = this.GetNextClassNum();
		this.m_TransitionFXMat.SetMaterial(this.m_effectMaterials[nextClassNum]);
		this.m_subFXSparksMat.SetMaterial(this.m_effectMaterials[nextClassNum]);
		ParticleSystem.MainModule main = this.m_subFXAura.main;
		ParticleSystem.MainModule main2 = this.m_subFXMotes.main;
		main.startColor = this.m_glowColors[nextClassNum];
		main2.startColor = this.m_bannerColors[nextClassNum];
		Vector3 lossyScale = base.transform.lossyScale;
		float num = Mathf.Max(lossyScale.x, Mathf.Max(lossyScale.y, lossyScale.z));
		lossyScale = new Vector3(num, num, num);
		this.m_TransitionFXParticles.transform.localScale = lossyScale;
		this.m_subFXAura.transform.localScale = lossyScale;
		this.m_subFXMotes.transform.localScale = lossyScale;
		this.m_subFXSparks.transform.localScale = lossyScale;
	}

	// Token: 0x06008E98 RID: 36504 RVA: 0x002E0588 File Offset: 0x002DE788
	private void UpdateMaterials()
	{
		int nextClassNum = this.GetNextClassNum();
		this.m_bannerMaterial.SetColor("_Color1", this.m_bannerColors[this.m_currentClassNum]);
		this.m_bannerMaterial.SetColor("_Color2", this.m_bannerColors[nextClassNum]);
		this.m_bannerMaterial.SetColor("_ColorGlow", this.m_glowColors[nextClassNum]);
		if (this.m_GoldenRibbonMesh != null && this.m_goldenRibbonMaterial != null)
		{
			this.m_goldenRibbonMaterial.SetColor("_Color1", this.m_bannerColors[this.m_currentClassNum]);
			this.m_goldenRibbonMaterial.SetColor("_Color2", this.m_bannerColors[nextClassNum]);
			this.m_goldenRibbonMaterial.SetColor("_ColorGlow", this.m_glowColors[nextClassNum]);
		}
	}

	// Token: 0x06008E99 RID: 36505 RVA: 0x002E066A File Offset: 0x002DE86A
	private int GetNextClassNum()
	{
		return (this.m_currentClassNum + 1) % this.m_ClassList.Count;
	}

	// Token: 0x040076C2 RID: 30402
	public List<TAG_CLASS> m_ClassList;

	// Token: 0x040076C3 RID: 30403
	public List<MultiClassBannerTransition.ClassMaterials> m_ClassMaterials;

	// Token: 0x040076C4 RID: 30404
	public GameObject m_BannerMesh;

	// Token: 0x040076C5 RID: 30405
	public Material m_GoldenRibbonMaterial;

	// Token: 0x040076C6 RID: 30406
	public float m_TransitionTime = 0.5f;

	// Token: 0x040076C7 RID: 30407
	public float m_HoldTime = 2.5f;

	// Token: 0x040076C8 RID: 30408
	public float m_MoteDelayTime = 0.6f;

	// Token: 0x040076C9 RID: 30409
	public GameObject m_ParticleFXRoot;

	// Token: 0x040076CA RID: 30410
	public ParticleSystem m_TransitionFXParticles;

	// Token: 0x040076CB RID: 30411
	public ParticleSystem m_subFXSparks;

	// Token: 0x040076CC RID: 30412
	public ParticleSystem m_subFXAura;

	// Token: 0x040076CD RID: 30413
	public ParticleSystem m_subFXMotes;

	// Token: 0x040076CE RID: 30414
	public GameObject m_Shadow;

	// Token: 0x040076CF RID: 30415
	private GameObject m_GoldenRibbonMesh;

	// Token: 0x040076D0 RID: 30416
	private List<Color> m_bannerColors;

	// Token: 0x040076D1 RID: 30417
	private List<Color> m_glowColors;

	// Token: 0x040076D2 RID: 30418
	private List<Material> m_effectMaterials;

	// Token: 0x040076D3 RID: 30419
	private int m_currentClassNum;

	// Token: 0x040076D4 RID: 30420
	private float m_nextTransitionTime;

	// Token: 0x040076D5 RID: 30421
	private float m_transitionEndTime;

	// Token: 0x040076D6 RID: 30422
	private int m_multiClassGroupID;

	// Token: 0x040076D7 RID: 30423
	private GameObject m_multiClassGroupIcon;

	// Token: 0x040076D8 RID: 30424
	private Material m_bannerMaterial;

	// Token: 0x040076D9 RID: 30425
	private Material m_goldenRibbonMaterial;

	// Token: 0x040076DA RID: 30426
	private float m_materialDissolve;

	// Token: 0x040076DB RID: 30427
	private Renderer m_TransitionFXMat;

	// Token: 0x040076DC RID: 30428
	private Renderer m_subFXSparksMat;

	// Token: 0x020026BA RID: 9914
	[Serializable]
	public class ClassMaterials
	{
		// Token: 0x0400F1D5 RID: 61909
		public TAG_CLASS m_Class;

		// Token: 0x0400F1D6 RID: 61910
		public Color m_BannerColor;

		// Token: 0x0400F1D7 RID: 61911
		public Color m_GlowColor;

		// Token: 0x0400F1D8 RID: 61912
		public Material m_EffectMaterial;
	}
}
