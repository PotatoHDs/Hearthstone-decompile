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

[CustomEditClass]
public class BnetBar : MonoBehaviour
{
	public UberText m_currentTime;

	public BnetBarMenuButton m_menuButton;

	public GameObject m_menuButtonMesh;

	public BnetBarFriendButton m_friendButton;

	public GameObject m_currencyFrameContainer;

	public Flipbook m_batteryLevel;

	public Flipbook m_batteryLevelPhone;

	public GameObject m_socialToastBone;

	public ConnectionIndicator m_connectionIndicator;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_spectatorCountPrefabPath;

	public TooltipZone m_spectatorCountTooltipZone;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_spectatorModeIndicatorPrefab;

	[Header("Phone Aspect Ratio")]
	public float HorizontalMarginMinAspectRatio;

	public float HorizontalMarginWideAspectRatio;

	public float HorizontalMarginExtraWideAspectRatio;

	public static readonly int CameraDepth = 47;

	private static BnetBar s_instance;

	private float m_initialWidth;

	private float m_initialFriendButtonScaleX;

	private float m_initialMenuButtonScaleX;

	private float m_initialSpectatorModeIndicatorScaleX;

	private GameMenuInterface m_gameMenu;

	private bool m_gameMenuLoading;

	private bool m_isInitting = true;

	private GameObject m_loginTooltip;

	private bool m_hasUnacknowledgedPendingInvites;

	private GameObject m_spectatorCountPanel;

	private GameObject m_spectatorModeIndicator;

	private bool m_isLoggedIn;

	private bool m_buttonsEnabled;

	private bool m_buttonsDisabledPermanently;

	private int m_buttonsDisabledByRefCount;

	private HashSet<DialogBase> m_buttonsDisabledByDialog = new HashSet<DialogBase>();

	private bool m_suppressLoginTooltip;

	private float m_lastClockUpdate;

	private bool m_lastClockUpdateCanShowServerTime;

	private double m_serverClientOffsetInSec;

	private List<CurrencyFrame> m_currencyFrames = new List<CurrencyFrame>();

	private static readonly Vector3 LAYOUT_TOPLEFT_START_POINT = new Vector3(6f, 190f, 0f);

	private static readonly Vector3 LAYOUT_BOTTOMLEFT_START_POINT = new Vector3(6f, 5f, 0f);

