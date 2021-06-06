using System;

// Token: 0x0200079B RID: 1947
public class ModularEntityCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C94 RID: 27796 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public ModularEntityCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006C95 RID: 27797 RVA: 0x00231D34 File Offset: 0x0022FF34
	public override string BuildCardTextInHand(Entity entity)
	{
		string text = this.BuildFormattedText(entity);
		text = TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), text);
		return text.Trim(Environment.NewLine.ToCharArray());
	}

	// Token: 0x06006C96 RID: 27798 RVA: 0x0019DE03 File Offset: 0x0019C003
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		return string.Empty;
	}

	// Token: 0x06006C97 RID: 27799 RVA: 0x00231D74 File Offset: 0x0022FF74
	public override bool ContainsBonusDamageToken(Entity entity)
	{
		return TextUtils.HasBonusDamage(this.BuildFormattedText(entity));
	}

	// Token: 0x06006C98 RID: 27800 RVA: 0x00231D82 File Offset: 0x0022FF82
	public override bool ContainsBonusHealingToken(Entity entity)
	{
		return TextUtils.HasBonusHealing(this.BuildFormattedText(entity));
	}

	// Token: 0x06006C99 RID: 27801 RVA: 0x00231D90 File Offset: 0x0022FF90
	public virtual string GetRawCardTextInHandForCardBeingBuilt(Entity ent)
	{
		return CardTextBuilder.GetRawCardTextInHand(ent.GetCardId());
	}

	// Token: 0x06006C9A RID: 27802 RVA: 0x00231DA0 File Offset: 0x0022FFA0
	public override string BuildCardTextInHistory(Entity entity)
	{
		string arg;
		string arg2;
		this.GetPowersText(entity, out arg, out arg2);
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("ModularEntityCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return "";
		}
		string text = string.Format(this.GetRawCardTextInHandForCardBeingBuilt(entity), arg, arg2);
		text = TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, text);
		return text.Trim(Environment.NewLine.ToCharArray());
	}

	// Token: 0x06006C9B RID: 27803 RVA: 0x00231E28 File Offset: 0x00230028
	protected void GetPowersText(Entity entity, out string power1, out string power2)
	{
		power1 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1))
		{
			int tag = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				power1 = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
				power1 = this.GetPowerTextSubstring(power1);
			}
		}
		power2 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			int tag2 = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag2, true);
			if (entityDef2 != null)
			{
				power2 = CardTextBuilder.GetRawCardTextInHand(entityDef2.GetCardId());
				power2 = this.GetPowerTextSubstring(power2);
			}
		}
	}

	// Token: 0x06006C9C RID: 27804 RVA: 0x00231EC4 File Offset: 0x002300C4
	private string BuildFormattedText(Entity entity)
	{
		string arg;
		string arg2;
		this.GetPowersText(entity, out arg, out arg2);
		return string.Format(this.GetRawCardTextInHandForCardBeingBuilt(entity), arg, arg2);
	}

	// Token: 0x06006C9D RID: 27805 RVA: 0x00231EEC File Offset: 0x002300EC
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
