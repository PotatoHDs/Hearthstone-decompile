using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.DataModels;
using Hearthstone.Extensions;
using Hearthstone.UI;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class AchievementManager : IService
	{
		public enum AchievementStatus
		{
			UNKNOWN,
			ACTIVE,
			COMPLETED,
			REWARD_GRANTED,
			REWARD_ACKED,
			RESET
		}

		public delegate void StatusChangedDelegate(int achievementId, AchievementStatus status);

		public delegate void ProgressChangedDelegate(int achievementId, int progress);

		public delegate void CompletedDateChangedDelegate(int achievementId, long completedDate);

		public const long DefaultCompletedDate = 0L;

		public const int DefaultProgress = 0;

		public const AchievementStatus DefaultStatus = AchievementStatus.ACTIVE;

		public static readonly AssetReference ACHIEVEMENT_TOAST_PREFAB = new AssetReference("AchievementToast.prefab:9fa72c338c657d54fb9f63de21c1e4f8");

		private readonly PlayerState<PlayerAchievementState> m_playerState = new PlayerState<PlayerAchievementState>(CreateDefaultPlayerState);

		private readonly Map<int, int> m_claimingAchievements = new Map<int, int>();

		private readonly Map<int, int> m_achievementInGameProgress = new Map<int, int>();

		private Lazy<AchievementCategoryListDataModel> m_categories;

		private Queue<int> m_completedAchievements;

		private readonly RewardPresenter m_rewardPresenter = new RewardPresenter();

		private Widget m_achievementToast;

		private string m_previousToastName;

		private int m_toastNotificationSuppressionCount;

		private float m_endOfTurnToastPauseBufferSecs = 1.5f;

		private readonly AchievementStats m_stats;

		private readonly HashSet<AchievementSectionDbfRecord> m_dirtySections = new HashSet<AchievementSectionDbfRecord>();

		public bool IsSystemEnabled { get; private set; }

		public bool ToastNotificationsPaused => m_toastNotificationSuppressionCount > 0;

		public AchievementCategoryListDataModel Categories => m_categories.Value;

		public event StatusChangedDelegate OnStatusChanged = delegate
		{
		};

		public event ProgressChangedDelegate OnProgressChanged = delegate
		{
		};

		public event CompletedDateChangedDelegate OnCompletedDateChanged = delegate
		{
		};

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += WillReset;
			Network network = serviceLocator.Get<Network>();
			network.RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
			network.RegisterNetHandler(PlayerAchievementStateUpdate.PacketID.ID, ReceiveAchievementStateUpdateMessage);
			network.RegisterNetHandler(GameSetup.PacketID.ID, OnGameSetup);
			network.RegisterNetHandler(AchievementProgress.PacketID.ID, OnAchievementInGameProgress);
			network.RegisterNetHandler(AchievementComplete.PacketID.ID, OnAchievementComplete);
			m_playerState.OnStateChanged += OnStateChanged;
			m_toastNotificationSuppressionCount = 0;
			m_achievementToast = null;
			m_previousToastName = string.Empty;
			yield break;
		}

		public Type[] GetDependencies()
		{
			return new Type[1] { typeof(Network) };
		}

		public void Shutdown()
		{
			m_completedAchievements.Clear();
		}

		private void WillReset()
		{
			IsSystemEnabled = false;
			m_playerState.Reset();
			m_claimingAchievements.Clear();
			m_categories = new Lazy<AchievementCategoryListDataModel>(InitializeCategories);
			m_rewardPresenter.Clear();
			m_completedAchievements = new Queue<int>();
			m_achievementInGameProgress.Clear();
			m_previousToastName = string.Empty;
			m_achievementToast = null;
			m_toastNotificationSuppressionCount = 0;
			m_stats.InvalidateAll();
			m_dirtySections.Clear();
		}

		public static AchievementManager Get()
		{
			return HearthstoneServices.Get<AchievementManager>();
		}

		public AchievementManager()
		{
			m_categories = new Lazy<AchievementCategoryListDataModel>(InitializeCategories);
			m_completedAchievements = new Queue<int>();
			m_stats = new AchievementStats(m_playerState.GetState);
		}

		public AchievementDataModel GetAchievementDataModel(int achievementId)
		{
			return AchievementFactory.CreateAchievementDataModel(GameDbf.Achievement.GetRecord(achievementId), m_playerState.GetState(achievementId));
		}

		public bool IsAchievementComplete(int achievementId)
		{
			return ProgressUtils.IsAchievementComplete((AchievementStatus)m_playerState.GetState(achievementId).Status);
		}

		public bool ClaimAchievementReward(int achievementId, int chooseOneRewardItemId = 0)
		{
			if (m_playerState.GetState(achievementId).Status != 2)
			{
				return false;
			}
			if (m_claimingAchievements.ContainsKey(achievementId))
			{
				return false;
			}
			Network.Get().ClaimAchievementReward(achievementId, chooseOneRewardItemId);
			m_claimingAchievements.Add(achievementId, chooseOneRewardItemId);
			return true;
		}

		public void AckAchievement(int achievementId)
		{
			PlayerAchievementState state = m_playerState.GetState(achievementId);
			if (state.Status == 3)
			{
				state.Status = 4;
				m_playerState.UpdateState(achievementId, state);
				Network.Get().AckAchievement(achievementId);
			}
		}

		public float GetNotificationPauseBufferSeconds()
		{
			return m_endOfTurnToastPauseBufferSecs;
		}

		public bool IsShowingToast()
		{
			return m_achievementToast != null;
		}

		public bool CheckToastQueue()
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject != null && netObject.AchievementToastDisabled)
			{
				return false;
			}
			if (m_completedAchievements.Count > 0)
			{
				ShowAchievementComplete(m_completedAchievements.Dequeue());
				return true;
			}
			return false;
		}

		public void PauseToastNotifications()
		{
			m_toastNotificationSuppressionCount++;
		}

		public void UnpauseToastNotifications()
		{
			if (m_toastNotificationSuppressionCount != 0)
			{
				m_toastNotificationSuppressionCount--;
				if (!ToastNotificationsPaused)
				{
					CheckToastQueue();
				}
			}
		}

		public void ShowAchievementComplete(int achievementId)
		{
			if (!IsSystemEnabled)
			{
				return;
			}
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject != null && netObject.AchievementToastDisabled)
			{
				return;
			}
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achievementId);
			if (record == null)
			{
				return;
			}
			string @string = record.Name.GetString();
			if (IsShowingToast() || ToastNotificationsPaused)
			{
				if (!@string.Equals(m_previousToastName))
				{
					m_completedAchievements.Enqueue(achievementId);
				}
				return;
			}
			m_previousToastName = @string;
			m_achievementToast = WidgetInstance.Create(ACHIEVEMENT_TOAST_PREFAB);
			m_achievementToast.RegisterReadyListener(delegate
			{
				AchievementToast componentInChildren = m_achievementToast.GetComponentInChildren<AchievementToast>();
				componentInChildren.Initialize(GetAchievementDataModel(achievementId));
				componentInChildren.Show();
			});
			m_achievementToast.RegisterDeactivatedListener(delegate
			{
				m_achievementToast = null;
				if (!CheckToastQueue())
				{
					m_previousToastName = string.Empty;
					SocialToastMgr.Get().CheckToastQueue();
				}
			});
		}

		public bool ShowNextReward(Action callback)
		{
			return m_rewardPresenter.ShowNextReward(callback);
		}

		public void SelectSubcategory(AchievementCategoryDataModel category, AchievementSubcategoryDataModel subcategory)
		{
			category.SelectSubcategory(subcategory, m_playerState.GetState);
		}

		private static PlayerAchievementState CreateDefaultPlayerState(int id)
		{
			return new PlayerAchievementState
			{
				HasAchievementId = true,
				AchievementId = id,
				HasStatus = true,
				Status = 1,
				HasProgress = true,
				Progress = 0,
				HasCompletedDate = true,
				CompletedDate = 0L
			};
		}

		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				IsSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
				m_endOfTurnToastPauseBufferSecs = initialClientState.GuardianVars.EndOfTurnToastPauseBufferSecs;
			}
		}

		private void ReceiveAchievementStateUpdateMessage()
		{
			PlayerAchievementStateUpdate playerAchievementStateUpdate = Network.Get().GetPlayerAchievementStateUpdate();
			if (playerAchievementStateUpdate == null)
			{
				return;
			}
			foreach (PlayerAchievementState item in playerAchievementStateUpdate.Achievement)
			{
				m_playerState.UpdateState(item.AchievementId, item);
			}
			UpdateStats();
		}

		private void OnGameSetup()
		{
			m_achievementInGameProgress.Clear();
			foreach (PlayerAchievementState item in m_playerState)
			{
				m_achievementInGameProgress[item.AchievementId] = item.Progress;
			}
		}

		private void OnAchievementInGameProgress()
		{
			if (SpectatorManager.Get().IsSpectatingOrWatching)
			{
				return;
			}
			AchievementProgress achievementInGameProgress = Network.Get().GetAchievementInGameProgress();
			if (achievementInGameProgress == null || !achievementInGameProgress.HasAchievementId || !achievementInGameProgress.HasOpType || !achievementInGameProgress.HasAmount)
			{
				return;
			}
			int achievementId = achievementInGameProgress.AchievementId;
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achievementId);
			if (record == null)
			{
				return;
			}
			m_achievementInGameProgress.TryGetValue(achievementId, out var value);
			if (value >= record.Quota)
			{
				return;
			}
			int num = value;
			switch (achievementInGameProgress.OpType)
			{
			case ProgOpType.PROG_OP_ADD:
				value += achievementInGameProgress.Amount;
				break;
			case ProgOpType.PROG_OP_SET:
				value = achievementInGameProgress.Amount;
				break;
			}
			if (value != num)
			{
				m_achievementInGameProgress[achievementId] = value;
				if (num < record.Quota && value >= record.Quota)
				{
					ShowAchievementComplete(achievementId);
				}
			}
		}

		private void OnAchievementComplete()
		{
			AchievementComplete achievementComplete = Network.Get().GetAchievementComplete();
			if (achievementComplete == null)
			{
				return;
			}
			foreach (int achievementId in achievementComplete.AchievementIds)
			{
				ShowAchievementComplete(achievementId);
			}
		}

		private void OnStateChanged(PlayerAchievementState oldState, PlayerAchievementState newState)
		{
			if (IsSystemEnabled && newState != null)
			{
				if (oldState.Status != newState.Status)
				{
					UpdateStatus(newState.AchievementId, (AchievementStatus)oldState.Status, (AchievementStatus)newState.Status, newState.RewardItemOutput);
					this.OnStatusChanged(newState.AchievementId, (AchievementStatus)newState.Status);
				}
				if (oldState.Progress != newState.Progress)
				{
					this.OnProgressChanged(newState.AchievementId, newState.Progress);
				}
				if (oldState.CompletedDate != newState.CompletedDate)
				{
					this.OnCompletedDateChanged(newState.AchievementId, newState.CompletedDate);
				}
			}
		}

		private void UpdateStatus(int achievementId, AchievementStatus oldStatus, AchievementStatus newStatus, List<RewardItemOutput> rewardItemOutput = null)
		{
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achievementId);
			if (record == null)
			{
				return;
			}
			switch (newStatus)
			{
			case AchievementStatus.REWARD_GRANTED:
			{
				if (record.AchievementVisibility == Assets.Achievement.AchievementVisibility.HIDDEN)
				{
					break;
				}
				int chooseOneRewardItemId = 0;
				if (m_claimingAchievements.ContainsKey(achievementId))
				{
					chooseOneRewardItemId = m_claimingAchievements[achievementId];
					m_claimingAchievements.Remove(achievementId);
				}
				if ((GameDbf.RewardList.GetRecord(record.RewardList)?.RewardItems?.Count ?? 0) > 0)
				{
					m_rewardPresenter.EnqueueReward(AchievementFactory.CreateRewardScrollDataModel(achievementId, chooseOneRewardItemId, rewardItemOutput), delegate
					{
						AckAchievement(achievementId);
					});
				}
				else
				{
					AckAchievement(achievementId);
				}
				break;
			}
			default:
				Debug.LogWarning($"AchievementManager: unknown status {newStatus} for achievement id {achievementId}");
				break;
			case AchievementStatus.ACTIVE:
			case AchievementStatus.COMPLETED:
			case AchievementStatus.REWARD_ACKED:
				break;
			}
			if (record.AchievementSectionRecord != null)
			{
				m_dirtySections.Add(record.AchievementSectionRecord);
			}
		}

		private void UpdateStats()
		{
			if (m_dirtySections.Count == 0)
			{
				return;
			}
			foreach (AchievementSectionDbfRecord dirtySection in m_dirtySections)
			{
				m_stats.InvalidateSection(dirtySection);
			}
			List<AchievementSubcategoryDbfRecord> dirtySubcategoryRecords = (from record in GameDbf.AchievementSubcategory.GetRecords((AchievementSubcategoryDbfRecord record) => record.Sections.Any((AchievementSectionItemDbfRecord sectionItem) => m_dirtySections.Contains(sectionItem.AchievementSectionRecord)))
				orderby record.ID
				select record).ToList();
			foreach (AchievementSubcategoryDbfRecord item in dirtySubcategoryRecords)
			{
				m_stats.InvalidSubcategory(item);
			}
			List<AchievementCategoryDbfRecord> dirtyCategoryRecords = (from record in GameDbf.AchievementCategory.GetRecords((AchievementCategoryDbfRecord record) => record.Subcategories.Intersect(dirtySubcategoryRecords).Any())
				orderby record.ID
				select record).ToList();
			foreach (AchievementCategoryDbfRecord item2 in dirtyCategoryRecords)
			{
				m_stats.InvalidateCategory(item2);
			}
			List<AchievementCategoryDataModel> list = (from dataModel in Categories.Categories
				where dirtyCategoryRecords.Any((AchievementCategoryDbfRecord record) => record.ID == dataModel.ID)
				orderby dataModel.ID
				select dataModel).ToList();
			List<AchievementSubcategoryDataModel> collection = (from dataModel in list.SelectMany((AchievementCategoryDataModel category) => category.Subcategories.Subcategories)
				where dirtySubcategoryRecords.Any((AchievementSubcategoryDbfRecord record) => record.ID == dataModel.ID)
				select dataModel into datamodel
				orderby datamodel.ID
				select datamodel).ToList();
			dirtySubcategoryRecords.Zip(collection, delegate(AchievementSubcategoryDbfRecord record, AchievementSubcategoryDataModel dataModel)
			{
				dataModel.Stats.Points = m_stats.GetSubcategoryPoints(record);
				dataModel.Stats.Unclaimed = m_stats.GetSubcategoryUnclaimed(record);
				dataModel.Stats.CompletedAchievements = m_stats.GetSubcategoryCompleted(record);
				dataModel.Stats.UpdateCompletionPercentage();
			});
			dirtyCategoryRecords.Zip(list, delegate(AchievementCategoryDbfRecord record, AchievementCategoryDataModel dataModel)
			{
				dataModel.Stats.Points = m_stats.GetCategoryPoints(record);
				dataModel.Stats.Unclaimed = m_stats.GetCategoryUnclaimed(record);
				dataModel.Stats.CompletedAchievements = m_stats.GetCategoryCompleted(record);
				dataModel.Stats.UpdateCompletionPercentage();
			});
			Categories.Stats.Points = m_stats.GetTotalPoints();
			Categories.Stats.Unclaimed = m_stats.GetTotalUnclaimed();
			Categories.Stats.CompletedAchievements = m_stats.GetTotalCompleted();
			Categories.Stats.UpdateCompletionPercentage();
			m_dirtySections.Clear();
		}

		private AchievementCategoryListDataModel InitializeCategories()
		{
			return AchievementFactory.CreateAchievementListDataModel(m_stats, m_playerState.GetState);
		}

		public AchievementDataModel Debug_GetAchievementDataModel(int achievementId)
		{
			return GetAchievementDataModel(achievementId);
		}

		public string Debug_GetAchievementHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!IsSystemEnabled)
			{
				stringBuilder.AppendLine("SYSTEM DISABLED");
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendFormat("Score: {0} / {1}", m_stats.GetTotalPoints(), CategoryList.CountAvailablePoints());
			stringBuilder.AppendLine();
			foreach (PlayerAchievementState item in from a in m_playerState
				orderby a.Status, a.AchievementId
				select a)
			{
				stringBuilder.AppendLine(Debug_AchievementStateToString(item));
			}
			return stringBuilder.ToString();
		}

		private string Debug_AchievementStateToString(PlayerAchievementState achieveState)
		{
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achieveState.AchievementId);
			if (record == null)
			{
				return $"id={achieveState.AchievementId} INVALID";
			}
			string text = "";
			if (IsAchievementComplete(achieveState.AchievementId))
			{
				text = TimeUtils.UnixTimeStampToDateTimeLocal(achieveState.CompletedDate).ToString();
			}
			return string.Format("id={0} {1} [{2}/{3}] \"{4}\" {5}", achieveState.AchievementId, Enum.GetName(typeof(AchievementStatus), achieveState.Status), achieveState.Progress, record.Quota, record.Name?.GetString() ?? "<no name>", text);
		}
	}
}
