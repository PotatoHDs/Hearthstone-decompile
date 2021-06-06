using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EAC RID: 3756
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a Vector2 Variable.")]
	public class Vector2LowPassFilter : FsmStateAction
	{
		// Token: 0x0600A9F6 RID: 43510 RVA: 0x00353E62 File Offset: 0x00352062
		public override void Reset()
		{
			this.vector2Variable = null;
			this.filteringFactor = 0.1f;
		}

		// Token: 0x0600A9F7 RID: 43511 RVA: 0x00353E7B File Offset: 0x0035207B
		public override void OnEnter()
		{
			this.filteredVector = new Vector2(this.vector2Variable.Value.x, this.vector2Variable.Value.y);
		}

		// Token: 0x0600A9F8 RID: 43512 RVA: 0x00353EA8 File Offset: 0x003520A8
		public override void OnUpdate()
		{
			this.filteredVector.x = this.vector2Variable.Value.x * this.filteringFactor.Value + this.filteredVector.x * (1f - this.filteringFactor.Value);
			this.filteredVector.y = this.vector2Variable.Value.y * this.filteringFactor.Value + this.filteredVector.y * (1f - this.filteringFactor.Value);
			this.vector2Variable.Value = new Vector2(this.filteredVector.x, this.filteredVector.y);
		}

		// Token: 0x04009097 RID: 37015
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Vector2 Variable to filter. Should generally come from some constantly updated input")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009098 RID: 37016
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered vector and 90 percent of the previously filtered value")]
		public FsmFloat filteringFactor;

		// Token: 0x04009099 RID: 37017
		private Vector2 filteredVector;
	}
}
