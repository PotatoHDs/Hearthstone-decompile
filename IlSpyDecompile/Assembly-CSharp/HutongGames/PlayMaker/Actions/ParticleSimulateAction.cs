using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Simulates a Particle System at a variable speed.")]
	public class ParticleSimulateAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		[Tooltip("Time at which this particle displays. This leave the system in a paused state.")]
		public FsmFloat m_TimeToFastForwardTo;

		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;

		public override void Reset()
		{
			m_GameObject = null;
			m_TimeToFastForwardTo = 0f;
			m_IncludeChildren = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!(ownerDefaultTarget == null))
			{
				ParticleSystem component = ownerDefaultTarget.GetComponent<ParticleSystem>();
				if (component == null)
				{
					Debug.LogWarning($"ParticleSimulateAction.OnEnter() - GameObject {ownerDefaultTarget} has no ParticleSystem component");
				}
				else
				{
					component.Simulate(m_TimeToFastForwardTo.Value, m_IncludeChildren.Value);
				}
			}
		}
	}
}
