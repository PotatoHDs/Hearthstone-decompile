using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D22 RID: 3362
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Smoothly Rotates a 2d Game Object so its right vector points at a Target. The target can be defined as a 2d Game Object or a 2d/3d world Position. If you specify both, then the position will be used as a local offset from the object's position.")]
	public class SmoothLookAt2d : FsmStateAction
	{
		// Token: 0x0600A2A3 RID: 41635 RVA: 0x0033CE4C File Offset: 0x0033B04C
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.targetPosition2d = new FsmVector2
			{
				UseVariable = true
			};
			this.targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.rotationOffset = 0f;
			this.debug = false;
			this.speed = 5f;
			this.finishTolerance = 1f;
			this.finishEvent = null;
		}

		// Token: 0x0600A2A4 RID: 41636 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600A2A5 RID: 41637 RVA: 0x0033CECE File Offset: 0x0033B0CE
		public override void OnEnter()
		{
			this.previousGo = null;
		}

		// Token: 0x0600A2A6 RID: 41638 RVA: 0x0033CED7 File Offset: 0x0033B0D7
		public override void OnLateUpdate()
		{
			this.DoSmoothLookAt();
		}

		// Token: 0x0600A2A7 RID: 41639 RVA: 0x0033CEE0 File Offset: 0x0033B0E0
		private void DoSmoothLookAt()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			GameObject value = this.targetObject.Value;
			if (this.previousGo != ownerDefaultTarget)
			{
				this.lastRotation = ownerDefaultTarget.transform.rotation;
				this.desiredRotation = this.lastRotation;
				this.previousGo = ownerDefaultTarget;
			}
			Vector3 vector = new Vector3(this.targetPosition2d.Value.x, this.targetPosition2d.Value.y, 0f);
			if (!this.targetPosition.IsNone)
			{
				vector += this.targetPosition.Value;
			}
			if (value != null)
			{
				vector = value.transform.position;
				Vector3 vector2 = Vector3.zero;
				if (!this.targetPosition.IsNone)
				{
					vector2 += this.targetPosition.Value;
				}
				if (!this.targetPosition2d.IsNone)
				{
					vector2.x += this.targetPosition2d.Value.x;
					vector2.y += this.targetPosition2d.Value.y;
				}
				if (!this.targetPosition2d.IsNone || !this.targetPosition.IsNone)
				{
					vector += value.transform.TransformPoint(this.targetPosition2d.Value);
				}
			}
			Vector3 vector3 = vector - ownerDefaultTarget.transform.position;
			vector3.Normalize();
			float num = Mathf.Atan2(vector3.y, vector3.x) * 57.29578f;
			this.desiredRotation = Quaternion.Euler(0f, 0f, num - this.rotationOffset.Value);
			this.lastRotation = Quaternion.Slerp(this.lastRotation, this.desiredRotation, this.speed.Value * Time.deltaTime);
			ownerDefaultTarget.transform.rotation = this.lastRotation;
			if (this.debug.Value)
			{
				Debug.DrawLine(ownerDefaultTarget.transform.position, vector, Color.grey);
			}
			if (this.finishEvent != null && Mathf.Abs(Vector3.Angle(this.desiredRotation.eulerAngles, this.lastRotation.eulerAngles)) <= this.finishTolerance.Value)
			{
				base.Fsm.Event(this.finishEvent);
			}
		}

		// Token: 0x0400890B RID: 35083
		[RequiredField]
		[Tooltip("The GameObject to rotate to face a target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400890C RID: 35084
		[Tooltip("A target GameObject.")]
		public FsmGameObject targetObject;

		// Token: 0x0400890D RID: 35085
		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector2 targetPosition2d;

		// Token: 0x0400890E RID: 35086
		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector3 targetPosition;

		// Token: 0x0400890F RID: 35087
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;

		// Token: 0x04008910 RID: 35088
		[HasFloatSlider(0.5f, 15f)]
		[Tooltip("How fast the look at moves.")]
		public FsmFloat speed;

		// Token: 0x04008911 RID: 35089
		[Tooltip("Draw a line in the Scene View to the look at position.")]
		public FsmBool debug;

		// Token: 0x04008912 RID: 35090
		[Tooltip("If the angle to the target is less than this, send the Finish Event below. Measured in degrees.")]
		public FsmFloat finishTolerance;

		// Token: 0x04008913 RID: 35091
		[Tooltip("Event to send if the angle to target is less than the Finish Tolerance.")]
		public FsmEvent finishEvent;

		// Token: 0x04008914 RID: 35092
		private GameObject previousGo;

		// Token: 0x04008915 RID: 35093
		private Quaternion lastRotation;

		// Token: 0x04008916 RID: 35094
		private Quaternion desiredRotation;
	}
}
