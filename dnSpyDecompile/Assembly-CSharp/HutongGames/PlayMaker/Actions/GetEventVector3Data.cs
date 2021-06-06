using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E0F RID: 3599
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the Vector3 data from the last Event.")]
	public class GetEventVector3Data : FsmStateAction
	{
		// Token: 0x0600A70B RID: 42763 RVA: 0x0034B4DC File Offset: 0x003496DC
		public override void Reset()
		{
			this.getVector3Data = null;
		}

		// Token: 0x0600A70C RID: 42764 RVA: 0x0034B4E5 File Offset: 0x003496E5
		public override void OnEnter()
		{
			this.getVector3Data.Value = Fsm.EventData.Vector3Data;
			base.Finish();
		}

		// Token: 0x04008D8D RID: 36237
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the vector3 data in a variable.")]
		public FsmVector3 getVector3Data;
	}
}
