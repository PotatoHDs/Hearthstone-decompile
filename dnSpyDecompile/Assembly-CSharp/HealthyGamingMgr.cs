using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020008D2 RID: 2258
public class HealthyGamingMgr : IService, IHasUpdate
{
	// Token: 0x1700070C RID: 1804
	// (get) Token: 0x06007CDF RID: 31967 RVA: 0x00289C3A File Offset: 0x00287E3A
	private float DEBUG_TIMESCALE
	{
		get
		{
			return Vars.Key("CAIS.DebugTimescale").GetFloat(1f);
		}
	}

	// Token: 0x1700070D RID: 1805
	// (get) Token: 0x06007CE0 RID: 31968 RVA: 0x00289C50 File Offset: 0x00287E50
	private string DEBUG_TIMESCALE_LOGMSG
	{
		get
		{
			if (this.DEBUG_TIMESCALE != 1f)
			{
				return string.Format("[CAIS.DebugTimescale={0}]", this.DEBUG_TIMESCALE);
			}
			return string.Empty;
		}
	}

	// Token: 0x1700070E RID: 1806
	// (get) Token: 0x06007CE1 RID: 31969 RVA: 0x00289C7A File Offset: 0x00287E7A
	private float PLAYTIME_CHECK_FREQUENCY_SECONDS
	{
		get
		{
			return Mathf.Max(1f, this.DEBUG_TIMESCALE * Vars.Key("CAIS.PlayTimeCheckFrequencySeconds").GetFloat(300f));
		}
	}

	// Token: 0x1700070F RID: 1807
	// (get) Token: 0x06007CE2 RID: 31970 RVA: 0x00289CA1 File Offset: 0x00287EA1
	private float INITIAL_PLAYTIME_CHECK_FREQUENCY_SECONDS
	{
		get
		{
			return Mathf.Max(1f, this.DEBUG_TIMESCALE * Vars.Key("CAIS.InitialPlayTimeCheckFrequencySeconds").GetFloat(45f));
		}
	}

