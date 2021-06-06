using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusFSG;
using PegasusShared;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class FriendListFrame : MonoBehaviour
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x0600086B RID: 2155 RVA: 0x000319B0 File Offset: 0x0002FBB0
	// (set) Token: 0x0600086C RID: 2156 RVA: 0x000319B8 File Offset: 0x0002FBB8
	public bool IsStarted { get; private set; }

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x0600086D RID: 2157 RVA: 0x000319C1 File Offset: 0x0002FBC1
	public bool ShowingAddFriendFrame
	{
		get
		{
			return this.m_addFriendFrame != null;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x0600086E RID: 2158 RVA: 0x000319CF File Offset: 0x0002FBCF
	public bool IsInEditMode
	{
		get
		{
			return this.m_editMode > FriendListFrame.FriendListEditMode.NONE;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x0600086F RID: 2159 RVA: 0x000319DA File Offset: 0x0002FBDA
	public FriendListFrame.FriendListEditMode EditMode
	{
		get
		{
			return this.m_editMode;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000870 RID: 2160 RVA: 0x000319E2 File Offset: 0x0002FBE2
	public bool IsFlyoutOpen
	{
		get
		{
			return this.m_flyoutOpen;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000871 RID: 2161 RVA: 0x000319EC File Offset: 0x0002FBEC
	public bool IsReady
	{
		get
		{
			foreach (FriendListFriendFrame friendListFriendFrame in this.GetRenderedItems<FriendListFriendFrame>())
			{
				if (friendListFriendFrame.gameObject.activeInHierarchy && friendListFriendFrame.ShouldShowRankedMedal && !friendListFriendFrame.IsRankedMedalReady)
				{
					return false;
				}
			}
			return !(this.m_myRankedMedal == null) && this.m_myRankedMedal.IsReady;
		}
	}

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000872 RID: 2162 RVA: 0x00031A7C File Offset: 0x0002FC7C
	// (remove) Token: 0x06000873 RID: 2163 RVA: 0x00031AB4 File Offset: 0x0002FCB4
	public event Action OnStarted;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000874 RID: 2164 RVA: 0x00031AEC File Offset: 0x0002FCEC
	// (remove) Token: 0x06000875 RID: 2165 RVA: 0x00031B24 File Offset: 0x0002FD24
	public event Action AddFriendFrameOpened;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06000876 RID: 2166 RVA: 0x00031B5C File Offset: 0x0002FD5C
	// (remove) Token: 0x06000877 RID: 2167 RVA: 0x00031B94 File Offset: 0x0002FD94
	public event Action AddFriendFrameClosed;

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000878 RID: 2168 RVA: 0x00031BCC File Offset: 0x0002FDCC
	// (remove) Token: 0x06000879 RID: 2169 RVA: 0x00031C04 File Offset: 0x0002FE04
	public event Action RemoveFriendPopupOpened;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x0600087A RID: 2170 RVA: 0x00031C3C File Offset: 0x0002FE3C
	// (remove) Token: 0x0600087B RID: 2171 RVA: 0x00031C74 File Offset: 0x0002FE74
	public event Action RemoveFriendPopupClosed;

	// Token: 0x0600087C RID: 2172 RVA: 0x00031CAC File Offset: 0x0002FEAC
	private void Awake()
	{
		this.InitButtons();
		this.RegisterFriendEvents();
		this.CreateItemsCamera();
		this.UpdateBackgroundCollider();
		bool active = !UniversalInputManager.Get().IsTouchMode() || PlatformSettings.OS == OSCategory.PC;
		if (this.scrollbar != null)
		{
			this.scrollbar.gameObject.SetActive(active);
		}
		if (BnetFriendMgr.Get().HasOnlineFriends() || BnetNearbyPlayerMgr.Get().HasNearbyStrangers())
		{
			CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(null);
		}
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			this.items.GetComponent<BoxCollider>().enabled = false;
			this.temporaryAccountPaper.SetActive(true);
			this.temporaryAccountCover.SetActive(true);
			this.temporaryAccountDrawing.SetActive(true);
			this.temporaryAccountSignUpButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTemporaryAccountSignUpButtonPressed));
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00031D84 File Offset: 0x0002FF84
	private void Start()
	{
		this.UpdateMyself();
		this.InitItems();
		this.UpdateRAFState();
		this.UpdateFSGState();
		TelemetryManager.Client().SendFriendsListView(SceneMgr.Get().GetMode().ToString());
		this.me.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnMyRankedMedalWidgetReady));
		this.IsStarted = true;
		if (this.OnStarted != null)
		{
			this.OnStarted();
		}
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00031E18 File Offset: 0x00030018
	private void OnDestroy()
	{
		this.UnregisterFriendEvents();
		this.CloseAddFriendFrame();
		if (this.m_longListBehavior != null && this.m_longListBehavior.FreeList != null)
		{
			foreach (MobileFriendListItem mobileFriendListItem in this.m_longListBehavior.FreeList)
			{
				if (mobileFriendListItem != null)
				{
					UnityEngine.Object.Destroy(mobileFriendListItem.gameObject);
				}
			}
		}
		foreach (FriendListItemHeader friendListItemHeader in this.m_headers.Values)
		{
			if (friendListItemHeader != null)
			{
				UnityEngine.Object.Destroy(friendListItemHeader.gameObject);
			}
		}
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00031F10 File Offset: 0x00030110
	private void Update()
	{
		this.HandleKeyboardInput();
		if (this.m_nearbyPlayersNeedUpdate && Time.realtimeSinceStartup >= this.m_lastNearbyPlayersUpdate + 10f)
		{
			this.HandleNearbyPlayersChanged();
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00031F3C File Offset: 0x0003013C
	private void OnEnable()
	{
		if (this.m_nearbyPlayersNeedUpdate)
		{
			this.HandleNearbyPlayersChanged();
		}
		if (this.m_playersChangeList.GetChanges().Count > 0)
		{
			this.DoPlayersChanged(this.m_playersChangeList);
			this.m_playersChangeList.GetChanges().Clear();
		}
		if (this.items.IsInitialized)
		{
			this.ResumeItemsLayout();
		}
		this.UpdateMyself();
		this.items.ResetState();
		this.m_editMode = FriendListFrame.FriendListEditMode.NONE;
		this.m_friendToRemove = null;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00031FB8 File Offset: 0x000301B8
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.m_longListBehavior.ReleaseAllItems();
		this.m_allItems.Clear();
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00031FD0 File Offset: 0x000301D0
	public void SetItemsCameraEnabled(bool enable)
	{
		this.m_itemsCamera.gameObject.SetActive(enable);
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00031FE4 File Offset: 0x000301E4
	public void SetWorldRect(float x, float y, float width, float height)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(true);
		this.window.SetEntireSize(width, height);
		Vector3 vector = TransformUtil.ComputeWorldPoint(TransformUtil.ComputeSetPointBounds(this.window), new Vector3(0f, 1f, 0f));
		Vector3 translation = new Vector3(x, y, vector.z) - vector;
		base.transform.Translate(translation);
		this.UpdateItemsList();
		this.UpdateItemsCamera();
		this.UpdateBackgroundCollider();
		this.UpdateDropShadow();
		base.gameObject.SetActive(activeSelf);
		if (this.temporaryAccountDrawingBone != null && TemporaryAccountManager.IsTemporaryAccount())
		{
			this.temporaryAccountDrawing.transform.position = this.temporaryAccountDrawingBone.transform.position;
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000320B5 File Offset: 0x000302B5
	public void SetWorldPosition(float x, float y)
	{
		this.SetWorldPosition(new Vector3(x, y));
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x000320C4 File Offset: 0x000302C4
	public void SetWorldPosition(Vector3 pos)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(true);
		base.transform.position = pos;
		this.UpdateItemsList();
		this.UpdateItemsCamera();
		this.UpdateBackgroundCollider();
		base.gameObject.SetActive(activeSelf);
		if (this.temporaryAccountDrawingBone != null && TemporaryAccountManager.IsTemporaryAccount())
		{
			this.temporaryAccountDrawing.transform.position = this.temporaryAccountDrawingBone.transform.position;
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00032148 File Offset: 0x00030348
	public void SetWorldHeight(float height)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(true);
		this.window.SetEntireHeight(height);
		this.UpdateItemsList();
		this.UpdateItemsCamera();
		this.UpdateBackgroundCollider();
		this.UpdateDropShadow();
		base.gameObject.SetActive(activeSelf);
		if (this.temporaryAccountDrawingBone != null && TemporaryAccountManager.IsTemporaryAccount())
		{
			this.temporaryAccountDrawing.transform.position = this.temporaryAccountDrawingBone.transform.position;
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x000321D2 File Offset: 0x000303D2
	public void ShowAddFriendFrame(BnetPlayer player = null)
	{
		this.m_addFriendFrame = UnityEngine.Object.Instantiate<AddFriendFrame>(this.prefabs.addFriendFrame);
		this.m_addFriendFrame.Closed += this.CloseAddFriendFrame;
		if (player != null)
		{
			this.m_addFriendFrame.SetPlayer(player);
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00032210 File Offset: 0x00030410
	public void CloseAddFriendFrame()
	{
		if (this.m_addFriendFrame == null)
		{
			return;
		}
		this.m_addFriendFrame.Close();
		if (this.AddFriendFrameClosed != null)
		{
			this.AddFriendFrameClosed();
		}
		this.m_addFriendFrame = null;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00032248 File Offset: 0x00030448
	public void ShowRemoveFriendPopup(BnetPlayer friend)
	{
		this.m_friendToRemove = friend;
		if (this.m_friendToRemove == null)
		{
			return;
		}
		string uniqueName = FriendUtils.GetUniqueName(this.m_friendToRemove);
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_text = GameStrings.Format("GLOBAL_FRIENDLIST_REMOVE_FRIEND_ALERT_MESSAGE", new object[]
			{
				uniqueName
			}),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = new AlertPopup.ResponseCallback(this.OnRemoveFriendPopupResponse)
		};
		DialogManager.Get().ShowPopup(info, new DialogManager.DialogProcessCallback(this.OnRemoveFriendDialogShown), this.m_friendToRemove);
		if (this.RemoveFriendPopupOpened != null)
		{
			this.RemoveFriendPopupOpened();
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x000322E4 File Offset: 0x000304E4
	public void SelectFriend(BnetPlayer player)
	{
		foreach (FriendListFriendFrame friendListFriendFrame in this.GetRenderedItems<FriendListFriendFrame>())
		{
			Widget widget = friendListFriendFrame.GetWidget();
			if (widget != null)
			{
				if (friendListFriendFrame.GetFriend() == player)
				{
					widget.TriggerEvent("SHOW_HIGHLIGHT", default(Widget.TriggerEventParameters));
				}
				else
				{
					widget.TriggerEvent("HIDE_HIGHLIGHT", default(Widget.TriggerEventParameters));
				}
			}
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00032378 File Offset: 0x00030578
	public void UpdateRAFButtonGlow()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_RAF);
		this.rafButtonButtonGlow.SetActive(!@bool && this.m_isRAFButtonEnabled);
		this.UpdateFlyoutButtonGlow();
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x000323B4 File Offset: 0x000305B4
	public void UpdateFSGButtonGlow()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_CLICKED_FIRESIDE_GATHERINGS_BUTTON);
		this.fsgButtonButtonGlow.SetActive(!@bool && this.m_isFSGButtonEnabled);
		this.UpdateFlyoutButtonGlow();
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x000323EE File Offset: 0x000305EE
	private void UpdateFlyoutButtonGlow()
	{
		this.flyoutButtonGlow.ChangeState((this.fsgButtonButtonGlow.activeSelf || this.rafButtonButtonGlow.activeSelf || this.IsFlyoutOpen) ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00032423 File Offset: 0x00030623
	public OrientedBounds ComputeFrameWorldBounds()
	{
		return TransformUtil.ComputeOrientedWorldBounds(base.gameObject, new List<GameObject>
		{
			this.items.gameObject
		}, true);
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00032448 File Offset: 0x00030648
	public void SetRAFButtonEnabled(bool enabled)
	{
		if (this.m_isRAFButtonEnabled != enabled)
		{
			this.m_isRAFButtonEnabled = enabled;
			this.rafButton.GetComponent<UIBHighlight>().EnableResponse = this.m_isRAFButtonEnabled;
			this.rafButtonEnabledVisual.SetActive(enabled);
			this.rafButtonDisabledVisual.SetActive(!enabled);
			if (this.m_isRAFButtonEnabled)
			{
				this.rafButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRAFButtonReleased));
			}
			else
			{
				this.rafButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRAFButtonReleased));
			}
			this.UpdateRAFButtonGlow();
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x000324D8 File Offset: 0x000306D8
	public void SetFSGButtonEnabled()
	{
		bool flag = !FiresideGatheringManager.Get().IsCheckedIn && FiresideGatheringManager.CanRequestNearbyFSG;
		if (this.m_isFSGButtonEnabled != flag)
		{
			this.m_isFSGButtonEnabled = flag;
			this.fsgButton.SetEnabled(this.m_isFSGButtonEnabled, false);
			this.SetupFSGButtonAndFixFrameLength(this.m_isFSGButtonEnabled, this.flyoutMiddleFrame.transform.localScale.y, this.flyoutMiddleShadow.transform.localScale.y);
			if (this.m_isFSGButtonEnabled)
			{
				this.fsgButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFSGButtonReleased));
			}
			else
			{
				this.fsgButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFSGButtonReleased));
			}
			this.UpdateFSGButtonGlow();
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00032598 File Offset: 0x00030798
	private void SetupFSGButtonAndFixFrameLength(bool enabled, float middleFrameScaleY, float middleShadowScaleY)
	{
		if (enabled)
		{
			this.fsgButton.gameObject.SetActive(true);
			return;
		}
		this.fsgButton.gameObject.SetActive(false);
		middleFrameScaleY -= this.flyoutMiddleFrameScaleOffsetForFSG;
		middleShadowScaleY -= this.flyoutShadowScaleOffsetForFSG;
		this.flyoutMiddleFrame.transform.localScale = new Vector3(this.flyoutMiddleFrame.transform.localScale.x, middleFrameScaleY, this.flyoutMiddleFrame.transform.localScale.z);
		this.flyoutMiddleShadow.transform.localScale = new Vector3(this.flyoutMiddleShadow.transform.localScale.x, middleShadowScaleY, this.flyoutMiddleShadow.transform.localScale.z);
		this.flyoutFrameContainer.UpdateSlices();
		this.flyoutShadowContainer.UpdateSlices();
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00032675 File Offset: 0x00030875
	public void OpenFlyoutMenu()
	{
		if (this.flyoutMenu == null)
		{
			return;
		}
		this.m_flyoutOpen = true;
		this.flyoutMenu.SetActive(true);
		this.UpdateFlyoutButtonGlow();
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0003269F File Offset: 0x0003089F
	public void CloseFlyoutMenu()
	{
		if (this.flyoutMenu == null)
		{
			return;
		}
		this.m_flyoutOpen = false;
		this.flyoutMenu.SetActive(false);
		this.UpdateFlyoutButtonGlow();
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x000326CC File Offset: 0x000308CC
	private void CreateItemsCamera()
	{
		this.m_itemsCamera = new GameObject("ItemsCamera")
		{
			transform = 
			{
				parent = this.items.transform,
				localPosition = new Vector3(0f, 0f, -100f)
			}
		}.AddComponent<Camera>();
		this.m_itemsCamera.orthographic = true;
		this.m_itemsCamera.depth = (float)(BnetBar.CameraDepth + 1);
		this.m_itemsCamera.clearFlags = CameraClearFlags.Depth;
		this.m_itemsCamera.cullingMask = GameLayer.BattleNetFriendList.LayerBit();
		this.m_itemsCamera.allowHDR = false;
		this.UpdateItemsCamera();
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00032774 File Offset: 0x00030974
	private void UpdateItemsList()
	{
		Transform bottomRightBone = this.GetBottomRightBone();
		this.items.transform.position = (this.listInfo.topLeft.position + bottomRightBone.position) / 2f;
		Vector3 vector = bottomRightBone.position - this.listInfo.topLeft.position;
		this.items.ClipSize = new UnityEngine.Vector2(vector.x, Math.Abs(vector.y));
		if (this.innerShadow != null)
		{
			this.innerShadow.transform.position = this.items.transform.position;
			Vector3 vector2 = this.GetBottomRightBone().position - this.listInfo.topLeft.position;
			TransformUtil.SetLocalScaleToWorldDimension(this.innerShadow, new WorldDimensionIndex[]
			{
				new WorldDimensionIndex(Mathf.Abs(vector2.x), 0),
				new WorldDimensionIndex(Mathf.Abs(vector2.y), 2)
			});
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00032890 File Offset: 0x00030A90
	private void UpdateItemsCamera()
	{
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		Transform bottomRightBone = this.GetBottomRightBone();
		Vector3 vector = bnetCamera.WorldToScreenPoint(this.listInfo.topLeft.position);
		Vector3 vector2 = bnetCamera.WorldToScreenPoint(bottomRightBone.position);
		GeneralUtils.Swap<float>(ref vector.y, ref vector2.y);
		this.m_itemsCamera.pixelRect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		this.m_itemsCamera.orthographicSize = this.m_itemsCamera.rect.height * bnetCamera.orthographicSize;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00032944 File Offset: 0x00030B44
	private void UpdateBackgroundCollider()
	{
		Renderer[] componentsInChildren = this.window.GetComponentsInChildren<Renderer>();
		Bounds bounds = new Bounds(base.transform.position, Vector3.zero);
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer.bounds.size.x != 0f && renderer.bounds.size.y != 0f && renderer.bounds.size.z != 0f)
			{
				bounds.Encapsulate(renderer.bounds);
			}
		}
		Vector3 vector = base.transform.InverseTransformPoint(bounds.min);
		Vector3 vector2 = base.transform.InverseTransformPoint(bounds.max);
		BoxCollider boxCollider = base.GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		boxCollider.center = (vector + vector2) / 2f + Vector3.forward;
		boxCollider.size = vector2 - vector;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00032A61 File Offset: 0x00030C61
	private void UpdateDropShadow()
	{
		if (this.outerShadow == null)
		{
			return;
		}
		this.outerShadow.SetActive(!UniversalInputManager.Get().IsTouchMode());
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00032A8C File Offset: 0x00030C8C
	private void UpdateMyself()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null && myPlayer.IsDisplayable())
		{
			BnetBattleTag battleTag = myPlayer.GetBattleTag();
			if (Options.Get().GetBool(Option.STREAMER_MODE))
			{
				this.me.nameText.Text = string.Empty;
			}
			else
			{
				this.me.nameText.Text = string.Format("<color=#{0}>{1}</color> <size=32><color=#{2}>#{3}</color></size>", new object[]
				{
					"5ecaf0ff",
					battleTag.GetName(),
					"999999ff",
					battleTag.GetNumber().ToString()
				});
			}
			if (RankMgr.Get().GetLocalPlayerMedalInfo().IsDisplayable())
			{
				this.myPortrait.gameObject.SetActive(false);
				if (this.portraitBackground != null)
				{
					this.portraitBackground.GetComponent<Renderer>().SetMaterial(this.rankedBackground);
				}
			}
			else
			{
				this.myPortrait.SetProgramId(BnetProgramId.HEARTHSTONE);
				this.myPortrait.gameObject.SetActive(true);
				if (this.portraitBackground != null)
				{
					this.portraitBackground.GetComponent<Renderer>().SetMaterial(this.unrankedBackground);
				}
			}
			this.UpdateMyRankedMedalWidget();
			return;
		}
		this.me.nameText.Text = string.Empty;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00032BD8 File Offset: 0x00030DD8
	private void InitItems()
	{
		BnetFriendMgr bnetFriendMgr = BnetFriendMgr.Get();
		BnetNearbyPlayerMgr bnetNearbyPlayerMgr = BnetNearbyPlayerMgr.Get();
		this.items.SelectionEnabled = true;
		this.items.SelectedIndexChanging += ((int index) => index != -1);
		this.SuspendItemsLayout();
		this.UpdateCurrentFiresideGatherings();
		this.UpdateFoundFiresideGatherings();
		this.InitFiresideGatheringPlayers();
		this.UpdateRequests(bnetFriendMgr.GetReceivedInvites(), null);
		this.UpdateAllFriends(bnetFriendMgr.GetFriends(), null);
		this.UpdateAllNearbyPlayers(bnetNearbyPlayerMgr.GetNearbyStrangers(), null);
		this.UpdateAllNearbyPlayers(bnetNearbyPlayerMgr.GetNearbyFriends(), null);
		this.UpdateAllHeaders();
		this.ResumeItemsLayout();
		this.UpdateAllHeaderBackgrounds();
		this.UpdateSelectedItem();
		this.UpdateRAFButtonGlow();
		this.UpdateFSGButtonGlow();
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00032C98 File Offset: 0x00030E98
	public void UpdateItems()
	{
		foreach (FriendListRequestFrame friendListRequestFrame in this.GetRenderedItems<FriendListRequestFrame>())
		{
			friendListRequestFrame.UpdateInvite();
		}
		this.UpdateFriendItems();
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00032CF0 File Offset: 0x00030EF0
	public void UpdateFriendItems()
	{
		if (this.m_updateFriendItemsWhenAvailableCoroutine != null)
		{
			base.StopCoroutine(this.m_updateFriendItemsWhenAvailableCoroutine);
		}
		foreach (FriendListFriendFrame friendListFriendFrame in this.GetRenderedItems<FriendListFriendFrame>())
		{
			friendListFriendFrame.UpdateFriend();
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00032D54 File Offset: 0x00030F54
	public void UpdateFriendItemsWhenAvailable()
	{
		if (this.m_updateFriendItemsWhenAvailableCoroutine != null)
		{
			base.StopCoroutine(this.m_updateFriendItemsWhenAvailableCoroutine);
		}
		this.m_updateFriendItemsWhenAvailableCoroutine = base.StartCoroutine(this.UpdateFriendItemsWhenAvailableCoroutine());
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00032D7C File Offset: 0x00030F7C
	private IEnumerator UpdateFriendItemsWhenAvailableCoroutine()
	{
		while (!FriendChallengeMgr.Get().AmIAvailable())
		{
			yield return null;
		}
		this.m_updateFriendItemsWhenAvailableCoroutine = null;
		this.UpdateFriendItems();
		yield break;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00032D8C File Offset: 0x00030F8C
	private void UpdateCurrentFiresideGatherings()
	{
		for (int i = this.m_allItems.Count - 1; i >= 0; i--)
		{
			if (this.m_allItems[i].ItemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
			{
				this.m_allItems.RemoveAt(i);
			}
		}
		FSGConfig currentFSG = FiresideGatheringManager.Get().CurrentFSG;
		if (currentFSG != null)
		{
			FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.CurrentFiresideGathering, currentFSG);
			this.AddItem(itemToAdd);
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00032DF8 File Offset: 0x00030FF8
	private void UpdateFoundFiresideGatherings()
	{
		for (int i = this.m_allItems.Count - 1; i >= 0; i--)
		{
			if (this.m_allItems[i].ItemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
			{
				this.m_allItems.RemoveAt(i);
			}
		}
		foreach (FSGConfig itemData in FiresideGatheringManager.Get().GetFSGs())
		{
			FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.FoundFiresideGathering, itemData);
			this.AddItem(itemToAdd);
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00032EA0 File Offset: 0x000310A0
	private void InitFiresideGatheringPlayers()
	{
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		List<BnetPlayer> displayablePatronList = firesideGatheringManager.DisplayablePatronList;
		this.UpdateFiresideGatheringPlayers(displayablePatronList, null);
		if (firesideGatheringManager.CurrentFsgIsLargeScale)
		{
			string itemData = GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_PATRON_LIST_FOOTER_TEXT_LARGE_SCALE", new object[]
			{
				99
			});
			FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, itemData);
			this.AddItem(itemToAdd);
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00032EF8 File Offset: 0x000310F8
	private void UpdateFiresideGatheringPlayers(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		if (FiresideGatheringManager.Get().DisplayablePatronCount >= FiresideGatheringManager.Get().FriendListPatronCountLimit)
		{
			this.m_patronStrangersHidden = true;
			if (removedList == null)
			{
				removedList = new List<BnetPlayer>();
			}
			List<BnetPlayer> list = new List<BnetPlayer>();
			foreach (BnetPlayer bnetPlayer in FiresideGatheringManager.Get().DisplayablePatronList)
			{
				if (!BnetFriendMgr.Get().IsFriend(bnetPlayer))
				{
					list.Add(bnetPlayer);
				}
			}
			removedList.AddRange(list);
			if (addedList != null)
			{
				addedList.RemoveAll(new Predicate<BnetPlayer>(list.Contains));
			}
			if (!this.m_allItems.Exists((FriendListFrame.FriendListItem item) => item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter))
			{
				int num = Mathf.Clamp(FiresideGatheringManager.Get().DisplayablePatronCount, FiresideGatheringManager.Get().FriendListPatronCountLimit, 99);
				string itemData = (FiresideGatheringManager.Get().DisplayablePatronCount > 99) ? GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_PATRON_LIST_FOOTER_TEXT_LARGE_SCALE", new object[]
				{
					99
				}) : GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_PATRON_LIST_FOOTER_TEXT_SOFT_LIMIT", new object[]
				{
					num
				});
				FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, itemData);
				this.AddItem(itemToAdd);
			}
		}
		else if (this.m_patronStrangersHidden)
		{
			this.m_patronStrangersHidden = false;
			this.RemoveItem(false, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, string.Empty);
			if (addedList == null)
			{
				addedList = new List<BnetPlayer>();
			}
			List<BnetPlayer> list2 = new List<BnetPlayer>();
			foreach (BnetPlayer bnetPlayer2 in FiresideGatheringManager.Get().DisplayablePatronList)
			{
				if (!BnetFriendMgr.Get().IsFriend(bnetPlayer2) && !addedList.Contains(bnetPlayer2))
				{
					list2.Add(bnetPlayer2);
				}
			}
			addedList.AddRange(list2);
		}
		if (removedList != null)
		{
			foreach (BnetPlayer itemToRemove in removedList)
			{
				this.RemoveItem(false, MobileFriendListItem.TypeFlags.FiresideGatheringPlayer, itemToRemove);
			}
		}
		if (addedList != null)
		{
			foreach (BnetPlayer itemData2 in addedList)
			{
				FriendListFrame.FriendListItem itemToAdd2 = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.FiresideGatheringPlayer, itemData2);
				this.AddItem(itemToAdd2);
			}
		}
		this.UpdateFriendItems();
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00033188 File Offset: 0x00031388
	private void OnFiresideGatheringPresencePatronsUpdated(List<BnetPlayer> addedPatrons, List<BnetPlayer> removedPatrons)
	{
		this.UpdateFiresideGatheringPlayers(addedPatrons, removedPatrons);
		BnetFriendChangelist bnetFriendChangelist = null;
		bool flag = false;
		if (addedPatrons != null)
		{
			foreach (BnetPlayer bnetPlayer in addedPatrons)
			{
				flag = true;
				if (BnetFriendMgr.Get().IsFriend(bnetPlayer))
				{
					if (bnetFriendChangelist == null)
					{
						bnetFriendChangelist = new BnetFriendChangelist();
					}
					bnetFriendChangelist.AddRemovedFriend(bnetPlayer);
				}
			}
		}
		if (removedPatrons != null)
		{
			foreach (BnetPlayer bnetPlayer2 in removedPatrons)
			{
				flag = true;
				if (BnetFriendMgr.Get().IsFriend(bnetPlayer2))
				{
					if (bnetFriendChangelist == null)
					{
						bnetFriendChangelist = new BnetFriendChangelist();
					}
					bnetFriendChangelist.AddAddedFriend(bnetPlayer2);
				}
			}
		}
		if (bnetFriendChangelist != null)
		{
			this.OnFriendsChanged(bnetFriendChangelist, null);
			return;
		}
		if (flag)
		{
			this.SortAndRefreshTouchList();
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00033270 File Offset: 0x00031470
	private void RemoveAllFiresideGatheringPlayers()
	{
		this.m_allItems.RemoveAll((FriendListFrame.FriendListItem item) => item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer || item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter);
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x000332A0 File Offset: 0x000314A0
	private void UpdateRequests(List<BnetInvitation> addedList, List<BnetInvitation> removedList)
	{
		if (removedList == null && addedList == null)
		{
			return;
		}
		if (removedList != null)
		{
			foreach (BnetInvitation itemToRemove in removedList)
			{
				this.RemoveItem(false, MobileFriendListItem.TypeFlags.Request, itemToRemove);
			}
		}
		foreach (FriendListRequestFrame friendListRequestFrame in this.GetRenderedItems<FriendListRequestFrame>())
		{
			friendListRequestFrame.UpdateInvite();
		}
		if (addedList != null)
		{
			foreach (BnetInvitation itemData in addedList)
			{
				FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.Request, itemData);
				this.AddItem(itemToAdd);
			}
		}
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00033390 File Offset: 0x00031590
	private void UpdateAllFriends(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		if (removedList == null && addedList == null)
		{
			return;
		}
		if (removedList != null)
		{
			foreach (BnetPlayer itemToRemove in removedList)
			{
				this.RemoveItem(false, MobileFriendListItem.TypeFlags.Friend, itemToRemove);
			}
		}
		this.UpdateFriendItems();
		if (addedList != null)
		{
			foreach (BnetPlayer bnetPlayer in addedList)
			{
				if (!FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(bnetPlayer) && !BnetNearbyPlayerMgr.Get().IsNearbyPlayer(bnetPlayer))
				{
					bnetPlayer.GetPersistentGameId();
					FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.Friend, bnetPlayer);
					this.AddItem(itemToAdd);
				}
			}
		}
		this.SortAndRefreshTouchList();
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00033464 File Offset: 0x00031664
	private void UpdateAllNearbyPlayers(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		if (removedList != null)
		{
			foreach (BnetPlayer itemToRemove in removedList)
			{
				this.RemoveItem(false, MobileFriendListItem.TypeFlags.NearbyPlayer, itemToRemove);
			}
		}
		this.UpdateFriendItems();
		if (addedList != null)
		{
			foreach (BnetPlayer itemData in addedList)
			{
				FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.NearbyPlayer, itemData);
				this.AddItem(itemToAdd);
			}
		}
		this.SortAndRefreshTouchList();
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00033510 File Offset: 0x00031710
	private FriendListFriendFrame FindRenderedBaseFriendFrame(BnetPlayer friend)
	{
		return this.FindFirstRenderedItem<FriendListFriendFrame>((FriendListFriendFrame frame) => frame.GetFriend() == friend);
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0003353C File Offset: 0x0003173C
	private void UpdateFriendFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = this.FindRenderedBaseFriendFrame(friend);
		if (friendListFriendFrame != null)
		{
			friendListFriendFrame.UpdateFriend();
		}
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00033560 File Offset: 0x00031760
	private MobileFriendListItem CreateFriendFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = UnityEngine.Object.Instantiate<FriendListFriendFrame>(this.prefabs.friendItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListFriendFrame.gameObject, false);
		friendListFriendFrame.Initialize(friend, false, false);
		MobileFriendListItem result = this.FinishCreateVisualItem<FriendListFriendFrame>(friendListFriendFrame, MobileFriendListItem.TypeFlags.Friend, this.FindHeader(MobileFriendListItem.TypeFlags.Friend), friendListFriendFrame.gameObject);
		UberText.EnableAllTextObjects(objs, true);
		return result;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x000335B0 File Offset: 0x000317B0
	private MobileFriendListItem CreateNearbyPlayerFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = UnityEngine.Object.Instantiate<FriendListFriendFrame>(this.prefabs.friendItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListFriendFrame.gameObject, false);
		friendListFriendFrame.Initialize(friend, false, false);
		MobileFriendListItem result = this.FinishCreateVisualItem<FriendListFriendFrame>(friendListFriendFrame, MobileFriendListItem.TypeFlags.NearbyPlayer, this.FindHeader(MobileFriendListItem.TypeFlags.NearbyPlayer), friendListFriendFrame.gameObject);
		UberText.EnableAllTextObjects(objs, true);
		return result;
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00033600 File Offset: 0x00031800
	private MobileFriendListItem CreateRequestFrame(BnetInvitation invite)
	{
		FriendListRequestFrame friendListRequestFrame = UnityEngine.Object.Instantiate<FriendListRequestFrame>(this.prefabs.requestItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListRequestFrame.gameObject, false);
		friendListRequestFrame.SetInvite(invite);
		MobileFriendListItem result = this.FinishCreateVisualItem<FriendListRequestFrame>(friendListRequestFrame, MobileFriendListItem.TypeFlags.Request, this.FindHeader(MobileFriendListItem.TypeFlags.Request), friendListRequestFrame.gameObject);
		UberText.EnableAllTextObjects(objs, true);
		return result;
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00033658 File Offset: 0x00031858
	private MobileFriendListItem CreateFSGFrame(FSGConfig fsgConfig, MobileFriendListItem.TypeFlags flag)
	{
		FriendListFSGFrame friendListFSGFrame = UnityEngine.Object.Instantiate<FriendListFSGFrame>(this.prefabs.fsgItem);
		friendListFSGFrame.InitFrame(fsgConfig);
		return this.FinishCreateVisualItem<FriendListFSGFrame>(friendListFSGFrame, flag, null, friendListFSGFrame.gameObject);
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0003368C File Offset: 0x0003188C
	private MobileFriendListItem CreateFSGPlayerFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = UnityEngine.Object.Instantiate<FriendListFriendFrame>(this.prefabs.friendItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListFriendFrame.gameObject, false);
		friendListFriendFrame.Initialize(friend, true, false);
		MobileFriendListItem result = this.FinishCreateVisualItem<FriendListFriendFrame>(friendListFriendFrame, MobileFriendListItem.TypeFlags.FiresideGatheringPlayer, null, friendListFriendFrame.gameObject);
		UberText.EnableAllTextObjects(objs, true);
		return result;
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x000336D8 File Offset: 0x000318D8
	private MobileFriendListItem CreateFSGFooter(string text)
	{
		FriendListItemFooter friendListItemFooter = UnityEngine.Object.Instantiate<FriendListItemFooter>(this.prefabs.footerItem);
		friendListItemFooter.Text = text;
		return this.FinishCreateVisualItem<FriendListItemFooter>(friendListItemFooter, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, null, friendListItemFooter.gameObject);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0003370D File Offset: 0x0003190D
	private void UpdateAllHeaders()
	{
		this.UpdateRequestsHeader(null);
		this.UpdateNearbyPlayersHeader(null);
		this.UpdateFriendsHeader(null);
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00033724 File Offset: 0x00031924
	private void UpdateAllHeaderBackgrounds()
	{
		this.UpdateHeaderBackground(this.FindHeader(MobileFriendListItem.TypeFlags.Request));
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00033738 File Offset: 0x00031938
	private void UpdateRequestsHeader(FriendListItemHeader header = null)
	{
		int num = 0;
		foreach (FriendListFrame.FriendListItem friendListItem in this.m_allItems)
		{
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.Request)
			{
				num++;
			}
		}
		if (num > 0)
		{
			string text = GameStrings.Format("GLOBAL_FRIENDLIST_REQUESTS_HEADER", new object[]
			{
				num
			});
			if (header == null)
			{
				header = this.FindOrAddHeader(MobileFriendListItem.TypeFlags.Request);
				if (!this.DoesHeaderExist(MobileFriendListItem.TypeFlags.Request))
				{
					FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(true, MobileFriendListItem.TypeFlags.Request, null);
					this.AddItem(itemToAdd);
				}
			}
			header.SetText(text);
			return;
		}
		if (header == null)
		{
			this.RemoveItem(true, MobileFriendListItem.TypeFlags.Request, null);
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x00033810 File Offset: 0x00031A10
	private void UpdateNearbyPlayersHeader(FriendListItemHeader header = null)
	{
		int num = 0;
		foreach (FriendListFrame.FriendListItem friendListItem in this.m_allItems)
		{
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.NearbyPlayer && !FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(friendListItem.GetNearbyPlayer()))
			{
				num++;
			}
		}
		string text = GameStrings.Format("GLOBAL_FRIENDLIST_NEARBY_PLAYERS_HEADER", new object[]
		{
			num
		});
		if (header == null)
		{
			header = this.FindOrAddHeader(MobileFriendListItem.TypeFlags.NearbyPlayer);
			if (!this.DoesHeaderExist(MobileFriendListItem.TypeFlags.NearbyPlayer))
			{
				FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(true, MobileFriendListItem.TypeFlags.NearbyPlayer, null);
				this.AddItem(itemToAdd);
			}
		}
		FriendListNearbyPlayersHeader friendListNearbyPlayersHeader = header as FriendListNearbyPlayersHeader;
		if (friendListNearbyPlayersHeader != null)
		{
			friendListNearbyPlayersHeader.SetText(num);
		}
		else
		{
			header.SetText(text);
			Debug.LogError("FriendListFrame: Could not cast header to type FriendListNearbyPlayersHeader.");
		}
		if (header != null)
		{
			header.SetToggleEnabled(false);
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00033904 File Offset: 0x00031B04
	private List<FriendListFrame.FriendListItem> GetFriendItems()
	{
		List<FriendListFrame.FriendListItem> list = new List<FriendListFrame.FriendListItem>();
		foreach (FriendListFrame.FriendListItem item in this.m_allItems)
		{
			if (item.ItemMainType == MobileFriendListItem.TypeFlags.Friend)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00033968 File Offset: 0x00031B68
	private void UpdateFriendsHeader(FriendListItemHeader header = null)
	{
		List<FriendListFrame.FriendListItem> friendItems = this.GetFriendItems();
		int num = 0;
		foreach (FriendListFrame.FriendListItem friendListItem in friendItems)
		{
			BnetPlayer friend = friendListItem.GetFriend();
			if (friend.IsOnline() && !BnetNearbyPlayerMgr.Get().IsNearbyPlayer(friend) && !FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(friend))
			{
				num++;
			}
		}
		int count = friendItems.Count;
		string text;
		if (num == count)
		{
			text = GameStrings.Format("GLOBAL_FRIENDLIST_FRIENDS_HEADER_ALL_ONLINE", new object[]
			{
				num
			});
		}
		else
		{
			text = GameStrings.Format("GLOBAL_FRIENDLIST_FRIENDS_HEADER", new object[]
			{
				num,
				count
			});
		}
		if (header == null)
		{
			header = this.FindOrAddHeader(MobileFriendListItem.TypeFlags.Friend);
			if (!this.DoesHeaderExist(MobileFriendListItem.TypeFlags.Friend))
			{
				FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(true, MobileFriendListItem.TypeFlags.Friend, null);
				this.AddItem(itemToAdd);
			}
		}
		header.SetText(text);
		header.SetToggleEnabled(false);
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00033A74 File Offset: 0x00031C74
	private void UpdateHeaderBackground(FriendListItemHeader itemHeader)
	{
		if (itemHeader == null)
		{
			return;
		}
		MobileFriendListItem component = itemHeader.GetComponent<MobileFriendListItem>();
		if (component == null)
		{
			return;
		}
		TiledBackground tiledBackground = null;
		if (itemHeader.Background == null)
		{
			GameObject gameObject = new GameObject("ItemsBackground");
			gameObject.transform.parent = component.transform;
			TransformUtil.Identity(gameObject);
			gameObject.layer = 24;
			FriendListFrame.HeaderBackgroundInfo headerBackgroundInfo = ((component.Type & MobileFriendListItem.TypeFlags.Request) != (MobileFriendListItem.TypeFlags)0) ? this.listInfo.requestBackgroundInfo : this.listInfo.currentGameBackgroundInfo;
			gameObject.AddComponent<MeshFilter>().mesh = headerBackgroundInfo.mesh;
			gameObject.AddComponent<MeshRenderer>().SetMaterial(headerBackgroundInfo.material);
			tiledBackground = gameObject.AddComponent<TiledBackground>();
			itemHeader.Background = gameObject;
		}
		else
		{
			tiledBackground = itemHeader.Background.GetComponent<TiledBackground>();
		}
		tiledBackground.transform.parent = null;
		MobileFriendListItem.TypeFlags typeFlags = component.Type ^ MobileFriendListItem.TypeFlags.Header;
		Bounds bounds = new Bounds(component.transform.position, Vector3.zero);
		foreach (ITouchListItem touchListItem in this.items.RenderedItems)
		{
			MobileFriendListItem mobileFriendListItem = touchListItem as MobileFriendListItem;
			if (mobileFriendListItem != null && (mobileFriendListItem.Type & typeFlags) != (MobileFriendListItem.TypeFlags)0)
			{
				bounds.Encapsulate(mobileFriendListItem.ComputeWorldBounds());
			}
		}
		tiledBackground.transform.parent = component.transform.parent.transform;
		bounds.center = tiledBackground.transform.parent.transform.InverseTransformPoint(bounds.center);
		tiledBackground.SetBounds(bounds);
		TransformUtil.SetPosZ(tiledBackground.transform, 2f);
		tiledBackground.gameObject.SetActive(itemHeader.IsShowingContents);
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x00033C48 File Offset: 0x00031E48
	private FriendListItemHeader FindHeader(MobileFriendListItem.TypeFlags type)
	{
		type |= MobileFriendListItem.TypeFlags.Header;
		FriendListItemHeader result;
		this.m_headers.TryGetValue(type, out result);
		return result;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00033C6C File Offset: 0x00031E6C
	private bool DoesHeaderExist(MobileFriendListItem.TypeFlags type)
	{
		foreach (FriendListFrame.FriendListItem friendListItem in this.m_allItems)
		{
			if (friendListItem.IsHeader && friendListItem.SubType == type)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00033CD4 File Offset: 0x00031ED4
	private FriendListItemHeader FindOrAddHeader(MobileFriendListItem.TypeFlags type)
	{
		type |= MobileFriendListItem.TypeFlags.Header;
		FriendListItemHeader friendListItemHeader = this.FindHeader(type);
		if (friendListItemHeader == null)
		{
			FriendListFrame.FriendListItem friendListItem = new FriendListFrame.FriendListItem(true, type, null);
			if (type == (MobileFriendListItem.TypeFlags.NearbyPlayer | MobileFriendListItem.TypeFlags.Header))
			{
				friendListItemHeader = UnityEngine.Object.Instantiate<FriendListNearbyPlayersHeader>(this.prefabs.nearbyPlayersHeaderItem);
				((FriendListNearbyPlayersHeader)friendListItemHeader).OnPanelOpened += this.CloseFlyoutMenu;
			}
			else
			{
				friendListItemHeader = UnityEngine.Object.Instantiate<FriendListItemHeader>(this.prefabs.headerItem);
			}
			this.m_headers[type] = friendListItemHeader;
			Option option = Option.FRIENDS_LIST_FRIEND_SECTION_HIDE;
			MobileFriendListItem.TypeFlags subType = friendListItem.SubType;
			if (subType <= MobileFriendListItem.TypeFlags.CurrentGame)
			{
				if (subType == MobileFriendListItem.TypeFlags.Friend || subType == MobileFriendListItem.TypeFlags.CurrentGame)
				{
					option = Option.FRIENDS_LIST_FRIEND_SECTION_HIDE;
				}
			}
			else if (subType != MobileFriendListItem.TypeFlags.NearbyPlayer)
			{
				if (subType == MobileFriendListItem.TypeFlags.Request)
				{
					option = Option.FRIENDS_LIST_REQUEST_SECTION_HIDE;
				}
			}
			else
			{
				option = Option.FRIENDS_LIST_NEARBYPLAYER_SECTION_HIDE;
			}
			friendListItemHeader.SubType = friendListItem.SubType;
			friendListItemHeader.Option = option;
			bool showHeaderSection = this.GetShowHeaderSection(option);
			friendListItemHeader.SetInitialShowContents(showHeaderSection);
			friendListItemHeader.ClearToggleListeners();
			friendListItemHeader.AddToggleListener(new FriendListItemHeader.ToggleContentsFunc(this.OnHeaderSectionToggle), friendListItemHeader);
			UberText[] objs = UberText.EnableAllTextInObject(friendListItemHeader.gameObject, false);
			this.FinishCreateVisualItem<FriendListItemHeader>(friendListItemHeader, type, null, null);
			UberText.EnableAllTextObjects(objs, true);
		}
		return friendListItemHeader;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00033DF0 File Offset: 0x00031FF0
	private void OnHeaderSectionToggle(bool show, object userdata)
	{
		FriendListItemHeader header = (FriendListItemHeader)userdata;
		this.SetShowHeaderSection(header.Option, show);
		int startingLongListIndex = this.m_allItems.FindIndex((FriendListFrame.FriendListItem item) => item.IsHeader && item.SubType == header.SubType);
		this.items.RefreshList(startingLongListIndex, true);
		this.UpdateHeaderBackground(header);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00033E54 File Offset: 0x00032054
	public T FindFirstRenderedItem<T>(Predicate<T> predicate = null) where T : MonoBehaviour
	{
		foreach (ITouchListItem touchListItem in this.items.RenderedItems)
		{
			T component = touchListItem.GetComponent<T>();
			if (component != null && (predicate == null || predicate(component)))
			{
				return component;
			}
		}
		return default(T);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00033ED0 File Offset: 0x000320D0
	private List<T> GetRenderedItems<T>() where T : MonoBehaviour
	{
		List<T> list = new List<T>();
		foreach (ITouchListItem touchListItem in this.items.RenderedItems)
		{
			T component = touchListItem.GetComponent<T>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00033F3C File Offset: 0x0003213C
	private MobileFriendListItem FinishCreateVisualItem<T>(T obj, MobileFriendListItem.TypeFlags type, ITouchListItem parent, GameObject showObj) where T : MonoBehaviour
	{
		MobileFriendListItem mobileFriendListItem = obj.gameObject.GetComponent<MobileFriendListItem>();
		if (mobileFriendListItem == null)
		{
			mobileFriendListItem = obj.gameObject.AddComponent<MobileFriendListItem>();
			if (obj is FriendListFriendFrame)
			{
				((FriendListFriendFrame)((object)obj)).InitializeMobileFriendListItem(mobileFriendListItem);
			}
			BoxCollider component = mobileFriendListItem.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.size = new Vector3(component.size.x, component.size.y + this.items.elementSpacing, component.size.z);
			}
		}
		mobileFriendListItem.Type = type;
		mobileFriendListItem.SetShowObject(showObj);
		mobileFriendListItem.SetParent(parent);
		if (mobileFriendListItem.Selectable)
		{
			BnetPlayer selectedFriend = FriendMgr.Get().GetSelectedFriend();
			if (selectedFriend != null)
			{
				BnetPlayer bnetPlayer = null;
				if (obj is FriendListFriendFrame)
				{
					bnetPlayer = ((FriendListFriendFrame)((object)obj)).GetFriend();
				}
				if (bnetPlayer != null && selectedFriend == bnetPlayer)
				{
					mobileFriendListItem.Selected();
				}
			}
		}
		return mobileFriendListItem;
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00034034 File Offset: 0x00032234
	private bool RemoveItem(bool isHeader, MobileFriendListItem.TypeFlags type, object itemToRemove)
	{
		int num = this.m_allItems.FindIndex(delegate(FriendListFrame.FriendListItem item)
		{
			if (item.IsHeader != isHeader || item.SubType != type)
			{
				return false;
			}
			if (itemToRemove == null)
			{
				return true;
			}
			MobileFriendListItem.TypeFlags type2 = type;
			if (type2 > MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
			{
				if (type2 <= MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
				{
					if (type2 == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
					{
						return item.GetFiresideGatheringPlayer() == (BnetPlayer)itemToRemove;
					}
					if (type2 != MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
					{
						return false;
					}
				}
				else
				{
					if (type2 == MobileFriendListItem.TypeFlags.Request)
					{
						return item.GetInvite() == (BnetInvitation)itemToRemove;
					}
					if (type2 != MobileFriendListItem.TypeFlags.FoundFiresideGathering)
					{
						return false;
					}
				}
				return item.GetFSGConfig() == (FSGConfig)itemToRemove;
			}
			if (type2 <= MobileFriendListItem.TypeFlags.CurrentGame)
			{
				if (type2 == MobileFriendListItem.TypeFlags.Friend || type2 == MobileFriendListItem.TypeFlags.CurrentGame)
				{
					return item.GetFriend() == (BnetPlayer)itemToRemove;
				}
			}
			else
			{
				if (type2 == MobileFriendListItem.TypeFlags.NearbyPlayer)
				{
					return item.GetNearbyPlayer() == (BnetPlayer)itemToRemove;
				}
				if (type2 == MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
				{
					return item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter;
				}
			}
			return false;
		});
		if (num < 0)
		{
			return false;
		}
		this.m_allItems.RemoveAt(num);
		return true;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00034087 File Offset: 0x00032287
	private void AddItem(FriendListFrame.FriendListItem itemToAdd)
	{
		this.m_allItems.Add(itemToAdd);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00034095 File Offset: 0x00032295
	private void SuspendItemsLayout()
	{
		this.items.SuspendLayout();
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x000340A2 File Offset: 0x000322A2
	private void ResumeItemsLayout()
	{
		this.items.ResumeLayout(false);
		this.SortAndRefreshTouchList();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x000340B8 File Offset: 0x000322B8
	public void ToggleRemoveFriendsMode()
	{
		FriendListFrame.FriendListEditMode editMode = this.m_editMode;
		FriendListFrame.FriendListEditMode editFriendsMode;
		if (editMode != FriendListFrame.FriendListEditMode.NONE)
		{
			if (editMode != FriendListFrame.FriendListEditMode.REMOVE_FRIENDS)
			{
				global::Log.All.PrintError("FriendListFrame: Should not be toggling Remove Friends mode when in mode {0}!", new object[]
				{
					this.m_editMode
				});
				return;
			}
			editFriendsMode = FriendListFrame.FriendListEditMode.NONE;
		}
		else
		{
			editFriendsMode = FriendListFrame.FriendListEditMode.REMOVE_FRIENDS;
		}
		this.SetEditFriendsMode(editFriendsMode);
		this.removeFriendButtonDisabledVisual.SetActive(this.m_editMode == FriendListFrame.FriendListEditMode.REMOVE_FRIENDS);
		this.removeFriendButtonEnabledVisual.SetActive(this.m_editMode == FriendListFrame.FriendListEditMode.NONE);
		this.removeFriendButtonButtonGlow.SetActive(this.m_editMode == FriendListFrame.FriendListEditMode.REMOVE_FRIENDS);
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00034145 File Offset: 0x00032345
	private bool SetEditFriendsMode(FriendListFrame.FriendListEditMode mode)
	{
		this.m_editMode = mode;
		this.SortAndRefreshTouchList();
		this.UpdateFriendItems();
		return true;
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0003415B File Offset: 0x0003235B
	public void ExitRemoveFriendsMode()
	{
		if (this.m_editMode == FriendListFrame.FriendListEditMode.REMOVE_FRIENDS)
		{
			this.ToggleRemoveFriendsMode();
		}
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0003416C File Offset: 0x0003236C
	private void SortAndRefreshTouchList()
	{
		if (this.items.IsLayoutSuspended)
		{
			return;
		}
		this.m_allItems.Sort(new Comparison<FriendListFrame.FriendListItem>(this.ItemsSortCompare));
		if (this.m_longListBehavior == null)
		{
			this.m_longListBehavior = new FriendListFrame.VirtualizedFriendsListBehavior(this);
			this.items.LongListBehavior = this.m_longListBehavior;
			return;
		}
		this.items.RefreshList(0, true);
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x000341D4 File Offset: 0x000323D4
	private int ItemsSortCompare(FriendListFrame.FriendListItem item1, FriendListFrame.FriendListItem item2)
	{
		int num = item2.ItemFlags.CompareTo(item1.ItemFlags);
		if (num != 0)
		{
			return num;
		}
		MobileFriendListItem.TypeFlags itemFlags = item1.ItemFlags;
		if (itemFlags == MobileFriendListItem.TypeFlags.Friend || itemFlags == MobileFriendListItem.TypeFlags.CurrentGame)
		{
			return FriendUtils.FriendSortCompare(item1.GetFriend(), item2.GetFriend());
		}
		if (itemFlags == MobileFriendListItem.TypeFlags.NearbyPlayer)
		{
			return FriendUtils.FriendSortCompare(item1.GetNearbyPlayer(), item2.GetNearbyPlayer());
		}
		if (itemFlags == MobileFriendListItem.TypeFlags.Request)
		{
			BnetInvitation invite = item1.GetInvite();
			BnetInvitation invite2 = item2.GetInvite();
			int num2 = string.Compare(invite.GetInviterName(), invite2.GetInviterName(), true);
			if (num2 != 0)
			{
				return num2;
			}
			long lo = (long)invite.GetInviterId().GetLo();
			long lo2 = (long)invite2.GetInviterId().GetLo();
			return (int)(lo - lo2);
		}
		else
		{
			if (itemFlags == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
			{
				FSGConfig fsgconfig = item1.GetFSGConfig();
				FSGConfig fsgconfig2 = item2.GetFSGConfig();
				return FiresideGatheringManager.Get().FiresideGatheringSort(fsgconfig, fsgconfig2);
			}
			if (itemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
			{
				return FiresideGatheringManager.Get().FiresideGatheringPlayerSort(item1.GetFiresideGatheringPlayer(), item2.GetFiresideGatheringPlayer());
			}
			return 0;
		}
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x000342E0 File Offset: 0x000324E0
	private void RegisterFriendEvents()
	{
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		FriendChallengeMgr.Get().AddChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		BnetNearbyPlayerMgr.Get().AddChangeListener(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersChanged));
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		SpectatorManager.Get().OnInviteReceived += this.SpectatorManager_OnInviteReceivedOrSent;
		SpectatorManager.Get().OnInviteSent += this.SpectatorManager_OnInviteReceivedOrSent;
		Network.Get().RegisterNetHandler(RequestNearbyFSGsResponse.PacketID.ID, new Network.NetHandler(this.OnRequestNearbyFSGsResponse), null);
		FiresideGatheringManager.Get().OnJoinFSG += this.OnJoinFSG;
		FiresideGatheringManager.Get().OnLeaveFSG += this.OnLeaveFSG;
		FiresideGatheringManager.OnPatronListUpdated += this.OnFiresideGatheringPresencePatronsUpdated;
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.UpdateFSGState));
		NetCache.Get().RegisterUpdatedListener(typeof(FSGFeatureConfig), new Action(this.UpdateFSGState));
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0003442C File Offset: 0x0003262C
	private void UnregisterFriendEvents()
	{
		BnetFriendMgr.RemoveChangeListenerFromInstance(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged), null);
		BnetPresenceMgr.RemovePlayersChangedListenerFromInstance(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged), null);
		FriendChallengeMgr.RemoveChangedListenerFromInstance(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged), null);
		BnetNearbyPlayerMgr.RemoveChangeListenerFromInstance(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersChanged), null);
		SceneMgr sceneMgr = SceneMgr.Get();
		if (sceneMgr != null)
		{
			sceneMgr.UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		}
		if (SpectatorManager.InstanceExists())
		{
			SpectatorManager spectatorManager = SpectatorManager.Get();
			spectatorManager.OnInviteReceived -= this.SpectatorManager_OnInviteReceivedOrSent;
			spectatorManager.OnInviteSent -= this.SpectatorManager_OnInviteReceivedOrSent;
		}
		Network network = Network.Get();
		if (network != null)
		{
			network.RemoveNetHandler(RequestNearbyFSGsResponse.PacketID.ID, new Network.NetHandler(this.OnRequestNearbyFSGsResponse));
		}
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		if (firesideGatheringManager != null)
		{
			firesideGatheringManager.OnJoinFSG -= this.OnJoinFSG;
			firesideGatheringManager.OnLeaveFSG -= this.OnLeaveFSG;
			FiresideGatheringManager.OnPatronListUpdated -= this.OnFiresideGatheringPresencePatronsUpdated;
		}
		NetCache netCache = NetCache.Get();
		if (netCache != null)
		{
			netCache.RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.UpdateFSGState));
		}
		NetCache netCache2 = NetCache.Get();
		if (netCache2 == null)
		{
			return;
		}
		netCache2.RemoveUpdatedListener(typeof(FSGFeatureConfig), new Action(this.UpdateFSGState));
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00034584 File Offset: 0x00032784
	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		this.SuspendItemsLayout();
		this.UpdateRequests(changelist.GetAddedReceivedInvites(), changelist.GetRemovedReceivedInvites());
		this.UpdateAllFriends(changelist.GetAddedFriends(), changelist.GetRemovedFriends());
		this.UpdateAllHeaders();
		this.ResumeItemsLayout();
		this.UpdateAllHeaderBackgrounds();
		this.UpdateSelectedItem();
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x000345D4 File Offset: 0x000327D4
	private void OnNearbyPlayersChanged(BnetNearbyPlayerChangelist changelist, object userData)
	{
		this.m_nearbyPlayersNeedUpdate = true;
		if (changelist.GetAddedStrangers() != null)
		{
			foreach (BnetPlayer p in changelist.GetAddedStrangers())
			{
				FriendListFrame.NearbyPlayerUpdate item = new FriendListFrame.NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType.Added, p);
				this.m_nearbyPlayerUpdates.Remove(item);
				this.m_nearbyPlayerUpdates.Add(item);
			}
		}
		if (changelist.GetRemovedStrangers() != null)
		{
			foreach (BnetPlayer p2 in changelist.GetRemovedStrangers())
			{
				FriendListFrame.NearbyPlayerUpdate item2 = new FriendListFrame.NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType.Removed, p2);
				this.m_nearbyPlayerUpdates.Remove(item2);
				this.m_nearbyPlayerUpdates.Add(item2);
			}
		}
		if (changelist.GetAddedFriends() != null)
		{
			foreach (BnetPlayer p3 in changelist.GetAddedFriends())
			{
				FriendListFrame.NearbyPlayerUpdate item3 = new FriendListFrame.NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType.Added, p3);
				this.m_nearbyPlayerUpdates.Remove(item3);
				this.m_nearbyPlayerUpdates.Add(item3);
			}
		}
		if (changelist.GetRemovedFriends() != null)
		{
			foreach (BnetPlayer p4 in changelist.GetRemovedFriends())
			{
				FriendListFrame.NearbyPlayerUpdate item4 = new FriendListFrame.NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType.Removed, p4);
				this.m_nearbyPlayerUpdates.Remove(item4);
				this.m_nearbyPlayerUpdates.Add(item4);
			}
		}
		if (changelist.GetAddedPlayers() != null)
		{
			foreach (BnetPlayer p5 in changelist.GetAddedPlayers())
			{
				FriendListFrame.NearbyPlayerUpdate item5 = new FriendListFrame.NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType.Added, p5);
				this.m_nearbyPlayerUpdates.Remove(item5);
				this.m_nearbyPlayerUpdates.Add(item5);
			}
		}
		if (changelist.GetRemovedPlayers() != null)
		{
			foreach (BnetPlayer p6 in changelist.GetRemovedPlayers())
			{
				FriendListFrame.NearbyPlayerUpdate item6 = new FriendListFrame.NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType.Removed, p6);
				this.m_nearbyPlayerUpdates.Remove(item6);
				this.m_nearbyPlayerUpdates.Add(item6);
			}
		}
		if (base.gameObject.activeInHierarchy && Time.realtimeSinceStartup >= this.m_lastNearbyPlayersUpdate + 10f)
		{
			this.HandleNearbyPlayersChanged();
		}
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00034884 File Offset: 0x00032A84
	private void HandleNearbyPlayersChanged()
	{
		if (!this.m_nearbyPlayersNeedUpdate)
		{
			return;
		}
		this.UpdateFriendItems();
		BnetFriendChangelist bnetFriendChangelist = null;
		if (this.m_nearbyPlayerUpdates.Count > 0)
		{
			this.SuspendItemsLayout();
			foreach (FriendListFrame.NearbyPlayerUpdate nearbyPlayerUpdate in this.m_nearbyPlayerUpdates)
			{
				if (nearbyPlayerUpdate.Change == FriendListFrame.NearbyPlayerUpdate.ChangeType.Added)
				{
					if (!FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(nearbyPlayerUpdate.Player))
					{
						FriendListFrame.FriendListItem itemToAdd = new FriendListFrame.FriendListItem(false, MobileFriendListItem.TypeFlags.NearbyPlayer, nearbyPlayerUpdate.Player);
						this.AddItem(itemToAdd);
						if (BnetFriendMgr.Get().IsFriend(nearbyPlayerUpdate.Player))
						{
							if (bnetFriendChangelist == null)
							{
								bnetFriendChangelist = new BnetFriendChangelist();
							}
							bnetFriendChangelist.AddRemovedFriend(nearbyPlayerUpdate.Player);
						}
					}
				}
				else
				{
					this.RemoveItem(false, MobileFriendListItem.TypeFlags.NearbyPlayer, nearbyPlayerUpdate.Player);
					if (BnetFriendMgr.Get().IsFriend(nearbyPlayerUpdate.Player))
					{
						if (bnetFriendChangelist == null)
						{
							bnetFriendChangelist = new BnetFriendChangelist();
						}
						bnetFriendChangelist.AddAddedFriend(nearbyPlayerUpdate.Player);
					}
				}
			}
			this.m_nearbyPlayerUpdates.Clear();
			this.UpdateAllHeaders();
			this.ResumeItemsLayout();
			this.UpdateAllHeaderBackgrounds();
			this.UpdateSelectedItem();
		}
		this.m_nearbyPlayersNeedUpdate = false;
		this.m_lastNearbyPlayersUpdate = Time.realtimeSinceStartup;
		if (bnetFriendChangelist != null)
		{
			this.OnFriendsChanged(bnetFriendChangelist, null);
		}
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x000349D0 File Offset: 0x00032BD0
	private void DoPlayersChanged(BnetPlayerChangelist changelist)
	{
		this.SuspendItemsLayout();
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		bool flag = false;
		bool flag2 = false;
		foreach (BnetPlayerChange bnetPlayerChange in changelist.GetChanges())
		{
			BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
			BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
			if (newPlayer == myPlayer)
			{
				this.UpdateMyself();
				BnetGameAccount hearthstoneGameAccount = newPlayer.GetHearthstoneGameAccount();
				if (oldPlayer == null || oldPlayer.GetHearthstoneGameAccount() == null)
				{
					flag = hearthstoneGameAccount.CanBeInvitedToGame();
				}
				else
				{
					flag = (oldPlayer.GetHearthstoneGameAccount().CanBeInvitedToGame() != hearthstoneGameAccount.CanBeInvitedToGame());
				}
			}
			else
			{
				if (oldPlayer == null || oldPlayer.GetBestName() != newPlayer.GetBestName())
				{
					flag2 = true;
				}
				this.UpdateFriendFrame(newPlayer);
			}
		}
		if (flag)
		{
			this.UpdateItems();
		}
		else if (flag2)
		{
			this.UpdateFriendItems();
		}
		this.UpdateAllHeaders();
		this.UpdateAllHeaderBackgrounds();
		this.ResumeItemsLayout();
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x00034ADC File Offset: 0x00032CDC
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (base.gameObject.activeInHierarchy)
		{
			this.DoPlayersChanged(changelist);
			return;
		}
		List<BnetPlayerChange> changes = changelist.GetChanges();
		this.m_playersChangeList.GetChanges().AddRange(changes);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00034B16 File Offset: 0x00032D16
	private void OnRequestNearbyFSGsResponse()
	{
		global::Log.FiresideGatherings.Print("FriendListFrame.OnNearbyFSGsResponse", Array.Empty<object>());
		this.UpdateCurrentFiresideGatherings();
		this.UpdateFoundFiresideGatherings();
		this.SortAndRefreshTouchList();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00034B3E File Offset: 0x00032D3E
	private void OnJoinFSG(FSGConfig gathering)
	{
		global::Log.FiresideGatherings.Print("FriendListFrame.OnJoinFSG", Array.Empty<object>());
		this.UpdateCurrentFiresideGatherings();
		this.UpdateFiresideGatheringPlayers(FiresideGatheringManager.Get().DisplayablePatronList, null);
		this.SortAndRefreshTouchList();
		this.UpdateFSGState();
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00034B77 File Offset: 0x00032D77
	private void OnLeaveFSG(FSGConfig gathering)
	{
		global::Log.FiresideGatherings.Print("FriendListFrame.OnLeaveFSG", Array.Empty<object>());
		this.UpdateFoundFiresideGatherings();
		this.RemoveAllFiresideGatheringPlayers();
		this.SortAndRefreshTouchList();
		this.UpdateFSGState();
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00034BA8 File Offset: 0x00032DA8
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode - SceneMgr.Mode.FRIENDLY > 1)
		{
			return;
		}
		if (ChatMgr.Get() != null)
		{
			ChatMgr.Get().CloseFriendsList();
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00034BEA File Offset: 0x00032DEA
	private void SpectatorManager_OnInviteReceivedOrSent(OnlineEventType evt, BnetPlayer inviter)
	{
		this.UpdateFriendFrame(inviter);
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00034BF3 File Offset: 0x00032DF3
	private void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		if (player == BnetPresenceMgr.Get().GetMyPlayer())
		{
			this.UpdateFriendItems();
			return;
		}
		this.UpdateFriendFrame(player);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00034C10 File Offset: 0x00032E10
	private bool HandleKeyboardInput()
	{
		FatalErrorMgr.Get().HasError();
		return false;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00034C20 File Offset: 0x00032E20
	private void OnAddFriendButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		this.CloseFlyoutMenu();
		if (this.m_addFriendFrame != null)
		{
			this.CloseAddFriendFrame();
			return;
		}
		if (this.AddFriendFrameOpened != null)
		{
			this.AddFriendFrameOpened();
		}
		BnetPlayer selectedFriend = FriendMgr.Get().GetSelectedFriend();
		this.ShowAddFriendFrame(selectedFriend);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00034C81 File Offset: 0x00032E81
	private void OnEditFriendsButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		this.ToggleRemoveFriendsMode();
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00034C9D File Offset: 0x00032E9D
	private void OnRAFButtonReleased(UIEvent e)
	{
		if (!this.m_isRAFButtonEnabled)
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		RAFManager.Get().ShowRAFFrame();
		TelemetryManager.Client().SendClickRecruitAFriend();
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00034CD0 File Offset: 0x00032ED0
	private void OnRAFButtonOver(UIEvent e)
	{
		TooltipZone component = this.rafButton.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		string bodytext = (GameUtils.GetNextTutorial() != 0) ? GameStrings.Get("GLUE_RAF_TOOLTIP_LOCKED_DESC") : GameStrings.Get("GLUE_RAF_TOOLTIP_DESC");
		component.ShowSocialTooltip(this.rafButton, GameStrings.Get("GLUE_RAF_TOOLTIP_HEADLINE"), bodytext, 75f, GameLayer.BattleNetDialog, 0);
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00034D30 File Offset: 0x00032F30
	private void OnRAFButtonOut(UIEvent e)
	{
		TooltipZone component = this.rafButton.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00034D58 File Offset: 0x00032F58
	private void OnFSGButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		Options.Get().SetBool(Option.HAS_CLICKED_FIRESIDE_GATHERINGS_BUTTON, true);
		FiresideGatheringManager.Get().ShowFindFSGDialog();
		ChatMgr.Get().CloseFriendsList();
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00034D94 File Offset: 0x00032F94
	private bool OnRemoveFriendDialogShown(DialogBase dialog, object userData)
	{
		BnetPlayer player = (BnetPlayer)userData;
		if (!BnetFriendMgr.Get().IsFriend(player))
		{
			return false;
		}
		this.m_removeFriendPopup = (AlertPopup)dialog;
		return true;
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00034DC4 File Offset: 0x00032FC4
	private void OnRemoveFriendPopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM && this.m_friendToRemove != null)
		{
			BnetFriendMgr.Get().RemoveFriend(this.m_friendToRemove);
		}
		this.m_friendToRemove = null;
		this.m_removeFriendPopup = null;
		if (this.RemoveFriendPopupClosed != null)
		{
			this.RemoveFriendPopupClosed();
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00034E04 File Offset: 0x00033004
	private void OnFlyoutButtonReleased(UIEvent e)
	{
		if (this.IsInEditMode)
		{
			this.ExitRemoveFriendsMode();
		}
		else if (this.m_flyoutOpen)
		{
			this.CloseFlyoutMenu();
		}
		else
		{
			this.OpenFlyoutMenu();
		}
		if (ChatMgr.Get().IsChatLogUIShowing())
		{
			ChatMgr.Get().CloseChatUI(false);
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00034E44 File Offset: 0x00033044
	private void UpdateSelectedItem()
	{
		BnetPlayer selectedFriend = FriendMgr.Get().GetSelectedFriend();
		FriendListFriendFrame friendListFriendFrame = this.FindRenderedBaseFriendFrame(selectedFriend);
		if (friendListFriendFrame == null)
		{
			if (this.items.SelectedIndex == -1)
			{
				return;
			}
			this.items.SelectedIndex = -1;
			if (this.m_removeFriendPopup != null)
			{
				this.m_removeFriendPopup.Hide();
				this.m_removeFriendPopup = null;
				if (this.RemoveFriendPopupClosed != null)
				{
					this.RemoveFriendPopupClosed();
					return;
				}
			}
		}
		else
		{
			this.items.SelectedIndex = this.items.IndexOf(friendListFriendFrame.GetComponent<MobileFriendListItem>());
		}
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00034ED8 File Offset: 0x000330D8
	private void UpdateRAFState()
	{
		if (SetRotationManager.ShouldShowSetRotationIntro() || SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN || WelcomeQuests.Get() != null || TemporaryAccountManager.IsTemporaryAccount() || GameUtils.GetNextTutorial() != 0)
		{
			this.SetRAFButtonEnabled(false);
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00034F10 File Offset: 0x00033110
	private void UpdateFSGState()
	{
		this.SetFSGButtonEnabled();
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00034F18 File Offset: 0x00033118
	private void InitButtons()
	{
		this.addFriendButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnAddFriendButtonReleased));
		this.removeFriendButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnEditFriendsButtonReleased));
		this.rafButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRAFButtonReleased));
		this.rafButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnRAFButtonOver));
		this.rafButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnRAFButtonOut));
		this.fsgButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFSGButtonReleased));
		this.flyoutMenuButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFlyoutButtonReleased));
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00034FD4 File Offset: 0x000331D4
	private bool GetShowHeaderSection(Option setoption)
	{
		return !(bool)Options.Get().GetOption(setoption, false);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00034FEF File Offset: 0x000331EF
	private void SetShowHeaderSection(Option sectionoption, bool show)
	{
		if (this.GetShowHeaderSection(sectionoption) != show)
		{
			Options.Get().SetOption(sectionoption, !show);
		}
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0003500F File Offset: 0x0003320F
	private Transform GetBottomRightBone()
	{
		if (!(this.scrollbar != null) || !this.scrollbar.gameObject.activeSelf)
		{
			return this.listInfo.bottomRight;
		}
		return this.listInfo.bottomRightWithScrollbar;
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00035048 File Offset: 0x00033248
	private void OnTemporaryAccountSignUpButtonPressed(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		ChatMgr.Get().CloseFriendsList();
		TemporaryAccountManager.Get().ShowHealUpPage(TemporaryAccountManager.HealUpReason.FRIENDS_LIST, null);
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00035074 File Offset: 0x00033274
	private void OnMyRankedMedalWidgetReady(Widget widget)
	{
		this.m_myRankedMedalWidget = widget;
		this.m_myRankedMedal = widget.GetComponentInChildren<RankedMedal>();
		this.UpdateMyRankedMedalWidget();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00035090 File Offset: 0x00033290
	private void UpdateMyRankedMedalWidget()
	{
		if (this.m_myRankedMedal == null)
		{
			return;
		}
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		if (localPlayerMedalInfo == null || !localPlayerMedalInfo.IsDisplayable())
		{
			this.m_myRankedMedalWidget.Hide();
			return;
		}
		this.m_myRankedMedalWidget.Show();
		localPlayerMedalInfo.CreateOrUpdateDataModel(localPlayerMedalInfo.GetBestCurrentRankFormatType(), ref this.m_myRankedDataModel, RankedMedal.DisplayMode.Default, false, false, delegate(RankedPlayDataModel dm)
		{
			this.m_myRankedMedal.BindRankedPlayDataModel(dm);
		});
	}

	// Token: 0x040005C4 RID: 1476
	public FriendListFrame.Me me;

	// Token: 0x040005C5 RID: 1477
	public FriendListFrame.Prefabs prefabs;

	// Token: 0x040005C6 RID: 1478
	public FriendListFrame.ListInfo listInfo;

	// Token: 0x040005C7 RID: 1479
	public TouchList items;

	// Token: 0x040005C8 RID: 1480
	public PlayerPortrait myPortrait;

	// Token: 0x040005C9 RID: 1481
	public PegUIElement addFriendButton;

	// Token: 0x040005CA RID: 1482
	public PegUIElement removeFriendButton;

	// Token: 0x040005CB RID: 1483
	public GameObject removeFriendButtonEnabledVisual;

	// Token: 0x040005CC RID: 1484
	public GameObject removeFriendButtonDisabledVisual;

	// Token: 0x040005CD RID: 1485
	public GameObject removeFriendButtonButtonGlow;

	// Token: 0x040005CE RID: 1486
	public PegUIElement rafButton;

	// Token: 0x040005CF RID: 1487
	public GameObject rafButtonEnabledVisual;

	// Token: 0x040005D0 RID: 1488
	public GameObject rafButtonDisabledVisual;

	// Token: 0x040005D1 RID: 1489
	public GameObject rafButtonButtonGlow;

	// Token: 0x040005D2 RID: 1490
	public GameObject rafButtonGlowBone;

	// Token: 0x040005D3 RID: 1491
	public TouchListScrollbar scrollbar;

	// Token: 0x040005D4 RID: 1492
	public NineSliceElement window;

	// Token: 0x040005D5 RID: 1493
	public PegUIElement fsgButton;

	// Token: 0x040005D6 RID: 1494
	public GameObject fsgButtonButtonGlow;

	// Token: 0x040005D7 RID: 1495
	public GameObject portraitBackground;

	// Token: 0x040005D8 RID: 1496
	public Material unrankedBackground;

	// Token: 0x040005D9 RID: 1497
	public Material rankedBackground;

	// Token: 0x040005DA RID: 1498
	public GameObject innerShadow;

	// Token: 0x040005DB RID: 1499
	public GameObject outerShadow;

	// Token: 0x040005DC RID: 1500
	public GameObject temporaryAccountPaper;

	// Token: 0x040005DD RID: 1501
	public GameObject temporaryAccountCover;

	// Token: 0x040005DE RID: 1502
	public GameObject temporaryAccountDrawingBone;

	// Token: 0x040005DF RID: 1503
	public GameObject temporaryAccountDrawing;

	// Token: 0x040005E0 RID: 1504
	public UIBButton temporaryAccountSignUpButton;

	// Token: 0x040005E1 RID: 1505
	public PegUIElement flyoutMenuButton;

	// Token: 0x040005E2 RID: 1506
	public GameObject flyoutMenu;

	// Token: 0x040005E3 RID: 1507
	public float flyoutMiddleFrameScaleOffsetForFSG;

	// Token: 0x040005E4 RID: 1508
	public float flyoutShadowScaleOffsetForFSG;

	// Token: 0x040005E5 RID: 1509
	public GameObject flyoutMiddleFrame;

	// Token: 0x040005E6 RID: 1510
	public GameObject flyoutMiddleShadow;

	// Token: 0x040005E7 RID: 1511
	public MultiSliceElement flyoutFrameContainer;

	// Token: 0x040005E8 RID: 1512
	public MultiSliceElement flyoutShadowContainer;

	// Token: 0x040005E9 RID: 1513
	public HighlightState flyoutButtonGlow;

	// Token: 0x040005EA RID: 1514
	private AddFriendFrame m_addFriendFrame;

	// Token: 0x040005EB RID: 1515
	private AlertPopup m_removeFriendPopup;

	// Token: 0x040005EC RID: 1516
	private Camera m_itemsCamera;

	// Token: 0x040005ED RID: 1517
	private FriendListFrame.FriendListEditMode m_editMode;

	// Token: 0x040005EE RID: 1518
	private BnetPlayer m_friendToRemove;

	// Token: 0x040005EF RID: 1519
	private bool m_flyoutOpen;

	// Token: 0x040005F0 RID: 1520
	private bool m_patronStrangersHidden;

	// Token: 0x040005F1 RID: 1521
	private const int PatronCountHardLimit = 99;

	// Token: 0x040005F2 RID: 1522
	private RankedMedal m_myRankedMedal;

	// Token: 0x040005F3 RID: 1523
	private Widget m_myRankedMedalWidget;

	// Token: 0x040005F4 RID: 1524
	private RankedPlayDataModel m_myRankedDataModel;

	// Token: 0x040005F5 RID: 1525
	private Coroutine m_updateFriendItemsWhenAvailableCoroutine;

	// Token: 0x040005F6 RID: 1526
	private List<FriendListFrame.NearbyPlayerUpdate> m_nearbyPlayerUpdates = new List<FriendListFrame.NearbyPlayerUpdate>();

	// Token: 0x040005F7 RID: 1527
	private BnetPlayerChangelist m_playersChangeList = new BnetPlayerChangelist();

	// Token: 0x040005F8 RID: 1528
	private float m_lastNearbyPlayersUpdate;

	// Token: 0x040005F9 RID: 1529
	private bool m_nearbyPlayersNeedUpdate;

	// Token: 0x040005FA RID: 1530
	private const float NEARBY_PLAYERS_UPDATE_TIME = 10f;

	// Token: 0x040005FB RID: 1531
	private bool m_isRAFButtonEnabled = true;

	// Token: 0x040005FC RID: 1532
	private bool m_isFSGButtonEnabled = true;

	// Token: 0x040005FD RID: 1533
	private List<FriendListFrame.FriendListItem> m_allItems = new List<FriendListFrame.FriendListItem>();

	// Token: 0x040005FE RID: 1534
	private FriendListFrame.VirtualizedFriendsListBehavior m_longListBehavior;

	// Token: 0x040005FF RID: 1535
	private Dictionary<MobileFriendListItem.TypeFlags, FriendListItemHeader> m_headers = new Dictionary<MobileFriendListItem.TypeFlags, FriendListItemHeader>();

	// Token: 0x02001389 RID: 5001
	public enum FriendListEditMode
	{
		// Token: 0x0400A70A RID: 42762
		NONE,
		// Token: 0x0400A70B RID: 42763
		REMOVE_FRIENDS
	}

	// Token: 0x0200138A RID: 5002
	private class NearbyPlayerUpdate
	{
		// Token: 0x0600D7BD RID: 55229 RVA: 0x003EC4EE File Offset: 0x003EA6EE
		public NearbyPlayerUpdate(FriendListFrame.NearbyPlayerUpdate.ChangeType c, BnetPlayer p)
		{
			this.Change = c;
			this.Player = p;
		}

		// Token: 0x0600D7BE RID: 55230 RVA: 0x003EC504 File Offset: 0x003EA704
		public override bool Equals(object obj)
		{
			FriendListFrame.NearbyPlayerUpdate nearbyPlayerUpdate = obj as FriendListFrame.NearbyPlayerUpdate;
			return nearbyPlayerUpdate != null && this.Change == nearbyPlayerUpdate.Change && this.Player.GetAccountId() == nearbyPlayerUpdate.Player.GetAccountId();
		}

		// Token: 0x0600D7BF RID: 55231 RVA: 0x003EC548 File Offset: 0x003EA748
		public override int GetHashCode()
		{
			return this.Player.GetHashCode();
		}

		// Token: 0x0400A70C RID: 42764
		public FriendListFrame.NearbyPlayerUpdate.ChangeType Change;

		// Token: 0x0400A70D RID: 42765
		public BnetPlayer Player;

		// Token: 0x02002977 RID: 10615
		public enum ChangeType
		{
			// Token: 0x0400FCDA RID: 64730
			Added,
			// Token: 0x0400FCDB RID: 64731
			Removed
		}
	}

	// Token: 0x0200138B RID: 5003
	[Serializable]
	public class Me
	{
		// Token: 0x0400A70E RID: 42766
		public UberText nameText;

		// Token: 0x0400A70F RID: 42767
		public AsyncReference m_rankedMedalWidgetReference;
	}

	// Token: 0x0200138C RID: 5004
	[Serializable]
	public class RecentOpponent
	{
		// Token: 0x0400A710 RID: 42768
		public PegUIElement button;

		// Token: 0x0400A711 RID: 42769
		public UberText nameText;
	}

	// Token: 0x0200138D RID: 5005
	[Serializable]
	public class Prefabs
	{
		// Token: 0x0400A712 RID: 42770
		public FriendListItemHeader headerItem;

		// Token: 0x0400A713 RID: 42771
		public FriendListItemFooter footerItem;

		// Token: 0x0400A714 RID: 42772
		public FriendListNearbyPlayersHeader nearbyPlayersHeaderItem;

		// Token: 0x0400A715 RID: 42773
		public FriendListRequestFrame requestItem;

		// Token: 0x0400A716 RID: 42774
		public FriendListFriendFrame friendItem;

		// Token: 0x0400A717 RID: 42775
		public FriendListFSGFrame fsgItem;

		// Token: 0x0400A718 RID: 42776
		public AddFriendFrame addFriendFrame;
	}

	// Token: 0x0200138E RID: 5006
	[Serializable]
	public class ListInfo
	{
		// Token: 0x0400A719 RID: 42777
		public Transform topLeft;

		// Token: 0x0400A71A RID: 42778
		public Transform bottomRight;

		// Token: 0x0400A71B RID: 42779
		public Transform bottomRightWithScrollbar;

		// Token: 0x0400A71C RID: 42780
		public FriendListFrame.HeaderBackgroundInfo requestBackgroundInfo;

		// Token: 0x0400A71D RID: 42781
		public FriendListFrame.HeaderBackgroundInfo currentGameBackgroundInfo;
	}

	// Token: 0x0200138F RID: 5007
	[Serializable]
	public class HeaderBackgroundInfo
	{
		// Token: 0x0400A71E RID: 42782
		public Mesh mesh;

		// Token: 0x0400A71F RID: 42783
		public Material material;
	}

	// Token: 0x02001390 RID: 5008
	public struct FriendListItem
	{
		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600D7C5 RID: 55237 RVA: 0x003EC555 File Offset: 0x003EA755
		// (set) Token: 0x0600D7C6 RID: 55238 RVA: 0x003EC55D File Offset: 0x003EA75D
		public MobileFriendListItem.TypeFlags ItemFlags { get; private set; }

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600D7C7 RID: 55239 RVA: 0x003EC566 File Offset: 0x003EA766
		public bool IsHeader
		{
			get
			{
				return MobileFriendListItem.ItemIsHeader(this.ItemFlags);
			}
		}

		// Token: 0x0600D7C8 RID: 55240 RVA: 0x003EC573 File Offset: 0x003EA773
		public BnetPlayer GetFriend()
		{
			if ((this.ItemFlags & MobileFriendListItem.TypeFlags.Friend) == (MobileFriendListItem.TypeFlags)0)
			{
				return null;
			}
			return (BnetPlayer)this.m_item;
		}

		// Token: 0x0600D7C9 RID: 55241 RVA: 0x003EC58C File Offset: 0x003EA78C
		public BnetPlayer GetNearbyPlayer()
		{
			if ((this.ItemFlags & MobileFriendListItem.TypeFlags.NearbyPlayer) == (MobileFriendListItem.TypeFlags)0)
			{
				return null;
			}
			return (BnetPlayer)this.m_item;
		}

		// Token: 0x0600D7CA RID: 55242 RVA: 0x003EC5A5 File Offset: 0x003EA7A5
		public BnetInvitation GetInvite()
		{
			if ((this.ItemFlags & MobileFriendListItem.TypeFlags.Request) == (MobileFriendListItem.TypeFlags)0)
			{
				return null;
			}
			return (BnetInvitation)this.m_item;
		}

		// Token: 0x0600D7CB RID: 55243 RVA: 0x003EC5C2 File Offset: 0x003EA7C2
		public FSGConfig GetFSGConfig()
		{
			if ((this.ItemFlags & MobileFriendListItem.TypeFlags.FoundFiresideGathering) == (MobileFriendListItem.TypeFlags)0 && (this.ItemFlags & MobileFriendListItem.TypeFlags.CurrentFiresideGathering) == (MobileFriendListItem.TypeFlags)0)
			{
				return null;
			}
			return (FSGConfig)this.m_item;
		}

		// Token: 0x0600D7CC RID: 55244 RVA: 0x003EC5EA File Offset: 0x003EA7EA
		public BnetPlayer GetFiresideGatheringPlayer()
		{
			if ((this.ItemFlags & MobileFriendListItem.TypeFlags.FiresideGatheringPlayer) == (MobileFriendListItem.TypeFlags)0)
			{
				return null;
			}
			return (BnetPlayer)this.m_item;
		}

		// Token: 0x0600D7CD RID: 55245 RVA: 0x003EC604 File Offset: 0x003EA804
		public string GetText()
		{
			return (string)this.m_item;
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600D7CE RID: 55246 RVA: 0x003EC611 File Offset: 0x003EA811
		public MobileFriendListItem.TypeFlags ItemMainType
		{
			get
			{
				if (this.IsHeader)
				{
					return MobileFriendListItem.TypeFlags.Header;
				}
				return this.SubType;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x0600D7CF RID: 55247 RVA: 0x003EC623 File Offset: 0x003EA823
		public MobileFriendListItem.TypeFlags SubType
		{
			get
			{
				return this.ItemFlags & ~MobileFriendListItem.TypeFlags.Header;
			}
		}

		// Token: 0x0600D7D0 RID: 55248 RVA: 0x003EC62E File Offset: 0x003EA82E
		public override string ToString()
		{
			if (this.IsHeader)
			{
				return string.Format("[{0}]Header", this.SubType);
			}
			return string.Format("[{0}]{1}", this.ItemMainType, this.m_item);
		}

		// Token: 0x0600D7D1 RID: 55249 RVA: 0x003EC66C File Offset: 0x003EA86C
		public Type GetFrameType()
		{
			MobileFriendListItem.TypeFlags itemMainType = this.ItemMainType;
			if (itemMainType > MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
			{
				if (itemMainType <= MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
				{
					if (itemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
					{
						return typeof(FriendListFriendFrame);
					}
					if (itemMainType != MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
					{
						goto IL_9F;
					}
				}
				else
				{
					if (itemMainType == MobileFriendListItem.TypeFlags.Request)
					{
						return typeof(FriendListRequestFrame);
					}
					if (itemMainType != MobileFriendListItem.TypeFlags.FoundFiresideGathering)
					{
						goto IL_9F;
					}
				}
				return typeof(FriendListFSGFrame);
			}
			switch (itemMainType)
			{
			case MobileFriendListItem.TypeFlags.Header:
				return typeof(FriendListItemHeader);
			case MobileFriendListItem.TypeFlags.Friend:
			case MobileFriendListItem.TypeFlags.CurrentGame:
				return typeof(FriendListFriendFrame);
			case MobileFriendListItem.TypeFlags.Friend | MobileFriendListItem.TypeFlags.Header:
				break;
			default:
				if (itemMainType == MobileFriendListItem.TypeFlags.NearbyPlayer)
				{
					return typeof(FriendListFriendFrame);
				}
				if (itemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
				{
					return typeof(FriendListItemFooter);
				}
				break;
			}
			IL_9F:
			throw new Exception(string.Concat(new object[]
			{
				"Unknown ItemType: ",
				this.ItemFlags,
				" (",
				(int)this.ItemFlags,
				")"
			}));
		}

		// Token: 0x0600D7D2 RID: 55250 RVA: 0x003EC75C File Offset: 0x003EA95C
		public FriendListItem(bool isHeader, MobileFriendListItem.TypeFlags itemType, object itemData)
		{
			this = default(FriendListFrame.FriendListItem);
			if (!isHeader && itemData == null)
			{
				global::Log.All.Print("FriendListItem: itemData is null! itemType=" + itemType, Array.Empty<object>());
			}
			this.m_item = itemData;
			this.ItemFlags = itemType;
			if (isHeader)
			{
				this.ItemFlags |= MobileFriendListItem.TypeFlags.Header;
				return;
			}
			this.ItemFlags &= ~MobileFriendListItem.TypeFlags.Header;
		}

		// Token: 0x0400A721 RID: 42785
		private object m_item;
	}

	// Token: 0x02001391 RID: 5009
	private class VirtualizedFriendsListBehavior : TouchList.ILongListBehavior
	{
		// Token: 0x0600D7D3 RID: 55251 RVA: 0x003EC7C4 File Offset: 0x003EA9C4
		public VirtualizedFriendsListBehavior(FriendListFrame friendList)
		{
			this.m_friendList = friendList;
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600D7D4 RID: 55252 RVA: 0x003EC7E5 File Offset: 0x003EA9E5
		public List<MobileFriendListItem> FreeList
		{
			get
			{
				return this.m_freelist;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600D7D5 RID: 55253 RVA: 0x003EC7ED File Offset: 0x003EA9ED
		public int AllItemsCount
		{
			get
			{
				return this.m_friendList.m_allItems.Count;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x0600D7D6 RID: 55254 RVA: 0x003EC800 File Offset: 0x003EAA00
		public int MaxVisibleItems
		{
			get
			{
				if (this.m_cachedMaxVisibleItems >= 0)
				{
					return this.m_cachedMaxVisibleItems;
				}
				this.m_cachedMaxVisibleItems = 0;
				UnityEngine.Vector2 clipSize = this.m_friendList.items.ClipSize;
				Bounds prefabBounds = FriendListFrame.VirtualizedFriendsListBehavior.GetPrefabBounds(this.m_friendList.prefabs.requestItem.gameObject);
				Bounds prefabBounds2 = FriendListFrame.VirtualizedFriendsListBehavior.GetPrefabBounds(this.m_friendList.prefabs.friendItem.gameObject);
				float a = prefabBounds.max.y - prefabBounds.min.y;
				float b = prefabBounds2.max.y - prefabBounds2.min.y;
				float num = Mathf.Min(a, b);
				if (num > 0f)
				{
					int num2 = Mathf.CeilToInt(clipSize.y / num);
					this.m_cachedMaxVisibleItems = num2 + 3;
				}
				return this.m_cachedMaxVisibleItems;
			}
		}

		// Token: 0x0600D7D7 RID: 55255 RVA: 0x003EC8D0 File Offset: 0x003EAAD0
		private static Bounds GetPrefabBounds(GameObject prefabGameObject)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefabGameObject);
			gameObject.SetActive(true);
			Bounds result = TransformUtil.ComputeSetPointBounds(gameObject);
			UnityEngine.Object.DestroyImmediate(gameObject);
			return result;
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x0600D7D8 RID: 55256 RVA: 0x000052D6 File Offset: 0x000034D6
		public int MinBuffer
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x0600D7D9 RID: 55257 RVA: 0x003EC8F7 File Offset: 0x003EAAF7
		public int MaxAcquiredItems
		{
			get
			{
				return this.MaxVisibleItems + 2 * this.MinBuffer;
			}
		}

		// Token: 0x0600D7DA RID: 55258 RVA: 0x003EC908 File Offset: 0x003EAB08
		public bool IsItemShowable(int allItemsIndex)
		{
			if (allItemsIndex < 0 || allItemsIndex >= this.AllItemsCount)
			{
				return false;
			}
			FriendListFrame.FriendListItem friendListItem = this.m_friendList.m_allItems[allItemsIndex];
			if (friendListItem.IsHeader && !this.m_friendList.IsInEditMode)
			{
				return true;
			}
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter && !this.m_friendList.IsInEditMode)
			{
				return true;
			}
			if (friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.FoundFiresideGathering && friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.CurrentFiresideGathering && friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
			{
				FriendListItemHeader friendListItemHeader = this.m_friendList.FindHeader(friendListItem.SubType);
				if (friendListItemHeader == null || !friendListItemHeader.IsShowingContents)
				{
					return false;
				}
			}
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
			{
				if (FiresideGatheringManager.Get().IsCheckedIn)
				{
					return false;
				}
				if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB) && friendListItem.GetFSGConfig().IsInnkeeper && !friendListItem.GetFSGConfig().IsSetupComplete)
				{
					return false;
				}
			}
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
			{
				if (!FiresideGatheringManager.Get().IsCheckedIn)
				{
					return false;
				}
				if (!FiresideGatheringManager.Get().IsCheckedInToFSG(friendListItem.GetFSGConfig().FsgId))
				{
					return false;
				}
			}
			if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.Friend && BnetNearbyPlayerMgr.Get().IsNearbyPlayer(friendListItem.GetFriend()))
			{
				return false;
			}
			if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.NearbyPlayer && FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(friendListItem.GetNearbyPlayer()))
			{
				return false;
			}
			if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer && (!friendListItem.GetFiresideGatheringPlayer().IsOnline() || friendListItem.GetFiresideGatheringPlayer().GetBestProgramId() != BnetProgramId.HEARTHSTONE))
			{
				return false;
			}
			if (this.m_friendList.EditMode == FriendListFrame.FriendListEditMode.REMOVE_FRIENDS)
			{
				if (friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.Header)
				{
					if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer && !BnetFriendMgr.Get().IsFriend(friendListItem.GetFiresideGatheringPlayer()))
					{
						return false;
					}
					if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.NearbyPlayer && !BnetFriendMgr.Get().IsFriend(friendListItem.GetNearbyPlayer()))
					{
						return false;
					}
					if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
					{
						return false;
					}
					if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering || friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
					{
						return false;
					}
				}
				else if (friendListItem.ItemFlags == (MobileFriendListItem.TypeFlags.NearbyPlayer | MobileFriendListItem.TypeFlags.Header))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600D7DB RID: 55259 RVA: 0x003ECB2C File Offset: 0x003EAD2C
		public Vector3 GetItemSize(int allItemsIndex)
		{
			if (allItemsIndex < 0 || allItemsIndex >= this.AllItemsCount)
			{
				return Vector3.zero;
			}
			FriendListFrame.FriendListItem friendListItem = this.m_friendList.m_allItems[allItemsIndex];
			if (this.m_boundsByType == null)
			{
				this.InitializeBoundsByTypeArray();
			}
			int boundsByTypeIndex = this.GetBoundsByTypeIndex(friendListItem.ItemMainType);
			return this.m_boundsByType[boundsByTypeIndex].size;
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x0600D7DC RID: 55260 RVA: 0x003ECB8C File Offset: 0x003EAD8C
		private bool HasCollapsedHeaders
		{
			get
			{
				foreach (KeyValuePair<MobileFriendListItem.TypeFlags, FriendListItemHeader> keyValuePair in this.m_friendList.m_headers)
				{
					if (!keyValuePair.Value.IsShowingContents)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600D7DD RID: 55261 RVA: 0x003ECBF4 File Offset: 0x003EADF4
		public void ReleaseAllItems()
		{
			if (this.m_acquiredItems.Count == 0)
			{
				return;
			}
			if (this.m_freelist == null)
			{
				this.m_freelist = new List<MobileFriendListItem>();
			}
			foreach (MobileFriendListItem mobileFriendListItem in this.m_acquiredItems)
			{
				if (mobileFriendListItem.IsHeader)
				{
					mobileFriendListItem.gameObject.SetActive(false);
				}
				else if (this.m_freelist.Count >= 20)
				{
					UnityEngine.Object.Destroy(mobileFriendListItem.gameObject);
				}
				else
				{
					this.m_freelist.Add(mobileFriendListItem);
					mobileFriendListItem.gameObject.SetActive(false);
				}
				mobileFriendListItem.Unselected();
			}
			this.m_acquiredItems.Clear();
		}

		// Token: 0x0600D7DE RID: 55262 RVA: 0x003ECCBC File Offset: 0x003EAEBC
		public void ReleaseItem(ITouchListItem item)
		{
			MobileFriendListItem mobileFriendListItem = item as MobileFriendListItem;
			if (mobileFriendListItem == null)
			{
				throw new ArgumentException("given item is not MobileFriendListItem: " + item);
			}
			if (this.m_freelist == null)
			{
				this.m_freelist = new List<MobileFriendListItem>();
			}
			if (mobileFriendListItem.IsHeader)
			{
				mobileFriendListItem.gameObject.SetActive(false);
			}
			else if (this.m_freelist.Count >= 20)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
			else
			{
				this.m_freelist.Add(mobileFriendListItem);
				mobileFriendListItem.gameObject.SetActive(false);
			}
			if (!this.m_acquiredItems.Remove(mobileFriendListItem))
			{
				global::Log.All.Print("VirtualizedFriendsListBehavior.ReleaseItem item not found in m_acquiredItems: {0}", new object[]
				{
					mobileFriendListItem
				});
			}
			mobileFriendListItem.Unselected();
		}

		// Token: 0x0600D7DF RID: 55263 RVA: 0x003ECD74 File Offset: 0x003EAF74
		public ITouchListItem AcquireItem(int index)
		{
			if (this.m_acquiredItems.Count >= this.MaxAcquiredItems)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Bug in ILongListBehavior? there are too many acquired items! index=",
					index,
					" max=",
					this.MaxAcquiredItems,
					" maxVisible=",
					this.MaxVisibleItems,
					" minBuffer=",
					this.MinBuffer,
					" acquiredItems.Count=",
					this.m_acquiredItems.Count,
					" hasCollapsedHeaders=",
					this.HasCollapsedHeaders.ToString()
				}));
			}
			if (index < 0 || index >= this.m_friendList.m_allItems.Count)
			{
				throw new IndexOutOfRangeException(string.Format("Invalid index, {0} has {1} elements.", DebugUtils.GetHierarchyPathAndType(this.m_friendList, '.'), this.m_friendList.m_allItems.Count));
			}
			FriendListFrame.FriendListItem item = this.m_friendList.m_allItems[index];
			MobileFriendListItem.TypeFlags itemMainType = item.ItemMainType;
			Type frameType = item.GetFrameType();
			if (this.m_freelist != null && !item.IsHeader)
			{
				int num = this.m_freelist.FindLastIndex(delegate(MobileFriendListItem m)
				{
					if (!item.IsHeader)
					{
						return m.GetComponent(frameType) != null;
					}
					return m.IsHeader;
				});
				if (num >= 0 && this.m_freelist[num] == null)
				{
					for (int i = 0; i < this.m_freelist.Count; i++)
					{
						if (this.m_freelist[i] == null)
						{
							this.m_freelist.RemoveAt(i);
							i--;
						}
					}
					num = this.m_freelist.FindLastIndex(delegate(MobileFriendListItem m)
					{
						if (!item.IsHeader)
						{
							return m.GetComponent(frameType) != null;
						}
						return m.IsHeader;
					});
				}
				if (num >= 0)
				{
					MobileFriendListItem mobileFriendListItem = this.m_freelist[num];
					this.m_freelist.RemoveAt(num);
					mobileFriendListItem.gameObject.SetActive(true);
					if (itemMainType > MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
					{
						if (itemMainType <= MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
						{
							if (itemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
							{
								goto IL_25B;
							}
							if (itemMainType != MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
							{
								goto IL_3B0;
							}
						}
						else
						{
							if (itemMainType == MobileFriendListItem.TypeFlags.Request)
							{
								FriendListRequestFrame component = mobileFriendListItem.GetComponent<FriendListRequestFrame>();
								component.gameObject.SetActive(true);
								component.SetInvite(item.GetInvite());
								this.m_friendList.FinishCreateVisualItem<FriendListRequestFrame>(component, itemMainType, this.m_friendList.FindHeader(itemMainType), component.gameObject);
								component.UpdateInvite();
								goto IL_3E8;
							}
							if (itemMainType != MobileFriendListItem.TypeFlags.FoundFiresideGathering)
							{
								goto IL_3B0;
							}
						}
						FriendListFSGFrame component2 = mobileFriendListItem.GetComponent<FriendListFSGFrame>();
						component2.InitFrame(item.GetFSGConfig());
						this.m_friendList.FinishCreateVisualItem<FriendListFSGFrame>(component2, itemMainType, null, component2.gameObject);
						goto IL_3E8;
					}
					if (itemMainType != MobileFriendListItem.TypeFlags.Friend && itemMainType != MobileFriendListItem.TypeFlags.NearbyPlayer)
					{
						if (itemMainType != MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
						{
							goto IL_3B0;
						}
						FriendListItemFooter component3 = mobileFriendListItem.GetComponent<FriendListItemFooter>();
						component3.Text = item.GetText();
						this.m_friendList.FinishCreateVisualItem<FriendListItemFooter>(component3, itemMainType, null, component3.gameObject);
						goto IL_3E8;
					}
					IL_25B:
					FriendListFriendFrame component4 = mobileFriendListItem.GetComponent<FriendListFriendFrame>();
					component4.gameObject.SetActive(true);
					BnetPlayer player = null;
					bool flag = false;
					if (itemMainType != MobileFriendListItem.TypeFlags.Friend)
					{
						if (itemMainType != MobileFriendListItem.TypeFlags.NearbyPlayer)
						{
							if (itemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
							{
								player = item.GetFiresideGatheringPlayer();
								flag = true;
							}
						}
						else
						{
							player = item.GetNearbyPlayer();
						}
					}
					else
					{
						player = item.GetFriend();
					}
					component4.Initialize(player, flag, false);
					FriendListItemHeader parent = flag ? null : this.m_friendList.FindHeader(itemMainType);
					this.m_friendList.FinishCreateVisualItem<FriendListFriendFrame>(component4, itemMainType, parent, component4.gameObject);
					goto IL_3E8;
					IL_3B0:
					throw new NotImplementedException(string.Concat(new object[]
					{
						"VirtualizedFriendsListBehavior.AcquireItem[reuse] frameType=",
						frameType.FullName,
						" itemType=",
						itemMainType
					}));
					IL_3E8:
					this.m_acquiredItems.Add(mobileFriendListItem);
					return mobileFriendListItem;
				}
			}
			MobileFriendListItem mobileFriendListItem2;
			if (itemMainType <= MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
			{
				if (itemMainType <= MobileFriendListItem.TypeFlags.Friend)
				{
					if (itemMainType == MobileFriendListItem.TypeFlags.Header)
					{
						mobileFriendListItem2 = this.m_friendList.FindHeader(item.SubType).GetComponent<MobileFriendListItem>();
						goto IL_553;
					}
					if (itemMainType == MobileFriendListItem.TypeFlags.Friend)
					{
						BnetPlayer friend = item.GetFriend();
						mobileFriendListItem2 = this.m_friendList.CreateFriendFrame(friend);
						goto IL_553;
					}
				}
				else
				{
					if (itemMainType == MobileFriendListItem.TypeFlags.NearbyPlayer)
					{
						mobileFriendListItem2 = this.m_friendList.CreateNearbyPlayerFrame(item.GetNearbyPlayer());
						goto IL_553;
					}
					if (itemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
					{
						mobileFriendListItem2 = this.m_friendList.CreateFSGFooter(item.GetText());
						goto IL_553;
					}
				}
			}
			else if (itemMainType <= MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
			{
				if (itemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
				{
					mobileFriendListItem2 = this.m_friendList.CreateFSGPlayerFrame(item.GetFiresideGatheringPlayer());
					goto IL_553;
				}
				if (itemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
				{
					mobileFriendListItem2 = this.m_friendList.CreateFSGFrame(item.GetFSGConfig(), MobileFriendListItem.TypeFlags.CurrentFiresideGathering);
					goto IL_553;
				}
			}
			else
			{
				if (itemMainType == MobileFriendListItem.TypeFlags.Request)
				{
					mobileFriendListItem2 = this.m_friendList.CreateRequestFrame(item.GetInvite());
					goto IL_553;
				}
				if (itemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
				{
					mobileFriendListItem2 = this.m_friendList.CreateFSGFrame(item.GetFSGConfig(), MobileFriendListItem.TypeFlags.FoundFiresideGathering);
					goto IL_553;
				}
			}
			throw new NotImplementedException("VirtualizedFriendsListBehavior.AcquireItem[new] type=" + frameType.FullName);
			IL_553:
			this.m_acquiredItems.Add(mobileFriendListItem2);
			return mobileFriendListItem2;
		}

		// Token: 0x0600D7E0 RID: 55264 RVA: 0x003ED2E4 File Offset: 0x003EB4E4
		private void InitializeBoundsByTypeArray()
		{
			Array values = Enum.GetValues(typeof(MobileFriendListItem.TypeFlags));
			this.m_boundsByType = new Bounds[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				MobileFriendListItem.TypeFlags itemType = (MobileFriendListItem.TypeFlags)values.GetValue(i);
				Component prefab = this.GetPrefab(itemType);
				int boundsByTypeIndex = this.GetBoundsByTypeIndex(itemType);
				this.m_boundsByType[boundsByTypeIndex] = ((prefab == null) ? default(Bounds) : FriendListFrame.VirtualizedFriendsListBehavior.GetPrefabBounds(prefab.gameObject));
			}
		}

		// Token: 0x0600D7E1 RID: 55265 RVA: 0x003ED370 File Offset: 0x003EB570
		private int GetBoundsByTypeIndex(MobileFriendListItem.TypeFlags itemType)
		{
			if (itemType <= MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
			{
				switch (itemType)
				{
				case MobileFriendListItem.TypeFlags.Header:
					return 0;
				case MobileFriendListItem.TypeFlags.Friend:
					return 4;
				case MobileFriendListItem.TypeFlags.Friend | MobileFriendListItem.TypeFlags.Header:
					break;
				case MobileFriendListItem.TypeFlags.CurrentGame:
					return 3;
				default:
					if (itemType == MobileFriendListItem.TypeFlags.NearbyPlayer)
					{
						return 2;
					}
					if (itemType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
					{
						return 8;
					}
					break;
				}
			}
			else if (itemType <= MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
			{
				if (itemType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
				{
					return 6;
				}
				if (itemType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
				{
					return 7;
				}
			}
			else
			{
				if (itemType == MobileFriendListItem.TypeFlags.Request)
				{
					return 1;
				}
				if (itemType == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
				{
					return 5;
				}
			}
			throw new Exception(string.Concat(new object[]
			{
				"Unknown ItemType: ",
				itemType,
				" (",
				(int)itemType,
				")"
			}));
		}

		// Token: 0x0600D7E2 RID: 55266 RVA: 0x003ED414 File Offset: 0x003EB614
		private Component GetPrefab(MobileFriendListItem.TypeFlags itemType)
		{
			if (itemType > MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
			{
				if (itemType <= MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
				{
					if (itemType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
					{
						goto IL_81;
					}
					if (itemType != MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
					{
						goto IL_B4;
					}
				}
				else
				{
					if (itemType == MobileFriendListItem.TypeFlags.Request)
					{
						return this.m_friendList.prefabs.requestItem;
					}
					if (itemType != MobileFriendListItem.TypeFlags.FoundFiresideGathering)
					{
						goto IL_B4;
					}
				}
				return this.m_friendList.prefabs.fsgItem;
			}
			switch (itemType)
			{
			case MobileFriendListItem.TypeFlags.Header:
				return this.m_friendList.prefabs.headerItem;
			case MobileFriendListItem.TypeFlags.Friend:
			case MobileFriendListItem.TypeFlags.CurrentGame:
				break;
			case MobileFriendListItem.TypeFlags.Friend | MobileFriendListItem.TypeFlags.Header:
				goto IL_B4;
			default:
				if (itemType == MobileFriendListItem.TypeFlags.NearbyPlayer)
				{
					return this.m_friendList.prefabs.friendItem;
				}
				if (itemType != MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
				{
					goto IL_B4;
				}
				return this.m_friendList.prefabs.footerItem;
			}
			IL_81:
			return this.m_friendList.prefabs.friendItem;
			IL_B4:
			throw new Exception(string.Concat(new object[]
			{
				"Unknown ItemType: ",
				itemType,
				" (",
				(int)itemType,
				")"
			}));
		}

		// Token: 0x0400A722 RID: 42786
		private FriendListFrame m_friendList;

		// Token: 0x0400A723 RID: 42787
		private int m_cachedMaxVisibleItems = -1;

		// Token: 0x0400A724 RID: 42788
		private const int MAX_FREELIST_ITEMS = 20;

		// Token: 0x0400A725 RID: 42789
		private List<MobileFriendListItem> m_freelist;

		// Token: 0x0400A726 RID: 42790
		private HashSet<MobileFriendListItem> m_acquiredItems = new HashSet<MobileFriendListItem>();

		// Token: 0x0400A727 RID: 42791
		private Bounds[] m_boundsByType;
	}
}
