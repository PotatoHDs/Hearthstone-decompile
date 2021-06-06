using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001117 RID: 4375
	public class QuestManager : IService
	{
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x0600BF8A RID: 49034 RVA: 0x003A5988 File Offset: 0x003A3B88
		// (remove) Token: 0x0600BF8B RID: 49035 RVA: 0x003A59C0 File Offset: 0x003A3BC0
		public event QuestManager.OnQuestRerolledHandler OnQuestRerolled;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x0600BF8C RID: 49036 RVA: 0x003A59F8 File Offset: 0x003A3BF8
		// (remove) Token: 0x0600BF8D RID: 49037 RVA: 0x003A5A30 File Offset: 0x003A3C30
		public event QuestManager.OnQuestRerollCountChangedHandler OnQuestRerollCountChanged;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x0600BF8E RID: 49038 RVA: 0x003A5A68 File Offset: 0x003A3C68
		// (remove) Token: 0x0600BF8F RID: 49039 RVA: 0x003A5AA0 File Offset: 0x003A3CA0
		public event QuestManager.OnQuestProgressHandler OnQuestProgress;

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600BF90 RID: 49040 RVA: 0x003A5AD5 File Offset: 0x003A3CD5
		// (set) Token: 0x0600BF91 RID: 49041 RVA: 0x003A5ADD File Offset: 0x003A3CDD
		public bool IsSystemEnabled { get; private set; }

		// Token: 0x0600BF92 RID: 49042 RVA: 0x003A5AE6 File Offset: 0x003A3CE6
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += this.WillReset;
			Network network = serviceLocator.Get<Network>();
			network.RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState), null);
			network.RegisterNetHandler(PlayerQuestStateUpdate.PacketID.ID, new Network.NetHandler(this.ReceivePlayerQuestStateUpdateMessage), null);
			network.RegisterNetHandler(PlayerQuestPoolStateUpdate.PacketID.ID, new Network.NetHandler(this.ReceivePlayerQuestPoolStateUpdateMessage), null);
			network.RegisterNetHandler(RerollQuestResponse.PacketID.ID, new Network.NetHandler(this.ReceiveRerollQuestResponseMessage), null);
			yield break;
		}

		// Token: 0x0600BF93 RID: 49043 RVA: 0x001B7846 File Offset: 0x001B5A46
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(Network)
			};
		}

		// Token: 0x0600BF94 RID: 49044 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Shutdown()
		{
		}

		// Token: 0x0600BF95 RID: 49045 RVA: 0x003A5AFC File Offset: 0x003A3CFC
		private void WillReset()
		{
			this.IsSystemEnabled = false;
			this.m_showQuestPoolTypesFromLogin = false;
			this.m_questState.Clear();
			this.m_questPoolState.Clear();
			this.m_questPoolTypesToShow.Clear();
			this.m_rewardPresenter.Clear();
			this.m_questPoolNextQuestTime.Clear();
			this.m_checkForNewQuestsIntervalSecs = this.ResetCheckForNewQuestsInterval();
			Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.CheckForNewQuestsScheduledCallback), null);
		}

		// Token: 0x0600BF96 RID: 49046 RVA: 0x003A5B6D File Offset: 0x003A3D6D
		public static QuestManager Get()
		{
			return HearthstoneServices.Get<QuestManager>();
		}

		// Token: 0x0600BF97 RID: 49047 RVA: 0x003A5B74 File Offset: 0x003A3D74
		public QuestDataModel CreateQuestDataModelById(int questId)
		{
			PlayerQuestState questState;
			if (!this.m_questState.TryGetValue(questId, out questState))
			{
				questState = new PlayerQuestState
				{
					QuestId = questId
				};
			}
			return this.CreateQuestDataModel(questState);
		}

		// Token: 0x0600BF98 RID: 49048 RVA: 0x003A5BA8 File Offset: 0x003A3DA8
		public RewardScrollDataModel CreateRewardScrollDataModelByQuestId(int questId, List<RewardItemOutput> rewardItemOutput = null)
		{
			QuestDbfRecord record = GameDbf.Quest.GetRecord(questId);
			if (record == null)
			{
				return new RewardScrollDataModel();
			}
			RewardScrollDataModel rewardScrollDataModel = new RewardScrollDataModel();
			DbfLocValue name = record.Name;
			rewardScrollDataModel.DisplayName = ((name != null) ? name : string.Empty);
			rewardScrollDataModel.Description = ProgressUtils.FormatDescription(record.Description, record.Quota);
			rewardScrollDataModel.RewardList = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList, 0, rewardItemOutput);
			return rewardScrollDataModel;
		}

		// Token: 0x0600BF99 RID: 49049 RVA: 0x003A5C1C File Offset: 0x003A3E1C
		public QuestListDataModel CreateActiveQuestsDataModel(QuestPool.QuestPoolType questPoolType, bool appendTimeUntilNextQuest)
		{
			QuestListDataModel questListDataModel = new QuestListDataModel();
			List<int> list2 = new List<int>();
			if (questPoolType == QuestPool.QuestPoolType.NONE)
			{
				list2.Add(0);
			}
			else
			{
				(from r in GameDbf.QuestPool.GetRecords()
				where r.QuestPoolType == questPoolType
				orderby r.ID descending
				select r.ID).Aggregate(list2, delegate(List<int> list, int id)
				{
					list.Add(id);
					return list;
				});
			}
			using (List<int>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int questPoolId = enumerator.Current;
					int num = 0;
					IEnumerable<PlayerQuestState> values = this.m_questState.Values;
					Func<PlayerQuestState, bool> predicate;
					Func<PlayerQuestState, bool> <>9__4;
					if ((predicate = <>9__4) == null)
					{
						predicate = (<>9__4 = delegate(PlayerQuestState q)
						{
							QuestDbfRecord record2 = GameDbf.Quest.GetRecord(q.QuestId);
							return record2 != null && record2.QuestPool == questPoolId && this.IsQuestActive(q);
						});
					}
					foreach (PlayerQuestState questState in from q in values.Where(predicate)
					orderby q.QuestId descending
					select q)
					{
						questListDataModel.Quests.Add(this.CreateQuestDataModel(questState));
						num++;
					}
					if (this.CanBeGrantedPoolQuests() && appendTimeUntilNextQuest)
					{
						QuestPoolDbfRecord record = GameDbf.QuestPool.GetRecord(questPoolId);
						if (record != null)
						{
							int b = Mathf.Max(record.MaxQuestsActive - num, 0);
							int count = Mathf.Min(record.NumQuestsGranted, b);
							foreach (QuestDataModel item in Enumerable.Repeat<QuestDataModel>(this.CreateNextQuestTimeDataModel(record), count))
							{
								questListDataModel.Quests.Add(item);
							}
						}
					}
				}
			}
			return questListDataModel;
		}

		// Token: 0x0600BF9A RID: 49050 RVA: 0x003A5EA4 File Offset: 0x003A40A4
		public bool ShowQuestNotification(Action callback)
		{
			QuestManager.<>c__DisplayClass36_0 CS$<>8__locals1 = new QuestManager.<>c__DisplayClass36_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.callback = callback;
			if (this.m_questPoolTypesToShow.Count == 0)
			{
				return false;
			}
			List<PlayerQuestState> list2 = new List<PlayerQuestState>();
			if (this.m_questPoolTypesToShow.Contains(QuestPool.QuestPoolType.NONE) || this.m_questPoolTypesToShow.Contains(QuestPool.QuestPoolType.DAILY))
			{
				this.m_questPoolTypesToShow.Remove(QuestPool.QuestPoolType.NONE);
				this.m_questPoolTypesToShow.Remove(QuestPool.QuestPoolType.DAILY);
				list2 = this.GetActiveQuestStatesForPool(QuestPool.QuestPoolType.NONE);
				list2.AddRange(this.GetActiveQuestStatesForPool(QuestPool.QuestPoolType.DAILY));
			}
			else
			{
				if (!this.m_questPoolTypesToShow.Contains(QuestPool.QuestPoolType.WEEKLY))
				{
					return false;
				}
				this.m_questPoolTypesToShow.Remove(QuestPool.QuestPoolType.WEEKLY);
				list2 = this.GetActiveQuestStatesForPool(QuestPool.QuestPoolType.WEEKLY);
			}
			if (list2.Count == 0)
			{
				return false;
			}
			QuestManager.<>c__DisplayClass36_0 CS$<>8__locals2 = CS$<>8__locals1;
			QuestListDataModel questListDataModel = new QuestListDataModel();
			questListDataModel.Quests = (from state in list2
			orderby state.QuestId descending
			select CS$<>8__locals1.<>4__this.CreateQuestDataModel(state) into dataModel
			orderby dataModel.PoolType
			select dataModel).Aggregate(new DataModelList<QuestDataModel>(), delegate(DataModelList<QuestDataModel> list, QuestDataModel dataModel)
			{
				dataModel.RerollCount = 0;
				list.Add(dataModel);
				return list;
			});
			CS$<>8__locals2.questListDataModel = questListDataModel;
			CS$<>8__locals1.showIKS = this.m_showQuestPoolTypesFromLogin;
			this.m_showQuestPoolTypesFromLogin = false;
			CS$<>8__locals1.widget = WidgetInstance.Create(QuestManager.QUEST_NOTIFICATION_PREFAB, false);
			CS$<>8__locals1.widget.RegisterReadyListener(delegate(object _)
			{
				QuestNotificationPopup componentInChildren = CS$<>8__locals1.widget.GetComponentInChildren<QuestNotificationPopup>();
				componentInChildren.Initialize(RewardTrackManager.Get().TrackDataModel, CS$<>8__locals1.questListDataModel, CS$<>8__locals1.callback, CS$<>8__locals1.showIKS);
				componentInChildren.Show();
			}, null, true);
			return true;
		}

		// Token: 0x0600BF9B RID: 49051 RVA: 0x003A6030 File Offset: 0x003A4230
		public bool AckQuest(int questId)
		{
			PlayerQuestState playerQuestState;
			if (!this.m_questState.TryGetValue(questId, out playerQuestState))
			{
				return false;
			}
			if (!this.NeedsAck(playerQuestState))
			{
				return false;
			}
			QuestManager.QuestStatus status = (QuestManager.QuestStatus)playerQuestState.Status;
			if (status != QuestManager.QuestStatus.NEW)
			{
				if (status == QuestManager.QuestStatus.REWARD_GRANTED)
				{
					playerQuestState.Status = 5;
				}
			}
			else
			{
				playerQuestState.Status = 2;
			}
			Network.Get().AckQuest(questId);
			return false;
		}

		// Token: 0x0600BF9C RID: 49052 RVA: 0x003A6088 File Offset: 0x003A4288
		public bool RerollQuest(int questId)
		{
			PlayerQuestState playerQuestState;
			if (!this.m_questState.TryGetValue(questId, out playerQuestState))
			{
				return false;
			}
			if (!this.IsQuestActive(playerQuestState))
			{
				return false;
			}
			QuestDbfRecord record = GameDbf.Quest.GetRecord(playerQuestState.QuestId);
			if (record == null)
			{
				return false;
			}
			if (this.GetQuestPoolRerollCount(record.QuestPool) <= 0)
			{
				return false;
			}
			Network.Get().RerollQuest(questId);
			return true;
		}

		// Token: 0x0600BF9D RID: 49053 RVA: 0x003A60E5 File Offset: 0x003A42E5
		public bool ShowNextReward(Action callback)
		{
			return this.m_rewardPresenter.ShowNextReward(callback);
		}

		// Token: 0x0600BF9E RID: 49054 RVA: 0x003A60F4 File Offset: 0x003A42F4
		public bool CanBeGrantedPoolQuests()
		{
			foreach (PlayerQuestState playerQuestState in this.m_questState.Values)
			{
				if (playerQuestState.QuestId >= 104 && playerQuestState.QuestId <= 108 && this.IsQuestActive(playerQuestState))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600BF9F RID: 49055 RVA: 0x003A616C File Offset: 0x003A436C
		private QuestDataModel CreateQuestDataModel(PlayerQuestState questState)
		{
			QuestDbfRecord record = GameDbf.Quest.GetRecord(questState.QuestId);
			if (record == null)
			{
				return new QuestDataModel();
			}
			DataModelList<string> dataModelList = new DataModelList<string>();
			string icon = record.Icon;
			if (icon != null)
			{
				(from s in icon.Split(new char[]
				{
					','
				})
				select s.Trim()).Aggregate(dataModelList, delegate(DataModelList<string> list, string element)
				{
					list.Add(element);
					return list;
				});
			}
			QuestDataModel questDataModel = new QuestDataModel();
			questDataModel.QuestId = questState.QuestId;
			questDataModel.PoolId = this.GetQuestPoolId(record);
			questDataModel.PoolType = this.GetQuestPoolType(record);
			questDataModel.DisplayMode = QuestManager.QuestTileDisplayMode.DEFAULT;
			DbfLocValue name = record.Name;
			questDataModel.Name = ((name != null) ? name.GetString(true) : null);
			questDataModel.Description = ProgressUtils.FormatDescription(record.Description, record.Quota);
			questDataModel.Icon = dataModelList;
			questDataModel.Progress = questState.Progress;
			questDataModel.Quota = record.Quota;
			questDataModel.RerollCount = this.GetQuestPoolRerollCount(record.QuestPool);
			questDataModel.Rewards = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList, 0, null);
			questDataModel.RewardTrackXp = RewardTrackManager.Get().ApplyXpBonusPercent(record.RewardTrackXp);
			questDataModel.ProgressMessage = ProgressUtils.FormatProgressMessage(questState.Progress, record.Quota);
			questDataModel.Status = (QuestManager.QuestStatus)questState.Status;
			return questDataModel;
		}

		// Token: 0x0600BFA0 RID: 49056 RVA: 0x003A62E4 File Offset: 0x003A44E4
		private QuestDataModel CreateNextQuestTimeDataModel(QuestPoolDbfRecord questPoolRecord)
		{
			return new QuestDataModel
			{
				DisplayMode = QuestManager.QuestTileDisplayMode.NEXT_QUEST_TIME,
				PoolType = questPoolRecord.QuestPoolType,
				TimeUntilNextQuest = GameStrings.Format("GLOBAL_PROGRESSION_QUEST_TIME_UNTIL_NEXT", new object[]
				{
					this.GetTimeUntilNextQuestString(questPoolRecord.ID)
				})
			};
		}

		// Token: 0x0600BFA1 RID: 49057 RVA: 0x003A6330 File Offset: 0x003A4530
		private bool NeedsAck(PlayerQuestState questState)
		{
			QuestManager.QuestStatus status = (QuestManager.QuestStatus)questState.Status;
			return status == QuestManager.QuestStatus.NEW || status == QuestManager.QuestStatus.REWARD_GRANTED;
		}

		// Token: 0x0600BFA2 RID: 49058 RVA: 0x003A6350 File Offset: 0x003A4550
		private bool IsQuestActive(PlayerQuestState questState)
		{
			QuestManager.QuestStatus status = (QuestManager.QuestStatus)questState.Status;
			return status - QuestManager.QuestStatus.NEW <= 1;
		}

		// Token: 0x0600BFA3 RID: 49059 RVA: 0x003A6370 File Offset: 0x003A4570
		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				this.IsSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
				this.m_checkForNewQuestsIntervalJitterSecs = initialClientState.GuardianVars.CheckForNewQuestsIntervalJitterSecs;
			}
		}

		// Token: 0x0600BFA4 RID: 49060 RVA: 0x003A63B8 File Offset: 0x003A45B8
		private void ReceivePlayerQuestStateUpdateMessage()
		{
			PlayerQuestStateUpdate playerQuestStateUpdate = Network.Get().GetPlayerQuestStateUpdate();
			if (playerQuestStateUpdate == null)
			{
				return;
			}
			foreach (PlayerQuestState playerQuestState in playerQuestStateUpdate.Quest)
			{
				PlayerQuestState oldState;
				this.m_questState.TryGetValue(playerQuestState.QuestId, out oldState);
				this.m_questState[playerQuestState.QuestId] = playerQuestState;
				this.HandlePlayerQuestStateChange(oldState, playerQuestState);
			}
			if (playerQuestStateUpdate.ShowQuestNotificationForPoolType.Count > 0 && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
			{
				this.m_showQuestPoolTypesFromLogin = true;
			}
			foreach (int item in playerQuestStateUpdate.ShowQuestNotificationForPoolType)
			{
				this.m_questPoolTypesToShow.Add((QuestPool.QuestPoolType)item);
			}
		}

		// Token: 0x0600BFA5 RID: 49061 RVA: 0x003A64B0 File Offset: 0x003A46B0
		private void HandlePlayerQuestStateChange(PlayerQuestState oldState, PlayerQuestState newState)
		{
			if (!this.IsSystemEnabled)
			{
				return;
			}
			if (newState == null)
			{
				return;
			}
			QuestDbfRecord record = GameDbf.Quest.GetRecord(newState.QuestId);
			if (record == null)
			{
				return;
			}
			switch (newState.Status)
			{
			case 1:
				this.m_questPoolTypesToShow.Add(this.GetQuestPoolType(record));
				return;
			case 2:
			case 3:
				if (oldState != null && oldState.Progress < newState.Progress)
				{
					QuestManager.OnQuestProgressHandler onQuestProgress = this.OnQuestProgress;
					if (onQuestProgress == null)
					{
						return;
					}
					onQuestProgress(this.CreateQuestDataModel(newState));
					return;
				}
				break;
			case 4:
				if (record.RewardList == 0)
				{
					this.AckQuest(newState.QuestId);
					return;
				}
				this.m_rewardPresenter.EnqueueReward(this.CreateRewardScrollDataModelByQuestId(newState.QuestId, newState.RewardItemOutput), delegate
				{
					this.AckQuest(newState.QuestId);
				});
				return;
			case 5:
			case 6:
			case 7:
				break;
			default:
				Debug.LogWarningFormat("QuestManager: unknown status {0} for quest id {1}", new object[]
				{
					newState.Status,
					newState.QuestId
				});
				break;
			}
		}

		// Token: 0x0600BFA6 RID: 49062 RVA: 0x003A6604 File Offset: 0x003A4804
		private void ReceivePlayerQuestPoolStateUpdateMessage()
		{
			PlayerQuestPoolStateUpdate playerQuestPoolStateUpdate = Network.Get().GetPlayerQuestPoolStateUpdate();
			if (playerQuestPoolStateUpdate == null)
			{
				return;
			}
			foreach (PlayerQuestPoolState playerQuestPoolState in playerQuestPoolStateUpdate.QuestPool)
			{
				this.m_questPoolState[playerQuestPoolState.QuestPoolId] = playerQuestPoolState;
				QuestManager.OnQuestRerollCountChangedHandler onQuestRerollCountChanged = this.OnQuestRerollCountChanged;
				if (onQuestRerollCountChanged != null)
				{
					onQuestRerollCountChanged(playerQuestPoolState.QuestPoolId, playerQuestPoolState.RerollAvailableCount);
				}
				DateTime value = DateTime.Now.AddSeconds((double)playerQuestPoolState.SecondsUntilNextGrant);
				this.m_questPoolNextQuestTime[playerQuestPoolState.QuestPoolId] = value;
			}
			this.m_checkForNewQuestsIntervalSecs = this.ResetCheckForNewQuestsInterval();
			this.ScheduleCheckForNewQuests(null);
		}

		// Token: 0x0600BFA7 RID: 49063 RVA: 0x003A66D4 File Offset: 0x003A48D4
		private void ReceiveRerollQuestResponseMessage()
		{
			RerollQuestResponse rerollQuestResponse = Network.Get().GetRerollQuestResponse();
			if (rerollQuestResponse == null)
			{
				return;
			}
			if (GameDbf.Quest.GetRecord(rerollQuestResponse.RerolledQuestId) == null)
			{
				return;
			}
			QuestManager.OnQuestRerolledHandler onQuestRerolled = this.OnQuestRerolled;
			if (onQuestRerolled == null)
			{
				return;
			}
			onQuestRerolled(rerollQuestResponse.RerolledQuestId, rerollQuestResponse.GrantedQuestId, rerollQuestResponse.Success);
		}

		// Token: 0x0600BFA8 RID: 49064 RVA: 0x003A6728 File Offset: 0x003A4928
		private int GetQuestPoolId(QuestDbfRecord questAsset)
		{
			int? num;
			if (questAsset == null)
			{
				num = null;
			}
			else
			{
				QuestPoolDbfRecord questPoolRecord = questAsset.QuestPoolRecord;
				num = ((questPoolRecord != null) ? new int?(questPoolRecord.ID) : null);
			}
			int? num2 = num;
			if (num2 == null)
			{
				return 0;
			}
			return num2.GetValueOrDefault();
		}

		// Token: 0x0600BFA9 RID: 49065 RVA: 0x003A6778 File Offset: 0x003A4978
		private QuestPool.QuestPoolType GetQuestPoolType(QuestDbfRecord questAsset)
		{
			QuestPool.QuestPoolType? questPoolType;
			if (questAsset == null)
			{
				questPoolType = null;
			}
			else
			{
				QuestPoolDbfRecord questPoolRecord = questAsset.QuestPoolRecord;
				questPoolType = ((questPoolRecord != null) ? new QuestPool.QuestPoolType?(questPoolRecord.QuestPoolType) : null);
			}
			QuestPool.QuestPoolType? questPoolType2 = questPoolType;
			if (questPoolType2 == null)
			{
				return QuestPool.QuestPoolType.NONE;
			}
			return questPoolType2.GetValueOrDefault();
		}

		// Token: 0x0600BFAA RID: 49066 RVA: 0x003A67C8 File Offset: 0x003A49C8
		private List<PlayerQuestState> GetActiveQuestStatesForPool(QuestPool.QuestPoolType questPoolType)
		{
			return (from state in this.m_questState.Values
			where this.IsQuestActive(state)
			where this.GetQuestPoolType(GameDbf.Quest.GetRecord(state.QuestId)) == questPoolType
			select state).ToList<PlayerQuestState>();
		}

		// Token: 0x0600BFAB RID: 49067 RVA: 0x003A681C File Offset: 0x003A4A1C
		private List<PlayerQuestState> GetQuestStatesForPool(int questPoolId)
		{
			return this.m_questState.Values.Where(delegate(PlayerQuestState state)
			{
				QuestDbfRecord record = GameDbf.Quest.GetRecord(state.QuestId);
				return record != null && record.QuestPool == questPoolId;
			}).ToList<PlayerQuestState>();
		}

		// Token: 0x0600BFAC RID: 49068 RVA: 0x003A6858 File Offset: 0x003A4A58
		private string GetTimeUntilNextQuestString(int questPoolId)
		{
			DateTime d;
			if (!this.m_questPoolNextQuestTime.TryGetValue(questPoolId, out d))
			{
				return "";
			}
			TimeSpan t = d - DateTime.Now;
			if (t <= TimeSpan.Zero)
			{
				return "";
			}
			return TimeUtils.GetElapsedTimeString((long)t.TotalSeconds, TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET, true);
		}

		// Token: 0x0600BFAD RID: 49069 RVA: 0x003A68B0 File Offset: 0x003A4AB0
		private int GetQuestPoolRerollCount(int questPoolId)
		{
			int result = 0;
			PlayerQuestPoolState playerQuestPoolState;
			if (this.m_questPoolState.TryGetValue(questPoolId, out playerQuestPoolState))
			{
				result = playerQuestPoolState.RerollAvailableCount;
			}
			return result;
		}

		// Token: 0x0600BFAE RID: 49070 RVA: 0x003A68D8 File Offset: 0x003A4AD8
		private bool ScheduleCheckForNewQuests(float? delaySecondsOverride = null)
		{
			Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.CheckForNewQuestsScheduledCallback), null);
			float num = this.NextCheckForNewQuestsInterval();
			if (delaySecondsOverride != null)
			{
				num = delaySecondsOverride.Value;
			}
			else
			{
				TimeSpan? timeSpan = null;
				foreach (KeyValuePair<int, DateTime> keyValuePair in this.m_questPoolNextQuestTime)
				{
					int key = keyValuePair.Key;
					TimeSpan timeSpan2 = keyValuePair.Value - DateTime.Now;
					if (timeSpan == null || timeSpan2 < timeSpan)
					{
						timeSpan = new TimeSpan?(timeSpan2);
					}
				}
				if (timeSpan != null && timeSpan.Value.TotalSeconds > (double)num)
				{
					num = (float)timeSpan.Value.TotalSeconds;
				}
			}
			return Processor.ScheduleCallback(num, true, new Processor.ScheduledCallback(this.CheckForNewQuestsScheduledCallback), null);
		}

		// Token: 0x0600BFAF RID: 49071 RVA: 0x003A69F0 File Offset: 0x003A4BF0
		private void CheckForNewQuestsScheduledCallback(object userData)
		{
			if (!Network.IsLoggedIn())
			{
				return;
			}
			if (!GameMgr.Get().IsFindingGame())
			{
				Network.Get().CheckForNewQuests();
			}
			this.ScheduleCheckForNewQuests(null);
		}

		// Token: 0x0600BFB0 RID: 49072 RVA: 0x003A6A2B File Offset: 0x003A4C2B
		private float ResetCheckForNewQuestsInterval()
		{
			this.m_checkForNewQuestsIntervalSecs = 2f;
			return this.m_checkForNewQuestsIntervalSecs;
		}

		// Token: 0x0600BFB1 RID: 49073 RVA: 0x003A6A3E File Offset: 0x003A4C3E
		private float NextCheckForNewQuestsInterval()
		{
			float result = this.m_checkForNewQuestsIntervalSecs + UnityEngine.Random.Range(0f, this.m_checkForNewQuestsIntervalJitterSecs);
			this.m_checkForNewQuestsIntervalSecs *= 2f;
			return result;
		}

		// Token: 0x0600BFB2 RID: 49074 RVA: 0x003A6A6C File Offset: 0x003A4C6C
		public string GetQuestDebugHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsSystemEnabled)
			{
				stringBuilder.AppendLine("SYSTEM DISABLED");
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine("---------- Daily Quests ----------");
			this.AppendQuestPoolStateStringForDebugHud(stringBuilder, 1);
			this.AppendQuestStateStringsForDebugHud(stringBuilder, this.GetQuestStatesForPool(1));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Weekly Quests ----------");
			this.AppendQuestPoolStateStringForDebugHud(stringBuilder, 2);
			this.AppendQuestStateStringsForDebugHud(stringBuilder, this.GetQuestStatesForPool(2));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Weekly Quests (Static) ----------");
			this.AppendQuestPoolStateStringForDebugHud(stringBuilder, 4);
			this.AppendQuestStateStringsForDebugHud(stringBuilder, this.GetQuestStatesForPool(4));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Other Quests ----------");
			this.AppendQuestStateStringsForDebugHud(stringBuilder, this.GetQuestStatesForPool(0));
			return stringBuilder.ToString();
		}

		// Token: 0x0600BFB3 RID: 49075 RVA: 0x003A6B38 File Offset: 0x003A4D38
		private void AppendQuestPoolStateStringForDebugHud(StringBuilder sb, int questPoolId)
		{
			int questPoolRerollCount = this.GetQuestPoolRerollCount(questPoolId);
			sb.AppendFormat("Rerolls: {0} | Next Quest In: \"{1}\" ({2})\n", questPoolRerollCount, this.GetTimeUntilNextQuestString(questPoolId), this.GetTimeUntilNextQuestDebugString(questPoolId));
		}

		// Token: 0x0600BFB4 RID: 49076 RVA: 0x003A6B70 File Offset: 0x003A4D70
		private void AppendQuestStateStringsForDebugHud(StringBuilder sb, List<PlayerQuestState> questStates)
		{
			foreach (PlayerQuestState questState in from q in questStates
			orderby q.Status, q.QuestId
			select q)
			{
				sb.AppendLine(this.QuestStateToString(questState));
			}
		}

		// Token: 0x0600BFB5 RID: 49077 RVA: 0x003A6C08 File Offset: 0x003A4E08
		private string QuestStateToString(PlayerQuestState questState)
		{
			QuestDbfRecord record = GameDbf.Quest.GetRecord(questState.QuestId);
			if (record == null)
			{
				return string.Format("id={0} INVALID", questState.QuestId);
			}
			string format = "id={0} {1} [{2}/{3}] \"{4}\"";
			object[] array = new object[5];
			array[0] = questState.QuestId;
			array[1] = Enum.GetName(typeof(QuestManager.QuestStatus), questState.Status);
			array[2] = questState.Progress;
			array[3] = record.Quota;
			int num = 4;
			DbfLocValue name = record.Name;
			array[num] = (((name != null) ? name.GetString(true) : null) ?? "<no name>");
			return string.Format(format, array);
		}

		// Token: 0x0600BFB6 RID: 49078 RVA: 0x003A6CB8 File Offset: 0x003A4EB8
		private string GetTimeUntilNextQuestDebugString(int questPoolId)
		{
			DateTime d;
			if (!this.m_questPoolNextQuestTime.TryGetValue(questPoolId, out d))
			{
				return "unknown";
			}
			TimeSpan timeSpan = d - DateTime.Now;
			string result;
			if (timeSpan <= TimeSpan.Zero)
			{
				result = "now";
			}
			else
			{
				result = TimeUtils.GetDevElapsedTimeString(timeSpan);
			}
			return result;
		}

		// Token: 0x0600BFB7 RID: 49079 RVA: 0x003A6D04 File Offset: 0x003A4F04
		public bool DebugScheduleCheckForNewQuests(float delaySeconds)
		{
			this.m_checkForNewQuestsIntervalSecs = this.ResetCheckForNewQuestsInterval();
			return this.ScheduleCheckForNewQuests(new float?(delaySeconds));
		}

		// Token: 0x0600BFB8 RID: 49080 RVA: 0x003A6D1E File Offset: 0x003A4F1E
		public void SimulateQuestProgress(int questId)
		{
			QuestManager.OnQuestProgressHandler onQuestProgress = this.OnQuestProgress;
			if (onQuestProgress == null)
			{
				return;
			}
			onQuestProgress(this.CreateQuestDataModelById(questId));
		}

		// Token: 0x0600BFB9 RID: 49081 RVA: 0x003A6D37 File Offset: 0x003A4F37
		public void SimulateQuestNotificationPopup(QuestPool.QuestPoolType poolType)
		{
			this.m_questPoolTypesToShow.Clear();
			this.m_questPoolTypesToShow.Add(poolType);
		}

		// Token: 0x04009B81 RID: 39809
		public static readonly AssetReference QUEST_NOTIFICATION_PREFAB = new AssetReference("QuestNotificationPopup.prefab:23a71f92e200b3243b16be8e4d42c0c8");

		// Token: 0x04009B82 RID: 39810
		private Map<int, PlayerQuestState> m_questState = new Map<int, PlayerQuestState>();

		// Token: 0x04009B83 RID: 39811
		private Map<int, PlayerQuestPoolState> m_questPoolState = new Map<int, PlayerQuestPoolState>();

		// Token: 0x04009B84 RID: 39812
		private HashSet<QuestPool.QuestPoolType> m_questPoolTypesToShow = new HashSet<QuestPool.QuestPoolType>();

		// Token: 0x04009B85 RID: 39813
		private bool m_showQuestPoolTypesFromLogin;

		// Token: 0x04009B86 RID: 39814
		private Map<int, DateTime> m_questPoolNextQuestTime = new Map<int, DateTime>();

		// Token: 0x04009B87 RID: 39815
		private const float CHECK_FOR_NEW_QUESTS_MIN_INTERVAL_SECS = 2f;

		// Token: 0x04009B88 RID: 39816
		private float m_checkForNewQuestsIntervalSecs = 2f;

		// Token: 0x04009B89 RID: 39817
		private float m_checkForNewQuestsIntervalJitterSecs;

		// Token: 0x04009B8A RID: 39818
		private readonly RewardPresenter m_rewardPresenter = new RewardPresenter();

		// Token: 0x04009B8B RID: 39819
		private const int START_OF_STARTER_CHAIN_QUEST_ID = 104;

		// Token: 0x04009B8C RID: 39820
		private const int END_OF_STARTER_CHAIN_QUEST_ID = 108;

		// Token: 0x04009B8D RID: 39821
		private const int INVALID_QUEST_POOL_ID = 0;

		// Token: 0x04009B8E RID: 39822
		private const int DAILY_QUEST_POOL_ID = 1;

		// Token: 0x04009B8F RID: 39823
		private const int WEEKLY_QUEST_POOL_ID = 2;

		// Token: 0x04009B90 RID: 39824
		private const int WEEKLY_QUEST_STATIC_POOL_ID = 4;

		// Token: 0x020028DF RID: 10463
		public enum QuestStatus
		{
			// Token: 0x0400FB1A RID: 64282
			UNKNOWN,
			// Token: 0x0400FB1B RID: 64283
			NEW,
			// Token: 0x0400FB1C RID: 64284
			ACTIVE,
			// Token: 0x0400FB1D RID: 64285
			COMPLETED,
			// Token: 0x0400FB1E RID: 64286
			REWARD_GRANTED,
			// Token: 0x0400FB1F RID: 64287
			REWARD_ACKED,
			// Token: 0x0400FB20 RID: 64288
			REROLLED,
			// Token: 0x0400FB21 RID: 64289
			RESET
		}

		// Token: 0x020028E0 RID: 10464
		public enum QuestTileDisplayMode
		{
			// Token: 0x0400FB23 RID: 64291
			DEFAULT,
			// Token: 0x0400FB24 RID: 64292
			NEXT_QUEST_TIME
		}

		// Token: 0x020028E1 RID: 10465
		// (Invoke) Token: 0x06013D2D RID: 81197
		public delegate void OnQuestRerolledHandler(int rerolledQuestId, int grantedQuestId, bool success);

		// Token: 0x020028E2 RID: 10466
		// (Invoke) Token: 0x06013D31 RID: 81201
		public delegate void OnQuestRerollCountChangedHandler(int questPoolId, int rerollCount);

		// Token: 0x020028E3 RID: 10467
		// (Invoke) Token: 0x06013D35 RID: 81205
		public delegate void OnQuestProgressHandler(QuestDataModel questDataModel);
	}
}
