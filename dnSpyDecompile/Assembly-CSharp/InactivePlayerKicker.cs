using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
public class InactivePlayerKicker : IService, IHasUpdate
{
	// Token: 0x1700073E RID: 1854
	// (get) Token: 0x06007E05 RID: 32261 RVA: 0x0028D536 File Offset: 0x0028B736
	// (set) Token: 0x06007E06 RID: 32262 RVA: 0x0028D53E File Offset: 0x0028B73E
	public bool WasKickedForInactivity { get; private set; }

	// Token: 0x06007E07 RID: 32263 RVA: 0x0028D547 File Offset: 0x0028B747
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		serviceLocator.Get<SceneMgr>().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		serviceLocator.Get<ReconnectMgr>().OnReconnectComplete += this.OnReconnect;
		this.m_gameMgr = serviceLocator.Get<GameMgr>();
		if (HearthstoneApplication.IsInternal())
		{
			Options.Get().RegisterChangedListener(Option.IDLE_KICK_TIME, new Options.ChangedCallback(this.OnOptionChanged));
			Options.Get().RegisterChangedListener(Option.IDLE_KICKER, new Options.ChangedCallback(this.OnOptionChanged));
		}
		Processor.RegisterOnGUIDelegate(new Action(this.OnGUI));
		yield break;
	}

	// Token: 0x06007E08 RID: 32264 RVA: 0x0028D55D File Offset: 0x0028B75D
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(SceneMgr),
			typeof(ReconnectMgr),
			typeof(GameMgr)
		};
	}

	// Token: 0x06007E09 RID: 32265 RVA: 0x0028D59C File Offset: 0x0028B79C
	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= this.WillReset;
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			sceneMgr.UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		}
		ReconnectMgr reconnectMgr;
		if (HearthstoneServices.TryGet<ReconnectMgr>(out reconnectMgr))
		{
			reconnectMgr.OnReconnectComplete -= this.OnReconnect;
		}
		if (HearthstoneApplication.IsInternal())
		{
			Options.Get().UnregisterChangedListener(Option.IDLE_KICK_TIME, new Options.ChangedCallback(this.OnOptionChanged));
			Options.Get().UnregisterChangedListener(Option.IDLE_KICKER, new Options.ChangedCallback(this.OnOptionChanged));
		}
	}

	// Token: 0x06007E0A RID: 32266 RVA: 0x0028D62F File Offset: 0x0028B82F
	public void Update()
	{
		this.CheckInactivity();
	}

	// Token: 0x06007E0B RID: 32267 RVA: 0x0028D637 File Offset: 0x0028B837
	private void OnGUI()
	{
		this.CheckActivity();
	}

	// Token: 0x06007E0C RID: 32268 RVA: 0x0028D63F File Offset: 0x0028B83F
	private void WillReset()
	{
		this.SetShouldCheckForInactivity(true);
	}

	// Token: 0x06007E0D RID: 32269 RVA: 0x0028D648 File Offset: 0x0028B848
	public static InactivePlayerKicker Get()
	{
		return HearthstoneServices.Get<InactivePlayerKicker>();
	}

	// Token: 0x06007E0E RID: 32270 RVA: 0x0028D64F File Offset: 0x0028B84F
	public void OnLoggedIn()
	{
		this.UpdateIdleKickTimeOption();
		this.UpdateCheckForInactivity();
	}

	// Token: 0x06007E0F RID: 32271 RVA: 0x0028D65D File Offset: 0x0028B85D
	private void OnReconnect()
	{
		this.SetShouldCheckForInactivity(true);
		this.WasKickedForInactivity = false;
	}

	// Token: 0x06007E10 RID: 32272 RVA: 0x0028D66D File Offset: 0x0028B86D
	public bool IsCheckingForInactivity()
	{
		return this.m_checkingForInactivity;
	}

	// Token: 0x06007E11 RID: 32273 RVA: 0x0028D675 File Offset: 0x0028B875
	public bool ShouldCheckForInactivity()
	{
		return this.m_shouldCheckForInactivity;
	}

	// Token: 0x06007E12 RID: 32274 RVA: 0x0028D67D File Offset: 0x0028B87D
	public void SetShouldCheckForInactivity(bool check)
	{
		if (this.m_shouldCheckForInactivity == check)
		{
			return;
		}
		this.m_shouldCheckForInactivity = check;
		this.UpdateCheckForInactivity();
	}

	// Token: 0x06007E13 RID: 32275 RVA: 0x0028D696 File Offset: 0x0028B896
	public float GetKickSec()
	{
		return this.m_kickSec;
	}

	// Token: 0x06007E14 RID: 32276 RVA: 0x0028D69E File Offset: 0x0028B89E
	public void SetKickSec(float sec)
	{
		this.m_kickSec = sec;
	}

	// Token: 0x06007E15 RID: 32277 RVA: 0x0028D6A8 File Offset: 0x0028B8A8
	public bool SetKickTimeStr(string timeStr)
	{
		float kickSec;
		if (!TimeUtils.TryParseDevSecFromElapsedTimeString(timeStr, out kickSec))
		{
			return false;
		}
		this.SetKickSec(kickSec);
		return true;
	}

	// Token: 0x06007E16 RID: 32278 RVA: 0x0028D6C9 File Offset: 0x0028B8C9
	private bool CanCheckForInactivity()
	{
		return !DemoMgr.Get().IsExpoDemo() && this.m_shouldCheckForInactivity && (!HearthstoneApplication.IsInternal() || Options.Get().GetBool(Option.IDLE_KICKER));
	}

	// Token: 0x06007E17 RID: 32279 RVA: 0x0028D6FC File Offset: 0x0028B8FC
	private void UpdateCheckForInactivity()
	{
		bool checkingForInactivity = this.m_checkingForInactivity;
		this.m_checkingForInactivity = this.CanCheckForInactivity();
		if (this.m_checkingForInactivity && !checkingForInactivity)
		{
			this.StartCheckForInactivity();
		}
	}

	// Token: 0x06007E18 RID: 32280 RVA: 0x0028D72D File Offset: 0x0028B92D
	private void StartCheckForInactivity()
	{
		this.m_activityDetected = false;
		this.m_inactivityStartTimestamp = Time.realtimeSinceStartup;
	}

	// Token: 0x06007E19 RID: 32281 RVA: 0x0028D744 File Offset: 0x0028B944
	private void CheckActivity()
	{
		if (!this.IsCheckingForInactivity())
		{
			return;
		}
		EventType type = Event.current.type;
		if (type <= EventType.MouseUp || type - EventType.MouseDrag <= 3)
		{
			this.m_activityDetected = true;
			return;
		}
		if (this.m_gameMgr.IsSpectator())
		{
			this.m_activityDetected = true;
		}
	}

	// Token: 0x06007E1A RID: 32282 RVA: 0x0028D78C File Offset: 0x0028B98C
	private void CheckInactivity()
	{
		if (!this.IsCheckingForInactivity())
		{
			return;
		}
		if (this.m_activityDetected)
		{
			this.m_inactivityStartTimestamp = Time.realtimeSinceStartup;
			this.m_activityDetected = false;
			ReconnectMgr.Get().ReconnectBlockedByInactivity = false;
			return;
		}
		if (Time.realtimeSinceStartup - this.m_inactivityStartTimestamp >= this.m_kickSec)
		{
			if (!Network.IsLoggedIn())
			{
				return;
			}
			Error.AddFatal(FatalErrorReason.INACTIVITY_TIMEOUT, "GLOBAL_ERROR_INACTIVITY_KICK", Array.Empty<object>());
			ReconnectMgr.Get().ReconnectBlockedByInactivity = true;
			this.WasKickedForInactivity = true;
		}
	}

	// Token: 0x06007E1B RID: 32283 RVA: 0x0028D806 File Offset: 0x0028BA06
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FATAL_ERROR)
		{
			this.SetShouldCheckForInactivity(false);
		}
	}

	// Token: 0x06007E1C RID: 32284 RVA: 0x0028D81D File Offset: 0x0028BA1D
	private void UpdateIdleKickTimeOption()
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return;
		}
		this.SetKickTimeStr(Options.Get().GetString(Option.IDLE_KICK_TIME));
	}

	// Token: 0x06007E1D RID: 32285 RVA: 0x0028D83A File Offset: 0x0028BA3A
	private void OnOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		if (option == Option.IDLE_KICKER)
		{
			this.UpdateCheckForInactivity();
			return;
		}
		if (option != Option.IDLE_KICK_TIME)
		{
			Error.AddDevFatal("InactivePlayerKicker.OnOptionChanged() - unhandled option {0}", new object[]
			{
				option
			});
			return;
		}
		this.UpdateIdleKickTimeOption();
	}

	// Token: 0x040065F4 RID: 26100
	private const float DEFAULT_KICK_SEC = 1800f;

	// Token: 0x040065F6 RID: 26102
	private bool m_checkingForInactivity;

	// Token: 0x040065F7 RID: 26103
	private bool m_shouldCheckForInactivity = true;

	// Token: 0x040065F8 RID: 26104
	private float m_kickSec = 1800f;

	// Token: 0x040065F9 RID: 26105
	private bool m_activityDetected;

	// Token: 0x040065FA RID: 26106
	private float m_inactivityStartTimestamp;

	// Token: 0x040065FB RID: 26107
	private GameMgr m_gameMgr;
}
