using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blizzard.T5.Core;

// Token: 0x020000F5 RID: 245
public class CollectibleCardFilter
{
	// Token: 0x06000E3A RID: 3642 RVA: 0x0004FEC0 File Offset: 0x0004E0C0
	public static CollectibleCardFilter.FilterMask FilterMaskFromPremiumType(TAG_PREMIUM premiumType)
	{
		CollectibleCardFilter.FilterMask filterMask = CollectibleCardFilter.FilterMask.NONE;
		if (premiumType != TAG_PREMIUM.GOLDEN)
		{
			if (premiumType != TAG_PREMIUM.DIAMOND)
			{
				filterMask |= CollectibleCardFilter.FilterMask.PREMIUM_NORMAL;
			}
			else
			{
				filterMask |= CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND;
			}
		}
		else
		{
			filterMask |= CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN;
		}
		return filterMask;
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x0004FEEA File Offset: 0x0004E0EA
	public void SetDeckRuleset(DeckRuleset deckRuleset)
	{
		this.m_deckRuleset = deckRuleset;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0004FEF3 File Offset: 0x0004E0F3
	public void FilterTheseCardSets(params TAG_CARD_SET[] cardSets)
	{
		this.m_filterCardSets = null;
		if (cardSets != null && cardSets.Length != 0)
		{
			this.m_filterCardSets = cardSets;
		}
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x0004FF0C File Offset: 0x0004E10C
	public bool CardSetFilterIncludesWild()
	{
		if (this.m_filterCardSets == null && this.m_specificCards == null)
		{
			return true;
		}
		if (this.m_filterCardSets != null)
		{
			TAG_CARD_SET[] filterCardSets = this.m_filterCardSets;
			for (int i = 0; i < filterCardSets.Length; i++)
			{
				if (GameUtils.IsWildCardSet(filterCardSets[i]))
				{
					return true;
				}
			}
		}
		if (this.m_specificCards != null)
		{
			using (List<int>.Enumerator enumerator = this.m_specificCards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (GameUtils.IsWildCard(enumerator.Current))
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0004FFA8 File Offset: 0x0004E1A8
	public bool CardSetFilterIsAllStandardSets()
	{
		if (this.m_filterCardSets == null)
		{
			return false;
		}
		List<TAG_CARD_SET> equals = new List<TAG_CARD_SET>(GameUtils.GetStandardSets());
		return new HashSet<TAG_CARD_SET>(this.m_filterCardSets).SetEquals(equals);
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0004FFDC File Offset: 0x0004E1DC
	public bool CardSetFilterIsClassicSet()
	{
		if (this.m_filterCardSets == null)
		{
			return false;
		}
		List<TAG_CARD_SET> equals = new List<TAG_CARD_SET>
		{
			TAG_CARD_SET.VANILLA
		};
		return new HashSet<TAG_CARD_SET>(this.m_filterCardSets).SetEquals(equals);
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00050015 File Offset: 0x0004E215
	public void FilterTheseClasses(params TAG_CLASS[] classTypes)
	{
		this.m_filterClasses = null;
		if (classTypes != null && classTypes.Length != 0)
		{
			this.m_filterClasses = classTypes;
		}
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0005002C File Offset: 0x0004E22C
	public void FilterTheseCardTypes(params TAG_CARDTYPE[] cardTypes)
	{
		this.m_filterCardTypes = null;
		if (cardTypes != null && cardTypes.Length != 0)
		{
			this.m_filterCardTypes = cardTypes;
		}
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00050043 File Offset: 0x0004E243
	public void FilterManaCost(int? manaCost)
	{
		this.m_filterManaCost = manaCost;
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0005004C File Offset: 0x0004E24C
	public bool IsManaCostFilterActive
	{
		get
		{
			return this.m_filterManaCost != null;
		}
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x00050059 File Offset: 0x0004E259
	public void FilterOnlyOwned(bool owned)
	{
		this.m_filterOwnedMinimum = null;
		if (owned)
		{
			this.m_filterOwnedMinimum = new int?(1);
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00050076 File Offset: 0x0004E276
	public void FilterByMask(List<CollectibleCardFilter.FilterMask> filterMasks)
	{
		if (filterMasks == null)
		{
			filterMasks = new List<CollectibleCardFilter.FilterMask>
			{
				CollectibleCardFilter.FilterMask.ALL
			};
		}
		this.m_filterMasks = filterMasks;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00050090 File Offset: 0x0004E290
	public void FilterOnlyCraftable(bool onlyCraftable)
	{
		this.m_filterOnlyCraftable = null;
		if (onlyCraftable)
		{
			this.m_filterOnlyCraftable = new bool?(true);
		}
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x000500AD File Offset: 0x0004E2AD
	public void FilterOnlyUncraftable(bool onlyUncraftable, CollectibleCardFilter.FilterMask? premiums)
	{
		this.m_filterOnlyUncraftable = null;
		this.m_craftableFilterPremiums = premiums;
		if (onlyUncraftable)
		{
			this.m_filterOnlyUncraftable = new bool?(true);
		}
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000500D1 File Offset: 0x0004E2D1
	public void FilterLeagueBannedCardsSubset(HashSet<string> leagueBannedCardsSubset)
	{
		this.m_leagueBannedCardsSubset = leagueBannedCardsSubset;
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x000500DA File Offset: 0x0004E2DA
	public void FilterSearchText(string searchText)
	{
		this.m_filterText = searchText;
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000500E3 File Offset: 0x0004E2E3
	public bool HasSearchText()
	{
		return !string.IsNullOrEmpty(this.m_filterText);
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000500F3 File Offset: 0x0004E2F3
	public void FilterHero(bool isHero)
	{
		this.m_filterIsHero = isHero;
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000500FC File Offset: 0x0004E2FC
	public void FilterSpecificCards(List<int> specificCards)
	{
		this.m_specificCards = specificCards;
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x00050108 File Offset: 0x0004E308
	public CollectionManager.FindCardsResult GenerateResults()
	{
		bool? filterOnlyCraftable = this.m_filterOnlyCraftable;
		if (this.m_filterOnlyUncraftable != null)
		{
			bool? filterOnlyUncraftable = this.m_filterOnlyUncraftable;
			bool flag = true;
			if (filterOnlyUncraftable.GetValueOrDefault() == flag & filterOnlyUncraftable != null)
			{
				filterOnlyCraftable = new bool?(false);
			}
		}
		CollectionManager collectionManager = CollectionManager.Get();
		string filterText = this.m_filterText;
		int? filterManaCost = this.m_filterManaCost;
		return collectionManager.FindOrderedCards(filterText, this.m_filterMasks, filterManaCost, this.m_filterCardSets, this.m_filterClasses, this.m_filterCardTypes, null, null, new bool?(this.m_filterIsHero), this.m_filterOwnedMinimum, null, filterOnlyCraftable, this.m_craftableFilterPremiums, null, this.m_deckRuleset, false, this.m_leagueBannedCardsSubset, this.m_specificCards, this.m_filterCounterpartCards);
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000501D0 File Offset: 0x0004E3D0
	private static void AddSearchableTokensToSet(string str, HashSet<string> addToList, bool split = true)
	{
		string[] array;
		if (!split)
		{
			(array = new string[1])[0] = str;
		}
		else
		{
			array = str.Split(CollectibleCardFilter.SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries);
		}
		string[] array2 = array;
		string[] array3 = array2;
		for (int i = 0; i < array3.Length; i++)
		{
			CollectibleCardFilter.AddSingleSearchableTokenToSet(array3[i], addToList);
		}
		if (array2.Length > 1)
		{
			CollectibleCardFilter.AddSingleSearchableTokenToSet(str, addToList);
		}
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0005021F File Offset: 0x0004E41F
	public static void AddSearchableTokensToSet<T>(T structType, Func<T, bool> hasTypeString, Func<T, string> getTypeString, HashSet<string> addToList) where T : struct
	{
		if (hasTypeString(structType))
		{
			CollectibleCardFilter.AddSearchableTokensToSet(getTypeString(structType), addToList, true);
		}
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x00050238 File Offset: 0x0004E438
	public static void AddSingleSearchableTokenToSet(string token, HashSet<string> addToList)
	{
		string text = token.ToLower();
		string text2 = CollectibleCardFilter.ConvertEuropeanCharacters(text);
		string text3 = CollectibleCardFilter.RemoveDiacritics(text);
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

	// Token: 0x06000E51 RID: 3665 RVA: 0x00050284 File Offset: 0x0004E484
	public static List<CollectionManager.CollectibleCardFilterFunc> FiltersFromSearchString(string searchString)
	{
		List<CollectionManager.CollectibleCardFilterFunc> list = new List<CollectionManager.CollectibleCardFilterFunc>();
		string b = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_GOLDEN");
		string b2 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_DIAMOND");
		string b3 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ARTIST");
		string b4 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_HEALTH");
		string b5 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ATTACK");
		string b6 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_OWNED");
		string b7 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MANA").ToLower();
		string b8 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_RARITY");
		string b9 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_TYPE");
		string b10 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING");
		string b11 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EXTRA");
		string b12 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_NEW");
		string b13 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_HAS");
		string[] array = searchString.ToLower().Split(CollectibleCardFilter.SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries);
		StringBuilder regularTokens = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			if (!(array[i] == b11) && !(array[i] == b10))
			{
				if (array[i] == b12)
				{
					list.Add((CollectibleCard card) => card.IsNewCard);
				}
				else if (array[i] == b)
				{
					list.Add((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.GOLDEN);
				}
				else if (array[i] == b2)
				{
					list.Add((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.DIAMOND);
				}
				else
				{
					bool flag = false;
					if (CollectibleCardFilter.SearchTagColons.Any(new Func<char, bool>(array[i].Contains)))
					{
						string[] array2 = array[i].Split(CollectibleCardFilter.SearchTagColons);
						if (array2.Length == 2)
						{
							string a = array2[0].Trim();
							string val = array2[1].Trim();
							bool flag2;
							int minVal;
							int maxVal;
							GeneralUtils.ParseNumericRange(val, out flag2, out minVal, out maxVal);
							if (flag2)
							{
								if (a == b5)
								{
									list.Add((CollectibleCard card) => card.Attack >= minVal && card.Attack <= maxVal);
									list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION || card.CardType == TAG_CARDTYPE.WEAPON);
									flag = true;
								}
								if (a == b4)
								{
									list.Add((CollectibleCard card) => card.Health >= minVal && card.Health <= maxVal);
									list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION);
									flag = true;
								}
								if (a == b7)
								{
									list.Add((CollectibleCard card) => card.ManaCost >= minVal && card.ManaCost <= maxVal);
									flag = true;
								}
								if (a == b6)
								{
									list.Add((CollectibleCard card) => card.OwnedCount >= minVal && card.OwnedCount <= maxVal);
									flag = true;
								}
							}
							else
							{
								if (a == b3)
								{
									list.Add((CollectibleCard card) => CollectibleCard.FindTextInternational(val, card.ArtistName));
									flag = true;
								}
								if (a == b8)
								{
									list.Add((CollectibleCard card) => CollectibleCard.FindTextInternational(val, GameStrings.GetRarityText(card.Rarity)));
									flag = true;
								}
								if (a == b9)
								{
									list.Add(delegate(CollectibleCard card)
									{
										string cardTypeName = GameStrings.GetCardTypeName(card.CardType);
										return cardTypeName != null && CollectibleCard.FindTextInternational(val, cardTypeName);
									});
									flag = true;
								}
								if (a == b13)
								{
									list.Add((CollectibleCard card) => card.FindTextInCard(val));
									flag = true;
								}
								if (a == b5)
								{
									string b14 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_ATTACK").ToLower();
									string b15 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_ATTACK").ToLower();
									string a2 = val.ToLower();
									if (a2 == b14)
									{
										list.Add((CollectibleCard card) => card.Attack % 2 == 0);
										list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION || card.CardType == TAG_CARDTYPE.WEAPON);
										flag = true;
									}
									else if (a2 == b15)
									{
										list.Add((CollectibleCard card) => card.Attack % 2 == 1);
										list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION || card.CardType == TAG_CARDTYPE.WEAPON);
										flag = true;
									}
								}
								if (a == b4)
								{
									string b16 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_HEALTH").ToLower();
									string b17 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_HEALTH").ToLower();
									string a3 = val.ToLower();
									if (a3 == b16)
									{
										list.Add((CollectibleCard card) => card.Health % 2 == 0);
										list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION);
										flag = true;
									}
									else if (a3 == b17)
									{
										list.Add((CollectibleCard card) => card.Health % 2 == 1);
										list.Add((CollectibleCard card) => card.CardType == TAG_CARDTYPE.MINION);
										flag = true;
									}
								}
								if (a == b7)
								{
									string b18 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA").ToLower();
									string b19 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA").ToLower();
									string a4 = val.ToLower();
									if (a4 == b18)
									{
										list.Add((CollectibleCard card) => card.ManaCost % 2 == 0);
										flag = true;
									}
									else if (a4 == b19)
									{
										list.Add((CollectibleCard card) => card.ManaCost % 2 == 1);
										flag = true;
									}
								}
								if (a == b6)
								{
									string b20 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_CARDS").ToLower();
									string b21 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_CARDS").ToLower();
									string a5 = val.ToLower();
									if (a5 == b20)
									{
										list.Add((CollectibleCard card) => card.OwnedCount % 2 == 0);
										flag = true;
									}
									else if (a5 == b21)
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
			}
		}
		list.Add((CollectibleCard card) => card.FindTextInCard(regularTokens.ToString()));
		return list;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0005097C File Offset: 0x0004EB7C
	public static string ConvertEuropeanCharacters(string input)
	{
		int length = input.Length;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			string value;
			if (CollectibleCardFilter.s_europeanConversionTable.TryGetValue(input[i], out value))
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

	// Token: 0x06000E53 RID: 3667 RVA: 0x000509D8 File Offset: 0x0004EBD8
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

	// Token: 0x06000E54 RID: 3668 RVA: 0x00050A2F File Offset: 0x0004EC2F
	public static string CreateSearchTerm_Mana_OddEven(bool isOdd)
	{
		return string.Format("{0}{1}{2}", GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MANA"), CollectibleCardFilter.SearchTagColons.First<char>(), GameStrings.Get(isOdd ? "GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA" : "GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA"));
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x00050A68 File Offset: 0x0004EC68
	public void ClearOutFiltersFromSetFilterDropdown()
	{
		this.m_specificCards = null;
		this.m_filterCardSets = null;
	}

	// Token: 0x040009CA RID: 2506
	private TAG_CARD_SET[] m_filterCardSets;

	// Token: 0x040009CB RID: 2507
	protected TAG_CLASS[] m_filterClasses;

	// Token: 0x040009CC RID: 2508
	private TAG_CARDTYPE[] m_filterCardTypes;

	// Token: 0x040009CD RID: 2509
	private int? m_filterManaCost;

	// Token: 0x040009CE RID: 2510
	private int? m_filterOwnedMinimum = new int?(1);

	// Token: 0x040009CF RID: 2511
	private List<CollectibleCardFilter.FilterMask> m_filterMasks;

	// Token: 0x040009D0 RID: 2512
	private bool? m_filterOnlyCraftable;

	// Token: 0x040009D1 RID: 2513
	private bool? m_filterOnlyUncraftable;

	// Token: 0x040009D2 RID: 2514
	private CollectibleCardFilter.FilterMask? m_craftableFilterPremiums;

	// Token: 0x040009D3 RID: 2515
	private string m_filterText;

	// Token: 0x040009D4 RID: 2516
	private bool m_filterIsHero;

	// Token: 0x040009D5 RID: 2517
	private DeckRuleset m_deckRuleset;

	// Token: 0x040009D6 RID: 2518
	private HashSet<string> m_leagueBannedCardsSubset;

	// Token: 0x040009D7 RID: 2519
	private List<int> m_specificCards;

	// Token: 0x040009D8 RID: 2520
	private bool? m_filterCounterpartCards = new bool?(true);

	// Token: 0x040009D9 RID: 2521
	private static Map<char, string> s_europeanConversionTable = new Map<char, string>
	{
		{
			'œ',
			"oe"
		},
		{
			'æ',
			"ae"
		},
		{
			'’',
			"'"
		},
		{
			'«',
			"\""
		},
		{
			'»',
			"\""
		},
		{
			'ä',
			"ae"
		},
		{
			'ü',
			"ue"
		},
		{
			'ö',
			"oe"
		},
		{
			'ß',
			"ss"
		}
	};

	// Token: 0x040009DA RID: 2522
	public static readonly char[] SearchTagColons = new char[]
	{
		':',
		'：'
	};

	// Token: 0x040009DB RID: 2523
	public static readonly char[] SearchTokenDelimiters = new char[]
	{
		' ',
		'\t'
	};

	// Token: 0x02001410 RID: 5136
	[Flags]
	public enum FilterMask
	{
		// Token: 0x0400A8C4 RID: 43204
		NONE = 0,
		// Token: 0x0400A8C5 RID: 43205
		PREMIUM_NORMAL = 2,
		// Token: 0x0400A8C6 RID: 43206
		PREMIUM_GOLDEN = 4,
		// Token: 0x0400A8C7 RID: 43207
		PREMIUM_DIAMOND = 8,
		// Token: 0x0400A8C8 RID: 43208
		PREMIUM_ALL = 14,
		// Token: 0x0400A8C9 RID: 43209
		OWNED = 16,
		// Token: 0x0400A8CA RID: 43210
		UNOWNED = 32,
		// Token: 0x0400A8CB RID: 43211
		ALL = -1
	}

	// Token: 0x02001411 RID: 5137
	// (Invoke) Token: 0x0600D986 RID: 55686
	public delegate void OnResultsUpdated();
}
