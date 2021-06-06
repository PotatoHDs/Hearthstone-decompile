using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB6 RID: 3766
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Clamps the Magnitude of Vector3 Variable.")]
	public class Vector3ClampMagnitude : FsmStateAction
	{
		// Token: 0x0600AA21 RID: 43553 RVA: 0x00354696 File Offset: 0x00352896
		public override void Reset()
		{
			this.vector3Variable = null;
			this.maxLength = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA22 RID: 43554 RVA: 0x003546AD File Offset: 0x003528AD
		public override void OnEnter()
		{
			this.DoVector3ClampMagnitude();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA23 RID: 43555 RVA: 0x003546C3 File Offset: 0x003528C3
		public override void OnUpdate()
		{
			this.DoVector3ClampMagnitude();
		}

		// Token: 0x0600AA24 RID: 43556 RVA: 0x003546CB File Offset: 0x003528CB
		private void DoVector3ClampMagnitude()
		{
			this.vector3Variable.Value = Vector3.ClampMagnitude(this.vector3Variable.Value, this.maxLength.Value);
		}

		// Token: 0x040090BE RID: 37054
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040090BF RID: 37055
		[RequiredField]
		public FsmFloat maxLength;

		// Token: 0x040090C0 RID: 37056
		public bool everyFrame;
	}
}
