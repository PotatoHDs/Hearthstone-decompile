using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F33 RID: 3891
	[ActionCategory("Pegasus")]
	[Tooltip("Tests if a Game Object is active.")]
	public class GameObjectIsActiveAction : FsmStateAction
	{
		// Token: 0x0600AC5B RID: 44123 RVA: 0x0035C910 File Offset: 0x0035AB10
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AC5C RID: 44124 RVA: 0x0035C935 File Offset: 0x0035AB35
		public override void OnEnter()
		{
			this.DoIsActive();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC5D RID: 44125 RVA: 0x0035C94B File Offset: 0x0035AB4B
		public override void OnUpdate()
		{
			this.DoIsActive();
		}

		// Token: 0x0600AC5E RID: 44126 RVA: 0x0035C954 File Offset: 0x0035AB54
		private void DoIsActive()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				bool activeInHierarchy = ownerDefaultTarget.activeInHierarchy;
				this.storeResult.Value = activeInHierarchy;
				base.Fsm.Event(activeInHierarchy ? this.trueEvent : this.falseEvent);
				return;
			}
			Debug.LogError("FSM GameObjectIsActive Error: GameObject is Null!");
		}

		// Token: 0x04009337 RID: 37687
		[RequiredField]
		[Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009338 RID: 37688
		[Tooltip("Event to send if the GameObject is active.")]
		public FsmEvent trueEvent;

		// Token: 0x04009339 RID: 37689
		[Tooltip("Event to send if the GameObject is NOT active.")]
		public FsmEvent falseEvent;

		// Token: 0x0400933A RID: 37690
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x0400933B RID: 37691
		public bool everyFrame;
	}
}
