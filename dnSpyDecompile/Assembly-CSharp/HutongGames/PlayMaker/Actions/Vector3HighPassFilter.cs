using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB7 RID: 3767
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Use a high pass filter to isolate sudden changes in a Vector3 Variable. Useful when working with Get Device Acceleration to remove the constant effect of gravity.")]
	public class Vector3HighPassFilter : FsmStateAction
	{
		// Token: 0x0600AA26 RID: 43558 RVA: 0x003546F3 File Offset: 0x003528F3
		public override void Reset()
		{
			this.vector3Variable = null;
			this.filteringFactor = 0.1f;
		}

		// Token: 0x0600AA27 RID: 43559 RVA: 0x0035470C File Offset: 0x0035290C
		public override void OnEnter()
		{
			this.filteredVector = new Vector3(this.vector3Variable.Value.x, this.vector3Variable.Value.y, this.vector3Variable.Value.z);
		}

		// Token: 0x0600AA28 RID: 43560 RVA: 0x0035474C File Offset: 0x0035294C
		public override void OnUpdate()
		{
			this.filteredVector.x = this.vector3Variable.Value.x - (this.vector3Variable.Value.x * this.filteringFactor.Value + this.filteredVector.x * (1f - this.filteringFactor.Value));
			this.filteredVector.y = this.vector3Variable.Value.y - (this.vector3Variable.Value.y * this.filteringFactor.Value + this.filteredVector.y * (1f - this.filteringFactor.Value));
			this.filteredVector.z = this.vector3Variable.Value.z - (this.vector3Variable.Value.z * this.filteringFactor.Value + this.filteredVector.z * (1f - this.filteringFactor.Value));
			this.vector3Variable.Value = new Vector3(this.filteredVector.x, this.filteredVector.y, this.filteredVector.z);
		}

		// Token: 0x040090C1 RID: 37057
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Vector3 Variable to filter. Should generally come from some constantly updated input, e.g., acceleration.")]
		public FsmVector3 vector3Variable;

		// Token: 0x040090C2 RID: 37058
		[Tooltip("Determines how much influence new changes have.")]
		public FsmFloat filteringFactor;

		// Token: 0x040090C3 RID: 37059
		private Vector3 filteredVector;
	}
}
