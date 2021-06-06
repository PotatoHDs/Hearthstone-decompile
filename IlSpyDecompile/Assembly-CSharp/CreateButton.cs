using PegasusShared;
using UnityEngine;

public class CreateButton : CraftingButton
{
	protected override void OnRelease()
	{
		if (!Network.IsLoggedIn())
		{
			CollectionManager.ShowFeatureDisabledWhileOfflinePopup();
		}
		else
		{
			if (CraftingManager.Get().GetPendingServerTransaction() != null || CraftingManager.Get().GetShownActor() == null || CraftingManager.Get().GetShownActor().GetEntityDef() == null)
			{
				return;
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				GetComponent<Animation>().Play("CardExchange_ButtonPress2_phone");
			}
			else
			{
				GetComponent<Animation>().Play("CardExchange_ButtonPress2");
			}
			bool flag = false;
			string cardId = CraftingManager.Get().GetShownActor().GetEntityDef()
				.GetCardId();
			DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
			if (deckRuleset != null)
			{
				CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
				flag = !deckRuleset.Filter(DefLoader.Get().GetEntityDef(cardId), editedDeck);
			}
			else
			{
				flag = GameUtils.GetCardSetFormat(GameUtils.GetCardSetFromCardID(cardId)) != FormatType.FT_STANDARD;
			}
			if (CraftingManager.Get().GetNumClientTransactions() != 0)
			{
				flag = false;
			}
			if (flag)
			{
				string cardSetFormatAsString = GameUtils.GetCardSetFormatAsString(GameUtils.GetCardSetFromCardID(cardId));
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo
				{
					m_headerText = GameStrings.Get("GLUE_CRAFTING_" + cardSetFormatAsString + "_CARD_HEADER"),
					m_cancelText = GameStrings.Get("GLUE_CRAFTING_NONSTANDARD_CARD_WARNING_CANCEL"),
					m_confirmText = GameStrings.Get("GLUE_CRAFTING_NONSTANDARD_CARD_WARNING_CONFIRM"),
					m_showAlertIcon = true,
					m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
					m_responseCallback = OnConfirmCreateResponse
				};
				if (SceneMgr.Get().IsInTavernBrawlMode())
				{
					popupInfo.m_headerText = GameStrings.Get("GLUE_CRAFTING_INVALID_CARD_TAVERN_BRAWL_HEADER");
					popupInfo.m_text = GameStrings.Get("GLUE_CRAFTING_INVALID_CARD_TAVERN_BRAWL_DESC");
				}
				else if (CollectionManager.Get().AccountEverHadWildCards())
				{
					popupInfo.m_text = GameStrings.Get("GLUE_CRAFTING_" + cardSetFormatAsString + "_CARD_DESC");
				}
				else
				{
					popupInfo.m_text = GameStrings.Get("GLUE_CRAFTING_" + cardSetFormatAsString + "_CARD_FIRST_DESC");
				}
				DialogManager.Get().ShowPopup(popupInfo);
			}
			else
			{
				DoCreate();
			}
		}
	}

	private void OnConfirmCreateResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CONFIRM)
		{
			return;
		}
		if (!CollectionManager.Get().AccountEverHadWildCards())
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_CRAFTING_WILD_CARD_HEADER"),
				m_text = GameStrings.Get("GLUE_CRAFTING_WILD_CARD_INTRO_DESC"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = delegate
				{
					DoCreate();
					Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, val: true);
					Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 6);
					Options.Get().SetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK, val: false);
					UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
					Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN, val: true);
					Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK, val: true);
				}
			};
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			DoCreate();
		}
	}

	public override void EnableButton()
	{
		if (CraftingManager.Get().GetNumClientTransactions() < 0)
		{
			base.EnterUndoMode();
			return;
		}
		labelText.Text = GameStrings.Get("GLUE_CRAFTING_CREATE");
		base.EnableButton();
	}

	private void DoCreate()
	{
		CraftingManager.Get().CreateButtonPressed();
	}
}
