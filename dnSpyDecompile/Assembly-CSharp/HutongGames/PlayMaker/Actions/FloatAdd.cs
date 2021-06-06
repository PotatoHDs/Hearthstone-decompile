using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C2A RID: 3114
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Adds a value to a Float Variable.")]
	public class FloatAdd : FsmStateAction
	{
		// Token: 0x06009E3F RID: 40511 RVA: 0x0032B0FF File Offset: 0x003292FF
		public override void Reset()
		{
			this.floatVariable = null;
			this.add = null;
			this.everyFrame = false;
			this.perSecond = false;
		}

		// Token: 0x06009E40 RID: 40512 RVA: 0x0032B11D File Offset: 0x0032931D
		public override void OnEnter()
		{
			this.DoFloatAdd();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E41 RID: 40513 RVA: 0x0032B133 File Offset: 0x00329333
		public override void OnUpdate()
		{
			this.DoFloatAdd();
		}

		// Token: 0x06009E42 RID: 40514 RVA: 0x0032B13C File Offset: 0x0032933C
		private void DoFloatAdd()
		{
			if (!this.perSecond)
			{
				this.floatVariable.Value += this.add.Value;
				return;
			}
			this.floatVariable.Value += this.add.Value * Time.deltaTime;
		}

		// Token: 0x04008395 RID: 33685
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable to add to.")]
		public FsmFloat floatVariable;

		// Token: 0x04008396 RID: 33686
		[RequiredField]
		[Tooltip("Amount to add.")]
		public FsmFloat add;

		// Token: 0x04008397 RID: 33687
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x04008398 RID: 33688
		[Tooltip("Used with Every Frame. Adds the value over one second to make the operation frame rate independent.")]
		public bool perSecond;
	}
}
