using System;
using System.Collections.Generic;
using Assets;

// Token: 0x020007BA RID: 1978
public abstract class DeckRule
{
	// Token: 0x06006D87 RID: 28039 RVA: 0x0023522C File Offset: 0x0023342C
	public DeckRule()
	{
		this.m_ruleType = DeckRule.RuleType.UNKNOWN;
	}

	// Token: 0x06006D88 RID: 28040 RVA: 0x0023523C File Offset: 0x0023343C
	public DeckRule(DeckRule.RuleType ruleType, DeckRulesetRuleDbfRecord record)
	{
		this.m_ruleType = ruleType;
		this.m_id = record.ID;
		this.m_deckRulesetId = record.DeckRulesetId;
		this.m_appliesToSubsetId = record.AppliesToSubsetId;
		this.m_appliesToIsNot = record.AppliesToIsNot;
		this.m_ruleIsNot = record.RuleIsNot;
		this.m_minValue = record.MinValue;
		this.m_maxValue = record.MaxValue;
		this.m_tag = record.Tag;
		this.m_tagMinValue = record.TagMinValue;
		this.m_tagMaxValue = record.TagMaxValue;
		this.m_stringValue = record.StringValue;
		this.m_errorString = ((record.ErrorString != null) ? record.ErrorString.GetString(true) : "");
		this.m_showInvalidCards = record.ShowInvalidCards;
		this.m_subsets = new List<HashSet<string>>();
		if (this.m_appliesToSubsetId != 0)
		{
			this.m_appliesToSubset = GameDbf.GetIndex().GetSubsetById(this.m_appliesToSubsetId);
		}
		this.m_subsets = GameDbf.GetIndex().GetSubsetsForRule(this.m_id);
	}

	// Token: 0x06006D89 RID: 28041 RVA: 0x00235346 File Offset: 0x00233546
	public int GetID()
	{
		return this.m_id;
	}

	// Token: 0x06006D8A RID: 28042 RVA: 0x0023534E File Offset: 0x0023354E
	public static DeckRule CreateFromDBF(DeckRulesetRuleDbfRecord record)
	{
		return DeckRule.GetRule(record);
	}

	// Token: 0x06006D8B RID: 28043 RVA: 0x00235358 File Offset: 0x00233558
	public static DeckRule GetRule(DeckRulesetRuleDbfRecord record)
	{
		switch (record.RuleType)
		{
		case DeckRulesetRule.RuleType.HAS_TAG_VALUE:
			return new DeckRule_HasTagValue(record);
		case DeckRulesetRule.RuleType.COUNT_CARDS_IN_DECK:
			return new DeckRule_CountCardsInDeck(record);
		case DeckRulesetRule.RuleType.COUNT_COPIES_OF_EACH_CARD:
			return new DeckRule_CountCopiesOfEachCard(record);
		case DeckRulesetRule.RuleType.IS_IN_ANY_SUBSET:
			return new DeckRule_IsInAnySubset(record);
		case DeckRulesetRule.RuleType.IS_IN_ALL_SUBSETS:
			return new DeckRule_IsInAllSubsets(record);
		case DeckRulesetRule.RuleType.PLAYER_OWNS_EACH_COPY:
			return new DeckRule_PlayerOwnsEachCopy(record);
		case DeckRulesetRule.RuleType.IS_NOT_ROTATED:
			return new DeckRule_IsNotRotated(record);
		case DeckRulesetRule.RuleType.DECK_SIZE:
			return new DeckRule_DeckSize(record);
		case DeckRulesetRule.RuleType.IS_CLASS_OR_NEUTRAL_CARD:
			return new DeckRule_IsClassCardOrNeutral(record);
		case DeckRulesetRule.RuleType.IS_CARD_PLAYABLE:
			return new DeckRule_IsCardPlayable(record);
		case DeckRulesetRule.RuleType.IS_NOT_BANNED_IN_LEAGUE:
			return new DeckRule_IsNotBannedInLeague(record);
		case DeckRulesetRule.RuleType.IS_IN_CARDSET:
			return new DeckRule_IsInCardset(record);
		case DeckRulesetRule.RuleType.IS_IN_FORMAT:
			return new DeckRule_IsInFormat(record);
		}
		return new DeckRule_DefaultType(record.RuleType.ToString(), record);
	}

	// Token: 0x17000675 RID: 1653
	// (get) Token: 0x06006D8C RID: 28044 RVA: 0x0023543F File Offset: 0x0023363F
	public DeckRule.RuleType Type
	{
		get
		{
			return this.m_ruleType;
		}
	}

	// Token: 0x17000676 RID: 1654
	// (get) Token: 0x06006D8D RID: 28045 RVA: 0x00235447 File Offset: 0x00233647
	public bool RuleIsNot
	{
		get
		{
			return this.m_ruleIsNot;
		}
	}

