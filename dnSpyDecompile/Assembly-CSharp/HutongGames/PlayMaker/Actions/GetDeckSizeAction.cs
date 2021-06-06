using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F3A RID: 3898
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the size of a player's deck in passed int.")]
	public class GetDeckSizeAction : FsmStateAction
	{
		// Token: 0x0600AC73 RID: 44147 RVA: 0x0035CEE2 File Offset: 0x0035B0E2
		public override void Reset()
		{
			this.m_PlayerSide = Player.Side.NEUTRAL;
			this.m_DeckSize = null;
		}

		// Token: 0x0600AC74 RID: 44148 RVA: 0x0035CEF4 File Offset: 0x0035B0F4
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_DeckSize == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store deck size!", new object[]
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
			if (this.m_PlayerSide == Player.Side.NEUTRAL)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No deck exists for player side {1}!", new object[]
				{
					this,
					this.m_PlayerSide
				});
				base.Finish();
				return;
			}
			Player player = (this.m_PlayerSide == Player.Side.FRIENDLY) ? GameState.Get().GetFriendlySidePlayer() : GameState.Get().GetOpposingSidePlayer();
			if (player == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find player for side {1}!", new object[]
				{
					this,
					this.m_PlayerSide
				});
				base.Finish();
				return;
			}
			ZoneDeck deckZone = player.GetDeckZone();
			if (deckZone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find deck for player {1}!", new object[]
				{
					this,
					player
				});
				base.Finish();
				return;
			}
			this.m_DeckSize.Value = deckZone.GetCardCount();
			base.Finish();
		}

		// Token: 0x0400934A RID: 37706
		[Tooltip("Which player's deck are we querying the size of?")]
		public Player.Side m_PlayerSide;

		// Token: 0x0400934B RID: 37707
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmInt m_DeckSize;
	}
}
