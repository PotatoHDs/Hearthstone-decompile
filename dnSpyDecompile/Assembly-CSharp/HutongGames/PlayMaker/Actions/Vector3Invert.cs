using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB9 RID: 3769
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Reverses the direction of a Vector3 Variable. Same as multiplying by -1.")]
	public class Vector3Invert : FsmStateAction
	{
		// Token: 0x0600AA2E RID: 43566 RVA: 0x003549E3 File Offset: 0x00352BE3
		public override void Reset()
		{
			this.vector3Variable = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA2F RID: 43567 RVA: 0x003549F3 File Offset: 0x00352BF3
		public override void OnEnter()
		{
			this.vector3Variable.Value = this.vector3Variable.Value * -1f;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA30 RID: 43568 RVA: 0x00354A23 File Offset: 0x00352C23
		public override void OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Variable.Value * -1f;
		}

		// Token: 0x040090CD RID: 37069
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090CE RID: 37070
		public bool everyFrame;
	}
}
