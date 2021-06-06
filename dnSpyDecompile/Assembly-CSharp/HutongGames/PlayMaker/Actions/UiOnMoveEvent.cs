using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E43 RID: 3651
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnMoveEvent is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnMoveEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7FE RID: 43006 RVA: 0x0034E6FD File Offset: 0x0034C8FD
		public override void Reset()
		{
			base.Reset();
			this.onMoveEvent = null;
		}

		// Token: 0x0600A7FF RID: 43007 RVA: 0x0034E70C File Offset: 0x0034C90C
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Move, new UnityAction<BaseEventData>(this.OnMoveDelegate));
		}

		// Token: 0x0600A800 RID: 43008 RVA: 0x0034E722 File Offset: 0x0034C922
		private void OnMoveDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onMoveEvent);
		}

		// Token: 0x04008E8E RID: 36494
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnMoveEvent is called")]
		public FsmEvent onMoveEvent;
	}
}
