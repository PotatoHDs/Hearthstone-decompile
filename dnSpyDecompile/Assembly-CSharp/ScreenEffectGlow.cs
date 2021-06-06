using System;
using UnityEngine;

// Token: 0x02000A82 RID: 2690
[ExecuteAlways]
public class ScreenEffectGlow : ScreenEffect
{
	// Token: 0x0600902C RID: 36908 RVA: 0x002ECEE2 File Offset: 0x002EB0E2
	private void Awake()
	{
		this.m_PreviousLayer = base.gameObject.layer;
	}

	// Token: 0x0600902D RID: 36909 RVA: 0x002ECEF5 File Offset: 0x002EB0F5
	private void Start()
	{
		this.SetLayer();
	}

	// Token: 0x0600902E RID: 36910 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x0600902F RID: 36911 RVA: 0x002ECF00 File Offset: 0x002EB100
	private void SetLayer()
	{
		if (this.m_PreviousRenderGlowOnly != this.m_RenderGlowOnly)
		{
			this.m_PreviousRenderGlowOnly = this.m_RenderGlowOnly;
			if (this.m_RenderGlowOnly)
			{
				this.m_PreviousLayer = base.gameObject.layer;
				SceneUtils.SetLayer(base.gameObject, GameLayer.ScreenEffects);
				return;
			}
			SceneUtils.SetLayer(base.gameObject, this.m_PreviousLayer, null);
		}
	}

	// Token: 0x04007918 RID: 31000
	public bool m_RenderGlowOnly;

	// Token: 0x04007919 RID: 31001
	private bool m_PreviousRenderGlowOnly;

	// Token: 0x0400791A RID: 31002
	private int m_PreviousLayer;
}
