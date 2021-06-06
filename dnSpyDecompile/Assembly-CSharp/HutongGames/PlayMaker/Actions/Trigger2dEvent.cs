using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D24 RID: 3364
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Detect 2D trigger collisions between Game Objects that have RigidBody2D/Collider2D components.")]
	public class Trigger2dEvent : FsmStateAction
	{
		// Token: 0x0600A2AC RID: 41644 RVA: 0x0033D341 File Offset: 0x0033B541
		public override void Reset()
		{
			this.gameObject = null;
			this.trigger = Trigger2DType.OnTriggerEnter2D;
			this.collideTag = "";
			this.sendEvent = null;
			this.storeCollider = null;
		}

		// Token: 0x0600A2AD RID: 41645 RVA: 0x0033D370 File Offset: 0x0033B570
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
			switch (this.trigger)
			{
			case Trigger2DType.OnTriggerEnter2D:
				base.Fsm.HandleTriggerEnter2D = true;
				return;
			case Trigger2DType.OnTriggerStay2D:
				base.Fsm.HandleTriggerStay2D = true;
				return;
			case Trigger2DType.OnTriggerExit2D:
				base.Fsm.HandleTriggerExit2D = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A2AE RID: 41646 RVA: 0x0033D3E4 File Offset: 0x0033B5E4
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

		// Token: 0x0600A2AF RID: 41647 RVA: 0x0033D435 File Offset: 0x0033B635
		public override void OnExit()
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				return;
			}
			this.RemoveCallback();
			this.gameObject.GameObject.OnChange -= this.UpdateCallback;
		}

		// Token: 0x0600A2B0 RID: 41648 RVA: 0x0033D467 File Offset: 0x0033B667
		private void UpdateCallback()
		{
			this.RemoveCallback();
			this.GetProxyComponent();
			this.AddCallback();
		}

		// Token: 0x0600A2B1 RID: 41649 RVA: 0x0033D47C File Offset: 0x0033B67C
		private void GetProxyComponent()
		{
			this.cachedProxy = null;
			GameObject value = this.gameObject.GameObject.Value;
			if (value == null)
			{
				return;
			}
			switch (this.trigger)
			{
			case Trigger2DType.OnTriggerEnter2D:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerEnter2D>(value);
				return;
			case Trigger2DType.OnTriggerStay2D:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerStay2D>(value);
				return;
			case Trigger2DType.OnTriggerExit2D:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerExit2D>(value);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A2B2 RID: 41650 RVA: 0x0033D4EC File Offset: 0x0033B6EC
		private void AddCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.trigger)
			{
			case Trigger2DType.OnTriggerEnter2D:
				this.cachedProxy.AddTrigger2DEventCallback(new PlayMakerProxyBase.Trigger2DEvent(this.TriggerEnter2D));
				return;
			case Trigger2DType.OnTriggerStay2D:
				this.cachedProxy.AddTrigger2DEventCallback(new PlayMakerProxyBase.Trigger2DEvent(this.TriggerStay2D));
				return;
			case Trigger2DType.OnTriggerExit2D:
				this.cachedProxy.AddTrigger2DEventCallback(new PlayMakerProxyBase.Trigger2DEvent(this.TriggerExit2D));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A2B3 RID: 41651 RVA: 0x0033D56C File Offset: 0x0033B76C
		private void RemoveCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.trigger)
			{
			case Trigger2DType.OnTriggerEnter2D:
				this.cachedProxy.RemoveTrigger2DEventCallback(new PlayMakerProxyBase.Trigger2DEvent(this.TriggerEnter2D));
				return;
			case Trigger2DType.OnTriggerStay2D:
				this.cachedProxy.RemoveTrigger2DEventCallback(new PlayMakerProxyBase.Trigger2DEvent(this.TriggerStay2D));
				return;
			case Trigger2DType.OnTriggerExit2D:
				this.cachedProxy.RemoveTrigger2DEventCallback(new PlayMakerProxyBase.Trigger2DEvent(this.TriggerExit2D));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A2B4 RID: 41652 RVA: 0x0033D5E9 File Offset: 0x0033B7E9
		private void StoreCollisionInfo(Collider2D collisionInfo)
		{
			this.storeCollider.Value = collisionInfo.gameObject;
		}

		// Token: 0x0600A2B5 RID: 41653 RVA: 0x0033D5FC File Offset: 0x0033B7FC
		public override void DoTriggerEnter2D(Collider2D other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.TriggerEnter2D(other);
			}
		}

		// Token: 0x0600A2B6 RID: 41654 RVA: 0x0033D612 File Offset: 0x0033B812
		public override void DoTriggerStay2D(Collider2D other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.TriggerStay2D(other);
			}
		}

		// Token: 0x0600A2B7 RID: 41655 RVA: 0x0033D628 File Offset: 0x0033B828
		public override void DoTriggerExit2D(Collider2D other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.TriggerExit2D(other);
			}
		}

		// Token: 0x0600A2B8 RID: 41656 RVA: 0x0033D63E File Offset: 0x0033B83E
		private void TriggerEnter2D(Collider2D other)
		{
			if (this.trigger == Trigger2DType.OnTriggerEnter2D && FsmStateAction.TagMatches(this.collideTag, other))
			{
				this.StoreCollisionInfo(other);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A2B9 RID: 41657 RVA: 0x0033D66E File Offset: 0x0033B86E
		private void TriggerStay2D(Collider2D other)
		{
			if (this.trigger == Trigger2DType.OnTriggerStay2D && FsmStateAction.TagMatches(this.collideTag, other))
			{
				this.StoreCollisionInfo(other);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A2BA RID: 41658 RVA: 0x0033D69F File Offset: 0x0033B89F
		private void TriggerExit2D(Collider2D other)
		{
			if (this.trigger == Trigger2DType.OnTriggerExit2D && FsmStateAction.TagMatches(this.collideTag, other))
			{
				this.StoreCollisionInfo(other);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A2BB RID: 41659 RVA: 0x0033D6D0 File Offset: 0x0033B8D0
		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysics2dSetup(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
		}

		// Token: 0x04008920 RID: 35104
		[Tooltip("The GameObject to detect collisions on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008921 RID: 35105
		[Tooltip("The type of trigger event to detect.")]
		public Trigger2DType trigger;

		// Token: 0x04008922 RID: 35106
		[UIHint(UIHint.TagMenu)]
		[Tooltip("Filter by Tag.")]
		public FsmString collideTag;

		// Token: 0x04008923 RID: 35107
		[Tooltip("Event to send if the trigger event is detected.")]
		public FsmEvent sendEvent;

		// Token: 0x04008924 RID: 35108
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		// Token: 0x04008925 RID: 35109
		private PlayMakerProxyBase cachedProxy;
	}
}
