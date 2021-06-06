using System;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FE0 RID: 4064
	[RequireComponent(typeof(Renderer))]
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/PackLogo", UniqueWithinCategory = "asset")]
	public class PackLogo : CustomWidgetBehavior
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600B0E4 RID: 45284 RVA: 0x0036A47C File Offset: 0x0036867C
		// (set) Token: 0x0600B0E5 RID: 45285 RVA: 0x0036A484 File Offset: 0x00368684
		[Overridable]
		public PackLogo.LogoTextureType TextureType
		{
			get
			{
				return this.m_textureType;
			}
			set
			{
				this.m_textureType = value;
			}
		}

		// Token: 0x0600B0E6 RID: 45286 RVA: 0x0036A490 File Offset: 0x00368690
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.m_displayedPack = BoosterDbId.INVALID;
			this.m_renderer = base.GetComponent<Renderer>();
			this.m_renderer.enabled = false;
			if (this.m_baseMaterial == null)
			{
				this.m_baseMaterial = (this.m_renderer.GetSharedMaterial() ?? this.m_renderer.GetMaterial());
			}
			this.CreatePreviewableObject();
		}

		// Token: 0x0600B0E7 RID: 45287 RVA: 0x0036A4F6 File Offset: 0x003686F6
		protected override void OnDestroy()
		{
			AssetHandle.SafeDispose<Texture>(ref this.m_texture);
			base.OnDestroy();
		}

		// Token: 0x0600B0E8 RID: 45288 RVA: 0x0036A509 File Offset: 0x00368709
		private void CreatePreviewableObject()
		{
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				this.m_displayedPack = BoosterDbId.INVALID;
				this.m_renderer.enabled = false;
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.PackLogo.OnInitialize() - AssetLoader not available");
					callback(null);
					return;
				}
				if (this.m_baseMaterial == null)
				{
					Debug.LogWarning("Hearthstone.UI.PackLogo.OnInitialize() - No material found");
					callback(null);
					return;
				}
				this.m_displayedPack = this.GetDesiredPack();
				if (this.m_displayedPack == BoosterDbId.INVALID)
				{
					callback(null);
					return;
				}
				this.m_displayedTextureType = this.m_textureType;
				string textureName = null;
				using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(this.m_displayedPack))
				{
					StorePackDef storePackDef = assetHandle ? assetHandle.Asset.GetComponent<StorePackDef>() : null;
					if (storePackDef == null)
					{
						callback(null);
						return;
					}
					switch (this.m_textureType)
					{
					case PackLogo.LogoTextureType.Default:
						textureName = storePackDef.GetLogoTextureName();
						break;
					case PackLogo.LogoTextureType.Glow:
						textureName = storePackDef.GetLogoTextureGlowName();
						break;
					case PackLogo.LogoTextureType.MiniSet:
						textureName = storePackDef.GetMiniSetTextureName();
						break;
					}
					if (string.IsNullOrEmpty(textureName))
					{
						Debug.LogWarning("Hearthstone.UI.PackLogo.OnInitialize() - No logo texture defined");
						callback(null);
						return;
					}
				}
				AssetHandleCallback<Texture> onTextureLoaded = null;
				onTextureLoaded = delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
				{
					try
					{
						if (!loadedTexture)
						{
							if ((bool)data)
							{
								Error.AddDevFatal("Loading localized logo failed.  This is normal if we're on android and just switched.  Trying unlocalized.", Array.Empty<object>());
								AssetLoader.Get().LoadAsset<Texture>(textureName, onTextureLoaded, false, AssetLoadingOptions.DisableLocalization);
							}
							else
							{
								Debug.LogError(string.Format("Failed to load unlocalized texture {0}!", this.name));
							}
						}
						else
						{
							AssetHandle.Set<Texture>(ref this.m_texture, loadedTexture);
							this.m_renderer.enabled = true;
							this.m_material = new Material(this.m_baseMaterial);
							this.m_material.mainTexture = this.m_texture;
							this.m_renderer.SetMaterial(this.m_material);
						}
					}
					finally
					{
						if (loadedTexture != null)
						{
							((IDisposable)loadedTexture).Dispose();
						}
					}
				};
				this.m_renderer.enabled = false;
				AssetLoader.Get().LoadAsset<Texture>(textureName, onTextureLoaded, true, this.m_localizeTexture ? AssetLoadingOptions.None : AssetLoadingOptions.DisableLocalization);
				callback(null);
			}, (CustomWidgetBehavior.IPreviewableObject o) => this.m_displayedPack != this.GetDesiredPack() || this.m_displayedTextureType != this.m_textureType, null);
		}

		// Token: 0x0600B0E9 RID: 45289 RVA: 0x0036A52C File Offset: 0x0036872C
		private BoosterDbId GetDesiredPack()
		{
			if (this.m_useDataModel)
			{
				IDataModel dataModel;
				if (base.GetDataModel(25, out dataModel))
				{
					return ((PackDataModel)dataModel).Type;
				}
				if (this.m_textureType == PackLogo.LogoTextureType.MiniSet && base.GetDataModel(15, out dataModel))
				{
					MiniSetDbfRecord dbfRecord = MiniSetLayout.GetDbfRecord((ProductDataModel)dataModel);
					if (dbfRecord != null)
					{
						return (BoosterDbId)dbfRecord.BoosterId;
					}
				}
				if (Application.isPlaying)
				{
					return BoosterDbId.INVALID;
				}
			}
			return this.m_defaultPack;
		}

		// Token: 0x04009578 RID: 38264
		private const BoosterDbId FIRST_EXPANSION = BoosterDbId.GOBLINS_VS_GNOMES;

		// Token: 0x04009579 RID: 38265
		[Tooltip("This is the pack displayed by default. INVALID means nothing will be displayed.")]
		[SerializeField]
		private BoosterDbId m_defaultPack = BoosterDbId.GOBLINS_VS_GNOMES;

		// Token: 0x0400957A RID: 38266
		[Tooltip("If true, it will use data model 'pack' whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		// Token: 0x0400957B RID: 38267
		[Tooltip("Which pack logo texture should get used?")]
		[SerializeField]
		private PackLogo.LogoTextureType m_textureType;

		// Token: 0x0400957C RID: 38268
		[Tooltip("Try to load the localized version of this texture first")]
		[SerializeField]
		private bool m_localizeTexture = true;

		// Token: 0x0400957D RID: 38269
		[Tooltip("Base Material to texture with")]
		[SerializeField]
		private Material m_baseMaterial;

		// Token: 0x0400957E RID: 38270
		private Renderer m_renderer;

		// Token: 0x0400957F RID: 38271
		private Material m_material;

		// Token: 0x04009580 RID: 38272
		private AssetHandle<Texture> m_texture;

		// Token: 0x04009581 RID: 38273
		private BoosterDbId m_displayedPack;

		// Token: 0x04009582 RID: 38274
		private PackLogo.LogoTextureType m_displayedTextureType;

		// Token: 0x02002817 RID: 10263
		public enum LogoTextureType
		{
			// Token: 0x0400F881 RID: 63617
			Default,
			// Token: 0x0400F882 RID: 63618
			Glow,
			// Token: 0x0400F883 RID: 63619
			MiniSet
		}
	}
}
