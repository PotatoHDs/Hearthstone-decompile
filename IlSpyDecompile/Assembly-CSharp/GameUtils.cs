using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using bgs;
using bgs.types;
using Hearthstone;
using Hearthstone.DungeonCrawl;
using Hearthstone.Login;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using UnityEngine;
using UnityEngine.Events;

public class GameUtils
{
	[Serializable]
	public class StringEvent : UnityEvent<string>
	{
	}

	public delegate void EmoteSoundLoaded(CardSoundSpell emoteObj);

	public delegate void LoadActorCallback(Actor actor);

	private class LoadActorCallbackInfo
	{
		public DefLoader.DisposableFullDef fullDef;

		public TAG_PREMIUM premium;
	}

	public static StringEvent OnAnimationExitEvent = new StringEvent();

	public static readonly IEnumerable<TAG_CLASS> ORDERED_HERO_CLASSES = new TAG_CLASS[10]
	{
		TAG_CLASS.DEMONHUNTER,
		TAG_CLASS.DRUID,
		TAG_CLASS.HUNTER,
		TAG_CLASS.MAGE,
		TAG_CLASS.PALADIN,
		TAG_CLASS.PRIEST,
		TAG_CLASS.ROGUE,
		TAG_CLASS.SHAMAN,
		TAG_CLASS.WARLOCK,
		TAG_CLASS.WARRIOR
	};

	public static readonly IEnumerable<TAG_CLASS> CLASSIC_ORDERED_HERO_CLASSES = new TAG_CLASS[9]
	{
		TAG_CLASS.DRUID,
		TAG_CLASS.HUNTER,
		TAG_CLASS.MAGE,
		TAG_CLASS.PALADIN,
		TAG_CLASS.PRIEST,
		TAG_CLASS.ROGUE,
		TAG_CLASS.SHAMAN,
		TAG_CLASS.WARLOCK,
		TAG_CLASS.WARRIOR
	};

	private static ReactiveNetCacheObject<NetCache.NetCacheProfileProgress> s_profileProgress = ReactiveNetCacheObject<NetCache.NetCacheProfileProgress>.CreateInstance();

	public static string TranslateDbIdToCardId(int dbId, bool showWarning = false)
	{
		CardDbfRecord record = GameDbf.Card.GetRecord(dbId);
		if (record == null)
		{
			if (showWarning)
			{
				Log.All.PrintError("GameUtils.TranslateDbIdToCardId() - Failed to find card with database id {0} in the Card DBF.", dbId);
			}
			return null;
		}
		string noteMiniGuid = record.NoteMiniGuid;
		if (noteMiniGuid == null)
		{
			if (showWarning)
			{
				Log.All.PrintError("GameUtils.TranslateDbIdToCardId() - Card with database id {0} has no NOTE_MINI_GUID field in the Card DBF.", dbId);
			}
			return null;
		}
		return noteMiniGuid;
	}

	public static int TranslateCardIdToDbId(string cardId, bool showWarning = false)
	{
		CardDbfRecord cardRecord = GetCardRecord(cardId);
		if (cardRecord == null)
		{
			if (showWarning)
			{
				Log.All.PrintError("GameUtils.TranslateCardIdToDbId() - There is no card with NOTE_MINI_GUID {0} in the Card DBF.", cardId);
			}
			return 0;
		}
		return cardRecord.ID;
	}

	public static bool IsCardCollectible(string cardId)
	{
		return GetCardTagValue(cardId, GAME_TAG.COLLECTIBLE) == 1;
	}

	public static bool IsCardInBattlegroundsPool(string cardId)
	{
		if (GetCardTagValue(cardId, GAME_TAG.IS_BACON_POOL_MINION) != 1)
		{
			return GetCardTagValue(cardId, GAME_TAG.BACON_HERO_CAN_BE_DRAFTED) == 1;
		}
		return true;
	}

	public static bool IsAdventureRotated(AdventureDbId adventureID)
	{
		return IsAdventureRotated(adventureID, DateTime.UtcNow);
	}

