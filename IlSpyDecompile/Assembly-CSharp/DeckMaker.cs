using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using UnityEngine;

public class DeckMaker
{
	public class DeckChoiceFill
	{
		public EntityDef m_removeTemplate;

		public List<EntityDef> m_addChoices = new List<EntityDef>();

		public string m_reason;

		public DeckChoiceFill(EntityDef remove, params EntityDef[] addChoices)
		{
			m_removeTemplate = remove;
			if (addChoices != null && addChoices.Length != 0)
			{
				m_addChoices = new List<EntityDef>(addChoices);
			}
		}

		public DeckFill GetDeckFillChoice(int idx)
		{
			if (idx >= m_addChoices.Count)
			{
				return null;
			}
			return new DeckFill
			{
				m_removeTemplate = m_removeTemplate,
				m_addCard = m_addChoices[idx],
				m_reason = m_reason
			};
		}
	}

	public class DeckFill
	{
		public EntityDef m_removeTemplate;

		public EntityDef m_addCard;

		public string m_reason;
	}

	public delegate bool CardRequirementsCondition(EntityDef entityDef);

	private class CardRequirements
	{
		public int m_requiredCount;

		public CardRequirementsCondition m_condition;

		private string m_reason;

		public CardRequirements(int requiredCount, CardRequirementsCondition condition, string reason = "")
		{
			m_requiredCount = requiredCount;
			m_condition = condition;
			m_reason = reason;
		}

		public string GetRequirementReason()
		{
			if (string.IsNullOrEmpty(m_reason))
			{
				return "No reason!";
			}
			return GameStrings.Get(m_reason);
		}
	}

	private class SortableEntityDef
	{
		public EntityDef m_entityDef;

		public int m_suggestWeight;
	}

	private static readonly CardRequirements[] s_OrderedCardRequirements = new CardRequirements[6]
	{
		new CardRequirements(8, (EntityDef e) => IsMinion(e) && HasMinCost(e, 1) && HasMaxCost(e, 2), "GLUE_RDM_LOW_COST"),
		new CardRequirements(5, (EntityDef e) => IsMinion(e) && HasMinCost(e, 3) && HasMaxCost(e, 4), "GLUE_RDM_MEDIUM_COST"),
		new CardRequirements(4, (EntityDef e) => IsMinion(e) && HasMinCost(e, 5), "GLUE_RDM_HIGH_COST"),
		new CardRequirements(7, (EntityDef e) => IsSpell(e), "GLUE_RDM_MORE_SPELLS"),
		new CardRequirements(2, (EntityDef e) => IsWeapon(e), "GLUE_RDM_MORE_WEAPONS"),
		new CardRequirements(int.MaxValue, (EntityDef e) => IsMinion(e), "GLUE_RDM_NO_SPECIFICS")
	};

	private const int s_randomChoicePoolSize = 8;

	private const int s_priorityWeightDifference = 100;

	private static bool IsMinion(EntityDef e)
	{
		return e.GetCardType() == TAG_CARDTYPE.MINION;
	}

	private static bool IsSpell(EntityDef e)
	{
		return e.GetCardType() == TAG_CARDTYPE.SPELL;
	}

	private static bool IsWeapon(EntityDef e)
	{
		return e.GetCardType() == TAG_CARDTYPE.WEAPON;
	}

	private static bool HasMinCost(EntityDef e, int minCost)
	{
		return e.GetCost() >= minCost;
	}

	private static bool HasMaxCost(EntityDef e, int maxCost)
	{
		return e.GetCost() <= maxCost;
	}

