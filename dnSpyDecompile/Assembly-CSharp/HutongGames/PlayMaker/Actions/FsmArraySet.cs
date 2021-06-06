using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC9 RID: 3017
	[ActionCategory(ActionCategory.Array)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Obsolete("This action was wip and accidentally released.")]
	[Tooltip("Set an item in an Array Variable in another FSM.")]
	public class FsmArraySet : FsmStateAction
	{
		// Token: 0x06009C91 RID: 40081 RVA: 0x00325C3F File Offset: 0x00323E3F
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x00325C5F File Offset: 0x00323E5F
		public override void OnEnter()
		{
			this.DoSetFsmString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x00325C78 File Offset: 0x00323E78
		private void DoSetFsmString()
		{
			if (this.setValue == null)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.goLastFrame)
			{
				this.goLastFrame = ownerDefaultTarget;
				this.fsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, this.fsmName.Value);
			}
			if (this.fsm == null)
			{
				base.LogWarning("Could not find FSM: " + this.fsmName.Value);
				return;
			}
			FsmString fsmString = this.fsm.FsmVariables.GetFsmString(this.variableName.Value);
			if (fsmString != null)
			{
				fsmString.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x00325D4C File Offset: 0x00323F4C
		public override void OnUpdate()
		{
			this.DoSetFsmString();
		}

		// Token: 0x04008209 RID: 33289
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400820A RID: 33290
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x0400820B RID: 33291
		[RequiredField]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x0400820C RID: 33292
		[Tooltip("Set the value of the variable.")]
		public FsmString setValue;

		// Token: 0x0400820D RID: 33293
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x0400820E RID: 33294
		private GameObject goLastFrame;

		// Token: 0x0400820F RID: 33295
		private PlayMakerFSM fsm;
	}
}