	// Token: 0x17000710 RID: 1808
	// (get) Token: 0x06007CE3 RID: 31971 RVA: 0x00289CC8 File Offset: 0x00287EC8
	private float CHINA_CAIS_ACTIVE_DISPLAY_TIME_SECONDS
	{
		get
		{
			return Mathf.Max(1f, this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaCAISActiveDisplayTimeSeconds").GetFloat(60f));
		}
	}

	// Token: 0x17000711 RID: 1809
	// (get) Token: 0x06007CE4 RID: 31972 RVA: 0x00289CEF File Offset: 0x00287EEF
	private int CHINA_FEATURES_LOCKOUT_THRESHOLD_MINUTES
	{
		get
		{
			return Mathf.RoundToInt(this.DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaFeaturesLockoutThresholdMinutes").GetInt(180));
		}
	}

	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x06007CE5 RID: 31973 RVA: 0x00289D12 File Offset: 0x00287F12
	private int CHINA_FIRST_MESSAGE_THRESHOLD_MINUTES
	{
		get
		{
			return Mathf.RoundToInt(this.DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaFirstMessageThresholdMinutes").GetInt(60));
		}
	}

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x06007CE6 RID: 31974 RVA: 0x00289D32 File Offset: 0x00287F32
	private float CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES
	{
		get
		{
			return this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaFirstMessageFrequencyMinutes").GetFloat(60f);
		}
	}

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x06007CE7 RID: 31975 RVA: 0x00289D4F File Offset: 0x00287F4F
	public float CHINA_FIRST_MESSAGE_DISPLAY_TIME_SECONDS
	{
		get
		{
			return Mathf.Clamp(this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaFirstMessageDisplayTimeSeconds").GetFloat(60f), 1f, this.CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES * 60f);
		}
	}

	// Token: 0x17000715 RID: 1813
	// (get) Token: 0x06007CE8 RID: 31976 RVA: 0x00289D82 File Offset: 0x00287F82
	private int CHINA_SECOND_MESSAGE_THRESHOLD_MINUTES
	{
		get
		{
			return Mathf.RoundToInt(this.DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaSecondMessageThresholdMinutes").GetInt(180));
		}
	}

	// Token: 0x17000716 RID: 1814
	// (get) Token: 0x06007CE9 RID: 31977 RVA: 0x00289DA5 File Offset: 0x00287FA5
	private float CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES
	{
		get
		{
			return this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaSecondMessageFrequencyMinutes").GetFloat(30f);
		}
	}

	// Token: 0x17000717 RID: 1815
	// (get) Token: 0x06007CEA RID: 31978 RVA: 0x00289DC2 File Offset: 0x00287FC2
	public float CHINA_SECOND_MESSAGE_DISPLAY_TIME_SECONDS
	{
		get
		{
			return Mathf.Clamp(this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaSecondMessageDisplayTimeSeconds").GetFloat(60f), 1f, this.CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES * 60f);
		}
	}

	// Token: 0x17000718 RID: 1816
	// (get) Token: 0x06007CEB RID: 31979 RVA: 0x00289DF5 File Offset: 0x00287FF5
	private int CHINA_THIRD_MESSAGE_THRESHOLD_MINUTES
	{
		get
		{
			return Mathf.RoundToInt(this.DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaThirdMessageThresholdMinutes").GetInt(300));
		}
	}

	// Token: 0x17000719 RID: 1817
	// (get) Token: 0x06007CEC RID: 31980 RVA: 0x00289E18 File Offset: 0x00288018
	private float CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES
	{
		get
		{
			return this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaThirdMessageFrequencyMinutes").GetFloat(15f);
		}
	}

	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x06007CED RID: 31981 RVA: 0x00289E35 File Offset: 0x00288035
	public float CHINA_THIRD_MESSAGE_DISPLAY_TIME_SECONDS
	{
		get
		{
			return Mathf.Clamp(this.DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaThirdMessageDisplayTimeSeconds").GetFloat(60f), 1f, this.CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES * 60f);
		}
	}

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x06007CEE RID: 31982 RVA: 0x00289E68 File Offset: 0x00288068
	private float KOREA_MESSAGE_FREQUENCY_MINUTES
	{
		get
		{
			return this.DEBUG_TIMESCALE * Vars.Key("CAIS.KoreaMessageFrequencyMinutes").GetFloat(60f);
		}
	}

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x06007CEF RID: 31983 RVA: 0x00289E85 File Offset: 0x00288085
	private float KOREA_MESSAGE_DISPLAY_TIME_SECONDS
	{
		get
		{
			return Mathf.Clamp(this.DEBUG_TIMESCALE * Vars.Key("CAIS.KoreaMessageDisplayTimeSeconds").GetFloat(60f), 1f, this.KOREA_MESSAGE_FREQUENCY_MINUTES * 60f);
		}
	}

	// Token: 0x06007CF0 RID: 31984 RVA: 0x00289EB8 File Offset: 0x002880B8
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (Options.Get().GetBool(Option.HEALTHY_GAMING_DEBUG, false))
		{
			this.m_DebugMode = true;
		}
		this.m_NextCheckTime = Time.realtimeSinceStartup + this.INITIAL_PLAYTIME_CHECK_FREQUENCY_SECONDS;
		HearthstoneApplication.Get().WillReset += this.WillReset;
		HearthstoneApplication.Get().Resetting += this.OnReset;
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		Processor.RunCoroutine(this.InitNetworkData(), this);
		this.m_UpdateEnabled = true;
		yield break;
	}

	// Token: 0x06007CF1 RID: 31985 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06007CF2 RID: 31986 RVA: 0x00289EC8 File Offset: 0x002880C8
	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= this.WillReset;
		HearthstoneApplication.Get().Resetting -= this.OnReset;
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x06007CF3 RID: 31987 RVA: 0x00289F18 File Offset: 0x00288118
	public void Update()
	{
		if (!this.m_UpdateEnabled)
		{
			return;
		}
		if (!this.m_NetworkDataReady)
		{
			return;
		}
		if (Time.realtimeSinceStartup < this.m_NextCheckTime)
		{
			return;
		}
		this.m_NextCheckTime = Time.realtimeSinceStartup + this.PLAYTIME_CHECK_FREQUENCY_SECONDS;
		string accountCountry = this.m_AccountCountry;
		if (accountCountry == "CHN")
		{
			this.ChinaRestrictions();
			return;
		}
		if (!(accountCountry == "KOR"))
		{
			this.m_UpdateEnabled = false;
			return;
		}
		this.KoreaRestrictions();
	}

	// Token: 0x06007CF4 RID: 31988 RVA: 0x00289F8F File Offset: 0x0028818F
	public static HealthyGamingMgr Get()
	{
		return HearthstoneServices.Get<HealthyGamingMgr>();
	}

	// Token: 0x06007CF5 RID: 31989 RVA: 0x00289F96 File Offset: 0x00288196
	public void OnLoggedIn()
	{
		this.m_BattleNetReady = true;
	}

	// Token: 0x06007CF6 RID: 31990 RVA: 0x00289F9F File Offset: 0x0028819F
	public bool isArenaEnabled()
	{
		return this.m_HealthyGamingArenaEnabled;
	}

	// Token: 0x06007CF7 RID: 31991 RVA: 0x00289FA7 File Offset: 0x002881A7
	public ulong GetSessionStartTime()
	{
		return this.m_SessionStartTime;
	}

	// Token: 0x06007CF8 RID: 31992 RVA: 0x00289FAF File Offset: 0x002881AF
	private void WillReset()
	{
		this.StopCoroutinesAndResetState();
	}

	// Token: 0x06007CF9 RID: 31993 RVA: 0x00289FB7 File Offset: 0x002881B7
	private void OnReset()
	{
		Processor.RunCoroutine(this.InitNetworkData(), this);
	}

	// Token: 0x06007CFA RID: 31994 RVA: 0x00289FAF File Offset: 0x002881AF
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.StopCoroutinesAndResetState();
	}

	// Token: 0x06007CFB RID: 31995 RVA: 0x00289FC6 File Offset: 0x002881C6
	private void StopCoroutinesAndResetState()
	{
		this.m_BattleNetReady = false;
		this.m_NetworkDataReady = false;
		Processor.StopAllCoroutinesWithObjectRef(this);
	}

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x06007CFC RID: 31996 RVA: 0x00289FDC File Offset: 0x002881DC
	private bool IsInitializationReady
	{
		get
		{
			return this.m_BattleNetReady && NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() != null;
		}
	}

	// Token: 0x06007CFD RID: 31997 RVA: 0x00289FF7 File Offset: 0x002881F7
	private IEnumerator InitNetworkData()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			yield break;
		}
		while (!this.IsInitializationReady)
		{
			yield return null;
		}
		this.m_AccountCountry = BattleNet.GetAccountCountry();
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		bool caisEnabled = false;
		if (this.m_AccountCountry == "CHN" || this.m_AccountCountry == "KOR")
		{
			if (PlatformSettings.IsMobile())
			{
				if (this.m_AccountCountry == "CHN")
				{
					caisEnabled = netObject.CaisEnabledMobileChina;
				}
				else if (this.m_AccountCountry == "KOR")
				{
					caisEnabled = netObject.CaisEnabledMobileSouthKorea;
				}
			}
			else
			{
				caisEnabled = netObject.CaisEnabledNonMobile;
			}
		}
		this.m_UpdateEnabled = caisEnabled;
		this.m_Restrictions = default(Lockouts);
		BattleNet.GetPlayRestrictions(ref this.m_Restrictions, true);
		while (!this.m_Restrictions.loaded)
		{
			BattleNet.GetPlayRestrictions(ref this.m_Restrictions, false);
			yield return null;
		}
		this.m_SessionStartTime = this.m_Restrictions.sessionStartTime;
		this.m_PlayedMinutes = this.m_Restrictions.CAISplayed;
		this.m_RestedMinutes = this.m_Restrictions.CAISrested;
		if (this.m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] Healthy Gaming Debug Logging ON", Array.Empty<object>());
			Debug.LogFormat("[HealthyGaming] Account Region: " + BattleNet.GetAccountRegion().ToString(), Array.Empty<object>());
			Debug.LogFormat("[HealthyGaming] Current Region: " + BattleNet.GetCurrentRegion().ToString(), Array.Empty<object>());
		}
		Debug.LogFormat("[HealthyGaming] CAIS ServerEnabledForPlatform={0} AccountCAISForChina={1} Country={2} TimeSec={3} PlayedTimeMin={4} RestedTimeMin={5} SessionStartTime={6}", new object[]
		{
			caisEnabled,
			this.m_Restrictions.CAISactive,
			this.m_AccountCountry,
			Time.realtimeSinceStartup,
			this.m_PlayedMinutes,
			this.m_RestedMinutes,
			this.m_SessionStartTime
		});
		if (!this.m_Restrictions.CAISactive && this.m_AccountCountry == "CHN")
		{
			Debug.LogFormat("[HealthyGaming] Healthy Gaming Deactivated: account not set for CAIS.", Array.Empty<object>());
			this.m_UpdateEnabled = false;
			yield break;
		}
		if (caisEnabled)
		{
			Debug.LogFormat("[HealthyGaming] Healthy Gaming Active!", Array.Empty<object>());
		}
		if (this.m_AccountCountry == "KOR")
		{
			this.m_NextMessageDisplayTime = Time.realtimeSinceStartup + this.KOREA_MESSAGE_FREQUENCY_MINUTES * 60f;
		}
		if (this.m_AccountCountry == "CHN")
		{
			string text = "GLOBAL_HEALTHY_GAMING_CHINA_CAIS_ACTIVE";
			if (UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
			{
				text += "_PHONE";
			}
			string textArg = GameStrings.Get(text);
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, textArg, SocialToastMgr.TOAST_TYPE.DEFAULT, this.CHINA_CAIS_ACTIVE_DISPLAY_TIME_SECONDS);
			this.m_NextMessageDisplayTime = -2f;
		}
		this.m_NetworkDataReady = true;
		yield break;
	}

	// Token: 0x06007CFE RID: 31998 RVA: 0x0028A008 File Offset: 0x00288208
	private void KoreaRestrictions()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (this.m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] Minutes Played: " + realtimeSinceStartup / 60f, Array.Empty<object>());
		}
		if (realtimeSinceStartup < this.m_NextMessageDisplayTime)
		{
			return;
		}
		this.m_NextMessageDisplayTime += this.KOREA_MESSAGE_FREQUENCY_MINUTES * 60f;
		int num = (int)(realtimeSinceStartup / 60f) / 60;
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, GameStrings.Format("GLOBAL_HEALTHY_GAMING_TOAST", new object[]
		{
			num
		}), SocialToastMgr.TOAST_TYPE.DEFAULT, this.KOREA_MESSAGE_DISPLAY_TIME_SECONDS);
	}

