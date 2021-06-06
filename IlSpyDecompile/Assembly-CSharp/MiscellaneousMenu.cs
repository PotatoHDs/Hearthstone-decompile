using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class MiscellaneousMenu : ButtonListMenu
{
	[CustomEditField(Sections = "Template Items")]
	public Transform m_menuBone;

	public Material m_redButtonMaterial;

	private static MiscellaneousMenu s_instance;

	private UIBButton m_creditsButton;

	private UIBButton m_managePrivacyButton;

	private UIBButton m_privacyPolicyButton;

	private UIBButton m_restorePurchasesButton;

	private UIBButton m_skipNprButton;

	private bool IsOpeningDataManagmentLink { get; set; }

	protected override void Awake()
	{
		m_menuParent = m_menuBone;
		m_targetLayer = GameLayer.HighPriorityUI;
		base.Awake();
		s_instance = this;
		m_creditsButton = CreateMenuButton("CreditsButton", "GLOBAL_OPTIONS_CREDITS", OnCreditsButtonReleased);
		m_managePrivacyButton = CreateMenuButton("ManagePrivacyButton", "GLOBAL_OPTIONS_PRIVACY_POLICY", OnManagePrivacyButtonReleased);
		m_privacyPolicyButton = CreateMenuButton("PrivacyPolicyButton", "GLUE_PRIVACY_POLICY_TITLE", OnPrivacyPolicyButtonReleased);
		m_restorePurchasesButton = CreateMenuButton("RestorePurchasesButton", "GLOBAL_OPTIONS_RESTORE_PURCHASES", OnRestorePurchasesButtonReleased);
		string buttonTextString = (RankMgr.Get().UseLegacyRankedPlay() ? "GLOBAL_OPTIONS_SKIP_TO_RANK_25" : "GLOBAL_OPTIONS_SKIP_NPR");
		m_skipNprButton = CreateMenuButton("SkipNprButton", buttonTextString, OnSkipNprButtonReleased);
		ButtonListMenu.MakeButtonRed(m_skipNprButton, m_redButtonMaterial);
		m_menu.m_headerText.Text = GameStrings.Get("GLOBAL_OPTIONS_MISCELLANEOUS_LABEL");
	}

	public static MiscellaneousMenu Get()
	{
		return s_instance;
	}

	protected override List<UIBButton> GetButtons()
	{
		List<UIBButton> list = new List<UIBButton>();
		list.Add(m_creditsButton);
		list.Add(m_managePrivacyButton);
		list.Add(m_privacyPolicyButton);
		if (PlatformSettings.OS == OSCategory.iOS)
		{
			list.Add(m_restorePurchasesButton);
		}
		if (RankMgr.Get().CanPromoteSelfManually() && !CollectionManager.Get().IsInEditMode() && Network.IsLoggedIn())
		{
			list.Add(m_skipNprButton);
		}
		return list;
	}

	private void OnCreditsButtonReleased(UIEvent e)
	{
		Hide();
		if ((!(NarrativeManager.Get() != null) || !NarrativeManager.Get().IsShowingBlockingDialog()) && SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN)
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.CREDITS);
		}
	}

	private void OnManagePrivacyButtonReleased(UIEvent e)
	{
		if (!IsOpeningDataManagmentLink)
		{
			IsOpeningDataManagmentLink = true;
			Processor.QueueJob("OpenDataManagmentLink", Job_OpenDataManagementLink()).AddJobFinishedEventListener(delegate
			{
				IsOpeningDataManagmentLink = false;
			});
		}
	}

	private void OnPrivacyPolicyButtonReleased(UIEvent e)
	{
		Application.OpenURL(ExternalUrlService.Get().GetPrivacyPolicyLink());
	}

	private IEnumerator<IAsyncJobResult> Job_OpenDataManagementLink()
	{
		GenerateSSOToken tokenGenerator = new GenerateSSOToken();
		yield return tokenGenerator;
		if (!tokenGenerator.HasToken)
		{
			yield return new JobFailedResult("Could not generate SSO token to open data management link");
		}
		Application.OpenURL(ExternalUrlService.Get().GetDataManagementLink(tokenGenerator.Token));
	}

	private void OnRestorePurchasesButtonReleased(UIEvent e)
	{
		Hide();
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLOBAL_OPTIONS_RESTORE_PURCHASES"),
			m_text = GameStrings.Get("GLOBAL_OPTIONS_RESTORE_PURCHASES_POPUP_TEXT"),
			m_confirmText = GameStrings.Get("GLOBAL_SWITCH_ACCOUNT"),
			m_cancelText = GameStrings.Get("GLOBAL_BACK"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					GameUtils.LogoutConfirmation();
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	private void OnSkipNprButtonReleased(UIEvent e)
	{
		Hide();
		DialogManager.Get().ShowLeaguePromoteSelfManuallyDialog(delegate
		{
			if (!Network.IsLoggedIn())
			{
				DialogManager.Get().ShowReconnectHelperDialog(RequestLeaguePromoteSelf);
			}
			else
			{
				RequestLeaguePromoteSelf();
			}
		});
	}

	private void RequestLeaguePromoteSelf()
	{
		RankMgr.Get().DidPromoteSelfThisSession = true;
		Network.Get().RegisterNetHandler(LeaguePromoteSelfResponse.PacketID.ID, OnLeaguePromoteSelfResponse);
		Network.Get().RequestLeaguePromoteSelf();
	}

	private void OnTransitionToHubComplete_ShowRankedIntro(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnTransitionToHubComplete_ShowRankedIntro);
		PopupDisplayManager.Get().ShowRankedIntro();
	}

	public void OnLeaguePromoteSelfResponse()
	{
		Network.Get().RemoveNetHandler(LeaguePromoteSelfResponse.PacketID.ID, OnLeaguePromoteSelfResponse);
		LeaguePromoteSelfResponse leaguePromoteSelfResponse = Network.Get().GetLeaguePromoteSelfResponse();
		if (leaguePromoteSelfResponse.ErrorCode == ErrorCode.ERROR_OK)
		{
			RankMgr.Get().DidPromoteSelfThisSession = true;
			Network.Get().RegisterNetHandler(MedalInfo.PacketID.ID, OnMedalInfoResponse);
			NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
		}
		else
		{
			RankMgr.Get().DidPromoteSelfThisSession = false;
			Log.All.PrintError("Player not able to Skip NPR. Player={0}, Error={1}", BnetPresenceMgr.Get().GetMyPlayer().GetAccountId(), leaguePromoteSelfResponse.ErrorCode);
		}
	}

	public void OnMedalInfoResponse()
	{
		Network.Get().RemoveNetHandler(MedalInfo.PacketID.ID, OnMedalInfoResponse);
		if (SetRotationManager.ShouldShowSetRotationIntro())
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
			else
			{
				Box.Get().TryToStartSetRotationFromHub();
			}
			Box.Get().AddTransitionFinishedListener(OnTransitionToHubComplete_ShowRankedIntro);
		}
		else if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			Box.Get().AddTransitionFinishedListener(OnTransitionToHubComplete_ShowRankedIntro);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
		else
		{
			OnTransitionToHubComplete_ShowRankedIntro(null);
		}
	}
}
