using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C6F RID: 3183
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the Mass of a Game Object's Rigid Body.")]
	public class GetMass : ComponentAction<Rigidbody>
	{
		// Token: 0x06009F79 RID: 40825 RVA: 0x0032E90F File Offset: 0x0032CB0F
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
		}

		// Token: 0x06009F7A RID: 40826 RVA: 0x0032E91F File Offset: 0x0032CB1F
		public override void OnEnter()
		{
			this.DoGetMass();
			base.Finish();
		}

		// Token: 0x06009F7B RID: 40827 RVA: 0x0032E930 File Offset: 0x0032CB30
		private void DoGetMass()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.storeResult.Value = base.rigidbody.mass;
			}
		}

		// Token: 0x04008515 RID: 34069
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject that owns the Rigidbody")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008516 RID: 34070
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the mass in a float variable.")]
		public FsmFloat storeResult;
	}
}
