public class PuzzleTurnStartIndicator : TurnStartIndicator
{
	public UberText m_ProgressText;

	public UberText m_PuzzleNameText;

	public override void Show()
	{
		if (m_ProgressText == null)
		{
			Log.Gameplay.PrintError("PuzzleTurnStartIndicator.Show(): m_ProgressText on {0} is null, please assign an UberText!", this);
			return;
		}
		if (m_ProgressText == null)
		{
			Log.Gameplay.PrintError("PuzzleTurnStartIndicator.Show(): m_PuzzleNameText on {0} is null, please assign an UberText!", this);
			return;
		}
		ZoneSecret secretZone = GameState.Get().GetFriendlySidePlayer().GetSecretZone();
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		Entity puzzleEntity = secretZone.GetPuzzleEntity();
		if (puzzleEntity != null)
		{
			m_ProgressText.Text = string.Format(GameStrings.Get("BOTA_PUZZLE_PROGRESS"), puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS), puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL));
			int num = gameEntity.GetTag(GAME_TAG.PUZZLE_NAME);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(num);
			if (entityDef != null)
			{
				m_PuzzleNameText.Text = entityDef.GetName();
			}
			else
			{
				Log.Gameplay.PrintError("PuzzleTurnStartIndicator.Show(): could not find name for card ID {0}, puzzle {1}/{2}.", num, puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS), puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL));
				m_PuzzleNameText.Text = "";
			}
		}
		base.Show();
	}
}
