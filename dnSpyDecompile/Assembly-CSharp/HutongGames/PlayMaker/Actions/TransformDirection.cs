using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E21 RID: 3617
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms a Direction from a Game Object's local space to world space.")]
	public class TransformDirection : FsmStateAction
	{
		// Token: 0x0600A752 RID: 42834 RVA: 0x0034C54E File Offset: 0x0034A74E
		public override void Reset()
		{
			this.gameObject = null;
			this.localDirection = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A753 RID: 42835 RVA: 0x0034C56C File Offset: 0x0034A76C
		public override void OnEnter()
		{
			this.DoTransformDirection();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A754 RID: 42836 RVA: 0x0034C582 File Offset: 0x0034A782
		public override void OnUpdate()
		{
			this.DoTransformDirection();
		}

		// Token: 0x0600A755 RID: 42837 RVA: 0x0034C58C File Offset: 0x0034A78C
		private void DoTransformDirection()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.storeResult.Value = ownerDefaultTarget.transform.TransformDirection(this.localDirection.Value);
		}

		// Token: 0x04008DEC RID: 36332
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008DED RID: 36333
		[RequiredField]
		public FsmVector3 localDirection;

		// Token: 0x04008DEE RID: 36334
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		// Token: 0x04008DEF RID: 36335
		public bool everyFrame;
	}
}
