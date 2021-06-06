using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Emit particles in a Particle System immediately.\nIf the particle system is not playing it will start playing.")]
	public class ParticleEmitAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		[Tooltip("The number of particles to emit.")]
		public FsmInt m_Count;

		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;

		public override void Reset()
		{
			m_GameObject = null;
			m_Count = 10;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			if (m_IncludeChildren.Value)
			{
				EmitParticlesRecurse(ownerDefaultTarget);
			}
			else
			{
				EmitParticles(ownerDefaultTarget);
			}
			Finish();
		}

		private void EmitParticles(GameObject go)
		{
			ParticleSystem component = go.GetComponent<ParticleSystem>();
			if (component == null)
			{
				Debug.LogWarning($"ParticleEmitAction.OnEnter() - GameObject {go} has no ParticleSystem component");
			}
			else
			{
				component.Emit(m_Count.Value);
			}
		}

		private void EmitParticlesRecurse(GameObject go)
		{
			ParticleSystem component = go.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Emit(m_Count.Value);
			}
			foreach (Transform item in go.transform)
			{
				EmitParticlesRecurse(item.gameObject);
			}
		}
	}
}
