using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C40 RID: 3136
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a GameObject is a Child of another GameObject.")]
	public class GameObjectIsChildOf : FsmStateAction
	{
		// Token: 0x06009EA4 RID: 40612 RVA: 0x0032BFD4 File Offset: 0x0032A1D4
		public override void Reset()
		{
			this.gameObject = null;
			this.isChildOf = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
		}

		// Token: 0x06009EA5 RID: 40613 RVA: 0x0032BFF9 File Offset: 0x0032A1F9
		public override void OnEnter()
		{
			this.DoIsChildOf(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x06009EA6 RID: 40614 RVA: 0x0032C018 File Offset: 0x0032A218
		private void DoIsChildOf(GameObject go)
		{
			if (go == null || this.isChildOf == null)
			{
				return;
			}
			bool flag = go.transform.IsChildOf(this.isChildOf.Value.transform);
			this.storeResult.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x04008400 RID: 33792
		[RequiredField]
		[Tooltip("GameObject to test.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008401 RID: 33793
		[RequiredField]
		[Tooltip("Is it a child of this GameObject?")]
		public FsmGameObject isChildOf;

		// Token: 0x04008402 RID: 33794
		[Tooltip("Event to send if GameObject is a child.")]
		public FsmEvent trueEvent;

		// Token: 0x04008403 RID: 33795
		[Tooltip("Event to send if GameObject is NOT a child.")]
		public FsmEvent falseEvent;

		// Token: 0x04008404 RID: 33796
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store result in a bool variable")]
		public FsmBool storeResult;
	}
}
