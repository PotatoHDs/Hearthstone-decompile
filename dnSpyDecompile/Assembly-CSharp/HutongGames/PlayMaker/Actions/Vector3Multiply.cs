using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EBC RID: 3772
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Multiplies a Vector3 variable by a Float.")]
	public class Vector3Multiply : FsmStateAction
	{
		// Token: 0x0600AA3B RID: 43579 RVA: 0x00354C2D File Offset: 0x00352E2D
		public override void Reset()
		{
			this.vector3Variable = null;
			this.multiplyBy = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600AA3C RID: 43580 RVA: 0x00354C4D File Offset: 0x00352E4D
		public override void OnEnter()
		{
			this.vector3Variable.Value = this.vector3Variable.Value * this.multiplyBy.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA3D RID: 43581 RVA: 0x00354C83 File Offset: 0x00352E83
		public override void OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Variable.Value * this.multiplyBy.Value;
		}

		// Token: 0x040090D7 RID: 37079
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090D8 RID: 37080
		[RequiredField]
		public FsmFloat multiplyBy;

		// Token: 0x040090D9 RID: 37081
		public bool everyFrame;
	}
}
