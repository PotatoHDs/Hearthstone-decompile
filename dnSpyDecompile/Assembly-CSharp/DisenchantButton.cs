using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class DisenchantButton : CraftingButton
{
	// Token: 0x060013B6 RID: 5046 RVA: 0x0007146D File Offset: 0x0006F66D
	public override void EnableButton()
	{
		if (CraftingManager.Get().GetNumClientTransactions() > 0)
		{
			base.EnterUndoMode();
			return;
		}
		this.labelText.Text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT");
		base.EnableButton();
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000714A0 File Offset: 0x0006F6A0
	protected override void OnRelease()
	{
		if (!Network.IsLoggedIn())
		{
			CollectionManager.ShowFeatureDisabledWhileOfflinePopup();
			return;
		}
		if (CraftingManager.Get().GetPendingServerTransaction() != null)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			base.GetComponent<Animation>().Play("CardExchange_ButtonPress1_phone");
		}
		else
		{
			base.GetComponent<Animation>().Play("CardExchange_ButtonPress1");
		}
		if (CraftingManager.Get().GetNumClientTransactions() > 0)
		{
			this.DoDisenchant();
			return;
		}
		CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(new CollectionManager.DelOnAllDeckContents(this.OnReadyToStartDisenchant));
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x00071524 File Offset: 0x0006F724
	private void OnReadyToStartDisenchant()
	{
		if (!CraftingManager.Get().IsCardShowing())
		{
			return;
		}
		EntityDef entityDef = CraftingManager.Get().GetShownActor().GetEntityDef();
		string cardId = entityDef.GetCardId();
		List<string> postDisenchantInvalidDeckNames = this.GetPostDisenchantInvalidDeckNames(false);
		if (postDisenchantInvalidDeckNames.Count == 0)
		{
			int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending(TAG_PREMIUM.GOLDEN);
			int numOwnedIncludePending2 = CraftingManager.Get().GetNumOwnedIncludePending(TAG_PREMIUM.NORMAL);
			int num = numOwnedIncludePending + numOwnedIncludePending2;
			if (CraftingManager.Get().GetNumClientTransactions() <= 0 && this.m_lastwarnedCard != cardId && ((!entityDef.IsElite() && num <= 2) || (entityDef.IsElite() && num <= 1)))
			{
				this.m_lastwarnedCard = cardId;
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_HEADER");
				popupInfo.m_text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM2_DESC");
				popupInfo.m_showAlertIcon = true;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnConfirmDisenchantResponse);
				this.PendingDisenchantWarnings.Add(popupInfo);
			}
		}
		else
		{
			string text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_DESC");
			foreach (string str in postDisenchantInvalidDeckNames)
			{
				text = text + "\n" + str;
			}
			AlertPopup.PopupInfo popupInfo2 = new AlertPopup.PopupInfo();
			popupInfo2.m_headerText = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_HEADER");
			popupInfo2.m_text = text;
			popupInfo2.m_showAlertIcon = false;
			popupInfo2.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo2.m_responseCallback = new AlertPopup.ResponseCallback(this.OnConfirmDisenchantResponse);
			this.PendingDisenchantWarnings.Add(popupInfo2);
		}
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardId, false);
		if ((from x in CollectionManager.Get().GetOwnedCards()
		where GameUtils.IsClassicCardSet(x.Set) && x.GetEntityDef() != null && x.GetEntityDef().GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID) == cardDbId
		select x).Any<CollectibleCard>())
		{
			AlertPopup.PopupInfo popupInfo3 = new AlertPopup.PopupInfo();
			popupInfo3.m_headerText = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM_HEADER");
			popupInfo3.m_text = GameStrings.Get("GLUE_CRAFTING_DISENCHANT_CONFIRM3_DESC");
			popupInfo3.m_showAlertIcon = true;
			popupInfo3.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo3.m_responseCallback = new AlertPopup.ResponseCallback(this.OnConfirmDisenchantResponse);
			this.PendingDisenchantWarnings.Add(popupInfo3);
		}
		if (this.PendingDisenchantWarnings.Count > 0)
		{
			this.ShowNextDisenchantWarning();
			return;
		}
		this.DoDisenchant();
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x00071780 File Offset: 0x0006F980
	private void ShowNextDisenchantWarning()
	{
		if (this.PendingDisenchantWarnings.Count == 0)
		{
			return;
		}
		AlertPopup.PopupInfo info = this.PendingDisenchantWarnings[0];
		this.PendingDisenchantWarnings.RemoveAt(0);
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x000717BF File Offset: 0x0006F9BF
	private void OnConfirmDisenchantResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			this.PendingDisenchantWarnings.Clear();
			return;
		}
		if (this.PendingDisenchantWarnings.Count > 0)
		{
			this.ShowNextDisenchantWarning();
			return;
		}
		this.DoDisenchant();
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x000717EC File Offset: 0x0006F9EC
	private void DoDisenchant()
	{
		CraftingManager.Get().DisenchantButtonPressed();
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x000717F8 File Offset: 0x0006F9F8
	private List<string> GetPostDisenchantInvalidDeckNames(bool includeDuelsDecks = false)
	{
		Actor shownActor = CraftingManager.Get().GetShownActor();
		string cardId = shownActor.GetEntityDef().GetCardId();
		TAG_PREMIUM premium = shownActor.GetPremium();
		int numOwnedIncludePending = CraftingManager.Get().GetNumOwnedIncludePending();
		int num = Mathf.Max(0, numOwnedIncludePending - 1);
		int fixedRewardCounterpartCardID = GameUtils.GetFixedRewardCounterpartCardID(GameUtils.TranslateCardIdToDbId(cardId, false));
		bool flag = fixedRewardCounterpartCardID != 0 && GameUtils.IsClassicCard(fixedRewardCounterpartCardID);
		SortedDictionary<long, CollectionDeck> decks = CollectionManager.Get().GetDecks();
		List<string> list = new List<string>();
		foreach (CollectionDeck collectionDeck in decks.Values)
		{
			int cardCountFirstMatchingSlot = collectionDeck.GetCardCountFirstMatchingSlot(cardId, premium);
			if (fixedRewardCounterpartCardID > 0 && flag)
			{
				cardCountFirstMatchingSlot = collectionDeck.GetCardCountFirstMatchingSlot(GameUtils.TranslateDbIdToCardId(fixedRewardCounterpartCardID, false), premium);
			}
			if (cardCountFirstMatchingSlot > num && !collectionDeck.Locked && (collectionDeck.Type != DeckType.PVPDR_DECK || includeDuelsDecks))
			{
				list.Add(collectionDeck.Name);
				Log.CollectionManager.Print(string.Format("Disenchanting will invalidate deck '{0}'", collectionDeck.Name), Array.Empty<object>());
			}
		}
		return list;
	}

	// Token: 0x04000CE9 RID: 3305
	private string m_lastwarnedCard;

	// Token: 0x04000CEA RID: 3306
	private List<AlertPopup.PopupInfo> PendingDisenchantWarnings = new List<AlertPopup.PopupInfo>();
}
