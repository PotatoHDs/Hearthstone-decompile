using Blizzard.T5.AssetManager;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderToTextureOpaqueAlphaFill : RenderToTexturePostProcess
{
	private const string ALPHA_FILL_SHADER_NAME = "ARTT_AlphaOpaqueFill.mat:0ff23894e37f8374a9dda7e852f9bcd3";

	private AssetHandle<Material> m_materialHandle;

	private Material m_alphaFillMaterial;

	private CommandBuffer m_commandBuffer;

	private Color m_clearColor = Color.clear;

	public void Init(int outputTextureWidth, int outputTextureHeight)
	{
		CreateAlphaFillMaterial();
		CreateCommandBuffer(outputTextureWidth, outputTextureHeight);
	}

	public void Init(int outputTextureWidth, int outputTextureHeight, Color clearColor)
	{
		m_clearColor = clearColor;
		Init(outputTextureWidth, outputTextureHeight);
	}

	public void End()
	{
		m_materialHandle.Dispose();
	}

	public bool IsUsedBy(DiamondRenderToTexture r2t)
	{
		return r2t.m_OpaqueObjectAlphaFill;
	}

	public void AddCommandBuffers(Camera camera)
	{
		camera.AddCommandBuffer(CameraEvent.AfterForwardOpaque, m_commandBuffer);
	}

	private void CreateAlphaFillMaterial()
	{
		m_materialHandle = AssetLoader.Get().LoadAsset<Material>("ARTT_AlphaOpaqueFill.mat:0ff23894e37f8374a9dda7e852f9bcd3");
		m_alphaFillMaterial = m_materialHandle.Asset;
	}

	private void CreateCommandBuffer(int outputTextureWidth, int outputTextureHeight)
	{
		m_commandBuffer = new CommandBuffer();
		m_commandBuffer.name = "AlphaOpaqueBlend";
		int num = Shader.PropertyToID("_TempAlphaTex");
		m_commandBuffer.GetTemporaryRT(num, outputTextureWidth, outputTextureHeight, 0, FilterMode.Bilinear);
		m_commandBuffer.SetGlobalColor("_ClearColor", m_clearColor);
		m_commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, num, m_alphaFillMaterial);
		m_commandBuffer.Blit(num, BuiltinRenderTextureType.CameraTarget);
		m_commandBuffer.ReleaseTemporaryRT(num);
	}
}
