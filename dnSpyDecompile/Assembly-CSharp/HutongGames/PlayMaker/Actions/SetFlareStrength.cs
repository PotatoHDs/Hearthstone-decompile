using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB7 RID: 3511
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the intensity of all Flares in the scene.")]
	public class SetFlareStrength : FsmStateAction
	{
		// Token: 0x0600A585 RID: 42373 RVA: 0x00346D89 File Offset: 0x00344F89
		public override void Reset()
		{
			this.flareStrength = 0.2f;
			this.everyFrame = false;
		}

		// Token: 0x0600A586 RID: 42374 RVA: 0x00346DA2 File Offset: 0x00344FA2
		public override void OnEnter()
		{
			this.DoSetFlareStrength();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A587 RID: 42375 RVA: 0x00346DB8 File Offset: 0x00344FB8
		public override void OnUpdate()
		{
			this.DoSetFlareStrength();
		}

		// Token: 0x0600A588 RID: 42376 RVA: 0x00346DC0 File Offset: 0x00344FC0
		private void DoSetFlareStrength()
		{
			RenderSettings.flareStrength = this.flareStrength.Value;
		}

		// Token: 0x04008C10 RID: 35856
		[RequiredField]
		public FsmFloat flareStrength;

		// Token: 0x04008C11 RID: 35857
		public bool everyFrame;
	}
}
