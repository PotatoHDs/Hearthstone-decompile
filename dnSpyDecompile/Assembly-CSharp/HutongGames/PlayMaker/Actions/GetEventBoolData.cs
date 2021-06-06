using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E09 RID: 3593
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the Bool data from the last Event.")]
	public class GetEventBoolData : FsmStateAction
	{
		// Token: 0x0600A6F9 RID: 42745 RVA: 0x0034B33F File Offset: 0x0034953F
		public override void Reset()
		{
			this.getBoolData = null;
		}

		// Token: 0x0600A6FA RID: 42746 RVA: 0x0034B348 File Offset: 0x00349548
		public override void OnEnter()
		{
			this.getBoolData.Value = Fsm.EventData.BoolData;
			base.Finish();
		}

		// Token: 0x04008D85 RID: 36229
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the bool data in a variable.")]
		public FsmBool getBoolData;
	}
}
