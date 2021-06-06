using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public class QuestToastManager : IService
	{
		public static readonly AssetReference QUEST_PROGRESS_TOAST_PREFAB = new AssetReference("QuestProgressToast.prefab:a14a1594ad6e85242a7b50b26c840edd");

		private bool m_isSystemEnabled;

		private List<QuestDataModel> m_questList = new List<QuestDataModel>();

		private List<QuestProgressToast> m_activeToasts = new List<QuestProgressToast>();

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += WillReset;
			serviceLocator.Get<Network>().RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
			QuestManager.Get().OnQuestProgress += OnQuestProgress;
			yield break;
		}

		public Type[] GetDependencies()
		{
			return new Type[2]
			{
				typeof(Network),
				typeof(QuestManager)
			};
		}

		public void Shutdown()
		{
			if (HearthstoneServices.TryGet<QuestManager>(out var service))
			{
				service.OnQuestProgress -= OnQuestProgress;
			}
		}

		private void OnQuestProgress(QuestDataModel questDataModel)
		{
			m_questList.Add(questDataModel);
		}

		private void WillReset()
		{
			m_isSystemEnabled = false;
			m_questList.Clear();
		}

		public static QuestToastManager Get()
		{
			return HearthstoneServices.Get<QuestToastManager>();
		}

		public void ShowQuestProgress()
		{
			if (!m_isSystemEnabled)
			{
				return;
			}
			foreach (QuestDataModel quest in m_questList)
			{
				Widget widget = WidgetInstance.Create(QUEST_PROGRESS_TOAST_PREFAB);
				widget.RegisterReadyListener(delegate
				{
					QuestProgressToast componentInChildren = widget.GetComponentInChildren<QuestProgressToast>();
					componentInChildren.Initialize(quest);
					OverlayUI.Get().AddGameObject(componentInChildren.transform.parent.gameObject);
					m_activeToasts.Add(componentInChildren);
					UpdateToastPositions();
					componentInChildren.Show();
				});
				widget.RegisterDeactivatedListener(delegate
				{
					m_activeToasts.Remove(widget.GetComponentInChildren<QuestProgressToast>());
					UpdateToastPositions();
				});
			}
			m_questList.Clear();
		}

		private void UpdateToastPositions()
		{
			int num = 0;
			foreach (QuestProgressToast activeToast in m_activeToasts)
			{
				if (num > 0)
				{
					TransformUtil.SetPoint(activeToast.gameObject, Anchor.BOTTOM, m_activeToasts[num - 1].gameObject, Anchor.TOP, m_activeToasts[num - 1].GetOffset());
				}
				num++;
			}
		}

		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				m_isSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
			}
		}
	}
}
