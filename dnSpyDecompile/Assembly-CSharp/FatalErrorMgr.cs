using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using Hearthstone;

// Token: 0x020002DD RID: 733
public class FatalErrorMgr
{
	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x06002655 RID: 9813 RVA: 0x000C0D74 File Offset: 0x000BEF74
	// (set) Token: 0x06002656 RID: 9814 RVA: 0x000C0D7C File Offset: 0x000BEF7C
	public bool IsUnrecoverable { get; private set; }

	// Token: 0x06002657 RID: 9815 RVA: 0x000C0D85 File Offset: 0x000BEF85
	public static FatalErrorMgr Get()
	{
		if (FatalErrorMgr.s_instance == null)
		{
			FatalErrorMgr.s_instance = new FatalErrorMgr();
		}
		return FatalErrorMgr.s_instance;
	}

	// Token: 0x06002658 RID: 9816 RVA: 0x000C0D9D File Offset: 0x000BEF9D
	public static bool IsInitialized()
	{
		return FatalErrorMgr.s_instance != null;
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x000C0DA7 File Offset: 0x000BEFA7
	public void Add(FatalErrorMessage message)
	{
		this.m_messages.Add(message);
		this.FireErrorListeners(message);
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x000C0DBC File Offset: 0x000BEFBC
	public bool AddUnique(FatalErrorMessage message)
	{
		if (!string.IsNullOrEmpty(message.m_id))
		{
			using (List<FatalErrorMessage>.Enumerator enumerator = this.m_messages.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.m_id == message.m_id)
					{
						return false;
					}
				}
			}
		}
		this.Add(message);
		return true;
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x000C0E34 File Offset: 0x000BF034
	public void SetErrorCode(string prefixSource, string errorSubset1, string errorSubset2 = null, string errorSubset3 = null)
	{
		this.m_generatedErrorCode = prefixSource + ":" + errorSubset1;
		if (errorSubset2 != null)
		{
			this.m_generatedErrorCode = this.m_generatedErrorCode + ":" + errorSubset2;
		}
		if (errorSubset3 != null)
		{
			this.m_generatedErrorCode = this.m_generatedErrorCode + ":" + errorSubset3;
		}
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x000C0E89 File Offset: 0x000BF089
	public void ClearAllErrors()
	{
		this.m_messages.Clear();
		this.m_generatedErrorCode = null;
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x000C0E9D File Offset: 0x000BF09D
	public bool AddErrorListener(FatalErrorMgr.ErrorCallback callback)
	{
		return this.AddErrorListener(callback, null);
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x000C0EA8 File Offset: 0x000BF0A8
	public bool AddErrorListener(FatalErrorMgr.ErrorCallback callback, object userData)
	{
		FatalErrorMgr.ErrorListener errorListener = new FatalErrorMgr.ErrorListener();
		errorListener.SetCallback(callback);
		errorListener.SetUserData(userData);
		if (this.m_errorListeners.Contains(errorListener))
		{
			return false;
		}
		this.m_errorListeners.Add(errorListener);
		return true;
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x000C0EE6 File Offset: 0x000BF0E6
	public bool RemoveErrorListener(FatalErrorMgr.ErrorCallback callback)
	{
		return this.RemoveErrorListener(callback, null);
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x000C0EF0 File Offset: 0x000BF0F0
	public bool RemoveErrorListener(FatalErrorMgr.ErrorCallback callback, object userData)
	{
		FatalErrorMgr.ErrorListener errorListener = new FatalErrorMgr.ErrorListener();
		errorListener.SetCallback(callback);
		errorListener.SetUserData(userData);
		return this.m_errorListeners.Remove(errorListener);
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x000C0F1D File Offset: 0x000BF11D
	public List<FatalErrorMessage> GetMessages()
	{
		return this.m_messages;
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x000C0F25 File Offset: 0x000BF125
	public string GetFormattedErrorCode()
	{
		return this.m_generatedErrorCode;
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x000C0F2D File Offset: 0x000BF12D
	public bool HasError()
	{
		return this.m_messages.Count > 0;
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x000C0F3D File Offset: 0x000BF13D
	public void NotifyExitPressed()
	{
		this.SendAcknowledgements();
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x000C0F4F File Offset: 0x000BF14F
	public static bool IsReconnectAllowedBasedOnFatalErrorReason(FatalErrorReason reason)
	{
		return reason != FatalErrorReason.LOGIN_FROM_ANOTHER_DEVICE && reason != FatalErrorReason.ADMIN_KICK_OR_BAN && reason != FatalErrorReason.ACCOUNT_SETUP_ERROR && (reason != FatalErrorReason.BREAKING_NEWS || SceneMgr.Get().GetMode() != SceneMgr.Mode.STARTUP);
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x000C0F73 File Offset: 0x000BF173
	public void SetUnrecoverable(bool isUnrecoverable)
	{
		this.IsUnrecoverable = isUnrecoverable;
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x000C0F7C File Offset: 0x000BF17C
	private void SendAcknowledgements()
	{
		foreach (FatalErrorMessage fatalErrorMessage in this.m_messages.ToArray())
		{
			if (fatalErrorMessage.m_ackCallback != null)
			{
				fatalErrorMessage.m_ackCallback(fatalErrorMessage.m_ackUserData);
			}
		}
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x000C0FC0 File Offset: 0x000BF1C0
	protected void FireErrorListeners(FatalErrorMessage message)
	{
		FatalErrorMgr.ErrorListener[] array = this.m_errorListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(message);
		}
	}

	// Token: 0x040015C8 RID: 5576
	private static FatalErrorMgr s_instance;

	// Token: 0x040015C9 RID: 5577
	private List<FatalErrorMessage> m_messages = new List<FatalErrorMessage>();

	// Token: 0x040015CA RID: 5578
	private string m_text;

	// Token: 0x040015CB RID: 5579
	private List<FatalErrorMgr.ErrorListener> m_errorListeners = new List<FatalErrorMgr.ErrorListener>();

	// Token: 0x040015CC RID: 5580
	private string m_generatedErrorCode;

	// Token: 0x020015EE RID: 5614
	// (Invoke) Token: 0x0600E244 RID: 57924
	public delegate void ErrorCallback(FatalErrorMessage message, object userData);

	// Token: 0x020015EF RID: 5615
	protected class ErrorListener : EventListener<FatalErrorMgr.ErrorCallback>
	{
		// Token: 0x0600E247 RID: 57927 RVA: 0x00402EE4 File Offset: 0x004010E4
		public void Fire(FatalErrorMessage message)
		{
			if (!GeneralUtils.IsCallbackValid(this.m_callback))
			{
				return;
			}
			this.m_callback(message, this.m_userData);
		}
	}
}
