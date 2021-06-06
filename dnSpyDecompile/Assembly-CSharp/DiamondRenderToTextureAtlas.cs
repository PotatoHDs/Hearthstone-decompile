using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000A70 RID: 2672
public class DiamondRenderToTextureAtlas : GreedyPacker
{
	// Token: 0x17000819 RID: 2073
	// (get) Token: 0x06008F95 RID: 36757 RVA: 0x002E82B8 File Offset: 0x002E64B8
	public int Index { get; }

	// Token: 0x1700081A RID: 2074
	// (get) Token: 0x06008F96 RID: 36758 RVA: 0x002E82C0 File Offset: 0x002E64C0
	public RenderTexture Texture { get; }

	// Token: 0x1700081B RID: 2075
	// (get) Token: 0x06008F97 RID: 36759 RVA: 0x002E82C8 File Offset: 0x002E64C8
	// (set) Token: 0x06008F98 RID: 36760 RVA: 0x002E82D0 File Offset: 0x002E64D0
	public bool IsRealTime { get; private set; }

	// Token: 0x1700081C RID: 2076
	// (get) Token: 0x06008F99 RID: 36761 RVA: 0x002E82D9 File Offset: 0x002E64D9
	// (set) Token: 0x06008F9A RID: 36762 RVA: 0x002E82E1 File Offset: 0x002E64E1
	public bool Dirty { get; set; }

	// Token: 0x1700081D RID: 2077
	// (get) Token: 0x06008F9B RID: 36763 RVA: 0x002E82EA File Offset: 0x002E64EA
	// (set) Token: 0x06008F9C RID: 36764 RVA: 0x002E82F2 File Offset: 0x002E64F2
	public List<DiamondRenderToTextureAtlas.RegisteredTexture> RegisteredTextures { get; private set; }

	// Token: 0x1700081E RID: 2078
	// (get) Token: 0x06008F9D RID: 36765 RVA: 0x002E82FB File Offset: 0x002E64FB
	// (set) Token: 0x06008F9E RID: 36766 RVA: 0x002E8303 File Offset: 0x002E6503
	public Color ClearColor { get; private set; } = Color.clear;

	// Token: 0x06008F9F RID: 36767 RVA: 0x002E830C File Offset: 0x002E650C
	public DiamondRenderToTextureAtlas(int index, int width, int height, RenderTextureFormat format = RenderTextureFormat.ARGB32) : base(width, height)
	{
		this.Index = index;
		this.Texture = RenderTextureTracker.Get().CreateNewTexture(width, height, 16, format, RenderTextureReadWrite.Default);
		this.Texture.useMipMap = false;
		this.Texture.autoGenerateMips = false;
		this.m_size = new Vector2Int(width, height);
		this.m_totalPixelSpace = width * height;
		this.RegisteredTextures = new List<DiamondRenderToTextureAtlas.RegisteredTexture>();
		this.m_renderPostProcessList = new List<RenderToTexturePostProcess>();
	}

	// Token: 0x06008FA0 RID: 36768 RVA: 0x002E8390 File Offset: 0x002E6590
	public bool Insert(DiamondRenderToTexture r2t)
	{
		int freeSpace = this.GetFreeSpace();
		if (r2t.TextureSize.x * r2t.TextureSize.y > freeSpace)
		{
			return false;
		}
		bool flag = this.RegisteredTextures.Count == 0;
		if (!flag && !this.UsesSamePostProcess(r2t))
		{
			return false;
		}
		if (flag)
		{
			this.ClearColor = r2t.m_ClearColor;
		}
		RectInt atlasPosition = base.Insert(r2t.TextureSize.x, r2t.TextureSize.y);
		if (atlasPosition.x == -1 && atlasPosition.y == -1)
		{
			return false;
		}
		this.RegisteredTextures.Add(new DiamondRenderToTextureAtlas.RegisteredTexture
		{
			DiamondRenderToTexture = r2t,
			AtlasPosition = atlasPosition
		});
		r2t.OnAddedToAtlas(this.Texture, new Rect((float)atlasPosition.xMin / (float)this.m_size.x, (float)atlasPosition.yMin / (float)this.m_size.y, (float)atlasPosition.width / (float)this.m_size.x, (float)atlasPosition.height / (float)this.m_size.y));
		this.BuildCommandBuffers();
		if (flag)
		{
			this.SetupPostProcess(r2t);
		}
		this.m_totalPixelUsed += atlasPosition.width * atlasPosition.height;
		if (r2t.m_RealtimeRender)
		{
			this.IsRealTime = true;
		}
		this.Dirty = true;
		return true;
	}

