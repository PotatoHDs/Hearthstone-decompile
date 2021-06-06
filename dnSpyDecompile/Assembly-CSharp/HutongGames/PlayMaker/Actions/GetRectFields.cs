using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C81 RID: 3201
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Get the individual fields of a Rect Variable and store them in Float Variables.")]
	public class GetRectFields : FsmStateAction
	{
		// Token: 0x06009FC6 RID: 40902 RVA: 0x0032F39F File Offset: 0x0032D59F
		public override void Reset()
		{
			this.rectVariable = null;
			this.storeX = null;
			this.storeY = null;
			this.storeWidth = null;
			this.storeHeight = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FC7 RID: 40903 RVA: 0x0032F3CB File Offset: 0x0032D5CB
		public override void OnEnter()
		{
			this.DoGetRectFields();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FC8 RID: 40904 RVA: 0x0032F3E1 File Offset: 0x0032D5E1
		public override void OnUpdate()
		{
			this.DoGetRectFields();
		}

		// Token: 0x06009FC9 RID: 40905 RVA: 0x0032F3EC File Offset: 0x0032D5EC
		private void DoGetRectFields()
		{
			if (this.rectVariable.IsNone)
			{
				return;
			}
			this.storeX.Value = this.rectVariable.Value.x;
			this.storeY.Value = this.rectVariable.Value.y;
			this.storeWidth.Value = this.rectVariable.Value.width;
			this.storeHeight.Value = this.rectVariable.Value.height;
		}

		// Token: 0x0400854F RID: 34127
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		// Token: 0x04008550 RID: 34128
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;

		// Token: 0x04008551 RID: 34129
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;

		// Token: 0x04008552 RID: 34130
		[UIHint(UIHint.Variable)]
		public FsmFloat storeWidth;

		// Token: 0x04008553 RID: 34131
		[UIHint(UIHint.Variable)]
		public FsmFloat storeHeight;

		// Token: 0x04008554 RID: 34132
		public bool everyFrame;
	}
}
