using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E03 RID: 3587
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Forces a Game Object's Rigid Body to Sleep at least one frame.")]
	public class Sleep : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A6DB RID: 42715 RVA: 0x0034A7BE File Offset: 0x003489BE
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A6DC RID: 42716 RVA: 0x0034A7C7 File Offset: 0x003489C7
		public override void OnEnter()
		{
			this.DoSleep();
			base.Finish();
		}

		// Token: 0x0600A6DD RID: 42717 RVA: 0x0034A7D8 File Offset: 0x003489D8
		private void DoSleep()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.Sleep();
			}
		}

		// Token: 0x04008D58 RID: 36184
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
	}
}
