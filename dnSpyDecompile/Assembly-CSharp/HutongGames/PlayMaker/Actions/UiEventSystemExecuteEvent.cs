using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E37 RID: 3639
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("The eventType will be executed on all components on the GameObject that can handle it.")]
	public class UiEventSystemExecuteEvent : FsmStateAction
	{
		// Token: 0x0600A7CD RID: 42957 RVA: 0x0034DB0B File Offset: 0x0034BD0B
		public override void Reset()
		{
			this.gameObject = null;
			this.eventHandler = UiEventSystemExecuteEvent.EventHandlers.Submit;
			this.success = null;
			this.canNotHandleEvent = null;
		}

		// Token: 0x0600A7CE RID: 42958 RVA: 0x0034DB33 File Offset: 0x0034BD33
		public override void OnEnter()
		{
			base.Fsm.Event(this.ExecuteEvent() ? this.success : this.canNotHandleEvent);
			base.Finish();
		}

		// Token: 0x0600A7CF RID: 42959 RVA: 0x0034DB5C File Offset: 0x0034BD5C
		private bool ExecuteEvent()
		{
			this.go = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (this.go == null)
			{
				base.LogError("Missing GameObject ");
				return false;
			}
			switch ((UiEventSystemExecuteEvent.EventHandlers)this.eventHandler.Value)
			{
			case UiEventSystemExecuteEvent.EventHandlers.Submit:
				if (!ExecuteEvents.CanHandleEvent<ISubmitHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<ISubmitHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.beginDrag:
				if (!ExecuteEvents.CanHandleEvent<IBeginDragHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IBeginDragHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.beginDragHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.cancel:
				if (!ExecuteEvents.CanHandleEvent<ICancelHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<ICancelHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.cancelHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.deselectHandler:
				if (!ExecuteEvents.CanHandleEvent<IDeselectHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IDeselectHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.dragHandler:
				if (!ExecuteEvents.CanHandleEvent<IDragHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IDragHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.dragHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.dropHandler:
				if (!ExecuteEvents.CanHandleEvent<IDropHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IDropHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.dropHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.endDragHandler:
				if (!ExecuteEvents.CanHandleEvent<IEndDragHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IEndDragHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.endDragHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.initializePotentialDrag:
				if (!ExecuteEvents.CanHandleEvent<IInitializePotentialDragHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IInitializePotentialDragHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.initializePotentialDrag);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.pointerClickHandler:
				if (!ExecuteEvents.CanHandleEvent<IPointerClickHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IPointerClickHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.pointerDownHandler:
				if (!ExecuteEvents.CanHandleEvent<IPointerDownHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IPointerDownHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.pointerEnterHandler:
				if (!ExecuteEvents.CanHandleEvent<IPointerEnterHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IPointerEnterHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.pointerExitHandler:
				if (!ExecuteEvents.CanHandleEvent<IPointerExitHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IPointerExitHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.pointerUpHandler:
				if (!ExecuteEvents.CanHandleEvent<IPointerUpHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IPointerUpHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.scrollHandler:
				if (!ExecuteEvents.CanHandleEvent<IScrollHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IScrollHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.scrollHandler);
				break;
			case UiEventSystemExecuteEvent.EventHandlers.updateSelectedHandler:
				if (!ExecuteEvents.CanHandleEvent<IUpdateSelectedHandler>(this.go))
				{
					return false;
				}
				ExecuteEvents.Execute<IUpdateSelectedHandler>(this.go, new BaseEventData(EventSystem.current), ExecuteEvents.updateSelectedHandler);
				break;
			}
			return true;
		}

		// Token: 0x04008E5D RID: 36445
		[RequiredField]
		[Tooltip("The GameObject with  an IEventSystemHandler component (a UI button for example).")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E5E RID: 36446
		[Tooltip("The Type of handler to execute")]
		[ObjectType(typeof(UiEventSystemExecuteEvent.EventHandlers))]
		public FsmEnum eventHandler;

		// Token: 0x04008E5F RID: 36447
		[Tooltip("Event Sent if execution was possible on GameObject")]
		public FsmEvent success;

		// Token: 0x04008E60 RID: 36448
		[Tooltip("Event Sent if execution was NOT possible on GameObject because it can not handle the eventHandler selected")]
		public FsmEvent canNotHandleEvent;

		// Token: 0x04008E61 RID: 36449
		private GameObject go;

		// Token: 0x020027B0 RID: 10160
		public enum EventHandlers
		{
			// Token: 0x0400F53A RID: 62778
			Submit,
			// Token: 0x0400F53B RID: 62779
			beginDrag,
			// Token: 0x0400F53C RID: 62780
			cancel,
			// Token: 0x0400F53D RID: 62781
			deselectHandler,
			// Token: 0x0400F53E RID: 62782
			dragHandler,
			// Token: 0x0400F53F RID: 62783
			dropHandler,
			// Token: 0x0400F540 RID: 62784
			endDragHandler,
			// Token: 0x0400F541 RID: 62785
			initializePotentialDrag,
			// Token: 0x0400F542 RID: 62786
			pointerClickHandler,
			// Token: 0x0400F543 RID: 62787
			pointerDownHandler,
			// Token: 0x0400F544 RID: 62788
			pointerEnterHandler,
			// Token: 0x0400F545 RID: 62789
			pointerExitHandler,
			// Token: 0x0400F546 RID: 62790
			pointerUpHandler,
			// Token: 0x0400F547 RID: 62791
			scrollHandler,
			// Token: 0x0400F548 RID: 62792
			submitHandler,
			// Token: 0x0400F549 RID: 62793
			updateSelectedHandler
		}
	}
}
