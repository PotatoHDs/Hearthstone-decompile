using UnityEngine;

namespace Hearthstone.UI
{
	public class PlayAnimationStateAction : StateActionImplementation
	{
		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			if (!GetOverride(0).Resolve(out var gameObject) || base.AnimationClip == null)
			{
				Complete(success: false);
				return;
			}
			Animation animation = gameObject.GetComponent<Animation>();
			if (animation == null)
			{
				animation = gameObject.AddComponent<Animation>();
				animation.hideFlags = HideFlags.HideAndDontSave;
			}
			animation.clip = base.AnimationClip;
			animation.playAutomatically = false;
			animation.AddClip(base.AnimationClip, base.AnimationClip.name);
			animation.Play(base.AnimationClip.name, PlayMode.StopAll);
			Complete(success: true);
		}
	}
}
