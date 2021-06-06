using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA8 RID: 3752
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Use a high pass filter to isolate sudden changes in a Vector2 Variable.")]
	public class Vector2HighPassFilter : FsmStateAction
	{
		// Token: 0x0600A9E5 RID: 43493 RVA: 0x00353AFC File Offset: 0x00351CFC
		public override void Reset()
		{
			this.vector2Variable = null;
			this.filteringFactor = 0.1f;
		}

		// Token: 0x0600A9E6 RID: 43494 RVA: 0x00353B15 File Offset: 0x00351D15
		public override void OnEnter()
		{
			this.filteredVector = new Vector2(this.vector2Variable.Value.x, this.vector2Variable.Value.y);
		}

		// Token: 0x0600A9E7 RID: 43495 RVA: 0x00353B44 File Offset: 0x00351D44
		public override void OnUpdate()
		{
			this.filteredVector.x = this.vector2Variable.Value.x - (this.vector2Variable.Value.x * this.filteringFactor.Value + this.filteredVector.x * (1f - this.filteringFactor.Value));
			this.filteredVector.y = this.vector2Variable.Value.y - (this.vector2Variable.Value.y * this.filteringFactor.Value + this.filteredVector.y * (1f - this.filteringFactor.Value));
			this.vector2Variable.Value = new Vector2(this.filteredVector.x, this.filteredVector.y);
		}

		// Token: 0x04009084 RID: 36996
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Vector2 Variable to filter. Should generally come from some constantly updated input.")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009085 RID: 36997
		[Tooltip("Determines how much influence new changes have.")]
		public FsmFloat filteringFactor;

		// Token: 0x04009086 RID: 36998
		private Vector2 filteredVector;
	}
}
