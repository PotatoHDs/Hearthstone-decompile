using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C3A RID: 3130
	[ActionCategory(ActionCategory.Logic)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Sends Events based on the current State of an FSM.")]
	public class FsmStateSwitch : FsmStateAction
	{
		// Token: 0x06009E87 RID: 40583 RVA: 0x0032BB32 File Offset: 0x00329D32
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = null;
			this.compareTo = new FsmString[1];
			this.sendEvent = new FsmEvent[1];
			this.everyFrame = false;
		}

		// Token: 0x06009E88 RID: 40584 RVA: 0x0032BB61 File Offset: 0x00329D61
		public override void OnEnter()
		{
			this.DoFsmStateSwitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E89 RID: 40585 RVA: 0x0032BB77 File Offset: 0x00329D77
		public override void OnUpdate()
		{
			this.DoFsmStateSwitch();
		}

		// Token: 0x06009E8A RID: 40586 RVA: 0x0032BB80 File Offset: 0x00329D80
		private void DoFsmStateSwitch()
		{
			GameObject value = this.gameObject.Value;
			if (value == null)
			{
				return;
			}
			if (value != this.previousGo)
			{
				this.fsm = ActionHelpers.GetGameObjectFsm(value, this.fsmName.Value);
				this.previousGo = value;
			}
			if (this.fsm == null)
			{
				return;
			}
			string activeStateName = this.fsm.ActiveStateName;
			for (int i = 0; i < this.compareTo.Length; i++)
			{
				if (activeStateName == this.compareTo[i].Value)
				{
					base.Fsm.Event(this.sendEvent[i]);
					return;
				}
			}
		}

		// Token: 0x040083DB RID: 33755
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmGameObject gameObject;

		// Token: 0x040083DC RID: 33756
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on GameObject. Useful if there is more than one FSM on the GameObject.")]
		public FsmString fsmName;

		// Token: 0x040083DD RID: 33757
		[CompoundArray("State Switches", "Compare State", "Send Event")]
		public FsmString[] compareTo;

		// Token: 0x040083DE RID: 33758
		public FsmEvent[] sendEvent;

		// Token: 0x040083DF RID: 33759
		[Tooltip("Repeat every frame. Useful if you're waiting for a particular result.")]
		public bool everyFrame;

		// Token: 0x040083E0 RID: 33760
		private GameObject previousGo;

		// Token: 0x040083E1 RID: 33761
		private PlayMakerFSM fsm;
	}
}
