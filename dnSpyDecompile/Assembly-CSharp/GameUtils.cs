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

// Token: 0x020009BA RID: 2490
public class GameUtils
{
	// Token: 0x0600875A RID: 34650 RVA: 0x002BADFC File Offset: 0x002B8FFC
	public static string TranslateDbIdToCardId(int dbId, bool showWarning = false)
	{
		CardDbfRecord record = GameDbf.Card.GetRecord(dbId);
		if (record == null)
		{
			if (showWarning)
			{
				global::Log.All.PrintError("GameUtils.TranslateDbIdToCardId() - Failed to find card with database id {0} in the Card DBF.", new object[]
				{
					dbId
				});
			}
			return null;
		}
		string noteMiniGuid = record.NoteMiniGuid;
		if (noteMiniGuid == null)
		{
			if (showWarning)
			{
				global::Log.All.PrintError("GameUtils.TranslateDbIdToCardId() - Card with database id {0} has no NOTE_MINI_GUID field in the Card DBF.", new object[]
				{
					dbId
				});
			}
			return null;
		}
		return noteMiniGuid;
	}

	// Token: 0x0600875B RID: 34651 RVA: 0x002BAE6C File Offset: 0x002B906C
	public static int TranslateCardIdToDbId(string cardId, bool showWarning = false)
	{
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
		if (cardRecord == null)
		{
			if (showWarning)
			{
				global::Log.All.PrintError("GameUtils.TranslateCardIdToDbId() - There is no card with NOTE_MINI_GUID {0} in the Card DBF.", new object[]
				{
					cardId
				});
			}
			return 0;
		}
		return cardRecord.ID;
	}

	// Token: 0x0600875C RID: 34652 RVA: 0x002BAEA7 File Offset: 0x002B90A7
	public static bool IsCardCollectible(string cardId)
	{
		return GameUtils.GetCardTagValue(cardId, GAME_TAG.COLLECTIBLE) == 1;
	}

	// Token: 0x0600875D RID: 34653 RVA: 0x002BAEB7 File Offset: 0x002B90B7
	public static bool IsCardInBattlegroundsPool(string cardId)
	{
		return GameUtils.GetCardTagValue(cardId, GAME_TAG.IS_BACON_POOL_MINION) == 1 || GameUtils.GetCardTagValue(cardId, GAME_TAG.BACON_HERO_CAN_BE_DRAFTED) == 1;
	}

	// Token: 0x0600875E RID: 34654 RVA: 0x002BAED7 File Offset: 0x002B90D7
	public static bool IsAdventureRotated(AdventureDbId adventureID)
	{
		return GameUtils.IsAdventureRotated(adventureID, DateTime.UtcNow);
	}

