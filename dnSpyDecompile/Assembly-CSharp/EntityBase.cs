using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;

// Token: 0x020008A2 RID: 2210
public abstract class EntityBase
{
	// Token: 0x17000702 RID: 1794
	// (get) Token: 0x06007A39 RID: 31289 RVA: 0x0027CC93 File Offset: 0x0027AE93
	// (set) Token: 0x06007A38 RID: 31288 RVA: 0x0027CC83 File Offset: 0x0027AE83
	protected string m_cardId
	{
		get
		{
			return this.m_cardIdInternal;
		}
		set
		{
			this.m_cardIdInternal = value;
			this.m_cardSetTimingRecords = null;
		}
	}

	// Token: 0x06007A3A RID: 31290 RVA: 0x0027CC9B File Offset: 0x0027AE9B
	public bool HasTag(GAME_TAG tag)
	{
		return this.GetTag(tag) > 0;
	}

	// Token: 0x06007A3B RID: 31291 RVA: 0x0027CCA7 File Offset: 0x0027AEA7
	public TagMap GetTags()
	{
		return this.m_tags;
	}

	// Token: 0x06007A3C RID: 31292 RVA: 0x0027CCAF File Offset: 0x0027AEAF
	public int GetTag(int tag)
	{
		return this.m_tags.GetTag(tag);
	}

	// Token: 0x06007A3D RID: 31293 RVA: 0x0027CCC0 File Offset: 0x0027AEC0
	public int GetTag(GAME_TAG enumTag)
	{
		return this.m_tags.GetTag((int)enumTag);
	}

	// Token: 0x06007A3E RID: 31294 RVA: 0x0027CCDC File Offset: 0x0027AEDC
	public TagEnum GetTag<TagEnum>(GAME_TAG enumTag)
	{
		int tag = this.GetTag(enumTag);
		return (TagEnum)((object)Enum.ToObject(typeof(TagEnum), tag));
	}

	// Token: 0x06007A3F RID: 31295 RVA: 0x0027CD06 File Offset: 0x0027AF06
	public void SetTag(int tag, int tagValue)
	{
		this.m_tags.SetTag(tag, tagValue);
	}

	// Token: 0x06007A40 RID: 31296 RVA: 0x0027CD15 File Offset: 0x0027AF15
	public void SetTag(GAME_TAG tag, int tagValue)
	{
		this.SetTag((int)tag, tagValue);
	}

