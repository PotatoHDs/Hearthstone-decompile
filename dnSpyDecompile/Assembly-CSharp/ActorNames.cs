using System;
using UnityEngine;

// Token: 0x02000846 RID: 2118
public class ActorNames
{
	// Token: 0x0600726F RID: 29295 RVA: 0x0024E33C File Offset: 0x0024C53C
	public static string GetZoneActor(TAG_CARDTYPE cardType, TAG_CLASS classTag, TAG_ZONE zoneTag, Player controller, TAG_PREMIUM premium, bool isQuest, bool isSideQuest, bool isSigil, bool isGhostly, bool isPuzzle, TAG_PUZZLE_TYPE puzzleType, bool isRulebook, bool isSidekickHero)
	{
		switch (zoneTag)
		{
		case TAG_ZONE.PLAY:
		{
			string playActor = ActorNames.GetPlayActor(cardType, premium);
			if (!"Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9".Equals(playActor))
			{
				return playActor;
			}
			break;
		}
		case TAG_ZONE.DECK:
		case TAG_ZONE.REMOVEDFROMGAME:
		case TAG_ZONE.SETASIDE:
			return "Card_Invisible.prefab:579b3b9a80234754593f24582f9cb93b";
		case TAG_ZONE.HAND:
		{
			if (controller == null || !controller.IsRevealed())
			{
				return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
			}
			string handActor = ActorNames.GetHandActor(cardType, premium, isSidekickHero);
			if (!"Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9".Equals(handActor))
			{
				return handActor;
			}
			break;
		}
		case TAG_ZONE.GRAVEYARD:
			if (isGhostly && controller.GetSide() == Player.Side.OPPOSING)
			{
				return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
			}
			if (cardType == TAG_CARDTYPE.MINION)
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_MINION, premium);
			}
			if (cardType == TAG_CARDTYPE.WEAPON)
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_WEAPON, premium);
			}
			if (cardType == TAG_CARDTYPE.SPELL)
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_SPELL, premium);
			}
			if (cardType == TAG_CARDTYPE.HERO)
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_HERO, premium);
			}
			break;
		case TAG_ZONE.SECRET:
			if (isQuest)
			{
				return "Card_Play_Quest.prefab:321b6d1ad558ebd46996c1f4eeaccb0c";
			}
			if (isPuzzle)
			{
				if (puzzleType == TAG_PUZZLE_TYPE.MIRROR)
				{
					return "Card_Play_Puzzle_Mirror.prefab:4583d6e2b04fad74986ef47b4ff00c79";
				}
				if (puzzleType == TAG_PUZZLE_TYPE.LETHAL)
				{
					return "Card_Play_Puzzle_Lethal.prefab:00d669c10a286e84cb91df1d40312d4b";
				}
				if (puzzleType == TAG_PUZZLE_TYPE.SURVIVAL)
				{
					return "Card_Play_Puzzle_Survival.prefab:036a2c2eee552fc4db25051107a0b797";
				}
				if (puzzleType == TAG_PUZZLE_TYPE.CLEAR)
				{
					return "Card_Play_Puzzle_BoardClear.prefab:fd9eec17f48c319468f103336095ad7b";
				}
			}
			if (isRulebook)
			{
				return "Card_Play_Rulebook.prefab:a8fbb8b315f4a3244be82718c1606858";
			}
			if (isSideQuest)
			{
				if (classTag == TAG_CLASS.DRUID)
				{
					return "Card_Play_SideQuest_Druid.prefab:d1430dc4bc9786640a02f4b178b59393";
				}
				if (classTag == TAG_CLASS.HUNTER)
				{
					return "Card_Play_SideQuest_Hunter.prefab:c9ed37b5a056d4e4885dc882d9d37664";
				}
				if (classTag == TAG_CLASS.MAGE)
				{
					return "Card_Play_SideQuest_Mage.prefab:39faefe5a4f9cf54ba9d85deb7627acb";
				}
				if (classTag == TAG_CLASS.PALADIN)
				{
					return "Card_Play_SideQuest_Paladin.prefab:396bf10a7c7da404ea3624e009861780";
				}
				if (classTag == TAG_CLASS.ROGUE)
				{
					return "Card_Play_SideQuest_Rogue.prefab:e805c70aa076e6743925e8d06a4be247";
				}
			}
			if (isSigil && classTag == TAG_CLASS.DEMONHUNTER)
			{
				return "Card_Play_Sigil_DemonHunter.prefab:b1ee048f6f0150e4ebd512208fb6a707";
			}
			if (classTag == TAG_CLASS.HUNTER)
			{
				return "Card_Play_Secret_Hunter.prefab:fdf71d0657e17a7428a43c1a8f319818";
			}
			if (classTag == TAG_CLASS.MAGE)
			{
				return "Card_Play_Secret_Mage.prefab:ffc78954f637f6f4d8b8bb7ec0b936ca";
			}
			if (classTag == TAG_CLASS.PALADIN)
			{
				return "Card_Play_Secret_Paladin.prefab:b0f3901ff0fad674bb7c72faa7966e73";
			}
			if (classTag == TAG_CLASS.ROGUE)
			{
				return "Card_Play_Secret_Rogue.prefab:1b224ad272f03724c9bc0aa802456c3e";
			}
			if (classTag == TAG_CLASS.WARRIOR)
			{
				return "Card_Play_Secret_Wanderer.prefab:9eaa9bf6015f05f4e9bbe9ba5e42b20f";
			}
			return "Card_Play_Secret_Mage.prefab:ffc78954f637f6f4d8b8bb7ec0b936ca";
		}
		Debug.LogWarningFormat("ActorNames.GetZoneActor() - Can't determine actor for {0}. Returning {1} instead.", new object[]
		{
			cardType,
			"Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9"
		});
		return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
	}

	// Token: 0x06007270 RID: 29296 RVA: 0x0024E4EA File Offset: 0x0024C6EA
	private static bool ShouldObfuscate(Entity entity)
	{
		return entity.GetController() != null && !entity.GetController().IsFriendlySide() && entity.IsObfuscated();
	}

	// Token: 0x06007271 RID: 29297 RVA: 0x0024E50C File Offset: 0x0024C70C
	public static string GetZoneActor(Entity entity, TAG_ZONE zoneTag)
	{
		if (ActorNames.ShouldObfuscate(entity) && zoneTag == TAG_ZONE.PLAY)
		{
			return "Card_Play_Obfuscated.prefab:682f46c64054e9948875d38245cbacae";
		}
		if (entity.IsHero() && zoneTag == TAG_ZONE.GRAVEYARD)
		{
			return ActorNames.GetGraveyardActorForHero(entity);
		}
		return ActorNames.GetZoneActor(entity.GetCardType(), entity.GetClass(), zoneTag, entity.GetController(), entity.GetPremiumType(), entity.IsQuest(), entity.IsSideQuest(), entity.IsSigil(), entity.HasTag(GAME_TAG.GHOSTLY), entity.IsPuzzle(), entity.GetPuzzleType(), entity.IsRulebook(), entity.IsSidekickHero());
	}

	// Token: 0x06007272 RID: 29298 RVA: 0x0024E594 File Offset: 0x0024C794
	public static string GetZoneActor(EntityDef entityDef, TAG_ZONE zoneTag)
	{
		return ActorNames.GetZoneActor(entityDef.GetCardType(), entityDef.GetClass(), zoneTag, null, TAG_PREMIUM.NORMAL, entityDef.IsQuest(), entityDef.IsSideQuest(), entityDef.IsSigil(), entityDef.HasTag(GAME_TAG.GHOSTLY), entityDef.IsPuzzle(), entityDef.GetPuzzleType(), entityDef.IsRulebook(), entityDef.IsSidekickHero());
	}

	// Token: 0x06007273 RID: 29299 RVA: 0x0024E5EC File Offset: 0x0024C7EC
	public static string GetZoneActor(EntityDef entityDef, TAG_ZONE zoneTag, TAG_PREMIUM premium)
	{
		return ActorNames.GetZoneActor(entityDef.GetCardType(), entityDef.GetClass(), zoneTag, null, premium, entityDef.IsQuest(), entityDef.IsSideQuest(), entityDef.IsSigil(), entityDef.HasTag(GAME_TAG.GHOSTLY), entityDef.IsPuzzle(), entityDef.GetPuzzleType(), entityDef.IsRulebook(), entityDef.IsSidekickHero());
	}

	// Token: 0x06007274 RID: 29300 RVA: 0x0024E644 File Offset: 0x0024C844
	private static string GetGraveyardActorForHero(Entity entity)
	{
		Card card = entity.GetCard();
		if (entity.IsHero() && card != null && card.GetPrevZone() is ZoneHero)
		{
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_HERO, TAG_PREMIUM.NORMAL);
		}
		return ActorNames.GetZoneActor(entity.GetCardType(), entity.GetClass(), TAG_ZONE.GRAVEYARD, entity.GetController(), entity.GetPremiumType(), entity.IsQuest(), entity.IsSideQuest(), entity.IsSigil(), entity.HasTag(GAME_TAG.GHOSTLY), entity.IsPuzzle(), entity.GetPuzzleType(), entity.IsRulebook(), entity.IsSidekickHero());
	}

	// Token: 0x06007275 RID: 29301 RVA: 0x0024E6D4 File Offset: 0x0024C8D4
	public static string GetHandActor(TAG_CARDTYPE cardType, TAG_PREMIUM premiumType, bool isSidekickHero = false)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.HERO:
			if (isSidekickHero)
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.SIDEKICK_HERO, premiumType);
			}
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_HERO, premiumType);
		case TAG_CARDTYPE.MINION:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_MINION, premiumType);
		case TAG_CARDTYPE.SPELL:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_SPELL, premiumType);
		case TAG_CARDTYPE.WEAPON:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_WEAPON, premiumType);
		case TAG_CARDTYPE.HERO_POWER:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, premiumType);
		}
		return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
	}

	// Token: 0x06007276 RID: 29302 RVA: 0x0024E745 File Offset: 0x0024C945
	public static string GetHandActor(TAG_CARDTYPE cardType)
	{
		return ActorNames.GetHandActor(cardType, TAG_PREMIUM.NORMAL, false);
	}

	// Token: 0x06007277 RID: 29303 RVA: 0x0024E74F File Offset: 0x0024C94F
	public static string GetHandActor(Entity entity)
	{
		return ActorNames.GetHandActor(entity.GetCardType(), entity.GetPremiumType(), entity.IsSidekickHero());
	}

	// Token: 0x06007278 RID: 29304 RVA: 0x0024E768 File Offset: 0x0024C968
	public static string GetHandActor(EntityDef entityDef)
	{
		return ActorNames.GetHandActor(entityDef.GetCardType(), TAG_PREMIUM.NORMAL, entityDef.IsSidekickHero());
	}

	// Token: 0x06007279 RID: 29305 RVA: 0x0024E77C File Offset: 0x0024C97C
	public static string GetHandActor(EntityDef entityDef, TAG_PREMIUM premiumType)
	{
		return ActorNames.GetHandActor(entityDef.GetCardType(), premiumType, entityDef.IsSidekickHero());
	}

	// Token: 0x0600727A RID: 29306 RVA: 0x0024E790 File Offset: 0x0024C990
	public static string GetHeroSkinOrHandActor(TAG_CARDTYPE type, TAG_PREMIUM premium)
	{
		if (type == TAG_CARDTYPE.HERO)
		{
			return "Card_Hero_Skin.prefab:ed2af57fa6b571741ab047c2c3e0e663";
		}
		return ActorNames.GetHandActor(type, premium, false);
	}

	// Token: 0x0600727B RID: 29307 RVA: 0x0024E7A4 File Offset: 0x0024C9A4
	public static string GetPlayActor(TAG_CARDTYPE cardType, TAG_PREMIUM premiumType)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.HERO:
			return "Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d";
		case TAG_CARDTYPE.MINION:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_MINION, premiumType);
		case TAG_CARDTYPE.SPELL:
			return "Card_Invisible.prefab:579b3b9a80234754593f24582f9cb93b";
		case TAG_CARDTYPE.ENCHANTMENT:
			return "Card_Play_Enchantment.prefab:cc1eafed24951ee4c92ad007507b1b69";
		case TAG_CARDTYPE.WEAPON:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_WEAPON, premiumType);
		case TAG_CARDTYPE.ITEM:
		case TAG_CARDTYPE.TOKEN:
		case TAG_CARDTYPE.BLANK:
			break;
		case TAG_CARDTYPE.HERO_POWER:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_HERO_POWER, premiumType);
		case TAG_CARDTYPE.GAME_MODE_BUTTON:
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_GAME_MODE_BUTTON, premiumType);
		default:
			if (cardType == TAG_CARDTYPE.MOVE_MINION_HOVER_TARGET)
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_MOVE_MINION_HOVER_TARGET, premiumType);
			}
			break;
		}
		return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
	}

	// Token: 0x0600727C RID: 29308 RVA: 0x0024E828 File Offset: 0x0024CA28
	public static string GetBigCardActor(Entity entity)
	{
		return ActorNames.GetHistoryActor(entity, HistoryInfoType.NONE);
	}

	// Token: 0x0600727D RID: 29309 RVA: 0x0024E834 File Offset: 0x0024CA34
	public static bool ShouldDisplayTooltipInsteadOfBigCard(Entity entity)
	{
		TAG_CARDTYPE cardType = entity.GetCardType();
		return cardType == TAG_CARDTYPE.GAME_MODE_BUTTON;
	}

	// Token: 0x0600727E RID: 29310 RVA: 0x0024E850 File Offset: 0x0024CA50
	public static string GetHistoryActor(Entity entity, HistoryInfoType historyTileType)
	{
		if (entity.IsSecret() && entity.IsHidden())
		{
			return ActorNames.GetHistorySecretActor(entity);
		}
		if (ActorNames.ShouldObfuscate(entity))
		{
			return "History_Obfuscated.prefab:d620dfa4ff929274d8805efec62fc096";
		}
		if (string.IsNullOrEmpty(entity.GetCardId()))
		{
			return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
		}
		TAG_CARDTYPE cardType = entity.GetCardType();
		TAG_PREMIUM premiumType = entity.GetPremiumType();
		if (cardType != TAG_CARDTYPE.HERO)
		{
			if (cardType != TAG_CARDTYPE.HERO_POWER)
			{
				return ActorNames.GetHandActor(entity);
			}
			if (entity.GetController().IsFriendlySide())
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, premiumType);
			}
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER_OPPONENT, premiumType);
		}
		else if (entity.GetZone() != TAG_ZONE.PLAY || historyTileType == HistoryInfoType.CARD_PLAYED)
		{
			if (entity.IsSidekickHero())
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.SIDEKICK_HERO, premiumType);
			}
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HAND_HERO, premiumType);
		}
		else
		{
			if (!entity.IsSidekickHero())
			{
				return "History_Hero.prefab:a040b63fa76fd4348b2a41b3bdc9789c";
			}
			if (entity.GetController().IsFriendlySide())
			{
				return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.SIDEKICK_HERO, premiumType);
			}
			return ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.SIDEKICK_HERO_OPPONENT, premiumType);
		}
	}

	// Token: 0x0600727F RID: 29311 RVA: 0x0024E924 File Offset: 0x0024CB24
	public static string GetHistorySigilActor(Entity entity)
	{
		TAG_CLASS @class = entity.GetClass();
		if (@class == TAG_CLASS.DEMONHUNTER)
		{
			return "History_Sigil_DemonHunter.prefab:9ff48bb7c66476d40ba0df1661c2a1a2";
		}
		Debug.LogWarning(string.Format("ActorNames.GetHistorySigilActor() - No actor for class {0}. Returning {1} instead.", @class, "History_Sigil_DemonHunter.prefab:9ff48bb7c66476d40ba0df1661c2a1a2"));
		return "History_Sigil_DemonHunter.prefab:9ff48bb7c66476d40ba0df1661c2a1a2";
	}

	// Token: 0x06007280 RID: 29312 RVA: 0x0024E964 File Offset: 0x0024CB64
	public static string GetHistorySecretActor(Entity entity)
	{
		TAG_CLASS @class = entity.GetClass();
		if (@class == TAG_CLASS.HUNTER)
		{
			return "History_Secret_Hunter.prefab:5e8dcf274b20d714abaec2a80904d83e";
		}
		if (@class == TAG_CLASS.MAGE)
		{
			return "History_Secret_Mage.prefab:6efbdae2809ad704ab794654d8bf2156";
		}
		if (@class == TAG_CLASS.PALADIN)
		{
			return "History_Secret_Paladin.prefab:158dc4838feed994db5c6d8e6cb7792b";
		}
		if (@class == TAG_CLASS.ROGUE)
		{
			return "History_Secret_Rogue.prefab:c827cbea9c33b7c45967ec3281c012cf";
		}
		if (entity.IsDarkWandererSecret())
		{
			return "History_Secret_Wanderer.prefab:7b140cf72c157604899f60f60bb37bd8";
		}
		Debug.LogWarning(string.Format("ActorNames.GetHistorySecretActor() - No actor for class {0}. Returning {1} instead.", @class, "History_Secret_Mage.prefab:6efbdae2809ad704ab794654d8bf2156"));
		return "History_Secret_Mage.prefab:6efbdae2809ad704ab794654d8bf2156";
	}

	// Token: 0x06007281 RID: 29313 RVA: 0x0024E9D0 File Offset: 0x0024CBD0
	public static string GetNameWithPremiumType(ActorNames.ACTOR_ASSET actorName, TAG_PREMIUM premiumType)
	{
		string result = null;
		if (premiumType != TAG_PREMIUM.GOLDEN)
		{
			if (premiumType != TAG_PREMIUM.DIAMOND)
			{
				goto IL_2C;
			}
			if (ActorNames.s_diamondActorAssets.TryGetValue(actorName, out result))
			{
				return result;
			}
		}
		if (ActorNames.s_premiumActorAssets.TryGetValue(actorName, out result))
		{
			return result;
		}
		IL_2C:
		if (ActorNames.s_actorAssets.TryGetValue(actorName, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x04005B4B RID: 23371
	public const string INVISIBLE = "Card_Invisible.prefab:579b3b9a80234754593f24582f9cb93b";

	// Token: 0x04005B4C RID: 23372
	public const string HIDDEN = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";

	// Token: 0x04005B4D RID: 23373
	public const string HAND_MINION = "Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d";

	// Token: 0x04005B4E RID: 23374
	public const string HAND_SPELL = "Card_Hand_Ability.prefab:3c3f5189f0d0b3745a1c1ca21d41efe0";

	// Token: 0x04005B4F RID: 23375
	public const string HAND_WEAPON = "Card_Hand_Weapon.prefab:30888a1fdca5c6c43abcc5d9dca55783";

	// Token: 0x04005B50 RID: 23376
	public const string HAND_FATIGUE = "Card_Hand_Fatigue.prefab:ae394ca0bb29a964eb4c7eeb555f2fae";

	// Token: 0x04005B51 RID: 23377
	public const string HAND_BURNED_CARD = "Card_Hand_BurnAway.prefab:869912636c30bc244bace332571afc94";

	// Token: 0x04005B52 RID: 23378
	public const string HAND_HERO = "Card_Hand_Hero.prefab:a977c49edb5fb5d4c8dee4d2344d1395";

	// Token: 0x04005B53 RID: 23379
	public const string PLAY_MINION = "Card_Play_Ally.prefab:23b7de16184fa8042bf6b734e7ca4d60";

	// Token: 0x04005B54 RID: 23380
	public const string PLAY_WEAPON = "Card_Play_Weapon.prefab:71f767d4f10681a45ac853936d1db800";

	// Token: 0x04005B55 RID: 23381
	public const string PLAY_HERO = "Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d";

	// Token: 0x04005B56 RID: 23382
	public const string PLAY_HERO_POWER = "Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af";

	// Token: 0x04005B57 RID: 23383
	public const string PLAY_HERO_POWER_PREMIUM = "Card_Play_HeroPower_Premium.prefab:015ad985f9ec49e4db327d131fd79901";

	// Token: 0x04005B58 RID: 23384
	public const string PLAY_ENCHANTMENT = "Card_Play_Enchantment.prefab:cc1eafed24951ee4c92ad007507b1b69";

	// Token: 0x04005B59 RID: 23385
	public const string PLAY_OBFUSCATED = "Card_Play_Obfuscated.prefab:682f46c64054e9948875d38245cbacae";

	// Token: 0x04005B5A RID: 23386
	public const string SECRET = "Card_Play_Secret_Mage.prefab:ffc78954f637f6f4d8b8bb7ec0b936ca";

	// Token: 0x04005B5B RID: 23387
	public const string SECRET_HUNTER = "Card_Play_Secret_Hunter.prefab:fdf71d0657e17a7428a43c1a8f319818";

	// Token: 0x04005B5C RID: 23388
	public const string SECRET_MAGE = "Card_Play_Secret_Mage.prefab:ffc78954f637f6f4d8b8bb7ec0b936ca";

	// Token: 0x04005B5D RID: 23389
	public const string SECRET_PALADIN = "Card_Play_Secret_Paladin.prefab:b0f3901ff0fad674bb7c72faa7966e73";

	// Token: 0x04005B5E RID: 23390
	public const string SECRET_WANDERER = "Card_Play_Secret_Wanderer.prefab:9eaa9bf6015f05f4e9bbe9ba5e42b20f";

	// Token: 0x04005B5F RID: 23391
	public const string SECRET_ROGUE = "Card_Play_Secret_Rogue.prefab:1b224ad272f03724c9bc0aa802456c3e";

	// Token: 0x04005B60 RID: 23392
	public const string PUZZLE_MIRROR = "Card_Play_Puzzle_Mirror.prefab:4583d6e2b04fad74986ef47b4ff00c79";

	// Token: 0x04005B61 RID: 23393
	public const string PUZZLE_LETHAL = "Card_Play_Puzzle_Lethal.prefab:00d669c10a286e84cb91df1d40312d4b";

	// Token: 0x04005B62 RID: 23394
	public const string PUZZLE_SURVIVAL = "Card_Play_Puzzle_Survival.prefab:036a2c2eee552fc4db25051107a0b797";

	// Token: 0x04005B63 RID: 23395
	public const string PUZZLE_CLEAR = "Card_Play_Puzzle_BoardClear.prefab:fd9eec17f48c319468f103336095ad7b";

	// Token: 0x04005B64 RID: 23396
	public const string RULEBOOK = "Card_Play_Rulebook.prefab:a8fbb8b315f4a3244be82718c1606858";

	// Token: 0x04005B65 RID: 23397
	public const string QUEST = "Card_Play_Quest.prefab:321b6d1ad558ebd46996c1f4eeaccb0c";

	// Token: 0x04005B66 RID: 23398
	public const string SIDEQUEST_DRUID = "Card_Play_SideQuest_Druid.prefab:d1430dc4bc9786640a02f4b178b59393";

	// Token: 0x04005B67 RID: 23399
	public const string SIDEQUEST_HUNTER = "Card_Play_SideQuest_Hunter.prefab:c9ed37b5a056d4e4885dc882d9d37664";

	// Token: 0x04005B68 RID: 23400
	public const string SIDEQUEST_MAGE = "Card_Play_SideQuest_Mage.prefab:39faefe5a4f9cf54ba9d85deb7627acb";

	// Token: 0x04005B69 RID: 23401
	public const string SIDEQUEST_PALADIN = "Card_Play_SideQuest_Paladin.prefab:396bf10a7c7da404ea3624e009861780";

	// Token: 0x04005B6A RID: 23402
	public const string SIDEQUEST_ROGUE = "Card_Play_SideQuest_Rogue.prefab:e805c70aa076e6743925e8d06a4be247";

	// Token: 0x04005B6B RID: 23403
	public const string SIGIL_DEMONHUNTER = "Card_Play_Sigil_DemonHunter.prefab:b1ee048f6f0150e4ebd512208fb6a707";

	// Token: 0x04005B6C RID: 23404
	public const string HISTORY_HERO = "History_Hero.prefab:a040b63fa76fd4348b2a41b3bdc9789c";

	// Token: 0x04005B6D RID: 23405
	public const string HISTORY_HERO_POWER = "History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53";

	// Token: 0x04005B6E RID: 23406
	public const string HISTORY_HERO_POWER_PREMIUM = "History_HeroPower_Premium.prefab:081da807b95b8495e9f16825c5164787";

	// Token: 0x04005B6F RID: 23407
	public const string HISTORY_HERO_POWER_OPPONENT = "History_HeroPower_Opponent.prefab:a99d23d6e8630f94b96a8e096fffb16f";

	// Token: 0x04005B70 RID: 23408
	public const string HISTORY_SECRET_HUNTER = "History_Secret_Hunter.prefab:5e8dcf274b20d714abaec2a80904d83e";

	// Token: 0x04005B71 RID: 23409
	public const string HISTORY_SECRET_MAGE = "History_Secret_Mage.prefab:6efbdae2809ad704ab794654d8bf2156";

	// Token: 0x04005B72 RID: 23410
	public const string HISTORY_SECRET_PALADIN = "History_Secret_Paladin.prefab:158dc4838feed994db5c6d8e6cb7792b";

	// Token: 0x04005B73 RID: 23411
	public const string HISTORY_SECRET_ROGUE = "History_Secret_Rogue.prefab:c827cbea9c33b7c45967ec3281c012cf";

	// Token: 0x04005B74 RID: 23412
	public const string HISTORY_SECRET_WANDERER = "History_Secret_Wanderer.prefab:7b140cf72c157604899f60f60bb37bd8";

	// Token: 0x04005B75 RID: 23413
	public const string HISTORY_OBFUSCATED = "History_Obfuscated.prefab:d620dfa4ff929274d8805efec62fc096";

	// Token: 0x04005B76 RID: 23414
	public const string HISTORY_SIGIL_DEMONHUNTER = "History_Sigil_DemonHunter.prefab:9ff48bb7c66476d40ba0df1661c2a1a2";

	// Token: 0x04005B77 RID: 23415
	public const string BACON_LEADERBOARD_HERO = "Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0";

	// Token: 0x04005B78 RID: 23416
	public const string CHOOSE_HERO = "Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d";

	// Token: 0x04005B79 RID: 23417
	public const string COLLECTION_CARD_BACK = "Collection_Card_Back.prefab:a208f592a46e4f447b3026e82444177e";

	// Token: 0x04005B7A RID: 23418
	public const string COLLECTION_DECK_TILE = "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291";

	// Token: 0x04005B7B RID: 23419
	public const string COLLECTION_DECK_TILE_PHONE = "DeckCardBar_phone.prefab:bd1c5e767f791984e851553bc5cb3b07";

	// Token: 0x04005B7C RID: 23420
	public const string HERO_SKIN = "Card_Hero_Skin.prefab:ed2af57fa6b571741ab047c2c3e0e663";

	// Token: 0x04005B7D RID: 23421
	public const string RANDOM_REWARD = "Card_Random_Reward.prefab:403211800142ebf4593a290b92655167";

	// Token: 0x04005B7E RID: 23422
	public const string DUNGEON_CRAWL_BOSS = "Card_DungeonCrawl_Boss.prefab:c7f700bb034424e46a7c2321e4621965";

	// Token: 0x04005B7F RID: 23423
	public const string DUNGEON_CRAWL_BOSS_GIL = "Card_DungeonCrawl_Boss_Gil.prefab:4966dfc0194a4614bb8b217a816ee1d5";

	// Token: 0x04005B80 RID: 23424
	public const string DUNGEON_CRAWL_HERO = "Card_Dungeon_Play_Hero.prefab:183cb9cc59697844e911776ec349fe5e";

	// Token: 0x04005B81 RID: 23425
	public const string MODULAR_BUNDLE_HERO_SKIN = "Modular_Bundle_Card_Hero_Skin.prefab:ad8fda5915cc96747abd0e15821c9857";

	// Token: 0x04005B82 RID: 23426
	public const string MODULAR_BUNDLE_CARD_BACK = "Modular_Bundle_Card_Back.prefab:939c318747e79d54f81ad2abab4584a2";

	// Token: 0x04005B83 RID: 23427
	public static readonly Map<ActorNames.ACTOR_ASSET, string> s_actorAssets = new Map<ActorNames.ACTOR_ASSET, string>
	{
		{
			ActorNames.ACTOR_ASSET.HAND_MINION,
			"Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d"
		},
		{
			ActorNames.ACTOR_ASSET.HAND_SPELL,
			"Card_Hand_Ability.prefab:3c3f5189f0d0b3745a1c1ca21d41efe0"
		},
		{
			ActorNames.ACTOR_ASSET.HAND_WEAPON,
			"Card_Hand_Weapon.prefab:30888a1fdca5c6c43abcc5d9dca55783"
		},
		{
			ActorNames.ACTOR_ASSET.HAND_HERO,
			"Card_Hand_Hero.prefab:a977c49edb5fb5d4c8dee4d2344d1395"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_MINION,
			"Card_Play_Ally.prefab:23b7de16184fa8042bf6b734e7ca4d60"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_WEAPON,
			"Card_Play_Weapon.prefab:71f767d4f10681a45ac853936d1db800"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_HERO_POWER,
			"Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_GAME_MODE_BUTTON,
			"Card_Play_GameModeButton.prefab:6d260d8912ac3f945a4177ba5882eaf2"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_MOVE_MINION_HOVER_TARGET,
			"Card_Play_MoveMinionHoverTarget.prefab:1f57541a9fdc77344810e84b76693bc4"
		},
		{
			ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER,
			"History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53"
		},
		{
			ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER_OPPONENT,
			"History_HeroPower_Opponent.prefab:a99d23d6e8630f94b96a8e096fffb16f"
		},
		{
			ActorNames.ACTOR_ASSET.SIDEKICK_HERO,
			"HACK_Card_Sidekick_Hero.prefab:41f48e8a9035839458411693ff83dc05"
		},
		{
			ActorNames.ACTOR_ASSET.SIDEKICK_HERO_OPPONENT,
			"HACK_Card_Sidekick_Hero_Opponent.prefab:e5febd2683397284283842747bdf0894"
		}
	};

	// Token: 0x04005B84 RID: 23428
	public static readonly Map<ActorNames.ACTOR_ASSET, string> s_premiumActorAssets = new Map<ActorNames.ACTOR_ASSET, string>
	{
		{
			ActorNames.ACTOR_ASSET.HAND_MINION,
			"Card_Hand_Ally_Premium.prefab:b0f0a4abee3293540830967b829f2bec"
		},
		{
			ActorNames.ACTOR_ASSET.HAND_SPELL,
			"Card_Hand_Ability_Premium.prefab:5105f461bc4a48e4c8bf452b93cfd772"
		},
		{
			ActorNames.ACTOR_ASSET.HAND_WEAPON,
			"Card_Hand_Weapon_Premium.prefab:c7736007f7a350942bbe40e466ac357c"
		},
		{
			ActorNames.ACTOR_ASSET.HAND_HERO,
			"Card_Hand_Hero_Premium.prefab:aca669662daf766449cd351fe4691f8f"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_MINION,
			"Card_Play_Ally_Premium.prefab:99bd268ec3a056d4795110a141c6fd75"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_WEAPON,
			"Card_Play_Weapon_Premium.prefab:66cbba9ed8f300c43834ab519327f094"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_HERO_POWER,
			"Card_Play_HeroPower_Premium.prefab:015ad985f9ec49e4db327d131fd79901"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_GAME_MODE_BUTTON,
			"Card_Play_GameModeButton.prefab:6d260d8912ac3f945a4177ba5882eaf2"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_MOVE_MINION_HOVER_TARGET,
			"Card_Play_MoveMinionHoverTarget.prefab:1f57541a9fdc77344810e84b76693bc4"
		},
		{
			ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER,
			"History_HeroPower_Premium.prefab:081da807b95b8495e9f16825c5164787"
		},
		{
			ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER_OPPONENT,
			"History_HeroPower_Opponent_Premium.prefab:82e1456f33aae4b3d9b2dac73aaa3ffa"
		},
		{
			ActorNames.ACTOR_ASSET.SIDEKICK_HERO,
			"HACK_Card_Sidekick_Hero_Premium.prefab:188eb019cdd95564ab7a79318c09610e"
		},
		{
			ActorNames.ACTOR_ASSET.SIDEKICK_HERO_OPPONENT,
			"HACK_Card_Sidekick_Hero_Opponent_Premium.prefab:61b2057d1c5a1034abe309908083cab8"
		}
	};

	// Token: 0x04005B85 RID: 23429
	public static readonly Map<ActorNames.ACTOR_ASSET, string> s_diamondActorAssets = new Map<ActorNames.ACTOR_ASSET, string>
	{
		{
			ActorNames.ACTOR_ASSET.HAND_MINION,
			"Card_Hand_Ally_Diamond.prefab:5fdbef3fa7e0c05419050d01202a85d3"
		},
		{
			ActorNames.ACTOR_ASSET.PLAY_MINION,
			"Card_Play_Ally_Diamond.prefab:42fb12461ed7d0142a34f9b72399421c"
		}
	};

	// Token: 0x02002439 RID: 9273
	public enum ACTOR_ASSET
	{
		// Token: 0x0400E98D RID: 59789
		HAND_MINION,
		// Token: 0x0400E98E RID: 59790
		HAND_SPELL,
		// Token: 0x0400E98F RID: 59791
		HAND_WEAPON,
		// Token: 0x0400E990 RID: 59792
		HAND_HERO,
		// Token: 0x0400E991 RID: 59793
		PLAY_MINION,
		// Token: 0x0400E992 RID: 59794
		PLAY_WEAPON,
		// Token: 0x0400E993 RID: 59795
		PLAY_HERO,
		// Token: 0x0400E994 RID: 59796
		PLAY_HERO_POWER,
		// Token: 0x0400E995 RID: 59797
		PLAY_GAME_MODE_BUTTON,
		// Token: 0x0400E996 RID: 59798
		PLAY_MOVE_MINION_HOVER_TARGET,
		// Token: 0x0400E997 RID: 59799
		HISTORY_HERO_POWER,
		// Token: 0x0400E998 RID: 59800
		HISTORY_HERO_POWER_OPPONENT,
		// Token: 0x0400E999 RID: 59801
		SIDEKICK_HERO,
		// Token: 0x0400E99A RID: 59802
		SIDEKICK_HERO_OPPONENT
	}
}
