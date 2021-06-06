using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D0F RID: 3343
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Tests if a Game Object's Rigid Body 2D is Kinematic.")]
	public class IsKinematic2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A247 RID: 41543 RVA: 0x0033B548 File Offset: 0x00339748
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.store = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A248 RID: 41544 RVA: 0x0033B56D File Offset: 0x0033976D
		public override void OnEnter()
		{
			this.DoIsKinematic();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A249 RID: 41545 RVA: 0x0033B583 File Offset: 0x00339783
		public override void OnUpdate()
		{
			this.DoIsKinematic();
		}

		// Token: 0x0600A24A RID: 41546 RVA: 0x0033B58C File Offset: 0x0033978C
		private void DoIsKinematic()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			bool isKinematic = base.rigidbody2d.isKinematic;
			this.store.Value = isKinematic;
			base.Fsm.Event(isKinematic ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x04008885 RID: 34949
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("the GameObject with a Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008886 RID: 34950
		[Tooltip("Event Sent if Kinematic")]
		public FsmEvent trueEvent;

		// Token: 0x04008887 RID: 34951
		[Tooltip("Event sent if not Kinematic")]
		public FsmEvent falseEvent;

		// Token: 0x04008888 RID: 34952
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Kinematic state")]
		public FsmBool store;

		// Token: 0x04008889 RID: 34953
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
