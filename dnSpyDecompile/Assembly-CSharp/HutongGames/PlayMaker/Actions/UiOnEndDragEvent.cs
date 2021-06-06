using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E41 RID: 3649
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event Called by the EventSystem once dragging ends.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnEndDragEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7F6 RID: 42998 RVA: 0x0034E675 File Offset: 0x0034C875
		public override void Reset()
		{
			base.Reset();
			this.onEndDragEvent = null;
		}

		// Token: 0x0600A7F7 RID: 42999 RVA: 0x0034E684 File Offset: 0x0034C884
		public override void OnEnter()
		{
			base.Init(EventTriggerType.EndDrag, new UnityAction<BaseEventData>(this.OnEndDragDelegate));
		}

		// Token: 0x0600A7F8 RID: 43000 RVA: 0x0034E69A File Offset: 0x0034C89A
		private void OnEndDragDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onEndDragEvent);
		}

		// Token: 0x04008E8C RID: 36492
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnEndDrag is called")]
		public FsmEvent onEndDragEvent;
	}
}
