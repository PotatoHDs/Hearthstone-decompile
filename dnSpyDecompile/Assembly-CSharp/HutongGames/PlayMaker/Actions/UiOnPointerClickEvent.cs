using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E44 RID: 3652
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerClick is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerClickEvent : EventTriggerActionBase
	{
		// Token: 0x0600A802 RID: 43010 RVA: 0x0034E741 File Offset: 0x0034C941
		public override void Reset()
		{
			base.Reset();
			this.onPointerClickEvent = null;
		}

		// Token: 0x0600A803 RID: 43011 RVA: 0x0034E750 File Offset: 0x0034C950
		public override void OnEnter()
		{
			base.Init(EventTriggerType.PointerClick, new UnityAction<BaseEventData>(this.OnPointerClickDelegate));
		}

		// Token: 0x0600A804 RID: 43012 RVA: 0x0034E765 File Offset: 0x0034C965
		private void OnPointerClickDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onPointerClickEvent);
		}

		// Token: 0x04008E8F RID: 36495
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerClick is called")]
		public FsmEvent onPointerClickEvent;
	}
}
