using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

namespace Hearthstone.UI
{
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/AdventureLogo", UniqueWithinCategory = "asset")]
	public class AdventureLogo : CustomWidgetBehavior
	{
		private class AdventureLogoRenderData
		{
			public Renderer m_renderer;

			public Material m_material;

			public Material m_baseMaterial;
		}

		private class AdventureLogoCallbackData
		{
			public AdventureLogoRenderData m_renderData;

			public AdventureDbId m_adventureDbId;

			public bool m_useLocalizationFallback;

			public AdventureLogoCallbackData(AdventureLogoRenderData renderData, AdventureDbId adventureDbId, bool useLocalizationFallback)
			{
				m_renderData = renderData;
				m_adventureDbId = adventureDbId;
				m_useLocalizationFallback = useLocalizationFallback;
			}
		}

		private const AdventureDbId FIRST_ADVENTURE = AdventureDbId.NAXXRAMAS;

		[Tooltip("This is the adventure displayed by default. INVALID means nothing will be displayed.")]
		[SerializeField]
		private AdventureDbId m_defaultAdventure = AdventureDbId.NAXXRAMAS;

		[Tooltip("Try to load the localized version of this texture first.")]
		[SerializeField]
		private bool m_localizeTexture = true;

		[Tooltip("Display a shadow.")]
		[SerializeField]
		private bool m_useShadow = true;

		[Tooltip("Logo Material to texture with.")]
		[SerializeField]
		private Material m_logoBaseMaterial;

		[Tooltip("Shadow Material to texture with.")]
		[SerializeField]
		private Material m_shadowBaseMaterial;

		private AdventureLogoRenderData m_logoRenderData = new AdventureLogoRenderData();

		private AdventureLogoRenderData m_shadowRenderData = new AdventureLogoRenderData();

		private AdventureDbId m_displayedAdventure;

		private AdventureDbId m_desiredAdventure;

		private bool m_isUsingShadow;

