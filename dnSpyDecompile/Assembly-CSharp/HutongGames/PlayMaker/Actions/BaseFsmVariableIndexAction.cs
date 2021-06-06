using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD8 RID: 3032
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	public abstract class BaseFsmVariableIndexAction : FsmStateAction
	{
		// Token: 0x06009CC4 RID: 40132 RVA: 0x0032684E File Offset: 0x00324A4E
		public override void Reset()
		{
			this.fsmNotFound = null;
			this.variableNotFound = null;
		}

		// Token: 0x06009CC5 RID: 40133 RVA: 0x00326860 File Offset: 0x00324A60
		protected bool UpdateCache(GameObject go, string fsmName)
		{
			if (go == null)
			{
				return false;
			}
			if (this.fsm == null || this.cachedGameObject != go || this.cachedFsmName != fsmName)
			{
				this.fsm = ActionHelpers.GetGameObjectFsm(go, fsmName);
				this.cachedGameObject = go;
				this.cachedFsmName = fsmName;
				if (this.fsm == null)
				{
					base.LogWarning("Could not find FSM: " + fsmName);
					base.Fsm.Event(this.fsmNotFound);
				}
			}
			return true;
		}

		// Token: 0x06009CC6 RID: 40134 RVA: 0x003268EE File Offset: 0x00324AEE
		protected void DoVariableNotFound(string variableName)
		{
			base.LogWarning("Could not find variable: " + variableName);
			base.Fsm.Event(this.variableNotFound);
		}

		// Token: 0x0400823D RID: 33341
		[ActionSection("Events")]
		[Tooltip("The event to trigger if the index is out of range")]
		public FsmEvent indexOutOfRange;

		// Token: 0x0400823E RID: 33342
		[Tooltip("The event to send if the FSM is not found.")]
		public FsmEvent fsmNotFound;

		// Token: 0x0400823F RID: 33343
		[Tooltip("The event to send if the Variable is not found.")]
		public FsmEvent variableNotFound;

		// Token: 0x04008240 RID: 33344
		private GameObject cachedGameObject;

		// Token: 0x04008241 RID: 33345
		private string cachedFsmName;

		// Token: 0x04008242 RID: 33346
		protected PlayMakerFSM fsm;
	}
}
