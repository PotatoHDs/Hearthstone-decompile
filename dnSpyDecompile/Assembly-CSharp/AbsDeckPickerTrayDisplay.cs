using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public abstract class AbsDeckPickerTrayDisplay : MonoBehaviour
{
	// Token: 0x06002202 RID: 8706 RVA: 0x000A7DB0 File Offset: 0x000A5FB0
	public virtual void Awake()
	{
		this.m_randomDeckPickerTray.transform.localPosition = this.m_randomDecksShownBone.transform.localPosition;
		DeckPickerTray.Get().SetDeckPickerTrayDisplayReference(this);
		DeckPickerTray.Get().RegisterHandlers();
		if (this.m_backButton != null)
		{
			this.m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
			this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonReleased));
		}
		this.m_playButtonWidgetReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayButtonWidgetReady));
		if (this.m_heroPowerShadowQuad != null)
		{
			this.m_heroPowerShadowQuad.SetActive(false);
		}
		if (this.m_heroName != null)
		{
			this.m_heroName.RichText = false;
			this.m_heroName.Text = "";
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_slidingTray = base.gameObject.GetComponentInChildren<SlidingTray>();
			this.m_slidingTray.RegisterTrayToggleListener(new SlidingTray.TrayToggledListener(this.OnSlidingTrayToggled));
		}
		PopupDisplayManager.Get().AddPopupShownListener(new Action(this.OnPopupShown));
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x000A7ED8 File Offset: 0x000A60D8
	protected virtual void OnDestroy()
	{
		PopupDisplayManager popupDisplayManager = PopupDisplayManager.Get();
		if (popupDisplayManager != null)
		{
			popupDisplayManager.RemovePopupShownListener(new Action(this.OnPopupShown));
		}
		if (DeckPickerTray.IsInitialized())
		{
			DeckPickerTray.Get().UnregisterHandlers();
		}
		this.m_heroPowerDefs.DisposeValuesAndClear<string, DefLoader.DisposableFullDef>();
		DefLoader.DisposableFullDef selectedHeroPowerFullDef = this.m_selectedHeroPowerFullDef;
		if (selectedHeroPowerFullDef != null)
		{
			selectedHeroPowerFullDef.Dispose();
		}
		this.m_selectedHeroPowerFullDef = null;
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x000A7F35 File Offset: 0x000A6135
	private void OnApplicationPause(bool pauseStatus)
	{
		if (GameMgr.Get().IsFindingGame())
		{
			GameMgr.Get().CancelFindGame();
		}
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x000A7F4E File Offset: 0x000A614E
	public virtual void HandleGameStartupFailure()
	{
		this.SetPlayButtonEnabled(true);
		this.SetBackButtonEnabled(true);
		this.SetHeroButtonsEnabled(true);
		this.SetHeroRaised(true);
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x000A7F6C File Offset: 0x000A616C
	public virtual void OnServerGameStarted()
	{
		FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x000A7F86 File Offset: 0x000A6186
	public virtual void OnServerGameCanceled()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY || TavernBrawlManager.IsInTavernBrawlFriendlyChallenge())
		{
			return;
		}
		this.HandleGameStartupFailure();
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x000A7FA4 File Offset: 0x000A61A4
	protected virtual bool OnNavigateBackImplementation()
	{
		if (!this.m_backButton.IsEnabled())
		{
			return false;
		}
		SceneMgr.Mode mode = (SceneMgr.Get() != null) ? SceneMgr.Get().GetMode() : SceneMgr.Mode.INVALID;
		if (mode == SceneMgr.Mode.FRIENDLY)
		{
			if (FiresideGatheringManager.Get() != null && FiresideGatheringManager.Get().CurrentFiresideGatheringMode != FiresideGatheringManager.FiresideGatheringMode.NONE)
			{
				this.BackOutToFiresideGathering();
			}
			else
			{
				this.BackOutToHub();
			}
			if (FriendChallengeMgr.Get() != null)
			{
				FriendChallengeMgr.Get().CancelChallenge();
			}
		}
		this.SetPlayButtonEnabled(false);
		this.SetBackButtonEnabled(false);
		this.SetHeroButtonsEnabled(false);
		GameMgr.Get().CancelFindGame();
		SoundManager.Get().Stop(this.m_lastPickLine);
		return true;
	}

	// Token: 0x06002209 RID: 8713 RVA: 0x000A8040 File Offset: 0x000A6240
	protected virtual void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroActor = go.GetComponent<Actor>();
		if (this.m_heroActor == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.OnHeroActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		go.transform.parent = this.m_hierarchyDetails.transform;
		this.UpdateHeroActorOrientation();
		this.m_heroActor.SetUnlit();
		UnityEngine.Object.Destroy(this.m_heroActor.m_healthObject);
		UnityEngine.Object.Destroy(this.m_heroActor.m_attackObject);
		this.m_heroActor.Hide();
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x000A80E4 File Offset: 0x000A62E4
	protected virtual void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		try
		{
			EntityDef entityDef = (fullDef != null) ? fullDef.EntityDef : null;
			if (entityDef != null)
			{
				AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData heroFullDefLoadedCallbackData = userData as AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData;
				TAG_PREMIUM premium = (entityDef.GetCardSet() == TAG_CARD_SET.HERO_SKINS) ? TAG_PREMIUM.GOLDEN : CollectionManager.Get().GetBestCardPremium(cardId);
				heroFullDefLoadedCallbackData.HeroPickerButton.UpdateDisplay(fullDef, premium);
				Vector3 originalLocalPosition = (heroFullDefLoadedCallbackData.HeroPickerButton.m_raiseAndLowerRoot != null) ? heroFullDefLoadedCallbackData.HeroPickerButton.m_raiseAndLowerRoot.transform.localPosition : heroFullDefLoadedCallbackData.HeroPickerButton.transform.localPosition;
				heroFullDefLoadedCallbackData.HeroPickerButton.SetOriginalLocalPosition(originalLocalPosition);
				if (entityDef.GetClass() != TAG_CLASS.WHIZBANG)
				{
					string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(entityDef.GetCardId());
					if (!string.IsNullOrEmpty(heroPowerCardIdFromHero))
					{
						this.LoadHeroPowerDef(heroPowerCardIdFromHero);
					}
					else
					{
						Debug.LogErrorFormat("No hero power set up for hero {0}", new object[]
						{
							entityDef.GetCardId()
						});
					}
				}
			}
			this.m_heroDefsLoading--;
		}
		finally
		{
			if (fullDef != null)
			{
				((IDisposable)fullDef).Dispose();
			}
		}
	}

	// Token: 0x0600220B RID: 8715 RVA: 0x000A81EC File Offset: 0x000A63EC
	protected virtual void OnSlidingTrayToggled(bool isShowing)
	{
		if (!isShowing && PracticePickerTrayDisplay.Get() != null && PracticePickerTrayDisplay.Get().IsShown())
		{
			Navigation.GoBack();
		}
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x000A8210 File Offset: 0x000A6410
	public virtual void ResetCurrentMode()
	{
		if (this.m_selectedHeroButton != null)
		{
			this.SetPlayButtonEnabled(true);
			this.SetHeroRaised(true);
		}
		else
		{
			this.SetPlayButtonEnabled(false);
		}
		this.SetHeroButtonsEnabled(true);
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void PreUnload()
	{
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x000A8240 File Offset: 0x000A6440
	public virtual void InitAssets()
	{
		Log.PlayModeInvestigation.PrintInfo("AbsDeckPickerTrayDisplay.InitAssets() called", Array.Empty<object>());
		this.m_PreviousFormatType = Options.GetFormatType();
		this.m_PreviousInRankedPlayMode = Options.GetInRankedPlayMode();
		this.m_HeroPickerButtonCount = this.ValidateHeroCount();
		this.SetupHeroLayout();
		this.LoadHero();
		if (this.ShouldShowHeroPower())
		{
			this.LoadHeroPower();
			this.LoadGoldenHeroPower();
		}
		base.StartCoroutine(this.LoadHeroButtons(null));
		base.StartCoroutine(this.InitModeWhenReady());
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x000A82C6 File Offset: 0x000A64C6
	protected virtual IEnumerator WaitForHeroPickerButtonsLoaded()
	{
		while (this.m_heroButtons.Count < this.m_HeroPickerButtonCount)
		{
			yield return null;
		}
		foreach (HeroPickerButton button in this.m_heroButtons)
		{
			while (button.GetComponent<WidgetTemplate>().IsChangingStates)
			{
				yield return null;
			}
			button = null;
		}
		List<HeroPickerButton>.Enumerator enumerator = default(List<HeroPickerButton>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x000A82D5 File Offset: 0x000A64D5
	protected virtual IEnumerator InitDeckDependentElements()
	{
		yield return base.StartCoroutine(this.WaitForHeroPickerButtonsLoaded());
		this.InitForMode(SceneMgr.Get().GetMode());
		yield break;
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x000A82E4 File Offset: 0x000A64E4
	protected virtual IEnumerator InitHeroPickerElements()
	{
		yield return base.StartCoroutine(this.WaitForHeroPickerButtonsLoaded());
		this.InitHeroPickerButtons();
		yield break;
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x000A82F3 File Offset: 0x000A64F3
	protected virtual IEnumerator InitModeWhenReady()
	{
		while (this.m_heroDefsLoading > 0 || this.m_heroActor == null || ((this.m_heroPowerActor == null || this.m_goldenHeroPowerActor == null) && this.ShouldShowHeroPower()))
		{
			yield return null;
		}
		this.m_Loaded = true;
		PlayGameScene playGameScene = SceneMgr.Get().GetScene() as PlayGameScene;
		if (playGameScene != null)
		{
			playGameScene.OnDeckPickerLoaded(this);
		}
		this.FireDeckTrayLoadedEvent();
		this.InitRichPresence(null);
		this.SetBackButtonEnabled(true);
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY || TavernBrawlManager.IsInTavernBrawlFriendlyChallenge())
		{
			if (!FriendChallengeMgr.Get().HasChallenge())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Get().GetPrevMode(), SceneMgr.TransitionHandlerType.SCENEMGR, null);
				yield break;
			}
			FriendChallengeMgr.Get().AddChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		}
		yield break;
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x000A8304 File Offset: 0x000A6504
	protected virtual void InitForMode(SceneMgr.Mode mode)
	{
		if (mode <= SceneMgr.Mode.FRIENDLY)
		{
			if (mode == SceneMgr.Mode.COLLECTIONMANAGER)
			{
				this.SetPlayButtonText(GameStrings.Get("GLUE_CHOOSE"));
				return;
			}
			if (mode != SceneMgr.Mode.FRIENDLY)
			{
				return;
			}
		}
		else if (mode != SceneMgr.Mode.ADVENTURE)
		{
			if (mode != SceneMgr.Mode.FIRESIDE_GATHERING)
			{
				return;
			}
		}
		else
		{
			if (AdventureConfig.Get() != null && AdventureConfig.Get().IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
			{
				this.SetPlayButtonText(GameStrings.Get("GLUE_CHOOSE"));
				return;
			}
			return;
		}
		if (FiresideGatheringManager.Get().InBrawlMode())
		{
			this.SetHeaderForTavernBrawl();
			return;
		}
		string key = (mode == SceneMgr.Mode.FIRESIDE_GATHERING) ? "GLUE_CHOOSE_OPPONENT" : "GLUE_CHOOSE";
		this.SetPlayButtonText(GameStrings.Get(key));
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void InitHeroPickerButtons()
	{
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x000A8398 File Offset: 0x000A6598
	protected virtual void InitRichPresence(Global.PresenceStatus? newStatus = null)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.FRIENDLY)
		{
			if (mode != SceneMgr.Mode.ADVENTURE)
			{
				if (mode == SceneMgr.Mode.TAVERN_BRAWL)
				{
					if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
					{
						newStatus = new Global.PresenceStatus?(Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING);
					}
				}
			}
			else
			{
				AdventureData.Adventuresubscene currentSubScene = AdventureConfig.Get().CurrentSubScene;
				if (currentSubScene == AdventureData.Adventuresubscene.PRACTICE)
				{
					newStatus = new Global.PresenceStatus?(Global.PresenceStatus.PRACTICE_DECKPICKER);
				}
			}
		}
		else
		{
			newStatus = new Global.PresenceStatus?(Global.PresenceStatus.FRIENDLY_DECKPICKER);
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				newStatus = new Global.PresenceStatus?(Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING);
			}
		}
		if (newStatus != null)
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				newStatus.Value
			});
		}
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x000A8438 File Offset: 0x000A6638
	protected virtual void TransitionToFormatType(FormatType formatType, bool inRankedPlayMode, float transitionSpeed = 2f)
	{
		Options.SetFormatType(formatType);
		Options.SetInRankedPlayMode(inRankedPlayMode);
		this.UpdateHeroActorOrientation();
	}

	// Token: 0x06002217 RID: 8727 RVA: 0x000A844C File Offset: 0x000A664C
	protected virtual void PlayGame()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.FRIENDLY && mode != SceneMgr.Mode.TAVERN_BRAWL)
		{
			if (mode == SceneMgr.Mode.FIRESIDE_GATHERING)
			{
				bool flag = FiresideGatheringManager.Get().InBrawlMode();
				bool flag2 = TavernBrawlManager.Get().SelectHeroBeforeMission();
				bool flag3 = false;
				if (TavernBrawlManager.Get().CurrentMission() != null)
				{
					flag3 = GameUtils.IsAIMission(TavernBrawlManager.Get().CurrentMission().missionId);
				}
				if (flag && flag3)
				{
					if (TavernBrawlManager.Get().SelectHeroBeforeMission())
					{
						int heroCardDbId = GameUtils.TranslateCardIdToDbId(this.m_selectedHeroButton.GetEntityDef().GetCardId(), false);
						TavernBrawlManager.Get().StartGameWithHero(heroCardDbId);
					}
				}
				else
				{
					if (flag && flag2)
					{
						int num = GameUtils.TranslateCardIdToDbId(this.m_selectedHeroButton.GetEntityDef().GetCardId(), false);
						FriendChallengeMgr.Get().SelectHeroBeforeSendingChallenge((long)num);
					}
					if (!flag3 && ((flag && flag2) || !flag))
					{
						FiresideGatheringDisplay.Get().ShowOpponentPickerTray(delegate
						{
							this.SetPlayButtonEnabled(true);
						});
						this.SetPlayButtonEnabled(false);
					}
				}
			}
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			if (this.m_selectedHeroButton == null)
			{
				Debug.LogError("Trying to play Tavern Brawl game with no m_selectedHeroButton!");
				return;
			}
			int num2 = GameUtils.TranslateCardIdToDbId(this.m_selectedHeroButton.GetEntityDef().GetCardId(), false);
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				FriendChallengeMgr.Get().SelectHero((long)num2);
				FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_OPPONENT_WAITING_READY", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
			}
			else
			{
				TavernBrawlManager.Get().StartGameWithHero(num2);
			}
		}
		SoundManager.Get().Stop(this.m_lastPickLine);
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x000A85D8 File Offset: 0x000A67D8
	protected virtual void ShowHero()
	{
		this.m_heroActor.Show();
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(this.m_heroActor.GetEntityDef().GetCardId());
		if (this.ShouldShowHeroPower() && !string.IsNullOrEmpty(heroPowerCardIdFromHero))
		{
			TAG_PREMIUM premium = this.m_heroActor.GetPremium();
			this.ShowHeroPower(premium);
		}
		else
		{
			this.m_heroPowerShadowQuad.SetActive(false);
			if (this.m_heroPowerActor != null)
			{
				this.m_heroPowerActor.Hide();
			}
			if (this.m_goldenHeroPower != null)
			{
				this.m_goldenHeroPowerActor.Hide();
			}
		}
		if (this.m_selectedHeroName == null)
		{
			this.m_heroName.Text = "";
		}
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x000A8684 File Offset: 0x000A6884
	protected virtual void SelectHero(HeroPickerButton button, bool showTrayForPhone = true)
	{
		if (button == this.m_selectedHeroButton && !UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		this.DeselectLastSelectedHero();
		if (AbsDeckPickerTrayDisplay.HIGHLIGHT_SELECTED_DECK)
		{
			button.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			button.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		this.m_selectedHeroButton = button;
		this.UpdateHeroInfo(button);
		button.SetSelected(true);
		this.ShowPreconHero(true);
		this.RemoveHeroLockedTooltip();
		if (UniversalInputManager.UsePhoneUI && showTrayForPhone)
		{
			this.m_slidingTray.ToggleTraySlider(true, null, true);
		}
		bool flag = this.IsHeroPlayable(button);
		if (flag && !NotificationManager.Get().IsQuotePlaying && button.HasCardDef)
		{
			SoundManager.Get().LoadAndPlay(button.HeroPickerSelectedPrefab, button.gameObject, 1f, new SoundManager.LoadedCallback(this.OnLastPickLineLoaded));
		}
		this.SetPlayButtonEnabled(flag);
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void UpdateHeroInfo(HeroPickerButton button)
	{
	}

	// Token: 0x0600221B RID: 8731 RVA: 0x000A8760 File Offset: 0x000A6960
	protected virtual void BackOutToHub()
	{
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x000A879C File Offset: 0x000A699C
	protected virtual void BackOutToFiresideGathering()
	{
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING))
		{
			return;
		}
		if (DeckPickerTrayDisplay.Get() != null)
		{
			FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.FIRESIDE_GATHERING, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_slidingTray.ToggleTraySlider(false, null, true);
		}
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x000A8808 File Offset: 0x000A6A08
	protected void UpdateValidHeroClasses()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (Options.GetFormatType() == FormatType.FT_CLASSIC && mode != SceneMgr.Mode.ADVENTURE)
		{
			this.m_validClasses = new List<TAG_CLASS>(GameUtils.CLASSIC_ORDERED_HERO_CLASSES);
		}
		else
		{
			this.m_validClasses = new List<TAG_CLASS>(GameUtils.ORDERED_HERO_CLASSES);
		}
		if (!this.IsChoosingHero())
		{
			this.m_validClasses.Add(TAG_CLASS.WHIZBANG);
		}
		ScenarioDbId? scenarioDbId = null;
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			scenarioDbId = new ScenarioDbId?(AdventureConfig.Get().GetMission());
		}
		if (mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode()) || FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			scenarioDbId = new ScenarioDbId?((ScenarioDbId)TavernBrawlManager.Get().CurrentMission().missionId);
		}
		if (scenarioDbId != null)
		{
			ScenarioDbId? scenarioDbId2 = scenarioDbId;
			ScenarioDbId scenarioDbId3 = ScenarioDbId.INVALID;
			if (!(scenarioDbId2.GetValueOrDefault() == scenarioDbId3 & scenarioDbId2 != null))
			{
				ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenarioDbId.Value);
				for (int i = 0; i < record.ClassExclusions.Count; i++)
				{
					this.m_validClasses.Remove((TAG_CLASS)record.ClassExclusions[i].ClassId);
				}
			}
		}
	}

	// Token: 0x0600221E RID: 8734 RVA: 0x000A8928 File Offset: 0x000A6B28
	protected virtual int ValidateHeroCount()
	{
		this.UpdateValidHeroClasses();
		return this.m_validClasses.Count;
	}

	// Token: 0x0600221F RID: 8735 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool ShouldShowHeroPower()
	{
		return false;
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x000A893C File Offset: 0x000A6B3C
	private bool DeckPickerInRankedPlayMode()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		return mode == SceneMgr.Mode.TOURNAMENT && Options.GetInRankedPlayMode();
	}

	// Token: 0x06002221 RID: 8737 RVA: 0x000A8960 File Offset: 0x000A6B60
	private Transform GetActiveHeroBone()
	{
		bool flag = this.DeckPickerInRankedPlayMode();
		if (this.m_heroRaised)
		{
			if (!flag)
			{
				return this.m_Hero_Bone;
			}
			return this.m_Ranked_Hero_Bone;
		}
		else
		{
			if (!flag)
			{
				return this.m_Hero_BoneDown;
			}
			return this.m_Ranked_Hero_BoneDown;
		}
	}

	// Token: 0x06002222 RID: 8738 RVA: 0x000A899D File Offset: 0x000A6B9D
	private Transform GetActiveHeroNameBone()
	{
		if (!this.DeckPickerInRankedPlayMode())
		{
			return this.m_HeroName_Bone;
		}
		return this.m_Ranked_HeroName_Bone;
	}

	// Token: 0x06002223 RID: 8739 RVA: 0x000A89B4 File Offset: 0x000A6BB4
	private void UpdateHeroActorOrientation()
	{
		if (this.m_heroActor != null)
		{
			iTween.Stop(this.m_heroActor.gameObject);
			Transform activeHeroBone = this.GetActiveHeroBone();
			if (activeHeroBone != null)
			{
				this.m_heroActor.transform.localScale = activeHeroBone.localScale;
				this.m_heroActor.transform.localPosition = activeHeroBone.localPosition;
			}
		}
		if (this.m_heroName != null)
		{
			Transform activeHeroNameBone = this.GetActiveHeroNameBone();
			if (activeHeroNameBone != null)
			{
				this.m_heroName.transform.localScale = activeHeroNameBone.localScale;
				this.m_heroName.transform.localPosition = activeHeroNameBone.localPosition;
			}
		}
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x000A8A68 File Offset: 0x000A6C68
	protected virtual void SetHeroRaised(bool raised)
	{
		this.m_heroRaised = raised;
		Transform activeHeroBone = this.GetActiveHeroBone();
		if (activeHeroBone == null || this.m_HeroPower_Bone == null || this.m_HeroPower_BoneDown == null)
		{
			Debug.LogWarning("SetHeroRaised tried using transforms that were undefined!");
			return;
		}
		Vector3 localPosition = activeHeroBone.localPosition;
		Vector3 position = raised ? this.m_HeroPower_Bone.localPosition : this.m_HeroPower_BoneDown.localPosition;
		this.MoveToRaisedPosition(localPosition, this.m_heroActor, raised, null);
		if (this.ShouldShowHeroPower())
		{
			this.m_heroPowerShadowQuad.SetActive(raised);
			this.MoveToRaisedPosition(position, this.m_heroPowerActor, raised, this.m_heroPower);
			this.MoveToRaisedPosition(position, this.m_goldenHeroPowerActor, raised, this.m_goldenHeroPower);
		}
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x000A8B24 File Offset: 0x000A6D24
	private void MoveToRaisedPosition(Vector3 position, Actor actor, bool raised, PegUIElement pegUiElement = null)
	{
		if (actor == null)
		{
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutExpo,
			"islocal",
			true
		});
		iTween.MoveTo(actor.gameObject, args);
		if (pegUiElement != null)
		{
			Collider component = pegUiElement.GetComponent<Collider>();
			if (component != null)
			{
				component.enabled = raised;
				return;
			}
			Debug.LogWarning("Could not locate Collider for " + pegUiElement.name + " when trying to SetHeroRaised");
		}
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x000A8BDC File Offset: 0x000A6DDC
	protected virtual void SetPlayButtonEnabled(bool enable)
	{
		if (enable && SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY && !FriendChallengeMgr.Get().HasChallenge())
		{
			return;
		}
		this.m_playButtonEnabled = enable;
		if (this.m_playButton != null && this.m_playButton.IsEnabled() != enable)
		{
			if (enable)
			{
				this.m_playButton.Enable();
				return;
			}
			this.m_playButton.Disable(false);
		}
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x000A8C44 File Offset: 0x000A6E44
	protected virtual void SetCollectionButtonEnabled(bool enable)
	{
		if (this.m_collectionButton != null)
		{
			this.m_collectionButton.SetEnabled(enable, false);
			this.m_collectionButton.Flip(enable, false);
		}
	}

	// Token: 0x06002228 RID: 8744 RVA: 0x000A8C70 File Offset: 0x000A6E70
	protected virtual void SetHeroButtonsEnabled(bool enable)
	{
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			if (!heroPickerButton.IsLocked() || !enable)
			{
				heroPickerButton.SetEnabled(enable, false);
			}
		}
	}

	// Token: 0x06002229 RID: 8745 RVA: 0x000A8CD0 File Offset: 0x000A6ED0
	protected virtual void SetHeaderForTavernBrawl()
	{
		string key = "GLUE_CHOOSE";
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			key = ((TavernBrawlManager.Get().CurrentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING) ? "GLUE_BRAWL_PATRON" : "GLUE_BRAWL_FRIEND");
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			key = "GLUE_BRAWL";
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
			{
				key = "GLUE_CHOOSE_OPPONENT";
				TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
				TavernBrawlMission mission2 = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
				bool flag = FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL && mission != null && mission.GameType == GameType.GT_FSG_BRAWL_1P_VS_AI;
				bool flag2 = FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL && mission2 != null && mission2.GameType == GameType.GT_TB_1P_VS_AI;
				if (flag || flag2)
				{
					key = "GLUE_CHOOSE";
				}
			}
		}
		this.SetPlayButtonText(GameStrings.Get(key));
	}

	// Token: 0x0600222A RID: 8746 RVA: 0x000A8D9F File Offset: 0x000A6F9F
	protected virtual bool IsHeroPlayable(HeroPickerButton button)
	{
		return !button.IsLocked();
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x000A8DAC File Offset: 0x000A6FAC
	public virtual int GetSelectedHeroID()
	{
		if (this.m_selectedHeroButton != null)
		{
			GuestHeroDbfRecord guestHero = this.m_selectedHeroButton.GetGuestHero();
			if (guestHero != null)
			{
				return guestHero.CardId;
			}
		}
		return 0;
	}

	// Token: 0x0600222C RID: 8748 RVA: 0x000A8DDE File Offset: 0x000A6FDE
	public virtual long GetSelectedDeckID()
	{
		if (!(this.m_selectedHeroButton == null))
		{
			return this.m_selectedHeroButton.GetPreconDeckID();
		}
		return 0L;
	}

	// Token: 0x0600222D RID: 8749
	protected abstract void GoBackUntilOnNavigateBackCalled();

	// Token: 0x0600222E RID: 8750 RVA: 0x00004EB5 File Offset: 0x000030B5
	protected virtual void OnBackButtonReleased(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x0600222F RID: 8751 RVA: 0x000A8DFC File Offset: 0x000A6FFC
	protected virtual void OnPlayGameButtonReleased(UIEvent e)
	{
		if (!Network.IsLoggedIn() && SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			DialogManager.Get().ShowReconnectHelperDialog(null, null);
			return;
		}
		if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			return;
		}
		if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			return;
		}
		this.SetPlayButtonEnabled(false);
		this.SetHeroButtonsEnabled(false);
		this.PlayGame();
	}

	// Token: 0x06002230 RID: 8752 RVA: 0x000A8E60 File Offset: 0x000A7060
	protected virtual void OnHeroButtonReleased(UIEvent e)
	{
		HeroPickerButton button = (HeroPickerButton)e.GetElement();
		this.SelectHero(button, true);
		SoundManager.Get().LoadAndPlay("tournament_screen_select_hero.prefab:2b9bdf587ac07084b8f7d5c4bce33ecf");
	}

	// Token: 0x06002231 RID: 8753 RVA: 0x000A8E95 File Offset: 0x000A7095
	protected virtual void OnHeroMouseOver(UIEvent e)
	{
		if (e == null)
		{
			return;
		}
		((HeroPickerButton)e.GetElement()).SetHighlightState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6");
	}

	// Token: 0x06002232 RID: 8754 RVA: 0x000A8EC4 File Offset: 0x000A70C4
	protected virtual void OnHeroMouseOut(UIEvent e)
	{
		HeroPickerButton heroPickerButton = (HeroPickerButton)e.GetElement();
		if (!UniversalInputManager.UsePhoneUI || !heroPickerButton.IsSelected())
		{
			heroPickerButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	// Token: 0x06002233 RID: 8755 RVA: 0x000A8EFC File Offset: 0x000A70FC
	protected virtual void OnHeroPowerMouseOver(UIEvent e)
	{
		this.m_isMouseOverHeroPower = true;
		if (this.m_heroActor.GetPremium() == TAG_PREMIUM.GOLDEN)
		{
			if (this.m_goldenHeroPowerBigCard == null)
			{
				AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, TAG_PREMIUM.GOLDEN), new PrefabCallback<GameObject>(this.OnGoldenHeroPowerLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
				return;
			}
			this.ShowHeroPowerBigCard(true);
			return;
		}
		else
		{
			if (this.m_heroPowerBigCard == null)
			{
				AssetLoader.Get().InstantiatePrefab("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", new PrefabCallback<GameObject>(this.OnHeroPowerLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
				return;
			}
			this.ShowHeroPowerBigCard(false);
			return;
		}
	}

	// Token: 0x06002234 RID: 8756 RVA: 0x000A8F94 File Offset: 0x000A7194
	protected virtual void OnHeroPowerMouseOut(UIEvent e)
	{
		this.m_isMouseOverHeroPower = false;
		if (this.m_heroPowerBigCard != null)
		{
			iTween.Stop(this.m_heroPowerBigCard.gameObject);
			this.m_heroPowerBigCard.Hide();
		}
		if (this.m_goldenHeroPowerBigCard != null)
		{
			iTween.Stop(this.m_goldenHeroPowerBigCard.gameObject);
			this.m_goldenHeroPowerBigCard.Hide();
		}
	}

	// Token: 0x06002235 RID: 8757 RVA: 0x000A8FFC File Offset: 0x000A71FC
	protected void LoadHeroPowerDef(string heroPowerCardId)
	{
		DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(heroPowerCardId, null);
		this.m_heroPowerDefs.SetOrReplaceDisposable(heroPowerCardId, fullDef);
	}

	// Token: 0x06002236 RID: 8758 RVA: 0x000A9024 File Offset: 0x000A7224
	protected void OnPlayButtonWidgetReady(VisualController visualController)
	{
		if (visualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!", Array.Empty<object>());
			return;
		}
		this.m_playButton = visualController.GetComponent<PlayButton>();
		if (this.m_playButton == null)
		{
			return;
		}
		if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL)
		{
			visualController.Owner.TriggerEvent("LANTERN", default(Widget.TriggerEventParameters));
		}
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayGameButtonReleased));
		this.SetPlayButtonEnabled(this.m_playButtonEnabled);
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x000A90B8 File Offset: 0x000A72B8
	protected void OnHeroPickerButtonWidgetReady(WidgetInstance widget)
	{
		HeroPickerButton componentInChildren = widget.GetComponentInChildren<HeroPickerButton>();
		this.m_heroButtons.Add(componentInChildren);
		this.SetUpHeroPickerButton(componentInChildren, this.m_heroButtons.Count - 1);
		componentInChildren.Lock();
		componentInChildren.Activate(false);
		componentInChildren.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnHeroButtonReleased));
		componentInChildren.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnHeroMouseOver));
		componentInChildren.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHeroMouseOut));
		Vector3 originalLocalPosition = (componentInChildren.m_raiseAndLowerRoot != null) ? componentInChildren.m_raiseAndLowerRoot.transform.localPosition : base.transform.localPosition;
		componentInChildren.SetOriginalLocalPosition(originalLocalPosition);
	}

	// Token: 0x06002238 RID: 8760 RVA: 0x000A916C File Offset: 0x000A736C
	protected void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroPowerActor = go.GetComponent<Actor>();
		if (this.m_heroPowerActor == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_heroPower = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>();
		GameUtils.SetParent(go, this.m_heroPowerContainer, false);
		go.transform.localScale = this.m_HeroPower_Bone.localScale;
		go.transform.localPosition = this.m_HeroPower_Bone.localPosition;
		this.m_heroPowerActor.SetUnlit();
		this.m_heroPower.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnHeroPowerMouseOver));
		this.m_heroPower.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHeroPowerMouseOut));
		this.m_heroPowerActor.Hide();
		this.m_heroPower.GetComponent<Collider>().enabled = false;
		this.m_heroName.Text = "";
		base.StartCoroutine(this.UpdateHeroSkinHeroPower());
	}

	// Token: 0x06002239 RID: 8761 RVA: 0x000A9284 File Offset: 0x000A7484
	protected void OnGoldenHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_goldenHeroPowerActor = go.GetComponent<Actor>();
		if (this.m_goldenHeroPowerActor == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_goldenHeroPower = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>();
		GameUtils.SetParent(go, this.m_heroPowerContainer, false);
		go.transform.localScale = this.m_HeroPower_Bone.localScale;
		go.transform.localPosition = this.m_HeroPower_Bone.localPosition;
		this.m_goldenHeroPowerActor.SetUnlit();
		this.m_goldenHeroPowerActor.SetPremium(TAG_PREMIUM.GOLDEN);
		this.m_goldenHeroPower.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnHeroPowerMouseOver));
		this.m_goldenHeroPower.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHeroPowerMouseOut));
		this.m_goldenHeroPowerActor.Hide();
		this.m_goldenHeroPower.GetComponent<Collider>().enabled = false;
		this.m_heroName.Text = "";
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x000A939C File Offset: 0x000A759C
	protected void OnHeroPowerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		component.transform.parent = this.m_heroPower.transform;
		component.TurnOffCollider();
		SceneUtils.SetLayer(component.gameObject, this.m_heroPower.gameObject.layer, null);
		this.m_heroPowerBigCard = component;
		if (this.m_isMouseOverHeroPower)
		{
			this.ShowHeroPowerBigCard(false);
		}
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x000A943C File Offset: 0x000A763C
	protected void OnGoldenHeroPowerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		component.transform.parent = this.m_heroPower.transform;
		component.TurnOffCollider();
		SceneUtils.SetLayer(component.gameObject, this.m_heroPower.gameObject.layer, null);
		this.m_goldenHeroPowerBigCard = component;
		if (this.m_isMouseOverHeroPower)
		{
			this.ShowHeroPowerBigCard(true);
		}
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x000A94DA File Offset: 0x000A76DA
	protected void OnPopupShown()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_slidingTray.ToggleTraySlider(false, null, false);
		}
	}

	// Token: 0x0600223D RID: 8765 RVA: 0x000A94F6 File Offset: 0x000A76F6
	private void OnLastPickLineLoaded(AudioSource source, object callbackData)
	{
		SoundManager.Get().Stop(this.m_lastPickLine);
		this.m_lastPickLine = source;
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x000A9510 File Offset: 0x000A7710
	protected virtual void OnFriendChallengeWaitingForOpponentDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			return;
		}
		if (FriendChallengeMgr.Get().AmIInGameState())
		{
			return;
		}
		this.ResetCurrentMode();
		FriendChallengeMgr.Get().DeselectDeckOrHero();
		FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x000A9540 File Offset: 0x000A7740
	protected virtual void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		if (challengeEvent == FriendChallengeEvent.SELECTED_DECK_OR_HERO)
		{
			if (!SceneMgr.Get().IsInTavernBrawlMode())
			{
				if (player == BnetPresenceMgr.Get().GetMyPlayer())
				{
					return;
				}
				if (!FriendChallengeMgr.Get().DidISelectDeckOrHero())
				{
					return;
				}
				FriendlyChallengeHelper.Get().HideFriendChallengeWaitingForOpponentDialog();
				return;
			}
		}
		else
		{
			if (challengeEvent == FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE || challengeEvent == FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS || challengeEvent == FriendChallengeEvent.QUEUE_CANCELED)
			{
				FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
				this.GoBackUntilOnNavigateBackCalled();
				return;
			}
			if (challengeEvent == FriendChallengeEvent.DESELECTED_DECK_OR_HERO)
			{
				if (player == BnetPresenceMgr.Get().GetMyPlayer())
				{
					return;
				}
				if (FriendChallengeMgr.Get().DidISelectDeckOrHero())
				{
					FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", new AlertPopup.ResponseCallback(this.OnFriendChallengeWaitingForOpponentDialogResponse));
					return;
				}
				this.ResetCurrentMode();
				this.SetBackButtonEnabled(true);
			}
		}
	}

	// Token: 0x06002240 RID: 8768 RVA: 0x000A95EF File Offset: 0x000A77EF
	protected IEnumerator LoadHeroButtons(int? m_cheatOverrideHeroPickerButtonCount = null)
	{
		if (m_cheatOverrideHeroPickerButtonCount != null)
		{
			this.m_HeroPickerButtonCount = m_cheatOverrideHeroPickerButtonCount.Value;
		}
		else
		{
			this.m_HeroPickerButtonCount = this.ValidateHeroCount();
		}
		this.SetupHeroLayout();
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			UnityEngine.Object.Destroy(heroPickerButton.gameObject);
		}
		this.m_heroButtons.Clear();
		HeroPickerDataModel heroPickerDataModel = this.GetHeroPickerDataModel();
		for (int i = 0; i < this.m_HeroPickerButtonCount; i++)
		{
			WidgetInstance heroPickerButtonWidget = WidgetInstance.Create(this.m_heroButtonWidgetPrefab, false);
			if (heroPickerDataModel != null)
			{
				heroPickerButtonWidget.BindDataModel(heroPickerDataModel, false);
			}
			heroPickerButtonWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnHeroPickerButtonWidgetReady(heroPickerButtonWidget);
			}, null, true);
		}
		yield return base.StartCoroutine(this.InitDeckDependentElements());
		base.StartCoroutine(this.InitHeroPickerElements());
		yield break;
	}

	// Token: 0x06002241 RID: 8769 RVA: 0x000A9608 File Offset: 0x000A7808
	protected void SetupHeroLayout()
	{
		if (this.m_HeroPickerButtonCount <= 0 || this.m_HeroPickerButtonCount > this.m_heroPickerButtonBonesByHeroCount.Count || this.m_heroPickerButtonBonesByHeroCount[this.m_HeroPickerButtonCount] == null)
		{
			Log.Adventures.PrintWarning("Deck/Class Picker Instantiated with an unsupported amount of heroes: " + this.m_HeroPickerButtonCount, Array.Empty<object>());
			return;
		}
		GameObject gameObject = this.m_heroPickerButtonBonesByHeroCount[this.m_HeroPickerButtonCount];
		this.m_heroBones = new List<Transform>();
		this.m_heroBones.AddRange(gameObject.GetComponentsInChildren<Transform>());
		this.m_heroBones.RemoveAt(0);
		if (this.m_heroBones.Count != this.m_HeroPickerButtonCount)
		{
			Log.Adventures.PrintWarning("Layout for {0} heroes yielded an incorrect amount of transforms. This will result in errors when displaying heroes!", new object[]
			{
				this.m_HeroPickerButtonCount
			});
		}
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x000A96DF File Offset: 0x000A78DF
	protected void LoadHero()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x000A9705 File Offset: 0x000A7905
	protected void LoadHeroPower()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x000A972A File Offset: 0x000A792A
	protected void LoadGoldenHeroPower()
	{
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_HERO_POWER, TAG_PREMIUM.GOLDEN), new PrefabCallback<GameObject>(this.OnGoldenHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002245 RID: 8773 RVA: 0x000A9751 File Offset: 0x000A7951
	protected IEnumerator UpdateHeroSkinHeroPower()
	{
		while (this.m_heroActor == null || !this.m_heroActor.HasCardDef)
		{
			yield return null;
		}
		HeroSkinHeroPower componentInChildren = this.m_heroPowerActor.gameObject.GetComponentInChildren<HeroSkinHeroPower>();
		if (componentInChildren == null)
		{
			yield break;
		}
		componentInChildren.m_Actor.AlwaysRenderPremiumPortrait = !GameUtils.IsVanillaHero(this.m_heroActor.GetEntityDef().GetCardId());
		componentInChildren.m_Actor.UpdateMaterials();
		componentInChildren.m_Actor.UpdateTextures();
		yield break;
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x000A9760 File Offset: 0x000A7960
	protected void UpdateHeroPowerInfo(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		this.SetHeroPowerActorColliderEnabled(true);
		this.m_heroPowerActor.SetFullDef(def);
		DefLoader.DisposableFullDef selectedHeroPowerFullDef = this.m_selectedHeroPowerFullDef;
		if (selectedHeroPowerFullDef != null)
		{
			selectedHeroPowerFullDef.Dispose();
		}
		this.m_selectedHeroPowerFullDef = def.Share();
		this.m_heroPowerActor.SetUnlit();
		def.CardDef.m_AlwaysRenderPremiumPortrait = false;
		this.m_heroPowerActor.UpdateAllComponents();
		this.m_goldenHeroPowerActor.SetFullDef(def);
		this.m_goldenHeroPowerActor.UpdateAllComponents();
		this.m_goldenHeroPowerActor.SetUnlit();
		this.ShowHeroPower(premium);
		if (premium != TAG_PREMIUM.GOLDEN && this.m_heroActor.GetEntityDef().GetCardSet() == TAG_CARD_SET.HERO_SKINS)
		{
			base.StartCoroutine(this.UpdateHeroSkinHeroPower());
		}
	}

	// Token: 0x06002247 RID: 8775 RVA: 0x000A9810 File Offset: 0x000A7A10
	protected void UpdateCustomHeroPowerBigCard(GameObject heroPowerBigCard)
	{
		if (!this.m_heroActor.HasCardDef)
		{
			Debug.LogWarning("AbsDeckPickerTrayDisplay.UpdateCustomHeroPowerBigCard heroCardDef = null!");
			return;
		}
		Actor componentInChildren = heroPowerBigCard.GetComponentInChildren<Actor>();
		TAG_CARD_SET cardSet = this.m_heroActor.GetEntityDef().GetCardSet();
		componentInChildren.AlwaysRenderPremiumPortrait = (cardSet == TAG_CARD_SET.HERO_SKINS);
		componentInChildren.UpdateMaterials();
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x000A985C File Offset: 0x000A7A5C
	protected void ShowHeroPowerBigCard(bool isGolden = false)
	{
		Actor actor = isGolden ? this.m_goldenHeroPowerBigCard : this.m_heroPowerBigCard;
		Actor actor2 = (!isGolden) ? this.m_goldenHeroPowerBigCard : this.m_heroPowerBigCard;
		DefLoader.DisposableFullDef selectedHeroPowerFullDef = this.m_selectedHeroPowerFullDef;
		if (((selectedHeroPowerFullDef != null) ? selectedHeroPowerFullDef.CardDef : null) == null)
		{
			return;
		}
		actor.SetCardDef(this.m_selectedHeroPowerFullDef.DisposableCardDef);
		actor.SetEntityDef(this.m_selectedHeroPowerFullDef.EntityDef);
		actor.UpdateAllComponents();
		actor.Show();
		if (actor2 != null)
		{
			actor2.Hide();
		}
		this.UpdateCustomHeroPowerBigCard(actor.gameObject);
		float d = 1f;
		float d2 = 1.5f;
		Vector3 vector = UniversalInputManager.Get().IsTouchMode() ? new Vector3(0.019f, 0.54f, 3f) : new Vector3(0.019f, 0.54f, -1.12f);
		GameObject gameObject = actor.gameObject;
		if (UniversalInputManager.UsePhoneUI)
		{
			gameObject.transform.localPosition = new Vector3(-11.4f, 0.6f, -0.14f);
			gameObject.transform.localScale = Vector3.one * 3.2f;
			AnimationUtil.GrowThenDrift(gameObject, this.m_HeroPower_Bone.transform.position, 2f);
			return;
		}
		Vector3 b = PlatformSettings.IsTablet ? new Vector3(0f, 0.1f, 0.1f) : new Vector3(0.1f, 0.1f, 0.1f);
		gameObject.transform.localPosition = vector;
		gameObject.transform.localScale = Vector3.one * d;
		iTween.ScaleTo(gameObject, Vector3.one * d2, 0.15f);
		iTween.MoveTo(gameObject, iTween.Hash(new object[]
		{
			"position",
			vector + b,
			"isLocal",
			true,
			"time",
			10
		}));
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x000A9A60 File Offset: 0x000A7C60
	protected void ShowHeroPower(TAG_PREMIUM premium)
	{
		if (this.m_heroPowerShadowQuad != null)
		{
			this.m_heroPowerShadowQuad.SetActive(true);
		}
		if (premium == TAG_PREMIUM.GOLDEN)
		{
			this.m_heroPowerActor.Hide();
			this.m_goldenHeroPowerActor.Show();
			this.m_goldenHeroPower.GetComponent<Collider>().enabled = true;
			return;
		}
		this.m_goldenHeroPowerActor.Hide();
		this.m_heroPowerActor.Show();
		this.m_heroPower.GetComponent<Collider>().enabled = true;
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x000A9ADC File Offset: 0x000A7CDC
	protected void ShowPreconHero(bool show)
	{
		if (show && SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE && AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.PRACTICE && PracticePickerTrayDisplay.Get() != null && PracticePickerTrayDisplay.Get().IsShown())
		{
			return;
		}
		if (show)
		{
			this.ShowHero();
			return;
		}
		if (this.m_heroActor)
		{
			this.m_heroActor.Hide();
		}
		if (this.m_heroPowerActor)
		{
			this.m_heroPowerActor.Hide();
		}
		if (this.m_goldenHeroPowerActor)
		{
			this.m_goldenHeroPowerActor.Hide();
		}
		if (this.m_heroPower)
		{
			this.m_heroPower.GetComponent<Collider>().enabled = false;
		}
		if (this.m_goldenHeroPower)
		{
			this.m_goldenHeroPower.GetComponent<Collider>().enabled = false;
		}
		this.m_heroName.Text = "";
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x000A9BBF File Offset: 0x000A7DBF
	protected void HideHeroPowerActor()
	{
		this.m_heroPowerShadowQuad.SetActive(false);
		if (this.m_heroPowerActor != null)
		{
			this.m_heroPowerActor.Hide();
		}
		if (this.m_goldenHeroPower != null)
		{
			this.m_goldenHeroPowerActor.Hide();
		}
	}

	// Token: 0x0600224C RID: 8780 RVA: 0x000A9C00 File Offset: 0x000A7E00
	protected void SetUpHeroPickerButton(HeroPickerButton button, int heroCount)
	{
		GameObject gameObject = button.gameObject;
		Transform parent = gameObject.transform.parent;
		gameObject.name = string.Format("{0}_{1}", gameObject.name, heroCount);
		parent.transform.SetParent(this.m_heroBones[heroCount], false);
		parent.transform.localScale = Vector3.one;
		parent.transform.localPosition = Vector3.zero;
		parent.SetParent(this.m_basicDeckPageContainer.transform, true);
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x000A9C84 File Offset: 0x000A7E84
	protected void AddHeroLockedTooltip(string name, string description)
	{
		this.RemoveHeroLockedTooltip();
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_tooltipPrefab);
		SceneUtils.SetLayer(gameObject, UniversalInputManager.UsePhoneUI ? GameLayer.IgnoreFullScreenEffects : GameLayer.Default);
		this.m_heroLockedTooltip = gameObject.GetComponent<TooltipPanel>();
		this.m_heroLockedTooltip.Reset();
		this.m_heroLockedTooltip.Initialize(name, description);
		GameUtils.SetParent(this.m_heroLockedTooltip, this.m_tooltipBone, false);
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x000A9CF0 File Offset: 0x000A7EF0
	protected void RemoveHeroLockedTooltip()
	{
		if (this.m_heroLockedTooltip != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_heroLockedTooltip.gameObject);
		}
	}

	// Token: 0x0600224F RID: 8783 RVA: 0x000A9D10 File Offset: 0x000A7F10
	protected void DeselectLastSelectedHero()
	{
		if (this.m_selectedHeroButton == null)
		{
			return;
		}
		this.m_selectedHeroButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		this.m_selectedHeroButton.SetSelected(false);
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x000A9D3C File Offset: 0x000A7F3C
	protected void FireDeckTrayLoadedEvent()
	{
		AbsDeckPickerTrayDisplay.DeckTrayLoaded[] array = this.m_DeckTrayLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x000A9D6C File Offset: 0x000A7F6C
	protected void FireFormatTypePickerClosedEvent()
	{
		AbsDeckPickerTrayDisplay.FormatTypePickerClosed[] array = this.m_FormatTypePickerClosedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x000A9D9C File Offset: 0x000A7F9C
	protected bool IsChoosingHero()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		return mode == SceneMgr.Mode.COLLECTIONMANAGER || mode == SceneMgr.Mode.TAVERN_BRAWL || this.IsChoosingHeroForTavernBrawlChallenge() || this.IsInFiresideGatheringAndInBrawlMode() || this.IsChoosingHeroForDungeonCrawlAdventure() || this.IsChoosingHeroForPvPDungeonRunDeck();
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x000A9DDD File Offset: 0x000A7FDD
	protected bool IsChoosingHeroForTavernBrawlChallenge()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsChallengeTavernBrawl();
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x000A9DF8 File Offset: 0x000A7FF8
	protected bool IsInFiresideGatheringAndInBrawlMode()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode();
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x000A9E14 File Offset: 0x000A8014
	protected bool IsChoosingHeroForDungeonCrawlAdventure()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE && GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode());
	}

	// Token: 0x06002256 RID: 8790 RVA: 0x000A9E35 File Offset: 0x000A8035
	protected bool IsChoosingHeroForPvPDungeonRunDeck()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN;
	}

	// Token: 0x06002257 RID: 8791 RVA: 0x000A9E48 File Offset: 0x000A8048
	protected bool OnPlayButtonPressed_SaveHeroAndAdvanceToDungeonRunIfNecessary()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDataDbfRecord selectedAdventureDataRecord = adventureConfig.GetSelectedAdventureDataRecord();
		if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode()) && selectedAdventureDataRecord.DungeonCrawlPickHeroFirst)
		{
			adventureConfig.SelectedHeroClass = this.m_selectedHeroButton.m_heroClass;
			adventureConfig.ChangeSubScene(AdventureData.Adventuresubscene.DUNGEON_CRAWL, true);
			return true;
		}
		return false;
	}

	// Token: 0x06002258 RID: 8792 RVA: 0x000A9E98 File Offset: 0x000A8098
	protected void SetBackButtonEnabled(bool enable)
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			if (enable)
			{
				return;
			}
			enable = false;
		}
		if (this.m_backButton != null && this.m_backButton.IsEnabled() != enable)
		{
			this.m_backButton.SetEnabled(enable, false);
			this.m_backButton.Flip(enable, false);
		}
	}

	// Token: 0x06002259 RID: 8793 RVA: 0x000A9EEE File Offset: 0x000A80EE
	protected void SetHeroPowerActorColliderEnabled(bool enable = true)
	{
		if (this.m_heroPowerActor != null)
		{
			this.m_heroPowerActor.GetComponent<Collider>().enabled = enable;
		}
		if (this.m_goldenHeroPower != null)
		{
			this.m_goldenHeroPowerActor.GetComponent<Collider>().enabled = enable;
		}
	}

	// Token: 0x0600225A RID: 8794 RVA: 0x000A9F30 File Offset: 0x000A8130
	protected void SetUpHeroCrowns()
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)AdventureConfig.Get().GetSelectedAdventure(), (int)AdventureConfig.Get().GetSelectedMode());
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(gameSaveDataServerKey, adventureDataRecord.Name))
		{
			return;
		}
		if (adventureDataRecord != null && adventureDataRecord.DungeonCrawlDisplayHeroWinsPerChapter)
		{
			WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)AdventureConfig.Get().GetMission());
			if (wingRecordFromMissionId == null)
			{
				Log.Adventures.PrintError("SetUpHeroCrowns() - No WingRecord found for mission {0}, so cannot set up hero crowns.", new object[]
				{
					AdventureConfig.Get().GetMission()
				});
				return;
			}
			GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys adventureDungeonCrawlWingProgressSubkeys;
			if (!GameSaveDataManager.GetProgressSubkeysForDungeonCrawlWing(wingRecordFromMissionId, out adventureDungeonCrawlWingProgressSubkeys))
			{
				Log.Adventures.PrintError("GetProgressSubkeysForDungeonCrawlWing could not find progress subkeys for Wing {0}, so we don't know which Heroes to show crowns over.", new object[]
				{
					wingRecordFromMissionId.ID
				});
				return;
			}
			List<long> list;
			if (GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, adventureDungeonCrawlWingProgressSubkeys.heroCardWins, out list) && list != null)
			{
				this.ActivateCrownsForHeroCardDbIds(list);
				return;
			}
		}
		else
		{
			long num = 0L;
			List<TAG_CLASS> list2 = new List<TAG_CLASS>();
			foreach (TAG_CLASS tag_CLASS in GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap())
			{
				GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys adventureDungeonCrawlClassProgressSubkeys;
				if (GameSaveDataManager.GetProgressSubkeyForDungeonCrawlClass(tag_CLASS, out adventureDungeonCrawlClassProgressSubkeys) && GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, adventureDungeonCrawlClassProgressSubkeys.runWins, out num) && num > 0L)
				{
					list2.Add(tag_CLASS);
				}
			}
			this.ActivateCrownsForClasses(list2);
		}
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x000AA09C File Offset: 0x000A829C
	protected List<Transform> ActivateCrownsForClasses(List<TAG_CLASS> classes)
	{
		List<Transform> result = new List<Transform>();
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			if (classes.Contains(heroPickerButton.m_heroClass))
			{
				heroPickerButton.m_crown.SetActive(true);
			}
		}
		return result;
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x000AA10C File Offset: 0x000A830C
	protected void ActivateCrownsForHeroCardDbIds(List<long> cardDbIds)
	{
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			EntityDef entityDef = heroPickerButton.GetEntityDef();
			if (entityDef != null)
			{
				int num = GameUtils.TranslateCardIdToDbId(entityDef.GetCardId(), false);
				if (cardDbIds.Contains((long)num))
				{
					heroPickerButton.m_crown.SetActive(true);
				}
			}
		}
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x000AA188 File Offset: 0x000A8388
	public void Unload()
	{
		DeckPickerTray.Get().UnregisterHandlers();
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().RemoveChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnFriendChallengeChanged));
		}
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x000AA1B3 File Offset: 0x000A83B3
	public bool IsLoaded()
	{
		return this.m_Loaded;
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x000AA1BB File Offset: 0x000A83BB
	public void AddDeckTrayLoadedListener(AbsDeckPickerTrayDisplay.DeckTrayLoaded dlg)
	{
		this.m_DeckTrayLoadedListeners.Add(dlg);
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x000AA1C9 File Offset: 0x000A83C9
	public void RemoveDeckTrayLoadedListener(AbsDeckPickerTrayDisplay.DeckTrayLoaded dlg)
	{
		this.m_DeckTrayLoadedListeners.Remove(dlg);
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x000AA1D8 File Offset: 0x000A83D8
	public void AddFormatTypePickerClosedListener(AbsDeckPickerTrayDisplay.FormatTypePickerClosed dlg)
	{
		this.m_FormatTypePickerClosedListeners.Add(dlg);
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x000AA1E6 File Offset: 0x000A83E6
	public void RemoveFormatTypePickerClosedListener(AbsDeckPickerTrayDisplay.FormatTypePickerClosed dlg)
	{
		this.m_FormatTypePickerClosedListeners.Remove(dlg);
	}

	// Token: 0x06002263 RID: 8803 RVA: 0x000AA1F5 File Offset: 0x000A83F5
	public void SetPlayButtonText(string text)
	{
		if (this.m_playButton != null)
		{
			this.m_playButton.SetText(text);
		}
	}

	// Token: 0x06002264 RID: 8804 RVA: 0x000AA211 File Offset: 0x000A8411
	public void SetPlayButtonTextAlpha(float alpha)
	{
		if (this.m_playButton != null)
		{
			this.m_playButton.m_newPlayButtonText.TextAlpha = alpha;
		}
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x000AA232 File Offset: 0x000A8432
	public void AddPlayButtonListener(UIEventType type, UIEvent.Handler handler)
	{
		if (this.m_playButton != null)
		{
			this.m_playButton.AddEventListener(type, handler);
		}
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x000AA250 File Offset: 0x000A8450
	public void RemovePlayButtonListener(UIEventType type, UIEvent.Handler handler)
	{
		if (this.m_playButton != null)
		{
			this.m_playButton.RemoveEventListener(type, handler);
		}
	}

	// Token: 0x06002267 RID: 8807 RVA: 0x000AA26E File Offset: 0x000A846E
	public void SetHeaderText(string text)
	{
		if (this.m_modeName != null)
		{
			this.m_modeName.Text = text;
		}
	}

	// Token: 0x06002268 RID: 8808 RVA: 0x000AA28C File Offset: 0x000A848C
	public HeroPickerDataModel GetHeroPickerDataModel()
	{
		VisualController component = base.GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		Widget owner = component.Owner;
		IDataModel dataModel;
		if (!owner.GetDataModel(13, out dataModel))
		{
			dataModel = new HeroPickerDataModel();
			owner.BindDataModel(dataModel, false);
		}
		return dataModel as HeroPickerDataModel;
	}

	// Token: 0x06002269 RID: 8809 RVA: 0x000AA2D2 File Offset: 0x000A84D2
	public void CheatLoadHeroButtons(int buttonsToDisplay)
	{
		base.StartCoroutine(this.LoadHeroButtons(new int?(buttonsToDisplay)));
	}

	// Token: 0x040012CB RID: 4811
	protected static readonly PlatformDependentValue<bool> HIGHLIGHT_SELECTED_DECK = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		Phone = false,
		Tablet = true,
		PC = true
	};

	// Token: 0x040012CC RID: 4812
	private const string PLAYBUTTON_FIRESIDE_LATERN_EVENT = "LANTERN";

	// Token: 0x040012CD RID: 4813
	public GameObject m_randomDeckPickerTray;

	// Token: 0x040012CE RID: 4814
	public Transform m_Hero_Bone;

	// Token: 0x040012CF RID: 4815
	public Transform m_Hero_BoneDown;

	// Token: 0x040012D0 RID: 4816
	public Transform m_HeroName_Bone;

	// Token: 0x040012D1 RID: 4817
	public Transform m_Ranked_Hero_Bone;

	// Token: 0x040012D2 RID: 4818
	public Transform m_Ranked_Hero_BoneDown;

	// Token: 0x040012D3 RID: 4819
	public Transform m_Ranked_HeroName_Bone;

	// Token: 0x040012D4 RID: 4820
	public Transform m_HeroPower_Bone;

	// Token: 0x040012D5 RID: 4821
	public Transform m_HeroPower_BoneDown;

	// Token: 0x040012D6 RID: 4822
	public AsyncReference m_playButtonWidgetReference;

	// Token: 0x040012D7 RID: 4823
	public UberText m_heroName;

	// Token: 0x040012D8 RID: 4824
	[CustomEditField(Sections = "Hero Button Placement")]
	public List<GameObject> m_heroPickerButtonBonesByHeroCount;

	// Token: 0x040012D9 RID: 4825
	public float m_heroPickerButtonHorizontalSpacing;

	// Token: 0x040012DA RID: 4826
	public float m_heroPickerButtonVerticalSpacing;

	// Token: 0x040012DB RID: 4827
	public GameObject m_hierarchyDetails;

	// Token: 0x040012DC RID: 4828
	public GameObject m_basicDeckPageContainer;

	// Token: 0x040012DD RID: 4829
	public GameObject m_tooltipPrefab;

	// Token: 0x040012DE RID: 4830
	public Transform m_tooltipBone;

	// Token: 0x040012DF RID: 4831
	public UberText m_modeName;

	// Token: 0x040012E0 RID: 4832
	public UIBButton m_backButton;

	// Token: 0x040012E1 RID: 4833
	public UIBButton m_collectionButton;

	// Token: 0x040012E2 RID: 4834
	public GameObject m_basicDeckPage;

	// Token: 0x040012E3 RID: 4835
	public GameObject m_trayFrame;

	// Token: 0x040012E4 RID: 4836
	public GameObject m_randomDecksShownBone;

	// Token: 0x040012E5 RID: 4837
	public GameObject m_heroPowerContainer;

	// Token: 0x040012E6 RID: 4838
	public GameObject m_heroPowerShadowQuad;

	// Token: 0x040012E7 RID: 4839
	[CustomEditField(Sections = "Prefab References", T = EditType.GAME_OBJECT)]
	public string m_heroButtonWidgetPrefab;

	// Token: 0x040012E8 RID: 4840
	[CustomEditField(Sections = "Prefab References", T = EditType.GAME_OBJECT)]
	public string m_heroPickerCrownPrefab;

	// Token: 0x040012E9 RID: 4841
	protected FormatType m_PreviousFormatType;

	// Token: 0x040012EA RID: 4842
	protected bool m_PreviousInRankedPlayMode;

	// Token: 0x040012EB RID: 4843
	protected bool m_isMouseOverHeroPower;

	// Token: 0x040012EC RID: 4844
	private bool m_playButtonEnabled;

	// Token: 0x040012ED RID: 4845
	private bool m_heroRaised = true;

	// Token: 0x040012EE RID: 4846
	protected int m_heroDefsLoading = int.MaxValue;

	// Token: 0x040012EF RID: 4847
	protected int m_HeroPickerButtonCount;

	// Token: 0x040012F0 RID: 4848
	protected List<HeroPickerButton> m_heroButtons = new List<HeroPickerButton>();

	// Token: 0x040012F1 RID: 4849
	protected Map<string, DefLoader.DisposableFullDef> m_heroPowerDefs = new Map<string, DefLoader.DisposableFullDef>();

	// Token: 0x040012F2 RID: 4850
	protected List<AbsDeckPickerTrayDisplay.DeckTrayLoaded> m_DeckTrayLoadedListeners = new List<AbsDeckPickerTrayDisplay.DeckTrayLoaded>();

	// Token: 0x040012F3 RID: 4851
	protected List<AbsDeckPickerTrayDisplay.FormatTypePickerClosed> m_FormatTypePickerClosedListeners = new List<AbsDeckPickerTrayDisplay.FormatTypePickerClosed>();

	// Token: 0x040012F4 RID: 4852
	protected string m_selectedHeroName;

	// Token: 0x040012F5 RID: 4853
	protected bool m_Loaded;

	// Token: 0x040012F6 RID: 4854
	protected TooltipPanel m_heroLockedTooltip;

	// Token: 0x040012F7 RID: 4855
	protected DefLoader.DisposableFullDef m_selectedHeroPowerFullDef;

	// Token: 0x040012F8 RID: 4856
	protected HeroPickerButton m_selectedHeroButton;

	// Token: 0x040012F9 RID: 4857
	protected SlidingTray m_slidingTray;

	// Token: 0x040012FA RID: 4858
	protected PlayButton m_playButton;

	// Token: 0x040012FB RID: 4859
	private AudioSource m_lastPickLine;

	// Token: 0x040012FC RID: 4860
	protected PegUIElement m_heroPower;

	// Token: 0x040012FD RID: 4861
	protected PegUIElement m_goldenHeroPower;

	// Token: 0x040012FE RID: 4862
	protected Actor m_heroActor;

	// Token: 0x040012FF RID: 4863
	protected Actor m_heroPowerActor;

	// Token: 0x04001300 RID: 4864
	protected Actor m_goldenHeroPowerActor;

	// Token: 0x04001301 RID: 4865
	protected Actor m_heroPowerBigCard;

	// Token: 0x04001302 RID: 4866
	protected Actor m_goldenHeroPowerBigCard;

	// Token: 0x04001303 RID: 4867
	protected List<Transform> m_heroBones;

	// Token: 0x04001304 RID: 4868
	protected List<TAG_CLASS> m_validClasses = new List<TAG_CLASS>();

	// Token: 0x04001305 RID: 4869
	public Transform empty;

	// Token: 0x02001573 RID: 5491
	// (Invoke) Token: 0x0600E040 RID: 57408
	public delegate void DeckTrayLoaded();

	// Token: 0x02001574 RID: 5492
	// (Invoke) Token: 0x0600E044 RID: 57412
	public delegate void FormatTypePickerClosed();

	// Token: 0x02001575 RID: 5493
	protected class HeroFullDefLoadedCallbackData
	{
		// Token: 0x0600E047 RID: 57415 RVA: 0x003FCE50 File Offset: 0x003FB050
		public HeroFullDefLoadedCallbackData(HeroPickerButton button, TAG_PREMIUM premium)
		{
			this.HeroPickerButton = button;
			this.Premium = premium;
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x0600E048 RID: 57416 RVA: 0x003FCE66 File Offset: 0x003FB066
		// (set) Token: 0x0600E049 RID: 57417 RVA: 0x003FCE6E File Offset: 0x003FB06E
		public HeroPickerButton HeroPickerButton { get; private set; }

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x0600E04A RID: 57418 RVA: 0x003FCE77 File Offset: 0x003FB077
		// (set) Token: 0x0600E04B RID: 57419 RVA: 0x003FCE7F File Offset: 0x003FB07F
		public TAG_PREMIUM Premium { get; private set; }
	}
}