	public static IEnumerable<DeckFill> GetFillCards(CollectionDeck deck, DeckRuleset deckRuleset)
	{
		bool flag = true;
		InitFromDeck(deck, deckRuleset, out var currentDeckCards, out var currentInvalidCards, out var cardsICanAddToDeck);
		int remainingCardsToFill = CollectionManager.Get().GetDeckSize() - currentDeckCards.Count;
		if (remainingCardsToFill <= 0)
		{
			yield break;
		}
		if (flag)
		{
			foreach (DeckFill invalidFillCard in GetInvalidFillCards(cardsICanAddToDeck, currentDeckCards, currentInvalidCards))
			{
				remainingCardsToFill--;
				yield return invalidFillCard;
			}
		}
		int j = 0;
		while (j < s_OrderedCardRequirements.Length)
		{
			if (remainingCardsToFill <= 0)
			{
				break;
			}
			CardRequirements cardReq = s_OrderedCardRequirements[j];
			CardRequirementsCondition condition = cardReq.m_condition;
			int count = currentDeckCards.FindAll((EntityDef e) => condition(e)).Count;
			int cardsToAddFromSet = Mathf.Min(cardReq.m_requiredCount - count, remainingCardsToFill);
			int num;
			if (cardsToAddFromSet > 0)
			{
				List<EntityDef> list = cardsICanAddToDeck.FindAll((EntityDef e) => condition(e));
				foreach (EntityDef item in list)
				{
					if (cardsToAddFromSet <= 0)
					{
						break;
					}
					cardsICanAddToDeck.Remove(item);
					currentDeckCards.Add(item);
					num = cardsToAddFromSet - 1;
					cardsToAddFromSet = num;
					num = remainingCardsToFill - 1;
					remainingCardsToFill = num;
					yield return new DeckFill
					{
						m_removeTemplate = null,
						m_addCard = item,
						m_reason = cardReq.GetRequirementReason()
					};
				}
			}
			num = j + 1;
			j = num;
		}
		j = 0;
		while (j < cardsICanAddToDeck.Count)
		{
			EntityDef entityDef = cardsICanAddToDeck[j];
			if (entityDef != null)
			{
				currentDeckCards.Add(entityDef);
				cardsICanAddToDeck[j] = null;
				yield return new DeckFill
				{
					m_removeTemplate = null,
					m_addCard = entityDef,
					m_reason = null
				};
			}
			int num = j + 1;
			j = num;
		}
	}

	public static DeckChoiceFill GetFillCardChoices(CollectionDeck deck, EntityDef referenceCard, int choices, DeckRuleset deckRuleset = null)
	{
		if (deckRuleset == null)
		{
			deckRuleset = deck.GetRuleset();
		}
		InitFromDeck(deck, deckRuleset, out var currentDeckCards, out var currentInvalidCards, out var distinctCardsICanAddToDeck);
		return GetFillCard(referenceCard, distinctCardsICanAddToDeck, currentDeckCards, currentInvalidCards, choices);
	}

	private static void InitFromDeck(CollectionDeck deck, DeckRuleset deckRuleset, out List<EntityDef> currentDeckCards, out List<EntityDef> currentInvalidCards, out List<EntityDef> distinctCardsICanAddToDeck)
	{
		CollectionManager collectionManager = CollectionManager.Get();
		List<SortableEntityDef> list = new List<SortableEntityDef>();
		currentDeckCards = new List<EntityDef>();
		currentInvalidCards = new List<EntityDef>();
		bool flag = false;
		bool flag2 = false;
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			foreach (TAG_PREMIUM value in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				int count = slot.GetCount(value);
				if (count <= 0)
				{
					continue;
				}
				CollectibleCard card = CollectionManager.Get().GetCard(slot.CardID, value);
				if (card == null)
				{
					continue;
				}
				EntityDef entityDef = card.GetEntityDef();
				for (int i = 0; i < count; i++)
				{
					if (deck.IsValidSlot(slot))
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
		if (flag && flag2)
		{
			flag = false;
			flag2 = false;
		}
		foreach (KeyValuePair<string, EntityDef> kvpair in DefLoader.Get().GetAllEntityDefs())
		{
			CollectibleCard card2 = collectionManager.GetCard(kvpair.Key, TAG_PREMIUM.NORMAL);
			if (card2 != null && !card2.IsHeroSkin && (card2.GetEntityDef().GetClasses().Contains(deck.GetClass()) || card2.Class == TAG_CLASS.NEUTRAL) && (deckRuleset == null || deckRuleset.Filter(kvpair.Value, deck)) && !CollectionManager.Get().OwnsCoreVersion(GameUtils.TranslateCardIdToDbId(card2.GetEntityDef().GetCardId())) && !RankMgr.Get().IsCardLockedInCurrentLeague(card2.GetEntityDef()) && (!flag || card2.ManaCost % 2 == 0) && (!flag2 || card2.ManaCost % 2 != 0))
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
					list.Add(new SortableEntityDef
					{
						m_entityDef = kvpair.Value,
						m_suggestWeight = card2.SuggestWeight
					});
				}
			}
		}
		int randomizer = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		list.Sort(delegate(SortableEntityDef lhs, SortableEntityDef rhs)
		{
			int num3 = rhs.m_suggestWeight - lhs.m_suggestWeight;
			return (num3 != 0) ? num3 : ((lhs.GetHashCode() ^ randomizer) - (rhs.GetHashCode() ^ randomizer));
		});
		distinctCardsICanAddToDeck = new List<EntityDef>();
		foreach (SortableEntityDef item in list)
		{
			distinctCardsICanAddToDeck.Add(item.m_entityDef);
		}
	}

