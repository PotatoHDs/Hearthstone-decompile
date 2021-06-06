using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestLog : MonoBehaviour
	{
		public Widget m_dailyQuestsListWidget;

		public Widget m_weeklyQuestsListWidget;

		public int m_maxQuestsPerRow;

		private Widget m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			UpdateQuestLists();
		}

		private void UpdateQuestLists()
		{
			if (QuestManager.Get().CanBeGrantedPoolQuests())
			{
				m_widget.TriggerEvent("CODE_SHOW_POOL_TITLE_TEXT");
			}
			else
			{
				m_widget.TriggerEvent("CODE_HIDE_POOL_TITLE_TEXT");
			}
			QuestListDataModel questListDataModel = new QuestListDataModel();
			foreach (QuestDataModel quest in QuestManager.Get().CreateActiveQuestsDataModel(QuestPool.QuestPoolType.NONE, appendTimeUntilNextQuest: true).Quests)
			{
				if (questListDataModel.Quests.Count < m_maxQuestsPerRow)
				{
					questListDataModel.Quests.Add(quest);
					continue;
				}
				break;
			}
			foreach (QuestDataModel quest2 in QuestManager.Get().CreateActiveQuestsDataModel(QuestPool.QuestPoolType.DAILY, appendTimeUntilNextQuest: true).Quests)
			{
				if (questListDataModel.Quests.Count < m_maxQuestsPerRow)
				{
					questListDataModel.Quests.Add(quest2);
					continue;
				}
				break;
			}
			m_dailyQuestsListWidget.BindDataModel(questListDataModel);
			QuestListDataModel dataModel = QuestManager.Get().CreateActiveQuestsDataModel(QuestPool.QuestPoolType.WEEKLY, appendTimeUntilNextQuest: true);
			m_weeklyQuestsListWidget.BindDataModel(dataModel);
		}
	}
}
