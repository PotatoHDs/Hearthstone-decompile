using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Play a Particle System. mschweitzer: I think this is equivalent to Simulate with a 1.0 speed.")]
	public class ParticlePlayAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;

		public override void Reset()
		{
			m_GameObject = null;
			m_IncludeChildren = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			ParticleSystem component = ownerDefaultTarget.GetComponent<ParticleSystem>();
			if (component == null && !m_IncludeChildren.Value)
			{
				Debug.LogWarning($"ParticlePlayAction.OnEnter() - {ownerDefaultTarget} has no ParticleSystem component. Owner={base.Owner}");
				Finish();
			}
			else if (component == null && m_IncludeChildren.Value)
			{
				ParticleSystem[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Play(m_IncludeChildren.Value);
				}
				Finish();
			}
			else
			{
				component.Play(m_IncludeChildren.Value);
				Finish();
			}
		}
	}
}
