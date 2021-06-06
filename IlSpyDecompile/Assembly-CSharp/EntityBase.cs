using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;

public abstract class EntityBase
{
	protected static int DEFAULT_TAG_MAP_SIZE = 15;

	protected TagMap m_tags = new TagMap(DEFAULT_TAG_MAP_SIZE);

	protected TagMap m_cachedTagsForDormant = new TagMap(DEFAULT_TAG_MAP_SIZE);

	private List<CardSetTimingDbfRecord> m_cardSetTimingRecords;

	private string m_cardIdInternal;

	protected string m_cardId
	{
		get
		{
			return m_cardIdInternal;
		}
		set
		{
			m_cardIdInternal = value;
			m_cardSetTimingRecords = null;
		}
	}

	public bool IsCollectionManagerFilterManaCostByEven => GetTag(GAME_TAG.COLLECTIONMANAGER_FILTER_MANA_EVEN) != 0;

	public bool IsCollectionManagerFilterManaCostByOdd => GetTag(GAME_TAG.COLLECTIONMANAGER_FILTER_MANA_ODD) != 0;

	public bool HasTag(GAME_TAG tag)
	{
		return GetTag(tag) > 0;
	}

	public TagMap GetTags()
	{
		return m_tags;
	}

	public int GetTag(int tag)
	{
		return m_tags.GetTag(tag);
	}

	public int GetTag(GAME_TAG enumTag)
	{
		return m_tags.GetTag((int)enumTag);
	}

	public TagEnum GetTag<TagEnum>(GAME_TAG enumTag)
	{
		int tag = GetTag(enumTag);
		return (TagEnum)Enum.ToObject(typeof(TagEnum), tag);
	}

	public void SetTag(int tag, int tagValue)
	{
		m_tags.SetTag(tag, tagValue);
	}

	public void SetTag(GAME_TAG tag, int tagValue)
	{
		SetTag((int)tag, tagValue);
	}

