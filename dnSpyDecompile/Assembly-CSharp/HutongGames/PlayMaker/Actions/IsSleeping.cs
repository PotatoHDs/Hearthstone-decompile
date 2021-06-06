using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CCD RID: 3277
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Tests if a Game Object's Rigid Body is sleeping.")]
	public class IsSleeping : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A0EA RID: 41194 RVA: 0x00332A40 File Offset: 0x00330C40
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.store = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0EB RID: 41195 RVA: 0x00332A65 File Offset: 0x00330C65
		public override void OnEnter()
		{
			this.DoIsSleeping();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0EC RID: 41196 RVA: 0x00332A7B File Offset: 0x00330C7B
		public override void OnUpdate()
		{
			this.DoIsSleeping();
		}

		// Token: 0x0600A0ED RID: 41197 RVA: 0x00332A84 File Offset: 0x00330C84
		private void DoIsSleeping()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				bool flag = base.rigidbody.IsSleeping();
				this.store.Value = flag;
				base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
			}
		}

		// Token: 0x0400867A RID: 34426
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400867B RID: 34427
		public FsmEvent trueEvent;

		// Token: 0x0400867C RID: 34428
		public FsmEvent falseEvent;

		// Token: 0x0400867D RID: 34429
		[UIHint(UIHint.Variable)]
		public FsmBool store;

		// Token: 0x0400867E RID: 34430
		public bool everyFrame;
	}
}
