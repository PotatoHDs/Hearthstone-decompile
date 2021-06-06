using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E4C RID: 3660
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when Called by the EventSystem when the object associated with this EventTrigger is updated.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnUpdateSelectedEvent : EventTriggerActionBase
	{
		// Token: 0x0600A822 RID: 43042 RVA: 0x0034E95B File Offset: 0x0034CB5B
		public override void Reset()
		{
			base.Reset();
			this.onUpdateSelectedEvent = null;
		}

		// Token: 0x0600A823 RID: 43043 RVA: 0x0034E96A File Offset: 0x0034CB6A
		public override void OnEnter()
		{
			base.Init(EventTriggerType.UpdateSelected, new UnityAction<BaseEventData>(this.OnUpdateSelectedDelegate));
		}

		// Token: 0x0600A824 RID: 43044 RVA: 0x0034E97F File Offset: 0x0034CB7F
		private void OnUpdateSelectedDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onUpdateSelectedEvent);
		}

		// Token: 0x04008E97 RID: 36503
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnUpdateSelected is called")]
		public FsmEvent onUpdateSelectedEvent;
	}
}
