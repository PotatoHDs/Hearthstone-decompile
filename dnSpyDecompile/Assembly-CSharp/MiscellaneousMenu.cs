using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000B1F RID: 2847
[CustomEditClass]
public class MiscellaneousMenu : ButtonListMenu
{
	// Token: 0x17000896 RID: 2198
	// (get) Token: 0x06009739 RID: 38713 RVA: 0x0030E0BA File Offset: 0x0030C2BA
	// (set) Token: 0x0600973A RID: 38714 RVA: 0x0030E0C2 File Offset: 0x0030C2C2
	private bool IsOpeningDataManagmentLink { get; set; }

	// Token: 0x0600973B RID: 38715 RVA: 0x0030E0CC File Offset: 0x0030C2CC
	protected override void Awake()
	{
		this.m_menuParent = this.m_menuBone;
		this.m_targetLayer = GameLayer.HighPriorityUI;
		base.Awake();
		MiscellaneousMenu.s_instance = this;
		this.m_creditsButton = base.CreateMenuButton("CreditsButton", "GLOBAL_OPTIONS_CREDITS", new UIEvent.Handler(this.OnCreditsButtonReleased));
		this.m_managePrivacyButton = base.CreateMenuButton("ManagePrivacyButton", "GLOBAL_OPTIONS_PRIVACY_POLICY", new UIEvent.Handler(this.OnManagePrivacyButtonReleased));
		this.m_privacyPolicyButton = base.CreateMenuButton("PrivacyPolicyButton", "GLUE_PRIVACY_POLICY_TITLE", new UIEvent.Handler(this.OnPrivacyPolicyButtonReleased));
		this.m_restorePurchasesButton = base.CreateMenuButton("RestorePurchasesButton", "GLOBAL_OPTIONS_RESTORE_PURCHASES", new UIEvent.Handler(this.OnRestorePurchasesButtonReleased));
		string buttonTextString = RankMgr.Get().UseLegacyRankedPlay() ? "GLOBAL_OPTIONS_SKIP_TO_RANK_25" : "GLOBAL_OPTIONS_SKIP_NPR";
		this.m_skipNprButton = base.CreateMenuButton("SkipNprButton", buttonTextString, new UIEvent.Handler(this.OnSkipNprButtonReleased));
		ButtonListMenu.MakeButtonRed(this.m_skipNprButton, this.m_redButtonMaterial);
		this.m_menu.m_headerText.Text = GameStrings.Get("GLOBAL_OPTIONS_MISCELLANEOUS_LABEL");
	}

	// Token: 0x0600973C RID: 38716 RVA: 0x0030E1E3 File Offset: 0x0030C3E3
	public static MiscellaneousMenu Get()
	{
		return MiscellaneousMenu.s_instance;
	}

	// Token: 0x0600973D RID: 38717 RVA: 0x0030E1EC File Offset: 0x0030C3EC
	protected override List<UIBButton> GetButtons()
	{
		List<UIBButton> list = new List<UIBButton>();
		list.Add(this.m_creditsButton);
		list.Add(this.m_managePrivacyButton);
		list.Add(this.m_privacyPolicyButton);
		if (PlatformSettings.OS == OSCategory.iOS)
		{
			list.Add(this.m_restorePurchasesButton);
		}
		if (RankMgr.Get().CanPromoteSelfManually() && !CollectionManager.Get().IsInEditMode() && Network.IsLoggedIn())
		{
			list.Add(this.m_skipNprButton);
		}
		return list;
	}

