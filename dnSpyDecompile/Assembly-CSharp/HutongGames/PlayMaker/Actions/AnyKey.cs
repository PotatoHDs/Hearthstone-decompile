using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB5 RID: 2997
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the user hits any Key or Mouse Button.")]
	public class AnyKey : FsmStateAction
	{
		// Token: 0x06009C3F RID: 39999 RVA: 0x00324D0B File Offset: 0x00322F0B
		public override void Reset()
		{
			this.sendEvent = null;
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x00324D14 File Offset: 0x00322F14
		public override void OnUpdate()
		{
			if (Input.anyKeyDown)
			{
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x040081C2 RID: 33218
		[RequiredField]
		[Tooltip("Event to send when any Key or Mouse Button is pressed.")]
		public FsmEvent sendEvent;
	}
}
