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

public class HealthyGamingMgr : IService, IHasUpdate
{
	private bool m_BattleNetReady;

	private string m_AccountCountry = string.Empty;

	private Lockouts m_Restrictions;

	private bool m_NetworkDataReady;

	private float m_NextCheckTime;

	private float m_NextMessageDisplayTime;

	private int m_PlayedMinutes;

	private int m_RestedMinutes;

	private ulong m_SessionStartTime;

	private bool m_DebugMode;

	private bool m_HealthyGamingArenaEnabled = true;

	private bool m_UpdateEnabled;

	private float DEBUG_TIMESCALE => Vars.Key("CAIS.DebugTimescale").GetFloat(1f);

	private string DEBUG_TIMESCALE_LOGMSG
	{
		get
		{
			if (DEBUG_TIMESCALE != 1f)
			{
				return $"[CAIS.DebugTimescale={DEBUG_TIMESCALE}]";
			}
			return string.Empty;
		}
	}

	private float PLAYTIME_CHECK_FREQUENCY_SECONDS => Mathf.Max(1f, DEBUG_TIMESCALE * Vars.Key("CAIS.PlayTimeCheckFrequencySeconds").GetFloat(300f));

	private float INITIAL_PLAYTIME_CHECK_FREQUENCY_SECONDS => Mathf.Max(1f, DEBUG_TIMESCALE * Vars.Key("CAIS.InitialPlayTimeCheckFrequencySeconds").GetFloat(45f));

