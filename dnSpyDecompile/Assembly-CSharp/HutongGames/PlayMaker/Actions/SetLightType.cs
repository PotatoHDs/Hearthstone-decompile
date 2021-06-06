using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE3 RID: 3555
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Set Spot, Directional, or Point Light type.")]
	public class SetLightType : ComponentAction<Light>
	{
		// Token: 0x0600A649 RID: 42569 RVA: 0x00348BE2 File Offset: 0x00346DE2
		public override void Reset()
		{
			this.gameObject = null;
			this.lightType = LightType.Point;
		}

		// Token: 0x0600A64A RID: 42570 RVA: 0x00348BFC File Offset: 0x00346DFC
		public override void OnEnter()
		{
			this.DoSetLightType();
			base.Finish();
		}

		// Token: 0x0600A64B RID: 42571 RVA: 0x00348C0C File Offset: 0x00346E0C
		private void DoSetLightType()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.type = (LightType)this.lightType.Value;
			}
		}

		// Token: 0x04008CD1 RID: 36049
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CD2 RID: 36050
		[ObjectType(typeof(LightType))]
		public FsmEnum lightType;
	}
}
