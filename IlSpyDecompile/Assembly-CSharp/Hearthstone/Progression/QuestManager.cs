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
	public class QuestManager : IService
	{
		public enum QuestStatus
		{
			UNKNOWN,
			NEW,
			ACTIVE,
			COMPLETED,
			REWARD_GRANTED,
			REWARD_ACKED,
			REROLLED,
			RESET
		}

		public enum QuestTileDisplayMode
		{
			DEFAULT,
			NEXT_QUEST_TIME
		}

		public delegate void OnQuestRerolledHandler(int rerolledQuestId, int grantedQuestId, bool success);

		public delegate void OnQuestRerollCountChangedHandler(int questPoolId, int rerollCount);

		public delegate void OnQuestProgressHandler(QuestDataModel questDataModel);

		public static readonly AssetReference QUEST_NOTIFICATION_PREFAB = new AssetReference("QuestNotificationPopup.prefab:23a71f92e200b3243b16be8e4d42c0c8");

		private Map<int, PlayerQuestState> m_questState = new Map<int, PlayerQuestState>();

		private Map<int, PlayerQuestPoolState> m_questPoolState = new Map<int, PlayerQuestPoolState>();

		private HashSet<QuestPool.QuestPoolType> m_questPoolTypesToShow = new HashSet<QuestPool.QuestPoolType>();

		private bool m_showQuestPoolTypesFromLogin;

		private Map<int, DateTime> m_questPoolNextQuestTime = new Map<int, DateTime>();

		private const float CHECK_FOR_NEW_QUESTS_MIN_INTERVAL_SECS = 2f;

		private float m_checkForNewQuestsIntervalSecs = 2f;

		private float m_checkForNewQuestsIntervalJitterSecs;

		private readonly RewardPresenter m_rewardPresenter = new RewardPresenter();

		private const int START_OF_STARTER_CHAIN_QUEST_ID = 104;

		private const int END_OF_STARTER_CHAIN_QUEST_ID = 108;

		private const int INVALID_QUEST_POOL_ID = 0;

		private const int DAILY_QUEST_POOL_ID = 1;

		private const int WEEKLY_QUEST_POOL_ID = 2;

		private const int WEEKLY_QUEST_STATIC_POOL_ID = 4;

		public bool IsSystemEnabled { get; private set; }

		public event OnQuestRerolledHandler OnQuestRerolled;

		public event OnQuestRerollCountChangedHandler OnQuestRerollCountChanged;

		public event OnQuestProgressHandler OnQuestProgress;

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += WillReset;
			Network network = serviceLocator.Get<Network>();
			network.RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
			network.RegisterNetHandler(PlayerQuestStateUpdate.PacketID.ID, ReceivePlayerQuestStateUpdateMessage);
			network.RegisterNetHandler(PlayerQuestPoolStateUpdate.PacketID.ID, ReceivePlayerQuestPoolStateUpdateMessage);
			network.RegisterNetHandler(RerollQuestResponse.PacketID.ID, ReceiveRerollQuestResponseMessage);
			yield break;
		}

		public Type[] GetDependencies()
		{
			return new Type[1] { typeof(Network) };
		}

		public void Shutdown()
		{
		}

		private void WillReset()
		{
			IsSystemEnabled = false;
			m_showQuestPoolTypesFromLogin = false;
			m_questState.Clear();
			m_questPoolState.Clear();
			m_questPoolTypesToShow.Clear();
			m_rewardPresenter.Clear();
			m_questPoolNextQuestTime.Clear();
			m_checkForNewQuestsIntervalSecs = ResetCheckForNewQuestsInterval();
			Processor.CancelScheduledCallback(CheckForNewQuestsScheduledCallback);
		}

		public static QuestManager Get()
		{
			return HearthstoneServices.Get<QuestManager>();
		}

		public QuestDataModel CreateQuestDataModelById(int questId)
		{
			if (!m_questState.TryGetValue(questId, out var value))
			{
				value = new PlayerQuestState
				{
					QuestId = questId
				};
			}
			return CreateQuestDataModel(value);
		}

		public RewardScrollDataModel CreateRewardScrollDataModelByQuestId(int questId, List<RewardItemOutput> rewardItemOutput = null)
		{
			QuestDbfRecord record = GameDbf.Quest.GetRecord(questId);
			if (record == null)
			{
				return new RewardScrollDataModel();
			}
			RewardScrollDataModel rewardScrollDataModel = new RewardScrollDataModel();
			DbfLocValue name = record.Name;
			rewardScrollDataModel.DisplayName = ((name != null) ? ((string)name) : string.Empty);
			rewardScrollDataModel.Description = ProgressUtils.FormatDescription(record.Description, record.Quota);
			rewardScrollDataModel.RewardList = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList, 0, rewardItemOutput);
			return rewardScrollDataModel;
		}

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
			foreach (int questPoolId in list2)
			{
				int num = 0;
				foreach (PlayerQuestState item in from q in m_questState.Values.Where(delegate(PlayerQuestState q)
					{
						QuestDbfRecord record2 = GameDbf.Quest.GetRecord(q.QuestId);
						return record2 != null && record2.QuestPool == questPoolId && IsQuestActive(q);
					})
					orderby q.QuestId descending
					select q)
				{
					questListDataModel.Quests.Add(CreateQuestDataModel(item));
					num++;
				}
				if (!CanBeGrantedPoolQuests() || !appendTimeUntilNextQuest)
				{
					continue;
				}
				QuestPoolDbfRecord record = GameDbf.QuestPool.GetRecord(questPoolId);
				if (record == null)
				{
					continue;
				}
				int b = Mathf.Max(record.MaxQuestsActive - num, 0);
				int count = Mathf.Min(record.NumQuestsGranted, b);
				foreach (QuestDataModel item2 in Enumerable.Repeat(CreateNextQuestTimeDataModel(record), count))
				{
					questListDataModel.Quests.Add(item2);
				}
			}
			return questListDataModel;
		}

		public bool ShowQuestNotification(Action callback)
		{
			if (m_questPoolTypesToShow.Count == 0)
			{
				return false;
			}
			List<PlayerQuestState> list2 = new List<PlayerQuestState>();
			if (m_questPoolTypesToShow.Contains(QuestPool.QuestPoolType.NONE) || m_questPoolTypesToShow.Contains(QuestPool.QuestPoolType.DAILY))
			{
				m_questPoolTypesToShow.Remove(QuestPool.QuestPoolType.NONE);
				m_questPoolTypesToShow.Remove(QuestPool.QuestPoolType.DAILY);
				list2 = GetActiveQuestStatesForPool(QuestPool.QuestPoolType.NONE);
				list2.AddRange(GetActiveQuestStatesForPool(QuestPool.QuestPoolType.DAILY));
			}
			else
			{
				if (!m_questPoolTypesToShow.Contains(QuestPool.QuestPoolType.WEEKLY))
				{
					return false;
				}
				m_questPoolTypesToShow.Remove(QuestPool.QuestPoolType.WEEKLY);
				list2 = GetActiveQuestStatesForPool(QuestPool.QuestPoolType.WEEKLY);
			}
			if (list2.Count == 0)
			{
				return false;
			}
			QuestListDataModel questListDataModel = new QuestListDataModel
			{
				Quests = (from state in list2
					orderby state.QuestId descending
					select CreateQuestDataModel(state) into dataModel
					orderby dataModel.PoolType
					select dataModel).Aggregate(new DataModelList<QuestDataModel>(), delegate(DataModelList<QuestDataModel> list, QuestDataModel dataModel)
				{
					dataModel.RerollCount = 0;
					list.Add(dataModel);
					return list;
				})
			};
			bool showIKS = m_showQuestPoolTypesFromLogin;
			m_showQuestPoolTypesFromLogin = false;
			Widget widget = WidgetInstance.Create(QUEST_NOTIFICATION_PREFAB);
			widget.RegisterReadyListener(delegate
			{
				QuestNotificationPopup componentInChildren = widget.GetComponentInChildren<QuestNotificationPopup>();
				componentInChildren.Initialize(RewardTrackManager.Get().TrackDataModel, questListDataModel, callback, showIKS);
				componentInChildren.Show();
			});
			return true;
		}

		public bool AckQuest(int questId)
		{
			if (!m_questState.TryGetValue(questId, out var value))
			{
				return false;
			}
			if (!NeedsAck(value))
			{
				return false;
			}
			switch (value.Status)
			{
			case 1:
				value.Status = 2;
				break;
			case 4:
				value.Status = 5;
				break;
			}
			Network.Get().AckQuest(questId);
			return false;
		}

		public bool RerollQuest(int questId)
		{
			if (!m_questState.TryGetValue(questId, out var value))
			{
				return false;
			}
			if (!IsQuestActive(value))
			{
				return false;
			}
			QuestDbfRecord record = GameDbf.Quest.GetRecord(value.QuestId);
			if (record == null)
			{
				return false;
			}
			if (GetQuestPoolRerollCount(record.QuestPool) <= 0)
			{
				return false;
			}
			Network.Get().RerollQuest(questId);
			return true;
		}

		public bool ShowNextReward(Action callback)
		{
			return m_rewardPresenter.ShowNextReward(callback);
		}

		public bool CanBeGrantedPoolQuests()
		{
			foreach (PlayerQuestState value in m_questState.Values)
			{
				if (value.QuestId >= 104 && value.QuestId <= 108 && IsQuestActive(value))
				{
					return false;
				}
			}
			return true;
		}

		private QuestDataModel CreateQuestDataModel(PlayerQuestState questState)
		{
			QuestDbfRecord record = GameDbf.Quest.GetRecord(questState.QuestId);
			if (record == null)
			{
				return new QuestDataModel();
			}
			DataModelList<string> dataModelList = new DataModelList<string>();
			(from s in record.Icon?.Split(',')
				select s.Trim()).Aggregate(dataModelList, delegate(DataModelList<string> list, string element)
			{
				list.Add(element);
				return list;
			});
			return new QuestDataModel
			{
				QuestId = questState.QuestId,
				PoolId = GetQuestPoolId(record),
				PoolType = GetQuestPoolType(record),
				DisplayMode = QuestTileDisplayMode.DEFAULT,
				Name = record.Name?.GetString(),
				Description = ProgressUtils.FormatDescription(record.Description, record.Quota),
				Icon = dataModelList,
				Progress = questState.Progress,
				Quota = record.Quota,
				RerollCount = GetQuestPoolRerollCount(record.QuestPool),
				Rewards = RewardUtils.CreateRewardListDataModelFromRewardListId(record.RewardList),
				RewardTrackXp = RewardTrackManager.Get().ApplyXpBonusPercent(record.RewardTrackXp),
				ProgressMessage = ProgressUtils.FormatProgressMessage(questState.Progress, record.Quota),
				Status = (QuestStatus)questState.Status
			};
		}

		private QuestDataModel CreateNextQuestTimeDataModel(QuestPoolDbfRecord questPoolRecord)
		{
			QuestDataModel questDataModel = new QuestDataModel();
			questDataModel.DisplayMode = QuestTileDisplayMode.NEXT_QUEST_TIME;
			questDataModel.PoolType = questPoolRecord.QuestPoolType;
			questDataModel.TimeUntilNextQuest = GameStrings.Format("GLOBAL_PROGRESSION_QUEST_TIME_UNTIL_NEXT", GetTimeUntilNextQuestString(questPoolRecord.ID));
			return questDataModel;
		}

		private bool NeedsAck(PlayerQuestState questState)
		{
			QuestStatus status = (QuestStatus)questState.Status;
			if (status == QuestStatus.NEW || status == QuestStatus.REWARD_GRANTED)
			{
				return true;
			}
			return false;
		}

		private bool IsQuestActive(PlayerQuestState questState)
		{
			QuestStatus status = (QuestStatus)questState.Status;
			if ((uint)(status - 1) <= 1u)
			{
				return true;
			}
			return false;
		}

		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				IsSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
				m_checkForNewQuestsIntervalJitterSecs = initialClientState.GuardianVars.CheckForNewQuestsIntervalJitterSecs;
			}
		}

		private void ReceivePlayerQuestStateUpdateMessage()
		{
			PlayerQuestStateUpdate playerQuestStateUpdate = Network.Get().GetPlayerQuestStateUpdate();
			if (playerQuestStateUpdate == null)
			{
				return;
			}
			foreach (PlayerQuestState item in playerQuestStateUpdate.Quest)
			{
				m_questState.TryGetValue(item.QuestId, out var value);
				m_questState[item.QuestId] = item;
				HandlePlayerQuestStateChange(value, item);
			}
			if (playerQuestStateUpdate.ShowQuestNotificationForPoolType.Count > 0 && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
			{
				m_showQuestPoolTypesFromLogin = true;
			}
			foreach (int item2 in playerQuestStateUpdate.ShowQuestNotificationForPoolType)
			{
				m_questPoolTypesToShow.Add((QuestPool.QuestPoolType)item2);
			}
		}

		private void HandlePlayerQuestStateChange(PlayerQuestState oldState, PlayerQuestState newState)
		{
			if (!IsSystemEnabled || newState == null)
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
				m_questPoolTypesToShow.Add(GetQuestPoolType(record));
				break;
			case 2:
			case 3:
				if (oldState != null && oldState.Progress < newState.Progress)
				{
					this.OnQuestProgress?.Invoke(CreateQuestDataModel(newState));
				}
				break;
			case 4:
				if (record.RewardList == 0)
				{
					AckQuest(newState.QuestId);
					break;
				}
				m_rewardPresenter.EnqueueReward(CreateRewardScrollDataModelByQuestId(newState.QuestId, newState.RewardItemOutput), delegate
				{
					AckQuest(newState.QuestId);
				});
				break;
			default:
				Debug.LogWarningFormat("QuestManager: unknown status {0} for quest id {1}", newState.Status, newState.QuestId);
				break;
			case 5:
			case 6:
			case 7:
				break;
			}
		}

		private void ReceivePlayerQuestPoolStateUpdateMessage()
		{
			PlayerQuestPoolStateUpdate playerQuestPoolStateUpdate = Network.Get().GetPlayerQuestPoolStateUpdate();
			if (playerQuestPoolStateUpdate == null)
			{
				return;
			}
			foreach (PlayerQuestPoolState item in playerQuestPoolStateUpdate.QuestPool)
			{
				m_questPoolState[item.QuestPoolId] = item;
				this.OnQuestRerollCountChanged?.Invoke(item.QuestPoolId, item.RerollAvailableCount);
				DateTime value = DateTime.Now.AddSeconds(item.SecondsUntilNextGrant);
				m_questPoolNextQuestTime[item.QuestPoolId] = value;
			}
			m_checkForNewQuestsIntervalSecs = ResetCheckForNewQuestsInterval();
			ScheduleCheckForNewQuests();
		}

		private void ReceiveRerollQuestResponseMessage()
		{
			RerollQuestResponse rerollQuestResponse = Network.Get().GetRerollQuestResponse();
			if (rerollQuestResponse != null && GameDbf.Quest.GetRecord(rerollQuestResponse.RerolledQuestId) != null)
			{
				this.OnQuestRerolled?.Invoke(rerollQuestResponse.RerolledQuestId, rerollQuestResponse.GrantedQuestId, rerollQuestResponse.Success);
			}
		}

		private int GetQuestPoolId(QuestDbfRecord questAsset)
		{
			return questAsset?.QuestPoolRecord?.ID ?? 0;
		}

		private QuestPool.QuestPoolType GetQuestPoolType(QuestDbfRecord questAsset)
		{
			return questAsset?.QuestPoolRecord?.QuestPoolType ?? QuestPool.QuestPoolType.NONE;
		}

		private List<PlayerQuestState> GetActiveQuestStatesForPool(QuestPool.QuestPoolType questPoolType)
		{
			return (from state in m_questState.Values
				where IsQuestActive(state)
				where GetQuestPoolType(GameDbf.Quest.GetRecord(state.QuestId)) == questPoolType
				select state).ToList();
		}

		private List<PlayerQuestState> GetQuestStatesForPool(int questPoolId)
		{
			return m_questState.Values.Where(delegate(PlayerQuestState state)
			{
				QuestDbfRecord record = GameDbf.Quest.GetRecord(state.QuestId);
				return record != null && record.QuestPool == questPoolId;
			}).ToList();
		}

		private string GetTimeUntilNextQuestString(int questPoolId)
		{
			if (!m_questPoolNextQuestTime.TryGetValue(questPoolId, out var value))
			{
				return "";
			}
			TimeSpan timeSpan = value - DateTime.Now;
			if (timeSpan <= TimeSpan.Zero)
			{
				return "";
			}
			return TimeUtils.GetElapsedTimeString((long)timeSpan.TotalSeconds, TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET, roundUp: true);
		}

		private int GetQuestPoolRerollCount(int questPoolId)
		{
			int result = 0;
			if (m_questPoolState.TryGetValue(questPoolId, out var value))
			{
				result = value.RerollAvailableCount;
			}
			return result;
		}

		private bool ScheduleCheckForNewQuests(float? delaySecondsOverride = null)
		{
			Processor.CancelScheduledCallback(CheckForNewQuestsScheduledCallback);
			float num = NextCheckForNewQuestsInterval();
			if (delaySecondsOverride.HasValue)
			{
				num = delaySecondsOverride.Value;
			}
			else
			{
				TimeSpan? timeSpan = null;
				foreach (KeyValuePair<int, DateTime> item in m_questPoolNextQuestTime)
				{
					_ = item.Key;
					TimeSpan timeSpan2 = item.Value - DateTime.Now;
					if (timeSpan.HasValue)
					{
						TimeSpan value = timeSpan2;
						TimeSpan? timeSpan3 = timeSpan;
						if (!(value < timeSpan3))
						{
							continue;
						}
					}
					timeSpan = timeSpan2;
				}
				if (timeSpan.HasValue && timeSpan.Value.TotalSeconds > (double)num)
				{
					num = (float)timeSpan.Value.TotalSeconds;
				}
			}
			return Processor.ScheduleCallback(num, realTime: true, CheckForNewQuestsScheduledCallback);
		}

		private void CheckForNewQuestsScheduledCallback(object userData)
		{
			if (Network.IsLoggedIn())
			{
				if (!GameMgr.Get().IsFindingGame())
				{
					Network.Get().CheckForNewQuests();
				}
				ScheduleCheckForNewQuests();
			}
		}

		private float ResetCheckForNewQuestsInterval()
		{
			m_checkForNewQuestsIntervalSecs = 2f;
			return m_checkForNewQuestsIntervalSecs;
		}

		private float NextCheckForNewQuestsInterval()
		{
			float result = m_checkForNewQuestsIntervalSecs + UnityEngine.Random.Range(0f, m_checkForNewQuestsIntervalJitterSecs);
			m_checkForNewQuestsIntervalSecs *= 2f;
			return result;
		}

		public string GetQuestDebugHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!IsSystemEnabled)
			{
				stringBuilder.AppendLine("SYSTEM DISABLED");
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine("---------- Daily Quests ----------");
			AppendQuestPoolStateStringForDebugHud(stringBuilder, 1);
			AppendQuestStateStringsForDebugHud(stringBuilder, GetQuestStatesForPool(1));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Weekly Quests ----------");
			AppendQuestPoolStateStringForDebugHud(stringBuilder, 2);
			AppendQuestStateStringsForDebugHud(stringBuilder, GetQuestStatesForPool(2));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Weekly Quests (Static) ----------");
			AppendQuestPoolStateStringForDebugHud(stringBuilder, 4);
			AppendQuestStateStringsForDebugHud(stringBuilder, GetQuestStatesForPool(4));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Other Quests ----------");
			AppendQuestStateStringsForDebugHud(stringBuilder, GetQuestStatesForPool(0));
			return stringBuilder.ToString();
		}

		private void AppendQuestPoolStateStringForDebugHud(StringBuilder sb, int questPoolId)
		{
			int questPoolRerollCount = GetQuestPoolRerollCount(questPoolId);
			sb.AppendFormat("Rerolls: {0} | Next Quest In: \"{1}\" ({2})\n", questPoolRerollCount, GetTimeUntilNextQuestString(questPoolId), GetTimeUntilNextQuestDebugString(questPoolId));
		}

		private void AppendQuestStateStringsForDebugHud(StringBuilder sb, List<PlayerQuestState> questStates)
		{
			foreach (PlayerQuestState item in from q in questStates
				orderby q.Status, q.QuestId
				select q)
			{
				sb.AppendLine(QuestStateToString(item));
			}
		}

		private string QuestStateToString(PlayerQuestState questState)
		{
			QuestDbfRecord record = GameDbf.Quest.GetRecord(questState.QuestId);
			if (record == null)
			{
				return $"id={questState.QuestId} INVALID";
			}
			return string.Format("id={0} {1} [{2}/{3}] \"{4}\"", questState.QuestId, Enum.GetName(typeof(QuestStatus), questState.Status), questState.Progress, record.Quota, record.Name?.GetString() ?? "<no name>");
		}

		private string GetTimeUntilNextQuestDebugString(int questPoolId)
		{
			if (!m_questPoolNextQuestTime.TryGetValue(questPoolId, out var value))
			{
				return "unknown";
			}
			TimeSpan timeSpan = value - DateTime.Now;
			if (timeSpan <= TimeSpan.Zero)
			{
				return "now";
			}
			return TimeUtils.GetDevElapsedTimeString(timeSpan);
		}

		public bool DebugScheduleCheckForNewQuests(float delaySeconds)
		{
			m_checkForNewQuestsIntervalSecs = ResetCheckForNewQuestsInterval();
			return ScheduleCheckForNewQuests(delaySeconds);
		}

		public void SimulateQuestProgress(int questId)
		{
			this.OnQuestProgress?.Invoke(CreateQuestDataModelById(questId));
		}

		public void SimulateQuestNotificationPopup(QuestPool.QuestPoolType poolType)
		{
			m_questPoolTypesToShow.Clear();
			m_questPoolTypesToShow.Add(poolType);
		}
	}
}
