using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE1 RID: 3553
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Range of a Light.")]
	public class SetLightRange : ComponentAction<Light>
	{
		// Token: 0x0600A63F RID: 42559 RVA: 0x00348AEA File Offset: 0x00346CEA
		public override void Reset()
		{
			this.gameObject = null;
			this.lightRange = 20f;
			this.everyFrame = false;
		}

		// Token: 0x0600A640 RID: 42560 RVA: 0x00348B0A File Offset: 0x00346D0A
		public override void OnEnter()
		{
			this.DoSetLightRange();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A641 RID: 42561 RVA: 0x00348B20 File Offset: 0x00346D20
		public override void OnUpdate()
		{
			this.DoSetLightRange();
		}

		// Token: 0x0600A642 RID: 42562 RVA: 0x00348B28 File Offset: 0x00346D28
		private void DoSetLightRange()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.range = this.lightRange.Value;
			}
		}

		// Token: 0x04008CCB RID: 36043
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CCC RID: 36044
		public FsmFloat lightRange;

		// Token: 0x04008CCD RID: 36045
		public bool everyFrame;
	}
}
