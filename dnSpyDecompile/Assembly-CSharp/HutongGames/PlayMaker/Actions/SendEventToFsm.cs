using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA0 RID: 3488
	[Obsolete("This action is obsolete; use Send Event with Event Target instead.")]
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event to another Fsm after an optional delay. Specify an Fsm Name or use the first Fsm on the object.")]
	public class SendEventToFsm : FsmStateAction
	{
		// Token: 0x0600A520 RID: 42272 RVA: 0x00345A36 File Offset: 0x00343C36
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = null;
			this.sendEvent = null;
			this.delay = null;
			this.requireReceiver = false;
		}

		// Token: 0x0600A521 RID: 42273 RVA: 0x00345A5C File Offset: 0x00343C5C
		public override void OnEnter()
		{
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				base.Finish();
				return;
			}
			PlayMakerFSM gameObjectFsm = ActionHelpers.GetGameObjectFsm(this.go, this.fsmName.Value);
			if (gameObjectFsm == null)
			{
				if (this.requireReceiver)
				{
					base.LogError("GameObject doesn't have FsmComponent: " + this.go.name + " " + this.fsmName.Value);
				}
				return;
			}
			if ((double)this.delay.Value < 0.001)
			{
				gameObjectFsm.Fsm.Event(this.sendEvent.Value);
				base.Finish();
				return;
			}
			this.delayedEvent = gameObjectFsm.Fsm.DelayedEvent(FsmEvent.GetFsmEvent(this.sendEvent.Value), this.delay.Value);
		}

		// Token: 0x0600A522 RID: 42274 RVA: 0x00345B49 File Offset: 0x00343D49
		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(this.delayedEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x04008BB8 RID: 35768
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BB9 RID: 35769
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008BBA RID: 35770
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		public FsmString sendEvent;

		// Token: 0x04008BBB RID: 35771
		[HasFloatSlider(0f, 10f)]
		public FsmFloat delay;

		// Token: 0x04008BBC RID: 35772
		private bool requireReceiver;

		// Token: 0x04008BBD RID: 35773
		private GameObject go;

		// Token: 0x04008BBE RID: 35774
		private DelayedEvent delayedEvent;
	}
}
