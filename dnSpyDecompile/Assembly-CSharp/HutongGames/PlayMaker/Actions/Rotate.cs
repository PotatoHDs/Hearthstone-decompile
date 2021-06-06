using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D75 RID: 3445
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a Game Object around each Axis. Use a Vector3 Variable and/or XYZ components. To leave any axis unchanged, set variable to 'None'.")]
	public class Rotate : FsmStateAction
	{
		// Token: 0x0600A446 RID: 42054 RVA: 0x003428B0 File Offset: 0x00340AB0
		public override void Reset()
		{
			this.gameObject = null;
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
			this.space = Space.Self;
			this.perSecond = false;
			this.everyFrame = true;
			this.lateUpdate = false;
			this.fixedUpdate = false;
		}

		// Token: 0x0600A447 RID: 42055 RVA: 0x00342924 File Offset: 0x00340B24
		public override void OnPreprocess()
		{
			if (this.fixedUpdate)
			{
				base.Fsm.HandleFixedUpdate = true;
			}
			if (this.lateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x0600A448 RID: 42056 RVA: 0x0034294E File Offset: 0x00340B4E
		public override void OnEnter()
		{
			if (!this.everyFrame && !this.lateUpdate && !this.fixedUpdate)
			{
				this.DoRotate();
				base.Finish();
			}
		}

		// Token: 0x0600A449 RID: 42057 RVA: 0x00342974 File Offset: 0x00340B74
		public override void OnUpdate()
		{
			if (!this.lateUpdate && !this.fixedUpdate)
			{
				this.DoRotate();
			}
		}

		// Token: 0x0600A44A RID: 42058 RVA: 0x0034298C File Offset: 0x00340B8C
		public override void OnLateUpdate()
		{
			if (this.lateUpdate)
			{
				this.DoRotate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A44B RID: 42059 RVA: 0x003429AA File Offset: 0x00340BAA
		public override void OnFixedUpdate()
		{
			if (this.fixedUpdate)
			{
				this.DoRotate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A44C RID: 42060 RVA: 0x003429C8 File Offset: 0x00340BC8
		private void DoRotate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = this.vector.IsNone ? new Vector3(this.xAngle.Value, this.yAngle.Value, this.zAngle.Value) : this.vector.Value;
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
			if (!this.perSecond)
			{
				ownerDefaultTarget.transform.Rotate(vector, this.space);
				return;
			}
			ownerDefaultTarget.transform.Rotate(vector * Time.deltaTime, this.space);
		}

		// Token: 0x04008AB4 RID: 35508
		[RequiredField]
		[Tooltip("The game object to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008AB5 RID: 35509
		[Tooltip("A rotation vector specifying rotation around x, y, and z axis. NOTE: You can override individual axis below.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		// Token: 0x04008AB6 RID: 35510
		[Tooltip("Rotation around x axis.")]
		public FsmFloat xAngle;

		// Token: 0x04008AB7 RID: 35511
		[Tooltip("Rotation around y axis.")]
		public FsmFloat yAngle;

		// Token: 0x04008AB8 RID: 35512
		[Tooltip("Rotation around z axis.")]
		public FsmFloat zAngle;

		// Token: 0x04008AB9 RID: 35513
		[Tooltip("Rotate in local or world space.")]
		public Space space;

		// Token: 0x04008ABA RID: 35514
		[Tooltip("Rotation is specified in degrees per second. In other words, the amount to rotate in over one second. This allows rotations to be frame rate independent. It is the same as multiplying the rotation by Time.deltaTime.")]
		public bool perSecond;

		// Token: 0x04008ABB RID: 35515
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008ABC RID: 35516
		[Tooltip("Perform the rotation in LateUpdate. This is useful if you want to override the rotation of objects that are animated or otherwise rotated in Update.")]
		public bool lateUpdate;

		// Token: 0x04008ABD RID: 35517
		[Tooltip("Perform the rotation in FixedUpdate. This is useful when working with rigid bodies and physics.")]
		public bool fixedUpdate;
	}
}
