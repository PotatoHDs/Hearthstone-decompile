using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E4A RID: 3658
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when Called by the EventSystem when a Select event occurs.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnSelectEvent : EventTriggerActionBase
	{
		// Token: 0x0600A81A RID: 43034 RVA: 0x0034E8D3 File Offset: 0x0034CAD3
		public override void Reset()
		{
			base.Reset();
			this.onSelectEvent = null;
		}

		// Token: 0x0600A81B RID: 43035 RVA: 0x0034E8E2 File Offset: 0x0034CAE2
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Select, new UnityAction<BaseEventData>(this.OnSelectDelegate));
		}

		// Token: 0x0600A81C RID: 43036 RVA: 0x0034E8F8 File Offset: 0x0034CAF8
		private void OnSelectDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onSelectEvent);
		}

		// Token: 0x04008E95 RID: 36501
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnSelect is called")]
		public FsmEvent onSelectEvent;
	}
}
