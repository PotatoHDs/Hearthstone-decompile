using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF5 RID: 3317
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Moves a Game Object towards a Target. Optionally sends an event when successful. The Target can be specified as a Game Object or a world Position. If you specify both, then the Position is used as a local offset from the Object's Position.")]
	public class MoveTowards : FsmStateAction
	{
		// Token: 0x0600A1BE RID: 41406 RVA: 0x0033910D File Offset: 0x0033730D
		public override void Reset()
		{
			this.gameObject = null;
			this.targetObject = null;
			this.maxSpeed = 10f;
			this.finishDistance = 1f;
			this.finishEvent = null;
		}

		// Token: 0x0600A1BF RID: 41407 RVA: 0x00339144 File Offset: 0x00337344
		public override void OnUpdate()
		{
			this.DoMoveTowards();
		}

		// Token: 0x0600A1C0 RID: 41408 RVA: 0x0033914C File Offset: 0x0033734C
		private void DoMoveTowards()
		{
			if (!this.UpdateTargetPos())
			{
				return;
			}
			this.go.transform.position = Vector3.MoveTowards(this.go.transform.position, this.targetPos, this.maxSpeed.Value * Time.deltaTime);
			if ((this.go.transform.position - this.targetPos).magnitude < this.finishDistance.Value)
			{
				base.Fsm.Event(this.finishEvent);
				base.Finish();
			}
		}

		// Token: 0x0600A1C1 RID: 41409 RVA: 0x003391E8 File Offset: 0x003373E8
		public bool UpdateTargetPos()
		{
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				return false;
			}
			this.goTarget = this.targetObject.Value;
			if (this.goTarget == null && this.targetPosition.IsNone)
			{
				return false;
			}
			if (this.goTarget != null)
			{
				this.targetPos = ((!this.targetPosition.IsNone) ? this.goTarget.transform.TransformPoint(this.targetPosition.Value) : this.goTarget.transform.position);
			}
			else
			{
				this.targetPos = this.targetPosition.Value;
			}
			this.targetPosWithVertical = this.targetPos;
			if (this.ignoreVertical.Value)
			{
				this.targetPos.y = this.go.transform.position.y;
			}
			return true;
		}

		// Token: 0x0600A1C2 RID: 41410 RVA: 0x003392E5 File Offset: 0x003374E5
		public Vector3 GetTargetPos()
		{
			return this.targetPos;
		}

		// Token: 0x0600A1C3 RID: 41411 RVA: 0x003392ED File Offset: 0x003374ED
		public Vector3 GetTargetPosWithVertical()
		{
			return this.targetPosWithVertical;
		}

		// Token: 0x040087D4 RID: 34772
		[RequiredField]
		[Tooltip("The GameObject to Move")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040087D5 RID: 34773
		[Tooltip("A target GameObject to move towards. Or use a world Target Position below.")]
		public FsmGameObject targetObject;

		// Token: 0x040087D6 RID: 34774
		[Tooltip("A world position if no Target Object. Otherwise used as a local offset from the Target Object.")]
		public FsmVector3 targetPosition;

		// Token: 0x040087D7 RID: 34775
		[Tooltip("Ignore any height difference in the target.")]
		public FsmBool ignoreVertical;

		// Token: 0x040087D8 RID: 34776
		[HasFloatSlider(0f, 20f)]
		[Tooltip("The maximum movement speed. HINT: You can make this a variable to change it over time.")]
		public FsmFloat maxSpeed;

		// Token: 0x040087D9 RID: 34777
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent.")]
		public FsmFloat finishDistance;

		// Token: 0x040087DA RID: 34778
		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;

		// Token: 0x040087DB RID: 34779
		private GameObject go;

		// Token: 0x040087DC RID: 34780
		private GameObject goTarget;

		// Token: 0x040087DD RID: 34781
		private Vector3 targetPos;

		// Token: 0x040087DE RID: 34782
		private Vector3 targetPosWithVertical;
	}
}
