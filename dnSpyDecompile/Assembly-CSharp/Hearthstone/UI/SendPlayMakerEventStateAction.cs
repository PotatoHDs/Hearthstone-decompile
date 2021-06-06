using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001026 RID: 4134
	public class SendPlayMakerEventStateAction : StateActionImplementation
	{
		// Token: 0x0600B351 RID: 45905 RVA: 0x003738E5 File Offset: 0x00371AE5
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B352 RID: 45906 RVA: 0x00373900 File Offset: 0x00371B00
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			string @string = base.GetString(0);
			bool flag = false;
			GameObject gameObject;
			if (base.GetOverride(0).Resolve(out gameObject))
			{
				foreach (Component component in gameObject.GetComponents<Component>())
				{
					PlayMakerFSM playMakerFSM = component as PlayMakerFSM;
					if (Application.IsPlaying(component) && playMakerFSM != null)
					{
						flag = true;
						playMakerFSM.SendEvent(@string);
					}
				}
			}
			if (!flag)
			{
				bool isPlaying = Application.isPlaying;
			}
			base.Complete(true);
		}
	}
}
