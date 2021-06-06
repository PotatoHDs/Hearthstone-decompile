using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class RenderTextureTracker
{
	// Token: 0x060021D7 RID: 8663 RVA: 0x000052CE File Offset: 0x000034CE
	private RenderTextureTracker()
	{
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x000A6C2F File Offset: 0x000A4E2F
	public static RenderTextureTracker Get()
	{
		if (RenderTextureTracker.m_instance == null)
		{
			RenderTextureTracker.m_instance = new RenderTextureTracker();
		}
		return RenderTextureTracker.m_instance;
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x000A6C47 File Offset: 0x000A4E47
	public RenderTexture CreateNewTexture(int width, int height, int depth, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
	{
		return new RenderTexture(width, height, depth, format, readWrite);
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x000A6C55 File Offset: 0x000A4E55
	public void DestroyRenderTexture(RenderTexture renderTexture)
	{
		UnityEngine.Object.Destroy(renderTexture);
	}

	// Token: 0x060021DB RID: 8667 RVA: 0x000A6C5D File Offset: 0x000A4E5D
	public void ReleaseRenderTexture(RenderTexture renderTexture)
	{
		renderTexture.Release();
	}

	// Token: 0x040012B6 RID: 4790
	private static RenderTextureTracker m_instance;
}
