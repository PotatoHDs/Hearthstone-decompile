using System.Collections;
using PegasusShared;
using UnityEngine;

public class DeckOptionsMenu : MonoBehaviour
{
	public GameObject m_root;

	public UberText m_convertText;

	public PegUIElement m_renameButton;

	public PegUIElement m_deleteButton;

	public PegUIElement m_switchFormatButton;

	public PegUIElement m_retireButton;

	public DeckCopyPasteButton m_copyPasteDeckButton;

	public PegUIElement m_deckHelperButton;

	public GameObject m_top;

	public GameObject m_bottom;

	public HighlightState m_highlight;

	public Transform m_showBone;

	public Transform m_hideBone;

	public Transform[] m_buttonPositions;

	public Transform[] m_bottomPositions;

	public float[] m_topScales;

	private int m_buttonCount;

	private bool m_shown;

	private CollectionDeck m_deck;

	private CollectionDeckInfo m_deckInfo;

	private bool m_deleteButtonAlertBeingProcessed;

	public bool IsShown => m_shown;

	public void Awake()
	{
		m_root.SetActive(value: false);
		if (m_renameButton != null)
		{
			m_renameButton.AddEventListener(UIEventType.RELEASE, OnRenameButtonReleased);
		}
		if (m_deleteButton != null)
		{
			m_deleteButton.AddEventListener(UIEventType.RELEASE, OnDeleteButtonReleased);
		}
		if (m_switchFormatButton != null)
		{
			m_switchFormatButton.AddEventListener(UIEventType.RELEASE, OnSwitchFormatButtonReleased);
		}
		if (m_retireButton != null)
		{
			m_retireButton.AddEventListener(UIEventType.RELEASE, OnRetireButtonReleased);
		}
		if (m_copyPasteDeckButton != null)
		{
			m_copyPasteDeckButton.AddEventListener(UIEventType.RELEASE, OnCopyButtonReleased);
		}
		if (m_deckHelperButton != null)
		{
			m_deckHelperButton.AddEventListener(UIEventType.RELEASE, OnDeckHelperButtonReleased);
		}
	}

	public void Show()
	{
		if (!m_shown)
		{
			iTween.Stop(base.gameObject);
			m_root.SetActive(value: true);
			SetSwitchFormatText(m_deck.FormatType);
			UpdateLayout();
			if (m_buttonCount == 0)
			{
				m_root.SetActive(value: false);
				return;
			}
			Hashtable args = iTween.Hash("position", m_showBone.transform.position, "time", 0.35f, "easeType", iTween.EaseType.easeOutCubic, "oncomplete", "FinishShow", "oncompletetarget", base.gameObject);
			iTween.MoveTo(m_root, args);
			m_shown = true;
		}
	}

	private void FinishShow()
	{
	}

	public void Hide(bool animate = true)
	{
		if (m_shown)
		{
			iTween.Stop(base.gameObject);
			if (!animate)
			{
				m_root.SetActive(value: false);
				return;
			}
			m_root.SetActive(value: true);
			Hashtable args = iTween.Hash("position", m_hideBone.transform.position, "time", 0.35f, "easeType", iTween.EaseType.easeOutCubic, "oncomplete", "FinishHide", "oncompletetarget", base.gameObject);
			iTween.MoveTo(m_root, args);
			m_shown = false;
		}
	}

	private void FinishHide()
	{
		if (!m_shown)
		{
			m_root.SetActive(value: false);
		}
	}

	public void SetDeck(CollectionDeck deck)
	{
		m_deck = deck;
	}

	public void SetDeckInfo(CollectionDeckInfo deckInfo)
	{
		m_deckInfo = deckInfo;
	}

	private void OnRenameButtonReleased(UIEvent e)
	{
		m_deckInfo.Hide();
		CollectionDeckTray.Get().GetDecksContent().RenameCurrentlyEditingDeck();
	}

	private void OnDeleteButtonReleased(UIEvent e)
	{
		if (m_deleteButtonAlertBeingProcessed)
		{
			Debug.LogWarning("DeckOptionsMenu:OnDeleteButtonReleased: Called while a Delete button alert was already being processed");
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_HEADER");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_DESC");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = OnDeleteButtonConfirmationResponse;
		m_deckInfo.Hide();
		m_deleteButtonAlertBeingProcessed = true;
		DialogManager.Get().ShowPopup(popupInfo, OnDeleteButtonAlertPopupProcessed);
	}

