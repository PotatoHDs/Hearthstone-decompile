using System;
using System.Linq;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001118 RID: 4376
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestNotificationPopup : MonoBehaviour
	{
		// Token: 0x0600BFBC RID: 49084 RVA: 0x003A6DBA File Offset: 0x003A4FBA
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					this.Hide();
				}
			});
			this.m_widget.RegisterDoneChangingStatesListener(new Action<object>(this.HandleDoneChangingStates), null, true, true);
		}

		// Token: 0x0600BFBD RID: 49085 RVA: 0x003A6DF9 File Offset: 0x003A4FF9
		private void OnDestroy()
		{
			Action callback = this.m_callback;
			if (callback != null)
			{
				callback();
			}
			if (this.m_IKSShown)
			{
				InnKeepersSpecial.Close();
			}
		}

		// Token: 0x0600BFBE RID: 49086 RVA: 0x003A6E1C File Offset: 0x003A501C
		public void Initialize(RewardTrackDataModel rewardTrackDataModel, QuestListDataModel questListDataModel, Action callback, bool showIKS)
		{
			this.m_callback = callback;
			this.m_shouldShowIKS = showIKS;
			if (rewardTrackDataModel != null)
			{
				this.m_widget.BindDataModel(rewardTrackDataModel, false);
			}
			if (questListDataModel != null)
			{
				Widget widget = this.m_widget;
				QuestListDataModel questListDataModel2 = new QuestListDataModel();
				questListDataModel2.Quests = questListDataModel.Quests.Take(this.m_maxQuestsPerRow).Aggregate(new DataModelList<QuestDataModel>(), delegate(DataModelList<QuestDataModel> acc, QuestDataModel dataModel)
				{
					dataModel.RerollCount = 0;
					acc.Add(dataModel);
					return acc;
				});
				widget.BindDataModel(questListDataModel2, false);
			}
			SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadNextScene));
			FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}

		// Token: 0x0600BFBF RID: 49087 RVA: 0x003A6EC9 File Offset: 0x003A50C9
		public void Show()
		{
			OverlayUI.Get().AddGameObject(base.gameObject.transform.parent.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
			this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				if (this.m_shouldShowIKS)
				{
					this.m_IKSShown = InnKeepersSpecial.CheckShow(new Action(this.Hide));
				}
				this.m_widget.TriggerEvent("SHOW", default(Widget.TriggerEventParameters));
			}, null, true, true);
		}

		// Token: 0x0600BFC0 RID: 49088 RVA: 0x003A6F08 File Offset: 0x003A5108
		public void Hide()
		{
			this.AckQuests();
			SceneMgr.Get().UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnPreLoadNextScene));
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}

		// Token: 0x0600BFC1 RID: 49089 RVA: 0x003A6F60 File Offset: 0x003A5160
		private void HandleDoneChangingStates(object unused)
		{
			Listable componentInChildren = this.m_questList.GetComponentInChildren<Listable>();
			TransformUtil.SetPoint(componentInChildren.GetOrCreateColliderFromItemBounds(false), Anchor.CENTER_XZ, this.m_widget, Anchor.CENTER_XZ);
			foreach (Widget widget in componentInChildren.gameObject.GetComponentsInChildren<Widget>())
			{
				widget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
				QuestDataModel dataModel = widget.GetDataModel<QuestDataModel>();
				if (dataModel != null && dataModel.Status == QuestManager.QuestStatus.NEW)
				{
					widget.TriggerEvent("CODE_GRANTED", default(Widget.TriggerEventParameters));
				}
			}
		}

		// Token: 0x0600BFC2 RID: 49090 RVA: 0x003A6FEC File Offset: 0x003A51EC
		private void AckQuests()
		{
			if (this.m_widget == null)
			{
				return;
			}
			QuestListDataModel dataModel = this.m_widget.GetDataModel<QuestListDataModel>();
			if (dataModel == null)
			{
				return;
			}
			QuestManager questManager = QuestManager.Get();
			if (questManager == null)
			{
				return;
			}
			foreach (QuestDataModel questDataModel in dataModel.Quests)
			{
				if (questDataModel.Status == QuestManager.QuestStatus.NEW)
				{
					questManager.AckQuest(questDataModel.QuestId);
				}
			}
		}

		// Token: 0x0600BFC3 RID: 49091 RVA: 0x003A7074 File Offset: 0x003A5274
		private void OnPreLoadNextScene(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
		{
			this.Hide();
		}

		// Token: 0x0600BFC4 RID: 49092 RVA: 0x003A7074 File Offset: 0x003A5274
		private void OnFatalError(FatalErrorMessage message, object userData)
		{
			this.Hide();
		}

		// Token: 0x04009B91 RID: 39825
		public Widget m_questList;

		// Token: 0x04009B92 RID: 39826
		public int m_maxQuestsPerRow;

		// Token: 0x04009B93 RID: 39827
		private WidgetTemplate m_widget;

		// Token: 0x04009B94 RID: 39828
		private Action m_callback;

		// Token: 0x04009B95 RID: 39829
		private QuestPool.QuestPoolType m_questPoolType;

		// Token: 0x04009B96 RID: 39830
		private bool m_IKSShown;

		// Token: 0x04009B97 RID: 39831
		private bool m_shouldShowIKS;

		// Token: 0x04009B98 RID: 39832
		private const string CODE_HIDE = "CODE_HIDE";

		// Token: 0x04009B99 RID: 39833
		private const int RETURNING_PLAYER_PROXY_QUEST_ID = 99;
	}
}
