using System;

// Token: 0x0200093E RID: 2366
public class DownloadPermissionManager
{
	// Token: 0x17000772 RID: 1906
	// (get) Token: 0x060082E9 RID: 33513 RVA: 0x002A7A10 File Offset: 0x002A5C10
	// (set) Token: 0x060082EA RID: 33514 RVA: 0x002A7A17 File Offset: 0x002A5C17
	public static bool CellularEnabled
	{
		get
		{
			return DownloadPermissionManager.s_cellularEnabledSession;
		}
		set
		{
			DownloadPermissionManager.s_cellularEnabledSession = value;
		}
	}

	// Token: 0x17000773 RID: 1907
	// (get) Token: 0x060082EB RID: 33515 RVA: 0x002A7A1F File Offset: 0x002A5C1F
	// (set) Token: 0x060082EC RID: 33516 RVA: 0x002A7A2D File Offset: 0x002A5C2D
	public static bool DownloadEnabled
	{
		get
		{
			return Options.Get().GetBool(Option.ASSET_DOWNLOAD_ENABLED);
		}
		set
		{
			Options.Get().SetBool(Option.ASSET_DOWNLOAD_ENABLED, value);
		}
	}

	// Token: 0x060082ED RID: 33517 RVA: 0x002A7A3C File Offset: 0x002A5C3C
	public static void TryAskForCellularPermission(long totalSize, Action OnComplete)
	{
		if (!DownloadPermissionManager.s_currentlyAskingForCellular && !DownloadPermissionManager.s_askedCelluarSession && DialogManager.Get() != null)
		{
			string arg = DownloadStatusView.FormatBytesAsHumanReadable(totalSize);
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_HEADER"),
				m_text = string.Format(GameStrings.Get("GLOBAL_ASSET_DOWNLOAD_CELLULAR_POPUP_ADDITIONAL_BODY"), arg),
				m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES"),
				m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
				m_responseCallback = delegate(AlertPopup.Response response, object data)
				{
					DownloadPermissionManager.CellularEnabled = (response == AlertPopup.Response.CONFIRM);
					Log.Downloader.PrintInfo("User said {0} to cell prompt.", new object[]
					{
						DownloadPermissionManager.CellularEnabled ? "yes" : "no"
					});
					DownloadPermissionManager.s_askedCelluarSession = true;
					OnComplete();
				}
			};
			DialogManager.Get().ShowPopup(info, new DialogManager.DialogProcessCallback(DownloadPermissionManager.OnRequestShown));
		}
	}

	// Token: 0x060082EE RID: 33518 RVA: 0x002A7B04 File Offset: 0x002A5D04
	private static bool OnRequestShown(DialogBase dialog, object userData)
	{
		DownloadPermissionManager.s_currentlyAskingForCellular = true;
		dialog.AddHiddenOrDestroyedListener(new DialogBase.HideCallback(DownloadPermissionManager.RequestClosed));
		return true;
	}

	// Token: 0x060082EF RID: 33519 RVA: 0x002A7B1F File Offset: 0x002A5D1F
	private static void RequestClosed(DialogBase dialog, object userData)
	{
		DownloadPermissionManager.s_currentlyAskingForCellular = false;
	}

	// Token: 0x04006D91 RID: 28049
	private static bool s_cellularEnabledSession;

	// Token: 0x04006D92 RID: 28050
	private static bool s_askedCelluarSession;

	// Token: 0x04006D93 RID: 28051
	private static bool s_currentlyAskingForCellular;
}
