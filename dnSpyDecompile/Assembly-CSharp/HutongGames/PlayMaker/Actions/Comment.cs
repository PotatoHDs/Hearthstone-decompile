using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BEE RID: 3054
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Adds a text area to the action list. NOTE: Doesn't do anything, just for notes...")]
	public class Comment : FsmStateAction
	{
		// Token: 0x06009D44 RID: 40260 RVA: 0x00328876 File Offset: 0x00326A76
		public override void Reset()
		{
			this.comment = "";
		}

		// Token: 0x06009D45 RID: 40261 RVA: 0x00328883 File Offset: 0x00326A83
		public override void OnEnter()
		{
			base.Finish();
		}

		// Token: 0x040082B7 RID: 33463
		[UIHint(UIHint.Comment)]
		public string comment;
	}
}
