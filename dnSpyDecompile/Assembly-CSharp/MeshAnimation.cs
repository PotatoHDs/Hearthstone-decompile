using System;
using UnityEngine;

// Token: 0x02000A4E RID: 2638
public class MeshAnimation : MonoBehaviour
{
	// Token: 0x06008E69 RID: 36457 RVA: 0x002DF2F7 File Offset: 0x002DD4F7
	private void Start()
	{
		this.m_Mesh = base.GetComponent<MeshFilter>();
	}

	// Token: 0x06008E6A RID: 36458 RVA: 0x002DF308 File Offset: 0x002DD508
	private void Update()
	{
		if (!this.m_Playing)
		{
			return;
		}
		this.m_FrameTime += Time.deltaTime;
		if (this.m_FrameTime >= this.FrameDuration)
		{
			this.m_Index = (this.m_Index + 1) % this.Meshes.Length;
			this.m_FrameTime -= this.FrameDuration;
			if (!this.Loop && this.m_Index == 0)
			{
				this.m_Playing = false;
				base.enabled = false;
				return;
			}
			this.m_Mesh.mesh = this.Meshes[this.m_Index];
		}
	}

	// Token: 0x06008E6B RID: 36459 RVA: 0x002DF39F File Offset: 0x002DD59F
	public void Play()
	{
		base.enabled = true;
		this.m_Playing = true;
	}

	// Token: 0x06008E6C RID: 36460 RVA: 0x002DF3AF File Offset: 0x002DD5AF
	public void Stop()
	{
		this.m_Playing = false;
		base.enabled = false;
	}

	// Token: 0x06008E6D RID: 36461 RVA: 0x002DF3BF File Offset: 0x002DD5BF
	public void Reset()
	{
		this.m_Mesh.mesh = this.Meshes[0];
		this.m_FrameTime = 0f;
		this.m_Index = 0;
	}

	// Token: 0x04007688 RID: 30344
	public Mesh[] Meshes;

	// Token: 0x04007689 RID: 30345
	public bool Loop;

	// Token: 0x0400768A RID: 30346
	public float FrameDuration;

	// Token: 0x0400768B RID: 30347
	private int m_Index;

	// Token: 0x0400768C RID: 30348
	private bool m_Playing;

	// Token: 0x0400768D RID: 30349
	private float m_FrameTime;

	// Token: 0x0400768E RID: 30350
	private MeshFilter m_Mesh;
}
