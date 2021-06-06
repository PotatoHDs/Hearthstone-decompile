using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Copies a game object's transform to another game object.")]
	public class CopyTransformAction : FsmStateAction
	{
		[RequiredField]
		public FsmGameObject m_SourceObject;

		[RequiredField]
		public FsmGameObject m_TargetObject;

		public FsmBool m_Position;

		public FsmBool m_Rotation;

		public FsmBool m_Scale;

		[Tooltip("Copies the transform in world space if checked, otherwise copies in local space.")]
		public FsmBool m_WorldSpace;

		public override void Reset()
		{
			m_SourceObject = new FsmGameObject
			{
				UseVariable = true
			};
			m_TargetObject = new FsmGameObject
			{
				UseVariable = true
			};
			m_Position = true;
			m_Rotation = true;
			m_Scale = true;
			m_WorldSpace = true;
		}

		public override void OnEnter()
		{
			if (m_SourceObject == null || m_SourceObject.IsNone || !m_SourceObject.Value || m_TargetObject == null || m_TargetObject.IsNone || !m_TargetObject.Value)
			{
				Finish();
				return;
			}
			Transform transform = m_SourceObject.Value.transform;
			Transform transform2 = m_TargetObject.Value.transform;
			if (m_WorldSpace.Value)
			{
				if (m_Position.Value)
				{
					transform2.position = transform.position;
				}
				if (m_Rotation.Value)
				{
					transform2.rotation = transform.rotation;
				}
				if (m_Scale.Value)
				{
					TransformUtil.CopyWorldScale(transform2, transform);
				}
			}
			else
			{
				if (m_Position.Value)
				{
					transform2.localPosition = transform.localPosition;
				}
				if (m_Rotation.Value)
				{
					transform2.localRotation = transform.localRotation;
				}
				if (m_Scale.Value)
				{
					transform2.localScale = transform.localScale;
				}
			}
			Finish();
		}
	}
}
