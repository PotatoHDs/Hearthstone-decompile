using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000999 RID: 2457
[CustomEditClass(DefaultCollapsed = true)]
internal class TagVisualConfiguration : MonoBehaviour
{
	// Token: 0x0600864B RID: 34379 RVA: 0x002B5344 File Offset: 0x002B3544
	public void Awake()
	{
		TagVisualConfiguration.s_instance = this;
	}

	// Token: 0x0600864C RID: 34380 RVA: 0x002B534C File Offset: 0x002B354C
	private void OnDestroy()
	{
		TagVisualConfiguration.s_instance = null;
	}

	// Token: 0x0600864D RID: 34381 RVA: 0x002B5354 File Offset: 0x002B3554
	public static TagVisualConfiguration Get()
	{
		return TagVisualConfiguration.s_instance;
	}

	// Token: 0x0600864E RID: 34382 RVA: 0x002B535C File Offset: 0x002B355C
	public void ActivateStateSpells(Card card)
	{
		if (card == null || card.GetActor() == null || card.GetEntity() == null)
		{
			return;
		}
		if (card.GetEntity().GetController() != null && !card.GetEntity().GetController().IsFriendlySide() && card.GetEntity().IsObfuscated())
		{
			return;
		}
		foreach (TagVisualConfigurationEntry tagVisualConfigurationEntry in this.m_TagVisuals)
		{
			TagVisualConfigurationEntry tagVisualConfigurationEntry2 = tagVisualConfigurationEntry;
			if (tagVisualConfigurationEntry.m_ReferenceTag != GAME_TAG.TAG_NOT_SET)
			{
				tagVisualConfigurationEntry2 = this.FindTagEntry(tagVisualConfigurationEntry.m_ReferenceTag);
			}
			if (tagVisualConfigurationEntry2 != null && tagVisualConfigurationEntry2.m_IsPlayStateSpell)
			{
				TagDelta tagDelta = new TagDelta();
				tagDelta.tag = (int)tagVisualConfigurationEntry.m_Tag;
				tagDelta.oldValue = 0;
				tagDelta.newValue = card.GetEntity().GetTag(tagVisualConfigurationEntry.m_Tag);
				if (tagVisualConfigurationEntry2.m_BeforeAlways != null)
				{
					foreach (TagVisualActionConfiguration actionConfig in tagVisualConfigurationEntry2.m_BeforeAlways.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig, card, false, tagDelta, false, true, null);
					}
				}
				if (tagDelta.newValue > 0 && tagVisualConfigurationEntry2.m_TagAdded != null)
				{
					foreach (TagVisualActionConfiguration actionConfig2 in tagVisualConfigurationEntry2.m_TagAdded.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig2, card, false, tagDelta, false, true, null);
					}
				}
				if (tagVisualConfigurationEntry2.m_AfterAlways != null)
				{
					foreach (TagVisualActionConfiguration actionConfig3 in tagVisualConfigurationEntry2.m_AfterAlways.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig3, card, false, tagDelta, false, true, null);
					}
				}
			}
		}
	}

	// Token: 0x0600864F RID: 34383 RVA: 0x002B5598 File Offset: 0x002B3798
	public void ActivateHandStateSpells(Card card, bool forceActivate = false)
	{
		if (card == null || card.GetActor() == null || card.GetEntity() == null)
		{
			return;
		}
		if (card.GetEntity().GetController() != null && !card.GetEntity().GetController().IsFriendlySide() && card.GetEntity().IsObfuscated())
		{
			return;
		}
		if (((card.GetZone() != null) ? card.GetZone().m_ServerTag : TAG_ZONE.SETASIDE) != TAG_ZONE.HAND)
		{
			return;
		}
		foreach (TagVisualConfigurationEntry tagVisualConfigurationEntry in this.m_TagVisuals)
		{
			TagVisualConfigurationEntry tagVisualConfigurationEntry2 = tagVisualConfigurationEntry;
			if (tagVisualConfigurationEntry.m_ReferenceTag != GAME_TAG.TAG_NOT_SET)
			{
				tagVisualConfigurationEntry2 = this.FindTagEntry(tagVisualConfigurationEntry.m_ReferenceTag);
			}
			if (tagVisualConfigurationEntry2 != null && tagVisualConfigurationEntry2.m_IsHandStateSpell)
			{
				TagDelta tagDelta = new TagDelta();
				tagDelta.tag = (int)tagVisualConfigurationEntry.m_Tag;
				tagDelta.oldValue = 0;
				tagDelta.newValue = card.GetEntity().GetTag(tagVisualConfigurationEntry.m_Tag);
				if (tagVisualConfigurationEntry2.m_BeforeAlways != null)
				{
					foreach (TagVisualActionConfiguration actionConfig in tagVisualConfigurationEntry2.m_BeforeAlways.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig, card, false, tagDelta, false, forceActivate, null);
					}
				}
				if (tagDelta.newValue > 0 && tagVisualConfigurationEntry2.m_TagAdded != null)
				{
					foreach (TagVisualActionConfiguration actionConfig2 in tagVisualConfigurationEntry2.m_TagAdded.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig2, card, false, tagDelta, false, forceActivate, null);
					}
				}
				if (tagVisualConfigurationEntry2.m_AfterAlways != null)
				{
					foreach (TagVisualActionConfiguration actionConfig3 in tagVisualConfigurationEntry2.m_AfterAlways.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig3, card, false, tagDelta, false, forceActivate, null);
					}
				}
			}
		}
	}

	// Token: 0x06008650 RID: 34384 RVA: 0x002B57F4 File Offset: 0x002B39F4
	public void DeactivateHandStateSpells(Card card, Actor actor)
	{
		if (card == null || actor == null || card.GetEntity() == null)
		{
			return;
		}
		foreach (TagVisualConfigurationEntry tagVisualConfigurationEntry in this.m_TagVisuals)
		{
			TagVisualConfigurationEntry tagVisualConfigurationEntry2 = tagVisualConfigurationEntry;
			if (tagVisualConfigurationEntry.m_ReferenceTag != GAME_TAG.TAG_NOT_SET)
			{
				tagVisualConfigurationEntry2 = this.FindTagEntry(tagVisualConfigurationEntry.m_ReferenceTag);
			}
			if (tagVisualConfigurationEntry2 != null && tagVisualConfigurationEntry2.m_IsHandStateSpell)
			{
				TagDelta tagDelta = new TagDelta();
				tagDelta.tag = (int)tagVisualConfigurationEntry.m_Tag;
				tagDelta.oldValue = 0;
				tagDelta.newValue = card.GetEntity().GetTag(tagVisualConfigurationEntry.m_Tag);
				if (tagVisualConfigurationEntry2.m_BeforeAlways != null)
				{
					foreach (TagVisualActionConfiguration actionConfig in tagVisualConfigurationEntry2.m_BeforeAlways.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig, card, false, tagDelta, false, false, actor);
					}
				}
				if (tagVisualConfigurationEntry2.m_TagRemoved != null)
				{
					foreach (TagVisualActionConfiguration actionConfig2 in tagVisualConfigurationEntry2.m_TagRemoved.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig2, card, false, tagDelta, false, false, actor);
					}
				}
				if (tagVisualConfigurationEntry2.m_AfterAlways != null)
				{
					foreach (TagVisualActionConfiguration actionConfig3 in tagVisualConfigurationEntry2.m_AfterAlways.m_Actions)
					{
						this.ConditionallyExecuteAction(tagVisualConfigurationEntry2, actionConfig3, card, false, tagDelta, false, false, actor);
					}
				}
			}
		}
	}

	// Token: 0x06008651 RID: 34385 RVA: 0x002B59F4 File Offset: 0x002B3BF4
	public void ProcessTagChange(GAME_TAG tag, Card card, bool fromShowEntity, TagDelta change)
	{
		TagVisualConfigurationEntry tagVisualConfigurationEntry = this.FindTagEntry(tag);
		if (tagVisualConfigurationEntry == null || card == null)
		{
			return;
		}
		if (!card.CanShowActorVisuals() && !tagVisualConfigurationEntry.m_IgnoreCanShowActorVisuals)
		{
			return;
		}
		if (tagVisualConfigurationEntry.m_ReferenceTag != GAME_TAG.TAG_NOT_SET)
		{
			tagVisualConfigurationEntry = this.FindTagEntry(tagVisualConfigurationEntry.m_ReferenceTag);
			if (tagVisualConfigurationEntry == null)
			{
				return;
			}
		}
		if (tagVisualConfigurationEntry.m_BeforeAlways != null)
		{
			foreach (TagVisualActionConfiguration actionConfig in tagVisualConfigurationEntry.m_BeforeAlways.m_Actions)
			{
				this.ConditionallyExecuteAction(tagVisualConfigurationEntry, actionConfig, card, fromShowEntity, change, true, true, null);
			}
		}
		if (change.newValue != 0 && change.oldValue == 0 && tagVisualConfigurationEntry.m_TagAdded != null)
		{
			using (List<TagVisualActionConfiguration>.Enumerator enumerator = tagVisualConfigurationEntry.m_TagAdded.m_Actions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TagVisualActionConfiguration actionConfig2 = enumerator.Current;
					this.ConditionallyExecuteAction(tagVisualConfigurationEntry, actionConfig2, card, fromShowEntity, change, true, true, null);
				}
				goto IL_146;
			}
		}
		if (change.newValue == 0 && change.oldValue != 0 && tagVisualConfigurationEntry.m_TagRemoved != null)
		{
			foreach (TagVisualActionConfiguration actionConfig3 in tagVisualConfigurationEntry.m_TagRemoved.m_Actions)
			{
				this.ConditionallyExecuteAction(tagVisualConfigurationEntry, actionConfig3, card, fromShowEntity, change, true, true, null);
			}
		}
		IL_146:
		if (tagVisualConfigurationEntry.m_AfterAlways != null)
		{
			foreach (TagVisualActionConfiguration actionConfig4 in tagVisualConfigurationEntry.m_AfterAlways.m_Actions)
			{
				this.ConditionallyExecuteAction(tagVisualConfigurationEntry, actionConfig4, card, fromShowEntity, change, true, true, null);
			}
		}
	}

	// Token: 0x06008652 RID: 34386 RVA: 0x002B5BC8 File Offset: 0x002B3DC8
	private void ConditionallyExecuteAction(TagVisualConfigurationEntry entry, TagVisualActionConfiguration actionConfig, Card card, bool fromShowEntity, TagDelta change, bool fromTagChange = true, bool forceActivate = true, Actor overrideActor = null)
	{
		if (actionConfig == null || card == null)
		{
			return;
		}
		if (this.IsActionConditionMet(actionConfig.m_Condition, card, fromShowEntity, fromTagChange, overrideActor))
		{
			this.ExecuteAction(actionConfig, card, change, forceActivate, overrideActor);
		}
	}

	// Token: 0x06008653 RID: 34387 RVA: 0x002B5BFC File Offset: 0x002B3DFC
	private void ExecuteAction(TagVisualActionConfiguration actionConfig, Card card, TagDelta change, bool forceActivate, Actor overrideActor)
	{
		if (card == null)
		{
			return;
		}
		switch (actionConfig.m_Action)
		{
		case TagVisualActorFunction.ACTIVATE_SPELL_STATE:
			this.ActivateSpellState(actionConfig.m_SpellType, actionConfig.m_SpellState, card, forceActivate, overrideActor);
			return;
		case TagVisualActorFunction.PLAY_SOUND_PREFAB:
		{
			AssetReference assetReference = actionConfig.m_PlaySoundPrefabParameters;
			if (assetReference != null)
			{
				SoundManager.Get().LoadAndPlay(assetReference);
				return;
			}
			break;
		}
		case TagVisualActorFunction.ACTIVATE_STATE_FUNCTION:
		{
			TagVisualActorStateFunction stateFunctionParameters = actionConfig.m_StateFunctionParameters;
			this.ActivateStateFunction(stateFunctionParameters, card, true, change);
			return;
		}
		case TagVisualActorFunction.DEACTIVATE_STATE_FUNCTION:
		{
			TagVisualActorStateFunction stateFunctionParameters2 = actionConfig.m_StateFunctionParameters;
			this.ActivateStateFunction(stateFunctionParameters2, card, false, change);
			return;
		}
		case TagVisualActorFunction.UPDATE_ACTOR:
			card.UpdateActor(false, null);
			return;
		case TagVisualActorFunction.UPDATE_ACTOR_COMPONENTS:
			if (overrideActor != null)
			{
				overrideActor.UpdateAllComponents();
				return;
			}
			card.UpdateActorComponents();
			return;
		case TagVisualActorFunction.UPDATE_SIDEQUEST_UI:
			card.UpdateSideQuestUI(false);
			return;
		case TagVisualActorFunction.UPDATE_QUEST_UI:
			card.UpdateQuestUI();
			return;
		case TagVisualActorFunction.UPDATE_PUZZLE_UI:
			card.UpdatePuzzleUI();
			return;
		case TagVisualActorFunction.UPDATE_HERO_POWER_VISUALS:
			card.UpdateHeroPowerRelatedVisual();
			return;
		case TagVisualActorFunction.UPDATE_TEXT_COMPONENTS:
		{
			Actor actor = card.GetActor();
			if (overrideActor != null)
			{
				actor = overrideActor;
			}
			if (actor != null)
			{
				actor.UpdateTextComponents();
				return;
			}
			break;
		}
		case TagVisualActorFunction.UPDATE_BAUBLE:
			card.UpdateBauble();
			return;
		case TagVisualActorFunction.UPDATE_ATTACHED_CARD_BAUBLE:
			if (card.GetEntity() != null)
			{
				Entity entity = GameState.Get().GetEntity(card.GetEntity().GetAttached());
				if (entity != null && entity.GetCard() != null)
				{
					entity.GetCard().UpdateBauble();
					return;
				}
			}
			break;
		case TagVisualActorFunction.ACTIVATE_LIFETIME_EFFECTS:
			card.ActivateLifetimeEffects();
			return;
		case TagVisualActorFunction.DEACTIVATE_LIFETIME_EFFECTS:
			card.DeactivateLifetimeEffects();
			return;
		case TagVisualActorFunction.CANCEL_ACTIVE_SPELLS:
			card.CancelActiveSpells();
			return;
		case TagVisualActorFunction.ACTIVATE_CUSTOM_KEYWORD_EFFECT:
			card.ActivateCustomKeywordEffect();
			return;
		case TagVisualActorFunction.DEACTIVATE_CUSTOM_KEYWORD_EFFECT:
			card.DeactivateCustomKeywordEffect();
			return;
		case TagVisualActorFunction.ACTIVATE_STATE_SPELLS:
			card.ActivateStateSpells(false);
			return;
		case TagVisualActorFunction.SPELL_POWER_MOUSE_OVER_EVENT:
		{
			Entity entity2 = card.GetEntity();
			if (entity2 == null)
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL.NONE);
				return;
			}
			ZoneMgr.Get().OnSpellPowerEntityMousedOver(entity2.GetSpellPowerSchool());
			return;
		}
		case TagVisualActorFunction.SPELL_POWER_MOUSE_OUT_EVENT:
		{
			Entity entity3 = card.GetEntity();
			if (entity3 == null)
			{
				ZoneMgr.Get().OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL.NONE);
				return;
			}
			ZoneMgr.Get().OnSpellPowerEntityMousedOut(entity3.GetSpellPowerSchool());
			return;
		}
		case TagVisualActorFunction.HEALING_DOES_DAMAGE_MOUSE_OVER_EVENT:
			ZoneMgr.Get().OnHealingDoesDamageEntityMousedOver();
			return;
		case TagVisualActorFunction.HEALING_DOES_DAMAGE_MOUSE_OUT_EVENT:
			ZoneMgr.Get().OnHealingDoesDamageEntityMousedOut();
			return;
		case TagVisualActorFunction.LIFESTEAL_DOES_DAMAGE_MOUSE_OVER_EVENT:
			ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOver();
			return;
		case TagVisualActorFunction.LIFESTEAL_DOES_DAMAGE_MOUSE_OUT_EVENT:
			ZoneMgr.Get().OnLifestealDoesDamageEntityMousedOut();
			return;
		case TagVisualActorFunction.UPDATE_WATERMARK:
		{
			Actor actor2 = card.GetActor();
			if (overrideActor != null)
			{
				actor2 = overrideActor;
			}
			Entity entity4 = card.GetEntity();
			if (actor2 != null && entity4 != null)
			{
				actor2.SetWatermarkCardSetOverride(entity4.GetWatermarkCardSetOverride());
				actor2.UpdateMeshComponents();
			}
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06008654 RID: 34388 RVA: 0x002B5E8C File Offset: 0x002B408C
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
				return;
			}
			card.GetActor().DeactivateTaunt();
			return;
		case TagVisualActorStateFunction.DEATHRATTLE:
			card.ToggleDeathrattle(isActive);
			return;
		case TagVisualActorStateFunction.EXHAUSTED:
			card.HandleCardExhaustedTagChanged(change);
			return;
		case TagVisualActorStateFunction.ARMS_DEALING:
			if (isActive)
			{
				card.ActivateActorArmsDealingSpell();
				return;
			}
			break;
		case TagVisualActorStateFunction.CARD_COST_HEALTH:
			card.UpdateCardCostHealth(change);
			return;
		case TagVisualActorStateFunction.DORMANT:
			if (isActive)
			{
				card.ActivateDormantStateVisual();
				return;
			}
			card.DeactivateDormantStateVisual();
			return;
		case TagVisualActorStateFunction.TECH_LEVEL_MANA_GEM:
		{
			if (!isActive || !card.CanShowActorVisuals())
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.TECH_LEVEL_MANA_GEM));
				return;
			}
			Spell spell = card.GetActor().GetSpell(SpellType.TECH_LEVEL_MANA_GEM);
			if (spell != null)
			{
				spell.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("TechLevel").Value = card.GetEntity().GetTechLevel();
				spell.ActivateState(SpellStateType.BIRTH);
				return;
			}
			break;
		}
		case TagVisualActorStateFunction.COIN_ON_ENEMY_MINIONS:
		{
			if (!isActive)
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.BACON_SHOP_MINION_COIN));
				return;
			}
			Spell spell2 = card.GetActor().GetSpell(SpellType.BACON_SHOP_MINION_COIN);
			if (spell2 != null)
			{
				spell2.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("TechLevel").Value = card.GetEntity().GetTechLevel();
				spell2.ActivateState(SpellStateType.BIRTH);
				return;
			}
			break;
		}
		case TagVisualActorStateFunction.DECK_POWER_UP:
			if (isActive)
			{
				Spell spell3 = card.GetActor().GetSpell(SpellType.DECK_POWER_UP);
				if (spell3 != null && card.GetHeroCard() != null && card.GetHeroCard().gameObject != null)
				{
					spell3.SetSource(card.GetHeroCard().gameObject);
					spell3.ForceUpdateTransform();
					SpellUtils.ActivateBirthIfNecessary(spell3);
					return;
				}
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.DECK_POWER_UP));
			}
			break;
		case TagVisualActorStateFunction.COIN_MANA_GEM:
		{
			if (!isActive || !card.CanShowActorVisuals())
			{
				SpellUtils.ActivateDeathIfNecessary(card.GetActor().GetSpellIfLoaded(SpellType.COIN_MANA_GEM));
				return;
			}
			Spell spell4 = card.GetActor().GetSpell(SpellType.COIN_MANA_GEM);
			if (spell4 != null)
			{
				spell4.ActivateState(SpellStateType.BIRTH);
				return;
			}
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06008655 RID: 34389 RVA: 0x002B60C0 File Offset: 0x002B42C0
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
		case TagVisualActorCondition.DOES_SPELL_HAVE_STATE:
			flag = this.CompareSpellState(spellType, spellState, card, overrideActor);
			break;
		case TagVisualActorCondition.DOES_TAG_HAVE_VALUE:
		{
			Entity entity = card.GetEntity();
			if (tagComparisonEntity == TagVisualActorConditionEntity.CONTROLLER)
			{
				entity = card.GetController();
			}
			else if (tagComparisonEntity == TagVisualActorConditionEntity.HERO)
			{
				entity = card.GetHero();
			}
			else if (tagComparisonEntity == TagVisualActorConditionEntity.GAME)
			{
				entity = ((GameState.Get() != null) ? GameState.Get().GetGameEntity() : null);
			}
			flag = this.CompareTagValue(tagComparisonOperator, tag, tagValue, entity);
			break;
		}
		case TagVisualActorCondition.IS_ENRAGED:
			flag = (card.GetEntity() != null && card.GetEntity().IsEnraged());
			break;
		case TagVisualActorCondition.IS_ASLEEP:
			flag = (card.GetEntity() != null && card.GetEntity().IsAsleep());
			break;
		case TagVisualActorCondition.IS_FRIENDLY:
			flag = (card.GetEntity() != null && card.GetEntity().GetController() != null && card.GetEntity().GetController().IsFriendlySide());
			break;
		case TagVisualActorCondition.IS_MOUSED_OVER:
			flag = card.IsMousedOver();
			break;
		case TagVisualActorCondition.IS_ENCHANTMENT:
			flag = (card.GetEntity() != null && card.GetEntity().IsEnchantment());
			break;
		case TagVisualActorCondition.IS_DISABLED_HERO_POWER:
			flag = (card.GetEntity() != null && card.GetEntity().GetController() != null && card.GetEntity().GetController().HasTag(GAME_TAG.HERO_POWER_DISABLED));
			break;
		case TagVisualActorCondition.IS_FROM_SHOW_ENTITY:
			flag = fromShowEntity;
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
				flag = (entity2 != null && entity2.GetCard() != null && entity2.GetCard().CanShowActorVisuals());
			}
			break;
		case TagVisualActorCondition.SHOULD_USE_TECH_LEVEL_MANA_GEM:
			flag = (actor != null && actor.UseTechLevelManaGem());
			break;
		case TagVisualActorCondition.IS_REAL_TIME_DORMANT:
			flag = (card.GetEntity() != null && card.GetEntity().GetRealTimeIsDormant());
			break;
		case TagVisualActorCondition.IS_AI_CONTROLLER:
			flag = (card.GetEntity() != null && card.GetEntity().GetController() != null && card.GetEntity().GetController().IsAI());
			break;
		case TagVisualActorCondition.IS_FROM_TAG_CHANGE:
			flag = fromTagChange;
			break;
		case TagVisualActorCondition.SHOULD_USE_COIN_ON_ENEMY_MINIONS:
			flag = (actor != null && !actor.GetEntity().IsControlledByFriendlySidePlayer() && GameState.Get() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.BACON_COIN_ON_ENEMY_MINIONS));
			break;
		case TagVisualActorCondition.IS_VALID_OPTION:
			flag = (GameState.Get() != null && GameState.Get().IsValidOption(card.GetEntity()));
			break;
		}
		if (invertCondition)
		{
			flag = !flag;
		}
		return flag;
	}

	// Token: 0x06008656 RID: 34390 RVA: 0x002B63BC File Offset: 0x002B45BC
	private bool IsActionConditionMet(TagVisualActorConditionConfiguration condition, Card card, bool fromShowEntity, bool fromTagChange, Actor overrideActor)
	{
		bool flag;
		switch (condition.m_Condition)
		{
		case TagVisualActorCondition.ALWAYS:
			flag = true;
			break;
		case TagVisualActorCondition.AND:
			flag = (this.IsActionConditionMet(condition.m_Parameters.m_InvertConditionLHS, condition.m_Parameters.m_ConditionLHS, condition.m_Parameters.m_TagLHS, condition.m_Parameters.m_ComparisonOperatorLHS, condition.m_Parameters.m_ValueLHS, condition.m_Parameters.m_TagComparisonEntityLHS, condition.m_Parameters.m_SpellTypeLHS, condition.m_Parameters.m_SpellStateLHS, card, fromShowEntity, fromTagChange, overrideActor) && this.IsActionConditionMet(condition.m_Parameters.m_InvertConditionRHS, condition.m_Parameters.m_ConditionRHS, condition.m_Parameters.m_TagRHS, condition.m_Parameters.m_ComparisonOperatorRHS, condition.m_Parameters.m_ValueRHS, condition.m_Parameters.m_TagComparisonEntityRHS, condition.m_Parameters.m_SpellTypeRHS, condition.m_Parameters.m_SpellStateRHS, card, fromShowEntity, fromTagChange, overrideActor));
			if (condition.m_InvertCondition)
			{
				flag = !flag;
			}
			break;
		case TagVisualActorCondition.OR:
			flag = (this.IsActionConditionMet(condition.m_Parameters.m_InvertConditionLHS, condition.m_Parameters.m_ConditionLHS, condition.m_Parameters.m_TagLHS, condition.m_Parameters.m_ComparisonOperatorLHS, condition.m_Parameters.m_ValueLHS, condition.m_Parameters.m_TagComparisonEntityLHS, condition.m_Parameters.m_SpellTypeLHS, condition.m_Parameters.m_SpellStateLHS, card, fromShowEntity, fromTagChange, overrideActor) || this.IsActionConditionMet(condition.m_Parameters.m_InvertConditionRHS, condition.m_Parameters.m_ConditionRHS, condition.m_Parameters.m_TagRHS, condition.m_Parameters.m_ComparisonOperatorRHS, condition.m_Parameters.m_ValueRHS, condition.m_Parameters.m_TagComparisonEntityRHS, condition.m_Parameters.m_SpellTypeRHS, condition.m_Parameters.m_SpellStateRHS, card, fromShowEntity, fromTagChange, overrideActor));
			if (condition.m_InvertCondition)
			{
				flag = !flag;
			}
			break;
		default:
			flag = this.IsActionConditionMet(condition.m_InvertCondition, condition.m_Condition, condition.m_Parameters.m_Tag, condition.m_Parameters.m_ComparisonOperator, condition.m_Parameters.m_Value, condition.m_Parameters.m_TagComparisonEntity, condition.m_Parameters.m_SpellType, condition.m_Parameters.m_SpellState, card, fromShowEntity, fromTagChange, overrideActor);
			break;
		}
		return flag;
	}

	// Token: 0x06008657 RID: 34391 RVA: 0x002B660C File Offset: 0x002B480C
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
		if (actor != null)
		{
			Spell spell = (spellState == SpellStateType.BIRTH) ? actor.GetSpell(spellType) : actor.GetSpellIfLoaded(spellType);
			if (spell != null)
			{
				if (forceActivate)
				{
					spell.ActivateState(spellState);
					if (spell.GetSource() == null && card != null)
					{
						spell.SetSource(card.gameObject);
						return;
					}
				}
				else if (SpellUtils.ActivateStateIfNecessary(spell, spellState) && spell.GetSource() == null && card != null)
				{
					spell.SetSource(card.gameObject);
				}
			}
		}
	}

	// Token: 0x06008658 RID: 34392 RVA: 0x002B66BC File Offset: 0x002B48BC
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
		if (!(actor != null))
		{
			return result;
		}
		Spell spellIfLoaded = actor.GetSpellIfLoaded(spellType);
		if (spellIfLoaded != null)
		{
			return spellIfLoaded.GetActiveState() == spellState;
		}
		return spellState == SpellStateType.NONE;
	}

	// Token: 0x06008659 RID: 34393 RVA: 0x002B6714 File Offset: 0x002B4914
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
			result = (entity.GetTag(tag) == value);
			break;
		case TagVisualActorConditionComparisonOperator.GREATER_THAN:
			result = (entity.GetTag(tag) > value);
			break;
		case TagVisualActorConditionComparisonOperator.GREATER_THAN_OR_EQUAL:
			result = (entity.GetTag(tag) >= value);
			break;
		case TagVisualActorConditionComparisonOperator.LESS_THAN:
			result = (entity.GetTag(tag) < value);
			break;
		case TagVisualActorConditionComparisonOperator.LESS_THAN_OR_EQUAL:
			result = (entity.GetTag(tag) <= value);
			break;
		}
		return result;
	}

	// Token: 0x0600865A RID: 34394 RVA: 0x002B6790 File Offset: 0x002B4990
	private TagVisualConfigurationEntry FindTagEntry(GAME_TAG tag)
	{
		foreach (TagVisualConfigurationEntry tagVisualConfigurationEntry in this.m_TagVisuals)
		{
			if (tagVisualConfigurationEntry.m_Tag == tag)
			{
				return tagVisualConfigurationEntry;
			}
		}
		return null;
	}

	// Token: 0x040071EB RID: 29163
	[CustomEditField(SearchField = "m_Tag")]
	public List<TagVisualConfigurationEntry> m_TagVisuals = new List<TagVisualConfigurationEntry>();

	// Token: 0x040071EC RID: 29164
	private static TagVisualConfiguration s_instance;
}
