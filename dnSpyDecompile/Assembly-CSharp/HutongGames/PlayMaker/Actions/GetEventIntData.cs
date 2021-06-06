using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E0B RID: 3595
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the Int data from the last Event.")]
	public class GetEventIntData : FsmStateAction
	{
		// Token: 0x0600A6FF RID: 42751 RVA: 0x0034B38B File Offset: 0x0034958B
		public override void Reset()
		{
			this.getIntData = null;
		}

		// Token: 0x0600A700 RID: 42752 RVA: 0x0034B394 File Offset: 0x00349594
		public override void OnEnter()
		{
			this.getIntData.Value = Fsm.EventData.IntData;
			base.Finish();
		}

		// Token: 0x04008D87 RID: 36231
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the int data in a variable.")]
		public FsmInt getIntData;
	}
}
