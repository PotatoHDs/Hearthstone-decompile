using System;
using bgs;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class ChatFrames : MonoBehaviour
{
	// Token: 0x1700005C RID: 92
	// (get) Token: 0x060006EA RID: 1770 RVA: 0x00028048 File Offset: 0x00026248
	// (set) Token: 0x060006EB RID: 1771 RVA: 0x00028055 File Offset: 0x00026255
	public BnetPlayer Receiver
	{
		get
		{
			return this.chatLogFrame.Receiver;
		}
		set
		{
			this.chatLogFrame.Receiver = value;
			if (this.chatLogFrame.Receiver == null)
			{
				ChatMgr.Get().CloseChatUI(true);
			}
			this.OnFramesMoved();
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00028084 File Offset: 0x00026284
	private void Awake()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		BnetEventMgr.Get().AddChangeListener(new BnetEventMgr.ChangeCallback(this.OnBnetEventOccurred));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.chatLogFrame.CloseButtonReleased += this.OnCloseButtonReleased;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x000280EC File Offset: 0x000262EC
	private void OnDestroy()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		}
		if (BnetEventMgr.Get() != null)
		{
			BnetEventMgr.Get().RemoveChangeListener(new BnetEventMgr.ChangeCallback(this.OnBnetEventOccurred));
		}
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}
		this.OnFramesMoved();
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00028159 File Offset: 0x00026359
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00028178 File Offset: 0x00026378
	private void Update()
	{
		bool flag = DialogManager.Get().ShowingDialog();
		if (flag != this.wasShowingDialog)
		{
			if (flag && this.chatLogFrame.HasFocus)
			{
				this.OnPopupOpened();
			}
			else if (!flag && ChatMgr.Get().FriendListFrame != null && !ChatMgr.Get().FriendListFrame.ShowingAddFriendFrame && !this.chatLogFrame.HasFocus)
			{
				this.OnPopupClosed();
			}
			this.wasShowingDialog = flag;
		}
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x000281F4 File Offset: 0x000263F4
	public void Back()
	{
		if (DialogManager.Get().ShowingDialog())
		{
			return;
		}
		if (ChatMgr.Get().FriendListFrame.ShowingAddFriendFrame)
		{
			ChatMgr.Get().FriendListFrame.CloseAddFriendFrame();
			return;
		}
		if (this.Receiver != null)
		{
			this.Receiver = null;
			return;
		}
		ChatMgr.Get().CloseChatUI(true);
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002824A File Offset: 0x0002644A
	private void OnFramesMoved()
	{
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().OnChatFramesMoved();
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x00028263 File Offset: 0x00026463
	private void OnCloseButtonReleased()
	{
		ChatMgr.Get().CloseChatUI(true);
		if (UniversalInputManager.UsePhoneUI)
		{
			ChatMgr.Get().ShowFriendsList();
		}
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00028286 File Offset: 0x00026486
	private void OnPopupOpened()
	{
		if (this.chatLogFrame.HasFocus)
		{
			this.chatLogFrame.Focus(false);
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x000282A1 File Offset: 0x000264A1
	private void OnPopupClosed()
	{
		if (this.Receiver != null)
		{
			this.chatLogFrame.Focus(true);
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x000282B7 File Offset: 0x000264B7
	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			ChatMgr.Get().CleanUp();
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x000282C6 File Offset: 0x000264C6
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		ChatMgr.Get().CleanUp();
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x000282D2 File Offset: 0x000264D2
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			ChatMgr.Get().CleanUp();
		}
	}

	// Token: 0x040004D3 RID: 1235
	public MobileChatLogFrame chatLogFrame;

	// Token: 0x040004D4 RID: 1236
	private bool wasShowingDialog;
}
