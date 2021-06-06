using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE0 RID: 3552
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Intensity of a Light.")]
	public class SetLightIntensity : ComponentAction<Light>
	{
		// Token: 0x0600A63A RID: 42554 RVA: 0x00348A6D File Offset: 0x00346C6D
		public override void Reset()
		{
			this.gameObject = null;
			this.lightIntensity = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A63B RID: 42555 RVA: 0x00348A8D File Offset: 0x00346C8D
		public override void OnEnter()
		{
			this.DoSetLightIntensity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A63C RID: 42556 RVA: 0x00348AA3 File Offset: 0x00346CA3
		public override void OnUpdate()
		{
			this.DoSetLightIntensity();
		}

		// Token: 0x0600A63D RID: 42557 RVA: 0x00348AAC File Offset: 0x00346CAC
		private void DoSetLightIntensity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.intensity = this.lightIntensity.Value;
			}
		}

		// Token: 0x04008CC8 RID: 36040
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CC9 RID: 36041
		public FsmFloat lightIntensity;

		// Token: 0x04008CCA RID: 36042
		public bool everyFrame;
	}
}
