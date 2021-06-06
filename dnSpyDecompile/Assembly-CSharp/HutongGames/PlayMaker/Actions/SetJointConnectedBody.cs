using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DDB RID: 3547
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Connect a joint to a game object.")]
	public class SetJointConnectedBody : FsmStateAction
	{
		// Token: 0x0600A626 RID: 42534 RVA: 0x00348866 File Offset: 0x00346A66
		public override void Reset()
		{
			this.joint = null;
			this.rigidBody = null;
		}

		// Token: 0x0600A627 RID: 42535 RVA: 0x00348878 File Offset: 0x00346A78
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.joint);
			if (ownerDefaultTarget != null)
			{
				Joint component = ownerDefaultTarget.GetComponent<Joint>();
				if (component != null)
				{
					component.connectedBody = ((this.rigidBody.Value == null) ? null : this.rigidBody.Value.GetComponent<Rigidbody>());
				}
			}
			base.Finish();
		}

		// Token: 0x04008CBD RID: 36029
		[RequiredField]
		[CheckForComponent(typeof(Joint))]
		[Tooltip("The joint to connect. Requires a Joint component.")]
		public FsmOwnerDefault joint;

		// Token: 0x04008CBE RID: 36030
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The game object to connect to the Joint. Set to none to connect the Joint to the world.")]
		public FsmGameObject rigidBody;
	}
}
