using System;
using Blizzard.T5.AssetManager;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000A76 RID: 2678
public class RenderToTextureOpaqueAlphaFill : RenderToTexturePostProcess
{
	// Token: 0x06008FCF RID: 36815 RVA: 0x002E9A24 File Offset: 0x002E7C24
	public void Init(int outputTextureWidth, int outputTextureHeight)
	{
		this.CreateAlphaFillMaterial();
		this.CreateCommandBuffer(outputTextureWidth, outputTextureHeight);
	}

	// Token: 0x06008FD0 RID: 36816 RVA: 0x002E9A34 File Offset: 0x002E7C34
	public void Init(int outputTextureWidth, int outputTextureHeight, Color clearColor)
	{
		this.m_clearColor = clearColor;
		this.Init(outputTextureWidth, outputTextureHeight);
	}

	// Token: 0x06008FD1 RID: 36817 RVA: 0x002E9A45 File Offset: 0x002E7C45
	public void End()
	{
		this.m_materialHandle.Dispose();
	}

	// Token: 0x06008FD2 RID: 36818 RVA: 0x002E9A52 File Offset: 0x002E7C52
	public bool IsUsedBy(DiamondRenderToTexture r2t)
	{
		return r2t.m_OpaqueObjectAlphaFill;
	}

	// Token: 0x06008FD3 RID: 36819 RVA: 0x002E9A5A File Offset: 0x002E7C5A
	public void AddCommandBuffers(Camera camera)
	{
		camera.AddCommandBuffer(CameraEvent.AfterForwardOpaque, this.m_commandBuffer);
	}

	// Token: 0x06008FD4 RID: 36820 RVA: 0x002E9A6A File Offset: 0x002E7C6A
	private void CreateAlphaFillMaterial()
	{
		this.m_materialHandle = AssetLoader.Get().LoadAsset<Material>("ARTT_AlphaOpaqueFill.mat:0ff23894e37f8374a9dda7e852f9bcd3", AssetLoadingOptions.None);
		this.m_alphaFillMaterial = this.m_materialHandle.Asset;
	}

	// Token: 0x06008FD5 RID: 36821 RVA: 0x002E9A98 File Offset: 0x002E7C98
	private void CreateCommandBuffer(int outputTextureWidth, int outputTextureHeight)
	{
		this.m_commandBuffer = new CommandBuffer();
		this.m_commandBuffer.name = "AlphaOpaqueBlend";
		int nameID = Shader.PropertyToID("_TempAlphaTex");
		this.m_commandBuffer.GetTemporaryRT(nameID, outputTextureWidth, outputTextureHeight, 0, FilterMode.Bilinear);
		this.m_commandBuffer.SetGlobalColor("_ClearColor", this.m_clearColor);
		this.m_commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, nameID, this.m_alphaFillMaterial);
		this.m_commandBuffer.Blit(nameID, BuiltinRenderTextureType.CameraTarget);
		this.m_commandBuffer.ReleaseTemporaryRT(nameID);
	}

	// Token: 0x0400787A RID: 30842
	private const string ALPHA_FILL_SHADER_NAME = "ARTT_AlphaOpaqueFill.mat:0ff23894e37f8374a9dda7e852f9bcd3";

	// Token: 0x0400787B RID: 30843
	private AssetHandle<Material> m_materialHandle;

	// Token: 0x0400787C RID: 30844
	private Material m_alphaFillMaterial;

	// Token: 0x0400787D RID: 30845
	private CommandBuffer m_commandBuffer;

	// Token: 0x0400787E RID: 30846
	private Color m_clearColor = Color.clear;
}
