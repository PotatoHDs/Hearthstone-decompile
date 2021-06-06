using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CFB RID: 3323
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last joint break event.")]
	public class GetJointBreakInfo : FsmStateAction
	{
		// Token: 0x0600A1D9 RID: 41433 RVA: 0x003394C7 File Offset: 0x003376C7
		public override void Reset()
		{
			this.breakForce = null;
		}

		// Token: 0x0600A1DA RID: 41434 RVA: 0x003394D0 File Offset: 0x003376D0
		public override void OnEnter()
		{
			this.breakForce.Value = base.Fsm.JointBreakForce;
			base.Finish();
		}

		// Token: 0x040087EC RID: 34796
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the force that broke the joint.")]
		public FsmFloat breakForce;
	}
}
