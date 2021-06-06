using System;
using UnityEngine;

// Token: 0x02000A21 RID: 2593
public class DisableMesh_ColorAlpha : MonoBehaviour
{
	// Token: 0x06008BC3 RID: 35779 RVA: 0x002CBE40 File Offset: 0x002CA040
	private void Start()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_material = this.m_renderer.GetMaterial();
		if (this.m_material == null)
		{
			base.enabled = false;
		}
		if (!this.m_material.HasProperty("_Color"))
		{
			base.enabled = false;
		}
	}

	// Token: 0x06008BC4 RID: 35780 RVA: 0x002CBE98 File Offset: 0x002CA098
	private void Update()
	{
		if (this.m_material.color.a == 0f)
		{
			this.m_renderer.enabled = false;
			return;
		}
		this.m_renderer.enabled = true;
	}

	// Token: 0x0400749D RID: 29853
	private Material m_material;

	// Token: 0x0400749E RID: 29854
	private Renderer m_renderer;
}
