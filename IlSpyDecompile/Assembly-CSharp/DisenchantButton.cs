using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using UnityEngine;

public class DisenchantButton : CraftingButton
{
	private string m_lastwarnedCard;

	private List<AlertPopup.PopupInfo> PendingDisenchantWarnings = new List<AlertPopup.PopupInfo>();

	public override void EnableButton()
	{
		if (CraftingManager.Get().GetNumClientTransactions() > 0)
		{
			base.EnterUndoMode();
			return;
		}
		labelText.Text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT");
		base.EnableButton();
	}

	protected override void OnRelease()
	{
		if (!Network.IsLoggedIn())
		{
			CollectionManager.ShowFeatureDisabledWhileOfflinePopup();
		}
		else if (CraftingManager.Get().GetPendingServerTransaction() == null)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				GetComponent<Animation>().Play("CardExchange_ButtonPress1_phone");
			}
			else
			{
				GetComponent<Animation>().Play("CardExchange_ButtonPress1");
			}
			if (CraftingManager.Get().GetNumClientTransactions() > 0)
			{
				DoDisenchant();
			}
			else
			{
				CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(OnReadyToStartDisenchant);
			}
		}
	}

	private void OnReadyToStartDisenchant()
	{
		if (!CraftingManager.Get().IsCardShowing())
		{
			return;
		}
		EntityDef entityDef = CraftingManager.Get().GetShownActor().GetEntityDef();
		string cardId = entityDef.GetCardId();
		List<string> postDisenchantInvalidDeckNames = GetPostDisenchantInvalidDeckNames();
		if (postDisenchantInvalidDeckNames.Count == 0)
		{
			int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending(TAG_PREMIUM.GOLDEN);
			int numOwnedIncludePending2 = CraftingManager.Get().GetNumOwnedIncludePending(TAG_PREMIUM.NORMAL);
			int num = numOwnedIncludePending + numOwnedIncludePending2;
			if (CraftingManager.Get().GetNumClientTransactions() <= 0 && m_lastwarnedCard != cardId && ((!entityDef.IsElite() && num <= 2) || (entityDef.IsElite() && num <= 1)))
			{
				m_lastwarnedCard = cardId;
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_HEADER");
				popupInfo.m_text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM2_DESC");
				popupInfo.m_showAlertIcon = true;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				popupInfo.m_responseCallback = OnConfirmDisenchantResponse;
				PendingDisenchantWarnings.Add(popupInfo);
			}
		}
		else
		{
			string text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_DESC");
			foreach (string item in postDisenchantInvalidDeckNames)
			{
				text = text + "\n" + item;
			}
			AlertPopup.PopupInfo popupInfo2 = new AlertPopup.PopupInfo();
			popupInfo2.m_headerText = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_HEADER");
			popupInfo2.m_text = text;
			popupInfo2.m_showAlertIcon = false;
			popupInfo2.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo2.m_responseCallback = OnConfirmDisenchantResponse;
			PendingDisenchantWarnings.Add(popupInfo2);
		}
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardId);
		if ((from x in CollectionManager.Get().GetOwnedCards()
			where GameUtils.IsClassicCardSet(x.Set) && x.GetEntityDef() != null && x.GetEntityDef().GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID) == cardDbId
			select x).Any())
		{
			AlertPopup.PopupInfo popupInfo3 = new AlertPopup.PopupInfo();
			popupInfo3.m_headerText = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_HEADER");
			popupInfo3.m_text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM3_DESC");
			popupInfo3.m_showAlertIcon = true;
			popupInfo3.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo3.m_responseCallback = OnConfirmDisenchantResponse;
			PendingDisenchantWarnings.Add(popupInfo3);
		}
		if (PendingDisenchantWarnings.Count > 0)
		{
			ShowNextDisenchantWarning();
		}
		else
		{
			DoDisenchant();
		}
	}

	private void ShowNextDisenchantWarning()
	{
		if (PendingDisenchantWarnings.Count != 0)
		{
			AlertPopup.PopupInfo info = PendingDisenchantWarnings[0];
			PendingDisenchantWarnings.RemoveAt(0);
			DialogManager.Get().ShowPopup(info);
		}
	}

	private void OnConfirmDisenchantResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			PendingDisenchantWarnings.Clear();
		}
		else if (PendingDisenchantWarnings.Count > 0)
		{
			ShowNextDisenchantWarning();
		}
		else
		{
			DoDisenchant();
		}
	}

	private void DoDisenchant()
	{
		CraftingManager.Get().DisenchantButtonPressed();
	}

	private List<string> GetPostDisenchantInvalidDeckNames(bool includeDuelsDecks = false)
	{
		Actor shownActor = CraftingManager.Get().GetShownActor();
		string cardId = shownActor.GetEntityDef().GetCardId();
		TAG_PREMIUM premium = shownActor.GetPremium();
		int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending();
		int num = Mathf.Max(0, numOwnedIncludePending - 1);
		int fixedRewardCounterpartCardID = GameUtils.GetFixedRewardCounterpartCardID(GameUtils.TranslateCardIdToDbId(cardId));
		bool flag = fixedRewardCounterpartCardID != 0 && GameUtils.IsClassicCard(fixedRewardCounterpartCardID);
		SortedDictionary<long, CollectionDeck> decks = CollectionManager.Get().GetDecks();
		List<string> list = new List<string>();
		foreach (CollectionDeck value in decks.Values)
		{
			int cardCountFirstMatchingSlot = value.GetCardCountFirstMatchingSlot(cardId, premium);
			if (fixedRewardCounterpartCardID > 0 && flag)
			{
				cardCountFirstMatchingSlot = value.GetCardCountFirstMatchingSlot(GameUtils.TranslateDbIdToCardId(fixedRewardCounterpartCardID), premium);
			}
			if (cardCountFirstMatchingSlot > num && !value.Locked && (value.Type != DeckType.PVPDR_DECK || includeDuelsDecks))
			{
				list.Add(value.Name);
				Log.CollectionManager.Print($"Disenchanting will invalidate deck '{value.Name}'");
			}
		}
		return list;
	}
}
