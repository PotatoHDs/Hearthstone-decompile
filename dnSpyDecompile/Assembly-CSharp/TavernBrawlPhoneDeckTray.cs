using System;
using PegasusShared;
using UnityEngine;

// Token: 0x0200073A RID: 1850
public class TavernBrawlPhoneDeckTray : BasePhoneDeckTray
{
	// Token: 0x060068BE RID: 26814 RVA: 0x00222345 File Offset: 0x00220545
	protected override void Awake()
	{
		base.Awake();
		this.m_RetireButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRetireClicked));
		TavernBrawlPhoneDeckTray.s_instance = this;
	}

	// Token: 0x060068BF RID: 26815 RVA: 0x0022236C File Offset: 0x0022056C
	private void OnDestroy()
	{
		TavernBrawlPhoneDeckTray.s_instance = null;
		CollectionManager.Get().ClearEditedDeck();
	}

	// Token: 0x060068C0 RID: 26816 RVA: 0x0022237E File Offset: 0x0022057E
	public static TavernBrawlPhoneDeckTray Get()
	{
		return TavernBrawlPhoneDeckTray.s_instance;
	}

	// Token: 0x060068C1 RID: 26817 RVA: 0x00222388 File Offset: 0x00220588
	public void Initialize()
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck != null)
		{
			this.OnTavernBrawlDeckInitialized(collectionDeck);
		}
	}

	// Token: 0x060068C2 RID: 26818 RVA: 0x002223AA File Offset: 0x002205AA
	private void OnTavernBrawlDeckInitialized(CollectionDeck tavernBrawlDeck)
	{
		if (tavernBrawlDeck == null)
		{
			Debug.LogError("Tavern Brawl deck is null.");
			return;
		}
		CollectionManager.Get().SetEditedDeck(tavernBrawlDeck, null);
		this.OnCardCountUpdated(tavernBrawlDeck.GetTotalCardCount());
		this.m_cardsContent.UpdateCardList();
	}

	// Token: 0x060068C3 RID: 26819 RVA: 0x002223E0 File Offset: 0x002205E0
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
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnRetireButtonConfirmationResponse);
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060068C4 RID: 26820 RVA: 0x0006FF44 File Offset: 0x0006E144
	private void OnRetireButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		Network.Get().TavernBrawlRetire();
	}

	// Token: 0x040055D7 RID: 21975
	[CustomEditField(Sections = "Buttons")]
	public StandardPegButtonNew m_RetireButton;

	// Token: 0x040055D8 RID: 21976
	private static TavernBrawlPhoneDeckTray s_instance;
}
