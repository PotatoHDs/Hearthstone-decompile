using System;
using PegasusShared;

public class PushNotificationManager
{
	public const int UNASKED = 1;

	public const int DISALLOWED = 2;

	public const int ALLOWED = 3;

	private static PushNotificationManager s_instance;

	private bool m_isShowingContext;

	private Action m_dismissCallback;

	private static void GetDevicePushNotificationStatus()
	{
	}

	public static PushNotificationManager Get()
	{
		if (s_instance == null)
		{
			s_instance = new PushNotificationManager();
		}
		return s_instance;
	}

	public static bool ShouldDisallowPushNotification()
	{
		if (Options.Get().GetInt(Option.PUSH_NOTIFICATION_STATUS) == 2)
		{
			Log.Login.Print("Push Notifiaction is Disallowed");
			return true;
		}
		return false;
	}

	public void DisallowPushNotification()
	{
		Log.Login.Print("Setting Push Notifiaction to Disallowed");
		Options.Get().SetInt(Option.PUSH_NOTIFICATION_STATUS, 2);
	}

	public bool AllowRegisterPushAtLogin()
	{
		if (PlatformSettings.RuntimeOS != OSCategory.iOS)
		{
			Options.Get().SetInt(Option.PUSH_NOTIFICATION_STATUS, 3);
		}
		if (Options.Get().GetInt(Option.PUSH_NOTIFICATION_STATUS) == 3)
		{
			return true;
		}
		return false;
	}

	public bool ShowPushNotificationContext(Action dismissCallback)
	{
		if (PlatformSettings.RuntimeOS == OSCategory.PC || PlatformSettings.RuntimeOS == OSCategory.Mac)
		{
			return false;
		}
		if (SpectatorManager.Get().IsSpectatingOrWatching)
		{
			return false;
		}
		if (Options.Get().GetInt(Option.PUSH_NOTIFICATION_STATUS, 1) != 1)
		{
			return false;
		}
		if (GetGamesWon() < 3)
		{
			return false;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PUSH_NOTIFICATION_CONTEXT_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_PUSH_NOTIFICATION_CONTEXT_BODY");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLUE_PUSH_NOTIFICATION_CONTEXT_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_PUSH_NOTIFICATION_CONTEXT_CANCEL");
		popupInfo.m_responseCallback = OnPushNotificationContextResponse;
		m_dismissCallback = dismissCallback;
		DialogManager.Get().ShowPopup(popupInfo);
		m_isShowingContext = true;
		return true;
	}

	public void ShowPushNotificationsDisabledContext()
	{
		Log.Login.Print("Showing dialog indicating permissions are disabled");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PUSH_NOTIFICATION_CONTEXT_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_PUSH_NOTIFICATION_EXTENDED_CONTEXT_BODY");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate
		{
			if (m_dismissCallback != null)
			{
				m_dismissCallback();
				m_dismissCallback = null;
			}
			m_isShowingContext = false;
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnPushNotificationContextResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			Log.Login.Print("Push Notification prompt permission not granted");
			DisallowPushNotification();
		}
		else
		{
			Options.Get().SetInt(Option.PUSH_NOTIFICATION_STATUS, 3);
			GetDevicePushNotificationStatus();
		}
		if (m_dismissCallback != null)
		{
			m_dismissCallback();
			m_dismissCallback = null;
		}
		m_isShowingContext = false;
	}

	public int GetGamesWon()
	{
		int num = 0;
		if (NetCache.Get() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records == null)
		{
			return num;
		}
		foreach (NetCache.PlayerRecord record in NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records)
		{
			if (record.Data == 0)
			{
				switch (record.RecordType)
				{
				case GameType.GT_VS_AI:
				case GameType.GT_ARENA:
				case GameType.GT_RANKED:
				case GameType.GT_CASUAL:
				case GameType.GT_TAVERNBRAWL:
				case GameType.GT_FSG_BRAWL:
				case GameType.GT_FSG_BRAWL_2P_COOP:
					num += record.Wins;
					break;
				}
			}
		}
		return num;
	}

	public bool IsShowingContext()
	{
		return m_isShowingContext;
	}
}
