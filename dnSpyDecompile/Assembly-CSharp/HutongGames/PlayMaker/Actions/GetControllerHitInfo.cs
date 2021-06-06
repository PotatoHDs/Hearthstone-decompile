using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C51 RID: 3153
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Gets info on the last Character Controller collision and store in variables.")]
	public class GetControllerHitInfo : FsmStateAction
	{
		// Token: 0x06009EED RID: 40685 RVA: 0x0032CDB5 File Offset: 0x0032AFB5
		public override void Reset()
		{
			this.gameObjectHit = null;
			this.contactPoint = null;
			this.contactNormal = null;
			this.moveDirection = null;
			this.moveLength = null;
			this.physicsMaterialName = null;
		}

		// Token: 0x06009EEE RID: 40686 RVA: 0x0032CDE1 File Offset: 0x0032AFE1
		public override void OnPreprocess()
		{
			base.Fsm.HandleControllerColliderHit = true;
		}

		// Token: 0x06009EEF RID: 40687 RVA: 0x0032CDF0 File Offset: 0x0032AFF0
		private void StoreTriggerInfo()
		{
			if (base.Fsm.ControllerCollider == null)
			{
				return;
			}
			this.gameObjectHit.Value = base.Fsm.ControllerCollider.gameObject;
			this.contactPoint.Value = base.Fsm.ControllerCollider.point;
			this.contactNormal.Value = base.Fsm.ControllerCollider.normal;
			this.moveDirection.Value = base.Fsm.ControllerCollider.moveDirection;
			this.moveLength.Value = base.Fsm.ControllerCollider.moveLength;
			this.physicsMaterialName.Value = base.Fsm.ControllerCollider.collider.material.name;
		}

		// Token: 0x06009EF0 RID: 40688 RVA: 0x0032CEB7 File Offset: 0x0032B0B7
		public override void OnEnter()
		{
			this.StoreTriggerInfo();
			base.Finish();
		}

		// Token: 0x06009EF1 RID: 40689 RVA: 0x0032CEC5 File Offset: 0x0032B0C5
		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysicsSetup(base.Owner);
		}

		// Token: 0x0400844D RID: 33869
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject hit in the last collision.")]
		public FsmGameObject gameObjectHit;

		// Token: 0x0400844E RID: 33870
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the contact point of the last collision in world coordinates.")]
		public FsmVector3 contactPoint;

		// Token: 0x0400844F RID: 33871
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the normal of the last collision.")]
		public FsmVector3 contactNormal;

		// Token: 0x04008450 RID: 33872
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the direction of the last move before the collision.")]
		public FsmVector3 moveDirection;

		// Token: 0x04008451 RID: 33873
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance of the last move before the collision.")]
		public FsmFloat moveLength;

		// Token: 0x04008452 RID: 33874
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the physics material of the Game Object Hit. Useful for triggering different effects. Audio, particles...")]
		public FsmString physicsMaterialName;
	}
}
