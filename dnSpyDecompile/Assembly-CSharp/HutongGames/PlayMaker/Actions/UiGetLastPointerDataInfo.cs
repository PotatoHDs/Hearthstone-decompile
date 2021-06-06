using System;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E38 RID: 3640
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets pointer data on the last System event.")]
	public class UiGetLastPointerDataInfo : FsmStateAction
	{
		// Token: 0x0600A7D1 RID: 42961 RVA: 0x0034DEB0 File Offset: 0x0034C0B0
		public override void Reset()
		{
			this.clickCount = null;
			this.clickTime = null;
			this.delta = null;
			this.dragging = null;
			this.inputButton = PointerEventData.InputButton.Left;
			this.eligibleForClick = null;
			this.enterEventCamera = null;
			this.pressEventCamera = null;
			this.isPointerMoving = null;
			this.isScrolling = null;
			this.lastPress = null;
			this.pointerDrag = null;
			this.pointerEnter = null;
			this.pointerId = null;
			this.pointerPress = null;
			this.position = null;
			this.pressPosition = null;
			this.rawPointerPress = null;
			this.scrollDelta = null;
			this.used = null;
			this.useDragThreshold = null;
			this.worldNormal = null;
			this.worldPosition = null;
		}

		// Token: 0x0600A7D2 RID: 42962 RVA: 0x0034DF68 File Offset: 0x0034C168
		public override void OnEnter()
		{
			if (UiGetLastPointerDataInfo.lastPointerEventData == null)
			{
				base.Finish();
				return;
			}
			if (!this.clickCount.IsNone)
			{
				this.clickCount.Value = UiGetLastPointerDataInfo.lastPointerEventData.clickCount;
			}
			if (!this.clickTime.IsNone)
			{
				this.clickTime.Value = UiGetLastPointerDataInfo.lastPointerEventData.clickTime;
			}
			if (!this.delta.IsNone)
			{
				this.delta.Value = UiGetLastPointerDataInfo.lastPointerEventData.delta;
			}
			if (!this.dragging.IsNone)
			{
				this.dragging.Value = UiGetLastPointerDataInfo.lastPointerEventData.dragging;
			}
			if (!this.inputButton.IsNone)
			{
				this.inputButton.Value = UiGetLastPointerDataInfo.lastPointerEventData.button;
			}
			if (!this.eligibleForClick.IsNone)
			{
				this.eligibleForClick.Value = UiGetLastPointerDataInfo.lastPointerEventData.eligibleForClick;
			}
			if (!this.enterEventCamera.IsNone)
			{
				this.enterEventCamera.Value = UiGetLastPointerDataInfo.lastPointerEventData.enterEventCamera.gameObject;
			}
			if (!this.isPointerMoving.IsNone)
			{
				this.isPointerMoving.Value = UiGetLastPointerDataInfo.lastPointerEventData.IsPointerMoving();
			}
			if (!this.isScrolling.IsNone)
			{
				this.isScrolling.Value = UiGetLastPointerDataInfo.lastPointerEventData.IsScrolling();
			}
			if (!this.lastPress.IsNone)
			{
				this.lastPress.Value = UiGetLastPointerDataInfo.lastPointerEventData.lastPress;
			}
			if (!this.pointerDrag.IsNone)
			{
				this.pointerDrag.Value = UiGetLastPointerDataInfo.lastPointerEventData.pointerDrag;
			}
			if (!this.pointerEnter.IsNone)
			{
				this.pointerEnter.Value = UiGetLastPointerDataInfo.lastPointerEventData.pointerEnter;
			}
			if (!this.pointerId.IsNone)
			{
				this.pointerId.Value = UiGetLastPointerDataInfo.lastPointerEventData.pointerId;
			}
			if (!this.pointerPress.IsNone)
			{
				this.pointerPress.Value = UiGetLastPointerDataInfo.lastPointerEventData.pointerPress;
			}
			if (!this.position.IsNone)
			{
				this.position.Value = UiGetLastPointerDataInfo.lastPointerEventData.position;
			}
			if (!this.pressEventCamera.IsNone)
			{
				this.pressEventCamera.Value = UiGetLastPointerDataInfo.lastPointerEventData.pressEventCamera.gameObject;
			}
			if (!this.pressPosition.IsNone)
			{
				this.pressPosition.Value = UiGetLastPointerDataInfo.lastPointerEventData.pressPosition;
			}
			if (!this.rawPointerPress.IsNone)
			{
				this.rawPointerPress.Value = UiGetLastPointerDataInfo.lastPointerEventData.rawPointerPress;
			}
			if (!this.scrollDelta.IsNone)
			{
				this.scrollDelta.Value = UiGetLastPointerDataInfo.lastPointerEventData.scrollDelta;
			}
			if (!this.used.IsNone)
			{
				this.used.Value = UiGetLastPointerDataInfo.lastPointerEventData.used;
			}
			if (!this.useDragThreshold.IsNone)
			{
				this.useDragThreshold.Value = UiGetLastPointerDataInfo.lastPointerEventData.useDragThreshold;
			}
			if (!this.worldNormal.IsNone)
			{
				this.worldNormal.Value = UiGetLastPointerDataInfo.lastPointerEventData.pointerCurrentRaycast.worldNormal;
			}
			if (!this.worldPosition.IsNone)
			{
				this.worldPosition.Value = UiGetLastPointerDataInfo.lastPointerEventData.pointerCurrentRaycast.worldPosition;
			}
			base.Finish();
		}

		// Token: 0x04008E62 RID: 36450
		public static PointerEventData lastPointerEventData;

		// Token: 0x04008E63 RID: 36451
		[Tooltip("Number of clicks in a row.")]
		[UIHint(UIHint.Variable)]
		public FsmInt clickCount;

		// Token: 0x04008E64 RID: 36452
		[Tooltip("The last time a click event was sent.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat clickTime;

		// Token: 0x04008E65 RID: 36453
		[Tooltip("Pointer delta since last update.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 delta;

		// Token: 0x04008E66 RID: 36454
		[Tooltip("Is a drag operation currently occuring.")]
		[UIHint(UIHint.Variable)]
		public FsmBool dragging;

		// Token: 0x04008E67 RID: 36455
		[Tooltip("The InputButton for this event.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PointerEventData.InputButton))]
		public FsmEnum inputButton;

		// Token: 0x04008E68 RID: 36456
		[Tooltip("Is the pointer being pressed? (Not documented by Unity)")]
		[UIHint(UIHint.Variable)]
		public FsmBool eligibleForClick;

		// Token: 0x04008E69 RID: 36457
		[Tooltip("The camera associated with the last OnPointerEnter event.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject enterEventCamera;

		// Token: 0x04008E6A RID: 36458
		[Tooltip("The camera associated with the last OnPointerPress event.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject pressEventCamera;

		// Token: 0x04008E6B RID: 36459
		[Tooltip("Is the pointer moving.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isPointerMoving;

		// Token: 0x04008E6C RID: 36460
		[Tooltip("Is scroll being used on the input device.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isScrolling;

		// Token: 0x04008E6D RID: 36461
		[Tooltip("The GameObject for the last press event.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject lastPress;

		// Token: 0x04008E6E RID: 36462
		[Tooltip("The object that is receiving OnDrag.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerDrag;

		// Token: 0x04008E6F RID: 36463
		[Tooltip("The object that received 'OnPointerEnter'.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerEnter;

		// Token: 0x04008E70 RID: 36464
		[Tooltip("Id of the pointer (touch id).")]
		[UIHint(UIHint.Variable)]
		public FsmInt pointerId;

		// Token: 0x04008E71 RID: 36465
		[Tooltip("The GameObject that received the OnPointerDown.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerPress;

		// Token: 0x04008E72 RID: 36466
		[Tooltip("Current pointer position.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 position;

		// Token: 0x04008E73 RID: 36467
		[Tooltip("Position of the press.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 pressPosition;

		// Token: 0x04008E74 RID: 36468
		[Tooltip("The object that the press happened on even if it can not handle the press event.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject rawPointerPress;

		// Token: 0x04008E75 RID: 36469
		[Tooltip("The amount of scroll since the last update.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 scrollDelta;

		// Token: 0x04008E76 RID: 36470
		[Tooltip("Is the event used?")]
		[UIHint(UIHint.Variable)]
		public FsmBool used;

		// Token: 0x04008E77 RID: 36471
		[Tooltip("Should a drag threshold be used?")]
		[UIHint(UIHint.Variable)]
		public FsmBool useDragThreshold;

		// Token: 0x04008E78 RID: 36472
		[Tooltip("The normal of the last raycast in world coordinates.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 worldNormal;

		// Token: 0x04008E79 RID: 36473
		[Tooltip("The world position of the last raycast.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 worldPosition;
	}
}
