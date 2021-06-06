using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F5F RID: 3935
	[ActionCategory("Pegasus")]
	[Tooltip("Simulates a Particle System at a variable speed.")]
	public class ParticleSimulateAction : FsmStateAction
	{
		// Token: 0x0600AD08 RID: 44296 RVA: 0x0035FA5D File Offset: 0x0035DC5D
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_TimeToFastForwardTo = 0f;
			this.m_IncludeChildren = false;
		}

		// Token: 0x0600AD09 RID: 44297 RVA: 0x0035FA84 File Offset: 0x0035DC84
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			ParticleSystem component = ownerDefaultTarget.GetComponent<ParticleSystem>();
			if (component == null)
			{
				Debug.LogWarning(string.Format("ParticleSimulateAction.OnEnter() - GameObject {0} has no ParticleSystem component", ownerDefaultTarget));
				return;
			}
			component.Simulate(this.m_TimeToFastForwardTo.Value, this.m_IncludeChildren.Value);
		}

		// Token: 0x040093E8 RID: 37864
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093E9 RID: 37865
		[Tooltip("Time at which this particle displays. This leave the system in a paused state.")]
		public FsmFloat m_TimeToFastForwardTo;

		// Token: 0x040093EA RID: 37866
		[Tooltip("Run this action on all child objects' Particle Systems.")]
		public FsmBool m_IncludeChildren;
	}
}
