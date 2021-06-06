using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD7 RID: 3543
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the size of light halos.")]
	public class SetHaloStrength : FsmStateAction
	{
		// Token: 0x0600A615 RID: 42517 RVA: 0x0034870E File Offset: 0x0034690E
		public override void Reset()
		{
			this.haloStrength = 0.5f;
			this.everyFrame = false;
		}

		// Token: 0x0600A616 RID: 42518 RVA: 0x00348727 File Offset: 0x00346927
		public override void OnEnter()
		{
			this.DoSetHaloStrength();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A617 RID: 42519 RVA: 0x0034873D File Offset: 0x0034693D
		public override void OnUpdate()
		{
			this.DoSetHaloStrength();
		}

		// Token: 0x0600A618 RID: 42520 RVA: 0x00348745 File Offset: 0x00346945
		private void DoSetHaloStrength()
		{
			RenderSettings.haloStrength = this.haloStrength.Value;
		}

		// Token: 0x04008CB3 RID: 36019
		[RequiredField]
		public FsmFloat haloStrength;

		// Token: 0x04008CB4 RID: 36020
		public bool everyFrame;
	}
}
