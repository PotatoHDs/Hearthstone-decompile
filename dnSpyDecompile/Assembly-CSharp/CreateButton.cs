using System;
using PegasusShared;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class CreateButton : CraftingButton
{
	// Token: 0x06001329 RID: 4905 RVA: 0x0006DC94 File Offset: 0x0006BE94
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
		if (CraftingManager.Get().GetShownActor() == null || CraftingManager.Get().GetShownActor().GetEntityDef() == null)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			base.GetComponent<Animation>().Play("CardExchange_ButtonPress2_phone");
		}
		else
		{
			base.GetComponent<Animation>().Play("CardExchange_ButtonPress2");
		}
		string cardId = CraftingManager.Get().GetShownActor().GetEntityDef().GetCardId();
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		bool flag;
		if (deckRuleset != null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			flag = !deckRuleset.Filter(DefLoader.Get().GetEntityDef(cardId), editedDeck, Array.Empty<DeckRule.RuleType>());
		}
		else
		{
			flag = (GameUtils.GetCardSetFormat(GameUtils.GetCardSetFromCardID(cardId)) != FormatType.FT_STANDARD);
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
				m_responseCallback = new AlertPopup.ResponseCallback(this.OnConfirmCreateResponse)
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
			return;
		}
		this.DoCreate();
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0006DE80 File Offset: 0x0006C080
	private void OnConfirmCreateResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			if (!CollectionManager.Get().AccountEverHadWildCards())
			{
				AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
				{
					m_headerText = GameStrings.Get("GLUE_CRAFTING_WILD_CARD_HEADER"),
					m_text = GameStrings.Get("GLUE_CRAFTING_WILD_CARD_INTRO_DESC"),
					m_showAlertIcon = true,
					m_responseDisplay = AlertPopup.ResponseDisplay.OK,
					m_responseCallback = delegate(AlertPopup.Response r, object data)
					{
						this.DoCreate();
						Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, true);
						Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 6);
						Options.Get().SetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK, false);
						UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
						Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN, true);
						Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK, true);
					}
				};
				DialogManager.Get().ShowPopup(info);
				return;
			}
			this.DoCreate();
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0006DEF5 File Offset: 0x0006C0F5
	public override void EnableButton()
	{
		if (CraftingManager.Get().GetNumClientTransactions() < 0)
		{
			base.EnterUndoMode();
			return;
		}
		this.labelText.Text = GameStrings.Get("GLUE_CRAFTING_CREATE");
		base.EnableButton();
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x0006DF26 File Offset: 0x0006C126
	private void DoCreate()
	{
		CraftingManager.Get().CreateButtonPressed();
	}
}
