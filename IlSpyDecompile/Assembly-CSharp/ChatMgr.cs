using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Hearthstone;
using UnityEngine;

public class ChatMgr : MonoBehaviour
{
	public delegate void PlayerChatInfoChangedCallback(PlayerChatInfo chatInfo, object userData);

	public delegate void FriendListToggled(bool open);

	private class PlayerChatInfoChangedListener : EventListener<PlayerChatInfoChangedCallback>
	{
		public void Fire(PlayerChatInfo chatInfo)
		{
			m_callback(chatInfo, m_userData);
		}
	}

	private enum KeyboardState
	{
		None,
		Below,
		Above
	}

	public ChatMgrPrefabs m_Prefabs;

	public ChatMgrBubbleInfo m_ChatBubbleInfo;

	public Float_MobileOverride m_friendsListXOffset;

	public Float_MobileOverride m_friendsListYOffset;

	public Float_MobileOverride m_friendsListWidthPadding;

	public Float_MobileOverride m_friendsListHeightPadding;

	public float m_chatLogXOffset;

	public Float_MobileOverride m_friendsListWidth;

	private static ChatMgr s_instance;

	private List<ChatBubbleFrame> m_chatBubbleFrames = new List<ChatBubbleFrame>();

	private IChatLogUI m_chatLogUI;

	private FriendListFrame m_friendListFrame;

	private PegUIElement m_closeCatcher;

	private List<BnetPlayer> m_recentWhisperPlayers = new List<BnetPlayer>();

	private Map<BnetAccountId, string> m_pendingChatMessages = new Map<BnetAccountId, string>();

	private bool m_chatLogFrameShown;

	private Map<BnetPlayer, PlayerChatInfo> m_playerChatInfos = new Map<BnetPlayer, PlayerChatInfo>();

	private List<PlayerChatInfoChangedListener> m_playerChatInfoChangedListeners = new List<PlayerChatInfoChangedListener>();

	private KeyboardState keyboardState;

	private Rect keyboardArea = new Rect(0f, 0f, 0f, 0f);

	private FatalErrorMgr m_fatalErrorMgr;

	private Map<Renderer, int> m_friendListOriginalLayers = new Map<Renderer, int>();

	public FriendListFrame FriendListFrame => m_friendListFrame;

	public Rect KeyboardRect => keyboardArea;

	public event FriendListToggled OnFriendListToggled;

	public event Action OnChatLogShown;

	public static event Action OnStarted;

	private void Awake()
	{
		s_instance = this;
		m_fatalErrorMgr = FatalErrorMgr.Get();
		BnetWhisperMgr.Get().AddWhisperListener(OnWhisper);
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		m_fatalErrorMgr.AddErrorListener(OnFatalError);
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		touchScreenService.AddOnVirtualKeyboardShowListener(OnKeyboardShow);
		touchScreenService.AddOnVirtualKeyboardHideListener(OnKeyboardHide);
		HearthstoneApplication.Get().WillReset += WillReset;
		InitCloseCatcher();
		InitChatLogUI();
	}

