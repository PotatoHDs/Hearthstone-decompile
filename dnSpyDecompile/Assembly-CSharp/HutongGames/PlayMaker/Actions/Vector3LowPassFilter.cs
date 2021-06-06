using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EBB RID: 3771
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a Vector3 Variable. Useful when working with Get Device Acceleration to isolate gravity.")]
	public class Vector3LowPassFilter : FsmStateAction
	{
		// Token: 0x0600AA37 RID: 43575 RVA: 0x00354ACA File Offset: 0x00352CCA
		public override void Reset()
		{
			this.vector3Variable = null;
			this.filteringFactor = 0.1f;
		}

		// Token: 0x0600AA38 RID: 43576 RVA: 0x00354AE3 File Offset: 0x00352CE3
		public override void OnEnter()
		{
			this.filteredVector = new Vector3(this.vector3Variable.Value.x, this.vector3Variable.Value.y, this.vector3Variable.Value.z);
		}

		// Token: 0x0600AA39 RID: 43577 RVA: 0x00354B20 File Offset: 0x00352D20
		public override void OnUpdate()
		{
			this.filteredVector.x = this.vector3Variable.Value.x * this.filteringFactor.Value + this.filteredVector.x * (1f - this.filteringFactor.Value);
			this.filteredVector.y = this.vector3Variable.Value.y * this.filteringFactor.Value + this.filteredVector.y * (1f - this.filteringFactor.Value);
			this.filteredVector.z = this.vector3Variable.Value.z * this.filteringFactor.Value + this.filteredVector.z * (1f - this.filteringFactor.Value);
			this.vector3Variable.Value = new Vector3(this.filteredVector.x, this.filteredVector.y, this.filteredVector.z);
		}

		// Token: 0x040090D4 RID: 37076
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Vector3 Variable to filter. Should generally come from some constantly updated input, e.g., acceleration.")]
		public FsmVector3 vector3Variable;

		// Token: 0x040090D5 RID: 37077
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered vector and 90 percent of the previously filtered value.")]
		public FsmFloat filteringFactor;

		// Token: 0x040090D6 RID: 37078
		private Vector3 filteredVector;
	}
}
