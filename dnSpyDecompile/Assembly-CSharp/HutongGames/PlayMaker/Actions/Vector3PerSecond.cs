using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EBF RID: 3775
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Multiplies a Vector3 variable by Time.deltaTime. Useful for frame rate independent motion.")]
	public class Vector3PerSecond : FsmStateAction
	{
		// Token: 0x0600AA48 RID: 43592 RVA: 0x00354F3F File Offset: 0x0035313F
		public override void Reset()
		{
			this.vector3Variable = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA49 RID: 43593 RVA: 0x00354F4F File Offset: 0x0035314F
		public override void OnEnter()
		{
			this.vector3Variable.Value = this.vector3Variable.Value * Time.deltaTime;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA4A RID: 43594 RVA: 0x00354F7F File Offset: 0x0035317F
		public override void OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Variable.Value * Time.deltaTime;
		}

		// Token: 0x040090E2 RID: 37090
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090E3 RID: 37091
		public bool everyFrame;
	}
}
