using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F36 RID: 3894
	[ActionCategory("Pegasus")]
	[Tooltip("Get scene ambient color")]
	public class GetBoardAmbientColorAction : FsmStateAction
	{
		// Token: 0x0600AC67 RID: 44135 RVA: 0x0035CA9F File Offset: 0x0035AC9F
		public override void Reset()
		{
			this.m_Color = Color.white;
		}

		// Token: 0x0600AC68 RID: 44136 RVA: 0x0035CAB4 File Offset: 0x0035ACB4
		public override void OnEnter()
		{
			this.m_Color.Value = RenderSettings.ambientLight;
			Board board = Board.Get();
			if (board != null)
			{
				this.m_Color.Value = board.m_AmbientColor;
			}
			base.Finish();
		}

		// Token: 0x04009343 RID: 37699
		public FsmColor m_Color;
	}
}
