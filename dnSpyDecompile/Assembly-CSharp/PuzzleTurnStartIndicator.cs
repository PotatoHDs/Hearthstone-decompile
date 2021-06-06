using System;

// Token: 0x02000B29 RID: 2857
public class PuzzleTurnStartIndicator : TurnStartIndicator
{
	// Token: 0x0600978E RID: 38798 RVA: 0x003101B0 File Offset: 0x0030E3B0
	public override void Show()
	{
		if (this.m_ProgressText == null)
		{
			Log.Gameplay.PrintError("PuzzleTurnStartIndicator.Show(): m_ProgressText on {0} is null, please assign an UberText!", new object[]
			{
				this
			});
			return;
		}
		if (this.m_ProgressText == null)
		{
			Log.Gameplay.PrintError("PuzzleTurnStartIndicator.Show(): m_PuzzleNameText on {0} is null, please assign an UberText!", new object[]
			{
				this
			});
			return;
		}
		ZoneSecret secretZone = GameState.Get().GetFriendlySidePlayer().GetSecretZone();
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		Entity puzzleEntity = secretZone.GetPuzzleEntity();
		if (puzzleEntity != null)
		{
			this.m_ProgressText.Text = string.Format(GameStrings.Get("BOTA_PUZZLE_PROGRESS"), puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS), puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL));
			int tag = gameEntity.GetTag(GAME_TAG.PUZZLE_NAME);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				this.m_PuzzleNameText.Text = entityDef.GetName();
			}
			else
			{
				Log.Gameplay.PrintError("PuzzleTurnStartIndicator.Show(): could not find name for card ID {0}, puzzle {1}/{2}.", new object[]
				{
					tag,
					puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS),
					puzzleEntity.GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL)
				});
				this.m_PuzzleNameText.Text = "";
			}
		}
		base.Show();
	}

	// Token: 0x04007EEA RID: 32490
	public UberText m_ProgressText;

	// Token: 0x04007EEB RID: 32491
	public UberText m_PuzzleNameText;
}
