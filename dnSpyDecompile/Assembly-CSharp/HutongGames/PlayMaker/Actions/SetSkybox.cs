using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF9 RID: 3577
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the global Skybox.")]
	public class SetSkybox : FsmStateAction
	{
		// Token: 0x0600A6AB RID: 42667 RVA: 0x00349ECE File Offset: 0x003480CE
		public override void Reset()
		{
			this.skybox = null;
		}

		// Token: 0x0600A6AC RID: 42668 RVA: 0x00349ED7 File Offset: 0x003480D7
		public override void OnEnter()
		{
			RenderSettings.skybox = this.skybox.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6AD RID: 42669 RVA: 0x00349EF7 File Offset: 0x003480F7
		public override void OnUpdate()
		{
			RenderSettings.skybox = this.skybox.Value;
		}

		// Token: 0x04008D2C RID: 36140
		public FsmMaterial skybox;

		// Token: 0x04008D2D RID: 36141
		[Tooltip("Repeat every frame. Useful if the Skybox is changing.")]
		public bool everyFrame;
	}
}
