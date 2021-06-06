using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F60 RID: 3936
	[ActionCategory("Pegasus")]
	[Tooltip("Stop a Particle System.")]
	public class ParticleStopAction : FsmStateAction
	{
		// Token: 0x0600AD0B RID: 44299 RVA: 0x0035FAEA File Offset: 0x0035DCEA
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_IncludeChildren = false;
		}

		// Token: 0x0600AD0C RID: 44300 RVA: 0x0035FB00 File Offset: 0x0035DD00
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
				Debug.LogWarning(string.Format("ParticleStopAction.OnEnter() - GameObject {0} has no ParticleSystem component", ownerDefaultTarget));
				base.Finish();
				return;
			}
			component.Stop(this.m_IncludeChildren.Value);
			base.Finish();
		}

		// Token: 0x040093EB RID: 37867
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093EC RID: 37868
		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;
	}
}
