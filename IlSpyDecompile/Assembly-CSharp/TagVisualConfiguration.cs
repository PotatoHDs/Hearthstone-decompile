using System.Collections.Generic;
using UnityEngine;

[CustomEditClass(DefaultCollapsed = true)]
internal class TagVisualConfiguration : MonoBehaviour
{
	[CustomEditField(SearchField = "m_Tag")]
	public List<TagVisualConfigurationEntry> m_TagVisuals = new List<TagVisualConfigurationEntry>();

	private static TagVisualConfiguration s_instance;

	public void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static TagVisualConfiguration Get()
	{
		return s_instance;
	}

	public void ActivateStateSpells(Card card)
	{
		if (card == null || card.GetActor() == null || card.GetEntity() == null || (card.GetEntity().GetController() != null && !card.GetEntity().GetController().IsFriendlySide() && card.GetEntity().IsObfuscated()))
		{
			return;
		}
		foreach (TagVisualConfigurationEntry tagVisual in m_TagVisuals)
		{
			TagVisualConfigurationEntry tagVisualConfigurationEntry = tagVisual;
			if (tagVisual.m_ReferenceTag != 0)
			{
				tagVisualConfigurationEntry = FindTagEntry(tagVisual.m_ReferenceTag);
			}
			if (tagVisualConfigurationEntry == null || !tagVisualConfigurationEntry.m_IsPlayStateSpell)
			{
				continue;
			}
			TagDelta tagDelta = new TagDelta();
			tagDelta.tag = (int)tagVisual.m_Tag;
			tagDelta.oldValue = 0;
			tagDelta.newValue = card.GetEntity().GetTag(tagVisual.m_Tag);
			if (tagVisualConfigurationEntry.m_BeforeAlways != null)
			{
				foreach (TagVisualActionConfiguration action in tagVisualConfigurationEntry.m_BeforeAlways.m_Actions)
				{
					ConditionallyExecuteAction(tagVisualConfigurationEntry, action, card, fromShowEntity: false, tagDelta, fromTagChange: false);
				}
			}
			if (tagDelta.newValue > 0 && tagVisualConfigurationEntry.m_TagAdded != null)
			{
				foreach (TagVisualActionConfiguration action2 in tagVisualConfigurationEntry.m_TagAdded.m_Actions)
				{
					ConditionallyExecuteAction(tagVisualConfigurationEntry, action2, card, fromShowEntity: false, tagDelta, fromTagChange: false);
				}
			}
			if (tagVisualConfigurationEntry.m_AfterAlways == null)
			{
				continue;
			}
			foreach (TagVisualActionConfiguration action3 in tagVisualConfigurationEntry.m_AfterAlways.m_Actions)
			{
				ConditionallyExecuteAction(tagVisualConfigurationEntry, action3, card, fromShowEntity: false, tagDelta, fromTagChange: false);
			}
		}
	}

	public void ActivateHandStateSpells(Card card, bool forceActivate = false)
	{
		if (card == null || card.GetActor() == null || card.GetEntity() == null || (card.GetEntity().GetController() != null && !card.GetEntity().GetController().IsFriendlySide() && card.GetEntity().IsObfuscated()) || ((card.GetZone() != null) ? card.GetZone().m_ServerTag : TAG_ZONE.SETASIDE) != TAG_ZONE.HAND)
		{
			return;
		}
		foreach (TagVisualConfigurationEntry tagVisual in m_TagVisuals)
		{
			TagVisualConfigurationEntry tagVisualConfigurationEntry = tagVisual;
			if (tagVisual.m_ReferenceTag != 0)
			{
				tagVisualConfigurationEntry = FindTagEntry(tagVisual.m_ReferenceTag);
			}
			if (tagVisualConfigurationEntry == null || !tagVisualConfigurationEntry.m_IsHandStateSpell)
			{
				continue;
			}
			TagDelta tagDelta = new TagDelta();
			tagDelta.tag = (int)tagVisual.m_Tag;
			tagDelta.oldValue = 0;
			tagDelta.newValue = card.GetEntity().GetTag(tagVisual.m_Tag);
			if (tagVisualConfigurationEntry.m_BeforeAlways != null)
			{
				foreach (TagVisualActionConfiguration action in tagVisualConfigurationEntry.m_BeforeAlways.m_Actions)
				{
					ConditionallyExecuteAction(tagVisualConfigurationEntry, action, card, fromShowEntity: false, tagDelta, fromTagChange: false, forceActivate);
				}
			}
			if (tagDelta.newValue > 0 && tagVisualConfigurationEntry.m_TagAdded != null)
			{
				foreach (TagVisualActionConfiguration action2 in tagVisualConfigurationEntry.m_TagAdded.m_Actions)
				{
					ConditionallyExecuteAction(tagVisualConfigurationEntry, action2, card, fromShowEntity: false, tagDelta, fromTagChange: false, forceActivate);
				}
			}
			if (tagVisualConfigurationEntry.m_AfterAlways == null)
			{
				continue;
			}
			foreach (TagVisualActionConfiguration action3 in tagVisualConfigurationEntry.m_AfterAlways.m_Actions)
			{
				ConditionallyExecuteAction(tagVisualConfigurationEntry, action3, card, fromShowEntity: false, tagDelta, fromTagChange: false, forceActivate);
			}
		}
	}

