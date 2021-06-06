using System.Collections.Generic;
using UnityEngine;

public abstract class RewardData
{
	private Reward.Type m_type;

	private NetCache.ProfileNotice.NoticeOrigin m_origin = NetCache.ProfileNotice.NoticeOrigin.UNKNOWN;

	private long m_originData;

	protected List<long> m_noticeIDs = new List<long>();

	private bool m_isDummyReward;

	public Reward.Type RewardType => m_type;

	public NetCache.ProfileNotice.NoticeOrigin Origin => m_origin;

	public long OriginData => m_originData;

	public bool IsDummyReward => m_isDummyReward;

	public string NameOverride { get; set; }

	public string DescriptionOverride { get; set; }

	public int? RewardChestAssetId { get; set; }

	public int? RewardChestBagNum { get; set; }

	public void LoadRewardObject(Reward.DelOnRewardLoaded callback)
	{
		LoadRewardObject(callback, null);
	}

	public void LoadRewardObject(Reward.DelOnRewardLoaded callback, object callbackData)
	{
		string assetPath = GetAssetPath();
		if (string.IsNullOrEmpty(assetPath))
		{
			Debug.LogError($"Reward.LoadRewardObject(): Do not know how to load reward object for {this}.");
			return;
		}
		Reward.LoadRewardCallbackData callbackData2 = new Reward.LoadRewardCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		AssetLoader.Get().InstantiatePrefab(assetPath, OnRewardObjectLoaded, callbackData2);
	}

	public void SetOrigin(NetCache.ProfileNotice.NoticeOrigin origin, long originData)
	{
		m_origin = origin;
		m_originData = originData;
	}

	public void AddNoticeID(long noticeID)
	{
		if (!m_noticeIDs.Contains(noticeID))
		{
			m_noticeIDs.Add(noticeID);
		}
	}

	public List<long> GetNoticeIDs()
	{
		return m_noticeIDs;
	}

	public bool HasNotices()
	{
		return m_noticeIDs.Count > 0;
	}

	public void AcknowledgeNotices()
	{
		long[] array = m_noticeIDs.ToArray();
		m_noticeIDs.Clear();
		long[] array2 = array;
		foreach (long id in array2)
		{
			Network.Get().AckNotice(id);
		}
	}

	public void MarkAsDummyReward()
	{
		m_isDummyReward = true;
	}

	protected RewardData(Reward.Type type)
	{
		m_type = type;
	}

	protected abstract string GetAssetPath();

	private void OnRewardObjectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"Reward.OnRewardObjectLoaded() - game object is null assetRef={assetRef}");
			return;
		}
		Reward component = go.GetComponent<Reward>();
		if (component == null)
		{
			Debug.LogErrorFormat("Reward.OnRewardObjectLoaded() - loaded game object has no reward component assetRef={0}", assetRef);
			return;
		}
		go.transform.parent = SceneMgr.Get().SceneObject.transform;
		component.SetData(this, updateVisuals: true);
		Reward.LoadRewardCallbackData loadRewardCallbackData = callbackData as Reward.LoadRewardCallbackData;
		component.NotifyLoadedWhenReady(loadRewardCallbackData);
	}
}
