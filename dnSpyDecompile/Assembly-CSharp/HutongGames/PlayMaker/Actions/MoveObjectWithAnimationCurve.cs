using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF4 RID: 3316
	[ActionCategory("Pegasus")]
	[Tooltip("Move a GameObject to another GameObject with an Animation Curve. Works like iTween Move To, but with better performance.")]
	public class MoveObjectWithAnimationCurve : FsmStateAction
	{
		// Token: 0x0600A1B5 RID: 41397 RVA: 0x00338D8C File Offset: 0x00336F8C
		public override void Reset()
		{
			this.elapsedTime = 0f;
			this.worldSpace = true;
			this.destinationObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.destinationLocation = new FsmVector3
			{
				UseVariable = true
			};
			this.animCurveTimeScale = 1f;
		}

		// Token: 0x0600A1B6 RID: 41398 RVA: 0x00338DE4 File Offset: 0x00336FE4
		public override void OnEnter()
		{
			if (this.animationCurve == null)
			{
				base.Finish();
				return;
			}
			if (this.animationCurve.curve.length == 0)
			{
				base.Finish();
				return;
			}
			if (this.animCurveTimeScale.Value <= 0f)
			{
				base.Finish();
				return;
			}
			if (!this.destinationObject.Value)
			{
				this.destinationObject = new FsmGameObject
				{
					UseVariable = true
				};
			}
			this.elapsedTime = 0f;
			this.gameObjectToMove = base.Fsm.GetOwnerDefaultTarget(this.objectToMove);
			this.GetDestinationPosition();
			this.GetSourcePosition();
			this.GetMaxTime();
		}

		// Token: 0x0600A1B7 RID: 41399 RVA: 0x00338E8C File Offset: 0x0033708C
		public override void OnUpdate()
		{
			this.elapsedTime += Time.deltaTime;
			this.currentTime = this.elapsedTime * this.animCurveTimeScale.Value;
			if (this.trackDestination.Value)
			{
				this.GetDestinationPosition();
			}
			this.UpdatePosition();
			if (this.currentTime >= this.maxTime)
			{
				this.DoEvent();
				base.Finish();
				return;
			}
		}

		// Token: 0x0600A1B8 RID: 41400 RVA: 0x00338EF8 File Offset: 0x003370F8
		private void UpdatePosition()
		{
			this.gameObjectToMove = base.Fsm.GetOwnerDefaultTarget(this.objectToMove);
			if (!this.gameObjectToMove)
			{
				base.Finish();
				return;
			}
			Vector3 vector = this.sourcePosition + (this.destinationPosition - this.sourcePosition) * this.animationCurve.curve.Evaluate(this.currentTime);
			if (this.worldSpace.Value)
			{
				this.gameObjectToMove.transform.position = vector;
				return;
			}
			this.gameObjectToMove.transform.localPosition = vector;
		}

		// Token: 0x0600A1B9 RID: 41401 RVA: 0x00338F98 File Offset: 0x00337198
		private void DoEvent()
		{
			if (this.finishEvent != null)
			{
				base.Fsm.Event(this.finishEvent);
			}
		}

		// Token: 0x0600A1BA RID: 41402 RVA: 0x00338FB4 File Offset: 0x003371B4
		private void GetDestinationPosition()
		{
			Vector3 b = this.destinationLocation.IsNone ? Vector3.zero : this.destinationLocation.Value;
			this.destinationPosition = Vector3.zero;
			if (!this.destinationObject.IsNone)
			{
				this.destinationPosition = (this.worldSpace.Value ? this.destinationObject.Value.transform.position : this.destinationObject.Value.transform.localPosition);
			}
			this.destinationPosition += b;
		}

		// Token: 0x0600A1BB RID: 41403 RVA: 0x0033904C File Offset: 0x0033724C
		private void GetSourcePosition()
		{
			this.sourcePosition = Vector3.zero;
			if (this.gameObjectToMove)
			{
				this.sourcePosition = (this.worldSpace.Value ? this.gameObjectToMove.transform.position : this.gameObjectToMove.transform.localPosition);
			}
		}

		// Token: 0x0600A1BC RID: 41404 RVA: 0x003390A8 File Offset: 0x003372A8
		private void GetMaxTime()
		{
			AnimationCurve curve = this.animationCurve.curve;
			this.maxTime = curve[curve.length - 1].time;
		}

		// Token: 0x040087C6 RID: 34758
		[RequiredField]
		public FsmOwnerDefault objectToMove;

		// Token: 0x040087C7 RID: 34759
		[Tooltip("Object to move to.")]
		public FsmGameObject destinationObject;

		// Token: 0x040087C8 RID: 34760
		[Tooltip("Keep track of destination object location every frame, otherwise the location will only be looked up once at the beginning of the action.")]
		public FsmBool trackDestination = false;

		// Token: 0x040087C9 RID: 34761
		[Tooltip("Move to a specific position vector. If Destination Object is defined, this is used as an offset.")]
		public FsmVector3 destinationLocation;

		// Token: 0x040087CA RID: 34762
		[Tooltip("Use worldspace instead of local.")]
		public FsmBool worldSpace = true;

		// Token: 0x040087CB RID: 34763
		[Tooltip("Animation curve to use as easing for movement.")]
		[RequiredField]
		public FsmAnimationCurve animationCurve;

		// Token: 0x040087CC RID: 34764
		[Tooltip("Time scale of animation curve.")]
		public FsmFloat animCurveTimeScale = 1f;

		// Token: 0x040087CD RID: 34765
		[Tooltip("Optionally send an Event when the animation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x040087CE RID: 34766
		private float elapsedTime;

		// Token: 0x040087CF RID: 34767
		private float currentTime;

		// Token: 0x040087D0 RID: 34768
		private float maxTime;

		// Token: 0x040087D1 RID: 34769
		private Vector3 destinationPosition;

		// Token: 0x040087D2 RID: 34770
		private Vector3 sourcePosition;

		// Token: 0x040087D3 RID: 34771
		private GameObject gameObjectToMove;
	}
}
