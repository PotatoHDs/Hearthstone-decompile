using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F63 RID: 3939
	[ActionCategory("Pegasus")]
	[Tooltip("Store a player's cardback materials based on Player side.")]
	public class PlayerGetCardbackAction : FsmStateAction
	{
		// Token: 0x0600AD17 RID: 44311 RVA: 0x0035FDFB File Offset: 0x0035DFFB
		public override void Reset()
		{
			this.m_PlayerSide = Player.Side.FRIENDLY;
			this.m_CardbackMaterial = null;
			this.m_CardbackTextureFlat = null;
		}

		// Token: 0x0600AD18 RID: 44312 RVA: 0x0035FE14 File Offset: 0x0035E014
		public override void OnEnter()
		{
			base.OnEnter();
			Player.Side playerSide = this.m_PlayerSide;
			CardBack cardBack;
			if (playerSide == Player.Side.OPPOSING)
			{
				cardBack = CardBackManager.Get().GetOpponentCardBack();
			}
			else
			{
				cardBack = CardBackManager.Get().GetFriendlyCardBack();
			}
			if (cardBack != null)
			{
				this.m_CardbackMaterial.Value = cardBack.m_CardBackMaterial;
				this.m_CardbackTextureFlat.Value = cardBack.m_CardBackTexture;
			}
			base.Finish();
		}

		// Token: 0x040093F9 RID: 37881
		public Player.Side m_PlayerSide;

		// Token: 0x040093FA RID: 37882
		public FsmMaterial m_CardbackMaterial;

		// Token: 0x040093FB RID: 37883
		public FsmTexture m_CardbackTextureFlat;
	}
}
