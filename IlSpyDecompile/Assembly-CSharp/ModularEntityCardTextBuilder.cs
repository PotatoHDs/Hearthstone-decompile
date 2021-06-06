using System;

public class ModularEntityCardTextBuilder : CardTextBuilder
{
	public ModularEntityCardTextBuilder()
	{
		m_useEntityForTextInPlay = true;
	}

	public override string BuildCardTextInHand(Entity entity)
	{
		string powersText = BuildFormattedText(entity);
		powersText = TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
		return powersText.Trim(Environment.NewLine.ToCharArray());
	}

	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		return string.Empty;
	}

	public override bool ContainsBonusDamageToken(Entity entity)
	{
		return TextUtils.HasBonusDamage(BuildFormattedText(entity));
	}

	public override bool ContainsBonusHealingToken(Entity entity)
	{
		return TextUtils.HasBonusHealing(BuildFormattedText(entity));
	}

	public virtual string GetRawCardTextInHandForCardBeingBuilt(Entity ent)
	{
		return CardTextBuilder.GetRawCardTextInHand(ent.GetCardId());
	}

	public override string BuildCardTextInHistory(Entity entity)
	{
		GetPowersText(entity, out var power, out var power2);
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("ModularEntityCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", entity.GetEntityId());
			return "";
		}
		string powersText = string.Format(GetRawCardTextInHandForCardBeingBuilt(entity), power, power2);
		powersText = TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, powersText);
		return powersText.Trim(Environment.NewLine.ToCharArray());
	}

	protected void GetPowersText(Entity entity, out string power1, out string power2)
	{
		power1 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1))
		{
			int tag = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag);
			if (entityDef != null)
			{
				power1 = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
				power1 = GetPowerTextSubstring(power1);
			}
		}
		power2 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			int tag2 = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag2);
			if (entityDef2 != null)
			{
				power2 = CardTextBuilder.GetRawCardTextInHand(entityDef2.GetCardId());
				power2 = GetPowerTextSubstring(power2);
			}
		}
	}

	private string BuildFormattedText(Entity entity)
	{
		GetPowersText(entity, out var power, out var power2);
		return string.Format(GetRawCardTextInHandForCardBeingBuilt(entity), power, power2);
	}

	private string GetPowerTextSubstring(string powerText)
	{
		int num = powerText.IndexOf('@');
		if (num >= 0)
		{
			return powerText.Substring(num + 1);
		}
		return powerText;
	}
}
