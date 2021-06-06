using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using PegasusShared;
using UnityEngine;

// Token: 0x020008CF RID: 2255
public class GenericRewardChestNoticeManager : IService
{
	// Token: 0x06007CC5 RID: 31941 RVA: 0x00289460 File Offset: 0x00287660
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<NetCache>().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		yield break;
	}

	// Token: 0x06007CC6 RID: 31942 RVA: 0x00289476 File Offset: 0x00287676
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(NetCache)
		};
	}

	// Token: 0x06007CC7 RID: 31943 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06007CC8 RID: 31944 RVA: 0x0028948B File Offset: 0x0028768B
	public static GenericRewardChestNoticeManager Get()
	{
		return HearthstoneServices.Get<GenericRewardChestNoticeManager>();
	}

	// Token: 0x06007CC9 RID: 31945 RVA: 0x00289494 File Offset: 0x00287694
	public HashSet<long> GetReadyGenericRewardChestNotices()
	{
		HashSet<long> hashSet = new HashSet<long>();
		foreach (GenericRewardChestNoticeManager.GenericRewardChestAssetStatus genericRewardChestAssetStatus in this.m_mapOfRewardChestAssetIdToNoticeIds.Values)
		{
			if (genericRewardChestAssetStatus.m_isReady)
			{
				hashSet.UnionWith(genericRewardChestAssetStatus.m_noticeIds);
			}
		}
		return hashSet;
	}

	// Token: 0x06007CCA RID: 31946 RVA: 0x00289500 File Offset: 0x00287700
	public bool RegisterRewardsUpdatedListener(GenericRewardChestNoticeManager.GenericRewardUpdatedCallback callback, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener genericRewardChestUpdatedListener = new GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener();
		genericRewardChestUpdatedListener.SetCallback(callback);
		genericRewardChestUpdatedListener.SetUserData(userData);
		if (this.m_genericRewardUpdatedListeners.Contains(genericRewardChestUpdatedListener))
		{
			return false;
		}
		this.m_genericRewardUpdatedListeners.Add(genericRewardChestUpdatedListener);
		return true;
	}

	// Token: 0x06007CCB RID: 31947 RVA: 0x00289543 File Offset: 0x00287743
	public bool RemoveRewardsUpdatedListener(GenericRewardChestNoticeManager.GenericRewardUpdatedCallback callback)
	{
		return this.RemoveRewardsUpdatedListener(callback, null);
	}

	// Token: 0x06007CCC RID: 31948 RVA: 0x00289550 File Offset: 0x00287750
	public bool RemoveRewardsUpdatedListener(GenericRewardChestNoticeManager.GenericRewardUpdatedCallback callback, object userData)
	{
		if (callback == null)
		{
			return false;
		}
		GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener genericRewardChestUpdatedListener = new GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener();
		genericRewardChestUpdatedListener.SetCallback(callback);
		genericRewardChestUpdatedListener.SetUserData(userData);
		if (!this.m_genericRewardUpdatedListeners.Contains(genericRewardChestUpdatedListener))
		{
			return false;
		}
		this.m_genericRewardUpdatedListeners.Remove(genericRewardChestUpdatedListener);
		return true;
	}

	// Token: 0x06007CCD RID: 31949 RVA: 0x00289594 File Offset: 0x00287794
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>() == null)
		{
			return;
		}
		bool flag = false;
		foreach (NetCache.ProfileNotice profileNotice in newNotices)
		{
			if (NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST == profileNotice.Type)
			{
				NetCache.ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = profileNotice as NetCache.ProfileNoticeGenericRewardChest;
				if (profileNoticeGenericRewardChest == null || profileNoticeGenericRewardChest.RewardChestHash == null || profileNoticeGenericRewardChest.RewardChestByteSize == 0U)
				{
					Debug.LogError(string.Format("ProfileNoticeGenericRewardChest with asset id [{0}] with no hash or a byte size of 0. Unable to request reward chest record information.", profileNoticeGenericRewardChest.RewardChestAssetId));
					if (GameDbf.RewardChest.HasRecord(profileNoticeGenericRewardChest.RewardChestAssetId))
					{
						Debug.LogWarning(string.Format("Local RewardChest record found for asset id {0}. Using cached value.", profileNoticeGenericRewardChest.RewardChestAssetId));
						this.InformListenersThatNoticeIsReady(profileNotice.NoticeID);
					}
				}
				else
				{
					AssetRecordInfo assetRecordInfo = new AssetRecordInfo();
					assetRecordInfo.Asset = new AssetKey();
					assetRecordInfo.Asset.Type = AssetType.ASSET_TYPE_REWARD_CHEST;
					assetRecordInfo.Asset.AssetId = profileNoticeGenericRewardChest.RewardChestAssetId;
					assetRecordInfo.RecordByteSize = profileNoticeGenericRewardChest.RewardChestByteSize;
					assetRecordInfo.RecordHash = profileNoticeGenericRewardChest.RewardChestHash;
					if (!this.m_mapOfRewardChestAssetIdToNoticeIds.ContainsKey(profileNoticeGenericRewardChest.RewardChestAssetId))
					{
						this.m_mapOfRewardChestAssetIdToNoticeIds[profileNoticeGenericRewardChest.RewardChestAssetId] = new GenericRewardChestNoticeManager.GenericRewardChestAssetStatus();
					}
					this.m_mapOfRewardChestAssetIdToNoticeIds[profileNoticeGenericRewardChest.RewardChestAssetId].m_noticeIds.Add(profileNotice.NoticeID);
					if (!flag)
					{
						flag = DownloadableDbfCache.Get().IsAssetRequestInProgress(profileNoticeGenericRewardChest.RewardChestAssetId, AssetType.ASSET_TYPE_REWARD_CHEST);
					}
					DownloadableDbfCache.Get().LoadCachedAssets(!flag, new DownloadableDbfCache.LoadCachedAssetCallback(this.OnRewardChestDownloadableDbfAssetsLoaded), new AssetRecordInfo[]
					{
						assetRecordInfo
					});
				}
			}
		}
	}

	// Token: 0x06007CCE RID: 31950 RVA: 0x00289750 File Offset: 0x00287950
	private void OnRewardChestDownloadableDbfAssetsLoaded(AssetKey requestedKey, ErrorCode code, byte[] assetBytes)
	{
		if (code != ErrorCode.ERROR_OK)
		{
			Debug.LogError(string.Format("Unable to get reward chest asset information for Reward Chest ID: {0}, ErrorCode: {1}", requestedKey.AssetId, code));
			return;
		}
		GenericRewardChestNoticeManager.GenericRewardChestAssetStatus genericRewardChestAssetStatus = this.m_mapOfRewardChestAssetIdToNoticeIds[requestedKey.AssetId];
		genericRewardChestAssetStatus.m_isReady = true;
		foreach (long noticeId in genericRewardChestAssetStatus.m_noticeIds)
		{
			this.InformListenersThatNoticeIsReady(noticeId);
		}
	}

	// Token: 0x06007CCF RID: 31951 RVA: 0x002897E0 File Offset: 0x002879E0
	private void InformListenersThatNoticeIsReady(long noticeId)
	{
		GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener[] array = this.m_genericRewardUpdatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(noticeId);
		}
	}

	// Token: 0x04006572 RID: 25970
	private Dictionary<int, GenericRewardChestNoticeManager.GenericRewardChestAssetStatus> m_mapOfRewardChestAssetIdToNoticeIds = new Dictionary<int, GenericRewardChestNoticeManager.GenericRewardChestAssetStatus>();

	// Token: 0x04006573 RID: 25971
	private List<GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener> m_genericRewardUpdatedListeners = new List<GenericRewardChestNoticeManager.GenericRewardChestUpdatedListener>();

	// Token: 0x02002556 RID: 9558
	private class GenericRewardChestAssetStatus
	{
		// Token: 0x0400ED2E RID: 60718
		public bool m_isReady;

		// Token: 0x0400ED2F RID: 60719
		public HashSet<long> m_noticeIds = new HashSet<long>();
	}

	// Token: 0x02002557 RID: 9559
	// (Invoke) Token: 0x060132AF RID: 78511
	public delegate void GenericRewardUpdatedCallback(long receivedRewardNoticeIds, object userData);

	// Token: 0x02002558 RID: 9560
	private class GenericRewardChestUpdatedListener : EventListener<GenericRewardChestNoticeManager.GenericRewardUpdatedCallback>
	{
		// Token: 0x060132B2 RID: 78514 RVA: 0x0052B3CE File Offset: 0x005295CE
		public void Fire(long receivedRewardNoticeIds)
		{
			this.m_callback(receivedRewardNoticeIds, this.m_userData);
		}
	}
}