	private void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= WillReset;
		}
		if (HearthstoneServices.TryGet<ITouchScreenService>(out var service))
		{
			service.RemoveOnVirtualKeyboardShowListener(OnKeyboardShow);
			service.RemoveOnVirtualKeyboardHideListener(OnKeyboardHide);
		}
		if (DialogManager.Get() != null)
		{
			DialogManager.Get().OnDialogShown -= OnDialogShown;
		}
		this.OnChatLogShown = null;
		s_instance = null;
	}

	private void Start()
	{
		DialogManager.Get().OnDialogShown += OnDialogShown;
		SoundManager.Get().Load("receive_message.prefab:8e90a827cd4a0e849953158396cd1ee1");
		UpdateLayout();
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			OnKeyboardShow();
		}
		if (ChatMgr.OnStarted != null)
		{
			ChatMgr.OnStarted();
		}
	}

	private void Update()
	{
		Rect rect = keyboardArea;
		keyboardArea = TextField.KeyboardArea;
		if (keyboardArea != rect)
		{
			UpdateLayout();
		}
	}

	public static ChatMgr Get()
	{
		return s_instance;
	}

	private void WillReset()
	{
		CleanUp();
		m_fatalErrorMgr.AddErrorListener(OnFatalError);
	}

	private KeyboardState ComputeKeyboardState()
	{
		if (keyboardArea.height > 0f)
		{
			float y = keyboardArea.y;
			float num = (float)Screen.height - keyboardArea.yMax;
			if (!(y > num))
			{
				return KeyboardState.Above;
			}
			return KeyboardState.Below;
		}
		return KeyboardState.None;
	}

	private void InitCloseCatcher()
	{
		GameObject gameObject = CameraUtils.CreateInputBlocker(BaseUI.Get().GetBnetCamera(), "CloseCatcher", this);
		m_closeCatcher = gameObject.AddComponent<PegUIElement>();
		m_closeCatcher.AddEventListener(UIEventType.RELEASE, OnCloseCatcherRelease);
		m_closeCatcher.gameObject.SetActive(value: false);
	}

	private void InitChatLogUI()
	{
		if (IsMobilePlatform())
		{
			m_chatLogUI = new MobileChatLogUI();
		}
		else
		{
			m_chatLogUI = new DesktopChatLogUI();
		}
	}

	private FriendListFrame CreateFriendsListUI()
	{
		string text = (UniversalInputManager.UsePhoneUI ? "FriendListFrame_phone.prefab:91e737585d7bfd2449b46fbecb87ded7" : "FriendListFrame.prefab:cdf3b7f04b5ed45cb8ba0160d43a5bf6");
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.parent = base.transform;
		return gameObject.GetComponent<FriendListFrame>();
	}

	public void UpdateLayout()
	{
		if (m_friendListFrame != null || m_chatLogUI.IsShowing)
		{
			UpdateLayoutForOnScreenKeyboard();
		}
		UpdateChatBubbleParentLayout();
	}

	private void UpdateLayoutForOnScreenKeyboard()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			UpdateLayoutForOnScreenKeyboardOnPhone();
			return;
		}
		keyboardState = ComputeKeyboardState();
		bool flag = IsMobilePlatform();
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			flag = false;
		}
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		float num = bnetCamera.orthographicSize * 2f;
		float num2 = num * bnetCamera.aspect;
		float num3 = bnetCamera.transform.position.y + num / 2f;
		float num4 = bnetCamera.transform.position.x - num2 / 2f;
		float num5 = 0f;
		if (keyboardState != KeyboardState.None && flag)
		{
			num5 = num * keyboardArea.height / (float)Screen.height;
		}
		float num6 = 0f;
		if (m_friendListFrame != null)
		{
			OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(BaseUI.Get().m_BnetBar.m_friendButton.gameObject);
			if (flag)
			{
				float num7 = ((keyboardState == KeyboardState.Below) ? num5 : (orientedBounds.Extents[1].y * 2f));
				m_friendListFrame.SetWorldHeight(num - num7);
			}
			OrientedBounds orientedBounds2 = m_friendListFrame.ComputeFrameWorldBounds();
			if (orientedBounds2 != null)
			{
				if (!flag || keyboardState != KeyboardState.Below)
				{
					float x = num4 + orientedBounds2.Extents[0].x + orientedBounds2.CenterOffset.x + (float)m_friendsListXOffset;
					float y = orientedBounds.GetTrueCenterPosition().y + orientedBounds.Extents[1].y + orientedBounds2.Extents[1].y + orientedBounds2.CenterOffset.y;
					m_friendListFrame.SetWorldPosition(x, y);
				}
				else if (flag && keyboardState == KeyboardState.Below)
				{
					float x2 = num4 + orientedBounds2.Extents[0].x + orientedBounds2.CenterOffset.x + (float)m_friendsListXOffset;
					float y2 = bnetCamera.transform.position.y - num / 2f + num5 + orientedBounds2.Extents[1].y + orientedBounds2.CenterOffset.y;
					m_friendListFrame.SetWorldPosition(x2, y2);
				}
				num6 = orientedBounds2.Extents[0].magnitude * 2f;
			}
		}
		if (m_chatLogUI.IsShowing)
		{
			ChatFrames component = m_chatLogUI.GameObject.GetComponent<ChatFrames>();
			if (component != null)
			{
				float num8 = num3;
				if (keyboardState == KeyboardState.Above)
				{
					num8 -= num5;
				}
				float num9 = num - num5;
				if (keyboardState == KeyboardState.None && flag)
				{
					OrientedBounds orientedBounds3 = TransformUtil.ComputeOrientedWorldBounds(BaseUI.Get().m_BnetBar.m_friendButton.gameObject);
					num9 -= orientedBounds3.Extents[1].y * 2f;
				}
				float num10 = num4;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num10 += num6 + (float)m_friendsListXOffset + m_chatLogXOffset;
				}
				float num11 = num2;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num11 -= num6 + (float)m_friendsListXOffset + m_chatLogXOffset;
				}
				component.chatLogFrame.SetWorldRect(num10, num8, num11, num9);
			}
		}
		OnChatFramesMoved();
	}

	private void UpdateLayoutForOnScreenKeyboardOnPhone()
	{
		keyboardState = ComputeKeyboardState();
		bool flag = UniversalInputManager.Get().IsTouchMode();
		float horizontalMargin = BnetBar.Get().HorizontalMargin;
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		float num = bnetCamera.orthographicSize * 2f;
		float num2 = num * bnetCamera.aspect - horizontalMargin * 2f;
		float num3 = bnetCamera.transform.position.y + num / 2f;
		float num4 = bnetCamera.transform.position.x - num2 / 2f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		if (keyboardState != KeyboardState.None && flag)
		{
			num5 = num * keyboardArea.height / (float)Screen.height;
			num6 = num2 * keyboardArea.width / (float)Screen.width;
			num7 = num2 * keyboardArea.xMin / (float)Screen.width;
		}
		if (m_friendListFrame != null)
		{
			float x = num4 + (float)m_friendsListXOffset;
			float y = num3 + (float)m_friendsListYOffset;
			float width = (float)m_friendsListWidth + (float)m_friendsListWidthPadding;
			float height = num + (float)m_friendsListHeightPadding;
			m_friendListFrame.SetWorldRect(x, y, width, height);
		}
		if (m_chatLogUI.IsShowing)
		{
			ChatFrames component = m_chatLogUI.GameObject.GetComponent<ChatFrames>();
			if (component != null)
			{
				float num8 = num3;
				if (keyboardState == KeyboardState.Above)
				{
					num8 -= num5;
				}
				float height2 = num - num5;
				float num9 = num4 + num7;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num9 += (float)m_friendsListWidth;
				}
				float num10 = ((num6 == 0f) ? num2 : num6);
				if (!UniversalInputManager.UsePhoneUI)
				{
					num10 -= (float)m_friendsListWidth;
				}
				component.chatLogFrame.SetWorldRect(num9, num8, num10, height2);
			}
		}
		OnChatFramesMoved();
	}

	public bool IsChatLogFrameShown()
	{
		if (IsMobilePlatform())
		{
			return IsChatLogUIShowing();
		}
		return m_chatLogFrameShown;
	}

	public bool IsChatLogUIShowing()
	{
		return m_chatLogUI.IsShowing;
	}

	private void OnCloseCatcherRelease(UIEvent e)
	{
		if (m_chatLogUI != null && m_chatLogUI.IsShowing)
		{
			m_chatLogUI.Hide();
		}
		if (FriendListFrame != null && FriendListFrame.IsInEditMode)
		{
			FriendListFrame.ExitRemoveFriendsMode();
		}
		else if (FriendListFrame != null && FriendListFrame.IsFlyoutOpen)
		{
			FriendListFrame.CloseFlyoutMenu();
		}
		else
		{
			CloseFriendsList();
		}
	}

	public bool IsFriendListShowing()
	{
		if (!(m_friendListFrame == null))
		{
			return m_friendListFrame.gameObject.activeSelf;
		}
		return false;
	}

	public void ShowFriendsList()
	{
		if ((SetRotationManager.Get() == null || !SetRotationManager.Get().CheckForSetRotationRollover()) && (PlayerMigrationManager.Get() == null || !PlayerMigrationManager.Get().CheckForPlayerMigrationRequired()))
		{
			if (m_friendListFrame == null)
			{
				m_friendListFrame = CreateFriendsListUI();
			}
			TransformUtil.SetPosZ(m_closeCatcher, m_friendListFrame.transform.position.z + 100f);
			m_friendListFrame.gameObject.SetActive(value: true);
			m_closeCatcher.gameObject.SetActive(value: true);
			UpdateLayout();
			m_friendListFrame.SetItemsCameraEnabled(enable: false);
			StartCoroutine(ShowFriendsListWhenReady());
		}
	}

	private IEnumerator ShowFriendsListWhenReady()
	{
		while (m_friendListFrame == null || !m_friendListFrame.IsReady)
		{
			if (m_friendListFrame == null)
			{
				yield break;
			}
			yield return null;
		}
		m_friendListFrame.UpdateFriendItems();
		Get().FriendListFrame.items.RecalculateItemSizeAndOffsets(ignoreCurrentPosition: true);
		m_friendListFrame.SetItemsCameraEnabled(enable: true);
		if (this.OnFriendListToggled != null)
		{
			this.OnFriendListToggled(open: true);
		}
	}

	private void HideFriendsList()
	{
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().m_activeFSGMenu = -1L;
		}
		if (IsFriendListShowing())
		{
			m_friendListFrame.gameObject.SetActive(value: false);
		}
		if (m_closeCatcher != null)
		{
			m_closeCatcher.gameObject.SetActive(value: false);
		}
		if (this.OnFriendListToggled != null)
		{
			this.OnFriendListToggled(open: false);
		}
	}

	public void CloseFriendsList()
	{
		DestroyFriendListFrame();
	}

	public void GoBack()
	{
		if (IsFriendListShowing())
		{
			CloseChatUI();
		}
		else if (m_chatLogUI.IsShowing)
		{
			m_chatLogUI.Hide();
			ShowFriendsList();
		}
	}

	public void CloseChatUI(bool closeFriendList = true)
	{
		if (m_chatLogUI.IsShowing)
		{
			m_chatLogUI.Hide();
		}
		if (closeFriendList)
		{
			CloseFriendsList();
		}
	}

	public void CleanUp()
	{
		DestroyFriendListFrame();
	}

	private void DestroyFriendListFrame()
	{
		HideFriendsList();
		if (!(m_friendListFrame == null))
		{
			UnityEngine.Object.Destroy(m_friendListFrame.gameObject);
			m_friendListFrame = null;
		}
	}

	public void SetPendingMessage(BnetAccountId playerID, string message)
	{
		m_pendingChatMessages[playerID] = message;
	}

	public string GetPendingMessage(BnetAccountId playerID)
	{
		string value = "";
		m_pendingChatMessages.TryGetValue(playerID, out value);
		return value;
	}

	public List<BnetPlayer> GetRecentWhisperPlayers()
	{
		return m_recentWhisperPlayers;
	}

	public void AddRecentWhisperPlayerToTop(BnetPlayer player)
	{
		int num = m_recentWhisperPlayers.FindIndex((BnetPlayer currPlayer) => currPlayer == player);
		if (num < 0)
		{
			if (m_recentWhisperPlayers.Count == 10)
			{
				m_recentWhisperPlayers.RemoveAt(m_recentWhisperPlayers.Count - 1);
			}
		}
		else
		{
			m_recentWhisperPlayers.RemoveAt(num);
		}
		m_recentWhisperPlayers.Insert(0, player);
	}

	public void AddRecentWhisperPlayerToBottom(BnetPlayer player)
	{
		if (!m_recentWhisperPlayers.Contains(player))
		{
			if (m_recentWhisperPlayers.Count == 10)
			{
				m_recentWhisperPlayers.RemoveAt(m_recentWhisperPlayers.Count - 1);
			}
			m_recentWhisperPlayers.Add(player);
		}
	}

	public void AddPlayerChatInfoChangedListener(PlayerChatInfoChangedCallback callback)
	{
		AddPlayerChatInfoChangedListener(callback, null);
	}

	public void AddPlayerChatInfoChangedListener(PlayerChatInfoChangedCallback callback, object userData)
	{
		PlayerChatInfoChangedListener playerChatInfoChangedListener = new PlayerChatInfoChangedListener();
		playerChatInfoChangedListener.SetCallback(callback);
		playerChatInfoChangedListener.SetUserData(userData);
		if (!m_playerChatInfoChangedListeners.Contains(playerChatInfoChangedListener))
		{
			m_playerChatInfoChangedListeners.Add(playerChatInfoChangedListener);
		}
	}

	public bool RemovePlayerChatInfoChangedListener(PlayerChatInfoChangedCallback callback)
	{
		return RemovePlayerChatInfoChangedListener(callback, null);
	}

	public bool RemovePlayerChatInfoChangedListener(PlayerChatInfoChangedCallback callback, object userData)
	{
		PlayerChatInfoChangedListener playerChatInfoChangedListener = new PlayerChatInfoChangedListener();
		playerChatInfoChangedListener.SetCallback(callback);
		playerChatInfoChangedListener.SetUserData(userData);
		return m_playerChatInfoChangedListeners.Remove(playerChatInfoChangedListener);
	}

	public PlayerChatInfo GetPlayerChatInfo(BnetPlayer player)
	{
		PlayerChatInfo value = null;
		m_playerChatInfos.TryGetValue(player, out value);
		return value;
	}

	public PlayerChatInfo RegisterPlayerChatInfo(BnetPlayer player)
	{
		if (!m_playerChatInfos.TryGetValue(player, out var value))
		{
			value = new PlayerChatInfo();
			value.SetPlayer(player);
			m_playerChatInfos.Add(player, value);
		}
		return value;
	}

	public void UpdateFriendItemsWhenAvailable()
	{
		if (m_friendListFrame != null)
		{
			m_friendListFrame.UpdateFriendItemsWhenAvailable();
		}
	}

	public void OnFriendListOpened()
	{
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			OnKeyboardShow();
		}
		else
		{
			UpdateChatBubbleParentLayout();
		}
	}

	public void OnFriendListClosed()
	{
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			OnKeyboardShow();
		}
		else
		{
			UpdateChatBubbleParentLayout();
		}
	}

	public void OnFriendListFriendSelected(BnetPlayer friend)
	{
		ShowChatForPlayer(friend);
		if (m_friendListFrame != null)
		{
			m_friendListFrame.SelectFriend(friend);
		}
	}

	public void OnChatLogFrameShown()
	{
		m_chatLogFrameShown = true;
	}

	public void OnChatLogFrameHidden()
	{
		m_chatLogFrameShown = false;
	}

	public void OnChatReceiverChanged(BnetPlayer player)
	{
		UpdatePlayerFocusTime(player);
	}

	public void OnChatFramesMoved()
	{
		UpdateChatBubbleParentLayout();
	}

	public bool HandleKeyboardInput()
	{
		if (m_fatalErrorMgr.HasError())
		{
			return false;
		}
		if (InputCollection.GetKeyUp(KeyCode.Escape) && m_chatLogUI.IsShowing)
		{
			m_chatLogUI.Hide();
			return true;
		}
		if (IsMobilePlatform() && m_chatLogUI.IsShowing && InputCollection.GetKeyUp(KeyCode.Escape))
		{
			m_chatLogUI.GoBack();
			return true;
		}
		return false;
	}

	public void HandleGUIInput()
	{
		if (!m_fatalErrorMgr.HasError() && !IsMobilePlatform())
		{
			HandleGUIInputForQuickChat();
		}
	}

	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		BnetPlayer theirPlayer = WhisperUtil.GetTheirPlayer(whisper);
		AddRecentWhisperPlayerToTop(theirPlayer);
		PlayerChatInfo playerChatInfo = RegisterPlayerChatInfo(WhisperUtil.GetTheirPlayer(whisper));
		try
		{
			if (m_chatLogUI.IsShowing && WhisperUtil.IsSpeakerOrReceiver(m_chatLogUI.Receiver, whisper) && IsMobilePlatform())
			{
				playerChatInfo.SetLastSeenWhisper(whisper);
			}
			else
			{
				PopupNewChatBubble(whisper);
			}
		}
		finally
		{
			FireChatInfoChangedEvent(playerChatInfo);
		}
	}

	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		if (removedFriends == null)
		{
			return;
		}
		foreach (BnetPlayer friend in removedFriends)
		{
			int num = m_recentWhisperPlayers.FindIndex((BnetPlayer player) => friend == player);
			if (num >= 0)
			{
				m_recentWhisperPlayers.RemoveAt(num);
			}
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		m_fatalErrorMgr.RemoveErrorListener(OnFatalError);
		CleanUp();
	}

	private void HandleGUIInputForQuickChat()
	{
		if (m_chatLogUI == null)
		{
			return;
		}
		if (!m_chatLogUI.IsShowing)
		{
			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
			{
				ShowChatForPlayer(GetMostRecentWhisperedPlayer());
			}
		}
		else if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Escape)
		{
			m_chatLogUI.Hide();
		}
	}

	public bool IsMobilePlatform()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return PlatformSettings.OS != OSCategory.PC;
		}
		return false;
	}

	private void ShowChatForPlayer(BnetPlayer player)
	{
		if (player != null)
		{
			AddRecentWhisperPlayerToTop(player);
			PlayerChatInfo playerChatInfo = RegisterPlayerChatInfo(player);
			List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(player);
			if (whispersWithPlayer != null)
			{
				playerChatInfo.SetLastSeenWhisper(whispersWithPlayer.LastOrDefault((BnetWhisper whisper) => WhisperUtil.IsSpeaker(player, whisper)));
				FireChatInfoChangedEvent(playerChatInfo);
			}
		}
		if (m_chatLogUI.IsShowing)
		{
			m_chatLogUI.Hide();
		}
		if (FriendListFrame != null && FriendListFrame.IsFlyoutOpen)
		{
			FriendListFrame.CloseFlyoutMenu();
		}
		if (!m_chatLogUI.IsShowing)
		{
			if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
			{
				OptionsMenu.Get().Hide();
			}
			if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
			{
				MiscellaneousMenu.Get().Hide();
			}
			if (BnetBar.Get() != null)
			{
				BnetBar.Get().HideGameMenu();
			}
			m_chatLogUI.ShowForPlayer(GetMostRecentWhisperedPlayer());
			UpdateLayout();
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				CloseFriendsList();
			}
			if (this.OnChatLogShown != null)
			{
				this.OnChatLogShown();
			}
		}
	}

	private BnetPlayer GetMostRecentWhisperedPlayer()
	{
		if (m_recentWhisperPlayers.Count <= 0)
		{
			return null;
		}
		return m_recentWhisperPlayers[0];
	}

	private void UpdatePlayerFocusTime(BnetPlayer player)
	{
		PlayerChatInfo playerChatInfo = RegisterPlayerChatInfo(player);
		playerChatInfo.SetLastFocusTime(Time.realtimeSinceStartup);
		FireChatInfoChangedEvent(playerChatInfo);
	}

	private void FireChatInfoChangedEvent(PlayerChatInfo chatInfo)
	{
		PlayerChatInfoChangedListener[] array = m_playerChatInfoChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(chatInfo);
		}
	}

	private void UpdateChatBubbleParentLayout()
	{
		if (BaseUI.Get().GetChatBubbleBone() != null)
		{
			m_ChatBubbleInfo.m_Parent.transform.position = BaseUI.Get().GetChatBubbleBone().transform.position;
		}
	}

	private void UpdateChatBubbleLayout()
	{
		int count = m_chatBubbleFrames.Count;
		if (count != 0)
		{
			Component dst = m_ChatBubbleInfo.m_Parent;
			for (int num = count - 1; num >= 0; num--)
			{
				ChatBubbleFrame chatBubbleFrame = m_chatBubbleFrames[num];
				Anchor dstAnchor = (UniversalInputManager.UsePhoneUI ? Anchor.BOTTOM_LEFT : Anchor.TOP_LEFT);
				Anchor srcAnchor = ((!UniversalInputManager.UsePhoneUI) ? Anchor.BOTTOM_LEFT : Anchor.TOP_LEFT);
				TransformUtil.SetPoint(chatBubbleFrame, srcAnchor, dst, dstAnchor, Vector3.zero);
				dst = chatBubbleFrame;
			}
		}
	}

	private void PopupNewChatBubble(BnetWhisper whisper)
	{
		ChatBubbleFrame chatBubbleFrame = CreateChatBubble(whisper);
		m_chatBubbleFrames.Add(chatBubbleFrame);
		UpdateChatBubbleParentLayout();
		chatBubbleFrame.transform.parent = m_ChatBubbleInfo.m_Parent.transform;
		chatBubbleFrame.transform.localScale = chatBubbleFrame.m_ScaleOverride;
		SoundManager.Get().LoadAndPlay("receive_message.prefab:8e90a827cd4a0e849953158396cd1ee1");
		Hashtable args = iTween.Hash("scale", chatBubbleFrame.m_VisualRoot.transform.localScale, "time", m_ChatBubbleInfo.m_ScaleInSec, "easeType", m_ChatBubbleInfo.m_ScaleInEaseType, "oncomplete", "OnChatBubbleScaleInComplete", "oncompleteparams", chatBubbleFrame, "oncompletetarget", base.gameObject);
		chatBubbleFrame.m_VisualRoot.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
		iTween.ScaleTo(chatBubbleFrame.m_VisualRoot, args);
		MoveChatBubbles(chatBubbleFrame);
	}

	private ChatBubbleFrame CreateChatBubble(BnetWhisper whisper)
	{
		ChatBubbleFrame chatBubbleFrame = InstantiateChatBubble(m_Prefabs.m_ChatBubbleOneLineFrame, whisper);
		if (!chatBubbleFrame.DoesMessageFit())
		{
			UnityEngine.Object.Destroy(chatBubbleFrame.gameObject);
			chatBubbleFrame = InstantiateChatBubble(m_Prefabs.m_ChatBubbleSmallFrame, whisper);
		}
		SceneUtils.SetLayer(chatBubbleFrame, GameLayer.BattleNetDialog);
		return chatBubbleFrame;
	}

	private ChatBubbleFrame InstantiateChatBubble(ChatBubbleFrame prefab, BnetWhisper whisper)
	{
		ChatBubbleFrame chatBubbleFrame = UnityEngine.Object.Instantiate(prefab);
		chatBubbleFrame.SetWhisper(whisper);
		chatBubbleFrame.GetComponent<PegUIElement>().AddEventListener(UIEventType.RELEASE, OnChatBubbleReleased);
		return chatBubbleFrame;
	}

	private void MoveChatBubbles(ChatBubbleFrame newBubbleFrame)
	{
		Anchor dstAnchor = Anchor.TOP_LEFT;
		Anchor srcAnchor = Anchor.BOTTOM_LEFT;
		if ((bool)UniversalInputManager.UsePhoneUI && m_ChatBubbleInfo.m_Parent.transform.localPosition.y > -900f)
		{
			dstAnchor = Anchor.BOTTOM_LEFT;
			srcAnchor = Anchor.TOP_LEFT;
		}
		TransformUtil.SetPoint(newBubbleFrame, srcAnchor, m_ChatBubbleInfo.m_Parent, dstAnchor, Vector3.zero);
		int count = m_chatBubbleFrames.Count;
		if (count != 1)
		{
			Vector3[] array = new Vector3[count - 1];
			Component dst = newBubbleFrame;
			for (int num = count - 2; num >= 0; num--)
			{
				ChatBubbleFrame chatBubbleFrame = m_chatBubbleFrames[num];
				array[num] = chatBubbleFrame.transform.position;
				TransformUtil.SetPoint(chatBubbleFrame, srcAnchor, dst, dstAnchor, Vector3.zero);
				dst = chatBubbleFrame;
			}
			for (int num2 = count - 2; num2 >= 0; num2--)
			{
				ChatBubbleFrame chatBubbleFrame2 = m_chatBubbleFrames[num2];
				Hashtable args = iTween.Hash("islocal", true, "position", chatBubbleFrame2.transform.localPosition, "time", m_ChatBubbleInfo.m_MoveOverSec, "easeType", m_ChatBubbleInfo.m_MoveOverEaseType);
				chatBubbleFrame2.transform.position = array[num2];
				iTween.Stop(chatBubbleFrame2.gameObject, "move");
				iTween.MoveTo(chatBubbleFrame2.gameObject, args);
			}
		}
	}

	private void OnChatBubbleScaleInComplete(ChatBubbleFrame bubbleFrame)
	{
		Hashtable args = iTween.Hash("amount", 0f, "delay", m_ChatBubbleInfo.m_HoldSec, "time", m_ChatBubbleInfo.m_FadeOutSec, "easeType", m_ChatBubbleInfo.m_FadeOutEaseType, "oncomplete", "OnChatBubbleFadeOutComplete", "oncompleteparams", bubbleFrame, "oncompletetarget", base.gameObject);
		iTween.FadeTo(bubbleFrame.gameObject, args);
	}

	private void OnChatBubbleFadeOutComplete(ChatBubbleFrame bubbleFrame)
	{
		UnityEngine.Object.Destroy(bubbleFrame.gameObject);
		m_chatBubbleFrames.Remove(bubbleFrame);
	}

	private void RemoveAllChatBubbles()
	{
		foreach (ChatBubbleFrame chatBubbleFrame in m_chatBubbleFrames)
		{
			UnityEngine.Object.Destroy(chatBubbleFrame.gameObject);
		}
		m_chatBubbleFrames.Clear();
	}

	private void OnChatBubbleReleased(UIEvent e)
	{
		BnetPlayer theirPlayer = WhisperUtil.GetTheirPlayer(e.GetElement().GetComponent<ChatBubbleFrame>().GetWhisper());
		ShowChatForPlayer(theirPlayer);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			RemoveAllChatBubbles();
		}
	}

	public void OnKeyboardShow()
	{
		if (m_chatLogUI.IsShowing && BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.position != m_chatLogUI.GameObject.transform.position)
		{
			ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
			touchScreenService.RemoveOnVirtualKeyboardShowListener(OnKeyboardShow);
			touchScreenService.RemoveOnVirtualKeyboardHideListener(OnKeyboardHide);
			m_chatLogUI.Hide();
			m_chatLogUI.ShowForPlayer(GetMostRecentWhisperedPlayer());
			touchScreenService.AddOnVirtualKeyboardShowListener(OnKeyboardShow);
			touchScreenService.AddOnVirtualKeyboardHideListener(OnKeyboardHide);
		}
		if ((bool)BnetBarFriendButton.Get())
		{
			Vector2 vector = new Vector2(0f, Screen.height - 150);
			GameObject dst = BnetBarFriendButton.Get().gameObject;
			TransformUtil.SetPoint(m_ChatBubbleInfo.m_Parent, Anchor.BOTTOM_LEFT, dst, Anchor.BOTTOM_RIGHT, vector);
		}
		int count = m_chatBubbleFrames.Count;
		if (count != 0)
		{
			Component dst2 = m_ChatBubbleInfo.m_Parent;
			for (int num = count - 1; num >= 0; num--)
			{
				ChatBubbleFrame chatBubbleFrame = m_chatBubbleFrames[num];
				TransformUtil.SetPoint(chatBubbleFrame, Anchor.TOP_LEFT, dst2, Anchor.BOTTOM_LEFT, Vector3.zero);
				dst2 = chatBubbleFrame;
			}
		}
	}

	public void OnKeyboardHide()
	{
		UpdateLayout();
		UpdateChatBubbleLayout();
	}

	private void OnDialogShown()
	{
		if (DialogManager.Get().ShowingHighPriorityDialog() && m_chatLogUI.IsShowing)
		{
			m_chatLogUI.Hide();
		}
	}
}
