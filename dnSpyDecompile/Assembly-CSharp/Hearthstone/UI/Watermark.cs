using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FE1 RID: 4065
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Watermark", UniqueWithinCategory = "asset")]
	public class Watermark : CustomWidgetBehavior
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600B0ED RID: 45293 RVA: 0x0036A77F File Offset: 0x0036897F
		// (set) Token: 0x0600B0EE RID: 45294 RVA: 0x0036A792 File Offset: 0x00368992
		[Overridable]
		public string AdventureIdName
		{
			get
			{
				return this.m_adventureDbId.ToString();
			}
			set
			{
				this.ResetSources();
				if (!EnumUtils.TryGetEnum<AdventureDbId>(value, out this.m_adventureDbId))
				{
					Debug.LogErrorFormat("Invalid AdventureDbId name: {0}; reseting to {1}", new object[]
					{
						value,
						this.AdventureIdName
					});
				}
				this.UpdateTargetTextureSource();
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600B0EF RID: 45295 RVA: 0x0036A7CB File Offset: 0x003689CB
		// (set) Token: 0x0600B0F0 RID: 45296 RVA: 0x0036A7DE File Offset: 0x003689DE
		[Overridable]
		public string BoosterIdName
		{
			get
			{
				return this.m_boosterDbId.ToString();
			}
			set
			{
				this.ResetSources();
				if (!EnumUtils.TryGetEnum<BoosterDbId>(value, out this.m_boosterDbId))
				{
					Debug.LogErrorFormat("Invalid BoosterDbId name: {0}; reseting to {1}", new object[]
					{
						value,
						this.BoosterIdName
					});
				}
				this.UpdateTargetTextureSource();
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600B0F1 RID: 45297 RVA: 0x0036A817 File Offset: 0x00368A17
		// (set) Token: 0x0600B0F2 RID: 45298 RVA: 0x0036A81F File Offset: 0x00368A1F
		[Overridable]
		public bool UseDataModel
		{
			get
			{
				return this.m_useDataModel;
			}
			set
			{
				this.ResetSources();
				this.m_useDataModel = value;
				this.UpdateTargetTextureSource();
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600B0F3 RID: 45299 RVA: 0x0036A834 File Offset: 0x00368A34
		// (set) Token: 0x0600B0F4 RID: 45300 RVA: 0x0036A83C File Offset: 0x00368A3C
		[Overridable]
		public bool UseMiniSetWatermark
		{
			get
			{
				return this.m_useMiniSetWatermark;
			}
			set
			{
				this.m_useMiniSetWatermark = value;
				this.UpdateTargetTextureSource();
			}
		}

		// Token: 0x0600B0F5 RID: 45301 RVA: 0x0036A84B File Offset: 0x00368A4B
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.UpdateTargetTextureSource();
			this.CreatePreviewableObject();
		}

		// Token: 0x0600B0F6 RID: 45302 RVA: 0x0036A85F File Offset: 0x00368A5F
		public override bool TryIncrementDataVersion(int id)
		{
			if (this.UseDataModel && this.m_dataModelIDs.Contains(id))
			{
				base.IncrementLocalDataVersion();
				return true;
			}
			return false;
		}

		// Token: 0x0600B0F7 RID: 45303 RVA: 0x0036A880 File Offset: 0x00368A80
		protected override void OnDestroy()
		{
			this.CleanUpMaterial();
			base.OnDestroy();
		}

		// Token: 0x0600B0F8 RID: 45304 RVA: 0x0036A88E File Offset: 0x00368A8E
		private void ResetSources()
		{
			this.m_lastDataVersion = 0;
			this.m_useDataModel = false;
			this.m_adventureDbId = AdventureDbId.INVALID;
			this.m_boosterDbId = BoosterDbId.INVALID;
		}

		// Token: 0x0600B0F9 RID: 45305 RVA: 0x0036A8AC File Offset: 0x00368AAC
		private void CleanUpMaterial()
		{
			AssetHandle.SafeDispose<Texture>(ref this.m_texture);
		}

		// Token: 0x0600B0FA RID: 45306 RVA: 0x0036A8B9 File Offset: 0x00368AB9
		private void CreatePreviewableObject()
		{
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				this.CleanUpMaterial();
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.Watermark.OnInitialize() - AssetLoader not available");
					callback(null);
					return;
				}
				if (this.m_baseMaterial == null)
				{
					Debug.LogWarning("Hearthstone.UI.Watermark.OnInitialize() - No material found");
					callback(null);
					return;
				}
				this.m_currentTextureSource = this.m_targetTextureSource;
				string textureName = this.GetTextureName(this.m_currentTextureSource);
				if (string.IsNullOrEmpty(textureName))
				{
					callback(null);
					return;
				}
				AssetHandleCallback<Texture> onTextureLoaded = null;
				onTextureLoaded = delegate(AssetReference name, AssetHandle<Texture> tex, object data)
				{
					Watermark.CallbackData callbackData = (Watermark.CallbackData)data;
					if (this.m_quad == null || callbackData.m_asyncOperationId != this.m_textureAsyncOperationId)
					{
						AssetHandle.SafeDispose<Texture>(ref tex);
						return;
					}
					if (tex != null)
					{
						AssetHandle.Take<Texture>(ref this.m_texture, tex);
						this.m_material = new Material(this.m_baseMaterial);
						this.m_material.name = string.Format("{0}({1})", this.m_baseMaterial.name, this.m_texture.Asset.name);
						this.m_material.mainTexture = this.m_texture;
						this.m_quad.GetComponent<Renderer>().SetSharedMaterial(this.m_material);
						return;
					}
					if (callbackData.m_localized)
					{
						callbackData.m_localized = false;
						Error.AddDevFatal("Loading localized logo failed.  This is normal if we're on android and just switched.  Trying unlocalized.", Array.Empty<object>());
						AssetLoader.Get().LoadAsset<Texture>(textureName, onTextureLoaded, callbackData, AssetLoadingOptions.DisableLocalization);
						return;
					}
					Debug.LogError(string.Format("Failed to load unlocalized texture {0}!", this.name));
				};
				this.m_quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
				this.m_quad.transform.SetParent(base.gameObject.transform, false);
				this.m_textureAsyncOperationId++;
				AssetLoader.Get().LoadAsset<Texture>(textureName, onTextureLoaded, new Watermark.CallbackData
				{
					m_localized = true,
					m_asyncOperationId = this.m_textureAsyncOperationId
				}, AssetLoadingOptions.None);
				callback(this.m_quad);
			}, (CustomWidgetBehavior.IPreviewableObject o) => this.NeedsToRecreate(), null);
		}

		// Token: 0x0600B0FB RID: 45307 RVA: 0x0036A8DC File Offset: 0x00368ADC
		private bool NeedsToRecreate()
		{
			if (this.UseDataModel && this.m_lastDataVersion != base.GetLocalDataVersion())
			{
				this.UpdateTargetTextureSource();
			}
			return this.m_targetTextureSource.Type != this.m_currentTextureSource.Type || this.m_targetTextureSource.Id != this.m_currentTextureSource.Id;
		}

		// Token: 0x0600B0FC RID: 45308 RVA: 0x0036A93C File Offset: 0x00368B3C
		private Watermark.TextureSource GetTextureSourceFromDataModel()
		{
			if (this.UseDataModel)
			{
				IDataModel dataModel;
				if (base.GetDataModel(7, out dataModel))
				{
					this.m_dataModelIDs.Add(7);
					AdventureDataModel adventureDataModel = dataModel as AdventureDataModel;
					if (adventureDataModel.SelectedAdventure != AdventureDbId.INVALID)
					{
						Watermark.TextureSource result = new Watermark.TextureSource
						{
							Type = Watermark.TextureSource.SourceType.ADVENTURE_ID,
							Id = (int)adventureDataModel.SelectedAdventure
						};
						return result;
					}
				}
				if (base.GetDataModel(25, out dataModel))
				{
					this.m_dataModelIDs.Add(25);
					PackDataModel packDataModel = dataModel as PackDataModel;
					this.m_boosterDbId = packDataModel.Type;
					if (this.m_boosterDbId != BoosterDbId.INVALID)
					{
						Watermark.TextureSource result = new Watermark.TextureSource
						{
							Type = Watermark.TextureSource.SourceType.BOOSTER_ID,
							Id = (int)packDataModel.Type
						};
						return result;
					}
				}
				if (base.GetDataModel(15, out dataModel))
				{
					this.m_dataModelIDs.Add(15);
					foreach (RewardItemDataModel item in (dataModel as ProductDataModel).Items)
					{
						Watermark.TextureSource result2;
						if (this.GetTextureSourceFromRewardItemDataModel(item, out result2))
						{
							return result2;
						}
					}
				}
				if (base.GetDataModel(17, out dataModel))
				{
					this.m_dataModelIDs.Add(17);
					RewardItemDataModel item2 = dataModel as RewardItemDataModel;
					Watermark.TextureSource result3;
					if (this.GetTextureSourceFromRewardItemDataModel(item2, out result3))
					{
						return result3;
					}
				}
			}
			return default(Watermark.TextureSource);
		}

		// Token: 0x0600B0FD RID: 45309 RVA: 0x0036AA9C File Offset: 0x00368C9C
		private bool GetTextureSourceFromRewardItemDataModel(RewardItemDataModel item, out Watermark.TextureSource source)
		{
			RewardItemType itemType = item.ItemType;
			if (itemType == RewardItemType.BOOSTER)
			{
				source = new Watermark.TextureSource
				{
					Type = Watermark.TextureSource.SourceType.BOOSTER_ID,
					Id = item.ItemId
				};
				return true;
			}
			if (itemType == RewardItemType.ADVENTURE)
			{
				source = new Watermark.TextureSource
				{
					Type = Watermark.TextureSource.SourceType.ADVENTURE_ID,
					Id = item.ItemId
				};
				return true;
			}
			source = default(Watermark.TextureSource);
			return false;
		}

		// Token: 0x0600B0FE RID: 45310 RVA: 0x0036AB10 File Offset: 0x00368D10
		private void UpdateTargetTextureSource()
		{
			if (this.UseDataModel)
			{
				this.m_targetTextureSource = this.GetTextureSourceFromDataModel();
				this.m_lastDataVersion = base.GetLocalDataVersion();
				return;
			}
			if (this.m_adventureDbId != AdventureDbId.INVALID)
			{
				this.m_targetTextureSource.Type = Watermark.TextureSource.SourceType.ADVENTURE_ID;
				this.m_targetTextureSource.Id = (int)this.m_adventureDbId;
				return;
			}
			if (this.m_boosterDbId != BoosterDbId.INVALID)
			{
				this.m_targetTextureSource.Type = Watermark.TextureSource.SourceType.BOOSTER_ID;
				this.m_targetTextureSource.Id = (int)this.m_boosterDbId;
				return;
			}
			this.m_targetTextureSource.Type = Watermark.TextureSource.SourceType.NONE;
		}

		// Token: 0x0600B0FF RID: 45311 RVA: 0x0036AB98 File Offset: 0x00368D98
		private string GetTextureName(Watermark.TextureSource source)
		{
			Watermark.TextureSource.SourceType type = source.Type;
			if (type != Watermark.TextureSource.SourceType.ADVENTURE_ID)
			{
				if (type != Watermark.TextureSource.SourceType.BOOSTER_ID)
				{
					goto IL_B9;
				}
			}
			else
			{
				using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStoreAdventurePrefab((AdventureDbId)source.Id))
				{
					StoreAdventureDef storeAdventureDef = assetHandle ? assetHandle.Asset.GetComponent<StoreAdventureDef>() : null;
					return (storeAdventureDef != null) ? storeAdventureDef.GetAccentTextureName() : string.Empty;
				}
			}
			using (AssetHandle<GameObject> assetHandle2 = ShopUtils.LoadStorePackPrefab((BoosterDbId)source.Id))
			{
				StorePackDef storePackDef = assetHandle2 ? assetHandle2.Asset.GetComponent<StorePackDef>() : null;
				return (storePackDef != null) ? (this.m_useMiniSetWatermark ? storePackDef.GetMiniSetAccentTextureName() : storePackDef.GetAccentTextureName()) : string.Empty;
			}
			IL_B9:
			return string.Empty;
		}

		// Token: 0x04009583 RID: 38275
		[Tooltip("Base Material to texture with")]
		[SerializeField]
		private Material m_baseMaterial;

		// Token: 0x04009584 RID: 38276
		[Header("Mutually Exclusive")]
		[Tooltip("If true, it will use data model adventure/pack/product/rewardItem whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		// Token: 0x04009585 RID: 38277
		[Tooltip("Name of Adventure (AdventureDbId) to use the watermark from")]
		[SerializeField]
		private AdventureDbId m_adventureDbId;

		// Token: 0x04009586 RID: 38278
		[Tooltip("Name of Booster (BoosterDbId) to use the watermark from")]
		[SerializeField]
		private BoosterDbId m_boosterDbId;

		// Token: 0x04009587 RID: 38279
		[Tooltip("Whether or not to use the BoosterDbId's mini-set watermark in Booster mode")]
		[SerializeField]
		private bool m_useMiniSetWatermark;

		// Token: 0x04009588 RID: 38280
		private HashSet<int> m_dataModelIDs = new HashSet<int>();

		// Token: 0x04009589 RID: 38281
		private GameObject m_quad;

		// Token: 0x0400958A RID: 38282
		private Material m_material;

		// Token: 0x0400958B RID: 38283
		private AssetHandle<Texture> m_texture;

		// Token: 0x0400958C RID: 38284
		private Watermark.TextureSource m_targetTextureSource;

		// Token: 0x0400958D RID: 38285
		private Watermark.TextureSource m_currentTextureSource;

		// Token: 0x0400958E RID: 38286
		private int m_textureAsyncOperationId;

		// Token: 0x0400958F RID: 38287
		private int m_lastDataVersion;

		// Token: 0x02002819 RID: 10265
		private struct TextureSource
		{
			// Token: 0x0400F887 RID: 63623
			public Watermark.TextureSource.SourceType Type;

			// Token: 0x0400F888 RID: 63624
			public int Id;

			// Token: 0x020029A8 RID: 10664
			public enum SourceType
			{
				// Token: 0x0400FE0A RID: 65034
				NONE,
				// Token: 0x0400FE0B RID: 65035
				ADVENTURE_ID,
				// Token: 0x0400FE0C RID: 65036
				BOOSTER_ID
			}
		}

		// Token: 0x0200281A RID: 10266
		private struct CallbackData
		{
			// Token: 0x0400F889 RID: 63625
			public bool m_localized;

			// Token: 0x0400F88A RID: 63626
			public int m_asyncOperationId;
		}
	}
}
