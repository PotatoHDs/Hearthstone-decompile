using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E3F RID: 3647
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnDrag is called on the GameObject. Warning this event is sent every frame while dragging.\n Use GetLastPointerDataInfo action to get info from the event.")]
	public class UiOnDragEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7EE RID: 42990 RVA: 0x0034E5EF File Offset: 0x0034C7EF
		public override void Reset()
		{
			base.Reset();
			this.onDragEvent = null;
		}

		// Token: 0x0600A7EF RID: 42991 RVA: 0x0034E5FE File Offset: 0x0034C7FE
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Drag, new UnityAction<BaseEventData>(this.OnDragDelegate));
		}

		// Token: 0x0600A7F0 RID: 42992 RVA: 0x0034E613 File Offset: 0x0034C813
		private void OnDragDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onDragEvent);
		}

		// Token: 0x04008E8A RID: 36490
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnDrag is called")]
		public FsmEvent onDragEvent;
	}
}
