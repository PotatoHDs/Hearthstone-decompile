using System;
using UnityEngine;

// Token: 0x02000A4F RID: 2639
public class MeshUVScaler : MonoBehaviour
{
	// Token: 0x06008E6F RID: 36463 RVA: 0x002DF3E8 File Offset: 0x002DD5E8
	private void OnEnable()
	{
		this.meshFilter = base.GetComponent<MeshFilter>();
		this.skinnedMeshRenderer = base.GetComponent<SkinnedMeshRenderer>();
		if (this.meshFilter)
		{
			this.mesh = this.meshFilter.mesh;
		}
		else if (this.skinnedMeshRenderer)
		{
			this.mesh = this.skinnedMeshRenderer.sharedMesh;
		}
		if (!this.mesh)
		{
			base.enabled = false;
		}
		this.uvcache = this.mesh.uv;
		this.uvs = this.mesh.uv;
		this.UVScaleX = 1f;
		this.UVScaleY = 1f;
	}

	// Token: 0x06008E70 RID: 36464 RVA: 0x002DF498 File Offset: 0x002DD698
	private void Update()
	{
		if (!this.mesh)
		{
			return;
		}
		for (int i = 0; i < this.uvcache.Length; i++)
		{
			this.uvs[i] = new Vector2(this.uvcache[i].x * this.UVScaleX, this.uvcache[i].y * this.UVScaleY);
		}
		this.mesh.uv = this.uvs;
	}

	// Token: 0x06008E71 RID: 36465 RVA: 0x002DF518 File Offset: 0x002DD718
	private void OnDisable()
	{
		this.mesh.uv = this.uvcache;
	}

	// Token: 0x0400768F RID: 30351
	public float UVScaleX;

	// Token: 0x04007690 RID: 30352
	public float UVScaleY;

	// Token: 0x04007691 RID: 30353
	private Vector2[] uvcache;

	// Token: 0x04007692 RID: 30354
	private Vector2[] uvs;

	// Token: 0x04007693 RID: 30355
	private MeshFilter meshFilter;

	// Token: 0x04007694 RID: 30356
	private SkinnedMeshRenderer skinnedMeshRenderer;

	// Token: 0x04007695 RID: 30357
	private Mesh mesh;
}