		[Overridable]
		public long AdventureID
		{
			get
			{
				return (long)((m_desiredAdventure != 0) ? m_desiredAdventure : m_defaultAdventure);
			}
			set
			{
				m_desiredAdventure = (AdventureDbId)value;
			}
		}

		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return m_useShadow;
			}
			set
			{
				m_useShadow = value;
			}
		}

		[Overridable]
		public float ShadowOffset_X
		{
			get
			{
				if (!(m_shadowRenderData.m_renderer != null))
				{
					return 0f;
				}
				return m_shadowRenderData.m_renderer.transform.localPosition.x;
			}
			set
			{
				if (m_shadowRenderData.m_renderer != null)
				{
					Vector3 localPosition = m_shadowRenderData.m_renderer.transform.localPosition;
					localPosition.x = value;
					m_shadowRenderData.m_renderer.transform.localPosition = localPosition;
				}
			}
		}

		[Overridable]
		public float ShadowOffset_Y
		{
			get
			{
				if (!(m_shadowRenderData.m_renderer != null))
				{
					return 0f;
				}
				return m_shadowRenderData.m_renderer.transform.localPosition.y;
			}
			set
			{
				if (m_shadowRenderData.m_renderer != null)
				{
					Vector3 localPosition = m_shadowRenderData.m_renderer.transform.localPosition;
					localPosition.y = value;
					m_shadowRenderData.m_renderer.transform.localPosition = localPosition;
				}
			}
		}

		[Overridable]
		public float ShadowOffset_Z
		{
			get
			{
				if (!(m_shadowRenderData.m_renderer != null))
				{
					return 0f;
				}
				return m_shadowRenderData.m_renderer.transform.localPosition.z;
			}
			set
			{
				if (m_shadowRenderData.m_renderer != null)
				{
					Vector3 localPosition = m_shadowRenderData.m_renderer.transform.localPosition;
					localPosition.z = value;
					m_shadowRenderData.m_renderer.transform.localPosition = localPosition;
				}
			}
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
			m_displayedAdventure = AdventureDbId.INVALID;
			if (m_logoRenderData.m_renderer == null)
			{
				m_logoRenderData.m_renderer = CreateRenderObject("logo");
			}
			if (m_logoBaseMaterial == null)
			{
				m_logoBaseMaterial = m_logoRenderData.m_renderer.GetSharedMaterial() ?? m_logoRenderData.m_renderer.GetMaterial();
			}
			m_logoRenderData.m_baseMaterial = m_logoBaseMaterial;
			if (m_shadowRenderData.m_renderer == null)
			{
				m_shadowRenderData.m_renderer = CreateRenderObject("shadow");
			}
			if (m_shadowBaseMaterial == null)
			{
				m_shadowBaseMaterial = m_shadowRenderData.m_renderer.GetSharedMaterial() ?? m_shadowRenderData.m_renderer.GetMaterial();
			}
			m_shadowRenderData.m_baseMaterial = m_shadowBaseMaterial;
			CreatePreviewableObject();
		}

		private void CreatePreviewableObject()
		{
			string textureName;
			AssetHandleCallback<Texture> onTextureLoaded;
			CreatePreviewableObject(delegate(IPreviewableObject previewable, Action<GameObject> callback)
			{
				m_displayedAdventure = AdventureDbId.INVALID;
				m_isUsingShadow = false;
				m_logoRenderData.m_renderer.enabled = false;
				m_shadowRenderData.m_renderer.enabled = false;
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - AssetLoader not available");
					callback(null);
				}
				else if (m_logoBaseMaterial == null)
				{
					Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - No material found");
					callback(null);
				}
				else
				{
					m_displayedAdventure = (AdventureDbId)AdventureID;
					if (m_displayedAdventure == AdventureDbId.INVALID)
					{
						callback(null);
					}
					else
					{
						using AssetHandle<GameObject> assetHandle = ShopUtils.LoadStoreAdventurePrefab(m_displayedAdventure);
						StoreAdventureDef storeAdventureDef = (assetHandle ? assetHandle.Asset.GetComponent<StoreAdventureDef>() : null);
						if (storeAdventureDef == null)
						{
							callback(null);
						}
						else
						{
							textureName = storeAdventureDef.GetLogoTextureName();
							if (string.IsNullOrEmpty(textureName))
							{
								Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - No logo texture defined");
								callback(null);
							}
							else
							{
								onTextureLoaded = null;
								onTextureLoaded = delegate(AssetReference name, AssetHandle<Texture> loadedTexture, object data)
								{
									AdventureLogoCallbackData adventureLogoCallbackData = data as AdventureLogoCallbackData;
									if (adventureLogoCallbackData == null)
									{
										Debug.LogError($"Invalid callback data provided for {base.name}. Must be AdventureLogoCallbackData!");
										loadedTexture?.Dispose();
									}
									else if (adventureLogoCallbackData.m_adventureDbId != m_displayedAdventure)
									{
										loadedTexture?.Dispose();
									}
									else if (!loadedTexture)
									{
										if (adventureLogoCallbackData.m_useLocalizationFallback)
										{
											Error.AddDevFatal("Loading localized logo failed. This is normal if we're on android and just switched. Trying unlocalized.");
											AdventureLogoCallbackData callbackData3 = new AdventureLogoCallbackData(adventureLogoCallbackData.m_renderData, adventureLogoCallbackData.m_adventureDbId, useLocalizationFallback: true);
											AssetLoader.Get().LoadAsset(textureName, onTextureLoaded, callbackData3, AssetLoadingOptions.DisableLocalization);
										}
										else
										{
											Debug.LogError($"Failed to load texture {base.name} for {adventureLogoCallbackData.m_renderData.m_renderer.name}!");
										}
									}
									else
									{
										AdventureLogoRenderData renderData = adventureLogoCallbackData.m_renderData;
										Renderer renderer = renderData.m_renderer;
										Material baseMaterial = renderData.m_baseMaterial;
										renderer.enabled = true;
										HearthstoneServices.Get<DisposablesCleaner>()?.Attach(renderer, loadedTexture);
										renderData.m_material = new Material(baseMaterial);
										renderData.m_material.mainTexture = loadedTexture;
										renderData.m_renderer.SetMaterial(0, renderData.m_material);
									}
								};
								AdventureLogoCallbackData callbackData = new AdventureLogoCallbackData(m_logoRenderData, m_displayedAdventure, useLocalizationFallback: true);
								AssetLoader.Get().LoadAsset(textureName, onTextureLoaded, callbackData, (!m_localizeTexture) ? AssetLoadingOptions.DisableLocalization : AssetLoadingOptions.None);
								if (m_useShadow)
								{
									string logoShadowTextureName = storeAdventureDef.GetLogoShadowTextureName();
									if (string.IsNullOrEmpty(logoShadowTextureName))
									{
										Debug.LogWarning("Hearthstone.UI.AdventureLogo.OnInitialize() - No logo SHADOW texture defined");
									}
									else
									{
										AdventureLogoCallbackData callbackData2 = new AdventureLogoCallbackData(m_shadowRenderData, m_displayedAdventure, useLocalizationFallback: false);
										AssetLoader.Get().LoadAsset(logoShadowTextureName, onTextureLoaded, callbackData2);
										m_isUsingShadow = true;
									}
								}
								callback(null);
							}
						}
					}
				}
			}, (IPreviewableObject o) => m_displayedAdventure != (AdventureDbId)AdventureID || m_useShadow != m_isUsingShadow);
		}

		private Renderer CreateRenderObject(string suffix)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gameObject.transform.SetParent(base.transform, worldPositionStays: false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			Transform parent = gameObject.transform.parent;
			if (parent != null)
			{
				gameObject.layer = parent.gameObject.layer;
			}
			gameObject.name = $"{base.name}_{suffix}(Dynamic)";
			gameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
			if (gameObject.GetComponent<OwnedByWidgetBehavior>() == null)
			{
				gameObject.AddComponent<OwnedByWidgetBehavior>().Owner = this;
			}
			MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
			component.enabled = false;
			return component;
		}
	}
}
