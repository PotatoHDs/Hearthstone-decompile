using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C7B RID: 3195
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Position of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
	public class GetPosition : FsmStateAction
	{
		// Token: 0x06009FAC RID: 40876 RVA: 0x0032F02D File Offset: 0x0032D22D
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = null;
			this.y = null;
			this.z = null;
			this.space = Space.World;
			this.everyFrame = false;
		}

		// Token: 0x06009FAD RID: 40877 RVA: 0x0032F060 File Offset: 0x0032D260
		public override void OnEnter()
		{
			this.DoGetPosition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FAE RID: 40878 RVA: 0x0032F076 File Offset: 0x0032D276
		public override void OnUpdate()
		{
			this.DoGetPosition();
		}

		// Token: 0x06009FAF RID: 40879 RVA: 0x0032F080 File Offset: 0x0032D280
		private void DoGetPosition()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = (this.space == Space.World) ? ownerDefaultTarget.transform.position : ownerDefaultTarget.transform.localPosition;
			this.vector.Value = vector;
			this.x.Value = vector.x;
			this.y.Value = vector.y;
			this.z.Value = vector.z;
		}

		// Token: 0x0400853B RID: 34107
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400853C RID: 34108
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		// Token: 0x0400853D RID: 34109
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x0400853E RID: 34110
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x0400853F RID: 34111
		[UIHint(UIHint.Variable)]
		public FsmFloat z;

		// Token: 0x04008540 RID: 34112
		public Space space;

		// Token: 0x04008541 RID: 34113
		public bool everyFrame;
	}
}
