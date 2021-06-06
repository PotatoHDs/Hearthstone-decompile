using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F5D RID: 3933
	[ActionCategory("Pegasus")]
	[Tooltip("Pause a Particle System.")]
	public class ParticlePauseAction : FsmStateAction
	{
		// Token: 0x0600AD02 RID: 44290 RVA: 0x0035F8FD File Offset: 0x0035DAFD
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_IncludeChildren = false;
		}

		// Token: 0x0600AD03 RID: 44291 RVA: 0x0035F914 File Offset: 0x0035DB14
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			ParticleSystem component = ownerDefaultTarget.GetComponent<ParticleSystem>();
			if (component == null)
			{
				Debug.LogWarning(string.Format("ParticlePauseAction.OnEnter() - GameObject {0} has no ParticleSystem component", ownerDefaultTarget));
				base.Finish();
				return;
			}
			component.Pause(this.m_IncludeChildren.Value);
			base.Finish();
		}

		// Token: 0x040093E4 RID: 37860
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093E5 RID: 37861
		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;
	}
}
