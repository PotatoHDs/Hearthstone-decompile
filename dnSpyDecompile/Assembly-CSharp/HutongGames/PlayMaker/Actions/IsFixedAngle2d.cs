using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D0E RID: 3342
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Is the rigidbody2D constrained from rotating?Note: Prefer SetRigidBody2dConstraints when working in Unity 5")]
	public class IsFixedAngle2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A242 RID: 41538 RVA: 0x0033B4A0 File Offset: 0x003396A0
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.store = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A243 RID: 41539 RVA: 0x0033B4C5 File Offset: 0x003396C5
		public override void OnEnter()
		{
			this.DoIsFixedAngle();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A244 RID: 41540 RVA: 0x0033B4DB File Offset: 0x003396DB
		public override void OnUpdate()
		{
			this.DoIsFixedAngle();
		}

		// Token: 0x0600A245 RID: 41541 RVA: 0x0033B4E4 File Offset: 0x003396E4
		private void DoIsFixedAngle()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			bool flag = (base.rigidbody2d.constraints & RigidbodyConstraints2D.FreezeRotation) > RigidbodyConstraints2D.None;
			this.store.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x04008880 RID: 34944
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008881 RID: 34945
		[Tooltip("Event sent if the Rigidbody2D does have fixed angle")]
		public FsmEvent trueEvent;

		// Token: 0x04008882 RID: 34946
		[Tooltip("Event sent if the Rigidbody2D doesn't have fixed angle")]
		public FsmEvent falseEvent;

		// Token: 0x04008883 RID: 34947
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fixedAngle flag")]
		public FsmBool store;

		// Token: 0x04008884 RID: 34948
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
