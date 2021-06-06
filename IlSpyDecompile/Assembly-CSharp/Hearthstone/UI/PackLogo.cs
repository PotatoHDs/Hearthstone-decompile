using System;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	[RequireComponent(typeof(Renderer))]
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/PackLogo", UniqueWithinCategory = "asset")]
	public class PackLogo : CustomWidgetBehavior
	{
		public enum LogoTextureType
		{
			Default,
			Glow,
			MiniSet
		}

		private const BoosterDbId FIRST_EXPANSION = BoosterDbId.GOBLINS_VS_GNOMES;

		[Tooltip("This is the pack displayed by default. INVALID means nothing will be displayed.")]
		[SerializeField]
		private BoosterDbId m_defaultPack = BoosterDbId.GOBLINS_VS_GNOMES;

		[Tooltip("If true, it will use data model 'pack' whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		[Tooltip("Which pack logo texture should get used?")]
		[SerializeField]
		private LogoTextureType m_textureType;

		[Tooltip("Try to load the localized version of this texture first")]
		[SerializeField]
		private bool m_localizeTexture = true;

		[Tooltip("Base Material to texture with")]
		[SerializeField]
		private Material m_baseMaterial;

		private Renderer m_renderer;

		private Material m_material;

		private AssetHandle<Texture> m_texture;

		private BoosterDbId m_displayedPack;

		private LogoTextureType m_displayedTextureType;

		[Overridable]
		public LogoTextureType TextureType
		{
			get
			{
				return m_textureType;
			}
			set
			{
				m_textureType = value;
			}
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
			m_displayedPack = BoosterDbId.INVALID;
			m_renderer = GetComponent<Renderer>();
			m_renderer.enabled = false;
			if (m_baseMaterial == null)
			{
				m_baseMaterial = m_renderer.GetSharedMaterial() ?? m_renderer.GetMaterial();
			}
			CreatePreviewableObject();
		}

		protected override void OnDestroy()
		{
			AssetHandle.SafeDispose(ref m_texture);
			base.OnDestroy();
		}

		private void CreatePreviewableObject()
		{
			CreatePreviewableObject(delegate(IPreviewableObject previewable, Action<GameObject> callback)
			{
				m_displayedPack = BoosterDbId.INVALID;
				m_renderer.enabled = false;
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.PackLogo.OnInitialize() - AssetLoader not available");
					callback(null);
				}
				else if (m_baseMaterial == null)
				{
					Debug.LogWarning("Hearthstone.UI.PackLogo.OnInitialize() - No material found");
					callback(null);
				}
				else
				{
					m_displayedPack = GetDesiredPack();
					if (m_displayedPack == BoosterDbId.INVALID)
					{
						callback(null);
					}
					else
					{
						m_displayedTextureType = m_textureType;
						string textureName = null;
						using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(m_displayedPack))
						{
							StorePackDef storePackDef = (assetHandle ? assetHandle.Asset.GetComponent<StorePackDef>() : null);
							if (storePackDef == null)
							{
								callback(null);
								return;
							}
							switch (m_textureType)
							{
							case LogoTextureType.Default:
								textureName = storePackDef.GetLogoTextureName();
								break;
							case LogoTextureType.Glow:
								textureName = storePackDef.GetLogoTextureGlowName();
								break;
							case LogoTextureType.MiniSet:
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
							using (loadedTexture)
							{
								if (!loadedTexture)
								{
									if ((bool)data)
									{
										Error.AddDevFatal("Loading localized logo failed.  This is normal if we're on android and just switched.  Trying unlocalized.");
										AssetLoader.Get().LoadAsset(textureName, onTextureLoaded, false, AssetLoadingOptions.DisableLocalization);
									}
									else
									{
										Debug.LogError($"Failed to load unlocalized texture {base.name}!");
									}
								}
								else
								{
									AssetHandle.Set(ref m_texture, loadedTexture);
									m_renderer.enabled = true;
									m_material = new Material(m_baseMaterial);
									m_material.mainTexture = m_texture;
									m_renderer.SetMaterial(m_material);
								}
							}
						};
						m_renderer.enabled = false;
						AssetLoader.Get().LoadAsset(textureName, onTextureLoaded, true, (!m_localizeTexture) ? AssetLoadingOptions.DisableLocalization : AssetLoadingOptions.None);
						callback(null);
					}
				}
			}, (IPreviewableObject o) => m_displayedPack != GetDesiredPack() || m_displayedTextureType != m_textureType);
		}

		private BoosterDbId GetDesiredPack()
		{
			if (m_useDataModel)
			{
				if (GetDataModel(25, out var dataModel))
				{
					return ((PackDataModel)dataModel).Type;
				}
				if (m_textureType == LogoTextureType.MiniSet && GetDataModel(15, out dataModel))
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
			return m_defaultPack;
		}
	}
}
