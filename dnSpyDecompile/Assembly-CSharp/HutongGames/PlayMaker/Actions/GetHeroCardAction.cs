using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F40 RID: 3904
	[ActionCategory("Pegasus")]
	[Tooltip("Use this action to get the hero card for either side's hero.")]
	public class GetHeroCardAction : FsmStateAction
	{
		// Token: 0x0600AC88 RID: 44168 RVA: 0x0035D540 File Offset: 0x0035B740
		public override void Reset()
		{
			this.m_HeroCardGameObject = null;
		}

		// Token: 0x0600AC89 RID: 44169 RVA: 0x0035D54C File Offset: 0x0035B74C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_HeroCardGameObject == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store hero card!", new object[]
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
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No hero exists for player side {1}!", new object[]
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
			Card heroCard = player.GetHeroCard();
			if (heroCard == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find hero card for side {1}!", new object[]
				{
					this,
					this.m_PlayerSide
				});
				base.Finish();
				return;
			}
			this.m_HeroCardGameObject.Value = heroCard.gameObject;
			base.Finish();
		}

		// Token: 0x0400935D RID: 37725
		[Tooltip("Which player's hero are we querying for?")]
		public Player.Side m_PlayerSide;

		// Token: 0x0400935E RID: 37726
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmGameObject m_HeroCardGameObject;
	}
}
