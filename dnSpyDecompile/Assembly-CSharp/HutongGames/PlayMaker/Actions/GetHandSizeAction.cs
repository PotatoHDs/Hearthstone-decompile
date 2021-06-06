using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F3F RID: 3903
	[ActionCategory("Pegasus")]
	[Tooltip("Stores the size of a player's hand in passed int.")]
	public class GetHandSizeAction : FsmStateAction
	{
		// Token: 0x0600AC85 RID: 44165 RVA: 0x0035D3FC File Offset: 0x0035B5FC
		public override void Reset()
		{
			this.m_PlayerSide = Player.Side.NEUTRAL;
			this.m_HandSize = null;
		}

		// Token: 0x0600AC86 RID: 44166 RVA: 0x0035D40C File Offset: 0x0035B60C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_HandSize == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store hand size!", new object[]
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
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No hand exists for player side {1}!", new object[]
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
			ZoneHand handZone = player.GetHandZone();
			if (handZone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find hand for player {1}!", new object[]
				{
					this,
					player
				});
				base.Finish();
				return;
			}
			this.m_HandSize.Value = handZone.GetCardCount();
			base.Finish();
		}

		// Token: 0x0400935B RID: 37723
		[Tooltip("Which player's hand are we querying the size of?")]
		public Player.Side m_PlayerSide;

		// Token: 0x0400935C RID: 37724
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmInt m_HandSize;
	}
}
