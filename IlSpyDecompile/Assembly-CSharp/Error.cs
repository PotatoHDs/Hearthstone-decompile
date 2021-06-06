using Hearthstone;
using UnityEngine;

public static class Error
{
	public delegate void AcknowledgeCallback(object userData);

	public static readonly PlatformDependentValue<bool> HAS_APP_STORE = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	private static bool s_hasShownNonRepeatingDevWarning = false;

	public static void AddWarning(string header, string message, params object[] messageArgs)
	{
		AddWarning(new ErrorParams
		{
			m_header = header,
			m_message = string.Format(message, messageArgs)
		});
	}

	public static void AddWarningLoc(string headerKey, string messageKey, params object[] messageArgs)
	{
		AddWarning(new ErrorParams
		{
			m_header = GameStrings.Get(headerKey),
			m_message = GameStrings.Format(messageKey, messageArgs)
		});
	}

	public static void AddWarning(ErrorParams parms)
	{
		if (!DialogManager.Get())
		{
			parms.m_reason = FatalErrorReason.UNAVAILAVLE_DIALOGMANAGER_FOR_WARNING;
			AddFatal(parms);
			return;
		}
		Debug.LogWarning($"Error.AddWarning() - header={parms.m_header} message={parms.m_message}");
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(null, force: true);
		}
		ShowWarningDialog(parms);
	}

	public static void AddDevWarning(string header, string message, params object[] messageArgs)
	{
		string text = string.Format(message, messageArgs);
		if (!Debug.isDebugBuild)
		{
			Debug.LogWarning($"Error.AddDevWarning() - header={header} message={text}");
			return;
		}
		AddWarning(new ErrorParams
		{
			m_header = header,
			m_message = text
		});
	}

	public static void AddDevWarningNonRepeating(string header, string message, params object[] messageArgs)
	{
		if (!s_hasShownNonRepeatingDevWarning)
		{
			s_hasShownNonRepeatingDevWarning = true;
			AddDevWarning(header, message, messageArgs);
		}
		else
		{
			string arg = string.Format(message, messageArgs);
			Debug.LogWarning($"Error.AddDevWarningNonRepeating() - header={header} message={arg}");
		}
	}

	public static void AddFatal(FatalErrorReason reason, string messageKey, params object[] messageArgs)
	{
		AddFatal(new ErrorParams
		{
			m_message = GameStrings.Format(messageKey, messageArgs),
			m_reason = reason
		});
	}

	public static void AddFatal(ErrorParams parms)
	{
		Debug.LogError($"Error.AddFatal() - message={parms.m_message}");
		TelemetryManager.Client().SendFatalError(parms.m_reason.ToString());
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(null, force: true);
		}
		if (ShouldUseWarningDialogForFatalError())
		{
			if (string.IsNullOrEmpty(parms.m_header))
			{
				parms.m_header = "Fatal Error as Warning";
			}
			ShowWarningDialog(parms);
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

	public static void AddDevFatal(string message, params object[] messageArgs)
	{
		string text = string.Format(message, messageArgs);
		if (!HearthstoneApplication.IsInternal())
		{
			Debug.LogError($"Error.AddDevFatal() - message={text}");
			return;
		}
		Debug.LogError(text);
		if (SceneDebugger.Get() != null)
		{
			SceneDebugger.Get().AddErrorMessage(text);
		}
	}

	public static void AddDevFatalUnlessWorkarounds(string message, params object[] messageArgs)
	{
		string message2 = string.Format(message, messageArgs);
		if (HearthstoneApplication.UseDevWorkarounds())
		{
			Debug.LogError(message2);
		}
		else
		{
			AddDevFatal(message2);
		}
	}

	private static bool ShouldUseWarningDialogForFatalError()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		if (!DialogManager.Get())
		{
			return false;
		}
		return !Options.Get().GetBool(Option.ERROR_SCREEN);
	}

	private static void ShowWarningDialog(ErrorParams parms)
	{
		parms.m_type = ErrorType.WARNING;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_id = parms.m_header + parms.m_message;
		popupInfo.m_headerText = parms.m_header;
		popupInfo.m_text = parms.m_message;
		popupInfo.m_responseCallback = OnWarningPopupResponse;
		popupInfo.m_responseUserData = parms;
		popupInfo.m_showAlertIcon = true;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private static void OnWarningPopupResponse(AlertPopup.Response response, object userData)
	{
		ErrorParams errorParams = (ErrorParams)userData;
		if (errorParams.m_ackCallback != null)
		{
			errorParams.m_ackCallback(errorParams.m_ackUserData);
		}
	}
}
