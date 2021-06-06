using System;
using Hearthstone;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public static class Error
{
	// Token: 0x0600263B RID: 9787 RVA: 0x000C076C File Offset: 0x000BE96C
	public static void AddWarning(string header, string message, params object[] messageArgs)
	{
		Error.AddWarning(new ErrorParams
		{
			m_header = header,
			m_message = string.Format(message, messageArgs)
		});
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x000C078C File Offset: 0x000BE98C
	public static void AddWarningLoc(string headerKey, string messageKey, params object[] messageArgs)
	{
		Error.AddWarning(new ErrorParams
		{
			m_header = GameStrings.Get(headerKey),
			m_message = GameStrings.Format(messageKey, messageArgs)
		});
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x000C07B4 File Offset: 0x000BE9B4
	public static void AddWarning(ErrorParams parms)
	{
		if (!DialogManager.Get())
		{
			parms.m_reason = FatalErrorReason.UNAVAILAVLE_DIALOGMANAGER_FOR_WARNING;
			Error.AddFatal(parms);
			return;
		}
		Debug.LogWarning(string.Format("Error.AddWarning() - header={0} message={1}", parms.m_header, parms.m_message));
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(null, true);
		}
		Error.ShowWarningDialog(parms);
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x000C0810 File Offset: 0x000BEA10
	public static void AddDevWarning(string header, string message, params object[] messageArgs)
	{
		string text = string.Format(message, messageArgs);
		if (!Debug.isDebugBuild)
		{
			Debug.LogWarning(string.Format("Error.AddDevWarning() - header={0} message={1}", header, text));
			return;
		}
		Error.AddWarning(new ErrorParams
		{
			m_header = header,
			m_message = text
		});
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x000C0858 File Offset: 0x000BEA58
	public static void AddDevWarningNonRepeating(string header, string message, params object[] messageArgs)
	{
		if (!Error.s_hasShownNonRepeatingDevWarning)
		{
			Error.s_hasShownNonRepeatingDevWarning = true;
			Error.AddDevWarning(header, message, messageArgs);
			return;
		}
		string arg = string.Format(message, messageArgs);
		Debug.LogWarning(string.Format("Error.AddDevWarningNonRepeating() - header={0} message={1}", header, arg));
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x000C0894 File Offset: 0x000BEA94
	public static void AddFatal(FatalErrorReason reason, string messageKey, params object[] messageArgs)
	{
		Error.AddFatal(new ErrorParams
		{
			m_message = GameStrings.Format(messageKey, messageArgs),
			m_reason = reason
		});
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x000C08B4 File Offset: 0x000BEAB4
	public static void AddFatal(ErrorParams parms)
	{
		Debug.LogError(string.Format("Error.AddFatal() - message={0}", parms.m_message));
		TelemetryManager.Client().SendFatalError(parms.m_reason.ToString());
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(null, true);
		}
		if (Error.ShouldUseWarningDialogForFatalError())
		{
			if (string.IsNullOrEmpty(parms.m_header))
			{
				parms.m_header = "Fatal Error as Warning";
			}
			Error.ShowWarningDialog(parms);
			return;
		}
		parms.m_type = ErrorType.FATAL;
		FatalErrorMessage fatalErrorMessage = new FatalErrorMessage();
		fatalErrorMessage.m_id = (parms.m_header ?? string.Empty) + parms.m_message;
		fatalErrorMessage.m_text = parms.m_message;
		fatalErrorMessage.m_ackCallback = parms.m_ackCallback;
		fatalErrorMessage.m_ackUserData = parms.m_ackUserData;
		fatalErrorMessage.m_allowClick = parms.m_allowClick;
		fatalErrorMessage.m_redirectToStore = parms.m_redirectToStore;
		fatalErrorMessage.m_delayBeforeNextReset = parms.m_delayBeforeNextReset;
		fatalErrorMessage.m_reason = parms.m_reason;
		FatalErrorMgr.Get().Add(fatalErrorMessage);
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x000C09B8 File Offset: 0x000BEBB8
	public static void AddDevFatal(string message, params object[] messageArgs)
	{
		string text = string.Format(message, messageArgs);
		if (!HearthstoneApplication.IsInternal())
		{
			Debug.LogError(string.Format("Error.AddDevFatal() - message={0}", text));
			return;
		}
		Debug.LogError(text);
		if (SceneDebugger.Get() != null)
		{
			SceneDebugger.Get().AddErrorMessage(text);
		}
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x000C0A00 File Offset: 0x000BEC00
	public static void AddDevFatalUnlessWorkarounds(string message, params object[] messageArgs)
	{
		string message2 = string.Format(message, messageArgs);
		if (HearthstoneApplication.UseDevWorkarounds())
		{
			Debug.LogError(message2);
			return;
		}
		Error.AddDevFatal(message2, Array.Empty<object>());
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x000C0A2E File Offset: 0x000BEC2E
	private static bool ShouldUseWarningDialogForFatalError()
	{
		return !HearthstoneApplication.IsPublic() && DialogManager.Get() && !Options.Get().GetBool(Option.ERROR_SCREEN);
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x000C0A58 File Offset: 0x000BEC58
	private static void ShowWarningDialog(ErrorParams parms)
	{
		parms.m_type = ErrorType.WARNING;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_id = parms.m_header + parms.m_message;
		popupInfo.m_headerText = parms.m_header;
		popupInfo.m_text = parms.m_message;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(Error.OnWarningPopupResponse);
		popupInfo.m_responseUserData = parms;
		popupInfo.m_showAlertIcon = true;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x000C0ACC File Offset: 0x000BECCC
	private static void OnWarningPopupResponse(AlertPopup.Response response, object userData)
	{
		ErrorParams errorParams = (ErrorParams)userData;
		if (errorParams.m_ackCallback != null)
		{
			errorParams.m_ackCallback(errorParams.m_ackUserData);
		}
	}

	// Token: 0x0400159E RID: 5534
	public static readonly PlatformDependentValue<bool> HAS_APP_STORE = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	// Token: 0x0400159F RID: 5535
	private static bool s_hasShownNonRepeatingDevWarning = false;

	// Token: 0x020015ED RID: 5613
	// (Invoke) Token: 0x0600E240 RID: 57920
	public delegate void AcknowledgeCallback(object userData);
}
