using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FDA RID: 4058
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/AdventureLogo", UniqueWithinCategory = "asset")]
	public class AdventureLogo : CustomWidgetBehavior
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600B098 RID: 45208 RVA: 0x00368B00 File Offset: 0x00366D00
		// (set) Token: 0x0600B099 RID: 45209 RVA: 0x00368B19 File Offset: 0x00366D19
		[Overridable]
		public long AdventureID
		{
			get
			{
				return (long)((this.m_desiredAdventure != AdventureDbId.INVALID) ? this.m_desiredAdventure : this.m_defaultAdventure);
			}
			set
			{
				this.m_desiredAdventure = (AdventureDbId)value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600B09A RID: 45210 RVA: 0x00368B23 File Offset: 0x00366D23
		// (set) Token: 0x0600B09B RID: 45211 RVA: 0x00368B2B File Offset: 0x00366D2B
		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return this.m_useShadow;
			}
			set
			{
				this.m_useShadow = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600B09C RID: 45212 RVA: 0x00368B34 File Offset: 0x00366D34
		// (set) Token: 0x0600B09D RID: 45213 RVA: 0x00368B6C File Offset: 0x00366D6C
		[Overridable]
		public float ShadowOffset_X
		{
			get
			{
				if (!(this.m_shadowRenderData.m_renderer != null))
				{
					return 0f;
				}
				return this.m_shadowRenderData.m_renderer.transform.localPosition.x;
			}
			set
			{
				if (this.m_shadowRenderData.m_renderer != null)
				{
					Vector3 localPosition = this.m_shadowRenderData.m_renderer.transform.localPosition;
					localPosition.x = value;
					this.m_shadowRenderData.m_renderer.transform.localPosition = localPosition;
				}
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600B09E RID: 45214 RVA: 0x00368BC0 File Offset: 0x00366DC0
		// (set) Token: 0x0600B09F RID: 45215 RVA: 0x00368BF8 File Offset: 0x00366DF8
		[Overridable]
		public float ShadowOffset_Y
		{
			get
			{
				if (!(this.m_shadowRenderData.m_renderer != null))
				{
					return 0f;
				}
				return this.m_shadowRenderData.m_renderer.transform.localPosition.y;
			}
			set
			{
				if (this.m_shadowRenderData.m_renderer != null)
				{
					Vector3 localPosition = this.m_shadowRenderData.m_renderer.transform.localPosition;
					localPosition.y = value;
					this.m_shadowRenderData.m_renderer.transform.localPosition = localPosition;
				}
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600B0A0 RID: 45216 RVA: 0x00368C4C File Offset: 0x00366E4C
		// (set) Token: 0x0600B0A1 RID: 45217 RVA: 0x00368C84 File Offset: 0x00366E84
		[Overridable]
		public float ShadowOffset_Z
		{
			get
			{
				if (!(this.m_shadowRenderData.m_renderer != null))
				{
					return 0f;
				}
				return this.m_shadowRenderData.m_renderer.transform.localPosition.z;
			}
			set
			{
				if (this.m_shadowRenderData.m_renderer != null)
				{
					Vector3 localPosition = this.m_shadowRenderData.m_renderer.transform.localPosition;
					localPosition.z = value;
					this.m_shadowRenderData.m_renderer.transform.localPosition = localPosition;
				}
			}
		}

		// Token: 0x0600B0A2 RID: 45218 RVA: 0x00368CD8 File Offset: 0x00366ED8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.m_displayedAdventure = AdventureDbId.INVALID;
			if (this.m_logoRenderData.m_renderer == null)
			{
				this.m_logoRenderData.m_renderer = this.CreateRenderObject("logo");
			}
			if (this.m_logoBaseMaterial == null)
			{
				this.m_logoBaseMaterial = (this.m_logoRenderData.m_renderer.GetSharedMaterial() ?? this.m_logoRenderData.m_renderer.GetMaterial());
			}
			this.m_logoRenderData.m_baseMaterial = this.m_logoBaseMaterial;
			if (this.m_shadowRenderData.m_renderer == null)
			{
				this.m_shadowRenderData.m_renderer = this.CreateRenderObject("shadow");
			}
			if (this.m_shadowBaseMaterial == null)
			{
				this.m_shadowBaseMaterial = (this.m_shadowRenderData.m_renderer.GetSharedMaterial() ?? this.m_shadowRenderData.m_renderer.GetMaterial());
			}
			this.m_shadowRenderData.m_baseMaterial = this.m_shadowBaseMaterial;
			this.CreatePreviewableObject();
		}

		// Token: 0x0600B0A3 RID: 45219 RVA: 0x00368DDC File Offset: 0x00366FDC
		private void CreatePreviewableObject()
		{
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				this.m_displayedAdventure = AdventureDbId.INVALID;
				this.m_isUsingShadow = false;
				this.m_logoRenderData.m_renderer.enabled = false;
				this.m_shadowRenderData.m_renderer.enabled = false;
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - AssetLoader not available");
					callback(null);
					return;
				}
				if (this.m_logoBaseMaterial == null)
				{
					Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - No material found");
					callback(null);
					return;
				}
				this.m_displayedAdventure = (AdventureDbId)this.AdventureID;
				if (this.m_displayedAdventure == AdventureDbId.INVALID)
				{
					callback(null);
					return;
				}
				using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStoreAdventurePrefab(this.m_displayedAdventure))
				{
					StoreAdventureDef storeAdventureDef = assetHandle ? assetHandle.Asset.GetComponent<StoreAdventureDef>() : null;
					if (storeAdventureDef == null)
					{
						callback(null);
					}
					else
					{
						string textureName = storeAdventureDef.GetLogoTextureName();
						if (string.IsNullOrEmpty(textureName))
						{
							Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - No logo texture defined");
							callback(null);
						}
						else
						{
							AssetHandleCallback<Texture> onTextureLoaded = null;
							onTextureLoaded = delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
							{
								AdventureLogo.AdventureLogoCallbackData adventureLogoCallbackData = data as AdventureLogo.AdventureLogoCallbackData;
								if (adventureLogoCallbackData == null)
								{
									Debug.LogError(string.Format("Invalid callback data provided for {0}. Must be AdventureLogoCallbackData!", this.name));
									if (loadedTexture != null)
									{
										loadedTexture.Dispose();
									}
									return;
								}
								if (adventureLogoCallbackData.m_adventureDbId != this.m_displayedAdventure)
								{
									if (loadedTexture != null)
									{
										loadedTexture.Dispose();
									}
									return;
								}
								if (loadedTexture)
								{
									AdventureLogo.AdventureLogoRenderData renderData = adventureLogoCallbackData.m_renderData;
									Renderer renderer = renderData.m_renderer;
									Material baseMaterial = renderData.m_baseMaterial;
									renderer.enabled = true;
									DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
									if (disposablesCleaner != null)
									{
										disposablesCleaner.Attach(renderer, loadedTexture);
									}
									renderData.m_material = new Material(baseMaterial);
									renderData.m_material.mainTexture = loadedTexture;
									renderData.m_renderer.SetMaterial(0, renderData.m_material);
									return;
								}
								if (adventureLogoCallbackData.m_useLocalizationFallback)
								{
									Error.AddDevFatal("Loading localized logo failed. This is normal if we're on android and just switched. Trying unlocalized.", Array.Empty<object>());
									AdventureLogo.AdventureLogoCallbackData callbackData3 = new AdventureLogo.AdventureLogoCallbackData(adventureLogoCallbackData.m_renderData, adventureLogoCallbackData.m_adventureDbId, true);
									AssetLoader.Get().LoadAsset<Texture>(textureName, onTextureLoaded, callbackData3, AssetLoadingOptions.DisableLocalization);
									return;
								}
								Debug.LogError(string.Format("Failed to load texture {0} for {1}!", this.name, adventureLogoCallbackData.m_renderData.m_renderer.name));
							};
							AdventureLogo.AdventureLogoCallbackData callbackData = new AdventureLogo.AdventureLogoCallbackData(this.m_logoRenderData, this.m_displayedAdventure, true);
							AssetLoader.Get().LoadAsset<Texture>(textureName, onTextureLoaded, callbackData, this.m_localizeTexture ? AssetLoadingOptions.None : AssetLoadingOptions.DisableLocalization);
							if (this.m_useShadow)
							{
								string logoShadowTextureName = storeAdventureDef.GetLogoShadowTextureName();
								if (string.IsNullOrEmpty(logoShadowTextureName))
								{
									Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - No logo SHADOW texture defined");
								}
								else
								{
									AdventureLogo.AdventureLogoCallbackData callbackData2 = new AdventureLogo.AdventureLogoCallbackData(this.m_shadowRenderData, this.m_displayedAdventure, false);
									AssetLoader.Get().LoadAsset<Texture>(logoShadowTextureName, onTextureLoaded, callbackData2, AssetLoadingOptions.None);
									this.m_isUsingShadow = true;
								}
							}
							callback(null);
						}
					}
				}
			}, (CustomWidgetBehavior.IPreviewableObject o) => this.m_displayedAdventure != (AdventureDbId)this.AdventureID || this.m_useShadow != this.m_isUsingShadow, null);
		}

		// Token: 0x0600B0A4 RID: 45220 RVA: 0x00368E00 File Offset: 0x00367000
		private Renderer CreateRenderObject(string suffix)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gameObject.transform.SetParent(base.transform, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			Transform parent = gameObject.transform.parent;
			if (parent != null)
			{
				gameObject.layer = parent.gameObject.layer;
			}
			gameObject.name = string.Format("{0}_{1}(Dynamic)", base.name, suffix);
			gameObject.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			if (gameObject.GetComponent<OwnedByWidgetBehavior>() == null)
			{
				gameObject.AddComponent<OwnedByWidgetBehavior>().Owner = this;
			}
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			component.enabled = false;
			return component;
		}

		// Token: 0x0400953D RID: 38205
		private const AdventureDbId FIRST_ADVENTURE = AdventureDbId.NAXXRAMAS;

		// Token: 0x0400953E RID: 38206
		[Tooltip("This is the adventure displayed by default. INVALID means nothing will be displayed.")]
		[SerializeField]
		private AdventureDbId m_defaultAdventure = AdventureDbId.NAXXRAMAS;

		// Token: 0x0400953F RID: 38207
		[Tooltip("Try to load the localized version of this texture first.")]
		[SerializeField]
		private bool m_localizeTexture = true;

		// Token: 0x04009540 RID: 38208
		[Tooltip("Display a shadow.")]
		[SerializeField]
		private bool m_useShadow = true;

		// Token: 0x04009541 RID: 38209
		[Tooltip("Logo Material to texture with.")]
		[SerializeField]
		private Material m_logoBaseMaterial;

		// Token: 0x04009542 RID: 38210
		[Tooltip("Shadow Material to texture with.")]
		[SerializeField]
		private Material m_shadowBaseMaterial;

		// Token: 0x04009543 RID: 38211
		private AdventureLogo.AdventureLogoRenderData m_logoRenderData = new AdventureLogo.AdventureLogoRenderData();

		// Token: 0x04009544 RID: 38212
		private AdventureLogo.AdventureLogoRenderData m_shadowRenderData = new AdventureLogo.AdventureLogoRenderData();

		// Token: 0x04009545 RID: 38213
		private AdventureDbId m_displayedAdventure;

		// Token: 0x04009546 RID: 38214
		private AdventureDbId m_desiredAdventure;

		// Token: 0x04009547 RID: 38215
		private bool m_isUsingShadow;

		// Token: 0x0200280F RID: 10255
		private class AdventureLogoRenderData
		{
			// Token: 0x0400F867 RID: 63591
			public Renderer m_renderer;

			// Token: 0x0400F868 RID: 63592
			public Material m_material;

			// Token: 0x0400F869 RID: 63593
			public Material m_baseMaterial;
		}

		// Token: 0x02002810 RID: 10256
		private class AdventureLogoCallbackData
		{
			// Token: 0x06013AF2 RID: 80626 RVA: 0x0053A1CB File Offset: 0x005383CB
			public AdventureLogoCallbackData(AdventureLogo.AdventureLogoRenderData renderData, AdventureDbId adventureDbId, bool useLocalizationFallback)
			{
				this.m_renderData = renderData;
				this.m_adventureDbId = adventureDbId;
				this.m_useLocalizationFallback = useLocalizationFallback;
			}

			// Token: 0x0400F86A RID: 63594
			public AdventureLogo.AdventureLogoRenderData m_renderData;

			// Token: 0x0400F86B RID: 63595
			public AdventureDbId m_adventureDbId;

			// Token: 0x0400F86C RID: 63596
			public bool m_useLocalizationFallback;
		}
	}
}
