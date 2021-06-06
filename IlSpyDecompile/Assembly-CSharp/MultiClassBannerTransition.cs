using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiClassBannerTransition : MonoBehaviour
{
	[Serializable]
	public class ClassMaterials
	{
		public TAG_CLASS m_Class;

		public Color m_BannerColor;

		public Color m_GlowColor;

		public Material m_EffectMaterial;
	}

	public List<TAG_CLASS> m_ClassList;

	public List<ClassMaterials> m_ClassMaterials;

	public GameObject m_BannerMesh;

	public Material m_GoldenRibbonMaterial;

	public float m_TransitionTime = 0.5f;

	public float m_HoldTime = 2.5f;

	public float m_MoteDelayTime = 0.6f;

	public GameObject m_ParticleFXRoot;

	public ParticleSystem m_TransitionFXParticles;

	public ParticleSystem m_subFXSparks;

	public ParticleSystem m_subFXAura;

	public ParticleSystem m_subFXMotes;

	public GameObject m_Shadow;

	private GameObject m_GoldenRibbonMesh;

	private List<Color> m_bannerColors;

	private List<Color> m_glowColors;

	private List<Material> m_effectMaterials;

	private int m_currentClassNum;

	private float m_nextTransitionTime;

	private float m_transitionEndTime;

	private int m_multiClassGroupID;

	private GameObject m_multiClassGroupIcon;

	private Material m_bannerMaterial;

	private Material m_goldenRibbonMaterial;

	private float m_materialDissolve;

	private Renderer m_TransitionFXMat;

	private Renderer m_subFXSparksMat;

	public static int CompareClasses(TAG_CLASS classA, TAG_CLASS classB)
	{
		int num = Array.IndexOf(CollectionPageManager.CLASS_TAB_ORDER, classA);
		if (num < 0)
		{
			return 1;
		}
		int num2 = Array.IndexOf(CollectionPageManager.CLASS_TAB_ORDER, classB);
		if (num2 < 0)
		{
			return -1;
		}
		return num - num2;
	}

	private ClassMaterials GetClassMaterials(TAG_CLASS cycleClass)
	{
		foreach (ClassMaterials classMaterial in m_ClassMaterials)
		{
			if (classMaterial.m_Class == cycleClass)
			{
				return classMaterial;
			}
		}
		return null;
	}

	public void SetClasses(IEnumerable<TAG_CLASS> classes)
	{
		m_ClassList.Clear();
		m_ClassList.AddRange(classes);
		UpdateClassList();
		TransitionClass();
	}

	public void SetMultiClassGroup(int groupID)
	{
		if (m_multiClassGroupID == groupID)
		{
			return;
		}
		m_multiClassGroupID = groupID;
		if (m_multiClassGroupIcon != null)
		{
			UnityEngine.Object.Destroy(m_multiClassGroupIcon);
		}
		MultiClassGroupDbfRecord record = GameDbf.MultiClassGroup.GetRecord(groupID);
		if (record != null && !string.IsNullOrEmpty(record.IconAssetPath))
		{
			m_multiClassGroupIcon = AssetLoader.Get().InstantiatePrefab(record.IconAssetPath);
			if (!(m_multiClassGroupIcon == null))
			{
				m_multiClassGroupIcon.transform.parent = m_BannerMesh.transform.parent;
				TransformUtil.Identity(m_multiClassGroupIcon);
			}
		}
	}

	public void UpdateClassList()
	{
		if (m_bannerColors == null)
		{
			m_bannerColors = new List<Color>();
		}
		else
		{
			m_bannerColors.Clear();
		}
		if (m_glowColors == null)
		{
			m_glowColors = new List<Color>();
		}
		else
		{
			m_glowColors.Clear();
		}
		if (m_effectMaterials == null)
		{
			m_effectMaterials = new List<Material>();
		}
		else
		{
			m_effectMaterials.Clear();
		}
		foreach (TAG_CLASS @class in m_ClassList)
		{
			ClassMaterials classMaterials = GetClassMaterials(@class);
			m_bannerColors.Add(classMaterials.m_BannerColor);
			m_glowColors.Add(classMaterials.m_GlowColor);
			m_effectMaterials.Add(classMaterials.m_EffectMaterial);
		}
	}

	public void Awake()
	{
		UpdateClassList();
		m_bannerMaterial = m_BannerMesh.GetComponent<Renderer>().GetMaterial();
		m_TransitionFXMat = m_TransitionFXParticles.GetComponent<ParticleSystem>().GetComponent<Renderer>();
		m_subFXSparksMat = m_subFXSparks.GetComponent<Renderer>();
		ParticleSystem.MainModule main = m_subFXMotes.main;
		main.startDelay = m_MoteDelayTime;
		m_bannerMaterial.SetFloat("_DissolveTime", 0f);
		UpdateMaterials();
	}

	private void TransitionClass()
	{
		m_nextTransitionTime = Time.time + m_HoldTime;
		m_transitionEndTime = Time.time + m_TransitionTime;
		m_currentClassNum = (m_currentClassNum + 1) % m_ClassList.Count;
		UpdateMaterials();
		PlayParticles();
	}

	public void TransitionClassImmediately()
	{
		m_currentClassNum = (m_currentClassNum + 1) % m_ClassList.Count;
		m_bannerMaterial.SetFloat("_DissolveTime", 0f);
		if (m_GoldenRibbonMesh != null && m_goldenRibbonMaterial != null)
		{
			m_goldenRibbonMaterial.SetFloat("_DissolveTime", 0f);
		}
		UpdateMaterials();
	}

	public void Update()
	{
		if (m_ClassList.Count > 1 && Time.time > m_nextTransitionTime)
		{
			TransitionClass();
		}
		if ((double)Time.time < (double)m_transitionEndTime + (double)m_TransitionTime * 0.25)
		{
			m_materialDissolve = Mathf.Clamp((m_transitionEndTime - Time.time) / m_TransitionTime, 0f, 1f);
			m_bannerMaterial.SetFloat("_DissolveTime", m_materialDissolve);
			if (m_GoldenRibbonMesh != null && m_goldenRibbonMaterial != null)
			{
				m_goldenRibbonMaterial.SetFloat("_DissolveTime", m_materialDissolve);
			}
		}
	}

	public void SetGoldenCardMesh(GameObject mesh, int matIdx)
	{
		if (matIdx < 0)
		{
			return;
		}
		m_GoldenRibbonMesh = mesh;
		if (m_GoldenRibbonMesh != null)
		{
			Renderer component = m_GoldenRibbonMesh.GetComponent<Renderer>();
			List<Material> materials = component.GetMaterials();
			if (matIdx < materials.Count)
			{
				component.SetMaterial(matIdx, m_GoldenRibbonMaterial);
				m_goldenRibbonMaterial = component.GetMaterial(matIdx);
				m_goldenRibbonMaterial.SetFloat("_DissolveTime", 0f);
				UpdateMaterials();
			}
		}
	}

	public void TurnOffShadowsAndFX()
	{
		m_ParticleFXRoot.SetActive(value: false);
		m_Shadow.gameObject.SetActive(value: false);
		m_multiClassGroupIcon.GetComponent<MultiClassIcon>().m_ShadowMesh.SetActive(value: false);
	}

	private void PlayParticles()
	{
		UpdateParticleScale();
		m_TransitionFXParticles.Play();
		m_subFXSparks.Play();
		m_subFXAura.Play();
		m_subFXMotes.Play();
	}

	private void UpdateParticleScale()
	{
		int nextClassNum = GetNextClassNum();
		m_TransitionFXMat.SetMaterial(m_effectMaterials[nextClassNum]);
		m_subFXSparksMat.SetMaterial(m_effectMaterials[nextClassNum]);
		ParticleSystem.MainModule main = m_subFXAura.main;
		ParticleSystem.MainModule main2 = m_subFXMotes.main;
		main.startColor = m_glowColors[nextClassNum];
		main2.startColor = m_bannerColors[nextClassNum];
		Vector3 lossyScale = base.transform.lossyScale;
		float num = Mathf.Max(lossyScale.x, Mathf.Max(lossyScale.y, lossyScale.z));
		lossyScale = new Vector3(num, num, num);
		m_TransitionFXParticles.transform.localScale = lossyScale;
		m_subFXAura.transform.localScale = lossyScale;
		m_subFXMotes.transform.localScale = lossyScale;
		m_subFXSparks.transform.localScale = lossyScale;
	}

	private void UpdateMaterials()
	{
		int nextClassNum = GetNextClassNum();
		m_bannerMaterial.SetColor("_Color1", m_bannerColors[m_currentClassNum]);
		m_bannerMaterial.SetColor("_Color2", m_bannerColors[nextClassNum]);
		m_bannerMaterial.SetColor("_ColorGlow", m_glowColors[nextClassNum]);
		if (m_GoldenRibbonMesh != null && m_goldenRibbonMaterial != null)
		{
			m_goldenRibbonMaterial.SetColor("_Color1", m_bannerColors[m_currentClassNum]);
			m_goldenRibbonMaterial.SetColor("_Color2", m_bannerColors[nextClassNum]);
			m_goldenRibbonMaterial.SetColor("_ColorGlow", m_glowColors[nextClassNum]);
		}
	}

	private int GetNextClassNum()
	{
		return (m_currentClassNum + 1) % m_ClassList.Count;
	}
}
