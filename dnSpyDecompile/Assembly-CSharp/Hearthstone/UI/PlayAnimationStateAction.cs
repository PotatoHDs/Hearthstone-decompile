using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001022 RID: 4130
	public class PlayAnimationStateAction : StateActionImplementation
	{
		// Token: 0x0600B343 RID: 45891 RVA: 0x0037347E File Offset: 0x0037167E
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B344 RID: 45892 RVA: 0x0037349C File Offset: 0x0037169C
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			GameObject gameObject;
			if (!base.GetOverride(0).Resolve(out gameObject) || base.AnimationClip == null)
			{
				base.Complete(false);
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
			base.Complete(true);
		}
	}
}
