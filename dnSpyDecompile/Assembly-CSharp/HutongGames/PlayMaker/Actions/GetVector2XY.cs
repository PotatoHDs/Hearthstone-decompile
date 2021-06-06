using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA1 RID: 3745
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Get the XY channels of a Vector2 Variable and store them in Float Variables.")]
	public class GetVector2XY : FsmStateAction
	{
		// Token: 0x0600A9C4 RID: 43460 RVA: 0x003536A5 File Offset: 0x003518A5
		public override void Reset()
		{
			this.vector2Variable = null;
			this.storeX = null;
			this.storeY = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9C5 RID: 43461 RVA: 0x003536C3 File Offset: 0x003518C3
		public override void OnEnter()
		{
			this.DoGetVector2XYZ();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9C6 RID: 43462 RVA: 0x003536D9 File Offset: 0x003518D9
		public override void OnUpdate()
		{
			this.DoGetVector2XYZ();
		}

		// Token: 0x0600A9C7 RID: 43463 RVA: 0x003536E4 File Offset: 0x003518E4
		private void DoGetVector2XYZ()
		{
			if (this.vector2Variable == null)
			{
				return;
			}
			if (this.storeX != null)
			{
				this.storeX.Value = this.vector2Variable.Value.x;
			}
			if (this.storeY != null)
			{
				this.storeY.Value = this.vector2Variable.Value.y;
			}
		}

		// Token: 0x04009069 RID: 36969
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 source")]
		public FsmVector2 vector2Variable;

		// Token: 0x0400906A RID: 36970
		[UIHint(UIHint.Variable)]
		[Tooltip("The x component")]
		public FsmFloat storeX;

		// Token: 0x0400906B RID: 36971
		[UIHint(UIHint.Variable)]
		[Tooltip("The y component")]
		public FsmFloat storeY;

		// Token: 0x0400906C RID: 36972
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
