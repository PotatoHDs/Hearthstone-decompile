using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE2 RID: 3554
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Spot Angle of a Light.")]
	public class SetLightSpotAngle : ComponentAction<Light>
	{
		// Token: 0x0600A644 RID: 42564 RVA: 0x00348B66 File Offset: 0x00346D66
		public override void Reset()
		{
			this.gameObject = null;
			this.lightSpotAngle = 20f;
			this.everyFrame = false;
		}

		// Token: 0x0600A645 RID: 42565 RVA: 0x00348B86 File Offset: 0x00346D86
		public override void OnEnter()
		{
			this.DoSetLightRange();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A646 RID: 42566 RVA: 0x00348B9C File Offset: 0x00346D9C
		public override void OnUpdate()
		{
			this.DoSetLightRange();
		}

		// Token: 0x0600A647 RID: 42567 RVA: 0x00348BA4 File Offset: 0x00346DA4
		private void DoSetLightRange()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.spotAngle = this.lightSpotAngle.Value;
			}
		}

		// Token: 0x04008CCE RID: 36046
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CCF RID: 36047
		public FsmFloat lightSpotAngle;

		// Token: 0x04008CD0 RID: 36048
		public bool everyFrame;
	}
}
