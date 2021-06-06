using System;

// Token: 0x020007A7 RID: 1959
public class UndatakahCardTextBuilder : CardTextBuilder
{
	// Token: 0x06006CD0 RID: 27856 RVA: 0x002309C3 File Offset: 0x0022EBC3
	public UndatakahCardTextBuilder()
	{
		this.m_useEntityForTextInPlay = true;
	}

	// Token: 0x06006CD1 RID: 27857 RVA: 0x00232A58 File Offset: 0x00230C58
	public override string BuildCardTextInHand(Entity entity)
	{
		string key = "";
		if (entity.HasTag(GAME_TAG.CUSTOMTEXT3))
		{
			key = "GAMEPLAY_UNDATAKAH3";
		}
		else if (entity.HasTag(GAME_TAG.CUSTOMTEXT2))
		{
			key = "GAMEPLAY_UNDATAKAH2";
		}
		else if (entity.HasTag(GAME_TAG.CUSTOMTEXT1))
		{
			key = "GAMEPLAY_UNDATAKAH1";
		}
		Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.CUSTOMTEXT1));
		Entity entity3 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.CUSTOMTEXT2));
		Entity entity4 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.CUSTOMTEXT3));
		string arg = (entity2 != null && entity2.HasValidDisplayName()) ? entity2.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string arg2 = (entity3 != null && entity3.HasValidDisplayName()) ? entity3.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string arg3 = (entity4 != null && entity4.HasValidDisplayName()) ? entity4.GetName() : GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		string powersText = string.Format(GameStrings.Get(key), arg, arg2, arg3);
		return TextUtils.TransformCardText(entity.GetDamageBonus(), entity.GetDamageBonusDouble(), entity.GetHealingDouble(), powersText);
	}
}
