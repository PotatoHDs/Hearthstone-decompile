using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C69 RID: 3177
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when a Key is pressed.")]
	public class GetKeyDown : FsmStateAction
	{
		// Token: 0x06009F64 RID: 40804 RVA: 0x0032E758 File Offset: 0x0032C958
		public override void Reset()
		{
			this.sendEvent = null;
			this.key = KeyCode.None;
			this.storeResult = null;
		}

		// Token: 0x06009F65 RID: 40805 RVA: 0x0032E770 File Offset: 0x0032C970
		public override void OnUpdate()
		{
			bool keyDown = Input.GetKeyDown(this.key);
			if (keyDown)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = keyDown;
		}

		// Token: 0x04008503 RID: 34051
		[RequiredField]
		public KeyCode key;

		// Token: 0x04008504 RID: 34052
		public FsmEvent sendEvent;

		// Token: 0x04008505 RID: 34053
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
	}
}
