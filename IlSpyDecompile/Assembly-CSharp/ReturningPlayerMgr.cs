using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class ReturningPlayerMgr : IService
{
	public delegate void WelcomeBannerCloseCallback();

	private const int RETURNING_PLAYER_BANNER_ID = 1;

	private ReturningPlayerStatus m_returningPlayerProgress = ReturningPlayerStatus.RPS_NOT_RETURNING_PLAYER;

	private uint m_abTestGroup;

	private long m_notificationSuppressionTimeSeconds;

	public bool IsInReturningPlayerMode
	{
		get
		{
			if (m_returningPlayerProgress != ReturningPlayerStatus.RPS_ACTIVE)
			{
				return m_returningPlayerProgress == ReturningPlayerStatus.RPS_ACTIVE_WITH_MANY_LOSSES;
			}
			return true;
		}
	}

	public bool SuppressOldPopups => IsInReturningPlayerMode;

	public uint AbTestGroup => m_abTestGroup;

	public long NotificationSuppressionTimeDays => m_notificationSuppressionTimeSeconds;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(Network) };
	}

	public void Shutdown()
	{
	}

	public static ReturningPlayerMgr Get()
	{
		return HearthstoneServices.Get<ReturningPlayerMgr>();
	}

	public void SetReturningPlayerInfo(ReturningPlayerInfo info)
	{
		if (info == null)
		{
			Debug.LogError("SetReturningPlayerInfo called with no ReturningPlayerInfo!");
			return;
		}
		m_returningPlayerProgress = info.Status;
		if (info.HasAbTestGroup)
		{
			m_abTestGroup = info.AbTestGroup;
		}
		m_notificationSuppressionTimeSeconds = info.NotificationSuppressionTimeDays;
	}

	public bool ShowReturningPlayerWelcomeBannerIfNeeded(WelcomeBannerCloseCallback callback)
	{
		bool result = false;
		if (ShouldShowReturningPlayerWelcomeBanner())
		{
			BannerManager.Get().ShowBanner("WoodenSign_Paint_Welcome_Back.prefab:4cb64d2b8c67feb45b4e17042d58f1ba", null, GameStrings.Get("GLUE_RETURNING_PLAYER_WELCOME_DESC"), delegate
			{
				callback();
			});
			SetSeenReturningPlayerWelcomeBanner();
			result = true;
		}
		return result;
	}

	public bool ShouldShowReturningPlayerWelcomeBanner()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN && Options.Get().GetInt(Option.RETURNING_PLAYER_BANNER_SEEN) != 1)
		{
			return IsInReturningPlayerMode;
		}
		return false;
	}

	public void SetSeenReturningPlayerWelcomeBanner()
	{
		if (Options.Get().GetInt(Option.RETURNING_PLAYER_BANNER_SEEN) != 1)
		{
			Options.Get().SetInt(Option.RETURNING_PLAYER_BANNER_SEEN, 1);
		}
	}

	public bool PlayReturningPlayerInnkeeperGreetingIfNecessary()
	{
		if (!IsInReturningPlayerMode || Options.Get().GetBool(Option.HAS_HEARD_RETURNING_PLAYER_WELCOME_BACK_VO))
		{
			return false;
		}
		SoundManager.Get().LoadAndPlay("VO_Innkeeper_Male_Dwarf_ReturningPlayers_01.prefab:cd3f8a594d06834408cb5a119aa33a21");
		Options.Get().SetBool(Option.HAS_HEARD_RETURNING_PLAYER_WELCOME_BACK_VO, val: true);
		return true;
	}

	public void Cheat_SetReturningPlayerProgress(int progress)
	{
		m_returningPlayerProgress = (ReturningPlayerStatus)progress;
	}

	public void Cheat_ResetReturningPlayer()
	{
		Options.Get().SetInt(Option.RETURNING_PLAYER_BANNER_SEEN, 0);
	}
}
