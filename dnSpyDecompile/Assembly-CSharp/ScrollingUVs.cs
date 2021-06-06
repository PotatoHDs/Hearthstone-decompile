using System;
using UnityEngine;

// Token: 0x02000A86 RID: 2694
public class ScrollingUVs : MonoBehaviour
{
	// Token: 0x0600905C RID: 36956 RVA: 0x002EDB9F File Offset: 0x002EBD9F
	private void Start()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_material = this.m_renderer.GetMaterial(this.materialIndex);
	}

	// Token: 0x0600905D RID: 36957 RVA: 0x002EDBC4 File Offset: 0x002EBDC4
	private void LateUpdate()
	{
		if (!this.m_renderer.enabled)
		{
			return;
		}
		if (this.m_material == null)
		{
			this.m_material = this.m_renderer.GetMaterial(this.materialIndex);
		}
		this.m_offset += this.uvAnimationRate * Time.deltaTime;
		this.m_material.SetTextureOffset("_MainTex", this.m_offset);
	}

	// Token: 0x0400793E RID: 31038
	public int materialIndex;

	// Token: 0x0400793F RID: 31039
	public Vector2 uvAnimationRate = new Vector2(1f, 1f);

	// Token: 0x04007940 RID: 31040
	private Material m_material;

	// Token: 0x04007941 RID: 31041
	private Vector2 m_offset = Vector2.zero;

	// Token: 0x04007942 RID: 31042
	private Renderer m_renderer;
}
