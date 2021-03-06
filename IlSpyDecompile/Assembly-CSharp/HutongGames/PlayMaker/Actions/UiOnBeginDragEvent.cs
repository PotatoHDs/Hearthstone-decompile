using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends event when user starts to drag a GameObject.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class UiOnBeginDragEvent : EventTriggerActionBase
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnBeginDrag is called")]
		public FsmEvent onBeginDragEvent;

		public override void Reset()
		{
			base.Reset();
			onBeginDragEvent = null;
		}

		public override void OnEnter()
		{
			Init(EventTriggerType.BeginDrag, OnBeginDragDelegate);
		}

		private void OnBeginDragDelegate(BaseEventData data)
		{
			UiGetLastPointerDataInfo.lastPointerEventData = (PointerEventData)data;
			SendEvent(eventTarget, onBeginDragEvent);
		}
	}
}
