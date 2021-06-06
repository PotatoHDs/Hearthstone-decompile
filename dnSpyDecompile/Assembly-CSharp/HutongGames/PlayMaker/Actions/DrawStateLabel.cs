using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C1A RID: 3098
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Draws a state label for this FSM in the Game View. The label is drawn on the game object that owns the FSM. Use this to override the global setting in the PlayMaker Debug menu.")]
	public class DrawStateLabel : FsmStateAction
	{
		// Token: 0x06009DF8 RID: 40440 RVA: 0x0032A344 File Offset: 0x00328544
		public override void Reset()
		{
			this.showLabel = true;
		}

		// Token: 0x06009DF9 RID: 40441 RVA: 0x0032A352 File Offset: 0x00328552
		public override void OnEnter()
		{
			base.Fsm.ShowStateLabel = this.showLabel.Value;
			base.Finish();
		}

		// Token: 0x0400834D RID: 33613
		[RequiredField]
		[Tooltip("Set to True to show State labels, or False to hide them.")]
		public FsmBool showLabel;
	}
}
