using System;
using bgs;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class PlayerPortrait : MonoBehaviour
{
	// Token: 0x06000A42 RID: 2626 RVA: 0x00039F21 File Offset: 0x00038121
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedTexture);
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x00039F2E File Offset: 0x0003812E
	public BnetProgramId GetProgramId()
	{
		return this.m_programId;
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x00039F36 File Offset: 0x00038136
	public bool SetProgramId(BnetProgramId programId)
	{
		if (this.m_programId == programId)
		{
			return false;
		}
		this.m_programId = programId;
		this.UpdateIcon();
		return true;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00039F56 File Offset: 0x00038156
	public bool IsIconReady()
	{
		return this.m_loadingTextureName == null && this.m_currentTextureName != null;
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x00039F6B File Offset: 0x0003816B
	public bool IsIconLoading()
	{
		return this.m_loadingTextureName != null;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x00039F78 File Offset: 0x00038178
	private void UpdateIcon()
	{
		if (this.m_programId == null)
		{
			this.m_currentTextureName = null;
			this.m_loadingTextureName = null;
			base.GetComponent<Renderer>().GetMaterial().mainTexture = null;
			return;
		}
		string textureName = BnetProgramId.GetTextureName(this.m_programId);
		if (this.m_currentTextureName == textureName)
		{
			return;
		}
		if (this.m_loadingTextureName == textureName)
		{
			return;
		}
		this.m_loadingTextureName = textureName;
		AssetLoader.Get().LoadAsset<Texture>(this.m_loadingTextureName, new AssetHandleCallback<Texture>(this.OnTextureLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0003A008 File Offset: 0x00038208
	private void OnTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		try
		{
			if (!(assetRef.ToString() != this.m_loadingTextureName))
			{
				if (!texture)
				{
					global::Error.AddDevFatal("PlayerPortrait.OnTextureLoaded() - Failed to load {0}. ProgramId={1}", new object[]
					{
						assetRef,
						this.m_programId
					});
					this.m_currentTextureName = null;
					this.m_loadingTextureName = null;
				}
				else
				{
					this.m_currentTextureName = this.m_loadingTextureName;
					this.m_loadingTextureName = null;
					AssetHandle.Set<Texture>(ref this.m_loadedTexture, texture);
					base.GetComponent<Renderer>().GetMaterial().mainTexture = texture;
				}
			}
		}
		finally
		{
			if (texture != null)
			{
				((IDisposable)texture).Dispose();
			}
		}
	}

	// Token: 0x04000693 RID: 1683
	private BnetProgramId m_programId;

	// Token: 0x04000694 RID: 1684
	private string m_currentTextureName;

	// Token: 0x04000695 RID: 1685
	private string m_loadingTextureName;

	// Token: 0x04000696 RID: 1686
	private AssetHandle<Texture> m_loadedTexture;
}
