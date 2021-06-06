using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A65 RID: 2661
public class QuickBlendShape : MonoBehaviour
{
	// Token: 0x06008EF2 RID: 36594 RVA: 0x002E32E8 File Offset: 0x002E14E8
	private void Awake()
	{
		this.m_MeshFilter = base.GetComponent<MeshFilter>();
		this.m_OrgMesh = this.m_MeshFilter.sharedMesh;
		if (this.m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			MeshFilter component = base.GetComponent<MeshFilter>();
			if (component.sharedMesh == null && this.m_Meshes.Length != 0 && this.m_Meshes[0] != null)
			{
				component.sharedMesh = this.m_Meshes[0];
			}
			return;
		}
		this.CreateBlendMeshes();
	}

	// Token: 0x06008EF3 RID: 36595 RVA: 0x002E3364 File Offset: 0x002E1564
	private void Update()
	{
		if (this.m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			return;
		}
		if (!this.m_Play && this.m_AnimationType != QuickBlendShape.BLEND_SHAPE_ANIMATION_TYPE.Float)
		{
			return;
		}
		this.BlendShapeAnimate();
	}

	// Token: 0x06008EF4 RID: 36596 RVA: 0x002E338E File Offset: 0x002E158E
	private void OnEnable()
	{
		if (this.m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			return;
		}
		if (this.m_PlayOnAwake)
		{
			this.PlayAnimation();
		}
	}

	// Token: 0x06008EF5 RID: 36597 RVA: 0x002E33B0 File Offset: 0x002E15B0
	private void OnDisable()
	{
		if (this.m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			this.m_MeshFilter.sharedMesh = this.m_OrgMesh;
			return;
		}
		this.m_animTime = 0f;
		if (this.m_BlendMaterials != null)
		{
			foreach (Material material in this.m_BlendMaterials)
			{
				material.SetFloat("_Blend", 0f);
			}
		}
		if (this.m_MeshFilter != null && this.m_OrgMesh != null)
		{
			this.m_MeshFilter.sharedMesh = this.m_OrgMesh;
		}
	}

	// Token: 0x06008EF6 RID: 36598 RVA: 0x002E346C File Offset: 0x002E166C
	public void PlayAnimation()
	{
		if (this.m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			return;
		}
		if (this.m_MeshFilter == null)
		{
			return;
		}
		if (this.m_Meshes == null)
		{
			return;
		}
		if (this.m_BlendMeshes == null)
		{
			return;
		}
		this.m_animTime = 0f;
		this.m_Play = true;
	}

	// Token: 0x06008EF7 RID: 36599 RVA: 0x002E34BC File Offset: 0x002E16BC
	public void StopAnimation()
	{
		if (this.m_DisableOnMobile && PlatformSettings.IsMobile())
		{
			return;
		}
		this.m_Play = false;
	}

	// Token: 0x06008EF8 RID: 36600 RVA: 0x002E34D8 File Offset: 0x002E16D8
	private void BlendShapeAnimate()
	{
		if (this.m_BlendMaterials == null)
		{
			this.m_BlendMaterials = base.GetComponent<Renderer>().GetMaterials();
		}
		if (this.m_MeshFilter == null)
		{
			this.m_MeshFilter = base.GetComponent<MeshFilter>();
		}
		float time = this.m_BlendCurve.keys[this.m_BlendCurve.length - 1].time;
		this.m_animTime += Time.deltaTime;
		float num = this.m_BlendValue;
		if (num < 0f)
		{
			return;
		}
		if (this.m_AnimationType == QuickBlendShape.BLEND_SHAPE_ANIMATION_TYPE.Curve)
		{
			num = this.m_BlendCurve.Evaluate(this.m_animTime);
		}
		int num2 = Mathf.FloorToInt(num);
		if (num2 > this.m_BlendMeshes.Count - 1)
		{
			num2 -= this.m_BlendMeshes.Count - 1;
		}
		this.m_MeshFilter.mesh = this.m_BlendMeshes[num2];
		foreach (Material material in this.m_BlendMaterials)
		{
			material.SetFloat("_Blend", num - (float)Mathf.FloorToInt(num));
		}
		if (this.m_animTime > time)
		{
			if (this.m_Loop)
			{
				this.m_animTime = 0f;
				return;
			}
			this.m_Play = false;
		}
	}

	// Token: 0x06008EF9 RID: 36601 RVA: 0x002E362C File Offset: 0x002E182C
	private void CreateBlendMeshes()
	{
		this.m_BlendMeshes = new List<Mesh>();
		for (int i = 0; i < this.m_Meshes.Length; i++)
		{
			if (base.GetComponent<MeshFilter>() == null)
			{
				base.gameObject.AddComponent<MeshFilter>();
			}
			Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(this.m_Meshes[i]);
			int num = this.m_Meshes[i].vertices.Length;
			int num2 = i + 1;
			if (num2 > this.m_Meshes.Length - 1)
			{
				num2 = 0;
			}
			Mesh mesh2 = this.m_Meshes[num2];
			Vector4[] array = new Vector4[num];
			Vector3[] vertices = mesh2.vertices;
			for (int j = 0; j < num; j++)
			{
				if (j <= vertices.Length - 1)
				{
					Vector3 vector = vertices[j];
					array[j] = new Vector4(vector.x, vector.y, vector.z, 1f);
				}
			}
			mesh.vertices = this.m_Meshes[i].vertices;
			mesh.tangents = array;
			this.m_BlendMeshes.Add(mesh);
		}
	}

	// Token: 0x04007765 RID: 30565
	public bool m_DisableOnMobile;

	// Token: 0x04007766 RID: 30566
	public QuickBlendShape.BLEND_SHAPE_ANIMATION_TYPE m_AnimationType;

	// Token: 0x04007767 RID: 30567
	public float m_BlendValue;

	// Token: 0x04007768 RID: 30568
	public AnimationCurve m_BlendCurve;

	// Token: 0x04007769 RID: 30569
	public bool m_Loop = true;

	// Token: 0x0400776A RID: 30570
	public bool m_PlayOnAwake;

	// Token: 0x0400776B RID: 30571
	public Mesh[] m_Meshes;

	// Token: 0x0400776C RID: 30572
	private List<Mesh> m_BlendMeshes;

	// Token: 0x0400776D RID: 30573
	private bool m_Play;

	// Token: 0x0400776E RID: 30574
	private MeshFilter m_MeshFilter;

	// Token: 0x0400776F RID: 30575
	private float m_animTime;

	// Token: 0x04007770 RID: 30576
	private List<Material> m_BlendMaterials;

	// Token: 0x04007771 RID: 30577
	private Mesh m_OrgMesh;

	// Token: 0x020026BE RID: 9918
	public enum BLEND_SHAPE_ANIMATION_TYPE
	{
		// Token: 0x0400F1E4 RID: 61924
		Curve,
		// Token: 0x0400F1E5 RID: 61925
		Float
	}
}
