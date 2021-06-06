using UnityEngine;

namespace Hearthstone.UI
{
	public class TriggerParticleSystemStateAction : StateActionImplementation
	{
		public override void Run(bool runSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			if (GetOverride(0).Resolve(out var gameObject))
			{
				ParticleSystem componentInChildren = gameObject.GetComponentInChildren<ParticleSystem>();
				if (componentInChildren != null)
				{
					componentInChildren.Play(withChildren: true);
				}
			}
			Complete(success: true);
		}
	}
}
