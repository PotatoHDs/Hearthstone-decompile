using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EAE RID: 3758
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Multiplies a Vector2 variable by a Float.")]
	public class Vector2Multiply : FsmStateAction
	{
		// Token: 0x0600A9FE RID: 43518 RVA: 0x0035402F File Offset: 0x0035222F
		public override void Reset()
		{
			this.vector2Variable = null;
			this.multiplyBy = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A9FF RID: 43519 RVA: 0x0035404F File Offset: 0x0035224F
		public override void OnEnter()
		{
			this.vector2Variable.Value = this.vector2Variable.Value * this.multiplyBy.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA00 RID: 43520 RVA: 0x00354085 File Offset: 0x00352285
		public override void OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Variable.Value * this.multiplyBy.Value;
		}

		// Token: 0x0400909F RID: 37023
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector to Multiply")]
		public FsmVector2 vector2Variable;

		// Token: 0x040090A0 RID: 37024
		[RequiredField]
		[Tooltip("The multiplication factor")]
		public FsmFloat multiplyBy;

		// Token: 0x040090A1 RID: 37025
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
