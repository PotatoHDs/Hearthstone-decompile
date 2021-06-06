using PegasusShared;
using UnityEngine;

public class FriendListFSGFrame : FriendListUIElement
{
	public GameObject m_FSGFlyoutMenuTwoButtonFrame;

	public GameObject m_FSGFlyoutMenuThreeButtonFrame;

	public GameObject m_FSGJoinMenu;

	public GameObject m_FSGFlyout;

	public PegUIElement m_FSGJoinButton;

	public PegUIElement m_FSGEnterButton;

	public PegUIElement m_FSGLeaveButton;

	public PegUIElement m_FSGUpdateButton;

	public Renderer m_FSGEnterButtonRenderer;

	public Transform m_FSGPatronEnterButtonBone;

	public Transform m_FSGPatronLeaveButtonBone;

	public Transform m_FSGInnkeeperEnterButtonBone;

	public Transform m_FSGInnkeeperUpdateButtonBone;

	public Transform m_FSGInnkeeperLeaveButtonBone;

	public UberText m_FSGTitleText;

	public UberText m_TavernNameText;

	public UberText m_TavernNameJoinText;

	public GameObject m_Background;

	public GameObject m_HighlightBackground;

	public GameObject m_inFSGGradient;

	public GameObject m_LanternIcon;

	public GameObject m_ArrowIcon;

	public GameObject m_ArrowIconHighlight;

	public float m_TextXOffsetWithoutLantern;

	private long FSGID = -1L;

	private PegUIElement m_FSGMenuInputBlocker;

	private bool m_FSGMenuOpen;

	private Vector3? m_fsgMenuOrigLocalPos;

	public bool m_isInnkeeperSetup;

	private bool IsCheckedIn => FiresideGatheringManager.Get().IsCheckedInToFSG(FSGID);

	private GameObject FSGMenu
	{
		get
		{
			if (!IsCheckedIn)
			{
				return m_FSGJoinMenu;
			}
			return m_FSGFlyout;
		}
	}

