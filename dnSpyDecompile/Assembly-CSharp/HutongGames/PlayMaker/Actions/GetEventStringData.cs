using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E0D RID: 3597
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the String data from the last Event.")]
	public class GetEventStringData : FsmStateAction
	{
		// Token: 0x0600A705 RID: 42757 RVA: 0x0034B490 File Offset: 0x00349690
		public override void Reset()
		{
			this.getStringData = null;
		}

		// Token: 0x0600A706 RID: 42758 RVA: 0x0034B499 File Offset: 0x00349699
		public override void OnEnter()
		{
			this.getStringData.Value = Fsm.EventData.StringData;
			base.Finish();
		}

		// Token: 0x04008D8B RID: 36235
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the string data in a variable.")]
		public FsmString getStringData;
	}
}
