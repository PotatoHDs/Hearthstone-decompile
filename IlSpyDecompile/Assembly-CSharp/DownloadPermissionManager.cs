using System;

public class DownloadPermissionManager
{
	private static bool s_cellularEnabledSession;

	private static bool s_askedCelluarSession;

	private static bool s_currentlyAskingForCellular;

	public static bool CellularEnabled
	{
		get
		{
			return s_cellularEnabledSession;
		}
		set
		{
			s_cellularEnabledSession = value;
		}
	}

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

	public static void TryAskForCellularPermission(long totalSize, Action OnComplete)
	{
		if (!s_currentlyAskingForCellular && !s_askedCelluarSession && DialogManager.Get() != null)
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
					CellularEnabled = response == AlertPopup.Response.CONFIRM;
					Log.Downloader.PrintInfo("User said {0} to cell prompt.", CellularEnabled ? "yes" : "no");
					s_askedCelluarSession = true;
					OnComplete();
				}
			};
			DialogManager.Get().ShowPopup(info, OnRequestShown);
		}
	}

	private static bool OnRequestShown(DialogBase dialog, object userData)
	{
		s_currentlyAskingForCellular = true;
		dialog.AddHiddenOrDestroyedListener(RequestClosed);
		return true;
	}

	private static void RequestClosed(DialogBase dialog, object userData)
	{
		s_currentlyAskingForCellular = false;
	}
}
