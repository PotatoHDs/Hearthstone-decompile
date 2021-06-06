public class TB_RandomDeckKeepWinnerDeck : MissionEntity
{
	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	public override string GetNameBannerOverride(Player.Side playerSide)
	{
		Player playerBySide = GameState.Get().GetPlayerBySide(playerSide);
		int tag = playerBySide.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		int tag2 = playerBySide.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		int tag3 = playerBySide.GetTag(GAME_TAG.TAG_TB_RANDOM_DECK_TIME_ID);
		EntityDef entityDef = DefLoader.Get().GetEntityDef(tag);
		if (entityDef != null)
		{
			string name = entityDef.GetName();
			string text = string.Empty;
			if (tag2 != 0)
			{
				entityDef = DefLoader.Get().GetEntityDef(tag2);
				if (entityDef != null)
				{
					text = " + " + entityDef.GetName();
				}
			}
			string text2 = ((tag3 <= 0) ? string.Empty : (" " + tag3));
			return name + text + text2;
		}
		return null;
	}
}
