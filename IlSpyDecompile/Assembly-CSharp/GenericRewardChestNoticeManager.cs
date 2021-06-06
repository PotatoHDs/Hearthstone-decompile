using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using PegasusShared;
using UnityEngine;

public class GenericRewardChestNoticeManager : IService
{
	private class GenericRewardChestAssetStatus
	{
		public bool m_isReady;

		public HashSet<long> m_noticeIds = new HashSet<long>();
	}

	public delegate void GenericRewardUpdatedCallback(long receivedRewardNoticeIds, object userData);

	private class GenericRewardChestUpdatedListener : EventListener<GenericRewardUpdatedCallback>
	{
		public void Fire(long receivedRewardNoticeIds)
		{
			m_callback(receivedRewardNoticeIds, m_userData);
		}
	}

	private Dictionary<int, GenericRewardChestAssetStatus> m_mapOfRewardChestAssetIdToNoticeIds = new Dictionary<int, GenericRewardChestAssetStatus>();

	private List<GenericRewardChestUpdatedListener> m_genericRewardUpdatedListeners = new List<GenericRewardChestUpdatedListener>();

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(OnNewNotices);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(NetCache) };
	}

	public void Shutdown()
	{
	}

	public static GenericRewardChestNoticeManager Get()
	{
		return HearthstoneServices.Get<GenericRewardChestNoticeManager>();
	}

	public HashSet<long> GetReadyGenericRewardChestNotices()
	{
		HashSet<long> hashSet = new HashSet<long>();
		foreach (GenericRewardChestAssetStatus value in m_mapOfRewardChestAssetIdToNoticeIds.Values)
		{
			if (value.m_isReady)
			{
				hashSet.UnionWith(value.m_noticeIds);
			}
		}
		return hashSet;
	}

	public bool RegisterRewardsUpdatedListener(GenericRewardUpdatedCallback callback, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		GenericRewardChestUpdatedListener genericRewardChestUpdatedListener = new GenericRewardChestUpdatedListener();
		genericRewardChestUpdatedListener.SetCallback(callback);
		genericRewardChestUpdatedListener.SetUserData(userData);
		if (m_genericRewardUpdatedListeners.Contains(genericRewardChestUpdatedListener))
		{
			return false;
		}
		m_genericRewardUpdatedListeners.Add(genericRewardChestUpdatedListener);
		return true;
	}

	public bool RemoveRewardsUpdatedListener(GenericRewardUpdatedCallback callback)
	{
		return RemoveRewardsUpdatedListener(callback, null);
	}

	public bool RemoveRewardsUpdatedListener(GenericRewardUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		GenericRewardChestUpdatedListener genericRewardChestUpdatedListener = new GenericRewardChestUpdatedListener();
		genericRewardChestUpdatedListener.SetCallback(callback);
		genericRewardChestUpdatedListener.SetUserData(userData);
		if (!m_genericRewardUpdatedListeners.Contains(genericRewardChestUpdatedListener))
		{
			return false;
		}
		m_genericRewardUpdatedListeners.Remove(genericRewardChestUpdatedListener);
		return true;
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>() == null)
		{
			return;
		}
		bool flag = false;
		foreach (NetCache.ProfileNotice newNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST != newNotice.Type)
			{
				continue;
			}
			NetCache.ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = newNotice as NetCache.ProfileNoticeGenericRewardChest;
			if (profileNoticeGenericRewardChest == null || profileNoticeGenericRewardChest.RewardChestHash == null || profileNoticeGenericRewardChest.RewardChestByteSize == 0)
			{
				Debug.LogError($"ProfileNoticeGenericRewardChest with asset id [{profileNoticeGenericRewardChest.RewardChestAssetId}] with no hash or a byte size of 0. Unable to request reward chest record information.");
				if (GameDbf.RewardChest.HasRecord(profileNoticeGenericRewardChest.RewardChestAssetId))
				{
					Debug.LogWarning($"Local RewardChest record found for asset id {profileNoticeGenericRewardChest.RewardChestAssetId}. Using cached value.");
					InformListenersThatNoticeIsReady(newNotice.NoticeID);
				}
				continue;
			}
			AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
			assetRecordInfo.Asset = new AssetKey();
			assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_REWARD_CHEST;
			assetRecordInfo.Asset.AssetId = profileNoticeGenericRewardChest.RewardChestAssetId;
			assetRecordInfo.RecordByteSize = profileNoticeGenericRewardChest.RewardChestByteSize;
			assetRecordInfo.RecordHash = profileNoticeGenericRewardChest.RewardChestHash;
			if (!m_mapOfRewardChestAssetIdToNoticeIds.ContainsKey(profileNoticeGenericRewardChest.RewardChestAssetId))
			{
				m_mapOfRewardChestAssetIdToNoticeIds[profileNoticeGenericRewardChest.RewardChestAssetId] = new GenericRewardChestAssetStatus();
			}
			m_mapOfRewardChestAssetIdToNoticeIds[profileNoticeGenericRewardChest.RewardChestAssetId].m_noticeIds.Add(newNotice.NoticeID);
			if (!flag)
			{
				flag = DownloadableDbfCache.Get().IsAssetRequestInProgress(profileNoticeGenericRewardChest.RewardChestAssetId, AssetType.ASSET_TYPE_REWARD_CHEST);
			}
			DownloadableDbfCache.Get().LoadCachedAssets(!flag, OnRewardChestDownloadableDbfAssetsLoaded, assetRecordInfo);
		}
	}

	private void OnRewardChestDownloadableDbfAssetsLoaded(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
	{
		if (code != 0)
		{
			Debug.LogError($"Unable to get reward chest asset information for Reward Chest ID: {requestedKey.AssetId}, ErrorCode: {code}");
			return;
		}
		GenericRewardChestAssetStatus genericRewardChestAssetStatus = m_mapOfRewardChestAssetIdToNoticeIds[requestedKey.AssetId];
		genericRewardChestAssetStatus.m_isReady = true;
		foreach (long noticeId in genericRewardChestAssetStatus.m_noticeIds)
		{
			InformListenersThatNoticeIsReady(noticeId);
		}
	}

	private void InformListenersThatNoticeIsReady(long noticeId)
	{
		GenericRewardChestUpdatedListener[] array = m_genericRewardUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(noticeId);
		}
	}
}
