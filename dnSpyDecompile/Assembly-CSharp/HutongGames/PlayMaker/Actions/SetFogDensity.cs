using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DBA RID: 3514
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the density of the Fog in the scene.")]
	public class SetFogDensity : FsmStateAction
	{
		// Token: 0x0600A593 RID: 42387 RVA: 0x00346E70 File Offset: 0x00345070
		public override void Reset()
		{
			this.fogDensity = 0.5f;
			this.everyFrame = false;
		}

		// Token: 0x0600A594 RID: 42388 RVA: 0x00346E89 File Offset: 0x00345089
		public override void OnEnter()
		{
			this.DoSetFogDensity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A595 RID: 42389 RVA: 0x00346E9F File Offset: 0x0034509F
		public override void OnUpdate()
		{
			this.DoSetFogDensity();
		}

		// Token: 0x0600A596 RID: 42390 RVA: 0x00346EA7 File Offset: 0x003450A7
		private void DoSetFogDensity()
		{
			RenderSettings.fogDensity = this.fogDensity.Value;
		}

		// Token: 0x04008C17 RID: 35863
		[RequiredField]
		public FsmFloat fogDensity;

		// Token: 0x04008C18 RID: 35864
		public bool everyFrame;
	}
}
