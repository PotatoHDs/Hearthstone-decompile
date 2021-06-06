using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001027 RID: 4135
	public class SendToForegroundStateAction : StateActionImplementation
	{
		// Token: 0x0600B354 RID: 45908 RVA: 0x0037398F File Offset: 0x00371B8F
		public override void Run(bool loadSynchronously = false)
		{
			base.RunOnInstanceOrTargetGameObject(base.GetOverride(0).NestedReference, true, delegate(WidgetInstance instance)
			{
				this.SendToForeground(instance.Widget.gameObject);
			}, new Action<GameObject>(this.SendToForeground));
		}

		// Token: 0x0600B355 RID: 45909 RVA: 0x003739BC File Offset: 0x00371BBC
		private void SendToForeground(GameObject gameObject)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			UIContext.GetRoot().ShowPopup(gameObject, UIContext.BlurType.Legacy);
			base.Complete(true);
		}
	}
}
