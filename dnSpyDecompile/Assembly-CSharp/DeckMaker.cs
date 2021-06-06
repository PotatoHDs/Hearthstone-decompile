using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x0200088B RID: 2187
public class DeckMaker
{
	// Token: 0x0600776F RID: 30575 RVA: 0x0026FFA3 File Offset: 0x0026E1A3
	private static bool IsMinion(EntityDef e)
	{
		return e.GetCardType() == TAG_CARDTYPE.MINION;
	}

	// Token: 0x06007770 RID: 30576 RVA: 0x0026FFAE File Offset: 0x0026E1AE
	private static bool IsSpell(EntityDef e)
	{
		return e.GetCardType() == TAG_CARDTYPE.SPELL;
	}

	// Token: 0x06007771 RID: 30577 RVA: 0x0026FFB9 File Offset: 0x0026E1B9
	private static bool IsWeapon(EntityDef e)
	{
		return e.GetCardType() == TAG_CARDTYPE.WEAPON;
	}

	// Token: 0x06007772 RID: 30578 RVA: 0x0026FFC4 File Offset: 0x0026E1C4
	private static bool HasMinCost(EntityDef e, int minCost)
	{
		return e.GetCost() >= minCost;
	}

	// Token: 0x06007773 RID: 30579 RVA: 0x0026FFD2 File Offset: 0x0026E1D2
	private static bool HasMaxCost(EntityDef e, int maxCost)
	{
		return e.GetCost() <= maxCost;
	}

	// Token: 0x06007774 RID: 30580 RVA: 0x0026FFE0 File Offset: 0x0026E1E0
	public static IEnumerable<DeckMaker.DeckFill> GetFillCards(CollectionDeck deck, DeckRuleset deckRuleset)
	{
		bool flag = true;
		List<EntityDef> currentDeckCards;
		List<EntityDef> currentInvalidCards;
		List<EntityDef> cardsICanAddToDeck;
		DeckMaker.InitFromDeck(deck, deckRuleset, out currentDeckCards, out currentInvalidCards, out cardsICanAddToDeck);
		int remainingCardsToFill = CollectionManager.Get().GetDeckSize() - currentDeckCards.Count;
		if (remainingCardsToFill <= 0)
		{
			yield break;
		}
		int num;
		if (flag)
		{
			foreach (DeckMaker.DeckFill deckFill in DeckMaker.GetInvalidFillCards(cardsICanAddToDeck, currentDeckCards, currentInvalidCards))
			{
				num = remainingCardsToFill;
				remainingCardsToFill = num - 1;
				yield return deckFill;
			}
			IEnumerator<DeckMaker.DeckFill> enumerator = null;
		}
		for (int i = 0; i < DeckMaker.s_OrderedCardRequirements.Length; i = num)
		{
			if (remainingCardsToFill <= 0)
			{
				break;
			}
			DeckMaker.CardRequirements cardReq = DeckMaker.s_OrderedCardRequirements[i];
			DeckMaker.CardRequirementsCondition condition = cardReq.m_condition;
			int count = currentDeckCards.FindAll((EntityDef e) => condition(e)).Count;
			int cardsToAddFromSet = Mathf.Min(cardReq.m_requiredCount - count, remainingCardsToFill);
			if (cardsToAddFromSet > 0)
			{
				List<EntityDef> list = cardsICanAddToDeck.FindAll((EntityDef e) => condition(e));
				foreach (EntityDef entityDef in list)
				{
					if (cardsToAddFromSet <= 0)
					{
						break;
					}
					cardsICanAddToDeck.Remove(entityDef);
					currentDeckCards.Add(entityDef);
					num = cardsToAddFromSet - 1;
					cardsToAddFromSet = num;
					num = remainingCardsToFill - 1;
					remainingCardsToFill = num;
					yield return new DeckMaker.DeckFill
					{
						m_removeTemplate = null,
						m_addCard = entityDef,
						m_reason = cardReq.GetRequirementReason()
					};
				}
				List<EntityDef>.Enumerator enumerator2 = default(List<EntityDef>.Enumerator);
			}
			cardReq = null;
			num = i + 1;
		}
		for (int i = 0; i < cardsICanAddToDeck.Count; i = num)
		{
			EntityDef entityDef2 = cardsICanAddToDeck[i];
			if (entityDef2 != null)
			{
				currentDeckCards.Add(entityDef2);
				cardsICanAddToDeck[i] = null;
				yield return new DeckMaker.DeckFill
				{
					m_removeTemplate = null,
					m_addCard = entityDef2,
					m_reason = null
				};
			}
			num = i + 1;
		}
		yield break;
		yield break;
	}

