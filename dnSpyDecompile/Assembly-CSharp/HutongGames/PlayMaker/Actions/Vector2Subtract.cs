using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB3 RID: 3763
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Subtracts a Vector2 value from a Vector2 variable.")]
	public class Vector2Subtract : FsmStateAction
	{
		// Token: 0x0600AA13 RID: 43539 RVA: 0x0035447C File Offset: 0x0035267C
		public override void Reset()
		{
			this.vector2Variable = null;
			this.subtractVector = new FsmVector2
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600AA14 RID: 43540 RVA: 0x0035449E File Offset: 0x0035269E
		public override void OnEnter()
		{
			this.vector2Variable.Value = this.vector2Variable.Value - this.subtractVector.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA15 RID: 43541 RVA: 0x003544D4 File Offset: 0x003526D4
		public override void OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Variable.Value - this.subtractVector.Value;
		}

		// Token: 0x040090B1 RID: 37041
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 operand")]
		public FsmVector2 vector2Variable;

		// Token: 0x040090B2 RID: 37042
		[RequiredField]
		[Tooltip("The vector2 to subtract with")]
		public FsmVector2 subtractVector;

		// Token: 0x040090B3 RID: 37043
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
