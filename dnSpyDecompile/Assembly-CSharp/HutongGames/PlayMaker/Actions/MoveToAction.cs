using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F57 RID: 3927
	[ActionCategory("Pegasus")]
	[Tooltip("Instantly moves an object to a destination object's position or to a specified position vector.")]
	public class MoveToAction : FsmStateAction
	{
		// Token: 0x0600ACEC RID: 44268 RVA: 0x0035F3B7 File Offset: 0x0035D5B7
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_DestinationObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_VectorPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.m_Space = Space.World;
		}

		// Token: 0x0600ACED RID: 44269 RVA: 0x0035F3EC File Offset: 0x0035D5EC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				this.SetPosition(ownerDefaultTarget.transform);
			}
			base.Finish();
		}

		// Token: 0x0600ACEE RID: 44270 RVA: 0x0035F428 File Offset: 0x0035D628
		private void SetPosition(Transform source)
		{
			Vector3 vector = this.m_VectorPosition.IsNone ? Vector3.zero : this.m_VectorPosition.Value;
			if (!this.m_DestinationObject.IsNone && this.m_DestinationObject.Value != null)
			{
				Transform transform = this.m_DestinationObject.Value.transform;
				source.position = transform.position;
				if (this.m_Space == Space.World)
				{
					source.position += vector;
					return;
				}
				source.localPosition += vector;
				return;
			}
			else
			{
				if (this.m_Space == Space.World)
				{
					source.position = vector;
					return;
				}
				source.localPosition = vector;
				return;
			}
		}

		// Token: 0x040093CB RID: 37835
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093CC RID: 37836
		[Tooltip("Move to a destination object's position.")]
		public FsmGameObject m_DestinationObject;

		// Token: 0x040093CD RID: 37837
		[Tooltip("Move to a specific position vector. If Destination Object is defined, this is used as an offset.")]
		public FsmVector3 m_VectorPosition;

		// Token: 0x040093CE RID: 37838
		[Tooltip("Whether Vector Position is in local or world space.")]
		public Space m_Space;
	}
}
