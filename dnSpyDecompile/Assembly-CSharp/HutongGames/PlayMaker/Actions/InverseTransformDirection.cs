using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC9 RID: 3273
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms a Direction from world space to a Game Object's local space. The opposite of TransformDirection.")]
	public class InverseTransformDirection : FsmStateAction
	{
		// Token: 0x0600A0D6 RID: 41174 RVA: 0x00332718 File Offset: 0x00330918
		public override void Reset()
		{
			this.gameObject = null;
			this.worldDirection = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0D7 RID: 41175 RVA: 0x00332736 File Offset: 0x00330936
		public override void OnEnter()
		{
			this.DoInverseTransformDirection();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0D8 RID: 41176 RVA: 0x0033274C File Offset: 0x0033094C
		public override void OnUpdate()
		{
			this.DoInverseTransformDirection();
		}

		// Token: 0x0600A0D9 RID: 41177 RVA: 0x00332754 File Offset: 0x00330954
		private void DoInverseTransformDirection()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.storeResult.Value = ownerDefaultTarget.transform.InverseTransformDirection(this.worldDirection.Value);
		}

		// Token: 0x04008665 RID: 34405
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008666 RID: 34406
		[RequiredField]
		public FsmVector3 worldDirection;

		// Token: 0x04008667 RID: 34407
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		// Token: 0x04008668 RID: 34408
		public bool everyFrame;
	}
}
