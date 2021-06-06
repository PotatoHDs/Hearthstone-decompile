using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D03 RID: 3331
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last joint break 2D event.")]
	public class GetJointBreak2dInfo : FsmStateAction
	{
		// Token: 0x0600A20E RID: 41486 RVA: 0x0033A02F File Offset: 0x0033822F
		public override void Reset()
		{
			this.brokenJoint = null;
			this.reactionForce = null;
			this.reactionTorque = null;
		}

		// Token: 0x0600A20F RID: 41487 RVA: 0x0033A048 File Offset: 0x00338248
		private void StoreInfo()
		{
			if (base.Fsm.BrokenJoint2D == null)
			{
				return;
			}
			this.brokenJoint.Value = base.Fsm.BrokenJoint2D;
			this.reactionForce.Value = base.Fsm.BrokenJoint2D.reactionForce;
			this.reactionForceMagnitude.Value = base.Fsm.BrokenJoint2D.reactionForce.magnitude;
			this.reactionTorque.Value = base.Fsm.BrokenJoint2D.reactionTorque;
		}

		// Token: 0x0600A210 RID: 41488 RVA: 0x0033A0D8 File Offset: 0x003382D8
		public override void OnEnter()
		{
			this.StoreInfo();
			base.Finish();
		}

		// Token: 0x04008814 RID: 34836
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Joint2D))]
		[Tooltip("Get the broken joint.")]
		public FsmObject brokenJoint;

		// Token: 0x04008815 RID: 34837
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the reaction force exerted by the broken joint. Unity 5.3+")]
		public FsmVector2 reactionForce;

		// Token: 0x04008816 RID: 34838
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the magnitude of the reaction force exerted by the broken joint. Unity 5.3+")]
		public FsmFloat reactionForceMagnitude;

		// Token: 0x04008817 RID: 34839
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the reaction torque exerted by the broken joint. Unity 5.3+")]
		public FsmFloat reactionTorque;
	}
}
