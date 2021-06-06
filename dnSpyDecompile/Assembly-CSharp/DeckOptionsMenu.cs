using System;
using System.Collections;
using PegasusShared;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class DeckOptionsMenu : MonoBehaviour
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x0600136A RID: 4970 RVA: 0x0006FAE5 File Offset: 0x0006DCE5
	public bool IsShown
	{
		get
		{
			return this.m_shown;
		}
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
	public void Awake()
	{
		this.m_root.SetActive(false);
		if (this.m_renameButton != null)
		{
			this.m_renameButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRenameButtonReleased));
		}
		if (this.m_deleteButton != null)
		{
			this.m_deleteButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeleteButtonReleased));
		}
		if (this.m_switchFormatButton != null)
		{
			this.m_switchFormatButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSwitchFormatButtonReleased));
		}
		if (this.m_retireButton != null)
		{
			this.m_retireButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRetireButtonReleased));
		}
		if (this.m_copyPasteDeckButton != null)
		{
			this.m_copyPasteDeckButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCopyButtonReleased));
		}
		if (this.m_deckHelperButton != null)
		{
			this.m_deckHelperButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeckHelperButtonReleased));
		}
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x0006FBF4 File Offset: 0x0006DDF4
	public void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		iTween.Stop(base.gameObject);
		this.m_root.SetActive(true);
		this.SetSwitchFormatText(this.m_deck.FormatType);
		this.UpdateLayout();
		if (this.m_buttonCount == 0)
		{
			this.m_root.SetActive(false);
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_showBone.transform.position,
			"time",
			0.35f,
			"easeType",
			iTween.EaseType.easeOutCubic,
			"oncomplete",
			"FinishShow",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_root, args);
		this.m_shown = true;
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void FinishShow()
	{
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x0006FCD8 File Offset: 0x0006DED8
	public void Hide(bool animate = true)
	{
		if (!this.m_shown)
		{
			return;
		}
		iTween.Stop(base.gameObject);
		if (!animate)
		{
			this.m_root.SetActive(false);
			return;
		}
		this.m_root.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_hideBone.transform.position,
			"time",
			0.35f,
			"easeType",
			iTween.EaseType.easeOutCubic,
			"oncomplete",
			"FinishHide",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_root, args);
		this.m_shown = false;
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x0006FD9D File Offset: 0x0006DF9D
	private void FinishHide()
	{
		if (!this.m_shown)
		{
			this.m_root.SetActive(false);
		}
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x0006FDB3 File Offset: 0x0006DFB3
	public void SetDeck(CollectionDeck deck)
	{
		this.m_deck = deck;
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x0006FDBC File Offset: 0x0006DFBC
	public void SetDeckInfo(CollectionDeckInfo deckInfo)
	{
		this.m_deckInfo = deckInfo;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x0006FDC5 File Offset: 0x0006DFC5
	private void OnRenameButtonReleased(UIEvent e)
	{
		this.m_deckInfo.Hide();
		CollectionDeckTray.Get().GetDecksContent().RenameCurrentlyEditingDeck();
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x0006FDE4 File Offset: 0x0006DFE4
	private void OnDeleteButtonReleased(UIEvent e)
	{
		if (this.m_deleteButtonAlertBeingProcessed)
		{
			Debug.LogWarning("DeckOptionsMenu:OnDeleteButtonReleased: Called while a Delete button alert was already being processed");
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_HEADER");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_DESC");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnDeleteButtonConfirmationResponse);
		this.m_deckInfo.Hide();
		this.m_deleteButtonAlertBeingProcessed = true;
		DialogManager.Get().ShowPopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnDeleteButtonAlertPopupProcessed));
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x0006FE73 File Offset: 0x0006E073
	private bool OnDeleteButtonAlertPopupProcessed(DialogBase dialog, object userData)
	{
		this.m_deleteButtonAlertBeingProcessed = false;
		return true;
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x0006FE80 File Offset: 0x0006E080
	private void OnDeleteButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		CollectionDeckTray.Get().DeleteEditingDeck(true);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.OnDoneEditingDeck();
		}
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0006FEBC File Offset: 0x0006E0BC
	private void OnRetireButtonReleased(UIEvent e)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_headerText = GameStrings.Get("GLUE_TAVERN_BRAWL_RETIRE_CONFIRM_HEADER");
		if (TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC)
		{
			popupInfo.m_text = GameStrings.Get("GLUE_TAVERN_BRAWL_RETIRE_CONFIRM_DESC");
		}
		else
		{
			popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_RETIRE_CONFIRM_DESC");
		}
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnRetireButtonConfirmationResponse);
		this.m_deckInfo.Hide();
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x0006FF44 File Offset: 0x0006E144
	private void OnRetireButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		Network.Get().TavernBrawlRetire();
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnClosePressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0006FF55 File Offset: 0x0006E155
	private void OverOffClicker(UIEvent e)
	{
		Debug.Log("OverOffClicker");
		this.Hide(true);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x0006FF68 File Offset: 0x0006E168
	private void OnSwitchFormatButtonReleased(UIEvent e)
	{
		base.StartCoroutine(this.SwitchFormat());
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x0006FF77 File Offset: 0x0006E177
	private IEnumerator SwitchFormat()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideConvertTutorial();
		}
		this.m_deckInfo.Hide();
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		TraySection editingTraySection = CollectionDeckTray.Get().GetDecksContent().GetEditingTraySection();
		FormatType formatType = this.m_deck.FormatType;
		if (formatType != FormatType.FT_WILD)
		{
			if (formatType != FormatType.FT_STANDARD)
			{
				Debug.LogError("DeckOptionsMenu.SwitchFormat called with invalid deck format type " + this.m_deck.FormatType.ToString());
			}
			else
			{
				editingTraySection.m_deckFX.Play("DeckTraySectionCollectionDeck_StandardGlowOut");
				yield return new WaitForSeconds(0.5f);
			}
		}
		else
		{
			editingTraySection.m_deckFX.Play("DeckTraySectionCollectionDeck_WildGlowOut");
			yield return new WaitForSeconds(0.5f);
		}
		if (CollectionManager.Get().GetEditedDeck() != this.m_deck)
		{
			this.m_deck.FormatType = this.GetNextFormatType(this.m_deck.FormatType);
			yield break;
		}
		this.SetDeckFormat(this.GetNextFormatType(this.m_deck.FormatType));
		yield break;
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x0006FF86 File Offset: 0x0006E186
	private FormatType GetNextFormatType(FormatType formatType)
	{
		if (formatType == FormatType.FT_WILD)
		{
			return FormatType.FT_STANDARD;
		}
		if (formatType != FormatType.FT_STANDARD)
		{
			Debug.LogError("DeckOptionsMenu.SwitchFormat called with invalid deck format type " + formatType.ToString());
			return formatType;
		}
		return FormatType.FT_WILD;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x0006FFB4 File Offset: 0x0006E1B4
	private void SetDeckFormat(FormatType formatType)
	{
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray == null)
		{
			Debug.LogError("DeckOptionsMenu.SetDeckFormat: CollectionDeckTray.Get() returned null");
			return;
		}
		DeckTrayCardListContent cardsContent = collectionDeckTray.GetCardsContent();
		if (cardsContent == null)
		{
			Debug.LogError("DeckOptionsMenu.SetDeckFormat: collectionDeckTray.GetCardsContent() returned null");
			return;
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager == null)
		{
			Debug.LogError("DeckOptionsMenu.SetDeckFormat: CollectionManager.Get() returned null");
			return;
		}
		CollectionDeckBoxVisual editingDeckBox = collectionDeckTray.GetEditingDeckBox();
		if (editingDeckBox == null)
		{
			Debug.LogError("DeckOptionsMenu.SetDeckFormat: collectionDeckTray.GetEditingDeckBox() returned null");
			return;
		}
		this.m_deck.FormatType = formatType;
		editingDeckBox.SetFormatType(formatType);
		collectionManager.SetDeckRuleset(DeckRuleset.GetRuleset(formatType));
		CollectionManagerDisplay collectionManagerDisplay = collectionManager.GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.m_pageManager.RefreshCurrentPageContents(BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT);
			collectionManagerDisplay.UpdateSetFilters(formatType, true, false);
		}
		cardsContent.UpdateCardList();
		cardsContent.UpdateTileVisuals();
		if (collectionManagerDisplay != null && formatType != FormatType.FT_WILD && collectionManager.ShouldShowWildToStandardTutorial(true))
		{
			collectionManagerDisplay.ShowStandardInfoTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000700A0 File Offset: 0x0006E2A0
	private void SetSwitchFormatText(FormatType formatType)
	{
		if (formatType == FormatType.FT_CLASSIC)
		{
			return;
		}
		FormatType nextFormatType = this.GetNextFormatType(formatType);
		string key;
		if (new Map<FormatType, string>
		{
			{
				FormatType.FT_STANDARD,
				"GLUE_COLLECTION_TO_STANDARD"
			},
			{
				FormatType.FT_WILD,
				"GLUE_COLLECTION_TO_WILD"
			}
		}.TryGetValue(nextFormatType, out key))
		{
			this.m_convertText.Text = GameStrings.Get(key);
			return;
		}
		Debug.LogError("DeckOptionsMenu.SetSwitchFormatText called with unsupported next format type " + nextFormatType.ToString());
		this.m_convertText.Text = nextFormatType.ToString();
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x00070128 File Offset: 0x0006E328
	private void OnDeckHelperButtonReleased(UIEvent e)
	{
		this.m_deckInfo.Hide();
		DeckTrayDeckTileVisual firstInvalidCard = CollectionDeckTray.Get().GetCardsContent().GetFirstInvalidCard();
		CollectionDeckTray.Get().GetCardsContent().ShowDeckHelper(firstInvalidCard, true, firstInvalidCard != null);
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x00070168 File Offset: 0x0006E368
	private void UpdateLayout()
	{
		int buttonCount = this.GetButtonCount();
		if (buttonCount != this.m_buttonCount)
		{
			this.m_buttonCount = buttonCount;
			this.UpdateBackground();
		}
		this.UpdateButtons();
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x00070198 File Offset: 0x0006E398
	private void UpdateBackground()
	{
		if (this.m_buttonCount == 0)
		{
			return;
		}
		float z = this.m_topScales[this.m_buttonCount - 1];
		this.m_top.transform.transform.localScale = new Vector3(1f, 1f, z);
		this.m_bottom.transform.transform.position = this.m_bottomPositions[this.m_buttonCount - 1].position;
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x0007020C File Offset: 0x0006E40C
	private void UpdateButtons()
	{
		int num = 0;
		bool flag = this.ShowConvertButton();
		bool flag2 = this.ShowRenameButton();
		bool flag3 = this.ShowDeleteButton();
		bool flag4 = this.ShowCopyPasteDeckButton();
		bool flag5 = this.ShowRetireButton();
		bool flag6 = this.ShowDeckHelperButton();
		this.m_switchFormatButton.gameObject.SetActive(flag);
		if (flag)
		{
			if (this.m_deck.FormatType == FormatType.FT_WILD && this.m_highlight != null && CollectionManager.Get().ShouldShowWildToStandardTutorial(true))
			{
				this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			this.m_switchFormatButton.transform.position = this.m_buttonPositions[num].position;
			num++;
		}
		this.m_renameButton.gameObject.SetActive(flag2);
		if (flag2)
		{
			this.m_renameButton.transform.position = this.m_buttonPositions[num].position;
			num++;
		}
		this.m_copyPasteDeckButton.gameObject.SetActive(flag4);
		if (flag4)
		{
			this.m_copyPasteDeckButton.transform.position = this.m_buttonPositions[num].position;
			num++;
		}
		this.m_deckHelperButton.gameObject.SetActive(flag6);
		if (flag6)
		{
			this.m_deckHelperButton.transform.position = this.m_buttonPositions[num].position;
			num++;
		}
		this.m_deleteButton.gameObject.SetActive(flag3);
		if (flag3)
		{
			this.m_deleteButton.transform.position = this.m_buttonPositions[num].position;
			num++;
		}
		this.m_retireButton.gameObject.SetActive(flag5);
		if (flag5)
		{
			this.m_retireButton.transform.position = this.m_buttonPositions[num].position;
			num++;
		}
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000703C4 File Offset: 0x0006E5C4
	private int GetButtonCount()
	{
		return 0 + (this.ShowRenameButton() ? 1 : 0) + (this.ShowDeleteButton() ? 1 : 0) + (this.ShowConvertButton() ? 1 : 0) + (this.ShowCopyPasteDeckButton() ? 1 : 0) + (this.ShowRetireButton() ? 1 : 0) + (this.ShowDeckHelperButton() ? 1 : 0);
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x00070420 File Offset: 0x0006E620
	private bool ShowCopyPasteDeckButton()
	{
		if (this.ShowCopyDeckButton())
		{
			this.SetUpCopyButton();
			return true;
		}
		return false;
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x00070433 File Offset: 0x0006E633
	private void SetUpCopyButton()
	{
		this.m_copyPasteDeckButton.ButtonText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_COPY");
		this.m_copyPasteDeckButton.TooltipHeaderString = GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_HEADLINE");
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x00070464 File Offset: 0x0006E664
	private void OnCopyButtonReleased(UIEvent e)
	{
		if (!this.m_copyPasteDeckButton.IsClickEnabled())
		{
			return;
		}
		this.m_deckInfo.Hide();
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		ClipboardUtils.CopyToClipboard(editedDeck.GetShareableDeck().Serialize(true));
		UIStatus.Get().AddInfo(GameStrings.Get("GLUE_COLLECTION_DECK_COPIED_TOAST"));
		TelemetryManager.Client().SendDeckCopied(editedDeck.ID, editedDeck.GetShareableDeck().Serialize(false));
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000704D8 File Offset: 0x0006E6D8
	private bool ShowCopyDeckButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		this.m_copyPasteDeckButton.TooltipMessage = string.Empty;
		if (editedDeck.GetTotalCardCount() == 0)
		{
			return false;
		}
		DeckRuleViolation violation;
		bool enabled = editedDeck.CanCopyAsShareableDeck(out violation);
		this.m_copyPasteDeckButton.SetEnabled(enabled, false);
		this.m_copyPasteDeckButton.TooltipMessage = CollectionDeck.GetUserFriendlyCopyErrorMessageFromDeckRuleViolation(violation);
		return true;
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x00070534 File Offset: 0x0006E734
	private bool ShowRenameButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		return (editedDeck == null || !editedDeck.Locked) && !SceneMgr.Get().IsInTavernBrawlMode() && UniversalInputManager.Get().IsTouchMode();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x00070574 File Offset: 0x0006E774
	private bool ShowDeleteButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null && editedDeck.Locked)
		{
			return false;
		}
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			return UniversalInputManager.UsePhoneUI;
		}
		return UniversalInputManager.Get().IsTouchMode();
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000705BC File Offset: 0x0006E7BC
	private bool ShowRetireButton()
	{
		if (SceneMgr.Get().IsInTavernBrawlMode() && TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay != null && !tavernBrawlDisplay.IsInDeckEditMode())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000705FC File Offset: 0x0006E7FC
	private bool ShowConvertButton()
	{
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			return false;
		}
		if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			return false;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			return false;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		return editedDeck == null || editedDeck.FormatType != FormatType.FT_CLASSIC;
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x00070654 File Offset: 0x0006E854
	private bool ShowDeckHelperButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		return (editedDeck == null || !editedDeck.Locked) && editedDeck.GetTotalValidCardCount() < CollectionManager.Get().GetDeckSize() && CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.DECK_TEMPLATE && DeckHelper.HasChoicesToOffer(editedDeck);
	}

	// Token: 0x04000CAD RID: 3245
	public GameObject m_root;

	// Token: 0x04000CAE RID: 3246
	public UberText m_convertText;

	// Token: 0x04000CAF RID: 3247
	public PegUIElement m_renameButton;

	// Token: 0x04000CB0 RID: 3248
	public PegUIElement m_deleteButton;

	// Token: 0x04000CB1 RID: 3249
	public PegUIElement m_switchFormatButton;

	// Token: 0x04000CB2 RID: 3250
	public PegUIElement m_retireButton;

	// Token: 0x04000CB3 RID: 3251
	public DeckCopyPasteButton m_copyPasteDeckButton;

	// Token: 0x04000CB4 RID: 3252
	public PegUIElement m_deckHelperButton;

	// Token: 0x04000CB5 RID: 3253
	public GameObject m_top;

	// Token: 0x04000CB6 RID: 3254
	public GameObject m_bottom;

	// Token: 0x04000CB7 RID: 3255
	public HighlightState m_highlight;

	// Token: 0x04000CB8 RID: 3256
	public Transform m_showBone;

	// Token: 0x04000CB9 RID: 3257
	public Transform m_hideBone;

	// Token: 0x04000CBA RID: 3258
	public Transform[] m_buttonPositions;

	// Token: 0x04000CBB RID: 3259
	public Transform[] m_bottomPositions;

	// Token: 0x04000CBC RID: 3260
	public float[] m_topScales;

	// Token: 0x04000CBD RID: 3261
	private int m_buttonCount;

	// Token: 0x04000CBE RID: 3262
	private bool m_shown;

	// Token: 0x04000CBF RID: 3263
	private CollectionDeck m_deck;

	// Token: 0x04000CC0 RID: 3264
	private CollectionDeckInfo m_deckInfo;

	// Token: 0x04000CC1 RID: 3265
	private bool m_deleteButtonAlertBeingProcessed;
}
