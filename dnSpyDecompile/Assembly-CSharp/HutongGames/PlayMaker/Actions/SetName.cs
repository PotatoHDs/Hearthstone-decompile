using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DED RID: 3565
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets a Game Object's Name.")]
	public class SetName : FsmStateAction
	{
		// Token: 0x0600A671 RID: 42609 RVA: 0x003493EC File Offset: 0x003475EC
		public override void Reset()
		{
			this.gameObject = null;
			this.name = null;
		}

		// Token: 0x0600A672 RID: 42610 RVA: 0x003493FC File Offset: 0x003475FC
		public override void OnEnter()
		{
			this.DoSetLayer();
			base.Finish();
		}

		// Token: 0x0600A673 RID: 42611 RVA: 0x0034940C File Offset: 0x0034760C
		private void DoSetLayer()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			ownerDefaultTarget.name = this.name.Value;
		}

		// Token: 0x04008CF4 RID: 36084
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CF5 RID: 36085
		[RequiredField]
		public FsmString name;
	}
}
