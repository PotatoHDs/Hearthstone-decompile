using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E4B RID: 3659
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when OnSubmit is called on the GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnSubmitEvent : EventTriggerActionBase
	{
		// Token: 0x0600A81E RID: 43038 RVA: 0x0034E917 File Offset: 0x0034CB17
		public override void Reset()
		{
			base.Reset();
			this.onSubmitEvent = null;
		}

		// Token: 0x0600A81F RID: 43039 RVA: 0x0034E926 File Offset: 0x0034CB26
		public override void OnEnter()
		{
			base.Init(EventTriggerType.Submit, new UnityAction<BaseEventData>(this.OnSubmitDelegate));
		}

		// Token: 0x0600A820 RID: 43040 RVA: 0x0034E93C File Offset: 0x0034CB3C
		private void OnSubmitDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			base.SendEvent(this.eventTarget, this.onSubmitEvent);
		}

		// Token: 0x04008E96 RID: 36502
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnSubmitEvent is called")]
		public FsmEvent onSubmitEvent;
	}
}
