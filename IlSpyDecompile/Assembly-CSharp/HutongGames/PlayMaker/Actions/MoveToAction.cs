using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Instantly moves an object to a destination object's position or to a specified position vector.")]
	public class MoveToAction : FsmStateAction
	{
		public FsmOwnerDefault m_GameObject;

		[Tooltip("Move to a destination object's position.")]
		public FsmGameObject m_DestinationObject;

		[Tooltip("Move to a specific position vector. If Destination Object is defined, this is used as an offset.")]
		public FsmVector3 m_VectorPosition;

		[Tooltip("Whether Vector Position is in local or world space.")]
		public Space m_Space;

		public override void Reset()
		{
			m_GameObject = null;
			m_DestinationObject = new FsmGameObject
			{
				UseVariable = true
			};
			m_VectorPosition = new FsmVector3
			{
				UseVariable = true
			};
			m_Space = Space.World;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				SetPosition(ownerDefaultTarget.transform);
			}
			Finish();
		}

		private void SetPosition(Transform source)
		{
			Vector3 vector = (m_VectorPosition.IsNone ? Vector3.zero : m_VectorPosition.Value);
			if (!m_DestinationObject.IsNone && m_DestinationObject.Value != null)
			{
				Transform transform = m_DestinationObject.Value.transform;
				source.position = transform.position;
				if (m_Space == Space.World)
				{
					source.position += vector;
				}
				else
				{
					source.localPosition += vector;
				}
			}
			else if (m_Space == Space.World)
			{
				source.position = vector;
			}
			else
			{
				source.localPosition = vector;
			}
		}
	}
}
