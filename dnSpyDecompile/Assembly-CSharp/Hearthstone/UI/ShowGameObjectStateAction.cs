using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001028 RID: 4136
	public class ShowGameObjectStateAction : StateActionImplementation
	{
		// Token: 0x0600B358 RID: 45912 RVA: 0x003739F5 File Offset: 0x00371BF5
		public override void Run(bool loadSynchronously = false)
		{
			this.m_loadSynchronously = loadSynchronously;
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B359 RID: 45913 RVA: 0x00373A18 File Offset: 0x00371C18
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			GameObject gameObject;
			if (!base.GetOverride(0).Resolve(out gameObject))
			{
				base.Complete(false);
				return;
			}
			gameObject.SetActive(true);
			if (this.m_loadSynchronously)
			{
				SceneUtils.WalkSelfAndChildren(gameObject.transform, delegate(Transform t)
				{
					WidgetInstance component = t.GetComponent<WidgetInstance>();
					if (component != null)
					{
						component.Initialize();
						return false;
					}
					return !(t.GetComponent<WidgetTemplate>() != null);
				});
			}
			base.Complete(true);
		}

		// Token: 0x04009676 RID: 38518
		private bool m_loadSynchronously;
	}
}
