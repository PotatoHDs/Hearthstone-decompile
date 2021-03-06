using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class EventTriggerActionBase : ComponentAction<EventTrigger>
	{
		[DisplayOrder(0)]
		[RequiredField]
		[Tooltip("The GameObject with the UI component.")]
		public FsmOwnerDefault gameObject;

		[DisplayOrder(1)]
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		protected EventTrigger trigger;

		protected EventTrigger.Entry entry;

		public override void Reset()
		{
			gameObject = null;
			eventTarget = FsmEventTarget.Self;
		}

		protected void Init(EventTriggerType eventTriggerType, UnityAction<BaseEventData> call)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCacheAddComponent(ownerDefaultTarget))
			{
				trigger = cachedComponent;
				if (entry == null)
				{
					entry = new EventTrigger.Entry();
				}
				entry.eventID = eventTriggerType;
				entry.callback.AddListener(call);
				trigger.triggers.Add(entry);
			}
		}

		public override void OnExit()
		{
			entry.callback.RemoveAllListeners();
			trigger.triggers.Remove(entry);
		}
	}
}
