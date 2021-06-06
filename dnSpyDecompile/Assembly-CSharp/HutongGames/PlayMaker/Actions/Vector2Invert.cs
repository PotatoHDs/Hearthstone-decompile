using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EAA RID: 3754
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Reverses the direction of a Vector2 Variable. Same as multiplying by -1.")]
	public class Vector2Invert : FsmStateAction
	{
		// Token: 0x0600A9ED RID: 43501 RVA: 0x00353D7B File Offset: 0x00351F7B
		public override void Reset()
		{
			this.vector2Variable = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9EE RID: 43502 RVA: 0x00353D8B File Offset: 0x00351F8B
		public override void OnEnter()
		{
			this.vector2Variable.Value = this.vector2Variable.Value * -1f;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9EF RID: 43503 RVA: 0x00353DBB File Offset: 0x00351FBB
		public override void OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Variable.Value * -1f;
		}

		// Token: 0x04009090 RID: 37008
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector to invert")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009091 RID: 37009
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
