public class InvestigateCardTextBuilder : CardTextBuilder
{
	private const string TREASURE_BARN = "GIL_097a";

	private const string TREASURE_MOORS = "GIL_097b";

	private const string TREASURE_PANTRY = "GIL_097c";

	private const string TREASURE_MEADOWS = "GIL_097d";

	private const string TREASURE_MOON = "GIL_097e";

	private const string TREASURE_FURNACE = "GIL_097f";

	private const string TREASURE_MISSILES = "GIL_098b";

	public override string BuildCardTextInHand(Entity entity)
	{
		GetPowersText(entity, out var power);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), power);
	}

	public override string BuildCardName(Entity entity)
	{
		GetNamesText(entity, out var name);
		return $"{name}";
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		return string.Empty;
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		GetPowersText(entity, out var power);
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("TreasureCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", entity.GetEntityId());
			return "";
		}
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, power);
	}

	private void GetPowersText(Entity entity, out string power)
	{
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		rawCardTextInHand = rawCardTextInHand.Replace("@", ConvertNumber(entity.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
		rawCardTextInHand = rawCardTextInHand.Replace("*", ConvertNumberTwo(entity.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
		string text = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1))
		{
			int tag = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag);
			if (entityDef != null)
			{
				text = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
				text = text.Replace("@", ConvertNumber(entityDef.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
				text = text.Replace("*", ConvertNumberTwo(entityDef.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
			}
		}
		string text2 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			int tag2 = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag2);
			if (entityDef2 != null)
			{
				text2 = CardTextBuilder.GetRawCardTextInHand(entityDef2.GetCardId());
				text2 = text2.Replace("@", ConvertNumber(entityDef2.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
				text2 = text2.Replace("*", ConvertNumberTwo(entityDef2.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
			}
		}
		string text3 = string.Empty;
		string text4 = string.Empty;
		if (rawCardTextInHand != string.Empty && text != string.Empty)
		{
			text3 = ", ";
		}
		if ((rawCardTextInHand != string.Empty || text != string.Empty) && text2 != string.Empty)
		{
			text4 = "\n";
		}
		power = $"{rawCardTextInHand}{text3}{text}{text4}{text2}";
	}

	private void GetNamesText(Entity entity, out string name)
	{
		string text = string.Empty;
		EntityDef entityDef = entity.GetEntityDef();
		if (entityDef != null)
		{
			text = entityDef.GetName();
		}
		string text2 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1))
		{
			int tag = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag);
			if (entityDef2 != null)
			{
				text2 = entityDef2.GetName();
			}
		}
		string text3 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			int tag2 = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2);
			EntityDef entityDef3 = DefLoader.Get().GetEntityDef(tag2);
			if (entityDef3 != null)
			{
				text3 = entityDef3.GetName();
			}
		}
		string text4 = string.Empty;
		string text5 = string.Empty;
		if (text != string.Empty && text2 != string.Empty)
		{
			text4 = " ";
		}
		if ((text != string.Empty || text2 != string.Empty) && text3 != string.Empty)
		{
			text5 = " ";
		}
		name = $"{text}{text4}{text2}{text5}{text3}";
	}

	private string ConvertNumberTwo(string cardID, int size)
	{
		return "";
	}

	private string ConvertNumber(string cardID, int size)
	{
		int num = size;
		switch (cardID)
		{
		case "GIL_097a":
			num = size * 2;
			break;
		case "GIL_097b":
			num = size * 3;
			break;
		case "GIL_097c":
			num = size * 2;
			break;
		case "GIL_097d":
			num = size;
			break;
		case "GIL_097e":
			num = size;
			switch (num)
			{
			case 1:
				return "one";
			case 2:
				return "two";
			case 3:
				return "three";
			}
			break;
		case "GIL_097f":
			num = size;
			break;
		case "GIL_098b":
			num = size;
			break;
		}
		return string.Concat(num);
	}
}
