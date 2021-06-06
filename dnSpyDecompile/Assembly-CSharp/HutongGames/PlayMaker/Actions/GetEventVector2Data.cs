using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E0E RID: 3598
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the Vector2 data from the last Event.")]
	public class GetEventVector2Data : FsmStateAction
	{
		// Token: 0x0600A708 RID: 42760 RVA: 0x0034B4B6 File Offset: 0x003496B6
		public override void Reset()
		{
			this.getVector2Data = null;
		}

		// Token: 0x0600A709 RID: 42761 RVA: 0x0034B4BF File Offset: 0x003496BF
		public override void OnEnter()
		{
			this.getVector2Data.Value = Fsm.EventData.Vector2Data;
			base.Finish();
		}

		// Token: 0x04008D8C RID: 36236
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the vector2 data in a variable.")]
		public FsmVector2 getVector2Data;
	}
}
