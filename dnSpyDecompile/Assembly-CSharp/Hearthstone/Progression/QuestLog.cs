using System;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001116 RID: 4374
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestLog : MonoBehaviour
	{
		// Token: 0x0600BF87 RID: 49031 RVA: 0x003A582F File Offset: 0x003A3A2F
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.UpdateQuestLists();
		}

		// Token: 0x0600BF88 RID: 49032 RVA: 0x003A5844 File Offset: 0x003A3A44
		private void UpdateQuestLists()
		{
			if (QuestManager.Get().CanBeGrantedPoolQuests())
			{
				this.m_widget.TriggerEvent("CODE_SHOW_POOL_TITLE_TEXT", default(Widget.TriggerEventParameters));
			}
			else
			{
				this.m_widget.TriggerEvent("CODE_HIDE_POOL_TITLE_TEXT", default(Widget.TriggerEventParameters));
			}
			QuestListDataModel questListDataModel = new QuestListDataModel();
			foreach (QuestDataModel item in QuestManager.Get().CreateActiveQuestsDataModel(QuestPool.QuestPoolType.NONE, true).Quests)
			{
				if (questListDataModel.Quests.Count >= this.m_maxQuestsPerRow)
				{
					break;
				}
				questListDataModel.Quests.Add(item);
			}
			foreach (QuestDataModel item2 in QuestManager.Get().CreateActiveQuestsDataModel(QuestPool.QuestPoolType.DAILY, true).Quests)
			{
				if (questListDataModel.Quests.Count >= this.m_maxQuestsPerRow)
				{
					break;
				}
				questListDataModel.Quests.Add(item2);
			}
			this.m_dailyQuestsListWidget.BindDataModel(questListDataModel, false);
			QuestListDataModel dataModel = QuestManager.Get().CreateActiveQuestsDataModel(QuestPool.QuestPoolType.WEEKLY, true);
			this.m_weeklyQuestsListWidget.BindDataModel(dataModel, false);
		}

		// Token: 0x04009B79 RID: 39801
		public Widget m_dailyQuestsListWidget;

		// Token: 0x04009B7A RID: 39802
		public Widget m_weeklyQuestsListWidget;

		// Token: 0x04009B7B RID: 39803
		public int m_maxQuestsPerRow;

		// Token: 0x04009B7C RID: 39804
		private Widget m_widget;
	}
}
