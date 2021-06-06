using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusShared;
using UnityEngine;

public class DeckRuleset
{
	public enum DeckRulesetConstant
	{
		UnknownRuleset = 0,
		WildRuleset = 1,
		StandardRuleset = 2,
		ClassicRuleset = 482
	}

	private int m_id;

	private List<DeckRule> m_rules;

	private static Map<FormatType, DeckRuleset> s_FormatRulesets = new Map<FormatType, DeckRuleset>();

	private static DeckRuleset s_PVPDRRuleset;

	private static DeckRuleset s_PVPDRDisplayRuleset;

	public int Id => m_id;

	public List<DeckRule> Rules => m_rules;

	public static DeckRuleset GetDeckRuleset(int id)
	{
		if (new Map<int, FormatType>
		{
			{
				1,
				FormatType.FT_WILD
			},
			{
				2,
				FormatType.FT_STANDARD
			},
			{
				482,
				FormatType.FT_CLASSIC
			}
		}.TryGetValue(id, out var value))
		{
			return GetRuleset(value);
		}
		return GetDeckRulesetFromDBF(id);
	}

	private static DeckRuleset GetDeckRulesetFromDBF(int id)
	{
		if (id <= 0)
		{
			return null;
		}
		if (!GameDbf.DeckRuleset.HasRecord(id))
		{
			Debug.LogErrorFormat("DeckRuleset not found for id {0}", id);
			return null;
		}
		DeckRuleset deckRuleset = new DeckRuleset();
		deckRuleset.m_id = id;
		deckRuleset.m_rules = new List<DeckRule>();
		DeckRulesetRuleDbfRecord[] rulesForDeckRuleset = GameDbf.GetIndex().GetRulesForDeckRuleset(id);
		for (int i = 0; i < rulesForDeckRuleset.Length; i++)
		{
			DeckRule item = DeckRule.CreateFromDBF(rulesForDeckRuleset[i]);
			deckRuleset.m_rules.Add(item);
		}
		deckRuleset.m_rules.Sort(DeckRuleViolation.SortComparison_Rule);
		return deckRuleset;
	}

	public static DeckRuleset GetRuleset(FormatType formatType)
	{
		if (!s_FormatRulesets.TryGetValue(formatType, out var value))
		{
			if (!new Map<FormatType, int>
			{
				{
					FormatType.FT_WILD,
					1
				},
				{
					FormatType.FT_STANDARD,
					2
				},
				{
					FormatType.FT_CLASSIC,
					482
				}
			}.TryGetValue(formatType, out var value2))
			{
				Debug.LogError("DeckRuleset.GetRuleset called with invalid format type " + formatType);
				return null;
			}
			if (!GameDbf.DeckRuleset.HasRecord(value2))
			{
				Debug.LogError("Error generating ruleset for id " + value2 + ", could not find ruleset DBF");
				return null;
			}
			value = GetDeckRulesetFromDBF(value2);
			s_FormatRulesets.Add(formatType, value);
		}
		return value;
	}

	public static DeckRuleset GetPVPDRRuleset()
	{
		if (s_PVPDRRuleset == null)
		{
			s_PVPDRRuleset = BuildPVPDRRuleset();
		}
		return s_PVPDRRuleset;
	}

	public static DeckRuleset GetPVPDRDisplayRuleset()
	{
		if (s_PVPDRDisplayRuleset == null)
		{
			s_PVPDRDisplayRuleset = BuildPVPDRDisplayRuleset();
		}
		return s_PVPDRDisplayRuleset;
	}

