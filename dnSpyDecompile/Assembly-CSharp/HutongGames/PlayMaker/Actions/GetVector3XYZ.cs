using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C94 RID: 3220
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Get the XYZ channels of a Vector3 Variable and store them in Float Variables.")]
	public class GetVector3XYZ : FsmStateAction
	{
		// Token: 0x0600A01C RID: 40988 RVA: 0x0033002A File Offset: 0x0032E22A
		public override void Reset()
		{
			this.vector3Variable = null;
			this.storeX = null;
			this.storeY = null;
			this.storeZ = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A01D RID: 40989 RVA: 0x0033004F File Offset: 0x0032E24F
		public override void OnEnter()
		{
			this.DoGetVector3XYZ();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A01E RID: 40990 RVA: 0x00330065 File Offset: 0x0032E265
		public override void OnUpdate()
		{
			this.DoGetVector3XYZ();
		}

		// Token: 0x0600A01F RID: 40991 RVA: 0x00330070 File Offset: 0x0032E270
		private void DoGetVector3XYZ()
		{
			if (this.vector3Variable == null)
			{
				return;
			}
			if (this.storeX != null)
			{
				this.storeX.Value = this.vector3Variable.Value.x;
			}
			if (this.storeY != null)
			{
				this.storeY.Value = this.vector3Variable.Value.y;
			}
			if (this.storeZ != null)
			{
				this.storeZ.Value = this.vector3Variable.Value.z;
			}
		}

		// Token: 0x0400859A RID: 34202
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x0400859B RID: 34203
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;

		// Token: 0x0400859C RID: 34204
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;

		// Token: 0x0400859D RID: 34205
		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;

		// Token: 0x0400859E RID: 34206
		public bool everyFrame;
	}
}
