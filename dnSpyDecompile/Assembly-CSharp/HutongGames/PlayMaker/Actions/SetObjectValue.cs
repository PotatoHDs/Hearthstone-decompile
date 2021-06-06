using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DEE RID: 3566
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Sets the value of an Object Variable.")]
	public class SetObjectValue : FsmStateAction
	{
		// Token: 0x0600A675 RID: 42613 RVA: 0x00349446 File Offset: 0x00347646
		public override void Reset()
		{
			this.objectVariable = null;
			this.objectValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A676 RID: 42614 RVA: 0x0034945D File Offset: 0x0034765D
		public override void OnEnter()
		{
			this.objectVariable.Value = this.objectValue.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A677 RID: 42615 RVA: 0x00349483 File Offset: 0x00347683
		public override void OnUpdate()
		{
			this.objectVariable.Value = this.objectValue.Value;
		}

		// Token: 0x04008CF6 RID: 36086
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmObject objectVariable;

		// Token: 0x04008CF7 RID: 36087
		[RequiredField]
		public FsmObject objectValue;

		// Token: 0x04008CF8 RID: 36088
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
