using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C73 RID: 3187
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the specified Mouse Button is pressed. Optionally store the button state in a bool variable.")]
	public class GetMouseButtonDown : FsmStateAction
	{
		// Token: 0x06009F89 RID: 40841 RVA: 0x0032EC3B File Offset: 0x0032CE3B
		public override void Reset()
		{
			this.button = MouseButton.Left;
			this.sendEvent = null;
			this.storeResult = null;
			this.inUpdateOnly = true;
		}

		// Token: 0x06009F8A RID: 40842 RVA: 0x0032EC59 File Offset: 0x0032CE59
		public override void OnEnter()
		{
			if (!this.inUpdateOnly)
			{
				this.DoGetMouseButtonDown();
			}
		}

		// Token: 0x06009F8B RID: 40843 RVA: 0x0032EC69 File Offset: 0x0032CE69
		public override void OnUpdate()
		{
			this.DoGetMouseButtonDown();
		}

		// Token: 0x06009F8C RID: 40844 RVA: 0x0032EC74 File Offset: 0x0032CE74
		private void DoGetMouseButtonDown()
		{
			bool mouseButtonDown = Input.GetMouseButtonDown((int)this.button);
			if (mouseButtonDown)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = mouseButtonDown;
		}

		// Token: 0x04008522 RID: 34082
		[RequiredField]
		[Tooltip("The mouse button to test.")]
		public MouseButton button;

		// Token: 0x04008523 RID: 34083
		[Tooltip("Event to send if the mouse button is down.")]
		public FsmEvent sendEvent;

		// Token: 0x04008524 RID: 34084
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the button state in a Bool Variable.")]
		public FsmBool storeResult;

		// Token: 0x04008525 RID: 34085
		[Tooltip("Uncheck to run when entering the state.")]
		public bool inUpdateOnly;
	}
}
