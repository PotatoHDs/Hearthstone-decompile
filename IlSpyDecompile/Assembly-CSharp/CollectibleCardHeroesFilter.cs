using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleCardHeroesFilter : CollectibleCardFilter
{
	private static readonly Map<int, int> s_forcedPairs = new Map<int, int>
	{
		{ 7, 57751 },
		{ 1066, 57753 },
		{ 930, 57755 },
		{ 671, 57757 },
		{ 31, 57759 },
		{ 274, 57761 },
		{ 893, 57763 },
		{ 637, 57765 },
		{ 813, 57767 },
		{ 56550, 60238 }
	};

	private static readonly Map<TAG_CLASS, int> s_classOrder = new Map<TAG_CLASS, int>
	{
		{
			TAG_CLASS.DEMONHUNTER,
			0
		},
		{
			TAG_CLASS.DRUID,
			1
		},
		{
			TAG_CLASS.HUNTER,
			2
		},
		{
			TAG_CLASS.MAGE,
			3
		},
		{
			TAG_CLASS.PALADIN,
			4
		},
		{
			TAG_CLASS.PRIEST,
			5
		},
		{
			TAG_CLASS.ROGUE,
			6
		},
		{
			TAG_CLASS.SHAMAN,
			7
		},
		{
			TAG_CLASS.WARLOCK,
			8
		},
		{
			TAG_CLASS.WARRIOR,
			9
		},
		{
			TAG_CLASS.NEUTRAL,
			10
		}
	};

	private const int UNOWNED_PURCHASABLE_SORT_VALUE = 10000;

	private const int UNOWNED_UNPURCHASABLE_SORT_VALUE = 20000;

	private TAG_CLASS[] m_classTabOrder;

	private int m_heroesPerPage = 6;

	private List<CollectibleCard> m_results = new List<CollectibleCard>();

	private Map<TAG_CLASS, List<CollectibleCard>> m_currentResultsByClass = new Map<TAG_CLASS, List<CollectibleCard>>();

	public void Init(int heroesPerPage)
	{
		m_heroesPerPage = heroesPerPage;
		FilterHero(isHero: true);
		FilterOnlyOwned(owned: false);
	}

	public void UpdateResults()
	{
		m_results = (from c in GenerateResults().m_cards
			orderby c.OwnedCount descending, c.Class, c.Name
			select c).OrderBy(HeroSkinComparer).ToList();
		m_results.RemoveAll((CollectibleCard c) => c.PremiumType == TAG_PREMIUM.GOLDEN && c.OwnedCount == 0);
		m_results.RemoveAll(delegate(CollectibleCard c)
		{
			if (c.PremiumType == TAG_PREMIUM.GOLDEN)
			{
				return false;
			}
			CollectibleCard card = CollectionManager.Get().GetCard(c.CardId, TAG_PREMIUM.GOLDEN);
			return card != null && card.OwnedCount > 0;
		});
		foreach (KeyValuePair<int, int> pair in s_forcedPairs)
		{
			int num = m_results.FindIndex((CollectibleCard c) => c.CardDbId == pair.Key);
			int num2 = m_results.FindIndex((CollectibleCard c) => c.CardDbId == pair.Value);
			if (num != -1 && num2 != -1 && ((m_results[num].OwnedCount > 0 && m_results[num2].OwnedCount > 0) || (m_results[num].OwnedCount == 0 && m_results[num2].OwnedCount == 0)))
			{
				CollectibleCard item = m_results[num2];
				m_results.RemoveAt(num2);
				m_results.Insert(num + ((num <= num2) ? 1 : 0), item);
			}
		}
	}

	private int HeroSkinComparer(CollectibleCard card)
	{
		int num = 0;
		int num2 = s_classOrder[TAG_CLASS.NEUTRAL];
		if (s_classOrder.ContainsKey(card.Class))
		{
			num2 = s_classOrder[card.Class];
		}
		num += num2;
		if (card.OwnedCount == 0)
		{
			num += (HeroSkinUtils.CanBuyHeroSkinFromCollectionManager(card.CardId) ? 10000 : 20000);
		}
		return num;
	}

	public List<CollectibleCard> GetHeroesContents(int currentPage)
	{
		currentPage = Mathf.Min(currentPage, GetTotalNumPages());
		return m_results.Skip(m_heroesPerPage * (currentPage - 1)).Take(m_heroesPerPage).ToList();
	}

	public int GetTotalNumPages()
	{
		int count = m_results.Count;
		return count / m_heroesPerPage + ((count % m_heroesPerPage > 0) ? 1 : 0);
	}
}
