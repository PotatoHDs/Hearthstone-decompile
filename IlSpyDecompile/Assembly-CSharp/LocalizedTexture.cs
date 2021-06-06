using Blizzard.T5.AssetManager;
using UnityEngine;

[CustomEditClass]
public class LocalizedTexture : MonoBehaviour
{
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_textureName;

	private AssetHandle<Texture> m_loadedTexture;

	private void Awake()
	{
		if (string.IsNullOrEmpty(m_textureName))
		{
			Debug.LogWarningFormat("LocalizedTexture: skipping load for empty texture! go={0}", base.gameObject);
		}
		else
		{
			AssetLoader.Get().LoadAsset<Texture>(m_textureName, OnTextureLoaded);
		}
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_loadedTexture);
	}

	private void OnTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		AssetHandle.Take(ref m_loadedTexture, texture);
		if (m_loadedTexture == null)
		{
			if (PlatformSettings.LocaleVariant != LocaleVariant.China && Localization.GetLocale() != 0)
			{
				AssetLoader.Get().LoadAsset(ref m_loadedTexture, m_textureName, AssetLoadingOptions.DisableLocalization);
			}
			if (m_loadedTexture == null)
			{
				Debug.LogErrorFormat("Failed to load LocalizedTexture: go={0}, assetRef={1}", base.gameObject, assetRef);
				return;
			}
		}
		GetComponent<Renderer>().GetMaterial().mainTexture = m_loadedTexture;
	}
}
