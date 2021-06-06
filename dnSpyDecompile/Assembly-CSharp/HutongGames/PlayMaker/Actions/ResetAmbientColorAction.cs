using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F6A RID: 3946
	[ActionCategory("Pegasus")]
	[Tooltip("Set scene ambient color")]
	public class ResetAmbientColorAction : FsmStateAction
	{
		// Token: 0x0600AD2E RID: 44334 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600AD2F RID: 44335 RVA: 0x00360310 File Offset: 0x0035E510
		public override void OnEnter()
		{
			Board board = Board.Get();
			if (board != null)
			{
				board.ResetAmbientColor();
			}
			base.Finish();
		}

		// Token: 0x04009414 RID: 37908
		private SetRenderSettings m_renderSettings;
	}
}
