using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blizzard.T5.Core;

public class CollectibleCardFilter
{
	[Flags]
	public enum FilterMask
	{
		NONE = 0x0,
		PREMIUM_NORMAL = 0x2,
		PREMIUM_GOLDEN = 0x4,
		PREMIUM_DIAMOND = 0x8,
		PREMIUM_ALL = 0xE,
		OWNED = 0x10,
		UNOWNED = 0x20,
		ALL = -1
	}

	public delegate void OnResultsUpdated();

	private TAG_CARD_SET[] m_filterCardSets;

	protected TAG_CLASS[] m_filterClasses;

	private TAG_CARDTYPE[] m_filterCardTypes;

	private int? m_filterManaCost;

	private int? m_filterOwnedMinimum = 1;

	private List<FilterMask> m_filterMasks;

	private bool? m_filterOnlyCraftable;

	private bool? m_filterOnlyUncraftable;

	private FilterMask? m_craftableFilterPremiums;

	private string m_filterText;

	private bool m_filterIsHero;

	private DeckRuleset m_deckRuleset;

	private HashSet<string> m_leagueBannedCardsSubset;

	private List<int> m_specificCards;

	private bool? m_filterCounterpartCards = true;

	private static Map<char, string> s_europeanConversionTable = new Map<char, string>
	{
		{ 'œ', "oe" },
		{ 'æ', "ae" },
		{ '’', "'" },
		{ '«', "\"" },
		{ '»', "\"" },
		{ 'ä', "ae" },
		{ 'ü', "ue" },
		{ 'ö', "oe" },
		{ 'ß', "ss" }
	};

	public static readonly char[] SearchTagColons = new char[2] { ':', '：' };

	public static readonly char[] SearchTokenDelimiters = new char[2] { ' ', '\t' };

	public bool IsManaCostFilterActive => m_filterManaCost.HasValue;

	public static FilterMask FilterMaskFromPremiumType(TAG_PREMIUM premiumType)
	{
		FilterMask filterMask = FilterMask.NONE;
		return premiumType switch
		{
			TAG_PREMIUM.GOLDEN => filterMask | FilterMask.PREMIUM_GOLDEN, 
			TAG_PREMIUM.DIAMOND => filterMask | FilterMask.PREMIUM_DIAMOND, 
			_ => filterMask | FilterMask.PREMIUM_NORMAL, 
		};
	}

	public void SetDeckRuleset(DeckRuleset deckRuleset)
	{
		m_deckRuleset = deckRuleset;
	}

	public void FilterTheseCardSets(params TAG_CARD_SET[] cardSets)
	{
		m_filterCardSets = null;
		if (cardSets != null && cardSets.Length != 0)
		{
			m_filterCardSets = cardSets;
		}
	}

