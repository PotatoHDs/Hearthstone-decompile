using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F5A RID: 3930
	[ActionCategory("Pegasus")]
	[Tooltip("Remove all particles in a Particle System.")]
	public class ParticleClearAction : FsmStateAction
	{
		// Token: 0x0600ACF6 RID: 44278 RVA: 0x0035F67D File Offset: 0x0035D87D
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_IncludeChildren = false;
		}

		// Token: 0x0600ACF7 RID: 44279 RVA: 0x0035F694 File Offset: 0x0035D894
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			ParticleSystem component = ownerDefaultTarget.GetComponent<ParticleSystem>();
			if (component != null)
			{
				Debug.LogWarning(string.Format("ParticlePlayAction.OnEnter() - GameObject {0} has no ParticleSystem component", ownerDefaultTarget));
				base.Finish();
				return;
			}
			component.Clear(this.m_IncludeChildren.Value);
			base.Finish();
		}

		// Token: 0x040093D8 RID: 37848
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093D9 RID: 37849
		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;
	}
}
