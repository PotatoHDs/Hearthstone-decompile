using bgs;
using UnityEngine;

public class ChatFrames : MonoBehaviour
{
	public MobileChatLogFrame chatLogFrame;

	private bool wasShowingDialog;

	public BnetPlayer Receiver
	{
		get
		{
			return chatLogFrame.Receiver;
		}
		set
		{
			chatLogFrame.Receiver = value;
			if (chatLogFrame.Receiver == null)
			{
				ChatMgr.Get().CloseChatUI();
			}
			OnFramesMoved();
		}
	}

	private void Awake()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
		BnetEventMgr.Get().AddChangeListener(OnBnetEventOccurred);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		chatLogFrame.CloseButtonReleased += OnCloseButtonReleased;
	}

	private void OnDestroy()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded);
		}
		if (BnetEventMgr.Get() != null)
		{
			BnetEventMgr.Get().RemoveChangeListener(OnBnetEventOccurred);
		}
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		}
		OnFramesMoved();
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		bool flag = DialogManager.Get().ShowingDialog();
		if (flag != wasShowingDialog)
		{
			if (flag && chatLogFrame.HasFocus)
			{
				OnPopupOpened();
			}
			else if (!flag && ChatMgr.Get().FriendListFrame != null && !ChatMgr.Get().FriendListFrame.ShowingAddFriendFrame && !chatLogFrame.HasFocus)
			{
				OnPopupClosed();
			}
			wasShowingDialog = flag;
		}
	}

	public void Back()
	{
		if (!DialogManager.Get().ShowingDialog())
		{
			if (ChatMgr.Get().FriendListFrame.ShowingAddFriendFrame)
			{
				ChatMgr.Get().FriendListFrame.CloseAddFriendFrame();
			}
			else if (Receiver != null)
			{
				Receiver = null;
			}
			else
			{
				ChatMgr.Get().CloseChatUI();
			}
		}
	}

	private void OnFramesMoved()
	{
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().OnChatFramesMoved();
		}
	}

	private void OnCloseButtonReleased()
	{
		ChatMgr.Get().CloseChatUI();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			ChatMgr.Get().ShowFriendsList();
		}
	}

	private void OnPopupOpened()
	{
		if (chatLogFrame.HasFocus)
		{
			chatLogFrame.Focus(focus: false);
		}
	}

	private void OnPopupClosed()
	{
		if (Receiver != null)
		{
			chatLogFrame.Focus(focus: true);
		}
	}

	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			ChatMgr.Get().CleanUp();
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		ChatMgr.Get().CleanUp();
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			ChatMgr.Get().CleanUp();
		}
	}
}
