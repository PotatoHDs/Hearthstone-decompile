using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF4 RID: 3572
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Sets the individual fields of a Rect Variable. To leave any field unchanged, set variable to 'None'.")]
	public class SetRectFields : FsmStateAction
	{
		// Token: 0x0600A68F RID: 42639 RVA: 0x0034997C File Offset: 0x00347B7C
		public override void Reset()
		{
			this.rectVariable = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.width = new FsmFloat
			{
				UseVariable = true
			};
			this.height = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A690 RID: 42640 RVA: 0x003499DF File Offset: 0x00347BDF
		public override void OnEnter()
		{
			this.DoSetRectFields();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A691 RID: 42641 RVA: 0x003499F5 File Offset: 0x00347BF5
		public override void OnUpdate()
		{
			this.DoSetRectFields();
		}

		// Token: 0x0600A692 RID: 42642 RVA: 0x00349A00 File Offset: 0x00347C00
		private void DoSetRectFields()
		{
			if (this.rectVariable.IsNone)
			{
				return;
			}
			Rect value = this.rectVariable.Value;
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			if (!this.width.IsNone)
			{
				value.width = this.width.Value;
			}
			if (!this.height.IsNone)
			{
				value.height = this.height.Value;
			}
			this.rectVariable.Value = value;
		}

		// Token: 0x04008D10 RID: 36112
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		// Token: 0x04008D11 RID: 36113
		public FsmFloat x;

		// Token: 0x04008D12 RID: 36114
		public FsmFloat y;

		// Token: 0x04008D13 RID: 36115
		public FsmFloat width;

		// Token: 0x04008D14 RID: 36116
		public FsmFloat height;

		// Token: 0x04008D15 RID: 36117
		public bool everyFrame;
	}
}