	// Token: 0x17000677 RID: 1655
	// (get) Token: 0x06006D8E RID: 28046 RVA: 0x0023544F File Offset: 0x0023364F
	public bool ShowInvalidCards
	{
		get
		{
			return this.m_showInvalidCards;
		}
	}

	// Token: 0x17000678 RID: 1656
	// (get) Token: 0x06006D8F RID: 28047 RVA: 0x00235457 File Offset: 0x00233657
	public bool HasAppliesToSubset
	{
		get
		{
			return this.m_appliesToSubsetId != 0;
		}
	}

	// Token: 0x06006D90 RID: 28048 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool Filter(EntityDef def, CollectionDeck deck)
	{
		return true;
	}

	// Token: 0x06006D91 RID: 28049 RVA: 0x00235462 File Offset: 0x00233662
	public virtual bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		return this.DefaultYes(out reason);
	}

	// Token: 0x06006D92 RID: 28050
	public abstract bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason);

	// Token: 0x06006D93 RID: 28051 RVA: 0x0023546C File Offset: 0x0023366C
	public override string ToString()
	{
		return string.Format("{0}, id:{1}, deckruleset:{2}", this.m_ruleType, this.m_id, this.m_deckRulesetId);
	}

	// Token: 0x06006D94 RID: 28052 RVA: 0x00235499 File Offset: 0x00233699
	protected bool GetResult(bool val)
	{
		return val == !this.m_ruleIsNot;
	}

	// Token: 0x06006D95 RID: 28053 RVA: 0x002354A7 File Offset: 0x002336A7
	protected bool AppliesTo(string cardId)
	{
		return this.m_appliesToSubset == null || this.m_appliesToSubset.Contains(cardId) == !this.m_appliesToIsNot;
	}

	// Token: 0x06006D96 RID: 28054 RVA: 0x002354CA File Offset: 0x002336CA
	protected bool DefaultYes(out RuleInvalidReason reason)
	{
		reason = null;
		return true;
	}

	// Token: 0x040057F8 RID: 22520
	protected int m_id;

	// Token: 0x040057F9 RID: 22521
	protected int m_deckRulesetId;

	// Token: 0x040057FA RID: 22522
	protected int m_appliesToSubsetId;

	// Token: 0x040057FB RID: 22523
	protected HashSet<string> m_appliesToSubset;

	// Token: 0x040057FC RID: 22524
	protected bool m_appliesToIsNot;

	// Token: 0x040057FD RID: 22525
	protected DeckRule.RuleType m_ruleType;

	// Token: 0x040057FE RID: 22526
	protected bool m_ruleIsNot;

	// Token: 0x040057FF RID: 22527
	protected int m_minValue;

	// Token: 0x04005800 RID: 22528
	protected int m_maxValue;

	// Token: 0x04005801 RID: 22529
	protected int m_tag;

	// Token: 0x04005802 RID: 22530
	protected int m_tagMinValue;

	// Token: 0x04005803 RID: 22531
	protected int m_tagMaxValue;

	// Token: 0x04005804 RID: 22532
	protected string m_stringValue;

	// Token: 0x04005805 RID: 22533
	protected string m_errorString;

	// Token: 0x04005806 RID: 22534
	protected bool m_showInvalidCards;

	// Token: 0x04005807 RID: 22535
	protected List<HashSet<string>> m_subsets;

	// Token: 0x02002360 RID: 9056
	public enum RuleType
	{
		// Token: 0x0400E677 RID: 58999
		IS_IN_ANY_SUBSET,
		// Token: 0x0400E678 RID: 59000
		IS_IN_ALL_SUBSETS,
		// Token: 0x0400E679 RID: 59001
		IS_NOT_ROTATED,
		// Token: 0x0400E67A RID: 59002
		COUNT_COPIES_OF_EACH_CARD,
		// Token: 0x0400E67B RID: 59003
		PLAYER_OWNS_EACH_COPY,
		// Token: 0x0400E67C RID: 59004
		IS_CLASS_CARD_OR_NEUTRAL,
		// Token: 0x0400E67D RID: 59005
		COUNT_CARDS_IN_DECK,
		// Token: 0x0400E67E RID: 59006
		HAS_TAG_VALUE,
		// Token: 0x0400E67F RID: 59007
		DECK_SIZE,
		// Token: 0x0400E680 RID: 59008
		IS_CARD_PLAYABLE,
		// Token: 0x0400E681 RID: 59009
		IS_NOT_BANNED_IN_LEAGUE,
		// Token: 0x0400E682 RID: 59010
		IS_IN_CARDSET,
		// Token: 0x0400E683 RID: 59011
		IS_IN_FORMAT,
		// Token: 0x0400E684 RID: 59012
		UNKNOWN
	}
}
