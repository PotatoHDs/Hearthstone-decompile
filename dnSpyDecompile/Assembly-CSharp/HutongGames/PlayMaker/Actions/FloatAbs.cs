using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C29 RID: 3113
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Float variable to its absolute value.")]
	public class FloatAbs : FsmStateAction
	{
		// Token: 0x06009E3A RID: 40506 RVA: 0x0032B0B4 File Offset: 0x003292B4
		public override void Reset()
		{
			this.floatVariable = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E3B RID: 40507 RVA: 0x0032B0C4 File Offset: 0x003292C4
		public override void OnEnter()
		{
			this.DoFloatAbs();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E3C RID: 40508 RVA: 0x0032B0DA File Offset: 0x003292DA
		public override void OnUpdate()
		{
			this.DoFloatAbs();
		}

		// Token: 0x06009E3D RID: 40509 RVA: 0x0032B0E2 File Offset: 0x003292E2
		private void DoFloatAbs()
		{
			this.floatVariable.Value = Mathf.Abs(this.floatVariable.Value);
		}

		// Token: 0x04008393 RID: 33683
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable.")]
		public FsmFloat floatVariable;

		// Token: 0x04008394 RID: 33684
		[Tooltip("Repeat every frame. Useful if the Float variable is changing.")]
		public bool everyFrame;
	}
}
