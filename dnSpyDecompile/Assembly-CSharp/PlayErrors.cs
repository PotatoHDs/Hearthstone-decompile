using System;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class PlayErrors
{
	// Token: 0x06002F76 RID: 12150 RVA: 0x000F2434 File Offset: 0x000F0634
	public static void DisplayPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		Log.PlayErrors.Print(string.Concat(new object[]
		{
			"DisplayPlayError: ErrorType = ",
			error,
			", ErrorParam = ",
			errorParam,
			", ErrorSource = ",
			errorSource
		}), Array.Empty<object>());
		if (GameState.Get().GetGameEntity().NotifyOfPlayError(error, errorParam, errorSource))
		{
			return;
		}
		switch (error)
		{
		case PlayErrors.ErrorType.REQ_MINION_TARGET:
		case PlayErrors.ErrorType.REQ_FRIENDLY_TARGET:
		case PlayErrors.ErrorType.REQ_ENEMY_TARGET:
		case PlayErrors.ErrorType.REQ_DAMAGED_TARGET:
		case PlayErrors.ErrorType.REQ_FROZEN_TARGET:
		case PlayErrors.ErrorType.REQ_TARGET_MAX_ATTACK:
		case PlayErrors.ErrorType.REQ_TARGET_WITH_RACE:
		case PlayErrors.ErrorType.REQ_HERO_TARGET:
		case PlayErrors.ErrorType.REQ_HERO_OR_MINION_TARGET:
		case PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_SPELLS:
		case PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_OPPONENTS:
		case PlayErrors.ErrorType.REQ_TARGET_MIN_ATTACK:
		case PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_HERO_POWERS:
		case PlayErrors.ErrorType.REQ_ENEMY_TARGET_NOT_IMMUNE:
		case PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_BATTLECRIES:
		case PlayErrors.ErrorType.REQ_MINION_OR_ENEMY_HERO:
		case PlayErrors.ErrorType.REQ_LEGENDARY_TARGET:
		case PlayErrors.ErrorType.REQ_TARGET_WITH_BATTLECRY:
		case PlayErrors.ErrorType.REQ_TARGET_WITH_DEATHRATTLE:
		case PlayErrors.ErrorType.REQ_TARGET_EXACT_COST:
		case PlayErrors.ErrorType.REQ_STEALTHED_TARGET:
		case PlayErrors.ErrorType.REQ_TARGET_NON_TRIPLED_MINION:
		case PlayErrors.ErrorType.REQ_TWO_OF_A_KIND:
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_TARGET);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_MAX_SECRETS:
		case PlayErrors.ErrorType.REQ_CHARGE_TARGET:
		case PlayErrors.ErrorType.REQ_NONSELF_TARGET:
		case PlayErrors.ErrorType.REQ_SECRET_ZONE_CAP:
		case PlayErrors.ErrorType.REQ_TARGET_ATTACKED_THIS_TURN:
		case PlayErrors.ErrorType.REQ_MINIMUM_ENEMY_MINIONS:
		case PlayErrors.ErrorType.REQ_UNIQUE_SECRET_OR_QUEST:
		case PlayErrors.ErrorType.REQ_CAN_BE_ATTACKED:
		case PlayErrors.ErrorType.REQ_ACTION_PWR_IS_MASTER_PWR:
		case PlayErrors.ErrorType.REQ_TARGET_MAGNET:
		case PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0:
		case PlayErrors.ErrorType.REQ_ATTACKER_NOT_FROZEN:
		case PlayErrors.ErrorType.REQ_SUBCARD_IS_PLAYABLE:
		case PlayErrors.ErrorType.REQ_NOT_EXHAUSTED_HERO_POWER:
		case PlayErrors.ErrorType.REQ_ATTACKER_CAN_ATTACK:
		case PlayErrors.ErrorType.REQ_ALL_BASIC_TOTEMS_NOT_IN_PLAY:
		case PlayErrors.ErrorType.REQ_MINIMUM_TOTAL_MINIONS:
		case PlayErrors.ErrorType.REQ_MUST_TARGET_TAUNTER:
		case PlayErrors.ErrorType.REQ_UNDAMAGED_TARGET:
		case PlayErrors.ErrorType.REQ_SECRET_ZONE_CAP_FOR_NON_SECRET:
		case PlayErrors.ErrorType.REQ_MAX_QUESTS:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN:
		case PlayErrors.ErrorType.REQ_TARGET_NOT_VAMPIRE:
		case PlayErrors.ErrorType.REQ_TARGET_NOT_DAMAGEABLE_ONLY_BY_WEAPONS:
		case PlayErrors.ErrorType.REQ_NOT_DISABLED_HERO_POWER:
		case PlayErrors.ErrorType.REQ_HAND_NOT_FULL:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_NO_3_COST_CARD_IN_DECK:
		case PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_COMBOS:
		case (PlayErrors.ErrorType)75:
		case (PlayErrors.ErrorType)76:
		case PlayErrors.ErrorType.REQ_LITERALLY_UNPLAYABLE:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_HERO_HAS_ATTACK:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_SPELLS_PLAYED_THIS_TURN:
		case (PlayErrors.ErrorType)83:
		case (PlayErrors.ErrorType)84:
		case (PlayErrors.ErrorType)85:
		case (PlayErrors.ErrorType)87:
		case (PlayErrors.ErrorType)88:
		case (PlayErrors.ErrorType)91:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_HAS_OVERLOADED_MANA:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_HERO_ATTACKED_THIS_TURN:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_DRAWN_THIS_TURN:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_NOT_DRAWN_THIS_TURN:
		case PlayErrors.ErrorType.REQ_BOUGHT_MINION_THIS_TURN:
		case PlayErrors.ErrorType.REQ_SOLD_MINION_THIS_TURN:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_SOUL_FRAGMENT_IN_DECK:
		case PlayErrors.ErrorType.REQ_DAMAGED_TARGET_UNLESS_COMBO:
		case PlayErrors.ErrorType.REQ_NOT_MINION_DORMANT:
		case PlayErrors.ErrorType.REQ_TARGET_NOT_DORMANT:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_BOUGHT_RACE_THIS_TURN:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_SOLD_RACE_THIS_TURN:
		case (PlayErrors.ErrorType)107:
		case (PlayErrors.ErrorType)108:
		case (PlayErrors.ErrorType)109:
			break;
		case PlayErrors.ErrorType.REQ_TARGET_TO_PLAY:
			if ((errorSource.IsMinion() || errorSource.IsHero()) && errorSource.GetZone() == TAG_ZONE.PLAY)
			{
				GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_GENERIC);
				goto IL_3D0;
			}
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_PLAY);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_NUM_MINION_SLOTS:
		case PlayErrors.ErrorType.REQ_MINION_CAP_IF_TARGET_AVAILABLE:
		case PlayErrors.ErrorType.REQ_MINION_CAP:
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_FULL_MINIONS);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_WEAPON_EQUIPPED:
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_NEED_WEAPON);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_ENOUGH_MANA:
			if ((errorSource.IsSpell() && PlayErrors.DoSpellsCostHealth()) || errorSource.HasTag(GAME_TAG.CARD_COSTS_HEALTH))
			{
				GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_PLAY);
				goto IL_3D0;
			}
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_NEED_MANA);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_YOUR_TURN:
			return;
		case PlayErrors.ErrorType.REQ_NONSTEALTH_ENEMY_TARGET:
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_STEALTH);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE:
		case PlayErrors.ErrorType.REQ_TARGET_FOR_COMBO:
		case PlayErrors.ErrorType.REQ_TARGET_FOR_NO_COMBO:
		case PlayErrors.ErrorType.REQ_STEADY_SHOT:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND:
		case PlayErrors.ErrorType.REQ_FRIENDLY_MINION_DIED_THIS_TURN:
		case PlayErrors.ErrorType.REQ_FRIENDLY_MINION_DIED_THIS_GAME:
		case PlayErrors.ErrorType.REQ_ENEMY_WEAPON_EQUIPPED:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS:
		case PlayErrors.ErrorType.REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT:
		case PlayErrors.ErrorType.REQ_MUST_PLAY_OTHER_CARD_FIRST:
		case PlayErrors.ErrorType.REQ_CANNOT_PLAY_THIS:
		case PlayErrors.ErrorType.REQ_FRIENDLY_MINIONS_OF_RACE_DIED_THIS_GAME:
		case PlayErrors.ErrorType.REQ_OPPONENT_PLAYED_CARDS_THIS_GAME:
		case PlayErrors.ErrorType.REQ_FRIENDLY_MINION_OF_RACE_DIED_THIS_TURN:
		case PlayErrors.ErrorType.REQ_FRIENDLY_MINION_OF_RACE_IN_HAND:
		case PlayErrors.ErrorType.REQ_FRIENDLY_DEATHRATTLE_MINION_DIED_THIS_GAME:
		case PlayErrors.ErrorType.REQ_FRIENDLY_REBORN_MINION_DIED_THIS_GAME:
		case PlayErrors.ErrorType.REQ_MINION_DIED_THIS_GAME:
		case PlayErrors.ErrorType.REQ_BOARD_NOT_COMPLETELY_FULL:
		case PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_PLAYER_HEALTH_CHANGED_THIS_TURN:
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_PLAY);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_NOT_EXHAUSTED_ACTIVATE:
			if (errorSource.IsHero())
			{
				GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_I_ATTACKED);
				goto IL_3D0;
			}
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_MINION_ATTACKED);
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_TARGET_TAUNTER:
			PlayErrors.DisplayTauntErrorEffects();
			goto IL_3D0;
		case PlayErrors.ErrorType.REQ_NOT_MINION_JUST_PLAYED:
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_SUMMON_SICKNESS);
			goto IL_3D0;
		default:
			if (error == PlayErrors.ErrorType.REQ_DRAG_TO_PLAY)
			{
				goto IL_3D0;
			}
			break;
		}
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_GENERIC);
		IL_3D0:
		string errorDescription = PlayErrors.GetErrorDescription(error, errorParam, errorSource);
		if (string.IsNullOrEmpty(errorDescription))
		{
			return;
		}
		GameplayErrorManager.Get().DisplayMessage(errorDescription);
	}

	// Token: 0x06002F77 RID: 12151 RVA: 0x000F2830 File Offset: 0x000F0A30
	private static bool CanShowMinionTauntError()
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		int num;
		int num2;
		GameState.Get().GetTauntCounts(opposingSidePlayer, out num, out num2);
		return num > 0 && num2 == 0;
	}

	// Token: 0x06002F78 RID: 12152 RVA: 0x000F2861 File Offset: 0x000F0A61
	private static void DisplayTauntErrorEffects()
	{
		if (PlayErrors.CanShowMinionTauntError())
		{
			GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(EmoteType.ERROR_TAUNT);
		}
		GameState.Get().ShowEnemyTauntCharacters();
	}

	// Token: 0x06002F79 RID: 12153 RVA: 0x000F288B File Offset: 0x000F0A8B
	private static bool DoSpellsCostHealth()
	{
		return GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.SPELLS_COST_HEALTH);
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x000F28A4 File Offset: 0x000F0AA4
	private static string GetErrorDescription(PlayErrors.ErrorType type, int? errorParam, Entity errorSource)
	{
		Log.PlayErrors.Print(string.Concat(new object[]
		{
			"GetErrorDescription: ",
			type,
			" ",
			errorParam
		}), Array.Empty<object>());
		if (type <= PlayErrors.ErrorType.REQ_TARGET_TAUNTER)
		{
			if (type <= PlayErrors.ErrorType.REQ_YOUR_TURN)
			{
				if (type == PlayErrors.ErrorType.NONE)
				{
					Debug.LogWarning("PlayErrors.GetErrorDescription() - Action is not valid, but no error string found.");
					return "";
				}
				switch (type)
				{
				case PlayErrors.ErrorType.REQ_MAX_SECRETS:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_MAX_SECRETS", new object[]
					{
						GameState.Get().GetMaxSecretsPerPlayer()
					});
				case PlayErrors.ErrorType.REQ_TARGET_MAX_ATTACK:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_MAX_ATTACK", new object[]
					{
						errorParam
					});
				case PlayErrors.ErrorType.REQ_TARGET_WITH_RACE:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_WITH_RACE", new object[]
					{
						GameStrings.GetRaceName((TAG_RACE)errorParam.Value)
					});
				case PlayErrors.ErrorType.REQ_TARGET_TO_PLAY:
					if ((errorSource.IsMinion() || errorSource.IsHero()) && errorSource.GetZone() == TAG_ZONE.PLAY)
					{
						return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_TO_ATTACK", Array.Empty<object>());
					}
					break;
				case PlayErrors.ErrorType.REQ_ENOUGH_MANA:
					if ((errorSource.IsSpell() && PlayErrors.DoSpellsCostHealth()) || errorSource.HasTag(GAME_TAG.CARD_COSTS_HEALTH))
					{
						return GameStrings.Get("GAMEPLAY_PlayErrors_REQ_ENOUGH_HEALTH");
					}
					if (errorSource.GetCard() != null && errorSource.GetCard().GetActor() != null && errorSource.GetCard().GetActor().UseCoinManaGem())
					{
						return GameStrings.Get("GAMEPLAY_PlayErrors_REQ_ENOUGH_COIN");
					}
					return GameStrings.Get("GAMEPLAY_PlayErrors_REQ_ENOUGH_MANA");
				case PlayErrors.ErrorType.REQ_YOUR_TURN:
					return "";
				}
			}
			else
			{
				if (type == PlayErrors.ErrorType.REQ_SECRET_ZONE_CAP)
				{
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_SECRET_ZONE_CAP", new object[]
					{
						GameState.Get().GetMaxSecretZoneSizePerPlayer()
					});
				}
				if (type == PlayErrors.ErrorType.REQ_MINIMUM_ENEMY_MINIONS)
				{
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_MINIMUM_ENEMY_MINIONS", new object[]
					{
						errorParam
					});
				}
				if (type == PlayErrors.ErrorType.REQ_TARGET_TAUNTER)
				{
					if (PlayErrors.CanShowMinionTauntError())
					{
						return GameStrings.Get("GAMEPLAY_PlayErrors_REQ_TARGET_TAUNTER_MINION");
					}
					return GameStrings.Get("GAMEPLAY_PlayErrors_REQ_TARGET_TAUNTER_CHARACTER");
				}
			}
		}
		else if (type <= PlayErrors.ErrorType.REQ_MINIMUM_TOTAL_MINIONS)
		{
			if (type == PlayErrors.ErrorType.REQ_ACTION_PWR_IS_MASTER_PWR)
			{
				return PlayErrors.ErrorInEditorOnly("[Unity Editor] Action power must be master power", Array.Empty<object>());
			}
			if (type == PlayErrors.ErrorType.REQ_TARGET_MIN_ATTACK)
			{
				return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_MIN_ATTACK", new object[]
				{
					errorParam
				});
			}
			if (type == PlayErrors.ErrorType.REQ_MINIMUM_TOTAL_MINIONS)
			{
				return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_MINIMUM_TOTAL_MINIONS", new object[]
				{
					errorParam
				});
			}
		}
		else if (type != PlayErrors.ErrorType.REQ_STEADY_SHOT)
		{
			switch (type)
			{
			case PlayErrors.ErrorType.REQ_SECRET_ZONE_CAP_FOR_NON_SECRET:
				return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_MAX_SECRETS", new object[]
				{
					GameState.Get().GetMaxSecretsPerPlayer()
				});
			case PlayErrors.ErrorType.REQ_TARGET_EXACT_COST:
				return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_EXACT_COST", new object[]
				{
					errorParam
				});
			case PlayErrors.ErrorType.REQ_STEALTHED_TARGET:
			case PlayErrors.ErrorType.REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT:
				break;
			case PlayErrors.ErrorType.REQ_MAX_QUESTS:
				return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_MAX_QUESTS", new object[]
				{
					GameState.Get().GetMaxQuestsPerPlayer()
				});
			default:
				switch (type)
				{
				case PlayErrors.ErrorType.REQ_TARGET_NON_TRIPLED_MINION:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_NON_TRIPLED_MINION", new object[]
					{
						errorParam
					});
				case PlayErrors.ErrorType.REQ_BOUGHT_MINION_THIS_TURN:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_BOUGHT_MINION_THIS_TURN", new object[]
					{
						errorParam
					});
				case PlayErrors.ErrorType.REQ_SOLD_MINION_THIS_TURN:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_SOLD_MINION_THIS_TURN", new object[]
					{
						errorParam
					});
				case PlayErrors.ErrorType.REQ_NOT_MINION_DORMANT:
					return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_NOT_MINION_DORMANT", new object[]
					{
						errorParam
					});
				}
				break;
			}
		}
		else if (errorSource.IsHeroPower() && errorSource.GetZone() == TAG_ZONE.PLAY)
		{
			return GameStrings.Format("GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABLE", Array.Empty<object>());
		}
		string key = null;
		if (PlayErrors.s_playErrorsMessages.TryGetValue(type, out key))
		{
			return GameStrings.Get(key);
		}
		return PlayErrors.ErrorInEditorOnly("[Unity Editor] Unknown play error ({0})", new object[]
		{
			type
		});
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string ErrorInEditorOnly(string format, params object[] args)
	{
		return "";
	}

	// Token: 0x04001A94 RID: 6804
	private static Map<PlayErrors.ErrorType, string> s_playErrorsMessages = new Map<PlayErrors.ErrorType, string>
	{
		{
			PlayErrors.ErrorType.REQ_MINION_TARGET,
			"GAMEPLAY_PlayErrors_REQ_MINION_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_TARGET,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_ENEMY_TARGET,
			"GAMEPLAY_PlayErrors_REQ_ENEMY_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_DAMAGED_TARGET,
			"GAMEPLAY_PlayErrors_REQ_DAMAGED_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_MAX_SECRETS,
			"GAMEPLAY_PlayErrors_REQ_MAX_SECRETS"
		},
		{
			PlayErrors.ErrorType.REQ_FROZEN_TARGET,
			"GAMEPLAY_PlayErrors_REQ_FROZEN_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_CHARGE_TARGET,
			"GAMEPLAY_PlayErrors_REQ_CHARGE_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_MAX_ATTACK,
			"GAMEPLAY_PlayErrors_REQ_TARGET_MAX_ATTACK"
		},
		{
			PlayErrors.ErrorType.REQ_NONSELF_TARGET,
			"GAMEPLAY_PlayErrors_REQ_NONSELF_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_WITH_RACE,
			"GAMEPLAY_PlayErrors_REQ_TARGET_WITH_RACE"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_TO_PLAY,
			"GAMEPLAY_PlayErrors_REQ_TARGET_TO_PLAY"
		},
		{
			PlayErrors.ErrorType.REQ_NUM_MINION_SLOTS,
			"GAMEPLAY_PlayErrors_REQ_NUM_MINION_SLOTS"
		},
		{
			PlayErrors.ErrorType.REQ_WEAPON_EQUIPPED,
			"GAMEPLAY_PlayErrors_REQ_WEAPON_EQUIPPED"
		},
		{
			PlayErrors.ErrorType.REQ_YOUR_TURN,
			"GAMEPLAY_PlayErrors_REQ_YOUR_TURN"
		},
		{
			PlayErrors.ErrorType.REQ_NONSTEALTH_ENEMY_TARGET,
			"GAMEPLAY_PlayErrors_REQ_NONSTEALTH_ENEMY_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_HERO_TARGET,
			"GAMEPLAY_PlayErrors_REQ_HERO_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_SECRET_ZONE_CAP,
			"GAMEPLAY_PlayErrors_REQ_SECRET_ZONE_CAP"
		},
		{
			PlayErrors.ErrorType.REQ_MINION_CAP_IF_TARGET_AVAILABLE,
			"GAMEPLAY_PlayErrors_REQ_MINION_CAP_IF_TARGET_AVAILABLE"
		},
		{
			PlayErrors.ErrorType.REQ_MINION_CAP,
			"GAMEPLAY_PlayErrors_REQ_MINION_CAP"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_ATTACKED_THIS_TURN,
			"GAMEPLAY_PlayErrors_REQ_TARGET_ATTACKED_THIS_TURN"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE,
			"GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABLE"
		},
		{
			PlayErrors.ErrorType.REQ_MINIMUM_ENEMY_MINIONS,
			"GAMEPLAY_PlayErrors_REQ_MINIMUM_ENEMY_MINIONS"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_FOR_COMBO,
			"GAMEPLAY_PlayErrors_REQ_TARGET_FOR_COMBO"
		},
		{
			PlayErrors.ErrorType.REQ_NOT_EXHAUSTED_ACTIVATE,
			"GAMEPLAY_PlayErrors_REQ_NOT_EXHAUSTED_ACTIVATE"
		},
		{
			PlayErrors.ErrorType.REQ_UNIQUE_SECRET_OR_QUEST,
			"GAMEPLAY_PlayErrors_REQ_UNIQUE_SECRET"
		},
		{
			PlayErrors.ErrorType.REQ_CAN_BE_ATTACKED,
			"GAMEPLAY_PlayErrors_REQ_CAN_BE_ATTACKED"
		},
		{
			PlayErrors.ErrorType.REQ_ACTION_PWR_IS_MASTER_PWR,
			"GAMEPLAY_PlayErrors_REQ_ACTION_PWR_IS_MASTER_PWR"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_MAGNET,
			"GAMEPLAY_PlayErrors_REQ_TARGET_MAGNET"
		},
		{
			PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0,
			"GAMEPLAY_PlayErrors_REQ_ATTACK_GREATER_THAN_0"
		},
		{
			PlayErrors.ErrorType.REQ_ATTACKER_NOT_FROZEN,
			"GAMEPLAY_PlayErrors_REQ_ATTACKER_NOT_FROZEN"
		},
		{
			PlayErrors.ErrorType.REQ_HERO_OR_MINION_TARGET,
			"GAMEPLAY_PlayErrors_REQ_HERO_OR_MINION_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_SPELLS,
			"GAMEPLAY_PlayErrors_REQ_CAN_BE_TARGETED_BY_SPELLS"
		},
		{
			PlayErrors.ErrorType.REQ_SUBCARD_IS_PLAYABLE,
			"GAMEPLAY_PlayErrors_REQ_SUBCARD_IS_PLAYABLE"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_FOR_NO_COMBO,
			"GAMEPLAY_PlayErrors_REQ_TARGET_FOR_NO_COMBO"
		},
		{
			PlayErrors.ErrorType.REQ_NOT_MINION_JUST_PLAYED,
			"GAMEPLAY_PlayErrors_REQ_NOT_MINION_JUST_PLAYED"
		},
		{
			PlayErrors.ErrorType.REQ_NOT_EXHAUSTED_HERO_POWER,
			"GAMEPLAY_PlayErrors_REQ_NOT_EXHAUSTED_HERO_POWER"
		},
		{
			PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_OPPONENTS,
			"GAMEPLAY_PlayErrors_REQ_CAN_BE_TARGETED_BY_OPPONENTS"
		},
		{
			PlayErrors.ErrorType.REQ_ATTACKER_CAN_ATTACK,
			"GAMEPLAY_PlayErrors_REQ_ATTACKER_CAN_ATTACK"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_MIN_ATTACK,
			"GAMEPLAY_PlayErrors_REQ_TARGET_MIN_ATTACK"
		},
		{
			PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_HERO_POWERS,
			"GAMEPLAY_PlayErrors_REQ_CAN_BE_TARGETED_BY_HERO_POWERS"
		},
		{
			PlayErrors.ErrorType.REQ_ENEMY_TARGET_NOT_IMMUNE,
			"GAMEPLAY_PlayErrors_REQ_ENEMY_TARGET_NOT_IMMUNE"
		},
		{
			PlayErrors.ErrorType.REQ_ALL_BASIC_TOTEMS_NOT_IN_PLAY,
			"GAMEPLAY_PlayErrors_REQ_ENTIRE_ENTOURAGE_NOT_IN_PLAY"
		},
		{
			PlayErrors.ErrorType.REQ_MINIMUM_TOTAL_MINIONS,
			"GAMEPLAY_PlayErrors_REQ_MINIMUM_TOTAL_MINIONS"
		},
		{
			PlayErrors.ErrorType.REQ_MUST_TARGET_TAUNTER,
			"GAMEPLAY_PlayErrors_REQ_MUST_TARGET_TAUNTER"
		},
		{
			PlayErrors.ErrorType.REQ_UNDAMAGED_TARGET,
			"GAMEPLAY_PlayErrors_REQ_UNDAMAGED_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_BATTLECRIES,
			"GAMEPLAY_PlayErrors_REQ_CAN_BE_TARGETED_BY_BATTLECRIES"
		},
		{
			PlayErrors.ErrorType.REQ_STEADY_SHOT,
			"GAMEPLAY_PlayErrors_REQ_STEADY_SHOT"
		},
		{
			PlayErrors.ErrorType.REQ_MINION_OR_ENEMY_HERO,
			"GAMEPLAY_PlayErrors_REQ_MINION_OR_ENEMY_HERO"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND,
			"GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_PLAYER_HEALTH_CHANGED_THIS_TURN,
			"GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABLE_AND_PLAYER_HEALTH_CHANGED_THIS_TURN"
		},
		{
			PlayErrors.ErrorType.REQ_LEGENDARY_TARGET,
			"GAMEPLAY_PlayErrors_REQ_LEGENDARY_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_MINION_DIED_THIS_TURN,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_MINION_DIED_THIS_TURN"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_MINION_DIED_THIS_GAME,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_MINION_DIED_THIS_GAME"
		},
		{
			PlayErrors.ErrorType.REQ_MINION_DIED_THIS_GAME,
			"GAMEPLAY_PlayErrors_REQ_MINION_DIED_THIS_GAME"
		},
		{
			PlayErrors.ErrorType.REQ_ENEMY_WEAPON_EQUIPPED,
			"GAMEPLAY_PlayErrors_REQ_ENEMY_WEAPON_EQUIPPED"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS,
			"GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_WITH_BATTLECRY,
			"GAMEPLAY_PlayErrors_REQ_TARGET_WITH_BATTLECRY"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_WITH_DEATHRATTLE,
			"GAMEPLAY_PlayErrors_REQ_TARGET_WITH_DEATHRATTLE"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS,
			"GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS"
		},
		{
			PlayErrors.ErrorType.REQ_STEALTHED_TARGET,
			"GAMEPLAY_PlayErrors_REQ_STEALTHED_TARGET"
		},
		{
			PlayErrors.ErrorType.REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT,
			"GAMEPLAY_PlayErrors_REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT"
		},
		{
			PlayErrors.ErrorType.REQ_MAX_QUESTS,
			"GAMEPLAY_PlayErrors_REQ_MAX_QUESTS"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN,
			"GAMEPLAY_PlayErrors_REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_NOT_VAMPIRE,
			"GAMEPLAY_PlayErrors_REQ_TARGET_NOT_VAMPIRE"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_NOT_DAMAGEABLE_ONLY_BY_WEAPONS,
			"GAMEPLAY_PlayErrors_REQ_TARGET_NOT_DAMAGEABLE_ONLY_BY_WEAPONS"
		},
		{
			PlayErrors.ErrorType.REQ_NOT_DISABLED_HERO_POWER,
			"GAMEPLAY_PlayErrors_REQ_NOT_DISABLED_HERO_POWER"
		},
		{
			PlayErrors.ErrorType.REQ_MUST_PLAY_OTHER_CARD_FIRST,
			"GAMEPLAY_PlayErrors_REQ_MUST_PLAY_OTHER_CARD_FIRST"
		},
		{
			PlayErrors.ErrorType.REQ_HAND_NOT_FULL,
			"GAMEPLAY_PlayErrors_REQ_HAND_NOT_FULL"
		},
		{
			PlayErrors.ErrorType.REQ_CAN_BE_TARGETED_BY_COMBOS,
			"GAMEPLAY_PlayErrors_REQ_CAN_BE_TARGETED_BY_COMBOS"
		},
		{
			PlayErrors.ErrorType.REQ_CANNOT_PLAY_THIS,
			"GAMEPLAY_PlayErrors_REQ_CANNOT_PLAY_THIS"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_MINIONS_OF_RACE_DIED_THIS_GAME,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_MINIONS_OF_RACE_DIED_THIS_GAME"
		},
		{
			PlayErrors.ErrorType.REQ_OPPONENT_PLAYED_CARDS_THIS_GAME,
			"GAMEPLAY_PlayErrors_REQ_OPPONENT_PLAYED_CARDS_THIS_GAME"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_MINION_OF_RACE_DIED_THIS_TURN,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_MINION_OF_RACE_DIED_THIS_TURN"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_MINION_OF_RACE_IN_HAND,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_MINION_OF_RACE_IN_HAND"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_DEATHRATTLE_MINION_DIED_THIS_GAME,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_DEATHRATTLE_MINION_DIED_THIS_GAME"
		},
		{
			PlayErrors.ErrorType.REQ_FRIENDLY_REBORN_MINION_DIED_THIS_GAME,
			"GAMEPLAY_PlayErrors_REQ_FRIENDLY_REBORN_MINION_DIED_THIS_GAME"
		},
		{
			PlayErrors.ErrorType.REQ_LITERALLY_UNPLAYABLE,
			"GAMEPLAY_PlayErrors_REQ_CANNOT_PLAY_THIS"
		},
		{
			PlayErrors.ErrorType.REQ_BOARD_NOT_COMPLETELY_FULL,
			"GAMEPLAY_PlayErrors_REQ_CANNOT_PLAY_THIS"
		},
		{
			PlayErrors.ErrorType.REQ_NOT_MINION_DORMANT,
			"GAMEPLAY_PlayErrors_REQ_NOT_MINION_DORMANT"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_NOT_DORMANT,
			"GAMEPLAY_PlayErrors_REQ_TARGET_NOT_DORMANT"
		},
		{
			PlayErrors.ErrorType.REQ_TWO_OF_A_KIND,
			"GAMEPLAY_PlayErrors_REQ_TWO_OF_A_KIND"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_NOT_HAVE_TAG,
			"GAMEPLAY_PlayErrors_REQ_TARGET_NOT_HAVE_TAG"
		},
		{
			PlayErrors.ErrorType.REQ_TARGET_MUST_HAVE_TAG,
			"GAMEPLAY_PlayErrors_REQ_TARGET_MUST_HAVE_TAG"
		},
		{
			PlayErrors.ErrorType.REQ_DRAG_TO_PLAY,
			"GAMEPLAY_PlayErrors_REQ_DRAG_TO_PLAY"
		}
	};

	// Token: 0x020016D9 RID: 5849
	public enum ErrorType
	{
		// Token: 0x0400B233 RID: 45619
		INVALID = -1,
		// Token: 0x0400B234 RID: 45620
		NONE,
		// Token: 0x0400B235 RID: 45621
		REQ_MINION_TARGET,
		// Token: 0x0400B236 RID: 45622
		REQ_FRIENDLY_TARGET,
		// Token: 0x0400B237 RID: 45623
		REQ_ENEMY_TARGET,
		// Token: 0x0400B238 RID: 45624
		REQ_DAMAGED_TARGET,
		// Token: 0x0400B239 RID: 45625
		REQ_MAX_SECRETS,
		// Token: 0x0400B23A RID: 45626
		REQ_FROZEN_TARGET,
		// Token: 0x0400B23B RID: 45627
		REQ_CHARGE_TARGET,
		// Token: 0x0400B23C RID: 45628
		REQ_TARGET_MAX_ATTACK,
		// Token: 0x0400B23D RID: 45629
		REQ_NONSELF_TARGET,
		// Token: 0x0400B23E RID: 45630
		REQ_TARGET_WITH_RACE,
		// Token: 0x0400B23F RID: 45631
		REQ_TARGET_TO_PLAY,
		// Token: 0x0400B240 RID: 45632
		REQ_NUM_MINION_SLOTS,
		// Token: 0x0400B241 RID: 45633
		REQ_WEAPON_EQUIPPED,
		// Token: 0x0400B242 RID: 45634
		REQ_ENOUGH_MANA,
		// Token: 0x0400B243 RID: 45635
		REQ_YOUR_TURN,
		// Token: 0x0400B244 RID: 45636
		REQ_NONSTEALTH_ENEMY_TARGET,
		// Token: 0x0400B245 RID: 45637
		REQ_HERO_TARGET,
		// Token: 0x0400B246 RID: 45638
		REQ_SECRET_ZONE_CAP,
		// Token: 0x0400B247 RID: 45639
		REQ_MINION_CAP_IF_TARGET_AVAILABLE,
		// Token: 0x0400B248 RID: 45640
		REQ_MINION_CAP,
		// Token: 0x0400B249 RID: 45641
		REQ_TARGET_ATTACKED_THIS_TURN,
		// Token: 0x0400B24A RID: 45642
		REQ_TARGET_IF_AVAILABLE,
		// Token: 0x0400B24B RID: 45643
		REQ_MINIMUM_ENEMY_MINIONS,
		// Token: 0x0400B24C RID: 45644
		REQ_TARGET_FOR_COMBO,
		// Token: 0x0400B24D RID: 45645
		REQ_NOT_EXHAUSTED_ACTIVATE,
		// Token: 0x0400B24E RID: 45646
		REQ_UNIQUE_SECRET_OR_QUEST,
		// Token: 0x0400B24F RID: 45647
		REQ_TARGET_TAUNTER,
		// Token: 0x0400B250 RID: 45648
		REQ_CAN_BE_ATTACKED,
		// Token: 0x0400B251 RID: 45649
		REQ_ACTION_PWR_IS_MASTER_PWR,
		// Token: 0x0400B252 RID: 45650
		REQ_TARGET_MAGNET,
		// Token: 0x0400B253 RID: 45651
		REQ_ATTACK_GREATER_THAN_0,
		// Token: 0x0400B254 RID: 45652
		REQ_ATTACKER_NOT_FROZEN,
		// Token: 0x0400B255 RID: 45653
		REQ_HERO_OR_MINION_TARGET,
		// Token: 0x0400B256 RID: 45654
		REQ_CAN_BE_TARGETED_BY_SPELLS,
		// Token: 0x0400B257 RID: 45655
		REQ_SUBCARD_IS_PLAYABLE,
		// Token: 0x0400B258 RID: 45656
		REQ_TARGET_FOR_NO_COMBO,
		// Token: 0x0400B259 RID: 45657
		REQ_NOT_MINION_JUST_PLAYED,
		// Token: 0x0400B25A RID: 45658
		REQ_NOT_EXHAUSTED_HERO_POWER,
		// Token: 0x0400B25B RID: 45659
		REQ_CAN_BE_TARGETED_BY_OPPONENTS,
		// Token: 0x0400B25C RID: 45660
		REQ_ATTACKER_CAN_ATTACK,
		// Token: 0x0400B25D RID: 45661
		REQ_TARGET_MIN_ATTACK,
		// Token: 0x0400B25E RID: 45662
		REQ_CAN_BE_TARGETED_BY_HERO_POWERS,
		// Token: 0x0400B25F RID: 45663
		REQ_ENEMY_TARGET_NOT_IMMUNE,
		// Token: 0x0400B260 RID: 45664
		REQ_ALL_BASIC_TOTEMS_NOT_IN_PLAY,
		// Token: 0x0400B261 RID: 45665
		REQ_MINIMUM_TOTAL_MINIONS,
		// Token: 0x0400B262 RID: 45666
		REQ_MUST_TARGET_TAUNTER,
		// Token: 0x0400B263 RID: 45667
		REQ_UNDAMAGED_TARGET,
		// Token: 0x0400B264 RID: 45668
		REQ_CAN_BE_TARGETED_BY_BATTLECRIES,
		// Token: 0x0400B265 RID: 45669
		REQ_STEADY_SHOT,
		// Token: 0x0400B266 RID: 45670
		REQ_MINION_OR_ENEMY_HERO,
		// Token: 0x0400B267 RID: 45671
		REQ_TARGET_IF_AVAILABLE_AND_DRAGON_IN_HAND,
		// Token: 0x0400B268 RID: 45672
		REQ_LEGENDARY_TARGET,
		// Token: 0x0400B269 RID: 45673
		REQ_FRIENDLY_MINION_DIED_THIS_TURN,
		// Token: 0x0400B26A RID: 45674
		REQ_FRIENDLY_MINION_DIED_THIS_GAME,
		// Token: 0x0400B26B RID: 45675
		REQ_ENEMY_WEAPON_EQUIPPED,
		// Token: 0x0400B26C RID: 45676
		REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_MINIONS,
		// Token: 0x0400B26D RID: 45677
		REQ_TARGET_WITH_BATTLECRY,
		// Token: 0x0400B26E RID: 45678
		REQ_TARGET_WITH_DEATHRATTLE,
		// Token: 0x0400B26F RID: 45679
		REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_FRIENDLY_SECRETS,
		// Token: 0x0400B270 RID: 45680
		REQ_SECRET_ZONE_CAP_FOR_NON_SECRET,
		// Token: 0x0400B271 RID: 45681
		REQ_TARGET_EXACT_COST,
		// Token: 0x0400B272 RID: 45682
		REQ_STEALTHED_TARGET,
		// Token: 0x0400B273 RID: 45683
		REQ_MINION_SLOT_OR_MANA_CRYSTAL_SLOT,
		// Token: 0x0400B274 RID: 45684
		REQ_MAX_QUESTS,
		// Token: 0x0400B275 RID: 45685
		REQ_TARGET_IF_AVAILABE_AND_ELEMENTAL_PLAYED_LAST_TURN,
		// Token: 0x0400B276 RID: 45686
		REQ_TARGET_NOT_VAMPIRE,
		// Token: 0x0400B277 RID: 45687
		REQ_TARGET_NOT_DAMAGEABLE_ONLY_BY_WEAPONS,
		// Token: 0x0400B278 RID: 45688
		REQ_NOT_DISABLED_HERO_POWER,
		// Token: 0x0400B279 RID: 45689
		REQ_MUST_PLAY_OTHER_CARD_FIRST,
		// Token: 0x0400B27A RID: 45690
		REQ_HAND_NOT_FULL,
		// Token: 0x0400B27B RID: 45691
		REQ_TARGET_IF_AVAILABLE_AND_NO_3_COST_CARD_IN_DECK,
		// Token: 0x0400B27C RID: 45692
		REQ_CAN_BE_TARGETED_BY_COMBOS,
		// Token: 0x0400B27D RID: 45693
		REQ_CANNOT_PLAY_THIS,
		// Token: 0x0400B27E RID: 45694
		REQ_FRIENDLY_MINIONS_OF_RACE_DIED_THIS_GAME,
		// Token: 0x0400B27F RID: 45695
		REQ_OPPONENT_PLAYED_CARDS_THIS_GAME = 77,
		// Token: 0x0400B280 RID: 45696
		REQ_LITERALLY_UNPLAYABLE,
		// Token: 0x0400B281 RID: 45697
		REQ_TARGET_IF_AVAILABLE_AND_HERO_HAS_ATTACK,
		// Token: 0x0400B282 RID: 45698
		REQ_FRIENDLY_MINION_OF_RACE_DIED_THIS_TURN,
		// Token: 0x0400B283 RID: 45699
		REQ_TARGET_IF_AVAILABLE_AND_MINIMUM_SPELLS_PLAYED_THIS_TURN,
		// Token: 0x0400B284 RID: 45700
		REQ_FRIENDLY_MINION_OF_RACE_IN_HAND,
		// Token: 0x0400B285 RID: 45701
		REQ_FRIENDLY_DEATHRATTLE_MINION_DIED_THIS_GAME = 86,
		// Token: 0x0400B286 RID: 45702
		REQ_FRIENDLY_REBORN_MINION_DIED_THIS_GAME = 89,
		// Token: 0x0400B287 RID: 45703
		REQ_MINION_DIED_THIS_GAME,
		// Token: 0x0400B288 RID: 45704
		REQ_BOARD_NOT_COMPLETELY_FULL = 92,
		// Token: 0x0400B289 RID: 45705
		REQ_TARGET_IF_AVAILABLE_AND_HAS_OVERLOADED_MANA,
		// Token: 0x0400B28A RID: 45706
		REQ_TARGET_IF_AVAILABLE_AND_HERO_ATTACKED_THIS_TURN,
		// Token: 0x0400B28B RID: 45707
		REQ_TARGET_IF_AVAILABLE_AND_DRAWN_THIS_TURN,
		// Token: 0x0400B28C RID: 45708
		REQ_TARGET_IF_AVAILABLE_AND_NOT_DRAWN_THIS_TURN,
		// Token: 0x0400B28D RID: 45709
		REQ_TARGET_NON_TRIPLED_MINION,
		// Token: 0x0400B28E RID: 45710
		REQ_BOUGHT_MINION_THIS_TURN,
		// Token: 0x0400B28F RID: 45711
		REQ_SOLD_MINION_THIS_TURN,
		// Token: 0x0400B290 RID: 45712
		REQ_TARGET_IF_AVAILABLE_AND_PLAYER_HEALTH_CHANGED_THIS_TURN,
		// Token: 0x0400B291 RID: 45713
		REQ_TARGET_IF_AVAILABLE_AND_SOUL_FRAGMENT_IN_DECK,
		// Token: 0x0400B292 RID: 45714
		REQ_DAMAGED_TARGET_UNLESS_COMBO,
		// Token: 0x0400B293 RID: 45715
		REQ_NOT_MINION_DORMANT,
		// Token: 0x0400B294 RID: 45716
		REQ_TARGET_NOT_DORMANT,
		// Token: 0x0400B295 RID: 45717
		REQ_TARGET_IF_AVAILABLE_AND_BOUGHT_RACE_THIS_TURN,
		// Token: 0x0400B296 RID: 45718
		REQ_TARGET_IF_AVAILABLE_AND_SOLD_RACE_THIS_TURN,
		// Token: 0x0400B297 RID: 45719
		REQ_TWO_OF_A_KIND = 110,
		// Token: 0x0400B298 RID: 45720
		REQ_TARGET_NOT_HAVE_TAG = 116,
		// Token: 0x0400B299 RID: 45721
		REQ_TARGET_MUST_HAVE_TAG,
		// Token: 0x0400B29A RID: 45722
		REQ_DRAG_TO_PLAY = 999
	}
}
