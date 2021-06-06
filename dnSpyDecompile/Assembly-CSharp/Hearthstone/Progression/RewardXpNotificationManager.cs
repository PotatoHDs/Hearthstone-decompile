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
	// Token: 0x02001124 RID: 4388
	public class RewardXpNotificationManager : IService
	{
		// Token: 0x0600C05B RID: 49243 RVA: 0x003AA1E4 File Offset: 0x003A83E4
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += this.WillReset;
			serviceLocator.Get<Network>().RegisterNetHandler(RewardTrackXpNotification.PacketID.ID, new Network.NetHandler(this.OnRewardTrackXpNotification), null);
			this.m_questXpRewardWidget = null;
			this.JustShowGameXp = false;
			yield break;
		}

		// Token: 0x0600C05C RID: 49244 RVA: 0x003A7509 File Offset: 0x003A5709
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(Network),
				typeof(QuestManager)
			};
		}

		// Token: 0x0600C05D RID: 49245 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Shutdown()
		{
		}

		// Token: 0x0600C05E RID: 49246 RVA: 0x003AA1FA File Offset: 0x003A83FA
		private void WillReset()
		{
			this.m_xpChanges.Clear();
			this.JustShowGameXp = false;
		}

		// Token: 0x0600C05F RID: 49247 RVA: 0x003AA20E File Offset: 0x003A840E
		public static RewardXpNotificationManager Get()
		{
			return HearthstoneServices.Get<RewardXpNotificationManager>();
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600C060 RID: 49248 RVA: 0x003AA215 File Offset: 0x003A8415
		public bool IsReady
		{
			get
			{
				return this.m_questXpRewardWidget != null && this.m_questXpRewardWidget.IsReady;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600C061 RID: 49249 RVA: 0x003AA232 File Offset: 0x003A8432
		public bool HasXpGainsToShow
		{
			get
			{
				return this.m_xpChanges.Count > 0;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x0600C062 RID: 49250 RVA: 0x003AA242 File Offset: 0x003A8442
		// (set) Token: 0x0600C063 RID: 49251 RVA: 0x003AA24A File Offset: 0x003A844A
		public bool IsShowingXpGains { get; private set; }

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600C064 RID: 49252 RVA: 0x003AA253 File Offset: 0x003A8453
		// (set) Token: 0x0600C065 RID: 49253 RVA: 0x003AA25B File Offset: 0x003A845B
		public bool JustShowGameXp { get; private set; }

		// Token: 0x0600C066 RID: 49254 RVA: 0x003AA264 File Offset: 0x003A8464
		public void InitEndOfGameFlow(Action callback)
		{
			this.m_questXpRewardWidget = WidgetInstance.Create(RewardXpNotificationManager.END_OF_GAME_QUEST_REWARD_FLOW_PREFAB, false);
			this.m_questXpRewardWidget.Hide();
			this.m_questXpRewardWidget.RegisterReadyListener(delegate(object _)
			{
				OverlayUI.Get().AddGameObject(this.m_questXpRewardWidget.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
				this.m_questXpReward = this.m_questXpRewardWidget.GetComponentInChildren<QuestXpReward>();
				Action callback2 = callback;
				if (callback2 == null)
				{
					return;
				}
				callback2();
			}, null, true);
		}

		// Token: 0x0600C067 RID: 49255 RVA: 0x003AA2BF File Offset: 0x003A84BF
		public void ShowRewardTrackXpGains(Action callback, bool justShowGameXp = false)
		{
			this.IsShowingXpGains = true;
			this.m_callback = callback;
			this.JustShowGameXp = justShowGameXp;
			Processor.RunCoroutine(this.ShowRewardTrackXpGainsWhenReady(), null);
		}

		// Token: 0x0600C068 RID: 49256 RVA: 0x003AA2E3 File Offset: 0x003A84E3
		public void TerminateEarly()
		{
			if (this.IsShowingXpGains)
			{
				QuestXpReward questXpReward = this.m_questXpReward;
				if (questXpReward != null)
				{
					questXpReward.ClearAndHide();
				}
				this.IsShowingXpGains = false;
				this.m_xpChanges.Clear();
				Action callback = this.m_callback;
				if (callback == null)
				{
					return;
				}
				callback();
			}
		}

		// Token: 0x0600C069 RID: 49257 RVA: 0x003AA320 File Offset: 0x003A8520
		public void ContinueNotifications()
		{
			if (this.JustShowGameXp)
			{
				this.JustShowGameXp = false;
				if (this.m_questXpReward != null)
				{
					this.m_questXpReward.ContinueNotifications();
					return;
				}
				this.TerminateEarly();
			}
		}

		// Token: 0x0600C06A RID: 49258 RVA: 0x003AA351 File Offset: 0x003A8551
		private IEnumerator ShowRewardTrackXpGainsWhenReady()
		{
			while (this.m_questXpReward == null)
			{
				yield return null;
			}
			this.m_questXpRewardWidget.Show();
			this.m_questXpReward.Initialize(this.m_xpChanges);
			this.m_xpChanges.Clear();
			this.m_questXpReward.ShowXpGains(delegate
			{
				UnityEngine.Object.Destroy(this.m_questXpRewardWidget);
				this.IsShowingXpGains = false;
				Action callback = this.m_callback;
				if (callback == null)
				{
					return;
				}
				callback();
			});
			yield break;
		}

		// Token: 0x0600C06B RID: 49259 RVA: 0x003AA360 File Offset: 0x003A8560
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
				this.m_xpChanges.Add(item);
			}
		}

		// Token: 0x0600C06C RID: 49260 RVA: 0x003AA3D4 File Offset: 0x003A85D4
		private void FlushGains(Action callback)
		{
			this.InitEndOfGameFlow(delegate
			{
				this.ShowRewardTrackXpGains(callback, false);
			});
		}

		// Token: 0x0600C06D RID: 49261 RVA: 0x003AA408 File Offset: 0x003A8608
		public void ShowXpNotificationsImmediate(Action callback)
		{
			if (this.HasXpGainsToShow)
			{
				FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
				FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc, null);
				this.FlushGains(delegate
				{
					FullScreenFXMgr.Get().StopBlur();
					Action callback3 = callback;
					if (callback3 == null)
					{
						return;
					}
					callback3();
				});
				return;
			}
			Action callback2 = callback;
			if (callback2 == null)
			{
				return;
			}
			callback2();
		}

		// Token: 0x0600C06E RID: 49262 RVA: 0x003AA474 File Offset: 0x003A8674
		public string GetRewardTrackDebugHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RewardTrackXpChange rewardTrackXpChange in this.m_xpChanges)
			{
				stringBuilder.AppendFormat("Source={0}(id:{1}) Lvl:{2} Xp:{3} -> Lvl:{4} Xp:{5}", new object[]
				{
					Enum.GetName(typeof(RewardSourceType), rewardTrackXpChange.RewardSourceType),
					rewardTrackXpChange.RewardSourceId,
					rewardTrackXpChange.PrevLevel,
					rewardTrackXpChange.PrevXp,
					rewardTrackXpChange.CurrLevel,
					rewardTrackXpChange.CurrXp
				});
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600C06F RID: 49263 RVA: 0x003AA550 File Offset: 0x003A8750
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
				this.m_xpChanges.Add(rewardTrackXpChange);
				return;
			case 2:
				this.m_xpChanges.Add(rewardTrackXpChange2);
				return;
			case 3:
				this.m_xpChanges.Add(rewardTrackXpChange);
				this.m_xpChanges.Add(rewardTrackXpChange2);
				return;
			case 4:
				this.m_xpChanges.Add(rewardTrackXpChange);
				this.m_xpChanges.Add(rewardTrackXpChange2);
				this.m_xpChanges.Add(rewardTrackXpChange3);
				return;
			case 5:
				rewardTrackXpChange.PrevXp = 0;
				rewardTrackXpChange.CurrXp = 90;
				rewardTrackXpChange.PrevLevel = 1;
				rewardTrackXpChange.CurrLevel = 2;
				this.m_xpChanges.Add(rewardTrackXpChange);
				rewardTrackXpChange3.PrevLevel = 2;
				rewardTrackXpChange3.CurrLevel = 5;
				rewardTrackXpChange3.CurrXp = 40;
				this.m_xpChanges.Add(rewardTrackXpChange3);
				rewardTrackXpChange2.PrevLevel = 5;
				rewardTrackXpChange2.CurrLevel = 5;
				this.m_xpChanges.Add(rewardTrackXpChange2);
				return;
			case 6:
				rewardTrackXpChange.CurrLevel = 2;
				this.m_xpChanges.Add(rewardTrackXpChange);
				return;
			case 7:
				this.m_xpChanges.Add(rewardTrackXpChange4);
				return;
			default:
				return;
			}
		}

		// Token: 0x04009BE6 RID: 39910
		public static readonly AssetReference END_OF_GAME_QUEST_REWARD_FLOW_PREFAB = new AssetReference("QuestXPReward.prefab:c545a0b6333b7eb4a8ce2c6d6ac6c54b");

		// Token: 0x04009BE7 RID: 39911
		private List<RewardTrackXpChange> m_xpChanges = new List<RewardTrackXpChange>();

		// Token: 0x04009BE8 RID: 39912
		private Widget m_questXpRewardWidget;

		// Token: 0x04009BE9 RID: 39913
		private QuestXpReward m_questXpReward;

		// Token: 0x04009BEA RID: 39914
		private Action m_callback;
	}
}