	// Token: 0x06008FA1 RID: 36769 RVA: 0x002E8504 File Offset: 0x002E6704
	public bool Remove(DiamondRenderToTexture r2t)
	{
		for (int i = this.RegisteredTextures.Count - 1; i >= 0; i--)
		{
			DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture = this.RegisteredTextures[i];
			if (!registeredTexture.DiamondRenderToTexture)
			{
				this.RegisteredTextures.RemoveAt(i);
			}
			else if (registeredTexture.DiamondRenderToTexture.GetInstanceID() == r2t.GetInstanceID())
			{
				Vector2Int textureSize = registeredTexture.DiamondRenderToTexture.TextureSize;
				base.Remove(registeredTexture.AtlasPosition);
				this.m_totalPixelUsed -= textureSize.x * textureSize.y;
				this.RegisteredTextures.RemoveAt(i);
				this.BuildCommandBuffers();
				this.Dirty = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008FA2 RID: 36770 RVA: 0x002E85BC File Offset: 0x002E67BC
	public bool CanFit(DiamondRenderToTextureAtlas atlas)
	{
		int freeSpace = this.GetFreeSpace();
		return atlas.m_totalPixelSpace <= freeSpace;
	}

	// Token: 0x06008FA3 RID: 36771 RVA: 0x002E85DC File Offset: 0x002E67DC
	public bool IsEmpty()
	{
		return this.RegisteredTextures.Count == 0;
	}

	// Token: 0x06008FA4 RID: 36772 RVA: 0x002E85EC File Offset: 0x002E67EC
	public bool Insert(DiamondRenderToTextureAtlas atlas)
	{
		if (!this.CanFit(atlas))
		{
			return false;
		}
		foreach (DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture in atlas.RegisteredTextures)
		{
			if (!this.Insert(registeredTexture.DiamondRenderToTexture))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06008FA5 RID: 36773 RVA: 0x002E8658 File Offset: 0x002E6858
	public void Destroy()
	{
		RenderTextureTracker.Get().ReleaseRenderTexture(this.Texture);
		foreach (RenderToTexturePostProcess renderToTexturePostProcess in this.m_renderPostProcessList)
		{
			renderToTexturePostProcess.End();
		}
	}

	// Token: 0x06008FA6 RID: 36774 RVA: 0x002E86B8 File Offset: 0x002E68B8
	public void Render(Camera camera)
	{
		camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, this.m_opaqueCommandBuffer);
		camera.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, this.m_transparentCommandBuffer);
		if (this.m_renderPostProcessList.Count > 0)
		{
			foreach (RenderToTexturePostProcess renderToTexturePostProcess in this.m_renderPostProcessList)
			{
				renderToTexturePostProcess.AddCommandBuffers(camera);
			}
		}
	}

	// Token: 0x06008FA7 RID: 36775 RVA: 0x002E8734 File Offset: 0x002E6934
	private void BuildCommandBuffers()
	{
		this.m_opaqueCommandBuffer = new CommandBuffer
		{
			name = "AtlasOpaqueRender"
		};
		this.m_transparentCommandBuffer = new CommandBuffer
		{
			name = "AtlasTransparentRender"
		};
		foreach (DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture in this.RegisteredTextures)
		{
			if (registeredTexture.DiamondRenderToTexture && registeredTexture.DiamondRenderToTexture.enabled)
			{
				RectInt atlasPosition = registeredTexture.AtlasPosition;
				Rect scissor = new Rect((float)atlasPosition.xMin + 0.5f, (float)atlasPosition.yMin + 0.5f, (float)atlasPosition.width - 1f, (float)atlasPosition.height - 1f);
				this.m_opaqueCommandBuffer.EnableScissorRect(scissor);
				this.m_transparentCommandBuffer.EnableScissorRect(scissor);
				foreach (DiamondRenderToTexture.RenderCommand renderCommand in registeredTexture.DiamondRenderToTexture.OpaqueRenderCommands)
				{
					this.m_opaqueCommandBuffer.DrawRenderer(renderCommand.Renderer, renderCommand.Material, renderCommand.MeshIndex);
				}
				foreach (DiamondRenderToTexture.RenderCommand renderCommand2 in registeredTexture.DiamondRenderToTexture.TransparentRenderCommands)
				{
					this.m_transparentCommandBuffer.DrawRenderer(renderCommand2.Renderer, renderCommand2.Material, renderCommand2.MeshIndex);
				}
				this.m_opaqueCommandBuffer.DisableScissorRect();
				this.m_transparentCommandBuffer.DisableScissorRect();
			}
		}
	}

	// Token: 0x06008FA8 RID: 36776 RVA: 0x002E8930 File Offset: 0x002E6B30
	private int GetFreeSpace()
	{
		return this.m_totalPixelSpace - this.m_totalPixelUsed;
	}

	// Token: 0x06008FA9 RID: 36777 RVA: 0x002E8940 File Offset: 0x002E6B40
	private bool UsesSamePostProcess(DiamondRenderToTexture r2t)
	{
		if (r2t.m_ClearColor != this.ClearColor)
		{
			return false;
		}
		using (List<RenderToTexturePostProcess>.Enumerator enumerator = this.m_renderPostProcessList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsUsedBy(r2t))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06008FAA RID: 36778 RVA: 0x002E89B0 File Offset: 0x002E6BB0
	private void SetupPostProcess(DiamondRenderToTexture r2t)
	{
		if (r2t.m_OpaqueObjectAlphaFill)
		{
			RenderToTextureOpaqueAlphaFill renderToTextureOpaqueAlphaFill = new RenderToTextureOpaqueAlphaFill();
			renderToTextureOpaqueAlphaFill.Init(this.m_size.x, this.m_size.y, r2t.m_ClearColor);
			this.m_renderPostProcessList.Add(renderToTextureOpaqueAlphaFill);
		}
	}

	// Token: 0x04007850 RID: 30800
	private const int RTT_DEPTH_BUFFER_SIZE = 16;

	// Token: 0x04007851 RID: 30801
	private const float PADDING = 0.5f;

	// Token: 0x04007852 RID: 30802
	private const float MARGIN = 1f;

	// Token: 0x04007859 RID: 30809
	private readonly Vector2Int m_size;

	// Token: 0x0400785A RID: 30810
	private readonly int m_totalPixelSpace;

	// Token: 0x0400785B RID: 30811
	private int m_totalPixelUsed;

	// Token: 0x0400785C RID: 30812
	private CommandBuffer m_opaqueCommandBuffer;

	// Token: 0x0400785D RID: 30813
	private CommandBuffer m_transparentCommandBuffer;

	// Token: 0x0400785E RID: 30814
	private List<RenderToTexturePostProcess> m_renderPostProcessList;

	// Token: 0x020026CB RID: 9931
	public struct RegisteredTexture
	{
		// Token: 0x0400F218 RID: 61976
		public DiamondRenderToTexture DiamondRenderToTexture;

		// Token: 0x0400F219 RID: 61977
		public RectInt AtlasPosition;
	}
}
