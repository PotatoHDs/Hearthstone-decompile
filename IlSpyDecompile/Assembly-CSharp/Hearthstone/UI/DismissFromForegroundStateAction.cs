using UnityEngine;

namespace Hearthstone.UI
{
	public class DismissFromForegroundStateAction : StateActionImplementation
	{
		public override void Run(bool loadSynchronously = false)
		{
			RunOnInstanceOrTargetGameObject(GetOverride(0).NestedReference, enableInstance: false, delegate(WidgetInstance instance)
			{
				DismissFromForeground(instance.Widget.gameObject);
			}, DismissFromForeground);
		}

		private void DismissFromForeground(GameObject gameObject)
		{
			UIContext.GetRoot().DismissPopup(gameObject);
			Complete(success: true);
		}
	}
}
