using Assets;

public class CardTextBuilderFactory
{
	public static CardTextBuilder Create(Assets.Card.CardTextBuilderType type)
	{
		return type switch
		{
			Assets.Card.CardTextBuilderType.JADE_GOLEM => new JadeGolemCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.JADE_GOLEM_TRIGGER => new JadeGolemTriggerCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.MODULAR_ENTITY => new ModularEntityCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.KAZAKUS_POTION_EFFECT => new KazakusPotionEffectCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.PRIMORDIAL_WAND => new PrimordialWandCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.ALTERNATE_CARD_TEXT => new AlternateCardTextCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.SCRIPT_DATA_NUM_1 => new ScriptDataNum1CardTextBuilder(), 
			Assets.Card.CardTextBuilderType.GALAKROND_COUNTER => new GalakrondCounterCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.DECORATE => new DecorateCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.GAMEPLAY_STRING => new GameplayStringTextBuilder(), 
			Assets.Card.CardTextBuilderType.ZOMBEAST => new ZombeastCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.ZOMBEAST_ENCHANTMENT => new ZombeastEnchantmentCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.HIDDEN_CHOICE => new HiddenChoiceCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.INVESTIGATE => new InvestigateCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.REFERENCE_CREATOR_ENTITY => new ReferenceCreatorEntityCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.REFERENCE_SCRIPT_DATA_NUM_1_ENTITY => new ReferenceScriptDataNum1EntityCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.REFERENCE_SCRIPT_DATA_NUM_1_NUM_2_ENTITY => new ReferenceScriptDataNum1Num2EntityCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.UNDATAKAH_ENCHANT => new UndatakahCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.PLAYER_TAG_THRESHOLD => new PlayerTagThresholdCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.DRUSTVAR_HORROR => new DrustvarHorrorTargetingTextBuilder(), 
			Assets.Card.CardTextBuilderType.SPELL_DAMAGE_ONLY => new SpellDamageOnlyCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.HIDDEN_ENTITY => new HiddenEntityCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.SCORE_VALUE_COUNT_DOWN => new ScoreValueCountDownCardTextBuilder(), 
			Assets.Card.CardTextBuilderType.SCRIPT_DATA_NUM_1_NUM_2 => new ScriptDataNum1Num2CardTextBuilder(), 
			Assets.Card.CardTextBuilderType.POWERED_UP => new PoweredUpTargetingTextBuilder(), 
			Assets.Card.CardTextBuilderType.MULTIPLE_ALT_TEXT_SCRIPT_DATA_NUMS => new MultiAltTextScriptDataNumsCardTextBuilder(), 
			_ => new CardTextBuilder(), 
		};
	}
}
