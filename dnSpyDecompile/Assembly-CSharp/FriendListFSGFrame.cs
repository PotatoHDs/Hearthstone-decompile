using System;
using PegasusShared;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class FriendListFSGFrame : FriendListUIElement
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000912 RID: 2322 RVA: 0x00035F7D File Offset: 0x0003417D
	private bool IsCheckedIn
	{
		get
		{
			return FiresideGatheringManager.Get().IsCheckedInToFSG(this.FSGID);
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000913 RID: 2323 RVA: 0x00035F8F File Offset: 0x0003418F
	private GameObject FSGMenu
	{
		get
		{
			if (!this.IsCheckedIn)
			{
				return this.m_FSGJoinMenu;
			}
			return this.m_FSGFlyout;
		}
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00035FA8 File Offset: 0x000341A8
	public void InitFrame(FSGConfig gathering)
	{
		this.FSGID = gathering.FsgId;
		this.m_isInnkeeperSetup = (gathering.IsInnkeeper && !gathering.IsSetupComplete);
		if (this.m_isInnkeeperSetup)
		{
			string text = "GLUE_FIRESIDE_GATHERING_INNKEEPER_CLICK_TO_SETUP";
			if (UniversalInputManager.UsePhoneUI)
			{
				text += "_PHONE";
			}
			this.m_TavernNameText.Text = GameStrings.Get(text);
		}
		else
		{
			this.m_TavernNameText.Text = FiresideGatheringManager.Get().GetTavernName_FriendsList(gathering);
		}
		this.m_TavernNameJoinText.Text = FiresideGatheringManager.Get().GetTavernName_FriendsList(gathering);
		if (this.m_isInnkeeperSetup)
		{
			this.m_FSGTitleText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_CLICK_TO_SETUP_TITLE");
		}
		else
		{
			this.m_FSGTitleText.Text = (this.IsCheckedIn ? GameStrings.Get("GLOBAL_FIRESIDE_GATHERING") : GameStrings.Get("GLUE_FSG_FOUND"));
		}
		this.m_FSGJoinButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnJoinButton));
		this.m_FSGEnterButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnEnterButton));
		this.m_FSGLeaveButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnLeaveButton));
		this.m_FSGUpdateButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnUpdateButton));
		bool flag = FiresideGatheringManager.Get().IsCheckedInToFSG(this.FSGID);
		this.m_LanternIcon.SetActive(flag);
		this.m_inFSGGradient.SetActive(flag);
		if (flag)
		{
			if (gathering.IsInnkeeper)
			{
				this.m_FSGFlyoutMenuTwoButtonFrame.SetActive(false);
				this.m_FSGFlyoutMenuThreeButtonFrame.SetActive(true);
				this.m_FSGUpdateButton.gameObject.SetActive(true);
				GameUtils.SetParent(this.m_FSGEnterButton.transform, this.m_FSGInnkeeperEnterButtonBone, false);
				GameUtils.SetParent(this.m_FSGUpdateButton.transform, this.m_FSGInnkeeperUpdateButtonBone, false);
				GameUtils.SetParent(this.m_FSGLeaveButton.transform, this.m_FSGInnkeeperLeaveButtonBone, false);
			}
			else
			{
				this.m_FSGFlyoutMenuTwoButtonFrame.SetActive(true);
				this.m_FSGFlyoutMenuThreeButtonFrame.SetActive(false);
				this.m_FSGUpdateButton.gameObject.SetActive(false);
				GameUtils.SetParent(this.m_FSGEnterButton.transform, this.m_FSGPatronEnterButtonBone, false);
				GameUtils.SetParent(this.m_FSGLeaveButton.transform, this.m_FSGPatronLeaveButtonBone, false);
			}
			this.SetEnabled(this.ShouldEnableEnterButton(), this.m_FSGEnterButton, this.m_FSGEnterButtonRenderer);
		}
		else
		{
			Vector3 localPosition = this.m_TavernNameText.transform.localPosition;
			Vector3 localPosition2 = this.m_FSGTitleText.transform.localPosition;
			localPosition.x = this.m_TextXOffsetWithoutLantern;
			localPosition2.x = this.m_TextXOffsetWithoutLantern;
			this.m_TavernNameText.transform.localPosition = localPosition;
			this.m_FSGTitleText.transform.localPosition = localPosition2;
		}
		if (FiresideGatheringManager.Get().m_activeFSGMenu == this.FSGID)
		{
			this.OpenFSGMenu();
		}
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0003627C File Offset: 0x0003447C
	private bool ShouldEnableEnterButton()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		return UserAttentionManager.GetAvailabilityBlockerReason(false) == AvailabilityBlockerReasons.NONE && mode != SceneMgr.Mode.FIRESIDE_GATHERING;
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x000362A4 File Offset: 0x000344A4
	protected override void OnDestroy()
	{
		this.m_FSGJoinButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnJoinButton));
		this.m_FSGLeaveButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnLeaveButton));
		this.m_FSGEnterButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnEnterButton));
		this.m_FSGUpdateButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnUpdateButton));
		base.OnDestroy();
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0003631B File Offset: 0x0003451B
	protected override void OnRelease()
	{
		if (this.m_isInnkeeperSetup)
		{
			FiresideGatheringManager.Get().ShowFiresideGatheringInnkeeperSetupDialog();
			return;
		}
		if (this.m_FSGMenuOpen)
		{
			this.CloseFSGMenu();
			return;
		}
		this.OpenFSGMenu();
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00036348 File Offset: 0x00034548
	private void OpenFSGMenu()
	{
		if (this.FSGMenu == null || this.m_FSGMenuOpen)
		{
			return;
		}
		FiresideGatheringManager.Get().m_activeFSGMenu = this.FSGID;
		this.m_HighlightBackground.SetActive(true);
		this.m_FSGMenuOpen = true;
		this.FSGMenu.gameObject.SetActive(true);
		if (this.m_fsgMenuOrigLocalPos != null)
		{
			this.FSGMenu.gameObject.transform.localPosition = this.m_fsgMenuOrigLocalPos.Value;
		}
		Bounds bounds = this.FSGMenu.GetComponent<Collider>().bounds;
		Camera camera = CameraUtils.FindFirstByLayer(this.FSGMenu.layer);
		Vector3 vector = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, bounds.center.z));
		if (vector.y < 0f)
		{
			if (this.m_fsgMenuOrigLocalPos == null)
			{
				this.m_fsgMenuOrigLocalPos = new Vector3?(this.FSGMenu.gameObject.transform.localPosition);
			}
			Vector3 vector2 = camera.WorldToScreenPoint(this.FSGMenu.gameObject.transform.position);
			this.FSGMenu.gameObject.transform.position = camera.ScreenToWorldPoint(new Vector3(vector2.x, vector2.y - vector.y, vector2.z));
		}
		this.InitFSGMenuInputBlocker();
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x000364B8 File Offset: 0x000346B8
	private void CloseFSGMenu()
	{
		if (this.FSGMenu == null || !this.m_FSGMenuOpen)
		{
			return;
		}
		FiresideGatheringManager.Get().m_activeFSGMenu = -1L;
		this.m_HighlightBackground.SetActive(false);
		this.m_FSGMenuOpen = false;
		this.FSGMenu.gameObject.SetActive(false);
		if (this.m_FSGMenuInputBlocker != null)
		{
			UnityEngine.Object.Destroy(this.m_FSGMenuInputBlocker.gameObject);
			this.m_FSGMenuInputBlocker = null;
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00036534 File Offset: 0x00034734
	private void InitFSGMenuInputBlocker()
	{
		if (this.m_FSGMenuInputBlocker != null)
		{
			UnityEngine.Object.Destroy(this.m_FSGMenuInputBlocker.gameObject);
			this.m_FSGMenuInputBlocker = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(this.FSGMenu.layer), "FSGMenuInputBlocker");
		gameObject.transform.parent = this.FSGMenu.transform;
		this.m_FSGMenuInputBlocker = gameObject.AddComponent<PegUIElement>();
		this.m_FSGMenuInputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFSGMenuInputBlockerReleased));
		TransformUtil.SetPosZ(this.m_FSGMenuInputBlocker, this.FSGMenu.transform.position.z + 1f);
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x000365E2 File Offset: 0x000347E2
	private void OnFSGMenuInputBlockerReleased(UIEvent e)
	{
		this.CloseFSGMenu();
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x000365EA File Offset: 0x000347EA
	private void OnEnterButton(UIEvent e)
	{
		if (this.ShouldEnableEnterButton() && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING))
		{
			Navigation.Clear();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.FIRESIDE_GATHERING, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			BnetBar.Get().HideFriendList();
			FiresideGatheringManager.Get().EnableTransitionInputBlocker(true);
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0003662A File Offset: 0x0003482A
	private void SetEnabled(bool enabled, PegUIElement button, Renderer buttonRenderer)
	{
		button.SetEnabled(enabled, false);
		buttonRenderer.GetMaterial().SetFloat("_Desaturate", (float)(enabled ? 0 : 1));
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0003664C File Offset: 0x0003484C
	private void OnJoinButton(UIEvent e)
	{
		ChatMgr.Get().CloseChatUI(true);
		FiresideGatheringManager.Get().CheckInToFSG(this.FSGID);
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00036669 File Offset: 0x00034869
	private void OnLeaveButton(UIEvent e)
	{
		ChatMgr.Get().CloseChatUI(true);
		FiresideGatheringManager.Get().CheckOutOfFSG(true);
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00036681 File Offset: 0x00034881
	private void OnUpdateButton(UIEvent e)
	{
		ChatMgr.Get().CloseChatUI(true);
		FiresideGatheringManager.Get().CheckOutOfFSG(true);
		FiresideGatheringManager.Get().ShowFiresideGatheringInnkeeperSetupDialog();
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x000366A3 File Offset: 0x000348A3
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.OnOver(oldState);
		this.m_Background.SetActive(false);
		this.m_HighlightBackground.SetActive(true);
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x000366D8 File Offset: 0x000348D8
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
		this.m_Background.SetActive(true);
		if (!this.m_FSGMenuOpen)
		{
			this.m_HighlightBackground.SetActive(false);
		}
	}

	// Token: 0x04000617 RID: 1559
	public GameObject m_FSGFlyoutMenuTwoButtonFrame;

	// Token: 0x04000618 RID: 1560
	public GameObject m_FSGFlyoutMenuThreeButtonFrame;

	// Token: 0x04000619 RID: 1561
	public GameObject m_FSGJoinMenu;

	// Token: 0x0400061A RID: 1562
	public GameObject m_FSGFlyout;

	// Token: 0x0400061B RID: 1563
	public PegUIElement m_FSGJoinButton;

	// Token: 0x0400061C RID: 1564
	public PegUIElement m_FSGEnterButton;

	// Token: 0x0400061D RID: 1565
	public PegUIElement m_FSGLeaveButton;

	// Token: 0x0400061E RID: 1566
	public PegUIElement m_FSGUpdateButton;

	// Token: 0x0400061F RID: 1567
	public Renderer m_FSGEnterButtonRenderer;

	// Token: 0x04000620 RID: 1568
	public Transform m_FSGPatronEnterButtonBone;

	// Token: 0x04000621 RID: 1569
	public Transform m_FSGPatronLeaveButtonBone;

	// Token: 0x04000622 RID: 1570
	public Transform m_FSGInnkeeperEnterButtonBone;

	// Token: 0x04000623 RID: 1571
	public Transform m_FSGInnkeeperUpdateButtonBone;

	// Token: 0x04000624 RID: 1572
	public Transform m_FSGInnkeeperLeaveButtonBone;

	// Token: 0x04000625 RID: 1573
	public UberText m_FSGTitleText;

	// Token: 0x04000626 RID: 1574
	public UberText m_TavernNameText;

	// Token: 0x04000627 RID: 1575
	public UberText m_TavernNameJoinText;

	// Token: 0x04000628 RID: 1576
	public GameObject m_Background;

	// Token: 0x04000629 RID: 1577
	public GameObject m_HighlightBackground;

	// Token: 0x0400062A RID: 1578
	public GameObject m_inFSGGradient;

	// Token: 0x0400062B RID: 1579
	public GameObject m_LanternIcon;

	// Token: 0x0400062C RID: 1580
	public GameObject m_ArrowIcon;

	// Token: 0x0400062D RID: 1581
	public GameObject m_ArrowIconHighlight;

	// Token: 0x0400062E RID: 1582
	public float m_TextXOffsetWithoutLantern;

	// Token: 0x0400062F RID: 1583
	private long FSGID = -1L;

	// Token: 0x04000630 RID: 1584
	private PegUIElement m_FSGMenuInputBlocker;

	// Token: 0x04000631 RID: 1585
	private bool m_FSGMenuOpen;

	// Token: 0x04000632 RID: 1586
	private Vector3? m_fsgMenuOrigLocalPos;

	// Token: 0x04000633 RID: 1587
	public bool m_isInnkeeperSetup;
}
