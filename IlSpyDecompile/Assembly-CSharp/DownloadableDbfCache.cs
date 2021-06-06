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

public class DownloadableDbfCache : IService
{
	public delegate void LoadCachedAssetCallback(AssetKey requestedKey, ErrorCode code, byte[] assetBytes);

	private const int PRUNE_CACHED_ASSETS_MAX_AGE_DAYS = 124;

	private Map<int, KeyValuePair<AssetRecordInfo, LoadCachedAssetCallback>> m_assetRequests = new Map<int, KeyValuePair<AssetRecordInfo, LoadCachedAssetCallback>>();

	private HashSet<AssetKey> m_requiredClientStaticAssetsStillPending = new HashSet<AssetKey>();

	private int m_nextCallbackToken = -1;

	public bool IsRequiredClientStaticAssetsStillPending
	{
		get
		{
			if (NetCache.Get().GetNetObject<ClientStaticAssetsResponse>() == null)
			{
				return true;
			}
			return m_requiredClientStaticAssetsStillPending.Count > 0;
		}
	}

	private int NextCallbackToken => ++m_nextCallbackToken;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<Network>().RegisterNetHandler(GetAssetResponse.PacketID.ID, Network_OnGetAssetResponse);
		serviceLocator.Get<NetCache>().RegisterUpdatedListener(typeof(ClientStaticAssetsResponse), NetCache_OnClientStaticAssetsResponse);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(Network),
			typeof(NetCache)
		};
	}

	public void Shutdown()
	{
	}

	public static DownloadableDbfCache Get()
	{
		return HearthstoneServices.Get<DownloadableDbfCache>();
	}

	public bool IsAssetRequestInProgress(int assetId, AssetType assetType)
	{
		if (m_assetRequests.Any((KeyValuePair<int, KeyValuePair<AssetRecordInfo, LoadCachedAssetCallback>> kv) => kv.Value.Key.Asset.AssetId == assetId && kv.Value.Key.Asset.Type == assetType))
		{
			return true;
		}
		return false;
	}

	public bool IsAssetRequestInProgress(AssetKey assetKey)
	{
		if (m_assetRequests.Any((KeyValuePair<int, KeyValuePair<AssetRecordInfo, LoadCachedAssetCallback>> kv) => kv.Value.Key.Asset.AssetId == assetKey.AssetId && kv.Value.Key.Asset.Type == assetKey.Type))
		{
			return true;
		}
		return false;
	}

	public bool LoadCachedAssets(bool canRequestFromServer, LoadCachedAssetCallback cb, params AssetRecordInfo[] assets)
	{
		if (assets.Length == 0)
		{
			return false;
		}
		List<AssetKey> list = new List<AssetKey>();
		byte[] array = null;
		foreach (AssetRecordInfo assetRecordInfo in assets)
		{
			if (assetRecordInfo == null)
			{
				continue;
			}
			if (assetRecordInfo.RecordHash == null)
			{
				if (assetRecordInfo.RecordByteSize == 0)
				{
					list.Add(assetRecordInfo.Asset);
				}
				continue;
			}
			bool flag = false;
			string cachedAssetFilePath = GetCachedAssetFilePath(assetRecordInfo.Asset.Type, assetRecordInfo.Asset.AssetId, assetRecordInfo.RecordHash);
			if (!File.Exists(cachedAssetFilePath))
			{
				flag = assetRecordInfo.RecordByteSize != 0;
				if (!flag)
				{
					m_requiredClientStaticAssetsStillPending.Remove(assetRecordInfo.Asset);
				}
				try
				{
					Directory.CreateDirectory(GetCachedAssetFolder(assetRecordInfo.Asset.Type));
				}
				catch (Exception ex)
				{
					Error.AddDevFatal("Error creating cached asset folder {0}:\n{1}", cachedAssetFilePath, ex.ToString());
					return false;
				}
			}
			else
			{
				try
				{
					if (new FileInfo(cachedAssetFilePath).Length != assetRecordInfo.RecordByteSize)
					{
						flag = true;
					}
					else if (assetRecordInfo.RecordByteSize != 0)
					{
						byte[] array2 = File.ReadAllBytes(cachedAssetFilePath);
						if (GeneralUtils.AreArraysEqual(SHA1.Create().ComputeHash(array2, 0, array2.Length), assetRecordInfo.RecordHash))
						{
							Log.Downloader.Print("LoadCachedAsset: locally available=true {0} id={1} hash={2}", assetRecordInfo.Asset.Type, assetRecordInfo.Asset.AssetId, (assetRecordInfo.RecordHash == null) ? "<null>" : assetRecordInfo.RecordHash.ToHexString());
							if (array == null)
							{
								array = array2;
							}
							SetCachedAssetIntoDbfSystem(assetRecordInfo.Asset.Type, array2);
							m_requiredClientStaticAssetsStillPending.Remove(assetRecordInfo.Asset);
						}
						else
						{
							flag = true;
						}
					}
				}
				catch (Exception ex2)
				{
					Error.AddDevFatal("Error reading cached asset folder {0}:\n{1}", cachedAssetFilePath, ex2.ToString());
					list.Add(assetRecordInfo.Asset);
				}
			}
			if (flag)
			{
				list.Add(assetRecordInfo.Asset);
				if (canRequestFromServer)
				{
					Log.Downloader.Print("LoadCachedAsset: locally available=false, requesting from server {0} id={1} hash={2}", assetRecordInfo.Asset.Type, assetRecordInfo.Asset.AssetId, (assetRecordInfo.RecordHash == null) ? "<null>" : assetRecordInfo.RecordHash.ToHexString());
				}
				else
				{
					Log.Downloader.Print("LoadCachedAsset: locally available=false, not requesting from server yet - {0} id={1} hash={2}", assetRecordInfo.Asset.Type, assetRecordInfo.Asset.AssetId, (assetRecordInfo.RecordHash == null) ? "<null>" : assetRecordInfo.RecordHash.ToHexString());
				}
			}
		}
		AssetRecordInfo assetRecordInfo2 = assets[0];
		if (list.Count > 0)
		{
			if (canRequestFromServer)
			{
				int nextCallbackToken = NextCallbackToken;
				if (cb != null)
				{
					m_assetRequests[nextCallbackToken] = new KeyValuePair<AssetRecordInfo, LoadCachedAssetCallback>(assetRecordInfo2, cb);
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

	private static string GetCachedAssetFolder(AssetType assetType)
	{
		string arg = assetType switch
		{
			AssetType.ASSET_TYPE_SCENARIO => "Scenario", 
			AssetType.ASSET_TYPE_SUBSET_CARD => "Subset", 
			AssetType.ASSET_TYPE_DECK_RULESET => "DeckRuleset", 
			_ => "Other", 
		};
		string cachePath = FileUtils.CachePath;
		return $"{cachePath}/{arg}";
	}

	private static string GetCachedAssetFileExtension(AssetType assetType)
	{
		return assetType switch
		{
			AssetType.ASSET_TYPE_SCENARIO => "scen", 
			AssetType.ASSET_TYPE_SUBSET_CARD => "subset_card", 
			AssetType.ASSET_TYPE_DECK_RULESET => "deck_ruleset", 
			_ => assetType.ToString().Replace("ASSET_TYPE_", "").ToLower(), 
		};
	}

	private static string GetCachedAssetFilePath(AssetType assetType, int assetId, byte[] assetHash)
	{
		string cachedAssetFolder = GetCachedAssetFolder(assetType);
		string cachedAssetFileExtension = GetCachedAssetFileExtension(assetType);
		return $"{cachedAssetFolder}/{assetId}_{assetHash.ToHexString()}.{cachedAssetFileExtension}";
	}

	private static void StoreReceivedAssetIntoLocalCache(AssetType assetType, int assetId, byte[] assetBytes, int assetBytesLength)
	{
		byte[] assetHash = SHA1.Create().ComputeHash(assetBytes, 0, assetBytesLength);
		string cachedAssetFilePath = GetCachedAssetFilePath(assetType, assetId, assetHash);
		try
		{
			if (!File.Exists(cachedAssetFilePath))
			{
				File.Create(cachedAssetFilePath).Dispose();
			}
			using FileStream fileStream = new FileStream(cachedAssetFilePath, FileMode.Truncate);
			fileStream.Write(assetBytes, 0, assetBytesLength);
		}
		catch (Exception ex)
		{
			Error.AddDevFatal("Error saving cached asset {0}:\n{1}", cachedAssetFilePath, ex.ToString());
		}
	}

	private static void SetCachedAssetIntoDbfSystem(AssetType assetType, byte[] assetBytes)
	{
		switch (assetType)
		{
		case AssetType.ASSET_TYPE_SCENARIO:
			SetCachedAssetIntoDbfSystem_Scenario(ProtobufUtil.ParseFrom<ScenarioDbRecord>(assetBytes, 0, assetBytes.Length));
			break;
		case AssetType.ASSET_TYPE_DECK_RULESET:
			SetCachedAssetIntoDbfSystem_DeckRuleset(ProtobufUtil.ParseFrom<DeckRulesetDbRecord>(assetBytes, 0, assetBytes.Length));
			break;
		case AssetType.ASSET_TYPE_SUBSET_CARD:
			SetCachedAssetIntoDbfSystem_SubsetCard(ProtobufUtil.ParseFrom<SubsetCardListDbRecord>(assetBytes, 0, assetBytes.Length));
			break;
		case AssetType.ASSET_TYPE_REWARD_CHEST:
			SetCachedAssetIntoDbfSystem_RewardChest(ProtobufUtil.ParseFrom<RewardChestDbRecord>(assetBytes, 0, assetBytes.Length));
			break;
		case AssetType.ASSET_TYPE_GUEST_HEROES:
			SetCachedAssetIntoDbfSystem_GuestHero(ProtobufUtil.ParseFrom<GuestHeroDbRecord>(assetBytes, 0, assetBytes.Length));
			break;
		case AssetType.ASSET_TYPE_DECK_TEMPLATE:
			SetCachedAssetIntoDbfSystem_DeckTemplate(ProtobufUtil.ParseFrom<DeckTemplateDbRecord>(assetBytes, 0, assetBytes.Length));
			break;
		}
	}

	private static void SetCachedAssetIntoDbfSystem_Scenario(ScenarioDbRecord protoScenario)
	{
		List<ScenarioGuestHeroesDbfRecord> outScenarioGuestHeroRecords;
		List<ClassExclusionsDbfRecord> outClassExclusionsRecords;
		ScenarioDbfRecord dbf = DbfUtils.ConvertFromProtobuf(protoScenario, out outScenarioGuestHeroRecords, out outClassExclusionsRecords);
		if (dbf == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(protoScenario) returned null:\n{0}", (protoScenario == null) ? "(null)" : protoScenario.ToString());
			return;
		}
		GameDbf.Scenario.ReplaceRecordByRecordId(dbf);
		GameDbf.ScenarioGuestHeroes.RemoveRecordsWhere((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == dbf.ID);
		foreach (ScenarioGuestHeroesDbfRecord item in outScenarioGuestHeroRecords)
		{
			GameDbf.ScenarioGuestHeroes.AddRecord(item);
		}
		GameDbf.ClassExclusions.RemoveRecordsWhere((ClassExclusionsDbfRecord r) => r.ScenarioId == dbf.ID);
		foreach (ClassExclusionsDbfRecord item2 in outClassExclusionsRecords)
		{
			GameDbf.ClassExclusions.AddRecord(item2);
		}
	}

	private static void SetCachedAssetIntoDbfSystem_DeckTemplate(DeckTemplateDbRecord protoDeckTemplate)
	{
		DeckDbfRecord deckDbf;
		List<DeckCardDbfRecord> deckCardDbfs;
		DeckTemplateDbfRecord dbf = DbfUtils.ConvertFromProtobuf(protoDeckTemplate, out deckDbf, out deckCardDbfs);
		if (dbf == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(protoSDeckTemplate) returned null:\n{0}", (protoDeckTemplate == null) ? "(null)" : protoDeckTemplate.ToString());
			return;
		}
		GameDbf.DeckTemplate.ReplaceRecordByRecordId(dbf);
		GameDbf.Deck.ReplaceRecordByRecordId(deckDbf);
		GameDbf.DeckCard.RemoveRecordsWhere((DeckCardDbfRecord r) => r.DeckId == dbf.DeckId);
		foreach (DeckCardDbfRecord item in deckCardDbfs)
		{
			GameDbf.DeckCard.AddRecord(item);
		}
	}

	private static void SetCachedAssetIntoDbfSystem_DeckRuleset(DeckRulesetDbRecord proto)
	{
		DeckRulesetDbfRecord deckRulesetDbfRecord = DbfUtils.ConvertFromProtobuf(proto);
		if (deckRulesetDbfRecord == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(proto) returned null:\n{0}", (proto == null) ? "(null)" : proto.ToString());
		}
		else
		{
			GameDbf.DeckRuleset.ReplaceRecordByRecordId(deckRulesetDbfRecord);
		}
		foreach (DeckRulesetRuleDbRecord rule in proto.Rules)
		{
			List<int> outTargetSubsetIds;
			DeckRulesetRuleDbfRecord dbfRule = DbfUtils.ConvertFromProtobuf(rule, out outTargetSubsetIds);
			GameDbf.DeckRulesetRule.ReplaceRecordByRecordId(dbfRule);
			GameDbf.DeckRulesetRuleSubset.RemoveRecordsWhere((DeckRulesetRuleSubsetDbfRecord r) => r.DeckRulesetRuleId == dbfRule.ID);
			if (outTargetSubsetIds != null)
			{
				for (int i = 0; i < outTargetSubsetIds.Count; i++)
				{
					DeckRulesetRuleSubsetDbfRecord deckRulesetRuleSubsetDbfRecord = new DeckRulesetRuleSubsetDbfRecord();
					deckRulesetRuleSubsetDbfRecord.SetDeckRulesetRuleId(dbfRule.ID);
					deckRulesetRuleSubsetDbfRecord.SetSubsetId(outTargetSubsetIds[i]);
					GameDbf.DeckRulesetRuleSubset.AddRecord(deckRulesetRuleSubsetDbfRecord);
				}
			}
		}
	}

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

	private static void SetCachedAssetIntoDbfSystem_RewardChest(RewardChestDbRecord proto)
	{
		RewardChestDbfRecord rewardChestDbfRecord = DbfUtils.ConvertFromProtobuf(proto);
		if (rewardChestDbfRecord == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(RewardChestDbRecord) returned null:\n{0}", (proto == null) ? "(null)" : proto.ToString());
		}
		else
		{
			GameDbf.RewardChest.ReplaceRecordByRecordId(rewardChestDbfRecord);
		}
	}

	private static void SetCachedAssetIntoDbfSystem_GuestHero(GuestHeroDbRecord proto)
	{
		GuestHeroDbfRecord guestHeroDbfRecord = DbfUtils.ConvertFromProtobuf(proto);
		if (guestHeroDbfRecord == null)
		{
			Log.Downloader.Print("DbfUtils.ConvertFromProtobuf(GuestHeroDbfRecord) returned null:\n{0}", (proto == null) ? "(null)" : proto.ToString());
		}
		else
		{
			GameDbf.GuestHero.ReplaceRecordByRecordId(guestHeroDbfRecord);
		}
	}

	private void NetCache_OnClientStaticAssetsResponse()
	{
		ClientStaticAssetsResponse netObject = NetCache.Get().GetNetObject<ClientStaticAssetsResponse>();
		if (netObject == null)
		{
			return;
		}
		foreach (AssetRecordInfo item in netObject.AssetsToGet)
		{
			m_requiredClientStaticAssetsStillPending.Add(item.Asset);
		}
		LoadCachedAssets(canRequestFromServer: true, null, netObject.AssetsToGet.ToArray());
	}

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
				m_requiredClientStaticAssetsStillPending.Remove(assetResponse2.RequestedKey);
			}
			else
			{
				Log.Downloader.Print("Network_OnGetAssetResponse: error={0}:{1} type={2}:{3} id={4}", (int)assetResponse2.ErrorCode, assetResponse2.ErrorCode.ToString(), (int)assetResponse2.RequestedKey.Type, assetResponse2.RequestedKey.Type.ToString(), assetResponse2.RequestedKey.AssetId);
				if (errorCode == ErrorCode.ERROR_OK)
				{
					errorCode = assetResponse2.ErrorCode;
				}
				if (m_requiredClientStaticAssetsStillPending.Contains(assetResponse2.RequestedKey))
				{
					Error.AddDevFatal(GameStrings.Get("GLUE_REQUIRED_CLIENT_STATIC_ASSETS_ERROR_MESSAGE"));
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
				StoreReceivedAssetIntoLocalCache(requestedKey.Type, requestedKey.AssetId, array, array.Length);
				SetCachedAssetIntoDbfSystem(requestedKey.Type, array);
			}
		}
		Processor.CancelScheduledCallback(PruneCachedAssetFiles);
		Processor.ScheduleCallback(5f, realTime: true, PruneCachedAssetFiles);
		if (!m_assetRequests.TryGetValue(assetResponse.ClientToken, out var value))
		{
			return;
		}
		AssetRecordInfo key = value.Key;
		LoadCachedAssetCallback value2 = value.Value;
		m_assetRequests.Remove(assetResponse.ClientToken);
		if (!map.TryGetValue(key.Asset, out var value3))
		{
			if (LoadCachedAssets(false, value2, key))
			{
				return;
			}
			value3 = new byte[0];
		}
		value2(key.Asset, errorCode, value3);
	}

	private static void PruneCachedAssetFiles(object userData)
	{
		string cachePath = FileUtils.CachePath;
		string message = null;
		string text = null;
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(cachePath);
			if (!directoryInfo.Exists)
			{
				return;
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			foreach (DirectoryInfo obj in directories)
			{
				message = obj.FullName;
				FileInfo[] files = obj.GetFiles();
				foreach (FileInfo fileInfo in files)
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
		catch (Exception ex)
		{
			Error.AddDevWarning("Error pruning dir={0} file={1}:\n{2}", message, text, ex.ToString());
		}
	}
}
