using System;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E3B RID: 3643
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Checks if Pointer is over a UI object, optionally takes a pointer ID, otherwise uses the current event.")]
	public class UiIsPointerOverUiObject : FsmStateAction
	{
		// Token: 0x0600A7DD RID: 42973 RVA: 0x0034E449 File Offset: 0x0034C649
		public override void Reset()
		{
			this.pointerId = new FsmInt
			{
				UseVariable = true
			};
			this.pointerOverUI = null;
			this.pointerNotOverUI = null;
			this.isPointerOverUI = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7DE RID: 42974 RVA: 0x0034E479 File Offset: 0x0034C679
		public override void OnEnter()
		{
			this.DoCheckPointer();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7DF RID: 42975 RVA: 0x0034E48F File Offset: 0x0034C68F
		public override void OnUpdate()
		{
			this.DoCheckPointer();
		}

		// Token: 0x0600A7E0 RID: 42976 RVA: 0x0034E498 File Offset: 0x0034C698
		private void DoCheckPointer()
		{
			bool flag = false;
			if (this.pointerId.IsNone)
			{
				flag = EventSystem.current.IsPointerOverGameObject();
			}
			else if (EventSystem.current.currentInputModule is PointerInputModule)
			{
				flag = (EventSystem.current.currentInputModule as PointerInputModule).IsPointerOverGameObject(this.pointerId.Value);
			}
			this.isPointerOverUI.Value = flag;
			base.Fsm.Event(flag ? this.pointerOverUI : this.pointerNotOverUI);
		}

		// Token: 0x04008E82 RID: 36482
		[Tooltip("Optional PointerId. Leave to none to use the current event")]
		public FsmInt pointerId;

		// Token: 0x04008E83 RID: 36483
		[Tooltip("Event to send when the Pointer is over an UI object.")]
		public FsmEvent pointerOverUI;

		// Token: 0x04008E84 RID: 36484
		[Tooltip("Event to send when the Pointer is NOT over an UI object.")]
		public FsmEvent pointerNotOverUI;

		// Token: 0x04008E85 RID: 36485
		[UIHint(UIHint.Variable)]
		public FsmBool isPointerOverUI;

		// Token: 0x04008E86 RID: 36486
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
