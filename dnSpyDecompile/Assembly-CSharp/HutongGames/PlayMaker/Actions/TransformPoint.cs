using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E23 RID: 3619
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms a Position from a Game Object's local space to world space.")]
	public class TransformPoint : FsmStateAction
	{
		// Token: 0x0600A75A RID: 42842 RVA: 0x0034C7A8 File Offset: 0x0034A9A8
		public override void Reset()
		{
			this.gameObject = null;
			this.localPosition = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A75B RID: 42843 RVA: 0x0034C7C6 File Offset: 0x0034A9C6
		public override void OnEnter()
		{
			this.DoTransformPoint();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A75C RID: 42844 RVA: 0x0034C7DC File Offset: 0x0034A9DC
		public override void OnUpdate()
		{
			this.DoTransformPoint();
		}

		// Token: 0x0600A75D RID: 42845 RVA: 0x0034C7E4 File Offset: 0x0034A9E4
		private void DoTransformPoint()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.storeResult.Value = ownerDefaultTarget.transform.TransformPoint(this.localPosition.Value);
		}

		// Token: 0x04008DF7 RID: 36343
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008DF8 RID: 36344
		[RequiredField]
		public FsmVector3 localPosition;

		// Token: 0x04008DF9 RID: 36345
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		// Token: 0x04008DFA RID: 36346
		public bool everyFrame;
	}
}
