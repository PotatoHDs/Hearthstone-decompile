using System;
using Assets;

// Token: 0x0200078D RID: 1933
public class CardTextBuilderFactory
{
	// Token: 0x06006C5C RID: 27740 RVA: 0x00230D4C File Offset: 0x0022EF4C
	public static CardTextBuilder Create(Assets.Card.CardTextBuilderType type)
	{
		switch (type)
		{
		case Assets.Card.CardTextBuilderType.JADE_GOLEM:
			return new JadeGolemCardTextBuilder();
		case Assets.Card.CardTextBuilderType.JADE_GOLEM_TRIGGER:
			return new JadeGolemTriggerCardTextBuilder();
		case Assets.Card.CardTextBuilderType.MODULAR_ENTITY:
			return new ModularEntityCardTextBuilder();
		case Assets.Card.CardTextBuilderType.KAZAKUS_POTION_EFFECT:
			return new KazakusPotionEffectCardTextBuilder();
		case Assets.Card.CardTextBuilderType.PRIMORDIAL_WAND:
			return new PrimordialWandCardTextBuilder();
		case Assets.Card.CardTextBuilderType.ALTERNATE_CARD_TEXT:
			return new AlternateCardTextCardTextBuilder();
		case Assets.Card.CardTextBuilderType.SCRIPT_DATA_NUM_1:
			return new ScriptDataNum1CardTextBuilder();
		case Assets.Card.CardTextBuilderType.GALAKROND_COUNTER:
			return new GalakrondCounterCardTextBuilder();
		case Assets.Card.CardTextBuilderType.DECORATE:
			return new DecorateCardTextBuilder();
		case Assets.Card.CardTextBuilderType.PLAYER_TAG_THRESHOLD:
			return new PlayerTagThresholdCardTextBuilder();
		case Assets.Card.CardTextBuilderType.GAMEPLAY_STRING:
			return new GameplayStringTextBuilder();
		case Assets.Card.CardTextBuilderType.ZOMBEAST:
			return new ZombeastCardTextBuilder();
		case Assets.Card.CardTextBuilderType.ZOMBEAST_ENCHANTMENT:
			return new ZombeastEnchantmentCardTextBuilder();
		case Assets.Card.CardTextBuilderType.HIDDEN_CHOICE:
			return new HiddenChoiceCardTextBuilder();
		case Assets.Card.CardTextBuilderType.INVESTIGATE:
			return new InvestigateCardTextBuilder();
		case Assets.Card.CardTextBuilderType.REFERENCE_CREATOR_ENTITY:
			return new ReferenceCreatorEntityCardTextBuilder();
		case Assets.Card.CardTextBuilderType.REFERENCE_SCRIPT_DATA_NUM_1_ENTITY:
			return new ReferenceScriptDataNum1EntityCardTextBuilder();
		case Assets.Card.CardTextBuilderType.REFERENCE_SCRIPT_DATA_NUM_1_NUM_2_ENTITY:
			return new ReferenceScriptDataNum1Num2EntityCardTextBuilder();
		case Assets.Card.CardTextBuilderType.UNDATAKAH_ENCHANT:
			return new UndatakahCardTextBuilder();
		case Assets.Card.CardTextBuilderType.SPELL_DAMAGE_ONLY:
			return new SpellDamageOnlyCardTextBuilder();
		case Assets.Card.CardTextBuilderType.DRUSTVAR_HORROR:
			return new DrustvarHorrorTargetingTextBuilder();
		case Assets.Card.CardTextBuilderType.HIDDEN_ENTITY:
			return new HiddenEntityCardTextBuilder();
		case Assets.Card.CardTextBuilderType.SCORE_VALUE_COUNT_DOWN:
			return new ScoreValueCountDownCardTextBuilder();
		case Assets.Card.CardTextBuilderType.SCRIPT_DATA_NUM_1_NUM_2:
			return new ScriptDataNum1Num2CardTextBuilder();
		case Assets.Card.CardTextBuilderType.POWERED_UP:
			return new PoweredUpTargetingTextBuilder();
		case Assets.Card.CardTextBuilderType.MULTIPLE_ALT_TEXT_SCRIPT_DATA_NUMS:
			return new MultiAltTextScriptDataNumsCardTextBuilder();
		}
		return new CardTextBuilder();
	}
}
