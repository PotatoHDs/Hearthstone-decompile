using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E35 RID: 3637
	public abstract class EventTriggerActionBase : ComponentAction<EventTrigger>
	{
		// Token: 0x0600A7C4 RID: 42948 RVA: 0x0034D8C2 File Offset: 0x0034BAC2
		public override void Reset()
		{
			this.gameObject = null;
			this.eventTarget = FsmEventTarget.Self;
		}

		// Token: 0x0600A7C5 RID: 42949 RVA: 0x0034D8D8 File Offset: 0x0034BAD8
		protected void Init(EventTriggerType eventTriggerType, UnityAction<BaseEventData> call)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCacheAddComponent(ownerDefaultTarget))
			{
				this.trigger = this.cachedComponent;
				if (this.entry == null)
				{
					this.entry = new EventTrigger.Entry();
				}
				this.entry.eventID = eventTriggerType;
				this.entry.callback.AddListener(call);
				this.trigger.triggers.Add(this.entry);
			}
		}

		// Token: 0x0600A7C6 RID: 42950 RVA: 0x0034D952 File Offset: 0x0034BB52
		public override void OnExit()
		{
			this.entry.callback.RemoveAllListeners();
			this.trigger.triggers.Remove(this.entry);
		}

		// Token: 0x04008E52 RID: 36434
		[DisplayOrder(0)]
		[RequiredField]
		[Tooltip("The GameObject with the UI component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E53 RID: 36435
		[DisplayOrder(1)]
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008E54 RID: 36436
		protected EventTrigger trigger;

		// Token: 0x04008E55 RID: 36437
		protected EventTrigger.Entry entry;
	}
}