	public void DeactivateHandStateSpells(Card card, Actor actor)
	{
		if (card == null || actor == null || card.GetEntity() == null)
		{
			return;
		}
		foreach (TagVisualConfigurationEntry tagVisual in m_TagVisuals)
		{
			TagVisualConfigurationEntry tagVisualConfigurationEntry = tagVisual;
			if (tagVisual.m_ReferenceTag != 0)
			{
				tagVisualConfigurationEntry = FindTagEntry(tagVisual.m_ReferenceTag);
			}
			if (tagVisualConfigurationEntry == null || !tagVisualConfigurationEntry.m_IsHandStateSpell)
			{
				continue;
			}
			TagDelta tagDelta = new TagDelta();
			tagDelta.tag = (int)tagVisual.m_Tag;
			tagDelta.oldValue = 0;
			tagDelta.newValue = card.GetEntity().GetTag(tagVisual.m_Tag);
			if (tagVisualConfigurationEntry.m_BeforeAlways != null)
			{
				foreach (TagVisualActionConfiguration action in tagVisualConfigurationEntry.m_BeforeAlways.m_Actions)
				{
					ConditionallyExecuteAction(tagVisualConfigurationEntry, action, card, fromShowEntity: false, tagDelta, fromTagChange: false, forceActivate: false, actor);
				}
			}
			if (tagVisualConfigurationEntry.m_TagRemoved != null)
			{
				foreach (TagVisualActionConfiguration action2 in tagVisualConfigurationEntry.m_TagRemoved.m_Actions)
				{
					ConditionallyExecuteAction(tagVisualConfigurationEntry, action2, card, fromShowEntity: false, tagDelta, fromTagChange: false, forceActivate: false, actor);
				}
			}
			if (tagVisualConfigurationEntry.m_AfterAlways == null)
			{
				continue;
			}
			foreach (TagVisualActionConfiguration action3 in tagVisualConfigurationEntry.m_AfterAlways.m_Actions)
			{
				ConditionallyExecuteAction(tagVisualConfigurationEntry, action3, card, fromShowEntity: false, tagDelta, fromTagChange: false, forceActivate: false, actor);
			}
		}
	}

