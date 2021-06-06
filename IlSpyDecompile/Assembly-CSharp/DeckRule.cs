using System.Collections.Generic;
using Assets;

public abstract class DeckRule
{
	public enum RuleType
	{
		IS_IN_ANY_SUBSET,
		IS_IN_ALL_SUBSETS,
		IS_NOT_ROTATED,
		COUNT_COPIES_OF_EACH_CARD,
		PLAYER_OWNS_EACH_COPY,
		IS_CLASS_CARD_OR_NEUTRAL,
		COUNT_CARDS_IN_DECK,
		HAS_TAG_VALUE,
		DECK_SIZE,
		IS_CARD_PLAYABLE,
		IS_NOT_BANNED_IN_LEAGUE,
		IS_IN_CARDSET,
		IS_IN_FORMAT,
		UNKNOWN
	}

	protected int m_id;

	protected int m_deckRulesetId;

	protected int m_appliesToSubsetId;

	protected HashSet<string> m_appliesToSubset;

	protected bool m_appliesToIsNot;

	protected RuleType m_ruleType;

	protected bool m_ruleIsNot;

	protected int m_minValue;

	protected int m_maxValue;

	protected int m_tag;

	protected int m_tagMinValue;

	protected int m_tagMaxValue;

	protected string m_stringValue;

	protected string m_errorString;

	protected bool m_showInvalidCards;

	protected List<HashSet<string>> m_subsets;

	public RuleType Type => m_ruleType;

	public bool RuleIsNot => m_ruleIsNot;

	public bool ShowInvalidCards => m_showInvalidCards;

	public bool HasAppliesToSubset => m_appliesToSubsetId != 0;

	public DeckRule()
	{
		m_ruleType = RuleType.UNKNOWN;
	}

	public DeckRule(RuleType ruleType, DeckRulesetRuleDbfRecord record)
	{
		m_ruleType = ruleType;
		m_id = record.ID;
		m_deckRulesetId = record.DeckRulesetId;
		m_appliesToSubsetId = record.AppliesToSubsetId;
		m_appliesToIsNot = record.AppliesToIsNot;
		m_ruleIsNot = record.RuleIsNot;
		m_minValue = record.MinValue;
		m_maxValue = record.MaxValue;
		m_tag = record.Tag;
		m_tagMinValue = record.TagMinValue;
		m_tagMaxValue = record.TagMaxValue;
		m_stringValue = record.StringValue;
		m_errorString = ((record.ErrorString != null) ? record.ErrorString.GetString() : "");
		m_showInvalidCards = record.ShowInvalidCards;
		m_subsets = new List<HashSet<string>>();
		if (m_appliesToSubsetId != 0)
		{
			m_appliesToSubset = GameDbf.GetIndex().GetSubsetById(m_appliesToSubsetId);
		}
		m_subsets = GameDbf.GetIndex().GetSubsetsForRule(m_id);
	}

	public int GetID()
	{
		return m_id;
	}

	public static DeckRule CreateFromDBF(DeckRulesetRuleDbfRecord record)
	{
		return GetRule(record);
	}

	public static DeckRule GetRule(DeckRulesetRuleDbfRecord record)
	{
		return record.RuleType switch
		{
			DeckRulesetRule.RuleType.IS_CLASS_OR_NEUTRAL_CARD => new DeckRule_IsClassCardOrNeutral(record), 
			DeckRulesetRule.RuleType.IS_IN_ANY_SUBSET => new DeckRule_IsInAnySubset(record), 
			DeckRulesetRule.RuleType.IS_IN_ALL_SUBSETS => new DeckRule_IsInAllSubsets(record), 
			DeckRulesetRule.RuleType.COUNT_CARDS_IN_DECK => new DeckRule_CountCardsInDeck(record), 
			DeckRulesetRule.RuleType.HAS_TAG_VALUE => new DeckRule_HasTagValue(record), 
			DeckRulesetRule.RuleType.COUNT_COPIES_OF_EACH_CARD => new DeckRule_CountCopiesOfEachCard(record), 
			DeckRulesetRule.RuleType.PLAYER_OWNS_EACH_COPY => new DeckRule_PlayerOwnsEachCopy(record), 
			DeckRulesetRule.RuleType.DECK_SIZE => new DeckRule_DeckSize(record), 
			DeckRulesetRule.RuleType.IS_NOT_ROTATED => new DeckRule_IsNotRotated(record), 
			DeckRulesetRule.RuleType.IS_CARD_PLAYABLE => new DeckRule_IsCardPlayable(record), 
			DeckRulesetRule.RuleType.IS_NOT_BANNED_IN_LEAGUE => new DeckRule_IsNotBannedInLeague(record), 
			DeckRulesetRule.RuleType.IS_IN_CARDSET => new DeckRule_IsInCardset(record), 
			DeckRulesetRule.RuleType.IS_IN_FORMAT => new DeckRule_IsInFormat(record), 
			_ => new DeckRule_DefaultType(record.RuleType.ToString(), record), 
		};
	}

	public virtual bool Filter(EntityDef def, CollectionDeck deck)
	{
		return true;
	}

	public virtual bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		return DefaultYes(out reason);
	}

	public abstract bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason);

	public override string ToString()
	{
		return $"{m_ruleType}, id:{m_id}, deckruleset:{m_deckRulesetId}";
	}

	protected bool GetResult(bool val)
	{
		return val == !m_ruleIsNot;
	}

	protected bool AppliesTo(string cardId)
	{
		if (m_appliesToSubset == null)
		{
			return true;
		}
		return m_appliesToSubset.Contains(cardId) == !m_appliesToIsNot;
	}

	protected bool DefaultYes(out RuleInvalidReason reason)
	{
		reason = null;
		return true;
	}
}
