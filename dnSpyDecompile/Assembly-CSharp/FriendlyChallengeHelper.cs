using System;
using bgs;

// Token: 0x020002F7 RID: 759
public class FriendlyChallengeHelper
{
	// Token: 0x170004FD RID: 1277
	// (get) Token: 0x0600284C RID: 10316 RVA: 0x000CAB8F File Offset: 0x000C8D8F
	// (set) Token: 0x0600284D RID: 10317 RVA: 0x000CAB97 File Offset: 0x000C8D97
	public BnetAccountId ActiveChallengeMenu { get; set; }

	// Token: 0x0600284E RID: 10318 RVA: 0x000CABA0 File Offset: 0x000C8DA0
	public static FriendlyChallengeHelper Get()
	{
		if (FriendlyChallengeHelper.s_instance == null)
		{
			FriendlyChallengeHelper.s_instance = new FriendlyChallengeHelper();
		}
		return FriendlyChallengeHelper.s_instance;
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x000CABB8 File Offset: 0x000C8DB8
	public void StartChallengeOrWaitForOpponent(string waitingDialogText, AlertPopup.ResponseCallback waitingCallback)
	{
		if (!FriendChallengeMgr.Get().DidOpponentSelectDeckOrHero())
		{
			this.ShowFriendChallengeWaitingForOpponentDialog(waitingDialogText, waitingCallback);
		}
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x000CABCE File Offset: 0x000C8DCE
	public void HideFriendChallengeWaitingForOpponentDialog()
	{
		if (this.m_friendChallengeWaitingPopup == null)
		{
			return;
		}
		this.m_friendChallengeWaitingPopup.Hide();
		this.m_friendChallengeWaitingPopup = null;
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x000CABF4 File Offset: 0x000C8DF4
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

	// Token: 0x06002852 RID: 10322 RVA: 0x000CAC57 File Offset: 0x000C8E57
	public void StopWaitingForFriendChallenge()
	{
		this.HideFriendChallengeWaitingForOpponentDialog();
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x000CAC5F File Offset: 0x000C8E5F
	public void HideAllDeckShareDialogs()
	{
		this.HideDeckShareRequestDialog();
		this.HideDeckShareRequestCanceledDialog();
		this.HideDeckShareRequestDeclinedDialog();
		this.HideDeckShareRequestWaitingDialog();
		this.HideDeckShareErrorDialog();
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x000CAC80 File Offset: 0x000C8E80
	public void ShowDeckShareRequestCanceledDialog()
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUEST_CANCELED", new object[]
		{
			myOpponent.GetBestName()
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			this.m_deckShareRequestCanceledPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x000CACF4 File Offset: 0x000C8EF4
	public void HideDeckShareRequestCanceledDialog()
	{
		if (this.m_deckShareRequestCanceledPopup == null)
		{
			return;
		}
		this.m_deckShareRequestCanceledPopup.Hide();
		this.m_deckShareRequestCanceledPopup = null;
	}

	// Token: 0x06002856 RID: 10326 RVA: 0x000CAD18 File Offset: 0x000C8F18
	public void ShowDeckShareRequestDeclinedDialog()
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUEST_DECLINED", new object[]
		{
			myOpponent.GetBestName()
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			this.m_deckShareRequestDeclinedPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x06002857 RID: 10327 RVA: 0x000CAD8C File Offset: 0x000C8F8C
	public void HideDeckShareRequestDeclinedDialog()
	{
		if (this.m_deckShareRequestDeclinedPopup == null)
		{
			return;
		}
		this.m_deckShareRequestDeclinedPopup.Hide();
		this.m_deckShareRequestDeclinedPopup = null;
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x000CADB0 File Offset: 0x000C8FB0
	public void ShowDeckShareRequestWaitingDialog(AlertPopup.ResponseCallback waitingCallback)
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUEST_WAITING_RESPONSE", new object[]
		{
			myOpponent.GetBestName()
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
		popupInfo.m_responseCallback = waitingCallback;
		DialogManager.DialogProcessCallback callback = delegate(DialogBase dialog, object userData)
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				return false;
			}
			this.m_deckShareRequestWaitingPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x06002859 RID: 10329 RVA: 0x000CAE2B File Offset: 0x000C902B
	public void HideDeckShareRequestWaitingDialog()
	{
		if (this.m_deckShareRequestWaitingPopup == null)
		{
			return;
		}
		this.m_deckShareRequestWaitingPopup.Hide();
		this.m_deckShareRequestWaitingPopup = null;
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x000CAE50 File Offset: 0x000C9050
	public void ShowDeckShareRequestDialog(AlertPopup.ResponseCallback waitingCallback)
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DECK_SHARE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_DECK_SHARE_REQUESTED", new object[]
		{
			myOpponent.GetBestName()
		});
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
			this.m_deckShareRequestPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x000CAEEB File Offset: 0x000C90EB
	public bool IsShowingDeckShareRequestDialog()
	{
		return this.m_deckShareRequestPopup != null;
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x000CAEF9 File Offset: 0x000C90F9
	public void HideDeckShareRequestDialog()
	{
		if (this.m_deckShareRequestPopup == null)
		{
			return;
		}
		this.m_deckShareRequestPopup.Hide();
		this.m_deckShareRequestPopup = null;
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x000CAF1C File Offset: 0x000C911C
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
			this.m_deckShareErrorPopup = (AlertPopup)dialog;
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x000CAF76 File Offset: 0x000C9176
	public void HideDeckShareErrorDialog()
	{
		if (this.m_deckShareErrorPopup == null)
		{
			return;
		}
		this.m_deckShareErrorPopup.Hide();
		this.m_deckShareErrorPopup = null;
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x000CAF9C File Offset: 0x000C919C
	private void ShowFriendChallengeWaitingForOpponentDialog(string dialogText, AlertPopup.ResponseCallback callback)
	{
		BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_text = GameStrings.Format(dialogText, new object[]
		{
			FriendUtils.GetUniqueName(myOpponent)
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
		popupInfo.m_responseCallback = callback;
		DialogManager.Get().ShowPopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnFriendChallengeWaitingForOpponentDialogProcessed));
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x000CB001 File Offset: 0x000C9201
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
		this.m_friendChallengeWaitingPopup = (AlertPopup)dialog;
		return true;
	}

	// Token: 0x040016E7 RID: 5863
	private static FriendlyChallengeHelper s_instance;

	// Token: 0x040016E8 RID: 5864
	private AlertPopup m_friendChallengeWaitingPopup;

	// Token: 0x040016E9 RID: 5865
	private AlertPopup m_deckShareRequestWaitingPopup;

	// Token: 0x040016EA RID: 5866
	private AlertPopup m_deckShareRequestDeclinedPopup;

	// Token: 0x040016EB RID: 5867
	private AlertPopup m_deckShareRequestCanceledPopup;

	// Token: 0x040016EC RID: 5868
	private AlertPopup m_deckShareRequestPopup;

	// Token: 0x040016ED RID: 5869
	private AlertPopup m_deckShareErrorPopup;
}