	// Token: 0x06007775 RID: 30581 RVA: 0x0026FFF8 File Offset: 0x0026E1F8
	public static DeckMaker.DeckChoiceFill GetFillCardChoices(CollectionDeck deck, EntityDef referenceCard, int choices, DeckRuleset deckRuleset = null)
	{
		if (deckRuleset == null)
		{
			deckRuleset = deck.GetRuleset();
		}
		List<EntityDef> currentDeckCards;
		List<EntityDef> currentInvalidCards;
		List<EntityDef> cardsICanAddToDeck;
		DeckMaker.InitFromDeck(deck, deckRuleset, out currentDeckCards, out currentInvalidCards, out cardsICanAddToDeck);
		return DeckMaker.GetFillCard(referenceCard, cardsICanAddToDeck, currentDeckCards, currentInvalidCards, choices);
	}

	// Token: 0x06007776 RID: 30582 RVA: 0x00270028 File Offset: 0x0026E228
	private static void InitFromDeck(CollectionDeck deck, DeckRuleset deckRuleset, out List<EntityDef> currentDeckCards, out List<EntityDef> currentInvalidCards, out List<EntityDef> distinctCardsICanAddToDeck)
	{
		CollectionManager collectionManager = CollectionManager.Get();
		List<DeckMaker.SortableEntityDef> list = new List<DeckMaker.SortableEntityDef>();
		currentDeckCards = new List<EntityDef>();
		currentInvalidCards = new List<EntityDef>();
		bool flag = false;
		bool flag2 = false;
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			foreach (object obj in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				TAG_PREMIUM premium = (TAG_PREMIUM)obj;
				int count = collectionDeckSlot.GetCount(premium);
				if (count > 0)
				{
					CollectibleCard card = CollectionManager.Get().GetCard(collectionDeckSlot.CardID, premium);
					if (card != null)
					{
						EntityDef entityDef = card.GetEntityDef();
						for (int i = 0; i < count; i++)
						{
							if (deck.IsValidSlot(collectionDeckSlot, false, false, false))
							{
								currentDeckCards.Add(entityDef);
							}
							else
							{
								currentInvalidCards.Add(entityDef);
							}
						}
						if (entityDef.IsCollectionManagerFilterManaCostByEven)
						{
							flag = true;
						}
						if (entityDef.IsCollectionManagerFilterManaCostByOdd)
						{
							flag2 = true;
						}
					}
				}
			}
		}
		if (flag && flag2)
		{
			flag = false;
			flag2 = false;
		}
		using (Map<string, EntityDef>.Enumerator enumerator3 = DefLoader.Get().GetAllEntityDefs().GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				KeyValuePair<string, EntityDef> kvpair = enumerator3.Current;
				CollectibleCard card2 = collectionManager.GetCard(kvpair.Key, TAG_PREMIUM.NORMAL);
				if (card2 != null && !card2.IsHeroSkin && (card2.GetEntityDef().GetClasses(null).Contains(deck.GetClass()) || card2.Class == TAG_CLASS.NEUTRAL) && (deckRuleset == null || deckRuleset.Filter(kvpair.Value, deck, Array.Empty<DeckRule.RuleType>())) && !CollectionManager.Get().OwnsCoreVersion(GameUtils.TranslateCardIdToDbId(card2.GetEntityDef().GetCardId(), false)) && !RankMgr.Get().IsCardLockedInCurrentLeague(card2.GetEntityDef()) && (!flag || card2.ManaCost % 2 == 0) && (!flag2 || card2.ManaCost % 2 != 0))
				{
					int a = 2;
					if (deckRuleset != null)
					{
						a = Mathf.Min(2, deckRuleset.GetMaxCopiesOfCardAllowed(kvpair.Value));
					}
					int num = card2.OwnedCount;
					CollectibleCard card3 = collectionManager.GetCard(kvpair.Key, TAG_PREMIUM.GOLDEN);
					if (card3 != null)
					{
						num += card3.OwnedCount;
					}
					int count2 = currentDeckCards.FindAll((EntityDef e) => e == kvpair.Value).Count;
					int num2 = Mathf.Min(a, num) - count2;
					for (int j = 0; j < num2; j++)
					{
						list.Add(new DeckMaker.SortableEntityDef
						{
							m_entityDef = kvpair.Value,
							m_suggestWeight = card2.SuggestWeight
						});
					}
				}
			}
		}
		int randomizer = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		list.Sort(delegate(DeckMaker.SortableEntityDef lhs, DeckMaker.SortableEntityDef rhs)
		{
			int num3 = rhs.m_suggestWeight - lhs.m_suggestWeight;
			if (num3 != 0)
			{
				return num3;
			}
			return (lhs.GetHashCode() ^ randomizer) - (rhs.GetHashCode() ^ randomizer);
		});
		distinctCardsICanAddToDeck = new List<EntityDef>();
		foreach (DeckMaker.SortableEntityDef sortableEntityDef in list)
		{
			distinctCardsICanAddToDeck.Add(sortableEntityDef.m_entityDef);
		}
	}

	// Token: 0x06007777 RID: 30583 RVA: 0x002703F4 File Offset: 0x0026E5F4
	private static IEnumerable<DeckMaker.DeckFill> GetInvalidFillCards(List<EntityDef> cardsICanAddToDeck, List<EntityDef> currentDeckCards, List<EntityDef> currentInvalidCards)
	{
		EntityDef[] tplCards = currentInvalidCards.ToArray();
		int num;
		for (int i = 0; i < tplCards.Length; i = num)
		{
			DeckMaker.DeckFill deckFillChoice = DeckMaker.GetFillCard(tplCards[i], cardsICanAddToDeck, null, currentInvalidCards, 1).GetDeckFillChoice(0);
			if (DeckMaker.ReplaceInvalidCard(deckFillChoice, cardsICanAddToDeck, currentDeckCards, currentInvalidCards))
			{
				yield return deckFillChoice;
			}
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x06007778 RID: 30584 RVA: 0x00270412 File Offset: 0x0026E612
	private static bool ReplaceInvalidCard(DeckMaker.DeckFill choice, List<EntityDef> cardsICanAddToDeck, List<EntityDef> currentDeckCards, List<EntityDef> currentInvalidCards)
	{
		if (choice == null)
		{
			return false;
		}
		if (!currentInvalidCards.Remove(choice.m_removeTemplate))
		{
			return false;
		}
		cardsICanAddToDeck.Remove(choice.m_addCard);
		currentDeckCards.Add(choice.m_addCard);
		return true;
	}

	// Token: 0x06007779 RID: 30585 RVA: 0x00270444 File Offset: 0x0026E644
	private static DeckMaker.DeckChoiceFill GetFillCard(EntityDef referenceCard, List<EntityDef> cardsICanAddToDeck, List<EntityDef> currentDeckCards, List<EntityDef> currentInvalidCards, int totalNumChoices = 3)
	{
		if (referenceCard == null && currentInvalidCards != null && currentInvalidCards.Count > 0)
		{
			referenceCard = currentInvalidCards.First<EntityDef>();
		}
		int cardRequirementsStartIndex = DeckMaker.GetCardRequirementsStartIndex(referenceCard, currentDeckCards);
		DeckMaker.DeckChoiceFill deckChoiceFill = new DeckMaker.DeckChoiceFill(referenceCard, Array.Empty<EntityDef>());
		for (int i = cardRequirementsStartIndex; i < DeckMaker.s_OrderedCardRequirements.Length; i++)
		{
			if (totalNumChoices <= 0)
			{
				break;
			}
			DeckMaker.CardRequirements cardRequirements = DeckMaker.s_OrderedCardRequirements[i];
			DeckMaker.CardRequirementsCondition condition = cardRequirements.m_condition;
			List<EntityDef> list = cardsICanAddToDeck.FindAll((EntityDef e) => condition(e));
			if (list.Count > 0)
			{
				int num = 8;
				List<EntityDef> list2 = new List<EntityDef>();
				List<EntityDef> list3 = new List<EntityDef>();
				int num2 = int.MinValue;
				foreach (EntityDef entityDef in list.Distinct<EntityDef>())
				{
					CollectibleCard card = CollectionManager.Get().GetCard(entityDef.GetCardId(), TAG_PREMIUM.NORMAL);
					num2 = Mathf.Max(num2, card.SuggestWeight);
				}
				foreach (EntityDef entityDef2 in list.Distinct<EntityDef>())
				{
					if (num <= 0)
					{
						break;
					}
					CollectibleCard card2 = CollectionManager.Get().GetCard(entityDef2.GetCardId(), TAG_PREMIUM.NORMAL);
					if (num2 - card2.SuggestWeight > 100)
					{
						list3.Add(entityDef2);
					}
					else
					{
						list2.Add(entityDef2);
					}
					num--;
				}
				GeneralUtils.Shuffle<EntityDef>(list2);
				GeneralUtils.Shuffle<EntityDef>(list3);
				int num3 = Mathf.Min(list2.Count, totalNumChoices);
				int num4 = Mathf.Min(list3.Count, totalNumChoices - num3);
				if (num3 > 0)
				{
					deckChoiceFill.m_addChoices.AddRange(list2.GetRange(0, num3));
				}
				if (num4 > 0)
				{
					deckChoiceFill.m_addChoices.AddRange(list3.GetRange(0, num4));
				}
				totalNumChoices -= num3 + num4;
				deckChoiceFill.m_reason = ((referenceCard == null) ? cardRequirements.GetRequirementReason() : GameStrings.Format("GLUE_RDM_TEMPLATE_REPLACE", new object[]
				{
					referenceCard.GetName()
				}));
			}
		}
		return deckChoiceFill;
	}

	// Token: 0x0600777A RID: 30586 RVA: 0x0027066C File Offset: 0x0026E86C
	private static int GetCardRequirementsStartIndex(EntityDef referenceCard, List<EntityDef> currentDeckCards)
	{
		if (referenceCard != null)
		{
			for (int i = 0; i < DeckMaker.s_OrderedCardRequirements.Length; i++)
			{
				if (DeckMaker.s_OrderedCardRequirements[i].m_condition(referenceCard))
				{
					return i;
				}
			}
		}
		else if (currentDeckCards != null)
		{
			for (int j = 0; j < DeckMaker.s_OrderedCardRequirements.Length; j++)
			{
				DeckMaker.CardRequirements cardRequirements = DeckMaker.s_OrderedCardRequirements[j];
				DeckMaker.CardRequirementsCondition condition = cardRequirements.m_condition;
				if (currentDeckCards.FindAll((EntityDef e) => condition(e)).Count < cardRequirements.m_requiredCount)
				{
					return j;
				}
			}
		}
		return 0;
	}

	// Token: 0x04005D94 RID: 23956
	private static readonly DeckMaker.CardRequirements[] s_OrderedCardRequirements = new DeckMaker.CardRequirements[]
	{
		new DeckMaker.CardRequirements(8, (EntityDef e) => DeckMaker.IsMinion(e) && DeckMaker.HasMinCost(e, 1) && DeckMaker.HasMaxCost(e, 2), "GLUE_RDM_LOW_COST"),
		new DeckMaker.CardRequirements(5, (EntityDef e) => DeckMaker.IsMinion(e) && DeckMaker.HasMinCost(e, 3) && DeckMaker.HasMaxCost(e, 4), "GLUE_RDM_MEDIUM_COST"),
		new DeckMaker.CardRequirements(4, (EntityDef e) => DeckMaker.IsMinion(e) && DeckMaker.HasMinCost(e, 5), "GLUE_RDM_HIGH_COST"),
		new DeckMaker.CardRequirements(7, (EntityDef e) => DeckMaker.IsSpell(e), "GLUE_RDM_MORE_SPELLS"),
		new DeckMaker.CardRequirements(2, (EntityDef e) => DeckMaker.IsWeapon(e), "GLUE_RDM_MORE_WEAPONS"),
		new DeckMaker.CardRequirements(int.MaxValue, (EntityDef e) => DeckMaker.IsMinion(e), "GLUE_RDM_NO_SPECIFICS")
	};

	// Token: 0x04005D95 RID: 23957
	private const int s_randomChoicePoolSize = 8;

	// Token: 0x04005D96 RID: 23958
	private const int s_priorityWeightDifference = 100;

	// Token: 0x020024C1 RID: 9409
	public class DeckChoiceFill
	{
		// Token: 0x060130CB RID: 78027 RVA: 0x00527D98 File Offset: 0x00525F98
		public DeckChoiceFill(EntityDef remove, params EntityDef[] addChoices)
		{
			this.m_removeTemplate = remove;
			if (addChoices != null && addChoices.Length != 0)
			{
				this.m_addChoices = new List<EntityDef>(addChoices);
			}
		}

		// Token: 0x060130CC RID: 78028 RVA: 0x00527DC8 File Offset: 0x00525FC8
		public DeckMaker.DeckFill GetDeckFillChoice(int idx)
		{
			if (idx >= this.m_addChoices.Count)
			{
				return null;
			}
			return new DeckMaker.DeckFill
			{
				m_removeTemplate = this.m_removeTemplate,
				m_addCard = this.m_addChoices[idx],
				m_reason = this.m_reason
			};
		}

		// Token: 0x0400EBA4 RID: 60324
		public EntityDef m_removeTemplate;

		// Token: 0x0400EBA5 RID: 60325
		public List<EntityDef> m_addChoices = new List<EntityDef>();

		// Token: 0x0400EBA6 RID: 60326
		public string m_reason;
	}

	// Token: 0x020024C2 RID: 9410
	public class DeckFill
	{
		// Token: 0x0400EBA7 RID: 60327
		public EntityDef m_removeTemplate;

		// Token: 0x0400EBA8 RID: 60328
		public EntityDef m_addCard;

		// Token: 0x0400EBA9 RID: 60329
		public string m_reason;
	}

	// Token: 0x020024C3 RID: 9411
	// (Invoke) Token: 0x060130CF RID: 78031
	public delegate bool CardRequirementsCondition(EntityDef entityDef);

	// Token: 0x020024C4 RID: 9412
	private class CardRequirements
	{
		// Token: 0x060130D2 RID: 78034 RVA: 0x00527E14 File Offset: 0x00526014
		public CardRequirements(int requiredCount, DeckMaker.CardRequirementsCondition condition, string reason = "")
		{
			this.m_requiredCount = requiredCount;
			this.m_condition = condition;
			this.m_reason = reason;
		}

		// Token: 0x060130D3 RID: 78035 RVA: 0x00527E31 File Offset: 0x00526031
		public string GetRequirementReason()
		{
			if (string.IsNullOrEmpty(this.m_reason))
			{
				return "No reason!";
			}
			return GameStrings.Get(this.m_reason);
		}

		// Token: 0x0400EBAA RID: 60330
		public int m_requiredCount;

		// Token: 0x0400EBAB RID: 60331
		public DeckMaker.CardRequirementsCondition m_condition;

		// Token: 0x0400EBAC RID: 60332
		private string m_reason;
	}

	// Token: 0x020024C5 RID: 9413
	private class SortableEntityDef
	{
		// Token: 0x0400EBAD RID: 60333
		public EntityDef m_entityDef;

		// Token: 0x0400EBAE RID: 60334
		public int m_suggestWeight;
	}
}
