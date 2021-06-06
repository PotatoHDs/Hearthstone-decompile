using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DDD RID: 3549
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Color of a Light.")]
	public class SetLightColor : ComponentAction<Light>
	{
		// Token: 0x0600A62D RID: 42541 RVA: 0x00348935 File Offset: 0x00346B35
		public override void Reset()
		{
			this.gameObject = null;
			this.lightColor = Color.white;
			this.everyFrame = false;
		}

		// Token: 0x0600A62E RID: 42542 RVA: 0x00348955 File Offset: 0x00346B55
		public override void OnEnter()
		{
			this.DoSetLightColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A62F RID: 42543 RVA: 0x0034896B File Offset: 0x00346B6B
		public override void OnUpdate()
		{
			this.DoSetLightColor();
		}

		// Token: 0x0600A630 RID: 42544 RVA: 0x00348974 File Offset: 0x00346B74
		private void DoSetLightColor()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.color = this.lightColor.Value;
			}
		}

		// Token: 0x04008CC1 RID: 36033
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CC2 RID: 36034
		[RequiredField]
		public FsmColor lightColor;

		// Token: 0x04008CC3 RID: 36035
		public bool everyFrame;
	}
}
