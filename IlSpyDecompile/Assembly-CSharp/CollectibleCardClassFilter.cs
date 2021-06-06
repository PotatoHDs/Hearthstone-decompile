using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleCardClassFilter : CollectibleCardFilter
{
	private int m_cardsPerPage = 8;

	private TAG_CLASS[] m_classTabOrder;

	private Map<TAG_CLASS, List<CollectibleCard>> m_currentResultsByClass = new Map<TAG_CLASS, List<CollectibleCard>>();

	public CollectionManager.FindCardsResult FindCardsResult { get; private set; }

	public void Init(TAG_CLASS[] classTabOrder, int cardsPerPage)
	{
		m_classTabOrder = classTabOrder;
		m_cardsPerPage = cardsPerPage;
		for (int i = 0; i < classTabOrder.Length; i++)
		{
			m_currentResultsByClass[classTabOrder[i]] = new List<CollectibleCard>();
		}
	}

	public void UpdateResults()
	{
		FindCardsResult = GenerateResults();
		List<CollectibleCard> cards = FindCardsResult.m_cards;
		foreach (KeyValuePair<TAG_CLASS, List<CollectibleCard>> item in m_currentResultsByClass)
		{
			item.Value.Clear();
		}
		foreach (CollectibleCard item2 in cards)
		{
			foreach (TAG_CLASS @class in item2.Classes)
			{
				if (m_filterClasses == null || m_filterClasses.Contains(@class))
				{
					if (!m_currentResultsByClass.ContainsKey(@class))
					{
						Error.AddDevFatal("Card: {0} ({1}) has an invalid class: {2}. Cannot render page.", item2.Name, item2.CardId, item2.Class);
						return;
					}
					m_currentResultsByClass[@class].Add(item2);
				}
			}
		}
	}

	public int GetNumPagesForClass(TAG_CLASS cardClass)
	{
		if (!m_currentResultsByClass.TryGetValue(cardClass, out var value))
		{
			return 0;
		}
		return value.Count / m_cardsPerPage + ((value.Count % m_cardsPerPage > 0) ? 1 : 0);
	}

	public int GetNumNewCardsForClass(TAG_CLASS cardClass)
	{
		return m_currentResultsByClass[cardClass].Where((CollectibleCard c) => c.IsNewCard).Count();
	}

	public int GetTotalNumPages()
	{
		int num = 0;
		TAG_CLASS[] classTabOrder = m_classTabOrder;
		foreach (TAG_CLASS cardClass in classTabOrder)
		{
			num += GetNumPagesForClass(cardClass);
		}
		return num;
	}

	public List<CollectibleCard> GetPageContents(int page)
	{
		if (page < 0 || page > GetTotalNumPages())
		{
			return new List<CollectibleCard>();
		}
		int num = 0;
		for (int i = 0; i < m_classTabOrder.Length; i++)
		{
			int num2 = num;
			TAG_CLASS tAG_CLASS = m_classTabOrder[i];
			num += GetNumPagesForClass(tAG_CLASS);
			if (page <= num)
			{
				int pageWithinClass = page - num2;
				int collectionPage;
				return GetPageContentsForClass(tAG_CLASS, pageWithinClass, calculateCollectionPage: false, out collectionPage);
			}
		}
		return new List<CollectibleCard>();
	}

	public TAG_CLASS GetCurrentClassFromPage(int page)
	{
		if (page < 0 || page > GetTotalNumPages())
		{
			return TAG_CLASS.INVALID;
		}
		int num = 0;
		for (int i = 0; i < m_classTabOrder.Length; i++)
		{
			TAG_CLASS tAG_CLASS = m_classTabOrder[i];
			num += GetNumPagesForClass(tAG_CLASS);
			if (page <= num)
			{
				return tAG_CLASS;
			}
		}
		return TAG_CLASS.INVALID;
	}

	public List<CollectibleCard> GetFirstNonEmptyPage(out int collectionPage)
	{
		collectionPage = 0;
		TAG_CLASS pageClass = TAG_CLASS.NEUTRAL;
		for (int i = 0; i < m_classTabOrder.Length; i++)
		{
			if (m_currentResultsByClass[m_classTabOrder[i]].Count > 0)
			{
				pageClass = m_classTabOrder[i];
				break;
			}
		}
		return GetPageContentsForClass(pageClass, 1, calculateCollectionPage: true, out collectionPage);
	}

	public List<CollectibleCard> GetPageContentsForClass(TAG_CLASS pageClass, int pageWithinClass, bool calculateCollectionPage, out int collectionPage)
	{
		collectionPage = 0;
		if (pageWithinClass <= 0 || pageWithinClass > GetNumPagesForClass(pageClass))
		{
			return new List<CollectibleCard>();
		}
		if (calculateCollectionPage)
		{
			for (int i = 0; i < m_classTabOrder.Length; i++)
			{
				TAG_CLASS tAG_CLASS = m_classTabOrder[i];
				if (tAG_CLASS == pageClass)
				{
					break;
				}
				collectionPage += GetNumPagesForClass(tAG_CLASS);
			}
			collectionPage += pageWithinClass;
		}
		List<CollectibleCard> list = m_currentResultsByClass[pageClass];
		if (list == null)
		{
			return new List<CollectibleCard>();
		}
		return list.Skip(m_cardsPerPage * (pageWithinClass - 1)).Take(m_cardsPerPage).ToList();
	}

	public List<CollectibleCard> GetPageContentsForCard(string cardID, TAG_PREMIUM premiumType, out int collectionPage, TAG_CLASS classContext = TAG_CLASS.INVALID)
	{
		collectionPage = 0;
		IEnumerable<TAG_CLASS> classes = DefLoader.Get().GetEntityDef(cardID).GetClasses();
		TAG_CLASS tAG_CLASS = TAG_CLASS.NEUTRAL;
		if (classes.Count() == 1)
		{
			tAG_CLASS = classes.ElementAt(0);
		}
		else if (classContext != 0 && classes.Contains(classContext))
		{
			tAG_CLASS = classContext;
		}
		else
		{
			Debug.LogWarning("CollectibleCardClassFilter.GetPageContentsForCard() - The specified card class mismatches its class context.");
		}
		int num = m_currentResultsByClass[tAG_CLASS].FindIndex((CollectibleCard obj) => obj.CardId == cardID && obj.PremiumType == premiumType);
		if (num < 0)
		{
			return new List<CollectibleCard>();
		}
		int num2 = num + 1;
		int pageWithinClass = num2 / m_cardsPerPage + ((num2 % m_cardsPerPage > 0) ? 1 : 0);
		return GetPageContentsForClass(tAG_CLASS, pageWithinClass, calculateCollectionPage: true, out collectionPage);
	}

	private int GetUniqueCardCount(List<CollectibleCard> cards)
	{
		HashSet<string> hashSet = new HashSet<string>();
		for (int i = 0; i < cards.Count; i++)
		{
			hashSet.Add(cards[i].CardId);
		}
		return hashSet.Count;
	}
}
