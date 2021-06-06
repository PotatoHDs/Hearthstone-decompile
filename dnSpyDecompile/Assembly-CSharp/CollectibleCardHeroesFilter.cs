using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class CollectibleCardHeroesFilter : CollectibleCardFilter
{
	// Token: 0x06000E58 RID: 3672 RVA: 0x00050B6C File Offset: 0x0004ED6C
	public void Init(int heroesPerPage)
	{
		this.m_heroesPerPage = heroesPerPage;
		base.FilterHero(true);
		base.FilterOnlyOwned(false);
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x00050B84 File Offset: 0x0004ED84
	public void UpdateResults()
	{
		this.m_results = (from c in base.GenerateResults().m_cards
		orderby c.OwnedCount descending, c.Class, c.Name
		select c).OrderBy(new Func<CollectibleCard, int>(this.HeroSkinComparer)).ToList<CollectibleCard>();
		this.m_results.RemoveAll((CollectibleCard c) => c.PremiumType == TAG_PREMIUM.GOLDEN && c.OwnedCount == 0);
		this.m_results.RemoveAll(delegate(CollectibleCard c)
		{
			if (c.PremiumType == TAG_PREMIUM.GOLDEN)
			{
				return false;
			}
			CollectibleCard card = CollectionManager.Get().GetCard(c.CardId, TAG_PREMIUM.GOLDEN);
			return card != null && card.OwnedCount > 0;
		});
		using (Map<int, int>.Enumerator enumerator = CollectibleCardHeroesFilter.s_forcedPairs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> pair = enumerator.Current;
				int num = this.m_results.FindIndex((CollectibleCard c) => c.CardDbId == pair.Key);
				int num2 = this.m_results.FindIndex((CollectibleCard c) => c.CardDbId == pair.Value);
				if (num != -1 && num2 != -1 && ((this.m_results[num].OwnedCount > 0 && this.m_results[num2].OwnedCount > 0) || (this.m_results[num].OwnedCount == 0 && this.m_results[num2].OwnedCount == 0)))
				{
					CollectibleCard item = this.m_results[num2];
					this.m_results.RemoveAt(num2);
					this.m_results.Insert(num + ((num > num2) ? 0 : 1), item);
				}
			}
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x00050D84 File Offset: 0x0004EF84
	private int HeroSkinComparer(CollectibleCard card)
	{
		int num = 0;
		int num2 = CollectibleCardHeroesFilter.s_classOrder[TAG_CLASS.NEUTRAL];
		if (CollectibleCardHeroesFilter.s_classOrder.ContainsKey(card.Class))
		{
			num2 = CollectibleCardHeroesFilter.s_classOrder[card.Class];
		}
		num += num2;
		if (card.OwnedCount == 0)
		{
			num += (HeroSkinUtils.CanBuyHeroSkinFromCollectionManager(card.CardId) ? 10000 : 20000);
		}
		return num;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x00050DEC File Offset: 0x0004EFEC
	public List<CollectibleCard> GetHeroesContents(int currentPage)
	{
		currentPage = Mathf.Min(currentPage, this.GetTotalNumPages());
		return this.m_results.Skip(this.m_heroesPerPage * (currentPage - 1)).Take(this.m_heroesPerPage).ToList<CollectibleCard>();
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00050E24 File Offset: 0x0004F024
	public int GetTotalNumPages()
	{
		int count = this.m_results.Count;
		return count / this.m_heroesPerPage + ((count % this.m_heroesPerPage > 0) ? 1 : 0);
	}

	// Token: 0x040009DC RID: 2524
	private static readonly Map<int, int> s_forcedPairs = new Map<int, int>
	{
		{
			7,
			57751
		},
		{
			1066,
			57753
		},
		{
			930,
			57755
		},
		{
			671,
			57757
		},
		{
			31,
			57759
		},
		{
			274,
			57761
		},
		{
			893,
			57763
		},
		{
			637,
			57765
		},
		{
			813,
			57767
		},
		{
			56550,
			60238
		}
	};

	// Token: 0x040009DD RID: 2525
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

	// Token: 0x040009DE RID: 2526
	private const int UNOWNED_PURCHASABLE_SORT_VALUE = 10000;

	// Token: 0x040009DF RID: 2527
	private const int UNOWNED_UNPURCHASABLE_SORT_VALUE = 20000;

	// Token: 0x040009E0 RID: 2528
	private TAG_CLASS[] m_classTabOrder;

	// Token: 0x040009E1 RID: 2529
	private int m_heroesPerPage = 6;

	// Token: 0x040009E2 RID: 2530
	private List<CollectibleCard> m_results = new List<CollectibleCard>();

	// Token: 0x040009E3 RID: 2531
	private Map<TAG_CLASS, List<CollectibleCard>> m_currentResultsByClass = new Map<TAG_CLASS, List<CollectibleCard>>();
}
