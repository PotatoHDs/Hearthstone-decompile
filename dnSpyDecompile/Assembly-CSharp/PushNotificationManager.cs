using System;
using PegasusShared;

// Token: 0x020005F8 RID: 1528
public class PushNotificationManager
{
	// Token: 0x0600531E RID: 21278 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void GetDevicePushNotificationStatus()
	{
	}

	// Token: 0x0600531F RID: 21279 RVA: 0x001B2657 File Offset: 0x001B0857
	public static PushNotificationManager Get()
	{
		if (PushNotificationManager.s_instance == null)
		{
			PushNotificationManager.s_instance = new PushNotificationManager();
		}
		return PushNotificationManager.s_instance;
	}

	// Token: 0x06005320 RID: 21280 RVA: 0x001B266F File Offset: 0x001B086F
	public static bool ShouldDisallowPushNotification()
	{
		if (Options.Get().GetInt(Option.PUSH_NOTIFICATION_STATUS) == 2)
		{
			Log.Login.Print("Push Notifiaction is Disallowed", Array.Empty<object>());
			return true;
		}
		return false;
	}

	// Token: 0x06005321 RID: 21281 RVA: 0x001B2697 File Offset: 0x001B0897
	public void DisallowPushNotification()
	{
		Log.Login.Print("Setting Push Notifiaction to Disallowed", Array.Empty<object>());
		Options.Get().SetInt(Option.PUSH_NOTIFICATION_STATUS, 2);
	}

	// Token: 0x06005322 RID: 21282 RVA: 0x001B26BA File Offset: 0x001B08BA
	public bool AllowRegisterPushAtLogin()
	{
		if (PlatformSettings.RuntimeOS != OSCategory.iOS)
		{
			Options.Get().SetInt(Option.PUSH_NOTIFICATION_STATUS, 3);
		}
		return Options.Get().GetInt(Option.PUSH_NOTIFICATION_STATUS) == 3;
	}

	// Token: 0x06005323 RID: 21283 RVA: 0x001B26E4 File Offset: 0x001B08E4
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
		if (this.GetGamesWon() < 3)
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
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnPushNotificationContextResponse);
		this.m_dismissCallback = dismissCallback;
		DialogManager.Get().ShowPopup(popupInfo);
		this.m_isShowingContext = true;
		return true;
	}

	// Token: 0x06005324 RID: 21284 RVA: 0x001B27B8 File Offset: 0x001B09B8
	public void ShowPushNotificationsDisabledContext()
	{
		Log.Login.Print("Showing dialog indicating permissions are disabled", Array.Empty<object>());
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PUSH_NOTIFICATION_CONTEXT_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_PUSH_NOTIFICATION_EXTENDED_CONTEXT_BODY");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response2, object userData2)
		{
			if (this.m_dismissCallback != null)
			{
				this.m_dismissCallback();
				this.m_dismissCallback = null;
			}
			this.m_isShowingContext = false;
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06005325 RID: 21285 RVA: 0x001B2834 File Offset: 0x001B0A34
	private void OnPushNotificationContextResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			Log.Login.Print("Push Notification prompt permission not granted", Array.Empty<object>());
			this.DisallowPushNotification();
		}
		else
		{
			Options.Get().SetInt(Option.PUSH_NOTIFICATION_STATUS, 3);
			PushNotificationManager.GetDevicePushNotificationStatus();
		}
		if (this.m_dismissCallback != null)
		{
			this.m_dismissCallback();
			this.m_dismissCallback = null;
		}
		this.m_isShowingContext = false;
	}

	// Token: 0x06005326 RID: 21286 RVA: 0x001B2894 File Offset: 0x001B0A94
	public int GetGamesWon()
	{
		int num = 0;
		if (NetCache.Get() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records == null)
		{
			return num;
		}
		foreach (NetCache.PlayerRecord playerRecord in NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records)
		{
			if (playerRecord.Data == 0)
			{
				GameType recordType = playerRecord.RecordType;
				if (recordType <= GameType.GT_CASUAL)
				{
					if (recordType != GameType.GT_VS_AI && recordType != GameType.GT_ARENA && recordType - GameType.GT_RANKED > 1)
					{
						continue;
					}
				}
				else if (recordType != GameType.GT_TAVERNBRAWL && recordType != GameType.GT_FSG_BRAWL && recordType != GameType.GT_FSG_BRAWL_2P_COOP)
				{
					continue;
				}
				num += playerRecord.Wins;
			}
		}
		return num;
	}

	// Token: 0x06005327 RID: 21287 RVA: 0x001B2950 File Offset: 0x001B0B50
	public bool IsShowingContext()
	{
		return this.m_isShowingContext;
	}

	// Token: 0x040049CA RID: 18890
	public const int UNASKED = 1;

	// Token: 0x040049CB RID: 18891
	public const int DISALLOWED = 2;

	// Token: 0x040049CC RID: 18892
	public const int ALLOWED = 3;

	// Token: 0x040049CD RID: 18893
	private static PushNotificationManager s_instance;

	// Token: 0x040049CE RID: 18894
	private bool m_isShowingContext;

	// Token: 0x040049CF RID: 18895
	private Action m_dismissCallback;
}
