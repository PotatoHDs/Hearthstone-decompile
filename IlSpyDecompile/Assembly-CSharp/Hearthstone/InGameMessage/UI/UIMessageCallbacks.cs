using System;

namespace Hearthstone.InGameMessage.UI
{
	public class UIMessageCallbacks
	{
		public Action OnShown { get; set; }

		public Action OnClosed { get; set; }

		public Action OnStoreOpened { get; set; }
	}
}
