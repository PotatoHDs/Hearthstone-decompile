using System;
using UnityEngine;

// Token: 0x02000A22 RID: 2594
public class DisableMesh_ColorBlack : MonoBehaviour
{
	// Token: 0x06008BC6 RID: 35782 RVA: 0x002CBECC File Offset: 0x002CA0CC
	private void Start()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_material = this.m_renderer.GetMaterial();
		if (this.m_material == null)
		{
			base.enabled = false;
		}
		if (!this.m_material.HasProperty("_Color") && !this.m_material.HasProperty("_TintColor"))
		{
			base.enabled = false;
		}
		if (this.m_material.HasProperty("_TintColor"))
		{
			this.m_tintColor = true;
		}
	}

	// Token: 0x06008BC7 RID: 35783 RVA: 0x002CBF50 File Offset: 0x002CA150
	private void Update()
	{
		if (this.m_tintColor)
		{
			this.m_color = this.m_material.GetColor("_TintColor");
		}
		else
		{
			this.m_color = this.m_material.color;
		}
		if (this.m_color.r < 0.01f && this.m_color.g < 0.01f && this.m_color.b < 0.01f)
		{
			this.m_renderer.enabled = false;
			return;
		}
		this.m_renderer.enabled = true;
	}

	// Token: 0x0400749F RID: 29855
	private Material m_material;

	// Token: 0x040074A0 RID: 29856
	private bool m_tintColor;

	// Token: 0x040074A1 RID: 29857
	private Color m_color = Color.black;

	// Token: 0x040074A2 RID: 29858
	private Renderer m_renderer;
}
