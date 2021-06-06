using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E40 RID: 3648
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnDrop is called on the GameObject. Warning this event is sent everyframe while dragging.\n Use GetLastPointerDataInfo action to get info from the event.")]
	public class UiOnDropEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7F2 RID: 42994 RVA: 0x0034E632 File Offset: 0x0034C832
		public override void Reset()
		{
			base.Reset();
			this.onDropEvent = null;
		}

		// Token: 0x0600A7F3 RID: 42995 RVA: 0x0034E641 File Offset: 0x0034C841
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Drop, new UnityAction<BaseEventData>(this.OnDropDelegate));
		}

		// Token: 0x0600A7F4 RID: 42996 RVA: 0x0034E656 File Offset: 0x0034C856
		private void OnDropDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onDropEvent);
		}

		// Token: 0x04008E8B RID: 36491
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnDrop is called")]
		public FsmEvent onDropEvent;
	}
}
