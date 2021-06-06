using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EBD RID: 3773
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Normalizes a Vector3 Variable.")]
	public class Vector3Normalize : FsmStateAction
	{
		// Token: 0x0600AA3F RID: 43583 RVA: 0x00354CAB File Offset: 0x00352EAB
		public override void Reset()
		{
			this.vector3Variable = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA40 RID: 43584 RVA: 0x00354CBC File Offset: 0x00352EBC
		public override void OnEnter()
		{
			this.vector3Variable.Value = this.vector3Variable.Value.normalized;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA41 RID: 43585 RVA: 0x00354CF8 File Offset: 0x00352EF8
		public override void OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Variable.Value.normalized;
		}

		// Token: 0x040090DA RID: 37082
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090DB RID: 37083
		public bool everyFrame;
	}
}
