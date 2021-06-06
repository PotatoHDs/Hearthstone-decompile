using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E48 RID: 3656
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerUp is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerUpEvent : EventTriggerActionBase
	{
		// Token: 0x0600A812 RID: 43026 RVA: 0x0034E84D File Offset: 0x0034CA4D
		public override void Reset()
		{
			base.Reset();
			this.onPointerUpEvent = null;
		}

		// Token: 0x0600A813 RID: 43027 RVA: 0x0034E85C File Offset: 0x0034CA5C
		public override void OnEnter()
		{
			base.Init(EventTriggerType.PointerUp, new UnityAction<BaseEventData>(this.OnPointerUpDelegate));
		}

		// Token: 0x0600A814 RID: 43028 RVA: 0x0034E871 File Offset: 0x0034CA71
		private void OnPointerUpDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onPointerUpEvent);
		}

		// Token: 0x04008E93 RID: 36499
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerUp is called")]
		public FsmEvent onPointerUpEvent;
	}
}
