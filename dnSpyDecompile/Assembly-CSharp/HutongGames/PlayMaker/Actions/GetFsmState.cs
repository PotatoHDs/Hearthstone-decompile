using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C60 RID: 3168
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "fsmComponent", false)]
	[Tooltip("Gets the name of the specified FSMs current state. Either reference the fsm component directly, or find it on a game object.")]
	public class GetFsmState : FsmStateAction
	{
		// Token: 0x06009F37 RID: 40759 RVA: 0x0032DE5D File Offset: 0x0032C05D
		public override void Reset()
		{
			this.fsmComponent = null;
			this.gameObject = null;
			this.fsmName = "";
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F38 RID: 40760 RVA: 0x0032DE8B File Offset: 0x0032C08B
		public override void OnEnter()
		{
			this.DoGetFsmState();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F39 RID: 40761 RVA: 0x0032DEA1 File Offset: 0x0032C0A1
		public override void OnUpdate()
		{
			this.DoGetFsmState();
		}

		// Token: 0x06009F3A RID: 40762 RVA: 0x0032DEAC File Offset: 0x0032C0AC
		private void DoGetFsmState()
		{
			if (this.fsm == null)
			{
				if (this.fsmComponent != null)
				{
					this.fsm = this.fsmComponent;
				}
				else
				{
					GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
					if (ownerDefaultTarget != null)
					{
						this.fsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, this.fsmName.Value);
					}
				}
				if (this.fsm == null)
				{
					this.storeResult.Value = "";
					return;
				}
			}
			this.storeResult.Value = this.fsm.ActiveStateName;
		}

		// Token: 0x040084C2 RID: 33986
		[Tooltip("Drag a PlayMakerFSM component here.")]
		public PlayMakerFSM fsmComponent;

		// Token: 0x040084C3 RID: 33987
		[Tooltip("If not specifying the component above, specify the GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084C4 RID: 33988
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on Game Object. If left blank it will find the first PlayMakerFSM on the GameObject.")]
		public FsmString fsmName;

		// Token: 0x040084C5 RID: 33989
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the state name in a string variable.")]
		public FsmString storeResult;

		// Token: 0x040084C6 RID: 33990
		[Tooltip("Repeat every frame. E.g.,  useful if you're waiting for the state to change.")]
		public bool everyFrame;

		// Token: 0x040084C7 RID: 33991
		private PlayMakerFSM fsm;
	}
}
