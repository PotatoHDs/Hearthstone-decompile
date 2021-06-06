using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D01 RID: 3329
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Detect collisions between Game Objects that have RigidBody2D/Collider2D components.")]
	public class Collision2dEvent : FsmStateAction
	{
		// Token: 0x0600A1F7 RID: 41463 RVA: 0x003399F5 File Offset: 0x00337BF5
		public override void Reset()
		{
			this.collision = Collision2DType.OnCollisionEnter2D;
			this.collideTag = "";
			this.sendEvent = null;
			this.storeCollider = null;
			this.storeForce = null;
		}

		// Token: 0x0600A1F8 RID: 41464 RVA: 0x00339A24 File Offset: 0x00337C24
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
			case Collision2DType.OnCollisionEnter2D:
				base.Fsm.HandleCollisionEnter2D = true;
				return;
			case Collision2DType.OnCollisionStay2D:
				base.Fsm.HandleCollisionStay2D = true;
				return;
			case Collision2DType.OnCollisionExit2D:
				base.Fsm.HandleCollisionExit2D = true;
				return;
			case Collision2DType.OnParticleCollision:
				base.Fsm.HandleParticleCollision = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A1F9 RID: 41465 RVA: 0x00339AAC File Offset: 0x00337CAC
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

		// Token: 0x0600A1FA RID: 41466 RVA: 0x00339AFD File Offset: 0x00337CFD
		public override void OnExit()
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				return;
			}
			this.RemoveCallback();
			this.gameObject.GameObject.OnChange -= this.UpdateCallback;
		}

		// Token: 0x0600A1FB RID: 41467 RVA: 0x00339B2F File Offset: 0x00337D2F
		private void UpdateCallback()
		{
			this.RemoveCallback();
			this.GetProxyComponent();
			this.AddCallback();
		}

		// Token: 0x0600A1FC RID: 41468 RVA: 0x00339B44 File Offset: 0x00337D44
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
			case Collision2DType.OnCollisionEnter2D:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionEnter2D>(value);
				return;
			case Collision2DType.OnCollisionStay2D:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionStay2D>(value);
				return;
			case Collision2DType.OnCollisionExit2D:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionExit2D>(value);
				return;
			case Collision2DType.OnParticleCollision:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerParticleCollision>(value);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A1FD RID: 41469 RVA: 0x00339BC4 File Offset: 0x00337DC4
		private void AddCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.collision)
			{
			case Collision2DType.OnCollisionEnter2D:
				this.cachedProxy.AddCollision2DEventCallback(new PlayMakerProxyBase.Collision2DEvent(this.CollisionEnter2D));
				return;
			case Collision2DType.OnCollisionStay2D:
				this.cachedProxy.AddCollision2DEventCallback(new PlayMakerProxyBase.Collision2DEvent(this.CollisionStay2D));
				return;
			case Collision2DType.OnCollisionExit2D:
				this.cachedProxy.AddCollision2DEventCallback(new PlayMakerProxyBase.Collision2DEvent(this.CollisionExit2D));
				return;
			case Collision2DType.OnParticleCollision:
				this.cachedProxy.AddParticleCollisionEventCallback(new PlayMakerProxyBase.ParticleCollisionEvent(this.ParticleCollision));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A1FE RID: 41470 RVA: 0x00339C60 File Offset: 0x00337E60
		private void RemoveCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.collision)
			{
			case Collision2DType.OnCollisionEnter2D:
				this.cachedProxy.RemoveCollision2DEventCallback(new PlayMakerProxyBase.Collision2DEvent(this.CollisionEnter2D));
				return;
			case Collision2DType.OnCollisionStay2D:
				this.cachedProxy.RemoveCollision2DEventCallback(new PlayMakerProxyBase.Collision2DEvent(this.CollisionStay2D));
				return;
			case Collision2DType.OnCollisionExit2D:
				this.cachedProxy.RemoveCollision2DEventCallback(new PlayMakerProxyBase.Collision2DEvent(this.CollisionExit2D));
				return;
			case Collision2DType.OnParticleCollision:
				this.cachedProxy.RemoveParticleCollisionEventCallback(new PlayMakerProxyBase.ParticleCollisionEvent(this.ParticleCollision));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A1FF RID: 41471 RVA: 0x00339CFC File Offset: 0x00337EFC
		private void StoreCollisionInfo(Collision2D collisionInfo)
		{
			this.storeCollider.Value = collisionInfo.gameObject;
			this.storeForce.Value = collisionInfo.relativeVelocity.magnitude;
		}

		// Token: 0x0600A200 RID: 41472 RVA: 0x00339D33 File Offset: 0x00337F33
		public override void DoCollisionEnter2D(Collision2D collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.CollisionEnter2D(collisionInfo);
			}
		}

		// Token: 0x0600A201 RID: 41473 RVA: 0x00339D49 File Offset: 0x00337F49
		public override void DoCollisionStay2D(Collision2D collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.CollisionStay2D(collisionInfo);
			}
		}

		// Token: 0x0600A202 RID: 41474 RVA: 0x00339D5F File Offset: 0x00337F5F
		public override void DoCollisionExit2D(Collision2D collisionInfo)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.CollisionExit2D(collisionInfo);
			}
		}

		// Token: 0x0600A203 RID: 41475 RVA: 0x00339D75 File Offset: 0x00337F75
		public override void DoParticleCollision(GameObject other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.ParticleCollision(other);
			}
		}

		// Token: 0x0600A204 RID: 41476 RVA: 0x00339D8B File Offset: 0x00337F8B
		private void CollisionEnter2D(Collision2D collisionInfo)
		{
			if (this.collision == Collision2DType.OnCollisionEnter2D && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				this.StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A205 RID: 41477 RVA: 0x00339DBB File Offset: 0x00337FBB
		private void CollisionStay2D(Collision2D collisionInfo)
		{
			if (this.collision == Collision2DType.OnCollisionStay2D && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				this.StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A206 RID: 41478 RVA: 0x00339DEC File Offset: 0x00337FEC
		private void CollisionExit2D(Collision2D collisionInfo)
		{
			if (this.collision == Collision2DType.OnCollisionExit2D && FsmStateAction.TagMatches(this.collideTag, collisionInfo))
			{
				this.StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A207 RID: 41479 RVA: 0x00339E20 File Offset: 0x00338020
		private void ParticleCollision(GameObject other)
		{
			if (this.collision == Collision2DType.OnParticleCollision && FsmStateAction.TagMatches(this.collideTag, other))
			{
				if (this.storeCollider != null)
				{
					this.storeCollider.Value = other;
				}
				this.storeForce.Value = 0f;
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A208 RID: 41480 RVA: 0x00339E79 File Offset: 0x00338079
		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysics2dSetup(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
		}

		// Token: 0x04008806 RID: 34822
		[Tooltip("The GameObject to detect collisions on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008807 RID: 34823
		[Tooltip("The type of collision to detect.")]
		public Collision2DType collision;

		// Token: 0x04008808 RID: 34824
		[UIHint(UIHint.TagMenu)]
		[Tooltip("Filter by Tag.")]
		public FsmString collideTag;

		// Token: 0x04008809 RID: 34825
		[Tooltip("Event to send if a collision is detected.")]
		public FsmEvent sendEvent;

		// Token: 0x0400880A RID: 34826
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		// Token: 0x0400880B RID: 34827
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the force of the collision. NOTE: Use Get Collision 2D Info to get more info about the collision.")]
		public FsmFloat storeForce;

		// Token: 0x0400880C RID: 34828
		private PlayMakerProxyBase cachedProxy;
	}
}
