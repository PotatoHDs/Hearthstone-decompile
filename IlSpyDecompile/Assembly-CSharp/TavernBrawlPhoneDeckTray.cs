using PegasusShared;
using UnityEngine;

public class TavernBrawlPhoneDeckTray : BasePhoneDeckTray
{
	[CustomEditField(Sections = "Buttons")]
	public StandardPegButtonNew m_RetireButton;

	private static TavernBrawlPhoneDeckTray s_instance;

	protected override void Awake()
	{
		base.Awake();
		m_RetireButton.AddEventListener(UIEventType.RELEASE, OnRetireClicked);
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
		CollectionManager.Get().ClearEditedDeck();
	}

	public static TavernBrawlPhoneDeckTray Get()
	{
		return s_instance;
	}

	public void Initialize()
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck != null)
		{
			OnTavernBrawlDeckInitialized(collectionDeck);
		}
	}

	private void OnTavernBrawlDeckInitialized(CollectionDeck tavernBrawlDeck)
	{
		if (tavernBrawlDeck == null)
		{
			Debug.LogError("Tavern Brawl deck is null.");
			return;
		}
		CollectionManager.Get().SetEditedDeck(tavernBrawlDeck);
		OnCardCountUpdated(tavernBrawlDeck.GetTotalCardCount());
		m_cardsContent.UpdateCardList();
	}

	private void OnRetireClicked(UIEvent e)
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
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnRetireButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			Network.Get().TavernBrawlRetire();
		}
	}
}
