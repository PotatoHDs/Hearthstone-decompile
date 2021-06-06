using System;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200110C RID: 4364
	public class JournalButton : MonoBehaviour
	{
		// Token: 0x0600BF31 RID: 48945 RVA: 0x003A3F14 File Offset: 0x003A2114
		private void Awake()
		{
			this.m_toolTip = base.GetComponent<TooltipZone>();
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.BindDataModel(AchievementManager.Get().Categories, false);
			this.m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel, false);
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "BUTTON_CLICKED")
				{
					this.OnClicked();
				}
				if (PlatformSettings.Screen >= ScreenCategory.Tablet)
				{
					if (eventName == "SHOW_TOOLTIP")
					{
						if (this.m_toolTip != null)
						{
							this.m_toolTip.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_JOURNAL_HEADLINE"), GameStrings.Get("GLUE_TOOLTIP_BUTTON_JOURNAL_DESC"), 0);
							return;
						}
					}
					else if (eventName == "HIDE_TOOLTIP" && this.m_toolTip != null)
					{
						this.m_toolTip.HideTooltip();
					}
				}
			});
		}

		// Token: 0x0600BF32 RID: 48946 RVA: 0x003A3F7C File Offset: 0x003A217C
		public void OnClicked()
		{
			if (this.m_journalPopUpWidget != null)
			{
				return;
			}
			this.m_journalPopUpWidget = WidgetInstance.Create(JournalPopup.JOURNAL_POPUP_PREFAB, false);
			this.m_journalPopUpWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnJournalPopupWidgetReady();
			}, null, true);
		}

		// Token: 0x0600BF33 RID: 48947 RVA: 0x003A3FBC File Offset: 0x003A21BC
		private void OnJournalPopupWidgetReady()
		{
			JournalPopup componentInChildren = this.m_journalPopUpWidget.GetComponentInChildren<JournalPopup>();
			if (componentInChildren == null)
			{
				return;
			}
			componentInChildren.Show();
		}

		// Token: 0x04009B4B RID: 39755
		private Widget m_widget;

		// Token: 0x04009B4C RID: 39756
		private Widget m_journalPopUpWidget;

		// Token: 0x04009B4D RID: 39757
		private TooltipZone m_toolTip;

		// Token: 0x04009B4E RID: 39758
		private const string BUTTON_CLICKED = "BUTTON_CLICKED";

		// Token: 0x04009B4F RID: 39759
		private const string SHOW_TOOLTIP = "SHOW_TOOLTIP";

		// Token: 0x04009B50 RID: 39760
		private const string HIDE_TOOLTIP = "HIDE_TOOLTIP";
	}
}
