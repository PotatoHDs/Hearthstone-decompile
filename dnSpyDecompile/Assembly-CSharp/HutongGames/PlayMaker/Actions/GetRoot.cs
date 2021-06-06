using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C82 RID: 3202
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the top most parent of the Game Object.\nIf the game object has no parent, returns itself.")]
	public class GetRoot : FsmStateAction
	{
		// Token: 0x06009FCB RID: 40907 RVA: 0x0032F47F File Offset: 0x0032D67F
		public override void Reset()
		{
			this.gameObject = null;
			this.storeRoot = null;
		}

		// Token: 0x06009FCC RID: 40908 RVA: 0x0032F48F File Offset: 0x0032D68F
		public override void OnEnter()
		{
			this.DoGetRoot();
			base.Finish();
		}

		// Token: 0x06009FCD RID: 40909 RVA: 0x0032F4A0 File Offset: 0x0032D6A0
		private void DoGetRoot()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.storeRoot.Value = ownerDefaultTarget.transform.root.gameObject;
		}

		// Token: 0x04008555 RID: 34133
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008556 RID: 34134
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeRoot;
	}
}