	public void ProcessTagChange(GAME_TAG tag, Card card, bool fromShowEntity, TagDelta change)
	{
		TagVisualConfigurationEntry tagVisualConfigurationEntry = FindTagEntry(tag);
		if (tagVisualConfigurationEntry == null || card == null || (!card.CanShowActorVisuals() && !tagVisualConfigurationEntry.m_IgnoreCanShowActorVisuals))
		{
			return;
		}
		if (tagVisualConfigurationEntry.m_ReferenceTag != 0)
		{
			tagVisualConfigurationEntry = FindTagEntry(tagVisualConfigurationEntry.m_ReferenceTag);
			if (tagVisualConfigurationEntry == null)
			{
				return;
			}
		}
		if (tagVisualConfigurationEntry.m_BeforeAlways != null)
		{
			foreach (TagVisualActionConfiguration action in tagVisualConfigurationEntry.m_BeforeAlways.m_Actions)
			{
				ConditionallyExecuteAction(tagVisualConfigurationEntry, action, card, fromShowEntity, change);
			}
		}
		if (change.newValue != 0 && change.oldValue == 0 && tagVisualConfigurationEntry.m_TagAdded != null)
		{
			foreach (TagVisualActionConfiguration action2 in tagVisualConfigurationEntry.m_TagAdded.m_Actions)
			{
				ConditionallyExecuteAction(tagVisualConfigurationEntry, action2, card, fromShowEntity, change);
			}
		}
		else if (change.newValue == 0 && change.oldValue != 0 && tagVisualConfigurationEntry.m_TagRemoved != null)
		{
			foreach (TagVisualActionConfiguration action3 in tagVisualConfigurationEntry.m_TagRemoved.m_Actions)
			{
				ConditionallyExecuteAction(tagVisualConfigurationEntry, action3, card, fromShowEntity, change);
			}
		}
		if (tagVisualConfigurationEntry.m_AfterAlways == null)
		{
			return;
		}
		foreach (TagVisualActionConfiguration action4 in tagVisualConfigurationEntry.m_AfterAlways.m_Actions)
		{
			ConditionallyExecuteAction(tagVisualConfigurationEntry, action4, card, fromShowEntity, change);
		}
	}

	private void ConditionallyExecuteAction(TagVisualConfigurationEntry entry, TagVisualActionConfiguration actionConfig, Card card, bool fromShowEntity, TagDelta change, bool fromTagChange = true, bool forceActivate = true, Actor overrideActor = null)
	{
		if (actionConfig != null && !(card == null) && IsActionConditionMet(actionConfig.m_Condition, card, fromShowEntity, fromTagChange, overrideActor))
		{
			ExecuteAction(actionConfig, card, change, forceActivate, overrideActor);
		}
	}

