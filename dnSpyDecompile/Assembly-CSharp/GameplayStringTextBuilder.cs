using System;

// Token: 0x02000793 RID: 1939
public class GameplayStringTextBuilder : CardTextBuilder
{
	// Token: 0x06006C70 RID: 27760 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public GameplayStringTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006C71 RID: 27761 RVA: 0x00231248 File Offset: 0x0022F448
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = base.BuildCardTextInHand(entity);
		int num = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) + 1;
		int tag = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		if (num > tag)
		{
			num = tag;
		}
		int num2 = text.IndexOf("@");
		if (num2 != -1)
		{
			string newValue = GameStrings.Get(this.GetGameStringsKey(entity.GetCardId()) + num.ToString());
			text = text.Substring(0, num2 + 1);
			text = text.Replace("@", newValue);
		}
		return text;
	}

	// Token: 0x06006C72 RID: 27762 RVA: 0x002312BE File Offset: 0x0022F4BE
	public override void OnTagChange(Card card, TagDelta tagChange)
	{
		if ((tagChange.tag == 2 || tagChange.tag == 3) && card != null && card.GetActor() != null)
		{
			card.GetActor().UpdateTextComponents();
		}
	}

	// Token: 0x06006C73 RID: 27763 RVA: 0x002312F4 File Offset: 0x0022F4F4
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		string text = base.BuildCardTextInHand(entityDef);
		int num = text.IndexOf("@");
		if (num != -1)
		{
			string newValue = GameStrings.Get(this.GetGameStringsKey(entityDef.GetCardId()) + "1");
			text = text.Substring(0, num + 1);
			text = text.Replace("@", newValue);
		}
		return text;
	}

	// Token: 0x06006C74 RID: 27764 RVA: 0x00231350 File Offset: 0x0022F550
	public override string BuildCardTextInHistory(Entity entity)
	{
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("DiamondSpellstoneTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return "";
		}
		string text = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		int num = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) + 1;
		int tag = entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		if (num > tag)
		{
			num = tag;
		}
		int num2 = text.IndexOf("@");
		if (num2 != -1)
		{
			string newValue = GameStrings.Get(this.GetGameStringsKey(entity.GetCardId()) + num.ToString());
			text = text.Substring(0, num2 + 1);
			text = text.Replace("@", newValue);
		}
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, text);
	}

	// Token: 0x06006C75 RID: 27765 RVA: 0x00231418 File Offset: 0x0022F618
	private string GetGameStringsKey(string cardID)
	{
		if (cardID.Contains("LOOT_507"))
		{
			return "GAMEPLAY_DIAMOND_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_091"))
		{
			return "GAMEPLAY_PEARL_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_064"))
		{
			return "GAMEPLAY_SAPPHIRE_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_051"))
		{
			return "GAMEPLAY_JASPER_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_043"))
		{
			return "GAMEPLAY_AMETHYST_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_103"))
		{
			return "GAMEPLAY_RUBY_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_503"))
		{
			return "GAMEPLAY_ONYX_SPELLSTONE_";
		}
		if (cardID.Contains("LOOT_526d"))
		{
			return "GAMEPLAY_LOOT_526d_DARKNESS_";
		}
		if (cardID.Contains("TOT_109t"))
		{
			return "GAMEPLAY_TOT_109t_STASIS_DRAGON_";
		}
		if (cardID.Contains("TRLA_1"))
		{
			return "GAMEPLAY_TRLA_TROLL_SHRINE_";
		}
		return "";
	}

	// Token: 0x040057AD RID: 22445
	private const string SPELLSTONE_DIAMOND = "LOOT_507";

	// Token: 0x040057AE RID: 22446
	private const string SPELLSTONE_PEARL = "LOOT_091";

	// Token: 0x040057AF RID: 22447
	private const string SPELLSTONE_SAPPHIRE = "LOOT_064";

	// Token: 0x040057B0 RID: 22448
	private const string SPELLSTONE_AMETHYST = "LOOT_043";

	// Token: 0x040057B1 RID: 22449
	private const string SPELLSTONE_JASPER = "LOOT_051";

	// Token: 0x040057B2 RID: 22450
	private const string SPELLSTONE_RUBY = "LOOT_103";

	// Token: 0x040057B3 RID: 22451
	private const string SPELLSTONE_ONYX = "LOOT_503";

	// Token: 0x040057B4 RID: 22452
	private const string DARKNESS_TOKEN = "LOOT_526d";

	// Token: 0x040057B5 RID: 22453
	private const string STASIS_DRAGON_TOKEN = "TOT_109t";

	// Token: 0x040057B6 RID: 22454
	private const string TROLL_SHRINE_TOKEN = "TRLA_1";
}
