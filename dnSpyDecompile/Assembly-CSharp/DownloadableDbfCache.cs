using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;

// Token: 0x0200089B RID: 2203
public class DownloadableDbfCache : IService
{
	// Token: 0x06007930 RID: 31024 RVA: 0x00278249 File Offset: 0x00276449
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<Network>().RegisterNetHandler(GetAssetResponse.PacketID.ID, new Network.NetHandler(this.Network_OnGetAssetResponse), null);
		serviceLocator.Get<NetCache>().RegisterUpdatedListener(typeof(ClientStaticAssetsResponse), new Action(this.NetCache_OnClientStaticAssetsResponse));
		yield break;
	}

	// Token: 0x06007931 RID: 31025 RVA: 0x00245221 File Offset: 0x00243421
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(NetCache)
		};
	}

	// Token: 0x06007932 RID: 31026 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06007933 RID: 31027 RVA: 0x0027825F File Offset: 0x0027645F
	public static DownloadableDbfCache Get()
	{
		return HearthstoneServices.Get<DownloadableDbfCache>();
	}

	// Token: 0x06007934 RID: 31028 RVA: 0x00278268 File Offset: 0x00276468
	public bool IsAssetRequestInProgress(int assetId, AssetType assetType)
	{
		return this.m_assetRequests.Any((KeyValuePair<int, KeyValuePair<AssetRecordInfo, DownloadableDbfCache.LoadCachedAssetCallback>> kv) => kv.Value.Key.Asset.AssetId == assetId && kv.Value.Key.Asset.Type == assetType);
	}

	// Token: 0x06007935 RID: 31029 RVA: 0x002782A8 File Offset: 0x002764A8
	public bool IsAssetRequestInProgress(AssetKey assetKey)
	{
		return this.m_assetRequests.Any((KeyValuePair<int, KeyValuePair<AssetRecordInfo, DownloadableDbfCache.LoadCachedAssetCallback>> kv) => kv.Value.Key.Asset.AssetId == assetKey.AssetId && kv.Value.Key.Asset.Type == assetKey.Type);
	}

	// Token: 0x170006FF RID: 1791
	// (get) Token: 0x06007936 RID: 31030 RVA: 0x002782DE File Offset: 0x002764DE
	public bool IsRequiredClientStaticAssetsStillPending
	{
		get
		{
			return NetCache.Get().GetNetObject<ClientStaticAssetsResponse>() == null || this.m_requiredClientStaticAssetsStillPending.Count > 0;
		}
	}

	// Token: 0x06007937 RID: 31031 RVA: 0x002782FC File Offset: 0x002764FC
	public bool LoadCachedAssets(bool canRequestFromServer, DownloadableDbfCache.LoadCachedAssetCallback cb, params AssetRecordInfo[] assets)
	{
		if (assets.Length == 0)
		{
			return false;
		}
		List<AssetKey> list = new List<AssetKey>();
		byte[] array = null;
		foreach (AssetRecordInfo assetRecordInfo in assets)
		{
			if (assetRecordInfo != null)
			{
				if (assetRecordInfo.RecordHash != null)
				{
					bool flag = false;
					string cachedAssetFilePath = DownloadableDbfCache.GetCachedAssetFilePath(assetRecordInfo.Asset.Type, assetRecordInfo.Asset.AssetId, assetRecordInfo.RecordHash);
					if (!File.Exists(cachedAssetFilePath))
					{
						flag = (assetRecordInfo.RecordByteSize > 0U);
						if (!flag)
						{
							this.m_requiredClientStaticAssetsStillPending.Remove(assetRecordInfo.Asset);
						}
						try
						{
							Directory.CreateDirectory(DownloadableDbfCache.GetCachedAssetFolder(assetRecordInfo.Asset.Type));
							goto IL_1FC;
						}
						catch (Exception ex)
						{
							Error.AddDevFatal("Error creating cached asset folder {0}:\n{1}", new object[]
							{
								cachedAssetFilePath,
								ex.ToString()
							});
							return false;
						}
						goto IL_E7;
					}
					goto IL_E7;
					IL_1FC:
					if (!flag)
					{
						goto IL_2CD;
					}
					list.Add(assetRecordInfo.Asset);
					if (canRequestFromServer)
					{
						Log.Downloader.Print("LoadCachedAsset: locally available=false, requesting from server {0} id={1} hash={2}", new object[]
						{
							assetRecordInfo.Asset.Type,
							assetRecordInfo.Asset.AssetId,
							(assetRecordInfo.RecordHash == null) ? "<null>" : assetRecordInfo.RecordHash.ToHexString()
						});
						goto IL_2CD;
					}
					Log.Downloader.Print("LoadCachedAsset: locally available=false, not requesting from server yet - {0} id={1} hash={2}", new object[]
					{
						assetRecordInfo.Asset.Type,
						assetRecordInfo.Asset.AssetId,
						(assetRecordInfo.RecordHash == null) ? "<null>" : assetRecordInfo.RecordHash.ToHexString()
					});
					goto IL_2CD;
					IL_E7:
					try
					{
						if (new FileInfo(cachedAssetFilePath).Length != (long)((ulong)assetRecordInfo.RecordByteSize))
						{
							flag = true;
						}
						else if (assetRecordInfo.RecordByteSize != 0U)
						{
							byte[] array2 = File.ReadAllBytes(cachedAssetFilePath);
							if (GeneralUtils.AreArraysEqual<byte>(SHA1.Create().ComputeHash(array2, 0, array2.Length), assetRecordInfo.RecordHash))
							{
								Log.Downloader.Print("LoadCachedAsset: locally available=true {0} id={1} hash={2}", new object[]
								{
									assetRecordInfo.Asset.Type,
									assetRecordInfo.Asset.AssetId,
									(assetRecordInfo.RecordHash == null) ? "<null>" : assetRecordInfo.RecordHash.ToHexString()
								});
								if (array == null)
								{
									array = array2;
								}
								DownloadableDbfCache.SetCachedAssetIntoDbfSystem(assetRecordInfo.Asset.Type, array2);
								this.m_requiredClientStaticAssetsStillPending.Remove(assetRecordInfo.Asset);
							}
							else
							{
								flag = true;
							}
						}
					}
					catch (Exception ex2)
					{
						Error.AddDevFatal("Error reading cached asset folder {0}:\n{1}", new object[]
						{
							cachedAssetFilePath,
							ex2.ToString()
						});
						list.Add(assetRecordInfo.Asset);
					}
					goto IL_1FC;
				}
				if (assetRecordInfo.RecordByteSize == 0U)
				{
					list.Add(assetRecordInfo.Asset);
				}
			}
			IL_2CD:;
		}
		AssetRecordInfo assetRecordInfo2 = assets[0];
		if (list.Count > 0)
		{
			if (canRequestFromServer)
			{
				int nextCallbackToken = this.NextCallbackToken;
				if (cb != null)
				{
					this.m_assetRequests[nextCallbackToken] = new KeyValuePair<AssetRecordInfo, DownloadableDbfCache.LoadCachedAssetCallback>(assetRecordInfo2, cb);
				}
				Network.Get().SendAssetRequest(nextCallbackToken, list);
			}
		}
		else if (assetRecordInfo2 != null && cb != null)
		{
			if (array == null)
			{
				array = new byte[0];
			}
			cb(assetRecordInfo2.Asset, ErrorCode.ERROR_OK, array);
		}
		return list.Count == 0;
	}

	// Token: 0x06007938 RID: 31032 RVA: 0x0027866C File Offset: 0x0027686C
	private static string GetCachedAssetFolder(AssetType assetType)
	{
		string arg;
		switch (assetType)
		{
		case AssetType.ASSET_TYPE_SCENARIO:
			arg = "Scenario";
			break;
		case AssetType.ASSET_TYPE_SUBSET_CARD:
			arg = "Subset";
			break;
		case AssetType.ASSET_TYPE_DECK_RULESET:
			arg = "DeckRuleset";
			break;
		default:
			arg = "Other";
			break;
		}
		string cachePath = FileUtils.CachePath;
		return string.Format("{0}/{1}", cachePath, arg);
	}

	// Token: 0x06007939 RID: 31033 RVA: 0x002786C0 File Offset: 0x002768C0
	private static string GetCachedAssetFileExtension(AssetType assetType)
	{
		switch (assetType)
		{
		case AssetType.ASSET_TYPE_SCENARIO:
			return "scen";
		case AssetType.ASSET_TYPE_SUBSET_CARD:
			return "subset_card";
		case AssetType.ASSET_TYPE_DECK_RULESET:
			return "deck_ruleset";
		default:
			return assetType.ToString().Replace("ASSET_TYPE_", "").ToLower();
		}
	}

	// Token: 0x0600793A RID: 31034 RVA: 0x00278718 File Offset: 0x00276918
	private static string GetCachedAssetFilePath(AssetType assetType, int assetId, byte[] assetHash)
	{
		string cachedAssetFolder = DownloadableDbfCache.GetCachedAssetFolder(assetType);
		string cachedAssetFileExtension = DownloadableDbfCache.GetCachedAssetFileExtension(assetType);
		return string.Format("{0}/{1}_{2}.{3}", new object[]
		{
			cachedAssetFolder,
			assetId,
			assetHash.ToHexString(),
			cachedAssetFileExtension
		});
	}

	// Token: 0x0600793B RID: 31035 RVA: 0x00278760 File Offset: 0x00276960
	private static void StoreReceivedAssetIntoLocalCache(AssetType assetType, int assetId, byte[] assetBytes, int assetBytesLength)
	{
		byte[] assetHash = SHA1.Create().ComputeHash(assetBytes, 0, assetBytesLength);
		string cachedAssetFilePath = DownloadableDbfCache.GetCachedAssetFilePath(assetType, assetId, assetHash);
		try
		{
			if (!File.Exists(cachedAssetFilePath))
			{
				File.Create(cachedAssetFilePath).Dispose();
			}
			using (FileStream fileStream = new FileStream(cachedAssetFilePath, FileMode.Truncate))
			{
				fileStream.Write(assetBytes, 0, assetBytesLength);
			}
		}
		catch (Exception ex)
		{
			Error.AddDevFatal("Error saving cached asset {0}:\n{1}", new object[]
			{
				cachedAssetFilePath,
				ex.ToString()
			});
		}
	}

	// Token: 0x0600793C RID: 31036 RVA: 0x002787F4 File Offset: 0x002769F4
	private static void SetCachedAssetIntoDbfSystem(AssetType assetType, byte[] assetBytes)
	{
		switch (assetType)
		{
		case AssetType.ASSET_TYPE_SCENARIO:
			DownloadableDbfCache.SetCachedAssetIntoDbfSystem_Scenario(ProtobufUtil.ParseFrom<ScenarioDbRecord>(assetBytes, 0, assetBytes.Length));
			return;
		case AssetType.ASSET_TYPE_SUBSET_CARD:
			DownloadableDbfCache.SetCachedAssetIntoDbfSystem_SubsetCard(ProtobufUtil.ParseFrom<SubsetCardListDbRecord>(assetBytes, 0, assetBytes.Length));
			return;
		case AssetType.ASSET_TYPE_DECK_RULESET:
			DownloadableDbfCache.SetCachedAssetIntoDbfSystem_DeckRuleset(ProtobufUtil.ParseFrom<DeckRulesetDbRecord>(assetBytes, 0, assetBytes.Length));
			return;
		case AssetType.ASSET_TYPE_REWARD_CHEST:
			DownloadableDbfCache.SetCachedAssetIntoDbfSystem_RewardChest(ProtobufUtil.ParseFrom<RewardChestDbRecord>(assetBytes, 0, assetBytes.Length));
			return;
		case AssetType.ASSET_TYPE_GUEST_HEROES:
			DownloadableDbfCache.SetCachedAssetIntoDbfSystem_GuestHero(ProtobufUtil.ParseFrom<GuestHeroDbRecord>(assetBytes, 0, assetBytes.Length));
			return;
		case AssetType.ASSET_TYPE_DECK_TEMPLATE:
			DownloadableDbfCache.SetCachedAssetIntoDbfSystem_DeckTemplate(ProtobufUtil.ParseFrom<DeckTemplateDbRecord>(assetBytes, 0, assetBytes.Length));
			return;
		default:
			return;
		}
	}

	// Token: 0x0600793D RID: 31037 RVA: 0x00278884 File Offset: 0x00276A84
	private static void SetCachedAssetIntoDbfSystem_Scenario(ScenarioDbRecord protoScenario)
	{
		List<ScenarioGuestHeroesDbfRecord> list;
		List<ClassExclusionsDbfRecord> list2;
		ScenarioDbfRecord dbf = DbfUtils.ConvertFromProtobuf(protoScenario, out list, out list2);
		if (dbf == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(protoScenario) returned null:\n{0}", new object[]
			{
				(protoScenario == null) ? "(null)" : protoScenario.ToString()
			});
			return;
		}
		GameDbf.Scenario.ReplaceRecordByRecordId(dbf);
		GameDbf.ScenarioGuestHeroes.RemoveRecordsWhere((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == dbf.ID);
		foreach (ScenarioGuestHeroesDbfRecord record in list)
		{
			GameDbf.ScenarioGuestHeroes.AddRecord(record);
		}
		GameDbf.ClassExclusions.RemoveRecordsWhere((ClassExclusionsDbfRecord r) => r.ScenarioId == dbf.ID);
		foreach (ClassExclusionsDbfRecord record2 in list2)
		{
			GameDbf.ClassExclusions.AddRecord(record2);
		}
	}

	// Token: 0x0600793E RID: 31038 RVA: 0x002789A0 File Offset: 0x00276BA0
	private static void SetCachedAssetIntoDbfSystem_DeckTemplate(DeckTemplateDbRecord protoDeckTemplate)
	{
		DeckDbfRecord record;
		List<DeckCardDbfRecord> list;
		DeckTemplateDbfRecord dbf = DbfUtils.ConvertFromProtobuf(protoDeckTemplate, out record, out list);
		if (dbf == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(protoSDeckTemplate) returned null:\n{0}", new object[]
			{
				(protoDeckTemplate == null) ? "(null)" : protoDeckTemplate.ToString()
			});
			return;
		}
		GameDbf.DeckTemplate.ReplaceRecordByRecordId(dbf);
		GameDbf.Deck.ReplaceRecordByRecordId(record);
		GameDbf.DeckCard.RemoveRecordsWhere((DeckCardDbfRecord r) => r.DeckId == dbf.DeckId);
		foreach (DeckCardDbfRecord record2 in list)
		{
			GameDbf.DeckCard.AddRecord(record2);
		}
	}

	// Token: 0x0600793F RID: 31039 RVA: 0x00278A6C File Offset: 0x00276C6C
	private static void SetCachedAssetIntoDbfSystem_DeckRuleset(DeckRulesetDbRecord proto)
	{
		DeckRulesetDbfRecord deckRulesetDbfRecord = DbfUtils.ConvertFromProtobuf(proto);
		if (deckRulesetDbfRecord == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(proto) returned null:\n{0}", new object[]
			{
				(proto == null) ? "(null)" : proto.ToString()
			});
		}
		else
		{
			GameDbf.DeckRuleset.ReplaceRecordByRecordId(deckRulesetDbfRecord);
		}
		foreach (DeckRulesetRuleDbRecord proto2 in proto.Rules)
		{
			List<int> list;
			DeckRulesetRuleDbfRecord dbfRule = DbfUtils.ConvertFromProtobuf(proto2, out list);
			GameDbf.DeckRulesetRule.ReplaceRecordByRecordId(dbfRule);
			GameDbf.DeckRulesetRuleSubset.RemoveRecordsWhere((DeckRulesetRuleSubsetDbfRecord r) => r.DeckRulesetRuleId == dbfRule.ID);
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					DeckRulesetRuleSubsetDbfRecord deckRulesetRuleSubsetDbfRecord = new DeckRulesetRuleSubsetDbfRecord();
					deckRulesetRuleSubsetDbfRecord.SetDeckRulesetRuleId(dbfRule.ID);
					deckRulesetRuleSubsetDbfRecord.SetSubsetId(list[i]);
					GameDbf.DeckRulesetRuleSubset.AddRecord(deckRulesetRuleSubsetDbfRecord);
				}
			}
		}
	}

	// Token: 0x06007940 RID: 31040 RVA: 0x00278B88 File Offset: 0x00276D88
	private static void SetCachedAssetIntoDbfSystem_SubsetCard(SubsetCardListDbRecord proto)
	{
		SubsetDbfRecord dbf = GameDbf.Subset.GetRecord(proto.SubsetId);
		if (dbf == null)
		{
			dbf = new SubsetDbfRecord();
			dbf.SetID(proto.SubsetId);
			GameDbf.Subset.AddRecord(dbf);
		}
		GameDbf.SubsetCard.RemoveRecordsWhere((SubsetCardDbfRecord r) => r.SubsetId == dbf.ID);
		foreach (int cardId in proto.CardIds)
		{
			SubsetCardDbfRecord subsetCardDbfRecord = new SubsetCardDbfRecord();
			subsetCardDbfRecord.SetSubsetId(dbf.ID);
			subsetCardDbfRecord.SetCardId(cardId);
			GameDbf.SubsetCard.AddRecord(subsetCardDbfRecord);
		}
	}

	// Token: 0x06007941 RID: 31041 RVA: 0x00278C64 File Offset: 0x00276E64
	private static void SetCachedAssetIntoDbfSystem_RewardChest(RewardChestDbRecord proto)
	{
		RewardChestDbfRecord rewardChestDbfRecord = DbfUtils.ConvertFromProtobuf(proto);
		if (rewardChestDbfRecord == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(RewardChestDbRecord) returned null:\n{0}", new object[]
			{
				(proto == null) ? "(null)" : proto.ToString()
			});
			return;
		}
		GameDbf.RewardChest.ReplaceRecordByRecordId(rewardChestDbfRecord);
	}

	// Token: 0x06007942 RID: 31042 RVA: 0x00278CB0 File Offset: 0x00276EB0
	private static void SetCachedAssetIntoDbfSystem_GuestHero(GuestHeroDbRecord proto)
	{
		GuestHeroDbfRecord guestHeroDbfRecord = DbfUtils.ConvertFromProtobuf(proto);
		if (guestHeroDbfRecord == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(GuestHeroDbfRecord) returned null:\n{0}", new object[]
			{
				(proto == null) ? "(null)" : proto.ToString()
			});
			return;
		}
		GameDbf.GuestHero.ReplaceRecordByRecordId(guestHeroDbfRecord);
	}

	// Token: 0x06007943 RID: 31043 RVA: 0x00278CFC File Offset: 0x00276EFC
	private void NetCache_OnClientStaticAssetsResponse()
	{
		ClientStaticAssetsResponse netObject = NetCache.Get().GetNetObject<ClientStaticAssetsResponse>();
		if (netObject == null)
		{
			return;
		}
		foreach (AssetRecordInfo assetRecordInfo in netObject.AssetsToGet)
		{
			this.m_requiredClientStaticAssetsStillPending.Add(assetRecordInfo.Asset);
		}
		this.LoadCachedAssets(true, null, netObject.AssetsToGet.ToArray());
	}

	// Token: 0x06007944 RID: 31044 RVA: 0x00278D80 File Offset: 0x00276F80
	private void Network_OnGetAssetResponse()
	{
		GetAssetResponse assetResponse = Network.Get().GetAssetResponse();
		if (assetResponse == null)
		{
			return;
		}
		ErrorCode errorCode = ErrorCode.ERROR_OK;
		Map<AssetKey, byte[]> map = new Map<AssetKey, byte[]>();
		for (int i = 0; i < assetResponse.Responses.Count; i++)
		{
			AssetResponse assetResponse2 = assetResponse.Responses[i];
			if (assetResponse2.ErrorCode == ErrorCode.ERROR_OK)
			{
				this.m_requiredClientStaticAssetsStillPending.Remove(assetResponse2.RequestedKey);
			}
			else
			{
				Log.Downloader.Print("Network_OnGetAssetResponse: error={0}:{1} type={2}:{3} id={4}", new object[]
				{
					(int)assetResponse2.ErrorCode,
					assetResponse2.ErrorCode.ToString(),
					(int)assetResponse2.RequestedKey.Type,
					assetResponse2.RequestedKey.Type.ToString(),
					assetResponse2.RequestedKey.AssetId
				});
				if (errorCode == ErrorCode.ERROR_OK)
				{
					errorCode = assetResponse2.ErrorCode;
				}
				if (this.m_requiredClientStaticAssetsStillPending.Contains(assetResponse2.RequestedKey))
				{
					Error.AddDevFatal(GameStrings.Get("GLUE_REQUIRED_CLIENT_STATIC_ASSETS_ERROR_MESSAGE"), Array.Empty<object>());
					return;
				}
			}
			AssetKey requestedKey = assetResponse2.RequestedKey;
			byte[] array = null;
			if (assetResponse2.HasScenarioAsset)
			{
				array = ProtobufUtil.ToByteArray(assetResponse2.ScenarioAsset);
			}
			if (assetResponse2.HasSubsetCardListAsset)
			{
				array = ProtobufUtil.ToByteArray(assetResponse2.SubsetCardListAsset);
			}
			if (assetResponse2.HasDeckRulesetAsset)
			{
				array = ProtobufUtil.ToByteArray(assetResponse2.DeckRulesetAsset);
			}
			if (assetResponse2.HasRewardChestAsset)
			{
				array = ProtobufUtil.ToByteArray(assetResponse2.RewardChestAsset);
			}
			if (assetResponse2.HasGuestHeroAsset)
			{
				array = ProtobufUtil.ToByteArray(assetResponse2.GuestHeroAsset);
			}
			if (assetResponse2.HasDeckTemplateAsset)
			{
				array = ProtobufUtil.ToByteArray(assetResponse2.DeckTemplateAsset);
			}
			if (array != null)
			{
				map[requestedKey] = array;
				DownloadableDbfCache.StoreReceivedAssetIntoLocalCache(requestedKey.Type, requestedKey.AssetId, array, array.Length);
				DownloadableDbfCache.SetCachedAssetIntoDbfSystem(requestedKey.Type, array);
			}
		}
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(DownloadableDbfCache.PruneCachedAssetFiles), null);
		Processor.ScheduleCallback(5f, true, new Processor.ScheduledCallback(DownloadableDbfCache.PruneCachedAssetFiles), null);
		KeyValuePair<AssetRecordInfo, DownloadableDbfCache.LoadCachedAssetCallback> keyValuePair;
		if (this.m_assetRequests.TryGetValue(assetResponse.ClientToken, out keyValuePair))
		{
			AssetRecordInfo key = keyValuePair.Key;
			DownloadableDbfCache.LoadCachedAssetCallback value = keyValuePair.Value;
			this.m_assetRequests.Remove(assetResponse.ClientToken);
			byte[] assetBytes;
			if (!map.TryGetValue(key.Asset, out assetBytes))
			{
				if (this.LoadCachedAssets(false, value, new AssetRecordInfo[]
				{
					key
				}))
				{
					return;
				}
				assetBytes = new byte[0];
			}
			value(key.Asset, errorCode, assetBytes);
		}
	}

	// Token: 0x06007945 RID: 31045 RVA: 0x0027901C File Offset: 0x0027721C
	private static void PruneCachedAssetFiles(object userData)
	{
		string cachePath = FileUtils.CachePath;
		string message = null;
		string text = null;
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(cachePath);
			if (directoryInfo.Exists)
			{
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					message = directoryInfo2.FullName;
					foreach (FileInfo fileInfo in directoryInfo2.GetFiles())
					{
						text = fileInfo.Name;
						TimeSpan timeSpan = DateTime.Now - fileInfo.LastWriteTime;
						if (fileInfo.LastWriteTime < DateTime.Now && timeSpan.TotalDays > 124.0)
						{
							fileInfo.Delete();
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Error.AddDevWarning("Error pruning dir={0} file={1}:\n{2}", message, new object[]
			{
				text,
				ex.ToString()
			});
		}
	}

	// Token: 0x17000700 RID: 1792
	// (get) Token: 0x06007946 RID: 31046 RVA: 0x00279110 File Offset: 0x00277310
	private int NextCallbackToken
	{
		get
		{
			int num = this.m_nextCallbackToken + 1;
			this.m_nextCallbackToken = num;
			return num;
		}
	}

	// Token: 0x04005E3B RID: 24123
	private const int PRUNE_CACHED_ASSETS_MAX_AGE_DAYS = 124;

	// Token: 0x04005E3C RID: 24124
	private Map<int, KeyValuePair<AssetRecordInfo, DownloadableDbfCache.LoadCachedAssetCallback>> m_assetRequests = new Map<int, KeyValuePair<AssetRecordInfo, DownloadableDbfCache.LoadCachedAssetCallback>>();

	// Token: 0x04005E3D RID: 24125
	private HashSet<AssetKey> m_requiredClientStaticAssetsStillPending = new HashSet<AssetKey>();

	// Token: 0x04005E3E RID: 24126
	private int m_nextCallbackToken = -1;

	// Token: 0x02002510 RID: 9488
	// (Invoke) Token: 0x060131DE RID: 78302
	public delegate void LoadCachedAssetCallback(AssetKey requestedKey, ErrorCode code, byte[] assetBytes);
}
