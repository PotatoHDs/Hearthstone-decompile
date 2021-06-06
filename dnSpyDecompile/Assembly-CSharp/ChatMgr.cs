using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Hearthstone;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class ChatMgr : MonoBehaviour
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000730 RID: 1840 RVA: 0x00029060 File Offset: 0x00027260
	// (remove) Token: 0x06000731 RID: 1841 RVA: 0x00029098 File Offset: 0x00027298
	public event ChatMgr.FriendListToggled OnFriendListToggled;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000732 RID: 1842 RVA: 0x000290D0 File Offset: 0x000272D0
	// (remove) Token: 0x06000733 RID: 1843 RVA: 0x00029108 File Offset: 0x00027308
	public event Action OnChatLogShown;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000734 RID: 1844 RVA: 0x00029140 File Offset: 0x00027340
	// (remove) Token: 0x06000735 RID: 1845 RVA: 0x00029174 File Offset: 0x00027374
	public static event Action OnStarted;

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000736 RID: 1846 RVA: 0x000291A7 File Offset: 0x000273A7
	public FriendListFrame FriendListFrame
	{
		get
		{
			return this.m_friendListFrame;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000737 RID: 1847 RVA: 0x000291AF File Offset: 0x000273AF
	public Rect KeyboardRect
	{
		get
		{
			return this.keyboardArea;
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x000291B8 File Offset: 0x000273B8
	private void Awake()
	{
		ChatMgr.s_instance = this;
		this.m_fatalErrorMgr = FatalErrorMgr.Get();
		BnetWhisperMgr.Get().AddWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		this.m_fatalErrorMgr.AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		touchScreenService.AddOnVirtualKeyboardShowListener(new Action(this.OnKeyboardShow));
		touchScreenService.AddOnVirtualKeyboardHideListener(new Action(this.OnKeyboardHide));
		HearthstoneApplication.Get().WillReset += this.WillReset;
		this.InitCloseCatcher();
		this.InitChatLogUI();
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00029268 File Offset: 0x00027468
	private void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= this.WillReset;
		}
		ITouchScreenService touchScreenService;
		if (HearthstoneServices.TryGet<ITouchScreenService>(out touchScreenService))
		{
			touchScreenService.RemoveOnVirtualKeyboardShowListener(new Action(this.OnKeyboardShow));
			touchScreenService.RemoveOnVirtualKeyboardHideListener(new Action(this.OnKeyboardHide));
		}
		if (DialogManager.Get() != null)
		{
			DialogManager.Get().OnDialogShown -= this.OnDialogShown;
		}
		this.OnChatLogShown = null;
		ChatMgr.s_instance = null;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x000292F4 File Offset: 0x000274F4
	private void Start()
	{
		DialogManager.Get().OnDialogShown += this.OnDialogShown;
		SoundManager.Get().Load("receive_message.prefab:8e90a827cd4a0e849953158396cd1ee1");
		this.UpdateLayout();
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			this.OnKeyboardShow();
		}
		if (ChatMgr.OnStarted != null)
		{
			ChatMgr.OnStarted();
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00029358 File Offset: 0x00027558
	private void Update()
	{
		Rect rhs = this.keyboardArea;
		this.keyboardArea = TextField.KeyboardArea;
		if (this.keyboardArea != rhs)
		{
			this.UpdateLayout();
		}
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002938B File Offset: 0x0002758B
	public static ChatMgr Get()
	{
		return ChatMgr.s_instance;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00029392 File Offset: 0x00027592
	private void WillReset()
	{
		this.CleanUp();
		this.m_fatalErrorMgr.AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x000293B4 File Offset: 0x000275B4
	private ChatMgr.KeyboardState ComputeKeyboardState()
	{
		if (this.keyboardArea.height <= 0f)
		{
			return ChatMgr.KeyboardState.None;
		}
		float y = this.keyboardArea.y;
		float num = (float)Screen.height - this.keyboardArea.yMax;
		if (y <= num)
		{
			return ChatMgr.KeyboardState.Above;
		}
		return ChatMgr.KeyboardState.Below;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x000293FC File Offset: 0x000275FC
	private void InitCloseCatcher()
	{
		GameObject gameObject = CameraUtils.CreateInputBlocker(BaseUI.Get().GetBnetCamera(), "CloseCatcher", this);
		this.m_closeCatcher = gameObject.AddComponent<PegUIElement>();
		this.m_closeCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCloseCatcherRelease));
		this.m_closeCatcher.gameObject.SetActive(false);
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x00029455 File Offset: 0x00027655
	private void InitChatLogUI()
	{
		if (this.IsMobilePlatform())
		{
			this.m_chatLogUI = new MobileChatLogUI();
			return;
		}
		this.m_chatLogUI = new DesktopChatLogUI();
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00029478 File Offset: 0x00027678
	private FriendListFrame CreateFriendsListUI()
	{
		string input = UniversalInputManager.UsePhoneUI ? "FriendListFrame_phone.prefab:91e737585d7bfd2449b46fbecb87ded7" : "FriendListFrame.prefab:cdf3b7f04b5ed45cb8ba0160d43a5bf6";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.parent = base.transform;
		return gameObject.GetComponent<FriendListFrame>();
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x000294D2 File Offset: 0x000276D2
	public void UpdateLayout()
	{
		if (this.m_friendListFrame != null || this.m_chatLogUI.IsShowing)
		{
			this.UpdateLayoutForOnScreenKeyboard();
		}
		this.UpdateChatBubbleParentLayout();
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x000294FC File Offset: 0x000276FC
	private void UpdateLayoutForOnScreenKeyboard()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.UpdateLayoutForOnScreenKeyboardOnPhone();
			return;
		}
		this.keyboardState = this.ComputeKeyboardState();
		bool flag = this.IsMobilePlatform();
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
		if (this.keyboardState > ChatMgr.KeyboardState.None && flag)
		{
			num5 = num * this.keyboardArea.height / (float)Screen.height;
		}
		float num6 = 0f;
		if (this.m_friendListFrame != null)
		{
			OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(BaseUI.Get().m_BnetBar.m_friendButton.gameObject, true);
			if (flag)
			{
				float num7 = (this.keyboardState == ChatMgr.KeyboardState.Below) ? num5 : (orientedBounds.Extents[1].y * 2f);
				this.m_friendListFrame.SetWorldHeight(num - num7);
			}
			OrientedBounds orientedBounds2 = this.m_friendListFrame.ComputeFrameWorldBounds();
			if (orientedBounds2 != null)
			{
				if (!flag || this.keyboardState != ChatMgr.KeyboardState.Below)
				{
					float x = num4 + orientedBounds2.Extents[0].x + orientedBounds2.CenterOffset.x + this.m_friendsListXOffset;
					float y = orientedBounds.GetTrueCenterPosition().y + orientedBounds.Extents[1].y + orientedBounds2.Extents[1].y + orientedBounds2.CenterOffset.y;
					this.m_friendListFrame.SetWorldPosition(x, y);
				}
				else if (flag && this.keyboardState == ChatMgr.KeyboardState.Below)
				{
					float x2 = num4 + orientedBounds2.Extents[0].x + orientedBounds2.CenterOffset.x + this.m_friendsListXOffset;
					float y2 = bnetCamera.transform.position.y - num / 2f + num5 + orientedBounds2.Extents[1].y + orientedBounds2.CenterOffset.y;
					this.m_friendListFrame.SetWorldPosition(x2, y2);
				}
				num6 = orientedBounds2.Extents[0].magnitude * 2f;
			}
		}
		if (this.m_chatLogUI.IsShowing)
		{
			ChatFrames component = this.m_chatLogUI.GameObject.GetComponent<ChatFrames>();
			if (component != null)
			{
				float num8 = num3;
				if (this.keyboardState == ChatMgr.KeyboardState.Above)
				{
					num8 -= num5;
				}
				float num9 = num - num5;
				if (this.keyboardState == ChatMgr.KeyboardState.None && flag)
				{
					OrientedBounds orientedBounds3 = TransformUtil.ComputeOrientedWorldBounds(BaseUI.Get().m_BnetBar.m_friendButton.gameObject, true);
					num9 -= orientedBounds3.Extents[1].y * 2f;
				}
				float num10 = num4;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num10 += num6 + this.m_friendsListXOffset + this.m_chatLogXOffset;
				}
				float num11 = num2;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num11 -= num6 + this.m_friendsListXOffset + this.m_chatLogXOffset;
				}
				component.chatLogFrame.SetWorldRect(num10, num8, num11, num9);
			}
		}
		this.OnChatFramesMoved();
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00029870 File Offset: 0x00027A70
	private void UpdateLayoutForOnScreenKeyboardOnPhone()
	{
		this.keyboardState = this.ComputeKeyboardState();
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
		if (this.keyboardState > ChatMgr.KeyboardState.None && flag)
		{
			num5 = num * this.keyboardArea.height / (float)Screen.height;
			num6 = num2 * this.keyboardArea.width / (float)Screen.width;
			num7 = num2 * this.keyboardArea.xMin / (float)Screen.width;
		}
		if (this.m_friendListFrame != null)
		{
			float x = num4 + this.m_friendsListXOffset;
			float y = num3 + this.m_friendsListYOffset;
			float width = this.m_friendsListWidth + this.m_friendsListWidthPadding;
			float height = num + this.m_friendsListHeightPadding;
			this.m_friendListFrame.SetWorldRect(x, y, width, height);
		}
		if (this.m_chatLogUI.IsShowing)
		{
			ChatFrames component = this.m_chatLogUI.GameObject.GetComponent<ChatFrames>();
			if (component != null)
			{
				float num8 = num3;
				if (this.keyboardState == ChatMgr.KeyboardState.Above)
				{
					num8 -= num5;
				}
				float height2 = num - num5;
				float num9 = num4 + num7;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num9 += this.m_friendsListWidth;
				}
				float num10 = (num6 == 0f) ? num2 : num6;
				if (!UniversalInputManager.UsePhoneUI)
				{
					num10 -= this.m_friendsListWidth;
				}
				component.chatLogFrame.SetWorldRect(num9, num8, num10, height2);
			}
		}
		this.OnChatFramesMoved();
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00029A7D File Offset: 0x00027C7D
	public bool IsChatLogFrameShown()
	{
		if (this.IsMobilePlatform())
		{
			return this.IsChatLogUIShowing();
		}
		return this.m_chatLogFrameShown;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00029A94 File Offset: 0x00027C94
	public bool IsChatLogUIShowing()
	{
		return this.m_chatLogUI.IsShowing;
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00029AA4 File Offset: 0x00027CA4
	private void OnCloseCatcherRelease(UIEvent e)
	{
		if (this.m_chatLogUI != null && this.m_chatLogUI.IsShowing)
		{
			this.m_chatLogUI.Hide();
		}
		if (this.FriendListFrame != null && this.FriendListFrame.IsInEditMode)
		{
			this.FriendListFrame.ExitRemoveFriendsMode();
			return;
		}
		if (this.FriendListFrame != null && this.FriendListFrame.IsFlyoutOpen)
		{
			this.FriendListFrame.CloseFlyoutMenu();
			return;
		}
		this.CloseFriendsList();
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00029B25 File Offset: 0x00027D25
	public bool IsFriendListShowing()
	{
		return !(this.m_friendListFrame == null) && this.m_friendListFrame.gameObject.activeSelf;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00029B48 File Offset: 0x00027D48
	public void ShowFriendsList()
	{
		if (SetRotationManager.Get() != null && SetRotationManager.Get().CheckForSetRotationRollover())
		{
			return;
		}
		if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			return;
		}
		if (this.m_friendListFrame == null)
		{
			this.m_friendListFrame = this.CreateFriendsListUI();
		}
		TransformUtil.SetPosZ(this.m_closeCatcher, this.m_friendListFrame.transform.position.z + 100f);
		this.m_friendListFrame.gameObject.SetActive(true);
		this.m_closeCatcher.gameObject.SetActive(true);
		this.UpdateLayout();
		this.m_friendListFrame.SetItemsCameraEnabled(false);
		base.StartCoroutine(this.ShowFriendsListWhenReady());
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00029BFE File Offset: 0x00027DFE
	private IEnumerator ShowFriendsListWhenReady()
	{
		while (this.m_friendListFrame == null || !this.m_friendListFrame.IsReady)
		{
			if (this.m_friendListFrame == null)
			{
				yield break;
			}
			yield return null;
		}
		this.m_friendListFrame.UpdateFriendItems();
		ChatMgr.Get().FriendListFrame.items.RecalculateItemSizeAndOffsets(true);
		this.m_friendListFrame.SetItemsCameraEnabled(true);
		if (this.OnFriendListToggled != null)
		{
			this.OnFriendListToggled(true);
		}
		yield break;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00029C10 File Offset: 0x00027E10
	private void HideFriendsList()
	{
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().m_activeFSGMenu = -1L;
		}
		if (this.IsFriendListShowing())
		{
			this.m_friendListFrame.gameObject.SetActive(false);
		}
		if (this.m_closeCatcher != null)
		{
			this.m_closeCatcher.gameObject.SetActive(false);
		}
		if (this.OnFriendListToggled != null)
		{
			this.OnFriendListToggled(false);
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00029C7C File Offset: 0x00027E7C
	public void CloseFriendsList()
	{
		this.DestroyFriendListFrame();
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x00029C84 File Offset: 0x00027E84
	public void GoBack()
	{
		if (this.IsFriendListShowing())
		{
			this.CloseChatUI(true);
			return;
		}
		if (this.m_chatLogUI.IsShowing)
		{
			this.m_chatLogUI.Hide();
			this.ShowFriendsList();
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00029CB4 File Offset: 0x00027EB4
	public void CloseChatUI(bool closeFriendList = true)
	{
		if (this.m_chatLogUI.IsShowing)
		{
			this.m_chatLogUI.Hide();
		}
		if (closeFriendList)
		{
			this.CloseFriendsList();
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00029C7C File Offset: 0x00027E7C
	public void CleanUp()
	{
		this.DestroyFriendListFrame();
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00029CD7 File Offset: 0x00027ED7
	private void DestroyFriendListFrame()
	{
		this.HideFriendsList();
		if (this.m_friendListFrame == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_friendListFrame.gameObject);
		this.m_friendListFrame = null;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00029D05 File Offset: 0x00027F05
	public void SetPendingMessage(BnetAccountId playerID, string message)
	{
		this.m_pendingChatMessages[playerID] = message;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00029D14 File Offset: 0x00027F14
	public string GetPendingMessage(BnetAccountId playerID)
	{
		string result = "";
		this.m_pendingChatMessages.TryGetValue(playerID, out result);
		return result;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00029D37 File Offset: 0x00027F37
	public List<BnetPlayer> GetRecentWhisperPlayers()
	{
		return this.m_recentWhisperPlayers;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00029D40 File Offset: 0x00027F40
	public void AddRecentWhisperPlayerToTop(BnetPlayer player)
	{
		int num = this.m_recentWhisperPlayers.FindIndex((BnetPlayer currPlayer) => currPlayer == player);
		if (num < 0)
		{
			if (this.m_recentWhisperPlayers.Count == 10)
			{
				this.m_recentWhisperPlayers.RemoveAt(this.m_recentWhisperPlayers.Count - 1);
			}
		}
		else
		{
			this.m_recentWhisperPlayers.RemoveAt(num);
		}
		this.m_recentWhisperPlayers.Insert(0, player);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00029DC0 File Offset: 0x00027FC0
	public void AddRecentWhisperPlayerToBottom(BnetPlayer player)
	{
		if (this.m_recentWhisperPlayers.Contains(player))
		{
			return;
		}
		if (this.m_recentWhisperPlayers.Count == 10)
		{
			this.m_recentWhisperPlayers.RemoveAt(this.m_recentWhisperPlayers.Count - 1);
		}
		this.m_recentWhisperPlayers.Add(player);
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00029E0F File Offset: 0x0002800F
	public void AddPlayerChatInfoChangedListener(ChatMgr.PlayerChatInfoChangedCallback callback)
	{
		this.AddPlayerChatInfoChangedListener(callback, null);
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00029E1C File Offset: 0x0002801C
	public void AddPlayerChatInfoChangedListener(ChatMgr.PlayerChatInfoChangedCallback callback, object userData)
	{
		ChatMgr.PlayerChatInfoChangedListener playerChatInfoChangedListener = new ChatMgr.PlayerChatInfoChangedListener();
		playerChatInfoChangedListener.SetCallback(callback);
		playerChatInfoChangedListener.SetUserData(userData);
		if (this.m_playerChatInfoChangedListeners.Contains(playerChatInfoChangedListener))
		{
			return;
		}
		this.m_playerChatInfoChangedListeners.Add(playerChatInfoChangedListener);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00029E58 File Offset: 0x00028058
	public bool RemovePlayerChatInfoChangedListener(ChatMgr.PlayerChatInfoChangedCallback callback)
	{
		return this.RemovePlayerChatInfoChangedListener(callback, null);
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00029E64 File Offset: 0x00028064
	public bool RemovePlayerChatInfoChangedListener(ChatMgr.PlayerChatInfoChangedCallback callback, object userData)
	{
		ChatMgr.PlayerChatInfoChangedListener playerChatInfoChangedListener = new ChatMgr.PlayerChatInfoChangedListener();
		playerChatInfoChangedListener.SetCallback(callback);
		playerChatInfoChangedListener.SetUserData(userData);
		return this.m_playerChatInfoChangedListeners.Remove(playerChatInfoChangedListener);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00029E94 File Offset: 0x00028094
	public PlayerChatInfo GetPlayerChatInfo(BnetPlayer player)
	{
		PlayerChatInfo result = null;
		this.m_playerChatInfos.TryGetValue(player, out result);
		return result;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00029EB4 File Offset: 0x000280B4
	public PlayerChatInfo RegisterPlayerChatInfo(BnetPlayer player)
	{
		PlayerChatInfo playerChatInfo;
		if (!this.m_playerChatInfos.TryGetValue(player, out playerChatInfo))
		{
			playerChatInfo = new PlayerChatInfo();
			playerChatInfo.SetPlayer(player);
			this.m_playerChatInfos.Add(player, playerChatInfo);
		}
		return playerChatInfo;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00029EEC File Offset: 0x000280EC
	public void UpdateFriendItemsWhenAvailable()
	{
		if (this.m_friendListFrame != null)
		{
			this.m_friendListFrame.UpdateFriendItemsWhenAvailable();
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00029F07 File Offset: 0x00028107
	public void OnFriendListOpened()
	{
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			this.OnKeyboardShow();
			return;
		}
		this.UpdateChatBubbleParentLayout();
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00029F07 File Offset: 0x00028107
	public void OnFriendListClosed()
	{
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			this.OnKeyboardShow();
			return;
		}
		this.UpdateChatBubbleParentLayout();
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x00029F22 File Offset: 0x00028122
	public void OnFriendListFriendSelected(BnetPlayer friend)
	{
		this.ShowChatForPlayer(friend);
		if (this.m_friendListFrame != null)
		{
			this.m_friendListFrame.SelectFriend(friend);
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00029F45 File Offset: 0x00028145
	public void OnChatLogFrameShown()
	{
		this.m_chatLogFrameShown = true;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00029F4E File Offset: 0x0002814E
	public void OnChatLogFrameHidden()
	{
		this.m_chatLogFrameShown = false;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00029F57 File Offset: 0x00028157
	public void OnChatReceiverChanged(BnetPlayer player)
	{
		this.UpdatePlayerFocusTime(player);
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00029F60 File Offset: 0x00028160
	public void OnChatFramesMoved()
	{
		this.UpdateChatBubbleParentLayout();
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00029F68 File Offset: 0x00028168
	public bool HandleKeyboardInput()
	{
		if (this.m_fatalErrorMgr.HasError())
		{
			return false;
		}
		if (InputCollection.GetKeyUp(KeyCode.Escape) && this.m_chatLogUI.IsShowing)
		{
			this.m_chatLogUI.Hide();
			return true;
		}
		if (this.IsMobilePlatform() && this.m_chatLogUI.IsShowing && InputCollection.GetKeyUp(KeyCode.Escape))
		{
			this.m_chatLogUI.GoBack();
			return true;
		}
		return false;
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00029FD3 File Offset: 0x000281D3
	public void HandleGUIInput()
	{
		if (this.m_fatalErrorMgr.HasError())
		{
			return;
		}
		if (this.IsMobilePlatform())
		{
			return;
		}
		this.HandleGUIInputForQuickChat();
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00029FF4 File Offset: 0x000281F4
	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		BnetPlayer theirPlayer = WhisperUtil.GetTheirPlayer(whisper);
		this.AddRecentWhisperPlayerToTop(theirPlayer);
		PlayerChatInfo playerChatInfo = this.RegisterPlayerChatInfo(WhisperUtil.GetTheirPlayer(whisper));
		try
		{
			if (this.m_chatLogUI.IsShowing && WhisperUtil.IsSpeakerOrReceiver(this.m_chatLogUI.Receiver, whisper) && this.IsMobilePlatform())
			{
				playerChatInfo.SetLastSeenWhisper(whisper);
			}
			else
			{
				this.PopupNewChatBubble(whisper);
			}
		}
		finally
		{
			this.FireChatInfoChangedEvent(playerChatInfo);
		}
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0002A070 File Offset: 0x00028270
	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		if (removedFriends == null)
		{
			return;
		}
		using (List<BnetPlayer>.Enumerator enumerator = removedFriends.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BnetPlayer friend = enumerator.Current;
				int num = this.m_recentWhisperPlayers.FindIndex((BnetPlayer player) => friend == player);
				if (num >= 0)
				{
					this.m_recentWhisperPlayers.RemoveAt(num);
				}
			}
		}
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0002A0F8 File Offset: 0x000282F8
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.m_fatalErrorMgr.RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.CleanUp();
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0002A118 File Offset: 0x00028318
	private void HandleGUIInputForQuickChat()
	{
		if (this.m_chatLogUI == null)
		{
			return;
		}
		if (!this.m_chatLogUI.IsShowing)
		{
			if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
			{
				this.ShowChatForPlayer(this.GetMostRecentWhisperedPlayer());
				return;
			}
		}
		else if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Escape)
		{
			this.m_chatLogUI.Hide();
		}
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0002A189 File Offset: 0x00028389
	public bool IsMobilePlatform()
	{
		return UniversalInputManager.Get().IsTouchMode() && PlatformSettings.OS != OSCategory.PC;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0002A1A4 File Offset: 0x000283A4
	private void ShowChatForPlayer(BnetPlayer player)
	{
		if (player != null)
		{
			this.AddRecentWhisperPlayerToTop(player);
			PlayerChatInfo playerChatInfo = this.RegisterPlayerChatInfo(player);
			List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(player);
			if (whispersWithPlayer != null)
			{
				playerChatInfo.SetLastSeenWhisper(whispersWithPlayer.LastOrDefault((BnetWhisper whisper) => WhisperUtil.IsSpeaker(player, whisper)));
				this.FireChatInfoChangedEvent(playerChatInfo);
			}
		}
		if (this.m_chatLogUI.IsShowing)
		{
			this.m_chatLogUI.Hide();
		}
		if (this.FriendListFrame != null && this.FriendListFrame.IsFlyoutOpen)
		{
			this.FriendListFrame.CloseFlyoutMenu();
		}
		if (!this.m_chatLogUI.IsShowing)
		{
			if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
			{
				OptionsMenu.Get().Hide(true);
			}
			if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
			{
				MiscellaneousMenu.Get().Hide();
			}
			if (BnetBar.Get() != null)
			{
				BnetBar.Get().HideGameMenu();
			}
			this.m_chatLogUI.ShowForPlayer(this.GetMostRecentWhisperedPlayer());
			this.UpdateLayout();
			if (UniversalInputManager.UsePhoneUI)
			{
				this.CloseFriendsList();
			}
			if (this.OnChatLogShown != null)
			{
				this.OnChatLogShown();
			}
		}
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0002A2FA File Offset: 0x000284FA
	private BnetPlayer GetMostRecentWhisperedPlayer()
	{
		if (this.m_recentWhisperPlayers.Count <= 0)
		{
			return null;
		}
		return this.m_recentWhisperPlayers[0];
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0002A318 File Offset: 0x00028518
	private void UpdatePlayerFocusTime(BnetPlayer player)
	{
		PlayerChatInfo playerChatInfo = this.RegisterPlayerChatInfo(player);
		playerChatInfo.SetLastFocusTime(Time.realtimeSinceStartup);
		this.FireChatInfoChangedEvent(playerChatInfo);
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0002A340 File Offset: 0x00028540
	private void FireChatInfoChangedEvent(PlayerChatInfo chatInfo)
	{
		ChatMgr.PlayerChatInfoChangedListener[] array = this.m_playerChatInfoChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(chatInfo);
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0002A370 File Offset: 0x00028570
	private void UpdateChatBubbleParentLayout()
	{
		if (BaseUI.Get().GetChatBubbleBone() != null)
		{
			this.m_ChatBubbleInfo.m_Parent.transform.position = BaseUI.Get().GetChatBubbleBone().transform.position;
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0002A3B0 File Offset: 0x000285B0
	private void UpdateChatBubbleLayout()
	{
		int count = this.m_chatBubbleFrames.Count;
		if (count == 0)
		{
			return;
		}
		Component dst = this.m_ChatBubbleInfo.m_Parent;
		for (int i = count - 1; i >= 0; i--)
		{
			ChatBubbleFrame chatBubbleFrame = this.m_chatBubbleFrames[i];
			Anchor dstAnchor = UniversalInputManager.UsePhoneUI ? Anchor.BOTTOM_LEFT : Anchor.TOP_LEFT;
			Anchor srcAnchor = UniversalInputManager.UsePhoneUI ? Anchor.TOP_LEFT : Anchor.BOTTOM_LEFT;
			TransformUtil.SetPoint(chatBubbleFrame, srcAnchor, dst, dstAnchor, Vector3.zero);
			dst = chatBubbleFrame;
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0002A428 File Offset: 0x00028628
	private void PopupNewChatBubble(BnetWhisper whisper)
	{
		ChatBubbleFrame chatBubbleFrame = this.CreateChatBubble(whisper);
		this.m_chatBubbleFrames.Add(chatBubbleFrame);
		this.UpdateChatBubbleParentLayout();
		chatBubbleFrame.transform.parent = this.m_ChatBubbleInfo.m_Parent.transform;
		chatBubbleFrame.transform.localScale = chatBubbleFrame.m_ScaleOverride;
		SoundManager.Get().LoadAndPlay("receive_message.prefab:8e90a827cd4a0e849953158396cd1ee1");
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			chatBubbleFrame.m_VisualRoot.transform.localScale,
			"time",
			this.m_ChatBubbleInfo.m_ScaleInSec,
			"easeType",
			this.m_ChatBubbleInfo.m_ScaleInEaseType,
			"oncomplete",
			"OnChatBubbleScaleInComplete",
			"oncompleteparams",
			chatBubbleFrame,
			"oncompletetarget",
			base.gameObject
		});
		chatBubbleFrame.m_VisualRoot.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
		iTween.ScaleTo(chatBubbleFrame.m_VisualRoot, args);
		this.MoveChatBubbles(chatBubbleFrame);
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x0002A560 File Offset: 0x00028760
	private ChatBubbleFrame CreateChatBubble(BnetWhisper whisper)
	{
		ChatBubbleFrame chatBubbleFrame = this.InstantiateChatBubble(this.m_Prefabs.m_ChatBubbleOneLineFrame, whisper);
		if (!chatBubbleFrame.DoesMessageFit())
		{
			UnityEngine.Object.Destroy(chatBubbleFrame.gameObject);
			chatBubbleFrame = this.InstantiateChatBubble(this.m_Prefabs.m_ChatBubbleSmallFrame, whisper);
		}
		SceneUtils.SetLayer(chatBubbleFrame, GameLayer.BattleNetDialog);
		return chatBubbleFrame;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0002A5AF File Offset: 0x000287AF
	private ChatBubbleFrame InstantiateChatBubble(ChatBubbleFrame prefab, BnetWhisper whisper)
	{
		ChatBubbleFrame chatBubbleFrame = UnityEngine.Object.Instantiate<ChatBubbleFrame>(prefab);
		chatBubbleFrame.SetWhisper(whisper);
		chatBubbleFrame.GetComponent<PegUIElement>().AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnChatBubbleReleased));
		return chatBubbleFrame;
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0002A5D8 File Offset: 0x000287D8
	private void MoveChatBubbles(ChatBubbleFrame newBubbleFrame)
	{
		Anchor dstAnchor = Anchor.TOP_LEFT;
		Anchor srcAnchor = Anchor.BOTTOM_LEFT;
		if (UniversalInputManager.UsePhoneUI && this.m_ChatBubbleInfo.m_Parent.transform.localPosition.y > -900f)
		{
			dstAnchor = Anchor.BOTTOM_LEFT;
			srcAnchor = Anchor.TOP_LEFT;
		}
		TransformUtil.SetPoint(newBubbleFrame, srcAnchor, this.m_ChatBubbleInfo.m_Parent, dstAnchor, Vector3.zero);
		int count = this.m_chatBubbleFrames.Count;
		if (count == 1)
		{
			return;
		}
		Vector3[] array = new Vector3[count - 1];
		Component dst = newBubbleFrame;
		for (int i = count - 2; i >= 0; i--)
		{
			ChatBubbleFrame chatBubbleFrame = this.m_chatBubbleFrames[i];
			array[i] = chatBubbleFrame.transform.position;
			TransformUtil.SetPoint(chatBubbleFrame, srcAnchor, dst, dstAnchor, Vector3.zero);
			dst = chatBubbleFrame;
		}
		for (int j = count - 2; j >= 0; j--)
		{
			ChatBubbleFrame chatBubbleFrame2 = this.m_chatBubbleFrames[j];
			Hashtable args = iTween.Hash(new object[]
			{
				"islocal",
				true,
				"position",
				chatBubbleFrame2.transform.localPosition,
				"time",
				this.m_ChatBubbleInfo.m_MoveOverSec,
				"easeType",
				this.m_ChatBubbleInfo.m_MoveOverEaseType
			});
			chatBubbleFrame2.transform.position = array[j];
			iTween.Stop(chatBubbleFrame2.gameObject, "move");
			iTween.MoveTo(chatBubbleFrame2.gameObject, args);
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0002A764 File Offset: 0x00028964
	private void OnChatBubbleScaleInComplete(ChatBubbleFrame bubbleFrame)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			this.m_ChatBubbleInfo.m_HoldSec,
			"time",
			this.m_ChatBubbleInfo.m_FadeOutSec,
			"easeType",
			this.m_ChatBubbleInfo.m_FadeOutEaseType,
			"oncomplete",
			"OnChatBubbleFadeOutComplete",
			"oncompleteparams",
			bubbleFrame,
			"oncompletetarget",
			base.gameObject
		});
		iTween.FadeTo(bubbleFrame.gameObject, args);
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0002A822 File Offset: 0x00028A22
	private void OnChatBubbleFadeOutComplete(ChatBubbleFrame bubbleFrame)
	{
		UnityEngine.Object.Destroy(bubbleFrame.gameObject);
		this.m_chatBubbleFrames.Remove(bubbleFrame);
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0002A83C File Offset: 0x00028A3C
	private void RemoveAllChatBubbles()
	{
		foreach (ChatBubbleFrame chatBubbleFrame in this.m_chatBubbleFrames)
		{
			UnityEngine.Object.Destroy(chatBubbleFrame.gameObject);
		}
		this.m_chatBubbleFrames.Clear();
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0002A89C File Offset: 0x00028A9C
	private void OnChatBubbleReleased(UIEvent e)
	{
		BnetPlayer theirPlayer = WhisperUtil.GetTheirPlayer(e.GetElement().GetComponent<ChatBubbleFrame>().GetWhisper());
		this.ShowChatForPlayer(theirPlayer);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.RemoveAllChatBubbles();
		}
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0002A8D8 File Offset: 0x00028AD8
	public void OnKeyboardShow()
	{
		if (this.m_chatLogUI.IsShowing && BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.position != this.m_chatLogUI.GameObject.transform.position)
		{
			ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
			touchScreenService.RemoveOnVirtualKeyboardShowListener(new Action(this.OnKeyboardShow));
			touchScreenService.RemoveOnVirtualKeyboardHideListener(new Action(this.OnKeyboardHide));
			this.m_chatLogUI.Hide();
			this.m_chatLogUI.ShowForPlayer(this.GetMostRecentWhisperedPlayer());
			touchScreenService.AddOnVirtualKeyboardShowListener(new Action(this.OnKeyboardShow));
			touchScreenService.AddOnVirtualKeyboardHideListener(new Action(this.OnKeyboardHide));
		}
		if (BnetBarFriendButton.Get())
		{
			Vector2 v = new Vector2(0f, (float)(Screen.height - 150));
			GameObject gameObject = BnetBarFriendButton.Get().gameObject;
			TransformUtil.SetPoint(this.m_ChatBubbleInfo.m_Parent, Anchor.BOTTOM_LEFT, gameObject, Anchor.BOTTOM_RIGHT, v);
		}
		int count = this.m_chatBubbleFrames.Count;
		if (count == 0)
		{
			return;
		}
		Component dst = this.m_ChatBubbleInfo.m_Parent;
		for (int i = count - 1; i >= 0; i--)
		{
			ChatBubbleFrame chatBubbleFrame = this.m_chatBubbleFrames[i];
			TransformUtil.SetPoint(chatBubbleFrame, Anchor.TOP_LEFT, dst, Anchor.BOTTOM_LEFT, Vector3.zero);
			dst = chatBubbleFrame;
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0002AA1F File Offset: 0x00028C1F
	public void OnKeyboardHide()
	{
		this.UpdateLayout();
		this.UpdateChatBubbleLayout();
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0002AA2D File Offset: 0x00028C2D
	private void OnDialogShown()
	{
		if (DialogManager.Get().ShowingHighPriorityDialog() && this.m_chatLogUI.IsShowing)
		{
			this.m_chatLogUI.Hide();
		}
	}

	// Token: 0x040004FD RID: 1277
	public ChatMgrPrefabs m_Prefabs;

	// Token: 0x040004FE RID: 1278
	public ChatMgrBubbleInfo m_ChatBubbleInfo;

	// Token: 0x040004FF RID: 1279
	public Float_MobileOverride m_friendsListXOffset;

	// Token: 0x04000500 RID: 1280
	public Float_MobileOverride m_friendsListYOffset;

	// Token: 0x04000501 RID: 1281
	public Float_MobileOverride m_friendsListWidthPadding;

	// Token: 0x04000502 RID: 1282
	public Float_MobileOverride m_friendsListHeightPadding;

	// Token: 0x04000503 RID: 1283
	public float m_chatLogXOffset;

	// Token: 0x04000504 RID: 1284
	public Float_MobileOverride m_friendsListWidth;

	// Token: 0x04000508 RID: 1288
	private static ChatMgr s_instance;

	// Token: 0x04000509 RID: 1289
	private List<ChatBubbleFrame> m_chatBubbleFrames = new List<ChatBubbleFrame>();

	// Token: 0x0400050A RID: 1290
	private IChatLogUI m_chatLogUI;

	// Token: 0x0400050B RID: 1291
	private FriendListFrame m_friendListFrame;

	// Token: 0x0400050C RID: 1292
	private PegUIElement m_closeCatcher;

	// Token: 0x0400050D RID: 1293
	private List<BnetPlayer> m_recentWhisperPlayers = new List<BnetPlayer>();

	// Token: 0x0400050E RID: 1294
	private global::Map<BnetAccountId, string> m_pendingChatMessages = new global::Map<BnetAccountId, string>();

	// Token: 0x0400050F RID: 1295
	private bool m_chatLogFrameShown;

	// Token: 0x04000510 RID: 1296
	private global::Map<BnetPlayer, PlayerChatInfo> m_playerChatInfos = new global::Map<BnetPlayer, PlayerChatInfo>();

	// Token: 0x04000511 RID: 1297
	private List<ChatMgr.PlayerChatInfoChangedListener> m_playerChatInfoChangedListeners = new List<ChatMgr.PlayerChatInfoChangedListener>();

	// Token: 0x04000512 RID: 1298
	private ChatMgr.KeyboardState keyboardState;

	// Token: 0x04000513 RID: 1299
	private Rect keyboardArea = new Rect(0f, 0f, 0f, 0f);

	// Token: 0x04000514 RID: 1300
	private FatalErrorMgr m_fatalErrorMgr;

	// Token: 0x04000515 RID: 1301
	private global::Map<Renderer, int> m_friendListOriginalLayers = new global::Map<Renderer, int>();

	// Token: 0x0200137A RID: 4986
	// (Invoke) Token: 0x0600D798 RID: 55192
	public delegate void PlayerChatInfoChangedCallback(PlayerChatInfo chatInfo, object userData);

	// Token: 0x0200137B RID: 4987
	// (Invoke) Token: 0x0600D79C RID: 55196
	public delegate void FriendListToggled(bool open);

	// Token: 0x0200137C RID: 4988
	private class PlayerChatInfoChangedListener : global::EventListener<ChatMgr.PlayerChatInfoChangedCallback>
	{
		// Token: 0x0600D79F RID: 55199 RVA: 0x003EC37D File Offset: 0x003EA57D
		public void Fire(PlayerChatInfo chatInfo)
		{
			this.m_callback(chatInfo, this.m_userData);
		}
	}

	// Token: 0x0200137D RID: 4989
	private enum KeyboardState
	{
		// Token: 0x0400A6E0 RID: 42720
		None,
		// Token: 0x0400A6E1 RID: 42721
		Below,
		// Token: 0x0400A6E2 RID: 42722
		Above
	}
}
