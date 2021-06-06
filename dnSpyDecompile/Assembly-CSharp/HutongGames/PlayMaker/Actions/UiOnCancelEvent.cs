using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E3D RID: 3645
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnCancel is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnCancelEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7E6 RID: 42982 RVA: 0x0034E566 File Offset: 0x0034C766
		public override void Reset()
		{
			this.gameObject = null;
			this.onCancelEvent = null;
		}

		// Token: 0x0600A7E7 RID: 42983 RVA: 0x0034E576 File Offset: 0x0034C776
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Cancel, new UnityAction<BaseEventData>(this.OnCancelDelegate));
		}

		// Token: 0x0600A7E8 RID: 42984 RVA: 0x0034E58C File Offset: 0x0034C78C
		private void OnCancelDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onCancelEvent);
		}

		// Token: 0x04008E88 RID: 36488
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnCancelEvent is called")]
		public FsmEvent onCancelEvent;
	}
}
