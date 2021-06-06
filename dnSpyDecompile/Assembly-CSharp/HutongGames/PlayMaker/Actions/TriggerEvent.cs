using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E25 RID: 3621
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Detect trigger collisions between GameObjects that have RigidBody/Collider components.")]
	public class TriggerEvent : FsmStateAction
	{
		// Token: 0x0600A767 RID: 42855 RVA: 0x0034CA46 File Offset: 0x0034AC46
		public override void Reset()
		{
			this.gameObject = null;
			this.trigger = TriggerType.OnTriggerEnter;
			this.collideTag = "";
			this.sendEvent = null;
			this.storeCollider = null;
		}

		// Token: 0x0600A768 RID: 42856 RVA: 0x0034CA74 File Offset: 0x0034AC74
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
			case TriggerType.OnTriggerEnter:
				base.Fsm.HandleTriggerEnter = true;
				return;
			case TriggerType.OnTriggerStay:
				base.Fsm.HandleTriggerStay = true;
				return;
			case TriggerType.OnTriggerExit:
				base.Fsm.HandleTriggerExit = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A769 RID: 42857 RVA: 0x0034CAE8 File Offset: 0x0034ACE8
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

		// Token: 0x0600A76A RID: 42858 RVA: 0x0034CB39 File Offset: 0x0034AD39
		public override void OnExit()
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				return;
			}
			this.RemoveCallback();
			this.gameObject.GameObject.OnChange -= this.UpdateCallback;
		}

		// Token: 0x0600A76B RID: 42859 RVA: 0x0034CB6B File Offset: 0x0034AD6B
		private void UpdateCallback()
		{
			this.RemoveCallback();
			this.GetProxyComponent();
			this.AddCallback();
		}

		// Token: 0x0600A76C RID: 42860 RVA: 0x0034CB80 File Offset: 0x0034AD80
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
			case TriggerType.OnTriggerEnter:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerEnter>(value);
				return;
			case TriggerType.OnTriggerStay:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerStay>(value);
				return;
			case TriggerType.OnTriggerExit:
				this.cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerTriggerExit>(value);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A76D RID: 42861 RVA: 0x0034CBF0 File Offset: 0x0034ADF0
		private void AddCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.trigger)
			{
			case TriggerType.OnTriggerEnter:
				this.cachedProxy.AddTriggerEventCallback(new PlayMakerProxyBase.TriggerEvent(this.TriggerEnter));
				return;
			case TriggerType.OnTriggerStay:
				this.cachedProxy.AddTriggerEventCallback(new PlayMakerProxyBase.TriggerEvent(this.TriggerStay));
				return;
			case TriggerType.OnTriggerExit:
				this.cachedProxy.AddTriggerEventCallback(new PlayMakerProxyBase.TriggerEvent(this.TriggerExit));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A76E RID: 42862 RVA: 0x0034CC70 File Offset: 0x0034AE70
		private void RemoveCallback()
		{
			if (this.cachedProxy == null)
			{
				return;
			}
			switch (this.trigger)
			{
			case TriggerType.OnTriggerEnter:
				this.cachedProxy.RemoveTriggerEventCallback(new PlayMakerProxyBase.TriggerEvent(this.TriggerEnter));
				return;
			case TriggerType.OnTriggerStay:
				this.cachedProxy.RemoveTriggerEventCallback(new PlayMakerProxyBase.TriggerEvent(this.TriggerStay));
				return;
			case TriggerType.OnTriggerExit:
				this.cachedProxy.RemoveTriggerEventCallback(new PlayMakerProxyBase.TriggerEvent(this.TriggerExit));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600A76F RID: 42863 RVA: 0x0034CCED File Offset: 0x0034AEED
		private void StoreCollisionInfo(Collider collisionInfo)
		{
			this.storeCollider.Value = collisionInfo.gameObject;
		}

		// Token: 0x0600A770 RID: 42864 RVA: 0x0034CD00 File Offset: 0x0034AF00
		public override void DoTriggerEnter(Collider other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.TriggerEnter(other);
			}
		}

		// Token: 0x0600A771 RID: 42865 RVA: 0x0034CD16 File Offset: 0x0034AF16
		public override void DoTriggerStay(Collider other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.TriggerStay(other);
			}
		}

		// Token: 0x0600A772 RID: 42866 RVA: 0x0034CD2C File Offset: 0x0034AF2C
		public override void DoTriggerExit(Collider other)
		{
			if (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				this.TriggerExit(other);
			}
		}

		// Token: 0x0600A773 RID: 42867 RVA: 0x0034CD42 File Offset: 0x0034AF42
		private void TriggerEnter(Collider other)
		{
			if (this.trigger == TriggerType.OnTriggerEnter && FsmStateAction.TagMatches(this.collideTag, other))
			{
				this.StoreCollisionInfo(other);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A774 RID: 42868 RVA: 0x0034CD72 File Offset: 0x0034AF72
		private void TriggerStay(Collider other)
		{
			if (this.trigger == TriggerType.OnTriggerStay && FsmStateAction.TagMatches(this.collideTag, other))
			{
				this.StoreCollisionInfo(other);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A775 RID: 42869 RVA: 0x0034CDA3 File Offset: 0x0034AFA3
		private void TriggerExit(Collider other)
		{
			if (this.trigger == TriggerType.OnTriggerExit && FsmStateAction.TagMatches(this.collideTag, other))
			{
				this.StoreCollisionInfo(other);
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0600A776 RID: 42870 RVA: 0x0034CDD4 File Offset: 0x0034AFD4
		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysicsSetup(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
		}

		// Token: 0x04008E05 RID: 36357
		[Tooltip("The GameObject to detect trigger events on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E06 RID: 36358
		[Tooltip("The type of trigger event to detect.")]
		public TriggerType trigger;

		// Token: 0x04008E07 RID: 36359
		[UIHint(UIHint.TagMenu)]
		[Tooltip("Filter by Tag.")]
		public FsmString collideTag;

		// Token: 0x04008E08 RID: 36360
		[Tooltip("Event to send if the trigger event is detected.")]
		public FsmEvent sendEvent;

		// Token: 0x04008E09 RID: 36361
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		// Token: 0x04008E0A RID: 36362
		private PlayMakerProxyBase cachedProxy;
	}
}
