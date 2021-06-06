using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD7 RID: 3031
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	public abstract class BaseFsmVariableAction : FsmStateAction
	{
		// Token: 0x06009CC0 RID: 40128 RVA: 0x0032678C File Offset: 0x0032498C
		public override void Reset()
		{
			this.fsmNotFound = null;
			this.variableNotFound = null;
		}

		// Token: 0x06009CC1 RID: 40129 RVA: 0x0032679C File Offset: 0x0032499C
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

		// Token: 0x06009CC2 RID: 40130 RVA: 0x0032682A File Offset: 0x00324A2A
		protected void DoVariableNotFound(string variableName)
		{
			base.LogWarning("Could not find variable: " + variableName);
			base.Fsm.Event(this.variableNotFound);
		}

		// Token: 0x04008238 RID: 33336
		[ActionSection("Events")]
		[Tooltip("The event to send if the FSM is not found.")]
		public FsmEvent fsmNotFound;

		// Token: 0x04008239 RID: 33337
		[Tooltip("The event to send if the Variable is not found.")]
		public FsmEvent variableNotFound;

		// Token: 0x0400823A RID: 33338
		private GameObject cachedGameObject;

		// Token: 0x0400823B RID: 33339
		private string cachedFsmName;

		// Token: 0x0400823C RID: 33340
		protected PlayMakerFSM fsm;
	}
}
