using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF8 RID: 3576
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the strength of the shadows cast by a Light.")]
	public class SetShadowStrength : ComponentAction<Light>
	{
		// Token: 0x0600A6A6 RID: 42662 RVA: 0x00349E50 File Offset: 0x00348050
		public override void Reset()
		{
			this.gameObject = null;
			this.shadowStrength = 0.8f;
			this.everyFrame = false;
		}

		// Token: 0x0600A6A7 RID: 42663 RVA: 0x00349E70 File Offset: 0x00348070
		public override void OnEnter()
		{
			this.DoSetShadowStrength();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6A8 RID: 42664 RVA: 0x00349E86 File Offset: 0x00348086
		public override void OnUpdate()
		{
			this.DoSetShadowStrength();
		}

		// Token: 0x0600A6A9 RID: 42665 RVA: 0x00349E90 File Offset: 0x00348090
		private void DoSetShadowStrength()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.shadowStrength = this.shadowStrength.Value;
			}
		}

		// Token: 0x04008D29 RID: 36137
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D2A RID: 36138
		public FsmFloat shadowStrength;

		// Token: 0x04008D2B RID: 36139
		public bool everyFrame;
	}
}
