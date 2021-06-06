using System.Collections;
using System.Collections.Generic;
using bgs;
using UnityEngine;

public class QuickChatFrame : MonoBehaviour
{
	public QuickChatFrameBones m_Bones;

	public QuickChatFramePrefabs m_Prefabs;

	public GameObject m_Background;

	public UberText m_ReceiverNameText;

	public UberText m_LastMessageText;

	public GameObject m_LastMessageShadow;

	public PegUIElement m_ChatLogButton;

	public Font m_InputFont;

	private DropdownControl m_recentPlayerDropdown;

	private ChatLogFrame m_chatLogFrame;

	private PegUIElement m_inputBlocker;

	private List<BnetPlayer> m_recentPlayers = new List<BnetPlayer>();

	private BnetPlayer m_receiver;

	private float m_initialLastMessageTextHeight;

	private float m_initialLastMessageShadowScaleZ;

	private Font m_localizedInputFont;

	private Map<Renderer, int> m_chatLogOriginalLayers = new Map<Renderer, int>();

	private void Awake()
	{
		InitRecentPlayers();
		if (!InitReceiver())
		{
			Object.Destroy(base.gameObject);
			return;
		}
		BnetWhisperMgr.Get().AddWhisperListener(OnWhisper);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		InitTransform();
		InitInputBlocker();
		InitLastMessage();
		InitChatLogFrame();
		InitInput();
		ShowInput(fromAwake: true);
	}

	private void Start()
	{
		InitRecentPlayerDropdown();
		if (ChatMgr.Get().IsChatLogFrameShown())
		{
			ShowChatLogFrame(onStart: true);
		}
		UpdateReceiver();
		ChatMgr.Get().OnChatReceiverChanged(m_receiver);
	}

