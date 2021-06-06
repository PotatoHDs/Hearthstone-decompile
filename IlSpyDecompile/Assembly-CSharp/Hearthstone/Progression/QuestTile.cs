using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestTile : MonoBehaviour
	{
		private Widget m_widget;

		private bool m_isRerollPending;

		private bool m_isRerollAnimPlaying;

		private bool m_wasRerollSuccessful;

		private int m_grantedQuestId;

		public static string QUEST_TILE_WIDGET_ASSET = "QuestTile.prefab:6a05035200522f3418a150db7763ce95";

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(WidgetEventListener);
			QuestManager.Get().OnQuestRerolled += OnQuestRerolled;
			QuestManager.Get().OnQuestRerollCountChanged += OnQuestRerollCountChanged;
			m_widget.RegisterDoneChangingStatesListener(delegate
			{
				if (ProgressUtils.ShowDebugIds)
				{
					m_widget.TriggerEvent("DEBUG_SHOW_ID");
				}
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}

		private void OnDestroy()
		{
			if (QuestManager.Get() != null)
			{
				QuestManager.Get().OnQuestRerolled -= OnQuestRerolled;
				QuestManager.Get().OnQuestRerollCountChanged -= OnQuestRerollCountChanged;
			}
		}

		private void WidgetEventListener(string eventName)
		{
			if (eventName.Equals("CLICKED_REROLL"))
			{
				RerollQuest();
			}
		}

		private void RerollQuest()
		{
			QuestDataModel dataModel = m_widget.GetDataModel<QuestDataModel>();
			if (dataModel != null && !m_isRerollPending)
			{
				if (!Network.IsLoggedIn())
				{
					ProgressUtils.ShowOfflinePopup();
				}
				else if (QuestManager.Get().RerollQuest(dataModel.QuestId))
				{
					m_isRerollPending = true;
					m_widget.TriggerEvent("CODE_REROLLED");
					m_isRerollAnimPlaying = true;
				}
			}
		}

		private void OnQuestRerolled(int rerolledQuestId, int grantedQuestId, bool success)
		{
			if (m_isRerollPending)
			{
				QuestDataModel dataModel = m_widget.GetDataModel<QuestDataModel>();
				if (dataModel != null && dataModel.QuestId == rerolledQuestId)
				{
					m_isRerollPending = false;
					m_wasRerollSuccessful = success;
					m_grantedQuestId = grantedQuestId;
					PlayQuestGrantedAnimIfReady();
				}
			}
		}

		private void OnQuestRerollCountChanged(int questPoolId, int rerollCount)
		{
			QuestDataModel dataModel = m_widget.GetDataModel<QuestDataModel>();
			if (dataModel != null && dataModel.PoolId == questPoolId)
			{
				dataModel.RerollCount = rerollCount;
			}
		}

		private void OnRerollAnimFinished()
		{
			m_isRerollAnimPlaying = false;
			PlayQuestGrantedAnimIfReady();
		}

		private void PlayQuestGrantedAnimIfReady()
		{
			if (m_isRerollPending || m_isRerollAnimPlaying)
			{
				return;
			}
			if (m_wasRerollSuccessful)
			{
				UpdateQuestDataModelByQuestId(m_grantedQuestId);
				m_widget.RegisterDoneChangingStatesListener(delegate
				{
					m_widget.TriggerEvent("CODE_GRANTED_BY_REROLL");
				}, null, callImmediatelyIfSet: true, doOnce: true);
			}
			else
			{
				m_widget.Show();
			}
		}

		private void UpdateQuestDataModelByQuestId(int questId)
		{
			m_widget.GetDataModel<QuestDataModel>()?.CopyFromDataModel(QuestManager.Get().CreateQuestDataModelById(questId));
		}
	}
}