	private void ExecuteAction(TagVisualActionConfiguration actionConfig, Card card, TagDelta change, bool forceActivate, Actor overrideActor)
	{
		if (card == null)
		{
			return;
		}
		switch (actionConfig.m_Action)
		{
		case TagVisualActorFunction.ACTIVATE_SPELL_STATE:
			ActivateSpellState(actionConfig.m_SpellType, actionConfig.m_SpellState, card, forceActivate, overrideActor);
			break;
		case TagVisualActorFunction.PLAY_SOUND_PREFAB:
		{
			AssetReference assetReference = actionConfig.m_PlaySoundPrefabParameters;
			if (assetReference != null)
			{
				SoundManager.Get().LoadAndPlay(assetReference);
			}
			break;
		}
		case TagVisualActorFunction.ACTIVATE_STATE_FUNCTION:
		{
			TagVisualActorStateFunction stateFunctionParameters2 = actionConfig.m_StateFunctionParameters;
			ActivateStateFunction(stateFunctionParameters2, card, isActive: true, change);
			break;
		}
		case TagVisualActorFunction.DEACTIVATE_STATE_FUNCTION:
		{
			TagVisualActorStateFunction stateFunctionParameters = actionConfig.m_StateFunctionParameters;
			ActivateStateFunction(stateFunctionParameters, card, isActive: false, change);
			break;
		}
		case TagVisualActorFunction.UPDATE_ACTOR:
			card.UpdateActor();
			break;
		case TagVisualActorFunction.UPDATE_ACTOR_COMPONENTS:
			if (overrideActor != null)
			{
				overrideActor.UpdateAllComponents();
			}
			else
			{
				card.UpdateActorComponents();
			}
			break;
		case TagVisualActorFunction.UPDATE_SIDEQUEST_UI:
			card.UpdateSideQuestUI(allowQuestComplete: false);
			break;
		case TagVisualActorFunction.UPDATE_QUEST_UI:
			card.UpdateQuestUI();
			break;
		case TagVisualActorFunction.UPDATE_PUZZLE_UI:
			card.UpdatePuzzleUI();
			break;
		case TagVisualActorFunction.UPDATE_HERO_POWER_VISUALS:
			card.UpdateHeroPowerRelatedVisual();
			break;
		case TagVisualActorFunction.UPDATE_TEXT_COMPONENTS:
		{
			Actor actor2 = card.GetActor();
			if (overrideActor != null)
			{
				actor2 = overrideActor;
			}
			if (actor2 != null)
			{
				actor2.UpdateTextComponents();
			}
			break;
		}
		case TagVisualActorFunction.UPDATE_BAUBLE:
			card.UpdateBauble();
			break;
		case TagVisualActorFunction.UPDATE_ATTACHED_CARD_BAUBLE:
			if (card.GetEntity() != null)
			{
				Entity entity4 = GameState.Get().GetEntity(card.GetEntity().GetAttached());
				if (entity4 != null && entity4.GetCard() != null)
				{
					entity4.GetCard().UpdateBauble();
				}
			}
			break;
		case TagVisualActorFunction.ACTIVATE_LIFETIME_EFFECTS:
			card.ActivateLifetimeEffects();
			break;
		case TagVisualActorFunction.DEACTIVATE_LIFETIME_EFFECTS:
			card.DeactivateLifetimeEffects();
			break;
		case TagVisualActorFunction.CANCEL_ACTIVE_SPELLS:
			card.CancelActiveSpells();
			break;
		case TagVisualActorFunction.ACTIVATE_CUSTOM_KEYWORD_EFFECT:
			card.ActivateCustomKeywordEffect();
			break;
		case TagVisualActorFunction.DEACTIVATE_CUSTOM_KEYWORD_EFFECT:
			card.DeactivateCustomKeywordEffect();
			break;
		case TagVisualActorFunction.ACTIVATE_STATE_SPELLS:
			card.ActivateStateSpells();
			break;
		case TagVisualActorFunction.SPELL_POWER_MOUSE_OVER_EVENT:
		{
			Entity entity3 = card.GetEntity();
			if (entity3 == null)
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOver();
			}
			else
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOver(entity3.GetSpellPowerSchool());
			}
			break;
		}
		case TagVisualActorFunction.SPELL_POWER_MOUSE_OUT_EVENT:
		{
			Entity entity2 = card.GetEntity();
			if (entity2 == null)
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOut();
			}
			else
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOut(entity2.GetSpellPowerSchool());
			}
			break;
		}
		case TagVisualActorFunction.HEALING_DOES_DAMAGE_MOUSE_OVER_EVENT:
			ZoneMgr.Get().OnHealingDoesDamageEntityMousedOver();
			break;
		case TagVisualActorFunction.HEALING_DOES_DAMAGE_MOUSE_OUT_EVENT:
			ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
			break;
		case TagVisualActorFunction.LIFESTEAL_DOES_DAMAGE_MOUSE_OVER_EVENT:
			ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOver();
			break;
		case TagVisualActorFunction.LIFESTEAL_DOES_DAMAGE_MOUSE_OUT_EVENT:
			ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOut();
			break;
		case TagVisualActorFunction.UPDATE_WATERMARK:
		{
			Actor actor = card.GetActor();
			if (overrideActor != null)
			{
				actor = overrideActor;
			}
			Entity entity = card.GetEntity();
			if (actor != null && entity != null)
			{
				actor.SetWatermarkCardSetOverride(entity.GetWatermarkCardSetOverride());
				actor.UpdateMeshComponents();
			}
			break;
		}
		}
	}

	private void ActivateStateFunction(TagVisualActorStateFunction stateFunction, Card card, bool isActive, TagDelta change)
	{
		if (card == null || card.GetActor() == null)
		{
			return;
		}
		switch (stateFunction)
		{
		case TagVisualActorStateFunction.TAUNT:
			if (isActive)
			{
				card.GetActor().ActivateTaunt();
			}
			else
			{
				card.GetActor().DeactivateTaunt();
			}
			break;
		case TagVisualActorStateFunction.DEATHRATTLE:
			card.ToggleDeathrattle(isActive);
			break;
		case TagVisualActorStateFunction.EXHAUSTED:
			card.HandleCardExhaustedTagChanged(change);
			break;
		case TagVisualActorStateFunction.DORMANT:
			if (isActive)
			{
				card.ActivateDormantStateVisual();
			}
			else
			{
				card.DeactivateDormantStateVisual();
			}
			break;
		case TagVisualActorStateFunction.ARMS_DEALING:
			if (isActive)
			{
				card.ActivateActorArmsDealingSpell();
			}
			break;
		case TagVisualActorStateFunction.CARD_COST_HEALTH:
			card.UpdateCardCostHealth(change);
			break;
		case TagVisualActorStateFunction.COIN_MANA_GEM:
			if (isActive && card.CanShowActorVisuals())
			{
				Spell spell4 = card.GetActor().GetSpell(SpellType.COIN_MANA_GEM);
				if (spell4 != null)
				{
					spell4.ActivateState(SpellStateType.BIRTH);
				}
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.COIN_MANA_GEM));
			}
			break;
		case TagVisualActorStateFunction.TECH_LEVEL_MANA_GEM:
			if (isActive && card.CanShowActorVisuals())
			{
				Spell spell2 = card.GetActor().GetSpell(SpellType.TECH_LEVEL_MANA_GEM);
				if (spell2 != null)
				{
					spell2.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("TechLevel").Value = card.GetEntity().GetTechLevel();
					spell2.ActivateState(SpellStateType.BIRTH);
				}
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.TECH_LEVEL_MANA_GEM));
			}
			break;
		case TagVisualActorStateFunction.COIN_ON_ENEMY_MINIONS:
			if (isActive)
			{
				Spell spell3 = card.GetActor().GetSpell(SpellType.BACON_SHOP_MINION_COIN);
				if (spell3 != null)
				{
					spell3.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("TechLevel").Value = card.GetEntity().GetTechLevel();
					spell3.ActivateState(SpellStateType.BIRTH);
				}
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.BACON_SHOP_MINION_COIN));
			}
			break;
		case TagVisualActorStateFunction.DECK_POWER_UP:
			if (isActive)
			{
				Spell spell = card.GetActor().GetSpell(SpellType.DECK_POWER_UP);
				if (spell != null && card.GetHeroCard() != null && card.GetHeroCard().gameObject != null)
				{
					spell.SetSource(card.GetHeroCard().gameObject);
					spell.ForceUpdateTransform();
					SpellUtils.ActivateBirthIfNecessary(spell);
				}
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.DECK_POWER_UP));
			}
			break;
		}
	}

	private bool IsActionConditionMet(bool invertCondition, TagVisualActorCondition condition, GAME_TAG tag, TagVisualActorConditionComparisonOperator tagComparisonOperator, int tagValue, TagVisualActorConditionEntity tagComparisonEntity, SpellType spellType, SpellStateType spellState, Card card, bool fromShowEntity, bool fromTagChange, Actor overrideActor)
	{
		bool flag = false;
		if (card == null)
		{
			return false;
		}
		Actor actor = card.GetActor();
		if (overrideActor != null)
		{
			actor = overrideActor;
		}
		switch (condition)
		{
		case TagVisualActorCondition.ALWAYS:
			flag = true;
			break;
		case TagVisualActorCondition.DOES_TAG_HAVE_VALUE:
		{
			Entity entity = card.GetEntity();
			switch (tagComparisonEntity)
			{
			case TagVisualActorConditionEntity.CONTROLLER:
				entity = card.GetController();
				break;
			case TagVisualActorConditionEntity.HERO:
				entity = card.GetHero();
				break;
			case TagVisualActorConditionEntity.GAME:
				entity = ((GameState.Get() != null) ? GameState.Get().GetGameEntity() : null);
				break;
			}
			flag = CompareTagValue(tagComparisonOperator, tag, tagValue, entity);
			break;
		}
		case TagVisualActorCondition.DOES_SPELL_HAVE_STATE:
			flag = CompareSpellState(spellType, spellState, card, overrideActor);
			break;
		case TagVisualActorCondition.IS_ENRAGED:
			flag = card.GetEntity() != null && card.GetEntity().IsEnraged();
			break;
		case TagVisualActorCondition.IS_ASLEEP:
			flag = card.GetEntity() != null && card.GetEntity().IsAsleep();
			break;
		case TagVisualActorCondition.IS_FRIENDLY:
			flag = card.GetEntity() != null && card.GetEntity().GetController() != null && card.GetEntity().GetController().IsFriendlySide();
			break;
		case TagVisualActorCondition.IS_MOUSED_OVER:
			flag = card.IsMousedOver();
			break;
		case TagVisualActorCondition.IS_ENCHANTMENT:
			flag = card.GetEntity() != null && card.GetEntity().IsEnchantment();
			break;
		case TagVisualActorCondition.IS_DISABLED_HERO_POWER:
			flag = card.GetEntity() != null && card.GetEntity().GetController() != null && card.GetEntity().GetController().HasTag(GAME_TAG.HERO_POWER_DISABLED);
			break;
		case TagVisualActorCondition.IS_FROM_SHOW_ENTITY:
			flag = fromShowEntity;
			break;
		case TagVisualActorCondition.IS_FROM_TAG_CHANGE:
			flag = fromTagChange;
			break;
		case TagVisualActorCondition.IS_REAL_TIME_DORMANT:
			flag = card.GetEntity() != null && card.GetEntity().GetRealTimeIsDormant();
			break;
		case TagVisualActorCondition.IS_AI_CONTROLLER:
			flag = card.GetEntity() != null && card.GetEntity().GetController() != null && card.GetEntity().GetController().IsAI();
			break;
		case TagVisualActorCondition.IS_VALID_OPTION:
			flag = GameState.Get() != null && GameState.Get().IsValidOption(card.GetEntity());
			break;
		case TagVisualActorCondition.SHOULD_SHOW_IMMUNE_VISUALS:
			flag = card.ShouldShowImmuneVisuals();
			break;
		case TagVisualActorCondition.CAN_SHOW_ACTOR_VISUALS:
			flag = card.CanShowActorVisuals();
			break;
		case TagVisualActorCondition.ATTACHED_CARD_CAN_SHOW_ACTOR_VISUALS:
			if (card.GetEntity() != null)
			{
				Entity entity2 = GameState.Get().GetEntity(card.GetEntity().GetAttached());
				flag = entity2 != null && entity2.GetCard() != null && entity2.GetCard().CanShowActorVisuals();
			}
			break;
		case TagVisualActorCondition.SHOULD_USE_TECH_LEVEL_MANA_GEM:
			flag = actor != null && actor.UseTechLevelManaGem();
			break;
		case TagVisualActorCondition.SHOULD_USE_COIN_ON_ENEMY_MINIONS:
			flag = actor != null && !actor.GetEntity().IsControlledByFriendlySidePlayer() && GameState.Get() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.BACON_COIN_ON_ENEMY_MINIONS);
			break;
		}
		if (invertCondition)
		{
			flag = !flag;
		}
		return flag;
	}

	private bool IsActionConditionMet(TagVisualActorConditionConfiguration condition, Card card, bool fromShowEntity, bool fromTagChange, Actor overrideActor)
	{
		bool flag = false;
		switch (condition.m_Condition)
		{
		case TagVisualActorCondition.ALWAYS:
			flag = true;
			break;
		case TagVisualActorCondition.AND:
			flag = IsActionConditionMet(condition.m_Parameters.m_InvertConditionLHS, condition.m_Parameters.m_ConditionLHS, condition.m_Parameters.m_TagLHS, condition.m_Parameters.m_ComparisonOperatorLHS, condition.m_Parameters.m_ValueLHS, condition.m_Parameters.m_TagComparisonEntityLHS, condition.m_Parameters.m_SpellTypeLHS, condition.m_Parameters.m_SpellStateLHS, card, fromShowEntity, fromTagChange, overrideActor) && IsActionConditionMet(condition.m_Parameters.m_InvertConditionRHS, condition.m_Parameters.m_ConditionRHS, condition.m_Parameters.m_TagRHS, condition.m_Parameters.m_ComparisonOperatorRHS, condition.m_Parameters.m_ValueRHS, condition.m_Parameters.m_TagComparisonEntityRHS, condition.m_Parameters.m_SpellTypeRHS, condition.m_Parameters.m_SpellStateRHS, card, fromShowEntity, fromTagChange, overrideActor);
			if (condition.m_InvertCondition)
			{
				flag = !flag;
			}
			break;
		case TagVisualActorCondition.OR:
			flag = IsActionConditionMet(condition.m_Parameters.m_InvertConditionLHS, condition.m_Parameters.m_ConditionLHS, condition.m_Parameters.m_TagLHS, condition.m_Parameters.m_ComparisonOperatorLHS, condition.m_Parameters.m_ValueLHS, condition.m_Parameters.m_TagComparisonEntityLHS, condition.m_Parameters.m_SpellTypeLHS, condition.m_Parameters.m_SpellStateLHS, card, fromShowEntity, fromTagChange, overrideActor) || IsActionConditionMet(condition.m_Parameters.m_InvertConditionRHS, condition.m_Parameters.m_ConditionRHS, condition.m_Parameters.m_TagRHS, condition.m_Parameters.m_ComparisonOperatorRHS, condition.m_Parameters.m_ValueRHS, condition.m_Parameters.m_TagComparisonEntityRHS, condition.m_Parameters.m_SpellTypeRHS, condition.m_Parameters.m_SpellStateRHS, card, fromShowEntity, fromTagChange, overrideActor);
			if (condition.m_InvertCondition)
			{
				flag = !flag;
			}
			break;
		default:
			flag = IsActionConditionMet(condition.m_InvertCondition, condition.m_Condition, condition.m_Parameters.m_Tag, condition.m_Parameters.m_ComparisonOperator, condition.m_Parameters.m_Value, condition.m_Parameters.m_TagComparisonEntity, condition.m_Parameters.m_SpellType, condition.m_Parameters.m_SpellState, card, fromShowEntity, fromTagChange, overrideActor);
			break;
		}
		return flag;
	}

	private void ActivateSpellState(SpellType spellType, SpellStateType spellState, Card card, bool forceActivate, Actor overrideActor)
	{
		if (card == null)
		{
			return;
		}
		Actor actor = card.GetActor();
		if (overrideActor != null)
		{
			actor = overrideActor;
		}
		if (!(actor != null))
		{
			return;
		}
		Spell spell = ((spellState == SpellStateType.BIRTH) ? actor.GetSpell(spellType) : actor.GetSpellIfLoaded(spellType));
		if (!(spell != null))
		{
			return;
		}
		if (forceActivate)
		{
			spell.ActivateState(spellState);
			if (spell.GetSource() == null && card != null)
			{
				spell.SetSource(card.gameObject);
			}
		}
		else if (SpellUtils.ActivateStateIfNecessary(spell, spellState) && spell.GetSource() == null && card != null)
		{
			spell.SetSource(card.gameObject);
		}
	}

	private bool CompareSpellState(SpellType spellType, SpellStateType spellState, Card card, Actor overrideActor)
	{
		bool result = false;
		if (card == null)
		{
			return false;
		}
		Actor actor = card.GetActor();
		if (overrideActor != null)
		{
			actor = overrideActor;
		}
		if (actor != null)
		{
			Spell spellIfLoaded = actor.GetSpellIfLoaded(spellType);
			if (spellIfLoaded != null)
			{
				return spellIfLoaded.GetActiveState() == spellState;
			}
			return spellState == SpellStateType.NONE;
		}
		return result;
	}

	private bool CompareTagValue(TagVisualActorConditionComparisonOperator op, GAME_TAG tag, int value, Entity entity)
	{
		bool result = false;
		if (entity == null)
		{
			return false;
		}
		switch (op)
		{
		case TagVisualActorConditionComparisonOperator.EQUAL:
			result = entity.GetTag(tag) == value;
			break;
		case TagVisualActorConditionComparisonOperator.GREATER_THAN:
			result = entity.GetTag(tag) > value;
			break;
		case TagVisualActorConditionComparisonOperator.GREATER_THAN_OR_EQUAL:
			result = entity.GetTag(tag) >= value;
			break;
		case TagVisualActorConditionComparisonOperator.LESS_THAN:
			result = entity.GetTag(tag) < value;
			break;
		case TagVisualActorConditionComparisonOperator.LESS_THAN_OR_EQUAL:
			result = entity.GetTag(tag) <= value;
			break;
		}
		return result;
	}

	private TagVisualConfigurationEntry FindTagEntry(GAME_TAG tag)
	{
		foreach (TagVisualConfigurationEntry tagVisual in m_TagVisuals)
		{
			if (tagVisual.m_Tag == tag)
			{
				return tagVisual;
			}
		}
		return null;
	}
}
