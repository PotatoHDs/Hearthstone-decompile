using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001020 RID: 4128
	public class HideGameObjectStateAction : StateActionImplementation
	{
		// Token: 0x0600B33C RID: 45884 RVA: 0x00373336 File Offset: 0x00371536
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B33D RID: 45885 RVA: 0x00373354 File Offset: 0x00371554
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			GameObject gameObject;
			if (!base.GetOverride(0).Resolve(out gameObject))
			{
				base.Complete(false);
				return;
			}
			gameObject.SetActive(false);
			base.Complete(true);
		}
	}
}