	private void OnDestroy()
	{
		BnetWhisperMgr.Get().RemoveWhisperListener(OnWhisper);
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject);
		}
	}

	public ChatLogFrame GetChatLogFrame()
	{
		return m_chatLogFrame;
	}

	public BnetPlayer GetReceiver()
	{
		return m_receiver;
	}

	public void SetReceiver(BnetPlayer player)
	{
		if (m_receiver != player)
		{
			m_receiver = player;
			UpdateReceiver();
			m_recentPlayerDropdown.setSelection(player);
			ChatMgr.Get().OnChatReceiverChanged(player);
		}
	}

	public void UpdateLayout()
	{
		if (m_chatLogFrame != null)
		{
			m_chatLogFrame.UpdateLayout();
		}
	}

	private void InitRecentPlayers()
	{
		UpdateRecentPlayers();
	}

	private void UpdateRecentPlayers()
	{
		m_recentPlayers.Clear();
		List<BnetPlayer> recentWhisperPlayers = ChatMgr.Get().GetRecentWhisperPlayers();
		for (int i = 0; i < recentWhisperPlayers.Count; i++)
		{
			BnetPlayer item = recentWhisperPlayers[i];
			m_recentPlayers.Add(item);
		}
	}

	private bool InitReceiver()
	{
		m_receiver = null;
		if (m_recentPlayers.Count == 0)
		{
			string message = ((BnetFriendMgr.Get().GetOnlineFriendCount() != 0) ? GameStrings.Get("GLOBAL_CHAT_NO_RECENT_CONVERSATIONS") : GameStrings.Get("GLOBAL_CHAT_NO_FRIENDS_ONLINE"));
			UIStatus.Get().AddError(message);
			return false;
		}
		m_receiver = m_recentPlayers[0];
		return true;
	}

	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		if (m_receiver != null && WhisperUtil.IsSpeaker(m_receiver, whisper))
		{
			UpdateReceiver();
		}
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(m_receiver);
		if (bnetPlayerChange != null)
		{
			BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
			BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
			if (oldPlayer == null || oldPlayer.IsOnline() != newPlayer.IsOnline())
			{
				UpdateReceiver();
			}
		}
	}

	private BnetWhisper FindLastWhisperFromReceiver()
	{
		List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(m_receiver);
		if (whispersWithPlayer == null)
		{
			return null;
		}
		for (int num = whispersWithPlayer.Count - 1; num >= 0; num--)
		{
			BnetWhisper bnetWhisper = whispersWithPlayer[num];
			if (WhisperUtil.IsSpeaker(m_receiver, bnetWhisper))
			{
				return bnetWhisper;
			}
		}
		return null;
	}

	private void InitTransform()
	{
		base.transform.parent = BaseUI.Get().transform;
		DefaultChatTransform();
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			TransformChatForKeyboard();
		}
	}

	private void InitLastMessage()
	{
		m_initialLastMessageTextHeight = m_LastMessageText.GetTextWorldSpaceBounds().size.y;
		m_initialLastMessageShadowScaleZ = m_LastMessageShadow.transform.localScale.z;
	}

	private void InitInputBlocker()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		float worldOffset = m_Bones.m_InputBlocker.position.z - base.transform.position.z;
		GameObject gameObject = CameraUtils.CreateInputBlocker(camera, "QuickChatInputBlocker", this, worldOffset);
		m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		m_inputBlocker.AddEventListener(UIEventType.RELEASE, OnInputBlockerReleased);
	}

	private void OnInputBlockerReleased(UIEvent e)
	{
		Object.Destroy(base.gameObject);
	}

	private void InitChatLogFrame()
	{
		m_ChatLogButton.AddEventListener(UIEventType.RELEASE, OnChatLogButtonReleased);
	}

	private void OnChatLogButtonReleased(UIEvent e)
	{
		if (ChatMgr.Get().IsChatLogFrameShown())
		{
			HideChatLogFrame();
		}
		else
		{
			ShowChatLogFrame();
		}
		UpdateReceiver();
		UniversalInputManager.Get().FocusTextInput(base.gameObject);
	}

	private void ShowChatLogFrame(bool onStart = false)
	{
		m_chatLogFrame = Object.Instantiate(m_Prefabs.m_ChatLogFrame);
		bool flag = base.transform.localScale == BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.localScale;
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((((UniversalInputManager.Get().IsTouchMode() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible()) && flag) || flag)
		{
			DefaultChatTransform();
		}
		m_chatLogFrame.transform.parent = base.transform;
		m_chatLogFrame.transform.position = m_Bones.m_ChatLog.position;
		if ((((UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible()) && flag) || flag)
		{
			TransformChatForKeyboard();
		}
		GameObject obj = (onStart ? base.gameObject : m_chatLogFrame.gameObject);
		StartCoroutine(ShowChatLogFrameWhenReady(obj));
	}

	private IEnumerator ShowChatLogFrameWhenReady(GameObject obj)
	{
		while (m_chatLogFrame == null || m_chatLogFrame.IsWaitingOnMedal)
		{
			if (m_chatLogFrame == null)
			{
				yield break;
			}
			yield return null;
		}
		ChatMgr.Get().OnChatLogFrameShown();
	}

	private void HideChatLogFrame()
	{
		Object.Destroy(m_chatLogFrame.gameObject);
		m_chatLogFrame = null;
		ChatMgr.Get().OnChatLogFrameHidden();
	}

	private void InitRecentPlayerDropdown()
	{
		m_recentPlayerDropdown = Object.Instantiate(m_Prefabs.m_Dropdown);
		m_recentPlayerDropdown.transform.parent = base.transform;
		m_recentPlayerDropdown.transform.position = m_Bones.m_RecentPlayerDropdown.position;
		m_recentPlayerDropdown.setItemTextCallback(OnRecentPlayerDropdownText);
		m_recentPlayerDropdown.setItemChosenCallback(OnRecentPlayerDropdownItemChosen);
		UpdateRecentPlayerDropdown();
		m_recentPlayerDropdown.setSelection(m_receiver);
	}

	private void UpdateRecentPlayerDropdown()
	{
		m_recentPlayerDropdown.clearItems();
		for (int i = 0; i < m_recentPlayers.Count; i++)
		{
			m_recentPlayerDropdown.addItem(m_recentPlayers[i]);
		}
	}

	private string OnRecentPlayerDropdownText(object val)
	{
		return FriendUtils.GetUniqueName((BnetPlayer)val);
	}

	private void OnRecentPlayerDropdownItemChosen(object selection, object prevSelection)
	{
		BnetPlayer receiver = (BnetPlayer)selection;
		SetReceiver(receiver);
	}

	private void UpdateReceiver()
	{
		UpdateLastMessage();
		if (m_chatLogFrame != null)
		{
			m_chatLogFrame.Receiver = m_receiver;
		}
	}

	private void UpdateLastMessage()
	{
		if (m_chatLogFrame != null)
		{
			HideLastMessage();
			return;
		}
		BnetWhisper bnetWhisper = FindLastWhisperFromReceiver();
		if (bnetWhisper == null)
		{
			HideLastMessage();
			return;
		}
		m_LastMessageText.gameObject.SetActive(value: true);
		m_LastMessageText.Text = ChatUtils.GetMessage(bnetWhisper);
		TransformUtil.SetPoint(m_LastMessageText, Anchor.BOTTOM_LEFT, m_Bones.m_LastMessage, Anchor.TOP_LEFT);
		m_ReceiverNameText.gameObject.SetActive(value: true);
		if (m_receiver.IsOnline())
		{
			m_ReceiverNameText.TextColor = GameColors.PLAYER_NAME_ONLINE;
		}
		else
		{
			m_ReceiverNameText.TextColor = GameColors.PLAYER_NAME_OFFLINE;
		}
		m_ReceiverNameText.Text = FriendUtils.GetUniqueName(m_receiver);
		TransformUtil.SetPoint(m_ReceiverNameText, Anchor.BOTTOM_LEFT, m_LastMessageText, Anchor.TOP_LEFT);
		m_LastMessageShadow.SetActive(value: true);
		Bounds textWorldSpaceBounds = m_LastMessageText.GetTextWorldSpaceBounds();
		Bounds textWorldSpaceBounds2 = m_ReceiverNameText.GetTextWorldSpaceBounds();
		float num = Mathf.Max(textWorldSpaceBounds.max.y, textWorldSpaceBounds2.max.y);
		float num2 = Mathf.Min(textWorldSpaceBounds.min.y, textWorldSpaceBounds2.min.y);
		float z = (num - num2) * m_initialLastMessageShadowScaleZ / m_initialLastMessageTextHeight;
		TransformUtil.SetLocalScaleZ(m_LastMessageShadow, z);
	}

	private void HideLastMessage()
	{
		m_ReceiverNameText.gameObject.SetActive(value: false);
		m_LastMessageText.gameObject.SetActive(value: false);
		m_LastMessageShadow.SetActive(value: false);
	}

	private void CyclePrevReceiver()
	{
		int num = m_recentPlayers.FindIndex((BnetPlayer currReceiver) => m_receiver == currReceiver);
		BnetPlayer receiver = ((num != 0) ? m_recentPlayers[num - 1] : m_recentPlayers[m_recentPlayers.Count - 1]);
		SetReceiver(receiver);
	}

	private void CycleNextReceiver()
	{
		int num = m_recentPlayers.FindIndex((BnetPlayer currReceiver) => m_receiver == currReceiver);
		BnetPlayer receiver = ((num != m_recentPlayers.Count - 1) ? m_recentPlayers[num + 1] : m_recentPlayers[0]);
		SetReceiver(receiver);
	}

	private void InitInput()
	{
		FontDefinition fontDef = FontTable.Get().GetFontDef(m_InputFont);
		if (fontDef == null)
		{
			m_localizedInputFont = m_InputFont;
		}
		else
		{
			m_localizedInputFont = fontDef.m_Font;
		}
	}

	private void ShowInput(bool fromAwake)
	{
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		Rect rect = CameraUtils.CreateGUIViewportRect(bnetCamera, m_Bones.m_InputTopLeft, m_Bones.m_InputBottomRight);
		if (Localization.GetLocale() == Locale.thTH)
		{
			Vector3 vector = bnetCamera.WorldToViewportPoint(m_Bones.m_InputTopLeft.position);
			Vector3 vector2 = bnetCamera.WorldToViewportPoint(m_Bones.m_InputBottomRight.position);
			float num = (vector.y - vector2.y) * 0.1f;
			vector = new Vector3(vector.x, vector.y - num, vector.z);
			vector2 = new Vector3(vector2.x, vector2.y + num, vector2.z);
			rect = new Rect(vector.x, 1f - vector.y, vector2.x - vector.x, vector.y - vector2.y);
		}
		string pendingMessage = ChatMgr.Get().GetPendingMessage(m_receiver.GetAccountId());
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_rect = rect,
			m_preprocessCallback = OnInputPreprocess,
			m_completedCallback = OnInputComplete,
			m_canceledCallback = OnInputCanceled,
			m_updatedCallback = OnInputChanged,
			m_font = m_localizedInputFont,
			m_maxCharacters = 512,
			m_touchScreenKeyboardHideInput = true,
			m_showVirtualKeyboard = fromAwake,
			m_hideVirtualKeyboardOnComplete = (fromAwake ? true : false),
			m_text = pendingMessage
		};
		UniversalInputManager.Get().UseTextInput(parms);
	}

	private bool OnInputPreprocess(Event e)
	{
		if (m_recentPlayers.Count < 2)
		{
			return false;
		}
		if (e.type != EventType.KeyDown)
		{
			return false;
		}
		KeyCode keyCode = e.keyCode;
		bool flag = (e.modifiers & EventModifiers.Shift) != 0;
		if (keyCode == KeyCode.UpArrow || (keyCode == KeyCode.Tab && flag))
		{
			CyclePrevReceiver();
			return true;
		}
		if (keyCode == KeyCode.DownArrow || keyCode == KeyCode.Tab)
		{
			CycleNextReceiver();
			return true;
		}
		return false;
	}

	private void OnInputChanged(string input)
	{
		ChatMgr.Get().SetPendingMessage(m_receiver.GetAccountId(), input);
	}

	private void OnInputComplete(string input)
	{
		if (!string.IsNullOrEmpty(input))
		{
			BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
			if (!BnetWhisperMgr.Get().SendWhisper(m_receiver, input))
			{
				if (ChatMgr.Get().IsChatLogFrameShown())
				{
					m_chatLogFrame.m_chatLog.OnWhisperFailed();
				}
				else if (!m_receiver.IsOnline())
				{
					string message = GameStrings.Format("GLOBAL_CHAT_RECEIVER_OFFLINE", m_receiver.GetBestName());
					UIStatus.Get().AddError(message);
				}
				else if (myPlayer.IsAppearingOffline())
				{
					string message2 = GameStrings.Get("GLOBAL_CHAT_SENDER_APPEAR_OFFLINE");
					UIStatus.Get().AddError(message2);
				}
				ChatMgr.Get().AddRecentWhisperPlayerToTop(m_receiver);
			}
		}
		ChatMgr.Get().SetPendingMessage(m_receiver.GetAccountId(), null);
		if (ChatMgr.Get().IsChatLogFrameShown())
		{
			ShowInput(fromAwake: false);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		Object.Destroy(base.gameObject);
	}

	private void DefaultChatTransform()
	{
		base.transform.position = BaseUI.Get().m_Bones.m_QuickChat.position;
		base.transform.localScale = BaseUI.Get().m_Bones.m_QuickChat.localScale;
		if (m_chatLogFrame != null)
		{
			m_chatLogFrame.UpdateLayout();
		}
	}

	private void TransformChatForKeyboard()
	{
		base.transform.position = BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.position;
		base.transform.localScale = BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.localScale;
		m_Prefabs.m_Dropdown.transform.localScale = new Vector3(50f, 50f, 50f);
		if (m_chatLogFrame != null)
		{
			m_chatLogFrame.UpdateLayout();
		}
	}
}
