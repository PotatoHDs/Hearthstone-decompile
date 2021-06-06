using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DEC RID: 3564
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Controls the appearance of Mouse Cursor.")]
	public class SetMouseCursor : FsmStateAction
	{
		// Token: 0x0600A66E RID: 42606 RVA: 0x00349393 File Offset: 0x00347593
		public override void Reset()
		{
			this.cursorTexture = null;
			this.hideCursor = false;
			this.lockCursor = false;
		}

		// Token: 0x0600A66F RID: 42607 RVA: 0x003493B4 File Offset: 0x003475B4
		public override void OnEnter()
		{
			PlayMakerGUI.LockCursor = this.lockCursor.Value;
			PlayMakerGUI.HideCursor = this.hideCursor.Value;
			PlayMakerGUI.MouseCursor = this.cursorTexture.Value;
			base.Finish();
		}

		// Token: 0x04008CF1 RID: 36081
		public FsmTexture cursorTexture;

		// Token: 0x04008CF2 RID: 36082
		public FsmBool hideCursor;

		// Token: 0x04008CF3 RID: 36083
		public FsmBool lockCursor;
	}
}
