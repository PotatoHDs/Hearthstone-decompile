using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C84 RID: 3204
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Scale of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
	public class GetScale : FsmStateAction
	{
		// Token: 0x06009FD4 RID: 40916 RVA: 0x0032F62B File Offset: 0x0032D82B
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.xScale = null;
			this.yScale = null;
			this.zScale = null;
			this.space = Space.World;
			this.everyFrame = false;
		}

		// Token: 0x06009FD5 RID: 40917 RVA: 0x0032F65E File Offset: 0x0032D85E
		public override void OnEnter()
		{
			this.DoGetScale();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FD6 RID: 40918 RVA: 0x0032F674 File Offset: 0x0032D874
		public override void OnUpdate()
		{
			this.DoGetScale();
		}

		// Token: 0x06009FD7 RID: 40919 RVA: 0x0032F67C File Offset: 0x0032D87C
		private void DoGetScale()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = (this.space == Space.World) ? ownerDefaultTarget.transform.lossyScale : ownerDefaultTarget.transform.localScale;
			this.vector.Value = vector;
			this.xScale.Value = vector.x;
			this.yScale.Value = vector.y;
			this.zScale.Value = vector.z;
		}

		// Token: 0x0400855F RID: 34143
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008560 RID: 34144
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		// Token: 0x04008561 RID: 34145
		[UIHint(UIHint.Variable)]
		public FsmFloat xScale;

		// Token: 0x04008562 RID: 34146
		[UIHint(UIHint.Variable)]
		public FsmFloat yScale;

		// Token: 0x04008563 RID: 34147
		[UIHint(UIHint.Variable)]
		public FsmFloat zScale;

		// Token: 0x04008564 RID: 34148
		public Space space;

		// Token: 0x04008565 RID: 34149
		public bool everyFrame;
	}
}
