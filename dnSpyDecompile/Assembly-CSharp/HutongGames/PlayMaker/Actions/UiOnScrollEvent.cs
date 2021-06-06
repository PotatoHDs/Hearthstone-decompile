using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E49 RID: 3657
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnScroll is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnScrollEvent : EventTriggerActionBase
	{
		// Token: 0x0600A816 RID: 43030 RVA: 0x0034E890 File Offset: 0x0034CA90
		public override void Reset()
		{
			base.Reset();
			this.onScrollEvent = null;
		}

		// Token: 0x0600A817 RID: 43031 RVA: 0x0034E89F File Offset: 0x0034CA9F
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Scroll, new UnityAction<BaseEventData>(this.OnScrollDelegate));
		}

		// Token: 0x0600A818 RID: 43032 RVA: 0x0034E8B4 File Offset: 0x0034CAB4
		private void OnScrollDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onScrollEvent);
		}

		// Token: 0x04008E94 RID: 36500
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnScroll is called")]
		public FsmEvent onScrollEvent;
	}
}
