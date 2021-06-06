using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class JournalTrayDisplay : MonoBehaviour
	{
		public const string TAB_SELECTED = "CODE_JOURNAL_TAB_SELECTED";

		public const string FINISHED_EVENT = "CODE_{0}_FINISHED";

		public const string ACTIVE_TAB_CLICKED_EVENT = "CODE_ACTIVE_{0}_CLICKED";

		private static int s_activeTabIndex = 1;

		private bool m_isChangingTab;

		private JournalMetaDataModel m_journalMetaData;

		private Widget m_widget;

		private void Awake()
		{
			m_journalMetaData = new JournalMetaDataModel();
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.BindDataModel(m_journalMetaData);
			m_journalMetaData.TabIndex = s_activeTabIndex;
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_JOURNAL_TAB_SELECTED")
				{
					HandleTabSelected();
				}
			});
		}

		private void HandleTabSelected()
		{
			if (m_isChangingTab)
			{
				return;
			}
			m_isChangingTab = true;
			EventDataModel e = m_widget.GetDataModel<EventDataModel>();
			if (e == null)
			{
				Debug.LogError("HandleTabSelected: no payload was sent with the CODE_JOURNAL_TAB_SELECTED event!");
				return;
			}
			int previousIndex = s_activeTabIndex;
			if (e.Payload is IConvertible)
			{
				s_activeTabIndex = Convert.ToInt32(e.Payload);
				m_journalMetaData.TabIndex = s_activeTabIndex;
			}
			if (previousIndex == s_activeTabIndex)
			{
				m_widget.RegisterDoneChangingStatesListener(delegate
				{
					IWidgetEventListener[] componentsInChildren = m_widget.GetComponentsInChildren<IWidgetEventListener>(includeInactive: false);
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].EventReceived($"CODE_ACTIVE_{e.SourceName}_CLICKED");
					}
					m_isChangingTab = false;
				}, null, callImmediatelyIfSet: true, doOnce: true);
				return;
			}
			m_widget.Hide();
			m_widget.RegisterDoneChangingStatesListener(delegate
			{
				m_widget.TriggerEvent($"CODE_{e.SourceName}_FINISHED", new Widget.TriggerEventParameters
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
				m_widget.Show();
				m_isChangingTab = false;
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
	}
}
