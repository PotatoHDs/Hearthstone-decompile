using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF6 RID: 3574
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the Rotation of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
	public class SetRotation : FsmStateAction
	{
		// Token: 0x0600A698 RID: 42648 RVA: 0x00349B04 File Offset: 0x00347D04
		public override void Reset()
		{
			this.gameObject = null;
			this.quaternion = null;
			this.vector = null;
			this.xAngle = new FsmFloat
			{
				UseVariable = true
			};
			this.yAngle = new FsmFloat
			{
				UseVariable = true
			};
			this.zAngle = new FsmFloat
			{
				UseVariable = true
			};
			this.space = Space.World;
			this.everyFrame = false;
			this.lateUpdate = false;
		}

		// Token: 0x0600A699 RID: 42649 RVA: 0x00349B71 File Offset: 0x00347D71
		public override void OnPreprocess()
		{
			if (this.lateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x0600A69A RID: 42650 RVA: 0x00349B87 File Offset: 0x00347D87
		public override void OnEnter()
		{
			if (!this.everyFrame && !this.lateUpdate)
			{
				this.DoSetRotation();
				base.Finish();
			}
		}

		// Token: 0x0600A69B RID: 42651 RVA: 0x00349BA5 File Offset: 0x00347DA5
		public override void OnUpdate()
		{
			if (!this.lateUpdate)
			{
				this.DoSetRotation();
			}
		}

		// Token: 0x0600A69C RID: 42652 RVA: 0x00349BB5 File Offset: 0x00347DB5
		public override void OnLateUpdate()
		{
			if (this.lateUpdate)
			{
				this.DoSetRotation();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A69D RID: 42653 RVA: 0x00349BD4 File Offset: 0x00347DD4
		private void DoSetRotation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector;
			if (!this.quaternion.IsNone)
			{
				vector = this.quaternion.Value.eulerAngles;
			}
			else if (!this.vector.IsNone)
			{
				vector = this.vector.Value;
			}
			else
			{
				vector = ((this.space == Space.Self) ? ownerDefaultTarget.transform.localEulerAngles : ownerDefaultTarget.transform.eulerAngles);
			}
			if (!this.xAngle.IsNone)
			{
				vector.x = this.xAngle.Value;
			}
			if (!this.yAngle.IsNone)
			{
				vector.y = this.yAngle.Value;
			}
			if (!this.zAngle.IsNone)
			{
				vector.z = this.zAngle.Value;
			}
			if (this.space == Space.Self)
			{
				ownerDefaultTarget.transform.localEulerAngles = vector;
				return;
			}
			ownerDefaultTarget.transform.eulerAngles = vector;
		}

		// Token: 0x04008D19 RID: 36121
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D1A RID: 36122
		[UIHint(UIHint.Variable)]
		[Tooltip("Use a stored quaternion, or vector angles below.")]
		public FsmQuaternion quaternion;

		// Token: 0x04008D1B RID: 36123
		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		[Tooltip("Use euler angles stored in a Vector3 variable, and/or set each axis below.")]
		public FsmVector3 vector;

		// Token: 0x04008D1C RID: 36124
		public FsmFloat xAngle;

		// Token: 0x04008D1D RID: 36125
		public FsmFloat yAngle;

		// Token: 0x04008D1E RID: 36126
		public FsmFloat zAngle;

		// Token: 0x04008D1F RID: 36127
		[Tooltip("Use local or world space.")]
		public Space space;

		// Token: 0x04008D20 RID: 36128
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008D21 RID: 36129
		[Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;
	}
}