	public bool CardSetFilterIncludesWild()
	{
		if (m_filterCardSets == null && m_specificCards == null)
		{
			return true;
		}
		if (m_filterCardSets != null)
		{
			TAG_CARD_SET[] filterCardSets = m_filterCardSets;
			for (int i = 0; i < filterCardSets.Length; i++)
			{
				if (GameUtils.IsWildCardSet(filterCardSets[i]))
				{
					return true;
				}
			}
		}
		if (m_specificCards != null)
		{
			foreach (int specificCard in m_specificCards)
			{
				if (GameUtils.IsWildCard(specificCard))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CardSetFilterIsAllStandardSets()
	{
		if (m_filterCardSets == null)
		{
			return false;
		}
		List<TAG_CARD_SET> equals = new List<TAG_CARD_SET>(GameUtils.GetStandardSets());
		return new HashSet<TAG_CARD_SET>(m_filterCardSets).SetEquals(equals);
	}

	public bool CardSetFilterIsClassicSet()
	{
		if (m_filterCardSets == null)
		{
			return false;
		}
		List<TAG_CARD_SET> equals = new List<TAG_CARD_SET> { TAG_CARD_SET.VANILLA };
		return new HashSet<TAG_CARD_SET>(m_filterCardSets).SetEquals(equals);
	}

	public void FilterTheseClasses(params TAG_CLASS[] classTypes)
	{
		m_filterClasses = null;
		if (classTypes != null && classTypes.Length != 0)
		{
			m_filterClasses = classTypes;
		}
	}

	public void FilterTheseCardTypes(params TAG_CARDTYPE[] cardTypes)
	{
		m_filterCardTypes = null;
		if (cardTypes != null && cardTypes.Length != 0)
		{
			m_filterCardTypes = cardTypes;
		}
	}

	public void FilterManaCost(int? manaCost)
	{
		m_filterManaCost = manaCost;
	}

	public void FilterOnlyOwned(bool owned)
	{
		m_filterOwnedMinimum = null;
		if (owned)
		{
			m_filterOwnedMinimum = 1;
		}
	}

	public void FilterByMask(List<FilterMask> filterMasks)
	{
		if (filterMasks == null)
		{
			filterMasks = new List<FilterMask> { FilterMask.ALL };
		}
		m_filterMasks = filterMasks;
	}

	public void FilterOnlyCraftable(bool onlyCraftable)
	{
		m_filterOnlyCraftable = null;
		if (onlyCraftable)
		{
			m_filterOnlyCraftable = true;
		}
	}

	public void FilterOnlyUncraftable(bool onlyUncraftable, FilterMask? premiums)
	{
		m_filterOnlyUncraftable = null;
		m_craftableFilterPremiums = premiums;
		if (onlyUncraftable)
		{
			m_filterOnlyUncraftable = true;
		}
	}

	public void FilterLeagueBannedCardsSubset(HashSet<string> leagueBannedCardsSubset)
	{
		m_leagueBannedCardsSubset = leagueBannedCardsSubset;
	}

	public void FilterSearchText(string searchText)
	{
		m_filterText = searchText;
	}

	public bool HasSearchText()
	{
		return !string.IsNullOrEmpty(m_filterText);
	}

	public void FilterHero(bool isHero)
	{
		m_filterIsHero = isHero;
	}

	public void FilterSpecificCards(List<int> specificCards)
	{
		m_specificCards = specificCards;
	}

	public CollectionManager.FindCardsResult GenerateResults()
	{
		bool? isCraftable = m_filterOnlyCraftable;
		if (m_filterOnlyUncraftable.HasValue && m_filterOnlyUncraftable == true)
		{
			isCraftable = false;
		}
		return CollectionManager.Get().FindOrderedCards(m_filterText, manaCost: m_filterManaCost, filterMasks: m_filterMasks, theseCardSets: m_filterCardSets, theseClassTypes: m_filterClasses, theseCardTypes: m_filterCardTypes, rarity: null, race: null, isHero: m_filterIsHero, minOwned: m_filterOwnedMinimum, notSeen: null, isCraftable: isCraftable, craftableFilterPremiums: m_craftableFilterPremiums, priorityFilters: null, deckRuleset: m_deckRuleset, returnAfterFirstResult: false, leagueBannedCardsSubset: m_leagueBannedCardsSubset, specificCards: m_specificCards, filterCounterpartCards: m_filterCounterpartCards);
	}

	private static void AddSearchableTokensToSet(string str, HashSet<string> addToList, bool split = true)
	{
		string[] array = (split ? str.Split(SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries) : new string[1] { str });
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			AddSingleSearchableTokenToSet(array2[i], addToList);
		}
		if (array.Length > 1)
		{
			AddSingleSearchableTokenToSet(str, addToList);
		}
	}

	public static void AddSearchableTokensToSet<T>(T structType, Func<T, bool> hasTypeString, Func<T, string> getTypeString, HashSet<string> addToList) where T : struct
	{
		if (hasTypeString(structType))
		{
			AddSearchableTokensToSet(getTypeString(structType), addToList);
		}
	}

	public static void AddSingleSearchableTokenToSet(string token, HashSet<string> addToList)
	{
		string text = token.ToLower();
		string text2 = ConvertEuropeanCharacters(text);
		string text3 = RemoveDiacritics(text);
		addToList.Add(text);
		if (!text.Equals(text2))
		{
			addToList.Add(text2);
		}
		if (!text.Equals(text3))
		{
			addToList.Add(text3);
		}
	}

	public static List<CollectionManager.CollectibleCardFilterFunc> FiltersFromSearchString(string searchString)
	{
		List<CollectionManager.CollectibleCardFilterFunc> list = new List<CollectionManager.CollectibleCardFilterFunc>();
		string text = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_GOLDEN");
		string text2 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_DIAMOND");
		string text3 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ARTIST");
		string text4 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_HEALTH");
		string text5 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ATTACK");
		string text6 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_OWNED");
		string text7 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MANA").ToLower();
		string text8 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_RARITY");
		string text9 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_TYPE");
		string text10 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING");
		string text11 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EXTRA");
		string text12 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_NEW");
		string text13 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_HAS");
		string[] array = searchString.ToLower().Split(SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries);
		StringBuilder regularTokens = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == text11 || array[i] == text10)
			{
				continue;
			}
			if (array[i] == text12)
			{
				list.Add((CollectibleCard card) => card.IsNewCard);
				continue;
			}
			if (array[i] == text)
			{
				list.Add((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.GOLDEN);
				continue;
			}
			if (array[i] == text2)
			{
				list.Add((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.DIAMOND);
				continue;
			}
			bool flag = false;
			if (((IEnumerable<char>)SearchTagColons).Any((Func<char, bool>)array[i].Contains))
			{
				string[] array2 = array[i].Split(SearchTagColons);
				if (array2.Length == 2)
				{
					string text14 = array2[0].Trim();
					string val = array2[1].Trim();
					GeneralUtils.ParseNumericRange(val, out var isNumericalValue, out var minVal, out var maxVal);
					if (isNumericalValue)
					{
						if (text14 == text5)
						{
							list.Add((CollectibleCard card) => card.Attack >= minVal && card.Attack <= maxVal);
							list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION || card.CardType == TAG_CARDTYPE.WEAPON);
							flag = true;
						}
						if (text14 == text4)
						{
							list.Add((CollectibleCard card) => card.Health >= minVal && card.Health <= maxVal);
							list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION);
							flag = true;
						}
						if (text14 == text7)
						{
							list.Add((CollectibleCard card) => card.ManaCost >= minVal && card.ManaCost <= maxVal);
							flag = true;
						}
						if (text14 == text6)
						{
							list.Add((CollectibleCard card) => card.OwnedCount >= minVal && card.OwnedCount <= maxVal);
							flag = true;
						}
					}
					else
					{
						if (text14 == text3)
						{
							list.Add((CollectibleCard card) => CollectibleCard.FindTextInternational(val, card.ArtistName));
							flag = true;
						}
						if (text14 == text8)
						{
							list.Add((CollectibleCard card) => CollectibleCard.FindTextInternational(val, GameStrings.GetRarityText(card.Rarity)));
							flag = true;
						}
						if (text14 == text9)
						{
							list.Add(delegate(CollectibleCard card)
							{
								string cardTypeName = GameStrings.GetCardTypeName(card.CardType);
								return cardTypeName != null && CollectibleCard.FindTextInternational(val, cardTypeName);
							});
							flag = true;
						}
						if (text14 == text13)
						{
							list.Add((CollectibleCard card) => card.FindTextInCard(val));
							flag = true;
						}
						if (text14 == text5)
						{
							string text15 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_ATTACK").ToLower();
							string text16 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_ATTACK").ToLower();
							string text17 = val.ToLower();
							if (text17 == text15)
							{
								list.Add((CollectibleCard card) => card.Attack % 2 == 0);
								list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION || card.CardType == TAG_CARDTYPE.WEAPON);
								flag = true;
							}
							else if (text17 == text16)
							{
								list.Add((CollectibleCard card) => card.Attack % 2 == 1);
								list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION || card.CardType == TAG_CARDTYPE.WEAPON);
								flag = true;
							}
						}
						if (text14 == text4)
						{
							string text18 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_HEALTH").ToLower();
							string text19 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_HEALTH").ToLower();
							string text20 = val.ToLower();
							if (text20 == text18)
							{
								list.Add((CollectibleCard card) => card.Health % 2 == 0);
								list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION);
								flag = true;
							}
							else if (text20 == text19)
							{
								list.Add((CollectibleCard card) => card.Health % 2 == 1);
								list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION);
								flag = true;
							}
						}
						if (text14 == text7)
						{
							string text21 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA").ToLower();
							string text22 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA").ToLower();
							string text23 = val.ToLower();
							if (text23 == text21)
							{
								list.Add((CollectibleCard card) => card.ManaCost % 2 == 0);
								flag = true;
							}
							else if (text23 == text22)
							{
								list.Add((CollectibleCard card) => card.ManaCost % 2 == 1);
								flag = true;
							}
						}
						if (text14 == text6)
						{
							string text24 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_CARDS").ToLower();
							string text25 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_CARDS").ToLower();
							string text26 = val.ToLower();
							if (text26 == text24)
							{
								list.Add((CollectibleCard card) => card.OwnedCount % 2 == 0);
								flag = true;
							}
							else if (text26 == text25)
							{
								list.Add((CollectibleCard card) => card.OwnedCount % 2 == 1);
								flag = true;
							}
						}
					}
				}
			}
			if (!flag)
			{
				regularTokens.Append(array[i]);
				regularTokens.Append(" ");
			}
		}
		list.Add((CollectibleCard card) => card.FindTextInCard(regularTokens.ToString()));
		return list;
	}

	public static string ConvertEuropeanCharacters(string input)
	{
		int length = input.Length;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			if (s_europeanConversionTable.TryGetValue(input[i], out var value))
			{
				stringBuilder.Append(value);
			}
			else
			{
				stringBuilder.Append(input[i]);
			}
		}
		return stringBuilder.ToString();
	}

	public static string RemoveDiacritics(string input)
	{
		string text = input.Normalize(NormalizationForm.FormD);
		int length = text.Length;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			if (CharUnicodeInfo.GetUnicodeCategory(text[i]) != UnicodeCategory.NonSpacingMark)
			{
				stringBuilder.Append(text[i]);
			}
		}
		return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
	}

	public static string CreateSearchTerm_Mana_OddEven(bool isOdd)
	{
		return string.Format("{0}{1}{2}", GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MANA"), SearchTagColons.First(), GameStrings.Get(isOdd ? "GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA" : "GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA"));
	}

	public void ClearOutFiltersFromSetFilterDropdown()
	{
		m_specificCards = null;
		m_filterCardSets = null;
	}
}
