using System;
using UnityEngine;

// Token: 0x02000A19 RID: 2585
public class ChangeMaterialFloat : MonoBehaviour
{
	// Token: 0x06008B6E RID: 35694 RVA: 0x002C94AC File Offset: 0x002C76AC
	private void Start()
	{
		this.m_intensityProperty = Shader.PropertyToID("_Intensity");
		if (this.m_Rend1 != null)
		{
			this.m_mat1 = this.m_Rend1.GetMaterial();
		}
		if (this.m_Rend2 != null)
		{
			this.m_mat2 = this.m_Rend2.GetMaterial();
		}
		if (this.m_Rend3 != null)
		{
			this.m_mat3 = this.m_Rend3.GetMaterial();
		}
		if (this.m_Rend4 != null)
		{
			this.m_mat4 = this.m_Rend4.GetMaterial();
		}
		if (this.m_Rend5 != null)
		{
			this.m_mat5 = this.m_Rend5.GetMaterial();
		}
		if (this.m_Rend6 != null)
		{
			this.m_mat6 = this.m_Rend6.GetMaterial();
		}
	}

	// Token: 0x06008B6F RID: 35695 RVA: 0x002C9584 File Offset: 0x002C7784
	private void Update()
	{
		if (this.m_Rend1 != null)
		{
			if (this.m_Intensity1 <= 0f)
			{
				this.m_Rend1.enabled = false;
			}
			else
			{
				this.m_Rend1.enabled = true;
			}
			this.m_mat1.SetFloat(this.m_intensityProperty, this.m_Intensity1);
		}
		if (this.m_Rend2 != null)
		{
			if (this.m_Intensity2 <= 0f)
			{
				this.m_Rend2.enabled = false;
			}
			else
			{
				this.m_Rend2.enabled = true;
			}
			this.m_mat2.SetFloat(this.m_intensityProperty, this.m_Intensity2);
		}
		if (this.m_Rend3 != null)
		{
			if (this.m_Intensity3 <= 0f)
			{
				this.m_Rend3.enabled = false;
			}
			else
			{
				this.m_Rend3.enabled = true;
			}
			this.m_mat3.SetFloat(this.m_intensityProperty, this.m_Intensity3);
		}
		if (this.m_Rend4 != null)
		{
			if (this.m_Intensity4 <= 0f)
			{
				this.m_Rend4.enabled = false;
			}
			else
			{
				this.m_Rend4.enabled = true;
			}
			this.m_mat4.SetFloat(this.m_intensityProperty, this.m_Intensity4);
		}
		if (this.m_Rend5 != null)
		{
			if (this.m_Intensity5 <= 0f)
			{
				this.m_Rend5.enabled = false;
			}
			else
			{
				this.m_Rend5.enabled = true;
			}
			this.m_mat5.SetFloat(this.m_intensityProperty, this.m_Intensity5);
		}
		if (this.m_Rend6 != null)
		{
			if (this.m_Intensity6 <= 0f)
			{
				this.m_Rend6.enabled = false;
			}
			else
			{
				this.m_Rend6.enabled = true;
			}
			this.m_mat6.SetFloat(this.m_intensityProperty, this.m_Intensity6);
		}
	}

	// Token: 0x06008B70 RID: 35696 RVA: 0x002C975C File Offset: 0x002C795C
	private void OnDestroy()
	{
		if (this.m_mat1 != null)
		{
			UnityEngine.Object.Destroy(this.m_mat1);
			this.m_mat1 = null;
		}
		if (this.m_mat2 != null)
		{
			UnityEngine.Object.Destroy(this.m_mat2);
			this.m_mat2 = null;
		}
		if (this.m_mat3 != null)
		{
			UnityEngine.Object.Destroy(this.m_mat3);
			this.m_mat3 = null;
		}
		if (this.m_mat4 != null)
		{
			UnityEngine.Object.Destroy(this.m_mat4);
			this.m_mat4 = null;
		}
		if (this.m_mat5 != null)
		{
			UnityEngine.Object.Destroy(this.m_mat5);
			this.m_mat5 = null;
		}
		if (this.m_mat6 != null)
		{
			UnityEngine.Object.Destroy(this.m_mat6);
			this.m_mat1 = null;
		}
	}

	// Token: 0x040073EC RID: 29676
	public Renderer m_Rend1;

	// Token: 0x040073ED RID: 29677
	public float m_Intensity1;

	// Token: 0x040073EE RID: 29678
	private Material m_mat1;

	// Token: 0x040073EF RID: 29679
	public Renderer m_Rend2;

	// Token: 0x040073F0 RID: 29680
	public float m_Intensity2;

	// Token: 0x040073F1 RID: 29681
	private Material m_mat2;

	// Token: 0x040073F2 RID: 29682
	public Renderer m_Rend3;

	// Token: 0x040073F3 RID: 29683
	public float m_Intensity3;

	// Token: 0x040073F4 RID: 29684
	private Material m_mat3;

	// Token: 0x040073F5 RID: 29685
	public Renderer m_Rend4;

	// Token: 0x040073F6 RID: 29686
	public float m_Intensity4;

	// Token: 0x040073F7 RID: 29687
	private Material m_mat4;

	// Token: 0x040073F8 RID: 29688
	public Renderer m_Rend5;

	// Token: 0x040073F9 RID: 29689
	public float m_Intensity5;

	// Token: 0x040073FA RID: 29690
	private Material m_mat5;

	// Token: 0x040073FB RID: 29691
	public Renderer m_Rend6;

	// Token: 0x040073FC RID: 29692
	public float m_Intensity6;

	// Token: 0x040073FD RID: 29693
	private Material m_mat6;

	// Token: 0x040073FE RID: 29694
	private int m_intensityProperty;
}
