using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DDE RID: 3550
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Texture projected by a Light.")]
	public class SetLightCookie : ComponentAction<Light>
	{
		// Token: 0x0600A632 RID: 42546 RVA: 0x003489BA File Offset: 0x00346BBA
		public override void Reset()
		{
			this.gameObject = null;
			this.lightCookie = null;
		}

		// Token: 0x0600A633 RID: 42547 RVA: 0x003489CA File Offset: 0x00346BCA
		public override void OnEnter()
		{
			this.DoSetLightCookie();
			base.Finish();
		}

		// Token: 0x0600A634 RID: 42548 RVA: 0x003489D8 File Offset: 0x00346BD8
		private void DoSetLightCookie()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.light.cookie = this.lightCookie.Value;
			}
		}

		// Token: 0x04008CC4 RID: 36036
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CC5 RID: 36037
		public FsmTexture lightCookie;
	}
}
