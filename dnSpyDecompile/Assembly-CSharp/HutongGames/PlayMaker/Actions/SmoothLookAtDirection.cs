using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E06 RID: 3590
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Smoothly Rotates a Game Object so its forward vector points in the specified Direction. Lets you fire an event when minmagnitude is reached")]
	public class SmoothLookAtDirection : FsmStateAction
	{
		// Token: 0x0600A6E9 RID: 42729 RVA: 0x0034ACA0 File Offset: 0x00348EA0
		public override void Reset()
		{
			this.gameObject = null;
			this.targetDirection = new FsmVector3
			{
				UseVariable = true
			};
			this.minMagnitude = 0.1f;
			this.upVector = new FsmVector3
			{
				UseVariable = true
			};
			this.keepVertical = true;
			this.speed = 5f;
			this.lateUpdate = true;
			this.finishEvent = null;
		}

		// Token: 0x0600A6EA RID: 42730 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600A6EB RID: 42731 RVA: 0x0034AD12 File Offset: 0x00348F12
		public override void OnEnter()
		{
			this.previousGo = null;
		}

		// Token: 0x0600A6EC RID: 42732 RVA: 0x0034AD1B File Offset: 0x00348F1B
		public override void OnUpdate()
		{
			if (!this.lateUpdate)
			{
				this.DoSmoothLookAtDirection();
			}
		}

		// Token: 0x0600A6ED RID: 42733 RVA: 0x0034AD2B File Offset: 0x00348F2B
		public override void OnLateUpdate()
		{
			if (this.lateUpdate)
			{
				this.DoSmoothLookAtDirection();
			}
		}

		// Token: 0x0600A6EE RID: 42734 RVA: 0x0034AD3C File Offset: 0x00348F3C
		private void DoSmoothLookAtDirection()
		{
			if (this.targetDirection.IsNone)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.previousGo != ownerDefaultTarget)
			{
				this.lastRotation = ownerDefaultTarget.transform.rotation;
				this.desiredRotation = this.lastRotation;
				this.previousGo = ownerDefaultTarget;
			}
			Vector3 value = this.targetDirection.Value;
			if (this.keepVertical.Value)
			{
				value.y = 0f;
			}
			bool flag = false;
			if (value.sqrMagnitude > this.minMagnitude.Value)
			{
				this.desiredRotation = Quaternion.LookRotation(value, this.upVector.IsNone ? Vector3.up : this.upVector.Value);
			}
			else
			{
				flag = true;
			}
			this.lastRotation = Quaternion.Slerp(this.lastRotation, this.desiredRotation, this.speed.Value * Time.deltaTime);
			ownerDefaultTarget.transform.rotation = this.lastRotation;
			if (flag)
			{
				base.Fsm.Event(this.finishEvent);
				if (this.finish.Value)
				{
					base.Finish();
				}
			}
		}

		// Token: 0x04008D6F RID: 36207
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D70 RID: 36208
		[RequiredField]
		[Tooltip("The direction to smoothly rotate towards.")]
		public FsmVector3 targetDirection;

		// Token: 0x04008D71 RID: 36209
		[Tooltip("Only rotate if Target Direction Vector length is greater than this threshold.")]
		public FsmFloat minMagnitude;

		// Token: 0x04008D72 RID: 36210
		[Tooltip("Keep this vector pointing up as the GameObject rotates.")]
		public FsmVector3 upVector;

		// Token: 0x04008D73 RID: 36211
		[RequiredField]
		[Tooltip("Eliminate any tilt up/down as the GameObject rotates.")]
		public FsmBool keepVertical;

		// Token: 0x04008D74 RID: 36212
		[RequiredField]
		[HasFloatSlider(0.5f, 15f)]
		[Tooltip("How quickly to rotate.")]
		public FsmFloat speed;

		// Token: 0x04008D75 RID: 36213
		[Tooltip("Perform in LateUpdate. This can help eliminate jitters in some situations.")]
		public bool lateUpdate;

		// Token: 0x04008D76 RID: 36214
		[Tooltip("Event to send if the direction difference is less than Min Magnitude.")]
		public FsmEvent finishEvent;

		// Token: 0x04008D77 RID: 36215
		[Tooltip("Stop running the action if the direction difference is less than Min Magnitude.")]
		public FsmBool finish;

		// Token: 0x04008D78 RID: 36216
		private GameObject previousGo;

		// Token: 0x04008D79 RID: 36217
		private Quaternion lastRotation;

		// Token: 0x04008D7A RID: 36218
		private Quaternion desiredRotation;
	}
}
