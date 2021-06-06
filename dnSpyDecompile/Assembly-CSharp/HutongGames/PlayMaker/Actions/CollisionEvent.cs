using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BEB RID: 3051
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Detect collisions between Game Objects that have RigidBody/Collider components.")]
	public class CollisionEvent : FsmStateAction
	{
		// Token: 0x06009D24 RID: 40228 RVA: 0x00327FDF File Offset: 0x003261DF
		public override void Reset()
		{
			this.gameObject = null;
			this.collision = CollisionType.OnCollisionEnter;
			this.collideTag = "";
			this.sendEvent = null;
			this.storeCollider = null;
			this.storeForce = null;
		}

		// Token: 0x06009D25 RID: 40229 RVA: 0x00328014 File Offset: 0x00326214
		public override void OnPreprocess()
		{
			if (this.gameObject == null)
			{
				this.gameObject = new FsmOwnerDefault();
			}
			if (this.gameObject.OwnerOption != OwnerDefaultOption.UseOwner)
			{
				this.GetProxyComponent();
				return;
			}
			switch (this.collision)
			{
			case CollisionType.OnCollisionEnter:
				base.Fsm.HandleCollisionEnter = true;
				return;
			case CollisionType.OnCollisionStay:
				base.Fsm.HandleCollisionStay = true;
				return;
			case CollisionType.OnCollisionExit:
				base.Fsm.HandleCollisionExit = true;
				return;
			case CollisionType.OnControllerColliderHit:
				base.Fsm.HandleControllerColliderHit = true;
				return;
			case CollisionType.OnParticleCollision:
				base.Fsm.HandleParticleCollision = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x06009D26 RID: 40230 RVA: 0x003280AC File Offset: 0x003262AC
		public override void OnEnter()
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				return;
			}
			if (this.cachedProxy == null)
			{
				this.GetProxyComponent();
			}
			this.AddCallback();
			this.gameObject.GameObject.OnChange += this.UpdateCallback;
		}

		// Token: 0x06009D27 RID: 40231 RVA: 0x003280FD File Offset: 0x003262FD
		public override void OnExit()
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				return;
			}
			this.RemoveCallback();
			this.gameObject.GameObject.OnChange -= this.UpdateCallback;
		}

		// Token: 0x06009D28 RID: 40232 RVA: 0x0032812F File Offset: 0x0032632F
		private void UpdateCallback()
		{
			this.RemoveCallback();
			this.GetProxyComponent();
			this.AddCallback();
		}

		// Token: 0x06009D29 RID: 40233 RVA: 0x00328144 File Offset: 0x00326344
		private void GetProxyComponent()
		{
			this.cachedProxy = null;
			GameObject value = this.gameObject.GameObject.Value;
			if (value == null)
			{
				return;
			}
			switch (this.collision)
			{
			case CollisionType.OnCollisionEnter:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionEnter>(value);
				return;
			case CollisionType.OnCollisionStay:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionStay>(value);
				return;
			case CollisionType.OnCollisionExit:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionExit>(value);
				return;
			case CollisionType.OnControllerColliderHit:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerControllerColliderHit>(value);
				return;
			case CollisionType.OnParticleCollision:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerParticleCollision>(value);
				return;
			default:
				return;
			}
		}

		// Token: 0x06009D2A RID: 40234 RVA: 0x003281D8 File Offset: 0x003263D8
		private void AddCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.collision)
			{
			case CollisionType.OnCollisionEnter:
				this.cachedProxy.AddCollisionEventCallback(new PlayMakerProxyBase.CollisionEvent(this.CollisionEnter));
				return;
			case CollisionType.OnCollisionStay:
				this.cachedProxy.AddCollisionEventCallback(new PlayMakerProxyBase.CollisionEvent(this.CollisionStay));
				return;
			case CollisionType.OnCollisionExit:
				this.cachedProxy.AddCollisionEventCallback(new PlayMakerProxyBase.CollisionEvent(this.CollisionExit));
				return;
			case CollisionType.OnControllerColliderHit:
				this.cachedProxy.AddControllerCollisionEventCallback(new PlayMakerProxyBase.ControllerCollisionEvent(this.ControllerColliderHit));
				return;
			case CollisionType.OnParticleCollision:
				this.cachedProxy.AddParticleCollisionEventCallback(new PlayMakerProxyBase.ParticleCollisionEvent(this.ParticleCollision));
				return;
			default:
				return;
			}
		}

		// Token: 0x06009D2B RID: 40235 RVA: 0x00328290 File Offset: 0x00326490
		private void RemoveCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.collision)
			{
			case CollisionType.OnCollisionEnter:
				this.cachedProxy.RemoveCollisionEventCallback(new PlayMakerProxyBase.CollisionEvent(this.CollisionEnter));
				return;
			case CollisionType.OnCollisionStay:
				this.cachedProxy.RemoveCollisionEventCallback(new PlayMakerProxyBase.CollisionEvent(this.CollisionStay));
				return;
			case CollisionType.OnCollisionExit:
				this.cachedProxy.RemoveCollisionEventCallback(new PlayMakerProxyBase.CollisionEvent(this.CollisionExit));
				return;
			case CollisionType.OnControllerColliderHit:
				this.cachedProxy.RemoveControllerCollisionEventCallback(new PlayMakerProxyBase.ControllerCollisionEvent(this.ControllerColliderHit));
				return;
			case CollisionType.OnParticleCollision:
				this.cachedProxy.RemoveParticleCollisionEventCallback(new PlayMakerProxyBase.ParticleCollisionEvent(this.ParticleCollision));
				return;
			default:
				return;
			}
		}

		// Token: 0x06009D2C RID: 40236 RVA: 0x00328348 File Offset: 0x00326548
		private void StoreCollisionInfo(Collision collisionInfo)
		{
			this.storeCollider.Value = collisionInfo.gameObject;
			this.storeForce.Value = collisionInfo.relativeVelocity.magnitude;
		}

		// Token: 0x06009D2D RID: 40237 RVA: 0x0032837F File Offset: 0x0032657F
		public override void DoCollisionEnter(Collision collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.CollisionEnter(collisionInfo);
			}
		}

		// Token: 0x06009D2E RID: 40238 RVA: 0x00328395 File Offset: 0x00326595
		public override void DoCollisionStay(Collision collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.CollisionStay(collisionInfo);
			}
		}

		// Token: 0x06009D2F RID: 40239 RVA: 0x003283AB File Offset: 0x003265AB
		public override void DoCollisionExit(Collision collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.CollisionExit(collisionInfo);
			}
		}

		// Token: 0x06009D30 RID: 40240 RVA: 0x003283C1 File Offset: 0x003265C1
		public override void DoControllerColliderHit(ControllerColliderHit collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.ControllerColliderHit(collisionInfo);
			}
		}

		// Token: 0x06009D31 RID: 40241 RVA: 0x003283D7 File Offset: 0x003265D7
		public override void DoParticleCollision(GameObject other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.ParticleCollision(other);
			}
		}

		// Token: 0x06009D32 RID: 40242 RVA: 0x003283ED File Offset: 0x003265ED
		private void CollisionEnter(Collision collisionInfo)
		{
			if (this.collision == CollisionType.OnCollisionEnter && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				this.StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x06009D33 RID: 40243 RVA: 0x0032841D File Offset: 0x0032661D
		private void CollisionStay(Collision collisionInfo)
		{
			if (this.collision == CollisionType.OnCollisionStay && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				this.StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x06009D34 RID: 40244 RVA: 0x0032844E File Offset: 0x0032664E
		private void CollisionExit(Collision collisionInfo)
		{
			if (this.collision == CollisionType.OnCollisionExit && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				this.StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x06009D35 RID: 40245 RVA: 0x00328480 File Offset: 0x00326680
		private void ControllerColliderHit(ControllerColliderHit collisionInfo)
		{
			if (this.collision == CollisionType.OnControllerColliderHit && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				if (this.storeCollider != null)
				{
					this.storeCollider.Value = collisionInfo.gameObject;
				}
				this.storeForce.Value = 0f;
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x06009D36 RID: 40246 RVA: 0x003284E0 File Offset: 0x003266E0
		private void ParticleCollision(GameObject other)
		{
			if (this.collision == CollisionType.OnParticleCollision && FsmStateAction.TagMatches(this.collideTag, other))
			{
				if (this.storeCollider != null)
				{
					this.storeCollider.Value = other;
				}
				this.storeForce.Value = 0f;
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x06009D37 RID: 40247 RVA: 0x00328539 File Offset: 0x00326739
		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysicsSetup(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
		}

		// Token: 0x040082A5 RID: 33445
		[Tooltip("The GameObject to detect collisions on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040082A6 RID: 33446
		[Tooltip("The type of collision to detect.")]
		public CollisionType collision;

		// Token: 0x040082A7 RID: 33447
		[UIHint(UIHint.TagMenu)]
		[Tooltip("Filter by Tag.")]
		public FsmString collideTag;

		// Token: 0x040082A8 RID: 33448
		[Tooltip("Event to send if a collision is detected.")]
		public FsmEvent sendEvent;

		// Token: 0x040082A9 RID: 33449
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		// Token: 0x040082AA RID: 33450
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the force of the collision. NOTE: Use Get Collision Info to get more info about the collision.")]
		public FsmFloat storeForce;

		// Token: 0x040082AB RID: 33451
		private PlayMakerProxyBase cachedProxy;
	}
}
