using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DiamondRenderToTextureAtlas : GreedyPacker
{
	public struct RegisteredTexture
	{
		public DiamondRenderToTexture DiamondRenderToTexture;

		public RectInt AtlasPosition;
	}

	private const int RTT_DEPTH_BUFFER_SIZE = 16;

	private const float PADDING = 0.5f;

	private const float MARGIN = 1f;

	private readonly Vector2Int m_size;

	private readonly int m_totalPixelSpace;

	private int m_totalPixelUsed;

	private CommandBuffer m_opaqueCommandBuffer;

	private CommandBuffer m_transparentCommandBuffer;

	private List<RenderToTexturePostProcess> m_renderPostProcessList;

	public int Index { get; }

	public RenderTexture Texture { get; }

	public bool IsRealTime { get; private set; }

	public bool Dirty { get; set; }

	public List<RegisteredTexture> RegisteredTextures { get; private set; }

	public Color ClearColor { get; private set; } = Color.clear;


	public DiamondRenderToTextureAtlas(int index, int width, int height, RenderTextureFormat format = RenderTextureFormat.ARGB32)
		: base(width, height)
	{
		Index = index;
		Texture = RenderTextureTracker.Get().CreateNewTexture(width, height, 16, format);
		Texture.useMipMap = false;
		Texture.autoGenerateMips = false;
		m_size = new Vector2Int(width, height);
		m_totalPixelSpace = width * height;
		RegisteredTextures = new List<RegisteredTexture>();
		m_renderPostProcessList = new List<RenderToTexturePostProcess>();
	}

	public bool Insert(DiamondRenderToTexture r2t)
	{
		int freeSpace = GetFreeSpace();
		if (r2t.TextureSize.x * r2t.TextureSize.y > freeSpace)
		{
			return false;
		}
		bool flag = RegisteredTextures.Count == 0;
		if (!flag && !UsesSamePostProcess(r2t))
		{
			return false;
		}
		if (flag)
		{
			ClearColor = r2t.m_ClearColor;
		}
		RectInt atlasPosition = Insert(r2t.TextureSize.x, r2t.TextureSize.y);
		if (atlasPosition.x == -1 && atlasPosition.y == -1)
		{
			return false;
		}
		RegisteredTextures.Add(new RegisteredTexture
		{
			DiamondRenderToTexture = r2t,
			AtlasPosition = atlasPosition
		});
		r2t.OnAddedToAtlas(Texture, new Rect((float)atlasPosition.xMin / (float)m_size.x, (float)atlasPosition.yMin / (float)m_size.y, (float)atlasPosition.width / (float)m_size.x, (float)atlasPosition.height / (float)m_size.y));
		BuildCommandBuffers();
		if (flag)
		{
			SetupPostProcess(r2t);
		}
		m_totalPixelUsed += atlasPosition.width * atlasPosition.height;
		if (r2t.m_RealtimeRender)
		{
			IsRealTime = true;
		}
		Dirty = true;
		return true;
	}

	public bool Remove(DiamondRenderToTexture r2t)
	{
		for (int num = RegisteredTextures.Count - 1; num >= 0; num--)
		{
			RegisteredTexture registeredTexture = RegisteredTextures[num];
			if (!registeredTexture.DiamondRenderToTexture)
			{
				RegisteredTextures.RemoveAt(num);
			}
			else if (registeredTexture.DiamondRenderToTexture.GetInstanceID() == r2t.GetInstanceID())
			{
				Vector2Int textureSize = registeredTexture.DiamondRenderToTexture.TextureSize;
				Remove(registeredTexture.AtlasPosition);
				m_totalPixelUsed -= textureSize.x * textureSize.y;
				RegisteredTextures.RemoveAt(num);
				BuildCommandBuffers();
				Dirty = true;
				return true;
			}
		}
		return false;
	}

	public bool CanFit(DiamondRenderToTextureAtlas atlas)
	{
		int freeSpace = GetFreeSpace();
		if (atlas.m_totalPixelSpace > freeSpace)
		{
			return false;
		}
		return true;
	}

	public bool IsEmpty()
	{
		return RegisteredTextures.Count == 0;
	}

	public bool Insert(DiamondRenderToTextureAtlas atlas)
	{
		if (!CanFit(atlas))
		{
			return false;
		}
		foreach (RegisteredTexture registeredTexture in atlas.RegisteredTextures)
		{
			if (!Insert(registeredTexture.DiamondRenderToTexture))
			{
				return false;
			}
		}
		return true;
	}

	public void Destroy()
	{
		RenderTextureTracker.Get().ReleaseRenderTexture(Texture);
		foreach (RenderToTexturePostProcess renderPostProcess in m_renderPostProcessList)
		{
			renderPostProcess.End();
		}
	}

	public void Render(Camera camera)
	{
		camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, m_opaqueCommandBuffer);
		camera.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, m_transparentCommandBuffer);
		if (m_renderPostProcessList.Count <= 0)
		{
			return;
		}
		foreach (RenderToTexturePostProcess renderPostProcess in m_renderPostProcessList)
		{
			renderPostProcess.AddCommandBuffers(camera);
		}
	}

	private void BuildCommandBuffers()
	{
		m_opaqueCommandBuffer = new CommandBuffer
		{
			name = "AtlasOpaqueRender"
		};
		m_transparentCommandBuffer = new CommandBuffer
		{
			name = "AtlasTransparentRender"
		};
		foreach (RegisteredTexture registeredTexture in RegisteredTextures)
		{
			if (!registeredTexture.DiamondRenderToTexture || !registeredTexture.DiamondRenderToTexture.enabled)
			{
				continue;
			}
			RectInt atlasPosition = registeredTexture.AtlasPosition;
			Rect scissor = new Rect((float)atlasPosition.xMin + 0.5f, (float)atlasPosition.yMin + 0.5f, (float)atlasPosition.width - 1f, (float)atlasPosition.height - 1f);
			m_opaqueCommandBuffer.EnableScissorRect(scissor);
			m_transparentCommandBuffer.EnableScissorRect(scissor);
			foreach (DiamondRenderToTexture.RenderCommand opaqueRenderCommand in registeredTexture.DiamondRenderToTexture.OpaqueRenderCommands)
			{
				m_opaqueCommandBuffer.DrawRenderer(opaqueRenderCommand.Renderer, opaqueRenderCommand.Material, opaqueRenderCommand.MeshIndex);
			}
			foreach (DiamondRenderToTexture.RenderCommand transparentRenderCommand in registeredTexture.DiamondRenderToTexture.TransparentRenderCommands)
			{
				m_transparentCommandBuffer.DrawRenderer(transparentRenderCommand.Renderer, transparentRenderCommand.Material, transparentRenderCommand.MeshIndex);
			}
			m_opaqueCommandBuffer.DisableScissorRect();
			m_transparentCommandBuffer.DisableScissorRect();
		}
	}

	private int GetFreeSpace()
	{
		return m_totalPixelSpace - m_totalPixelUsed;
	}

	private bool UsesSamePostProcess(DiamondRenderToTexture r2t)
	{
		if (r2t.m_ClearColor != ClearColor)
		{
			return false;
		}
		foreach (RenderToTexturePostProcess renderPostProcess in m_renderPostProcessList)
		{
			if (!renderPostProcess.IsUsedBy(r2t))
			{
				return false;
			}
		}
		return true;
	}

	private void SetupPostProcess(DiamondRenderToTexture r2t)
	{
		if (r2t.m_OpaqueObjectAlphaFill)
		{
			RenderToTextureOpaqueAlphaFill renderToTextureOpaqueAlphaFill = new RenderToTextureOpaqueAlphaFill();
			renderToTextureOpaqueAlphaFill.Init(m_size.x, m_size.y, r2t.m_ClearColor);
			m_renderPostProcessList.Add(renderToTextureOpaqueAlphaFill);
		}
	}
}
