using System;
using UnityEngine;

// Token: 0x02000A23 RID: 2595
public class DisableMesh_Intensity : MonoBehaviour
{
	// Token: 0x06008BC9 RID: 35785 RVA: 0x002CBFF0 File Offset: 0x002CA1F0
	private void Start()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_material = this.m_renderer.GetMaterial();
		if (this.m_material == null)
		{
			base.enabled = false;
		}
		if (!this.m_material.HasProperty("_Intensity"))
		{
			base.enabled = false;
		}
	}

	// Token: 0x06008BCA RID: 35786 RVA: 0x002CC048 File Offset: 0x002CA248
	private void Update()
	{
		if (this.m_material.GetFloat("_Intensity") == 0f)
		{
			this.m_renderer.enabled = false;
			return;
		}
		this.m_renderer.enabled = true;
	}

	// Token: 0x040074A3 RID: 29859
	private Material m_material;

	// Token: 0x040074A4 RID: 29860
	private Renderer m_renderer;
}
