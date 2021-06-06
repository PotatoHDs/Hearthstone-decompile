using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C43 RID: 3139
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on a Game Object's Tag.")]
	public class GameObjectTagSwitch : FsmStateAction
	{
		// Token: 0x06009EB2 RID: 40626 RVA: 0x0032C1B0 File Offset: 0x0032A3B0
		public override void Reset()
		{
			this.gameObject = null;
			this.compareTo = new FsmString[1];
			this.sendEvent = new FsmEvent[1];
			this.everyFrame = false;
		}

		// Token: 0x06009EB3 RID: 40627 RVA: 0x0032C1D8 File Offset: 0x0032A3D8
		public override void OnEnter()
		{
			this.DoTagSwitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EB4 RID: 40628 RVA: 0x0032C1EE File Offset: 0x0032A3EE
		public override void OnUpdate()
		{
			this.DoTagSwitch();
		}

		// Token: 0x06009EB5 RID: 40629 RVA: 0x0032C1F8 File Offset: 0x0032A3F8
		private void DoTagSwitch()
		{
			GameObject value = this.gameObject.Value;
			if (value == null)
			{
				return;
			}
			for (int i = 0; i < this.compareTo.Length; i++)
			{
				if (value.tag == this.compareTo[i].Value)
				{
					base.Fsm.Event(this.sendEvent[i]);
					return;
				}
			}
		}

		// Token: 0x0400840F RID: 33807
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject to test.")]
		public FsmGameObject gameObject;

		// Token: 0x04008410 RID: 33808
		[CompoundArray("Tag Switches", "Compare Tag", "Send Event")]
		[UIHint(UIHint.Tag)]
		public FsmString[] compareTo;

		// Token: 0x04008411 RID: 33809
		public FsmEvent[] sendEvent;

		// Token: 0x04008412 RID: 33810
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
