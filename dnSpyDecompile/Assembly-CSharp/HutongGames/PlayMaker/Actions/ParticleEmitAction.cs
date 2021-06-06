using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F5B RID: 3931
	[ActionCategory("Pegasus")]
	[Tooltip("Emit particles in a Particle System immediately.\nIf the particle system is not playing it will start playing.")]
	public class ParticleEmitAction : FsmStateAction
	{
		// Token: 0x0600ACF9 RID: 44281 RVA: 0x0035F701 File Offset: 0x0035D901
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Count = 10;
		}

		// Token: 0x0600ACFA RID: 44282 RVA: 0x0035F718 File Offset: 0x0035D918
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			if (this.m_IncludeChildren.Value)
			{
				this.EmitParticlesRecurse(ownerDefaultTarget);
			}
			else
			{
				this.EmitParticles(ownerDefaultTarget);
			}
			base.Finish();
		}

		// Token: 0x0600ACFB RID: 44283 RVA: 0x0035F76C File Offset: 0x0035D96C
		private void EmitParticles(GameObject go)
		{
			ParticleSystem component = go.GetComponent<ParticleSystem>();
			if (component == null)
			{
				Debug.LogWarning(string.Format("ParticleEmitAction.OnEnter() - GameObject {0} has no ParticleSystem component", go));
				return;
			}
			component.Emit(this.m_Count.Value);
		}

		// Token: 0x0600ACFC RID: 44284 RVA: 0x0035F7AC File Offset: 0x0035D9AC
		private void EmitParticlesRecurse(GameObject go)
		{
			ParticleSystem component = go.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Emit(this.m_Count.Value);
			}
			foreach (object obj in go.transform)
			{
				Transform transform = (Transform)obj;
				this.EmitParticlesRecurse(transform.gameObject);
			}
		}

		// Token: 0x040093DA RID: 37850
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093DB RID: 37851
		[Tooltip("The number of particles to emit.")]
		public FsmInt m_Count;

		// Token: 0x040093DC RID: 37852
		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;
	}
}
