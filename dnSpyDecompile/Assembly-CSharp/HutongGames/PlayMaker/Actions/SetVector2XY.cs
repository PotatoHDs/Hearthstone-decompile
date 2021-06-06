using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA4 RID: 3748
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Sets the XY channels of a Vector2 Variable. To leave any channel unchanged, set variable to 'None'.")]
	public class SetVector2XY : FsmStateAction
	{
		// Token: 0x0600A9D1 RID: 43473 RVA: 0x0035384A File Offset: 0x00351A4A
		public override void Reset()
		{
			this.vector2Variable = null;
			this.vector2Value = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A9D2 RID: 43474 RVA: 0x00353885 File Offset: 0x00351A85
		public override void OnEnter()
		{
			this.DoSetVector2XYZ();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9D3 RID: 43475 RVA: 0x0035389B File Offset: 0x00351A9B
		public override void OnUpdate()
		{
			this.DoSetVector2XYZ();
		}

		// Token: 0x0600A9D4 RID: 43476 RVA: 0x003538A4 File Offset: 0x00351AA4
		private void DoSetVector2XYZ()
		{
			if (this.vector2Variable == null)
			{
				return;
			}
			Vector2 value = this.vector2Variable.Value;
			if (!this.vector2Value.IsNone)
			{
				value = this.vector2Value.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this.vector2Variable.Value = value;
		}

		// Token: 0x04009073 RID: 36979
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009074 RID: 36980
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 source")]
		public FsmVector2 vector2Value;

		// Token: 0x04009075 RID: 36981
		[Tooltip("The x component. Override vector2Value if set")]
		public FsmFloat x;

		// Token: 0x04009076 RID: 36982
		[Tooltip("The y component.Override vector2Value if set")]
		public FsmFloat y;

		// Token: 0x04009077 RID: 36983
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