	public static bool IsAdventureRotated(AdventureDbId adventureID, DateTime utcTimestamp)
	{
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureID);
		if (record == null)
		{
			return false;
		}
		return !SpecialEventManager.Get().IsEventActive(record.StandardEvent, activeIfDoesNotExist: false, utcTimestamp);
	}

	public static bool IsBoosterRotated(BoosterDbId boosterID, DateTime utcTimestamp)
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)boosterID);
		if (record == null)
		{
			return false;
		}
		return !SpecialEventManager.Get().IsEventActive(record.StandardEvent, activeIfDoesNotExist: false, utcTimestamp);
	}

	public static FormatType GetCardSetFormat(TAG_CARD_SET cardSet)
	{
		if (cardSet == TAG_CARD_SET.VANILLA)
		{
			return FormatType.FT_CLASSIC;
		}
		if (IsSetRotated(cardSet))
		{
			return FormatType.FT_WILD;
		}
		return FormatType.FT_STANDARD;
	}

	public static TAG_CARD_SET[] GetCardSetsInFormat(FormatType formatType)
	{
		TAG_CARD_SET[] result = null;
		switch (formatType)
		{
		case FormatType.FT_CLASSIC:
			result = GetClassicSets();
			break;
		case FormatType.FT_STANDARD:
			result = GetStandardSets();
			break;
		case FormatType.FT_WILD:
			result = GetAllWildPlayableSets();
			break;
		}
		return result;
	}

	public static bool IsCardSetValidForFormat(FormatType formatType, TAG_CARD_SET cardSet)
	{
		switch (formatType)
		{
		case FormatType.FT_CLASSIC:
			return IsClassicCardSet(cardSet);
		case FormatType.FT_WILD:
			if (!IsWildCardSet(cardSet))
			{
				return IsStandardCardSet(cardSet);
			}
			return true;
		case FormatType.FT_STANDARD:
			return IsStandardCardSet(cardSet);
		default:
			return false;
		}
	}

	public static bool IsCardValidForFormat(FormatType formatType, int cardDbId)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardDbId);
		return IsCardValidForFormat(formatType, entityDef);
	}

	public static bool IsCardValidForFormat(FormatType formatType, string cardId)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		return IsCardValidForFormat(formatType, entityDef);
	}

	public static bool IsCardValidForFormat(FormatType formatType, EntityDef def)
	{
		if (def != null)
		{
			return IsCardSetValidForFormat(formatType, def.GetCardSet());
		}
		return false;
	}

	public static bool IsWildCardSet(TAG_CARD_SET cardSet)
	{
		return GetCardSetFormat(cardSet) == FormatType.FT_WILD;
	}

	public static bool IsWildCard(int cardDbId)
	{
		return IsWildCard(DefLoader.Get().GetEntityDef(cardDbId));
	}

	public static bool IsWildCard(string cardId)
	{
		return IsWildCard(DefLoader.Get().GetEntityDef(cardId));
	}

	public static bool IsWildCard(EntityDef def)
	{
		if (def != null)
		{
			return IsWildCardSet(def.GetCardSet());
		}
		return false;
	}

	public static bool IsClassicCardSet(TAG_CARD_SET cardSet)
	{
		return GetCardSetFormat(cardSet) == FormatType.FT_CLASSIC;
	}

	public static bool IsClassicCard(int cardDbId)
	{
		return IsClassicCard(DefLoader.Get().GetEntityDef(cardDbId));
	}

	public static bool IsClassicCard(string cardId)
	{
		return IsClassicCard(DefLoader.Get().GetEntityDef(cardId));
	}

	public static bool IsClassicCard(EntityDef def)
	{
		if (def != null)
		{
			return IsClassicCardSet(def.GetCardSet());
		}
		return false;
	}

	public static bool IsCoreCard(string cardId)
	{
		return IsCoreCard(DefLoader.Get().GetEntityDef(cardId));
	}

	public static bool IsCoreCard(EntityDef def)
	{
		return def?.IsCoreCard() ?? false;
	}

	public static bool IsStandardCardSet(TAG_CARD_SET cardSet)
	{
		return GetCardSetFormat(cardSet) == FormatType.FT_STANDARD;
	}

	public static bool IsStandardCard(int cardDbId)
	{
		return IsStandardCard(DefLoader.Get().GetEntityDef(cardDbId));
	}

	public static bool IsStandardCard(string cardId)
	{
		return IsStandardCard(DefLoader.Get().GetEntityDef(cardId));
	}

	public static bool IsStandardCard(EntityDef def)
	{
		if (def != null)
		{
			return IsStandardCardSet(def.GetCardSet());
		}
		return false;
	}

	public static string GetCardSetFormatAsString(TAG_CARD_SET cardSet)
	{
		return GetCardSetFormat(cardSet).ToString().Replace("FT_", "");
	}

	public static bool IsSetRotated(TAG_CARD_SET set)
	{
		return IsSetRotated(set, DateTime.UtcNow);
	}

	public static bool IsSetRotated(TAG_CARD_SET set, DateTime utcTimestamp)
	{
		CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(set);
		if (cardSet == null)
		{
			return false;
		}
		if (!SpecialEventManager.Get().IsEventActive(cardSet.StandardEvent, activeIfDoesNotExist: false, utcTimestamp))
		{
			return SpecialEventManager.Get().HasEventStarted(cardSet.StandardEvent);
		}
		return false;
	}

	public static bool IsCardRotated(int cardDbId)
	{
		return IsCardRotated(DefLoader.Get().GetEntityDef(cardDbId));
	}

	public static bool IsCardRotated(string cardId)
	{
		return IsCardRotated(DefLoader.Get().GetEntityDef(cardId));
	}

	public static bool IsCardRotated(EntityDef def)
	{
		return IsCardRotated(def, DateTime.UtcNow);
	}

	public static bool IsCardRotated(EntityDef def, DateTime utcTimestamp)
	{
		return IsSetRotated(def.GetCardSet(), utcTimestamp);
	}

	public static bool IsLegacySet(TAG_CARD_SET set)
	{
		return IsLegacySet(set, DateTime.UtcNow);
	}

	public static bool IsLegacySet(TAG_CARD_SET set, DateTime utcTimestamp)
	{
		CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(set);
		if (cardSet == null)
		{
			return false;
		}
		return SpecialEventManager.Get().IsEventActive(cardSet.LegacyCardSetEvent, activeIfDoesNotExist: false, utcTimestamp);
	}

	public static bool IsCardGameplayEventActive(EntityDef def)
	{
		return IsCardGameplayEventActive(def.GetCardId());
	}

	public static bool IsCardGameplayEventActive(string cardId)
	{
		CardDbfRecord cardRecord = GetCardRecord(cardId);
		if (cardRecord == null)
		{
			Debug.LogWarning($"GameUtils.IsCardGameplayEventActive could not find DBF record for card {cardId}");
			return false;
		}
		return SpecialEventManager.Get().IsEventActive(cardRecord.GameplayEvent, activeIfDoesNotExist: true);
	}

	public static bool IsCardCraftableWhenWild(string cardId)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		if (entityDef == null)
		{
			return false;
		}
		return GameDbf.GetIndex().GetCardSet(entityDef.GetCardSet())?.CraftableWhenWild ?? false;
	}

	public static bool DeckIncludesRotatedCards(int deckId)
	{
		DeckDbfRecord record = GameDbf.Deck.GetRecord(deckId);
		if (record == null)
		{
			Log.Decks.PrintWarning("DeckRuleset.IsDeckWild(): {0} is invalid deck id", deckId);
			return false;
		}
		foreach (DeckCardDbfRecord card in record.Cards)
		{
			if (IsCardRotated(card.CardId))
			{
				return true;
			}
		}
		return false;
	}

	public static TAG_CARD_SET[] GetStandardSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		foreach (TAG_CARD_SET displayableCardSet in CollectionManager.Get().GetDisplayableCardSets())
		{
			if (GetCardSetFormat(displayableCardSet) == FormatType.FT_STANDARD)
			{
				list.Add(displayableCardSet);
			}
		}
		return list.ToArray();
	}

	public static TAG_CARD_SET[] GetWildSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		foreach (TAG_CARD_SET displayableCardSet in CollectionManager.Get().GetDisplayableCardSets())
		{
			if (GetCardSetFormat(displayableCardSet) == FormatType.FT_WILD)
			{
				list.Add(displayableCardSet);
			}
		}
		return list.ToArray();
	}

	public static TAG_CARD_SET[] GetAllWildPlayableSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		list.AddRange(GetStandardSets());
		list.AddRange(GetWildSets());
		return list.ToArray();
	}

	public static TAG_CARD_SET[] GetLegacySets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		foreach (TAG_CARD_SET displayableCardSet in CollectionManager.Get().GetDisplayableCardSets())
		{
			if (IsLegacySet(displayableCardSet))
			{
				list.Add(displayableCardSet);
			}
		}
		return list.ToArray();
	}

	public static TAG_CARD_SET[] GetClassicSets()
	{
		return (from cardSet in CollectionManager.Get().GetDisplayableCardSets()
			where IsClassicCardSet(cardSet)
			select cardSet).ToArray();
	}

	public static TAG_CLASS GetTagClassFromCardDbId(int cardDbId)
	{
		return (TAG_CLASS)GameDbf.GetIndex().GetCardTagValue(cardDbId, GAME_TAG.CLASS);
	}

	public static int CountAllCollectibleCards()
	{
		return GameDbf.GetIndex().GetCollectibleCardCount();
	}

	public static List<string> GetAllCardIds()
	{
		return GameDbf.GetIndex().GetAllCardIds();
	}

	public static List<string> GetAllCollectibleCardIds()
	{
		return GameDbf.GetIndex().GetCollectibleCardIds();
	}

	public static List<int> GetAllCollectibleCardDbIds()
	{
		return GameDbf.GetIndex().GetCollectibleCardDbIds();
	}

	public static List<string> GetNonHeroSkinCollectibleCardIds()
	{
		List<string> list = new List<string>();
		foreach (string allCollectibleCardId in GetAllCollectibleCardIds())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(allCollectibleCardId);
			if (entityDef != null && !entityDef.IsHeroSkin())
			{
				list.Add(allCollectibleCardId);
			}
		}
		return list;
	}

	public static List<string> GetNonHeroSkinAllCardIds()
	{
		List<string> list = new List<string>();
		foreach (string allCardId in GetAllCardIds())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(allCardId);
			if (entityDef != null && !entityDef.IsHeroSkin() && entityDef.GetCardType() != TAG_CARDTYPE.ENCHANTMENT)
			{
				list.Add(allCardId);
			}
		}
		return list;
	}

	public static CardDbfRecord GetCardRecord(string cardId)
	{
		if (cardId == null)
		{
			return null;
		}
		return GameDbf.GetIndex().GetCardRecord(cardId);
	}

	public static List<CardChangeDbfRecord> GetCardChangeRecords(string cardId)
	{
		if (cardId == null)
		{
			return null;
		}
		int cardId2 = TranslateCardIdToDbId(cardId);
		return GameDbf.GetIndex().GetCardChangeRecords(cardId2);
	}

	public static int GetCardTagValue(string cardId, GAME_TAG tagId)
	{
		int cardDbId = TranslateCardIdToDbId(cardId);
		return GameDbf.GetIndex().GetCardTagValue(cardDbId, tagId);
	}

	public static int GetCardTagValue(int cardDbId, GAME_TAG tagId)
	{
		return GameDbf.GetIndex().GetCardTagValue(cardDbId, tagId);
	}

	public static IEnumerable<CardTagDbfRecord> GetCardTagRecords(string cardId)
	{
		int cardDbId = TranslateCardIdToDbId(cardId);
		return GameDbf.GetIndex().GetCardTagRecords(cardDbId);
	}

	public static string GetHeroPowerCardIdFromHero(string heroCardId)
	{
		int cardTagValue = GetCardTagValue(heroCardId, GAME_TAG.HERO_POWER);
		if (cardTagValue == 0)
		{
			return string.Empty;
		}
		return TranslateDbIdToCardId(cardTagValue);
	}

	public static string GetHeroPowerCardIdFromHero(int heroDbId)
	{
		if (GameDbf.Card.GetRecord(heroDbId) == null)
		{
			Debug.LogError($"GameUtils.GetHeroPowerCardIdFromHero() - failed to find record for heroDbId {heroDbId}");
			return string.Empty;
		}
		return TranslateDbIdToCardId(GetCardTagValue(heroDbId, GAME_TAG.HERO_POWER));
	}

	public static string GetCardIdFromHeroDbId(int heroDbId)
	{
		CardHeroDbfRecord record = GameDbf.CardHero.GetRecord(heroDbId);
		if (record == null)
		{
			Debug.LogError($"GameUtils.GetCardIdFromHeroDbId() - failed to find record for heroDbId {heroDbId}");
			return string.Empty;
		}
		return TranslateDbIdToCardId(record.CardId);
	}

	public static TAG_CARD_SET GetCardSetFromCardID(string cardID)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		if (entityDef == null)
		{
			Debug.LogError($"Null EntityDef in GetCardSetFromCardID() for {cardID}");
			return TAG_CARD_SET.INVALID;
		}
		return entityDef.GetCardSet();
	}

	public static int GetCardIdFromGuestHeroDbId(int guestHeroDbId)
	{
		GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(guestHeroDbId);
		if (record == null)
		{
			Debug.LogError($"GameUtils.GetCardIdFromGuestHeroDbId() - failed to find record for guestHeroDbId {guestHeroDbId}");
			return 0;
		}
		return record.CardId;
	}

	public static int GetFavoriteHeroCardDBIdFromClass(TAG_CLASS classTag)
	{
		string text = CollectionManager.Get().GetFavoriteHero(classTag)?.Name;
		if (string.IsNullOrEmpty(text))
		{
			text = CollectionManager.GetHeroCardId(classTag, CardHero.HeroType.VANILLA);
		}
		return TranslateCardIdToDbId(text);
	}

	public static bool IsVanillaHero(string cardId)
	{
		int id = TranslateCardIdToDbId(cardId);
		CardDbfRecord record = GameDbf.Card.GetRecord(id);
		if (record == null)
		{
			return false;
		}
		return record.CardHero?.HeroType == CardHero.HeroType.VANILLA;
	}

	public static string GetGalakrondCardIdByClass(TAG_CLASS classTag)
	{
		string result = "";
		switch (classTag)
		{
		case TAG_CLASS.PRIEST:
			result = "DRG_660";
			break;
		case TAG_CLASS.ROGUE:
			result = "DRG_610";
			break;
		case TAG_CLASS.SHAMAN:
			result = "DRG_620";
			break;
		case TAG_CLASS.WARLOCK:
			result = "DRG_600";
			break;
		case TAG_CLASS.WARRIOR:
			result = "DRG_650";
			break;
		}
		return result;
	}

	public static NetCache.HeroLevel GetHeroLevel(TAG_CLASS heroClass)
	{
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		if (netObject == null)
		{
			Debug.LogError("GameUtils.GetHeroLevel() - NetCache.NetCacheHeroLevels is null");
			return null;
		}
		return netObject.Levels.Find((NetCache.HeroLevel obj) => obj.Class == heroClass);
	}

	public static int? GetTotalHeroLevel()
	{
		int? result = null;
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		if (netObject != null)
		{
			result = 0;
			{
				foreach (NetCache.HeroLevel level in netObject.Levels)
				{
					result += level.CurrentLevel.Level;
				}
				return result;
			}
		}
		Debug.LogError("GameUtils.GetHeroLevel() - NetCache.NetCacheHeroLevels is null");
		return result;
	}

	public static int CardPremiumSortComparisonAsc(TAG_PREMIUM premium1, TAG_PREMIUM premium2)
	{
		return premium1 - premium2;
	}

	public static int CardPremiumSortComparisonDesc(TAG_PREMIUM premium1, TAG_PREMIUM premium2)
	{
		return premium2 - premium1;
	}

	public static bool CanConcedeCurrentMission()
	{
		if (GameState.Get() == null)
		{
			return false;
		}
		if (GameMgr.Get().IsTutorial())
		{
			return false;
		}
		if (GameMgr.Get().IsSpectator())
		{
			return false;
		}
		return true;
	}

	public static bool CanRestartCurrentMission(bool checkTutorial = true)
	{
		if (GameState.Get() == null)
		{
			return false;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.DISABLE_RESTART_BUTTON))
		{
			return false;
		}
		if (checkTutorial && GameMgr.Get().IsTutorial())
		{
			return false;
		}
		if (GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (!GameMgr.Get().IsAI())
		{
			return false;
		}
		if (!GameMgr.Get().HasLastPlayedDeckId())
		{
			return false;
		}
		if (!BattleNet.IsConnected())
		{
			return false;
		}
		if (DemoMgr.Get().IsDemo() && !DemoMgr.Get().CanRestartMissions())
		{
			return false;
		}
		if (GameMgr.Get().IsDungeonCrawlMission())
		{
			return false;
		}
		return true;
	}

	public static bool IsWaitingForOpponentReconnect()
	{
		if (GameState.Get() == null)
		{
			return false;
		}
		return GameState.Get().GetGameEntity().HasTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD);
	}

	public static Card GetJoustWinner(Network.HistMetaData metaData)
	{
		if (metaData == null)
		{
			return null;
		}
		if (metaData.MetaType != HistoryMeta.Type.JOUST)
		{
			return null;
		}
		return GameState.Get().GetEntity(metaData.Data)?.GetCard();
	}

	public static bool IsHistoryDeathTagChange(Network.HistTagChange tagChange)
	{
		Entity entity = GameState.Get().GetEntity(tagChange.Entity);
		if (entity == null)
		{
			return false;
		}
		if (entity.IsEnchantment())
		{
			return false;
		}
		if (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			return false;
		}
		if (tagChange.Tag == 360 && tagChange.Value == 1)
		{
			return true;
		}
		if (entity.IsMinion() && tagChange.Tag == 49 && tagChange.Value == 4 && entity.GetZone() == TAG_ZONE.PLAY)
		{
			return true;
		}
		return false;
	}

	public static bool IsHistoryDiscardTagChange(Network.HistTagChange tagChange)
	{
		if (tagChange.Tag != 49)
		{
			return false;
		}
		if (GameState.Get().GetEntity(tagChange.Entity).GetZone() != TAG_ZONE.HAND)
		{
			return false;
		}
		if (tagChange.Value != 4)
		{
			return false;
		}
		return true;
	}

	public static bool IsHistoryMovedToSetAsideTagChange(Network.HistTagChange tagChange)
	{
		if (tagChange.Tag != 49)
		{
			return false;
		}
		if (tagChange.Value != 6)
		{
			return false;
		}
		return true;
	}

	public static bool IsEntityDeathTagChange(Network.HistTagChange tagChange)
	{
		if (tagChange.Tag != 49)
		{
			return false;
		}
		if (tagChange.Value != 4)
		{
			return false;
		}
		if (GameState.Get().GetEntity(tagChange.Entity) == null)
		{
			return false;
		}
		return true;
	}

	public static bool IsCharacterDeathTagChange(Network.HistTagChange tagChange)
	{
		if (tagChange.Tag != 49)
		{
			return false;
		}
		if (tagChange.Value != 4)
		{
			return false;
		}
		Entity entity = GameState.Get().GetEntity(tagChange.Entity);
		if (entity == null)
		{
			return false;
		}
		if (!entity.IsCharacter())
		{
			return false;
		}
		return true;
	}

	public static bool IsPreGameOverPlayState(TAG_PLAYSTATE playState)
	{
		if ((uint)(playState - 2) <= 1u || (uint)(playState - 7) <= 1u)
		{
			return true;
		}
		return false;
	}

	public static bool IsGameOverTag(int entityId, int tag, int val)
	{
		return IsGameOverTag(GameState.Get().GetEntity(entityId) as Player, tag, val);
	}

	public static bool IsGameOverTag(Player player, int tag, int val)
	{
		if (player == null)
		{
			return false;
		}
		if (tag != 17)
		{
			return false;
		}
		if (!player.IsFriendlySide())
		{
			return false;
		}
		if ((uint)(val - 4) <= 2u)
		{
			return true;
		}
		return false;
	}

	public static bool IsFriendlyConcede(Network.HistTagChange tagChange)
	{
		if (tagChange.Tag != 17)
		{
			return false;
		}
		Player player = GameState.Get().GetEntity(tagChange.Entity) as Player;
		if (player == null)
		{
			return false;
		}
		if (!player.IsFriendlySide())
		{
			return false;
		}
		return tagChange.Value == 8;
	}

	public static bool IsBeginPhase(TAG_STEP step)
	{
		if ((uint)step <= 4u)
		{
			return true;
		}
		return false;
	}

	public static bool IsPastBeginPhase(TAG_STEP step)
	{
		return !IsBeginPhase(step);
	}

	public static bool IsMainPhase(TAG_STEP step)
	{
		if ((uint)(step - 5) <= 8u || (uint)(step - 16) <= 1u)
		{
			return true;
		}
		return false;
	}

	public static List<Entity> GetEntitiesKilledBySourceAmongstTargets(int damageSourceID, List<Entity> targetEntities)
	{
		List<Entity> list = new List<Entity>();
		foreach (Entity targetEntity in targetEntities)
		{
			if (targetEntity != null)
			{
				list.Add(targetEntity.CloneForZoneMgr());
			}
		}
		List<Entity> list2 = new List<Entity>();
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		List<PowerTaskList> list3 = new List<PowerTaskList>();
		if (powerProcessor.GetCurrentTaskList() != null)
		{
			list3.Add(powerProcessor.GetCurrentTaskList());
		}
		list3.AddRange(powerProcessor.GetPowerQueue().GetList());
		for (int i = 0; i < list3.Count; i++)
		{
			List<PowerTask> taskList = list3[i].GetTaskList();
			for (int j = 0; j < taskList.Count; j++)
			{
				PowerTask powerTask = taskList[j];
				Network.HistTagChange tagChange = powerTask.GetPower() as Network.HistTagChange;
				if (tagChange == null)
				{
					continue;
				}
				if (tagChange.Tag == 18)
				{
					list.Find((Entity targetEntity) => targetEntity.GetEntityId() == tagChange.Entity)?.SetTag(18, tagChange.Value);
				}
				else if (tagChange.Tag == 49 && tagChange.Value == 4)
				{
					Entity entity = list.Find((Entity targetEntity) => targetEntity.GetEntityId() == tagChange.Entity);
					if (entity != null && entity.GetTag(GAME_TAG.LAST_AFFECTED_BY) == damageSourceID)
					{
						list2.Add(entity);
					}
				}
			}
		}
		return list2;
	}

	public static void ApplyPower(Entity entity, Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.SHOW_ENTITY:
			ApplyShowEntity(entity, (Network.HistShowEntity)power);
			break;
		case Network.PowerType.HIDE_ENTITY:
			ApplyHideEntity(entity, (Network.HistHideEntity)power);
			break;
		case Network.PowerType.TAG_CHANGE:
			ApplyTagChange(entity, (Network.HistTagChange)power);
			break;
		}
	}

	public static void ApplyShowEntity(Entity entity, Network.HistShowEntity showEntity)
	{
		foreach (Network.Entity.Tag tag in showEntity.Entity.Tags)
		{
			entity.SetTag(tag.Name, tag.Value);
		}
	}

	public static void ApplyHideEntity(Entity entity, Network.HistHideEntity hideEntity)
	{
		entity.SetTag(GAME_TAG.ZONE, hideEntity.Zone);
	}

	public static void ApplyTagChange(Entity entity, Network.HistTagChange tagChange)
	{
		entity.SetTag(tagChange.Tag, tagChange.Value);
	}

	public static TAG_ZONE GetFinalZoneForEntity(Entity entity)
	{
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		List<PowerTaskList> list = new List<PowerTaskList>();
		if (powerProcessor.GetCurrentTaskList() != null)
		{
			list.Add(powerProcessor.GetCurrentTaskList());
		}
		list.AddRange(powerProcessor.GetPowerQueue().GetList());
		for (int num = list.Count - 1; num >= 0; num--)
		{
			List<PowerTask> taskList = list[num].GetTaskList();
			for (int num2 = taskList.Count - 1; num2 >= 0; num2--)
			{
				Network.HistTagChange histTagChange = taskList[num2].GetPower() as Network.HistTagChange;
				if (histTagChange != null && histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 49)
				{
					return (TAG_ZONE)histTagChange.Value;
				}
			}
		}
		return entity.GetZone();
	}

	public static bool IsEntityHiddenAfterCurrentTasklist(Entity entity)
	{
		if (!entity.IsHidden())
		{
			return false;
		}
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		if (powerProcessor.GetCurrentTaskList() != null)
		{
			foreach (PowerTask task in powerProcessor.GetCurrentTaskList().GetTaskList())
			{
				Network.HistShowEntity histShowEntity = task.GetPower() as Network.HistShowEntity;
				if (histShowEntity != null && histShowEntity.Entity.ID == entity.GetEntityId() && !string.IsNullOrEmpty(histShowEntity.Entity.CardID))
				{
					return false;
				}
			}
		}
		return true;
	}

	public static bool IsGalakrond(string cardId)
	{
		switch (cardId)
		{
		case "DRG_600":
		case "DRG_600t2":
		case "DRG_600t3":
		case "DRG_650":
		case "DRG_650t2":
		case "DRG_650t3":
		case "DRG_620":
		case "DRG_620t2":
		case "DRG_620t3":
		case "DRG_660":
		case "DRG_660t2":
		case "DRG_660t3":
		case "DRG_610":
		case "DRG_610t2":
		case "DRG_610t3":
			return true;
		default:
			return false;
		}
	}

	public static bool IsGalakrondInPlay(Player player)
	{
		if (player == null)
		{
			return false;
		}
		Entity hero = player.GetHero();
		if (hero == null)
		{
			return false;
		}
		return IsGalakrond(hero.GetCardId());
	}

	public static void DoDamageTasks(PowerTaskList powerTaskList, Card sourceCard, Card targetCard)
	{
		List<PowerTask> taskList = powerTaskList.GetTaskList();
		if (taskList == null || taskList.Count == 0)
		{
			return;
		}
		int entityId = sourceCard.GetEntity().GetEntityId();
		int entityId2 = targetCard.GetEntity().GetEntityId();
		foreach (PowerTask item in taskList)
		{
			Network.PowerHistory power = item.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType != HistoryMeta.Type.DAMAGE && histMetaData.MetaType != HistoryMeta.Type.HEALING)
				{
					continue;
				}
				foreach (int item2 in histMetaData.Info)
				{
					if (item2 == entityId || item2 == entityId2)
					{
						item.DoTask();
					}
				}
			}
			else
			{
				if (power.Type != Network.PowerType.TAG_CHANGE)
				{
					continue;
				}
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Entity == entityId || histTagChange.Entity == entityId2)
				{
					GAME_TAG tag = (GAME_TAG)histTagChange.Tag;
					if (tag == GAME_TAG.DAMAGE || tag == GAME_TAG.EXHAUSTED)
					{
						item.DoTask();
					}
				}
			}
		}
	}

	public static AdventureDbfRecord GetAdventureRecordFromMissionId(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return null;
		}
		int adventureId = record.AdventureId;
		return GameDbf.Adventure.GetRecord(adventureId);
	}

	public static WingDbfRecord GetWingRecordFromMissionId(int missionId)
	{
		WingDbId wingIdFromMissionId = GetWingIdFromMissionId((ScenarioDbId)missionId);
		if (wingIdFromMissionId == WingDbId.INVALID)
		{
			return null;
		}
		return GameDbf.Wing.GetRecord((int)wingIdFromMissionId);
	}

	public static WingDbId GetWingIdFromMissionId(ScenarioDbId missionId)
	{
		return (WingDbId)(GameDbf.Scenario.GetRecord((int)missionId)?.WingId ?? 0);
	}

	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord record in GameDbf.AdventureData.GetRecords())
		{
			if (record.AdventureId == adventureId && record.ModeId == modeId)
			{
				return record;
			}
		}
		return null;
	}

	public static List<ScenarioDbfRecord> GetClassChallengeRecords(int adventureId, int wingId)
	{
		List<ScenarioDbfRecord> list = new List<ScenarioDbfRecord>();
		foreach (ScenarioDbfRecord record in GameDbf.Scenario.GetRecords())
		{
			if (record.ModeId == 4 && record.AdventureId == adventureId && record.WingId == wingId)
			{
				list.Add(record);
			}
		}
		return list;
	}

	public static TAG_CLASS GetClassChallengeHeroClass(ScenarioDbfRecord rec)
	{
		if (rec.ModeId != 4)
		{
			return TAG_CLASS.INVALID;
		}
		int player1HeroCardId = rec.Player1HeroCardId;
		return DefLoader.Get().GetEntityDef(player1HeroCardId)?.GetClass() ?? TAG_CLASS.INVALID;
	}

	public static List<TAG_CLASS> GetClassChallengeHeroClasses(int adventureId, int wingId)
	{
		List<ScenarioDbfRecord> classChallengeRecords = GetClassChallengeRecords(adventureId, wingId);
		List<TAG_CLASS> list = new List<TAG_CLASS>();
		foreach (ScenarioDbfRecord item in classChallengeRecords)
		{
			list.Add(GetClassChallengeHeroClass(item));
		}
		return list;
	}

	public static bool IsAIMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return false;
		}
		if (record.Players == 1)
		{
			return true;
		}
		return false;
	}

	public static bool IsCoopMission(int missionId)
	{
		return GameDbf.Scenario.GetRecord(missionId)?.IsCoop ?? false;
	}

	public static string GetMissionHeroCardId(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return null;
		}
		int num = record.ClientPlayer2HeroCardId;
		if (num == 0)
		{
			num = record.Player2HeroCardId;
		}
		return TranslateDbIdToCardId(num);
	}

	public static string GetMissionHeroName(int missionId)
	{
		string missionHeroCardId = GetMissionHeroCardId(missionId);
		if (missionHeroCardId == null)
		{
			return null;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(missionHeroCardId);
		if (entityDef == null)
		{
			Debug.LogError($"GameUtils.GetMissionHeroName() - hero {missionHeroCardId} for mission {missionId} has no EntityDef");
			return null;
		}
		return entityDef.GetName();
	}

	public static string GetMissionHeroPowerCardId(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return null;
		}
		int clientPlayer2HeroPowerCardId = record.ClientPlayer2HeroPowerCardId;
		if (clientPlayer2HeroPowerCardId != 0)
		{
			return TranslateDbIdToCardId(clientPlayer2HeroPowerCardId);
		}
		int num = record.ClientPlayer2HeroCardId;
		if (num == 0)
		{
			num = record.Player2HeroCardId;
		}
		return GetHeroPowerCardIdFromHero(num);
	}

	public static bool IsMissionForAdventure(int missionId, int adventureId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return false;
		}
		return adventureId == record.AdventureId;
	}

	public static bool IsTutorialMission(int missionId)
	{
		return IsMissionForAdventure(missionId, 1);
	}

	public static bool IsPracticeMission(int missionId)
	{
		return IsMissionForAdventure(missionId, 2);
	}

	public static bool IsDungeonCrawlMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return false;
		}
		return DoesAdventureModeUseDungeonCrawlFormat((AdventureModeDbId)record.ModeId);
	}

	public static bool DoesAdventureModeUseDungeonCrawlFormat(AdventureModeDbId modeId)
	{
		if (modeId != AdventureModeDbId.DUNGEON_CRAWL)
		{
			return modeId == AdventureModeDbId.DUNGEON_CRAWL_HEROIC;
		}
		return true;
	}

	public static bool IsBoosterLatestActiveExpansion(int boosterId)
	{
		return boosterId == (int)GetLatestRewardableBooster();
	}

	public static BoosterDbId GetLatestRewardableBooster()
	{
		return GetRewardableBoosterOffsetFromLatest(0);
	}

	public static BoosterDbId GetRewardableBoosterOffsetFromLatest(int offset)
	{
		List<BoosterDbfRecord> rewardableBoosters = GetRewardableBoosters();
		if (rewardableBoosters.Count <= 0)
		{
			Debug.LogError("No active Booster sets found");
			return BoosterDbId.INVALID;
		}
		offset = Mathf.Clamp(offset, 0, rewardableBoosters.Count - 1);
		return (BoosterDbId)rewardableBoosters[offset].ID;
	}

	public static BoosterDbId GetRewardableBoosterFromSelector(RewardItem.BoosterSelector selector)
	{
		switch (selector)
		{
		case RewardItem.BoosterSelector.LATEST:
			return GetRewardableBoosterOffsetFromLatest(0);
		case RewardItem.BoosterSelector.LATEST_OFFSET_BY_1:
			return GetRewardableBoosterOffsetFromLatest(1);
		case RewardItem.BoosterSelector.LATEST_OFFSET_BY_2:
			return GetRewardableBoosterOffsetFromLatest(2);
		case RewardItem.BoosterSelector.LATEST_OFFSET_BY_3:
			return GetRewardableBoosterOffsetFromLatest(3);
		default:
			Debug.LogError($"Unknown BoosterSelector {selector}");
			return BoosterDbId.INVALID;
		}
	}

	public static AdventureDbId GetLatestActiveAdventure()
	{
		return GameDbf.Adventure.GetRecords((AdventureDbfRecord r) => !AdventureConfig.IsAdventureComingSoon((AdventureDbId)r.ID) && AdventureConfig.IsAdventureEventActive((AdventureDbId)r.ID)).Max((AdventureDbfRecord r) => (AdventureDbId)r.ID);
	}

	public static bool IsExpansionMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return false;
		}
		int adventureId = record.AdventureId;
		if (adventureId == 0)
		{
			return false;
		}
		return IsExpansionAdventure((AdventureDbId)adventureId);
	}

	public static bool IsExpansionAdventure(AdventureDbId adventureId)
	{
		if ((uint)adventureId <= 2u || adventureId == AdventureDbId.TAVERN_BRAWL)
		{
			return false;
		}
		return true;
	}

	public static string GetAdventureProductStringKey(int wingID)
	{
		AdventureDbId adventureIdByWingId = GetAdventureIdByWingId(wingID);
		if (adventureIdByWingId != 0)
		{
			return GameDbf.Adventure.GetRecord((int)adventureIdByWingId).ProductStringKey;
		}
		return string.Empty;
	}

	public static AdventureDbId GetAdventureId(int missionId)
	{
		return (AdventureDbId)(GameDbf.Scenario.GetRecord(missionId)?.AdventureId ?? 0);
	}

	public static AdventureDbId GetAdventureIdByWingId(int wingID)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(wingID);
		if (record == null)
		{
			return AdventureDbId.INVALID;
		}
		AdventureDbId adventureId = (AdventureDbId)record.AdventureId;
		if (!IsExpansionAdventure(adventureId))
		{
			return AdventureDbId.INVALID;
		}
		return adventureId;
	}

	public static AdventureModeDbId GetAdventureModeId(int missionId)
	{
		return (AdventureModeDbId)(GameDbf.Scenario.GetRecord(missionId)?.ModeId ?? 0);
	}

	public static bool IsHeroicAdventureMission(int missionId)
	{
		return IsModeHeroic(GetAdventureModeId(missionId));
	}

	public static bool IsModeHeroic(AdventureModeDbId mode)
	{
		if (mode != AdventureModeDbId.LINEAR_HEROIC)
		{
			return mode == AdventureModeDbId.DUNGEON_CRAWL_HEROIC;
		}
		return true;
	}

	public static AdventureModeDbId GetNormalModeFromHeroicMode(AdventureModeDbId mode)
	{
		return mode switch
		{
			AdventureModeDbId.DUNGEON_CRAWL_HEROIC => AdventureModeDbId.DUNGEON_CRAWL, 
			AdventureModeDbId.LINEAR_HEROIC => AdventureModeDbId.LINEAR, 
			_ => mode, 
		};
	}

	public static bool IsClassChallengeMission(int missionId)
	{
		return GetAdventureModeId(missionId) == AdventureModeDbId.CLASS_CHALLENGE;
	}

	public static int GetSortedWingUnlockIndex(WingDbfRecord wingRecord)
	{
		List<WingDbfRecord> records = GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == wingRecord.AdventureId);
		bool wingsHaveSameUnlockOrder = false;
		records.Sort(delegate(WingDbfRecord l, WingDbfRecord r)
		{
			int num = l.UnlockOrder - r.UnlockOrder;
			if (num == 0 && l.ID != r.ID)
			{
				wingsHaveSameUnlockOrder = true;
			}
			return num;
		});
		if (wingsHaveSameUnlockOrder)
		{
			return 0;
		}
		return records.FindIndex((WingDbfRecord r) => r.ID == wingRecord.ID);
	}

	public static int GetNumWingsInAdventure(AdventureDbId adventureId)
	{
		return GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)adventureId).Count;
	}

	public static bool AreAllTutorialsComplete(TutorialProgress progress)
	{
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM)
		{
			return false;
		}
		return progress == TutorialProgress.ILLIDAN_COMPLETE;
	}

	public static bool AreAllTutorialsComplete(long campaignProgress)
	{
		return AreAllTutorialsComplete((TutorialProgress)campaignProgress);
	}

	public static bool AreAllTutorialsComplete()
	{
		NetCache.NetCacheProfileProgress value = s_profileProgress.Value;
		if (value == null)
		{
			return false;
		}
		if (!AreAllTutorialsComplete(value.CampaignProgress))
		{
			return false;
		}
		return true;
	}

	public static int GetNextTutorial(TutorialProgress progress)
	{
		return progress switch
		{
			TutorialProgress.NOTHING_COMPLETE => 3, 
			TutorialProgress.HOGGER_COMPLETE => 4, 
			TutorialProgress.MILLHOUSE_COMPLETE => 249, 
			TutorialProgress.CHO_COMPLETE => 181, 
			TutorialProgress.MUKLA_COMPLETE => 201, 
			TutorialProgress.NESINGWARY_COMPLETE => 248, 
			_ => 0, 
		};
	}

	public static int GetNextTutorial()
	{
		NetCache.NetCacheProfileProgress value = s_profileProgress.Value;
		if (value == null)
		{
			return GetNextTutorial(Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS));
		}
		return GetNextTutorial(value.CampaignProgress);
	}

	public static string GetTutorialCardRewardDetails(int missionId)
	{
		switch (missionId)
		{
		case 3:
			return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL01");
		case 4:
			return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL02");
		case 181:
			return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL03");
		case 201:
			return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL04");
		case 248:
			return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL05");
		case 249:
			return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL06");
		default:
			Debug.LogWarning($"GameUtils.GetTutorialCardRewardDetails(): no card reward details for mission {missionId}");
			return "";
		}
	}

	public static string GetCurrentTutorialCardRewardDetails()
	{
		return GetTutorialCardRewardDetails(GameMgr.Get().GetMissionId());
	}

	public static int MissionSortComparison(ScenarioDbfRecord rec1, ScenarioDbfRecord rec2)
	{
		return rec1.SortOrder - rec2.SortOrder;
	}

	public static List<ScenarioGuestHeroesDbfRecord> GetScenarioGuestHeroes(int scenarioId)
	{
		return GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == scenarioId);
	}

	public static int GetDefeatedBossCount()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return 0;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		if (!DungeonCrawlUtil.IsDungeonRunActive(gameSaveDataServerKey))
		{
			return 0;
		}
		List<long> values = null;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out values);
		return values?.Count ?? 0;
	}

	public static List<FixedRewardActionDbfRecord> GetFixedActionRecords(FixedRewardAction.Type actionType)
	{
		return GameDbf.GetIndex().GetFixedActionRecordsForType(actionType);
	}

	public static FixedRewardDbfRecord GetFixedRewardForCard(string cardID, TAG_PREMIUM premium)
	{
		int num = TranslateCardIdToDbId(cardID);
		foreach (FixedRewardDbfRecord record in GameDbf.FixedReward.GetRecords())
		{
			int cardId = record.CardId;
			if (num == cardId)
			{
				int cardPremium = record.CardPremium;
				if (premium == (TAG_PREMIUM)cardPremium)
				{
					return record;
				}
			}
		}
		return null;
	}

	public static List<FixedRewardMapDbfRecord> GetFixedRewardMapRecordsForAction(int actionID)
	{
		return GameDbf.GetIndex().GetFixedRewardMapRecordsForAction(actionID);
	}

	public static int GetFixedRewardCounterpartCardID(int cardID)
	{
		foreach (FixedRewardActionDbfRecord fixedActionRecord in GetFixedActionRecords(FixedRewardAction.Type.OWNS_COUNTERPART_CARD))
		{
			if (!SpecialEventManager.Get().IsEventActive(fixedActionRecord.ActiveEvent, activeIfDoesNotExist: false))
			{
				continue;
			}
			foreach (FixedRewardMapDbfRecord item in GetFixedRewardMapRecordsForAction(fixedActionRecord.ID))
			{
				FixedRewardDbfRecord record = GameDbf.FixedReward.GetRecord(item.RewardId);
				if (GetCardTagValue(record.CardId, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID) == cardID)
				{
					return record.CardId;
				}
			}
		}
		return 0;
	}

	public static string GetOwnedCounterpartCardIDForFormat(EntityDef cardDef, FormatType formatType, int minOwned)
	{
		string text = TranslateDbIdToCardId(cardDef.GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID));
		if (text != null)
		{
			return text;
		}
		TAG_CARD_SET[] cardSetsInFormat = GetCardSetsInFormat(formatType);
		CollectionManager collectionManager = CollectionManager.Get();
		int? minOwned2 = minOwned;
		foreach (CollectibleCard card in collectionManager.FindCards(null, null, null, cardSetsInFormat, null, null, null, null, null, minOwned2).m_cards)
		{
			if (TranslateDbIdToCardId(card.GetEntityDef().GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID)) == cardDef.GetCardId())
			{
				return card.CardId;
			}
		}
		return text;
	}

	public static bool IsMatchmadeGameType(GameType gameType, int? missionId = null)
	{
		switch (gameType)
		{
		case GameType.GT_PVPDR_PAID:
		case GameType.GT_PVPDR:
			if (missionId.HasValue && DungeonCrawlUtil.IsPVPDRFriendlyEncounter(missionId.Value))
			{
				return false;
			}
			return true;
		case GameType.GT_ARENA:
		case GameType.GT_RANKED:
		case GameType.GT_CASUAL:
		case GameType.GT_BATTLEGROUNDS:
			return true;
		case GameType.GT_VS_AI:
		case GameType.GT_VS_FRIEND:
		case GameType.GT_TUTORIAL:
		case GameType.GT_FSG_BRAWL_VS_FRIEND:
		case GameType.GT_FSG_BRAWL_1P_VS_AI:
		case GameType.GT_BATTLEGROUNDS_FRIENDLY:
			return false;
		default:
			if (IsTavernBrawlGameType(gameType))
			{
				int num = 0;
				if (missionId.HasValue)
				{
					num = missionId.Value;
				}
				else
				{
					TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
					if (tavernBrawlMission == null)
					{
						return true;
					}
					num = tavernBrawlMission.missionId;
				}
				if (IsAIMission(num))
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}

	public static bool IsTavernBrawlGameType(GameType gameType)
	{
		if ((uint)(gameType - 16) <= 6u)
		{
			return true;
		}
		return false;
	}

	public static bool IsFiresideGatheringGameType(GameType gameType)
	{
		if ((uint)(gameType - 19) <= 3u)
		{
			return true;
		}
		return false;
	}

	public static bool IsPvpDrGameType(GameType gameType)
	{
		if ((uint)(gameType - 28) <= 1u)
		{
			return true;
		}
		return false;
	}

	public static bool ShouldShowArenaModeIcon()
	{
		return GameMgr.Get().GetGameType() == GameType.GT_ARENA;
	}

	public static bool ShouldShowCasualModeIcon()
	{
		return GameMgr.Get().GetGameType() == GameType.GT_CASUAL;
	}

	public static bool ShouldShowFriendlyChallengeIcon()
	{
		if (GameMgr.Get().GetGameType() == GameType.GT_VS_FRIEND)
		{
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public static bool ShouldShowTavernBrawlModeIcon()
	{
		GameType gameType = GameMgr.Get().GetGameType();
		if (gameType == GameType.GT_VS_FRIEND && FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			return true;
		}
		if (IsTavernBrawlGameType(gameType))
		{
			return true;
		}
		return false;
	}

	public static bool ShouldShowAdventureModeIcon()
	{
		int missionId = GameMgr.Get().GetMissionId();
		AdventureDbId adventureId = GetAdventureId(missionId);
		if (IsExpansionMission(missionId) && adventureId != AdventureDbId.TAVERN_BRAWL && adventureId != AdventureDbId.PVPDR)
		{
			return !IsTavernBrawlGameType(GameMgr.Get().GetGameType());
		}
		return false;
	}

	public static bool ShouldShowPvpDrModeIcon()
	{
		return GetAdventureId(GameMgr.Get().GetMissionId()) == AdventureDbId.PVPDR;
	}

	public static bool IsGameTypeRanked()
	{
		return IsGameTypeRanked(GameMgr.Get().GetGameType());
	}

	public static bool IsGameTypeRanked(GameType gameType)
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			return false;
		}
		return gameType == GameType.GT_RANKED;
	}

	public static void RequestPlayerPresence(BnetGameAccountId gameAccountId)
	{
		EntityId entityId = default(EntityId);
		entityId.hi = gameAccountId.GetHi();
		entityId.lo = gameAccountId.GetLo();
		List<PresenceFieldKey> list = new List<PresenceFieldKey>();
		PresenceFieldKey item = default(PresenceFieldKey);
		item.programId = BnetProgramId.BNET.GetValue();
		item.groupId = 2u;
		item.fieldId = 7u;
		item.uniqueId = 0uL;
		list.Add(item);
		item.programId = BnetProgramId.BNET.GetValue();
		item.groupId = 2u;
		item.fieldId = 3u;
		item.uniqueId = 0uL;
		list.Add(item);
		item.programId = BnetProgramId.BNET.GetValue();
		item.groupId = 2u;
		item.fieldId = 5u;
		item.uniqueId = 0uL;
		list.Add(item);
		if (IsGameTypeRanked())
		{
			PresenceFieldKey item2 = default(PresenceFieldKey);
			item2.programId = BnetProgramId.HEARTHSTONE.GetValue();
			item2.groupId = 2u;
			item2.fieldId = 18u;
			item2.uniqueId = 0uL;
			list.Add(item2);
		}
		PresenceFieldKey[] fieldList = list.ToArray();
		BattleNet.RequestPresenceFields(isGameAccountEntityId: true, entityId, fieldList);
	}

	public static bool IsAIPlayer(BnetGameAccountId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return false;
		}
		return !gameAccountId.IsValid();
	}

	public static bool IsHumanPlayer(BnetGameAccountId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return false;
		}
		return gameAccountId.IsValid();
	}

	public static bool IsBnetPlayer(BnetGameAccountId gameAccountId)
	{
		if (!IsHumanPlayer(gameAccountId))
		{
			return false;
		}
		return Network.ShouldBeConnectedToAurora();
	}

	public static bool IsGuestPlayer(BnetGameAccountId gameAccountId)
	{
		if (!IsHumanPlayer(gameAccountId))
		{
			return false;
		}
		return !Network.ShouldBeConnectedToAurora();
	}

	public static bool IsAnyTransitionActive()
	{
		SceneMgr sceneMgr = SceneMgr.Get();
		if (sceneMgr != null)
		{
			if (sceneMgr.IsTransitionNowOrPending())
			{
				return true;
			}
			PegasusScene scene = sceneMgr.GetScene();
			if (scene != null && scene.IsTransitioning())
			{
				return true;
			}
		}
		Box box = Box.Get();
		if (box != null && box.IsTransitioningToSceneMode())
		{
			return true;
		}
		LoadingScreen loadingScreen = LoadingScreen.Get();
		if (loadingScreen != null && loadingScreen.IsTransitioning())
		{
			return true;
		}
		return false;
	}

	public static void LogoutConfirmation()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get(Network.ShouldBeConnectedToAurora() ? "GLOBAL_SWITCH_ACCOUNT" : "GLOBAL_LOGIN_CONFIRM_TITLE"),
			m_text = GameStrings.Get(Network.ShouldBeConnectedToAurora() ? "GLOBAL_LOGOUT_CONFIRM_MESSAGE" : "GLOBAL_LOGIN_CONFIRM_MESSAGE"),
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = OnLogoutConfirmationResponse
		};
		DialogManager.Get().ShowPopup(info);
	}

	private static void OnLogoutConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			TemporaryAccountManager.Get().UnselectTemporaryAccount();
			Logout();
		}
	}

	public static void Logout()
	{
		GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: true);
		if (Network.ShouldBeConnectedToAurora())
		{
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
		}
		HearthstoneApplication.Get().ResetAndForceLogin();
	}

	public static int GetBoosterCount()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>()?.GetTotalNumBoosters() ?? 0;
	}

	public static int GetBoosterCount(int boosterStackId)
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject == null)
		{
			return 0;
		}
		return netObject.GetBoosterStack(boosterStackId)?.Count ?? 0;
	}

	public static bool HaveBoosters()
	{
		return GetBoosterCount() > 0;
	}

	public static PackOpeningRarity GetPackOpeningRarity(TAG_RARITY tag)
	{
		return tag switch
		{
			TAG_RARITY.COMMON => PackOpeningRarity.COMMON, 
			TAG_RARITY.FREE => PackOpeningRarity.COMMON, 
			TAG_RARITY.RARE => PackOpeningRarity.RARE, 
			TAG_RARITY.EPIC => PackOpeningRarity.EPIC, 
			TAG_RARITY.LEGENDARY => PackOpeningRarity.LEGENDARY, 
			_ => PackOpeningRarity.NONE, 
		};
	}

	public static List<BoosterDbfRecord> GetPackRecordsWithStorePrefab()
	{
		return GameDbf.Booster.GetRecords((BoosterDbfRecord r) => !string.IsNullOrEmpty(r.StorePrefab));
	}

	public static List<AdventureDbfRecord> GetSortedAdventureRecordsWithStorePrefab()
	{
		List<AdventureDbfRecord> records = GameDbf.Adventure.GetRecords((AdventureDbfRecord r) => !string.IsNullOrEmpty(r.StorePrefab));
		records.Sort((AdventureDbfRecord l, AdventureDbfRecord r) => r.SortOrder - l.SortOrder);
		return records;
	}

	public static List<AdventureDbfRecord> GetAdventureRecordsWithDefPrefab()
	{
		return GameDbf.Adventure.GetRecords((AdventureDbfRecord r) => !string.IsNullOrEmpty(r.AdventureDefPrefab));
	}

	public static List<AdventureDataDbfRecord> GetAdventureDataRecordsWithSubDefPrefab()
	{
		return GameDbf.AdventureData.GetRecords((AdventureDataDbfRecord r) => !string.IsNullOrEmpty(r.AdventureSubDefPrefab));
	}

	public static int PackSortingPredicate(BoosterDbfRecord left, BoosterDbfRecord right)
	{
		if (right.ListDisplayOrderCategory != left.ListDisplayOrderCategory)
		{
			return Mathf.Clamp(right.ListDisplayOrderCategory - left.ListDisplayOrderCategory, -1, 1);
		}
		return Mathf.Clamp(right.ListDisplayOrder - left.ListDisplayOrder, -1, 1);
	}

	public static IEnumerable<int> GetSortedPackIds(bool ascending = true)
	{
		List<BoosterDbfRecord> records = GameDbf.Booster.GetRecords();
		if (ascending)
		{
			records.Sort((BoosterDbfRecord l, BoosterDbfRecord r) => PackSortingPredicate(r, l));
		}
		else
		{
			records.Sort((BoosterDbfRecord l, BoosterDbfRecord r) => PackSortingPredicate(l, r));
		}
		return records.Select((BoosterDbfRecord b) => b.ID);
	}

	public static bool IsFakePackOpeningEnabled()
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		return Options.Get().GetBool(Option.FAKE_PACK_OPENING);
	}

	public static int GetFakePackCount()
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return 0;
		}
		return Options.Get().GetInt(Option.FAKE_PACK_COUNT);
	}

	public static bool IsFirstPurchaseBundleBooster(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return 181 == storePackId.Id;
		}
		return false;
	}

	public static bool IsMammothBundleBooster(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return 41 == storePackId.Id;
		}
		return false;
	}

	public static bool IsLimitedTimeOffer(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			int productDataFromStorePackId = GetProductDataFromStorePackId(storePackId);
			if (productDataFromStorePackId != 0)
			{
				Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, requireRealMoneyOption: false, productDataFromStorePackId).FirstOrDefault();
				if (bundle != null && !string.IsNullOrEmpty(bundle.ProductEvent))
				{
					SpecialEventType eventType = SpecialEventManager.GetEventType(bundle.ProductEvent);
					if (eventType != SpecialEventType.UNKNOWN)
					{
						DateTime? eventEndTimeUtc = SpecialEventManager.Get().GetEventEndTimeUtc(eventType);
						if (eventEndTimeUtc.HasValue && eventEndTimeUtc.Value.Subtract(DateTime.UtcNow).TotalDays < 365.0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	public static bool IsHiddenLicenseBundleBooster(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			BoosterDbId id = (BoosterDbId)storePackId.Id;
			if (id == BoosterDbId.MAMMOTH_BUNDLE || id == BoosterDbId.FIRST_PURCHASE)
			{
				return true;
			}
			return false;
		}
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			return true;
		}
		return false;
	}

	public static int GetProductDataFromStorePackId(StorePackId storePackId, int selectedIndex = 0)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			if (storePackId.Id == 181)
			{
				return 40;
			}
			if (storePackId.Id == 41)
			{
				return 27;
			}
			return storePackId.Id;
		}
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(storePackId.Id);
			if (selectedIndex >= regionNodeLayoutsForBundle.Count)
			{
				Log.Store.PrintWarning($"Selected invalid layout at index={selectedIndex}. Defaulting to layout at index=0.");
				selectedIndex = 0;
			}
			return regionNodeLayoutsForBundle[selectedIndex].HiddenLicenseId;
		}
		return 0;
	}

	public static int GetProductDataCountFromStorePackId(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			return 1;
		}
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			return StoreManager.Get().GetRegionNodeLayoutsForBundle(storePackId.Id).Count;
		}
		return 0;
	}

	public static List<BoosterDbfRecord> GetRewardableBoosters()
	{
		return (from r in GameDbf.Booster.GetRecords()
			where !IsBoosterRotated((BoosterDbId)r.ID, DateTime.UtcNow)
			where SpecialEventManager.Get().IsEventActive(r.RewardableEvent, activeIfDoesNotExist: false, DateTime.UtcNow)
			orderby r.LatestExpansionOrder descending
			select r).ToList();
	}

	public static int GetBoardIdFromAssetName(string name)
	{
		foreach (BoardDbfRecord record in GameDbf.Board.GetRecords())
		{
			string prefab = record.Prefab;
			if (!(name != prefab))
			{
				return record.ID;
			}
		}
		return 0;
	}

	public static UnityEngine.Object Instantiate(GameObject original, GameObject parent, bool withRotation = false)
	{
		if (original == null)
		{
			return null;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(original);
		SetParent(gameObject, parent, withRotation);
		return gameObject;
	}

	public static UnityEngine.Object Instantiate(Component original, GameObject parent, bool withRotation = false)
	{
		if (original == null)
		{
			return null;
		}
		Component component = UnityEngine.Object.Instantiate(original);
		SetParent(component, parent, withRotation);
		return component;
	}

	public static UnityEngine.Object Instantiate(UnityEngine.Object original)
	{
		if (original == null)
		{
			return null;
		}
		return UnityEngine.Object.Instantiate(original);
	}

	public static UnityEngine.Object InstantiateGameObject(string path, GameObject parent = null, bool withRotation = false)
	{
		if (path == null)
		{
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(path);
		if (parent != null)
		{
			SetParent(gameObject, parent, withRotation);
		}
		return gameObject;
	}

	public static void SetParent(Component child, Component parent, bool withRotation = false)
	{
		SetParent(child.transform, parent.transform, withRotation);
	}

	public static void SetParent(GameObject child, Component parent, bool withRotation = false)
	{
		SetParent(child.transform, parent.transform, withRotation);
	}

	public static void SetParent(Component child, GameObject parent, bool withRotation = false)
	{
		SetParent(child.transform, parent.transform, withRotation);
	}

	public static void SetParent(GameObject child, GameObject parent, bool withRotation = false)
	{
		SetParent(child.transform, parent.transform, withRotation);
	}

	private static void SetParent(Transform child, Transform parent, bool withRotation)
	{
		Vector3 localScale = child.localScale;
		Quaternion localRotation = child.localRotation;
		child.parent = parent;
		child.localPosition = Vector3.zero;
		child.localScale = localScale;
		if (withRotation)
		{
			child.localRotation = localRotation;
		}
	}

	public static void ResetTransform(GameObject obj)
	{
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		obj.transform.localRotation = Quaternion.identity;
	}

	public static void ResetTransform(Component comp)
	{
		ResetTransform(comp.gameObject);
	}

	public static T LoadGameObjectWithComponent<T>(string assetPath) where T : Component
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetPath);
		if (gameObject == null)
		{
			return null;
		}
		T component = gameObject.GetComponent<T>();
		if ((UnityEngine.Object)component == (UnityEngine.Object)null)
		{
			Debug.LogError($"{assetPath} object does not contain {typeof(T)} component.");
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		return component;
	}

	public static T FindChildByName<T>(Transform transform, string name) where T : Component
	{
		foreach (Transform item in transform)
		{
			if (item.name == name)
			{
				return item.GetComponent<T>();
			}
			T val = FindChildByName<T>(item, name);
			if ((UnityEngine.Object)val != (UnityEngine.Object)null)
			{
				return val;
			}
		}
		return null;
	}

	public static void PlayCardEffectDefSounds(CardEffectDef cardEffectDef)
	{
		if (cardEffectDef == null)
		{
			return;
		}
		foreach (string soundSpellPath in cardEffectDef.m_SoundSpellPaths)
		{
			AssetLoader.Get().InstantiatePrefab(soundSpellPath, delegate(AssetReference name, GameObject go, object data)
			{
				if (go == null)
				{
					Debug.LogError($"Unable to load spell object: {name}");
				}
				else
				{
					GameObject destroyObj = go;
					CardSoundSpell component = go.GetComponent<CardSoundSpell>();
					if (component == null)
					{
						Debug.LogError($"Card sound spell component not found: {name}");
						UnityEngine.Object.Destroy(destroyObj);
					}
					else
					{
						component.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
						{
							if (spell.GetActiveState() == SpellStateType.NONE)
							{
								UnityEngine.Object.Destroy(destroyObj);
							}
						});
						component.ForceDefaultAudioSource();
						component.Activate();
					}
				}
			});
		}
	}

	public static bool LoadCardDefEmoteSound(List<EmoteEntryDef> emoteDefs, EmoteType type, EmoteSoundLoaded callback)
	{
		if (callback == null)
		{
			Debug.LogError("No callback provided for LoadEmote!");
			return false;
		}
		if (emoteDefs == null)
		{
			return false;
		}
		EmoteEntryDef emoteEntryDef = emoteDefs.Find((EmoteEntryDef e) => e.m_emoteType == type);
		if (emoteEntryDef == null)
		{
			return false;
		}
		AssetLoader.Get().InstantiatePrefab(emoteEntryDef.m_emoteSoundSpellPath, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (go == null)
			{
				callback(null);
			}
			else
			{
				callback(go.GetComponent<CardSoundSpell>());
			}
		});
		return true;
	}

	public static bool LoadAndPositionCardActor(string actorName, string heroCardID, TAG_PREMIUM premium, LoadActorCallback callback)
	{
		if (!string.IsNullOrEmpty(heroCardID))
		{
			DefLoader.Get().LoadFullDef(heroCardID, delegate(string cardID, DefLoader.DisposableFullDef def, object userData)
			{
				LoadAndPositionCardActor_OnFullDefLoaded(actorName, cardID, def, userData, callback);
			}, premium);
			return true;
		}
		return false;
	}

	private static void LoadAndPositionCardActor_OnFullDefLoaded(string actorName, string cardID, DefLoader.DisposableFullDef def, object userData, LoadActorCallback callback)
	{
		TAG_PREMIUM premium = (TAG_PREMIUM)userData;
		LoadActorCallbackInfo callbackData2 = new LoadActorCallbackInfo
		{
			fullDef = def,
			premium = premium
		};
		AssetLoader.Get().InstantiatePrefab(actorName, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			LoadAndPositionActorCard_OnActorLoaded(assetRef, go, callbackData, callback);
		}, callbackData2, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private static void LoadAndPositionActorCard_OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData, LoadActorCallback callback)
	{
		LoadActorCallbackInfo loadActorCallbackInfo = callbackData as LoadActorCallbackInfo;
		using (loadActorCallbackInfo.fullDef)
		{
			if (go == null)
			{
				Debug.LogWarning($"GameUtils.OnHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
				return;
			}
			Actor component = go.GetComponent<Actor>();
			if (component == null)
			{
				Debug.LogWarning($"GameUtils.OnActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
				return;
			}
			component.SetPremium(loadActorCallbackInfo.premium);
			component.SetEntityDef(loadActorCallbackInfo.fullDef.EntityDef);
			component.SetCardDef(loadActorCallbackInfo.fullDef.DisposableCardDef);
			component.UpdateAllComponents();
			component.gameObject.name = loadActorCallbackInfo.fullDef.CardDef.name + "_actor";
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
			GemObject healthObject = component.GetHealthObject();
			if (healthObject != null)
			{
				healthObject.Hide();
			}
			callback?.Invoke(component);
		}
	}

	public static bool AtPrereleaseEvent()
	{
		return FiresideGatheringManager.Get().IsPrerelease;
	}

	public static bool IsBoosterWild(BoosterDbId boosterId)
	{
		if (boosterId == BoosterDbId.INVALID)
		{
			return false;
		}
		return IsBoosterWild(GameDbf.Booster.GetRecord((int)boosterId));
	}

	public static bool IsBoosterWild(BoosterDbfRecord boosterRecord)
	{
		if (boosterRecord != null)
		{
			SpecialEventType eventType = SpecialEventManager.GetEventType(boosterRecord.StandardEvent);
			if (eventType != SpecialEventType.UNKNOWN && eventType != 0 && SpecialEventManager.Get().HasEventEnded(eventType))
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsAdventureWild(AdventureDbId adventureId)
	{
		if (adventureId == AdventureDbId.INVALID)
		{
			return false;
		}
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
		if (record != null)
		{
			SpecialEventType eventType = SpecialEventManager.GetEventType(record.StandardEvent);
			if (eventType != SpecialEventType.UNKNOWN && eventType != 0 && SpecialEventManager.Get().HasEventEnded(eventType))
			{
				return true;
			}
		}
		return false;
	}
}
