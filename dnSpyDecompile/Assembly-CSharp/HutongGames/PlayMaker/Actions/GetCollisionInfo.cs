using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C4D RID: 3149
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last collision event and store in variables. See Unity Physics docs.")]
	public class GetCollisionInfo : FsmStateAction
	{
		// Token: 0x06009EDC RID: 40668 RVA: 0x0032CA02 File Offset: 0x0032AC02
		public override void Reset()
		{
			this.gameObjectHit = null;
			this.relativeVelocity = null;
			this.relativeSpeed = null;
			this.contactPoint = null;
			this.contactNormal = null;
			this.physicsMaterialName = null;
		}

		// Token: 0x06009EDD RID: 40669 RVA: 0x0032CA30 File Offset: 0x0032AC30
		private void StoreCollisionInfo()
		{
			if (base.Fsm.CollisionInfo == null)
			{
				return;
			}
			this.gameObjectHit.Value = base.Fsm.CollisionInfo.gameObject;
			this.relativeSpeed.Value = base.Fsm.CollisionInfo.relativeVelocity.magnitude;
			this.relativeVelocity.Value = base.Fsm.CollisionInfo.relativeVelocity;
			this.physicsMaterialName.Value = base.Fsm.CollisionInfo.collider.material.name;
			if (base.Fsm.CollisionInfo.contacts != null && base.Fsm.CollisionInfo.contacts.Length != 0)
			{
				this.contactPoint.Value = base.Fsm.CollisionInfo.contacts[0].point;
				this.contactNormal.Value = base.Fsm.CollisionInfo.contacts[0].normal;
			}
		}

		// Token: 0x06009EDE RID: 40670 RVA: 0x0032CB3A File Offset: 0x0032AD3A
		public override void OnEnter()
		{
			this.StoreCollisionInfo();
			base.Finish();
		}

		// Token: 0x04008436 RID: 33846
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;

		// Token: 0x04008437 RID: 33847
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the relative velocity of the collision.")]
		public FsmVector3 relativeVelocity;

		// Token: 0x04008438 RID: 33848
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the relative speed of the collision. Useful for controlling reactions. E.g., selecting an appropriate sound fx.")]
		public FsmFloat relativeSpeed;

		// Token: 0x04008439 RID: 33849
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the collision contact. Useful for spawning effects etc.")]
		public FsmVector3 contactPoint;

		// Token: 0x0400843A RID: 33850
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the collision normal vector. Useful for aligning spawned effects etc.")]
		public FsmVector3 contactNormal;

		// Token: 0x0400843B RID: 33851
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the name of the physics material of the colliding GameObject. Useful for triggering different effects. Audio, particles...")]
		public FsmString physicsMaterialName;
	}
}