	// Token: 0x06007A41 RID: 31297 RVA: 0x0027CD1F File Offset: 0x0027AF1F
	public void SetTag<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		this.SetTag((int)tag, Convert.ToInt32(tagValue));
	}

	// Token: 0x06007A42 RID: 31298 RVA: 0x0027CD33 File Offset: 0x0027AF33
	public void SetTags(Map<GAME_TAG, int> tagMap)
	{
		this.m_tags.SetTags(tagMap);
	}

	// Token: 0x06007A43 RID: 31299 RVA: 0x0027CD41 File Offset: 0x0027AF41
	public void SetTags(List<Network.Entity.Tag> tags)
	{
		this.m_tags.SetTags(tags);
	}

	// Token: 0x06007A44 RID: 31300 RVA: 0x0027CD4F File Offset: 0x0027AF4F
	public void ReplaceTags(TagMap tags)
	{
		this.m_tags.Replace(tags);
	}

	// Token: 0x06007A45 RID: 31301 RVA: 0x0027CD5D File Offset: 0x0027AF5D
	public void ReplaceTags(List<Network.Entity.Tag> tags)
	{
		this.m_tags.Replace(tags);
	}

	// Token: 0x06007A46 RID: 31302 RVA: 0x0027CD6B File Offset: 0x0027AF6B
	public bool HasReferencedTag(GAME_TAG enumTag)
	{
		return this.GetReferencedTag(enumTag) > 0;
	}

	// Token: 0x06007A47 RID: 31303 RVA: 0x0027CD77 File Offset: 0x0027AF77
	public bool HasReferencedTag(int tag)
	{
		return this.GetReferencedTag(tag) > 0;
	}

	// Token: 0x06007A48 RID: 31304 RVA: 0x0027CD83 File Offset: 0x0027AF83
	public int GetReferencedTag(GAME_TAG enumTag)
	{
		return this.GetReferencedTag((int)enumTag);
	}

	// Token: 0x06007A49 RID: 31305
	public abstract int GetReferencedTag(int tag);

	// Token: 0x06007A4A RID: 31306 RVA: 0x0027CD8C File Offset: 0x0027AF8C
	public bool HasCachedTagForDormant(GAME_TAG tag)
	{
		return this.GetCachedTagForDormant(tag) > 0;
	}

	// Token: 0x06007A4B RID: 31307 RVA: 0x0027CD98 File Offset: 0x0027AF98
	public TagMap GetCachedTagsForDormant()
	{
		return this.m_cachedTagsForDormant;
	}

	// Token: 0x06007A4C RID: 31308 RVA: 0x0027CDA0 File Offset: 0x0027AFA0
	public int GetCachedTagForDormant(int tag)
	{
		return this.m_cachedTagsForDormant.GetTag(tag);
	}

	// Token: 0x06007A4D RID: 31309 RVA: 0x0027CDB0 File Offset: 0x0027AFB0
	public int GetCachedTagForDormant(GAME_TAG enumTag)
	{
		return this.m_cachedTagsForDormant.GetTag((int)enumTag);
	}

	// Token: 0x06007A4E RID: 31310 RVA: 0x0027CDCB File Offset: 0x0027AFCB
	public void SetCachedTagForDormant(int tag, int tagValue)
	{
		this.m_cachedTagsForDormant.SetTag(tag, tagValue);
	}

	// Token: 0x06007A4F RID: 31311 RVA: 0x0027CDDA File Offset: 0x0027AFDA
	public void SetCachedTagForDormant(GAME_TAG tag, int tagValue)
	{
		this.SetCachedTagForDormant((int)tag, tagValue);
	}

	// Token: 0x06007A50 RID: 31312 RVA: 0x0027CDE4 File Offset: 0x0027AFE4
	public void SetCachedTagForDormant<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		this.SetCachedTagForDormant((int)tag, Convert.ToInt32(tagValue));
	}

	// Token: 0x06007A51 RID: 31313 RVA: 0x0027CDF8 File Offset: 0x0027AFF8
	public void SetCachedTagsForDormant(Map<GAME_TAG, int> tagMap)
	{
		this.m_cachedTagsForDormant.SetTags(tagMap);
	}

	// Token: 0x06007A52 RID: 31314 RVA: 0x0027CE06 File Offset: 0x0027B006
	public void SetCachedTagsForDormant(List<Network.Entity.Tag> tags)
	{
		this.m_cachedTagsForDormant.SetTags(tags);
	}

	// Token: 0x06007A53 RID: 31315 RVA: 0x0027CE14 File Offset: 0x0027B014
	public void ReplaceCachedTagsForDormant(TagMap tags)
	{
		this.m_cachedTagsForDormant.Replace(tags);
	}

	// Token: 0x06007A54 RID: 31316 RVA: 0x0027CE22 File Offset: 0x0027B022
	public void ReplaceCachedTagsForDormant(List<Network.Entity.Tag> tags)
	{
		this.m_cachedTagsForDormant.Replace(tags);
	}

	// Token: 0x06007A55 RID: 31317 RVA: 0x0027CE30 File Offset: 0x0027B030
	public bool HasCharge()
	{
		return this.HasTag(GAME_TAG.CHARGE);
	}

	// Token: 0x06007A56 RID: 31318 RVA: 0x0027CE3D File Offset: 0x0027B03D
	public bool ReferencesCharge()
	{
		return this.HasReferencedTag(GAME_TAG.CHARGE);
	}

	// Token: 0x06007A57 RID: 31319 RVA: 0x0027CE4A File Offset: 0x0027B04A
	public bool HasBattlecry()
	{
		return this.HasTag(GAME_TAG.BATTLECRY);
	}

	// Token: 0x06007A58 RID: 31320 RVA: 0x0027CE57 File Offset: 0x0027B057
	public bool ReferencesBattlecry()
	{
		return this.HasReferencedTag(GAME_TAG.BATTLECRY);
	}

	// Token: 0x06007A59 RID: 31321 RVA: 0x0027CE64 File Offset: 0x0027B064
	public bool CanBeTargetedBySpells()
	{
		return !this.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_SPELLS) && !this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A5A RID: 31322 RVA: 0x0027CE83 File Offset: 0x0027B083
	public bool CanBeTargetedByHeroPowers()
	{
		return !this.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_HERO_POWERS) && !this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A5B RID: 31323 RVA: 0x0027CEA2 File Offset: 0x0027B0A2
	public bool CanBeTargetedByBattlecries()
	{
		return !this.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_BATTLECRIES) && !this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A5C RID: 31324 RVA: 0x0027CEC1 File Offset: 0x0027B0C1
	public bool HasTriggerVisual()
	{
		return this.HasTag(GAME_TAG.TRIGGER_VISUAL);
	}

	// Token: 0x06007A5D RID: 31325 RVA: 0x0027CECB File Offset: 0x0027B0CB
	public bool HasInspire()
	{
		return this.HasTag(GAME_TAG.INSPIRE);
	}

	// Token: 0x06007A5E RID: 31326 RVA: 0x0027CED8 File Offset: 0x0027B0D8
	public bool HasOverKill()
	{
		return this.HasTag(GAME_TAG.OVERKILL);
	}

	// Token: 0x06007A5F RID: 31327 RVA: 0x0027CEE5 File Offset: 0x0027B0E5
	public bool HasSpellburst()
	{
		return this.HasTag(GAME_TAG.SPELLBURST);
	}

	// Token: 0x06007A60 RID: 31328 RVA: 0x0027CEF2 File Offset: 0x0027B0F2
	public bool HasFrenzy()
	{
		return this.HasTag(GAME_TAG.FRENZY);
	}

	// Token: 0x06007A61 RID: 31329 RVA: 0x0027CEFF File Offset: 0x0027B0FF
	public bool IsImmune()
	{
		return this.HasTag(GAME_TAG.IMMUNE) || this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A62 RID: 31330 RVA: 0x0027CF1B File Offset: 0x0027B11B
	public bool DontShowImmune()
	{
		return this.HasTag(GAME_TAG.DONT_SHOW_IMMUNE);
	}

	// Token: 0x06007A63 RID: 31331 RVA: 0x0027CF28 File Offset: 0x0027B128
	public bool IsPoisonous()
	{
		return this.HasTag(GAME_TAG.POISONOUS) || this.HasTag(GAME_TAG.NON_KEYWORD_POISONOUS);
	}

	// Token: 0x06007A64 RID: 31332 RVA: 0x0027CF44 File Offset: 0x0027B144
	public bool HasLifesteal()
	{
		return this.HasTag(GAME_TAG.LIFESTEAL);
	}

	// Token: 0x06007A65 RID: 31333 RVA: 0x0027CF51 File Offset: 0x0027B151
	public bool HasAura()
	{
		return this.HasTag(GAME_TAG.AURA);
	}

	// Token: 0x06007A66 RID: 31334 RVA: 0x0027CF5E File Offset: 0x0027B15E
	public bool HasHealthMin()
	{
		return this.GetTag(GAME_TAG.HEALTH_MINIMUM) > 0;
	}

	// Token: 0x06007A67 RID: 31335 RVA: 0x0027CF6E File Offset: 0x0027B16E
	public bool ReferencesImmune()
	{
		return this.HasReferencedTag(GAME_TAG.IMMUNE);
	}

	// Token: 0x06007A68 RID: 31336 RVA: 0x0027CF7B File Offset: 0x0027B17B
	public bool IsEnraged()
	{
		return this.HasTag(GAME_TAG.ENRAGED) && this.GetDamage() > 0;
	}

	// Token: 0x06007A69 RID: 31337 RVA: 0x0027CF95 File Offset: 0x0027B195
	public bool IsFreeze()
	{
		return this.HasTag(GAME_TAG.FREEZE);
	}

	// Token: 0x06007A6A RID: 31338 RVA: 0x0027CFA2 File Offset: 0x0027B1A2
	public int GetDamage()
	{
		return this.GetTag(GAME_TAG.DAMAGE);
	}

	// Token: 0x06007A6B RID: 31339 RVA: 0x0027CFAC File Offset: 0x0027B1AC
	public bool IsFrozen()
	{
		return this.HasTag(GAME_TAG.FROZEN);
	}

	// Token: 0x06007A6C RID: 31340 RVA: 0x0027CFB9 File Offset: 0x0027B1B9
	public bool IsDormant()
	{
		return this.HasTag(GAME_TAG.DORMANT);
	}

	// Token: 0x06007A6D RID: 31341 RVA: 0x0027CFC8 File Offset: 0x0027B1C8
	public bool IsAsleep()
	{
		return this.GetNumTurnsInPlay() == 0 && this.GetNumAttacksThisTurn() == 0 && !this.HasCharge() && !this.HasRush() && !this.ReferencesAutoAttack() && !this.HasTag(GAME_TAG.UNTOUCHABLE) && (GameState.Get() == null || GameState.Get().GetGameEntity() == null || GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_SLEEP_FX));
	}

	// Token: 0x06007A6E RID: 31342 RVA: 0x0027D03A File Offset: 0x0027B23A
	public bool IsStealthed()
	{
		return this.HasTag(GAME_TAG.STEALTH);
	}

	// Token: 0x06007A6F RID: 31343 RVA: 0x0027D047 File Offset: 0x0027B247
	public bool ReferencesStealth()
	{
		return this.HasReferencedTag(GAME_TAG.STEALTH);
	}

	// Token: 0x06007A70 RID: 31344 RVA: 0x0027D054 File Offset: 0x0027B254
	public bool HasTaunt()
	{
		return this.HasTag(GAME_TAG.TAUNT);
	}

	// Token: 0x06007A71 RID: 31345 RVA: 0x0027D061 File Offset: 0x0027B261
	public bool ReferencesTaunt()
	{
		return this.HasReferencedTag(GAME_TAG.TAUNT);
	}

	// Token: 0x06007A72 RID: 31346 RVA: 0x0027D06E File Offset: 0x0027B26E
	public bool HasDivineShield()
	{
		return this.HasTag(GAME_TAG.DIVINE_SHIELD);
	}

	// Token: 0x06007A73 RID: 31347 RVA: 0x0027D07B File Offset: 0x0027B27B
	public bool ReferencesDivineShield()
	{
		return this.HasReferencedTag(GAME_TAG.DIVINE_SHIELD);
	}

	// Token: 0x06007A74 RID: 31348 RVA: 0x0027D088 File Offset: 0x0027B288
	public bool ReferencesAutoAttack()
	{
		return this.HasReferencedTag(GAME_TAG.AUTOATTACK);
	}

	// Token: 0x06007A75 RID: 31349 RVA: 0x0027D095 File Offset: 0x0027B295
	public bool IsHero()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 3;
	}

	// Token: 0x06007A76 RID: 31350 RVA: 0x0027D0A5 File Offset: 0x0027B2A5
	public bool IsHeroPower()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 10;
	}

	// Token: 0x06007A77 RID: 31351 RVA: 0x0027D0B6 File Offset: 0x0027B2B6
	public bool IsSidekickHero()
	{
		return this.HasTag(GAME_TAG.SIDEKICK);
	}

	// Token: 0x06007A78 RID: 31352 RVA: 0x0027D0C3 File Offset: 0x0027B2C3
	public bool IsSidekickHeroPower()
	{
		return this.HasTag(GAME_TAG.SIDEKICK_HERO_POWER);
	}

	// Token: 0x06007A79 RID: 31353 RVA: 0x0027D0D0 File Offset: 0x0027B2D0
	public bool IsGameModeButton()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 12;
	}

	// Token: 0x06007A7A RID: 31354 RVA: 0x0027D0E1 File Offset: 0x0027B2E1
	public bool IsMinion()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 4;
	}

	// Token: 0x06007A7B RID: 31355 RVA: 0x0027D0F1 File Offset: 0x0027B2F1
	public bool IsSpell()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 5;
	}

	// Token: 0x06007A7C RID: 31356 RVA: 0x0027D101 File Offset: 0x0027B301
	public bool IsWeapon()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 7;
	}

	// Token: 0x06007A7D RID: 31357 RVA: 0x0027D111 File Offset: 0x0027B311
	public bool IsElite()
	{
		return this.GetTag(GAME_TAG.ELITE) > 0;
	}

	// Token: 0x06007A7E RID: 31358 RVA: 0x0027D120 File Offset: 0x0027B320
	public bool IsHeroSkin()
	{
		if (!this.IsHero())
		{
			return false;
		}
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(this.m_cardIdInternal);
		return cardRecord != null && cardRecord.CardHero != null;
	}

	// Token: 0x06007A7F RID: 31359 RVA: 0x0027D156 File Offset: 0x0027B356
	public bool IsHeroPowerOrGameModeButton()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 10 || this.GetTag(GAME_TAG.CARDTYPE) == 12;
	}

	// Token: 0x06007A80 RID: 31360 RVA: 0x0027D178 File Offset: 0x0027B378
	public bool IsMoveMinionHoverTarget()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 22;
	}

	// Token: 0x06007A81 RID: 31361 RVA: 0x0027D18C File Offset: 0x0027B38C
	public bool IsCustomCoin()
	{
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(this.m_cardIdInternal);
		return GameDbf.Coin.HasRecord((CoinDbfRecord coin) => coin.CardId == cardRecord.ID);
	}

	// Token: 0x06007A82 RID: 31362 RVA: 0x0027D1CB File Offset: 0x0027B3CB
	public TAG_CARDTYPE GetCardType()
	{
		return (TAG_CARDTYPE)this.GetTag(GAME_TAG.CARDTYPE);
	}

	// Token: 0x06007A83 RID: 31363 RVA: 0x0027D1D8 File Offset: 0x0027B3D8
	public TAG_PUZZLE_TYPE GetPuzzleType()
	{
		return (TAG_PUZZLE_TYPE)this.GetTag(GAME_TAG.PUZZLE_TYPE);
	}

	// Token: 0x06007A84 RID: 31364 RVA: 0x0027D1E5 File Offset: 0x0027B3E5
	public bool IsGame()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 1;
	}

	// Token: 0x06007A85 RID: 31365 RVA: 0x0027D1F5 File Offset: 0x0027B3F5
	public bool IsPlayer()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 2;
	}

	// Token: 0x06007A86 RID: 31366 RVA: 0x0027D205 File Offset: 0x0027B405
	public bool IsExhausted()
	{
		return this.HasTag(GAME_TAG.EXHAUSTED);
	}

	// Token: 0x06007A87 RID: 31367 RVA: 0x0027D20F File Offset: 0x0027B40F
	public bool IsAttached()
	{
		return this.HasTag(GAME_TAG.ATTACHED);
	}

	// Token: 0x06007A88 RID: 31368 RVA: 0x0027D219 File Offset: 0x0027B419
	public bool IsRecentlyArrived()
	{
		return this.HasTag(GAME_TAG.RECENTLY_ARRIVED);
	}

	// Token: 0x06007A89 RID: 31369 RVA: 0x0027D223 File Offset: 0x0027B423
	public bool IsObfuscated()
	{
		return this.HasTag(GAME_TAG.OBFUSCATED);
	}

	// Token: 0x06007A8A RID: 31370 RVA: 0x0027D230 File Offset: 0x0027B430
	public bool HasSecretDeathrattle()
	{
		return this.HasTag(GAME_TAG.SECRET_DEATHRATTLE);
	}

	// Token: 0x06007A8B RID: 31371 RVA: 0x0027D23D File Offset: 0x0027B43D
	public bool IsSecret()
	{
		return this.HasTag(GAME_TAG.SECRET);
	}

	// Token: 0x06007A8C RID: 31372 RVA: 0x0027D24A File Offset: 0x0027B44A
	public bool IsQuest()
	{
		return this.HasTag(GAME_TAG.QUEST);
	}

	// Token: 0x06007A8D RID: 31373 RVA: 0x0027D257 File Offset: 0x0027B457
	public bool IsSideQuest()
	{
		return this.HasTag(GAME_TAG.SIDEQUEST);
	}

	// Token: 0x06007A8E RID: 31374 RVA: 0x0027D264 File Offset: 0x0027B464
	public bool IsSigil()
	{
		return this.HasTag(GAME_TAG.SIGIL);
	}

	// Token: 0x06007A8F RID: 31375 RVA: 0x0027D271 File Offset: 0x0027B471
	public bool IsPuzzle()
	{
		return this.HasTag(GAME_TAG.PUZZLE);
	}

	// Token: 0x06007A90 RID: 31376 RVA: 0x0027D27E File Offset: 0x0027B47E
	public bool IsRulebook()
	{
		return this.HasTag(GAME_TAG.RULEBOOK);
	}

	// Token: 0x06007A91 RID: 31377 RVA: 0x0027D28B File Offset: 0x0027B48B
	public bool IsSecretOrQuestOrSideQuestOrSigil()
	{
		return this.IsSecret() || this.IsQuest() || this.IsSideQuest() || this.IsSigil();
	}

	// Token: 0x06007A92 RID: 31378 RVA: 0x0027D2AD File Offset: 0x0027B4AD
	public bool IsRevealed()
	{
		return this.HasTag(GAME_TAG.REVEALED);
	}

	// Token: 0x06007A93 RID: 31379 RVA: 0x0027D2BA File Offset: 0x0027B4BA
	public bool IsTwinspell()
	{
		return this.HasTag(GAME_TAG.TWINSPELL);
	}

	// Token: 0x06007A94 RID: 31380 RVA: 0x0027D2C7 File Offset: 0x0027B4C7
	public bool ReferencesSecret()
	{
		return this.HasReferencedTag(GAME_TAG.SECRET);
	}

	// Token: 0x06007A95 RID: 31381 RVA: 0x0027D2D4 File Offset: 0x0027B4D4
	public bool CanAttack()
	{
		return !this.HasTag(GAME_TAG.CANT_ATTACK) && !this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A96 RID: 31382 RVA: 0x0027D2F3 File Offset: 0x0027B4F3
	public bool CannotAttackHeroes()
	{
		return this.HasTag(GAME_TAG.CANNOT_ATTACK_HEROES);
	}

	// Token: 0x06007A97 RID: 31383 RVA: 0x0027D300 File Offset: 0x0027B500
	public bool CanBeAttacked()
	{
		return !this.HasTag(GAME_TAG.CANT_BE_ATTACKED) && !this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A98 RID: 31384 RVA: 0x0027D31F File Offset: 0x0027B51F
	public bool CanBeTargetedByOpponents()
	{
		return !this.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_OPPONENTS) && !this.HasTag(GAME_TAG.UNTOUCHABLE);
	}

	// Token: 0x06007A99 RID: 31385 RVA: 0x0027D33E File Offset: 0x0027B53E
	public bool IsMagnet()
	{
		return this.HasTag(GAME_TAG.MAGNET);
	}

	// Token: 0x06007A9A RID: 31386 RVA: 0x0027D34B File Offset: 0x0027B54B
	public int GetNumTurnsInPlay()
	{
		return this.GetTag(GAME_TAG.NUM_TURNS_IN_PLAY);
	}

	// Token: 0x06007A9B RID: 31387 RVA: 0x0027D358 File Offset: 0x0027B558
	public int GetNumAttacksThisTurn()
	{
		return this.GetTag(GAME_TAG.NUM_ATTACKS_THIS_TURN);
	}

	// Token: 0x06007A9C RID: 31388 RVA: 0x0027D368 File Offset: 0x0027B568
	public TAG_SPELL_SCHOOL GetSpellPowerSchool()
	{
		if (this.HasTag(GAME_TAG.SPELLPOWER))
		{
			return TAG_SPELL_SCHOOL.NONE;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_ARCANE))
		{
			return TAG_SPELL_SCHOOL.ARCANE;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_FIRE))
		{
			return TAG_SPELL_SCHOOL.FIRE;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_FROST))
		{
			return TAG_SPELL_SCHOOL.FROST;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_NATURE))
		{
			return TAG_SPELL_SCHOOL.NATURE;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_HOLY))
		{
			return TAG_SPELL_SCHOOL.HOLY;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_SHADOW))
		{
			return TAG_SPELL_SCHOOL.SHADOW;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_FEL))
		{
			return TAG_SPELL_SCHOOL.FEL;
		}
		if (this.HasTag(GAME_TAG.SPELLPOWER_PHYSICAL))
		{
			return TAG_SPELL_SCHOOL.PHYSICAL_COMBAT;
		}
		return TAG_SPELL_SCHOOL.NONE;
	}

	// Token: 0x06007A9D RID: 31389 RVA: 0x0027D400 File Offset: 0x0027B600
	public bool HasSpellPower()
	{
		return this.HasTag(GAME_TAG.SPELLPOWER) || this.HasTag(GAME_TAG.SPELLPOWER_ARCANE) || this.HasTag(GAME_TAG.SPELLPOWER_FIRE) || this.HasTag(GAME_TAG.SPELLPOWER_FROST) || this.HasTag(GAME_TAG.SPELLPOWER_NATURE) || this.HasTag(GAME_TAG.SPELLPOWER_HOLY) || this.HasTag(GAME_TAG.SPELLPOWER_SHADOW) || this.HasTag(GAME_TAG.SPELLPOWER_FEL) || this.HasTag(GAME_TAG.SPELLPOWER_PHYSICAL);
	}

	// Token: 0x06007A9E RID: 31390 RVA: 0x0027D482 File Offset: 0x0027B682
	public bool HasHeroPowerDamage()
	{
		return this.HasTag(GAME_TAG.HEROPOWER_DAMAGE);
	}

	// Token: 0x06007A9F RID: 31391 RVA: 0x0027D48F File Offset: 0x0027B68F
	public bool IsAffectedBySpellPower()
	{
		return this.HasTag(GAME_TAG.AFFECTED_BY_SPELL_POWER);
	}

	// Token: 0x06007AA0 RID: 31392 RVA: 0x0027D49C File Offset: 0x0027B69C
	public bool HasSpellPowerDouble()
	{
		return this.HasTag(GAME_TAG.SPELLPOWER_DOUBLE);
	}

	// Token: 0x06007AA1 RID: 31393 RVA: 0x0027D4A9 File Offset: 0x0027B6A9
	public bool ReferencesSpellPower()
	{
		return this.HasReferencedTag(GAME_TAG.SPELLPOWER);
	}

	// Token: 0x06007AA2 RID: 31394 RVA: 0x0027D4B6 File Offset: 0x0027B6B6
	public bool HasHealingDoesDamageHint()
	{
		return this.HasTag(GAME_TAG.HEALING_DOES_DAMAGE_HINT);
	}

	// Token: 0x06007AA3 RID: 31395 RVA: 0x0027D4C3 File Offset: 0x0027B6C3
	public bool HasLifestealDoesDamageHint()
	{
		return this.HasTag(GAME_TAG.LIFESTEAL_DOES_DAMAGE_HINT);
	}

	// Token: 0x06007AA4 RID: 31396 RVA: 0x0027D4D0 File Offset: 0x0027B6D0
	public int GetCost()
	{
		return this.GetTag(GAME_TAG.COST);
	}

	// Token: 0x06007AA5 RID: 31397 RVA: 0x0027D4DA File Offset: 0x0027B6DA
	public int GetATK()
	{
		return this.GetTag(GAME_TAG.ATK);
	}

	// Token: 0x06007AA6 RID: 31398 RVA: 0x0027D4E4 File Offset: 0x0027B6E4
	public int GetHealth()
	{
		return this.GetTag(GAME_TAG.HEALTH);
	}

	// Token: 0x06007AA7 RID: 31399 RVA: 0x0027D4EE File Offset: 0x0027B6EE
	public int GetDurability()
	{
		return this.GetTag(GAME_TAG.DURABILITY);
	}

	// Token: 0x06007AA8 RID: 31400 RVA: 0x0027D4FB File Offset: 0x0027B6FB
	public int GetArmor()
	{
		return this.GetTag(GAME_TAG.ARMOR);
	}

	// Token: 0x06007AA9 RID: 31401 RVA: 0x0027D508 File Offset: 0x0027B708
	public int GetAttached()
	{
		return this.GetTag(GAME_TAG.ATTACHED);
	}

	// Token: 0x06007AAA RID: 31402 RVA: 0x0027D512 File Offset: 0x0027B712
	public TAG_ZONE GetZone()
	{
		return (TAG_ZONE)this.GetTag(GAME_TAG.ZONE);
	}

	// Token: 0x06007AAB RID: 31403 RVA: 0x0027D51C File Offset: 0x0027B71C
	public int GetZonePosition()
	{
		return this.GetTag(GAME_TAG.ZONE_POSITION);
	}

	// Token: 0x06007AAC RID: 31404 RVA: 0x0027D529 File Offset: 0x0027B729
	public int GetCreatorId()
	{
		return this.GetTag(GAME_TAG.CREATOR);
	}

	// Token: 0x06007AAD RID: 31405 RVA: 0x0027D536 File Offset: 0x0027B736
	public int GetCreatorDBID()
	{
		return this.GetTag(GAME_TAG.CREATOR_DBID);
	}

	// Token: 0x06007AAE RID: 31406 RVA: 0x0027D543 File Offset: 0x0027B743
	public int GetControllerId()
	{
		return this.GetTag(GAME_TAG.CONTROLLER);
	}

	// Token: 0x06007AAF RID: 31407 RVA: 0x0027D54D File Offset: 0x0027B74D
	public int GetFatigue()
	{
		return this.GetTag(GAME_TAG.FATIGUE);
	}

	// Token: 0x06007AB0 RID: 31408 RVA: 0x0027D557 File Offset: 0x0027B757
	public int GetWindfury()
	{
		return this.GetTag(GAME_TAG.WINDFURY);
	}

	// Token: 0x06007AB1 RID: 31409 RVA: 0x0027D564 File Offset: 0x0027B764
	public bool HasWindfury()
	{
		return this.GetTag(GAME_TAG.WINDFURY) > 0;
	}

	// Token: 0x06007AB2 RID: 31410 RVA: 0x0027D574 File Offset: 0x0027B774
	public bool ReferencesWindfury()
	{
		return this.HasReferencedTag(GAME_TAG.WINDFURY);
	}

	// Token: 0x06007AB3 RID: 31411 RVA: 0x0027D581 File Offset: 0x0027B781
	public int GetExtraAttacksThisTurn()
	{
		return this.GetTag(GAME_TAG.EXTRA_ATTACKS_THIS_TURN);
	}

	// Token: 0x06007AB4 RID: 31412 RVA: 0x0027D58E File Offset: 0x0027B78E
	public bool HasCombo()
	{
		return this.HasTag(GAME_TAG.COMBO);
	}

	// Token: 0x06007AB5 RID: 31413 RVA: 0x0027D59B File Offset: 0x0027B79B
	public bool HasOverload()
	{
		return this.HasTag(GAME_TAG.OVERLOAD);
	}

	// Token: 0x06007AB6 RID: 31414 RVA: 0x0027D5A8 File Offset: 0x0027B7A8
	public bool HasDeathrattle()
	{
		return this.HasTag(GAME_TAG.DEATHRATTLE);
	}

	// Token: 0x06007AB7 RID: 31415 RVA: 0x0027D5B5 File Offset: 0x0027B7B5
	public bool ReferencesDeathrattle()
	{
		return this.HasReferencedTag(GAME_TAG.DEATHRATTLE);
	}

	// Token: 0x06007AB8 RID: 31416 RVA: 0x0027D5C2 File Offset: 0x0027B7C2
	public bool IsSilenced()
	{
		return this.HasTag(GAME_TAG.SILENCED);
	}

	// Token: 0x06007AB9 RID: 31417 RVA: 0x0027D5CF File Offset: 0x0027B7CF
	public int GetEntityId()
	{
		return this.GetTag(GAME_TAG.ENTITY_ID);
	}

	// Token: 0x06007ABA RID: 31418 RVA: 0x0027D5D9 File Offset: 0x0027B7D9
	public bool IsCharacter()
	{
		return this.IsHero() || this.IsMinion();
	}

	// Token: 0x06007ABB RID: 31419 RVA: 0x0027D5EB File Offset: 0x0027B7EB
	public bool IsItem()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 8;
	}

	// Token: 0x06007ABC RID: 31420 RVA: 0x0027D5FB File Offset: 0x0027B7FB
	public bool IsToken()
	{
		return this.GetTag(GAME_TAG.CARDTYPE) == 9;
	}

	// Token: 0x06007ABD RID: 31421 RVA: 0x0027D60C File Offset: 0x0027B80C
	public bool HasCustomKeywordEffect()
	{
		return this.HasTag(GAME_TAG.CUSTOM_KEYWORD_EFFECT);
	}

	// Token: 0x06007ABE RID: 31422 RVA: 0x0027D619 File Offset: 0x0027B819
	public int GetDisplayedCreatorId()
	{
		return this.GetTag(GAME_TAG.DISPLAYED_CREATOR);
	}

	// Token: 0x06007ABF RID: 31423 RVA: 0x0027D626 File Offset: 0x0027B826
	public bool HasRush()
	{
		return this.HasTag(GAME_TAG.RUSH);
	}

	// Token: 0x06007AC0 RID: 31424 RVA: 0x0027D633 File Offset: 0x0027B833
	public bool ReferencesRush()
	{
		return this.HasReferencedTag(GAME_TAG.RUSH);
	}

	// Token: 0x06007AC1 RID: 31425 RVA: 0x0027D640 File Offset: 0x0027B840
	public int GetTechLevel()
	{
		return this.GetTag(GAME_TAG.TECH_LEVEL);
	}

	// Token: 0x06007AC2 RID: 31426 RVA: 0x0027D650 File Offset: 0x0027B850
	public bool IsCoreCard()
	{
		TAG_CARD_SET cardSet = this.GetCardSet();
		return cardSet != TAG_CARD_SET.INVALID && GameDbf.GetIndex().GetCardSet(cardSet).IsCoreCardSet;
	}

	// Token: 0x06007AC3 RID: 31427 RVA: 0x0027D67C File Offset: 0x0027B87C
	public virtual TAG_CLASS GetClass()
	{
		IEnumerable<TAG_CLASS> classes = this.GetClasses(null);
		if (classes.Count<TAG_CLASS>() == 0)
		{
			return TAG_CLASS.INVALID;
		}
		if (1 == classes.Count<TAG_CLASS>())
		{
			return classes.First<TAG_CLASS>();
		}
		return TAG_CLASS.NEUTRAL;
	}

	// Token: 0x06007AC4 RID: 31428 RVA: 0x0027D6B0 File Offset: 0x0027B8B0
	public virtual IEnumerable<TAG_CLASS> GetClasses(Comparison<TAG_CLASS> classSorter = null)
	{
		List<TAG_CLASS> list = new List<TAG_CLASS>();
		uint num = (uint)this.GetTag(GAME_TAG.MULTIPLE_CLASSES);
		if (num == 0U)
		{
			TAG_CLASS tag = this.GetTag<TAG_CLASS>(GAME_TAG.CLASS);
			list.Add(tag);
		}
		else
		{
			int num2 = 1;
			while (num != 0U)
			{
				TAG_CLASS item;
				if (1U == (num & 1U) && EnumUtils.TryCast<TAG_CLASS>(num2, out item))
				{
					list.Add(item);
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

	// Token: 0x06007AC5 RID: 31429 RVA: 0x0027D71E File Offset: 0x0027B91E
	public bool IsMultiClass()
	{
		return this.GetClasses(null).Count<TAG_CLASS>() > 1;
	}

	// Token: 0x06007AC6 RID: 31430 RVA: 0x0027D72F File Offset: 0x0027B92F
	public bool IsAllowedInClass(TAG_CLASS classID)
	{
		return this.GetClasses(null).Contains(classID);
	}

	// Token: 0x06007AC7 RID: 31431 RVA: 0x0027D73E File Offset: 0x0027B93E
	public TAG_MULTI_CLASS_GROUP GetMultiClassGroup()
	{
		return (TAG_MULTI_CLASS_GROUP)this.GetTag(GAME_TAG.MULTI_CLASS_GROUP);
	}

	// Token: 0x06007AC8 RID: 31432 RVA: 0x0027D74B File Offset: 0x0027B94B
	public string GetCardId()
	{
		return this.m_cardId;
	}

	// Token: 0x06007AC9 RID: 31433 RVA: 0x0027D753 File Offset: 0x0027B953
	public void SetCardId(string cardId)
	{
		this.m_cardId = cardId;
		this.OnUpdateCardId();
	}

	// Token: 0x06007ACA RID: 31434 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnUpdateCardId()
	{
	}

	// Token: 0x06007ACB RID: 31435 RVA: 0x0027D764 File Offset: 0x0027B964
	public TAG_CARD_SET GetCardSet()
	{
		TAG_CARD_SET tag = (TAG_CARD_SET)this.GetTag(GAME_TAG.CARD_SET);
		if (tag != TAG_CARD_SET.INVALID)
		{
			return tag;
		}
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(this.m_cardIdInternal);
		if (cardRecord != null)
		{
			if (this.m_cardSetTimingRecords == null)
			{
				this.m_cardSetTimingRecords = GameDbf.CardSetTiming.GetRecords((CardSetTimingDbfRecord r) => r.CardId == cardRecord.ID, -1);
				if (!HearthstoneApplication.IsHearthstoneRunning)
				{
					this.m_cardSetTimingRecords.Sort((CardSetTimingDbfRecord a, CardSetTimingDbfRecord b) => -a.ID.CompareTo(b.ID));
				}
			}
			foreach (CardSetTimingDbfRecord cardSetTimingDbfRecord in this.m_cardSetTimingRecords)
			{
				if (!HearthstoneApplication.IsHearthstoneRunning || SpecialEventManager.Get().IsEventActive(cardSetTimingDbfRecord.EventTimingEvent, false))
				{
					return (TAG_CARD_SET)cardSetTimingDbfRecord.CardSetId;
				}
			}
			return TAG_CARD_SET.INVALID;
		}
		return TAG_CARD_SET.INVALID;
	}

	// Token: 0x06007ACC RID: 31436 RVA: 0x0027D868 File Offset: 0x0027BA68
	public TAG_SPELL_SCHOOL GetSpellSchool()
	{
		return this.GetTag<TAG_SPELL_SCHOOL>(GAME_TAG.SPELL_SCHOOL);
	}

	// Token: 0x17000703 RID: 1795
	// (get) Token: 0x06007ACD RID: 31437 RVA: 0x0027D875 File Offset: 0x0027BA75
	public bool IsCollectionManagerFilterManaCostByEven
	{
		get
		{
			return this.GetTag(GAME_TAG.COLLECTIONMANAGER_FILTER_MANA_EVEN) != 0;
		}
	}

	// Token: 0x17000704 RID: 1796
	// (get) Token: 0x06007ACE RID: 31438 RVA: 0x0027D885 File Offset: 0x0027BA85
	public bool IsCollectionManagerFilterManaCostByOdd
	{
		get
		{
			return this.GetTag(GAME_TAG.COLLECTIONMANAGER_FILTER_MANA_ODD) != 0;
		}
	}

	// Token: 0x04005ECF RID: 24271
	protected static int DEFAULT_TAG_MAP_SIZE = 15;

	// Token: 0x04005ED0 RID: 24272
	protected TagMap m_tags = new TagMap(EntityBase.DEFAULT_TAG_MAP_SIZE);

	// Token: 0x04005ED1 RID: 24273
	protected TagMap m_cachedTagsForDormant = new TagMap(EntityBase.DEFAULT_TAG_MAP_SIZE);

	// Token: 0x04005ED2 RID: 24274
	private List<CardSetTimingDbfRecord> m_cardSetTimingRecords;

	// Token: 0x04005ED3 RID: 24275
	private string m_cardIdInternal;
}
