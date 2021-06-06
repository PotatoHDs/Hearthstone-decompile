using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E42 RID: 3650
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when Called by the EventSystem when a drag has been found, but before it is valid to begin the drag.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnInitializePotentialDragEvent : EventTriggerActionBase
	{
		// Token: 0x0600A7FA RID: 43002 RVA: 0x0034E6B9 File Offset: 0x0034C8B9
		public override void Reset()
		{
			base.Reset();
			this.onInitializePotentialDragEvent = null;
		}

		// Token: 0x0600A7FB RID: 43003 RVA: 0x0034E6C8 File Offset: 0x0034C8C8
		public override void OnEnter()
		{
			base.Init(EventTriggerType.InitializePotentialDrag, new UnityAction<BaseEventData>(this.OnInitializePotentialDragDelegate));
		}

		// Token: 0x0600A7FC RID: 43004 RVA: 0x0034E6DE File Offset: 0x0034C8DE
		private void OnInitializePotentialDragDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onInitializePotentialDragEvent);
		}

		// Token: 0x04008E8D RID: 36493
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnInitializePotentialDrag is called")]
		public FsmEvent onInitializePotentialDragEvent;
	}
}
