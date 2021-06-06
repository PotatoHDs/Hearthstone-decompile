using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C54 RID: 3156
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Measures the Distance betweens 2 Game Objects and stores the result in a Float Variable.")]
	public class GetDistance : FsmStateAction
	{
		// Token: 0x06009EFD RID: 40701 RVA: 0x0032D0E8 File Offset: 0x0032B2E8
		public override void Reset()
		{
			this.gameObject = null;
			this.target = null;
			this.storeResult = null;
			this.everyFrame = true;
		}

		// Token: 0x06009EFE RID: 40702 RVA: 0x0032D106 File Offset: 0x0032B306
		public override void OnEnter()
		{
			this.DoGetDistance();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EFF RID: 40703 RVA: 0x0032D11C File Offset: 0x0032B31C
		public override void OnUpdate()
		{
			this.DoGetDistance();
		}

		// Token: 0x06009F00 RID: 40704 RVA: 0x0032D124 File Offset: 0x0032B324
		private void DoGetDistance()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null || this.target.Value == null || this.storeResult == null)
			{
				return;
			}
			this.storeResult.Value = Vector3.Distance(ownerDefaultTarget.transform.position, this.target.Value.transform.position);
		}

		// Token: 0x0400845F RID: 33887
		[RequiredField]
		[Tooltip("Measure distance from this GameObject.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008460 RID: 33888
		[RequiredField]
		[Tooltip("Target GameObject.")]
		public FsmGameObject target;

		// Token: 0x04008461 RID: 33889
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance in a float variable.")]
		public FsmFloat storeResult;

		// Token: 0x04008462 RID: 33890
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
