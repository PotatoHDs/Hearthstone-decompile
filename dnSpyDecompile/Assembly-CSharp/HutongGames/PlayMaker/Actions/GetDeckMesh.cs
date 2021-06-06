using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F38 RID: 3896
	[ActionCategory("Pegasus")]
	[Tooltip("Takes in a player and returns the gameobject for the deck mesh.")]
	public class GetDeckMesh : FsmStateAction
	{
		// Token: 0x0600AC6D RID: 44141 RVA: 0x0035CBB3 File Offset: 0x0035ADB3
		public override void Reset()
		{
			this.m_PlayerSide = Player.Side.NEUTRAL;
			this.m_DeckMesh = null;
		}

		// Token: 0x0600AC6E RID: 44142 RVA: 0x0035CBC4 File Offset: 0x0035ADC4
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_DeckMesh == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store deck object!", new object[]
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
			this.m_DeckMesh.Value = deckZone.GetActiveThickness().gameObject;
			this.m_DeckMesh.Value.SetActive(true);
			base.Finish();
		}

		// Token: 0x04009345 RID: 37701
		[Tooltip("Which player's deck are we querying the size of?")]
		public Player.Side m_PlayerSide;

		// Token: 0x04009346 RID: 37702
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Output GameObject.")]
		public FsmGameObject m_DeckMesh;
	}
}
