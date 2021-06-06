using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000653 RID: 1619
public class ReturningPlayerMgr : IService
{
	// Token: 0x06005B89 RID: 23433 RVA: 0x001DD56B File Offset: 0x001DB76B
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06005B8A RID: 23434 RVA: 0x001B7846 File Offset: 0x001B5A46
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network)
		};
	}

	// Token: 0x06005B8B RID: 23435 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06005B8C RID: 23436 RVA: 0x001DD573 File Offset: 0x001DB773
	public static ReturningPlayerMgr Get()
	{
		return HearthstoneServices.Get<ReturningPlayerMgr>();
	}

	// Token: 0x06005B8D RID: 23437 RVA: 0x001DD57A File Offset: 0x001DB77A
	public void SetReturningPlayerInfo(ReturningPlayerInfo info)
	{
		if (info == null)
		{
			Debug.LogError("SetReturningPlayerInfo called with no ReturningPlayerInfo!");
			return;
		}
		this.m_returningPlayerProgress = info.Status;
		if (info.HasAbTestGroup)
		{
			this.m_abTestGroup = info.AbTestGroup;
		}
		this.m_notificationSuppressionTimeSeconds = info.NotificationSuppressionTimeDays;
	}

	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x06005B8E RID: 23438 RVA: 0x001DD5B6 File Offset: 0x001DB7B6
	public bool IsInReturningPlayerMode
	{
		get
		{
			return this.m_returningPlayerProgress == ReturningPlayerStatus.RPS_ACTIVE || this.m_returningPlayerProgress == ReturningPlayerStatus.RPS_ACTIVE_WITH_MANY_LOSSES;
		}
	}

	// Token: 0x17000558 RID: 1368
	// (get) Token: 0x06005B8F RID: 23439 RVA: 0x001DD5CC File Offset: 0x001DB7CC
	public bool SuppressOldPopups
	{
		get
		{
			return this.IsInReturningPlayerMode;
		}
	}

	// Token: 0x17000559 RID: 1369
	// (get) Token: 0x06005B90 RID: 23440 RVA: 0x001DD5D4 File Offset: 0x001DB7D4
	public uint AbTestGroup
	{
		get
		{
			return this.m_abTestGroup;
		}
	}

	// Token: 0x1700055A RID: 1370
	// (get) Token: 0x06005B91 RID: 23441 RVA: 0x001DD5DC File Offset: 0x001DB7DC
	public long NotificationSuppressionTimeDays
	{
		get
		{
			return this.m_notificationSuppressionTimeSeconds;
		}
	}

	// Token: 0x06005B92 RID: 23442 RVA: 0x001DD5E4 File Offset: 0x001DB7E4
	public bool ShowReturningPlayerWelcomeBannerIfNeeded(ReturningPlayerMgr.WelcomeBannerCloseCallback callback)
	{
		bool result = false;
		if (this.ShouldShowReturningPlayerWelcomeBanner())
		{
			BannerManager.Get().ShowBanner("WoodenSign_Paint_Welcome_Back.prefab:4cb64d2b8c67feb45b4e17042d58f1ba", null, GameStrings.Get("GLUE_RETURNING_PLAYER_WELCOME_DESC"), delegate()
			{
				callback();
			}, null);
			this.SetSeenReturningPlayerWelcomeBanner();
			result = true;
		}
		return result;
	}

	// Token: 0x06005B93 RID: 23443 RVA: 0x001DD639 File Offset: 0x001DB839
	public bool ShouldShowReturningPlayerWelcomeBanner()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN && Options.Get().GetInt(Option.RETURNING_PLAYER_BANNER_SEEN) != 1 && this.IsInReturningPlayerMode;
	}

	// Token: 0x06005B94 RID: 23444 RVA: 0x001DD662 File Offset: 0x001DB862
	public void SetSeenReturningPlayerWelcomeBanner()
	{
		if (Options.Get().GetInt(Option.RETURNING_PLAYER_BANNER_SEEN) != 1)
		{
			Options.Get().SetInt(Option.RETURNING_PLAYER_BANNER_SEEN, 1);
		}
	}

	// Token: 0x06005B95 RID: 23445 RVA: 0x001DD688 File Offset: 0x001DB888
	public bool PlayReturningPlayerInnkeeperGreetingIfNecessary()
	{
		if (!this.IsInReturningPlayerMode || Options.Get().GetBool(Option.HAS_HEARD_RETURNING_PLAYER_WELCOME_BACK_VO))
		{
			return false;
		}
		SoundManager.Get().LoadAndPlay("VO_Innkeeper_Male_Dwarf_ReturningPlayers_01.prefab:cd3f8a594d06834408cb5a119aa33a21");
		Options.Get().SetBool(Option.HAS_HEARD_RETURNING_PLAYER_WELCOME_BACK_VO, true);
		return true;
	}

	// Token: 0x06005B96 RID: 23446 RVA: 0x001DD6D5 File Offset: 0x001DB8D5
	public void Cheat_SetReturningPlayerProgress(int progress)
	{
		this.m_returningPlayerProgress = (ReturningPlayerStatus)progress;
	}

	// Token: 0x06005B97 RID: 23447 RVA: 0x001DD6DE File Offset: 0x001DB8DE
	public void Cheat_ResetReturningPlayer()
	{
		Options.Get().SetInt(Option.RETURNING_PLAYER_BANNER_SEEN, 0);
	}

	// Token: 0x04004E35 RID: 20021
	private const int RETURNING_PLAYER_BANNER_ID = 1;

	// Token: 0x04004E36 RID: 20022
	private ReturningPlayerStatus m_returningPlayerProgress = ReturningPlayerStatus.RPS_NOT_RETURNING_PLAYER;

	// Token: 0x04004E37 RID: 20023
	private uint m_abTestGroup;

	// Token: 0x04004E38 RID: 20024
	private long m_notificationSuppressionTimeSeconds;

	// Token: 0x02002173 RID: 8563
	// (Invoke) Token: 0x06012386 RID: 74630
	public delegate void WelcomeBannerCloseCallback();
}
