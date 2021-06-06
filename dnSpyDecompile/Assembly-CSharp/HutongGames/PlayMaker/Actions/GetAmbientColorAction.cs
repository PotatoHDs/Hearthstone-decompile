using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F34 RID: 3892
	[ActionCategory("Pegasus")]
	[Tooltip("Get scene ambient color")]
	public class GetAmbientColorAction : FsmStateAction
	{
		// Token: 0x0600AC60 RID: 44128 RVA: 0x0035C9B6 File Offset: 0x0035ABB6
		public override void Reset()
		{
			this.m_Color = Color.white;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AC61 RID: 44129 RVA: 0x0035C9CF File Offset: 0x0035ABCF
		public override void OnEnter()
		{
			this.m_Color.Value = RenderSettings.ambientLight;
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC62 RID: 44130 RVA: 0x0035C9EF File Offset: 0x0035ABEF
		public override void OnUpdate()
		{
			this.m_Color.Value = RenderSettings.ambientLight;
		}

		// Token: 0x0400933C RID: 37692
		public FsmColor m_Color;

		// Token: 0x0400933D RID: 37693
		public bool m_EveryFrame;
	}
}
