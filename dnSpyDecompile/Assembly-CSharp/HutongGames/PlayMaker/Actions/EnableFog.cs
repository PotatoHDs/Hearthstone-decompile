using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C1E RID: 3102
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Enables/Disables Fog in the scene.")]
	public class EnableFog : FsmStateAction
	{
		// Token: 0x06009E09 RID: 40457 RVA: 0x0032A7F1 File Offset: 0x003289F1
		public override void Reset()
		{
			this.enableFog = true;
			this.everyFrame = false;
		}

		// Token: 0x06009E0A RID: 40458 RVA: 0x0032A806 File Offset: 0x00328A06
		public override void OnEnter()
		{
			RenderSettings.fog = this.enableFog.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E0B RID: 40459 RVA: 0x0032A826 File Offset: 0x00328A26
		public override void OnUpdate()
		{
			RenderSettings.fog = this.enableFog.Value;
		}

		// Token: 0x04008364 RID: 33636
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enableFog;

		// Token: 0x04008365 RID: 33637
		[Tooltip("Repeat every frame. Useful if the Enable Fog setting is changing.")]
		public bool everyFrame;
	}
}
