using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C49 RID: 3145
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when a Button is released.")]
	public class GetButtonUp : FsmStateAction
	{
		// Token: 0x06009ECC RID: 40652 RVA: 0x0032C71E File Offset: 0x0032A91E
		public override void Reset()
		{
			this.buttonName = "Fire1";
			this.sendEvent = null;
			this.storeResult = null;
		}

		// Token: 0x06009ECD RID: 40653 RVA: 0x0032C740 File Offset: 0x0032A940
		public override void OnUpdate()
		{
			bool buttonUp = Input.GetButtonUp(this.buttonName.Value);
			if (buttonUp)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = buttonUp;
		}

		// Token: 0x0400842A RID: 33834
		[RequiredField]
		[Tooltip("The name of the button. Set in the Unity Input Manager.")]
		public FsmString buttonName;

		// Token: 0x0400842B RID: 33835
		[Tooltip("Event to send if the button is released.")]
		public FsmEvent sendEvent;

		// Token: 0x0400842C RID: 33836
		[UIHint(UIHint.Variable)]
		[Tooltip("Set to True if the button is released.")]
		public FsmBool storeResult;
	}
}
