using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C44 RID: 3140
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Angle between a GameObject's forward axis and a Target. The Target can be defined as a GameObject or a world Position. If you specify both, then the Position will be used as a local offset from the Target Object's position.")]
	public class GetAngleToTarget : FsmStateAction
	{
		// Token: 0x06009EB7 RID: 40631 RVA: 0x0032C25C File Offset: 0x0032A45C
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.ignoreHeight = true;
			this.storeAngle = null;
			this.everyFrame = false;
		}

		// Token: 0x06009EB8 RID: 40632 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x06009EB9 RID: 40633 RVA: 0x0032C2A6 File Offset: 0x0032A4A6
		public override void OnLateUpdate()
		{
			this.DoGetAngleToTarget();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EBA RID: 40634 RVA: 0x0032C2BC File Offset: 0x0032A4BC
		private void DoGetAngleToTarget()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			GameObject value = this.targetObject.Value;
			if (value == null && this.targetPosition.IsNone)
			{
				return;
			}
			Vector3 a;
			if (value != null)
			{
				a = ((!this.targetPosition.IsNone) ? value.transform.TransformPoint(this.targetPosition.Value) : value.transform.position);
			}
			else
			{
				a = this.targetPosition.Value;
			}
			if (this.ignoreHeight.Value)
			{
				a.y = ownerDefaultTarget.transform.position.y;
			}
			Vector3 from = a - ownerDefaultTarget.transform.position;
			this.storeAngle.Value = Vector3.Angle(from, ownerDefaultTarget.transform.forward);
		}

		// Token: 0x04008413 RID: 33811
		[RequiredField]
		[Tooltip("The game object whose forward axis we measure from. If the target is dead ahead the angle will be 0.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008414 RID: 33812
		[Tooltip("The target object to measure the angle to. Or use target position.")]
		public FsmGameObject targetObject;

		// Token: 0x04008415 RID: 33813
		[Tooltip("The world position to measure an angle to. If Target Object is also specified, this vector is used as an offset from that object's position.")]
		public FsmVector3 targetPosition;

		// Token: 0x04008416 RID: 33814
		[Tooltip("Ignore height differences when calculating the angle.")]
		public FsmBool ignoreHeight;

		// Token: 0x04008417 RID: 33815
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the angle in a float variable.")]
		public FsmFloat storeAngle;

		// Token: 0x04008418 RID: 33816
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
