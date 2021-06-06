using System.Collections.Generic;
using Blizzard.T5.Core;
using Hearthstone;

public class FatalErrorMgr
{
	public delegate void ErrorCallback(FatalErrorMessage message, object userData);

	protected class ErrorListener : EventListener<ErrorCallback>
	{
		public void Fire(FatalErrorMessage message)
		{
			if (GeneralUtils.IsCallbackValid(m_callback))
			{
				m_callback(message, m_userData);
			}
		}
	}

	private static FatalErrorMgr s_instance;

	private List<FatalErrorMessage> m_messages = new List<FatalErrorMessage>();

	private string m_text;

	private List<ErrorListener> m_errorListeners = new List<ErrorListener>();

	private string m_generatedErrorCode;

	public bool IsUnrecoverable { get; private set; }

	public static FatalErrorMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new FatalErrorMgr();
		}
		return s_instance;
	}

	public static bool IsInitialized()
	{
		return s_instance != null;
	}

	public void Add(FatalErrorMessage message)
	{
		m_messages.Add(message);
		FireErrorListeners(message);
	}

	public bool AddUnique(FatalErrorMessage message)
	{
		if (!string.IsNullOrEmpty(message.m_id))
		{
			foreach (FatalErrorMessage message2 in m_messages)
			{
				if (message2.m_id == message.m_id)
				{
					return false;
				}
			}
		}
		Add(message);
		return true;
	}

	public void SetErrorCode(string prefixSource, string errorSubset1, string errorSubset2 = null, string errorSubset3 = null)
	{
		m_generatedErrorCode = prefixSource + ":" + errorSubset1;
		if (errorSubset2 != null)
		{
			m_generatedErrorCode = m_generatedErrorCode + ":" + errorSubset2;
		}
		if (errorSubset3 != null)
		{
			m_generatedErrorCode = m_generatedErrorCode + ":" + errorSubset3;
		}
	}

	public void ClearAllErrors()
	{
		m_messages.Clear();
		m_generatedErrorCode = null;
	}

	public bool AddErrorListener(ErrorCallback callback)
	{
		return AddErrorListener(callback, null);
	}

	public bool AddErrorListener(ErrorCallback callback, object userData)
	{
		ErrorListener errorListener = new ErrorListener();
		errorListener.SetCallback(callback);
		errorListener.SetUserData(userData);
		if (m_errorListeners.Contains(errorListener))
		{
			return false;
		}
		m_errorListeners.Add(errorListener);
		return true;
	}

	public bool RemoveErrorListener(ErrorCallback callback)
	{
		return RemoveErrorListener(callback, null);
	}

	public bool RemoveErrorListener(ErrorCallback callback, object userData)
	{
		ErrorListener errorListener = new ErrorListener();
		errorListener.SetCallback(callback);
		errorListener.SetUserData(userData);
		return m_errorListeners.Remove(errorListener);
	}

	public List<FatalErrorMessage> GetMessages()
	{
		return m_messages;
	}

	public string GetFormattedErrorCode()
	{
		return m_generatedErrorCode;
	}

	public bool HasError()
	{
		return m_messages.Count > 0;
	}

	public void NotifyExitPressed()
	{
		SendAcknowledgements();
		HearthstoneApplication.Get().Exit();
	}

	public static bool IsReconnectAllowedBasedOnFatalErrorReason(FatalErrorReason reason)
	{
		switch (reason)
		{
		case FatalErrorReason.LOGIN_FROM_ANOTHER_DEVICE:
		case FatalErrorReason.ADMIN_KICK_OR_BAN:
		case FatalErrorReason.ACCOUNT_SETUP_ERROR:
			return false;
		case FatalErrorReason.BREAKING_NEWS:
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.STARTUP)
			{
				return false;
			}
			break;
		}
		return true;
	}

	public void SetUnrecoverable(bool isUnrecoverable)
	{
		IsUnrecoverable = isUnrecoverable;
	}

	private void SendAcknowledgements()
	{
		FatalErrorMessage[] array = m_messages.ToArray();
		foreach (FatalErrorMessage fatalErrorMessage in array)
		{
			if (fatalErrorMessage.m_ackCallback != null)
			{
				fatalErrorMessage.m_ackCallback(fatalErrorMessage.m_ackUserData);
			}
		}
	}

	protected void FireErrorListeners(FatalErrorMessage message)
	{
		ErrorListener[] array = m_errorListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(message);
		}
	}
}
