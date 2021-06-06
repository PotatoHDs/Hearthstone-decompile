using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E3C RID: 3644
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when user starts to drag a GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnBeginDragEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7E2 RID: 42978 RVA: 0x0034E51A File Offset: 0x0034C71A
		public override void Reset()
		{
			base.Reset();
			this.onBeginDragEvent = null;
		}

		// Token: 0x0600A7E3 RID: 42979 RVA: 0x0034E529 File Offset: 0x0034C729
		public override void OnEnter()
		{
			base.Init(EventTriggerType.BeginDrag, new UnityAction<BaseEventData>(this.OnBeginDragDelegate));
		}

		// Token: 0x0600A7E4 RID: 42980 RVA: 0x0034E53F File Offset: 0x0034C73F
		private void OnBeginDragDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onBeginDragEvent);
		}

		// Token: 0x04008E87 RID: 36487
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnBeginDrag is called")]
		public FsmEvent onBeginDragEvent;
	}
}
