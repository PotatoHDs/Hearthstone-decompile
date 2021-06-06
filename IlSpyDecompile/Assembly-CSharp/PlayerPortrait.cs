using bgs;
using Blizzard.T5.AssetManager;
using UnityEngine;

public class PlayerPortrait : MonoBehaviour
{
	private BnetProgramId m_programId;

	private string m_currentTextureName;

	private string m_loadingTextureName;

	private AssetHandle<Texture> m_loadedTexture;

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_loadedTexture);
	}

	public BnetProgramId GetProgramId()
	{
		return m_programId;
	}

	public bool SetProgramId(BnetProgramId programId)
	{
		if (m_programId == programId)
		{
			return false;
		}
		m_programId = programId;
		UpdateIcon();
		return true;
	}

	public bool IsIconReady()
	{
		if (m_loadingTextureName == null)
		{
			return m_currentTextureName != null;
		}
		return false;
	}

	public bool IsIconLoading()
	{
		return m_loadingTextureName != null;
	}

	private void UpdateIcon()
	{
		if (m_programId == null)
		{
			m_currentTextureName = null;
			m_loadingTextureName = null;
			GetComponent<Renderer>().GetMaterial().mainTexture = null;
			return;
		}
		string textureName = BnetProgramId.GetTextureName(m_programId);
		if (!(m_currentTextureName == textureName) && !(m_loadingTextureName == textureName))
		{
			m_loadingTextureName = textureName;
			AssetLoader.Get().LoadAsset<Texture>(m_loadingTextureName, OnTextureLoaded);
		}
	}

	private void OnTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		using (texture)
		{
			if (!(assetRef.ToString() != m_loadingTextureName))
			{
				if (!texture)
				{
					Error.AddDevFatal("PlayerPortrait.OnTextureLoaded() - Failed to load {0}. ProgramId={1}", assetRef, m_programId);
					m_currentTextureName = null;
					m_loadingTextureName = null;
				}
				else
				{
					m_currentTextureName = m_loadingTextureName;
					m_loadingTextureName = null;
					AssetHandle.Set(ref m_loadedTexture, texture);
					GetComponent<Renderer>().GetMaterial().mainTexture = texture;
				}
			}
		}
	}
}
