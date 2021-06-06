using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA3 RID: 3747
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Sets the value of a Vector2 Variable.")]
	public class SetVector2Value : FsmStateAction
	{
		// Token: 0x0600A9CD RID: 43469 RVA: 0x003537F5 File Offset: 0x003519F5
		public override void Reset()
		{
			this.vector2Variable = null;
			this.vector2Value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9CE RID: 43470 RVA: 0x0035380C File Offset: 0x00351A0C
		public override void OnEnter()
		{
			this.vector2Variable.Value = this.vector2Value.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9CF RID: 43471 RVA: 0x00353832 File Offset: 0x00351A32
		public override void OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Value.Value;
		}

		// Token: 0x04009070 RID: 36976
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009071 RID: 36977
		[RequiredField]
		[Tooltip("The vector2 source")]
		public FsmVector2 vector2Value;

		// Token: 0x04009072 RID: 36978
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
