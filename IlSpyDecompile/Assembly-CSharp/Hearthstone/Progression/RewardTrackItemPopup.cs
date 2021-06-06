using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class RewardTrackItemPopup : MonoBehaviour
	{
		public const string SHOW_TAVERN_PASS = "CODE_SHOW_TAVERN_PASS";

		private WidgetTemplate m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_SHOW_TAVERN_PASS")
				{
					if (Network.IsLoggedIn())
					{
						Shop.OpenToTavernPassPageWhenReady();
					}
					else
					{
						ProgressUtils.ShowOfflinePopup();
					}
				}
			});
		}
	}
}