	private static IEnumerable<DeckFill> GetInvalidFillCards(List<EntityDef> cardsICanAddToDeck, List<EntityDef> currentDeckCards, List<EntityDef> currentInvalidCards)
	{
		EntityDef[] tplCards = currentInvalidCards.ToArray();
		int i = 0;
		while (i < tplCards.Length)
		{
			DeckFill deckFillChoice = GetFillCard(tplCards[i], cardsICanAddToDeck, null, currentInvalidCards, 1).GetDeckFillChoice(0);
			if (ReplaceInvalidCard(deckFillChoice, cardsICanAddToDeck, currentDeckCards, currentInvalidCards))
			{
				yield return deckFillChoice;
			}
			int num = i + 1;
			i = num;
		}
	}

	private static bool ReplaceInvalidCard(DeckFill choice, List<EntityDef> cardsICanAddToDeck, List<EntityDef> currentDeckCards, List<EntityDef> currentInvalidCards)
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

	private static DeckChoiceFill GetFillCard(EntityDef referenceCard, List<EntityDef> cardsICanAddToDeck, List<EntityDef> currentDeckCards, List<EntityDef> currentInvalidCards, int totalNumChoices = 3)
	{
		if (referenceCard == null && currentInvalidCards != null && currentInvalidCards.Count > 0)
		{
			referenceCard = currentInvalidCards.First();
		}
		int cardRequirementsStartIndex = GetCardRequirementsStartIndex(referenceCard, currentDeckCards);
		DeckChoiceFill deckChoiceFill = new DeckChoiceFill(referenceCard);
		for (int i = cardRequirementsStartIndex; i < s_OrderedCardRequirements.Length; i++)
		{
			if (totalNumChoices <= 0)
			{
				break;
			}
			CardRequirements cardRequirements = s_OrderedCardRequirements[i];
			CardRequirementsCondition condition = cardRequirements.m_condition;
			List<EntityDef> list = cardsICanAddToDeck.FindAll((EntityDef e) => condition(e));
			if (list.Count <= 0)
			{
				continue;
			}
			int num = 8;
			List<EntityDef> list2 = new List<EntityDef>();
			List<EntityDef> list3 = new List<EntityDef>();
			int num2 = int.MinValue;
			foreach (EntityDef item in list.Distinct())
			{
				CollectibleCard card = CollectionManager.Get().GetCard(item.GetCardId(), TAG_PREMIUM.NORMAL);
				num2 = Mathf.Max(num2, card.SuggestWeight);
			}
			foreach (EntityDef item2 in list.Distinct())
			{
				if (num <= 0)
				{
					break;
				}
				CollectibleCard card2 = CollectionManager.Get().GetCard(item2.GetCardId(), TAG_PREMIUM.NORMAL);
				if (num2 - card2.SuggestWeight > 100)
				{
					list3.Add(item2);
				}
				else
				{
					list2.Add(item2);
				}
				num--;
			}
			GeneralUtils.Shuffle(list2);
			GeneralUtils.Shuffle(list3);
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
			deckChoiceFill.m_reason = ((referenceCard == null) ? cardRequirements.GetRequirementReason() : GameStrings.Format("GLUE_RDM_TEMPLATE_REPLACE", referenceCard.GetName()));
		}
		return deckChoiceFill;
	}

	private static int GetCardRequirementsStartIndex(EntityDef referenceCard, List<EntityDef> currentDeckCards)
	{
		if (referenceCard != null)
		{
			for (int i = 0; i < s_OrderedCardRequirements.Length; i++)
			{
				if (s_OrderedCardRequirements[i].m_condition(referenceCard))
				{
					return i;
				}
			}
		}
		else if (currentDeckCards != null)
		{
			for (int j = 0; j < s_OrderedCardRequirements.Length; j++)
			{
				CardRequirements cardRequirements = s_OrderedCardRequirements[j];
				CardRequirementsCondition condition = cardRequirements.m_condition;
				if (currentDeckCards.FindAll((EntityDef e) => condition(e)).Count < cardRequirements.m_requiredCount)
				{
					return j;
				}
			}
		}
		return 0;
	}
}
