using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E46 RID: 3654
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerEnter is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerEnterEvent : EventTriggerActionBase
	{
		// Token: 0x0600A80A RID: 43018 RVA: 0x0034E7C7 File Offset: 0x0034C9C7
		public override void Reset()
		{
			base.Reset();
			this.onPointerEnterEvent = null;
		}

		// Token: 0x0600A80B RID: 43019 RVA: 0x0034E7D6 File Offset: 0x0034C9D6
		public override void OnEnter()
		{
			base.Init(EventTriggerType.PointerEnter, new UnityAction<BaseEventData>(this.OnPointerEnterDelegate));
		}

		// Token: 0x0600A80C RID: 43020 RVA: 0x0034E7EB File Offset: 0x0034C9EB
		private void OnPointerEnterDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onPointerEnterEvent);
		}

		// Token: 0x04008E91 RID: 36497
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerEnter is called")]
		public FsmEvent onPointerEnterEvent;
	}
}
