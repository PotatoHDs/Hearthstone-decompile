using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C74 RID: 3188
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the specified Mouse Button is released. Optionally store the button state in a bool variable.")]
	public class GetMouseButtonUp : FsmStateAction
	{
		// Token: 0x06009F8E RID: 40846 RVA: 0x0032ECAD File Offset: 0x0032CEAD
		public override void Reset()
		{
			this.button = MouseButton.Left;
			this.sendEvent = null;
			this.storeResult = null;
			this.inUpdateOnly = true;
		}

		// Token: 0x06009F8F RID: 40847 RVA: 0x0032ECCB File Offset: 0x0032CECB
		public override void OnEnter()
		{
			if (!this.inUpdateOnly)
			{
				this.DoGetMouseButtonUp();
			}
		}

		// Token: 0x06009F90 RID: 40848 RVA: 0x0032ECDB File Offset: 0x0032CEDB
		public override void OnUpdate()
		{
			this.DoGetMouseButtonUp();
		}

		// Token: 0x06009F91 RID: 40849 RVA: 0x0032ECE4 File Offset: 0x0032CEE4
		public void DoGetMouseButtonUp()
		{
			bool mouseButtonUp = Input.GetMouseButtonUp((int)this.button);
			if (mouseButtonUp)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = mouseButtonUp;
		}

		// Token: 0x04008526 RID: 34086
		[RequiredField]
		[Tooltip("The mouse button to test.")]
		public MouseButton button;

		// Token: 0x04008527 RID: 34087
		[Tooltip("Event to send if the mouse button is down.")]
		public FsmEvent sendEvent;

		// Token: 0x04008528 RID: 34088
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the pressed state in a Bool Variable.")]
		public FsmBool storeResult;

		// Token: 0x04008529 RID: 34089
		[Tooltip("Uncheck to run when entering the state.")]
		public bool inUpdateOnly;
	}
}
