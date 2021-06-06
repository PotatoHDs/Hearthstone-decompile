using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class DraftManager : IService
{
	// Token: 0x06002482 RID: 9346 RVA: 0x000B78D0 File Offset: 0x000B5AD0
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		serviceLocator.Get<GameMgr>().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(ArenaSessionResponse.PacketID.ID, new Network.NetHandler(this.OnArenaSessionResponse), null);
		network.RegisterNetHandler(DraftRewardsAcked.PacketID.ID, new Network.NetHandler(this.OnAckRewards), null);
		network.RegisterNetHandler(DraftError.PacketID.ID, new Network.NetHandler(this.OnError), null);
		network.RegisterNetHandler(DraftRemovePremiumsResponse.PacketID.ID, new Network.NetHandler(this.OnDraftRemovePremiumsResponse), null);
		yield break;
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x000B78E6 File Offset: 0x000B5AE6
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(GameMgr)
		};
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x000B7908 File Offset: 0x000B5B08
	private void WillReset()
	{
		this.ClearDeckInfo();
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x000B7910 File Offset: 0x000B5B10
	public static DraftManager Get()
	{
		return HearthstoneServices.Get<DraftManager>();
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x000B7917 File Offset: 0x000B5B17
	public void OnLoggedIn()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x000B7930 File Offset: 0x000B5B30
	public void RegisterDisplayHandlers()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(DraftBeginning.PacketID.ID, new Network.NetHandler(this.OnBegin), null);
		network.RegisterNetHandler(DraftRetired.PacketID.ID, new Network.NetHandler(this.OnRetire), null);
		network.RegisterNetHandler(DraftChoicesAndContents.PacketID.ID, new Network.NetHandler(this.OnChoicesAndContents), null);
		network.RegisterNetHandler(DraftChosen.PacketID.ID, new Network.NetHandler(this.OnChosen), null);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnDraftPurchaseAck));
		if (DemoMgr.Get().ArenaIs1WinMode())
		{
			StoreManager.Get().RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnDraftPurchaseAck));
		}
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x000B79F4 File Offset: 0x000B5BF4
	public void UnregisterDisplayHandlers()
	{
		Network network = Network.Get();
		network.RemoveNetHandler(DraftBeginning.PacketID.ID, new Network.NetHandler(this.OnBegin));
		network.RemoveNetHandler(DraftRetired.PacketID.ID, new Network.NetHandler(this.OnRetire));
		network.RemoveNetHandler(DraftChoicesAndContents.PacketID.ID, new Network.NetHandler(this.OnChoicesAndContents));
		network.RemoveNetHandler(DraftChosen.PacketID.ID, new Network.NetHandler(this.OnChosen));
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnDraftPurchaseAck));
		if (DemoMgr.Get().ArenaIs1WinMode())
		{
			StoreManager.Get().RemoveSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnDraftPurchaseAck));
		}
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x000B7AB1 File Offset: 0x000B5CB1
	public void RegisterDraftDeckSetListener(DraftManager.DraftDeckSet dlg)
	{
		this.m_draftDeckSetListeners.Add(dlg);
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x000B7ABF File Offset: 0x000B5CBF
	public void RemoveDraftDeckSetListener(DraftManager.DraftDeckSet dlg)
	{
		this.m_draftDeckSetListeners.Remove(dlg);
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x0600248C RID: 9356 RVA: 0x000B7ACE File Offset: 0x000B5CCE
	public ulong SecondsUntilEndOfSeason
	{
		get
		{
			if (this.m_currentSeason != null)
			{
				return this.m_currentSeason.Season.GameContentSeason.EndSecondsFromNow;
			}
			return 0UL;
		}
	}

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x0600248D RID: 9357 RVA: 0x000B7AF0 File Offset: 0x000B5CF0
	public int CurrentSeasonId
	{
		get
		{
			if (this.m_currentSeason != null)
			{
				return this.m_currentSeason.Season.GameContentSeason.SeasonId;
			}
			return 0;
		}
	}

	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x0600248E RID: 9358 RVA: 0x000B7B11 File Offset: 0x000B5D11
	public bool HasActiveRun
	{
		get
		{
			return this.m_currentSession != null && this.m_currentSession.HasIsActive && this.m_currentSession.IsActive;
		}
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x0600248F RID: 9359 RVA: 0x000B7B35 File Offset: 0x000B5D35
	public int ChosenIndex
	{
		get
		{
			return this.m_chosenIndex;
		}
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x000B7B3D File Offset: 0x000B5D3D
	public void RefreshCurrentSeasonFromServer()
	{
		Network.Get().SendArenaSessionRequest();
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x000B7B49 File Offset: 0x000B5D49
	public CollectionDeck GetDraftDeck()
	{
		return this.m_draftDeck;
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x000B7B51 File Offset: 0x000B5D51
	public int GetSlot()
	{
		return this.m_currentSlot;
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x000B7B59 File Offset: 0x000B5D59
	public DraftSlotType GetSlotType()
	{
		return this.m_currentSlotType;
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x000B7B61 File Offset: 0x000B5D61
	public bool HasSlotType(DraftSlotType slotType)
	{
		return this.m_uniqueDraftSlotTypesForDeck.Contains(slotType);
	}

	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x06002495 RID: 9365 RVA: 0x000B7B6F File Offset: 0x000B5D6F
	public bool CanShowWinsLosses
	{
		get
		{
			return this.m_hasReceivedSessionWinsLosses;
		}
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x000B7B77 File Offset: 0x000B5D77
	public int GetLosses()
	{
		return this.m_losses;
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x000B7B7F File Offset: 0x000B5D7F
	public int GetWins()
	{
		return this.m_wins;
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x000B7B87 File Offset: 0x000B5D87
	public int GetMaxWins()
	{
		return this.m_maxWins;
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x000B7B8F File Offset: 0x000B5D8F
	public int GetNumTicketsOwned()
	{
		return this.m_numTicketsOwned;
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x000B7B97 File Offset: 0x000B5D97
	public bool GetIsNewKey()
	{
		return this.m_isNewKey;
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x000B7B9F File Offset: 0x000B5D9F
	public bool DeckWasActiveDuringSession()
	{
		return this.m_deckActiveDuringSession;
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x000B7BA8 File Offset: 0x000B5DA8
	public AssetReference GetDraftPaperTexture()
	{
		string text = null;
		if (this.m_currentSeason != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				text = this.m_currentSeason.Season.DraftPaperTexturePhone;
			}
			else
			{
				text = this.m_currentSeason.Season.DraftPaperTexture;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			return new AssetReference(text);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			return DraftManager.DEFAULT_DRAFT_PAPER_TEXTURE_PHONE;
		}
		return DraftManager.DEFAULT_DRAFT_PAPER_TEXTURE;
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x000B7C15 File Offset: 0x000B5E15
	public bool GetDraftPaperTextColorOverride(ref Color overrideColor)
	{
		return this.m_currentSeason != null && !string.IsNullOrEmpty(this.m_currentSeason.Season.DraftPaperTextColor) && ColorUtility.TryParseHtmlString(this.m_currentSeason.Season.DraftPaperTextColor, out overrideColor);
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x000B7C50 File Offset: 0x000B5E50
	public AssetReference GetRewardPaperPrefab()
	{
		string text = null;
		if (this.m_currentSeason != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				text = this.m_currentSeason.Season.RewardPaperPrefabPhone;
			}
			else
			{
				text = this.m_currentSeason.Season.RewardPaperPrefab;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			return new AssetReference(text);
		}
		return ArenaRewardPaper.GetDefaultRewardPaper();
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x000B7CAC File Offset: 0x000B5EAC
	public string GetSceneHeadlineText()
	{
		if (this.m_currentSeason != null && this.m_currentSeason.Season.Strings.Count > 0)
		{
			return GameStrings.FormatStringWithPlurals(this.m_currentSeason.Season.Strings, "SCENE_HEADLINE", Array.Empty<object>());
		}
		return string.Empty;
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x000B7D00 File Offset: 0x000B5F00
	public bool ShouldActivateKey()
	{
		GameContentScenario gameContentScenario = this.m_currentSeason.Season.GameContentSeason.Scenarios.FirstOrDefault<GameContentScenario>();
		int num = (gameContentScenario == null) ? 0 : gameContentScenario.MaxWins;
		int num2 = (gameContentScenario == null) ? 0 : gameContentScenario.MaxLosses;
		return this.m_deckActiveDuringSession || (this.m_inRewards && this.m_wins < num && this.m_losses < num2);
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x000B7D68 File Offset: 0x000B5F68
	public List<RewardData> GetRewards()
	{
		if (this.m_chest != null)
		{
			return this.m_chest.Rewards;
		}
		return new List<RewardData>();
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x000B7D84 File Offset: 0x000B5F84
	public void MakeChoice(int choiceNum, TAG_PREMIUM choicePremium)
	{
		this.m_chosenIndex = choiceNum;
		if (this.m_draftDeck == null)
		{
			Debug.LogWarning("DraftManager.MakeChoice(): Trying to make a draft choice while the draft deck is null");
			return;
		}
		if (this.m_validSlot != this.m_currentSlot)
		{
			return;
		}
		this.m_validSlot++;
		Network.Get().MakeDraftChoice(this.m_draftDeck.ID, this.m_currentSlot, choiceNum, (int)choicePremium);
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x000B7DE5 File Offset: 0x000B5FE5
	public void NotifyOfFinalGame(bool wonFinalGame)
	{
		if (wonFinalGame)
		{
			this.m_wins++;
			return;
		}
		this.m_losses++;
	}

	// Token: 0x060024A4 RID: 9380 RVA: 0x000B7E08 File Offset: 0x000B6008
	public void FindGame()
	{
		GameContentScenario gameContentScenario = this.m_currentSeason.Season.GameContentSeason.Scenarios.FirstOrDefault<GameContentScenario>();
		int missionId = (gameContentScenario == null) ? 2 : gameContentScenario.ScenarioId;
		GameMgr.Get().FindGame(GameType.GT_ARENA, FormatType.FT_WILD, missionId, 0, 0L, null, new int?(this.CurrentSeasonId), false, null, GameType.GT_UNKNOWN);
		if (this.m_draftDeck != null)
		{
			Log.Decks.PrintInfo("Starting Arena Game With Deck:", Array.Empty<object>());
			this.m_draftDeck.LogDeckStringInformation();
		}
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x000B7E84 File Offset: 0x000B6084
	public TAG_PREMIUM GetDraftPremium(string cardId)
	{
		int numCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(cardId, TAG_PREMIUM.GOLDEN);
		if (CollectionManager.Get().GetNumCopiesInCollection(cardId, TAG_PREMIUM.DIAMOND) > 0 && this.m_draftDeck.GetCardIdCount(cardId, true) == 0)
		{
			return TAG_PREMIUM.DIAMOND;
		}
		if (numCopiesInCollection >= 2)
		{
			return TAG_PREMIUM.GOLDEN;
		}
		int cardCountAllMatchingSlots = this.m_draftDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.GOLDEN);
		if (numCopiesInCollection == 1 && (cardCountAllMatchingSlots == 0 || CollectionManager.Get().GetCard(cardId, TAG_PREMIUM.NORMAL).Rarity == TAG_RARITY.LEGENDARY))
		{
			return TAG_PREMIUM.GOLDEN;
		}
		return TAG_PREMIUM.NORMAL;
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x000B7EF2 File Offset: 0x000B60F2
	public bool ShouldShowFreeArenaWinScreen()
	{
		return SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_FROST_FESTIVAL_FREE_ARENA_WIN, false) && !Options.Get().GetBool(Option.HAS_SEEN_FREE_ARENA_WIN_DIALOG_THIS_DRAFT) && this.m_wins > 0;
	}

	// Token: 0x060024A7 RID: 9383 RVA: 0x000B7F24 File Offset: 0x000B6124
	public void PromptToDisablePremium()
	{
		if (this.m_pendingRequestToDisablePremiums)
		{
			return;
		}
		if (Options.Get().GetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT))
		{
			return;
		}
		if (this.m_inRewards)
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_DRAFT_REMOVE_PREMIUMS_DIALOG_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_DRAFT_REMOVE_PREMIUMS_DIALOG_BODY");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnDisablePremiumsConfirmationResponse);
		DialogManager.Get().ShowPopup(popupInfo);
		this.m_pendingRequestToDisablePremiums = true;
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x000B7FD4 File Offset: 0x000B61D4
	private void OnDisablePremiumsConfirmationResponse(AlertPopup.Response response, object userData)
	{
		this.m_pendingRequestToDisablePremiums = false;
		if (response != AlertPopup.Response.CONFIRM)
		{
			return;
		}
		Network.Get().DraftRequestDisablePremiums();
	}

	// Token: 0x060024A9 RID: 9385 RVA: 0x000B7FEC File Offset: 0x000B61EC
	private void OnDraftRemovePremiumsResponse()
	{
		Options.Get().SetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT, true);
		Network.DraftChoicesAndContents draftRemovePremiumsResponse = Network.Get().GetDraftRemovePremiumsResponse();
		this.m_draftDeck.GetSlots().Clear();
		foreach (Network.CardUserData cardUserData in draftRemovePremiumsResponse.DeckInfo.Cards)
		{
			string text = (cardUserData.DbId == 0) ? string.Empty : GameUtils.TranslateDbIdToCardId(cardUserData.DbId, false);
			for (int i = 0; i < cardUserData.Count; i++)
			{
				if (!this.m_draftDeck.AddCard(text, cardUserData.Premium, false))
				{
					Debug.LogWarning(string.Format("DraftManager.OnDraftRemovePremiumsResponse() - Card {0} could not be added to draft deck", text));
				}
			}
		}
		DraftPhoneDeckTray.Get().GetCardsContent().UpdateCardList();
		this.InformDraftDisplayOfChoices(draftRemovePremiumsResponse.Choices);
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x000B80DC File Offset: 0x000B62DC
	public bool ShowArenaPopup_SeasonEndingSoon(long secondsToCurrentSeasonEnd, Action popupClosedCallback)
	{
		if (this.m_currentSeason == null || !this.m_currentSeason.HasSeasonEndingSoonPrefab || string.IsNullOrEmpty(this.m_currentSeason.SeasonEndingSoonPrefab) || !this.m_currentSeason.HasSeason || this.m_currentSeason.Season == null || this.m_currentSeason.Season.Strings.Count == 0)
		{
			Error.AddDevWarning("No Season Data", "Cannot show 'Ending Soon' dialog - the current Arena season={0} does not have the ENDING_SOON_PREFAB data or header/body strings.", new object[]
			{
				this.CurrentSeasonId
			});
			return false;
		}
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_seconds = null,
			m_minutes = null,
			m_hours = "GLUE_ARENA_POPUP_ENDING_SOON_HEADER_HOURS",
			m_yesterday = null,
			m_days = "GLUE_ARENA_POPUP_ENDING_SOON_HEADER_DAYS",
			m_weeks = "GLUE_ARENA_POPUP_ENDING_SOON_HEADER_WEEKS",
			m_monthAgo = "GLUE_ARENA_POPUP_ENDING_SOON_HEADER_MONTHS"
		};
		BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
		popupInfo.m_prefabAssetRefs.Add(this.m_currentSeason.SeasonEndingSoonPrefab);
		popupInfo.m_prefabAssetRefs.Add(this.m_currentSeason.SeasonEndingSoonPrefabExtra);
		popupInfo.m_headerText = TimeUtils.GetElapsedTimeString(secondsToCurrentSeasonEnd, stringSet, true);
		popupInfo.m_bodyText = GameStrings.FormatStringWithPlurals(this.m_currentSeason.Season.Strings, "ENDING_SOON_BODY", Array.Empty<object>());
		popupInfo.m_responseUserData = this.CurrentSeasonId;
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_responseCallback = delegate(BasicPopup.Response response, object userData)
		{
			if (popupClosedCallback != null)
			{
				popupClosedCallback();
			}
		};
		return DialogManager.Get().ShowArenaSeasonPopup(UserAttentionBlocker.NONE, popupInfo);
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x000B8258 File Offset: 0x000B6458
	public bool ShowArenaPopup_SeasonComingSoon(long secondsToNextSeasonStart, Action popupClosedCallback)
	{
		if (this.m_currentSeason == null || !this.m_currentSeason.HasNextSeasonComingSoonPrefab || string.IsNullOrEmpty(this.m_currentSeason.NextSeasonComingSoonPrefab) || this.m_currentSeason.NextSeasonStrings == null || this.m_currentSeason.NextSeasonStrings.Count == 0)
		{
			Error.AddDevWarning("No Season Data", "Cannot show 'Coming Soon' dialog - the season after current Arena season={0} does not have the COMING_SOON_PREFAB data or header/body strings.", new object[]
			{
				this.CurrentSeasonId
			});
			return false;
		}
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_seconds = null,
			m_minutes = null,
			m_hours = "GLUE_ARENA_POPUP_COMING_SOON_HEADER_HOURS",
			m_yesterday = null,
			m_days = "GLUE_ARENA_POPUP_COMING_SOON_HEADER_DAYS",
			m_weeks = "GLUE_ARENA_POPUP_COMING_SOON_HEADER_WEEKS",
			m_monthAgo = "GLUE_ARENA_POPUP_COMING_SOON_HEADER_MONTHS"
		};
		BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
		popupInfo.m_prefabAssetRefs.Add(this.m_currentSeason.NextSeasonComingSoonPrefab);
		popupInfo.m_prefabAssetRefs.Add(this.m_currentSeason.NextSeasonComingSoonPrefabExtra);
		popupInfo.m_headerText = TimeUtils.GetElapsedTimeString(secondsToNextSeasonStart, stringSet, true);
		popupInfo.m_bodyText = GameStrings.FormatStringWithPlurals(this.m_currentSeason.NextSeasonStrings, "COMING_SOON_BODY", Array.Empty<object>());
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_responseUserData = this.m_currentSeason.NextSeasonId;
		popupInfo.m_responseCallback = delegate(BasicPopup.Response response, object userData)
		{
			if (popupClosedCallback != null)
			{
				popupClosedCallback();
			}
		};
		return DialogManager.Get().ShowArenaSeasonPopup(UserAttentionBlocker.NONE, popupInfo);
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x000B83C0 File Offset: 0x000B65C0
	public bool ShowNextArenaPopup(Action popupClosedCallback)
	{
		if (this.m_currentSeason == null || PopupDisplayManager.Get().IsShowing)
		{
			return false;
		}
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
		{
			int @int = Options.Get().GetInt(Option.LATEST_SEEN_ARENA_SEASON_ENDING);
			int int2 = Options.Get().GetInt(Option.LATEST_SEEN_ARENA_SEASON_STARTING);
			long? num = (!this.m_currentSeason.HasSeason) ? null : new long?((long)this.m_currentSeason.Season.GameContentSeason.EndSecondsFromNow);
			long? num2 = (!this.m_currentSeason.HasNextStartSecondsFromNow) ? null : new long?((long)this.m_currentSeason.NextStartSecondsFromNow);
			if (this.HasActiveRun && num != null && this.m_currentSeason.HasSeasonEndingSoonDays && num.Value <= (long)(this.m_currentSeason.SeasonEndingSoonDays * 86400) && @int < this.CurrentSeasonId)
			{
				int seasonIdEnding = this.CurrentSeasonId;
				Action popupClosedCallback2 = delegate()
				{
					Options.Get().SetInt(Option.LATEST_SEEN_ARENA_SEASON_ENDING, seasonIdEnding);
					if (popupClosedCallback != null)
					{
						popupClosedCallback();
					}
				};
				return this.ShowArenaPopup_SeasonEndingSoon(num.Value, popupClosedCallback2);
			}
			if (this.HasActiveRun && num2 != null && this.m_currentSeason.HasNextSeasonComingSoonDays && num2.Value <= (long)(this.m_currentSeason.NextSeasonComingSoonDays * 86400) && this.m_currentSeason.HasNextSeasonId && int2 < this.m_currentSeason.NextSeasonId)
			{
				int seasonIdStarting = this.m_currentSeason.NextSeasonId;
				Action popupClosedCallback3 = delegate()
				{
					Options.Get().SetInt(Option.LATEST_SEEN_ARENA_SEASON_STARTING, seasonIdStarting);
					if (popupClosedCallback != null)
					{
						popupClosedCallback();
					}
				};
				return this.ShowArenaPopup_SeasonComingSoon(num2.Value, popupClosedCallback3);
			}
		}
		return false;
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x000B8598 File Offset: 0x000B6798
	public void ClearAllInnkeeperPopups()
	{
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_HERO_CHOICE);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_CARD_CHOICE);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_CARD_CHOICE2);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_PLAY_MODE);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_1WIN);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_2LOSS);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_RETIRE);
		Options.Get().DeleteOption(Option.HAS_SEEN_FORGE_MAX_WIN);
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x000B861D File Offset: 0x000B681D
	public void ClearAllSeenPopups()
	{
		Options.Get().DeleteOption(Option.LATEST_SEEN_SCHEDULED_ENTERED_ARENA_DRAFT);
		Options.Get().DeleteOption(Option.HAS_SEEN_FREE_ARENA_WIN_DIALOG_THIS_DRAFT);
		Options.Get().DeleteOption(Option.LATEST_SEEN_ARENA_SEASON_ENDING);
		Options.Get().DeleteOption(Option.LATEST_SEEN_ARENA_SEASON_STARTING);
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x000B865C File Offset: 0x000B685C
	private void ClearDeckInfo()
	{
		this.m_draftDeck = null;
		this.m_hasReceivedSessionWinsLosses = false;
		this.m_losses = 0;
		this.m_wins = 0;
		this.m_maxWins = int.MaxValue;
		this.m_isNewKey = false;
		this.m_chest = null;
		this.m_deckActiveDuringSession = false;
		Options.Get().SetBool(Option.HAS_SEEN_FREE_ARENA_WIN_DIALOG_THIS_DRAFT, false);
		Options.Get().SetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT, false);
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x000B86C8 File Offset: 0x000B68C8
	private void OnBegin()
	{
		Options.Get().SetBool(Option.HAS_SEEN_FREE_ARENA_WIN_DIALOG_THIS_DRAFT, false);
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.DRAFT || (SceneMgr.Get().IsTransitionNowOrPending() && SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.DRAFT))
		{
			return;
		}
		this.m_hasReceivedSessionWinsLosses = true;
		Network.BeginDraft beginDraft = Network.Get().GetBeginDraft();
		this.m_draftDeck = new CollectionDeck
		{
			ID = beginDraft.DeckID,
			Type = DeckType.DRAFT_DECK,
			FormatType = FormatType.FT_WILD
		};
		this.m_wins = beginDraft.Wins;
		this.m_losses = 0;
		this.m_currentSlot = 0;
		this.m_currentSlotType = beginDraft.SlotType;
		this.m_uniqueDraftSlotTypesForDeck = beginDraft.UniqueSlotTypesForDraft;
		this.m_validSlot = 0;
		this.m_maxSlot = beginDraft.MaxSlot;
		this.m_chest = null;
		this.m_inRewards = false;
		this.m_currentSession = beginDraft.Session;
		Options.Get().SetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT, false);
		SessionRecord sessionRecord = new SessionRecord();
		sessionRecord.Wins = (uint)beginDraft.Wins;
		sessionRecord.Losses = 0U;
		sessionRecord.RunFinished = false;
		sessionRecord.SessionRecordType = SessionRecordType.ARENA;
		BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
		Log.Arena.Print(string.Format("DraftManager.OnBegin - Got new draft deck with ID: {0}", this.m_draftDeck.ID), Array.Empty<object>());
		this.InformDraftDisplayOfChoices(beginDraft.Heroes);
		this.FireDraftDeckSetEvent();
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x000B8824 File Offset: 0x000B6A24
	private void OnRetire()
	{
		Network.DraftRetired retiredDraft = Network.Get().GetRetiredDraft();
		Log.Arena.Print(string.Format("DraftManager.OnRetire deckID={0}", retiredDraft.Deck), Array.Empty<object>());
		this.m_chest = retiredDraft.Chest;
		this.m_inRewards = true;
		this.InformDraftDisplayOfChoices(new List<NetCache.CardDefinition>());
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x000B8880 File Offset: 0x000B6A80
	private void OnAckRewards()
	{
		SessionRecord sessionRecord = new SessionRecord();
		sessionRecord.Wins = (uint)this.m_wins;
		sessionRecord.Losses = (uint)this.m_losses;
		sessionRecord.RunFinished = true;
		sessionRecord.SessionRecordType = SessionRecordType.ARENA;
		BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
		if (!Options.Get().GetBool(Option.HAS_ACKED_ARENA_REWARDS, false) && UserAttentionManager.CanShowAttentionGrabber("DraftManager.OnAckRewards:" + Option.HAS_ACKED_ARENA_REWARDS))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_ARENA_1ST_REWARD"), "VO_INNKEEPER_ARENA_1ST_REWARD.prefab:660e915849550ae4085735866647d529", 0f, null, false);
			Options.Get().SetBool(Option.HAS_ACKED_ARENA_REWARDS, true);
		}
		Network.Get().GetRewardsAckDraftID();
		this.ClearDeckInfo();
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x000B894C File Offset: 0x000B6B4C
	private void OnChoicesAndContents()
	{
		Network.DraftChoicesAndContents draftChoicesAndContents = Network.Get().GetDraftChoicesAndContents();
		this.m_hasReceivedSessionWinsLosses = true;
		this.m_currentSlot = draftChoicesAndContents.Slot;
		this.m_currentSlotType = draftChoicesAndContents.SlotType;
		this.m_uniqueDraftSlotTypesForDeck = draftChoicesAndContents.UniqueSlotTypesForDraft;
		this.m_validSlot = draftChoicesAndContents.Slot;
		this.m_maxSlot = draftChoicesAndContents.MaxSlot;
		this.m_draftDeck = new CollectionDeck
		{
			ID = draftChoicesAndContents.DeckInfo.Deck,
			Type = DeckType.DRAFT_DECK,
			HeroCardID = draftChoicesAndContents.Hero.Name,
			HeroPremium = draftChoicesAndContents.Hero.Premium,
			HeroPowerCardID = draftChoicesAndContents.HeroPower.Name,
			FormatType = FormatType.FT_WILD
		};
		Log.Arena.Print(string.Format("DraftManager.OnChoicesAndContents - Draft Deck ID: {0}, Hero Card = {1}", this.m_draftDeck.ID, this.m_draftDeck.HeroCardID), Array.Empty<object>());
		foreach (Network.CardUserData cardUserData in draftChoicesAndContents.DeckInfo.Cards)
		{
			string text = (cardUserData.DbId == 0) ? string.Empty : GameUtils.TranslateDbIdToCardId(cardUserData.DbId, false);
			Log.Arena.Print(string.Format("DraftManager.OnChoicesAndContents - Draft deck contains card {0}", text), Array.Empty<object>());
			for (int i = 0; i < cardUserData.Count; i++)
			{
				if (!this.m_draftDeck.AddCard(text, cardUserData.Premium, false))
				{
					Debug.LogWarning(string.Format("DraftManager.OnChoicesAndContents() - Card {0} could not be added to draft deck", text));
				}
			}
		}
		this.m_losses = draftChoicesAndContents.Losses;
		if (draftChoicesAndContents.Wins > this.m_wins)
		{
			this.m_isNewKey = true;
		}
		else
		{
			this.m_isNewKey = false;
		}
		this.m_wins = draftChoicesAndContents.Wins;
		this.m_maxWins = draftChoicesAndContents.MaxWins;
		this.m_chest = draftChoicesAndContents.Chest;
		this.m_inRewards = (this.m_chest != null);
		this.m_currentSession = draftChoicesAndContents.Session;
		if (this.m_losses > 0 && DemoMgr.Get().ArenaIs1WinMode())
		{
			Network.Get().DraftRetire(this.GetDraftDeck().ID, this.GetSlot(), this.CurrentSeasonId);
			return;
		}
		if (this.m_wins == 5 && DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2013)
		{
			DemoMgr.Get().CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA_5_WINS"), false, false);
		}
		else if (this.m_losses == 3 && !Options.Get().GetBool(Option.HAS_LOST_IN_ARENA, false) && UserAttentionManager.CanShowAttentionGrabber("DraftManager.OnChoicesAndContents:" + Option.HAS_LOST_IN_ARENA))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_ARENA_3RD_LOSS"), "VO_INNKEEPER_ARENA_3RD_LOSS.prefab:6b2af024c9980d344a087295afb5e3df", 0f, null, false);
			Options.Get().SetBool(Option.HAS_LOST_IN_ARENA, true);
		}
		this.InformDraftDisplayOfChoices(draftChoicesAndContents.Choices);
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x000B8C48 File Offset: 0x000B6E48
	private void InformDraftDisplayOfChoices(List<NetCache.CardDefinition> choices)
	{
		DraftDisplay draftDisplay = DraftDisplay.Get();
		if (draftDisplay == null)
		{
			return;
		}
		if (this.m_inRewards)
		{
			draftDisplay.SetDraftMode(DraftDisplay.DraftMode.IN_REWARDS);
			return;
		}
		if (choices.Count == 0)
		{
			this.m_deckActiveDuringSession = true;
			draftDisplay.SetDraftMode(DraftDisplay.DraftMode.ACTIVE_DRAFT_DECK);
			return;
		}
		if (!Options.Get().GetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT) && this.GetSlotType() != DraftSlotType.DRAFT_SLOT_HERO_POWER)
		{
			foreach (NetCache.CardDefinition cardDefinition in choices)
			{
				cardDefinition.Premium = this.GetDraftPremium(cardDefinition.Name);
			}
		}
		draftDisplay.SetDraftMode(DraftDisplay.DraftMode.DRAFTING);
		draftDisplay.AcceptNewChoices(choices);
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x000B8D00 File Offset: 0x000B6F00
	private void InformDraftDisplayOfSelectedChoice(int chosenIndex)
	{
		DraftDisplay draftDisplay = DraftDisplay.Get();
		if (draftDisplay == null)
		{
			return;
		}
		draftDisplay.OnChoiceSelected(chosenIndex);
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x000B8D24 File Offset: 0x000B6F24
	private void OnChosen()
	{
		Network.DraftChosen draftChosen = Network.Get().GetDraftChosen();
		if (this.m_currentSlotType == DraftSlotType.DRAFT_SLOT_HERO)
		{
			Log.Arena.Print(string.Format("DraftManager.OnChosen(): hero={0} premium={1}", draftChosen.ChosenCard.Name, draftChosen.ChosenCard.Premium), Array.Empty<object>());
			this.m_draftDeck.HeroCardID = draftChosen.ChosenCard.Name;
			this.m_draftDeck.HeroPremium = draftChosen.ChosenCard.Premium;
		}
		else if (this.m_currentSlotType == DraftSlotType.DRAFT_SLOT_CARD)
		{
			this.m_draftDeck.AddCard(draftChosen.ChosenCard.Name, draftChosen.ChosenCard.Premium, false);
		}
		this.m_currentSlot++;
		this.m_currentSlotType = draftChosen.SlotType;
		if (this.m_currentSlot > this.m_maxSlot && DraftDisplay.Get() != null)
		{
			DraftDisplay.Get().DoDeckCompleteAnims();
		}
		this.InformDraftDisplayOfSelectedChoice(this.m_chosenIndex);
		this.InformDraftDisplayOfChoices(draftChosen.NextChoices);
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x000B8E2C File Offset: 0x000B702C
	private void OnError()
	{
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.DRAFT))
		{
			return;
		}
		DraftError draftError = Network.Get().GetDraftError();
		this.m_numTicketsOwned = (draftError.HasNumTicketsOwned ? draftError.NumTicketsOwned : 0);
		DraftDisplay draftDisplay = DraftDisplay.Get();
		switch (draftError.ErrorCode_)
		{
		case DraftError.ErrorCode.DE_UNKNOWN:
			Debug.LogError("DraftManager.OnError - UNKNOWN EXCEPTION - See server logs for more info.");
			return;
		case DraftError.ErrorCode.DE_NO_LICENSE:
			Debug.LogWarning("DraftManager.OnError - No License.  What does this mean???");
			return;
		case DraftError.ErrorCode.DE_RETIRE_FIRST:
			Debug.LogError("DraftManager.OnError - You cannot start a new draft while one is in progress.");
			return;
		case DraftError.ErrorCode.DE_NOT_IN_DRAFT:
			if (draftDisplay != null)
			{
				draftDisplay.SetDraftMode(DraftDisplay.DraftMode.NO_ACTIVE_DRAFT);
				return;
			}
			return;
		case DraftError.ErrorCode.DE_NOT_IN_DRAFT_BUT_COULD_BE:
			if (!Options.Get().GetBool(Option.HAS_SEEN_FORGE, false))
			{
				DraftDisplay.Get().SetDraftMode(DraftDisplay.DraftMode.NO_ACTIVE_DRAFT);
				return;
			}
			if (this.m_numTicketsOwned > 0)
			{
				DraftDisplay.Get().SetDraftMode(DraftDisplay.DraftMode.NO_ACTIVE_DRAFT);
				return;
			}
			this.RequestDraftBegin();
			return;
		case DraftError.ErrorCode.DE_FEATURE_DISABLED:
			Debug.LogError("DraftManager.OnError - The Arena is currently disabled. Returning to the hub.");
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_FORGE", Array.Empty<object>());
				return;
			}
			return;
		case DraftError.ErrorCode.DE_SEASON_INCREMENTED:
			Error.AddWarningLoc("GLOBAL_ERROR_GENERIC_HEADER", "GLOBAL_ARENA_SEASON_ERROR_NOT_ACTIVE", Array.Empty<object>());
			DraftManager.Get().RefreshCurrentSeasonFromServer();
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.DRAFT)
			{
				Navigation.GoBack();
				return;
			}
			return;
		}
		Debug.LogErrorFormat("DraftManager.onError - UNHANDLED ERROR - See server logs for more info. ERROR: {0}", new object[]
		{
			draftError.ErrorCode_
		});
	}

	// Token: 0x060024B8 RID: 9400 RVA: 0x000B8FA8 File Offset: 0x000B71A8
	private void OnArenaSessionResponse()
	{
		ArenaSessionResponse arenaSessionResponse = Network.Get().GetArenaSessionResponse();
		this.OnArenaSessionResponsePacket(arenaSessionResponse);
	}

	// Token: 0x060024B9 RID: 9401 RVA: 0x000B8FC8 File Offset: 0x000B71C8
	public void OnArenaSessionResponsePacket(ArenaSessionResponse response)
	{
		if (response == null || response.ErrorCode != ErrorCode.ERROR_OK || !response.HasSession)
		{
			return;
		}
		this.m_hasReceivedSessionWinsLosses = true;
		this.m_wins = (response.HasSession ? response.Session.Wins : 0);
		this.m_losses = (response.HasSession ? response.Session.Losses : 0);
		this.m_currentSession = (response.HasSession ? response.Session : null);
		if (response.HasCurrentSeason)
		{
			this.m_currentSeason = response.CurrentSeason;
		}
		if (GameMgr.Get().IsArena() || GameMgr.Get().IsNextArena())
		{
			SessionRecord sessionRecord = new SessionRecord();
			sessionRecord.Wins = (uint)this.m_wins;
			sessionRecord.Losses = (uint)this.m_losses;
			sessionRecord.RunFinished = false;
			sessionRecord.SessionRecordType = SessionRecordType.ARENA;
			BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
		}
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x000B90A8 File Offset: 0x000B72A8
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CANCELED:
			if (DraftDisplay.Get() != null)
			{
				DraftDisplay.Get().HandleGameStartupFailure();
			}
			break;
		case FindGameState.SERVER_GAME_CONNECTING:
			if (GameMgr.Get().IsNextArena() && !this.m_hasReceivedSessionWinsLosses)
			{
				this.RefreshCurrentSeasonFromServer();
			}
			break;
		}
		return false;
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x000B9122 File Offset: 0x000B7322
	private void OnDraftPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (this.m_draftDeck != null)
		{
			StoreManager.Get().HideStore(ShopType.ARENA_STORE);
			return;
		}
		this.RequestDraftBegin();
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x000B913E File Offset: 0x000B733E
	public void RequestDraftBegin()
	{
		Network.Get().DraftBegin();
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x000B914C File Offset: 0x000B734C
	private void FireDraftDeckSetEvent()
	{
		DraftManager.DraftDeckSet[] array = this.m_draftDeckSetListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_draftDeck);
		}
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x000B9181 File Offset: 0x000B7381
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (!GameMgr.Get().IsArena())
		{
			return;
		}
		if (mode != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x000B91B0 File Offset: 0x000B73B0
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		if (playState != TAG_PLAYSTATE.WON)
		{
			if (playState - TAG_PLAYSTATE.LOST > 1)
			{
				return;
			}
			if (this.GetLosses() == 2)
			{
				this.NotifyOfFinalGame(false);
			}
		}
		else
		{
			NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
			if (netObject == null || this.GetWins() >= netObject.BestForgeWins)
			{
				NetCache.Get().RefreshNetObject<NetCache.NetCacheProfileProgress>();
			}
			if (this.GetWins() == 11)
			{
				this.NotifyOfFinalGame(true);
				return;
			}
		}
	}

	// Token: 0x0400146C RID: 5228
	private CollectionDeck m_draftDeck;

	// Token: 0x0400146D RID: 5229
	private bool m_hasReceivedSessionWinsLosses;

	// Token: 0x0400146E RID: 5230
	private int m_currentSlot;

	// Token: 0x0400146F RID: 5231
	private DraftSlotType m_currentSlotType;

	// Token: 0x04001470 RID: 5232
	private List<DraftSlotType> m_uniqueDraftSlotTypesForDeck = new List<DraftSlotType>();

	// Token: 0x04001471 RID: 5233
	private int m_validSlot;

	// Token: 0x04001472 RID: 5234
	private int m_maxSlot;

	// Token: 0x04001473 RID: 5235
	private int m_losses;

	// Token: 0x04001474 RID: 5236
	private int m_wins;

	// Token: 0x04001475 RID: 5237
	private int m_maxWins = int.MaxValue;

	// Token: 0x04001476 RID: 5238
	private int m_numTicketsOwned;

	// Token: 0x04001477 RID: 5239
	private bool m_isNewKey;

	// Token: 0x04001478 RID: 5240
	private bool m_deckActiveDuringSession;

	// Token: 0x04001479 RID: 5241
	private Network.RewardChest m_chest;

	// Token: 0x0400147A RID: 5242
	private bool m_inRewards;

	// Token: 0x0400147B RID: 5243
	private ArenaSession m_currentSession;

	// Token: 0x0400147C RID: 5244
	private List<DraftManager.DraftDeckSet> m_draftDeckSetListeners = new List<DraftManager.DraftDeckSet>();

	// Token: 0x0400147D RID: 5245
	private bool m_pendingRequestToDisablePremiums;

	// Token: 0x0400147E RID: 5246
	private int m_chosenIndex;

	// Token: 0x0400147F RID: 5247
	private ArenaSeasonInfo m_currentSeason;

	// Token: 0x04001480 RID: 5248
	private static readonly AssetReference DEFAULT_DRAFT_PAPER_TEXTURE = "Forge_Main_Paper.psd:64b6646e1c591d545885572fccd74259";

	// Token: 0x04001481 RID: 5249
	private static readonly AssetReference DEFAULT_DRAFT_PAPER_TEXTURE_PHONE = "Forge_Main_Paper_phone.psd:ab59053fdba3ebd40bfd6ced4fd246bc";

	// Token: 0x020015C2 RID: 5570
	// (Invoke) Token: 0x0600E19C RID: 57756
	public delegate void DraftDeckSet(CollectionDeck deck);
}
