using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E05 RID: 3589
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Smoothly Rotates a Game Object so its forward vector points at a Target. The target can be defined as a Game Object or a world Position. If you specify both, then the position will be used as a local offset from the object's position.")]
	public class SmoothLookAt : FsmStateAction
	{
		// Token: 0x0600A6E3 RID: 42723 RVA: 0x0034AA24 File Offset: 0x00348C24
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.upVector = new FsmVector3
			{
				UseVariable = true
			};
			this.keepVertical = true;
			this.debug = false;
			this.speed = 5f;
			this.finishTolerance = 1f;
			this.finishEvent = null;
		}

		// Token: 0x0600A6E4 RID: 42724 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600A6E5 RID: 42725 RVA: 0x0034AAA2 File Offset: 0x00348CA2
		public override void OnEnter()
		{
			this.previousGo = null;
		}

		// Token: 0x0600A6E6 RID: 42726 RVA: 0x0034AAAB File Offset: 0x00348CAB
		public override void OnLateUpdate()
		{
			this.DoSmoothLookAt();
		}

		// Token: 0x0600A6E7 RID: 42727 RVA: 0x0034AAB4 File Offset: 0x00348CB4
		private void DoSmoothLookAt()
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
			if (this.previousGo != ownerDefaultTarget)
			{
				this.lastRotation = ownerDefaultTarget.transform.rotation;
				this.desiredRotation = this.lastRotation;
				this.previousGo = ownerDefaultTarget;
			}
			Vector3 vector;
			if (value != null)
			{
				vector = ((!this.targetPosition.IsNone) ? value.transform.TransformPoint(this.targetPosition.Value) : value.transform.position);
			}
			else
			{
				vector = this.targetPosition.Value;
			}
			if (this.keepVertical.Value)
			{
				vector.y = ownerDefaultTarget.transform.position.y;
			}
			Vector3 vector2 = vector - ownerDefaultTarget.transform.position;
			if (vector2 != Vector3.zero && vector2.sqrMagnitude > 0f)
			{
				this.desiredRotation = Quaternion.LookRotation(vector2, this.upVector.IsNone ? Vector3.up : this.upVector.Value);
			}
			this.lastRotation = Quaternion.Slerp(this.lastRotation, this.desiredRotation, this.speed.Value * Time.deltaTime);
			ownerDefaultTarget.transform.rotation = this.lastRotation;
			if (this.debug.Value)
			{
				Debug.DrawLine(ownerDefaultTarget.transform.position, vector, Color.grey);
			}
			if (this.finishEvent != null && Mathf.Abs(Vector3.Angle(vector - ownerDefaultTarget.transform.position, ownerDefaultTarget.transform.forward)) <= this.finishTolerance.Value)
			{
				base.Fsm.Event(this.finishEvent);
			}
		}

		// Token: 0x04008D63 RID: 36195
		[RequiredField]
		[Tooltip("The GameObject to rotate to face a target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D64 RID: 36196
		[Tooltip("A target GameObject.")]
		public FsmGameObject targetObject;

		// Token: 0x04008D65 RID: 36197
		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector3 targetPosition;

		// Token: 0x04008D66 RID: 36198
		[Tooltip("Used to keep the game object generally upright. If left undefined the world y axis is used.")]
		public FsmVector3 upVector;

		// Token: 0x04008D67 RID: 36199
		[Tooltip("Force the game object to remain vertical. Useful for characters.")]
		public FsmBool keepVertical;

		// Token: 0x04008D68 RID: 36200
		[HasFloatSlider(0.5f, 15f)]
		[Tooltip("How fast the look at moves.")]
		public FsmFloat speed;

		// Token: 0x04008D69 RID: 36201
		[Tooltip("Draw a line in the Scene View to the look at position.")]
		public FsmBool debug;

		// Token: 0x04008D6A RID: 36202
		[Tooltip("If the angle to the target is less than this, send the Finish Event below. Measured in degrees.")]
		public FsmFloat finishTolerance;

		// Token: 0x04008D6B RID: 36203
		[Tooltip("Event to send if the angle to target is less than the Finish Tolerance.")]
		public FsmEvent finishEvent;

		// Token: 0x04008D6C RID: 36204
		private GameObject previousGo;

		// Token: 0x04008D6D RID: 36205
		private Quaternion lastRotation;

		// Token: 0x04008D6E RID: 36206
		private Quaternion desiredRotation;
	}
}
