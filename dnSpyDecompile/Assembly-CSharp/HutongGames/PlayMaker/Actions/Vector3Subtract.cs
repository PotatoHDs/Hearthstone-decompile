using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC1 RID: 3777
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Subtracts a Vector3 value from a Vector3 variable.")]
	public class Vector3Subtract : FsmStateAction
	{
		// Token: 0x0600AA4F RID: 43599 RVA: 0x0035504D File Offset: 0x0035324D
		public override void Reset()
		{
			this.vector3Variable = null;
			this.subtractVector = new FsmVector3
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600AA50 RID: 43600 RVA: 0x0035506F File Offset: 0x0035326F
		public override void OnEnter()
		{
			this.vector3Variable.Value = this.vector3Variable.Value - this.subtractVector.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA51 RID: 43601 RVA: 0x003550A5 File Offset: 0x003532A5
		public override void OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Variable.Value - this.subtractVector.Value;
		}

		// Token: 0x040090E8 RID: 37096
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090E9 RID: 37097
		[RequiredField]
		public FsmVector3 subtractVector;

		// Token: 0x040090EA RID: 37098
		public bool everyFrame;
	}
}