	private static DeckRuleset BuildPVPDRRuleset()
	{
		PvPDungeonRunDisplay pvPDungeonRunDisplay = PvPDungeonRunDisplay.Get();
		if (pvPDungeonRunDisplay == null)
		{
			Debug.LogError("Unable to get PVPDR DeckRuleset; PvPDungeonRunDisplay unavailable");
			return null;
		}
		PVPDRLobbyDataModel pVPDRLobbyDataModel = pvPDungeonRunDisplay.GetPVPDRLobbyDataModel();
		PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(pVPDRLobbyDataModel.Season);
		if (record == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; unknown PVPDRSeason {0}", pVPDRLobbyDataModel.Season);
			return null;
		}
		ScenarioDbfRecord record2 = GameDbf.Scenario.GetRecord(record.ScenarioId);
		if (record2 == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; No scenario specified for season {0}", record.ID);
			return null;
		}
		DeckRuleset deckRulesetFromDBF = GetDeckRulesetFromDBF(record2.DeckRulesetId);
		if (deckRulesetFromDBF == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; no DeckRuleset found with id {0}}", record2.DeckRulesetId);
		}
		return deckRulesetFromDBF;
	}

	private static DeckRuleset BuildPVPDRDisplayRuleset()
	{
		PvPDungeonRunDisplay pvPDungeonRunDisplay = PvPDungeonRunDisplay.Get();
		if (pvPDungeonRunDisplay == null)
		{
			Debug.LogError("Unable to get PVPDR DeckRuleset; PvPDungeonRunDisplay unavailable");
			return null;
		}
		PVPDRLobbyDataModel pVPDRLobbyDataModel = pvPDungeonRunDisplay.GetPVPDRLobbyDataModel();
		PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(pVPDRLobbyDataModel.Season);
		if (record == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; unknown PVPDRSeason {0}", pVPDRLobbyDataModel.Season);
			return null;
		}
		DeckRuleset deckRulesetFromDBF = GetDeckRulesetFromDBF(record.DeckDisplayRulesetId);
		if (deckRulesetFromDBF == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; no DeckRuleset found with id {0}}", record.DeckDisplayRulesetId);
		}
		return deckRulesetFromDBF;
	}

	public bool Filter(EntityDef entity, CollectionDeck deck, params DeckRule.RuleType[] ignoreRules)
	{
		if (EntityIgnoresRuleset(entity) || EntityInDeckIgnoresRuleset(deck))
		{
			return true;
		}
		foreach (DeckRule rule in m_rules)
		{
			if ((ignoreRules == null || !ignoreRules.Contains(rule.Type)) && !rule.Filter(entity, deck))
			{
				return false;
			}
		}
		return true;
	}

	public bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, params DeckRule.RuleType[] ignoreRules)
	{
		RuleInvalidReason reason;
		DeckRule brokenRule;
		return CanAddToDeck(def, premium, deck, out reason, out brokenRule, ignoreRules);
	}

	public bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason, out DeckRule brokenRule, params DeckRule.RuleType[] ignoreRules)
	{
		reason = null;
		brokenRule = null;
		if (EntityIgnoresRuleset(def) || EntityInDeckIgnoresRuleset(deck))
		{
			return true;
		}
		foreach (DeckRule rule in m_rules)
		{
			if ((ignoreRules == null || !ignoreRules.Contains(rule.Type)) && !rule.CanAddToDeck(def, premium, deck, out reason))
			{
				brokenRule = rule;
				return false;
			}
		}
		return true;
	}

	public bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out List<RuleInvalidReason> reasons, out List<DeckRule> brokenRules, params DeckRule.RuleType[] ignoreRules)
	{
		if (EntityIgnoresRuleset(def) || EntityInDeckIgnoresRuleset(deck))
		{
			reasons = null;
			brokenRules = null;
			return true;
		}
		reasons = new List<RuleInvalidReason>();
		brokenRules = new List<DeckRule>();
		foreach (DeckRule rule in m_rules)
		{
			if ((ignoreRules == null || !ignoreRules.Contains(rule.Type)) && !rule.CanAddToDeck(def, premium, deck, out var reason))
			{
				reasons.Add(reason);
				brokenRules.Add(rule);
			}
		}
		if (brokenRules.Count == 0)
		{
			return true;
		}
		return false;
	}

	public bool IsDeckValid(CollectionDeck deck)
	{
		IList<DeckRuleViolation> violations;
		return IsDeckValid(deck, out violations);
	}

	public bool IsDeckValid(CollectionDeck deck, out IList<DeckRuleViolation> violations, params DeckRule.RuleType[] ignoreRules)
	{
		List<DeckRuleViolation> list = (List<DeckRuleViolation>)(violations = new List<DeckRuleViolation>());
		List<RuleInvalidReason> list2 = new List<RuleInvalidReason>();
		if (EntityInDeckIgnoresRuleset(deck))
		{
			return true;
		}
		bool result = true;
		foreach (DeckRule rule in m_rules)
		{
			if (ignoreRules == null || !ignoreRules.Contains(rule.Type))
			{
				RuleInvalidReason reason;
				bool flag = rule.IsDeckValid(deck, out reason);
				if (!flag)
				{
					list2.Add(reason);
					DeckRuleViolation item = new DeckRuleViolation(rule, reason.DisplayError);
					violations.Add(item);
					result = false;
				}
				Log.DeckRuleset.Print("validating rule={0} deck={1} result={2} reason={3}", rule, deck, flag, reason);
			}
		}
		list.Sort(DeckRuleViolation.SortComparison_Violation);
		CollapseSpecialBrokenRules(violations, list2);
		return result;
	}

	private void CollapseSpecialBrokenRules(IList<DeckRuleViolation> violations, List<RuleInvalidReason> reasons)
	{
		if (reasons.Count <= 1)
		{
			return;
		}
		DeckRule deckRule = null;
		int num = 0;
		List<int> list = null;
		for (int i = 0; i < violations.Count; i++)
		{
			DeckRule rule = violations[i].Rule;
			if (rule.Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY && !rule.RuleIsNot)
			{
				if (list == null)
				{
					list = new List<int>();
				}
				if (deckRule == null)
				{
					deckRule = rule;
				}
				list.Add(i);
				num += reasons[i].CountParam;
			}
			else if (rule.Type == DeckRule.RuleType.DECK_SIZE && reasons[i].IsMinimum)
			{
				if (list == null)
				{
					list = new List<int>();
				}
				if (deckRule == null)
				{
					deckRule = rule;
				}
				list.Add(i);
				num += reasons[i].CountParam;
			}
		}
		if (list != null && list.Count > 1)
		{
			for (int num2 = ((list == null) ? (-1) : (list.Count - 1)); num2 >= 0; num2--)
			{
				int index = list[num2];
				violations.RemoveAt(index);
				reasons.RemoveAt(index);
			}
			string text = GameStrings.Format("GLUE_COLLECTION_DECK_RULE_MISSING_CARDS", num);
			RuleInvalidReason item = new RuleInvalidReason(text, num);
			reasons.Add(item);
			DeckRuleViolation item2 = new DeckRuleViolation(deckRule, text);
			violations.Add(item2);
		}
	}

	private DeckRule_DeckSize GetDeckSizeRule(CollectionDeck deck)
	{
		DeckRule deckRule = ((m_rules == null) ? null : m_rules.FirstOrDefault((DeckRule r) => r is DeckRule_DeckSize));
		if (deckRule != null)
		{
			return deckRule as DeckRule_DeckSize;
		}
		return null;
	}

	public int GetDeckSize(CollectionDeck deck)
	{
		return GetDeckSizeRule(deck)?.GetMaximumDeckSize(deck) ?? 30;
	}

	public int GetMinimumAllowedDeckSize(CollectionDeck deck)
	{
		return GetDeckSizeRule(deck)?.GetMinimumDeckSize(deck) ?? 30;
	}

	public bool HasOwnershipOrRotatedRule()
	{
		return ((m_rules == null) ? null : m_rules.FirstOrDefault((DeckRule r) => r.Type == DeckRule.RuleType.IS_NOT_ROTATED || (r.Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY && !r.RuleIsNot))) != null;
	}

	public bool FilterFailsOnShowInvalidRule(EntityDef entity, CollectionDeck deck)
	{
		bool result = false;
		foreach (DeckRule rule in m_rules)
		{
			if (!rule.Filter(entity, deck))
			{
				if (!rule.ShowInvalidCards)
				{
					return false;
				}
				result = true;
			}
		}
		return result;
	}

	public bool HasIsPlayableRule()
	{
		foreach (DeckRule rule in m_rules)
		{
			if (rule.Type == DeckRule.RuleType.IS_CARD_PLAYABLE && !rule.RuleIsNot)
			{
				return true;
			}
		}
		return false;
	}

	public int GetMaxCopiesOfCardAllowed(EntityDef entity)
	{
		int num = int.MaxValue;
		foreach (DeckRule rule in m_rules)
		{
			if (rule is DeckRule_CountCopiesOfEachCard && ((DeckRule_CountCopiesOfEachCard)rule).GetMaxCopies(entity, out var maxCopies))
			{
				num = Mathf.Min(num, maxCopies);
			}
		}
		return num;
	}

	public HashSet<TAG_CARD_SET> GetAllowedCardSets()
	{
		HashSet<TAG_CARD_SET> hashSet = new HashSet<TAG_CARD_SET>();
		foreach (DeckRule rule in m_rules)
		{
			if (!(rule is DeckRule_IsInAnySubset))
			{
				continue;
			}
			foreach (int item in GameDbf.GetIndex().GetCardSetIdsForSubsetRule(rule.GetID()))
			{
				hashSet.Add((TAG_CARD_SET)item);
			}
		}
		return hashSet;
	}

	public bool EntityIgnoresRuleset(EntityDef def)
	{
		return def.HasTag(GAME_TAG.IGNORE_DECK_RULESET);
	}

	public bool EntityInDeckIgnoresRuleset(CollectionDeck deck)
	{
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(slot.CardID);
			if (EntityIgnoresRuleset(entityDef))
			{
				return true;
			}
		}
		return false;
	}
}
