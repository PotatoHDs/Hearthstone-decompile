using System.Collections.Generic;
using UnityEngine;

public class QuickBlendShape : MonoBehaviour
{
	public enum BLEND_SHAPE_ANIMATION_TYPE
	{
		Curve,
		Float
	}

	public bool m_DisableOnMobile;

	public BLEND_SHAPE_ANIMATION_TYPE m_AnimationType;

	public float m_BlendValue;

	public AnimationCurve m_BlendCurve;

	public bool m_Loop = true;

	public bool m_PlayOnAwake;

	public Mesh[] m_Meshes;

	private List<Mesh> m_BlendMeshes;

	private bool m_Play;

	private MeshFilter m_MeshFilter;

	private float m_animTime;

	private List<Material> m_BlendMaterials;

	private Mesh m_OrgMesh;

	private void Awake()
	{
		m_MeshFilter = GetComponent<MeshFilter>();
		m_OrgMesh = m_MeshFilter.sharedMesh;
		if (m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			MeshFilter component = GetComponent<MeshFilter>();
			if (component.sharedMesh == null && m_Meshes.Length != 0 && m_Meshes[0] != null)
			{
				component.sharedMesh = m_Meshes[0];
			}
		}
		else
		{
			CreateBlendMeshes();
		}
	}

	private void Update()
	{
		if ((!m_DisableOnMobile || !PlatformSettings.IsMobile()) && (m_Play || m_AnimationType == BLEND_SHAPE_ANIMATION_TYPE.Float))
		{
			BlendShapeAnimate();
		}
	}

	private void OnEnable()
	{
		if ((!m_DisableOnMobile || !PlatformSettings.IsMobile()) && m_PlayOnAwake)
		{
			PlayAnimation();
		}
	}

	private void OnDisable()
	{
		if (m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			m_MeshFilter.sharedMesh = m_OrgMesh;
			return;
		}
		m_animTime = 0f;
		if (m_BlendMaterials != null)
		{
			foreach (Material blendMaterial in m_BlendMaterials)
			{
				blendMaterial.SetFloat("_Blend", 0f);
			}
		}
		if (m_MeshFilter != null && m_OrgMesh != null)
		{
			m_MeshFilter.sharedMesh = m_OrgMesh;
		}
	}

	public void PlayAnimation()
	{
		if ((!m_DisableOnMobile || !PlatformSettings.IsMobile()) && !(m_MeshFilter == null) && m_Meshes != null && m_BlendMeshes != null)
		{
			m_animTime = 0f;
			m_Play = true;
		}
	}

	public void StopAnimation()
	{
		if (!m_DisableOnMobile || !PlatformSettings.IsMobile())
		{
			m_Play = false;
		}
	}

	private void BlendShapeAnimate()
	{
		if (m_BlendMaterials == null)
		{
			m_BlendMaterials = GetComponent<Renderer>().GetMaterials();
		}
		if (m_MeshFilter == null)
		{
			m_MeshFilter = GetComponent<MeshFilter>();
		}
		float time = m_BlendCurve.keys[m_BlendCurve.length - 1].time;
		m_animTime += Time.deltaTime;
		float num = m_BlendValue;
		if (num < 0f)
		{
			return;
		}
		if (m_AnimationType == BLEND_SHAPE_ANIMATION_TYPE.Curve)
		{
			num = m_BlendCurve.Evaluate(m_animTime);
		}
		int num2 = Mathf.FloorToInt(num);
		if (num2 > m_BlendMeshes.Count - 1)
		{
			num2 -= m_BlendMeshes.Count - 1;
		}
		m_MeshFilter.mesh = m_BlendMeshes[num2];
		foreach (Material blendMaterial in m_BlendMaterials)
		{
			blendMaterial.SetFloat("_Blend", num - (float)Mathf.FloorToInt(num));
		}
		if (m_animTime > time)
		{
			if (m_Loop)
			{
				m_animTime = 0f;
			}
			else
			{
				m_Play = false;
			}
		}
	}

	private void CreateBlendMeshes()
	{
		m_BlendMeshes = new List<Mesh>();
		for (int i = 0; i < m_Meshes.Length; i++)
		{
			if (GetComponent<MeshFilter>() == null)
			{
				base.gameObject.AddComponent<MeshFilter>();
			}
			Mesh mesh = Object.Instantiate(m_Meshes[i]);
			int num = m_Meshes[i].vertices.Length;
			int num2 = i + 1;
			if (num2 > m_Meshes.Length - 1)
			{
				num2 = 0;
			}
			Mesh obj = m_Meshes[num2];
			Vector4[] array = new Vector4[num];
			Vector3[] vertices = obj.vertices;
			for (int j = 0; j < num; j++)
			{
				if (j <= vertices.Length - 1)
				{
					Vector3 vector = vertices[j];
					array[j] = new Vector4(vector.x, vector.y, vector.z, 1f);
				}
			}
			mesh.vertices = m_Meshes[i].vertices;
			mesh.tangents = array;
			m_BlendMeshes.Add(mesh);
		}
	}
}
