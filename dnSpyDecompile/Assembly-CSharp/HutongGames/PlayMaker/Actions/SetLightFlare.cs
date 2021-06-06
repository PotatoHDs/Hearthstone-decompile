using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DDF RID: 3551
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Flare effect used by a Light.")]
	public class SetLightFlare : ComponentAction<Light>
	{
		// Token: 0x0600A636 RID: 42550 RVA: 0x00348A16 File Offset: 0x00346C16
		public override void Reset()
		{
			this.gameObject = null;
			this.lightFlare = null;
		}

		// Token: 0x0600A637 RID: 42551 RVA: 0x00348A26 File Offset: 0x00346C26
		public override void OnEnter()
		{
			this.DoSetLightRange();
			base.Finish();
		}

		// Token: 0x0600A638 RID: 42552 RVA: 0x00348A34 File Offset: 0x00346C34
		private void DoSetLightRange()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.flare = this.lightFlare;
			}
		}

		// Token: 0x04008CC6 RID: 36038
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CC7 RID: 36039
		public Flare lightFlare;
	}
}