	// Token: 0x0600973E RID: 38718 RVA: 0x0030E263 File Offset: 0x0030C463
	private void OnCreditsButtonReleased(UIEvent e)
	{
		this.Hide();
		if (NarrativeManager.Get() != null && NarrativeManager.Get().IsShowingBlockingDialog())
		{
			return;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
		{
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.CREDITS, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600973F RID: 38719 RVA: 0x0030E2A1 File Offset: 0x0030C4A1
	private void OnManagePrivacyButtonReleased(UIEvent e)
	{
		if (this.IsOpeningDataManagmentLink)
		{
			return;
		}
		this.IsOpeningDataManagmentLink = true;
		Processor.QueueJob("OpenDataManagmentLink", this.Job_OpenDataManagementLink(), Array.Empty<IJobDependency>()).AddJobFinishedEventListener(delegate(JobDefinition job, bool success)
		{
			this.IsOpeningDataManagmentLink = false;
		});
	}

	// Token: 0x06009740 RID: 38720 RVA: 0x0030E2D9 File Offset: 0x0030C4D9
	private void OnPrivacyPolicyButtonReleased(UIEvent e)
	{
		Application.OpenURL(ExternalUrlService.Get().GetPrivacyPolicyLink());
	}

	// Token: 0x06009741 RID: 38721 RVA: 0x0030E2EA File Offset: 0x0030C4EA
	private IEnumerator<IAsyncJobResult> Job_OpenDataManagementLink()
	{
		GenerateSSOToken tokenGenerator = new GenerateSSOToken();
		yield return tokenGenerator;
		if (!tokenGenerator.HasToken)
		{
			yield return new JobFailedResult("Could not generate SSO token to open data management link", Array.Empty<object>());
		}
		Application.OpenURL(ExternalUrlService.Get().GetDataManagementLink(tokenGenerator.Token));
		yield break;
	}

	// Token: 0x06009742 RID: 38722 RVA: 0x0030E2F4 File Offset: 0x0030C4F4
	private void OnRestorePurchasesButtonReleased(UIEvent e)
	{
		this.Hide();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_OPTIONS_RESTORE_PURCHASES");
		popupInfo.m_text = GameStrings.Get("GLOBAL_OPTIONS_RESTORE_PURCHASES_POPUP_TEXT");
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_SWITCH_ACCOUNT");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BACK");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				GameUtils.LogoutConfirmation();
			}
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x06009743 RID: 38723 RVA: 0x0030E38B File Offset: 0x0030C58B
	private void OnSkipNprButtonReleased(UIEvent e)
	{
		this.Hide();
		DialogManager.Get().ShowLeaguePromoteSelfManuallyDialog(delegate
		{
			if (!Network.IsLoggedIn())
			{
				DialogManager.Get().ShowReconnectHelperDialog(new Action(this.RequestLeaguePromoteSelf), null);
				return;
			}
			this.RequestLeaguePromoteSelf();
		});
	}

	// Token: 0x06009744 RID: 38724 RVA: 0x0030E3A9 File Offset: 0x0030C5A9
	private void RequestLeaguePromoteSelf()
	{
		RankMgr.Get().DidPromoteSelfThisSession = true;
		Network.Get().RegisterNetHandler(LeaguePromoteSelfResponse.PacketID.ID, new Network.NetHandler(this.OnLeaguePromoteSelfResponse), null);
		Network.Get().RequestLeaguePromoteSelf();
	}

	// Token: 0x06009745 RID: 38725 RVA: 0x0030E3E2 File Offset: 0x0030C5E2
	private void OnTransitionToHubComplete_ShowRankedIntro(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnTransitionToHubComplete_ShowRankedIntro));
		PopupDisplayManager.Get().ShowRankedIntro();
	}

	// Token: 0x06009746 RID: 38726 RVA: 0x0030E408 File Offset: 0x0030C608
	public void OnLeaguePromoteSelfResponse()
	{
		Network.Get().RemoveNetHandler(LeaguePromoteSelfResponse.PacketID.ID, new Network.NetHandler(this.OnLeaguePromoteSelfResponse));
		LeaguePromoteSelfResponse leaguePromoteSelfResponse = Network.Get().GetLeaguePromoteSelfResponse();
		if (leaguePromoteSelfResponse.ErrorCode == ErrorCode.ERROR_OK)
		{
			RankMgr.Get().DidPromoteSelfThisSession = true;
			Network.Get().RegisterNetHandler(MedalInfo.PacketID.ID, new Network.NetHandler(this.OnMedalInfoResponse), null);
			NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
			return;
		}
		RankMgr.Get().DidPromoteSelfThisSession = false;
		Log.All.PrintError("Player not able to Skip NPR. Player={0}, Error={1}", new object[]
		{
			BnetPresenceMgr.Get().GetMyPlayer().GetAccountId(),
			leaguePromoteSelfResponse.ErrorCode
		});
	}

	// Token: 0x06009747 RID: 38727 RVA: 0x0030E4C4 File Offset: 0x0030C6C4
	public void OnMedalInfoResponse()
	{
		Network.Get().RemoveNetHandler(MedalInfo.PacketID.ID, new Network.NetHandler(this.OnMedalInfoResponse));
		if (SetRotationManager.ShouldShowSetRotationIntro())
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
			else
			{
				Box.Get().TryToStartSetRotationFromHub();
			}
			Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnTransitionToHubComplete_ShowRankedIntro));
			return;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnTransitionToHubComplete_ShowRankedIntro));
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		this.OnTransitionToHubComplete_ShowRankedIntro(null);
	}

	// Token: 0x04007E8C RID: 32396
	[CustomEditField(Sections = "Template Items")]
	public Transform m_menuBone;

	// Token: 0x04007E8D RID: 32397
	public Material m_redButtonMaterial;

	// Token: 0x04007E8E RID: 32398
	private static MiscellaneousMenu s_instance;

	// Token: 0x04007E8F RID: 32399
	private UIBButton m_creditsButton;

	// Token: 0x04007E90 RID: 32400
	private UIBButton m_managePrivacyButton;

	// Token: 0x04007E91 RID: 32401
	private UIBButton m_privacyPolicyButton;

	// Token: 0x04007E92 RID: 32402
	private UIBButton m_restorePurchasesButton;

	// Token: 0x04007E93 RID: 32403
	private UIBButton m_skipNprButton;
}
