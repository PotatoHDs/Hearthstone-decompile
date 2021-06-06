using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C83 RID: 3203
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Rotation of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
	public class GetRotation : FsmStateAction
	{
		// Token: 0x06009FCF RID: 40911 RVA: 0x0032F4E4 File Offset: 0x0032D6E4
		public override void Reset()
		{
			this.gameObject = null;
			this.quaternion = null;
			this.vector = null;
			this.xAngle = null;
			this.yAngle = null;
			this.zAngle = null;
			this.space = Space.World;
			this.everyFrame = false;
		}

		// Token: 0x06009FD0 RID: 40912 RVA: 0x0032F51E File Offset: 0x0032D71E
		public override void OnEnter()
		{
			this.DoGetRotation();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FD1 RID: 40913 RVA: 0x0032F534 File Offset: 0x0032D734
		public override void OnUpdate()
		{
			this.DoGetRotation();
		}

		// Token: 0x06009FD2 RID: 40914 RVA: 0x0032F53C File Offset: 0x0032D73C
		private void DoGetRotation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (this.space == Space.World)
			{
				this.quaternion.Value = ownerDefaultTarget.transform.rotation;
				Vector3 eulerAngles = ownerDefaultTarget.transform.eulerAngles;
				this.vector.Value = eulerAngles;
				this.xAngle.Value = eulerAngles.x;
				this.yAngle.Value = eulerAngles.y;
				this.zAngle.Value = eulerAngles.z;
				return;
			}
			Vector3 localEulerAngles = ownerDefaultTarget.transform.localEulerAngles;
			this.quaternion.Value = Quaternion.Euler(localEulerAngles);
			this.vector.Value = localEulerAngles;
			this.xAngle.Value = localEulerAngles.x;
			this.yAngle.Value = localEulerAngles.y;
			this.zAngle.Value = localEulerAngles.z;
		}

		// Token: 0x04008557 RID: 34135
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008558 RID: 34136
		[UIHint(UIHint.Variable)]
		public FsmQuaternion quaternion;

		// Token: 0x04008559 RID: 34137
		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		public FsmVector3 vector;

		// Token: 0x0400855A RID: 34138
		[UIHint(UIHint.Variable)]
		public FsmFloat xAngle;

		// Token: 0x0400855B RID: 34139
		[UIHint(UIHint.Variable)]
		public FsmFloat yAngle;

		// Token: 0x0400855C RID: 34140
		[UIHint(UIHint.Variable)]
		public FsmFloat zAngle;

		// Token: 0x0400855D RID: 34141
		public Space space;

		// Token: 0x0400855E RID: 34142
		public bool everyFrame;
	}
}
