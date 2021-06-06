using System;

// Token: 0x020005BD RID: 1469
public class TB_RandomDeckKeepWinnerDeck : MissionEntity
{
	// Token: 0x060050FC RID: 20732 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x060050FD RID: 20733 RVA: 0x001A9660 File Offset: 0x001A7860
	public override string GetNameBannerOverride(Player.Side playerSide)
	{
		Player playerBySide = GameState.Get().GetPlayerBySide(playerSide);
		int tag = playerBySide.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		int tag2 = playerBySide.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		int tag3 = playerBySide.GetTag(GAME_TAG.TAG_TB_RANDOM_DECK_TIME_ID);
		EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
		if (entityDef != null)
		{
			string name = entityDef.GetName();
			string str = string.Empty;
			if (tag2 != 0)
			{
				entityDef = DefLoader.Get().GetEntityDef(tag2, true);
				if (entityDef != null)
				{
					str = " + " + entityDef.GetName();
				}
			}
			string str2 = (tag3 <= 0) ? string.Empty : (" " + tag3.ToString());
			return name + str + str2;
		}
		return null;
	}
}