	// Token: 0x06007CFF RID: 31999 RVA: 0x0028A09E File Offset: 0x0028829E
	private void ChinaRestrictions()
	{
		BattleNet.GetPlayRestrictions(ref this.m_Restrictions, true);
		Processor.RunCoroutine(this.ChinaRestrictionsUpdate(), this);
	}

	// Token: 0x06007D00 RID: 32000 RVA: 0x0028A0B9 File Offset: 0x002882B9
	private IEnumerator ChinaRestrictionsUpdate()
	{
		while (!this.m_Restrictions.loaded)
		{
			BattleNet.GetPlayRestrictions(ref this.m_Restrictions, false);
			yield return null;
		}
		this.m_PlayedMinutes = this.m_Restrictions.CAISplayed;
		this.m_RestedMinutes = this.m_Restrictions.CAISrested;
		int minutesPlayed = this.m_PlayedMinutes;
		if (this.m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] CAIS Time Played: {0} min    Rested: {1} min", new object[]
			{
				this.m_PlayedMinutes.ToString(),
				this.m_RestedMinutes.ToString()
			});
			Debug.LogFormat("[HealthyGaming] CAIS Minutes Played: {0} min", new object[]
			{
				minutesPlayed
			});
		}
		if (this.m_NextMessageDisplayTime == -2f)
		{
			yield return new WaitForSeconds(this.CHINA_CAIS_ACTIVE_DISPLAY_TIME_SECONDS);
			this.m_NextMessageDisplayTime = -1f;
		}
		if ((float)minutesPlayed < this.m_NextMessageDisplayTime && this.m_NextMessageDisplayTime > 0f)
		{
			yield break;
		}
		if (minutesPlayed >= this.CHINA_FEATURES_LOCKOUT_THRESHOLD_MINUTES)
		{
			this.ChinaRestrictions_LockoutFeatures(minutesPlayed);
		}
		if (minutesPlayed >= this.CHINA_FIRST_MESSAGE_THRESHOLD_MINUTES && minutesPlayed < this.CHINA_SECOND_MESSAGE_THRESHOLD_MINUTES)
		{
			this.ChinaRestrictions_LessThan3Hours(minutesPlayed);
		}
		if (minutesPlayed >= this.CHINA_SECOND_MESSAGE_THRESHOLD_MINUTES && minutesPlayed <= this.CHINA_THIRD_MESSAGE_THRESHOLD_MINUTES)
		{
			this.ChinaRestrictions_3to5Hours(minutesPlayed);
		}
		if (minutesPlayed > this.CHINA_THIRD_MESSAGE_THRESHOLD_MINUTES)
		{
			this.ChinaRestrictions_MoreThan5Hours(minutesPlayed);
		}
		yield break;
	}

	// Token: 0x06007D01 RID: 32001 RVA: 0x0028A0C8 File Offset: 0x002882C8
	private void ChinaRestrictions_LessThan3Hours(int minutesPlayed)
	{
		if (this.m_NextMessageDisplayTime < 0f)
		{
			this.m_NextMessageDisplayTime = (float)this.m_PlayedMinutes + (this.CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES - (float)minutesPlayed % this.CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES);
		}
		else
		{
			this.m_NextMessageDisplayTime = (float)this.m_PlayedMinutes + this.CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES;
		}
		string text = "GLOBAL_HEALTHY_GAMING_CHINA_LESS_THAN_THREE_HOURS";
		if (UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
		{
			text += "_PHONE";
		}
		int num = minutesPlayed / 60;
		string text2 = GameStrings.Format(text, new object[]
		{
			num
		});
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, text2, SocialToastMgr.TOAST_TYPE.DEFAULT, this.CHINA_FIRST_MESSAGE_DISPLAY_TIME_SECONDS);
		if (this.m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] GLOBAL_HEALTHY_GAMING_CHINA_LESS_THAN_THREE_HOURS: {0} minutes {1}", new object[]
			{
				minutesPlayed,
				this.DEBUG_TIMESCALE_LOGMSG
			});
			Debug.LogFormat("[HealthyGaming] First message: {0}", new object[]
			{
				GameStrings.Format("GLOBAL_HEALTHY_GAMING_CHINA_LESS_THAN_THREE_HOURS", new object[]
				{
					num
				})
			});
			Debug.LogFormat("[HealthyGaming] NextMessageDisplayTime: " + this.m_NextMessageDisplayTime.ToString(), Array.Empty<object>());
			return;
		}
		Debug.LogFormat("[HealthyGaming] Time: {0} sec,  Played: {1} min,  First message: {2}", new object[]
		{
			Time.realtimeSinceStartup,
			minutesPlayed,
			text2
		});
	}

	// Token: 0x06007D02 RID: 32002 RVA: 0x0028A218 File Offset: 0x00288418
	private void ChinaRestrictions_3to5Hours(int minutesPlayed)
	{
		if (this.m_NextMessageDisplayTime < 0f)
		{
			this.m_NextMessageDisplayTime = (float)this.m_PlayedMinutes + (this.CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES - (float)minutesPlayed % this.CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES);
		}
		else
		{
			this.m_NextMessageDisplayTime = (float)this.m_PlayedMinutes + this.CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES;
		}
		string text = "GLOBAL_HEALTHY_GAMING_CHINA_THREE_TO_FIVE_HOURS";
		if (UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
		{
			text += "_PHONE";
		}
		string text2 = GameStrings.Get(text);
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, text2, SocialToastMgr.TOAST_TYPE.DEFAULT, this.CHINA_SECOND_MESSAGE_DISPLAY_TIME_SECONDS);
		if (this.m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] GLOBAL_HEALTHY_GAMING_CHINA_THREE_TO_FIVE_HOURS: {0} minutes {1}", new object[]
			{
				minutesPlayed,
				this.DEBUG_TIMESCALE_LOGMSG
			});
			Debug.LogFormat("[HealthyGaming] Second message: {0}", new object[]
			{
				GameStrings.Get("GLOBAL_HEALTHY_GAMING_CHINA_THREE_TO_FIVE_HOURS")
			});
			Debug.LogFormat("[HealthyGaming] NextMessageDisplayTime: " + this.m_NextMessageDisplayTime.ToString(), Array.Empty<object>());
			return;
		}
		Debug.LogFormat("[HealthyGaming] Time: {0} sec,  Played: {1} min,  Second message: {2}", new object[]
		{
			Time.realtimeSinceStartup,
			minutesPlayed,
			text2
		});
	}

	// Token: 0x06007D03 RID: 32003 RVA: 0x0028A344 File Offset: 0x00288544
	private void ChinaRestrictions_MoreThan5Hours(int minutesPlayed)
	{
		if (this.m_NextMessageDisplayTime < 0f)
		{
			this.m_NextMessageDisplayTime = (float)this.m_PlayedMinutes + (this.CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES - (float)minutesPlayed % this.CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES);
		}
		else
		{
			this.m_NextMessageDisplayTime = (float)this.m_PlayedMinutes + this.CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES;
		}
		string text = "GLOBAL_HEALTHY_GAMING_CHINA_MORE_THAN_FIVE_HOURS";
		if (UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
		{
			text += "_PHONE";
		}
		string text2 = GameStrings.Get(text);
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, text2, SocialToastMgr.TOAST_TYPE.DEFAULT, this.CHINA_THIRD_MESSAGE_DISPLAY_TIME_SECONDS);
		if (this.m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] GLOBAL_HEALTHY_GAMING_CHINA_MORE_THAN_FIVE_HOURS: {0} minutes {1}", new object[]
			{
				minutesPlayed,
				this.DEBUG_TIMESCALE_LOGMSG
			});
			Debug.LogFormat("[HealthyGaming] Third message: {0}", new object[]
			{
				GameStrings.Get("GLOBAL_HEALTHY_GAMING_CHINA_MORE_THAN_FIVE_HOURS")
			});
			Debug.LogFormat("[HealthyGaming] NextMessageDisplayTime: " + this.m_NextMessageDisplayTime.ToString(), Array.Empty<object>());
			return;
		}
		Debug.LogFormat("[HealthyGaming] Time: {0} sec,  Played: {1} min,  Third message: {2}", new object[]
		{
			Time.realtimeSinceStartup,
			minutesPlayed,
			text2
		});
	}

	// Token: 0x06007D04 RID: 32004 RVA: 0x0028A470 File Offset: 0x00288670
	private void ChinaRestrictions_LockoutFeatures(int minutesPlayed)
	{
		this.m_HealthyGamingArenaEnabled = false;
		Box box = Box.Get();
		if (box != null)
		{
			box.UpdateUI(false);
		}
	}

	// Token: 0x04006578 RID: 25976
	private bool m_BattleNetReady;

	// Token: 0x04006579 RID: 25977
	private string m_AccountCountry = string.Empty;

	// Token: 0x0400657A RID: 25978
	private Lockouts m_Restrictions;

	// Token: 0x0400657B RID: 25979
	private bool m_NetworkDataReady;

	// Token: 0x0400657C RID: 25980
	private float m_NextCheckTime;

	// Token: 0x0400657D RID: 25981
	private float m_NextMessageDisplayTime;

	// Token: 0x0400657E RID: 25982
	private int m_PlayedMinutes;

	// Token: 0x0400657F RID: 25983
	private int m_RestedMinutes;

	// Token: 0x04006580 RID: 25984
	private ulong m_SessionStartTime;

	// Token: 0x04006581 RID: 25985
	private bool m_DebugMode;

	// Token: 0x04006582 RID: 25986
	private bool m_HealthyGamingArenaEnabled = true;

	// Token: 0x04006583 RID: 25987
	private bool m_UpdateEnabled;
}
