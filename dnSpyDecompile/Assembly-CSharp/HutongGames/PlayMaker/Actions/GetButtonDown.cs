using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C48 RID: 3144
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when a Button is pressed.")]
	public class GetButtonDown : FsmStateAction
	{
		// Token: 0x06009EC9 RID: 40649 RVA: 0x0032C6BD File Offset: 0x0032A8BD
		public override void Reset()
		{
			this.buttonName = "Fire1";
			this.sendEvent = null;
			this.storeResult = null;
		}

		// Token: 0x06009ECA RID: 40650 RVA: 0x0032C6E0 File Offset: 0x0032A8E0
		public override void OnUpdate()
		{
			bool buttonDown = Input.GetButtonDown(this.buttonName.Value);
			if (buttonDown)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = buttonDown;
		}

		// Token: 0x04008427 RID: 33831
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;

		// Token: 0x04008428 RID: 33832
		[Tooltip("Event to send if the button is pressed.")]
		public FsmEvent sendEvent;

		// Token: 0x04008429 RID: 33833
		[Tooltip("Set to True if the button is pressed.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
	}
}
