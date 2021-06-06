using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.Progression;
using PegasusGame;
using UnityEngine;

// Token: 0x020005A4 RID: 1444
public class TB_BaconShop : MissionEntity
{
	// Token: 0x06004FD1 RID: 20433 RVA: 0x001A2E30 File Offset: 0x001A1030
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER,
				true
			},
			{
				GameEntityOption.MULLIGAN_IS_CHOOSE_ONE,
				true
			},
			{
				GameEntityOption.MULLIGAN_TIMER_HAS_ALTERNATE_POSITION,
				true
			},
			{
				GameEntityOption.HERO_POWER_TOOLTIP_SHIFTED_DURING_MULLIGAN,
				true
			},
			{
				GameEntityOption.MULLIGAN_HAS_HERO_LOBBY,
				true
			},
			{
				GameEntityOption.DIM_OPPOSING_HERO_DURING_MULLIGAN,
				true
			},
			{
				GameEntityOption.HANDLE_COIN,
				false
			},
			{
				GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS,
				true
			},
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			},
			{
				GameEntityOption.SUPPRESS_CLASS_NAMES,
				true
			},
			{
				GameEntityOption.ALLOW_NAME_BANNER_MODE_ICONS,
				false
			},
			{
				GameEntityOption.USE_COMPACT_ENCHANTMENT_BANNERS,
				true
			},
			{
				GameEntityOption.ALLOW_FATIGUE,
				false
			},
			{
				GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN,
				true
			},
			{
				GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES,
				false
			},
			{
				GameEntityOption.ALLOW_SLEEP_FX,
				false
			},
			{
				GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR,
				true
			},
			{
				GameEntityOption.USES_PREMIUM_EMOTES,
				true
			},
			{
				GameEntityOption.CAN_SQUELCH_OPPONENT,
				false
			},
			{
				GameEntityOption.USES_BIG_CARDS,
				false
			},
			{
				GameEntityOption.DISPLAY_MULLIGAN_DETAIL_LABEL,
				true
			}
		};
	}

	// Token: 0x06004FD2 RID: 20434 RVA: 0x001A2EF8 File Offset: 0x001A10F8
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>
		{
			{
				GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME,
				"Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0"
			},
			{
				GameEntityOption.ALTERNATE_MULLIGAN_LOBBY_ACTOR_NAME,
				"Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0"
			},
			{
				GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
				"BaconTwoScoop.prefab:1e3e06c045e65674f9a8afccb8bcdec4"
			},
			{
				GameEntityOption.DEFEAT_SCREEN_PREFAB_PATH,
				"BaconTwoScoop.prefab:1e3e06c045e65674f9a8afccb8bcdec4"
			},
			{
				GameEntityOption.RULEBOOK_POPUP_PREFAB_PATH,
				"BaconInfoPopup.prefab:d5b6f1d5443d48947891de53cdd6c323"
			},
			{
				GameEntityOption.DEFEAT_AUDIO_PATH,
				null
			}
		};
	}

	// Token: 0x17000511 RID: 1297
	// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x001A2F54 File Offset: 0x001A1154
	// (set) Token: 0x06004FD4 RID: 20436 RVA: 0x001A2F5C File Offset: 0x001A115C
	public BattlegroundsRatingChange RatingChangeData { get; set; }

	// Token: 0x06004FD5 RID: 20437 RVA: 0x001A2F68 File Offset: 0x001A1168
	public TB_BaconShop()
	{
		this.m_gameOptions.AddOptions(TB_BaconShop.s_booleanOptions, TB_BaconShop.s_stringOptions);
		HistoryManager.Get().DisableHistory();
		PlayerLeaderboardManager.Get().SetEnabled(true);
		EndTurnButton.Get().SetDisabled(true);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		this.InitializePhasePopup();
		this.InitializeTurnTimer();
		this.m_gamePhase = 1;
		GameEntity.Coroutines.StartCoroutine(this.OnShopPhase());
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_HAS_SEEN_FIRST_VICTORY_TUTORIAL, out this.m_hasSeenInGameWinVO);
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_HAS_SEEN_FIRST_DEFEAT_TUTORIAL, out this.m_hasSeenInGameLoseVO);
		Network.Get().RequestGameRoundHistory();
		Network.Get().RequestRealtimeBattlefieldRaces();
		Network.Get().RegisterNetHandler(BattlegroundsRatingChange.PacketID.ID, new Network.NetHandler(this.OnBattlegroundsRatingChange), null);
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnEnded));
		}
	}

	// Token: 0x06004FD6 RID: 20438 RVA: 0x001A30E4 File Offset: 0x001A12E4
	~TB_BaconShop()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		}
		if (Network.Get() != null)
		{
			Network.Get().RemoveNetHandler(BattlegroundsRatingChange.PacketID.ID, new Network.NetHandler(this.OnBattlegroundsRatingChange));
		}
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnEnded));
		}
	}

	// Token: 0x06004FD7 RID: 20439 RVA: 0x001A316C File Offset: 0x001A136C
	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		this.m_gameplaySceneLoaded = true;
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		ManaCrystalMgr.Get().SetEnemyManaCounterActive(false);
		this.OverrideZonePlayBaseTransitionTime();
		int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
		PlayerLeaderboardManager.Get().SetNextOpponent(tag);
	}

	// Token: 0x06004FD8 RID: 20440 RVA: 0x001A31D0 File Offset: 0x001A13D0
	protected bool GetEnemyDeckTooltipContent(ref string headline, ref string description, int index)
	{
		if (index == 0)
		{
			TAG_RACE[] availableRacesInBattlegroundsExcludingAmalgam = GameState.Get().GetAvailableRacesInBattlegroundsExcludingAmalgam();
			if (availableRacesInBattlegroundsExcludingAmalgam.Length == 5)
			{
				headline = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_AVAILABLE_RACES_HEADLINE");
				description = GameStrings.Format("GAMEPLAY_TOOLTIP_BACON_AVAILABLE_RACES_DESC", new object[]
				{
					GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[0]),
					GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[1]),
					GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[2]),
					GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[3]),
					GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[4])
				});
				return true;
			}
		}
		else if (index == 1)
		{
			TAG_RACE[] missingRacesInBattlegrounds = GameState.Get().GetMissingRacesInBattlegrounds();
			if (missingRacesInBattlegrounds.Length == 3)
			{
				headline = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_UNAVAILABLE_RACES_HEADLINE");
				description = GameStrings.Format("GAMEPLAY_TOOLTIP_BACON_UNAVAILABLE_RACES_DESC", new object[]
				{
					GameStrings.GetRaceNameBattlegrounds(missingRacesInBattlegrounds[0]),
					GameStrings.GetRaceNameBattlegrounds(missingRacesInBattlegrounds[1]),
					GameStrings.GetRaceNameBattlegrounds(missingRacesInBattlegrounds[2])
				});
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004FD9 RID: 20441 RVA: 0x001A32A4 File Offset: 0x001A14A4
	protected bool GetFriendlyDeckTooltipContent(ref string headline, ref string description, int index)
	{
		if (index == 0)
		{
			int count = GameState.Get().GetFriendlySidePlayer().GetDeckZone().GetCards().Count;
			int num = 4 - count;
			headline = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_DARKMOON_PRIZES_HEADLINE");
			description = GameStrings.Format("GAMEPLAY_TOOLTIP_BACON_DARKMOON_PRIZES_DESC", new object[]
			{
				num
			});
			return true;
		}
		return false;
	}

	// Token: 0x06004FDA RID: 20442 RVA: 0x001A32FC File Offset: 0x001A14FC
	protected bool GetFriendlyManaTooltipContent(ref string headline, ref string description, int index)
	{
		if (index == 0)
		{
			headline = GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_COIN_HEADLINE");
			description = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_GOLD");
			return true;
		}
		return false;
	}

	// Token: 0x06004FDB RID: 20443 RVA: 0x001A331C File Offset: 0x001A151C
	public override InputManager.ZoneTooltipSettings GetZoneTooltipSettings()
	{
		bool allowed = GameState.Get().GetGameEntity().GetTag(GAME_TAG.DARKMOON_FAIRE_PRIZES_ACTIVE) == 1;
		return new InputManager.ZoneTooltipSettings
		{
			EnemyDeck = new InputManager.TooltipSettings(true, new InputManager.TooltipContentDelegate(this.GetEnemyDeckTooltipContent)),
			EnemyHand = new InputManager.TooltipSettings(false),
			EnemyMana = new InputManager.TooltipSettings(false),
			FriendlyDeck = new InputManager.TooltipSettings(allowed, new InputManager.TooltipContentDelegate(this.GetFriendlyDeckTooltipContent)),
			FriendlyMana = new InputManager.TooltipSettings(true, new InputManager.TooltipContentDelegate(this.GetFriendlyManaTooltipContent))
		};
	}

	// Token: 0x06004FDC RID: 20444 RVA: 0x001A33A8 File Offset: 0x001A15A8
	public override string GetMulliganDetailText()
	{
		TAG_RACE[] availableRacesInBattlegroundsExcludingAmalgam = GameState.Get().GetAvailableRacesInBattlegroundsExcludingAmalgam();
		if (Array.Exists<TAG_RACE>(availableRacesInBattlegroundsExcludingAmalgam, (TAG_RACE race) => race == TAG_RACE.INVALID))
		{
			return null;
		}
		if (availableRacesInBattlegroundsExcludingAmalgam.Length == 5)
		{
			return GameStrings.Format("GAMEPLAY_BACON_MULLIGAN_AVAILABLE_RACES", new object[]
			{
				GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[0]),
				GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[1]),
				GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[2]),
				GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[3]),
				GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[4])
			});
		}
		return null;
	}

	// Token: 0x06004FDD RID: 20445 RVA: 0x001A3438 File Offset: 0x001A1638
	public override Vector3 NameBannerPosition(global::Player.Side side)
	{
		if (side == global::Player.Side.FRIENDLY)
		{
			return new Vector3(0f, 5f, 11f);
		}
		return base.NameBannerPosition(side);
	}

	// Token: 0x06004FDE RID: 20446 RVA: 0x001A345C File Offset: 0x001A165C
	public override Vector3 GetMulliganTimerAlternatePosition()
	{
		if (MulliganManager.Get() == null || MulliganManager.Get().GetMulliganBanner() == null)
		{
			return new Vector3(100f, 0f, 0f);
		}
		if (GameState.Get().IsInChoiceMode() && MulliganManager.Get().GetMulliganButton() != null)
		{
			return MulliganManager.Get().GetMulliganButton().transform.position;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			return MulliganManager.Get().GetMulliganBanner().transform.position + new Vector3(-1.8f, 0f, -0.91f);
		}
		return MulliganManager.Get().GetMulliganBanner().transform.position;
	}

	// Token: 0x06004FDF RID: 20447 RVA: 0x001A3520 File Offset: 0x001A1720
	protected override Spell BlowUpHero(Card card, SpellType spellType)
	{
		if (card != null && card.GetActor() != null)
		{
			PlayMakerFSM component = card.GetActor().GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
		if (GameState.Get().IsMulliganManagerActive())
		{
			Transform parent = card.GetActor().gameObject.transform.parent;
			parent.position = new Vector3(-7.7726f, 0.0055918f, -8.054f);
			parent.localScale = new Vector3(1.134f, 1.134f, 1.134f);
			MulliganManager.Get().StopAllCoroutines();
		}
		return base.BlowUpHero(card, spellType);
	}

	// Token: 0x06004FE0 RID: 20448 RVA: 0x001A35C6 File Offset: 0x001A17C6
	public override bool ShouldDelayShowingFakeHeroPowerTooltip()
	{
		return !GameState.Get().IsMulliganManagerActive();
	}

	// Token: 0x06004FE1 RID: 20449 RVA: 0x001A35D7 File Offset: 0x001A17D7
	public override ActorStateType GetMulliganChoiceHighlightState()
	{
		return ActorStateType.CARD_SELECTABLE;
	}

	// Token: 0x06004FE2 RID: 20450 RVA: 0x001A35DA File Offset: 0x001A17DA
	public override bool IsHeroMulliganLobbyFinished()
	{
		return !GameState.Get().IsMulliganPhase() || this.CountPlayersFinishedMulligan() == this.CountPlayersInGame();
	}

	// Token: 0x06004FE3 RID: 20451 RVA: 0x001A35F8 File Offset: 0x001A17F8
	private int CountPlayersFinishedMulligan()
	{
		int num = 0;
		using (Map<int, global::SharedPlayerInfo>.ValueCollection.Enumerator enumerator = GameState.Get().GetPlayerInfoMap().Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetPlayerHero() != null)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06004FE4 RID: 20452 RVA: 0x001A365C File Offset: 0x001A185C
	private int CountPlayersInGame()
	{
		return GameState.Get().GetPlayerInfoMap().Values.Count;
	}

	// Token: 0x06004FE5 RID: 20453 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x06004FE6 RID: 20454 RVA: 0x001A3672 File Offset: 0x001A1872
	public override bool DoAlternateMulliganIntro()
	{
		if (!this.ShouldDoAlternateMulliganIntro())
		{
			return false;
		}
		GameEntity.Coroutines.StartCoroutine(this.DoBaconAlternateMulliganIntroWithTiming());
		return true;
	}

	// Token: 0x06004FE7 RID: 20455 RVA: 0x0019E261 File Offset: 0x0019C461
	protected override void HandleMulliganTagChange()
	{
		MulliganManager.Get().BeginMulligan();
	}

	// Token: 0x06004FE8 RID: 20456 RVA: 0x001A3690 File Offset: 0x001A1890
	public override Vector3 GetAlternateMulliganActorScale()
	{
		return TB_BaconShop.BATTLEGROUNDS_MULLIGAN_ACTOR_SCALE;
	}

	// Token: 0x06004FE9 RID: 20457 RVA: 0x001A369C File Offset: 0x001A189C
	public override int GetNumberOfFakeMulliganCardsToShowOnLeft(int numOriginalCards)
	{
		if (numOriginalCards >= 3)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x06004FEA RID: 20458 RVA: 0x001A36A5 File Offset: 0x001A18A5
	public override int GetNumberOfFakeMulliganCardsToShowOnRight(int numOriginalCards)
	{
		if (numOriginalCards >= 4)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x06004FEB RID: 20459 RVA: 0x001A36B0 File Offset: 0x001A18B0
	public override void ConfigureFakeMulliganCardActor(Actor actor, bool shown)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = actor as PlayerLeaderboardMainCardActor;
		if (playerLeaderboardMainCardActor == null)
		{
			return;
		}
		playerLeaderboardMainCardActor.ToggleLockedHeroView(shown);
	}

	// Token: 0x06004FEC RID: 20460 RVA: 0x001A36D8 File Offset: 0x001A18D8
	public override bool IsGameSpeedupConditionInEffect()
	{
		return !(Gameplay.Get() == null) && GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALLOW_GAME_SPEEDUP) && this.m_gamePhase == 2;
	}

	// Token: 0x06004FED RID: 20461 RVA: 0x001A3726 File Offset: 0x001A1926
	public override void ApplyMulliganActorStateChanges(Actor baseActor)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = (PlayerLeaderboardMainCardActor)baseActor;
		playerLeaderboardMainCardActor.SetAlternateNameTextActive(false);
		playerLeaderboardMainCardActor.m_playerNameBackground.SetActive(false);
		playerLeaderboardMainCardActor.m_nameTextMesh.gameObject.SetActive(true);
	}

	// Token: 0x06004FEE RID: 20462 RVA: 0x001A3751 File Offset: 0x001A1951
	public override void ApplyMulliganActorLobbyStateChanges(Actor baseActor)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = (PlayerLeaderboardMainCardActor)baseActor;
		playerLeaderboardMainCardActor.SetAlternateNameTextActive(false);
		playerLeaderboardMainCardActor.m_nameTextMesh.gameObject.SetActive(false);
		playerLeaderboardMainCardActor.m_playerNameBackground.SetActive(true);
		playerLeaderboardMainCardActor.SetFullyHighlighted(false);
	}

	// Token: 0x06004FEF RID: 20463 RVA: 0x001A3784 File Offset: 0x001A1984
	public override void ClearMulliganActorStateChanges(Actor baseActor)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = (PlayerLeaderboardMainCardActor)baseActor;
		playerLeaderboardMainCardActor.SetAlternateNameTextActive(false);
		playerLeaderboardMainCardActor.m_nameTextMesh.gameObject.SetActive(false);
		playerLeaderboardMainCardActor.m_playerNameBackground.SetActive(false);
		playerLeaderboardMainCardActor.m_playerNameText.gameObject.SetActive(false);
		playerLeaderboardMainCardActor.SetFullyHighlighted(false);
	}

	// Token: 0x06004FF0 RID: 20464 RVA: 0x001A37D2 File Offset: 0x001A19D2
	public override string GetMulliganBannerText()
	{
		return GameStrings.Get("GAMEPLAY_BACON_MULLIGAN_CHOOSE_HERO_BANNER");
	}

	// Token: 0x06004FF1 RID: 20465 RVA: 0x00090064 File Offset: 0x0008E264
	public override string GetMulliganBannerSubtitleText()
	{
		return null;
	}

	// Token: 0x06004FF2 RID: 20466 RVA: 0x001A37DE File Offset: 0x001A19DE
	public override string GetMulliganWaitingText()
	{
		return string.Format(GameStrings.Get("GAMEPLAY_BACON_MULLIGAN_WAITING_BANNER"), this.CountPlayersFinishedMulligan(), this.CountPlayersInGame());
	}

	// Token: 0x06004FF3 RID: 20467 RVA: 0x001A3805 File Offset: 0x001A1A05
	public override string GetMulliganWaitingSubtitleText()
	{
		if (MulliganManager.Get() != null && MulliganManager.Get().IsMulliganTimerActive())
		{
			return GameStrings.Get("GAMEPLAY_BACON_MULLIGAN_WAITING_BANNER_SUBTITLE");
		}
		return null;
	}

	// Token: 0x06004FF4 RID: 20468 RVA: 0x001A382C File Offset: 0x001A1A2C
	public override void QueueEntityForRemoval(global::Entity entity)
	{
		GameState.Get().QueueEntityForRemoval(entity);
	}

	// Token: 0x06004FF5 RID: 20469 RVA: 0x001A3839 File Offset: 0x001A1A39
	protected IEnumerator DoBaconAlternateMulliganIntroWithTiming()
	{
		SceneMgr.Get().NotifySceneLoaded();
		MulliganManager.Get().LoadMulliganButton();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		GameState.Get().GetGameEntity().NotifyOfHeroesFinishedAnimatingInMulligan();
		ScreenEffectsMgr.Get().SetActive(true);
		yield break;
	}

	// Token: 0x06004FF6 RID: 20470 RVA: 0x001A3844 File Offset: 0x001A1A44
	public override void OnMulliganCardsDealt(List<Card> startingCards)
	{
		foreach (Card callbackData in startingCards)
		{
			AssetLoader.Get().InstantiatePrefab(new AssetReference("BaconHeroMulliganBestPlaceVisual.prefab:6e6437cf53cbc0e4fbf0b3d6ce5a6856"), new PrefabCallback<GameObject>(this.OnBestPlaceVisualLoaded), callbackData, AssetLoadingOptions.None);
		}
	}

	// Token: 0x06004FF7 RID: 20471 RVA: 0x001A38B0 File Offset: 0x001A1AB0
	private void OnBestPlaceVisualLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		Card card = (Card)callbackData;
		int num = GameUtils.TranslateCardIdToDbId(card.GetEntity().GetCardId(), false);
		int bestPlaceForHero = this.GetBestPlaceForHero(num);
		BaconHeroMulliganBestPlaceVisual component = go.GetComponent<BaconHeroMulliganBestPlaceVisual>();
		this.m_mulliganBestPlaceVisuals.Add(component);
		component.SetVisualActive(bestPlaceForHero, num);
		GameUtils.SetParent(go, card.gameObject, false);
	}

	// Token: 0x06004FF8 RID: 20472 RVA: 0x001A3908 File Offset: 0x001A1B08
	private int GetBestPlaceForHero(int heroId)
	{
		List<long> list;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_BEST_HERO_PLACE, out list);
		List<long> list2;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_BEST_HERO_PLACE_HERO, out list2);
		if (list == null || list2 == null)
		{
			return int.MaxValue;
		}
		if (list.Count != list2.Count)
		{
			Debug.LogError("Error in GetBestPlaceForHero: List size mismatch!");
			return int.MaxValue;
		}
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] == (long)heroId && i < list.Count)
			{
				return (int)list[i];
			}
		}
		return int.MaxValue;
	}

	// Token: 0x06004FF9 RID: 20473 RVA: 0x001A39A0 File Offset: 0x001A1BA0
	public override void OnMulliganBeginDealNewCards()
	{
		foreach (BaconHeroMulliganBestPlaceVisual baconHeroMulliganBestPlaceVisual in this.m_mulliganBestPlaceVisuals)
		{
			baconHeroMulliganBestPlaceVisual.Hide();
		}
	}

	// Token: 0x06004FFA RID: 20474 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004FFB RID: 20475 RVA: 0x001A39F0 File Offset: 0x001A1BF0
	private void OverrideZonePlayBaseTransitionTime()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		ZonePlay battlefieldZone = GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone();
		ZonePlay battlefieldZone2 = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone();
		battlefieldZone.OverrideBaseTransitionTime(0.5f);
		battlefieldZone.ResetTransitionTime();
		battlefieldZone2.OverrideBaseTransitionTime(0.5f);
		battlefieldZone2.ResetTransitionTime();
	}

	// Token: 0x06004FFC RID: 20476 RVA: 0x001A3A45 File Offset: 0x001A1C45
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 1)
		{
			this.m_gamePhase = 1;
			yield return this.OnShopPhase();
		}
		if (missionEvent == 2)
		{
			this.m_gamePhase = 2;
			yield return this.OnCombatPhase();
		}
		if (missionEvent == 3)
		{
			int tag = this.GetFreezeButtonCard().GetEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			int num = this.GetFreezeButtonCard().GetEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
			num--;
			if (tag >= num)
			{
				this.SetInputEnableForFrozenButton(false);
			}
			else
			{
				this.SetInputEnableForFrozenButton(false);
				yield return new WaitForSeconds(0.75f);
				this.SetInputEnableForFrozenButton(true);
			}
		}
		if (missionEvent == 4)
		{
			this.SetInputEnableForRefreshButton(false);
			yield return new WaitForSeconds(0.75f);
			this.SetInputEnableForRefreshButton(true);
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor bobActor = this.GetBobActor();
		if (bobActor == null || bobActor.GetEntity() == null)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 101:
			if (this.ShouldPlayRateVO(0.25f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_ShopUpgradeLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 102:
			if (!this.m_enemySpeaking)
			{
				string voLine = TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04;
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 103:
			if (this.ShouldPlayRateVO(0.15f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_RecruitSmallLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 104:
			if (this.ShouldPlayRateVO(0.2f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_RecruitMediumLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 105:
			if (this.ShouldPlayRateVO(0.25f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_RecruitLargeLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 106:
			if (this.ShouldPlayRateVO(0.25f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_TripleLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 107:
			if (this.ShouldPlayRateVO(0.15f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_SellingLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 108:
			if (this.ShouldPlayRateVO(0.1f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_FreezingLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 109:
			if (this.ShouldPlayRateVO(0.1f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_RefreshLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 110:
			if (this.ShouldPlayRateVO(0.25f) && !this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PossibleTripleLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 111:
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_NewGameLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 112:
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostCombatGeneralLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 113:
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostCombatWinLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 114:
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostCombatLoseLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 115:
			GameState.Get().SetBusy(true);
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostShopGeneralLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(false);
			break;
		case 116:
			GameState.Get().SetBusy(true);
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostShopLoseLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(false);
			break;
		case 117:
			GameState.Get().SetBusy(true);
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostShopWinLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(false);
			break;
		case 118:
			GameState.Get().SetBusy(true);
			if (!this.m_enemySpeaking)
			{
				string voLine = this.GetRandomLine(TB_BaconShop.m_PostShopIsFirstLines);
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(false);
			break;
		case 119:
			GameState.Get().SetBusy(true);
			if (!this.m_enemySpeaking)
			{
				string voLine = TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AFK_01;
				yield return this.PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x06004FFD RID: 20477 RVA: 0x001A3A5B File Offset: 0x001A1C5B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		AchievementManager.Get().UnpauseToastNotifications();
		PlayerLeaderboardManager.Get().UpdateLayout(true);
		if (gameResult == TAG_PLAYSTATE.WON && this.m_hasSeenInGameWinVO == 0L)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait(TB_BaconShop.Bob_BrassRing_Quote, TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstVictory_01, 3f, 1f, true, false));
		}
		int realTimePlayerLeaderboardPlace = GameState.Get().GetFriendlySidePlayer().GetHero().GetRealTimePlayerLeaderboardPlace();
		if (gameResult == TAG_PLAYSTATE.LOST && this.m_hasSeenInGameLoseVO == 0L && realTimePlayerLeaderboardPlace > 4)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait(TB_BaconShop.Bob_BrassRing_Quote, TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01, 3f, 1f, true, false));
		}
		yield break;
	}

	// Token: 0x06004FFE RID: 20478 RVA: 0x001A3A71 File Offset: 0x001A1C71
	protected virtual IEnumerator OnShopPhase()
	{
		AchievementManager.Get().UnpauseToastNotifications();
		yield return this.ShowPopup("Shop");
		PlayerLeaderboardManager.Get().UpdateLayout(true);
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		this.UpdateNameBanner();
		this.ShowTechLevelDisplay(true);
		yield return new WaitForSeconds(3f);
		this.ShowShopTutorials();
		this.SetGameNotificationEmotesEnabled(true);
		GameState.Get().GetTimeTracker().ResetAccruedLostTime();
		yield break;
	}

	// Token: 0x06004FFF RID: 20479 RVA: 0x001A3A80 File Offset: 0x001A1C80
	protected virtual IEnumerator OnCombatPhase()
	{
		this.HideShopTutorials();
		yield return this.ShowPopup("Combat");
		this.ShowTechLevelDisplay(false);
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		this.UpdateNameBanner();
		this.ForceShowFriendlyHeroActor();
		InputManager.Get().HidePhoneHand();
		GameState.Get().GetTimeTracker().ResetAccruedLostTime();
		yield break;
	}

	// Token: 0x06005000 RID: 20480 RVA: 0x001A3A8F File Offset: 0x001A1C8F
	public override void HandleRealTimeMissionEvent(int missionEvent)
	{
		if (missionEvent == 2)
		{
			this.SetGameNotificationEmotesEnabled(false);
		}
	}

	// Token: 0x06005001 RID: 20481 RVA: 0x001A3A9C File Offset: 0x001A1C9C
	private void OnTurnEnded(int oldTurn, int newTurn, object userData)
	{
		if (GameState.Get().IsFriendlySidePlayerTurn())
		{
			return;
		}
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		FlowPerformanceBattlegrounds flowPerformanceBattlegrounds = (hearthstonePerformance != null) ? hearthstonePerformance.GetCurrentPerformanceFlow<FlowPerformanceBattlegrounds>() : null;
		if (flowPerformanceBattlegrounds != null)
		{
			flowPerformanceBattlegrounds.OnNewRoundStart();
		}
		GameEntity.Coroutines.StartCoroutine(GameState.Get().RejectUnresolvedChangesAfterDelay());
	}

	// Token: 0x06005002 RID: 20482 RVA: 0x001A3ADC File Offset: 0x001A1CDC
	public override string GetAttackSpellControllerOverride(global::Entity attacker)
	{
		if (attacker == null)
		{
			return null;
		}
		if (attacker.IsHero())
		{
			return "AttackSpellController_Battlegrounds_Hero.prefab:922da2c91f4cca1458b5901204d1d26c";
		}
		return "AttackSpellController_Battlegrounds_Minion.prefab:922da2c91f4cca1458b5901204d1d26c";
	}

	// Token: 0x06005003 RID: 20483 RVA: 0x001A3AF8 File Offset: 0x001A1CF8
	public override string GetVictoryScreenBannerText()
	{
		int realTimePlayerLeaderboardPlace = GameState.Get().GetFriendlySidePlayer().GetHero().GetRealTimePlayerLeaderboardPlace();
		if (realTimePlayerLeaderboardPlace == 0)
		{
			return string.Empty;
		}
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_PLACE_" + realTimePlayerLeaderboardPlace);
	}

	// Token: 0x06005004 RID: 20484 RVA: 0x001A3B38 File Offset: 0x001A1D38
	public override string GetBestNameForPlayer(int playerId)
	{
		string text = (GameState.Get().GetPlayerInfoMap().ContainsKey(playerId) && GameState.Get().GetPlayerInfoMap()[playerId] != null) ? GameState.Get().GetPlayerInfoMap()[playerId].GetName() : null;
		string text2 = (GameState.Get().GetPlayerInfoMap().ContainsKey(playerId) && GameState.Get().GetPlayerInfoMap()[playerId] != null && GameState.Get().GetPlayerInfoMap()[playerId].GetHero() != null) ? GameState.Get().GetPlayerInfoMap()[playerId].GetHero().GetName() : null;
		bool flag = GameState.Get().GetPlayerMap().ContainsKey(playerId) && GameState.Get().GetPlayerMap()[playerId].IsFriendlySide();
		bool @bool = Options.Get().GetBool(global::Option.STREAMER_MODE);
		if (text2 == null)
		{
			text2 = ((PlayerLeaderboardManager.Get() != null && PlayerLeaderboardManager.Get().GetTileForPlayerId(playerId) != null) ? PlayerLeaderboardManager.Get().GetTileForPlayerId(playerId).GetHeroName() : null);
		}
		if (flag)
		{
			if (@bool || text == null)
			{
				return GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME");
			}
			return text;
		}
		else if (@bool)
		{
			if (text2 == null)
			{
				return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
			}
			return text2;
		}
		else
		{
			if (text == null)
			{
				return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
			}
			return text;
		}
	}

	// Token: 0x06005005 RID: 20485 RVA: 0x001A3C80 File Offset: 0x001A1E80
	public override string GetNameBannerOverride(global::Player.Side side)
	{
		if (side != global::Player.Side.OPPOSING)
		{
			return null;
		}
		if (GameState.Get() == null)
		{
			return null;
		}
		if (!this.IsCustomGameModeAIHero())
		{
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
			return this.GetBestNameForPlayer(tag);
		}
		if (this.m_gamePhase == 2)
		{
			if (!(PlayerLeaderboardManager.Get() == null) && PlayerLeaderboardManager.Get().GetOddManOutOpponentHero() != null)
			{
				return PlayerLeaderboardManager.Get().GetOddManOutOpponentHero().GetName();
			}
			if (GameState.Get().GetOpposingSidePlayer() == null || GameState.Get().GetOpposingSidePlayer().GetHero() == null)
			{
				return null;
			}
			return GameState.Get().GetOpposingSidePlayer().GetHero().GetName();
		}
		else
		{
			if (GameState.Get().GetOpposingSidePlayer() == null || GameState.Get().GetOpposingSidePlayer().GetHero() == null)
			{
				return null;
			}
			return GameState.Get().GetOpposingSidePlayer().GetHero().GetName();
		}
	}

	// Token: 0x06005006 RID: 20486 RVA: 0x001A3D5C File Offset: 0x001A1F5C
	public override void PlayAlternateEnemyEmote(int playerId, EmoteType emoteType)
	{
		string text = "";
		NotificationManager.VisualEmoteType visualEmoteType = NotificationManager.VisualEmoteType.NONE;
		PlayerLeaderboardCard tileForPlayerId = PlayerLeaderboardManager.Get().GetTileForPlayerId(playerId);
		if (tileForPlayerId == null)
		{
			return;
		}
		Actor tileActor = tileForPlayerId.m_tileActor;
		switch (emoteType)
		{
		case EmoteType.GREETINGS:
			text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_GREETINGS");
			break;
		case EmoteType.WELL_PLAYED:
			text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_WELL_PLAYED");
			break;
		case EmoteType.OOPS:
			text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_OOPS");
			break;
		case EmoteType.THREATEN:
			text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_THREATEN");
			break;
		case EmoteType.THANKS:
			text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_THANKS");
			break;
		case EmoteType.SORRY:
			text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_SORRY");
			break;
		default:
			if (emoteType != EmoteType.WOW)
			{
				switch (emoteType)
				{
				case EmoteType.BATTLEGROUNDS_VISUAL_ONE:
					visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_01;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TWO:
					visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_02;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_THREE:
					visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_03;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_FOUR:
					visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_04;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_FIVE:
					visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_05;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_SIX:
					visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_06;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_HOT_STREAK:
					visualEmoteType = NotificationManager.VisualEmoteType.HOT_STREAK;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TRIPLE:
					visualEmoteType = NotificationManager.VisualEmoteType.TRIPLE;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_01:
					visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_01;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_02:
					visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_02;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_03:
					visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_03;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_04:
					visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_04;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_05:
					visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_05;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_06:
					visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_06;
					goto IL_14F;
				case EmoteType.BATTLEGROUNDS_VISUAL_BANANA:
					visualEmoteType = NotificationManager.VisualEmoteType.BANANA;
					goto IL_14F;
				}
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_INVALID");
			}
			else
			{
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_WOW");
			}
			break;
		}
		IL_14F:
		if (text != null || visualEmoteType != NotificationManager.VisualEmoteType.NONE)
		{
			NotificationManager.SpeechBubbleOptions options = new NotificationManager.SpeechBubbleOptions().WithActor(tileActor).WithBubbleScale(0.3f).WithSpeechText(text).WithSpeechBubbleDirection(Notification.SpeechBubbleDirection.MiddleLeft).WithParentToActor(false).WithDestroyWhenNewCreated(true).WithSpeechBubbleGroup(playerId).WithVisualEmoteType(visualEmoteType).WithEmoteDuration(1.5f).WithFinishCallback(new Action<int>(this.OnNotificationEnded));
			this.RequestNotification(options, emoteType);
		}
	}

	// Token: 0x06005007 RID: 20487 RVA: 0x001A3F20 File Offset: 0x001A2120
	private void RequestNotification(NotificationManager.SpeechBubbleOptions options, EmoteType emoteType)
	{
		int speechBubbleGroup = options.speechBubbleGroup;
		if (!this.m_emotesAllowedForPlayer.ContainsKey(speechBubbleGroup))
		{
			this.m_emotesAllowedForPlayer.Add(speechBubbleGroup, true);
			this.m_emotesQueuedForPlayer.Add(speechBubbleGroup, new QueueList<NotificationManager.SpeechBubbleOptions>());
			this.m_gameNotificationsQueuedForPlayer.Add(speechBubbleGroup, new LinkedList<NotificationManager.SpeechBubbleOptions>());
		}
		if (this.m_gameNotificationEmotes.Contains(emoteType))
		{
			if (this.m_priorityEmotes.Contains(emoteType))
			{
				this.m_gameNotificationsQueuedForPlayer[speechBubbleGroup].AddFirst(options);
			}
			else
			{
				this.m_gameNotificationsQueuedForPlayer[speechBubbleGroup].AddLast(options);
			}
		}
		else
		{
			this.m_emotesQueuedForPlayer[speechBubbleGroup].Enqueue(options);
		}
		this.PlayEmotesIfPossibleForPlayer(speechBubbleGroup);
	}

	// Token: 0x06005008 RID: 20488 RVA: 0x001A3FD1 File Offset: 0x001A21D1
	private void OnNotificationEnded(int playerId)
	{
		if (!this.m_emotesAllowedForPlayer.ContainsKey(playerId))
		{
			return;
		}
		this.m_emotesAllowedForPlayer[playerId] = true;
		this.PlayEmotesIfPossibleForPlayer(playerId);
	}

	// Token: 0x06005009 RID: 20489 RVA: 0x001A3FF8 File Offset: 0x001A21F8
	private void PlayEmotesIfPossibleForPlayer(int playerId)
	{
		if (!this.m_emotesAllowedForPlayer.ContainsKey(playerId) || !this.m_emotesAllowedForPlayer[playerId])
		{
			return;
		}
		if (this.m_emotesQueuedForPlayer.ContainsKey(playerId) && this.m_emotesQueuedForPlayer[playerId].Count > 0)
		{
			NotificationManager.Get().CreateSpeechBubble(this.m_emotesQueuedForPlayer[playerId].Dequeue());
			this.m_emotesAllowedForPlayer[playerId] = false;
			return;
		}
		if (this.m_gameNotificationEmotesAllowed && this.m_gameNotificationsQueuedForPlayer.ContainsKey(playerId) && this.m_gameNotificationsQueuedForPlayer[playerId].Count > 0)
		{
			NotificationManager.Get().CreateSpeechBubble(this.m_gameNotificationsQueuedForPlayer[playerId].First.Value);
			this.m_gameNotificationsQueuedForPlayer[playerId].RemoveFirst();
			this.m_emotesAllowedForPlayer[playerId] = false;
		}
	}

	// Token: 0x0600500A RID: 20490 RVA: 0x001A40D8 File Offset: 0x001A22D8
	private void SetGameNotificationEmotesEnabled(bool enabled)
	{
		this.m_gameNotificationEmotesAllowed = enabled;
		if (this.m_gameNotificationEmotesAllowed)
		{
			foreach (int playerId in this.m_emotesAllowedForPlayer.Keys.ToList<int>())
			{
				this.PlayEmotesIfPossibleForPlayer(playerId);
			}
		}
	}

	// Token: 0x0600500B RID: 20491 RVA: 0x001A4144 File Offset: 0x001A2344
	public override bool ShouldUseAlternateNameForPlayer(global::Player.Side side)
	{
		return side == global::Player.Side.OPPOSING;
	}

	// Token: 0x0600500C RID: 20492 RVA: 0x001A414A File Offset: 0x001A234A
	private bool IsCustomGameModeAIHero()
	{
		return this.IsShopPhase() || GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.BACON_ODD_PLAYER_OUT);
	}

	// Token: 0x0600500D RID: 20493 RVA: 0x001A416C File Offset: 0x001A236C
	public override string GetTurnTimerCountdownText(float timeRemainingInTurn)
	{
		if (this.m_gamePhase == 2)
		{
			return GameStrings.Get("GAMEPLAY_BACON_COMBAT_END_TURN_BUTTON_TEXT");
		}
		if (this.m_gamePhase != 1)
		{
			return "";
		}
		if (timeRemainingInTurn != 0f)
		{
			AchievementManager achievementManager = AchievementManager.Get();
			if (timeRemainingInTurn < achievementManager.GetNotificationPauseBufferSeconds() && !achievementManager.ToastNotificationsPaused)
			{
				achievementManager.PauseToastNotifications();
			}
			return GameStrings.Format("GAMEPLAY_END_TURN_BUTTON_COUNTDOWN", new object[]
			{
				Mathf.CeilToInt(timeRemainingInTurn)
			});
		}
		if (!global::TurnTimer.Get().IsRopeActive())
		{
			return GameStrings.Get("GAMEPLAY_BACON_SHOP_END_TURN_BUTTON_TEXT");
		}
		return "";
	}

	// Token: 0x0600500E RID: 20494 RVA: 0x001A41FC File Offset: 0x001A23FC
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		this.HideShopTutorials();
	}

	// Token: 0x0600500F RID: 20495 RVA: 0x001A420B File Offset: 0x001A240B
	protected void InitializePhasePopup()
	{
		AssetLoader.Get().InstantiatePrefab(this.BACON_PHASE_POPUP, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			this.m_phasePopup = go;
			this.m_phasePopup.SetActive(false);
		}, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005010 RID: 20496 RVA: 0x001A422C File Offset: 0x001A242C
	protected IEnumerator ShowPopup(string playmakerState)
	{
		if (!this.m_gameplaySceneLoaded)
		{
			yield break;
		}
		while (this.m_phasePopup == null)
		{
			yield return null;
		}
		this.m_phasePopup.SetActive(true);
		this.m_phasePopup.GetComponent<PlayMakerFSM>().SetState(playmakerState);
		yield break;
	}

	// Token: 0x06005011 RID: 20497 RVA: 0x001A4244 File Offset: 0x001A2444
	protected void UpdateNameBanner()
	{
		if (Gameplay.Get() == null)
		{
			return;
		}
		NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(global::Player.Side.OPPOSING);
		if (nameBannerForSide == null)
		{
			return;
		}
		nameBannerForSide.UpdatePlayerNameBanner();
	}

	// Token: 0x06005012 RID: 20498 RVA: 0x001A427C File Offset: 0x001A247C
	protected void InitializeTurnTimer()
	{
		global::TurnTimer.Get().SetGameModeSettings(new TurnTimerGameModeSettings
		{
			m_RopeFuseVolume = 0.05f,
			m_EndTurnButtonExplosionVolume = 0f,
			m_RopeRolloutVolume = 0.3f,
			m_PlayMusicStinger = false,
			m_PlayTimeoutFx = false,
			m_PlayTickSound = true
		});
	}

	// Token: 0x06005013 RID: 20499 RVA: 0x001A42CE File Offset: 0x001A24CE
	public bool IsShopPhase()
	{
		return this.m_gamePhase == 1;
	}

	// Token: 0x06005014 RID: 20500 RVA: 0x001A42DC File Offset: 0x001A24DC
	private void OnBattlegroundsRatingChange()
	{
		BattlegroundsRatingChange battlegroundsRatingChange = Network.Get().GetBattlegroundsRatingChange();
		this.RatingChangeData = battlegroundsRatingChange;
	}

	// Token: 0x06005015 RID: 20501 RVA: 0x001A42FB File Offset: 0x001A24FB
	private int GetTechLevelInt()
	{
		if (GameState.Get() == null)
		{
			return 0;
		}
		return GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.PLAYER_TECH_LEVEL);
	}

	// Token: 0x06005016 RID: 20502 RVA: 0x001A431C File Offset: 0x001A251C
	private void InitTurnCounter()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("BaconTechLevelRibbon.prefab:ad60cd0fe1c8eea4bb2f12cc280acda8", AssetLoadingOptions.None);
		this.m_techLevelCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_techLevelCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmInt("TechLevel").Value = this.GetTechLevelInt();
		component.SendEvent("Birth");
		Zone zone = ZoneMgr.Get().FindZoneOfType<ZoneHero>(global::Player.Side.OPPOSING);
		this.m_techLevelCounter.transform.localPosition = zone.transform.position + new Vector3(-1.294f, 0.21f, -0.152f);
		this.m_techLevelCounter.transform.localScale = Vector3.one * 0.58f;
		GameEntity.Coroutines.StartCoroutine(this.KeepTechLevelUpToDateCoroutine());
	}

	// Token: 0x06005017 RID: 20503 RVA: 0x001A43EB File Offset: 0x001A25EB
	protected void ShowTechLevelDisplay(bool shown)
	{
		if (this.m_techLevelCounter == null)
		{
			this.InitTurnCounter();
		}
		if (this.m_techLevelCounter != null)
		{
			this.m_techLevelCounter.gameObject.SetActive(shown);
		}
	}

	// Token: 0x06005018 RID: 20504 RVA: 0x001A4420 File Offset: 0x001A2620
	private IEnumerator KeepTechLevelUpToDateCoroutine()
	{
		for (;;)
		{
			if (!this.m_techLevelCounter.gameObject.activeInHierarchy)
			{
				yield return null;
			}
			int techLevelInt = this.GetTechLevelInt();
			if (techLevelInt != this.m_displayedTechLevelNumber)
			{
				PlayMakerFSM component = this.m_techLevelCounter.GetComponent<PlayMakerFSM>();
				component.FsmVariables.GetFsmInt("TechLevel").Value = techLevelInt;
				component.SendEvent("Action");
				this.UpdateTechLevelDisplayText(techLevelInt);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06005019 RID: 20505 RVA: 0x001A4430 File Offset: 0x001A2630
	public override void ToggleAlternateMulliganActorHighlight(Card card, bool highlighted)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = card.GetActor() as PlayerLeaderboardMainCardActor;
		if (playerLeaderboardMainCardActor != null)
		{
			playerLeaderboardMainCardActor.SetFullyHighlighted(highlighted);
		}
	}

	// Token: 0x0600501A RID: 20506 RVA: 0x001A445C File Offset: 0x001A265C
	public override bool ToggleAlternateMulliganActorHighlight(Actor actor, bool? highlighted = null)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = actor as PlayerLeaderboardMainCardActor;
		if (playerLeaderboardMainCardActor != null)
		{
			bool flag = (highlighted == null) ? (!playerLeaderboardMainCardActor.m_fullSelectionHighlight.activeSelf) : highlighted.Value;
			playerLeaderboardMainCardActor.SetFullyHighlighted(flag);
			return flag;
		}
		return false;
	}

	// Token: 0x0600501B RID: 20507 RVA: 0x001A44A4 File Offset: 0x001A26A4
	private void UpdateTechLevelDisplayText(int techLevel)
	{
		string headlineString = GameStrings.Get("GAMEPLAY_BACON_TAVERN_TIER");
		this.m_techLevelCounter.ChangeDialogText(headlineString, "", "", "");
		this.m_displayedTechLevelNumber = techLevel;
	}

	// Token: 0x0600501C RID: 20508 RVA: 0x001A44DE File Offset: 0x001A26DE
	protected void ShowShopTutorials()
	{
		this.HideShopTutorials();
	}

	// Token: 0x0600501D RID: 20509 RVA: 0x001A44E6 File Offset: 0x001A26E6
	protected virtual void HideShopTutorials()
	{
		TB_BaconShop.StopCoroutine(this.m_buyButtonTutorialCoroutine);
		TB_BaconShop.StopCoroutine(this.m_enemyMinionTutorialCoroutine);
		TB_BaconShop.StopCoroutine(this.m_playMinionTutorialCoroutine);
		this.HideBuyButtonTutorial(false);
		this.HidePlayMinionTutorial();
		this.HideShopMinionTutorial();
	}

	// Token: 0x0600501E RID: 20510 RVA: 0x001A451C File Offset: 0x001A271C
	private static void StopCoroutine(Coroutine coroutine)
	{
		if (coroutine == null)
		{
			return;
		}
		GameEntity.Coroutines.StopCoroutine(coroutine);
	}

	// Token: 0x0600501F RID: 20511 RVA: 0x001A452D File Offset: 0x001A272D
	protected virtual IEnumerator UpdateBuyButtonTutorial()
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(global::Player.Side.FRIENDLY);
		Zone buyButtonZone = null;
		foreach (Zone zone in list)
		{
			if (zone is ZoneGameModeButton && ((ZoneGameModeButton)zone).m_ButtonSlot == 2)
			{
				buyButtonZone = zone;
			}
		}
		if (buyButtonZone == null)
		{
			yield break;
		}
		Card buyCard = buyButtonZone.GetFirstCard();
		if (buyCard == null)
		{
			yield break;
		}
		while (this.IsPlayerOutOfMana(GameState.Get().GetFriendlySidePlayer()))
		{
			yield return null;
		}
		yield return this.PlayBobLineWithoutText(TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01);
		bool tutorialShown = false;
		while (!GameState.Get().IsInTargetMode())
		{
			if (buyCard.IsMousedOver() && tutorialShown)
			{
				tutorialShown = false;
				this.HideBuyButtonTutorial(true);
			}
			else if (!buyCard.IsMousedOver() && !tutorialShown)
			{
				tutorialShown = true;
				this.ShowBuyButtonTutorial(buyButtonZone);
			}
			yield return null;
		}
		this.m_hasSeenBuyButtonTutorial = true;
		this.HideBuyButtonTutorial(false);
		yield break;
	}

	// Token: 0x06005020 RID: 20512 RVA: 0x001A453C File Offset: 0x001A273C
	protected void ShowBuyButtonTutorial(Zone buyButtonZone)
	{
		Vector3 position = buyButtonZone.transform.position;
		Vector3 position2 = new Vector3(position.x, position.y, position.z + 2f);
		string key = "GAMEPLAY_BACON_BUY_TUTORIAL";
		this.m_buyButtonTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_buyButtonTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_buyButtonTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005021 RID: 20513 RVA: 0x001A45B5 File Offset: 0x001A27B5
	protected void HideBuyButtonTutorial(bool hideImmediately = false)
	{
		if (this.m_buyButtonTutorialNotification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_buyButtonTutorialNotification);
				return;
			}
			NotificationManager.Get().DestroyNotification(this.m_buyButtonTutorialNotification, 0f);
		}
	}

	// Token: 0x06005022 RID: 20514 RVA: 0x001A45EE File Offset: 0x001A27EE
	protected virtual IEnumerator UpdateShopMinionTutorial()
	{
		while (!GameState.Get().IsInTargetMode() && !this.m_hasSeenBuyButtonTutorial)
		{
			yield return null;
		}
		yield return this.PlayBobLineWithoutText(TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Hire_01);
		this.ShowShopMinionTutorial();
		while (GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards().Count < 2)
		{
			yield return null;
		}
		this.m_hasSeenEnemyMinionTutorial = true;
		this.HideShopMinionTutorial();
		yield break;
	}

	// Token: 0x06005023 RID: 20515 RVA: 0x001A4600 File Offset: 0x001A2800
	protected void ShowShopMinionTutorial()
	{
		Vector3 vector = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().transform.position;
		vector += new Vector3(0f, 0f, 2.25f);
		string key = "GAMEPLAY_BACON_ENEMY_MINION_TUTORIAL";
		this.m_enemyMinionTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, vector, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_enemyMinionTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.BottomThree);
		this.m_enemyMinionTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005024 RID: 20516 RVA: 0x001A4683 File Offset: 0x001A2883
	protected void HideShopMinionTutorial()
	{
		if (this.m_enemyMinionTutorialNotification != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_enemyMinionTutorialNotification, 0f);
		}
	}

	// Token: 0x06005025 RID: 20517 RVA: 0x001A46A8 File Offset: 0x001A28A8
	protected virtual IEnumerator UpdatePlayMinionTutorial()
	{
		while (!this.m_hasSeenEnemyMinionTutorial)
		{
			yield return null;
		}
		Card firstMinion = null;
		while (firstMinion == null)
		{
			List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
			if (cards.Any((Card c) => c.GetEntity().IsMinion()))
			{
				firstMinion = cards.First((Card c) => c.GetEntity().IsMinion());
			}
			yield return null;
		}
		yield return this.PlayBobLineWithoutText(TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitWork_01);
		yield return new WaitForSeconds(1.5f);
		this.ShowPlayMinionTutorial(firstMinion);
		while (!firstMinion.IsMousedOver() && firstMinion.GetZone().m_ServerTag == TAG_ZONE.HAND)
		{
			yield return null;
		}
		this.m_hasSeenPlayMinionTutorial = true;
		this.HidePlayMinionTutorial();
		yield break;
	}

	// Token: 0x06005026 RID: 20518 RVA: 0x001A46B8 File Offset: 0x001A28B8
	protected void ShowPlayMinionTutorial(Card firstMinion)
	{
		Vector3 position = firstMinion.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x - 0.08f, position.y + 0.2f, position.z + 1.2f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2f);
		}
		string key = "GAMEPLAY_BACON_PLAY_MINION_TUTORIAL";
		this.m_playMinionTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_playMinionTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_playMinionTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005027 RID: 20519 RVA: 0x001A476A File Offset: 0x001A296A
	protected void HidePlayMinionTutorial()
	{
		if (this.m_playMinionTutorialNotification != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_playMinionTutorialNotification, 0f);
		}
	}

	// Token: 0x06005028 RID: 20520 RVA: 0x001A4790 File Offset: 0x001A2990
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewModeLaunch_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewModeLaunchalt_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ModeSelect_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_HeroSelection_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstBattle_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_BattleEndFirstLoss_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstVictory_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AFK_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewGame_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewGame_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewGame_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_General_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_General_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_General_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatWin_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatWin_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatWin_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatLoss_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatLoss_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatLoss_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Behind_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Behind_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Behind_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Ahead_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Ahead_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Ahead_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstPlace_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstPlace_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstPlace_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_04,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_05,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterTriple_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterTriple_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterTriple_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_04,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_05,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_06,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_07,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_08,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterSelling_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterSelling_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Hire_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Hire_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Triple_01,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Triple_02,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Triple_03,
			TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitWork_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06005029 RID: 20521 RVA: 0x001A4C88 File Offset: 0x001A2E88
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		global::Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor bobActor = this.GetBobActor();
		if (bobActor == null || bobActor.GetEntity() == null)
		{
			return;
		}
		if (this.IsPlayerOutOfMana(currentPlayer))
		{
			if (this.ShouldPlayRateVO(0.1f))
			{
				string voLine = this.PopRandomLine(TB_BaconShop.m_SpecialIdleLines);
				GameEntity.Coroutines.StartCoroutine(this.PlayBobLineWithOffsetBubble(voLine));
				return;
			}
		}
		else if (this.ShouldPlayRateVO(0.05f))
		{
			string randomLine = this.GetRandomLine(TB_BaconShop.m_IdleLines);
			GameEntity.Coroutines.StartCoroutine(this.PlayBobLineWithOffsetBubble(randomLine));
		}
	}

	// Token: 0x0600502A RID: 20522 RVA: 0x001A4D3C File Offset: 0x001A2F3C
	protected Actor GetBobActor()
	{
		global::Entity hero = GameState.Get().GetOpposingSidePlayer().GetHero();
		if (hero != null && hero.GetCardId() == "TB_BaconShopBob")
		{
			return hero.GetHeroCard().GetActor();
		}
		return null;
	}

	// Token: 0x0600502B RID: 20523 RVA: 0x001A4D7B File Offset: 0x001A2F7B
	protected string GetRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		return lines[UnityEngine.Random.Range(0, lines.Count)];
	}

	// Token: 0x0600502C RID: 20524 RVA: 0x001A4D9C File Offset: 0x001A2F9C
	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string randomLine = this.GetRandomLine(lines);
		lines.Remove(randomLine);
		return randomLine;
	}

	// Token: 0x0600502D RID: 20525 RVA: 0x001A4DC7 File Offset: 0x001A2FC7
	protected bool IsPlayerOutOfMana(global::Player player)
	{
		return player.GetTag(GAME_TAG.RESOURCES) - player.GetTag(GAME_TAG.RESOURCES_USED) == 0;
	}

	// Token: 0x0600502E RID: 20526 RVA: 0x001A4DDD File Offset: 0x001A2FDD
	protected bool HasSeenAllTutorial()
	{
		return this.m_hasSeenBuyButtonTutorial && this.m_hasSeenEnemyMinionTutorial && this.m_hasSeenPlayMinionTutorial;
	}

	// Token: 0x0600502F RID: 20527 RVA: 0x001A4DF8 File Offset: 0x001A2FF8
	protected bool ShouldPlayRateVO(float chance)
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		return chance > num;
	}

	// Token: 0x06005030 RID: 20528 RVA: 0x001A4E19 File Offset: 0x001A3019
	protected IEnumerator PlayBobLineWithoutText(string voLine)
	{
		Actor bobActor = this.GetBobActor();
		if (bobActor != null && bobActor.GetEntity() != null)
		{
			this.m_enemySpeaking = true;
			yield return base.PlaySoundAndWait(voLine, "", Notification.SpeechBubbleDirection.TopLeft, bobActor, 1f, true, false, 3f, 0f);
			this.m_enemySpeaking = false;
		}
		yield break;
	}

	// Token: 0x06005031 RID: 20529 RVA: 0x001A4E2F File Offset: 0x001A302F
	protected virtual IEnumerator PlayBobLineWithOffsetBubble(string voLine)
	{
		Actor bobActor = this.GetBobActor();
		if (bobActor != null && bobActor.GetEntity() != null)
		{
			yield return this.PlayBobLineWithoutText(voLine);
		}
		yield break;
	}

	// Token: 0x06005032 RID: 20530 RVA: 0x001A4E48 File Offset: 0x001A3048
	protected Card GetGameModeButtonBySlot(int buttonSlot)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(global::Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone zone2 in list)
		{
			if (zone2 is ZoneGameModeButton && ((ZoneGameModeButton)zone2).m_ButtonSlot == buttonSlot)
			{
				zone = zone2;
			}
		}
		if (zone == null)
		{
			return null;
		}
		return zone.GetFirstCard();
	}

	// Token: 0x06005033 RID: 20531 RVA: 0x001A4EC4 File Offset: 0x001A30C4
	protected Card GetBuyButtonCard()
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(global::Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone zone2 in list)
		{
			if (zone2 is ZoneMoveMinionHoverTarget && ((ZoneMoveMinionHoverTarget)zone2).m_Slot == 1)
			{
				zone = zone2;
			}
		}
		if (zone == null)
		{
			return null;
		}
		return zone.GetFirstCard();
	}

	// Token: 0x06005034 RID: 20532 RVA: 0x001A4F40 File Offset: 0x001A3140
	protected Card GetFreezeButtonCard()
	{
		return this.GetGameModeButtonBySlot(1);
	}

	// Token: 0x06005035 RID: 20533 RVA: 0x001A4F49 File Offset: 0x001A3149
	protected Card GetRefreshButtonCard()
	{
		return this.GetGameModeButtonBySlot(2);
	}

	// Token: 0x06005036 RID: 20534 RVA: 0x001A4F52 File Offset: 0x001A3152
	protected Card GetTavernUpgradeButtonCard()
	{
		return this.GetGameModeButtonBySlot(3);
	}

	// Token: 0x06005037 RID: 20535 RVA: 0x001A4F5C File Offset: 0x001A315C
	protected void SetInputEnableForBuy(bool isEnabled)
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
		{
			card.SetInputEnabled(isEnabled);
		}
	}

	// Token: 0x06005038 RID: 20536 RVA: 0x001A4FBC File Offset: 0x001A31BC
	protected void SetInputEnableForRefreshButton(bool isEnabled)
	{
		Card refreshButtonCard = this.GetRefreshButtonCard();
		if (refreshButtonCard != null)
		{
			refreshButtonCard.SetInputEnabled(isEnabled);
		}
	}

	// Token: 0x06005039 RID: 20537 RVA: 0x001A4FE0 File Offset: 0x001A31E0
	protected void SetInputEnableForTavernUpgradeButton(bool isEnabled)
	{
		Card tavernUpgradeButtonCard = this.GetTavernUpgradeButtonCard();
		if (tavernUpgradeButtonCard != null)
		{
			tavernUpgradeButtonCard.SetInputEnabled(isEnabled);
		}
	}

	// Token: 0x0600503A RID: 20538 RVA: 0x001A5004 File Offset: 0x001A3204
	protected void SetInputEnableForFrozenButton(bool isEnabled)
	{
		Card freezeButtonCard = this.GetFreezeButtonCard();
		if (freezeButtonCard != null)
		{
			freezeButtonCard.SetInputEnabled(isEnabled);
		}
	}

	// Token: 0x0600503B RID: 20539 RVA: 0x001A5028 File Offset: 0x001A3228
	public override bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, global::Entity errorSource)
	{
		return error == PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0;
	}

	// Token: 0x0600503C RID: 20540 RVA: 0x001A5034 File Offset: 0x001A3234
	private void ForceShowFriendlyHeroActor()
	{
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		if (heroCard)
		{
			heroCard.ShowCard();
			if (heroCard.GetActor() != null)
			{
				heroCard.GetActor().Show();
			}
		}
	}

	// Token: 0x040045E1 RID: 17889
	private static Map<GameEntityOption, bool> s_booleanOptions = TB_BaconShop.InitBooleanOptions();

	// Token: 0x040045E2 RID: 17890
	private static Map<GameEntityOption, string> s_stringOptions = TB_BaconShop.InitStringOptions();

	// Token: 0x040045E3 RID: 17891
	protected const int MISSION_EVENT_SHOP = 1;

	// Token: 0x040045E4 RID: 17892
	protected const int MISSION_EVENT_COMBAT = 2;

	// Token: 0x040045E5 RID: 17893
	protected const int MISSION_EVENT_FREEZE_PAUSE = 3;

	// Token: 0x040045E6 RID: 17894
	protected const int MISSION_EVENT_REFRESH_PAUSE = 4;

	// Token: 0x040045E7 RID: 17895
	protected const int BACON_TURNS_PER_PRIZE = 4;

	// Token: 0x040045E8 RID: 17896
	private AssetReference BACON_PHASE_POPUP = new AssetReference("BaconTurnIndicator.prefab:6342ffe02abc782459036566466d277c");

	// Token: 0x040045E9 RID: 17897
	private static readonly AssetReference Bob_BrassRing_Quote = new AssetReference("Bob_BrassRing_Quote.prefab:89385ff7d67aa1e49bcf25bc15ca61f6");

	// Token: 0x040045EA RID: 17898
	protected const string PLAYMAKER_SHOP_STATE = "Shop";

	// Token: 0x040045EB RID: 17899
	protected const string PLAYMAKER_COMBAT_STATE = "Combat";

	// Token: 0x040045EC RID: 17900
	protected int m_gamePhase = 1;

	// Token: 0x040045ED RID: 17901
	private GameObject m_phasePopup;

	// Token: 0x040045EE RID: 17902
	private bool m_gameplaySceneLoaded;

	// Token: 0x040045F0 RID: 17904
	private Notification m_techLevelCounter;

	// Token: 0x040045F1 RID: 17905
	private int m_displayedTechLevelNumber;

	// Token: 0x040045F2 RID: 17906
	private List<BaconHeroMulliganBestPlaceVisual> m_mulliganBestPlaceVisuals = new List<BaconHeroMulliganBestPlaceVisual>();

	// Token: 0x040045F3 RID: 17907
	private readonly EmoteType[] m_gameNotificationEmotes = new EmoteType[]
	{
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_01,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_02,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_03,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_04,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_05,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_06,
		EmoteType.BATTLEGROUNDS_VISUAL_TRIPLE,
		EmoteType.BATTLEGROUNDS_VISUAL_HOT_STREAK,
		EmoteType.BATTLEGROUNDS_VISUAL_KNOCK_OUT,
		EmoteType.BATTLEGROUNDS_VISUAL_BANANA
	};

	// Token: 0x040045F4 RID: 17908
	private readonly EmoteType[] m_priorityEmotes = new EmoteType[]
	{
		EmoteType.BATTLEGROUNDS_VISUAL_BANANA
	};

	// Token: 0x040045F5 RID: 17909
	private Map<int, bool> m_emotesAllowedForPlayer = new Map<int, bool>();

	// Token: 0x040045F6 RID: 17910
	private Map<int, QueueList<NotificationManager.SpeechBubbleOptions>> m_emotesQueuedForPlayer = new Map<int, QueueList<NotificationManager.SpeechBubbleOptions>>();

	// Token: 0x040045F7 RID: 17911
	private Map<int, LinkedList<NotificationManager.SpeechBubbleOptions>> m_gameNotificationsQueuedForPlayer = new Map<int, LinkedList<NotificationManager.SpeechBubbleOptions>>();

	// Token: 0x040045F8 RID: 17912
	private bool m_gameNotificationEmotesAllowed = true;

	// Token: 0x040045F9 RID: 17913
	private static readonly PlatformDependentValue<Vector3> BATTLEGROUNDS_MULLIGAN_ACTOR_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(1.5f, 1.1f, 1.5f),
		Phone = new Vector3(0.9f, 1.1f, 0.9f)
	};

	// Token: 0x040045FA RID: 17914
	protected const float DELAY_BEFORE_FREEZE_BUTTON_ACTIVE = 0.75f;

	// Token: 0x040045FB RID: 17915
	protected const float DELAY_BEFORE_REFRESH_BUTTON_ACTIVE = 0.75f;

	// Token: 0x040045FC RID: 17916
	private const float ZONE_PLAY_BASE_TRANSITION_TIME = 0.5f;

	// Token: 0x040045FD RID: 17917
	protected const float DELAY_BEFORE_SHOWING_TUTORIAL_POPUPS = 3f;

	// Token: 0x040045FE RID: 17918
	protected Notification m_buyButtonTutorialNotification;

	// Token: 0x040045FF RID: 17919
	protected Notification m_enemyMinionTutorialNotification;

	// Token: 0x04004600 RID: 17920
	protected Notification m_playMinionTutorialNotification;

	// Token: 0x04004601 RID: 17921
	protected bool m_hasSeenBuyButtonTutorial;

	// Token: 0x04004602 RID: 17922
	protected bool m_hasSeenEnemyMinionTutorial;

	// Token: 0x04004603 RID: 17923
	protected bool m_hasSeenPlayMinionTutorial;

	// Token: 0x04004604 RID: 17924
	protected Coroutine m_buyButtonTutorialCoroutine;

	// Token: 0x04004605 RID: 17925
	protected Coroutine m_enemyMinionTutorialCoroutine;

	// Token: 0x04004606 RID: 17926
	protected Coroutine m_playMinionTutorialCoroutine;

	// Token: 0x04004607 RID: 17927
	private const float IDLE_VO_RATE = 0.05f;

	// Token: 0x04004608 RID: 17928
	private const float SPECIAL_IDLE_VO_RATE = 0.1f;

	// Token: 0x04004609 RID: 17929
	private const float UPGRADE_SHOP_VO_RATE = 0.25f;

	// Token: 0x0400460A RID: 17930
	private const float RECRUIT_SMALL_VO_RATE = 0.15f;

	// Token: 0x0400460B RID: 17931
	private const float RECRUIT_MEDIUM_VO_RATE = 0.2f;

	// Token: 0x0400460C RID: 17932
	private const float RECRUIT_LARGE_VO_RATE = 0.25f;

	// Token: 0x0400460D RID: 17933
	private const float TRIPLE_VO_RATE = 0.25f;

	// Token: 0x0400460E RID: 17934
	private const float SELLING_VO_RATE = 0.15f;

	// Token: 0x0400460F RID: 17935
	private const float FREEZING_VO_RATE = 0.1f;

	// Token: 0x04004610 RID: 17936
	private const float REFRESHING_VO_RATE = 0.1f;

	// Token: 0x04004611 RID: 17937
	private const float POSSIBLE_TRIPLE_VO_RATE = 0.25f;

	// Token: 0x04004612 RID: 17938
	private const GameSaveKeyId GAME_SAVE_PARENT_KEY = GameSaveKeyId.BACON;

	// Token: 0x04004613 RID: 17939
	private long m_hasSeenInGameWinVO;

	// Token: 0x04004614 RID: 17940
	private long m_hasSeenInGameLoseVO;

	// Token: 0x04004615 RID: 17941
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AFK_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AFK_01.prefab:f2f1ffe83d98b8b41b35eb26ed4d693f");

	// Token: 0x04004616 RID: 17942
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01.prefab:4dc4f16c60d79ed40be28f898346df02");

	// Token: 0x04004617 RID: 17943
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02.prefab:7d7ee4a2ade3b074887d5f84ba468cad");

	// Token: 0x04004618 RID: 17944
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterSelling_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterSelling_01.prefab:d71f34687d09a064bab5d202ea3fb965");

	// Token: 0x04004619 RID: 17945
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterSelling_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterSelling_02.prefab:4d26d158ea8cad747a185591e39f9b1c");

	// Token: 0x0400461A RID: 17946
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01.prefab:d3ad51eb14e20324387e5dbbd1e82811");

	// Token: 0x0400461B RID: 17947
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02.prefab:d46007a5929546448984c12a47029d1f");

	// Token: 0x0400461C RID: 17948
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03.prefab:03260a54e677e4247aa19eb29662371e");

	// Token: 0x0400461D RID: 17949
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04.prefab:ecf9f68fe25195a4a93b50d0c8e82a1a");

	// Token: 0x0400461E RID: 17950
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_01.prefab:dc92cab5423afa045b4ad528dd25f9d5");

	// Token: 0x0400461F RID: 17951
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_02.prefab:d845164292fb45a4f85eed478ad5d1c2");

	// Token: 0x04004620 RID: 17952
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_03.prefab:4704869d519d6e7479602dd15e12b175");

	// Token: 0x04004621 RID: 17953
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Ahead_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Ahead_01.prefab:3288c872e5b6fa94cab1cde547ffa249");

	// Token: 0x04004622 RID: 17954
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Ahead_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Ahead_02.prefab:2a404fb49fc15fe4880764fb8051dd3d");

	// Token: 0x04004623 RID: 17955
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Ahead_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Ahead_03.prefab:68a0c922c8462dc469834130d94e3fae");

	// Token: 0x04004624 RID: 17956
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_BattleEndFirstLoss_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_BattleEndFirstLoss_01.prefab:e92aab6f04ddc794fb25b092f7850cf1");

	// Token: 0x04004625 RID: 17957
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Behind_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Behind_01.prefab:9949ef92552e12d409b91401b66855f6");

	// Token: 0x04004626 RID: 17958
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Behind_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Behind_02.prefab:82611a7dc8d52934fad8a41dfa69df7e");

	// Token: 0x04004627 RID: 17959
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Behind_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Behind_03.prefab:e966c47cb01c4d8498d22e1819f56d7a");

	// Token: 0x04004628 RID: 17960
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatLoss_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatLoss_01.prefab:e93a9d925b5026648ac4886f50ed97b6");

	// Token: 0x04004629 RID: 17961
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatLoss_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatLoss_02.prefab:a928e53cb8bd8e24093e439ac8165ac3");

	// Token: 0x0400462A RID: 17962
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatLoss_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatLoss_03.prefab:6bc0f5c5cb0a76b438db710005f31efe");

	// Token: 0x0400462B RID: 17963
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_01.prefab:70e7bbcc29ab44448aeea263db38edb8");

	// Token: 0x0400462C RID: 17964
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_02.prefab:89433e3f8e6a7c94d873d809fdd1f174");

	// Token: 0x0400462D RID: 17965
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_03.prefab:8ff8566f08747ad4bb76409e6db1504b");

	// Token: 0x0400462E RID: 17966
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstBattle_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstBattle_01.prefab:b88923936df527147b6eda2517ce91ef");

	// Token: 0x0400462F RID: 17967
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01.prefab:4ddd2298c91dc9649b98c65a0cef0760");

	// Token: 0x04004630 RID: 17968
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstPlace_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstPlace_01.prefab:8df89f94428cec44f95052ec460ed183");

	// Token: 0x04004631 RID: 17969
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstPlace_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstPlace_02.prefab:e1b62ce3616dc684a9c10326bfc91805");

	// Token: 0x04004632 RID: 17970
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstPlace_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstPlace_03.prefab:3334db6851a1b29408176a6da35485cc");

	// Token: 0x04004633 RID: 17971
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstVictory_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstVictory_01.prefab:e40b154f86185d3428ffa48867241f76");

	// Token: 0x04004634 RID: 17972
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_01.prefab:4c2351f6456373847a9751832273237e");

	// Token: 0x04004635 RID: 17973
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_02.prefab:59fd24afbf2c79e4b849aa06dfa0f887");

	// Token: 0x04004636 RID: 17974
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_03.prefab:35a54fd7d02b7d04da5e45553a2a15d6");

	// Token: 0x04004637 RID: 17975
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_04.prefab:b276d6b9caa6d42409fac92ca276f52a");

	// Token: 0x04004638 RID: 17976
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_05 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_05.prefab:8854bc6d0924d394f9db40a1dbf04b91");

	// Token: 0x04004639 RID: 17977
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_06 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_06.prefab:08ddc4abcd45d8041aa5b2966a154bb7");

	// Token: 0x0400463A RID: 17978
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_07 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_07.prefab:722d1d42625746a4c9e0c719a932cacd");

	// Token: 0x0400463B RID: 17979
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_08 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_08.prefab:9c55261ed5a012141ba405958a673e3b");

	// Token: 0x0400463C RID: 17980
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_01.prefab:4afb188175871e640abb1747e4850e07");

	// Token: 0x0400463D RID: 17981
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_02.prefab:d5908d1fd355b8c4b8344e300dc4fc42");

	// Token: 0x0400463E RID: 17982
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_03.prefab:ef1bc7fb54548df48b81a875a2936a33");

	// Token: 0x0400463F RID: 17983
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_HeroSelection_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_HeroSelection_01.prefab:93cd3efc86126de478be0e56c8e275a7");

	// Token: 0x04004640 RID: 17984
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_01.prefab:bfd9513b46b92e84da5f22e01a0387a4");

	// Token: 0x04004641 RID: 17985
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_02.prefab:eb20d844bee8bdf4f9cbb514c8ab8580");

	// Token: 0x04004642 RID: 17986
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_01.prefab:a8ec2302c41e78f4e89fc7c9f8528dc7");

	// Token: 0x04004643 RID: 17987
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_02.prefab:3808bb035b74ac04f9bb4be91009e2b7");

	// Token: 0x04004644 RID: 17988
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_03.prefab:34248aac29c16274c95fb999635368ff");

	// Token: 0x04004645 RID: 17989
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_04.prefab:511dea6cbf561ba44b20f6dc56b0a300");

	// Token: 0x04004646 RID: 17990
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_05 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_05.prefab:0fcad57569928814aaa426ca3b9f03f9");

	// Token: 0x04004647 RID: 17991
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ModeSelect_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ModeSelect_01.prefab:261a9714c4cf3ad4d8944d9127a38ddf");

	// Token: 0x04004648 RID: 17992
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewGame_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewGame_01.prefab:4395f5f45bfe3ab4ab959b3d2b7476d5");

	// Token: 0x04004649 RID: 17993
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewGame_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewGame_02.prefab:e3c7fdc1472d0284691bcea3b672a96f");

	// Token: 0x0400464A RID: 17994
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewGame_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewGame_03.prefab:f60a6cb74fe1cac488ee17d4f7b5d6f7");

	// Token: 0x0400464B RID: 17995
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewModeLaunch_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewModeLaunch_01.prefab:bfb8f00e5538f5f46a7f1d7dc786b495");

	// Token: 0x0400464C RID: 17996
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewModeLaunchalt_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewModeLaunchalt_01.prefab:d8b03d2cbf46cb54680e5503cd84f350");

	// Token: 0x0400464D RID: 17997
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01.prefab:5192079f42e32944fb565694e6dfe411");

	// Token: 0x0400464E RID: 17998
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02.prefab:1e21d2bfca0cbed4799ae963f7ced37b");

	// Token: 0x0400464F RID: 17999
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03.prefab:907297373eaf42d44b8fa703fd3cce1d");

	// Token: 0x04004650 RID: 18000
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01.prefab:9efcc8572df531d439a3153a55f56014");

	// Token: 0x04004651 RID: 18001
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02.prefab:fc02b3772b2cc3f42882b6d210369eb0");

	// Token: 0x04004652 RID: 18002
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03.prefab:e3cbf2a35ac2e8245b5bb3de3baa054e");

	// Token: 0x04004653 RID: 18003
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01.prefab:347c19b9fd13bcf4db40d99a28ed0e9b");

	// Token: 0x04004654 RID: 18004
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02.prefab:eb000d8de28cd6d478b9a718ebe1fd9e");

	// Token: 0x04004655 RID: 18005
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03.prefab:291677e8b1c4fdc4c97cf78ecbf9f822");

	// Token: 0x04004656 RID: 18006
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitWork_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitWork_01.prefab:a5e1a6db102be6d4495aa1cd7dc7ddfc");

	// Token: 0x04004657 RID: 18007
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01.prefab:8070938a2c3ba2f4ea92b7f0b5fdf280");

	// Token: 0x04004658 RID: 18008
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01.prefab:d030c882e8a5da84e8c51af1134d9196");

	// Token: 0x04004659 RID: 18009
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02.prefab:233a050c0d7df76419ac0f03f9273cde");

	// Token: 0x0400465A RID: 18010
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01.prefab:f5019f07757dde341aae503b53a9102e");

	// Token: 0x0400465B RID: 18011
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_01.prefab:26a5500e887280c40a810c01741e2544");

	// Token: 0x0400465C RID: 18012
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_02.prefab:1aff064425948044791b8b9e3f8de61b");

	// Token: 0x0400465D RID: 18013
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_03.prefab:e14f16322b47b814d8ccb07a60ccf6d1");

	// Token: 0x0400465E RID: 18014
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01.prefab:ec1459e08d9b5a04e97c6a3499505cf6");

	// Token: 0x0400465F RID: 18015
	private static List<string> m_IdleLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_03,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_04,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Idle_05
	};

	// Token: 0x04004660 RID: 18016
	private static List<string> m_SpecialIdleLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_03,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_04,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_05,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_06,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_07,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Flavor_08
	};

	// Token: 0x04004661 RID: 18017
	private static List<string> m_ShopUpgradeLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03
	};

	// Token: 0x04004662 RID: 18018
	private static List<string> m_RecruitSmallLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03
	};

	// Token: 0x04004663 RID: 18019
	private static List<string> m_RecruitMediumLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03
	};

	// Token: 0x04004664 RID: 18020
	private static List<string> m_RecruitLargeLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03
	};

	// Token: 0x04004665 RID: 18021
	private static List<string> m_TripleLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterTriple_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterTriple_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterTriple_03,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Triple_03
	};

	// Token: 0x04004666 RID: 18022
	private static List<string> m_SellingLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterSelling_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterSelling_02
	};

	// Token: 0x04004667 RID: 18023
	private static List<string> m_FreezingLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02
	};

	// Token: 0x04004668 RID: 18024
	private static List<string> m_RefreshLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Hire_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Hire_02
	};

	// Token: 0x04004669 RID: 18025
	private static List<string> m_PossibleTripleLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Triple_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Triple_02
	};

	// Token: 0x0400466A RID: 18026
	private static List<string> m_NewGameLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewGame_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewGame_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_NewGame_03
	};

	// Token: 0x0400466B RID: 18027
	private static List<string> m_PostCombatGeneralLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_General_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_General_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_General_03
	};

	// Token: 0x0400466C RID: 18028
	private static List<string> m_PostCombatWinLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatWin_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatWin_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatWin_03
	};

	// Token: 0x0400466D RID: 18029
	private static List<string> m_PostCombatLoseLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatLoss_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatLoss_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_CombatLoss_03
	};

	// Token: 0x0400466E RID: 18030
	private static List<string> m_PostShopGeneralLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02
	};

	// Token: 0x0400466F RID: 18031
	private static List<string> m_PostShopLoseLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Behind_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Behind_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Behind_03
	};

	// Token: 0x04004670 RID: 18032
	private static List<string> m_PostShopWinLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Ahead_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Ahead_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_Ahead_03
	};

	// Token: 0x04004671 RID: 18033
	private static List<string> m_PostShopIsFirstLines = new List<string>
	{
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstPlace_01,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstPlace_02,
		TB_BaconShop.VO_DALA_BOSS_99h_Male_Human_FirstPlace_03
	};
}
