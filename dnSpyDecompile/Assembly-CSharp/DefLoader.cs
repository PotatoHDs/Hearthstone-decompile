using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Hearthstone;
using UnityEngine;

// Token: 0x02000895 RID: 2197
public class DefLoader
{
	// Token: 0x06007895 RID: 30869 RVA: 0x00275AEC File Offset: 0x00273CEC
	public static DefLoader Get()
	{
		if (DefLoader.s_instance != null && DefLoader.s_instance.m_isPlaying != Application.isPlaying)
		{
			DefLoader.s_instance = null;
		}
		if (DefLoader.s_instance == null)
		{
			DefLoader.s_instance = new DefLoader();
			DefLoader.s_instance.m_isPlaying = Application.isPlaying;
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.WillReset += DefLoader.s_instance.WillReset;
			}
			else if (Application.isPlaying)
			{
				Log.All.PrintWarning("DefLoader being initialized before HearthstoneApplication is initialized! This is very bad if you're running the game!", Array.Empty<object>());
			}
		}
		return DefLoader.s_instance;
	}

	// Token: 0x06007896 RID: 30870 RVA: 0x00275B7E File Offset: 0x00273D7E
	public void Initialize()
	{
		this.LoadAllEntityDefs();
	}

	// Token: 0x06007897 RID: 30871 RVA: 0x00275B86 File Offset: 0x00273D86
	public void Clear()
	{
		this.ClearEntityDefs();
	}

	// Token: 0x06007898 RID: 30872 RVA: 0x00275B8E File Offset: 0x00273D8E
	public Map<string, EntityDef> GetAllEntityDefs()
	{
		return this.m_entityDefCache;
	}

	// Token: 0x06007899 RID: 30873 RVA: 0x00275B96 File Offset: 0x00273D96
	public void ClearEntityDefs()
	{
		this.m_entityDefCache.Clear();
		this.m_loadedEntityDefs = false;
	}

	// Token: 0x0600789A RID: 30874 RVA: 0x00275BAC File Offset: 0x00273DAC
	public EntityDef GetEntityDef(string cardId)
	{
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		EntityDef entityDef = null;
		this.m_entityDefCache.TryGetValue(cardId, out entityDef);
		if (entityDef == null)
		{
			if (HearthstoneApplication.UseDevWorkarounds())
			{
				Debug.LogErrorFormat("DefLoader.GetEntityDef() - Failed to load {0}. Loading {1} instead.", new object[]
				{
					cardId,
					"PlaceholderCard"
				});
				EntityDef entityDef2;
				this.m_entityDefCache.TryGetValue("PlaceholderCard", out entityDef2);
				if (entityDef2 == null)
				{
					Error.AddDevFatal("DefLoader.GetEntityDef() - Failed to load {0} in place of {1}", new object[]
					{
						"PlaceholderCard",
						cardId
					});
					return null;
				}
				entityDef = entityDef2.Clone();
				entityDef.SetCardId(cardId);
				this.m_entityDefCache[cardId] = entityDef;
			}
			else
			{
				Error.AddDevFatal("DefLoader.GetEntityDef() - Failed to load {0}", new object[]
				{
					cardId
				});
			}
		}
		return entityDef;
	}

	// Token: 0x0600789B RID: 30875 RVA: 0x00275C64 File Offset: 0x00273E64
	public EntityDef GetEntityDef(int dbId, bool displayError = true)
	{
		string text = GameUtils.TranslateDbIdToCardId(dbId, false);
		if (text == null && displayError)
		{
			Debug.LogErrorFormat("DefLoader.GetEntityDef() - dbId {0} does not map to a cardId", new object[]
			{
				dbId
			});
			return null;
		}
		return this.GetEntityDef(text);
	}

	// Token: 0x0600789C RID: 30876 RVA: 0x00275CA4 File Offset: 0x00273EA4
	public void LoadAllEntityDefs()
	{
		List<string> allCardIds = GameUtils.GetAllCardIds();
		if (!allCardIds.Contains("PlaceholderCard"))
		{
			allCardIds.Add("PlaceholderCard");
		}
		List<string> list;
		this.m_entityDefCache = EntityDef.LoadBatchCardEntityDefs(allCardIds, out list);
		this.m_loadedEntityDefs = true;
		if (list.Count > 0)
		{
			if (Application.isEditor)
			{
				Debug.LogWarningFormat("LoadAllEntityDefs: Missing Cards! Proceed with caution. - Failed to load {0} card(s) on startup - {1}", new object[]
				{
					list.Count,
					string.Join(", ", list.ToArray())
				});
				return;
			}
			Error.AddDevWarning("Missing Cards", "Failed to load {0} card(s) on startup!\n\n{1}", new object[]
			{
				list.Count,
				string.Join(", ", list.ToArray())
			});
		}
	}

	// Token: 0x0600789D RID: 30877 RVA: 0x00275D5C File Offset: 0x00273F5C
	public bool HasLoadedEntityDefs()
	{
		return this.m_loadedEntityDefs;
	}

	// Token: 0x0600789E RID: 30878 RVA: 0x00275D64 File Offset: 0x00273F64
	public void LoadCardDef(string cardId, DefLoader.LoadDefCallback<DefLoader.DisposableCardDef> callback, object userData = null, CardPortraitQuality quality = null)
	{
		DefLoader.DisposableCardDef cardDef = this.GetCardDef(cardId, quality);
		callback(cardId, cardDef, userData);
	}

	// Token: 0x0600789F RID: 30879 RVA: 0x00275D84 File Offset: 0x00273F84
	public DefLoader.DisposableCardDef GetCardDef(int dbId)
	{
		string text = GameUtils.TranslateDbIdToCardId(dbId, false);
		if (text == null)
		{
			Debug.LogError(string.Format("DefLoader.GetCardDef() - dbId {0} does not map to a cardId", dbId));
			return null;
		}
		return this.GetCardDef(text, null);
	}

	// Token: 0x060078A0 RID: 30880 RVA: 0x00275DBC File Offset: 0x00273FBC
	public DefLoader.DisposableCardDef GetCardDef(string cardId, CardPortraitQuality quality = null)
	{
		if (string.IsNullOrEmpty(cardId) || AssetLoader.Get() == null)
		{
			return null;
		}
		if (quality == null)
		{
			quality = CardPortraitQuality.GetDefault();
		}
		if (PlatformSettings.ShouldFallbackToLowRes && quality.TextureQuality > 1)
		{
			quality.TextureQuality = 1;
		}
		AssetReference cardDefAssetRefFromCardId = HearthstoneServices.Get<IAliasedAssetResolver>().GetCardDefAssetRefFromCardId(cardId);
		AssetHandle<GameObject> assetHandle = AssetLoader.Get().GetOrInstantiateSharedPrefab(cardDefAssetRefFromCardId, AssetLoadingOptions.None);
		CardDef cardDef = assetHandle ? assetHandle.Asset.GetComponent<CardDef>() : null;
		if (cardDef == null)
		{
			if (assetHandle != null)
			{
				assetHandle.Dispose();
			}
			if (!HearthstoneApplication.UseDevWorkarounds())
			{
				Error.AddDevFatal("DefLoader.GetCardDef() - Failed to load {0}", new object[]
				{
					cardId
				});
				return null;
			}
			Debug.LogErrorFormat("DefLoader.GetCardDef() - Failed to load {0}. Using {1} instead.", new object[]
			{
				cardId,
				"PlaceholderCard"
			});
			assetHandle = this.LoadPlaceholderCardPrefab();
			if (!assetHandle)
			{
				Error.AddDevFatal("DefLoader.GetCardDef() - Failed to load {0} in place of {1}", new object[]
				{
					"PlaceholderCard",
					cardId
				});
				return null;
			}
			cardDef = assetHandle.Asset.GetComponent<CardDef>();
		}
		if (CardPortraitQuality.GetFromDef(cardDef) < quality)
		{
			CardTextureLoader.Load(cardDef, quality, false);
		}
		return new DefLoader.DisposableCardDef(assetHandle);
	}

	// Token: 0x060078A1 RID: 30881 RVA: 0x00275ED0 File Offset: 0x002740D0
	private AssetHandle<GameObject> LoadPlaceholderCardPrefab()
	{
		AssetReference cardDefAssetRefFromCardId = HearthstoneServices.Get<IAliasedAssetResolver>().GetCardDefAssetRefFromCardId("PlaceholderCard");
		AssetHandle<GameObject> orInstantiateSharedPrefab = AssetLoader.Get().GetOrInstantiateSharedPrefab(cardDefAssetRefFromCardId, AssetLoadingOptions.None);
		if (!orInstantiateSharedPrefab)
		{
			Debug.LogErrorFormat("DefLoader.LoadPlaceholderCardPrefab() - Failed to load {0}", new object[]
			{
				"PlaceholderCard"
			});
			return null;
		}
		return orInstantiateSharedPrefab;
	}

	// Token: 0x060078A2 RID: 30882 RVA: 0x00275B86 File Offset: 0x00273D86
	private void WillReset()
	{
		this.ClearEntityDefs();
	}

	// Token: 0x060078A3 RID: 30883 RVA: 0x00275F20 File Offset: 0x00274120
	public DefLoader.DisposableFullDef GetFullDef(int dbId)
	{
		string text = GameUtils.TranslateDbIdToCardId(dbId, false);
		if (text == null)
		{
			Debug.LogError(string.Format("DefLoader.GetCardDef() - dbId {0} does not map to a cardId", dbId));
			return null;
		}
		return this.GetFullDef(text, null);
	}

	// Token: 0x060078A4 RID: 30884 RVA: 0x00275F58 File Offset: 0x00274158
	public DefLoader.DisposableFullDef GetFullDef(string cardId, CardPortraitQuality quality = null)
	{
		EntityDef entityDef = this.GetEntityDef(cardId);
		DefLoader.DisposableCardDef cardDef = this.GetCardDef(cardId, quality);
		return new DefLoader.DisposableFullDef(entityDef, cardDef);
	}

	// Token: 0x060078A5 RID: 30885 RVA: 0x00275F7B File Offset: 0x0027417B
	public void LoadFullDef(string cardId, DefLoader.LoadDefCallback<DefLoader.DisposableFullDef> callback, object userData = null, CardPortraitQuality quality = null)
	{
		callback(cardId, this.GetFullDef(cardId, quality), userData);
	}

	// Token: 0x04005E15 RID: 24085
	private static DefLoader s_instance;

	// Token: 0x04005E16 RID: 24086
	private bool m_loadedEntityDefs;

	// Token: 0x04005E17 RID: 24087
	private Map<string, EntityDef> m_entityDefCache = new Map<string, EntityDef>();

	// Token: 0x04005E18 RID: 24088
	private bool m_isPlaying;

	// Token: 0x020024FB RID: 9467
	public class DisposableCardDef : IDisposable
	{
		// Token: 0x17002B63 RID: 11107
		// (get) Token: 0x0601318B RID: 78219 RVA: 0x0052967D File Offset: 0x0052787D
		// (set) Token: 0x0601318C RID: 78220 RVA: 0x00529685 File Offset: 0x00527885
		public CardDef CardDef { get; private set; }

		// Token: 0x0601318D RID: 78221 RVA: 0x0052968E File Offset: 0x0052788E
		public DisposableCardDef(AssetHandle<GameObject> cardPrefabInstance)
		{
			this.m_cardPrefabInstance = cardPrefabInstance;
			this.CardDef = (this.m_cardPrefabInstance ? this.m_cardPrefabInstance.Asset.GetComponent<CardDef>() : null);
		}

		// Token: 0x0601318E RID: 78222 RVA: 0x005296C3 File Offset: 0x005278C3
		public void Dispose()
		{
			AssetHandle<GameObject> cardPrefabInstance = this.m_cardPrefabInstance;
			if (cardPrefabInstance == null)
			{
				return;
			}
			cardPrefabInstance.Dispose();
		}

		// Token: 0x0601318F RID: 78223 RVA: 0x005296D5 File Offset: 0x005278D5
		public DefLoader.DisposableCardDef Share()
		{
			return new DefLoader.DisposableCardDef(this.m_cardPrefabInstance.Share());
		}

		// Token: 0x0400EC3C RID: 60476
		private AssetHandle<GameObject> m_cardPrefabInstance;
	}

	// Token: 0x020024FC RID: 9468
	public class DisposableFullDef : IDisposable
	{
		// Token: 0x17002B64 RID: 11108
		// (get) Token: 0x06013190 RID: 78224 RVA: 0x005296E7 File Offset: 0x005278E7
		public CardDef CardDef
		{
			get
			{
				DefLoader.DisposableCardDef disposableCardDef = this.DisposableCardDef;
				if (disposableCardDef == null)
				{
					return null;
				}
				return disposableCardDef.CardDef;
			}
		}

		// Token: 0x17002B65 RID: 11109
		// (get) Token: 0x06013191 RID: 78225 RVA: 0x005296FA File Offset: 0x005278FA
		public DefLoader.DisposableCardDef DisposableCardDef { get; }

		// Token: 0x17002B66 RID: 11110
		// (get) Token: 0x06013192 RID: 78226 RVA: 0x00529702 File Offset: 0x00527902
		// (set) Token: 0x06013193 RID: 78227 RVA: 0x0052970A File Offset: 0x0052790A
		public EntityDef EntityDef { get; private set; }

		// Token: 0x06013194 RID: 78228 RVA: 0x00529713 File Offset: 0x00527913
		public DisposableFullDef(EntityDef entityDef, DefLoader.DisposableCardDef cardDef)
		{
			this.EntityDef = entityDef;
			this.DisposableCardDef = cardDef;
		}

		// Token: 0x06013195 RID: 78229 RVA: 0x00529729 File Offset: 0x00527929
		public void Dispose()
		{
			DefLoader.DisposableCardDef disposableCardDef = this.DisposableCardDef;
			if (disposableCardDef == null)
			{
				return;
			}
			disposableCardDef.Dispose();
		}

		// Token: 0x06013196 RID: 78230 RVA: 0x0052973B File Offset: 0x0052793B
		public DefLoader.DisposableFullDef Share()
		{
			EntityDef entityDef = this.EntityDef;
			DefLoader.DisposableCardDef disposableCardDef = this.DisposableCardDef;
			return new DefLoader.DisposableFullDef(entityDef, (disposableCardDef != null) ? disposableCardDef.Share() : null);
		}
	}

	// Token: 0x020024FD RID: 9469
	// (Invoke) Token: 0x06013198 RID: 78232
	public delegate void LoadDefCallback<T>(string cardId, T def, object userData);
}
