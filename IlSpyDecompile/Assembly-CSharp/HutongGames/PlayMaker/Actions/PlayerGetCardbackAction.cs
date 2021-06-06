namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Store a player's cardback materials based on Player side.")]
	public class PlayerGetCardbackAction : FsmStateAction
	{
		public Player.Side m_PlayerSide;

		public FsmMaterial m_CardbackMaterial;

		public FsmTexture m_CardbackTextureFlat;

		public override void Reset()
		{
			m_PlayerSide = Player.Side.FRIENDLY;
			m_CardbackMaterial = null;
			m_CardbackTextureFlat = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Player.Side playerSide = m_PlayerSide;
			CardBack cardBack = ((playerSide != Player.Side.OPPOSING) ? CardBackManager.Get().GetFriendlyCardBack() : CardBackManager.Get().GetOpponentCardBack());
			if (cardBack != null)
			{
				m_CardbackMaterial.Value = cardBack.m_CardBackMaterial;
				m_CardbackTextureFlat.Value = cardBack.m_CardBackTexture;
			}
			Finish();
		}
	}
}
