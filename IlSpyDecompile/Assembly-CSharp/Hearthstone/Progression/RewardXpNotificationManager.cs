using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class RewardXpNotificationManager : IService
	{
		public static readonly AssetReference END_OF_GAME_QUEST_REWARD_FLOW_PREFAB = new AssetReference("QuestXPReward.prefab:c545a0b6333b7eb4a8ce2c6d6ac6c54b");

		private List<RewardTrackXpChange> m_xpChanges = new List<RewardTrackXpChange>();

		private Widget m_questXpRewardWidget;

		private QuestXpReward m_questXpReward;

		private Action m_callback;

		public bool IsReady
		{
			get
			{
				if (m_questXpRewardWidget != null)
				{
					return m_questXpRewardWidget.IsReady;
				}
				return false;
			}
		}

		public bool HasXpGainsToShow => m_xpChanges.Count > 0;

		public bool IsShowingXpGains { get; private set; }

		public bool JustShowGameXp { get; private set; }

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += WillReset;
			serviceLocator.Get<Network>().RegisterNetHandler(RewardTrackXpNotification.PacketID.ID, OnRewardTrackXpNotification);
			m_questXpRewardWidget = null;
			JustShowGameXp = false;
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
		}

		private void WillReset()
		{
			m_xpChanges.Clear();
			JustShowGameXp = false;
		}

		public static RewardXpNotificationManager Get()
		{
			return HearthstoneServices.Get<RewardXpNotificationManager>();
		}

		public void InitEndOfGameFlow(Action callback)
		{
			m_questXpRewardWidget = WidgetInstance.Create(END_OF_GAME_QUEST_REWARD_FLOW_PREFAB);
			m_questXpRewardWidget.Hide();
			m_questXpRewardWidget.RegisterReadyListener(delegate
			{
				OverlayUI.Get().AddGameObject(m_questXpRewardWidget.gameObject);
				m_questXpReward = m_questXpRewardWidget.GetComponentInChildren<QuestXpReward>();
				callback?.Invoke();
			});
		}

		public void ShowRewardTrackXpGains(Action callback, bool justShowGameXp = false)
		{
			IsShowingXpGains = true;
			m_callback = callback;
			JustShowGameXp = justShowGameXp;
			Processor.RunCoroutine(ShowRewardTrackXpGainsWhenReady());
		}

		public void TerminateEarly()
		{
			if (IsShowingXpGains)
			{
				m_questXpReward?.ClearAndHide();
				IsShowingXpGains = false;
				m_xpChanges.Clear();
				m_callback?.Invoke();
			}
		}

		public void ContinueNotifications()
		{
			if (JustShowGameXp)
			{
				JustShowGameXp = false;
				if (m_questXpReward != null)
				{
					m_questXpReward.ContinueNotifications();
				}
				else
				{
					TerminateEarly();
				}
			}
		}

		private IEnumerator ShowRewardTrackXpGainsWhenReady()
		{
			while (m_questXpReward == null)
			{
				yield return null;
			}
			m_questXpRewardWidget.Show();
			m_questXpReward.Initialize(m_xpChanges);
			m_xpChanges.Clear();
			m_questXpReward.ShowXpGains(delegate
			{
				UnityEngine.Object.Destroy(m_questXpRewardWidget);
				IsShowingXpGains = false;
				m_callback?.Invoke();
			});
		}

		private void OnRewardTrackXpNotification()
		{
			if (!QuestManager.Get().IsSystemEnabled)
			{
				return;
			}
			RewardTrackXpNotification rewardTrackXpNotification = Network.Get().GetRewardTrackXpNotification();
			if (rewardTrackXpNotification == null)
			{
				return;
			}
			foreach (RewardTrackXpChange item in rewardTrackXpNotification.XpChange)
			{
				m_xpChanges.Add(item);
			}
		}

		private void FlushGains(Action callback)
		{
			InitEndOfGameFlow(delegate
			{
				ShowRewardTrackXpGains(callback);
			});
		}

		public void ShowXpNotificationsImmediate(Action callback)
		{
			if (HasXpGainsToShow)
			{
				FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
				FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc);
				FlushGains(delegate
				{
					FullScreenFXMgr.Get().StopBlur();
					callback?.Invoke();
				});
			}
			else
			{
				callback?.Invoke();
			}
		}

		public string GetRewardTrackDebugHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RewardTrackXpChange xpChange in m_xpChanges)
			{
				stringBuilder.AppendFormat("Source={0}(id:{1}) Lvl:{2} Xp:{3} -> Lvl:{4} Xp:{5}", Enum.GetName(typeof(RewardSourceType), xpChange.RewardSourceType), xpChange.RewardSourceId, xpChange.PrevLevel, xpChange.PrevXp, xpChange.CurrLevel, xpChange.CurrXp);
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		public void DebugSimScenario(int scenarioId)
		{
			RewardTrackXpChange rewardTrackXpChange = new RewardTrackXpChange();
			rewardTrackXpChange.PrevLevel = 1;
			rewardTrackXpChange.CurrLevel = 1;
			rewardTrackXpChange.PrevXp = 0;
			rewardTrackXpChange.CurrXp = 40;
			rewardTrackXpChange.RewardSourceType = 4;
			int num = 114;
			GameDbf.Quest.GetRecord(num);
			RewardTrackXpChange rewardTrackXpChange2 = new RewardTrackXpChange();
			rewardTrackXpChange2.PrevLevel = 1;
			rewardTrackXpChange2.CurrLevel = 1;
			rewardTrackXpChange2.PrevXp = 40;
			rewardTrackXpChange2.CurrXp = 90;
			rewardTrackXpChange2.RewardSourceId = num;
			rewardTrackXpChange2.RewardSourceType = 1;
			num = 28;
			GameDbf.Quest.GetRecord(num);
			RewardTrackXpChange rewardTrackXpChange3 = new RewardTrackXpChange();
			rewardTrackXpChange3.PrevLevel = 1;
			rewardTrackXpChange3.CurrLevel = 2;
			rewardTrackXpChange3.PrevXp = 90;
			rewardTrackXpChange3.CurrXp = 90;
			rewardTrackXpChange3.RewardSourceId = num;
			rewardTrackXpChange3.RewardSourceType = 1;
			RewardTrackXpChange rewardTrackXpChange4 = new RewardTrackXpChange();
			rewardTrackXpChange4.PrevLevel = 2;
			rewardTrackXpChange4.CurrLevel = 7;
			rewardTrackXpChange4.PrevXp = 90;
			rewardTrackXpChange4.CurrXp = 23;
			rewardTrackXpChange4.RewardSourceType = 4;
			switch (scenarioId)
			{
			case 1:
				m_xpChanges.Add(rewardTrackXpChange);
				break;
			case 2:
				m_xpChanges.Add(rewardTrackXpChange2);
				break;
			case 3:
				m_xpChanges.Add(rewardTrackXpChange);
				m_xpChanges.Add(rewardTrackXpChange2);
				break;
			case 4:
				m_xpChanges.Add(rewardTrackXpChange);
				m_xpChanges.Add(rewardTrackXpChange2);
				m_xpChanges.Add(rewardTrackXpChange3);
				break;
			case 5:
				rewardTrackXpChange.PrevXp = 0;
				rewardTrackXpChange.CurrXp = 90;
				rewardTrackXpChange.PrevLevel = 1;
				rewardTrackXpChange.CurrLevel = 2;
				m_xpChanges.Add(rewardTrackXpChange);
				rewardTrackXpChange3.PrevLevel = 2;
				rewardTrackXpChange3.CurrLevel = 5;
				rewardTrackXpChange3.CurrXp = 40;
				m_xpChanges.Add(rewardTrackXpChange3);
				rewardTrackXpChange2.PrevLevel = 5;
				rewardTrackXpChange2.CurrLevel = 5;
				m_xpChanges.Add(rewardTrackXpChange2);
				break;
			case 6:
				rewardTrackXpChange.CurrLevel = 2;
				m_xpChanges.Add(rewardTrackXpChange);
				break;
			case 7:
				m_xpChanges.Add(rewardTrackXpChange4);
				break;
			}
		}
	}
}
