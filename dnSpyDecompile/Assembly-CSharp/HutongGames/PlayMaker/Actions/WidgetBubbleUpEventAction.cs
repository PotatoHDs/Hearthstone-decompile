using System;
using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F9B RID: 3995
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event up the UI hierarchy")]
	public class WidgetBubbleUpEventAction : FsmStateAction
	{
		// Token: 0x0600ADFF RID: 44543 RVA: 0x00362E3D File Offset: 0x0036103D
		public override void Reset()
		{
			this.eventName = null;
		}

		// Token: 0x0600AE00 RID: 44544 RVA: 0x00362E46 File Offset: 0x00361046
		public override void OnEnter()
		{
			this.SendEventUpward();
			base.Finish();
		}

		// Token: 0x0600AE01 RID: 44545 RVA: 0x00362E54 File Offset: 0x00361054
		private void SendEventUpward()
		{
			if (this.eventName == null || this.eventName.Value == null)
			{
				Debug.LogError("WidgetBubbleUpEventAction.SendEventUpward() - Event Name is null.");
				return;
			}
			SendEventUpwardStateAction.SendEventUpward(base.Owner, this.eventName.Value, null);
		}

		// Token: 0x040094CF RID: 38095
		[RequiredField]
		[Tooltip("Name of the event we're sending up.")]
		public FsmString eventName;
	}
}
