using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200067A RID: 1658
public abstract class RewardData
{
	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x06005CCD RID: 23757 RVA: 0x001E147B File Offset: 0x001DF67B
	public Reward.Type RewardType
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06005CCE RID: 23758 RVA: 0x001E1483 File Offset: 0x001DF683
	public NetCache.ProfileNotice.NoticeOrigin Origin
	{
		get
		{
			return this.m_origin;
		}
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x06005CCF RID: 23759 RVA: 0x001E148B File Offset: 0x001DF68B
	public long OriginData
	{
		get
		{
			return this.m_originData;
		}
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x06005CD0 RID: 23760 RVA: 0x001E1493 File Offset: 0x001DF693
	public bool IsDummyReward
	{
		get
		{
			return this.m_isDummyReward;
		}
	}

	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x001E149B File Offset: 0x001DF69B
	// (set) Token: 0x06005CD2 RID: 23762 RVA: 0x001E14A3 File Offset: 0x001DF6A3
	public string NameOverride { get; set; }

	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x001E14AC File Offset: 0x001DF6AC
	// (set) Token: 0x06005CD4 RID: 23764 RVA: 0x001E14B4 File Offset: 0x001DF6B4
	public string DescriptionOverride { get; set; }

	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x06005CD5 RID: 23765 RVA: 0x001E14BD File Offset: 0x001DF6BD
	// (set) Token: 0x06005CD6 RID: 23766 RVA: 0x001E14C5 File Offset: 0x001DF6C5
	public int? RewardChestAssetId { get; set; }

	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x06005CD7 RID: 23767 RVA: 0x001E14CE File Offset: 0x001DF6CE
	// (set) Token: 0x06005CD8 RID: 23768 RVA: 0x001E14D6 File Offset: 0x001DF6D6
	public int? RewardChestBagNum { get; set; }

	// Token: 0x06005CD9 RID: 23769 RVA: 0x001E14DF File Offset: 0x001DF6DF
	public void LoadRewardObject(Reward.DelOnRewardLoaded callback)
	{
		this.LoadRewardObject(callback, null);
	}

	// Token: 0x06005CDA RID: 23770 RVA: 0x001E14EC File Offset: 0x001DF6EC
	public void LoadRewardObject(Reward.DelOnRewardLoaded callback, object callbackData)
	{
		string assetPath = this.GetAssetPath();
		if (string.IsNullOrEmpty(assetPath))
		{
			Debug.LogError(string.Format("Reward.LoadRewardObject(): Do not know how to load reward object for {0}.", this));
			return;
		}
		Reward.LoadRewardCallbackData callbackData2 = new Reward.LoadRewardCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		AssetLoader.Get().InstantiatePrefab(assetPath, new PrefabCallback<GameObject>(this.OnRewardObjectLoaded), callbackData2, AssetLoadingOptions.None);
	}

	// Token: 0x06005CDB RID: 23771 RVA: 0x001E154C File Offset: 0x001DF74C
	public void SetOrigin(NetCache.ProfileNotice.NoticeOrigin origin, long originData)
	{
		this.m_origin = origin;
		this.m_originData = originData;
	}

	// Token: 0x06005CDC RID: 23772 RVA: 0x001E155C File Offset: 0x001DF75C
	public void AddNoticeID(long noticeID)
	{
		if (this.m_noticeIDs.Contains(noticeID))
		{
			return;
		}
		this.m_noticeIDs.Add(noticeID);
	}

	// Token: 0x06005CDD RID: 23773 RVA: 0x001E1579 File Offset: 0x001DF779
	public List<long> GetNoticeIDs()
	{
		return this.m_noticeIDs;
	}

	// Token: 0x06005CDE RID: 23774 RVA: 0x001E1581 File Offset: 0x001DF781
	public bool HasNotices()
	{
		return this.m_noticeIDs.Count > 0;
	}

	// Token: 0x06005CDF RID: 23775 RVA: 0x001E1594 File Offset: 0x001DF794
	public void AcknowledgeNotices()
	{
		long[] array = this.m_noticeIDs.ToArray();
		this.m_noticeIDs.Clear();
		foreach (long id in array)
		{
			Network.Get().AckNotice(id);
		}
	}

	// Token: 0x06005CE0 RID: 23776 RVA: 0x001E15D5 File Offset: 0x001DF7D5
	public void MarkAsDummyReward()
	{
		this.m_isDummyReward = true;
	}

	// Token: 0x06005CE1 RID: 23777 RVA: 0x001E15DE File Offset: 0x001DF7DE
	protected RewardData(Reward.Type type)
	{
		this.m_type = type;
	}

	// Token: 0x06005CE2 RID: 23778
	protected abstract string GetAssetPath();

	// Token: 0x06005CE3 RID: 23779 RVA: 0x001E1600 File Offset: 0x001DF800
	private void OnRewardObjectLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("Reward.OnRewardObjectLoaded() - game object is null assetRef={0}", assetRef));
			return;
		}
		Reward component = go.GetComponent<Reward>();
		if (component == null)
		{
			Debug.LogErrorFormat("Reward.OnRewardObjectLoaded() - loaded game object has no reward component assetRef={0}", new object[]
			{
				assetRef
			});
			return;
		}
		go.transform.parent = SceneMgr.Get().SceneObject.transform;
		component.SetData(this, true);
		Reward.LoadRewardCallbackData loadRewardCallbackData = callbackData as Reward.LoadRewardCallbackData;
		component.NotifyLoadedWhenReady(loadRewardCallbackData);
	}

	// Token: 0x04004EB3 RID: 20147
	private Reward.Type m_type;

	// Token: 0x04004EB4 RID: 20148
	private NetCache.ProfileNotice.NoticeOrigin m_origin = NetCache.ProfileNotice.NoticeOrigin.UNKNOWN;

	// Token: 0x04004EB5 RID: 20149
	private long m_originData;

	// Token: 0x04004EB6 RID: 20150
	protected List<long> m_noticeIDs = new List<long>();

	// Token: 0x04004EB7 RID: 20151
	private bool m_isDummyReward;
}
