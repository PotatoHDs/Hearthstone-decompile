using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F3E RID: 3902
	[ActionCategory("Pegasus")]
	[Tooltip("Sends an Event based on the active state of a GameObject.")]
	public class GetGameObjectIsActive : FsmStateAction
	{
		// Token: 0x0600AC80 RID: 44160 RVA: 0x0035D31E File Offset: 0x0035B51E
		public override void Reset()
		{
			this.gameObject = null;
			this.activeType = GetGameObjectIsActive.ActiveType.Self;
			this.stateIsTrueEvent = null;
			this.stateIsFalseEvent = null;
			this.delay = null;
		}

		// Token: 0x0600AC81 RID: 44161 RVA: 0x0035D344 File Offset: 0x0035B544
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			this.HandleEvent(((this.activeType == GetGameObjectIsActive.ActiveType.Self) ? ownerDefaultTarget.activeSelf : ownerDefaultTarget.activeInHierarchy) ? this.stateIsTrueEvent : this.stateIsFalseEvent);
			base.Finish();
		}

		// Token: 0x0600AC82 RID: 44162 RVA: 0x0035D398 File Offset: 0x0035B598
		private void HandleEvent(FsmEvent fsmEvent)
		{
			if (this.delay.Value < 0.001f)
			{
				base.Fsm.Event(fsmEvent);
				base.Finish();
				return;
			}
			this.m_delayedEvent = base.Fsm.DelayedEvent(fsmEvent, this.delay.Value);
		}

		// Token: 0x0600AC83 RID: 44163 RVA: 0x0035D3E7 File Offset: 0x0035B5E7
		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(this.m_delayedEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x04009355 RID: 37717
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009356 RID: 37718
		[Tooltip("Do we only look at the active state of the GameObject itself, or its parent as well?")]
		public GetGameObjectIsActive.ActiveType activeType;

		// Token: 0x04009357 RID: 37719
		[Space]
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Active state TRUE event.")]
		public FsmEvent stateIsTrueEvent;

		// Token: 0x04009358 RID: 37720
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Active state FALSE event.")]
		public FsmEvent stateIsFalseEvent;

		// Token: 0x04009359 RID: 37721
		[Tooltip("Delay before sending Event.")]
		public FsmFloat delay;

		// Token: 0x0400935A RID: 37722
		private DelayedEvent m_delayedEvent;

		// Token: 0x020027BB RID: 10171
		public enum ActiveType
		{
			// Token: 0x0400F57D RID: 62845
			Self,
			// Token: 0x0400F57E RID: 62846
			Hierarchy
		}
	}
}