	public void SetTag<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		SetTag((int)tag, Convert.ToInt32(tagValue));
	}

	public void SetTags(Map<GAME_TAG, int> tagMap)
	{
		m_tags.SetTags(tagMap);
	}

	public void SetTags(List<Network.Entity.Tag> tags)
	{
		m_tags.SetTags(tags);
	}

	public void ReplaceTags(TagMap tags)
	{
		m_tags.Replace(tags);
	}

	public void ReplaceTags(List<Network.Entity.Tag> tags)
	{
		m_tags.Replace(tags);
	}

	public bool HasReferencedTag(GAME_TAG enumTag)
	{
		return GetReferencedTag(enumTag) > 0;
	}

	public bool HasReferencedTag(int tag)
	{
		return GetReferencedTag(tag) > 0;
	}

	public int GetReferencedTag(GAME_TAG enumTag)
	{
		return GetReferencedTag((int)enumTag);
	}

	public abstract int GetReferencedTag(int tag);

	public bool HasCachedTagForDormant(GAME_TAG tag)
	{
		return GetCachedTagForDormant(tag) > 0;
	}

	public TagMap GetCachedTagsForDormant()
	{
		return m_cachedTagsForDormant;
	}

	public int GetCachedTagForDormant(int tag)
	{
		return m_cachedTagsForDormant.GetTag(tag);
	}

	public int GetCachedTagForDormant(GAME_TAG enumTag)
	{
		return m_cachedTagsForDormant.GetTag((int)enumTag);
	}

	public void SetCachedTagForDormant(int tag, int tagValue)
	{
		m_cachedTagsForDormant.SetTag(tag, tagValue);
	}

	public void SetCachedTagForDormant(GAME_TAG tag, int tagValue)
	{
		SetCachedTagForDormant((int)tag, tagValue);
	}

	public void SetCachedTagForDormant<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		SetCachedTagForDormant((int)tag, Convert.ToInt32(tagValue));
	}

	public void SetCachedTagsForDormant(Map<GAME_TAG, int> tagMap)
	{
		m_cachedTagsForDormant.SetTags(tagMap);
	}

	public void SetCachedTagsForDormant(List<Network.Entity.Tag> tags)
	{
		m_cachedTagsForDormant.SetTags(tags);
	}

	public void ReplaceCachedTagsForDormant(TagMap tags)
	{
		m_cachedTagsForDormant.Replace(tags);
	}

	public void ReplaceCachedTagsForDormant(List<Network.Entity.Tag> tags)
	{
		m_cachedTagsForDormant.Replace(tags);
	}

	public bool HasCharge()
	{
		return HasTag(GAME_TAG.CHARGE);
	}

	public bool ReferencesCharge()
	{
		return HasReferencedTag(GAME_TAG.CHARGE);
	}

	public bool HasBattlecry()
	{
		return HasTag(GAME_TAG.BATTLECRY);
	}

	public bool ReferencesBattlecry()
	{
		return HasReferencedTag(GAME_TAG.BATTLECRY);
	}

	public bool CanBeTargetedBySpells()
	{
		if (!HasTag(GAME_TAG.CANT_BE_TARGETED_BY_SPELLS))
		{
			return !HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return false;
	}

	public bool CanBeTargetedByHeroPowers()
	{
		if (!HasTag(GAME_TAG.CANT_BE_TARGETED_BY_HERO_POWERS))
		{
			return !HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return false;
	}

	public bool CanBeTargetedByBattlecries()
	{
		if (!HasTag(GAME_TAG.CANT_BE_TARGETED_BY_BATTLECRIES))
		{
			return !HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return false;
	}

	public bool HasTriggerVisual()
	{
		return HasTag(GAME_TAG.TRIGGER_VISUAL);
	}

	public bool HasInspire()
	{
		return HasTag(GAME_TAG.INSPIRE);
	}

	public bool HasOverKill()
	{
		return HasTag(GAME_TAG.OVERKILL);
	}

	public bool HasSpellburst()
	{
		return HasTag(GAME_TAG.SPELLBURST);
	}

	public bool HasFrenzy()
	{
		return HasTag(GAME_TAG.FRENZY);
	}

	public bool IsImmune()
	{
		if (!HasTag(GAME_TAG.IMMUNE))
		{
			return HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return true;
	}

	public bool DontShowImmune()
	{
		return HasTag(GAME_TAG.DONT_SHOW_IMMUNE);
	}

	public bool IsPoisonous()
	{
		if (!HasTag(GAME_TAG.POISONOUS))
		{
			return HasTag(GAME_TAG.NON_KEYWORD_POISONOUS);
		}
		return true;
	}

	public bool HasLifesteal()
	{
		return HasTag(GAME_TAG.LIFESTEAL);
	}

	public bool HasAura()
	{
		return HasTag(GAME_TAG.AURA);
	}

	public bool HasHealthMin()
	{
		return GetTag(GAME_TAG.HEALTH_MINIMUM) > 0;
	}

	public bool ReferencesImmune()
	{
		return HasReferencedTag(GAME_TAG.IMMUNE);
	}

	public bool IsEnraged()
	{
		if (HasTag(GAME_TAG.ENRAGED))
		{
			return GetDamage() > 0;
		}
		return false;
	}

	public bool IsFreeze()
	{
		return HasTag(GAME_TAG.FREEZE);
	}

	public int GetDamage()
	{
		return GetTag(GAME_TAG.DAMAGE);
	}

	public bool IsFrozen()
	{
		return HasTag(GAME_TAG.FROZEN);
	}

	public bool IsDormant()
	{
		return HasTag(GAME_TAG.DORMANT);
	}

	public bool IsAsleep()
	{
		if (GetNumTurnsInPlay() != 0)
		{
			return false;
		}
		if (GetNumAttacksThisTurn() != 0)
		{
			return false;
		}
		if (HasCharge())
		{
			return false;
		}
		if (HasRush())
		{
			return false;
		}
		if (ReferencesAutoAttack())
		{
			return false;
		}
		if (HasTag(GAME_TAG.UNTOUCHABLE))
		{
			return false;
		}
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && !GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_SLEEP_FX))
		{
			return false;
		}
		return true;
	}

	public bool IsStealthed()
	{
		return HasTag(GAME_TAG.STEALTH);
	}

	public bool ReferencesStealth()
	{
		return HasReferencedTag(GAME_TAG.STEALTH);
	}

	public bool HasTaunt()
	{
		return HasTag(GAME_TAG.TAUNT);
	}

	public bool ReferencesTaunt()
	{
		return HasReferencedTag(GAME_TAG.TAUNT);
	}

	public bool HasDivineShield()
	{
		return HasTag(GAME_TAG.DIVINE_SHIELD);
	}

	public bool ReferencesDivineShield()
	{
		return HasReferencedTag(GAME_TAG.DIVINE_SHIELD);
	}

	public bool ReferencesAutoAttack()
	{
		return HasReferencedTag(GAME_TAG.AUTOATTACK);
	}

	public bool IsHero()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 3;
	}

	public bool IsHeroPower()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 10;
	}

	public bool IsSidekickHero()
	{
		return HasTag(GAME_TAG.SIDEKICK);
	}

	public bool IsSidekickHeroPower()
	{
		return HasTag(GAME_TAG.SIDEKICK_HERO_POWER);
	}

	public bool IsGameModeButton()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 12;
	}

	public bool IsMinion()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 4;
	}

	public bool IsSpell()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 5;
	}

	public bool IsWeapon()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 7;
	}

	public bool IsElite()
	{
		return GetTag(GAME_TAG.ELITE) > 0;
	}

	public bool IsHeroSkin()
	{
		if (!IsHero())
		{
			return false;
		}
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(m_cardIdInternal);
		if (cardRecord == null)
		{
			return false;
		}
		return cardRecord.CardHero != null;
	}

	public bool IsHeroPowerOrGameModeButton()
	{
		if (GetTag(GAME_TAG.CARDTYPE) != 10)
		{
			return GetTag(GAME_TAG.CARDTYPE) == 12;
		}
		return true;
	}

	public bool IsMoveMinionHoverTarget()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 22;
	}

	public bool IsCustomCoin()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(m_cardIdInternal);
		return GameDbf.Coin.HasRecord((CoinDbfRecord coin) => coin.CardId == cardRecord.ID);
	}

	public TAG_CARDTYPE GetCardType()
	{
		return (TAG_CARDTYPE)GetTag(GAME_TAG.CARDTYPE);
	}

	public TAG_PUZZLE_TYPE GetPuzzleType()
	{
		return (TAG_PUZZLE_TYPE)GetTag(GAME_TAG.PUZZLE_TYPE);
	}

	public bool IsGame()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 1;
	}

	public bool IsPlayer()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 2;
	}

	public bool IsExhausted()
	{
		return HasTag(GAME_TAG.EXHAUSTED);
	}

	public bool IsAttached()
	{
		return HasTag(GAME_TAG.ATTACHED);
	}

	public bool IsRecentlyArrived()
	{
		return HasTag(GAME_TAG.RECENTLY_ARRIVED);
	}

	public bool IsObfuscated()
	{
		return HasTag(GAME_TAG.OBFUSCATED);
	}

	public bool HasSecretDeathrattle()
	{
		return HasTag(GAME_TAG.SECRET_DEATHRATTLE);
	}

	public bool IsSecret()
	{
		return HasTag(GAME_TAG.SECRET);
	}

	public bool IsQuest()
	{
		return HasTag(GAME_TAG.QUEST);
	}

	public bool IsSideQuest()
	{
		return HasTag(GAME_TAG.SIDEQUEST);
	}

	public bool IsSigil()
	{
		return HasTag(GAME_TAG.SIGIL);
	}

	public bool IsPuzzle()
	{
		return HasTag(GAME_TAG.PUZZLE);
	}

	public bool IsRulebook()
	{
		return HasTag(GAME_TAG.RULEBOOK);
	}

	public bool IsSecretOrQuestOrSideQuestOrSigil()
	{
		if (!IsSecret() && !IsQuest() && !IsSideQuest())
		{
			return IsSigil();
		}
		return true;
	}

	public bool IsRevealed()
	{
		return HasTag(GAME_TAG.REVEALED);
	}

	public bool IsTwinspell()
	{
		return HasTag(GAME_TAG.TWINSPELL);
	}

	public bool ReferencesSecret()
	{
		return HasReferencedTag(GAME_TAG.SECRET);
	}

	public bool CanAttack()
	{
		if (!HasTag(GAME_TAG.CANT_ATTACK))
		{
			return !HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return false;
	}

	public bool CannotAttackHeroes()
	{
		return HasTag(GAME_TAG.CANNOT_ATTACK_HEROES);
	}

	public bool CanBeAttacked()
	{
		if (!HasTag(GAME_TAG.CANT_BE_ATTACKED))
		{
			return !HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return false;
	}

	public bool CanBeTargetedByOpponents()
	{
		if (!HasTag(GAME_TAG.CANT_BE_TARGETED_BY_OPPONENTS))
		{
			return !HasTag(GAME_TAG.UNTOUCHABLE);
		}
		return false;
	}

	public bool IsMagnet()
	{
		return HasTag(GAME_TAG.MAGNET);
	}

	public int GetNumTurnsInPlay()
	{
		return GetTag(GAME_TAG.NUM_TURNS_IN_PLAY);
	}

	public int GetNumAttacksThisTurn()
	{
		return GetTag(GAME_TAG.NUM_ATTACKS_THIS_TURN);
	}

	public TAG_SPELL_SCHOOL GetSpellPowerSchool()
	{
		if (HasTag(GAME_TAG.SPELLPOWER))
		{
			return TAG_SPELL_SCHOOL.NONE;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_ARCANE))
		{
			return TAG_SPELL_SCHOOL.ARCANE;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_FIRE))
		{
			return TAG_SPELL_SCHOOL.FIRE;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_FROST))
		{
			return TAG_SPELL_SCHOOL.FROST;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_NATURE))
		{
			return TAG_SPELL_SCHOOL.NATURE;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_HOLY))
		{
			return TAG_SPELL_SCHOOL.HOLY;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_SHADOW))
		{
			return TAG_SPELL_SCHOOL.SHADOW;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_FEL))
		{
			return TAG_SPELL_SCHOOL.FEL;
		}
		if (HasTag(GAME_TAG.SPELLPOWER_PHYSICAL))
		{
			return TAG_SPELL_SCHOOL.PHYSICAL_COMBAT;
		}
		return TAG_SPELL_SCHOOL.NONE;
	}

	public bool HasSpellPower()
	{
		if (!HasTag(GAME_TAG.SPELLPOWER) && !HasTag(GAME_TAG.SPELLPOWER_ARCANE) && !HasTag(GAME_TAG.SPELLPOWER_FIRE) && !HasTag(GAME_TAG.SPELLPOWER_FROST) && !HasTag(GAME_TAG.SPELLPOWER_NATURE) && !HasTag(GAME_TAG.SPELLPOWER_HOLY) && !HasTag(GAME_TAG.SPELLPOWER_SHADOW) && !HasTag(GAME_TAG.SPELLPOWER_FEL))
		{
			return HasTag(GAME_TAG.SPELLPOWER_PHYSICAL);
		}
		return true;
	}

	public bool HasHeroPowerDamage()
	{
		return HasTag(GAME_TAG.HEROPOWER_DAMAGE);
	}

	public bool IsAffectedBySpellPower()
	{
		return HasTag(GAME_TAG.AFFECTED_BY_SPELL_POWER);
	}

	public bool HasSpellPowerDouble()
	{
		return HasTag(GAME_TAG.SPELLPOWER_DOUBLE);
	}

	public bool ReferencesSpellPower()
	{
		return HasReferencedTag(GAME_TAG.SPELLPOWER);
	}

	public bool HasHealingDoesDamageHint()
	{
		return HasTag(GAME_TAG.HEALING_DOES_DAMAGE_HINT);
	}

	public bool HasLifestealDoesDamageHint()
	{
		return HasTag(GAME_TAG.LIFESTEAL_DOES_DAMAGE_HINT);
	}

	public int GetCost()
	{
		return GetTag(GAME_TAG.COST);
	}

	public int GetATK()
	{
		return GetTag(GAME_TAG.ATK);
	}

	public int GetHealth()
	{
		return GetTag(GAME_TAG.HEALTH);
	}

	public int GetDurability()
	{
		return GetTag(GAME_TAG.DURABILITY);
	}

	public int GetArmor()
	{
		return GetTag(GAME_TAG.ARMOR);
	}

	public int GetAttached()
	{
		return GetTag(GAME_TAG.ATTACHED);
	}

	public TAG_ZONE GetZone()
	{
		return (TAG_ZONE)GetTag(GAME_TAG.ZONE);
	}

	public int GetZonePosition()
	{
		return GetTag(GAME_TAG.ZONE_POSITION);
	}

	public int GetCreatorId()
	{
		return GetTag(GAME_TAG.CREATOR);
	}

	public int GetCreatorDBID()
	{
		return GetTag(GAME_TAG.CREATOR_DBID);
	}

	public int GetControllerId()
	{
		return GetTag(GAME_TAG.CONTROLLER);
	}

	public int GetFatigue()
	{
		return GetTag(GAME_TAG.FATIGUE);
	}

	public int GetWindfury()
	{
		return GetTag(GAME_TAG.WINDFURY);
	}

	public bool HasWindfury()
	{
		return GetTag(GAME_TAG.WINDFURY) > 0;
	}

	public bool ReferencesWindfury()
	{
		return HasReferencedTag(GAME_TAG.WINDFURY);
	}

	public int GetExtraAttacksThisTurn()
	{
		return GetTag(GAME_TAG.EXTRA_ATTACKS_THIS_TURN);
	}

	public bool HasCombo()
	{
		return HasTag(GAME_TAG.COMBO);
	}

	public bool HasOverload()
	{
		return HasTag(GAME_TAG.OVERLOAD);
	}

	public bool HasDeathrattle()
	{
		return HasTag(GAME_TAG.DEATHRATTLE);
	}

	public bool ReferencesDeathrattle()
	{
		return HasReferencedTag(GAME_TAG.DEATHRATTLE);
	}

	public bool IsSilenced()
	{
		return HasTag(GAME_TAG.SILENCED);
	}

	public int GetEntityId()
	{
		return GetTag(GAME_TAG.ENTITY_ID);
	}

	public bool IsCharacter()
	{
		if (!IsHero())
		{
			return IsMinion();
		}
		return true;
	}

	public bool IsItem()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 8;
	}

	public bool IsToken()
	{
		return GetTag(GAME_TAG.CARDTYPE) == 9;
	}

	public bool HasCustomKeywordEffect()
	{
		return HasTag(GAME_TAG.CUSTOM_KEYWORD_EFFECT);
	}

	public int GetDisplayedCreatorId()
	{
		return GetTag(GAME_TAG.DISPLAYED_CREATOR);
	}

	public bool HasRush()
	{
		return HasTag(GAME_TAG.RUSH);
	}

	public bool ReferencesRush()
	{
		return HasReferencedTag(GAME_TAG.RUSH);
	}

	public int GetTechLevel()
	{
		return GetTag(GAME_TAG.TECH_LEVEL);
	}

	public bool IsCoreCard()
	{
		TAG_CARD_SET cardSet = GetCardSet();
		if (cardSet == TAG_CARD_SET.INVALID)
		{
			return false;
		}
		return GameDbf.GetIndex().GetCardSet(cardSet).IsCoreCardSet;
	}

	public virtual TAG_CLASS GetClass()
	{
		IEnumerable<TAG_CLASS> classes = GetClasses();
		if (classes.Count() == 0)
		{
			return TAG_CLASS.INVALID;
		}
		if (1 == classes.Count())
		{
			return classes.First();
		}
		return TAG_CLASS.NEUTRAL;
	}

	public virtual IEnumerable<TAG_CLASS> GetClasses(Comparison<TAG_CLASS> classSorter = null)
	{
		List<TAG_CLASS> list = new List<TAG_CLASS>();
		uint num = (uint)GetTag(GAME_TAG.MULTIPLE_CLASSES);
		if (num == 0)
		{
			TAG_CLASS tag = GetTag<TAG_CLASS>(GAME_TAG.CLASS);
			list.Add(tag);
		}
		else
		{
			int num2 = 1;
			while (num != 0)
			{
				if (1 == (num & 1) && EnumUtils.TryCast<TAG_CLASS>(num2, out var outVal))
				{
					list.Add(outVal);
				}
				num >>= 1;
				num2++;
			}
		}
		if (classSorter != null)
		{
			list.Sort(classSorter);
		}
		return list;
	}

	public bool IsMultiClass()
	{
		return GetClasses().Count() > 1;
	}

	public bool IsAllowedInClass(TAG_CLASS classID)
	{
		return GetClasses().Contains(classID);
	}

	public TAG_MULTI_CLASS_GROUP GetMultiClassGroup()
	{
		return (TAG_MULTI_CLASS_GROUP)GetTag(GAME_TAG.MULTI_CLASS_GROUP);
	}

	public string GetCardId()
	{
		return m_cardId;
	}

	public void SetCardId(string cardId)
	{
		m_cardId = cardId;
		OnUpdateCardId();
	}

	protected virtual void OnUpdateCardId()
	{
	}

	public TAG_CARD_SET GetCardSet()
	{
		TAG_CARD_SET tag = (TAG_CARD_SET)GetTag(GAME_TAG.CARD_SET);
		if (tag != 0)
		{
			return tag;
		}
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(m_cardIdInternal);
		if (cardRecord != null)
		{
			if (m_cardSetTimingRecords == null)
			{
				m_cardSetTimingRecords = GameDbf.CardSetTiming.GetRecords((CardSetTimingDbfRecord r) => r.CardId == cardRecord.ID);
				if (!HearthstoneApplication.IsHearthstoneRunning)
				{
					m_cardSetTimingRecords.Sort((CardSetTimingDbfRecord a, CardSetTimingDbfRecord b) => -a.ID.CompareTo(b.ID));
				}
			}
			foreach (CardSetTimingDbfRecord cardSetTimingRecord in m_cardSetTimingRecords)
			{
				if (!HearthstoneApplication.IsHearthstoneRunning || SpecialEventManager.Get().IsEventActive(cardSetTimingRecord.EventTimingEvent, activeIfDoesNotExist: false))
				{
					return (TAG_CARD_SET)cardSetTimingRecord.CardSetId;
				}
			}
		}
		return TAG_CARD_SET.INVALID;
	}

	public TAG_SPELL_SCHOOL GetSpellSchool()
	{
		return GetTag<TAG_SPELL_SCHOOL>(GAME_TAG.SPELL_SCHOOL);
	}
}
