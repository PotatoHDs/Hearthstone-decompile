using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F2F RID: 3887
	[ActionCategory("Pegasus")]
	[Tooltip("Set the visibility of a player's deck.")]
	public class SetDeckVisibilityAction : FsmStateAction
	{
		// Token: 0x0600AC46 RID: 44102 RVA: 0x0035C1C0 File Offset: 0x0035A3C0
		public override void Reset()
		{
			this.m_PlayerSide = Player.Side.NEUTRAL;
			this.m_Visible = false;
			this.m_ResetOnExit = false;
			this.m_deck = null;
			this.m_initialVisibility = false;
		}

		// Token: 0x0600AC47 RID: 44103 RVA: 0x0035C1EC File Offset: 0x0035A3EC
		public override void OnEnter()
		{
			base.OnEnter();
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
			this.m_deck = player.GetDeckZone();
			if (this.m_deck == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find deck for player {1}!", new object[]
				{
					this,
					player
				});
				base.Finish();
				return;
			}
			this.m_initialVisibility = this.m_deck.GetVisibility();
			this.m_deck.SetVisibility(this.m_Visible.Value);
			base.Finish();
		}

		// Token: 0x0600AC48 RID: 44104 RVA: 0x0035C318 File Offset: 0x0035A518
		public override void OnExit()
		{
			if (!this.m_ResetOnExit)
			{
				return;
			}
			if (this.m_deck == null)
			{
				return;
			}
			this.m_deck.SetVisibility(this.m_initialVisibility);
		}

		// Token: 0x04009318 RID: 37656
		[Tooltip("Which player's deck are we changing?")]
		public Player.Side m_PlayerSide;

		// Token: 0x04009319 RID: 37657
		[Tooltip("Should the Deck be set to visible or invisible?")]
		public FsmBool m_Visible;

		// Token: 0x0400931A RID: 37658
		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool m_ResetOnExit;

		// Token: 0x0400931B RID: 37659
		protected bool m_initialVisibility;

		// Token: 0x0400931C RID: 37660
		protected ZoneDeck m_deck;
	}
}