	private float CHINA_CAIS_ACTIVE_DISPLAY_TIME_SECONDS => Mathf.Max(1f, DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaCAISActiveDisplayTimeSeconds").GetFloat(60f));

	private int CHINA_FEATURES_LOCKOUT_THRESHOLD_MINUTES => Mathf.RoundToInt(DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaFeaturesLockoutThresholdMinutes").GetInt(180));

	private int CHINA_FIRST_MESSAGE_THRESHOLD_MINUTES => Mathf.RoundToInt(DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaFirstMessageThresholdMinutes").GetInt(60));

	private float CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES => DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaFirstMessageFrequencyMinutes").GetFloat(60f);

	public float CHINA_FIRST_MESSAGE_DISPLAY_TIME_SECONDS => Mathf.Clamp(DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaFirstMessageDisplayTimeSeconds").GetFloat(60f), 1f, CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES * 60f);

	private int CHINA_SECOND_MESSAGE_THRESHOLD_MINUTES => Mathf.RoundToInt(DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaSecondMessageThresholdMinutes").GetInt(180));

	private float CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES => DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaSecondMessageFrequencyMinutes").GetFloat(30f);

	public float CHINA_SECOND_MESSAGE_DISPLAY_TIME_SECONDS => Mathf.Clamp(DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaSecondMessageDisplayTimeSeconds").GetFloat(60f), 1f, CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES * 60f);

	private int CHINA_THIRD_MESSAGE_THRESHOLD_MINUTES => Mathf.RoundToInt(DEBUG_TIMESCALE * (float)Vars.Key("CAIS.ChinaThirdMessageThresholdMinutes").GetInt(300));

	private float CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES => DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaThirdMessageFrequencyMinutes").GetFloat(15f);

	public float CHINA_THIRD_MESSAGE_DISPLAY_TIME_SECONDS => Mathf.Clamp(DEBUG_TIMESCALE * Vars.Key("CAIS.ChinaThirdMessageDisplayTimeSeconds").GetFloat(60f), 1f, CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES * 60f);

	private float KOREA_MESSAGE_FREQUENCY_MINUTES => DEBUG_TIMESCALE * Vars.Key("CAIS.KoreaMessageFrequencyMinutes").GetFloat(60f);

	private float KOREA_MESSAGE_DISPLAY_TIME_SECONDS => Mathf.Clamp(DEBUG_TIMESCALE * Vars.Key("CAIS.KoreaMessageDisplayTimeSeconds").GetFloat(60f), 1f, KOREA_MESSAGE_FREQUENCY_MINUTES * 60f);

	private bool IsInitializationReady
	{
		get
		{
			if (!m_BattleNetReady)
			{
				return false;
			}
			if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
			{
				return false;
			}
			return true;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (Options.Get().GetBool(Option.HEALTHY_GAMING_DEBUG, defaultVal: false))
		{
			m_DebugMode = true;
		}
		m_NextCheckTime = Time.realtimeSinceStartup + INITIAL_PLAYTIME_CHECK_FREQUENCY_SECONDS;
		HearthstoneApplication.Get().WillReset += WillReset;
		HearthstoneApplication.Get().Resetting += OnReset;
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		Processor.RunCoroutine(InitNetworkData(), this);
		m_UpdateEnabled = true;
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= WillReset;
		HearthstoneApplication.Get().Resetting -= OnReset;
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
	}

	public void Update()
	{
		if (!m_UpdateEnabled || !m_NetworkDataReady || Time.realtimeSinceStartup < m_NextCheckTime)
		{
			return;
		}
		m_NextCheckTime = Time.realtimeSinceStartup + PLAYTIME_CHECK_FREQUENCY_SECONDS;
		string accountCountry = m_AccountCountry;
		if (!(accountCountry == "CHN"))
		{
			if (accountCountry == "KOR")
			{
				KoreaRestrictions();
			}
			else
			{
				m_UpdateEnabled = false;
			}
		}
		else
		{
			ChinaRestrictions();
		}
	}

	public static HealthyGamingMgr Get()
	{
		return HearthstoneServices.Get<HealthyGamingMgr>();
	}

	public void OnLoggedIn()
	{
		m_BattleNetReady = true;
	}

	public bool isArenaEnabled()
	{
		return m_HealthyGamingArenaEnabled;
	}

	public ulong GetSessionStartTime()
	{
		return m_SessionStartTime;
	}

	private void WillReset()
	{
		StopCoroutinesAndResetState();
	}

	private void OnReset()
	{
		Processor.RunCoroutine(InitNetworkData(), this);
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		StopCoroutinesAndResetState();
	}

	private void StopCoroutinesAndResetState()
	{
		m_BattleNetReady = false;
		m_NetworkDataReady = false;
		Processor.StopAllCoroutinesWithObjectRef(this);
	}

	private IEnumerator InitNetworkData()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			yield break;
		}
		while (!IsInitializationReady)
		{
			yield return null;
		}
		m_AccountCountry = BattleNet.GetAccountCountry();
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		bool caisEnabled = false;
		if (m_AccountCountry == "CHN" || m_AccountCountry == "KOR")
		{
			if (PlatformSettings.IsMobile())
			{
				if (m_AccountCountry == "CHN")
				{
					caisEnabled = netObject.CaisEnabledMobileChina;
				}
				else if (m_AccountCountry == "KOR")
				{
					caisEnabled = netObject.CaisEnabledMobileSouthKorea;
				}
			}
			else
			{
				caisEnabled = netObject.CaisEnabledNonMobile;
			}
		}
		m_UpdateEnabled = caisEnabled;
		m_Restrictions = default(Lockouts);
		BattleNet.GetPlayRestrictions(ref m_Restrictions, reload: true);
		while (!m_Restrictions.loaded)
		{
			BattleNet.GetPlayRestrictions(ref m_Restrictions, reload: false);
			yield return null;
		}
		m_SessionStartTime = m_Restrictions.sessionStartTime;
		m_PlayedMinutes = m_Restrictions.CAISplayed;
		m_RestedMinutes = m_Restrictions.CAISrested;
		if (m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] Healthy Gaming Debug Logging ON");
			Debug.LogFormat("[HealthyGaming] Account Region: " + BattleNet.GetAccountRegion());
			Debug.LogFormat("[HealthyGaming] Current Region: " + BattleNet.GetCurrentRegion());
		}
		Debug.LogFormat("[HealthyGaming] CAIS ServerEnabledForPlatform={0} AccountCAISForChina={1} Country={2} TimeSec={3} PlayedTimeMin={4} RestedTimeMin={5} SessionStartTime={6}", caisEnabled, m_Restrictions.CAISactive, m_AccountCountry, Time.realtimeSinceStartup, m_PlayedMinutes, m_RestedMinutes, m_SessionStartTime);
		if (!m_Restrictions.CAISactive && m_AccountCountry == "CHN")
		{
			Debug.LogFormat("[HealthyGaming] Healthy Gaming Deactivated: account not set for CAIS.");
			m_UpdateEnabled = false;
			yield break;
		}
		if (caisEnabled)
		{
			Debug.LogFormat("[HealthyGaming] Healthy Gaming Active!");
		}
		if (m_AccountCountry == "KOR")
		{
			m_NextMessageDisplayTime = Time.realtimeSinceStartup + KOREA_MESSAGE_FREQUENCY_MINUTES * 60f;
		}
		if (m_AccountCountry == "CHN")
		{
			string text = "GLOBAL_HEALTHY_GAMING_CHINA_CAIS_ACTIVE";
			if ((bool)UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
			{
				text += "_PHONE";
			}
			string textArg = GameStrings.Get(text);
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, textArg, SocialToastMgr.TOAST_TYPE.DEFAULT, CHINA_CAIS_ACTIVE_DISPLAY_TIME_SECONDS);
			m_NextMessageDisplayTime = -2f;
		}
		m_NetworkDataReady = true;
	}

	private void KoreaRestrictions()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] Minutes Played: " + realtimeSinceStartup / 60f);
		}
		if (!(realtimeSinceStartup < m_NextMessageDisplayTime))
		{
			m_NextMessageDisplayTime += KOREA_MESSAGE_FREQUENCY_MINUTES * 60f;
			int num = (int)(realtimeSinceStartup / 60f) / 60;
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, GameStrings.Format("GLOBAL_HEALTHY_GAMING_TOAST", num), SocialToastMgr.TOAST_TYPE.DEFAULT, KOREA_MESSAGE_DISPLAY_TIME_SECONDS);
		}
	}

