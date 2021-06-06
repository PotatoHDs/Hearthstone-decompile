using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class CollectibleCardClassFilter : CollectibleCardFilter
{
	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0004F9AD File Offset: 0x0004DBAD
	// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0004F9B5 File Offset: 0x0004DBB5
	public CollectionManager.FindCardsResult FindCardsResult { get; private set; }

	// Token: 0x06000E2E RID: 3630 RVA: 0x0004F9C0 File Offset: 0x0004DBC0
	public void Init(TAG_CLASS[] classTabOrder, int cardsPerPage)
	{
		this.m_classTabOrder = classTabOrder;
		this.m_cardsPerPage = cardsPerPage;
		for (int i = 0; i < classTabOrder.Length; i++)
		{
			this.m_currentResultsByClass[classTabOrder[i]] = new List<CollectibleCard>();
		}
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0004F9FC File Offset: 0x0004DBFC
	public void UpdateResults()
	{
		this.FindCardsResult = base.GenerateResults();
		List<CollectibleCard> cards = this.FindCardsResult.m_cards;
		foreach (KeyValuePair<TAG_CLASS, List<CollectibleCard>> keyValuePair in this.m_currentResultsByClass)
		{
			keyValuePair.Value.Clear();
		}
		foreach (CollectibleCard collectibleCard in cards)
		{
			foreach (TAG_CLASS tag_CLASS in collectibleCard.Classes)
			{
				if (this.m_filterClasses == null || this.m_filterClasses.Contains(tag_CLASS))
				{
					if (!this.m_currentResultsByClass.ContainsKey(tag_CLASS))
					{
						Error.AddDevFatal("Card: {0} ({1}) has an invalid class: {2}. Cannot render page.", new object[]
						{
							collectibleCard.Name,
							collectibleCard.CardId,
							collectibleCard.Class
						});
						return;
					}
					this.m_currentResultsByClass[tag_CLASS].Add(collectibleCard);
				}
			}
		}
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0004FB58 File Offset: 0x0004DD58
	public int GetNumPagesForClass(TAG_CLASS cardClass)
	{
		List<CollectibleCard> list;
		if (!this.m_currentResultsByClass.TryGetValue(cardClass, out list))
		{
			return 0;
		}
		return list.Count / this.m_cardsPerPage + ((list.Count % this.m_cardsPerPage > 0) ? 1 : 0);
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0004FB99 File Offset: 0x0004DD99
	public int GetNumNewCardsForClass(TAG_CLASS cardClass)
	{
		return (from c in this.m_currentResultsByClass[cardClass]
		where c.IsNewCard
		select c).Count<CollectibleCard>();
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0004FBD0 File Offset: 0x0004DDD0
	public int GetTotalNumPages()
	{
		int num = 0;
		foreach (TAG_CLASS cardClass in this.m_classTabOrder)
		{
			num += this.GetNumPagesForClass(cardClass);
		}
		return num;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0004FC04 File Offset: 0x0004DE04
	public List<CollectibleCard> GetPageContents(int page)
	{
		if (page < 0 || page > this.GetTotalNumPages())
		{
			return new List<CollectibleCard>();
		}
		int num = 0;
		for (int i = 0; i < this.m_classTabOrder.Length; i++)
		{
			int num2 = num;
			TAG_CLASS tag_CLASS = this.m_classTabOrder[i];
			num += this.GetNumPagesForClass(tag_CLASS);
			if (page <= num)
			{
				int pageWithinClass = page - num2;
				int num3;
				return this.GetPageContentsForClass(tag_CLASS, pageWithinClass, false, out num3);
			}
		}
		return new List<CollectibleCard>();
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x0004FC6C File Offset: 0x0004DE6C
	public TAG_CLASS GetCurrentClassFromPage(int page)
	{
		if (page < 0 || page > this.GetTotalNumPages())
		{
			return TAG_CLASS.INVALID;
		}
		int num = 0;
		for (int i = 0; i < this.m_classTabOrder.Length; i++)
		{
			TAG_CLASS tag_CLASS = this.m_classTabOrder[i];
			num += this.GetNumPagesForClass(tag_CLASS);
			if (page <= num)
			{
				return tag_CLASS;
			}
		}
		return TAG_CLASS.INVALID;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
	public List<CollectibleCard> GetFirstNonEmptyPage(out int collectionPage)
	{
		collectionPage = 0;
		TAG_CLASS pageClass = TAG_CLASS.NEUTRAL;
		for (int i = 0; i < this.m_classTabOrder.Length; i++)
		{
			if (this.m_currentResultsByClass[this.m_classTabOrder[i]].Count > 0)
			{
				pageClass = this.m_classTabOrder[i];
				break;
			}
		}
		return this.GetPageContentsForClass(pageClass, 1, true, out collectionPage);
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x0004FD10 File Offset: 0x0004DF10
	public List<CollectibleCard> GetPageContentsForClass(TAG_CLASS pageClass, int pageWithinClass, bool calculateCollectionPage, out int collectionPage)
	{
		collectionPage = 0;
		if (pageWithinClass <= 0 || pageWithinClass > this.GetNumPagesForClass(pageClass))
		{
			return new List<CollectibleCard>();
		}
		if (calculateCollectionPage)
		{
			for (int i = 0; i < this.m_classTabOrder.Length; i++)
			{
				TAG_CLASS tag_CLASS = this.m_classTabOrder[i];
				if (tag_CLASS == pageClass)
				{
					break;
				}
				collectionPage += this.GetNumPagesForClass(tag_CLASS);
			}
			collectionPage += pageWithinClass;
		}
		List<CollectibleCard> list = this.m_currentResultsByClass[pageClass];
		if (list == null)
		{
			return new List<CollectibleCard>();
		}
		return list.Skip(this.m_cardsPerPage * (pageWithinClass - 1)).Take(this.m_cardsPerPage).ToList<CollectibleCard>();
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
	public List<CollectibleCard> GetPageContentsForCard(string cardID, TAG_PREMIUM premiumType, out int collectionPage, TAG_CLASS classContext = TAG_CLASS.INVALID)
	{
		collectionPage = 0;
		IEnumerable<TAG_CLASS> classes = DefLoader.Get().GetEntityDef(cardID).GetClasses(null);
		TAG_CLASS tag_CLASS = TAG_CLASS.NEUTRAL;
		if (classes.Count<TAG_CLASS>() == 1)
		{
			tag_CLASS = classes.ElementAt(0);
		}
		else if (classContext != TAG_CLASS.INVALID && classes.Contains(classContext))
		{
			tag_CLASS = classContext;
		}
		else
		{
			Debug.LogWarning("CollectibleCardClassFilter.GetPageContentsForCard() - The specified card class mismatches its class context.");
		}
		int num = this.m_currentResultsByClass[tag_CLASS].FindIndex((CollectibleCard obj) => obj.CardId == cardID && obj.PremiumType == premiumType);
		if (num < 0)
		{
			return new List<CollectibleCard>();
		}
		int num2 = num + 1;
		int pageWithinClass = num2 / this.m_cardsPerPage + ((num2 % this.m_cardsPerPage > 0) ? 1 : 0);
		return this.GetPageContentsForClass(tag_CLASS, pageWithinClass, true, out collectionPage);
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0004FE68 File Offset: 0x0004E068
	private int GetUniqueCardCount(List<CollectibleCard> cards)
	{
		HashSet<string> hashSet = new HashSet<string>();
		for (int i = 0; i < cards.Count; i++)
		{
			hashSet.Add(cards[i].CardId);
		}
		return hashSet.Count;
	}

	// Token: 0x040009C6 RID: 2502
	private int m_cardsPerPage = 8;

	// Token: 0x040009C7 RID: 2503
	private TAG_CLASS[] m_classTabOrder;

	// Token: 0x040009C8 RID: 2504
	private Map<TAG_CLASS, List<CollectibleCard>> m_currentResultsByClass = new Map<TAG_CLASS, List<CollectibleCard>>();
}
