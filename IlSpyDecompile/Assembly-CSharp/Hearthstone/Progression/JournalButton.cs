using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class JournalButton : MonoBehaviour
	{
		private Widget m_widget;

		private Widget m_journalPopUpWidget;

		private TooltipZone m_toolTip;

		private const string BUTTON_CLICKED = "BUTTON_CLICKED";

		private const string SHOW_TOOLTIP = "SHOW_TOOLTIP";

		private const string HIDE_TOOLTIP = "HIDE_TOOLTIP";

		private void Awake()
		{
			m_toolTip = GetComponent<TooltipZone>();
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.BindDataModel(AchievementManager.Get().Categories);
			m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel);
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "BUTTON_CLICKED")
				{
					OnClicked();
				}
				if (PlatformSettings.Screen >= ScreenCategory.Tablet)
				{
					if (eventName == "SHOW_TOOLTIP")
					{
						if (m_toolTip != null)
						{
							m_toolTip.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_JOURNAL_HEADLINE"), GameStrings.Get("GLUE_TOOLTIP_BUTTON_JOURNAL_DESC"));
						}
					}
					else if (eventName == "HIDE_TOOLTIP" && m_toolTip != null)
					{
						m_toolTip.HideTooltip();
					}
				}
			});
		}

		public void OnClicked()
		{
			if (!(m_journalPopUpWidget != null))
			{
				m_journalPopUpWidget = WidgetInstance.Create(JournalPopup.JOURNAL_POPUP_PREFAB);
				m_journalPopUpWidget.RegisterReadyListener(delegate
				{
					OnJournalPopupWidgetReady();
				});
			}
		}

		private void OnJournalPopupWidgetReady()
		{
			JournalPopup componentInChildren = m_journalPopUpWidget.GetComponentInChildren<JournalPopup>();
			if (!(componentInChildren == null))
			{
				componentInChildren.Show();
			}
		}
	}
}
