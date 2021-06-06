using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C7C RID: 3196
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the name of the previously active state and stores it in a String Variable.")]
	public class GetPreviousStateName : FsmStateAction
	{
		// Token: 0x06009FB1 RID: 40881 RVA: 0x0032F109 File Offset: 0x0032D309
		public override void Reset()
		{
			this.storeName = null;
		}

		// Token: 0x06009FB2 RID: 40882 RVA: 0x0032F112 File Offset: 0x0032D312
		public override void OnEnter()
		{
			this.storeName.Value = ((base.Fsm.PreviousActiveState == null) ? null : base.Fsm.PreviousActiveState.Name);
			base.Finish();
		}

		// Token: 0x04008542 RID: 34114
		[UIHint(UIHint.Variable)]
		public FsmString storeName;
	}
}
