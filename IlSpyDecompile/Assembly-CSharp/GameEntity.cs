using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.Core.Streaming;
using Hearthstone.Progression;
using Hearthstone.Streaming;
using UnityEngine;

public class GameEntity : Entity
{
	protected class EndGameScreenContext
	{
		public EndGameScreen m_screen;

		public Spell m_enemyBlowUpSpell;

		public Spell m_friendlyBlowUpSpell;
	}

	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Map<string, AudioSource> m_preloadedSounds = new Map<string, AudioSource>();

	private int m_preloadsNeeded;

	private int m_realTimeTurn;

	private int m_realTimeStep;

	private static MonoBehaviour s_coroutines;

	protected GameEntityOptions m_gameOptions = new GameEntityOptions(s_booleanOptions, s_stringOptions);

	public string m_uuid { get; set; }

	protected static MonoBehaviour Coroutines
	{
		get
		{
			if (s_coroutines == null)
			{
				s_coroutines = new GameObject().AddComponent<EmptyScript>();
			}
			return s_coroutines;
		}
	}

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER,
				false
			},
			{
				GameEntityOption.MULLIGAN_IS_CHOOSE_ONE,
				false
			},
			{
				GameEntityOption.MULLIGAN_TIMER_HAS_ALTERNATE_POSITION,
				false
			},
			{
				GameEntityOption.HERO_POWER_TOOLTIP_SHIFTED_DURING_MULLIGAN,
				false
			},
			{
				GameEntityOption.MULLIGAN_REQUIRES_CONFIRMATION,
				true
			},
			{
				GameEntityOption.MULLIGAN_HAS_HERO_LOBBY,
				false
			},
			{
				GameEntityOption.DIM_OPPOSING_HERO_DURING_MULLIGAN,
				false
			},
			{
				GameEntityOption.HANDLE_COIN,
				true
			},
			{
				GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS,
				false
			},
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				true
			},
			{
				GameEntityOption.SUPPRESS_CLASS_NAMES,
				false
			},
			{
				GameEntityOption.USE_SECRET_CLASS_NAMES,
				true
			},
			{
				GameEntityOption.ALLOW_NAME_BANNER_MODE_ICONS,
				true
			},
			{
				GameEntityOption.USE_COMPACT_ENCHANTMENT_BANNERS,
				false
			},
			{
				GameEntityOption.ALLOW_FATIGUE,
				true
			},
			{
				GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN,
				false
			},
			{
				GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES,
				true
			},
			{
				GameEntityOption.ALLOW_SLEEP_FX,
				true
			},
			{
				GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR,
				false
			},
			{
				GameEntityOption.USES_PREMIUM_EMOTES,
				false
			},
			{
				GameEntityOption.CAN_SQUELCH_OPPONENT,
				true
			},
			{
				GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN,
				false
			},
			{
				GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP,
				false
			},
			{
				GameEntityOption.SHOW_HERO_TOOLTIPS,
				false
			},
			{
				GameEntityOption.USES_BIG_CARDS,
				true
			},
			{
				GameEntityOption.DISABLE_TOOLTIPS,
				false
			},
			{
				GameEntityOption.DELAY_CARD_SOUND_SPELLS,
				false
			},
			{
				GameEntityOption.DISPLAY_MULLIGAN_DETAIL_LABEL,
				false
			},
			{
				GameEntityOption.WAIT_FOR_RATING_INFO,
				true
			}
		};
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>
		{
			{
				GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME,
				null
			},
			{
				GameEntityOption.ALTERNATE_MULLIGAN_LOBBY_ACTOR_NAME,
				null
			},
			{
				GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
				"VictoryTwoScoop.prefab:b31e3c6c1e80ced4183c3e231c567669"
			},
			{
				GameEntityOption.DEFEAT_SCREEN_PREFAB_PATH,
				"DefeatTwoScoop.prefab:6535dd92d63fce1478220e9bc50e926b"
			},
			{
				GameEntityOption.RULEBOOK_POPUP_PREFAB_PATH,
				null
			},
			{
				GameEntityOption.VICTORY_AUDIO_PATH,
				"victory_jingle.prefab:23f19dd07c7a5114abe5f525099cbac4"
			},
			{
				GameEntityOption.DEFEAT_AUDIO_PATH,
				"defeat_jingle.prefab:0744a10f38e92f1438a02349c29a7b76"
			}
		};
	}

	public GameEntity()
	{
		PreloadAssets();
	}

	public virtual void OnCreate()
	{
	}

	public virtual void OnCreateGame()
	{
	}

	public virtual void OnDecommissionGame()
	{
	}

	public void FadeOutHeroActor(Actor actorToFade)
	{
		ToggleSpotLight(actorToFade.GetHeroSpotlight(), bOn: false);
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		Material heroMat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material heroFrameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		float @float = heroMat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			if (!heroMat || !heroFrameMat)
			{
				Log.Graphics.PrintWarning("Actor's portrait HeroMat or HeroFrameMat materials are null");
			}
			else
			{
				heroMat.SetFloat("_LightingBlend", (float)amount);
				heroFrameMat.SetFloat("_LightingBlend", (float)amount);
			}
		};
		Hashtable args = iTween.Hash("time", 0.25f, "from", @float, "to", 1f, "onupdate", action, "onupdatetarget", actorToFade.gameObject);
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	public void FadeOutActor(Actor actorToFade)
	{
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		Material mat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material frameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		float @float = mat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			mat.SetFloat("_LightingBlend", (float)amount);
			frameMat.SetFloat("_LightingBlend", (float)amount);
		};
		Hashtable args = iTween.Hash("time", 0.25f, "from", @float, "to", 1f, "onupdate", action, "onupdatetarget", actorToFade.gameObject);
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	private void ToggleSpotLight(Light light, bool bOn)
	{
		float num = 0.1f;
		float num2 = 1.3f;
		Action<object> action = delegate(object amount)
		{
			light.intensity = (float)amount;
		};
		Action<object> action2 = delegate
		{
			light.enabled = false;
		};
		if (bOn)
		{
			light.enabled = true;
			light.intensity = 0f;
			Hashtable args2 = iTween.Hash("time", num, "from", 0f, "to", num2, "onupdate", action, "onupdatetarget", light.gameObject);
			iTween.ValueTo(light.gameObject, args2);
		}
		else
		{
			Hashtable args3 = iTween.Hash("time", num, "from", light.intensity, "to", 0f, "onupdate", action, "onupdatetarget", light.gameObject, "oncomplete", action2);
			iTween.ValueTo(light.gameObject, args3);
		}
	}

	public void FadeInHeroActor(Actor actorToFade)
	{
		FadeInHeroActor(actorToFade, 0f);
	}

	public void FadeInHeroActor(Actor actorToFade, float lightBlendAmount)
	{
		if (!actorToFade)
		{
			Log.Graphics.PrintWarning("Actor to fade is null!");
			return;
		}
		ToggleSpotLight(actorToFade.GetHeroSpotlight(), bOn: true);
		if (!actorToFade.m_portraitMesh)
		{
			Log.Graphics.PrintWarning("Actor's portrait mesh is null!");
			return;
		}
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		if (!component)
		{
			Log.Graphics.PrintWarning("Actor's portrait mesh component render is null!");
			return;
		}
		Material heroMat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material heroFrameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		if (!heroMat || !heroFrameMat)
		{
			Log.Graphics.PrintWarning("Actor's portrait HeroMat or HeroFrameMat materials are null");
			return;
		}
		float @float = heroMat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			if (!heroMat || !heroFrameMat)
			{
				Log.Graphics.PrintWarning("Actor's portrait HeroMat or HeroFrameMat materials are null");
			}
			else
			{
				heroMat.SetFloat("_LightingBlend", (float)amount);
				heroFrameMat.SetFloat("_LightingBlend", (float)amount);
			}
		};
		Hashtable args = iTween.Hash("time", 0.25f, "from", @float, "to", lightBlendAmount, "onupdate", action, "onupdatetarget", actorToFade.gameObject);
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	public void FadeInActor(Actor actorToFade)
	{
		FadeInActor(actorToFade, 0f);
	}

	public void FadeInActor(Actor actorToFade, float lightBlendAmount)
	{
		Renderer component = actorToFade.m_portraitMesh.GetComponent<Renderer>();
		Material mat = component.GetMaterial(actorToFade.m_portraitMatIdx);
		Material frameMat = component.GetMaterial(actorToFade.m_portraitFrameMatIdx);
		float @float = mat.GetFloat("_LightingBlend");
		Action<object> action = delegate(object amount)
		{
			mat.SetFloat("_LightingBlend", (float)amount);
			frameMat.SetFloat("_LightingBlend", (float)amount);
		};
		Hashtable args = iTween.Hash("time", 0.25f, "from", @float, "to", lightBlendAmount, "onupdate", action, "onupdatetarget", actorToFade.gameObject);
		iTween.ValueTo(actorToFade.gameObject, args);
	}

	public void PreloadSound(string soundPath)
	{
		m_preloadsNeeded++;
		SoundLoader.LoadSound(soundPath, OnSoundLoaded, null, SoundManager.Get().GetPlaceholderSound());
	}

	private void OnSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_preloadsNeeded--;
		if (assetRef == null)
		{
			Debug.LogWarning(string.Format("GameEntity.OnSoundLoaded() - ERROR missing Asset Ref for sound!", assetRef));
			return;
		}
		if (go == null)
		{
			Debug.LogWarning($"GameEntity.OnSoundLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			Debug.LogWarning($"GameEntity.OnSoundLoaded() - ERROR \"{assetRef}\" has no Spell component");
		}
		else
		{
			m_preloadedSounds.Add(assetRef.ToString(), component);
		}
	}

	public void RemovePreloadedSound(string soundPath)
	{
		m_preloadedSounds.Remove(soundPath);
	}

	public bool CheckPreloadedSound(string soundPath)
	{
		AudioSource value;
		return m_preloadedSounds.TryGetValue(soundPath, out value);
	}

	public AudioSource GetPreloadedSound(string soundPath)
	{
		if (m_preloadedSounds.TryGetValue(soundPath, out var value))
		{
			return value;
		}
		Debug.LogError($"GameEntity.GetPreloadedSound() - \"{soundPath}\" was not preloaded");
		return null;
	}

	public bool IsPreloadingAssets()
	{
		return m_preloadsNeeded > 0;
	}

	public GameEntityOptions GetGameOptions()
	{
		return m_gameOptions;
	}

	public override bool HasValidDisplayName()
	{
		return false;
	}

	public override string GetName()
	{
		return "GameEntity";
	}

	public override string GetDebugName()
	{
		return "GameEntity";
	}

	public override void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta change = changeList[i];
			OnTagChanged(change);
		}
	}

	public override void InitRealTimeValues(List<Network.Entity.Tag> tags)
	{
		base.InitRealTimeValues(tags);
		foreach (Network.Entity.Tag tag in tags)
		{
			switch (tag.Name)
			{
			case 20:
				SetRealTimeTurn(tag.Value);
				GameState.Get().TriggerTurnTimerUpdateForTurn(tag.Value);
				break;
			case 19:
				SetRealTimeStep(tag.Value);
				break;
			case 1199:
				if (tag.Value != 0)
				{
					ManaCrystalMgr.Get().SetManaCrystalType(ManaCrystalType.COIN);
				}
				break;
			case 1347:
				if (tag.Value > 0)
				{
					Board.Get().ChangeBoardVisualState((TAG_BOARD_VISUAL_STATE)tag.Value);
				}
				break;
			}
		}
	}

	public override void OnRealTimeTagChanged(Network.HistTagChange change)
	{
		switch (change.Tag)
		{
		case 6:
			HandleRealTimeMissionEvent(change.Value);
			break;
		case 20:
			SetRealTimeTurn(change.Value);
			EndTurnButton.Get().OnTurnChanged();
			GameState.Get().UpdateOptionHighlights();
			break;
		case 19:
			SetRealTimeStep(change.Value);
			break;
		case 1199:
			if (change.Value != 0)
			{
				ManaCrystalMgr.Get().SetManaCrystalType(ManaCrystalType.COIN);
			}
			break;
		}
	}

	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		switch (change.tag)
		{
		case 20:
			EndTurnButton.Get().OnTurnChanged();
			GameState.Get().UpdateOptionHighlights();
			break;
		case 1000:
			EndTurnButton.Get().ApplyAlternativeAppearance();
			break;
		case 1027:
			TurnStartManager.Get().ApplyAlternativeAppearance();
			break;
		case 1347:
			Board.Get().ChangeBoardVisualState((TAG_BOARD_VISUAL_STATE)change.newValue);
			break;
		}
	}

	private void SetRealTimeTurn(int turn)
	{
		m_realTimeTurn = turn;
	}

	private void SetRealTimeStep(int step)
	{
		m_realTimeStep = step;
	}

	public bool IsCurrentTurnRealTime()
	{
		return m_realTimeTurn == GetTag(GAME_TAG.TURN);
	}

	public bool IsMulliganActiveRealTime()
	{
		return m_realTimeStep <= 4;
	}

	public virtual void PreloadAssets()
	{
	}

	public virtual void NotifyOfStartOfTurnEventsFinished()
	{
	}

	public virtual bool NotifyOfEndTurnButtonPushed()
	{
		return true;
	}

	public virtual bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		return true;
	}

	public virtual void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
	}

	public virtual void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
	}

	public virtual bool NotifyOfCardTooltipDisplayShow(Card card)
	{
		return true;
	}

	public virtual void NotifyOfCardTooltipDisplayHide(Card card)
	{
	}

	public virtual void NotifyOfCoinFlipResult()
	{
	}

	public virtual bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		return false;
	}

	public virtual string[] NotifyOfKeywordHelpPanelDisplay(Entity entity)
	{
		return null;
	}

	public virtual void NotifyOfCardGrabbed(Entity entity)
	{
	}

	public virtual void NotifyOfCardDropped(Entity entity)
	{
	}

	public virtual void NotifyOfTargetModeCancelled()
	{
	}

	public virtual void NotifyOfHelpPanelDisplay(int numPanels)
	{
	}

	public virtual void NotifyOfDebugCommand(int command)
	{
	}

	public virtual void NotifyOfManaCrystalSpawned()
	{
	}

	public virtual void NotifyOfEnemyManaCrystalSpawned()
	{
	}

	public virtual void NotifyOfTooltipZoneMouseOver(TooltipZone tooltip)
	{
	}

	public virtual void NotifyOfHistoryTokenMousedOver(GameObject mousedOverTile)
	{
	}

	public virtual void NotifyOfHistoryTokenMousedOut()
	{
	}

	public virtual void NotifyOfCustomIntroFinished()
	{
	}

	public virtual void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_EndGameScreen);
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Gameplay.Get().SaveOriginalTimeScale();
		AchievementManager.Get()?.PauseToastNotifications();
		Spell enemyBlowUpSpell = null;
		Spell friendlyBlowUpSpell = null;
		if (ShouldPlayHeroBlowUpSpells(playState))
		{
			switch (playState)
			{
			case TAG_PLAYSTATE.WON:
			{
				string stringOption = GetGameOptions().GetStringOption(GameEntityOption.VICTORY_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				enemyBlowUpSpell = BlowUpHero(heroCard, SpellType.ENDGAME_WIN);
				break;
			}
			case TAG_PLAYSTATE.LOST:
			{
				string stringOption = GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				friendlyBlowUpSpell = BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				break;
			}
			case TAG_PLAYSTATE.TIED:
			{
				string stringOption = GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				enemyBlowUpSpell = BlowUpHero(heroCard, SpellType.ENDGAME_DRAW);
				friendlyBlowUpSpell = BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				break;
			}
			}
		}
		ShowEndGameScreen(playState, enemyBlowUpSpell, friendlyBlowUpSpell);
	}

	public virtual void NotifyOfRealTimeTagChange(Entity entity, Network.HistTagChange tagChange)
	{
	}

	public virtual void ToggleAlternateMulliganActorHighlight(Card card, bool highlighted)
	{
	}

	public virtual bool ToggleAlternateMulliganActorHighlight(Actor actor, bool? highlighted = null)
	{
		return false;
	}

	public virtual bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return true;
	}

	public virtual string GetVictoryScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_VICTORY");
	}

	public virtual string GetDefeatScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_DEFEAT");
	}

	public virtual string GetTieScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_TIE");
	}

	public virtual void NotifyOfHeroesFinishedAnimatingInMulligan()
	{
	}

	public virtual bool NotifyOfTooltipDisplay(TooltipZone tooltip)
	{
		return false;
	}

	public virtual void NotifyOfMulliganInitialized()
	{
		if (!GameMgr.Get().IsTutorial())
		{
			AssetLoader.Get().InstantiatePrefab("EmoteHandler.prefab:5d44be0e8bb7fd14d9fbdbda6a74ab91", EmoteHandlerDoneLoadingCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
			if (!GameMgr.Get().IsAI() && GetGameOptions().GetBooleanOption(GameEntityOption.CAN_SQUELCH_OPPONENT))
			{
				AssetLoader.Get().InstantiatePrefab("EnemyEmoteHandler.prefab:6ace3edd8826cad4aaa0d0e0eb085012", EnemyEmoteHandlerDoneLoadingCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
			}
		}
	}

	public virtual AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		return heroCard.GetAnnouncerLine(type);
	}

	public virtual void NotifyOfMulliganEnded()
	{
	}

	private void EmoteHandlerDoneLoadingCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.FRIENDLY).transform.position;
	}

	private void EnemyEmoteHandlerDoneLoadingCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.position = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING).transform.position;
	}

	public virtual void NotifyOfGamePackOpened()
	{
	}

	public virtual void NotifyOfDefeatCoinAnimation()
	{
	}

	public virtual void SendCustomEvent(int eventID)
	{
	}

	public virtual string GetTurnStartReminderText()
	{
		return "";
	}

	public virtual bool IsHeroMulliganLobbyFinished()
	{
		return true;
	}

	public virtual ActorStateType GetMulliganChoiceHighlightState()
	{
		return ActorStateType.CARD_IDLE;
	}

	public virtual bool ShouldDelayShowingFakeHeroPowerTooltip()
	{
		return true;
	}

	public virtual Vector3 NameBannerPosition(Player.Side side)
	{
		if (side == Player.Side.FRIENDLY)
		{
			return new Vector3(0f, 5f, 22f);
		}
		return new Vector3(0f, 5f, -10f);
	}

	public virtual void PlayAlternateEnemyEmote(int playerId, EmoteType emoteType)
	{
	}

	public virtual Vector3 GetMulliganTimerAlternatePosition()
	{
		return Vector3.zero;
	}

	private bool ShouldSkipMulligan()
	{
		return HasTag(GAME_TAG.SKIP_MULLIGAN);
	}

	public virtual bool ShouldDoAlternateMulliganIntro()
	{
		return ShouldSkipMulligan();
	}

	public virtual bool DoAlternateMulliganIntro()
	{
		if (ShouldSkipMulligan())
		{
			Coroutines.StartCoroutine(SkipStandardMulliganWithTiming());
			return true;
		}
		return false;
	}

	protected IEnumerator SkipStandardMulliganWithTiming()
	{
		GameState.Get().SetMulliganBusy(busy: true);
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		MulliganManager.Get().SkipMulligan();
	}

	public virtual string GetMulliganDetailText()
	{
		return null;
	}

	public virtual void OnMulliganCardsDealt(List<Card> startingCards)
	{
	}

	public virtual void OnMulliganBeginDealNewCards()
	{
	}

	public virtual float GetAdditionalTimeToWaitForSpells()
	{
		return 0f;
	}

	public virtual bool ShouldShowBigCard()
	{
		return true;
	}

	public virtual string GetBestNameForPlayer(int playerId)
	{
		string text = ((GameState.Get().GetPlayerMap().ContainsKey(playerId) && GameState.Get().GetPlayerMap()[playerId] != null) ? GameState.Get().GetPlayerMap()[playerId].GetName() : null);
		bool num = GameState.Get().GetPlayerMap().ContainsKey(playerId) && GameState.Get().GetPlayerMap()[playerId].IsFriendlySide();
		bool @bool = Options.Get().GetBool(Option.STREAMER_MODE);
		if (num)
		{
			if (@bool || text == null)
			{
				return GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME");
			}
			return text;
		}
		if (@bool)
		{
			return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
		}
		if (text == null)
		{
			return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
		}
		return text;
	}

	public virtual List<RewardData> GetCustomRewards()
	{
		return null;
	}

	public virtual void HandleRealTimeMissionEvent(int missionEvent)
	{
	}

	public virtual void OnPlayThinkEmote()
	{
		if (!GameMgr.Get().IsAI())
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (UnityEngine.Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
		}
	}

	public virtual IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (!GameMgr.Get().IsAI())
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (UnityEngine.Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			AudioSource activeAudioSource = GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType)
				.GetActiveAudioSource();
			yield return new WaitForSeconds(activeAudioSource.clip.length);
		}
	}

	public virtual void OnEmotePlayed(Card card, EmoteType emoteType, CardSoundSpell emoteSpell)
	{
	}

	public virtual void NotifyOfOpponentWillPlayCard(string cardId)
	{
	}

	public virtual void NotifyOfOpponentPlayedCard(Entity entity)
	{
	}

	public virtual void NotifyOfFriendlyPlayedCard(Entity entity)
	{
	}

	public virtual void NotifyOfResetGameStarted()
	{
	}

	public virtual void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
	}

	public virtual void NotifyOfEntityAttacked(Entity attacker, Entity defender)
	{
	}

	public virtual void NotifyOfMinionPlayed(Entity minion)
	{
	}

	public virtual void NotifyOfHeroChanged(Entity newHero)
	{
	}

	public virtual void NotifyOfWeaponEquipped(Entity weapon)
	{
	}

	public virtual void NotifyOfSpellPlayed(Entity spell, Entity target)
	{
	}

	public virtual void NotifyOfHeroPowerUsed(Entity heroPower, Entity target)
	{
	}

	public virtual void NotifyOfMinionDied(Entity minion)
	{
	}

	public virtual void NotifyOfHeroDied(Entity hero)
	{
	}

	public virtual void NotifyOfWeaponDestroyed(Entity weapon)
	{
	}

	public virtual string UpdateCardText(Card card, Actor bigCardActor, string text)
	{
		return text;
	}

	public virtual void ApplyMulliganActorStateChanges(Actor baseActor)
	{
	}

	public virtual void ApplyMulliganActorLobbyStateChanges(Actor baseActor)
	{
	}

	public virtual void ClearMulliganActorStateChanges(Actor baseActor)
	{
	}

	public virtual string GetMulliganBannerText()
	{
		return GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND");
	}

	public virtual string GetMulliganBannerSubtitleText()
	{
		return GameStrings.Get("GAMEPLAY_MULLIGAN_SUBTITLE");
	}

	public virtual string GetMulliganWaitingText()
	{
		return GameStrings.Get("GAMEPLAY_MULLIGAN_STARTING_HAND");
	}

	public virtual string GetMulliganWaitingSubtitleText()
	{
		return null;
	}

	public virtual Vector3 GetAlternateMulliganActorScale()
	{
		return new Vector3(1f, 1f, 1f);
	}

	public virtual int GetNumberOfFakeMulliganCardsToShowOnLeft(int numOriginalCards)
	{
		return 0;
	}

	public virtual int GetNumberOfFakeMulliganCardsToShowOnRight(int numOriginalCards)
	{
		return 0;
	}

	public virtual void ConfigureFakeMulliganCardActor(Actor actor, bool shown)
	{
	}

	public virtual bool IsGameSpeedupConditionInEffect()
	{
		return false;
	}

	public virtual void StartMulliganSoundtracks(bool soft)
	{
		if (soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_MulliganSoft);
		}
		else
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Mulligan);
		}
	}

	public virtual void StartGameplaySoundtracks()
	{
		Board board = Board.Get();
		MusicPlaylistType type = MusicPlaylistType.InGame_Default;
		bool flag = board == null;
		bool flag2 = GameDownloadManagerProvider.Get().IsReadyAssetsInTags(new string[2]
		{
			DownloadTags.GetTagString(DownloadTags.Quality.MusicExpansion),
			DownloadTags.GetTagString(DownloadTags.Content.Base)
		});
		if (!(flag || !flag2))
		{
			type = board.m_BoardMusic;
		}
		MusicManager.Get().StartPlaylist(type);
	}

	public virtual string GetAlternatePlayerName()
	{
		return "";
	}

	public virtual void QueueEntityForRemoval(Entity entity)
	{
	}

	public virtual IEnumerator PlayMissionIntroLineAndWait()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsAfterIntroBeforeMulligan()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsAfterDealingBaseMulliganCards()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsBeforeCoinFlip()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsAfterCoinFlip()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsAfterDealingBonusCard()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsBeforeSpreadingMulliganCards()
	{
		yield break;
	}

	public virtual IEnumerator DoActionsAfterSpreadingMulliganCards()
	{
		yield break;
	}

	public virtual IEnumerator DoGameSpecificPostIntroActions()
	{
		yield break;
	}

	public virtual IEnumerator DoCustomIntro(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
		yield break;
	}

	public virtual void OnCustomIntroCancelled(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
	}

	public virtual bool ShouldAllowCardGrab(Entity entity)
	{
		return true;
	}

	public virtual string CustomChoiceBannerText()
	{
		return null;
	}

	public virtual InputManager.ZoneTooltipSettings GetZoneTooltipSettings()
	{
		return new InputManager.ZoneTooltipSettings();
	}

	protected virtual Spell BlowUpHero(Card card, SpellType spellType)
	{
		if (card == null)
		{
			return null;
		}
		Actor actor = card.GetActor();
		if (actor != null)
		{
			actor.ActivateAllSpellsDeathStates();
		}
		Spell result = card.ActivateActorSpell(spellType);
		Gameplay.Get().StartCoroutine(HideOtherElements(card));
		return result;
	}

	protected IEnumerator HideOtherElements(Card card)
	{
		yield return new WaitForSeconds(0.5f);
		Player controller = card.GetEntity().GetController();
		if (controller.GetHeroPowerCard() != null)
		{
			controller.GetHeroPowerCard().HideCard();
			controller.GetHeroPowerCard().GetActor().ToggleForceIdle(bOn: true);
			controller.GetHeroPowerCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetHeroPowerCard().GetActor().DoCardDeathVisuals();
			controller.GetHeroPowerCard().DeactivateCustomKeywordEffect();
		}
		if (controller.GetWeaponCard() != null)
		{
			controller.GetWeaponCard().HideCard();
			controller.GetWeaponCard().GetActor().ToggleForceIdle(bOn: true);
			controller.GetWeaponCard().GetActor().SetActorState(ActorStateType.CARD_IDLE);
			controller.GetWeaponCard().GetActor().DoCardDeathVisuals();
		}
		card.GetActor().HideArmorSpell();
		card.GetActor().GetHealthObject().Hide();
		card.GetActor().GetAttackObject().Hide();
		card.GetActor().ToggleForceIdle(bOn: true);
		card.GetActor().SetActorState(ActorStateType.CARD_IDLE);
	}

	protected void ShowEndGameScreen(TAG_PLAYSTATE playState, Spell enemyBlowUpSpell, Spell friendlyBlowUpSpell)
	{
		string text = null;
		switch (playState)
		{
		case TAG_PLAYSTATE.WON:
			text = GetGameOptions().GetStringOption(GameEntityOption.VICTORY_SCREEN_PREFAB_PATH);
			break;
		case TAG_PLAYSTATE.LOST:
		case TAG_PLAYSTATE.TIED:
			text = GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_SCREEN_PREFAB_PATH);
			break;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (!gameObject)
		{
			Debug.LogErrorFormat("GameEntity.ShowEndGameScreen() - FAILED to load \"{0}\"", text);
			return;
		}
		EndGameScreen component = gameObject.GetComponent<EndGameScreen>();
		if (!component)
		{
			Debug.LogErrorFormat("GameEntity.ShowEndGameScreen() - \"{0}\" does not have an EndGameScreen component", text);
			return;
		}
		EndGameScreenContext endGameScreenContext = new EndGameScreenContext();
		endGameScreenContext.m_screen = component;
		endGameScreenContext.m_enemyBlowUpSpell = enemyBlowUpSpell;
		endGameScreenContext.m_friendlyBlowUpSpell = friendlyBlowUpSpell;
		if ((bool)enemyBlowUpSpell && !enemyBlowUpSpell.IsFinished())
		{
			enemyBlowUpSpell.AddFinishedCallback(OnBlowUpSpellFinished, endGameScreenContext);
		}
		if ((bool)friendlyBlowUpSpell && !friendlyBlowUpSpell.IsFinished())
		{
			friendlyBlowUpSpell.AddFinishedCallback(OnBlowUpSpellFinished, endGameScreenContext);
		}
		ShowEndGameScreenAfterEffects(endGameScreenContext);
	}

	public virtual bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return true;
	}

	public virtual bool ShouldUseAlternateNameForPlayer(Player.Side side)
	{
		return false;
	}

	public virtual string GetNameBannerOverride(Player.Side side)
	{
		return null;
	}

	public virtual string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return null;
	}

	public virtual string GetTurnTimerCountdownText(float timeRemainingInTurn)
	{
		return null;
	}

	public virtual string GetAttackSpellControllerOverride(Entity attacker)
	{
		return null;
	}

	private void OnBlowUpSpellFinished(Spell spell, object userData)
	{
		EndGameScreenContext context = (EndGameScreenContext)userData;
		ShowEndGameScreenAfterEffects(context);
	}

	private void ShowEndGameScreenAfterEffects(EndGameScreenContext context)
	{
		if (AreBlowUpSpellsFinished(context))
		{
			Gameplay.Get().RestoreOriginalTimeScale();
			AchievementManager.Get()?.UnpauseToastNotifications();
			context.m_screen.Show();
		}
	}

	private bool AreBlowUpSpellsFinished(EndGameScreenContext context)
	{
		if (context.m_enemyBlowUpSpell != null && !context.m_enemyBlowUpSpell.IsFinished())
		{
			return false;
		}
		if (context.m_friendlyBlowUpSpell != null && !context.m_friendlyBlowUpSpell.IsFinished())
		{
			return false;
		}
		return true;
	}

	public virtual float? GetThinkEmoteDelayOverride()
	{
		return null;
	}

	public virtual string[] GetOverrideBoardClickSounds()
	{
		return null;
	}

	public virtual void OnTurnStartManagerFinished()
	{
	}

	public virtual void OnTurnTimerEnded(bool isFriendlyPlayerTurnTimer)
	{
	}
}
