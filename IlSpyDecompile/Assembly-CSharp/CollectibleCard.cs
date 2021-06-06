using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CollectibleCard
{
	private int m_CardDbId = -1;

	private DateTime m_LatestInsertDate = new DateTime(0L);

	private HashSet<string> m_SearchableTokens;

	private string m_LongSearchableName;

	private string m_LongSearchableNameNonEuropean;

	private string m_LongSearchableNameNoDiacritics;

	private EntityDef m_EntityDef;

	private TAG_PREMIUM m_PremiumType;

	private CardDbfRecord m_CardRecord;

	public int CardDbId => m_CardDbId;

	public string CardId => m_EntityDef.GetCardId();

	public string Name => CardTextBuilder.GetDefaultCardName(m_EntityDef);

	public string CardInHandText
	{
		get
		{
			CardTextBuilder cardTextBuilder = m_EntityDef.GetCardTextBuilder();
			if (cardTextBuilder != null)
			{
				return cardTextBuilder.BuildCardTextInHand(m_EntityDef);
			}
			return CardTextBuilder.GetDefaultCardTextInHand(m_EntityDef);
		}
	}

	public string ArtistName => m_EntityDef.GetArtistName();

	public int ManaCost => m_EntityDef.GetCost();

	public int Attack => m_EntityDef.GetATK();

	public int Health => m_EntityDef.GetHealth();

	public TAG_CARD_SET Set => m_EntityDef.GetCardSet();

	public TAG_CLASS Class => m_EntityDef.GetClass();

	public IEnumerable<TAG_CLASS> Classes => m_EntityDef.GetClasses();

	public TAG_MULTI_CLASS_GROUP MultiClassGroup => m_EntityDef.GetMultiClassGroup();

	public TAG_RARITY Rarity => m_EntityDef.GetRarity();

	public TAG_RACE Race => m_EntityDef.GetRace();

	public TAG_CARDTYPE CardType => m_EntityDef.GetCardType();

	public bool IsHeroSkin => m_EntityDef.IsHeroSkin();

	public TAG_PREMIUM PremiumType => m_PremiumType;

	public int SeenCount { get; set; }

	public int OwnedCount { get; set; }

	public int DisenchantCount
	{
		get
		{
			if (!IsCraftable)
			{
				return 0;
			}
			return Mathf.Max(OwnedCount - DefaultMaxCopiesPerDeck, 0);
		}
	}

	public TAG_SPELL_SCHOOL SpellSchool => m_EntityDef.GetSpellSchool();

	public int DefaultMaxCopiesPerDeck
	{
		get
		{
			if (!m_EntityDef.IsElite())
			{
				return 2;
			}
			return 1;
		}
	}

	public int CraftBuyCost => CraftingManager.Get().GetCardValue(CardId, PremiumType).GetBuyValue();

	public bool IsCraftable
	{
		get
		{
			if (CraftingManager.Get().GetCardValue(CardId, PremiumType) == null)
			{
				return false;
			}
			if (IsHeroSkin)
			{
				return false;
			}
			if (!FixedRewardsMgr.Get().CanCraftCard(CardId, PremiumType))
			{
				return false;
			}
			if (CraftingUI.IsCraftingEventForCardActive(CardId, PremiumType, out var _))
			{
				return true;
			}
			return false;
		}
	}

	public bool IsNewCard
	{
		get
		{
			if (OwnedCount > 0 && SeenCount < OwnedCount)
			{
				return SeenCount < DefaultMaxCopiesPerDeck;
			}
			return false;
		}
	}

	public int SuggestWeight => m_CardRecord.SuggestionWeight;

	public int ChangeVersion => m_CardRecord.ChangeVersion;

	public DateTime LatestInsertDate
	{
		get
		{
			return m_LatestInsertDate;
		}
		set
		{
			if (value > m_LatestInsertDate)
			{
				m_LatestInsertDate = value;
			}
		}
	}

	public CollectibleCard(CardDbfRecord cardRecord, EntityDef refEntityDef, TAG_PREMIUM premiumType)
	{
		m_CardDbId = cardRecord.ID;
		m_EntityDef = refEntityDef;
		m_PremiumType = premiumType;
		m_CardRecord = cardRecord;
	}

	public HashSet<string> GetSearchableTokens()
	{
		if (m_SearchableTokens == null)
		{
			m_SearchableTokens = new HashSet<string>();
			if (GameUtils.IsLegacySet(Set))
			{
				CollectibleCardFilter.AddSearchableTokensToSet(TAG_CARD_SET.LEGACY, GameStrings.HasCardSetName, GameStrings.GetCardSetName, m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet(TAG_CARD_SET.LEGACY, GameStrings.HasCardSetNameShortened, GameStrings.GetCardSetNameShortened, m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet(TAG_CARD_SET.LEGACY, GameStrings.HasCardSetNameInitials, GameStrings.GetCardSetNameInitials, m_SearchableTokens);
			}
			else
			{
				CollectibleCardFilter.AddSearchableTokensToSet(Set, GameStrings.HasCardSetName, GameStrings.GetCardSetName, m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet(Set, GameStrings.HasCardSetNameShortened, GameStrings.GetCardSetNameShortened, m_SearchableTokens);
				CollectibleCardFilter.AddSearchableTokensToSet(Set, GameStrings.HasCardSetNameInitials, GameStrings.GetCardSetNameInitials, m_SearchableTokens);
			}
			CollectibleCardFilter.AddSearchableTokensToSet(Rarity, GameStrings.HasRarityText, GameStrings.GetRarityText, m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet(Race, GameStrings.HasRaceName, GameStrings.GetRaceName, m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet(CardType, GameStrings.HasCardTypeName, GameStrings.GetCardTypeName, m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet(m_EntityDef.GetMultiClassGroup(), GameStrings.HasMultiClassGroupName, GameStrings.GetMultiClassGroupName, m_SearchableTokens);
			CollectibleCardFilter.AddSearchableTokensToSet(SpellSchool, GameStrings.HasSpellSchoolName, GameStrings.GetSpellSchoolName, m_SearchableTokens);
			if (m_EntityDef.HasTag(GAME_TAG.MINI_SET))
			{
				CollectibleCardFilter.AddSearchableTokensToSet(Set, GameStrings.HasMiniSetName, GameStrings.GetMiniSetName, m_SearchableTokens);
			}
			if (m_EntityDef.IsMultiClass())
			{
				foreach (TAG_CLASS @class in m_EntityDef.GetClasses())
				{
					if (GameStrings.HasClassName(@class))
					{
						CollectibleCardFilter.AddSingleSearchableTokenToSet(GameStrings.GetClassName(@class), m_SearchableTokens);
					}
				}
			}
			if (m_EntityDef.HasCharge() && GameStrings.HasKeywordName(GAME_TAG.CHARGE))
			{
				CollectibleCardFilter.AddSingleSearchableTokenToSet(GameStrings.GetKeywordName(GAME_TAG.CHARGE), m_SearchableTokens);
			}
			if (Race == TAG_RACE.ALL)
			{
				foreach (TAG_RACE value in Enum.GetValues(typeof(TAG_RACE)))
				{
					CollectibleCardFilter.AddSearchableTokensToSet(value, GameStrings.HasRaceName, GameStrings.GetRaceName, m_SearchableTokens);
				}
			}
		}
		return m_SearchableTokens;
	}

	public bool FindTextInCard(string searchStr)
	{
		searchStr = searchStr.Trim();
		if (GetSearchableTokens().Contains(searchStr))
		{
			return true;
		}
		if (m_LongSearchableName == null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Name);
			stringBuilder.Append(" ");
			stringBuilder.Append(CardInHandText);
			foreach (CardAdditonalSearchTermsDbfRecord searchTerm in m_CardRecord.SearchTerms)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(searchTerm.SearchTerm.GetString());
			}
			m_LongSearchableName = UberText.RemoveMarkupAndCollapseWhitespaces(stringBuilder.ToString());
			m_LongSearchableName = m_LongSearchableName.Trim().ToLower();
			m_LongSearchableNameNonEuropean = CollectibleCardFilter.ConvertEuropeanCharacters(m_LongSearchableName);
			m_LongSearchableNameNoDiacritics = CollectibleCardFilter.RemoveDiacritics(m_LongSearchableName);
		}
		if (!m_LongSearchableName.Contains(searchStr, StringComparison.OrdinalIgnoreCase) && !m_LongSearchableNameNonEuropean.Contains(searchStr, StringComparison.OrdinalIgnoreCase))
		{
			return m_LongSearchableNameNoDiacritics.Contains(searchStr, StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	public static bool FindTextInternational(string searchStr, string stringToSearch)
	{
		string str = CollectibleCardFilter.ConvertEuropeanCharacters(stringToSearch);
		string str2 = CollectibleCardFilter.RemoveDiacritics(stringToSearch);
		if (!stringToSearch.Contains(searchStr, StringComparison.OrdinalIgnoreCase) && !str.Contains(searchStr, StringComparison.OrdinalIgnoreCase))
		{
			return str2.Contains(searchStr, StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	public void AddCounts(int addOwnedCount, int addSeenCount, DateTime latestInsertDate)
	{
		OwnedCount += addOwnedCount;
		SeenCount += addSeenCount;
		LatestInsertDate = latestInsertDate;
	}

	public void RemoveCounts(int removeOwnedCount)
	{
		OwnedCount = Mathf.Max(OwnedCount - removeOwnedCount, 0);
	}

	public void ClearCounts()
	{
		OwnedCount = 0;
		SeenCount = 0;
	}

	public EntityDef GetEntityDef()
	{
		return m_EntityDef;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (CardDbId == ((CollectibleCard)obj).CardDbId)
		{
			return PremiumType == ((CollectibleCard)obj).PremiumType;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (int)(CardId.GetHashCode() + PremiumType);
	}
}
