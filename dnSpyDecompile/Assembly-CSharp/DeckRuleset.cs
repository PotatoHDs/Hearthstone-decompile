using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusShared;
using UnityEngine;

// Token: 0x020007C9 RID: 1993
public class DeckRuleset
{
	// Token: 0x17000679 RID: 1657
	// (get) Token: 0x06006DD9 RID: 28121 RVA: 0x0023677C File Offset: 0x0023497C
	public int Id
	{
		get
		{
			return this.m_id;
		}
	}

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x06006DDA RID: 28122 RVA: 0x00236784 File Offset: 0x00234984
	public List<DeckRule> Rules
	{
		get
		{
			return this.m_rules;
		}
	}

	// Token: 0x06006DDB RID: 28123 RVA: 0x0023678C File Offset: 0x0023498C
	public static DeckRuleset GetDeckRuleset(int id)
	{
		FormatType formatType;
		DeckRuleset result;
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
		}.TryGetValue(id, out formatType))
		{
			result = DeckRuleset.GetRuleset(formatType);
		}
		else
		{
			result = DeckRuleset.GetDeckRulesetFromDBF(id);
		}
		return result;
	}

	// Token: 0x06006DDC RID: 28124 RVA: 0x002367D8 File Offset: 0x002349D8
	private static DeckRuleset GetDeckRulesetFromDBF(int id)
	{
		if (id <= 0)
		{
			return null;
		}
		if (!GameDbf.DeckRuleset.HasRecord(id))
		{
			Debug.LogErrorFormat("DeckRuleset not found for id {0}", new object[]
			{
				id
			});
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
		deckRuleset.m_rules.Sort(new Comparison<DeckRule>(DeckRuleViolation.SortComparison_Rule));
		return deckRuleset;
	}

	// Token: 0x06006DDD RID: 28125 RVA: 0x00236874 File Offset: 0x00234A74
	public static DeckRuleset GetRuleset(FormatType formatType)
	{
		DeckRuleset deckRulesetFromDBF;
		if (!DeckRuleset.s_FormatRulesets.TryGetValue(formatType, out deckRulesetFromDBF))
		{
			int id;
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
			}.TryGetValue(formatType, out id))
			{
				Debug.LogError("DeckRuleset.GetRuleset called with invalid format type " + formatType.ToString());
				return null;
			}
			if (!GameDbf.DeckRuleset.HasRecord(id))
			{
				Debug.LogError("Error generating ruleset for id " + id.ToString() + ", could not find ruleset DBF");
				return null;
			}
			deckRulesetFromDBF = DeckRuleset.GetDeckRulesetFromDBF(id);
			DeckRuleset.s_FormatRulesets.Add(formatType, deckRulesetFromDBF);
		}
		return deckRulesetFromDBF;
	}

	// Token: 0x06006DDE RID: 28126 RVA: 0x0023691E File Offset: 0x00234B1E
	public static DeckRuleset GetPVPDRRuleset()
	{
		if (DeckRuleset.s_PVPDRRuleset == null)
		{
			DeckRuleset.s_PVPDRRuleset = DeckRuleset.BuildPVPDRRuleset();
		}
		return DeckRuleset.s_PVPDRRuleset;
	}

	// Token: 0x06006DDF RID: 28127 RVA: 0x00236936 File Offset: 0x00234B36
	public static DeckRuleset GetPVPDRDisplayRuleset()
	{
		if (DeckRuleset.s_PVPDRDisplayRuleset == null)
		{
			DeckRuleset.s_PVPDRDisplayRuleset = DeckRuleset.BuildPVPDRDisplayRuleset();
		}
		return DeckRuleset.s_PVPDRDisplayRuleset;
	}

	// Token: 0x06006DE0 RID: 28128 RVA: 0x00236950 File Offset: 0x00234B50
	private static DeckRuleset BuildPVPDRRuleset()
	{
		PvPDungeonRunDisplay pvPDungeonRunDisplay = PvPDungeonRunDisplay.Get();
		if (pvPDungeonRunDisplay == null)
		{
			Debug.LogError("Unable to get PVPDR DeckRuleset; PvPDungeonRunDisplay unavailable");
			return null;
		}
		PVPDRLobbyDataModel pvpdrlobbyDataModel = pvPDungeonRunDisplay.GetPVPDRLobbyDataModel();
		PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(pvpdrlobbyDataModel.Season);
		if (record == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; unknown PVPDRSeason {0}", new object[]
			{
				pvpdrlobbyDataModel.Season
			});
			return null;
		}
		ScenarioDbfRecord record2 = GameDbf.Scenario.GetRecord(record.ScenarioId);
		if (record2 == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; No scenario specified for season {0}", new object[]
			{
				record.ID
			});
			return null;
		}
		DeckRuleset deckRulesetFromDBF = DeckRuleset.GetDeckRulesetFromDBF(record2.DeckRulesetId);
		if (deckRulesetFromDBF == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; no DeckRuleset found with id {0}}", new object[]
			{
				record2.DeckRulesetId
			});
		}
		return deckRulesetFromDBF;
	}

	// Token: 0x06006DE1 RID: 28129 RVA: 0x00236A18 File Offset: 0x00234C18
	private static DeckRuleset BuildPVPDRDisplayRuleset()
	{
		PvPDungeonRunDisplay pvPDungeonRunDisplay = PvPDungeonRunDisplay.Get();
		if (pvPDungeonRunDisplay == null)
		{
			Debug.LogError("Unable to get PVPDR DeckRuleset; PvPDungeonRunDisplay unavailable");
			return null;
		}
		PVPDRLobbyDataModel pvpdrlobbyDataModel = pvPDungeonRunDisplay.GetPVPDRLobbyDataModel();
		PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(pvpdrlobbyDataModel.Season);
		if (record == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; unknown PVPDRSeason {0}", new object[]
			{
				pvpdrlobbyDataModel.Season
			});
			return null;
		}
		DeckRuleset deckRulesetFromDBF = DeckRuleset.GetDeckRulesetFromDBF(record.DeckDisplayRulesetId);
		if (deckRulesetFromDBF == null)
		{
			Debug.LogErrorFormat("Unable to get PVPDR DeckRuleset; no DeckRuleset found with id {0}}", new object[]
			{
				record.DeckDisplayRulesetId
			});
		}
		return deckRulesetFromDBF;
	}

	// Token: 0x06006DE2 RID: 28130 RVA: 0x00236AAC File Offset: 0x00234CAC
	public bool Filter(EntityDef entity, CollectionDeck deck, params DeckRule.RuleType[] ignoreRules)
	{
		if (this.EntityIgnoresRuleset(entity) || this.EntityInDeckIgnoresRuleset(deck))
		{
			return true;
		}
		foreach (DeckRule deckRule in this.m_rules)
		{
			if ((ignoreRules == null || !ignoreRules.Contains(deckRule.Type)) && !deckRule.Filter(entity, deck))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06006DE3 RID: 28131 RVA: 0x00236B30 File Offset: 0x00234D30
	public bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, params DeckRule.RuleType[] ignoreRules)
	{
		RuleInvalidReason ruleInvalidReason;
		DeckRule deckRule;
		return this.CanAddToDeck(def, premium, deck, out ruleInvalidReason, out deckRule, ignoreRules);
	}

	// Token: 0x06006DE4 RID: 28132 RVA: 0x00236B4C File Offset: 0x00234D4C
	public bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason, out DeckRule brokenRule, params DeckRule.RuleType[] ignoreRules)
	{
		reason = null;
		brokenRule = null;
		if (this.EntityIgnoresRuleset(def) || this.EntityInDeckIgnoresRuleset(deck))
		{
			return true;
		}
		foreach (DeckRule deckRule in this.m_rules)
		{
			if ((ignoreRules == null || !ignoreRules.Contains(deckRule.Type)) && !deckRule.CanAddToDeck(def, premium, deck, out reason))
			{
				brokenRule = deckRule;
				return false;
			}
		}
		return true;
	}

	// Token: 0x06006DE5 RID: 28133 RVA: 0x00236BE0 File Offset: 0x00234DE0
	public bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out List<RuleInvalidReason> reasons, out List<DeckRule> brokenRules, params DeckRule.RuleType[] ignoreRules)
	{
		if (this.EntityIgnoresRuleset(def) || this.EntityInDeckIgnoresRuleset(deck))
		{
			reasons = null;
			brokenRules = null;
			return true;
		}
		reasons = new List<RuleInvalidReason>();
		brokenRules = new List<DeckRule>();
		foreach (DeckRule deckRule in this.m_rules)
		{
			RuleInvalidReason item;
			if ((ignoreRules == null || !ignoreRules.Contains(deckRule.Type)) && !deckRule.CanAddToDeck(def, premium, deck, out item))
			{
				reasons.Add(item);
				brokenRules.Add(deckRule);
			}
		}
		return brokenRules.Count == 0;
	}

	// Token: 0x06006DE6 RID: 28134 RVA: 0x00236C98 File Offset: 0x00234E98
	public bool IsDeckValid(CollectionDeck deck)
	{
		IList<DeckRuleViolation> list;
		return this.IsDeckValid(deck, out list, Array.Empty<DeckRule.RuleType>());
	}

	// Token: 0x06006DE7 RID: 28135 RVA: 0x00236CB4 File Offset: 0x00234EB4
	public bool IsDeckValid(CollectionDeck deck, out IList<DeckRuleViolation> violations, params DeckRule.RuleType[] ignoreRules)
	{
		List<DeckRuleViolation> list = new List<DeckRuleViolation>();
		violations = list;
		List<RuleInvalidReason> list2 = new List<RuleInvalidReason>();
		if (this.EntityInDeckIgnoresRuleset(deck))
		{
			return true;
		}
		bool result = true;
		foreach (DeckRule deckRule in this.m_rules)
		{
			if (ignoreRules == null || !ignoreRules.Contains(deckRule.Type))
			{
				RuleInvalidReason ruleInvalidReason;
				bool flag = deckRule.IsDeckValid(deck, out ruleInvalidReason);
				if (!flag)
				{
					list2.Add(ruleInvalidReason);
					DeckRuleViolation item = new DeckRuleViolation(deckRule, ruleInvalidReason.DisplayError);
					violations.Add(item);
					result = false;
				}
				Log.DeckRuleset.Print("validating rule={0} deck={1} result={2} reason={3}", new object[]
				{
					deckRule,
					deck,
					flag,
					ruleInvalidReason
				});
			}
		}
		list.Sort(new Comparison<DeckRuleViolation>(DeckRuleViolation.SortComparison_Violation));
		this.CollapseSpecialBrokenRules(violations, list2);
		return result;
	}

	// Token: 0x06006DE8 RID: 28136 RVA: 0x00236DB0 File Offset: 0x00234FB0
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
			for (int j = (list == null) ? -1 : (list.Count - 1); j >= 0; j--)
			{
				int index = list[j];
				violations.RemoveAt(index);
				reasons.RemoveAt(index);
			}
			string text = GameStrings.Format("GLUE_COLLECTION_DECK_RULE_MISSING_CARDS", new object[]
			{
				num
			});
			RuleInvalidReason item = new RuleInvalidReason(text, num, false);
			reasons.Add(item);
			DeckRuleViolation item2 = new DeckRuleViolation(deckRule, text);
			violations.Add(item2);
		}
	}

	// Token: 0x06006DE9 RID: 28137 RVA: 0x00236EF0 File Offset: 0x002350F0
	private DeckRule_DeckSize GetDeckSizeRule(CollectionDeck deck)
	{
		DeckRule deckRule;
		if (this.m_rules != null)
		{
			deckRule = this.m_rules.FirstOrDefault((DeckRule r) => r is DeckRule_DeckSize);
		}
		else
		{
			deckRule = null;
		}
		DeckRule deckRule2 = deckRule;
		if (deckRule2 != null)
		{
			return deckRule2 as DeckRule_DeckSize;
		}
		return null;
	}

	// Token: 0x06006DEA RID: 28138 RVA: 0x00236F40 File Offset: 0x00235140
	public int GetDeckSize(CollectionDeck deck)
	{
		DeckRule_DeckSize deckSizeRule = this.GetDeckSizeRule(deck);
		if (deckSizeRule != null)
		{
			return deckSizeRule.GetMaximumDeckSize(deck);
		}
		return 30;
	}

	// Token: 0x06006DEB RID: 28139 RVA: 0x00236F64 File Offset: 0x00235164
	public int GetMinimumAllowedDeckSize(CollectionDeck deck)
	{
		DeckRule_DeckSize deckSizeRule = this.GetDeckSizeRule(deck);
		if (deckSizeRule != null)
		{
			return deckSizeRule.GetMinimumDeckSize(deck);
		}
		return 30;
	}

	// Token: 0x06006DEC RID: 28140 RVA: 0x00236F86 File Offset: 0x00235186
	public bool HasOwnershipOrRotatedRule()
	{
		object obj;
		if (this.m_rules != null)
		{
			obj = this.m_rules.FirstOrDefault((DeckRule r) => r.Type == DeckRule.RuleType.IS_NOT_ROTATED || (r.Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY && !r.RuleIsNot));
		}
		else
		{
			obj = null;
		}
		return obj != null;
	}

	// Token: 0x06006DED RID: 28141 RVA: 0x00236FC0 File Offset: 0x002351C0
	public bool FilterFailsOnShowInvalidRule(EntityDef entity, CollectionDeck deck)
	{
		bool result = false;
		foreach (DeckRule deckRule in this.m_rules)
		{
			if (!deckRule.Filter(entity, deck))
			{
				if (!deckRule.ShowInvalidCards)
				{
					result = false;
					break;
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06006DEE RID: 28142 RVA: 0x0023702C File Offset: 0x0023522C
	public bool HasIsPlayableRule()
	{
		foreach (DeckRule deckRule in this.m_rules)
		{
			if (deckRule.Type == DeckRule.RuleType.IS_CARD_PLAYABLE && !deckRule.RuleIsNot)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006DEF RID: 28143 RVA: 0x00237094 File Offset: 0x00235294
	public int GetMaxCopiesOfCardAllowed(EntityDef entity)
	{
		int num = int.MaxValue;
		foreach (DeckRule deckRule in this.m_rules)
		{
			int b;
			if (deckRule is DeckRule_CountCopiesOfEachCard && ((DeckRule_CountCopiesOfEachCard)deckRule).GetMaxCopies(entity, out b))
			{
				num = Mathf.Min(num, b);
			}
		}
		return num;
	}

	// Token: 0x06006DF0 RID: 28144 RVA: 0x00237108 File Offset: 0x00235308
	public HashSet<TAG_CARD_SET> GetAllowedCardSets()
	{
		HashSet<TAG_CARD_SET> hashSet = new HashSet<TAG_CARD_SET>();
		foreach (DeckRule deckRule in this.m_rules)
		{
			if (deckRule is DeckRule_IsInAnySubset)
			{
				foreach (int item in GameDbf.GetIndex().GetCardSetIdsForSubsetRule(deckRule.GetID()))
				{
					hashSet.Add((TAG_CARD_SET)item);
				}
			}
		}
		return hashSet;
	}

	// Token: 0x06006DF1 RID: 28145 RVA: 0x002371B4 File Offset: 0x002353B4
	public bool EntityIgnoresRuleset(EntityDef def)
	{
		return def.HasTag(GAME_TAG.IGNORE_DECK_RULESET);
	}

	// Token: 0x06006DF2 RID: 28146 RVA: 0x002371C4 File Offset: 0x002353C4
	public bool EntityInDeckIgnoresRuleset(CollectionDeck deck)
	{
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			if (this.EntityIgnoresRuleset(entityDef))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04005808 RID: 22536
	private int m_id;

	// Token: 0x04005809 RID: 22537
	private List<DeckRule> m_rules;

	// Token: 0x0400580A RID: 22538
	private static Map<FormatType, DeckRuleset> s_FormatRulesets = new Map<FormatType, DeckRuleset>();

	// Token: 0x0400580B RID: 22539
	private static DeckRuleset s_PVPDRRuleset;

	// Token: 0x0400580C RID: 22540
	private static DeckRuleset s_PVPDRDisplayRuleset;

	// Token: 0x02002361 RID: 9057
	public enum DeckRulesetConstant
	{
		// Token: 0x0400E686 RID: 59014
		UnknownRuleset,
		// Token: 0x0400E687 RID: 59015
		WildRuleset,
		// Token: 0x0400E688 RID: 59016
		StandardRuleset,
		// Token: 0x0400E689 RID: 59017
		ClassicRuleset = 482
	}
}
