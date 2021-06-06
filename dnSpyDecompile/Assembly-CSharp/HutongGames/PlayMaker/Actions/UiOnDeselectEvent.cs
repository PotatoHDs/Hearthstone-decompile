using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E3E RID: 3646
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnDeselect is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnDeselectEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7EA RID: 42986 RVA: 0x0034E5AB File Offset: 0x0034C7AB
		public override void Reset()
		{
			base.Reset();
			this.onDeselectEvent = null;
		}

		// Token: 0x0600A7EB RID: 42987 RVA: 0x0034E5BA File Offset: 0x0034C7BA
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Deselect, new UnityAction<BaseEventData>(this.OnDeselectDelegate));
		}

		// Token: 0x0600A7EC RID: 42988 RVA: 0x0034E5D0 File Offset: 0x0034C7D0
		private void OnDeselectDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onDeselectEvent);
		}

		// Token: 0x04008E89 RID: 36489
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnDeselectEvent is called")]
		public FsmEvent onDeselectEvent;
	}
}
