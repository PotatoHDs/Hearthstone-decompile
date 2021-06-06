using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C3B RID: 3131
	[ActionCategory(ActionCategory.Logic)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Tests if an FSM is in the specified State.")]
	public class FsmStateTest : FsmStateAction
	{
		// Token: 0x06009E8C RID: 40588 RVA: 0x0032BC26 File Offset: 0x00329E26
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = null;
			this.stateName = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E8D RID: 40589 RVA: 0x0032BC59 File Offset: 0x00329E59
		public override void OnEnter()
		{
			this.DoFsmStateTest();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E8E RID: 40590 RVA: 0x0032BC6F File Offset: 0x00329E6F
		public override void OnUpdate()
		{
			this.DoFsmStateTest();
		}

		// Token: 0x06009E8F RID: 40591 RVA: 0x0032BC78 File Offset: 0x00329E78
		private void DoFsmStateTest()
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
			bool value2 = false;
			if (this.fsm.ActiveStateName == this.stateName.Value)
			{
				base.Fsm.Event(this.trueEvent);
				value2 = true;
			}
			else
			{
				base.Fsm.Event(this.falseEvent);
			}
			this.storeResult.Value = value2;
		}

		// Token: 0x040083E2 RID: 33762
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmGameObject gameObject;

		// Token: 0x040083E3 RID: 33763
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on Game Object. Useful if there is more than one FSM on the GameObject.")]
		public FsmString fsmName;

		// Token: 0x040083E4 RID: 33764
		[RequiredField]
		[Tooltip("Check to see if the FSM is in this state.")]
		public FsmString stateName;

		// Token: 0x040083E5 RID: 33765
		[Tooltip("Event to send if the FSM is in the specified state.")]
		public FsmEvent trueEvent;

		// Token: 0x040083E6 RID: 33766
		[Tooltip("Event to send if the FSM is NOT in the specified state.")]
		public FsmEvent falseEvent;

		// Token: 0x040083E7 RID: 33767
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of this test in a bool variable. Useful if other actions depend on this test.")]
		public FsmBool storeResult;

		// Token: 0x040083E8 RID: 33768
		[Tooltip("Repeat every frame. Useful if you're waiting for a particular state.")]
		public bool everyFrame;

		// Token: 0x040083E9 RID: 33769
		private GameObject previousGo;

		// Token: 0x040083EA RID: 33770
		private PlayMakerFSM fsm;
	}
}
