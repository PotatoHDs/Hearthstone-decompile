using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Pause a Particle System.")]
	public class ParticlePauseAction : FsmStateAction
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
			if (component == null)
			{
				Debug.LogWarning($"ParticlePauseAction.OnEnter() - GameObject {ownerDefaultTarget} has no ParticleSystem component");
				Finish();
			}
			else
			{
				component.Pause(m_IncludeChildren.Value);
				Finish();
			}
		}
	}
}
