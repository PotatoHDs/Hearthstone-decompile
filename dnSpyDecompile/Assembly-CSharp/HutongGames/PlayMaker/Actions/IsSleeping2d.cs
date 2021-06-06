using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D10 RID: 3344
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Tests if a Game Object's Rigidbody 2D is sleeping.")]
	public class IsSleeping2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A24C RID: 41548 RVA: 0x0033B5E9 File Offset: 0x003397E9
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.store = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A24D RID: 41549 RVA: 0x0033B60E File Offset: 0x0033980E
		public override void OnEnter()
		{
			this.DoIsSleeping();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A24E RID: 41550 RVA: 0x0033B624 File Offset: 0x00339824
		public override void OnUpdate()
		{
			this.DoIsSleeping();
		}

		// Token: 0x0600A24F RID: 41551 RVA: 0x0033B62C File Offset: 0x0033982C
		private void DoIsSleeping()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			bool flag = base.rigidbody2d.IsSleeping();
			this.store.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x0400888A RID: 34954
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400888B RID: 34955
		[Tooltip("Event sent if sleeping")]
		public FsmEvent trueEvent;

		// Token: 0x0400888C RID: 34956
		[Tooltip("Event sent if not sleeping")]
		public FsmEvent falseEvent;

		// Token: 0x0400888D RID: 34957
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value in a Boolean variable")]
		public FsmBool store;

		// Token: 0x0400888E RID: 34958
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
