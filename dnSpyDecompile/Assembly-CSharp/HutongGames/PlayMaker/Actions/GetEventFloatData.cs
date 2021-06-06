using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E0A RID: 3594
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the Float data from the last Event.")]
	public class GetEventFloatData : FsmStateAction
	{
		// Token: 0x0600A6FC RID: 42748 RVA: 0x0034B365 File Offset: 0x00349565
		public override void Reset()
		{
			this.getFloatData = null;
		}

		// Token: 0x0600A6FD RID: 42749 RVA: 0x0034B36E File Offset: 0x0034956E
		public override void OnEnter()
		{
			this.getFloatData.Value = Fsm.EventData.FloatData;
			base.Finish();
		}

		// Token: 0x04008D86 RID: 36230
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the float data in a variable.")]
		public FsmFloat getFloatData;
	}
}
