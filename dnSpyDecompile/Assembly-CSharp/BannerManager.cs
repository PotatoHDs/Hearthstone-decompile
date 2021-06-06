using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class BannerManager
{
	// Token: 0x0600063E RID: 1598 RVA: 0x000248BF File Offset: 0x00022ABF
	public static BannerManager Get()
	{
		if (BannerManager.s_instance == null)
		{
			BannerManager.s_instance = new BannerManager();
		}
		return BannerManager.s_instance;
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x0600063F RID: 1599 RVA: 0x000248D7 File Offset: 0x00022AD7
	public bool IsShowing
	{
		get
		{
			return this.m_isShowing;
		}
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x000248E0 File Offset: 0x00022AE0
	private int GetOutstandingDisplayBannerId()
	{
		int @int = Vars.Key("Events.BannerIdOverride").GetInt(0);
		if (@int != 0)
		{
			return @int;
		}
		NetCache.NetCacheDisplayBanner netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisplayBanner>();
		if (netObject != null)
		{
			return netObject.Id;
		}
		return 0;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002491C File Offset: 0x00022B1C
	private bool AcknowledgeBanner(int banner)
	{
		this.m_seenBanners.Add(banner);
		if (banner != this.GetOutstandingDisplayBannerId() || this.m_bannerWasAcknowledged)
		{
			return false;
		}
		this.m_bannerWasAcknowledged = true;
		NetCache.NetCacheDisplayBanner netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisplayBanner>();
		if (netObject != null)
		{
			netObject.Id = banner;
			NetCache.Get().NetCacheChanged<NetCache.NetCacheDisplayBanner>();
		}
		Network.Get().AcknowledgeBanner(banner);
		return true;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0002497C File Offset: 0x00022B7C
	public void AutoAcknowledgeOutstandingBanner()
	{
		int outstandingDisplayBannerId = this.GetOutstandingDisplayBannerId();
		if (outstandingDisplayBannerId == 0)
		{
			return;
		}
		this.AcknowledgeBanner(outstandingDisplayBannerId);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0002499C File Offset: 0x00022B9C
	public bool ShowOutstandingBannerEvent(BannerManager.DelOnCloseBanner callback = null)
	{
		int outstandingDisplayBannerId = this.GetOutstandingDisplayBannerId();
		if (!Options.Get().GetBool(Option.HAS_SEEN_HUB, false))
		{
			return false;
		}
		if (this.m_seenBanners.Contains(outstandingDisplayBannerId))
		{
			return false;
		}
		if (ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			this.AcknowledgeBanner(outstandingDisplayBannerId);
			return false;
		}
		if (this.ShowBanner(outstandingDisplayBannerId, callback))
		{
			this.AcknowledgeBanner(outstandingDisplayBannerId);
			return true;
		}
		return false;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00024A00 File Offset: 0x00022C00
	public bool ShowBanner(string prefabAssetPath, string headerText, string text, BannerManager.DelOnCloseBanner callback = null, Action<BannerPopup> onCreateCallback = null)
	{
		BannerPopup bannerPopup = GameUtils.LoadGameObjectWithComponent<BannerPopup>(prefabAssetPath);
		if (bannerPopup == null)
		{
			return false;
		}
		if (onCreateCallback != null)
		{
			onCreateCallback(bannerPopup);
		}
		this.m_isShowing = true;
		bannerPopup.Show(headerText, text, delegate
		{
			this.OnBannerClose();
			if (callback != null)
			{
				callback();
			}
		});
		return true;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00024A5C File Offset: 0x00022C5C
	public bool ShowBanner(int bannerID, BannerManager.DelOnCloseBanner callback = null)
	{
		if (bannerID == 0)
		{
			return false;
		}
		BannerDbfRecord record = GameDbf.Banner.GetRecord(bannerID);
		string text = (record == null) ? null : record.Prefab;
		if (record == null || text == null)
		{
			Debug.LogWarning(string.Format("No banner defined for bannerID={0}", bannerID));
			return false;
		}
		return this.ShowBanner(text, record.HeaderText, record.Text, callback, null);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x00024AC4 File Offset: 0x00022CC4
	public void Cheat_ClearSeenBannersNewerThan(int bannerId)
	{
		this.m_seenBanners.RemoveAll((int i) => i >= bannerId);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00024AF6 File Offset: 0x00022CF6
	public void Cheat_ClearSeenBanners()
	{
		this.m_seenBanners.Clear();
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00024B03 File Offset: 0x00022D03
	private BannerManager()
	{
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x00024B16 File Offset: 0x00022D16
	private void OnBannerClose()
	{
		this.m_isShowing = false;
	}

	// Token: 0x04000459 RID: 1113
	private static BannerManager s_instance;

	// Token: 0x0400045A RID: 1114
	private bool m_bannerWasAcknowledged;

	// Token: 0x0400045B RID: 1115
	private List<int> m_seenBanners = new List<int>();

	// Token: 0x0400045C RID: 1116
	private bool m_isShowing;

	// Token: 0x0200136F RID: 4975
	// (Invoke) Token: 0x0600D778 RID: 55160
	public delegate void DelOnCloseBanner();
}
