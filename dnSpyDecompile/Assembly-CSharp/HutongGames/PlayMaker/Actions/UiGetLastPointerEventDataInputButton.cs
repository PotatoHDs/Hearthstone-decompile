using System;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E39 RID: 3641
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets pointer data Input Button on the last System event.")]
	public class UiGetLastPointerEventDataInputButton : FsmStateAction
	{
		// Token: 0x0600A7D4 RID: 42964 RVA: 0x0034E2B0 File Offset: 0x0034C4B0
		public override void Reset()
		{
			this.inputButton = PointerEventData.InputButton.Left;
			this.leftClick = null;
			this.middleClick = null;
			this.rightClick = null;
		}

		// Token: 0x0600A7D5 RID: 42965 RVA: 0x0034E2D8 File Offset: 0x0034C4D8
		public override void OnEnter()
		{
			this.ExecuteAction();
			base.Finish();
		}

		// Token: 0x0600A7D6 RID: 42966 RVA: 0x0034E2E8 File Offset: 0x0034C4E8
		private void ExecuteAction()
		{
			if (UiGetLastPointerDataInfo.lastPointerEventData == null)
			{
				return;
			}
			if (!this.inputButton.IsNone)
			{
				this.inputButton.Value = UiGetLastPointerDataInfo.lastPointerEventData.button;
			}
			if (!string.IsNullOrEmpty(this.leftClick.Name) && UiGetLastPointerDataInfo.lastPointerEventData.button == PointerEventData.InputButton.Left)
			{
				base.Fsm.Event(this.leftClick);
				return;
			}
			if (!string.IsNullOrEmpty(this.middleClick.Name) && UiGetLastPointerDataInfo.lastPointerEventData.button == PointerEventData.InputButton.Middle)
			{
				base.Fsm.Event(this.middleClick);
				return;
			}
			if (!string.IsNullOrEmpty(this.rightClick.Name) && UiGetLastPointerDataInfo.lastPointerEventData.button == PointerEventData.InputButton.Right)
			{
				base.Fsm.Event(this.rightClick);
			}
		}

		// Token: 0x04008E7A RID: 36474
		[Tooltip("Store the Input Button pressed (Left, Right, Middle)")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PointerEventData.InputButton))]
		public FsmEnum inputButton;

		// Token: 0x04008E7B RID: 36475
		[Tooltip("Event to send if Left Button clicked.")]
		public FsmEvent leftClick;

		// Token: 0x04008E7C RID: 36476
		[Tooltip("Event to send if Middle Button clicked.")]
		public FsmEvent middleClick;

		// Token: 0x04008E7D RID: 36477
		[Tooltip("Event to send if Right Button clicked.")]
		public FsmEvent rightClick;
	}
}
