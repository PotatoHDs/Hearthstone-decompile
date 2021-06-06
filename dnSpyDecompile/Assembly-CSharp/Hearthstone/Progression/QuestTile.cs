using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x0200111A RID: 4378
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestTile : MonoBehaviour
	{
		// Token: 0x0600BFD0 RID: 49104 RVA: 0x003A7240 File Offset: 0x003A5440
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener));
			QuestManager.Get().OnQuestRerolled += this.OnQuestRerolled;
			QuestManager.Get().OnQuestRerollCountChanged += this.OnQuestRerollCountChanged;
			this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				if (ProgressUtils.ShowDebugIds)
				{
					this.m_widget.TriggerEvent("DEBUG_SHOW_ID", default(Widget.TriggerEventParameters));
				}
			}, null, true, true);
		}

		// Token: 0x0600BFD1 RID: 49105 RVA: 0x003A72B6 File Offset: 0x003A54B6
		private void OnDestroy()
		{
			if (QuestManager.Get() != null)
			{
				QuestManager.Get().OnQuestRerolled -= this.OnQuestRerolled;
				QuestManager.Get().OnQuestRerollCountChanged -= this.OnQuestRerollCountChanged;
			}
		}

		// Token: 0x0600BFD2 RID: 49106 RVA: 0x003A72EB File Offset: 0x003A54EB
		private void WidgetEventListener(string eventName)
		{
			if (eventName.Equals("CLICKED_REROLL"))
			{
				this.RerollQuest();
			}
		}

		// Token: 0x0600BFD3 RID: 49107 RVA: 0x003A7300 File Offset: 0x003A5500
		private void RerollQuest()
		{
			QuestDataModel dataModel = this.m_widget.GetDataModel<QuestDataModel>();
			if (dataModel == null)
			{
				return;
			}
			if (this.m_isRerollPending)
			{
				return;
			}
			if (!Network.IsLoggedIn())
			{
				ProgressUtils.ShowOfflinePopup();
				return;
			}
			if (QuestManager.Get().RerollQuest(dataModel.QuestId))
			{
				this.m_isRerollPending = true;
				this.m_widget.TriggerEvent("CODE_REROLLED", default(Widget.TriggerEventParameters));
				this.m_isRerollAnimPlaying = true;
			}
		}

		// Token: 0x0600BFD4 RID: 49108 RVA: 0x003A7370 File Offset: 0x003A5570
		private void OnQuestRerolled(int rerolledQuestId, int grantedQuestId, bool success)
		{
			if (!this.m_isRerollPending)
			{
				return;
			}
			QuestDataModel dataModel = this.m_widget.GetDataModel<QuestDataModel>();
			if (dataModel == null || dataModel.QuestId != rerolledQuestId)
			{
				return;
			}
			this.m_isRerollPending = false;
			this.m_wasRerollSuccessful = success;
			this.m_grantedQuestId = grantedQuestId;
			this.PlayQuestGrantedAnimIfReady();
		}

		// Token: 0x0600BFD5 RID: 49109 RVA: 0x003A73C4 File Offset: 0x003A55C4
		private void OnQuestRerollCountChanged(int questPoolId, int rerollCount)
		{
			QuestDataModel dataModel = this.m_widget.GetDataModel<QuestDataModel>();
			if (dataModel == null)
			{
				return;
			}
			if (dataModel.PoolId != questPoolId)
			{
				return;
			}
			dataModel.RerollCount = rerollCount;
		}

		// Token: 0x0600BFD6 RID: 49110 RVA: 0x003A73F2 File Offset: 0x003A55F2
		private void OnRerollAnimFinished()
		{
			this.m_isRerollAnimPlaying = false;
			this.PlayQuestGrantedAnimIfReady();
		}

		// Token: 0x0600BFD7 RID: 49111 RVA: 0x003A7404 File Offset: 0x003A5604
		private void PlayQuestGrantedAnimIfReady()
		{
			if (this.m_isRerollPending)
			{
				return;
			}
			if (this.m_isRerollAnimPlaying)
			{
				return;
			}
			if (this.m_wasRerollSuccessful)
			{
				this.UpdateQuestDataModelByQuestId(this.m_grantedQuestId);
				this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
				{
					this.m_widget.TriggerEvent("CODE_GRANTED_BY_REROLL", default(Widget.TriggerEventParameters));
				}, null, true, true);
				return;
			}
			this.m_widget.Show();
		}

		// Token: 0x0600BFD8 RID: 49112 RVA: 0x003A7460 File Offset: 0x003A5660
		private void UpdateQuestDataModelByQuestId(int questId)
		{
			QuestDataModel dataModel = this.m_widget.GetDataModel<QuestDataModel>();
			if (dataModel == null)
			{
				return;
			}
			dataModel.CopyFromDataModel(QuestManager.Get().CreateQuestDataModelById(questId));
		}

		// Token: 0x04009B9D RID: 39837
		private Widget m_widget;

		// Token: 0x04009B9E RID: 39838
		private bool m_isRerollPending;

		// Token: 0x04009B9F RID: 39839
		private bool m_isRerollAnimPlaying;

		// Token: 0x04009BA0 RID: 39840
		private bool m_wasRerollSuccessful;

		// Token: 0x04009BA1 RID: 39841
		private int m_grantedQuestId;

		// Token: 0x04009BA2 RID: 39842
		public static string QUEST_TILE_WIDGET_ASSET = "QuestTile.prefab:6a05035200522f3418a150db7763ce95";
	}
}
