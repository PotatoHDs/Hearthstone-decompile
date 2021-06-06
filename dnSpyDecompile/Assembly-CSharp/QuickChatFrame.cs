using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class QuickChatFrame : MonoBehaviour
{
	// Token: 0x06000A6A RID: 2666 RVA: 0x0003D91C File Offset: 0x0003BB1C
	private void Awake()
	{
		this.InitRecentPlayers();
		if (!this.InitReceiver())
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		BnetWhisperMgr.Get().AddWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		this.InitTransform();
		this.InitInputBlocker();
		this.InitLastMessage();
		this.InitChatLogFrame();
		this.InitInput();
		this.ShowInput(true);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0003D996 File Offset: 0x0003BB96
	private void Start()
	{
		this.InitRecentPlayerDropdown();
		if (ChatMgr.Get().IsChatLogFrameShown())
		{
			this.ShowChatLogFrame(true);
		}
		this.UpdateReceiver();
		ChatMgr.Get().OnChatReceiverChanged(this.m_receiver);
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
	private void OnDestroy()
	{
		BnetWhisperMgr.Get().RemoveWhisperListener(new BnetWhisperMgr.WhisperCallback(this.OnWhisper));
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, false);
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0003DA1B File Offset: 0x0003BC1B
	public ChatLogFrame GetChatLogFrame()
	{
		return this.m_chatLogFrame;
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0003DA23 File Offset: 0x0003BC23
	public BnetPlayer GetReceiver()
	{
		return this.m_receiver;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0003DA2B File Offset: 0x0003BC2B
	public void SetReceiver(BnetPlayer player)
	{
		if (this.m_receiver == player)
		{
			return;
		}
		this.m_receiver = player;
		this.UpdateReceiver();
		this.m_recentPlayerDropdown.setSelection(player);
		ChatMgr.Get().OnChatReceiverChanged(player);
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0003DA5B File Offset: 0x0003BC5B
	public void UpdateLayout()
	{
		if (this.m_chatLogFrame != null)
		{
			this.m_chatLogFrame.UpdateLayout();
		}
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0003DA76 File Offset: 0x0003BC76
	private void InitRecentPlayers()
	{
		this.UpdateRecentPlayers();
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0003DA80 File Offset: 0x0003BC80
	private void UpdateRecentPlayers()
	{
		this.m_recentPlayers.Clear();
		List<BnetPlayer> recentWhisperPlayers = ChatMgr.Get().GetRecentWhisperPlayers();
		for (int i = 0; i < recentWhisperPlayers.Count; i++)
		{
			BnetPlayer item = recentWhisperPlayers[i];
			this.m_recentPlayers.Add(item);
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0003DAC8 File Offset: 0x0003BCC8
	private bool InitReceiver()
	{
		this.m_receiver = null;
		if (this.m_recentPlayers.Count == 0)
		{
			string message;
			if (BnetFriendMgr.Get().GetOnlineFriendCount() == 0)
			{
				message = GameStrings.Get("GLOBAL_CHAT_NO_FRIENDS_ONLINE");
			}
			else
			{
				message = GameStrings.Get("GLOBAL_CHAT_NO_RECENT_CONVERSATIONS");
			}
			UIStatus.Get().AddError(message, -1f);
			return false;
		}
		this.m_receiver = this.m_recentPlayers[0];
		return true;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0003DB32 File Offset: 0x0003BD32
	private void OnWhisper(BnetWhisper whisper, object userData)
	{
		if (this.m_receiver == null)
		{
			return;
		}
		if (!WhisperUtil.IsSpeaker(this.m_receiver, whisper))
		{
			return;
		}
		this.UpdateReceiver();
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0003DB54 File Offset: 0x0003BD54
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(this.m_receiver);
		if (bnetPlayerChange == null)
		{
			return;
		}
		BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
		BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
		if (oldPlayer != null && oldPlayer.IsOnline() == newPlayer.IsOnline())
		{
			return;
		}
		this.UpdateReceiver();
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0003DB98 File Offset: 0x0003BD98
	private BnetWhisper FindLastWhisperFromReceiver()
	{
		List<BnetWhisper> whispersWithPlayer = BnetWhisperMgr.Get().GetWhispersWithPlayer(this.m_receiver);
		if (whispersWithPlayer == null)
		{
			return null;
		}
		for (int i = whispersWithPlayer.Count - 1; i >= 0; i--)
		{
			BnetWhisper bnetWhisper = whispersWithPlayer[i];
			if (WhisperUtil.IsSpeaker(this.m_receiver, bnetWhisper))
			{
				return bnetWhisper;
			}
		}
		return null;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0003DBE8 File Offset: 0x0003BDE8
	private void InitTransform()
	{
		base.transform.parent = BaseUI.Get().transform;
		this.DefaultChatTransform();
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			this.TransformChatForKeyboard();
		}
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0003DC38 File Offset: 0x0003BE38
	private void InitLastMessage()
	{
		this.m_initialLastMessageTextHeight = this.m_LastMessageText.GetTextWorldSpaceBounds().size.y;
		this.m_initialLastMessageShadowScaleZ = this.m_LastMessageShadow.transform.localScale.z;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0003DC80 File Offset: 0x0003BE80
	private void InitInputBlocker()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		float worldOffset = this.m_Bones.m_InputBlocker.position.z - base.transform.position.z;
		GameObject gameObject = CameraUtils.CreateInputBlocker(camera, "QuickChatInputBlocker", this, worldOffset);
		this.m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerReleased));
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void OnInputBlockerReleased(UIEvent e)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0003DD03 File Offset: 0x0003BF03
	private void InitChatLogFrame()
	{
		this.m_ChatLogButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnChatLogButtonReleased));
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0003DD1E File Offset: 0x0003BF1E
	private void OnChatLogButtonReleased(UIEvent e)
	{
		if (ChatMgr.Get().IsChatLogFrameShown())
		{
			this.HideChatLogFrame();
		}
		else
		{
			this.ShowChatLogFrame(false);
		}
		this.UpdateReceiver();
		UniversalInputManager.Get().FocusTextInput(base.gameObject);
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0003DD54 File Offset: 0x0003BF54
	private void ShowChatLogFrame(bool onStart = false)
	{
		this.m_chatLogFrame = UnityEngine.Object.Instantiate<ChatLogFrame>(this.m_Prefabs.m_ChatLogFrame);
		bool flag = base.transform.localScale == BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.localScale;
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((((UniversalInputManager.Get().IsTouchMode() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible()) && flag) || flag)
		{
			this.DefaultChatTransform();
		}
		this.m_chatLogFrame.transform.parent = base.transform;
		this.m_chatLogFrame.transform.position = this.m_Bones.m_ChatLog.position;
		if ((((UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible()) && flag) || flag)
		{
			this.TransformChatForKeyboard();
		}
		GameObject obj = onStart ? base.gameObject : this.m_chatLogFrame.gameObject;
		base.StartCoroutine(this.ShowChatLogFrameWhenReady(obj));
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x0003DE4F File Offset: 0x0003C04F
	private IEnumerator ShowChatLogFrameWhenReady(GameObject obj)
	{
		while (this.m_chatLogFrame == null || this.m_chatLogFrame.IsWaitingOnMedal)
		{
			if (this.m_chatLogFrame == null)
			{
				yield break;
			}
			yield return null;
		}
		ChatMgr.Get().OnChatLogFrameShown();
		yield break;
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x0003DE5E File Offset: 0x0003C05E
	private void HideChatLogFrame()
	{
		UnityEngine.Object.Destroy(this.m_chatLogFrame.gameObject);
		this.m_chatLogFrame = null;
		ChatMgr.Get().OnChatLogFrameHidden();
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0003DE84 File Offset: 0x0003C084
	private void InitRecentPlayerDropdown()
	{
		this.m_recentPlayerDropdown = UnityEngine.Object.Instantiate<DropdownControl>(this.m_Prefabs.m_Dropdown);
		this.m_recentPlayerDropdown.transform.parent = base.transform;
		this.m_recentPlayerDropdown.transform.position = this.m_Bones.m_RecentPlayerDropdown.position;
		this.m_recentPlayerDropdown.setItemTextCallback(new DropdownControl.itemTextCallback(this.OnRecentPlayerDropdownText));
		this.m_recentPlayerDropdown.setItemChosenCallback(new DropdownControl.itemChosenCallback(this.OnRecentPlayerDropdownItemChosen));
		this.UpdateRecentPlayerDropdown();
		this.m_recentPlayerDropdown.setSelection(this.m_receiver);
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0003DF24 File Offset: 0x0003C124
	private void UpdateRecentPlayerDropdown()
	{
		this.m_recentPlayerDropdown.clearItems();
		for (int i = 0; i < this.m_recentPlayers.Count; i++)
		{
			this.m_recentPlayerDropdown.addItem(this.m_recentPlayers[i]);
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0003DF69 File Offset: 0x0003C169
	private string OnRecentPlayerDropdownText(object val)
	{
		return FriendUtils.GetUniqueName((BnetPlayer)val);
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0003DF78 File Offset: 0x0003C178
	private void OnRecentPlayerDropdownItemChosen(object selection, object prevSelection)
	{
		BnetPlayer receiver = (BnetPlayer)selection;
		this.SetReceiver(receiver);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0003DF93 File Offset: 0x0003C193
	private void UpdateReceiver()
	{
		this.UpdateLastMessage();
		if (this.m_chatLogFrame != null)
		{
			this.m_chatLogFrame.Receiver = this.m_receiver;
		}
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0003DFBC File Offset: 0x0003C1BC
	private void UpdateLastMessage()
	{
		if (this.m_chatLogFrame != null)
		{
			this.HideLastMessage();
			return;
		}
		BnetWhisper bnetWhisper = this.FindLastWhisperFromReceiver();
		if (bnetWhisper == null)
		{
			this.HideLastMessage();
			return;
		}
		this.m_LastMessageText.gameObject.SetActive(true);
		this.m_LastMessageText.Text = ChatUtils.GetMessage(bnetWhisper);
		TransformUtil.SetPoint(this.m_LastMessageText, Anchor.BOTTOM_LEFT, this.m_Bones.m_LastMessage, Anchor.TOP_LEFT);
		this.m_ReceiverNameText.gameObject.SetActive(true);
		if (this.m_receiver.IsOnline())
		{
			this.m_ReceiverNameText.TextColor = GameColors.PLAYER_NAME_ONLINE;
		}
		else
		{
			this.m_ReceiverNameText.TextColor = GameColors.PLAYER_NAME_OFFLINE;
		}
		this.m_ReceiverNameText.Text = FriendUtils.GetUniqueName(this.m_receiver);
		TransformUtil.SetPoint(this.m_ReceiverNameText, Anchor.BOTTOM_LEFT, this.m_LastMessageText, Anchor.TOP_LEFT);
		this.m_LastMessageShadow.SetActive(true);
		Bounds textWorldSpaceBounds = this.m_LastMessageText.GetTextWorldSpaceBounds();
		Bounds textWorldSpaceBounds2 = this.m_ReceiverNameText.GetTextWorldSpaceBounds();
		float num = Mathf.Max(textWorldSpaceBounds.max.y, textWorldSpaceBounds2.max.y);
		float num2 = Mathf.Min(textWorldSpaceBounds.min.y, textWorldSpaceBounds2.min.y);
		float z = (num - num2) * this.m_initialLastMessageShadowScaleZ / this.m_initialLastMessageTextHeight;
		TransformUtil.SetLocalScaleZ(this.m_LastMessageShadow, z);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0003E110 File Offset: 0x0003C310
	private void HideLastMessage()
	{
		this.m_ReceiverNameText.gameObject.SetActive(false);
		this.m_LastMessageText.gameObject.SetActive(false);
		this.m_LastMessageShadow.SetActive(false);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0003E140 File Offset: 0x0003C340
	private void CyclePrevReceiver()
	{
		int num = this.m_recentPlayers.FindIndex((BnetPlayer currReceiver) => this.m_receiver == currReceiver);
		BnetPlayer receiver;
		if (num == 0)
		{
			receiver = this.m_recentPlayers[this.m_recentPlayers.Count - 1];
		}
		else
		{
			receiver = this.m_recentPlayers[num - 1];
		}
		this.SetReceiver(receiver);
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0003E19C File Offset: 0x0003C39C
	private void CycleNextReceiver()
	{
		int num = this.m_recentPlayers.FindIndex((BnetPlayer currReceiver) => this.m_receiver == currReceiver);
		BnetPlayer receiver;
		if (num == this.m_recentPlayers.Count - 1)
		{
			receiver = this.m_recentPlayers[0];
		}
		else
		{
			receiver = this.m_recentPlayers[num + 1];
		}
		this.SetReceiver(receiver);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
	private void InitInput()
	{
		FontDefinition fontDef = FontTable.Get().GetFontDef(this.m_InputFont);
		if (fontDef == null)
		{
			this.m_localizedInputFont = this.m_InputFont;
			return;
		}
		this.m_localizedInputFont = fontDef.m_Font;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0003E238 File Offset: 0x0003C438
	private void ShowInput(bool fromAwake)
	{
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		Rect rect = CameraUtils.CreateGUIViewportRect(bnetCamera, this.m_Bones.m_InputTopLeft, this.m_Bones.m_InputBottomRight);
		if (Localization.GetLocale() == Locale.thTH)
		{
			Vector3 vector = bnetCamera.WorldToViewportPoint(this.m_Bones.m_InputTopLeft.position);
			Vector3 vector2 = bnetCamera.WorldToViewportPoint(this.m_Bones.m_InputBottomRight.position);
			float num = (vector.y - vector2.y) * 0.1f;
			vector = new Vector3(vector.x, vector.y - num, vector.z);
			vector2 = new Vector3(vector2.x, vector2.y + num, vector2.z);
			rect = new Rect(vector.x, 1f - vector.y, vector2.x - vector.x, vector.y - vector2.y);
		}
		string pendingMessage = ChatMgr.Get().GetPendingMessage(this.m_receiver.GetAccountId());
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_rect = rect,
			m_preprocessCallback = new UniversalInputManager.TextInputPreprocessCallback(this.OnInputPreprocess),
			m_completedCallback = new UniversalInputManager.TextInputCompletedCallback(this.OnInputComplete),
			m_canceledCallback = new UniversalInputManager.TextInputCanceledCallback(this.OnInputCanceled),
			m_updatedCallback = new UniversalInputManager.TextInputUpdatedCallback(this.OnInputChanged),
			m_font = this.m_localizedInputFont,
			m_maxCharacters = 512,
			m_touchScreenKeyboardHideInput = true,
			m_showVirtualKeyboard = fromAwake,
			m_hideVirtualKeyboardOnComplete = fromAwake,
			m_text = pendingMessage
		};
		UniversalInputManager.Get().UseTextInput(parms, false);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0003E3F4 File Offset: 0x0003C5F4
	private bool OnInputPreprocess(Event e)
	{
		if (this.m_recentPlayers.Count < 2)
		{
			return false;
		}
		if (e.type != EventType.KeyDown)
		{
			return false;
		}
		KeyCode keyCode = e.keyCode;
		bool flag = (e.modifiers & EventModifiers.Shift) > EventModifiers.None;
		if (keyCode == KeyCode.UpArrow || (keyCode == KeyCode.Tab && flag))
		{
			this.CyclePrevReceiver();
			return true;
		}
		if (keyCode == KeyCode.DownArrow || keyCode == KeyCode.Tab)
		{
			this.CycleNextReceiver();
			return true;
		}
		return false;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0003E45E File Offset: 0x0003C65E
	private void OnInputChanged(string input)
	{
		ChatMgr.Get().SetPendingMessage(this.m_receiver.GetAccountId(), input);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0003E478 File Offset: 0x0003C678
	private void OnInputComplete(string input)
	{
		if (!string.IsNullOrEmpty(input))
		{
			BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
			if (!BnetWhisperMgr.Get().SendWhisper(this.m_receiver, input))
			{
				if (ChatMgr.Get().IsChatLogFrameShown())
				{
					this.m_chatLogFrame.m_chatLog.OnWhisperFailed();
				}
				else if (!this.m_receiver.IsOnline())
				{
					string message = GameStrings.Format("GLOBAL_CHAT_RECEIVER_OFFLINE", new object[]
					{
						this.m_receiver.GetBestName()
					});
					UIStatus.Get().AddError(message, -1f);
				}
				else if (myPlayer.IsAppearingOffline())
				{
					string message2 = GameStrings.Get("GLOBAL_CHAT_SENDER_APPEAR_OFFLINE");
					UIStatus.Get().AddError(message2, -1f);
				}
				ChatMgr.Get().AddRecentWhisperPlayerToTop(this.m_receiver);
			}
		}
		ChatMgr.Get().SetPendingMessage(this.m_receiver.GetAccountId(), null);
		if (ChatMgr.Get().IsChatLogFrameShown())
		{
			this.ShowInput(false);
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0003E578 File Offset: 0x0003C778
	private void DefaultChatTransform()
	{
		base.transform.position = BaseUI.Get().m_Bones.m_QuickChat.position;
		base.transform.localScale = BaseUI.Get().m_Bones.m_QuickChat.localScale;
		if (this.m_chatLogFrame != null)
		{
			this.m_chatLogFrame.UpdateLayout();
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0003E5DC File Offset: 0x0003C7DC
	private void TransformChatForKeyboard()
	{
		base.transform.position = BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.position;
		base.transform.localScale = BaseUI.Get().m_Bones.m_QuickChatVirtualKeyboard.localScale;
		this.m_Prefabs.m_Dropdown.transform.localScale = new Vector3(50f, 50f, 50f);
		if (this.m_chatLogFrame != null)
		{
			this.m_chatLogFrame.UpdateLayout();
		}
	}

	// Token: 0x040006AB RID: 1707
	public QuickChatFrameBones m_Bones;

	// Token: 0x040006AC RID: 1708
	public QuickChatFramePrefabs m_Prefabs;

	// Token: 0x040006AD RID: 1709
	public GameObject m_Background;

	// Token: 0x040006AE RID: 1710
	public UberText m_ReceiverNameText;

	// Token: 0x040006AF RID: 1711
	public UberText m_LastMessageText;

	// Token: 0x040006B0 RID: 1712
	public GameObject m_LastMessageShadow;

	// Token: 0x040006B1 RID: 1713
	public PegUIElement m_ChatLogButton;

	// Token: 0x040006B2 RID: 1714
	public Font m_InputFont;

	// Token: 0x040006B3 RID: 1715
	private DropdownControl m_recentPlayerDropdown;

	// Token: 0x040006B4 RID: 1716
	private ChatLogFrame m_chatLogFrame;

	// Token: 0x040006B5 RID: 1717
	private PegUIElement m_inputBlocker;

	// Token: 0x040006B6 RID: 1718
	private List<BnetPlayer> m_recentPlayers = new List<BnetPlayer>();

	// Token: 0x040006B7 RID: 1719
	private BnetPlayer m_receiver;

	// Token: 0x040006B8 RID: 1720
	private float m_initialLastMessageTextHeight;

	// Token: 0x040006B9 RID: 1721
	private float m_initialLastMessageShadowScaleZ;

	// Token: 0x040006BA RID: 1722
	private Font m_localizedInputFont;

	// Token: 0x040006BB RID: 1723
	private global::Map<Renderer, int> m_chatLogOriginalLayers = new global::Map<Renderer, int>();
}
