using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E47 RID: 3655
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnPointerExit is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnPointerExitEvent : EventTriggerActionBase
	{
		// Token: 0x0600A80E RID: 43022 RVA: 0x0034E80A File Offset: 0x0034CA0A
		public override void Reset()
		{
			base.Reset();
			this.onPointerExitEvent = null;
		}

		// Token: 0x0600A80F RID: 43023 RVA: 0x0034E819 File Offset: 0x0034CA19
		public override void OnEnter()
		{
			base.Init(EventTriggerType.PointerExit, new UnityAction<BaseEventData>(this.OnPointerExitDelegate));
		}

		// Token: 0x0600A810 RID: 43024 RVA: 0x0034E82E File Offset: 0x0034CA2E
		private void OnPointerExitDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onPointerExitEvent);
		}

		// Token: 0x04008E92 RID: 36498
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when PointerExit is called")]
		public FsmEvent onPointerExitEvent;
	}
}
