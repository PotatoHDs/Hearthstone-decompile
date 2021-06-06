using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF7 RID: 3575
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the Scale of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
	public class SetScale : FsmStateAction
	{
		// Token: 0x0600A69F RID: 42655 RVA: 0x00349CDC File Offset: 0x00347EDC
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.z = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
			this.lateUpdate = false;
		}

		// Token: 0x0600A6A0 RID: 42656 RVA: 0x00349D3B File Offset: 0x00347F3B
		public override void OnPreprocess()
		{
			if (this.lateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x0600A6A1 RID: 42657 RVA: 0x00349D51 File Offset: 0x00347F51
		public override void OnEnter()
		{
			this.DoSetScale();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6A2 RID: 42658 RVA: 0x00349D67 File Offset: 0x00347F67
		public override void OnUpdate()
		{
			if (!this.lateUpdate)
			{
				this.DoSetScale();
			}
		}

		// Token: 0x0600A6A3 RID: 42659 RVA: 0x00349D77 File Offset: 0x00347F77
		public override void OnLateUpdate()
		{
			if (this.lateUpdate)
			{
				this.DoSetScale();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6A4 RID: 42660 RVA: 0x00349D98 File Offset: 0x00347F98
		private void DoSetScale()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 localScale = this.vector.IsNone ? ownerDefaultTarget.transform.localScale : this.vector.Value;
			if (!this.x.IsNone)
			{
				localScale.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				localScale.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				localScale.z = this.z.Value;
			}
			ownerDefaultTarget.transform.localScale = localScale;
		}

		// Token: 0x04008D22 RID: 36130
		[RequiredField]
		[Tooltip("The GameObject to scale.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D23 RID: 36131
		[UIHint(UIHint.Variable)]
		[Tooltip("Use stored Vector3 value, and/or set each axis below.")]
		public FsmVector3 vector;

		// Token: 0x04008D24 RID: 36132
		public FsmFloat x;

		// Token: 0x04008D25 RID: 36133
		public FsmFloat y;

		// Token: 0x04008D26 RID: 36134
		public FsmFloat z;

		// Token: 0x04008D27 RID: 36135
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008D28 RID: 36136
		[Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;
	}
}
