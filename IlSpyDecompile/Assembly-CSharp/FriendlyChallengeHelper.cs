using bgs;

public class FriendlyChallengeHelper
{
	private static FriendlyChallengeHelper s_instance;

	private AlertPopup m_friendChallengeWaitingPopup;

	private AlertPopup m_deckShareRequestWaitingPopup;

	private AlertPopup m_deckShareRequestDeclinedPopup;

	private AlertPopup m_deckShareRequestCanceledPopup;

	private AlertPopup m_deckShareRequestPopup;

	private AlertPopup m_deckShareErrorPopup;

	public BnetAccountId ActiveChallengeMenu { get; set; }

	public static FriendlyChallengeHelper Get()
	{
		if (s_instance == null)
		{
			s_instance = new FriendlyChallengeHelper();
		}
		return s_instance;
	}

	public void StartChallengeOrWaitForOpponent(string waitingDialogText, AlertPopup.ResponseCallback waitingCallback)
	{
		if (!FriendChallengeMgr.Get().DidOpponentSelectDeckOrHero())
		{
			ShowFriendChallengeWaitingForOpponentDialog(waitingDialogText, waitingCallback);
		}
	}

	public void HideFriendChallengeWaitingForOpponentDialog()
	{
		if (!(m_friendChallengeWaitingPopup == null))
		{
			m_friendChallengeWaitingPopup.Hide();
			m_friendChallengeWaitingPopup = null;
		}
	}

	public void WaitForFriendChallengeToStart()
	{
		int brawlLibraryItemId = 0;
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
			if (tavernBrawlMission != null)
			{
				brawlLibraryItemId = tavernBrawlMission.SelectedBrawlLibraryItemId;
			}
		}
		GameMgr.Get().WaitForFriendChallengeToStart(FriendChallengeMgr.Get().GetFormatType(), FriendChallengeMgr.Get().GetChallengeBrawlType(), FriendChallengeMgr.Get().GetScenarioId(), brawlLibraryItemId, FriendChallengeMgr.Get().IsChallengeBacon());
	}

	public void StopWaitingForFriendChallenge()
	{
		HideFriendChallengeWaitingForOpponentDialog();
	}

	public void HideAllDeckShareDialogs()
	{
		HideDeckShareRequestDialog();
		HideDeckShareRequestCanceledDialog();
		HideDeckShareRequestDeclinedDialog();
		HideDeckShareRequestWaitingDialog();
		HideDeckShareErrorDialog();
	}

	public void ShowDeckShareRequestCanceledDialog()
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUEST_CANCELED", myOpponent.GetBestName());
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			m_deckShareRequestCanceledPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	public void HideDeckShareRequestCanceledDialog()
	{
		if (!(m_deckShareRequestCanceledPopup == null))
		{
			m_deckShareRequestCanceledPopup.Hide();
			m_deckShareRequestCanceledPopup = null;
		}
	}

	public void ShowDeckShareRequestDeclinedDialog()
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUEST_DECLINED", myOpponent.GetBestName());
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			m_deckShareRequestDeclinedPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	public void HideDeckShareRequestDeclinedDialog()
	{
		if (!(m_deckShareRequestDeclinedPopup == null))
		{
			m_deckShareRequestDeclinedPopup.Hide();
			m_deckShareRequestDeclinedPopup = null;
		}
	}

	public void ShowDeckShareRequestWaitingDialog(AlertPopup.ResponseCallback waitingCallback)
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUEST_WAITING_RESPONSE", myOpponent.GetBestName());
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
		popupInfo.m_responseCallback = waitingCallback;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			m_deckShareRequestWaitingPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	public void HideDeckShareRequestWaitingDialog()
	{
		if (!(m_deckShareRequestWaitingPopup == null))
		{
			m_deckShareRequestWaitingPopup.Hide();
			m_deckShareRequestWaitingPopup = null;
		}
	}

	public void ShowDeckShareRequestDialog(AlertPopup.ResponseCallback waitingCallback)
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUESTED", myOpponent.GetBestName());
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = waitingCallback;
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_DECK_SHARE_ACCEPT_REQUEST");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_DECK_SHARE_DECLINE_REQUEST");
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			m_deckShareRequestPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	public bool IsShowingDeckShareRequestDialog()
	{
		return m_deckShareRequestPopup != null;
	}

	public void HideDeckShareRequestDialog()
	{
		if (!(m_deckShareRequestPopup == null))
		{
			m_deckShareRequestPopup.Hide();
			m_deckShareRequestPopup = null;
		}
	}

	public void ShowDeckShareErrorDialog()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_DECK_SHARE_ERROR");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			m_deckShareErrorPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	public void HideDeckShareErrorDialog()
	{
		if (!(m_deckShareErrorPopup == null))
		{
			m_deckShareErrorPopup.Hide();
			m_deckShareErrorPopup = null;
		}
	}

	private void ShowFriendChallengeWaitingForOpponentDialog(string dialogText, AlertPopup.ResponseCallback callback)
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_text = GameStrings.Format(dialogText, FriendUtils.GetUniqueName(myOpponent));
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
		popupInfo.m_responseCallback = callback;
		DialogManager.Get().ShowPopup(popupInfo, OnFriendChallengeWaitingForOpponentDialogProcessed);
	}

	private bool OnFriendChallengeWaitingForOpponentDialogProcessed(DialogBase dialog, object userData)
	{
		if (!FriendChallengeMgr.Get().HasChallenge())
		{
			return false;
		}
		if (FriendChallengeMgr.Get().DidOpponentSelectDeckOrHero())
		{
			return false;
		}
		m_friendChallengeWaitingPopup = (AlertPopup)dialog;
		return true;
	}
}