	public void InitFrame(FSGConfig gathering)
	{
		FSGID = gathering.FsgId;
		m_isInnkeeperSetup = gathering.IsInnkeeper && !gathering.IsSetupComplete;
		if (m_isInnkeeperSetup)
		{
			string text = "GLUE_FIRESIDE_GATHERING_INNKEEPER_CLICK_TO_SETUP";
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				text += "_PHONE";
			}
			m_TavernNameText.Text = GameStrings.Get(text);
		}
		else
		{
			m_TavernNameText.Text = FiresideGatheringManager.Get().GetTavernName_FriendsList(gathering);
		}
		m_TavernNameJoinText.Text = FiresideGatheringManager.Get().GetTavernName_FriendsList(gathering);
		if (m_isInnkeeperSetup)
		{
			m_FSGTitleText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_CLICK_TO_SETUP_TITLE");
		}
		else
		{
			m_FSGTitleText.Text = (IsCheckedIn ? GameStrings.Get("GLOBAL_FIRESIDE_GATHERING") : GameStrings.Get("GLUE_FSG_FOUND"));
		}
		m_FSGJoinButton.AddEventListener(UIEventType.RELEASE, OnJoinButton);
		m_FSGEnterButton.AddEventListener(UIEventType.RELEASE, OnEnterButton);
		m_FSGLeaveButton.AddEventListener(UIEventType.RELEASE, OnLeaveButton);
		m_FSGUpdateButton.AddEventListener(UIEventType.RELEASE, OnUpdateButton);
		bool flag = FiresideGatheringManager.Get().IsCheckedInToFSG(FSGID);
		m_LanternIcon.SetActive(flag);
		m_inFSGGradient.SetActive(flag);
		if (flag)
		{
			if (gathering.IsInnkeeper)
			{
				m_FSGFlyoutMenuTwoButtonFrame.SetActive(value: false);
				m_FSGFlyoutMenuThreeButtonFrame.SetActive(value: true);
				m_FSGUpdateButton.gameObject.SetActive(value: true);
				GameUtils.SetParent(m_FSGEnterButton.transform, m_FSGInnkeeperEnterButtonBone);
				GameUtils.SetParent(m_FSGUpdateButton.transform, m_FSGInnkeeperUpdateButtonBone);
				GameUtils.SetParent(m_FSGLeaveButton.transform, m_FSGInnkeeperLeaveButtonBone);
			}
			else
			{
				m_FSGFlyoutMenuTwoButtonFrame.SetActive(value: true);
				m_FSGFlyoutMenuThreeButtonFrame.SetActive(value: false);
				m_FSGUpdateButton.gameObject.SetActive(value: false);
				GameUtils.SetParent(m_FSGEnterButton.transform, m_FSGPatronEnterButtonBone);
				GameUtils.SetParent(m_FSGLeaveButton.transform, m_FSGPatronLeaveButtonBone);
			}
			SetEnabled(ShouldEnableEnterButton(), m_FSGEnterButton, m_FSGEnterButtonRenderer);
		}
		else
		{
			Vector3 localPosition = m_TavernNameText.transform.localPosition;
			Vector3 localPosition2 = m_FSGTitleText.transform.localPosition;
			localPosition.x = m_TextXOffsetWithoutLantern;
			localPosition2.x = m_TextXOffsetWithoutLantern;
			m_TavernNameText.transform.localPosition = localPosition;
			m_FSGTitleText.transform.localPosition = localPosition2;
		}
		if (FiresideGatheringManager.Get().m_activeFSGMenu == FSGID)
		{
			OpenFSGMenu();
		}
	}

	private bool ShouldEnableEnterButton()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (UserAttentionManager.GetAvailabilityBlockerReason(isFriendlyChallenge: false) != 0 || mode == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return false;
		}
		return true;
	}

	protected override void OnDestroy()
	{
		m_FSGJoinButton.RemoveEventListener(UIEventType.RELEASE, OnJoinButton);
		m_FSGLeaveButton.RemoveEventListener(UIEventType.RELEASE, OnLeaveButton);
		m_FSGEnterButton.RemoveEventListener(UIEventType.RELEASE, OnEnterButton);
		m_FSGUpdateButton.RemoveEventListener(UIEventType.RELEASE, OnUpdateButton);
		base.OnDestroy();
	}

	protected override void OnRelease()
	{
		if (m_isInnkeeperSetup)
		{
			FiresideGatheringManager.Get().ShowFiresideGatheringInnkeeperSetupDialog();
		}
		else if (m_FSGMenuOpen)
		{
			CloseFSGMenu();
		}
		else
		{
			OpenFSGMenu();
		}
	}

	private void OpenFSGMenu()
	{
		if (FSGMenu == null || m_FSGMenuOpen)
		{
			return;
		}
		FiresideGatheringManager.Get().m_activeFSGMenu = FSGID;
		m_HighlightBackground.SetActive(value: true);
		m_FSGMenuOpen = true;
		FSGMenu.gameObject.SetActive(value: true);
		if (m_fsgMenuOrigLocalPos.HasValue)
		{
			FSGMenu.gameObject.transform.localPosition = m_fsgMenuOrigLocalPos.Value;
		}
		Bounds bounds = FSGMenu.GetComponent<Collider>().bounds;
		Camera camera = CameraUtils.FindFirstByLayer(FSGMenu.layer);
		Vector3 vector = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, bounds.center.z));
		if (vector.y < 0f)
		{
			if (!m_fsgMenuOrigLocalPos.HasValue)
			{
				m_fsgMenuOrigLocalPos = FSGMenu.gameObject.transform.localPosition;
			}
			Vector3 vector2 = camera.WorldToScreenPoint(FSGMenu.gameObject.transform.position);
			FSGMenu.gameObject.transform.position = camera.ScreenToWorldPoint(new Vector3(vector2.x, vector2.y - vector.y, vector2.z));
		}
		InitFSGMenuInputBlocker();
	}

	private void CloseFSGMenu()
	{
		if (!(FSGMenu == null) && m_FSGMenuOpen)
		{
			FiresideGatheringManager.Get().m_activeFSGMenu = -1L;
			m_HighlightBackground.SetActive(value: false);
			m_FSGMenuOpen = false;
			FSGMenu.gameObject.SetActive(value: false);
			if (m_FSGMenuInputBlocker != null)
			{
				Object.Destroy(m_FSGMenuInputBlocker.gameObject);
				m_FSGMenuInputBlocker = null;
			}
		}
	}

	private void InitFSGMenuInputBlocker()
	{
		if (m_FSGMenuInputBlocker != null)
		{
			Object.Destroy(m_FSGMenuInputBlocker.gameObject);
			m_FSGMenuInputBlocker = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(FSGMenu.layer), "FSGMenuInputBlocker");
		gameObject.transform.parent = FSGMenu.transform;
		m_FSGMenuInputBlocker = gameObject.AddComponent<PegUIElement>();
		m_FSGMenuInputBlocker.AddEventListener(UIEventType.RELEASE, OnFSGMenuInputBlockerReleased);
		TransformUtil.SetPosZ(m_FSGMenuInputBlocker, FSGMenu.transform.position.z + 1f);
	}

	private void OnFSGMenuInputBlockerReleased(UIEvent e)
	{
		CloseFSGMenu();
	}

	private void OnEnterButton(UIEvent e)
	{
		if (ShouldEnableEnterButton() && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING))
		{
			Navigation.Clear();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.FIRESIDE_GATHERING);
			BnetBar.Get().HideFriendList();
			FiresideGatheringManager.Get().EnableTransitionInputBlocker(enabled: true);
		}
	}

	private void SetEnabled(bool enabled, PegUIElement button, Renderer buttonRenderer)
	{
		button.SetEnabled(enabled);
		buttonRenderer.GetMaterial().SetFloat("_Desaturate", (!enabled) ? 1 : 0);
	}

	private void OnJoinButton(UIEvent e)
	{
		ChatMgr.Get().CloseChatUI();
		FiresideGatheringManager.Get().CheckInToFSG(FSGID);
	}

	private void OnLeaveButton(UIEvent e)
	{
		ChatMgr.Get().CloseChatUI();
		FiresideGatheringManager.Get().CheckOutOfFSG(optOut: true);
	}

	private void OnUpdateButton(UIEvent e)
	{
		ChatMgr.Get().CloseChatUI();
		FiresideGatheringManager.Get().CheckOutOfFSG(optOut: true);
		FiresideGatheringManager.Get().ShowFiresideGatheringInnkeeperSetupDialog();
	}

	protected override void OnOver(InteractionState oldState)
	{
		base.OnOver(oldState);
		m_Background.SetActive(value: false);
		m_HighlightBackground.SetActive(value: true);
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
		m_Background.SetActive(value: true);
		if (!m_FSGMenuOpen)
		{
			m_HighlightBackground.SetActive(value: false);
		}
	}
}
