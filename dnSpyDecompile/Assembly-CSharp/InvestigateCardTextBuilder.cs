using System;

// Token: 0x02000796 RID: 1942
public class InvestigateCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006C7D RID: 27773 RVA: 0x0023161C File Offset: 0x0022F81C
	public override string BuildCardTextInHand(Entity entity)
	{
		string powersText;
		this.GetPowersText(entity, out powersText);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}

	// Token: 0x06006C7E RID: 27774 RVA: 0x0023164C File Offset: 0x0022F84C
	public override string BuildCardName(Entity entity)
	{
		string arg;
		this.GetNamesText(entity, out arg);
		return string.Format("{0}", arg);
	}

	// Token: 0x06006C7F RID: 27775 RVA: 0x0019DE03 File Offset: 0x0019C003
	public override string BuildCardTextInHand(EntityDef entityDef)
	{
		return string.Empty;
	}

	// Token: 0x06006C80 RID: 27776 RVA: 0x00231670 File Offset: 0x0022F870
	public override string BuildCardTextInHistory(Entity entity)
	{
		string powersText;
		this.GetPowersText(entity, out powersText);
		CardTextHistoryData cardTextHistoryData = entity.GetCardTextHistoryData();
		if (cardTextHistoryData == null)
		{
			Log.All.Print("TreasureCardTextBuilder.BuildCardTextInHistory: entity {0} does not have a CardTextHistoryData object.", new object[]
			{
				entity.GetEntityId()
			});
			return "";
		}
		return TextUtils.TransformCardText(cardTextHistoryData.m_damageBonus, cardTextHistoryData.m_damageBonusDouble, cardTextHistoryData.m_healingDouble, powersText);
	}

	// Token: 0x06006C81 RID: 27777 RVA: 0x002316D4 File Offset: 0x0022F8D4
	private void GetPowersText(Entity entity, out string power)
	{
		string text = CardTextBuilder.GetRawCardTextInHand(entity.GetCardId());
		text = text.Replace("@", this.ConvertNumber(entity.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
		text = text.Replace("*", this.ConvertNumberTwo(entity.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
		string text2 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_1))
		{
			int tag = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_1);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				text2 = CardTextBuilder.GetRawCardTextInHand(entityDef.GetCardId());
				text2 = text2.Replace("@", this.ConvertNumber(entityDef.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
				text2 = text2.Replace("*", this.ConvertNumberTwo(entityDef.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
			}
		}
		string text3 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			int tag2 = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag2, true);
			if (entityDef2 != null)
			{
				text3 = CardTextBuilder.GetRawCardTextInHand(entityDef2.GetCardId());
				text3 = text3.Replace("@", this.ConvertNumber(entityDef2.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
				text3 = text3.Replace("*", this.ConvertNumberTwo(entityDef2.GetCardId(), entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1)));
			}
		}
		string text4 = string.Empty;
		string text5 = string.Empty;
		if (text != string.Empty && text2 != string.Empty)
		{
			text4 = ", ";
		}
		if ((text != string.Empty || text2 != string.Empty) && text3 != string.Empty)
		{
			text5 = "\n";
		}
		power = string.Format("{0}{1}{2}{3}{4}", new object[]
		{
			text,
			text4,
			text2,
			text5,
			text3
		});
	}

	// Token: 0x06006C82 RID: 27778 RVA: 0x002318B0 File Offset: 0x0022FAB0
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
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef2 != null)
			{
				text2 = entityDef2.GetName();
			}
		}
		string text3 = string.Empty;
		if (entity.HasTag(GAME_TAG.MODULAR_ENTITY_PART_2))
		{
			int tag2 = entity.GetTag(GAME_TAG.MODULAR_ENTITY_PART_2);
			EntityDef entityDef3 = DefLoader.Get().GetEntityDef(tag2, true);
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
		name = string.Format("{0}{1}{2}{3}{4}", new object[]
		{
			text,
			text4,
			text2,
			text5,
			text3
		});
	}

	// Token: 0x06006C83 RID: 27779 RVA: 0x000D5239 File Offset: 0x000D3439
	private string ConvertNumberTwo(string cardID, int size)
	{
		return "";
	}

	// Token: 0x06006C84 RID: 27780 RVA: 0x002319D0 File Offset: 0x0022FBD0
	private string ConvertNumber(string cardID, int size)
	{
		int num = size;
		uint num2 = <PrivateImplementationDetails>.ComputeStringHash(cardID);
		if (num2 <= 2569404273U)
		{
			if (num2 != 2535849035U)
			{
				if (num2 != 2552626654U)
				{
					if (num2 == 2569404273U)
					{
						if (cardID == "GIL_097c")
						{
							num = size * 2;
						}
					}
				}
				else if (cardID == "GIL_097b")
				{
					num = size * 3;
				}
			}
			else if (cardID == "GIL_097a")
			{
				num = size * 2;
			}
		}
		else if (num2 <= 2602959511U)
		{
			if (num2 != 2586181892U)
			{
				if (num2 == 2602959511U)
				{
					if (cardID == "GIL_097e")
					{
						num = size;
						if (num == 1)
						{
							return "one";
						}
						if (num == 2)
						{
							return "two";
						}
						if (num == 3)
						{
							return "three";
						}
					}
				}
			}
			else if (cardID == "GIL_097d")
			{
				num = size;
			}
		}
		else if (num2 != 2619737130U)
		{
			if (num2 == 2988594485U)
			{
				if (cardID == "GIL_098b")
				{
					num = size;
				}
			}
		}
		else if (cardID == "GIL_097f")
		{
			num = size;
		}
		return string.Concat(num);
	}

	// Token: 0x040057B7 RID: 22455
	private const string TREASURE_BARN = "GIL_097a";

	// Token: 0x040057B8 RID: 22456
	private const string TREASURE_MOORS = "GIL_097b";

	// Token: 0x040057B9 RID: 22457
	private const string TREASURE_PANTRY = "GIL_097c";

	// Token: 0x040057BA RID: 22458
	private const string TREASURE_MEADOWS = "GIL_097d";

	// Token: 0x040057BB RID: 22459
	private const string TREASURE_MOON = "GIL_097e";

	// Token: 0x040057BC RID: 22460
	private const string TREASURE_FURNACE = "GIL_097f";

	// Token: 0x040057BD RID: 22461
	private const string TREASURE_MISSILES = "GIL_098b";
}