	private static readonly PlatformDependentValue<Vector3> LAYOUT_OFFSET_PADDING = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, 0f, 0f),
		Tablet = new Vector3(16f, 0f, 0f),
		MiniTablet = new Vector3(16f, 0f, 0f),
		Phone = new Vector3(16f, 0f, 0f)
	};

	private static readonly PlatformDependentValue<Vector3> LAYOUT_OFFSET_CURRENCY = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, -1f, 0f),
		Phone = new Vector3(0f, 0f, 0f)
	};

	private static readonly PlatformDependentValue<Vector3> LAYOUT_OFFSET_SPECTATOR_WIDGET = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(8f, 0f, 0f),
		Phone = new Vector3(8f, 1.3f, 0f)
	};

	[CustomEditField(Hide = true)]
	public float HorizontalMargin
	{
		get
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				return TransformUtil.GetAspectRatioDependentValue(HorizontalMarginMinAspectRatio, HorizontalMarginWideAspectRatio, HorizontalMarginExtraWideAspectRatio);
			}
			return 0f;
		}
	}

	public IEnumerable<CurrencyFrame> CurrencyFrames => m_currencyFrames;

	private bool ShouldShowSpectatorModeIndicator
	{
		get
		{
			bool flag = false;
			bool flag2 = SpectatorManager.Get().IsInSpectatorMode();
			bool result = false || flag || flag2;
			if ((bool)UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
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

	public event Action OnMenuOpened;

	private void Awake()
	{
		s_instance = this;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_menuButton.transform.localScale *= 2f;
			m_friendButton.transform.localScale *= 2f;
		}
		else
		{
			m_connectionIndicator.gameObject.SetActive(value: false);
		}
		m_initialWidth = GetComponent<Renderer>().bounds.size.x;
		m_initialFriendButtonScaleX = m_friendButton.transform.localScale.x;
		m_initialMenuButtonScaleX = m_menuButton.transform.localScale.x;
		m_menuButton.StateChanged = UpdateLayout;
		m_menuButton.AddEventListener(UIEventType.RELEASE, OnMenuButtonReleased);
		m_friendButton.AddEventListener(UIEventType.RELEASE, OnFriendButtonReleased);
		UpdateButtonEnableState();
		m_batteryLevel.gameObject.SetActive(value: false);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_batteryLevel = m_batteryLevelPhone;
			m_currentTime.gameObject.SetActive(value: false);
		}
		m_menuButton.SetPhoneStatusBarState(0);
		m_friendButton.gameObject.SetActive(value: false);
		ToggleActive(active: false);
	}

	private void OnDestroy()
	{
		if (!HearthstoneApplication.IsHearthstoneClosing)
		{
			SpectatorManager.Get().OnInviteReceived -= SpectatorManager_OnInviteReceived;
			SpectatorManager.Get().OnSpectatorToMyGame -= SpectatorManager_OnSpectatorToMyGame;
			SpectatorManager.Get().OnSpectatorModeChanged -= SpectatorManager_OnSpectatorModeChanged;
			if (Network.Get() != null)
			{
				Network.Get().RemoveNetHandler(GetServerTimeResponse.PacketID.ID, OnRequestGetServerTimeResponse);
			}
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.WillReset -= WillReset;
			}
		}
		s_instance = null;
	}

	private void Start()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
		}
		if (SpectatorManager.Get() != null)
		{
			SpectatorManager.Get().OnInviteReceived += SpectatorManager_OnInviteReceived;
			SpectatorManager.Get().OnSpectatorToMyGame += SpectatorManager_OnSpectatorToMyGame;
			SpectatorManager.Get().OnSpectatorModeChanged += SpectatorManager_OnSpectatorModeChanged;
		}
		if (Network.Get() != null)
		{
			Network.Get().RegisterNetHandler(GetServerTimeResponse.PacketID.ID, OnRequestGetServerTimeResponse);
		}
		HearthstoneApplication.Get().WillReset += WillReset;
		m_friendButton.gameObject.SetActive(value: false);
		if (m_friendButton != null)
		{
			m_friendButton.ShowPendingInvitesIcon(m_hasUnacknowledgedPendingInvites);
		}
	}

	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup - m_lastClockUpdate > 1f)
		{
			m_lastClockUpdate = realtimeSinceStartup;
			bool flag = !HearthstoneApplication.IsPublic() && Vars.Key("Application.ShowServerTime").GetBool(def: true);
			if (flag && TryGetServerTime(out var serverTime))
			{
				m_currentTime.Text = GameStrings.Format("GLOBAL_CURRENT_TIME_AND_DATE_DEV", GameStrings.Format("GLOBAL_CURRENT_TIME", DateTime.Now), GameStrings.Format("GLOBAL_CURRENT_DATE", serverTime), GameStrings.Format("GLOBAL_CURRENT_TIME", serverTime));
			}
			else if (Localization.GetLocale() == Locale.enGB)
			{
				m_currentTime.Text = $"{DateTime.Now:HH:mm}";
			}
			else
			{
				m_currentTime.Text = GameStrings.Format("GLOBAL_CURRENT_TIME", DateTime.Now);
			}
			if (Localization.GetLocale() == Locale.koKR)
			{
				m_currentTime.Text = m_currentTime.Text.Replace("AM", GameStrings.Format("GLOBAL_CURRENT_TIME_AM")).Replace("PM", GameStrings.Format("GLOBAL_CURRENT_TIME_PM"));
			}
			if (flag != m_lastClockUpdateCanShowServerTime)
			{
				UpdateLayout();
				m_lastClockUpdateCanShowServerTime = flag;
			}
		}
	}

	public static BnetBar Get()
	{
		return s_instance;
	}

	public void OnLoggedIn()
	{
		if (Network.ShouldBeConnectedToAurora())
		{
			m_friendButton.gameObject.SetActive(value: true);
		}
		Network.Get().GetServerTimeRequest();
		m_isLoggedIn = true;
		ToggleActive(active: true);
		Update();
		UpdateLayout();
	}

	public void UpdateLayout()
	{
		if (!m_isLoggedIn)
		{
			return;
		}
		float num = 0.5f;
		Bounds nearClipBounds = CameraUtils.GetNearClipBounds(PegUI.Get().orthographicUICam);
		nearClipBounds.min += new Vector3(HorizontalMargin, 0f, 0f);
		nearClipBounds.max -= new Vector3(HorizontalMargin, 0f, 0f);
		float num2 = (nearClipBounds.size.x + num) / m_initialWidth;
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
		m_menuButton.gameObject.SetActive(flag);
		TransformUtil.SetLocalScaleX(m_menuButton, m_initialMenuButtonScaleX / num2);
		TransformUtil.SetPoint(m_menuButton, Anchor.RIGHT, base.gameObject, Anchor.RIGHT, new Vector3(num3, y, 0f) - LAYOUT_OFFSET_PADDING);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			TransformUtil.SetPoint(m_menuButton, Anchor.RIGHT, base.gameObject, Anchor.RIGHT, new Vector3(num3, y, 0f));
			TransformUtil.SetLocalPosX(m_menuButton, m_menuButton.transform.localPosition.x + 0.05f);
			TransformUtil.SetLocalPosY(m_menuButton, LAYOUT_TOPLEFT_START_POINT.y);
			m_batteryLevel.gameObject.SetActive(value: true);
			int phoneStatusBarState = 1 + (m_connectionIndicator.IsVisible() ? 1 : 0);
			m_menuButton.SetPhoneStatusBarState(phoneStatusBarState);
			TransformUtil.SetLocalScaleX(m_currencyFrameContainer, 2f / num2);
			TransformUtil.SetLocalScaleY(m_currencyFrameContainer, 0.4f);
			if (flag)
			{
				PositionCurrencyFrame(m_batteryLevel.gameObject, new Vector3(m_menuButton.GetCurrencyFrameOffsetX(), LAYOUT_OFFSET_CURRENCY.Value.y, 0f));
			}
			else
			{
				PositionCurrencyFrame(m_batteryLevel.gameObject, new Vector3(100f, LAYOUT_OFFSET_CURRENCY.Value.y, 0f));
			}
		}
		else
		{
			TransformUtil.SetPoint(m_menuButton, Anchor.RIGHT, base.gameObject, Anchor.RIGHT, new Vector3(num3, y, 0f));
			TransformUtil.SetLocalScaleX(m_currencyFrameContainer, 1f / num2);
			PositionCurrencyFrame(m_menuButton.gameObject, new Vector3(m_menuButton.GetCurrencyFrameOffsetX(), LAYOUT_OFFSET_CURRENCY.Value.y, 0f));
		}
		bool flag2 = m_spectatorCountPanel != null && m_spectatorCountPanel.activeInHierarchy && SpectatorManager.Get().IsBeingSpectated();
		bool flag3 = !flag2 && m_spectatorModeIndicator != null && ShouldShowSpectatorModeIndicator;
		if ((bool)UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
		{
			flag2 = false;
			flag3 = false;
		}
		ShowSpectatorModeIndicator(flag3);
		GameObject previousWidget = null;
		bool flag4 = false;
		if (m_friendButton.gameObject.activeInHierarchy)
		{
			TransformUtil.SetLocalScaleX(m_friendButton, m_initialFriendButtonScaleX / num2);
			LayoutWidget_BottomLeft_Relative(m_friendButton.transform, ref previousWidget);
			TransformUtil.SetLocalScaleX(m_socialToastBone, 1f / num2);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				previousWidget = null;
				TransformUtil.SetLocalPosY(m_friendButton, LAYOUT_TOPLEFT_START_POINT.y);
				if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null)
				{
					TransformUtil.SetPosZ(m_friendButton, ChatMgr.Get().FriendListFrame.transform.position.z + 1f);
					flag4 = true;
				}
			}
		}
		if (flag2)
		{
			LayoutWidget_BottomLeft_Relative(m_spectatorCountPanel.transform, ref previousWidget, LAYOUT_OFFSET_SPECTATOR_WIDGET);
			if (flag4)
			{
				TransformUtil.SetPosZ(m_spectatorCountPanel, ChatMgr.Get().FriendListFrame.transform.position.z + 1f);
			}
		}
		if (flag3)
		{
			LayoutWidget_BottomLeft_Relative(m_spectatorModeIndicator.transform, ref previousWidget, LAYOUT_OFFSET_SPECTATOR_WIDGET);
			TransformUtil.SetLocalScaleX(m_spectatorModeIndicator, m_initialSpectatorModeIndicatorScaleX / num2);
			if (flag4)
			{
				TransformUtil.SetPosZ(m_spectatorModeIndicator, ChatMgr.Get().FriendListFrame.transform.position.z + 1f);
			}
		}
		GameObject previousWidget2 = previousWidget;
		Vector3 exactOffset;
		Vector3 offsetFromPrevious;
		if (previousWidget == null)
		{
			exactOffset = LAYOUT_BOTTOMLEFT_START_POINT;
			offsetFromPrevious = Vector3.zero;
		}
		else if (previousWidget == m_friendButton.gameObject)
		{
			exactOffset = new Vector3(15f, 0f, -1f);
			offsetFromPrevious = new Vector3(22f, LAYOUT_OFFSET_CURRENCY.Value.y, 0f);
		}
		else
		{
			exactOffset = new Vector3(7f, 0f, -1f);
			offsetFromPrevious = new Vector3(14f, LAYOUT_OFFSET_CURRENCY.Value.y, 0f);
		}
		exactOffset += (Vector3)LAYOUT_OFFSET_PADDING;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			previousWidget2 = m_friendButton.gameObject;
			if (!m_friendButton.gameObject.activeInHierarchy)
			{
				previousWidget2 = null;
				exactOffset = LAYOUT_TOPLEFT_START_POINT;
			}
		}
		exactOffset.z = -1f;
		LayoutWidget_LeftAligned_SetExactOffset(m_socialToastBone.transform, previousWidget2, exactOffset);
		TransformUtil.SetLocalScaleX(m_currentTime, 1f / num2);
		LayoutWidget_BottomLeft_Relative(m_currentTime.transform, ref previousWidget, offsetFromPrevious);
		if (PlatformSettings.IsTablet && m_isLoggedIn)
		{
			m_batteryLevel.gameObject.SetActive(value: true);
			LayoutWidget_LeftAligned_SetExactOffset(m_batteryLevel.transform, m_currentTime.gameObject, new Vector3(12f, 5f, 0f));
		}
		UpdateLoginTooltip();
		if (m_isInitting)
		{
			foreach (CurrencyFrame currencyFrame in m_currencyFrames)
			{
				currencyFrame.DeactivateCurrencyFrame();
			}
			m_isInitting = false;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			UpdateForPhone();
		}
	}

	public void RegisterCurrencyFrame(CurrencyFrame currencyFrame)
	{
		m_currencyFrames.Add(currencyFrame);
	}

	public CurrencyFrame GetCurrencyFrame(int slotIdx = 0)
	{
		if (slotIdx < 0 || slotIdx >= m_currencyFrames.Count())
		{
			return null;
		}
		return m_currencyFrames[slotIdx];
	}

	public int GetCurrencyFrameIndex(CurrencyFrame frame)
	{
		return m_currencyFrames.IndexOf(frame);
	}

	public void RefreshCurrency()
	{
		foreach (CurrencyFrame currencyFrame in m_currencyFrames)
		{
			currencyFrame.RefreshContents();
		}
		UpdateLayout();
	}

	public void HideCurrencyTemporarily()
	{
		foreach (CurrencyFrame currencyFrame in m_currencyFrames)
		{
			currencyFrame.HideTemporarily();
		}
	}

	public bool IsCurrencyFrameActive()
	{
		foreach (CurrencyFrame currencyFrame in m_currencyFrames)
		{
			if (currencyFrame.isActiveAndEnabled)
			{
				return true;
			}
		}
		return false;
	}

	public bool TryGetServerTime(out DateTime serverTime)
	{
		if (m_serverClientOffsetInSec != double.MaxValue)
		{
			serverTime = DateTime.UtcNow.AddSeconds(m_serverClientOffsetInSec).ToLocalTime();
			return true;
		}
		serverTime = DateTime.UtcNow;
		return false;
	}

	private static void LayoutWidget_LeftAligned_SetExactOffset(Transform transform, GameObject previousWidget, Vector3 exactOffset)
	{
		if (transform.gameObject.activeInHierarchy)
		{
			if (previousWidget == null)
			{
				TransformUtil.SetPoint(transform, Anchor.LEFT, Get().gameObject, Anchor.LEFT, exactOffset);
			}
			else
			{
				TransformUtil.SetPoint(transform, Anchor.LEFT, previousWidget, Anchor.RIGHT, exactOffset);
			}
		}
	}

	private static void LayoutWidget_BottomLeft_Relative(Transform transform, ref GameObject previousWidget, Vector3 offsetFromPrevious = default(Vector3))
	{
		if (transform.gameObject.activeInHierarchy)
		{
			if (previousWidget == null)
			{
				LayoutWidget_LeftAligned_SetExactOffset(transform, previousWidget, LAYOUT_BOTTOMLEFT_START_POINT);
				previousWidget = transform.gameObject;
			}
			else
			{
				LayoutWidget_LeftAligned_SetExactOffset(transform, previousWidget, offsetFromPrevious + LAYOUT_OFFSET_PADDING);
				previousWidget = transform.gameObject;
			}
		}
	}

	private void PositionCurrencyFrame(GameObject parent, Vector3 offset)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (CurrencyFrame currencyFrame in m_currencyFrames)
		{
			GameObject tooltipObject = currencyFrame.GetTooltipObject();
			if (tooltipObject != null)
			{
				tooltipObject.SetActive(value: false);
				list.Add(tooltipObject);
			}
		}
		TransformUtil.SetPoint(m_currencyFrameContainer, Anchor.RIGHT, parent, Anchor.LEFT, offset, includeInactive: false);
		list.ForEach(delegate(GameObject obj)
		{
			obj.SetActive(value: true);
		});
	}

	public bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(BackButton.backKey) || InputCollection.GetKeyUp(KeyCode.Escape))
		{
			return HandleEscapeKey();
		}
		ChatMgr chatMgr = ChatMgr.Get();
		if (chatMgr != null && chatMgr.HandleKeyboardInput())
		{
			return true;
		}
		return false;
	}

	public void ToggleGameMenu()
	{
		if (m_gameMenu == null)
		{
			LoadGameMenu();
			return;
		}
		if (m_gameMenu.GameMenuIsShown())
		{
			HideGameMenu();
			return;
		}
		m_gameMenu.GameMenuShow();
		if (this.OnMenuOpened != null)
		{
			this.OnMenuOpened();
		}
	}

	public bool IsActive()
	{
		return base.gameObject.activeSelf;
	}

	public void ToggleActive(bool active)
	{
		base.gameObject.SetActive(active);
		if (active)
		{
			UpdateLayout();
		}
	}

	public void PermanentlyDisableButtons()
	{
		m_buttonsDisabledPermanently = true;
		UpdateButtonEnableState();
	}

	public void ForceEnableButtons()
	{
		m_buttonsDisabledPermanently = false;
		m_buttonsDisabledByDialog.Clear();
		m_buttonsDisabledByRefCount = 0;
		UpdateButtonEnableState();
	}

	public void DisableButtonsByDialog(DialogBase dialog)
	{
		dialog.AddHiddenOrDestroyedListener(OnDisablingDialogHiddenOrDestroyed);
		m_buttonsDisabledByDialog.Add(dialog);
		UpdateButtonEnableState();
	}

	public void RequestDisableButtons()
	{
		m_buttonsDisabledByRefCount++;
		UpdateButtonEnableState();
	}

	public void CancelRequestToDisableButtons()
	{
		m_buttonsDisabledByRefCount--;
		UpdateButtonEnableState();
	}

	private void OnDisablingDialogHiddenOrDestroyed(DialogBase dialog, object userData)
	{
		m_buttonsDisabledByDialog.Remove(dialog);
		UpdateButtonEnableState();
	}

	public bool AreButtonsEnabled()
	{
		return m_buttonsEnabled;
	}

	public void HideGameMenu()
	{
		if (m_gameMenu != null && m_gameMenu.GameMenuIsShown())
		{
			m_gameMenu.GameMenuHide();
		}
	}

	public void HideOptionsMenu()
	{
		if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
		{
			OptionsMenu.Get().Hide();
		}
	}

	public void HideMiscellaneousMenu()
	{
		if (MiscellaneousMenu.Get() != null && MiscellaneousMenu.Get().IsShown())
		{
			MiscellaneousMenu.Get().Hide();
		}
	}

	public bool IsGameMenuShown()
	{
		if (m_gameMenu != null)
		{
			return m_gameMenu.GameMenuIsShown();
		}
		return false;
	}

	public void UpdateForPhone()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool active = mode == SceneMgr.Mode.HUB || mode == SceneMgr.Mode.LOGIN || mode == SceneMgr.Mode.GAMEPLAY || IsCurrencyFrameActive();
		m_menuButton.gameObject.SetActive(active);
	}

	public void UpdateLoginTooltip()
	{
		if (!Network.ShouldBeConnectedToAurora() && !m_suppressLoginTooltip && SceneMgr.Get().IsInGame() && GameMgr.Get().IsTutorial() && !GameMgr.Get().IsSpectator() && DemoMgr.Get().GetMode() != DemoMode.BLIZZ_MUSEUM)
		{
			if (m_loginTooltip == null)
			{
				m_loginTooltip = AssetLoader.Get().InstantiatePrefab("LoginPointer.prefab:e26056ee6e4b89c45899d54bc9497bb0");
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					m_loginTooltip.transform.localScale = new Vector3(60f, 60f, 60f);
				}
				else
				{
					m_loginTooltip.transform.localScale = new Vector3(40f, 40f, 40f);
				}
				TransformUtil.SetEulerAngleX(m_loginTooltip, 270f);
				SceneUtils.SetLayer(m_loginTooltip, GameLayer.BattleNet);
				m_loginTooltip.transform.parent = base.transform;
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				TransformUtil.SetPoint(m_loginTooltip, Anchor.RIGHT, m_batteryLevel.gameObject, Anchor.LEFT, new Vector3(-32f, 0f, 0f));
			}
			else
			{
				TransformUtil.SetPoint(m_loginTooltip, Anchor.RIGHT, m_menuButton, Anchor.LEFT, new Vector3(-80f, 0f, 0f));
			}
		}
		else
		{
			DestroyLoginTooltip();
		}
	}

	private void DestroyLoginTooltip()
	{
		if (m_loginTooltip != null)
		{
			UnityEngine.Object.Destroy(m_loginTooltip);
			m_loginTooltip = null;
		}
	}

	public void SuppressLoginTooltip(bool val)
	{
		m_suppressLoginTooltip = val;
		UpdateLayout();
	}

	private void ShowFriendList()
	{
		ChatMgr.Get().ShowFriendsList();
		m_hasUnacknowledgedPendingInvites = false;
		m_friendButton.ShowPendingInvitesIcon(m_hasUnacknowledgedPendingInvites);
	}

	public void HideFriendList()
	{
		ChatMgr.Get().CloseChatUI();
	}

	private void OnFriendButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		ToggleFriendListShowing();
		UpdateLayout();
	}

	private void ToggleFriendListShowing()
	{
		if (ChatMgr.Get().IsFriendListShowing())
		{
			HideFriendList();
		}
		else
		{
			ShowFriendList();
		}
		m_friendButton.HideTooltip();
	}

	private void UpdateButtonEnableState()
	{
		if (m_buttonsDisabledPermanently || m_buttonsDisabledByRefCount > 0 || m_buttonsDisabledByDialog.Any())
		{
			m_buttonsEnabled = false;
			m_menuButton.SetEnabled(enabled: false);
			m_friendButton.SetEnabled(enabled: false);
			HideMiscellaneousMenu();
			HideOptionsMenu();
			HideGameMenu();
			HideFriendList();
		}
		else
		{
			m_buttonsEnabled = true;
			m_menuButton.SetEnabled(enabled: true);
			m_friendButton.SetEnabled(enabled: true);
		}
	}

	private void WillReset()
	{
		if (m_gameMenu != null)
		{
			if (m_gameMenu.GameMenuIsShown())
			{
				m_gameMenu.GameMenuHide();
			}
			UnityEngine.Object.DestroyImmediate(m_gameMenu.GameMenuGetGameObject());
			m_gameMenu = null;
		}
		DestroyLoginTooltip();
		ToggleActive(active: false);
		m_isLoggedIn = false;
	}

	private bool HandleEscapeKey()
	{
		if (m_gameMenu != null && m_gameMenu.GameMenuIsShown())
		{
			m_gameMenu.GameMenuHide();
			return true;
		}
		if (OptionsMenu.Get() != null && OptionsMenu.Get().IsShown())
		{
			OptionsMenu.Get().Hide();
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
		switch (mode)
		{
		case SceneMgr.Mode.FATAL_ERROR:
			return true;
		case SceneMgr.Mode.LOGIN:
			return true;
		case SceneMgr.Mode.STARTUP:
			return true;
		default:
			if (!DemoMgr.Get().IsHubEscMenuEnabled(mode == SceneMgr.Mode.GAMEPLAY))
			{
				return true;
			}
			ToggleGameMenu();
			return true;
		}
	}

	private void OnMenuButtonReleased(UIEvent e)
	{
		if (GameMgr.Get().IsSpectator() || GameState.Get() == null || !GameState.Get().IsInTargetMode())
		{
			ToggleGameMenu();
		}
	}

	private void LoadGameMenu()
	{
		if (!m_gameMenuLoading && m_gameMenu == null)
		{
			m_gameMenuLoading = true;
			AssetLoader.Get().InstantiatePrefab("GameMenu.prefab:dc76cbcfb64a34d7e93755df33db2f80", ShowGameMenu);
		}
	}

	private void ShowGameMenu(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_gameMenu = go.GetComponent<GameMenu>();
		m_gameMenu.GameMenuShow();
		if (this.OnMenuOpened != null)
		{
			this.OnMenuOpened();
		}
		m_gameMenuLoading = false;
	}

	private void UpdateForDemoMode()
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			bool flag = false;
			bool flag2 = true;
			switch (DemoMgr.Get().GetMode())
			{
			case DemoMode.PAX_EAST_2013:
			case DemoMode.BLIZZCON_2013:
			case DemoMode.BLIZZCON_2015:
			case DemoMode.BLIZZCON_2017_ADVENTURE:
			case DemoMode.BLIZZCON_2017_BRAWL:
				flag = mode == SceneMgr.Mode.GAMEPLAY;
				flag2 = false;
				m_currencyFrameContainer.SetActive(value: false);
				break;
			case DemoMode.BLIZZCON_2014:
				flag2 = (flag = mode != SceneMgr.Mode.FRIENDLY);
				break;
			case DemoMode.BLIZZ_MUSEUM:
				flag = (flag2 = false);
				break;
			case DemoMode.ANNOUNCEMENT_5_0:
				flag2 = true;
				flag = true;
				break;
			case DemoMode.BLIZZCON_2016:
			case DemoMode.BLIZZCON_2018_BRAWL:
			case DemoMode.BLIZZCON_2019_BATTLEGROUNDS:
				flag = mode == SceneMgr.Mode.GAMEPLAY;
				flag2 = mode == SceneMgr.Mode.HUB;
				break;
			default:
				flag = mode != SceneMgr.Mode.FRIENDLY && mode != SceneMgr.Mode.TOURNAMENT;
				break;
			}
			if ((mode == SceneMgr.Mode.GAMEPLAY || (uint)(mode - 7) <= 1u) && DemoMgr.Get().GetMode() != DemoMode.ANNOUNCEMENT_5_0)
			{
				flag2 = false;
			}
			if (!flag)
			{
				m_menuButton.gameObject.SetActive(value: false);
			}
			if (!flag2)
			{
				m_friendButton.gameObject.SetActive(value: false);
			}
		}
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return;
		}
		m_suppressLoginTooltip = false;
		RefreshCurrency();
		bool num = mode != 0 && mode != SceneMgr.Mode.FATAL_ERROR;
		if (num)
		{
			if (SpectatorManager.Get().IsInSpectatorMode())
			{
				SpectatorManager_OnSpectatorModeChanged(OnlineEventType.ADDED, null);
			}
		}
		else if (m_spectatorModeIndicator != null && m_spectatorModeIndicator.activeSelf)
		{
			m_spectatorModeIndicator.SetActive(value: false);
		}
		if (num && m_spectatorCountPanel != null)
		{
			bool active = SpectatorManager.Get().IsBeingSpectated();
			if ((bool)UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
			{
				active = false;
			}
			m_spectatorCountPanel.SetActive(active);
		}
		UpdateForDemoMode();
		UpdateLayout();
	}

	private void SpectatorManager_OnInviteReceived(OnlineEventType evt, BnetPlayer inviter)
	{
		if (ChatMgr.Get().IsFriendListShowing() || !SpectatorManager.Get().HasAnyReceivedInvites())
		{
			m_hasUnacknowledgedPendingInvites = false;
		}
		else
		{
			m_hasUnacknowledgedPendingInvites = m_hasUnacknowledgedPendingInvites || evt == OnlineEventType.ADDED;
		}
		if (m_friendButton != null)
		{
			m_friendButton.ShowPendingInvitesIcon(m_hasUnacknowledgedPendingInvites);
		}
	}

	private void SpectatorManager_OnSpectatorToMyGame(OnlineEventType evt, BnetPlayer spectator)
	{
		int countSpectatingMe = SpectatorManager.Get().GetCountSpectatingMe();
		if (countSpectatingMe <= 0)
		{
			if (m_spectatorCountPanel == null)
			{
				return;
			}
		}
		else if (m_spectatorCountPanel == null)
		{
			string spectatorCountPrefabPath = m_spectatorCountPrefabPath;
			AssetLoader.Get().InstantiatePrefab(spectatorCountPrefabPath, delegate(AssetReference n, GameObject go, object d)
			{
				BnetBar bnetBar = Get();
				if (!(bnetBar == null))
				{
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
							component.AddEventListener(UIEventType.ROLLOVER, SpectatorCount_OnRollover);
							component.AddEventListener(UIEventType.ROLLOUT, SpectatorCount_OnRollout);
						}
						Material material = bnetBar.m_spectatorCountPanel.transform.Find("BeingWatchedHighlight").gameObject.GetComponent<Renderer>().GetMaterial();
						Color color = material.color;
						color.a = 0f;
						material.color = color;
					}
					Get().SpectatorManager_OnSpectatorToMyGame(evt, spectator);
				}
			});
			return;
		}
		m_spectatorCountPanel.transform.Find("UberText").GetComponent<UberText>().Text = countSpectatingMe.ToString();
		bool active = countSpectatingMe > 0;
		if ((bool)UniversalInputManager.UsePhoneUI && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame())
		{
			active = false;
		}
		m_spectatorCountPanel.SetActive(active);
		UpdateLayout();
		GameObject target = m_spectatorCountPanel.transform.Find("BeingWatchedHighlight").gameObject;
		iTween.Stop(target, includechildren: true);
		Action<object> action = delegate
		{
			if (!(Get() == null))
			{
				iTween.FadeTo(Get().m_spectatorCountPanel.transform.Find("BeingWatchedHighlight").gameObject, 0f, 0.5f);
			}
		};
		Hashtable args = iTween.Hash("alpha", 1f, "time", 0.5f, "oncomplete", action);
		iTween.FadeTo(target, args);
	}

	private static void SpectatorCount_OnRollover(UIEvent evt)
	{
		BnetBar bnetBar = Get();
		if (bnetBar == null)
		{
			return;
		}
		string headline = GameStrings.Get("GLOBAL_SPECTATOR_COUNT_PANEL_HEADER");
		BnetGameAccountId[] spectatorPartyMembers = SpectatorManager.Get().GetSpectatorPartyMembers();
		string bodytext;
		if (spectatorPartyMembers.Length == 1)
		{
			string playerBestName = BnetUtils.GetPlayerBestName(spectatorPartyMembers[0]);
			bodytext = GameStrings.Format("GLOBAL_SPECTATOR_COUNT_PANEL_TEXT_ONE", playerBestName);
		}
		else
		{
			string[] value = spectatorPartyMembers.Select((BnetGameAccountId id) => BnetUtils.GetPlayerBestName(id)).ToArray();
			bodytext = string.Join(", ", value);
		}
		bnetBar.m_spectatorCountTooltipZone.ShowSocialTooltip(bnetBar.m_spectatorCountPanel, headline, bodytext, 75f, GameLayer.BattleNetDialog);
		bnetBar.m_spectatorCountTooltipZone.AnchorTooltipTo(bnetBar.m_spectatorCountPanel, Anchor.TOP_LEFT, Anchor.BOTTOM_LEFT);
	}

	private static void SpectatorCount_OnRollout(UIEvent evt)
	{
		BnetBar bnetBar = Get();
		if (!(bnetBar == null))
		{
			bnetBar.m_spectatorCountTooltipZone.HideTooltip();
		}
	}

	private void ShowSpectatorModeIndicator(bool show)
	{
		if (m_spectatorModeIndicator != null)
		{
			m_spectatorModeIndicator.SetActive(show);
		}
		if (show)
		{
			UberText componentInChildren = m_spectatorModeIndicator.GetComponentInChildren<UberText>();
			if (componentInChildren != null && SpectatorManager.Get().IsInSpectatorMode())
			{
				componentInChildren.Text = GameStrings.Get("GLOBAL_SPECTATOR_MODE_INDICATOR_TEXT");
			}
		}
	}

	private void CheckSpectatorModeIndicator()
	{
		if (ShouldShowSpectatorModeIndicator && m_spectatorModeIndicator == null)
		{
			string spectatorModeIndicatorPrefab = m_spectatorModeIndicatorPrefab;
			AssetLoader.Get().InstantiatePrefab(spectatorModeIndicatorPrefab, delegate(AssetReference n, GameObject go, object d)
			{
				BnetBar bnetBar = Get();
				if (!(bnetBar == null) && !(go == null))
				{
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
							m_initialSpectatorModeIndicatorScaleX = component.m_localScale[bestScreenMatch].x;
						}
					}
					Get().CheckSpectatorModeIndicator();
				}
			});
		}
		else if (!(m_spectatorModeIndicator == null))
		{
			UpdateLayout();
		}
	}

	private void SpectatorManager_OnSpectatorModeChanged(OnlineEventType evt, BnetPlayer spectatee)
	{
		CheckSpectatorModeIndicator();
	}

	private void OnRequestGetServerTimeResponse()
	{
		ResponseWithRequest<GetServerTimeResponse, GetServerTimeRequest> serverTimeResponse = Network.Get().GetServerTimeResponse();
		ulong num = TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
		m_serverClientOffsetInSec = serverTimeResponse.Response.ServerUnixTime - (long)num;
	}
}
