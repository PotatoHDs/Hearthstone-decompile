using System;
using System.Collections.Generic;
using UnityEngine;

public class BannerManager
{
	public delegate void DelOnCloseBanner();

	private static BannerManager s_instance;

	private bool m_bannerWasAcknowledged;

	private List<int> m_seenBanners = new List<int>();

	private bool m_isShowing;

	public bool IsShowing => m_isShowing;

	public static BannerManager Get()
	{
		if (s_instance == null)
		{
			s_instance = new BannerManager();
		}
		return s_instance;
	}

	private int GetOutstandingDisplayBannerId()
	{
		int @int = Vars.Key("Events.BannerIdOverride").GetInt(0);
		if (@int != 0)
		{
			return @int;
		}
		return NetCache.Get().GetNetObject<NetCache.NetCacheDisplayBanner>()?.Id ?? 0;
	}

	private bool AcknowledgeBanner(int banner)
	{
		m_seenBanners.Add(banner);
		if (banner != GetOutstandingDisplayBannerId() || m_bannerWasAcknowledged)
		{
			return false;
		}
		m_bannerWasAcknowledged = true;
		NetCache.NetCacheDisplayBanner netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisplayBanner>();
		if (netObject != null)
		{
			netObject.Id = banner;
			NetCache.Get().NetCacheChanged<NetCache.NetCacheDisplayBanner>();
		}
		Network.Get().AcknowledgeBanner(banner);
		return true;
	}

	public void AutoAcknowledgeOutstandingBanner()
	{
		int outstandingDisplayBannerId = GetOutstandingDisplayBannerId();
		if (outstandingDisplayBannerId != 0)
		{
			AcknowledgeBanner(outstandingDisplayBannerId);
		}
	}

	public bool ShowOutstandingBannerEvent(DelOnCloseBanner callback = null)
	{
		int outstandingDisplayBannerId = GetOutstandingDisplayBannerId();
		if (!Options.Get().GetBool(Option.HAS_SEEN_HUB, defaultVal: false))
		{
			return false;
		}
		if (m_seenBanners.Contains(outstandingDisplayBannerId))
		{
			return false;
		}
		if (ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			AcknowledgeBanner(outstandingDisplayBannerId);
			return false;
		}
		if (ShowBanner(outstandingDisplayBannerId, callback))
		{
			AcknowledgeBanner(outstandingDisplayBannerId);
			return true;
		}
		return false;
	}

	public bool ShowBanner(string prefabAssetPath, string headerText, string text, DelOnCloseBanner callback = null, Action<BannerPopup> onCreateCallback = null)
	{
		BannerPopup bannerPopup = GameUtils.LoadGameObjectWithComponent<BannerPopup>(prefabAssetPath);
		if (bannerPopup == null)
		{
			return false;
		}
		onCreateCallback?.Invoke(bannerPopup);
		m_isShowing = true;
		bannerPopup.Show(headerText, text, delegate
		{
			OnBannerClose();
			if (callback != null)
			{
				callback();
			}
		});
		return true;
	}

	public bool ShowBanner(int bannerID, DelOnCloseBanner callback = null)
	{
		if (bannerID == 0)
		{
			return false;
		}
		BannerDbfRecord record = GameDbf.Banner.GetRecord(bannerID);
		string text = record?.Prefab;
		if (record == null || text == null)
		{
			Debug.LogWarning($"No banner defined for bannerID={bannerID}");
			return false;
		}
		return ShowBanner(text, record.HeaderText, record.Text, callback);
	}

	public void Cheat_ClearSeenBannersNewerThan(int bannerId)
	{
		m_seenBanners.RemoveAll((int i) => i >= bannerId);
	}

	public void Cheat_ClearSeenBanners()
	{
		m_seenBanners.Clear();
	}

	private BannerManager()
	{
	}

	private void OnBannerClose()
	{
		m_isShowing = false;
	}
}