	private void ChinaRestrictions()
	{
		BattleNet.GetPlayRestrictions(ref m_Restrictions, reload: true);
		Processor.RunCoroutine(ChinaRestrictionsUpdate(), this);
	}

	private IEnumerator ChinaRestrictionsUpdate()
	{
		while (!m_Restrictions.loaded)
		{
			BattleNet.GetPlayRestrictions(ref m_Restrictions, reload: false);
			yield return null;
		}
		m_PlayedMinutes = m_Restrictions.CAISplayed;
		m_RestedMinutes = m_Restrictions.CAISrested;
		int minutesPlayed = m_PlayedMinutes;
		if (m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] CAIS Time Played: {0} min    Rested: {1} min", m_PlayedMinutes.ToString(), m_RestedMinutes.ToString());
			Debug.LogFormat("[HealthyGaming] CAIS Minutes Played: {0} min", minutesPlayed);
		}
		if (m_NextMessageDisplayTime == -2f)
		{
			yield return new WaitForSeconds(CHINA_CAIS_ACTIVE_DISPLAY_TIME_SECONDS);
			m_NextMessageDisplayTime = -1f;
		}
		if (!((float)minutesPlayed < m_NextMessageDisplayTime) || !(m_NextMessageDisplayTime > 0f))
		{
			if (minutesPlayed >= CHINA_FEATURES_LOCKOUT_THRESHOLD_MINUTES)
			{
				ChinaRestrictions_LockoutFeatures(minutesPlayed);
			}
			if (minutesPlayed >= CHINA_FIRST_MESSAGE_THRESHOLD_MINUTES && minutesPlayed < CHINA_SECOND_MESSAGE_THRESHOLD_MINUTES)
			{
				ChinaRestrictions_LessThan3Hours(minutesPlayed);
			}
			if (minutesPlayed >= CHINA_SECOND_MESSAGE_THRESHOLD_MINUTES && minutesPlayed <= CHINA_THIRD_MESSAGE_THRESHOLD_MINUTES)
			{
				ChinaRestrictions_3to5Hours(minutesPlayed);
			}
			if (minutesPlayed > CHINA_THIRD_MESSAGE_THRESHOLD_MINUTES)
			{
				ChinaRestrictions_MoreThan5Hours(minutesPlayed);
			}
		}
	}

	private void ChinaRestrictions_LessThan3Hours(int minutesPlayed)
	{
		if (m_NextMessageDisplayTime < 0f)
		{
			m_NextMessageDisplayTime = (float)m_PlayedMinutes + (CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES - (float)minutesPlayed % CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES);
		}
		else
		{
			m_NextMessageDisplayTime = (float)m_PlayedMinutes + CHINA_FIRST_MESSAGE_FREQUENCY_MINUTES;
		}
		string text = "GLOBAL_HEALTHY_GAMING_CHINA_LESS_THAN_THREE_HOURS";
		if ((bool)UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
		{
			text += "_PHONE";
		}
		int num = minutesPlayed / 60;
		string text2 = GameStrings.Format(text, num);
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, text2, SocialToastMgr.TOAST_TYPE.DEFAULT, CHINA_FIRST_MESSAGE_DISPLAY_TIME_SECONDS);
		if (m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] GLOBAL_HEALTHY_GAMING_CHINA_LESS_THAN_THREE_HOURS: {0} minutes {1}", minutesPlayed, DEBUG_TIMESCALE_LOGMSG);
			Debug.LogFormat("[HealthyGaming] First message: {0}", GameStrings.Format("GLOBAL_HEALTHY_GAMING_CHINA_LESS_THAN_THREE_HOURS", num));
			Debug.LogFormat("[HealthyGaming] NextMessageDisplayTime: " + m_NextMessageDisplayTime);
		}
		else
		{
			Debug.LogFormat("[HealthyGaming] Time: {0} sec,  Played: {1} min,  First message: {2}", Time.realtimeSinceStartup, minutesPlayed, text2);
		}
	}

	private void ChinaRestrictions_3to5Hours(int minutesPlayed)
	{
		if (m_NextMessageDisplayTime < 0f)
		{
			m_NextMessageDisplayTime = (float)m_PlayedMinutes + (CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES - (float)minutesPlayed % CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES);
		}
		else
		{
			m_NextMessageDisplayTime = (float)m_PlayedMinutes + CHINA_SECOND_MESSAGE_FREQUENCY_MINUTES;
		}
		string text = "GLOBAL_HEALTHY_GAMING_CHINA_THREE_TO_FIVE_HOURS";
		if ((bool)UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
		{
			text += "_PHONE";
		}
		string text2 = GameStrings.Get(text);
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, text2, SocialToastMgr.TOAST_TYPE.DEFAULT, CHINA_SECOND_MESSAGE_DISPLAY_TIME_SECONDS);
		if (m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] GLOBAL_HEALTHY_GAMING_CHINA_THREE_TO_FIVE_HOURS: {0} minutes {1}", minutesPlayed, DEBUG_TIMESCALE_LOGMSG);
			Debug.LogFormat("[HealthyGaming] Second message: {0}", GameStrings.Get("GLOBAL_HEALTHY_GAMING_CHINA_THREE_TO_FIVE_HOURS"));
			Debug.LogFormat("[HealthyGaming] NextMessageDisplayTime: " + m_NextMessageDisplayTime);
		}
		else
		{
			Debug.LogFormat("[HealthyGaming] Time: {0} sec,  Played: {1} min,  Second message: {2}", Time.realtimeSinceStartup, minutesPlayed, text2);
		}
	}

	private void ChinaRestrictions_MoreThan5Hours(int minutesPlayed)
	{
		if (m_NextMessageDisplayTime < 0f)
		{
			m_NextMessageDisplayTime = (float)m_PlayedMinutes + (CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES - (float)minutesPlayed % CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES);
		}
		else
		{
			m_NextMessageDisplayTime = (float)m_PlayedMinutes + CHINA_THIRD_MESSAGE_FREQUENCY_MINUTES;
		}
		string text = "GLOBAL_HEALTHY_GAMING_CHINA_MORE_THAN_FIVE_HOURS";
		if ((bool)UniversalInputManager.UsePhoneUI && GameStrings.HasKey(text + "_PHONE"))
		{
			text += "_PHONE";
		}
		string text2 = GameStrings.Get(text);
		SocialToastMgr.Get().AddToast(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, text2, SocialToastMgr.TOAST_TYPE.DEFAULT, CHINA_THIRD_MESSAGE_DISPLAY_TIME_SECONDS);
		if (m_DebugMode)
		{
			Debug.LogFormat("[HealthyGaming] GLOBAL_HEALTHY_GAMING_CHINA_MORE_THAN_FIVE_HOURS: {0} minutes {1}", minutesPlayed, DEBUG_TIMESCALE_LOGMSG);
			Debug.LogFormat("[HealthyGaming] Third message: {0}", GameStrings.Get("GLOBAL_HEALTHY_GAMING_CHINA_MORE_THAN_FIVE_HOURS"));
			Debug.LogFormat("[HealthyGaming] NextMessageDisplayTime: " + m_NextMessageDisplayTime);
		}
		else
		{
			Debug.LogFormat("[HealthyGaming] Time: {0} sec,  Played: {1} min,  Third message: {2}", Time.realtimeSinceStartup, minutesPlayed, text2);
		}
	}

	private void ChinaRestrictions_LockoutFeatures(int minutesPlayed)
	{
		m_HealthyGamingArenaEnabled = false;
		Box box = Box.Get();
		if (box != null)
		{
			box.UpdateUI();
		}
	}
}
