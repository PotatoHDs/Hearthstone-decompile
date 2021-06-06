using System;
using System.Linq;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class QuestNotificationPopup : MonoBehaviour
	{
		public Widget m_questList;

		public int m_maxQuestsPerRow;

		private WidgetTemplate m_widget;

		private Action m_callback;

		private QuestPool.QuestPoolType m_questPoolType;

		private bool m_IKSShown;

		private bool m_shouldShowIKS;

		private const string CODE_HIDE = "CODE_HIDE";

		private const int RETURNING_PLAYER_PROXY_QUEST_ID = 99;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "CODE_HIDE")
				{
					Hide();
				}
			});
			m_widget.RegisterDoneChangingStatesListener(HandleDoneChangingStates, null, callImmediatelyIfSet: true, doOnce: true);
		}

		private void OnDestroy()
		{
			m_callback?.Invoke();
			if (m_IKSShown)
			{
				InnKeepersSpecial.Close();
			}
		}

		public void Initialize(RewardTrackDataModel rewardTrackDataModel, QuestListDataModel questListDataModel, Action callback, bool showIKS)
		{
			m_callback = callback;
			m_shouldShowIKS = showIKS;
			if (rewardTrackDataModel != null)
			{
				m_widget.BindDataModel(rewardTrackDataModel);
			}
			if (questListDataModel != null)
			{
				m_widget.BindDataModel(new QuestListDataModel
				{
					Quests = questListDataModel.Quests.Take(m_maxQuestsPerRow).Aggregate(new DataModelList<QuestDataModel>(), delegate(DataModelList<QuestDataModel> acc, QuestDataModel dataModel)
					{
						dataModel.RerollCount = 0;
						acc.Add(dataModel);
						return acc;
					})
				});
			}
			SceneMgr.Get().RegisterScenePreLoadEvent(OnPreLoadNextScene);
			FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		}

		public void Show()
		{
			OverlayUI.Get().AddGameObject(base.gameObject.transform.parent.gameObject);
			m_widget.RegisterDoneChangingStatesListener(delegate
			{
				if (m_shouldShowIKS)
				{
					m_IKSShown = InnKeepersSpecial.CheckShow(Hide);
				}
				m_widget.TriggerEvent("SHOW");
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}

		public void Hide()
		{
			AckQuests();
			SceneMgr.Get().UnregisterScenePreLoadEvent(OnPreLoadNextScene);
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}

		private void HandleDoneChangingStates(object unused)
		{
			Listable componentInChildren = m_questList.GetComponentInChildren<Listable>();
			TransformUtil.SetPoint(componentInChildren.GetOrCreateColliderFromItemBounds(isEnabled: false), Anchor.CENTER_XZ, m_widget, Anchor.CENTER_XZ);
			Widget[] componentsInChildren = componentInChildren.gameObject.GetComponentsInChildren<Widget>();
			foreach (Widget widget in componentsInChildren)
			{
				widget.TriggerEvent("DISABLE_INTERACTION");
				QuestDataModel dataModel = widget.GetDataModel<QuestDataModel>();
				if (dataModel != null && dataModel.Status == QuestManager.QuestStatus.NEW)
				{
					widget.TriggerEvent("CODE_GRANTED");
				}
			}
		}

		private void AckQuests()
		{
			if (m_widget == null)
			{
				return;
			}
			QuestListDataModel dataModel = m_widget.GetDataModel<QuestListDataModel>();
			if (dataModel == null)
			{
				return;
			}
			QuestManager questManager = QuestManager.Get();
			if (questManager == null)
			{
				return;
			}
			foreach (QuestDataModel quest in dataModel.Quests)
			{
				if (quest.Status == QuestManager.QuestStatus.NEW)
				{
					questManager.AckQuest(quest.QuestId);
				}
			}
		}

		private void OnPreLoadNextScene(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
		{
			Hide();
		}

		private void OnFatalError(FatalErrorMessage message, object userData)
		{
			Hide();
		}
	}
}