	// Token: 0x0600875F RID: 34655 RVA: 0x002BAEE4 File Offset: 0x002B90E4
	public static bool IsAdventureRotated(AdventureDbId adventureID, DateTime utcTimestamp)
	{
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureID);
		return record != null && !SpecialEventManager.Get().IsEventActive(record.StandardEvent, false, utcTimestamp);
	}

	// Token: 0x06008760 RID: 34656 RVA: 0x002BAF18 File Offset: 0x002B9118
	public static bool IsBoosterRotated(BoosterDbId boosterID, DateTime utcTimestamp)
	{
		BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)boosterID);
		return record != null && !SpecialEventManager.Get().IsEventActive(record.StandardEvent, false, utcTimestamp);
	}

	// Token: 0x06008761 RID: 34657 RVA: 0x002BAF4B File Offset: 0x002B914B
	public static FormatType GetCardSetFormat(TAG_CARD_SET cardSet)
	{
		if (cardSet == TAG_CARD_SET.VANILLA)
		{
			return FormatType.FT_CLASSIC;
		}
		if (GameUtils.IsSetRotated(cardSet))
		{
			return FormatType.FT_WILD;
		}
		return FormatType.FT_STANDARD;
	}

	// Token: 0x06008762 RID: 34658 RVA: 0x002BAF64 File Offset: 0x002B9164
	public static TAG_CARD_SET[] GetCardSetsInFormat(FormatType formatType)
	{
		TAG_CARD_SET[] result = null;
		switch (formatType)
		{
		case FormatType.FT_WILD:
			result = GameUtils.GetAllWildPlayableSets();
			break;
		case FormatType.FT_STANDARD:
			result = GameUtils.GetStandardSets();
			break;
		case FormatType.FT_CLASSIC:
			result = GameUtils.GetClassicSets();
			break;
		}
		return result;
	}

	// Token: 0x06008763 RID: 34659 RVA: 0x002BAFA0 File Offset: 0x002B91A0
	public static bool IsCardSetValidForFormat(FormatType formatType, TAG_CARD_SET cardSet)
	{
		switch (formatType)
		{
		case FormatType.FT_WILD:
			return GameUtils.IsWildCardSet(cardSet) || GameUtils.IsStandardCardSet(cardSet);
		case FormatType.FT_STANDARD:
			return GameUtils.IsStandardCardSet(cardSet);
		case FormatType.FT_CLASSIC:
			return GameUtils.IsClassicCardSet(cardSet);
		default:
			return false;
		}
	}

	// Token: 0x06008764 RID: 34660 RVA: 0x002BAFD8 File Offset: 0x002B91D8
	public static bool IsCardValidForFormat(FormatType formatType, int cardDbId)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardDbId, true);
		return GameUtils.IsCardValidForFormat(formatType, entityDef);
	}

	// Token: 0x06008765 RID: 34661 RVA: 0x002BAFFC File Offset: 0x002B91FC
	public static bool IsCardValidForFormat(FormatType formatType, string cardId)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		return GameUtils.IsCardValidForFormat(formatType, entityDef);
	}

	// Token: 0x06008766 RID: 34662 RVA: 0x002BB01C File Offset: 0x002B921C
	public static bool IsCardValidForFormat(FormatType formatType, EntityDef def)
	{
		return def != null && GameUtils.IsCardSetValidForFormat(formatType, def.GetCardSet());
	}

	// Token: 0x06008767 RID: 34663 RVA: 0x002BB02F File Offset: 0x002B922F
	public static bool IsWildCardSet(TAG_CARD_SET cardSet)
	{
		return GameUtils.GetCardSetFormat(cardSet) == FormatType.FT_WILD;
	}

	// Token: 0x06008768 RID: 34664 RVA: 0x002BB03A File Offset: 0x002B923A
	public static bool IsWildCard(int cardDbId)
	{
		return GameUtils.IsWildCard(DefLoader.Get().GetEntityDef(cardDbId, true));
	}

	// Token: 0x06008769 RID: 34665 RVA: 0x002BB04D File Offset: 0x002B924D
	public static bool IsWildCard(string cardId)
	{
		return GameUtils.IsWildCard(DefLoader.Get().GetEntityDef(cardId));
	}

	// Token: 0x0600876A RID: 34666 RVA: 0x002BB05F File Offset: 0x002B925F
	public static bool IsWildCard(EntityDef def)
	{
		return def != null && GameUtils.IsWildCardSet(def.GetCardSet());
	}

	// Token: 0x0600876B RID: 34667 RVA: 0x002BB071 File Offset: 0x002B9271
	public static bool IsClassicCardSet(TAG_CARD_SET cardSet)
	{
		return GameUtils.GetCardSetFormat(cardSet) == FormatType.FT_CLASSIC;
	}

	// Token: 0x0600876C RID: 34668 RVA: 0x002BB07C File Offset: 0x002B927C
	public static bool IsClassicCard(int cardDbId)
	{
		return GameUtils.IsClassicCard(DefLoader.Get().GetEntityDef(cardDbId, true));
	}

	// Token: 0x0600876D RID: 34669 RVA: 0x002BB08F File Offset: 0x002B928F
	public static bool IsClassicCard(string cardId)
	{
		return GameUtils.IsClassicCard(DefLoader.Get().GetEntityDef(cardId));
	}

	// Token: 0x0600876E RID: 34670 RVA: 0x002BB0A1 File Offset: 0x002B92A1
	public static bool IsClassicCard(EntityDef def)
	{
		return def != null && GameUtils.IsClassicCardSet(def.GetCardSet());
	}

	// Token: 0x0600876F RID: 34671 RVA: 0x002BB0B3 File Offset: 0x002B92B3
	public static bool IsCoreCard(string cardId)
	{
		return GameUtils.IsCoreCard(DefLoader.Get().GetEntityDef(cardId));
	}

	// Token: 0x06008770 RID: 34672 RVA: 0x002BB0C5 File Offset: 0x002B92C5
	public static bool IsCoreCard(EntityDef def)
	{
		return def != null && def.IsCoreCard();
	}

	// Token: 0x06008771 RID: 34673 RVA: 0x002BB0D2 File Offset: 0x002B92D2
	public static bool IsStandardCardSet(TAG_CARD_SET cardSet)
	{
		return GameUtils.GetCardSetFormat(cardSet) == FormatType.FT_STANDARD;
	}

	// Token: 0x06008772 RID: 34674 RVA: 0x002BB0DD File Offset: 0x002B92DD
	public static bool IsStandardCard(int cardDbId)
	{
		return GameUtils.IsStandardCard(DefLoader.Get().GetEntityDef(cardDbId, true));
	}

	// Token: 0x06008773 RID: 34675 RVA: 0x002BB0F0 File Offset: 0x002B92F0
	public static bool IsStandardCard(string cardId)
	{
		return GameUtils.IsStandardCard(DefLoader.Get().GetEntityDef(cardId));
	}

	// Token: 0x06008774 RID: 34676 RVA: 0x002BB102 File Offset: 0x002B9302
	public static bool IsStandardCard(EntityDef def)
	{
		return def != null && GameUtils.IsStandardCardSet(def.GetCardSet());
	}

	// Token: 0x06008775 RID: 34677 RVA: 0x002BB114 File Offset: 0x002B9314
	public static string GetCardSetFormatAsString(TAG_CARD_SET cardSet)
	{
		return GameUtils.GetCardSetFormat(cardSet).ToString().Replace("FT_", "");
	}

	// Token: 0x06008776 RID: 34678 RVA: 0x002BB144 File Offset: 0x002B9344
	public static bool IsSetRotated(TAG_CARD_SET set)
	{
		return GameUtils.IsSetRotated(set, DateTime.UtcNow);
	}

	// Token: 0x06008777 RID: 34679 RVA: 0x002BB154 File Offset: 0x002B9354
	public static bool IsSetRotated(TAG_CARD_SET set, DateTime utcTimestamp)
	{
		CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(set);
		return cardSet != null && !SpecialEventManager.Get().IsEventActive(cardSet.StandardEvent, false, utcTimestamp) && SpecialEventManager.Get().HasEventStarted(cardSet.StandardEvent);
	}

	// Token: 0x06008778 RID: 34680 RVA: 0x002BB198 File Offset: 0x002B9398
	public static bool IsCardRotated(int cardDbId)
	{
		return GameUtils.IsCardRotated(DefLoader.Get().GetEntityDef(cardDbId, true));
	}

	// Token: 0x06008779 RID: 34681 RVA: 0x002BB1AB File Offset: 0x002B93AB
	public static bool IsCardRotated(string cardId)
	{
		return GameUtils.IsCardRotated(DefLoader.Get().GetEntityDef(cardId));
	}

	// Token: 0x0600877A RID: 34682 RVA: 0x002BB1BD File Offset: 0x002B93BD
	public static bool IsCardRotated(EntityDef def)
	{
		return GameUtils.IsCardRotated(def, DateTime.UtcNow);
	}

	// Token: 0x0600877B RID: 34683 RVA: 0x002BB1CA File Offset: 0x002B93CA
	public static bool IsCardRotated(EntityDef def, DateTime utcTimestamp)
	{
		return GameUtils.IsSetRotated(def.GetCardSet(), utcTimestamp);
	}

	// Token: 0x0600877C RID: 34684 RVA: 0x002BB1D8 File Offset: 0x002B93D8
	public static bool IsLegacySet(TAG_CARD_SET set)
	{
		return GameUtils.IsLegacySet(set, DateTime.UtcNow);
	}

	// Token: 0x0600877D RID: 34685 RVA: 0x002BB1E8 File Offset: 0x002B93E8
	public static bool IsLegacySet(TAG_CARD_SET set, DateTime utcTimestamp)
	{
		CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(set);
		return cardSet != null && SpecialEventManager.Get().IsEventActive(cardSet.LegacyCardSetEvent, false, utcTimestamp);
	}

	// Token: 0x0600877E RID: 34686 RVA: 0x002BB218 File Offset: 0x002B9418
	public static bool IsCardGameplayEventActive(EntityDef def)
	{
		return GameUtils.IsCardGameplayEventActive(def.GetCardId());
	}

	// Token: 0x0600877F RID: 34687 RVA: 0x002BB228 File Offset: 0x002B9428
	public static bool IsCardGameplayEventActive(string cardId)
	{
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
		if (cardRecord == null)
		{
			Debug.LogWarning(string.Format("GameUtils.IsCardGameplayEventActive could not find DBF record for card {0}", cardId));
			return false;
		}
		return SpecialEventManager.Get().IsEventActive(cardRecord.GameplayEvent, true);
	}

	// Token: 0x06008780 RID: 34688 RVA: 0x002BB264 File Offset: 0x002B9464
	public static bool IsCardCraftableWhenWild(string cardId)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		if (entityDef == null)
		{
			return false;
		}
		CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(entityDef.GetCardSet());
		return cardSet != null && cardSet.CraftableWhenWild;
	}

	// Token: 0x06008781 RID: 34689 RVA: 0x002BB2A0 File Offset: 0x002B94A0
	public static bool DeckIncludesRotatedCards(int deckId)
	{
		DeckDbfRecord record = GameDbf.Deck.GetRecord(deckId);
		if (record == null)
		{
			global::Log.Decks.PrintWarning("DeckRuleset.IsDeckWild(): {0} is invalid deck id", new object[]
			{
				deckId
			});
			return false;
		}
		using (List<DeckCardDbfRecord>.Enumerator enumerator = record.Cards.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (GameUtils.IsCardRotated(enumerator.Current.CardId))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06008782 RID: 34690 RVA: 0x002BB330 File Offset: 0x002B9530
	public static TAG_CARD_SET[] GetStandardSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		foreach (TAG_CARD_SET tag_CARD_SET in CollectionManager.Get().GetDisplayableCardSets())
		{
			if (GameUtils.GetCardSetFormat(tag_CARD_SET) == FormatType.FT_STANDARD)
			{
				list.Add(tag_CARD_SET);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06008783 RID: 34691 RVA: 0x002BB39C File Offset: 0x002B959C
	public static TAG_CARD_SET[] GetWildSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		foreach (TAG_CARD_SET tag_CARD_SET in CollectionManager.Get().GetDisplayableCardSets())
		{
			if (GameUtils.GetCardSetFormat(tag_CARD_SET) == FormatType.FT_WILD)
			{
				list.Add(tag_CARD_SET);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06008784 RID: 34692 RVA: 0x002BB408 File Offset: 0x002B9608
	public static TAG_CARD_SET[] GetAllWildPlayableSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		list.AddRange(GameUtils.GetStandardSets());
		list.AddRange(GameUtils.GetWildSets());
		return list.ToArray();
	}

	// Token: 0x06008785 RID: 34693 RVA: 0x002BB42C File Offset: 0x002B962C
	public static TAG_CARD_SET[] GetLegacySets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		foreach (TAG_CARD_SET tag_CARD_SET in CollectionManager.Get().GetDisplayableCardSets())
		{
			if (GameUtils.IsLegacySet(tag_CARD_SET))
			{
				list.Add(tag_CARD_SET);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06008786 RID: 34694 RVA: 0x002BB498 File Offset: 0x002B9698
	public static TAG_CARD_SET[] GetClassicSets()
	{
		return (from cardSet in CollectionManager.Get().GetDisplayableCardSets()
		where GameUtils.IsClassicCardSet(cardSet)
		select cardSet).ToArray<TAG_CARD_SET>();
	}

	// Token: 0x06008787 RID: 34695 RVA: 0x002BB4CD File Offset: 0x002B96CD
	public static TAG_CLASS GetTagClassFromCardDbId(int cardDbId)
	{
		return (TAG_CLASS)GameDbf.GetIndex().GetCardTagValue(cardDbId, GAME_TAG.CLASS);
	}

	// Token: 0x06008788 RID: 34696 RVA: 0x002BB4DF File Offset: 0x002B96DF
	public static int CountAllCollectibleCards()
	{
		return GameDbf.GetIndex().GetCollectibleCardCount();
	}

	// Token: 0x06008789 RID: 34697 RVA: 0x002BB4EB File Offset: 0x002B96EB
	public static List<string> GetAllCardIds()
	{
		return GameDbf.GetIndex().GetAllCardIds();
	}

	// Token: 0x0600878A RID: 34698 RVA: 0x002BB4F7 File Offset: 0x002B96F7
	public static List<string> GetAllCollectibleCardIds()
	{
		return GameDbf.GetIndex().GetCollectibleCardIds();
	}

	// Token: 0x0600878B RID: 34699 RVA: 0x002BB503 File Offset: 0x002B9703
	public static List<int> GetAllCollectibleCardDbIds()
	{
		return GameDbf.GetIndex().GetCollectibleCardDbIds();
	}

	// Token: 0x0600878C RID: 34700 RVA: 0x002BB510 File Offset: 0x002B9710
	public static List<string> GetNonHeroSkinCollectibleCardIds()
	{
		List<string> list = new List<string>();
		foreach (string text in GameUtils.GetAllCollectibleCardIds())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
			if (entityDef != null && !entityDef.IsHeroSkin())
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x0600878D RID: 34701 RVA: 0x002BB580 File Offset: 0x002B9780
	public static List<string> GetNonHeroSkinAllCardIds()
	{
		List<string> list = new List<string>();
		foreach (string text in GameUtils.GetAllCardIds())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
			if (entityDef != null && !entityDef.IsHeroSkin() && entityDef.GetCardType() != TAG_CARDTYPE.ENCHANTMENT)
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x0600878E RID: 34702 RVA: 0x002BB5FC File Offset: 0x002B97FC
	public static CardDbfRecord GetCardRecord(string cardId)
	{
		if (cardId == null)
		{
			return null;
		}
		return GameDbf.GetIndex().GetCardRecord(cardId);
	}

	// Token: 0x0600878F RID: 34703 RVA: 0x002BB610 File Offset: 0x002B9810
	public static List<CardChangeDbfRecord> GetCardChangeRecords(string cardId)
	{
		if (cardId == null)
		{
			return null;
		}
		int cardId2 = GameUtils.TranslateCardIdToDbId(cardId, false);
		return GameDbf.GetIndex().GetCardChangeRecords(cardId2);
	}

	// Token: 0x06008790 RID: 34704 RVA: 0x002BB638 File Offset: 0x002B9838
	public static int GetCardTagValue(string cardId, GAME_TAG tagId)
	{
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardId, false);
		return GameDbf.GetIndex().GetCardTagValue(cardDbId, tagId);
	}

	// Token: 0x06008791 RID: 34705 RVA: 0x002BB659 File Offset: 0x002B9859
	public static int GetCardTagValue(int cardDbId, GAME_TAG tagId)
	{
		return GameDbf.GetIndex().GetCardTagValue(cardDbId, tagId);
	}

	// Token: 0x06008792 RID: 34706 RVA: 0x002BB668 File Offset: 0x002B9868
	public static IEnumerable<CardTagDbfRecord> GetCardTagRecords(string cardId)
	{
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardId, false);
		return GameDbf.GetIndex().GetCardTagRecords(cardDbId);
	}

	// Token: 0x06008793 RID: 34707 RVA: 0x002BB688 File Offset: 0x002B9888
	public static string GetHeroPowerCardIdFromHero(string heroCardId)
	{
		int cardTagValue = GameUtils.GetCardTagValue(heroCardId, GAME_TAG.HERO_POWER);
		if (cardTagValue == 0)
		{
			return string.Empty;
		}
		return GameUtils.TranslateDbIdToCardId(cardTagValue, false);
	}

	// Token: 0x06008794 RID: 34708 RVA: 0x002BB6B1 File Offset: 0x002B98B1
	public static string GetHeroPowerCardIdFromHero(int heroDbId)
	{
		if (GameDbf.Card.GetRecord(heroDbId) == null)
		{
			Debug.LogError(string.Format("GameUtils.GetHeroPowerCardIdFromHero() - failed to find record for heroDbId {0}", heroDbId));
			return string.Empty;
		}
		return GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(heroDbId, GAME_TAG.HERO_POWER), false);
	}

	// Token: 0x06008795 RID: 34709 RVA: 0x002BB6EC File Offset: 0x002B98EC
	public static string GetCardIdFromHeroDbId(int heroDbId)
	{
		CardHeroDbfRecord record = GameDbf.CardHero.GetRecord(heroDbId);
		if (record == null)
		{
			Debug.LogError(string.Format("GameUtils.GetCardIdFromHeroDbId() - failed to find record for heroDbId {0}", heroDbId));
			return string.Empty;
		}
		return GameUtils.TranslateDbIdToCardId(record.CardId, false);
	}

	// Token: 0x06008796 RID: 34710 RVA: 0x002BB730 File Offset: 0x002B9930
	public static TAG_CARD_SET GetCardSetFromCardID(string cardID)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		if (entityDef == null)
		{
			Debug.LogError(string.Format("Null EntityDef in GetCardSetFromCardID() for {0}", cardID));
			return TAG_CARD_SET.INVALID;
		}
		return entityDef.GetCardSet();
	}

	// Token: 0x06008797 RID: 34711 RVA: 0x002BB764 File Offset: 0x002B9964
	public static int GetCardIdFromGuestHeroDbId(int guestHeroDbId)
	{
		GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(guestHeroDbId);
		if (record == null)
		{
			Debug.LogError(string.Format("GameUtils.GetCardIdFromGuestHeroDbId() - failed to find record for guestHeroDbId {0}", guestHeroDbId));
			return 0;
		}
		return record.CardId;
	}

	// Token: 0x06008798 RID: 34712 RVA: 0x002BB7A0 File Offset: 0x002B99A0
	public static int GetFavoriteHeroCardDBIdFromClass(TAG_CLASS classTag)
	{
		NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(classTag);
		string text = (favoriteHero != null) ? favoriteHero.Name : null;
		if (string.IsNullOrEmpty(text))
		{
			text = CollectionManager.GetHeroCardId(classTag, CardHero.HeroType.VANILLA);
		}
		return GameUtils.TranslateCardIdToDbId(text, false);
	}

	// Token: 0x06008799 RID: 34713 RVA: 0x002BB7DC File Offset: 0x002B99DC
	public static bool IsVanillaHero(string cardId)
	{
		int id = GameUtils.TranslateCardIdToDbId(cardId, false);
		CardDbfRecord record = GameDbf.Card.GetRecord(id);
		if (record == null)
		{
			return false;
		}
		CardHeroDbfRecord cardHero = record.CardHero;
		CardHero.HeroType? heroType = (cardHero != null) ? new CardHero.HeroType?(cardHero.HeroType) : null;
		CardHero.HeroType heroType2 = CardHero.HeroType.VANILLA;
		return heroType.GetValueOrDefault() == heroType2 & heroType != null;
	}

	// Token: 0x0600879A RID: 34714 RVA: 0x002BB838 File Offset: 0x002B9A38
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

	// Token: 0x0600879B RID: 34715 RVA: 0x002BB890 File Offset: 0x002B9A90
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

	// Token: 0x0600879C RID: 34716 RVA: 0x002BB8DC File Offset: 0x002B9ADC
	public static int? GetTotalHeroLevel()
	{
		int? num = null;
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		if (netObject != null)
		{
			num = new int?(0);
			using (List<NetCache.HeroLevel>.Enumerator enumerator = netObject.Levels.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NetCache.HeroLevel heroLevel = enumerator.Current;
					num += heroLevel.CurrentLevel.Level;
				}
				return num;
			}
		}
		Debug.LogError("GameUtils.GetHeroLevel() - NetCache.NetCacheHeroLevels is null");
		return num;
	}

	// Token: 0x0600879D RID: 34717 RVA: 0x002BB984 File Offset: 0x002B9B84
	public static int CardPremiumSortComparisonAsc(TAG_PREMIUM premium1, TAG_PREMIUM premium2)
	{
		return premium1 - premium2;
	}

	// Token: 0x0600879E RID: 34718 RVA: 0x002BB989 File Offset: 0x002B9B89
	public static int CardPremiumSortComparisonDesc(TAG_PREMIUM premium1, TAG_PREMIUM premium2)
	{
		return premium2 - premium1;
	}

	// Token: 0x0600879F RID: 34719 RVA: 0x002BB98E File Offset: 0x002B9B8E
	public static bool CanConcedeCurrentMission()
	{
		return GameState.Get() != null && !GameMgr.Get().IsTutorial() && !GameMgr.Get().IsSpectator();
	}

	// Token: 0x060087A0 RID: 34720 RVA: 0x002BB9B8 File Offset: 0x002B9BB8
	public static bool CanRestartCurrentMission(bool checkTutorial = true)
	{
		return GameState.Get() != null && !GameState.Get().GetBooleanGameOption(GameEntityOption.DISABLE_RESTART_BUTTON) && (!checkTutorial || !GameMgr.Get().IsTutorial()) && !GameMgr.Get().IsSpectator() && GameMgr.Get().IsAI() && GameMgr.Get().HasLastPlayedDeckId() && BattleNet.IsConnected() && (!DemoMgr.Get().IsDemo() || DemoMgr.Get().CanRestartMissions()) && !GameMgr.Get().IsDungeonCrawlMission();
	}

	// Token: 0x060087A1 RID: 34721 RVA: 0x002BBA4B File Offset: 0x002B9C4B
	public static bool IsWaitingForOpponentReconnect()
	{
		return GameState.Get() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD);
	}

	// Token: 0x060087A2 RID: 34722 RVA: 0x002BBA6C File Offset: 0x002B9C6C
	public static global::Card GetJoustWinner(Network.HistMetaData metaData)
	{
		if (metaData == null)
		{
			return null;
		}
		if (metaData.MetaType != HistoryMeta.Type.JOUST)
		{
			return null;
		}
		global::Entity entity = GameState.Get().GetEntity(metaData.Data);
		if (entity == null)
		{
			return null;
		}
		return entity.GetCard();
	}

	// Token: 0x060087A3 RID: 34723 RVA: 0x002BBAA8 File Offset: 0x002B9CA8
	public static bool IsHistoryDeathTagChange(Network.HistTagChange tagChange)
	{
		global::Entity entity = GameState.Get().GetEntity(tagChange.Entity);
		return entity != null && !entity.IsEnchantment() && entity.GetCardType() != TAG_CARDTYPE.INVALID && ((tagChange.Tag == 360 && tagChange.Value == 1) || (entity.IsMinion() && tagChange.Tag == 49 && tagChange.Value == 4 && entity.GetZone() == TAG_ZONE.PLAY));
	}

	// Token: 0x060087A4 RID: 34724 RVA: 0x002BBB1E File Offset: 0x002B9D1E
	public static bool IsHistoryDiscardTagChange(Network.HistTagChange tagChange)
	{
		return tagChange.Tag == 49 && GameState.Get().GetEntity(tagChange.Entity).GetZone() == TAG_ZONE.HAND && tagChange.Value == 4;
	}

	// Token: 0x060087A5 RID: 34725 RVA: 0x002BBB52 File Offset: 0x002B9D52
	public static bool IsHistoryMovedToSetAsideTagChange(Network.HistTagChange tagChange)
	{
		return tagChange.Tag == 49 && tagChange.Value == 6;
	}

	// Token: 0x060087A6 RID: 34726 RVA: 0x002BBB6C File Offset: 0x002B9D6C
	public static bool IsEntityDeathTagChange(Network.HistTagChange tagChange)
	{
		return tagChange.Tag == 49 && tagChange.Value == 4 && GameState.Get().GetEntity(tagChange.Entity) != null;
	}

	// Token: 0x060087A7 RID: 34727 RVA: 0x002BBB9C File Offset: 0x002B9D9C
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
		global::Entity entity = GameState.Get().GetEntity(tagChange.Entity);
		return entity != null && entity.IsCharacter();
	}

	// Token: 0x060087A8 RID: 34728 RVA: 0x002BBBE1 File Offset: 0x002B9DE1
	public static bool IsPreGameOverPlayState(TAG_PLAYSTATE playState)
	{
		return playState - TAG_PLAYSTATE.WINNING <= 1 || playState - TAG_PLAYSTATE.DISCONNECTED <= 1;
	}

	// Token: 0x060087A9 RID: 34729 RVA: 0x002BBBF2 File Offset: 0x002B9DF2
	public static bool IsGameOverTag(int entityId, int tag, int val)
	{
		return GameUtils.IsGameOverTag(GameState.Get().GetEntity(entityId) as global::Player, tag, val);
	}

	// Token: 0x060087AA RID: 34730 RVA: 0x002BBC0C File Offset: 0x002B9E0C
	public static bool IsGameOverTag(global::Player player, int tag, int val)
	{
		return player != null && tag == 17 && player.IsFriendlySide() && val - 4 <= 2;
	}

	// Token: 0x060087AB RID: 34731 RVA: 0x002BBC3C File Offset: 0x002B9E3C
	public static bool IsFriendlyConcede(Network.HistTagChange tagChange)
	{
		if (tagChange.Tag != 17)
		{
			return false;
		}
		global::Player player = GameState.Get().GetEntity(tagChange.Entity) as global::Player;
		return player != null && player.IsFriendlySide() && tagChange.Value == 8;
	}

	// Token: 0x060087AC RID: 34732 RVA: 0x002BBC83 File Offset: 0x002B9E83
	public static bool IsBeginPhase(TAG_STEP step)
	{
		return step <= TAG_STEP.BEGIN_MULLIGAN;
	}

	// Token: 0x060087AD RID: 34733 RVA: 0x002BBC8C File Offset: 0x002B9E8C
	public static bool IsPastBeginPhase(TAG_STEP step)
	{
		return !GameUtils.IsBeginPhase(step);
	}

	// Token: 0x060087AE RID: 34734 RVA: 0x002BBC97 File Offset: 0x002B9E97
	public static bool IsMainPhase(TAG_STEP step)
	{
		return step - TAG_STEP.MAIN_BEGIN <= 8 || step - TAG_STEP.MAIN_CLEANUP <= 1;
	}

	// Token: 0x060087AF RID: 34735 RVA: 0x002BBCAC File Offset: 0x002B9EAC
	public static List<global::Entity> GetEntitiesKilledBySourceAmongstTargets(int damageSourceID, List<global::Entity> targetEntities)
	{
		List<global::Entity> list = new List<global::Entity>();
		foreach (global::Entity entity in targetEntities)
		{
			if (entity != null)
			{
				list.Add(entity.CloneForZoneMgr());
			}
		}
		List<global::Entity> list2 = new List<global::Entity>();
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
				if (tagChange != null)
				{
					if (tagChange.Tag == 18)
					{
						global::Entity entity2 = list.Find((global::Entity targetEntity) => targetEntity.GetEntityId() == tagChange.Entity);
						if (entity2 != null)
						{
							entity2.SetTag(18, tagChange.Value);
						}
					}
					else if (tagChange.Tag == 49 && tagChange.Value == 4)
					{
						global::Entity entity3 = list.Find((global::Entity targetEntity) => targetEntity.GetEntityId() == tagChange.Entity);
						if (entity3 != null && entity3.GetTag(GAME_TAG.LAST_AFFECTED_BY) == damageSourceID)
						{
							list2.Add(entity3);
						}
					}
				}
			}
		}
		return list2;
	}

	// Token: 0x060087B0 RID: 34736 RVA: 0x002BBE4C File Offset: 0x002BA04C
	public static void ApplyPower(global::Entity entity, Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.SHOW_ENTITY:
			GameUtils.ApplyShowEntity(entity, (Network.HistShowEntity)power);
			return;
		case Network.PowerType.HIDE_ENTITY:
			GameUtils.ApplyHideEntity(entity, (Network.HistHideEntity)power);
			return;
		case Network.PowerType.TAG_CHANGE:
			GameUtils.ApplyTagChange(entity, (Network.HistTagChange)power);
			return;
		default:
			return;
		}
	}

	// Token: 0x060087B1 RID: 34737 RVA: 0x002BBE9C File Offset: 0x002BA09C
	public static void ApplyShowEntity(global::Entity entity, Network.HistShowEntity showEntity)
	{
		foreach (Network.Entity.Tag tag in showEntity.Entity.Tags)
		{
			entity.SetTag(tag.Name, tag.Value);
		}
	}

	// Token: 0x060087B2 RID: 34738 RVA: 0x002BBF00 File Offset: 0x002BA100
	public static void ApplyHideEntity(global::Entity entity, Network.HistHideEntity hideEntity)
	{
		entity.SetTag(GAME_TAG.ZONE, hideEntity.Zone);
	}

	// Token: 0x060087B3 RID: 34739 RVA: 0x002BBF10 File Offset: 0x002BA110
	public static void ApplyTagChange(global::Entity entity, Network.HistTagChange tagChange)
	{
		entity.SetTag(tagChange.Tag, tagChange.Value);
	}

	// Token: 0x060087B4 RID: 34740 RVA: 0x002BBF24 File Offset: 0x002BA124
	public static TAG_ZONE GetFinalZoneForEntity(global::Entity entity)
	{
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		List<PowerTaskList> list = new List<PowerTaskList>();
		if (powerProcessor.GetCurrentTaskList() != null)
		{
			list.Add(powerProcessor.GetCurrentTaskList());
		}
		list.AddRange(powerProcessor.GetPowerQueue().GetList());
		for (int i = list.Count - 1; i >= 0; i--)
		{
			List<PowerTask> taskList = list[i].GetTaskList();
			for (int j = taskList.Count - 1; j >= 0; j--)
			{
				Network.HistTagChange histTagChange = taskList[j].GetPower() as Network.HistTagChange;
				if (histTagChange != null && histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 49)
				{
					return (TAG_ZONE)histTagChange.Value;
				}
			}
		}
		return entity.GetZone();
	}

	// Token: 0x060087B5 RID: 34741 RVA: 0x002BBFE0 File Offset: 0x002BA1E0
	public static bool IsEntityHiddenAfterCurrentTasklist(global::Entity entity)
	{
		if (!entity.IsHidden())
		{
			return false;
		}
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		if (powerProcessor.GetCurrentTaskList() != null)
		{
			foreach (PowerTask powerTask in powerProcessor.GetCurrentTaskList().GetTaskList())
			{
				Network.HistShowEntity histShowEntity = powerTask.GetPower() as Network.HistShowEntity;
				if (histShowEntity != null && histShowEntity.Entity.ID == entity.GetEntityId() && !string.IsNullOrEmpty(histShowEntity.Entity.CardID))
				{
					return false;
				}
			}
			return true;
		}
		return true;
	}

	// Token: 0x060087B6 RID: 34742 RVA: 0x002BC088 File Offset: 0x002BA288
	public static bool IsGalakrond(string cardId)
	{
		return cardId == "DRG_600" || cardId == "DRG_600t2" || cardId == "DRG_600t3" || cardId == "DRG_650" || cardId == "DRG_650t2" || cardId == "DRG_650t3" || cardId == "DRG_620" || cardId == "DRG_620t2" || cardId == "DRG_620t3" || cardId == "DRG_660" || cardId == "DRG_660t2" || cardId == "DRG_660t3" || cardId == "DRG_610" || cardId == "DRG_610t2" || cardId == "DRG_610t3";
	}

	// Token: 0x060087B7 RID: 34743 RVA: 0x002BC16C File Offset: 0x002BA36C
	public static bool IsGalakrondInPlay(global::Player player)
	{
		if (player == null)
		{
			return false;
		}
		global::Entity hero = player.GetHero();
		return hero != null && GameUtils.IsGalakrond(hero.GetCardId());
	}

	// Token: 0x060087B8 RID: 34744 RVA: 0x002BC198 File Offset: 0x002BA398
	public static void DoDamageTasks(PowerTaskList powerTaskList, global::Card sourceCard, global::Card targetCard)
	{
		List<PowerTask> taskList = powerTaskList.GetTaskList();
		if (taskList == null)
		{
			return;
		}
		if (taskList.Count == 0)
		{
			return;
		}
		int entityId = sourceCard.GetEntity().GetEntityId();
		int entityId2 = targetCard.GetEntity().GetEntityId();
		foreach (PowerTask powerTask in taskList)
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType != HistoryMeta.Type.DAMAGE && histMetaData.MetaType != HistoryMeta.Type.HEALING)
				{
					continue;
				}
				using (List<int>.Enumerator enumerator2 = histMetaData.Info.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						int num = enumerator2.Current;
						if (num == entityId || num == entityId2)
						{
							powerTask.DoTask();
						}
					}
					continue;
				}
			}
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Entity == entityId || histTagChange.Entity == entityId2)
				{
					GAME_TAG tag = (GAME_TAG)histTagChange.Tag;
					if (tag == GAME_TAG.DAMAGE || tag == GAME_TAG.EXHAUSTED)
					{
						powerTask.DoTask();
					}
				}
			}
		}
	}

	// Token: 0x060087B9 RID: 34745 RVA: 0x002BC2D8 File Offset: 0x002BA4D8
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

	// Token: 0x060087BA RID: 34746 RVA: 0x002BC308 File Offset: 0x002BA508
	public static WingDbfRecord GetWingRecordFromMissionId(int missionId)
	{
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId((ScenarioDbId)missionId);
		if (wingIdFromMissionId == WingDbId.INVALID)
		{
			return null;
		}
		return GameDbf.Wing.GetRecord((int)wingIdFromMissionId);
	}

	// Token: 0x060087BB RID: 34747 RVA: 0x002BC32C File Offset: 0x002BA52C
	public static WingDbId GetWingIdFromMissionId(ScenarioDbId missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)missionId);
		if (record != null)
		{
			return (WingDbId)record.WingId;
		}
		return WingDbId.INVALID;
	}

	// Token: 0x060087BC RID: 34748 RVA: 0x002BC350 File Offset: 0x002BA550
	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord adventureDataDbfRecord in GameDbf.AdventureData.GetRecords())
		{
			if (adventureDataDbfRecord.AdventureId == adventureId && adventureDataDbfRecord.ModeId == modeId)
			{
				return adventureDataDbfRecord;
			}
		}
		return null;
	}

	// Token: 0x060087BD RID: 34749 RVA: 0x002BC3BC File Offset: 0x002BA5BC
	public static List<ScenarioDbfRecord> GetClassChallengeRecords(int adventureId, int wingId)
	{
		List<ScenarioDbfRecord> list = new List<ScenarioDbfRecord>();
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords())
		{
			if (scenarioDbfRecord.ModeId == 4 && scenarioDbfRecord.AdventureId == adventureId && scenarioDbfRecord.WingId == wingId)
			{
				list.Add(scenarioDbfRecord);
			}
		}
		return list;
	}

	// Token: 0x060087BE RID: 34750 RVA: 0x002BC438 File Offset: 0x002BA638
	public static TAG_CLASS GetClassChallengeHeroClass(ScenarioDbfRecord rec)
	{
		if (rec.ModeId != 4)
		{
			return TAG_CLASS.INVALID;
		}
		int player1HeroCardId = rec.Player1HeroCardId;
		EntityDef entityDef = DefLoader.Get().GetEntityDef(player1HeroCardId, true);
		if (entityDef == null)
		{
			return TAG_CLASS.INVALID;
		}
		return entityDef.GetClass();
	}

	// Token: 0x060087BF RID: 34751 RVA: 0x002BC470 File Offset: 0x002BA670
	public static List<TAG_CLASS> GetClassChallengeHeroClasses(int adventureId, int wingId)
	{
		List<ScenarioDbfRecord> classChallengeRecords = GameUtils.GetClassChallengeRecords(adventureId, wingId);
		List<TAG_CLASS> list = new List<TAG_CLASS>();
		foreach (ScenarioDbfRecord rec in classChallengeRecords)
		{
			list.Add(GameUtils.GetClassChallengeHeroClass(rec));
		}
		return list;
	}

	// Token: 0x060087C0 RID: 34752 RVA: 0x002BC4D0 File Offset: 0x002BA6D0
	public static bool IsAIMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		return record != null && record.Players == 1;
	}

	// Token: 0x060087C1 RID: 34753 RVA: 0x002BC4FC File Offset: 0x002BA6FC
	public static bool IsCoopMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		return record != null && record.IsCoop;
	}

	// Token: 0x060087C2 RID: 34754 RVA: 0x002BC520 File Offset: 0x002BA720
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
		return GameUtils.TranslateDbIdToCardId(num, false);
	}

	// Token: 0x060087C3 RID: 34755 RVA: 0x002BC558 File Offset: 0x002BA758
	public static string GetMissionHeroName(int missionId)
	{
		string missionHeroCardId = GameUtils.GetMissionHeroCardId(missionId);
		if (missionHeroCardId == null)
		{
			return null;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(missionHeroCardId);
		if (entityDef == null)
		{
			Debug.LogError(string.Format("GameUtils.GetMissionHeroName() - hero {0} for mission {1} has no EntityDef", missionHeroCardId, missionId));
			return null;
		}
		return entityDef.GetName();
	}

	// Token: 0x060087C4 RID: 34756 RVA: 0x002BC5A0 File Offset: 0x002BA7A0
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
			return GameUtils.TranslateDbIdToCardId(clientPlayer2HeroPowerCardId, false);
		}
		int num = record.ClientPlayer2HeroCardId;
		if (num == 0)
		{
			num = record.Player2HeroCardId;
		}
		return GameUtils.GetHeroPowerCardIdFromHero(num);
	}

	// Token: 0x060087C5 RID: 34757 RVA: 0x002BC5E8 File Offset: 0x002BA7E8
	public static bool IsMissionForAdventure(int missionId, int adventureId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		return record != null && adventureId == record.AdventureId;
	}

	// Token: 0x060087C6 RID: 34758 RVA: 0x002BC60F File Offset: 0x002BA80F
	public static bool IsTutorialMission(int missionId)
	{
		return GameUtils.IsMissionForAdventure(missionId, 1);
	}

	// Token: 0x060087C7 RID: 34759 RVA: 0x002BC618 File Offset: 0x002BA818
	public static bool IsPracticeMission(int missionId)
	{
		return GameUtils.IsMissionForAdventure(missionId, 2);
	}

	// Token: 0x060087C8 RID: 34760 RVA: 0x002BC624 File Offset: 0x002BA824
	public static bool IsDungeonCrawlMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		return record != null && GameUtils.DoesAdventureModeUseDungeonCrawlFormat((AdventureModeDbId)record.ModeId);
	}

	// Token: 0x060087C9 RID: 34761 RVA: 0x002BC64D File Offset: 0x002BA84D
	public static bool DoesAdventureModeUseDungeonCrawlFormat(AdventureModeDbId modeId)
	{
		return modeId == AdventureModeDbId.DUNGEON_CRAWL || modeId == AdventureModeDbId.DUNGEON_CRAWL_HEROIC;
	}

	// Token: 0x060087CA RID: 34762 RVA: 0x002BC659 File Offset: 0x002BA859
	public static bool IsBoosterLatestActiveExpansion(int boosterId)
	{
		return boosterId == (int)GameUtils.GetLatestRewardableBooster();
	}

	// Token: 0x060087CB RID: 34763 RVA: 0x002BC663 File Offset: 0x002BA863
	public static BoosterDbId GetLatestRewardableBooster()
	{
		return GameUtils.GetRewardableBoosterOffsetFromLatest(0);
	}

	// Token: 0x060087CC RID: 34764 RVA: 0x002BC66C File Offset: 0x002BA86C
	public static BoosterDbId GetRewardableBoosterOffsetFromLatest(int offset)
	{
		List<BoosterDbfRecord> rewardableBoosters = GameUtils.GetRewardableBoosters();
		if (rewardableBoosters.Count <= 0)
		{
			Debug.LogError("No active Booster sets found");
			return BoosterDbId.INVALID;
		}
		offset = Mathf.Clamp(offset, 0, rewardableBoosters.Count - 1);
		return (BoosterDbId)rewardableBoosters[offset].ID;
	}

	// Token: 0x060087CD RID: 34765 RVA: 0x002BC6B4 File Offset: 0x002BA8B4
	public static BoosterDbId GetRewardableBoosterFromSelector(RewardItem.BoosterSelector selector)
	{
		switch (selector)
		{
		case RewardItem.BoosterSelector.LATEST:
			return GameUtils.GetRewardableBoosterOffsetFromLatest(0);
		case RewardItem.BoosterSelector.LATEST_OFFSET_BY_1:
			return GameUtils.GetRewardableBoosterOffsetFromLatest(1);
		case RewardItem.BoosterSelector.LATEST_OFFSET_BY_2:
			return GameUtils.GetRewardableBoosterOffsetFromLatest(2);
		case RewardItem.BoosterSelector.LATEST_OFFSET_BY_3:
			return GameUtils.GetRewardableBoosterOffsetFromLatest(3);
		default:
			Debug.LogError(string.Format("Unknown BoosterSelector {0}", selector));
			return BoosterDbId.INVALID;
		}
	}

	// Token: 0x060087CE RID: 34766 RVA: 0x002BC710 File Offset: 0x002BA910
	public static AdventureDbId GetLatestActiveAdventure()
	{
		return GameDbf.Adventure.GetRecords((AdventureDbfRecord r) => !AdventureConfig.IsAdventureComingSoon((AdventureDbId)r.ID) && AdventureConfig.IsAdventureEventActive((AdventureDbId)r.ID), -1).Max((AdventureDbfRecord r) => (AdventureDbId)r.ID);
	}

	// Token: 0x060087CF RID: 34767 RVA: 0x002BC76C File Offset: 0x002BA96C
	public static bool IsExpansionMission(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return false;
		}
		int adventureId = record.AdventureId;
		return adventureId != 0 && GameUtils.IsExpansionAdventure((AdventureDbId)adventureId);
	}

	// Token: 0x060087D0 RID: 34768 RVA: 0x002BC79C File Offset: 0x002BA99C
	public static bool IsExpansionAdventure(AdventureDbId adventureId)
	{
		return adventureId > AdventureDbId.PRACTICE && adventureId != AdventureDbId.TAVERN_BRAWL;
	}

	// Token: 0x060087D1 RID: 34769 RVA: 0x002BC7AC File Offset: 0x002BA9AC
	public static string GetAdventureProductStringKey(int wingID)
	{
		AdventureDbId adventureIdByWingId = GameUtils.GetAdventureIdByWingId(wingID);
		if (adventureIdByWingId != AdventureDbId.INVALID)
		{
			return GameDbf.Adventure.GetRecord((int)adventureIdByWingId).ProductStringKey;
		}
		return string.Empty;
	}

	// Token: 0x060087D2 RID: 34770 RVA: 0x002BC7DC File Offset: 0x002BA9DC
	public static AdventureDbId GetAdventureId(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return AdventureDbId.INVALID;
		}
		return (AdventureDbId)record.AdventureId;
	}

	// Token: 0x060087D3 RID: 34771 RVA: 0x002BC800 File Offset: 0x002BAA00
	public static AdventureDbId GetAdventureIdByWingId(int wingID)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(wingID);
		if (record == null)
		{
			return AdventureDbId.INVALID;
		}
		AdventureDbId adventureId = (AdventureDbId)record.AdventureId;
		if (!GameUtils.IsExpansionAdventure(adventureId))
		{
			return AdventureDbId.INVALID;
		}
		return adventureId;
	}

	// Token: 0x060087D4 RID: 34772 RVA: 0x002BC830 File Offset: 0x002BAA30
	public static AdventureModeDbId GetAdventureModeId(int missionId)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record == null)
		{
			return AdventureModeDbId.INVALID;
		}
		return (AdventureModeDbId)record.ModeId;
	}

	// Token: 0x060087D5 RID: 34773 RVA: 0x002BC854 File Offset: 0x002BAA54
	public static bool IsHeroicAdventureMission(int missionId)
	{
		return GameUtils.IsModeHeroic(GameUtils.GetAdventureModeId(missionId));
	}

	// Token: 0x060087D6 RID: 34774 RVA: 0x002BC861 File Offset: 0x002BAA61
	public static bool IsModeHeroic(AdventureModeDbId mode)
	{
		return mode == AdventureModeDbId.LINEAR_HEROIC || mode == AdventureModeDbId.DUNGEON_CRAWL_HEROIC;
	}

	// Token: 0x060087D7 RID: 34775 RVA: 0x002BC86D File Offset: 0x002BAA6D
	public static AdventureModeDbId GetNormalModeFromHeroicMode(AdventureModeDbId mode)
	{
		if (mode == AdventureModeDbId.DUNGEON_CRAWL_HEROIC)
		{
			return AdventureModeDbId.DUNGEON_CRAWL;
		}
		if (mode == AdventureModeDbId.LINEAR_HEROIC)
		{
			return AdventureModeDbId.LINEAR;
		}
		return mode;
	}

	// Token: 0x060087D8 RID: 34776 RVA: 0x002BC87C File Offset: 0x002BAA7C
	public static bool IsClassChallengeMission(int missionId)
	{
		return GameUtils.GetAdventureModeId(missionId) == AdventureModeDbId.CLASS_CHALLENGE;
	}

	// Token: 0x060087D9 RID: 34777 RVA: 0x002BC888 File Offset: 0x002BAA88
	public static int GetSortedWingUnlockIndex(WingDbfRecord wingRecord)
	{
		List<WingDbfRecord> records = GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == wingRecord.AdventureId, -1);
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

	// Token: 0x060087DA RID: 34778 RVA: 0x002BC8F0 File Offset: 0x002BAAF0
	public static int GetNumWingsInAdventure(AdventureDbId adventureId)
	{
		return GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == (int)adventureId, -1).Count;
	}

	// Token: 0x060087DB RID: 34779 RVA: 0x002BC926 File Offset: 0x002BAB26
	public static bool AreAllTutorialsComplete(TutorialProgress progress)
	{
		return DemoMgr.Get().GetMode() != DemoMode.BLIZZ_MUSEUM && progress == TutorialProgress.ILLIDAN_COMPLETE;
	}

	// Token: 0x060087DC RID: 34780 RVA: 0x002BC93B File Offset: 0x002BAB3B
	public static bool AreAllTutorialsComplete(long campaignProgress)
	{
		return GameUtils.AreAllTutorialsComplete((TutorialProgress)campaignProgress);
	}

	// Token: 0x060087DD RID: 34781 RVA: 0x002BC944 File Offset: 0x002BAB44
	public static bool AreAllTutorialsComplete()
	{
		NetCache.NetCacheProfileProgress value = GameUtils.s_profileProgress.Value;
		return value != null && GameUtils.AreAllTutorialsComplete(value.CampaignProgress);
	}

	// Token: 0x060087DE RID: 34782 RVA: 0x002BC971 File Offset: 0x002BAB71
	public static int GetNextTutorial(TutorialProgress progress)
	{
		if (progress == TutorialProgress.NOTHING_COMPLETE)
		{
			return 3;
		}
		if (progress == TutorialProgress.HOGGER_COMPLETE)
		{
			return 4;
		}
		if (progress == TutorialProgress.MILLHOUSE_COMPLETE)
		{
			return 249;
		}
		if (progress == TutorialProgress.CHO_COMPLETE)
		{
			return 181;
		}
		if (progress == TutorialProgress.MUKLA_COMPLETE)
		{
			return 201;
		}
		if (progress == TutorialProgress.NESINGWARY_COMPLETE)
		{
			return 248;
		}
		return 0;
	}

	// Token: 0x060087DF RID: 34783 RVA: 0x002BC9A8 File Offset: 0x002BABA8
	public static int GetNextTutorial()
	{
		NetCache.NetCacheProfileProgress value = GameUtils.s_profileProgress.Value;
		if (value == null)
		{
			return GameUtils.GetNextTutorial(Options.Get().GetEnum<TutorialProgress>(global::Option.LOCAL_TUTORIAL_PROGRESS));
		}
		return GameUtils.GetNextTutorial(value.CampaignProgress);
	}

	// Token: 0x060087E0 RID: 34784 RVA: 0x002BC9E0 File Offset: 0x002BABE0
	public static string GetTutorialCardRewardDetails(int missionId)
	{
		if (missionId <= 181)
		{
			if (missionId == 3)
			{
				return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL01");
			}
			if (missionId == 4)
			{
				return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL02");
			}
			if (missionId == 181)
			{
				return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL03");
			}
		}
		else
		{
			if (missionId == 201)
			{
				return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL04");
			}
			if (missionId == 248)
			{
				return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL05");
			}
			if (missionId == 249)
			{
				return GameStrings.Get("GLOBAL_REWARD_CARD_DETAILS_TUTORIAL06");
			}
		}
		Debug.LogWarning(string.Format("GameUtils.GetTutorialCardRewardDetails(): no card reward details for mission {0}", missionId));
		return "";
	}

	// Token: 0x060087E1 RID: 34785 RVA: 0x002BCA7F File Offset: 0x002BAC7F
	public static string GetCurrentTutorialCardRewardDetails()
	{
		return GameUtils.GetTutorialCardRewardDetails(GameMgr.Get().GetMissionId());
	}

	// Token: 0x060087E2 RID: 34786 RVA: 0x002BCA90 File Offset: 0x002BAC90
	public static int MissionSortComparison(ScenarioDbfRecord rec1, ScenarioDbfRecord rec2)
	{
		return rec1.SortOrder - rec2.SortOrder;
	}

	// Token: 0x060087E3 RID: 34787 RVA: 0x002BCAA0 File Offset: 0x002BACA0
	public static List<ScenarioGuestHeroesDbfRecord> GetScenarioGuestHeroes(int scenarioId)
	{
		return GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == scenarioId, -1);
	}

	// Token: 0x060087E4 RID: 34788 RVA: 0x002BCAD4 File Offset: 0x002BACD4
	public static int GetDefeatedBossCount()
	{
		int @int = Options.Get().GetInt(global::Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(global::Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return 0;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		if (!DungeonCrawlUtil.IsDungeonRunActive(gameSaveDataServerKey))
		{
			return 0;
		}
		List<long> list = null;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out list);
		if (list == null)
		{
			return 0;
		}
		return list.Count;
	}

	// Token: 0x060087E5 RID: 34789 RVA: 0x002BCB34 File Offset: 0x002BAD34
	public static List<FixedRewardActionDbfRecord> GetFixedActionRecords(FixedRewardAction.Type actionType)
	{
		return GameDbf.GetIndex().GetFixedActionRecordsForType(actionType);
	}

	// Token: 0x060087E6 RID: 34790 RVA: 0x002BCB44 File Offset: 0x002BAD44
	public static FixedRewardDbfRecord GetFixedRewardForCard(string cardID, TAG_PREMIUM premium)
	{
		int num = GameUtils.TranslateCardIdToDbId(cardID, false);
		foreach (FixedRewardDbfRecord fixedRewardDbfRecord in GameDbf.FixedReward.GetRecords())
		{
			int cardId = fixedRewardDbfRecord.CardId;
			if (num == cardId)
			{
				int cardPremium = fixedRewardDbfRecord.CardPremium;
				if (premium == (TAG_PREMIUM)cardPremium)
				{
					return fixedRewardDbfRecord;
				}
			}
		}
		return null;
	}

	// Token: 0x060087E7 RID: 34791 RVA: 0x002BCBC0 File Offset: 0x002BADC0
	public static List<FixedRewardMapDbfRecord> GetFixedRewardMapRecordsForAction(int actionID)
	{
		return GameDbf.GetIndex().GetFixedRewardMapRecordsForAction(actionID);
	}

	// Token: 0x060087E8 RID: 34792 RVA: 0x002BCBD0 File Offset: 0x002BADD0
	public static int GetFixedRewardCounterpartCardID(int cardID)
	{
		foreach (FixedRewardActionDbfRecord fixedRewardActionDbfRecord in GameUtils.GetFixedActionRecords(FixedRewardAction.Type.OWNS_COUNTERPART_CARD))
		{
			if (SpecialEventManager.Get().IsEventActive(fixedRewardActionDbfRecord.ActiveEvent, false))
			{
				foreach (FixedRewardMapDbfRecord fixedRewardMapDbfRecord in GameUtils.GetFixedRewardMapRecordsForAction(fixedRewardActionDbfRecord.ID))
				{
					FixedRewardDbfRecord record = GameDbf.FixedReward.GetRecord(fixedRewardMapDbfRecord.RewardId);
					if (GameUtils.GetCardTagValue(record.CardId, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID) == cardID)
					{
						return record.CardId;
					}
				}
			}
		}
		return 0;
	}

	// Token: 0x060087E9 RID: 34793 RVA: 0x002BCCAC File Offset: 0x002BAEAC
	public static string GetOwnedCounterpartCardIDForFormat(EntityDef cardDef, FormatType formatType, int minOwned)
	{
		string text = GameUtils.TranslateDbIdToCardId(cardDef.GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false);
		if (text != null)
		{
			return text;
		}
		TAG_CARD_SET[] cardSetsInFormat = GameUtils.GetCardSetsInFormat(formatType);
		CollectionManager collectionManager = CollectionManager.Get();
		string searchString = null;
		List<CollectibleCardFilter.FilterMask> filterMasks = null;
		int? manaCost = null;
		int? minOwned2 = new int?(minOwned);
		foreach (CollectibleCard collectibleCard in collectionManager.FindCards(searchString, filterMasks, manaCost, cardSetsInFormat, null, null, null, null, null, minOwned2, null, null, null, null, null, false, null, null, null).m_cards)
		{
			if (GameUtils.TranslateDbIdToCardId(collectibleCard.GetEntityDef().GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false) == cardDef.GetCardId())
			{
				text = collectibleCard.CardId;
				break;
			}
		}
		return text;
	}

	// Token: 0x060087EA RID: 34794 RVA: 0x002BCDB4 File Offset: 0x002BAFB4
	public static bool IsMatchmadeGameType(GameType gameType, int? missionId = null)
	{
		switch (gameType)
		{
		case GameType.GT_VS_AI:
		case GameType.GT_VS_FRIEND:
		case GameType.GT_TUTORIAL:
			return false;
		case (GameType)3:
		case GameType.GT_TEST_AI_VS_AI:
			goto IL_7E;
		case GameType.GT_ARENA:
		case GameType.GT_RANKED:
		case GameType.GT_CASUAL:
			break;
		default:
			switch (gameType)
			{
			case GameType.GT_FSG_BRAWL_VS_FRIEND:
			case GameType.GT_FSG_BRAWL_1P_VS_AI:
			case GameType.GT_BATTLEGROUNDS_FRIENDLY:
				return false;
			case GameType.GT_FSG_BRAWL:
			case GameType.GT_FSG_BRAWL_2P_COOP:
			case (GameType)25:
			case GameType.GT_RESERVED_18_22:
			case GameType.GT_RESERVED_18_23:
				goto IL_7E;
			case GameType.GT_BATTLEGROUNDS:
				break;
			case GameType.GT_PVPDR_PAID:
			case GameType.GT_PVPDR:
				return missionId == null || !DungeonCrawlUtil.IsPVPDRFriendlyEncounter(missionId.Value);
			default:
				goto IL_7E;
			}
			break;
		}
		return true;
		IL_7E:
		if (GameUtils.IsTavernBrawlGameType(gameType))
		{
			int missionId2;
			if (missionId != null)
			{
				missionId2 = missionId.Value;
			}
			else
			{
				TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
				if (tavernBrawlMission == null)
				{
					return true;
				}
				missionId2 = tavernBrawlMission.missionId;
			}
			return !GameUtils.IsAIMission(missionId2);
		}
		return false;
	}

	// Token: 0x060087EB RID: 34795 RVA: 0x002BCE80 File Offset: 0x002BB080
	public static bool IsTavernBrawlGameType(GameType gameType)
	{
		return gameType - GameType.GT_TAVERNBRAWL <= 6;
	}

	// Token: 0x060087EC RID: 34796 RVA: 0x002BCE8C File Offset: 0x002BB08C
	public static bool IsFiresideGatheringGameType(GameType gameType)
	{
		return gameType - GameType.GT_FSG_BRAWL_VS_FRIEND <= 3;
	}

	// Token: 0x060087ED RID: 34797 RVA: 0x002BCE98 File Offset: 0x002BB098
	public static bool IsPvpDrGameType(GameType gameType)
	{
		return gameType - GameType.GT_PVPDR_PAID <= 1;
	}

	// Token: 0x060087EE RID: 34798 RVA: 0x002BCEA4 File Offset: 0x002BB0A4
	public static bool ShouldShowArenaModeIcon()
	{
		return GameMgr.Get().GetGameType() == GameType.GT_ARENA;
	}

	// Token: 0x060087EF RID: 34799 RVA: 0x002BCEB3 File Offset: 0x002BB0B3
	public static bool ShouldShowCasualModeIcon()
	{
		return GameMgr.Get().GetGameType() == GameType.GT_CASUAL;
	}

	// Token: 0x060087F0 RID: 34800 RVA: 0x002BCEC2 File Offset: 0x002BB0C2
	public static bool ShouldShowFriendlyChallengeIcon()
	{
		return GameMgr.Get().GetGameType() == GameType.GT_VS_FRIEND && !FriendChallengeMgr.Get().IsChallengeTavernBrawl();
	}

	// Token: 0x060087F1 RID: 34801 RVA: 0x002BCEE4 File Offset: 0x002BB0E4
	public static bool ShouldShowTavernBrawlModeIcon()
	{
		GameType gameType = GameMgr.Get().GetGameType();
		return (gameType == GameType.GT_VS_FRIEND && FriendChallengeMgr.Get().IsChallengeTavernBrawl()) || GameUtils.IsTavernBrawlGameType(gameType);
	}

	// Token: 0x060087F2 RID: 34802 RVA: 0x002BCF1C File Offset: 0x002BB11C
	public static bool ShouldShowAdventureModeIcon()
	{
		int missionId = GameMgr.Get().GetMissionId();
		AdventureDbId adventureId = GameUtils.GetAdventureId(missionId);
		return GameUtils.IsExpansionMission(missionId) && adventureId != AdventureDbId.TAVERN_BRAWL && adventureId != AdventureDbId.PVPDR && !GameUtils.IsTavernBrawlGameType(GameMgr.Get().GetGameType());
	}

	// Token: 0x060087F3 RID: 34803 RVA: 0x002BCF61 File Offset: 0x002BB161
	public static bool ShouldShowPvpDrModeIcon()
	{
		return GameUtils.GetAdventureId(GameMgr.Get().GetMissionId()) == AdventureDbId.PVPDR;
	}

	// Token: 0x060087F4 RID: 34804 RVA: 0x002BCF79 File Offset: 0x002BB179
	public static bool IsGameTypeRanked()
	{
		return GameUtils.IsGameTypeRanked(GameMgr.Get().GetGameType());
	}

	// Token: 0x060087F5 RID: 34805 RVA: 0x002BCF8A File Offset: 0x002BB18A
	public static bool IsGameTypeRanked(GameType gameType)
	{
		return !DemoMgr.Get().IsExpoDemo() && gameType == GameType.GT_RANKED;
	}

	// Token: 0x060087F6 RID: 34806 RVA: 0x002BCFA0 File Offset: 0x002BB1A0
	public static void RequestPlayerPresence(BnetGameAccountId gameAccountId)
	{
		EntityId entityId = default(EntityId);
		entityId.hi = gameAccountId.GetHi();
		entityId.lo = gameAccountId.GetLo();
		List<PresenceFieldKey> list = new List<PresenceFieldKey>();
		PresenceFieldKey item = new PresenceFieldKey
		{
			programId = BnetProgramId.BNET.GetValue(),
			groupId = 2U,
			fieldId = 7U,
			uniqueId = 0UL
		};
		list.Add(item);
		item.programId = BnetProgramId.BNET.GetValue();
		item.groupId = 2U;
		item.fieldId = 3U;
		item.uniqueId = 0UL;
		list.Add(item);
		item.programId = BnetProgramId.BNET.GetValue();
		item.groupId = 2U;
		item.fieldId = 5U;
		item.uniqueId = 0UL;
		list.Add(item);
		if (GameUtils.IsGameTypeRanked())
		{
			list.Add(new PresenceFieldKey
			{
				programId = BnetProgramId.HEARTHSTONE.GetValue(),
				groupId = 2U,
				fieldId = 18U,
				uniqueId = 0UL
			});
		}
		PresenceFieldKey[] fieldList = list.ToArray();
		BattleNet.RequestPresenceFields(true, entityId, fieldList);
	}

	// Token: 0x060087F7 RID: 34807 RVA: 0x002BD0C1 File Offset: 0x002BB2C1
	public static bool IsAIPlayer(BnetGameAccountId gameAccountId)
	{
		return !(gameAccountId == null) && !gameAccountId.IsValid();
	}

	// Token: 0x060087F8 RID: 34808 RVA: 0x002BD0D7 File Offset: 0x002BB2D7
	public static bool IsHumanPlayer(BnetGameAccountId gameAccountId)
	{
		return !(gameAccountId == null) && gameAccountId.IsValid();
	}

	// Token: 0x060087F9 RID: 34809 RVA: 0x002BD0EA File Offset: 0x002BB2EA
	public static bool IsBnetPlayer(BnetGameAccountId gameAccountId)
	{
		return GameUtils.IsHumanPlayer(gameAccountId) && Network.ShouldBeConnectedToAurora();
	}

	// Token: 0x060087FA RID: 34810 RVA: 0x002BD0FB File Offset: 0x002BB2FB
	public static bool IsGuestPlayer(BnetGameAccountId gameAccountId)
	{
		return GameUtils.IsHumanPlayer(gameAccountId) && !Network.ShouldBeConnectedToAurora();
	}

	// Token: 0x060087FB RID: 34811 RVA: 0x002BD110 File Offset: 0x002BB310
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
		return loadingScreen != null && loadingScreen.IsTransitioning();
	}

	// Token: 0x060087FC RID: 34812 RVA: 0x002BD180 File Offset: 0x002BB380
	public static void LogoutConfirmation()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get(Network.ShouldBeConnectedToAurora() ? "GLOBAL_SWITCH_ACCOUNT" : "GLOBAL_LOGIN_CONFIRM_TITLE"),
			m_text = GameStrings.Get(Network.ShouldBeConnectedToAurora() ? "GLOBAL_LOGOUT_CONFIRM_MESSAGE" : "GLOBAL_LOGIN_CONFIRM_MESSAGE"),
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = new AlertPopup.ResponseCallback(GameUtils.OnLogoutConfirmationResponse)
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060087FD RID: 34813 RVA: 0x002BD201 File Offset: 0x002BB401
	private static void OnLogoutConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		TemporaryAccountManager.Get().UnselectTemporaryAccount();
		GameUtils.Logout();
	}

	// Token: 0x060087FE RID: 34814 RVA: 0x002BD217 File Offset: 0x002BB417
	public static void Logout()
	{
		GameMgr.Get().SetPendingAutoConcede(true);
		if (Network.ShouldBeConnectedToAurora())
		{
			ILoginService loginService = HearthstoneServices.Get<ILoginService>();
			if (loginService != null)
			{
				loginService.ClearAuthentication();
			}
		}
		HearthstoneApplication.Get().ResetAndForceLogin();
	}

	// Token: 0x060087FF RID: 34815 RVA: 0x002BD248 File Offset: 0x002BB448
	public static int GetBoosterCount()
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject == null)
		{
			return 0;
		}
		return netObject.GetTotalNumBoosters();
	}

	// Token: 0x06008800 RID: 34816 RVA: 0x002BD26C File Offset: 0x002BB46C
	public static int GetBoosterCount(int boosterStackId)
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject == null)
		{
			return 0;
		}
		NetCache.BoosterStack boosterStack = netObject.GetBoosterStack(boosterStackId);
		if (boosterStack == null)
		{
			return 0;
		}
		return boosterStack.Count;
	}

	// Token: 0x06008801 RID: 34817 RVA: 0x002BD29C File Offset: 0x002BB49C
	public static bool HaveBoosters()
	{
		return GameUtils.GetBoosterCount() > 0;
	}

	// Token: 0x06008802 RID: 34818 RVA: 0x002BD2A6 File Offset: 0x002BB4A6
	public static PackOpeningRarity GetPackOpeningRarity(TAG_RARITY tag)
	{
		switch (tag)
		{
		case TAG_RARITY.COMMON:
			return PackOpeningRarity.COMMON;
		case TAG_RARITY.FREE:
			return PackOpeningRarity.COMMON;
		case TAG_RARITY.RARE:
			return PackOpeningRarity.RARE;
		case TAG_RARITY.EPIC:
			return PackOpeningRarity.EPIC;
		case TAG_RARITY.LEGENDARY:
			return PackOpeningRarity.LEGENDARY;
		default:
			return PackOpeningRarity.NONE;
		}
	}

	// Token: 0x06008803 RID: 34819 RVA: 0x002BD2D1 File Offset: 0x002BB4D1
	public static List<BoosterDbfRecord> GetPackRecordsWithStorePrefab()
	{
		return GameDbf.Booster.GetRecords((BoosterDbfRecord r) => !string.IsNullOrEmpty(r.StorePrefab), -1);
	}

	// Token: 0x06008804 RID: 34820 RVA: 0x002BD300 File Offset: 0x002BB500
	public static List<AdventureDbfRecord> GetSortedAdventureRecordsWithStorePrefab()
	{
		List<AdventureDbfRecord> records = GameDbf.Adventure.GetRecords((AdventureDbfRecord r) => !string.IsNullOrEmpty(r.StorePrefab), -1);
		records.Sort((AdventureDbfRecord l, AdventureDbfRecord r) => r.SortOrder - l.SortOrder);
		return records;
	}

	// Token: 0x06008805 RID: 34821 RVA: 0x002BD35C File Offset: 0x002BB55C
	public static List<AdventureDbfRecord> GetAdventureRecordsWithDefPrefab()
	{
		return GameDbf.Adventure.GetRecords((AdventureDbfRecord r) => !string.IsNullOrEmpty(r.AdventureDefPrefab), -1);
	}

	// Token: 0x06008806 RID: 34822 RVA: 0x002BD388 File Offset: 0x002BB588
	public static List<AdventureDataDbfRecord> GetAdventureDataRecordsWithSubDefPrefab()
	{
		return GameDbf.AdventureData.GetRecords((AdventureDataDbfRecord r) => !string.IsNullOrEmpty(r.AdventureSubDefPrefab), -1);
	}

	// Token: 0x06008807 RID: 34823 RVA: 0x002BD3B4 File Offset: 0x002BB5B4
	public static int PackSortingPredicate(BoosterDbfRecord left, BoosterDbfRecord right)
	{
		if (right.ListDisplayOrderCategory != left.ListDisplayOrderCategory)
		{
			return Mathf.Clamp(right.ListDisplayOrderCategory - left.ListDisplayOrderCategory, -1, 1);
		}
		return Mathf.Clamp(right.ListDisplayOrder - left.ListDisplayOrder, -1, 1);
	}

	// Token: 0x06008808 RID: 34824 RVA: 0x002BD3F0 File Offset: 0x002BB5F0
	public static IEnumerable<int> GetSortedPackIds(bool ascending = true)
	{
		List<BoosterDbfRecord> records = GameDbf.Booster.GetRecords();
		if (ascending)
		{
			records.Sort((BoosterDbfRecord l, BoosterDbfRecord r) => GameUtils.PackSortingPredicate(r, l));
		}
		else
		{
			records.Sort((BoosterDbfRecord l, BoosterDbfRecord r) => GameUtils.PackSortingPredicate(l, r));
		}
		return from b in records
		select b.ID;
	}

	// Token: 0x06008809 RID: 34825 RVA: 0x002BD47C File Offset: 0x002BB67C
	public static bool IsFakePackOpeningEnabled()
	{
		return HearthstoneApplication.IsInternal() && Options.Get().GetBool(global::Option.FAKE_PACK_OPENING);
	}

	// Token: 0x0600880A RID: 34826 RVA: 0x002BD493 File Offset: 0x002BB693
	public static int GetFakePackCount()
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return 0;
		}
		return Options.Get().GetInt(global::Option.FAKE_PACK_COUNT);
	}

	// Token: 0x0600880B RID: 34827 RVA: 0x002BD4AA File Offset: 0x002BB6AA
	public static bool IsFirstPurchaseBundleBooster(StorePackId storePackId)
	{
		return storePackId.Type == StorePackType.BOOSTER && 181 == storePackId.Id;
	}

	// Token: 0x0600880C RID: 34828 RVA: 0x002BD4C4 File Offset: 0x002BB6C4
	public static bool IsMammothBundleBooster(StorePackId storePackId)
	{
		return storePackId.Type == StorePackType.BOOSTER && 41 == storePackId.Id;
	}

	// Token: 0x0600880D RID: 34829 RVA: 0x002BD4DC File Offset: 0x002BB6DC
	public static bool IsLimitedTimeOffer(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
		{
			int productDataFromStorePackId = GameUtils.GetProductDataFromStorePackId(storePackId, 0);
			if (productDataFromStorePackId != 0)
			{
				Network.Bundle bundle = StoreManager.Get().EnumerateBundlesForProductType(ProductType.PRODUCT_TYPE_HIDDEN_LICENSE, false, productDataFromStorePackId, 0, true).FirstOrDefault<Network.Bundle>();
				if (bundle != null && !string.IsNullOrEmpty(bundle.ProductEvent))
				{
					SpecialEventType eventType = SpecialEventManager.GetEventType(bundle.ProductEvent);
					if (eventType != SpecialEventType.UNKNOWN)
					{
						DateTime? eventEndTimeUtc = SpecialEventManager.Get().GetEventEndTimeUtc(eventType);
						if (eventEndTimeUtc != null && eventEndTimeUtc.Value.Subtract(DateTime.UtcNow).TotalDays < 365.0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600880E RID: 34830 RVA: 0x002BD578 File Offset: 0x002BB778
	public static bool IsHiddenLicenseBundleBooster(StorePackId storePackId)
	{
		if (storePackId.Type == StorePackType.BOOSTER)
		{
			BoosterDbId id = (BoosterDbId)storePackId.Id;
			return id == BoosterDbId.MAMMOTH_BUNDLE || id == BoosterDbId.FIRST_PURCHASE;
		}
		return storePackId.Type == StorePackType.MODULAR_BUNDLE;
	}

	// Token: 0x0600880F RID: 34831 RVA: 0x002BD5B4 File Offset: 0x002BB7B4
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
		else
		{
			if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				List<ModularBundleLayoutDbfRecord> regionNodeLayoutsForBundle = StoreManager.Get().GetRegionNodeLayoutsForBundle(storePackId.Id);
				if (selectedIndex >= regionNodeLayoutsForBundle.Count)
				{
					global::Log.Store.PrintWarning(string.Format("Selected invalid layout at index={0}. Defaulting to layout at index=0.", selectedIndex), Array.Empty<object>());
					selectedIndex = 0;
				}
				return regionNodeLayoutsForBundle[selectedIndex].HiddenLicenseId;
			}
			return 0;
		}
	}

	// Token: 0x06008810 RID: 34832 RVA: 0x002BD641 File Offset: 0x002BB841
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

	// Token: 0x06008811 RID: 34833 RVA: 0x002BD670 File Offset: 0x002BB870
	public static List<BoosterDbfRecord> GetRewardableBoosters()
	{
		return (from r in GameDbf.Booster.GetRecords()
		where !GameUtils.IsBoosterRotated((BoosterDbId)r.ID, DateTime.UtcNow)
		where SpecialEventManager.Get().IsEventActive(r.RewardableEvent, false, DateTime.UtcNow)
		orderby r.LatestExpansionOrder descending
		select r).ToList<BoosterDbfRecord>();
	}

	// Token: 0x06008812 RID: 34834 RVA: 0x002BD6F8 File Offset: 0x002BB8F8
	public static int GetBoardIdFromAssetName(string name)
	{
		foreach (BoardDbfRecord boardDbfRecord in GameDbf.Board.GetRecords())
		{
			string prefab = boardDbfRecord.Prefab;
			if (!(name != prefab))
			{
				return boardDbfRecord.ID;
			}
		}
		return 0;
	}

	// Token: 0x06008813 RID: 34835 RVA: 0x002BD764 File Offset: 0x002BB964
	public static UnityEngine.Object Instantiate(GameObject original, GameObject parent, bool withRotation = false)
	{
		if (original == null)
		{
			return null;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		GameUtils.SetParent(gameObject, parent, withRotation);
		return gameObject;
	}

	// Token: 0x06008814 RID: 34836 RVA: 0x002BD77F File Offset: 0x002BB97F
	public static UnityEngine.Object Instantiate(Component original, GameObject parent, bool withRotation = false)
	{
		if (original == null)
		{
			return null;
		}
		Component component = UnityEngine.Object.Instantiate<Component>(original);
		GameUtils.SetParent(component, parent, withRotation);
		return component;
	}

	// Token: 0x06008815 RID: 34837 RVA: 0x002BD79A File Offset: 0x002BB99A
	public static UnityEngine.Object Instantiate(UnityEngine.Object original)
	{
		if (original == null)
		{
			return null;
		}
		return UnityEngine.Object.Instantiate(original);
	}

	// Token: 0x06008816 RID: 34838 RVA: 0x002BD7B0 File Offset: 0x002BB9B0
	public static UnityEngine.Object InstantiateGameObject(string path, GameObject parent = null, bool withRotation = false)
	{
		if (path == null)
		{
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(path, AssetLoadingOptions.None);
		if (parent != null)
		{
			GameUtils.SetParent(gameObject, parent, withRotation);
		}
		return gameObject;
	}

	// Token: 0x06008817 RID: 34839 RVA: 0x002BD7E6 File Offset: 0x002BB9E6
	public static void SetParent(Component child, Component parent, bool withRotation = false)
	{
		GameUtils.SetParent(child.transform, parent.transform, withRotation);
	}

	// Token: 0x06008818 RID: 34840 RVA: 0x002BD7FA File Offset: 0x002BB9FA
	public static void SetParent(GameObject child, Component parent, bool withRotation = false)
	{
		GameUtils.SetParent(child.transform, parent.transform, withRotation);
	}

	// Token: 0x06008819 RID: 34841 RVA: 0x002BD80E File Offset: 0x002BBA0E
	public static void SetParent(Component child, GameObject parent, bool withRotation = false)
	{
		GameUtils.SetParent(child.transform, parent.transform, withRotation);
	}

	// Token: 0x0600881A RID: 34842 RVA: 0x002BD822 File Offset: 0x002BBA22
	public static void SetParent(GameObject child, GameObject parent, bool withRotation = false)
	{
		GameUtils.SetParent(child.transform, parent.transform, withRotation);
	}

	// Token: 0x0600881B RID: 34843 RVA: 0x002BD838 File Offset: 0x002BBA38
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

	// Token: 0x0600881C RID: 34844 RVA: 0x002BD876 File Offset: 0x002BBA76
	public static void ResetTransform(GameObject obj)
	{
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		obj.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x0600881D RID: 34845 RVA: 0x002BD8A8 File Offset: 0x002BBAA8
	public static void ResetTransform(Component comp)
	{
		GameUtils.ResetTransform(comp.gameObject);
	}

	// Token: 0x0600881E RID: 34846 RVA: 0x002BD8B8 File Offset: 0x002BBAB8
	public static T LoadGameObjectWithComponent<T>(string assetPath) where T : Component
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetPath, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			return default(T);
		}
		T component = gameObject.GetComponent<T>();
		if (component == null)
		{
			Debug.LogError(string.Format("{0} object does not contain {1} component.", assetPath, typeof(T)));
			UnityEngine.Object.Destroy(gameObject);
			return default(T);
		}
		return component;
	}

	// Token: 0x0600881F RID: 34847 RVA: 0x002BD92C File Offset: 0x002BBB2C
	public static T FindChildByName<T>(Transform transform, string name) where T : Component
	{
		foreach (object obj in transform)
		{
			Transform transform2 = (Transform)obj;
			if (transform2.name == name)
			{
				return transform2.GetComponent<T>();
			}
			T t = GameUtils.FindChildByName<T>(transform2, name);
			if (t != null)
			{
				return t;
			}
		}
		return default(T);
	}

	// Token: 0x06008820 RID: 34848 RVA: 0x002BD9BC File Offset: 0x002BBBBC
	public static void PlayCardEffectDefSounds(CardEffectDef cardEffectDef)
	{
		if (cardEffectDef == null)
		{
			return;
		}
		foreach (string input in cardEffectDef.m_SoundSpellPaths)
		{
			AssetLoader.Get().InstantiatePrefab(input, delegate(AssetReference name, GameObject go, object data)
			{
				if (go == null)
				{
					Debug.LogError(string.Format("Unable to load spell object: {0}", name));
					return;
				}
				GameObject destroyObj = go;
				CardSoundSpell component = go.GetComponent<CardSoundSpell>();
				if (component == null)
				{
					Debug.LogError(string.Format("Card sound spell component not found: {0}", name));
					UnityEngine.Object.Destroy(destroyObj);
					return;
				}
				component.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
				{
					if (spell.GetActiveState() == SpellStateType.NONE)
					{
						UnityEngine.Object.Destroy(destroyObj);
					}
				});
				component.ForceDefaultAudioSource();
				component.Activate();
			}, null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x06008821 RID: 34849 RVA: 0x002BDA40 File Offset: 0x002BBC40
	public static bool LoadCardDefEmoteSound(List<EmoteEntryDef> emoteDefs, EmoteType type, GameUtils.EmoteSoundLoaded callback)
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
				return;
			}
			callback(go.GetComponent<CardSoundSpell>());
		}, null, AssetLoadingOptions.None);
		return true;
	}

	// Token: 0x06008822 RID: 34850 RVA: 0x002BDAB8 File Offset: 0x002BBCB8
	public static bool LoadAndPositionCardActor(string actorName, string heroCardID, TAG_PREMIUM premium, GameUtils.LoadActorCallback callback)
	{
		if (!string.IsNullOrEmpty(heroCardID))
		{
			DefLoader.Get().LoadFullDef(heroCardID, delegate(string cardID, DefLoader.DisposableFullDef def, object userData)
			{
				GameUtils.LoadAndPositionCardActor_OnFullDefLoaded(actorName, cardID, def, userData, callback);
			}, premium, null);
			return true;
		}
		return false;
	}

	// Token: 0x06008823 RID: 34851 RVA: 0x002BDB04 File Offset: 0x002BBD04
	private static void LoadAndPositionCardActor_OnFullDefLoaded(string actorName, string cardID, DefLoader.DisposableFullDef def, object userData, GameUtils.LoadActorCallback callback)
	{
		TAG_PREMIUM premium = (TAG_PREMIUM)userData;
		GameUtils.LoadActorCallbackInfo callbackData2 = new GameUtils.LoadActorCallbackInfo
		{
			fullDef = def,
			premium = premium
		};
		AssetLoader.Get().InstantiatePrefab(actorName, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			GameUtils.LoadAndPositionActorCard_OnActorLoaded(assetRef, go, callbackData, callback);
		}, callbackData2, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06008824 RID: 34852 RVA: 0x002BDB5C File Offset: 0x002BBD5C
	private static void LoadAndPositionActorCard_OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData, GameUtils.LoadActorCallback callback)
	{
		GameUtils.LoadActorCallbackInfo loadActorCallbackInfo = callbackData as GameUtils.LoadActorCallbackInfo;
		using (loadActorCallbackInfo.fullDef)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("GameUtils.OnHeroActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			}
			else
			{
				Actor component = go.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning(string.Format("GameUtils.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
				}
				else
				{
					component.SetPremium(loadActorCallbackInfo.premium);
					component.SetEntityDef(loadActorCallbackInfo.fullDef.EntityDef);
					component.SetCardDef(loadActorCallbackInfo.fullDef.DisposableCardDef);
					component.UpdateAllComponents();
					component.gameObject.name = loadActorCallbackInfo.fullDef.CardDef.name + "_actor";
					if (UniversalInputManager.UsePhoneUI)
					{
						SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
					}
					GemObject healthObject = component.GetHealthObject();
					if (healthObject != null)
					{
						healthObject.Hide();
					}
					if (callback != null)
					{
						callback(component);
					}
				}
			}
		}
	}

	// Token: 0x06008825 RID: 34853 RVA: 0x002BDC68 File Offset: 0x002BBE68
	public static bool AtPrereleaseEvent()
	{
		return FiresideGatheringManager.Get().IsPrerelease;
	}

	// Token: 0x06008826 RID: 34854 RVA: 0x002BDC74 File Offset: 0x002BBE74
	public static bool IsBoosterWild(BoosterDbId boosterId)
	{
		return boosterId != BoosterDbId.INVALID && GameUtils.IsBoosterWild(GameDbf.Booster.GetRecord((int)boosterId));
	}

	// Token: 0x06008827 RID: 34855 RVA: 0x002BDC8C File Offset: 0x002BBE8C
	public static bool IsBoosterWild(BoosterDbfRecord boosterRecord)
	{
		if (boosterRecord != null)
		{
			SpecialEventType eventType = SpecialEventManager.GetEventType(boosterRecord.StandardEvent);
			if (eventType != SpecialEventType.UNKNOWN && eventType != SpecialEventType.IGNORE && SpecialEventManager.Get().HasEventEnded(eventType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008828 RID: 34856 RVA: 0x002BDCC0 File Offset: 0x002BBEC0
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
			if (eventType != SpecialEventType.UNKNOWN && eventType != SpecialEventType.IGNORE && SpecialEventManager.Get().HasEventEnded(eventType))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04007244 RID: 29252
	public static GameUtils.StringEvent OnAnimationExitEvent = new GameUtils.StringEvent();

	// Token: 0x04007245 RID: 29253
	public static readonly IEnumerable<TAG_CLASS> ORDERED_HERO_CLASSES = new TAG_CLASS[]
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

	// Token: 0x04007246 RID: 29254
	public static readonly IEnumerable<TAG_CLASS> CLASSIC_ORDERED_HERO_CLASSES = new TAG_CLASS[]
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

	// Token: 0x04007247 RID: 29255
	private static ReactiveNetCacheObject<NetCache.NetCacheProfileProgress> s_profileProgress = ReactiveNetCacheObject<NetCache.NetCacheProfileProgress>.CreateInstance();

	// Token: 0x02002669 RID: 9833
	[Serializable]
	public class StringEvent : UnityEvent<string>
	{
	}

	// Token: 0x0200266A RID: 9834
	// (Invoke) Token: 0x06013711 RID: 79633
	public delegate void EmoteSoundLoaded(CardSoundSpell emoteObj);

	// Token: 0x0200266B RID: 9835
	// (Invoke) Token: 0x06013715 RID: 79637
	public delegate void LoadActorCallback(Actor actor);

	// Token: 0x0200266C RID: 9836
	private class LoadActorCallbackInfo
	{
		// Token: 0x0400F08A RID: 61578
		public DefLoader.DisposableFullDef fullDef;

		// Token: 0x0400F08B RID: 61579
		public TAG_PREMIUM premium;
	}
}
