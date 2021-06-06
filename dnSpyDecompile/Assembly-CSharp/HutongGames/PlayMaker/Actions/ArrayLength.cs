using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC2 RID: 3010
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Gets the number of items in an Array.")]
	public class ArrayLength : FsmStateAction
	{
		// Token: 0x06009C79 RID: 40057 RVA: 0x0032570D File Offset: 0x0032390D
		public override void Reset()
		{
			this.array = null;
			this.length = null;
			this.everyFrame = false;
		}

		// Token: 0x06009C7A RID: 40058 RVA: 0x00325724 File Offset: 0x00323924
		public override void OnEnter()
		{
			this.length.Value = this.array.Length;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C7B RID: 40059 RVA: 0x0032574A File Offset: 0x0032394A
		public override void OnUpdate()
		{
			this.length.Value = this.array.Length;
		}

		// Token: 0x040081F3 RID: 33267
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable.")]
		public FsmArray array;

		// Token: 0x040081F4 RID: 33268
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the length in an Int Variable.")]
		public FsmInt length;

		// Token: 0x040081F5 RID: 33269
		[Tooltip("Repeat every frame. Useful if the array is changing and you're waiting for a particular length.")]
		public bool everyFrame;
	}
}
