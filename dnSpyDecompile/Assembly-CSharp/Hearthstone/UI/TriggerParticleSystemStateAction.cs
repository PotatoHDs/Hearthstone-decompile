using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001029 RID: 4137
	public class TriggerParticleSystemStateAction : StateActionImplementation
	{
		// Token: 0x0600B35B RID: 45915 RVA: 0x00373A95 File Offset: 0x00371C95
		public override void Run(bool runSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B35C RID: 45916 RVA: 0x00373AB0 File Offset: 0x00371CB0
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			GameObject gameObject;
			if (base.GetOverride(0).Resolve(out gameObject))
			{
				ParticleSystem componentInChildren = gameObject.GetComponentInChildren<ParticleSystem>();
				if (componentInChildren != null)
				{
					componentInChildren.Play(true);
				}
			}
			base.Complete(true);
		}
	}
}
