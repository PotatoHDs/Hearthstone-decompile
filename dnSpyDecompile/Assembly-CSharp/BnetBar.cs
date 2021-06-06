using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Blizzard.T5.Core;
using Hearthstone;
using Networking;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000073 RID: 115
[CustomEditClass]
public class BnetBar : MonoBehaviour
{
	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000669 RID: 1641 RVA: 0x00025214 File Offset: 0x00023414
	// (remove) Token: 0x0600066A RID: 1642 RVA: 0x0002524C File Offset: 0x0002344C
	public event Action OnMenuOpened;

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600066B RID: 1643 RVA: 0x00025281 File Offset: 0x00023481
	[CustomEditField(Hide = true)]
	public float HorizontalMargin
	{
		get
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				return TransformUtil.GetAspectRatioDependentValue(this.HorizontalMarginMinAspectRatio, this.HorizontalMarginWideAspectRatio, this.HorizontalMarginExtraWideAspectRatio);
			}
			return 0f;
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x000252AC File Offset: 0x000234AC
	private void Awake()
	{
		BnetBar.s_instance = this;
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_menuButton.transform.localScale *= 2f;
			this.m_friendButton.transform.localScale *= 2f;
		}
		else
		{
			this.m_connectionIndicator.gameObject.SetActive(false);
		}
		this.m_initialWidth = base.GetComponent<Renderer>().bounds.size.x;
		this.m_initialFriendButtonScaleX = this.m_friendButton.transform.localScale.x;
		this.m_initialMenuButtonScaleX = this.m_menuButton.transform.localScale.x;
		this.m_menuButton.StateChanged = new Action(this.UpdateLayout);
		this.m_menuButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnMenuButtonReleased));
		this.m_friendButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFriendButtonReleased));
		this.UpdateButtonEnableState();
		this.m_batteryLevel.gameObject.SetActive(false);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_batteryLevel = this.m_batteryLevelPhone;
			this.m_currentTime.gameObject.SetActive(false);
		}
		this.m_menuButton.SetPhoneStatusBarState(0);
		this.m_friendButton.gameObject.SetActive(false);
		this.ToggleActive(false);
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00025420 File Offset: 0x00023620
	private void OnDestroy()
	{
		if (!HearthstoneApplication.IsHearthstoneClosing)
		{
			SpectatorManager.Get().OnInviteReceived -= this.SpectatorManager_OnInviteReceived;
			SpectatorManager.Get().OnSpectatorToMyGame -= this.SpectatorManager_OnSpectatorToMyGame;
			SpectatorManager.Get().OnSpectatorModeChanged -= this.SpectatorManager_OnSpectatorModeChanged;
			if (Network.Get() != null)
			{
				Network.Get().RemoveNetHandler(GetServerTimeResponse.PacketID.ID, new Network.NetHandler(this.OnRequestGetServerTimeResponse));
			}
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.WillReset -= this.WillReset;
			}
		}
		BnetBar.s_instance = null;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x000254C8 File Offset: 0x000236C8
	private void Start()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		}
		if (SpectatorManager.Get() != null)
		{
			SpectatorManager.Get().OnInviteReceived += this.SpectatorManager_OnInviteReceived;
			SpectatorManager.Get().OnSpectatorToMyGame += this.SpectatorManager_OnSpectatorToMyGame;
			SpectatorManager.Get().OnSpectatorModeChanged += this.SpectatorManager_OnSpectatorModeChanged;
		}
		if (Network.Get() != null)
		{
			Network.Get().RegisterNetHandler(GetServerTimeResponse.PacketID.ID, new Network.NetHandler(this.OnRequestGetServerTimeResponse), null);
		}
		HearthstoneApplication.Get().WillReset += this.WillReset;
		this.m_friendButton.gameObject.SetActive(false);
		if (this.m_friendButton != null)
		{
			this.m_friendButton.ShowPendingInvitesIcon(this.m_hasUnacknowledgedPendingInvites);
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x000255AC File Offset: 0x000237AC
	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup - this.m_lastClockUpdate > 1f)
		{
			this.m_lastClockUpdate = realtimeSinceStartup;
			bool flag = !HearthstoneApplication.IsPublic() && Vars.Key("Application.ShowServerTime").GetBool(true);
			DateTime dateTime;
			if (flag && this.TryGetServerTime(out dateTime))
			{
				this.m_currentTime.Text = GameStrings.Format("GLOBAL_CURRENT_TIME_AND_DATE_DEV", new object[]
				{
					GameStrings.Format("GLOBAL_CURRENT_TIME", new object[]
					{
						DateTime.Now
					}),
					GameStrings.Format("GLOBAL_CURRENT_DATE", new object[]
					{
						dateTime
					}),
					GameStrings.Format("GLOBAL_CURRENT_TIME", new object[]
					{
						dateTime
					})
				});
			}
			else if (Localization.GetLocale() == Locale.enGB)
			{
				this.m_currentTime.Text = string.Format("{0:HH:mm}", DateTime.Now);
			}
			else
			{
				this.m_currentTime.Text = GameStrings.Format("GLOBAL_CURRENT_TIME", new object[]
				{
					DateTime.Now
				});
			}
			if (Localization.GetLocale() == Locale.koKR)
			{
				this.m_currentTime.Text = this.m_currentTime.Text.Replace("AM", GameStrings.Format("GLOBAL_CURRENT_TIME_AM", Array.Empty<object>())).Replace("PM", GameStrings.Format("GLOBAL_CURRENT_TIME_PM", Array.Empty<object>()));
			}
			if (flag != this.m_lastClockUpdateCanShowServerTime)
			{
				this.UpdateLayout();
				this.m_lastClockUpdateCanShowServerTime = flag;
			}
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0002572C File Offset: 0x0002392C
	public static BnetBar Get()
	{
		return BnetBar.s_instance;
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00025733 File Offset: 0x00023933
	public void OnLoggedIn()
	{
		if (Network.ShouldBeConnectedToAurora())
		{
			this.m_friendButton.gameObject.SetActive(true);
		}
		Network.Get().GetServerTimeRequest();
		this.m_isLoggedIn = true;
		this.ToggleActive(true);
		this.Update();
		this.UpdateLayout();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00025774 File Offset: 0x00023974
	public void UpdateLayout()
	{
		if (!this.m_isLoggedIn)
		{
			return;
		}
		float num = 0.5f;
		Bounds nearClipBounds = CameraUtils.GetNearClipBounds(PegUI.Get().orthographicUICam);
		nearClipBounds.min += new Vector3(this.HorizontalMargin, 0f, 0f);
		nearClipBounds.max -= new Vector3(this.HorizontalMargin, 0f, 0f);
		float num2 = (nearClipBounds.size.x + num) / this.m_initialWidth;
		TransformUtil.SetLocalPosX(base.gameObject, nearClipBounds.min.x - base.transform.parent.localPosition.x - num);
		TransformUtil.SetLocalScaleX(base.gameObject, num2);
		float num3 = -0.03f * num2;
		if (GeneralUtils.IsDevelopmentBuildTextVisible())
		{
			num3 -= CameraUtils.ScreenToWorldDist(PegUI.Get().orthographicUICam, 115f);
		}
		float y = 1f * base.transform.localScale.y;
		bool flag = true;
		if (!DemoMgr.Get().IsHubEscMenuEnabled(SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY))
		{
			flag = false;
		}
		this.m_menuButton.gameObject.SetActive(flag);
		TransformUtil.SetLocalScaleX(this.m_menuButton, this.m_initialMenuButtonScaleX / num2);
		TransformUtil.SetPoint(this.m_menuButton, Anchor.RIGHT, base.gameObject, Anchor.RIGHT, new Vector3(num3, y, 0f) - BnetBar.LAYOUT_OFFSET_PADDING);
		if (UniversalInputManager.UsePhoneUI)
		{
			TransformUtil.SetPoint(this.m_menuButton, Anchor.RIGHT, base.gameObject, Anchor.RIGHT, new Vector3(num3, y, 0f));
			TransformUtil.SetLocalPosX(this.m_menuButton, this.m_menuButton.transform.localPosition.x + 0.05f);
			TransformUtil.SetLocalPosY(this.m_menuButton, BnetBar.LAYOUT_TOPLEFT_START_POINT.y);
			this.m_batteryLevel.gameObject.SetActive(true);
			int phoneStatusBarState = 1 + (this.m_connectionIndicator.IsVisible() ? 1 : 0);
			this.m_menuButton.SetPhoneStatusBarState(phoneStatusBarState);
			TransformUtil.SetLocalScaleX(this.m_currencyFrameContainer, 2f / num2);
			TransformUtil.SetLocalScaleY(this.m_currencyFrameContainer, 0.4f);
			if (flag)
			{
				this.PositionCurrencyFrame(this.m_batteryLevel.gameObject, new Vector3(this.m_menuButton.GetCurrencyFrameOffsetX(), BnetBar.LAYOUT_OFFSET_CURRENCY.Value.y, 0f));
			}
			else
			{
				this.PositionCurrencyFrame(this.m_batteryLevel.gameObject, new Vector3(100f, BnetBar.LAYOUT_OFFSET_CURRENCY.Value.y, 0f));
			}
		}
		else
		{
			TransformUtil.SetPoint(this.m_menuButton, Anchor.RIGHT, base.gameObject, Anchor.RIGHT, new Vector3(num3, y, 0f));
			TransformUtil.SetLocalScaleX(this.m_currencyFrameContainer, 1f / num2);
			this.PositionCurrencyFrame(this.m_menuButton.gameObject, new Vector3(this.m_menuButton.GetCurrencyFrameOffsetX(), BnetBar.LAYOUT_OFFSET_CURRENCY.Value.y, 0f));
		}
		bool flag2 = this.m_spectatorCountPanel != null && this.m_spectatorCountPanel.activeInHierarchy && SpectatorManager.Get().IsBeingSpectated();
		bool flag3 = !flag2 && this.m_spectatorModeIndicator != null && this.ShouldShowSpectatorModeIndicator;
		if (UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
		{
			flag2 = false;
			flag3 = false;
		}
		this.ShowSpectatorModeIndicator(flag3);
		GameObject gameObject = null;
		bool flag4 = false;
		if (this.m_friendButton.gameObject.activeInHierarchy)
		{
			TransformUtil.SetLocalScaleX(this.m_friendButton, this.m_initialFriendButtonScaleX / num2);
			BnetBar.LayoutWidget_BottomLeft_Relative(this.m_friendButton.transform, ref gameObject, default(Vector3));
			TransformUtil.SetLocalScaleX(this.m_socialToastBone, 1f / num2);
			if (UniversalInputManager.UsePhoneUI)
			{
				gameObject = null;
				TransformUtil.SetLocalPosY(this.m_friendButton, BnetBar.LAYOUT_TOPLEFT_START_POINT.y);
				if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null)
				{
					TransformUtil.SetPosZ(this.m_friendButton, ChatMgr.Get().FriendListFrame.transform.position.z + 1f);
					flag4 = true;
				}
			}
		}
		if (flag2)
		{
			BnetBar.LayoutWidget_BottomLeft_Relative(this.m_spectatorCountPanel.transform, ref gameObject, BnetBar.LAYOUT_OFFSET_SPECTATOR_WIDGET);
			if (flag4)
			{
				TransformUtil.SetPosZ(this.m_spectatorCountPanel, ChatMgr.Get().FriendListFrame.transform.position.z + 1f);
			}
		}
		if (flag3)
		{
			BnetBar.LayoutWidget_BottomLeft_Relative(this.m_spectatorModeIndicator.transform, ref gameObject, BnetBar.LAYOUT_OFFSET_SPECTATOR_WIDGET);
			TransformUtil.SetLocalScaleX(this.m_spectatorModeIndicator, this.m_initialSpectatorModeIndicatorScaleX / num2);
			if (flag4)
			{
				TransformUtil.SetPosZ(this.m_spectatorModeIndicator, ChatMgr.Get().FriendListFrame.transform.position.z + 1f);
			}
		}
		GameObject previousWidget = gameObject;
		Vector3 vector;
		Vector3 zero;
		if (gameObject == null)
		{
			vector = BnetBar.LAYOUT_BOTTOMLEFT_START_POINT;
			zero = Vector3.zero;
		}
		else if (gameObject == this.m_friendButton.gameObject)
		{
			vector = new Vector3(15f, 0f, -1f);
			zero = new Vector3(22f, BnetBar.LAYOUT_OFFSET_CURRENCY.Value.y, 0f);
		}
		else
		{
			vector = new Vector3(7f, 0f, -1f);
			zero = new Vector3(14f, BnetBar.LAYOUT_OFFSET_CURRENCY.Value.y, 0f);
		}
		vector += BnetBar.LAYOUT_OFFSET_PADDING;
		if (UniversalInputManager.UsePhoneUI)
		{
			previousWidget = this.m_friendButton.gameObject;
			if (!this.m_friendButton.gameObject.activeInHierarchy)
			{
				previousWidget = null;
				vector = BnetBar.LAYOUT_TOPLEFT_START_POINT;
			}
		}
		vector.z = -1f;
		BnetBar.LayoutWidget_LeftAligned_SetExactOffset(this.m_socialToastBone.transform, previousWidget, vector);
		TransformUtil.SetLocalScaleX(this.m_currentTime, 1f / num2);
		BnetBar.LayoutWidget_BottomLeft_Relative(this.m_currentTime.transform, ref gameObject, zero);
		if (PlatformSettings.IsTablet && this.m_isLoggedIn)
		{
			this.m_batteryLevel.gameObject.SetActive(true);
			BnetBar.LayoutWidget_LeftAligned_SetExactOffset(this.m_batteryLevel.transform, this.m_currentTime.gameObject, new Vector3(12f, 5f, 0f));
		}
		this.UpdateLoginTooltip();
		if (this.m_isInitting)
		{
			foreach (CurrencyFrame currencyFrame in this.m_currencyFrames)
			{
				currencyFrame.DeactivateCurrencyFrame();
			}
			this.m_isInitting = false;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.UpdateForPhone();
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000673 RID: 1651 RVA: 0x00025E60 File Offset: 0x00024060
	public IEnumerable<CurrencyFrame> CurrencyFrames
	{
		get
		{
			return this.m_currencyFrames;
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00025E68 File Offset: 0x00024068
	public void RegisterCurrencyFrame(CurrencyFrame currencyFrame)
	{
		this.m_currencyFrames.Add(currencyFrame);
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00025E76 File Offset: 0x00024076
	public CurrencyFrame GetCurrencyFrame(int slotIdx = 0)
	{
		if (slotIdx < 0 || slotIdx >= this.m_currencyFrames.Count<CurrencyFrame>())
		{
			return null;
		}
		return this.m_currencyFrames[slotIdx];
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00025E98 File Offset: 0x00024098
	public int GetCurrencyFrameIndex(CurrencyFrame frame)
	{
		return this.m_currencyFrames.IndexOf(frame);
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00025EA8 File Offset: 0x000240A8
	public void RefreshCurrency()
	{
		foreach (CurrencyFrame currencyFrame in this.m_currencyFrames)
		{
			currencyFrame.RefreshContents();
		}
		this.UpdateLayout();
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00025F00 File Offset: 0x00024100
	public void HideCurrencyTemporarily()
	{
		foreach (CurrencyFrame currencyFrame in this.m_currencyFrames)
		{
			currencyFrame.HideTemporarily();
		}
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x00025F50 File Offset: 0x00024150
	public bool IsCurrencyFrameActive()
	{
		using (List<CurrencyFrame>.Enumerator enumerator = this.m_currencyFrames.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.isActiveAndEnabled)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00025FAC File Offset: 0x000241AC
	public bool TryGetServerTime(out DateTime serverTime)
	{
		if (this.m_serverClientOffsetInSec != 1.7976931348623157E+308)
		{
			serverTime = DateTime.UtcNow.AddSeconds(this.m_serverClientOffsetInSec).ToLocalTime();
			return true;
		}
		serverTime = DateTime.UtcNow;
		return false;
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00025FF9 File Offset: 0x000241F9
	private static void LayoutWidget_LeftAligned_SetExactOffset(Transform transform, GameObject previousWidget, Vector3 exactOffset)
	{
		if (!transform.gameObject.activeInHierarchy)
		{
			return;
		}
		if (previousWidget == null)
		{
			TransformUtil.SetPoint(transform, Anchor.LEFT, BnetBar.Get().gameObject, Anchor.LEFT, exactOffset);
			return;
		}
		TransformUtil.SetPoint(transform, Anchor.LEFT, previousWidget, Anchor.RIGHT, exactOffset);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x00026030 File Offset: 0x00024230
	private static void LayoutWidget_BottomLeft_Relative(Transform transform, ref GameObject previousWidget, Vector3 offsetFromPrevious = default(Vector3))
	{
		if (!transform.gameObject.activeInHierarchy)
		{
			return;
		}
		if (previousWidget == null)
		{
			BnetBar.LayoutWidget_LeftAligned_SetExactOffset(transform, previousWidget, BnetBar.LAYOUT_BOTTOMLEFT_START_POINT);
			previousWidget = transform.gameObject;
			return;
		}
		BnetBar.LayoutWidget_LeftAligned_SetExactOffset(transform, previousWidget, offsetFromPrevious + BnetBar.LAYOUT_OFFSET_PADDING);
		previousWidget = transform.gameObject;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002608C File Offset: 0x0002428C
	private void PositionCurrencyFrame(GameObject parent, Vector3 offset)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (CurrencyFrame currencyFrame in this.m_currencyFrames)
		{
			GameObject tooltipObject = currencyFrame.GetTooltipObject();
			if (tooltipObject != null)
			{
				tooltipObject.SetActive(false);
				list.Add(tooltipObject);
			}
		}
		TransformUtil.SetPoint(this.m_currencyFrameContainer, Anchor.RIGHT, parent, Anchor.LEFT, offset, false);
		list.ForEach(delegate(GameObject obj)
		{
			obj.SetActive(true);
		});
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x00026130 File Offset: 0x00024330
	public bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(BackButton.backKey) || InputCollection.GetKeyUp(KeyCode.Escape))
		{
			return this.HandleEscapeKey();
		}
		ChatMgr chatMgr = ChatMgr.Get();
		return chatMgr != null && chatMgr.HandleKeyboardInput();
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x00026174 File Offset: 0x00024374
	public void ToggleGameMenu()
	{
		if (this.m_gameMenu == null)
		{
			this.LoadGameMenu();
			return;
		}
		if (this.m_gameMenu.GameMenuIsShown())
		{
			this.HideGameMenu();
			return;
		}
		this.m_gameMenu.GameMenuShow();
		if (this.OnMenuOpened != null)
		{
			this.OnMenuOpened();
		}
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x000261C2 File Offset: 0x000243C2
	public bool IsActive()
	{
		return base.gameObject.activeSelf;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x000261CF File Offset: 0x000243CF
	public void ToggleActive(bool active)
	{
		base.gameObject.SetActive(active);
		if (active)
		{
			this.UpdateLayout();
		}
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x000261E6 File Offset: 0x000243E6
	public void PermanentlyDisableButtons()
	{
		this.m_buttonsDisabledPermanently = true;
		this.UpdateButtonEnableState();
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x000261F5 File Offset: 0x000243F5
	public void ForceEnableButtons()
	{
		this.m_buttonsDisabledPermanently = false;
		this.m_buttonsDisabledByDialog.Clear();
		this.m_buttonsDisabledByRefCount = 0;
		this.UpdateButtonEnableState();
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00026216 File Offset: 0x00024416
	public void DisableButtonsByDialog(DialogBase dialog)
	{
		dialog.AddHiddenOrDestroyedListener(new DialogBase.HideCallback(this.OnDisablingDialogHiddenOrDestroyed));
		this.m_buttonsDisabledByDialog.Add(dialog);
		this.UpdateButtonEnableState();
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0002623D File Offset: 0x0002443D
	public void RequestDisableButtons()
	{
		this.m_buttonsDisabledByRefCount++;
		this.UpdateButtonEnableState();
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00026253 File Offset: 0x00024453
	public void CancelRequestToDisableButtons()
	{
		this.m_buttonsDisabledByRefCount--;
		this.UpdateButtonEnableState();
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00026269 File Offset: 0x00024469
	private void OnDisablingDialogHiddenOrDestroyed(DialogBase dialog, object userData)
	{
		this.m_buttonsDisabledByDialog.Remove(dialog);
		this.UpdateButtonEnableState();
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0002627E File Offset: 0x0002447E
	public bool AreButtonsEnabled()
	{
		return this.m_buttonsEnabled;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00026286 File Offset: 0x00024486
	public void HideGameMenu()
	{
		if (this.m_gameMenu != null && this.m_gameMenu.GameMenuIsShown())
		{
			this.m_gameMenu.GameMenuHide();
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x000262A8 File Offset: 0x000244A8
	public void HideOptionsMenu()
	{
		if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
		{
			OptionsMenu.Get().Hide(true);
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x000262CE File Offset: 0x000244CE
	public void HideMiscellaneousMenu()
	{
		if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
		{
			MiscellaneousMenu.Get().Hide();
		}
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x000262F3 File Offset: 0x000244F3
	public bool IsGameMenuShown()
	{
		return this.m_gameMenu != null && this.m_gameMenu.GameMenuIsShown();
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0002630C File Offset: 0x0002450C
	public void UpdateForPhone()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool active = mode == SceneMgr.Mode.HUB || mode == SceneMgr.Mode.LOGIN || mode == SceneMgr.Mode.GAMEPLAY || this.IsCurrencyFrameActive();
		this.m_menuButton.gameObject.SetActive(active);
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0002634C File Offset: 0x0002454C
	public void UpdateLoginTooltip()
	{
		if (Network.ShouldBeConnectedToAurora() || this.m_suppressLoginTooltip || !SceneMgr.Get().IsInGame() || !GameMgr.Get().IsTutorial() || GameMgr.Get().IsSpectator() || DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM)
		{
			this.DestroyLoginTooltip();
			return;
		}
		if (this.m_loginTooltip == null)
		{
			this.m_loginTooltip = AssetLoader.Get().InstantiatePrefab("LoginPointer.prefab:e26056ee6e4b89c45899d54bc9497bb0", AssetLoadingOptions.None);
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_loginTooltip.transform.localScale = new Vector3(60f, 60f, 60f);
			}
			else
			{
				this.m_loginTooltip.transform.localScale = new Vector3(40f, 40f, 40f);
			}
			TransformUtil.SetEulerAngleX(this.m_loginTooltip, 270f);
			SceneUtils.SetLayer(this.m_loginTooltip, GameLayer.BattleNet);
			this.m_loginTooltip.transform.parent = base.transform;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			TransformUtil.SetPoint(this.m_loginTooltip, Anchor.RIGHT, this.m_batteryLevel.gameObject, Anchor.LEFT, new Vector3(-32f, 0f, 0f));
			return;
		}
		TransformUtil.SetPoint(this.m_loginTooltip, Anchor.RIGHT, this.m_menuButton, Anchor.LEFT, new Vector3(-80f, 0f, 0f));
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x000264C0 File Offset: 0x000246C0
	private void DestroyLoginTooltip()
	{
		if (this.m_loginTooltip != null)
		{
			UnityEngine.Object.Destroy(this.m_loginTooltip);
			this.m_loginTooltip = null;
		}
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x000264E2 File Offset: 0x000246E2
	public void SuppressLoginTooltip(bool val)
	{
		this.m_suppressLoginTooltip = val;
		this.UpdateLayout();
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x000264F1 File Offset: 0x000246F1
	private void ShowFriendList()
	{
		ChatMgr.Get().ShowFriendsList();
		this.m_hasUnacknowledgedPendingInvites = false;
		this.m_friendButton.ShowPendingInvitesIcon(this.m_hasUnacknowledgedPendingInvites);
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00026515 File Offset: 0x00024715
	public void HideFriendList()
	{
		ChatMgr.Get().CloseChatUI(true);
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00026522 File Offset: 0x00024722
	private void OnFriendButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		this.ToggleFriendListShowing();
		this.UpdateLayout();
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x00026544 File Offset: 0x00024744
	private void ToggleFriendListShowing()
	{
		if (ChatMgr.Get().IsFriendListShowing())
		{
			this.HideFriendList();
		}
		else
		{
			this.ShowFriendList();
		}
		this.m_friendButton.HideTooltip();
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0002656C File Offset: 0x0002476C
	private void UpdateButtonEnableState()
	{
		if (this.m_buttonsDisabledPermanently || this.m_buttonsDisabledByRefCount > 0 || this.m_buttonsDisabledByDialog.Any<DialogBase>())
		{
			this.m_buttonsEnabled = false;
			this.m_menuButton.SetEnabled(false, false);
			this.m_friendButton.SetEnabled(false, false);
			this.HideMiscellaneousMenu();
			this.HideOptionsMenu();
			this.HideGameMenu();
			this.HideFriendList();
			return;
		}
		this.m_buttonsEnabled = true;
		this.m_menuButton.SetEnabled(true, false);
		this.m_friendButton.SetEnabled(true, false);
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x000265F4 File Offset: 0x000247F4
	private void WillReset()
	{
		if (this.m_gameMenu != null)
		{
			if (this.m_gameMenu.GameMenuIsShown())
			{
				this.m_gameMenu.GameMenuHide();
			}
			UnityEngine.Object.DestroyImmediate(this.m_gameMenu.GameMenuGetGameObject());
			this.m_gameMenu = null;
		}
		this.DestroyLoginTooltip();
		this.ToggleActive(false);
		this.m_isLoggedIn = false;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0002664C File Offset: 0x0002484C
	private bool HandleEscapeKey()
	{
		if (this.m_gameMenu != null && this.m_gameMenu.GameMenuIsShown())
		{
			this.m_gameMenu.GameMenuHide();
			return true;
		}
		if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
		{
			OptionsMenu.Get().Hide(true);
			return true;
		}
		if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
		{
			MiscellaneousMenu.Get().Hide();
			return true;
		}
		if (QuestLog.Get() != null && QuestLog.Get().IsShown())
		{
			QuestLog.Get().Hide();
			return true;
		}
		if (GeneralStore.Get() != null && GeneralStore.Get().IsShown())
		{
			GeneralStore.Get().Close();
			return true;
		}
		ChatMgr chatMgr = ChatMgr.Get();
		if (chatMgr != null && chatMgr.HandleKeyboardInput())
		{
			return true;
		}
		if (CraftingTray.Get() != null && CraftingTray.Get().IsShown())
		{
			CraftingTray.Get().Hide();
			return true;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return true;
		}
		if (mode == SceneMgr.Mode.LOGIN)
		{
			return true;
		}
		if (mode == SceneMgr.Mode.STARTUP)
		{
			return true;
		}
		if (!DemoMgr.Get().IsHubEscMenuEnabled(mode == SceneMgr.Mode.GAMEPLAY))
		{
			return true;
		}
		this.ToggleGameMenu();
		return true;
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x00026785 File Offset: 0x00024985
	private void OnMenuButtonReleased(UIEvent e)
	{
		if (!GameMgr.Get().IsSpectator() && GameState.Get() != null && GameState.Get().IsInTargetMode())
		{
			return;
		}
		this.ToggleGameMenu();
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x000267AD File Offset: 0x000249AD
	private void LoadGameMenu()
	{
		if (!this.m_gameMenuLoading && this.m_gameMenu == null)
		{
			this.m_gameMenuLoading = true;
			AssetLoader.Get().InstantiatePrefab("GameMenu.prefab:dc76cbcfb64a34d7e93755df33db2f80", new PrefabCallback<GameObject>(this.ShowGameMenu), null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x000267E9 File Offset: 0x000249E9
	private void ShowGameMenu(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_gameMenu = go.GetComponent<GameMenu>();
		this.m_gameMenu.GameMenuShow();
		if (this.OnMenuOpened != null)
		{
			this.OnMenuOpened();
		}
		this.m_gameMenuLoading = false;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0002681C File Offset: 0x00024A1C
	private void UpdateForDemoMode()
	{
		if (!DemoMgr.Get().IsExpoDemo())
		{
			return;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool flag = true;
		bool flag2;
		switch (DemoMgr.Get().GetMode())
		{
		case DemoMode.PAX_EAST_2013:
		case DemoMode.BLIZZCON_2013:
		case DemoMode.BLIZZCON_2015:
		case DemoMode.BLIZZCON_2017_ADVENTURE:
		case DemoMode.BLIZZCON_2017_BRAWL:
			flag2 = (mode == SceneMgr.Mode.GAMEPLAY);
			flag = false;
			this.m_currencyFrameContainer.SetActive(false);
			goto IL_A9;
		case DemoMode.BLIZZCON_2014:
			flag2 = (flag = (mode != SceneMgr.Mode.FRIENDLY));
			goto IL_A9;
		case DemoMode.BLIZZ_MUSEUM:
			flag = (flag2 = false);
			goto IL_A9;
		case DemoMode.ANNOUNCEMENT_5_0:
			flag = true;
			flag2 = true;
			goto IL_A9;
		case DemoMode.BLIZZCON_2016:
		case DemoMode.BLIZZCON_2018_BRAWL:
		case DemoMode.BLIZZCON_2019_BATTLEGROUNDS:
			flag2 = (mode == SceneMgr.Mode.GAMEPLAY);
			flag = (mode == SceneMgr.Mode.HUB);
			goto IL_A9;
		}
		flag2 = (mode != SceneMgr.Mode.FRIENDLY && mode != SceneMgr.Mode.TOURNAMENT);
		IL_A9:
		if ((mode == SceneMgr.Mode.GAMEPLAY || mode - SceneMgr.Mode.TOURNAMENT <= 1) && DemoMgr.Get().GetMode() != DemoMode.ANNOUNCEMENT_5_0)
		{
			flag = false;
		}
		if (!flag2)
		{
			this.m_menuButton.gameObject.SetActive(false);
		}
		if (!flag)
		{
			this.m_friendButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00026914 File Offset: 0x00024B14
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return;
		}
		this.m_suppressLoginTooltip = false;
		this.RefreshCurrency();
		bool flag = mode != SceneMgr.Mode.INVALID && mode != SceneMgr.Mode.FATAL_ERROR;
		if (flag)
		{
			if (SpectatorManager.Get().IsInSpectatorMode())
			{
				this.SpectatorManager_OnSpectatorModeChanged(OnlineEventType.ADDED, null);
			}
		}
		else if (this.m_spectatorModeIndicator != null && this.m_spectatorModeIndicator.activeSelf)
		{
			this.m_spectatorModeIndicator.SetActive(false);
		}
		if (flag && this.m_spectatorCountPanel != null)
		{
			bool active = SpectatorManager.Get().IsBeingSpectated();
			if (UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
			{
				active = false;
			}
			this.m_spectatorCountPanel.SetActive(active);
		}
		this.UpdateForDemoMode();
		this.UpdateLayout();
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x000269D8 File Offset: 0x00024BD8
	private void SpectatorManager_OnInviteReceived(OnlineEventType evt, BnetPlayer inviter)
	{
		if (ChatMgr.Get().IsFriendListShowing() || !SpectatorManager.Get().HasAnyReceivedInvites())
		{
			this.m_hasUnacknowledgedPendingInvites = false;
		}
		else
		{
			this.m_hasUnacknowledgedPendingInvites = (this.m_hasUnacknowledgedPendingInvites || evt == OnlineEventType.ADDED);
		}
		if (this.m_friendButton != null)
		{
			this.m_friendButton.ShowPendingInvitesIcon(this.m_hasUnacknowledgedPendingInvites);
		}
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00026A3C File Offset: 0x00024C3C
	private void SpectatorManager_OnSpectatorToMyGame(OnlineEventType evt, BnetPlayer spectator)
	{
		int countSpectatingMe = SpectatorManager.Get().GetCountSpectatingMe();
		if (countSpectatingMe <= 0)
		{
			if (this.m_spectatorCountPanel == null)
			{
				return;
			}
		}
		else if (this.m_spectatorCountPanel == null)
		{
			string spectatorCountPrefabPath = this.m_spectatorCountPrefabPath;
			AssetLoader.Get().InstantiatePrefab(spectatorCountPrefabPath, delegate(AssetReference n, GameObject go, object d)
			{
				BnetBar bnetBar = BnetBar.Get();
				if (bnetBar == null)
				{
					return;
				}
				if (bnetBar.m_spectatorCountPanel != null)
				{
					UnityEngine.Object.Destroy(go);
				}
				else
				{
					bnetBar.m_spectatorCountPanel = go;
					bnetBar.m_spectatorCountPanel.transform.parent = bnetBar.transform;
					PegUIElement component = go.GetComponent<PegUIElement>();
					if (component != null)
					{
						component.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(BnetBar.SpectatorCount_OnRollover));
						component.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(BnetBar.SpectatorCount_OnRollout));
					}
					Material material = bnetBar.m_spectatorCountPanel.transform.Find("BeingWatchedHighlight").gameObject.GetComponent<Renderer>().GetMaterial();
					Color color = material.color;
					color.a = 0f;
					material.color = color;
				}
				BnetBar.Get().SpectatorManager_OnSpectatorToMyGame(evt, spectator);
			}, null, AssetLoadingOptions.None);
			return;
		}
		this.m_spectatorCountPanel.transform.Find("UberText").GetComponent<UberText>().Text = countSpectatingMe.ToString();
		bool active = countSpectatingMe > 0;
		if (UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
		{
			active = false;
		}
		this.m_spectatorCountPanel.SetActive(active);
		this.UpdateLayout();
		GameObject gameObject = this.m_spectatorCountPanel.transform.Find("BeingWatchedHighlight").gameObject;
		iTween.Stop(gameObject, true);
		Action<object> action = delegate(object ud)
		{
			if (BnetBar.Get() == null)
			{
				return;
			}
			iTween.FadeTo(BnetBar.Get().m_spectatorCountPanel.transform.Find("BeingWatchedHighlight").gameObject, 0f, 0.5f);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"alpha",
			1f,
			"time",
			0.5f,
			"oncomplete",
			action
		});
		iTween.FadeTo(gameObject, args);
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00026BA0 File Offset: 0x00024DA0
	private static void SpectatorCount_OnRollover(UIEvent evt)
	{
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return;
		}
		string headline = GameStrings.Get("GLOBAL_SPECTATOR_COUNT_PANEL_HEADER");
		BnetGameAccountId[] spectatorPartyMembers = SpectatorManager.Get().GetSpectatorPartyMembers(true, false);
		string bodytext;
		if (spectatorPartyMembers.Length == 1)
		{
			string playerBestName = BnetUtils.GetPlayerBestName(spectatorPartyMembers[0]);
			bodytext = GameStrings.Format("GLOBAL_SPECTATOR_COUNT_PANEL_TEXT_ONE", new object[]
			{
				playerBestName
			});
		}
		else
		{
			string[] value = (from id in spectatorPartyMembers
			select BnetUtils.GetPlayerBestName(id)).ToArray<string>();
			bodytext = string.Join(", ", value);
		}
		bnetBar.m_spectatorCountTooltipZone.ShowSocialTooltip(bnetBar.m_spectatorCountPanel, headline, bodytext, 75f, GameLayer.BattleNetDialog, 0);
		bnetBar.m_spectatorCountTooltipZone.AnchorTooltipTo(bnetBar.m_spectatorCountPanel, Anchor.TOP_LEFT, Anchor.BOTTOM_LEFT, 0);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00026C68 File Offset: 0x00024E68
	private static void SpectatorCount_OnRollout(UIEvent evt)
	{
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return;
		}
		bnetBar.m_spectatorCountTooltipZone.HideTooltip();
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00026C90 File Offset: 0x00024E90
	private bool ShouldShowSpectatorModeIndicator
	{
		get
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = SpectatorManager.Get().IsInSpectatorMode();
			bool result = flag || flag2 || flag3;
			if (UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
			{
				result = false;
			}
			if (SpectatorManager.Get().IsBeingSpectated())
			{
				result = false;
			}
			return result;
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00026CE0 File Offset: 0x00024EE0
	private void ShowSpectatorModeIndicator(bool show)
	{
		if (this.m_spectatorModeIndicator != null)
		{
			this.m_spectatorModeIndicator.SetActive(show);
		}
		if (show)
		{
			UberText componentInChildren = this.m_spectatorModeIndicator.GetComponentInChildren<UberText>();
			if (componentInChildren != null && SpectatorManager.Get().IsInSpectatorMode())
			{
				componentInChildren.Text = GameStrings.Get("GLOBAL_SPECTATOR_MODE_INDICATOR_TEXT");
			}
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00026D3C File Offset: 0x00024F3C
	private void CheckSpectatorModeIndicator()
	{
		if (this.ShouldShowSpectatorModeIndicator && this.m_spectatorModeIndicator == null)
		{
			string spectatorModeIndicatorPrefab = this.m_spectatorModeIndicatorPrefab;
			AssetLoader.Get().InstantiatePrefab(spectatorModeIndicatorPrefab, delegate(AssetReference n, GameObject go, object d)
			{
				BnetBar bnetBar = BnetBar.Get();
				if (bnetBar == null || go == null)
				{
					return;
				}
				if (bnetBar.m_spectatorModeIndicator != null)
				{
					UnityEngine.Object.Destroy(go);
				}
				else
				{
					bnetBar.m_spectatorModeIndicator = go;
					bnetBar.m_spectatorModeIndicator.transform.parent = bnetBar.transform;
					TransformOverride component = go.GetComponent<TransformOverride>();
					if (component != null)
					{
						int bestScreenMatch = PlatformSettings.GetBestScreenMatch(component.m_screenCategory);
						this.m_initialSpectatorModeIndicatorScaleX = component.m_localScale[bestScreenMatch].x;
					}
				}
				BnetBar.Get().CheckSpectatorModeIndicator();
			}, null, AssetLoadingOptions.None);
			return;
		}
		if (this.m_spectatorModeIndicator == null)
		{
			return;
		}
		this.UpdateLayout();
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00026D9B File Offset: 0x00024F9B
	private void SpectatorManager_OnSpectatorModeChanged(OnlineEventType evt, BnetPlayer spectatee)
	{
		this.CheckSpectatorModeIndicator();
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00026DA4 File Offset: 0x00024FA4
	private void OnRequestGetServerTimeResponse()
	{
		ResponseWithRequest<GetServerTimeResponse, GetServerTimeRequest> serverTimeResponse = Network.Get().GetServerTimeResponse();
		ulong num = global::TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
		this.m_serverClientOffsetInSec = (double)(serverTimeResponse.Response.ServerUnixTime - (long)num);
	}

	// Token: 0x04000470 RID: 1136
	public UberText m_currentTime;

	// Token: 0x04000471 RID: 1137
	public BnetBarMenuButton m_menuButton;

	// Token: 0x04000472 RID: 1138
	public GameObject m_menuButtonMesh;

	// Token: 0x04000473 RID: 1139
	public BnetBarFriendButton m_friendButton;

	// Token: 0x04000474 RID: 1140
	public GameObject m_currencyFrameContainer;

	// Token: 0x04000475 RID: 1141
	public Flipbook m_batteryLevel;

	// Token: 0x04000476 RID: 1142
	public Flipbook m_batteryLevelPhone;

	// Token: 0x04000477 RID: 1143
	public GameObject m_socialToastBone;

	// Token: 0x04000478 RID: 1144
	public ConnectionIndicator m_connectionIndicator;

	// Token: 0x04000479 RID: 1145
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_spectatorCountPrefabPath;

	// Token: 0x0400047A RID: 1146
	public TooltipZone m_spectatorCountTooltipZone;

	// Token: 0x0400047B RID: 1147
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_spectatorModeIndicatorPrefab;

	// Token: 0x0400047C RID: 1148
	[Header("Phone Aspect Ratio")]
	public float HorizontalMarginMinAspectRatio;

	// Token: 0x0400047D RID: 1149
	public float HorizontalMarginWideAspectRatio;

	// Token: 0x0400047E RID: 1150
	public float HorizontalMarginExtraWideAspectRatio;

	// Token: 0x04000480 RID: 1152
	public static readonly int CameraDepth = 47;

	// Token: 0x04000481 RID: 1153
	private static BnetBar s_instance;

	// Token: 0x04000482 RID: 1154
	private float m_initialWidth;

	// Token: 0x04000483 RID: 1155
	private float m_initialFriendButtonScaleX;

	// Token: 0x04000484 RID: 1156
	private float m_initialMenuButtonScaleX;

	// Token: 0x04000485 RID: 1157
	private float m_initialSpectatorModeIndicatorScaleX;

	// Token: 0x04000486 RID: 1158
	private GameMenuInterface m_gameMenu;

	// Token: 0x04000487 RID: 1159
	private bool m_gameMenuLoading;

	// Token: 0x04000488 RID: 1160
	private bool m_isInitting = true;

	// Token: 0x04000489 RID: 1161
	private GameObject m_loginTooltip;

	// Token: 0x0400048A RID: 1162
	private bool m_hasUnacknowledgedPendingInvites;

	// Token: 0x0400048B RID: 1163
	private GameObject m_spectatorCountPanel;

	// Token: 0x0400048C RID: 1164
	private GameObject m_spectatorModeIndicator;

	// Token: 0x0400048D RID: 1165
	private bool m_isLoggedIn;

	// Token: 0x0400048E RID: 1166
	private bool m_buttonsEnabled;

	// Token: 0x0400048F RID: 1167
	private bool m_buttonsDisabledPermanently;

	// Token: 0x04000490 RID: 1168
	private int m_buttonsDisabledByRefCount;

	// Token: 0x04000491 RID: 1169
	private HashSet<DialogBase> m_buttonsDisabledByDialog = new HashSet<DialogBase>();

	// Token: 0x04000492 RID: 1170
	private bool m_suppressLoginTooltip;

	// Token: 0x04000493 RID: 1171
	private float m_lastClockUpdate;

	// Token: 0x04000494 RID: 1172
	private bool m_lastClockUpdateCanShowServerTime;

	// Token: 0x04000495 RID: 1173
	private double m_serverClientOffsetInSec;

	// Token: 0x04000496 RID: 1174
	private List<CurrencyFrame> m_currencyFrames = new List<CurrencyFrame>();

	// Token: 0x04000497 RID: 1175
	private static readonly Vector3 LAYOUT_TOPLEFT_START_POINT = new Vector3(6f, 190f, 0f);

	// Token: 0x04000498 RID: 1176
	private static readonly Vector3 LAYOUT_BOTTOMLEFT_START_POINT = new Vector3(6f, 5f, 0f);

	// Token: 0x04000499 RID: 1177
	private static readonly PlatformDependentValue<Vector3> LAYOUT_OFFSET_PADDING = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, 0f, 0f),
		Tablet = new Vector3(16f, 0f, 0f),
		MiniTablet = new Vector3(16f, 0f, 0f),
		Phone = new Vector3(16f, 0f, 0f)
	};

	// Token: 0x0400049A RID: 1178
	private static readonly PlatformDependentValue<Vector3> LAYOUT_OFFSET_CURRENCY = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, -1f, 0f),
		Phone = new Vector3(0f, 0f, 0f)
	};

	// Token: 0x0400049B RID: 1179
	private static readonly PlatformDependentValue<Vector3> LAYOUT_OFFSET_SPECTATOR_WIDGET = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(8f, 0f, 0f),
		Phone = new Vector3(8f, 1.3f, 0f)
	};
}
