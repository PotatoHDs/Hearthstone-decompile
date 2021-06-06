namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Set scene ambient color")]
	public class ResetAmbientColorAction : FsmStateAction
	{
		private SetRenderSettings m_renderSettings;

		public override void Reset()
		{
		}

		public override void OnEnter()
		{
			Board board = Board.Get();
			if (board != null)
			{
				board.ResetAmbientColor();
			}
			Finish();
		}
	}
}
