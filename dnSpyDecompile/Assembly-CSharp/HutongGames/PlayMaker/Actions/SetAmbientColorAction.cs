using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F71 RID: 3953
	[ActionCategory("Pegasus")]
	[Tooltip("Set scene ambient color")]
	public class SetAmbientColorAction : FsmStateAction
	{
		// Token: 0x0600AD49 RID: 44361 RVA: 0x003607E0 File Offset: 0x0035E9E0
		public override void Reset()
		{
			this.m_Color = null;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AD4A RID: 44362 RVA: 0x003607F0 File Offset: 0x0035E9F0
		public override void OnEnter()
		{
			RenderSettings.ambientLight = this.m_Color.Value;
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD4B RID: 44363 RVA: 0x00360810 File Offset: 0x0035EA10
		public override void OnUpdate()
		{
			RenderSettings.ambientLight = this.m_Color.Value;
		}

		// Token: 0x04009429 RID: 37929
		public FsmColor m_Color;

		// Token: 0x0400942A RID: 37930
		public bool m_EveryFrame;
	}
}
