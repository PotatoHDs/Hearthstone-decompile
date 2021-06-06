using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C6A RID: 3178
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when a Key is released.")]
	public class GetKeyUp : FsmStateAction
	{
		// Token: 0x06009F67 RID: 40807 RVA: 0x0032E7A9 File Offset: 0x0032C9A9
		public override void Reset()
		{
			this.sendEvent = null;
			this.key = KeyCode.None;
			this.storeResult = null;
		}

		// Token: 0x06009F68 RID: 40808 RVA: 0x0032E7C0 File Offset: 0x0032C9C0
		public override void OnUpdate()
		{
			bool keyUp = Input.GetKeyUp(this.key);
			if (keyUp)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = keyUp;
		}

		// Token: 0x04008506 RID: 34054
		[RequiredField]
		public KeyCode key;

		// Token: 0x04008507 RID: 34055
		public FsmEvent sendEvent;

		// Token: 0x04008508 RID: 34056
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
	}
}
