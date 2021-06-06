using System;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001122 RID: 4386
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardTrackItemPopup : MonoBehaviour
	{
		// Token: 0x0600C026 RID: 49190 RVA: 0x003A8FCC File Offset: 0x003A71CC
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_SHOW_TAVERN_PASS")
				{
					if (Network.IsLoggedIn())
					{
						Shop.OpenToTavernPassPageWhenReady();
						return;
					}
					ProgressUtils.ShowOfflinePopup();
				}
			});
		}

		// Token: 0x04009BD6 RID: 39894
		public const string SHOW_TAVERN_PASS = "CODE_SHOW_TAVERN_PASS";

		// Token: 0x04009BD7 RID: 39895
		private WidgetTemplate m_widget;
	}
}
