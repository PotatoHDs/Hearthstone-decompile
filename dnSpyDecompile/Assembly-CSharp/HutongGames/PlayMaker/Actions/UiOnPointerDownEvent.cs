using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E45 RID: 3653
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerDown is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerDownEvent : EventTriggerActionBase
	{
		// Token: 0x0600A806 RID: 43014 RVA: 0x0034E784 File Offset: 0x0034C984
		public override void Reset()
		{
			base.Reset();
			this.onPointerDownEvent = null;
		}

		// Token: 0x0600A807 RID: 43015 RVA: 0x0034E793 File Offset: 0x0034C993
		public override void OnEnter()
		{
			base.Init(EventTriggerType.PointerDown, new UnityAction<BaseEventData>(this.OnPointerDownDelegate));
		}

		// Token: 0x0600A808 RID: 43016 RVA: 0x0034E7A8 File Offset: 0x0034C9A8
		private void OnPointerDownDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onPointerDownEvent);
		}

		// Token: 0x04008E90 RID: 36496
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerDown is called")]
		public FsmEvent onPointerDownEvent;
	}
}
