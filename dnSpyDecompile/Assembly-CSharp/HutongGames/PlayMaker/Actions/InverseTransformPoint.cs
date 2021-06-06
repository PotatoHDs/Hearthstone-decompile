using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CCA RID: 3274
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms position from world space to a Game Object's local space. The opposite of TransformPoint.")]
	public class InverseTransformPoint : FsmStateAction
	{
		// Token: 0x0600A0DB RID: 41179 RVA: 0x0033279E File Offset: 0x0033099E
		public override void Reset()
		{
			this.gameObject = null;
			this.worldPosition = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0DC RID: 41180 RVA: 0x003327BC File Offset: 0x003309BC
		public override void OnEnter()
		{
			this.DoInverseTransformPoint();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0DD RID: 41181 RVA: 0x003327D2 File Offset: 0x003309D2
		public override void OnUpdate()
		{
			this.DoInverseTransformPoint();
		}

		// Token: 0x0600A0DE RID: 41182 RVA: 0x003327DC File Offset: 0x003309DC
		private void DoInverseTransformPoint()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.storeResult.Value = ownerDefaultTarget.transform.InverseTransformPoint(this.worldPosition.Value);
		}

		// Token: 0x04008669 RID: 34409
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400866A RID: 34410
		[RequiredField]
		public FsmVector3 worldPosition;

		// Token: 0x0400866B RID: 34411
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		// Token: 0x0400866C RID: 34412
		public bool everyFrame;
	}
}
