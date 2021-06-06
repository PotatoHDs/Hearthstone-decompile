using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F42 RID: 3906
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the position of a player's leaderboard tile.")]
	public class GetLeaderboardTilePosition : FsmStateAction
	{
		// Token: 0x0600AC8E RID: 44174 RVA: 0x0035D77E File Offset: 0x0035B97E
		public override void Reset()
		{
			this.m_PlayerId = 1;
			this.m_TilePosition = null;
		}

		// Token: 0x0600AC8F RID: 44175 RVA: 0x0035D794 File Offset: 0x0035B994
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_TilePosition == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store tile position!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			if (GameState.Get() == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - GameState is null!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			if (PlayerLeaderboardManager.Get() == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - PlayerLeaderboardManager is null!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			PlayerLeaderboardCard tileForPlayerId = PlayerLeaderboardManager.Get().GetTileForPlayerId(this.m_PlayerId.Value);
			if (tileForPlayerId == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No player card exists for player id {1}!", new object[]
				{
					this,
					this.m_PlayerId.Value
				});
				base.Finish();
				return;
			}
			PlayerLeaderboardTile playerLeaderboardTile = tileForPlayerId.m_PlayerLeaderboardTile;
			if (playerLeaderboardTile == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No player tile exists for player id {1}!", new object[]
				{
					this,
					this.m_PlayerId.Value
				});
				base.Finish();
				return;
			}
			this.m_TilePosition.Value = playerLeaderboardTile.transform.position;
			base.Finish();
		}

		// Token: 0x04009360 RID: 37728
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Which player's tile are we looking for?")]
		public FsmInt m_PlayerId;

		// Token: 0x04009361 RID: 37729
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmVector3 m_TilePosition;
	}
}
