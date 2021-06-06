using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x020008E6 RID: 2278
[CustomEditClass]
public class LocalizedTexture : MonoBehaviour
{
	// Token: 0x06007E7B RID: 32379 RVA: 0x0028EE78 File Offset: 0x0028D078
	private void Awake()
	{
		if (string.IsNullOrEmpty(this.m_textureName))
		{
			Debug.LogWarningFormat("LocalizedTexture: skipping load for empty texture! go={0}", new object[]
			{
				base.gameObject
			});
			return;
		}
		AssetLoader.Get().LoadAsset<Texture>(this.m_textureName, new AssetHandleCallback<Texture>(this.OnTextureLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06007E7C RID: 32380 RVA: 0x0028EED0 File Offset: 0x0028D0D0
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedTexture);
	}

	// Token: 0x06007E7D RID: 32381 RVA: 0x0028EEE0 File Offset: 0x0028D0E0
	private void OnTextureLoaded(AssetReference assetRef, AssetHandle<Texture> texture, object callbackData)
	{
		AssetHandle.Take<Texture>(ref this.m_loadedTexture, texture);
		if (this.m_loadedTexture == null)
		{
			if (PlatformSettings.LocaleVariant != LocaleVariant.China && Localization.GetLocale() != Locale.enUS)
			{
				AssetLoader.Get().LoadAsset<Texture>(ref this.m_loadedTexture, this.m_textureName, AssetLoadingOptions.DisableLocalization);
			}
			if (this.m_loadedTexture == null)
			{
				Debug.LogErrorFormat("Failed to load LocalizedTexture: go={0}, assetRef={1}", new object[]
				{
					base.gameObject,
					assetRef
				});
				return;
			}
		}
		base.GetComponent<Renderer>().GetMaterial().mainTexture = this.m_loadedTexture;
	}

	// Token: 0x0400662B RID: 26155
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_textureName;

	// Token: 0x0400662C RID: 26156
	private AssetHandle<Texture> m_loadedTexture;
}
