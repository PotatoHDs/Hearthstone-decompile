using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200101F RID: 4127
	public class DismissFromForegroundStateAction : StateActionImplementation
	{
		// Token: 0x0600B338 RID: 45880 RVA: 0x003732E2 File Offset: 0x003714E2
		public override void Run(bool loadSynchronously = false)
		{
			base.RunOnInstanceOrTargetGameObject(base.GetOverride(0).NestedReference, false, delegate(WidgetInstance instance)
			{
				this.DismissFromForeground(instance.Widget.gameObject);
			}, new Action<GameObject>(this.DismissFromForeground));
		}

		// Token: 0x0600B339 RID: 45881 RVA: 0x0037330F File Offset: 0x0037150F
		private void DismissFromForeground(GameObject gameObject)
		{
			UIContext.GetRoot().DismissPopup(gameObject);
			base.Complete(true);
		}
	}
}
