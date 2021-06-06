using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EAF RID: 3759
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Normalizes a Vector2 Variable.")]
	public class Vector2Normalize : FsmStateAction
	{
		// Token: 0x0600AA02 RID: 43522 RVA: 0x003540AD File Offset: 0x003522AD
		public override void Reset()
		{
			this.vector2Variable = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA03 RID: 43523 RVA: 0x003540C0 File Offset: 0x003522C0
		public override void OnEnter()
		{
			this.vector2Variable.Value = this.vector2Variable.Value.normalized;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA04 RID: 43524 RVA: 0x003540FC File Offset: 0x003522FC
		public override void OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Variable.Value.normalized;
		}

		// Token: 0x040090A2 RID: 37026
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector to normalize")]
		public FsmVector2 vector2Variable;

		// Token: 0x040090A3 RID: 37027
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
