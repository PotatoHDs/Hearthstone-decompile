using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA0 RID: 3744
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Get Vector2 Length.")]
	public class GetVector2Length : FsmStateAction
	{
		// Token: 0x0600A9BF RID: 43455 RVA: 0x00353633 File Offset: 0x00351833
		public override void Reset()
		{
			this.vector2 = null;
			this.storeLength = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9C0 RID: 43456 RVA: 0x0035364A File Offset: 0x0035184A
		public override void OnEnter()
		{
			this.DoVectorLength();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9C1 RID: 43457 RVA: 0x00353660 File Offset: 0x00351860
		public override void OnUpdate()
		{
			this.DoVectorLength();
		}

		// Token: 0x0600A9C2 RID: 43458 RVA: 0x00353668 File Offset: 0x00351868
		private void DoVectorLength()
		{
			if (this.vector2 == null)
			{
				return;
			}
			if (this.storeLength == null)
			{
				return;
			}
			this.storeLength.Value = this.vector2.Value.magnitude;
		}

		// Token: 0x04009066 RID: 36966
		[Tooltip("The Vector2 to get the length from")]
		public FsmVector2 vector2;

		// Token: 0x04009067 RID: 36967
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 the length")]
		public FsmFloat storeLength;

		// Token: 0x04009068 RID: 36968
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
