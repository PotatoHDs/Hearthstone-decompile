using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F45 RID: 3909
	[ActionCategory("Pegasus")]
	[Tooltip("Get shadow color")]
	public class GetShadowColor : FsmStateAction
	{
		// Token: 0x0600AC99 RID: 44185 RVA: 0x0035DC5D File Offset: 0x0035BE5D
		public override void Reset()
		{
			this.m_Color = Color.black;
		}

		// Token: 0x0600AC9A RID: 44186 RVA: 0x0035DC6F File Offset: 0x0035BE6F
		public override void OnEnter()
		{
			this.m_Color.Value = Board.Get().m_ShadowColor;
			base.Finish();
		}

		// Token: 0x0600AC9B RID: 44187 RVA: 0x0035DC8C File Offset: 0x0035BE8C
		public override void OnUpdate()
		{
			this.m_Color.Value = Board.Get().m_ShadowColor;
		}

		// Token: 0x0400936E RID: 37742
		public FsmColor m_Color;
	}
}