	private bool OnDeleteButtonAlertPopupProcessed(DialogBase dialog, object userData)
	{
		m_deleteButtonAlertBeingProcessed = false;
		return true;
	}

	private void OnDeleteButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			CollectionDeckTray.Get().DeleteEditingDeck();
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.OnDoneEditingDeck();
			}
		}
	}

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
		popupInfo.m_responseCallback = OnRetireButtonConfirmationResponse;
		m_deckInfo.Hide();
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnRetireButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			Network.Get().TavernBrawlRetire();
		}
	}

	private void OnClosePressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private void OverOffClicker(UIEvent e)
	{
		Debug.Log("OverOffClicker");
		Hide();
	}

	private void OnSwitchFormatButtonReleased(UIEvent e)
	{
		StartCoroutine(SwitchFormat());
	}

	private IEnumerator SwitchFormat()
	{
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideConvertTutorial();
		}
		m_deckInfo.Hide();
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		TraySection editingTraySection = CollectionDeckTray.Get().GetDecksContent().GetEditingTraySection();
		switch (m_deck.FormatType)
		{
		case FormatType.FT_WILD:
			editingTraySection.m_deckFX.Play("DeckTraySectionCollectionDeck_WildGlowOut");
			yield return new WaitForSeconds(0.5f);
			break;
		case FormatType.FT_STANDARD:
			editingTraySection.m_deckFX.Play("DeckTraySectionCollectionDeck_StandardGlowOut");
			yield return new WaitForSeconds(0.5f);
			break;
		default:
			Debug.LogError("DeckOptionsMenu.SwitchFormat called with invalid deck format type " + m_deck.FormatType);
			break;
		}
		if (CollectionManager.Get().GetEditedDeck() != m_deck)
		{
			m_deck.FormatType = GetNextFormatType(m_deck.FormatType);
		}
		else
		{
			SetDeckFormat(GetNextFormatType(m_deck.FormatType));
		}
	}

	private FormatType GetNextFormatType(FormatType formatType)
	{
		switch (formatType)
		{
		case FormatType.FT_WILD:
			return FormatType.FT_STANDARD;
		case FormatType.FT_STANDARD:
			return FormatType.FT_WILD;
		default:
			Debug.LogError("DeckOptionsMenu.SwitchFormat called with invalid deck format type " + formatType);
			return formatType;
		}
	}

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
		m_deck.FormatType = formatType;
		editingDeckBox.SetFormatType(formatType);
		collectionManager.SetDeckRuleset(DeckRuleset.GetRuleset(formatType));
		CollectionManagerDisplay collectionManagerDisplay = collectionManager.GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.m_pageManager.RefreshCurrentPageContents(BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT);
			collectionManagerDisplay.UpdateSetFilters(formatType, editingDeck: true);
		}
		cardsContent.UpdateCardList();
		cardsContent.UpdateTileVisuals();
		if (collectionManagerDisplay != null && formatType != FormatType.FT_WILD && collectionManager.ShouldShowWildToStandardTutorial())
		{
			collectionManagerDisplay.ShowStandardInfoTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
	}

	private void SetSwitchFormatText(FormatType formatType)
	{
		if (formatType != FormatType.FT_CLASSIC)
		{
			FormatType nextFormatType = GetNextFormatType(formatType);
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
			}.TryGetValue(nextFormatType, out var value))
			{
				m_convertText.Text = GameStrings.Get(value);
				return;
			}
			Debug.LogError("DeckOptionsMenu.SetSwitchFormatText called with unsupported next format type " + nextFormatType);
			m_convertText.Text = nextFormatType.ToString();
		}
	}

	private void OnDeckHelperButtonReleased(UIEvent e)
	{
		m_deckInfo.Hide();
		DeckTrayDeckTileVisual firstInvalidCard = CollectionDeckTray.Get().GetCardsContent().GetFirstInvalidCard();
		CollectionDeckTray.Get().GetCardsContent().ShowDeckHelper(firstInvalidCard, continueAfterReplace: true, firstInvalidCard != null);
	}

	private void UpdateLayout()
	{
		int buttonCount = GetButtonCount();
		if (buttonCount != m_buttonCount)
		{
			m_buttonCount = buttonCount;
			UpdateBackground();
		}
		UpdateButtons();
	}

	private void UpdateBackground()
	{
		if (m_buttonCount != 0)
		{
			float z = m_topScales[m_buttonCount - 1];
			m_top.transform.transform.localScale = new Vector3(1f, 1f, z);
			m_bottom.transform.transform.position = m_bottomPositions[m_buttonCount - 1].position;
		}
	}

	private void UpdateButtons()
	{
		int num = 0;
		bool flag = ShowConvertButton();
		bool flag2 = ShowRenameButton();
		bool flag3 = ShowDeleteButton();
		bool flag4 = ShowCopyPasteDeckButton();
		bool flag5 = ShowRetireButton();
		bool flag6 = ShowDeckHelperButton();
		m_switchFormatButton.gameObject.SetActive(flag);
		if (flag)
		{
			if (m_deck.FormatType == FormatType.FT_WILD && m_highlight != null && CollectionManager.Get().ShouldShowWildToStandardTutorial())
			{
				m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			m_switchFormatButton.transform.position = m_buttonPositions[num].position;
			num++;
		}
		m_renameButton.gameObject.SetActive(flag2);
		if (flag2)
		{
			m_renameButton.transform.position = m_buttonPositions[num].position;
			num++;
		}
		m_copyPasteDeckButton.gameObject.SetActive(flag4);
		if (flag4)
		{
			m_copyPasteDeckButton.transform.position = m_buttonPositions[num].position;
			num++;
		}
		m_deckHelperButton.gameObject.SetActive(flag6);
		if (flag6)
		{
			m_deckHelperButton.transform.position = m_buttonPositions[num].position;
			num++;
		}
		m_deleteButton.gameObject.SetActive(flag3);
		if (flag3)
		{
			m_deleteButton.transform.position = m_buttonPositions[num].position;
			num++;
		}
		m_retireButton.gameObject.SetActive(flag5);
		if (flag5)
		{
			m_retireButton.transform.position = m_buttonPositions[num].position;
			num++;
		}
	}

	private int GetButtonCount()
	{
		return 0 + (ShowRenameButton() ? 1 : 0) + (ShowDeleteButton() ? 1 : 0) + (ShowConvertButton() ? 1 : 0) + (ShowCopyPasteDeckButton() ? 1 : 0) + (ShowRetireButton() ? 1 : 0) + (ShowDeckHelperButton() ? 1 : 0);
	}

	private bool ShowCopyPasteDeckButton()
	{
		if (ShowCopyDeckButton())
		{
			SetUpCopyButton();
			return true;
		}
		return false;
	}

	private void SetUpCopyButton()
	{
		m_copyPasteDeckButton.ButtonText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_COPY");
		m_copyPasteDeckButton.TooltipHeaderString = GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_HEADLINE");
	}

	private void OnCopyButtonReleased(UIEvent e)
	{
		if (m_copyPasteDeckButton.IsClickEnabled())
		{
			m_deckInfo.Hide();
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			ClipboardUtils.CopyToClipboard(editedDeck.GetShareableDeck().Serialize());
			UIStatus.Get().AddInfo(GameStrings.Get("GLUE_COLLECTION_DECK_COPIED_TOAST"));
			TelemetryManager.Client().SendDeckCopied(editedDeck.ID, editedDeck.GetShareableDeck().Serialize(includeComments: false));
		}
	}

	private bool ShowCopyDeckButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		m_copyPasteDeckButton.TooltipMessage = string.Empty;
		if (editedDeck.GetTotalCardCount() == 0)
		{
			return false;
		}
		DeckRuleViolation topViolation;
		bool flag = editedDeck.CanCopyAsShareableDeck(out topViolation);
		m_copyPasteDeckButton.SetEnabled(flag);
		m_copyPasteDeckButton.TooltipMessage = CollectionDeck.GetUserFriendlyCopyErrorMessageFromDeckRuleViolation(topViolation);
		return true;
	}

	private bool ShowRenameButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null && editedDeck.Locked)
		{
			return false;
		}
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			return false;
		}
		return UniversalInputManager.Get().IsTouchMode();
	}

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
		if (editedDeck != null && editedDeck.FormatType == FormatType.FT_CLASSIC)
		{
			return false;
		}
		return true;
	}

	private bool ShowDeckHelperButton()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null && editedDeck.Locked)
		{
			return false;
		}
		if (editedDeck.GetTotalValidCardCount() >= CollectionManager.Get().GetDeckSize())
		{
			return false;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			return false;
		}
		if (!DeckHelper.HasChoicesToOffer(editedDeck))
		{
			return false;
		}
		return true;
	}
}
