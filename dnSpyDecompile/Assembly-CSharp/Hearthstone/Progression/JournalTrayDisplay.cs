using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200110E RID: 4366
	[RequireComponent(typeof(WidgetTemplate))]
	public class JournalTrayDisplay : MonoBehaviour
	{
		// Token: 0x0600BF42 RID: 48962 RVA: 0x003A4364 File Offset: 0x003A2564
		private void Awake()
		{
			this.m_journalMetaData = new JournalMetaDataModel();
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.BindDataModel(this.m_journalMetaData, false);
			this.m_journalMetaData.TabIndex = JournalTrayDisplay.s_activeTabIndex;
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_JOURNAL_TAB_SELECTED")
				{
					this.HandleTabSelected();
				}
			});
		}

		// Token: 0x0600BF43 RID: 48963 RVA: 0x003A43C4 File Offset: 0x003A25C4
		private void HandleTabSelected()
		{
			if (this.m_isChangingTab)
			{
				return;
			}
			this.m_isChangingTab = true;
			EventDataModel e = this.m_widget.GetDataModel<EventDataModel>();
			if (e == null)
			{
				Debug.LogError("HandleTabSelected: no payload was sent with the CODE_JOURNAL_TAB_SELECTED event!");
				return;
			}
			int previousIndex = JournalTrayDisplay.s_activeTabIndex;
			if (e.Payload is IConvertible)
			{
				JournalTrayDisplay.s_activeTabIndex = Convert.ToInt32(e.Payload);
				this.m_journalMetaData.TabIndex = JournalTrayDisplay.s_activeTabIndex;
			}
			if (previousIndex == JournalTrayDisplay.s_activeTabIndex)
			{
				this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
				{
					IWidgetEventListener[] componentsInChildren = this.m_widget.GetComponentsInChildren<IWidgetEventListener>(false);
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].EventReceived(string.Format("CODE_ACTIVE_{0}_CLICKED", e.SourceName));
					}
					this.m_isChangingTab = false;
				}, null, true, true);
				return;
			}
			this.m_widget.Hide();
			this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				this.m_widget.TriggerEvent(string.Format("CODE_{0}_FINISHED", e.SourceName), new Widget.TriggerEventParameters
				{
					SourceName = e.SourceName,
					Payload = previousIndex,
					NoDownwardPropagation = true,
					IgnorePlaymaker = true
				});
				RewardTrackManager rewardTrackManager = RewardTrackManager.Get();
				if (e.SourceName.Equals("REWARD_TAB") && rewardTrackManager.TrackDataModel.Season > rewardTrackManager.TrackDataModel.SeasonLastSeen)
				{
					rewardTrackManager.SetRewardTrackSeasonLastSeen(rewardTrackManager.TrackDataModel.Season);
				}
				this.m_widget.Show();
				this.m_isChangingTab = false;
			}, null, true, true);
		}

		// Token: 0x04009B57 RID: 39767
		public const string TAB_SELECTED = "CODE_JOURNAL_TAB_SELECTED";

		// Token: 0x04009B58 RID: 39768
		public const string FINISHED_EVENT = "CODE_{0}_FINISHED";

		// Token: 0x04009B59 RID: 39769
		public const string ACTIVE_TAB_CLICKED_EVENT = "CODE_ACTIVE_{0}_CLICKED";

		// Token: 0x04009B5A RID: 39770
		private static int s_activeTabIndex = 1;

		// Token: 0x04009B5B RID: 39771
		private bool m_isChangingTab;

		// Token: 0x04009B5C RID: 39772
		private JournalMetaDataModel m_journalMetaData;

		// Token: 0x04009B5D RID: 39773
		private Widget m_widget;
	}
}
