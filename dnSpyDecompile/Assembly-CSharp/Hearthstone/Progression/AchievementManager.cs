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
	// Token: 0x02001103 RID: 4355
	public class AchievementManager : IService
	{
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x0600BEB2 RID: 48818 RVA: 0x003A1F44 File Offset: 0x003A0144
		// (remove) Token: 0x0600BEB3 RID: 48819 RVA: 0x003A1F7C File Offset: 0x003A017C
		public event AchievementManager.StatusChangedDelegate OnStatusChanged = delegate(int achievementId, AchievementManager.AchievementStatus status)
		{
		};

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x0600BEB4 RID: 48820 RVA: 0x003A1FB4 File Offset: 0x003A01B4
		// (remove) Token: 0x0600BEB5 RID: 48821 RVA: 0x003A1FEC File Offset: 0x003A01EC
		public event AchievementManager.ProgressChangedDelegate OnProgressChanged = delegate(int achievementId, int progress)
		{
		};

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x0600BEB6 RID: 48822 RVA: 0x003A2024 File Offset: 0x003A0224
		// (remove) Token: 0x0600BEB7 RID: 48823 RVA: 0x003A205C File Offset: 0x003A025C
		public event AchievementManager.CompletedDateChangedDelegate OnCompletedDateChanged = delegate(int achievementId, long completedDate)
		{
		};

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600BEB8 RID: 48824 RVA: 0x003A2091 File Offset: 0x003A0291
		// (set) Token: 0x0600BEB9 RID: 48825 RVA: 0x003A2099 File Offset: 0x003A0299
		public bool IsSystemEnabled { get; private set; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x0600BEBA RID: 48826 RVA: 0x003A20A2 File Offset: 0x003A02A2
		public bool ToastNotificationsPaused
		{
			get
			{
				return this.m_toastNotificationSuppressionCount > 0;
			}
		}

		// Token: 0x0600BEBB RID: 48827 RVA: 0x003A20AD File Offset: 0x003A02AD
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			HearthstoneApplication.Get().WillReset += this.WillReset;
			Network network = serviceLocator.Get<Network>();
			network.RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState), null);
			network.RegisterNetHandler(PlayerAchievementStateUpdate.PacketID.ID, new Network.NetHandler(this.ReceiveAchievementStateUpdateMessage), null);
			network.RegisterNetHandler(GameSetup.PacketID.ID, new Network.NetHandler(this.OnGameSetup), null);
			network.RegisterNetHandler(AchievementProgress.PacketID.ID, new Network.NetHandler(this.OnAchievementInGameProgress), null);
			network.RegisterNetHandler(AchievementComplete.PacketID.ID, new Network.NetHandler(this.OnAchievementComplete), null);
			this.m_playerState.OnStateChanged += this.OnStateChanged;
			this.m_toastNotificationSuppressionCount = 0;
			this.m_achievementToast = null;
			this.m_previousToastName = string.Empty;
			yield break;
		}

		// Token: 0x0600BEBC RID: 48828 RVA: 0x001B7846 File Offset: 0x001B5A46
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(Network)
			};
		}

		// Token: 0x0600BEBD RID: 48829 RVA: 0x003A20C3 File Offset: 0x003A02C3
		public void Shutdown()
		{
			this.m_completedAchievements.Clear();
		}

		// Token: 0x0600BEBE RID: 48830 RVA: 0x003A20D0 File Offset: 0x003A02D0
		private void WillReset()
		{
			this.IsSystemEnabled = false;
			this.m_playerState.Reset();
			this.m_claimingAchievements.Clear();
			this.m_categories = new Lazy<AchievementCategoryListDataModel>(new Func<AchievementCategoryListDataModel>(this.InitializeCategories));
			this.m_rewardPresenter.Clear();
			this.m_completedAchievements = new Queue<int>();
			this.m_achievementInGameProgress.Clear();
			this.m_previousToastName = string.Empty;
			this.m_achievementToast = null;
			this.m_toastNotificationSuppressionCount = 0;
			this.m_stats.InvalidateAll();
			this.m_dirtySections.Clear();
		}

		// Token: 0x0600BEBF RID: 48831 RVA: 0x003A2161 File Offset: 0x003A0361
		public static AchievementManager Get()
		{
			return HearthstoneServices.Get<AchievementManager>();
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x0600BEC0 RID: 48832 RVA: 0x003A2168 File Offset: 0x003A0368
		public AchievementCategoryListDataModel Categories
		{
			get
			{
				return this.m_categories.Value;
			}
		}

		// Token: 0x0600BEC1 RID: 48833 RVA: 0x003A2178 File Offset: 0x003A0378
		public AchievementManager()
		{
			this.m_categories = new Lazy<AchievementCategoryListDataModel>(new Func<AchievementCategoryListDataModel>(this.InitializeCategories));
			this.m_completedAchievements = new Queue<int>();
			this.m_stats = new AchievementStats(new Func<int, PlayerAchievementState>(this.m_playerState.GetState));
		}

		// Token: 0x0600BEC2 RID: 48834 RVA: 0x003A2286 File Offset: 0x003A0486
		public AchievementDataModel GetAchievementDataModel(int achievementId)
		{
			return AchievementFactory.CreateAchievementDataModel(GameDbf.Achievement.GetRecord(achievementId), this.m_playerState.GetState(achievementId));
		}

		// Token: 0x0600BEC3 RID: 48835 RVA: 0x003A22A4 File Offset: 0x003A04A4
		public bool IsAchievementComplete(int achievementId)
		{
			return ProgressUtils.IsAchievementComplete((AchievementManager.AchievementStatus)this.m_playerState.GetState(achievementId).Status);
		}

		// Token: 0x0600BEC4 RID: 48836 RVA: 0x003A22BC File Offset: 0x003A04BC
		public bool ClaimAchievementReward(int achievementId, int chooseOneRewardItemId = 0)
		{
			if (this.m_playerState.GetState(achievementId).Status != 2)
			{
				return false;
			}
			if (this.m_claimingAchievements.ContainsKey(achievementId))
			{
				return false;
			}
			Network.Get().ClaimAchievementReward(achievementId, chooseOneRewardItemId);
			this.m_claimingAchievements.Add(achievementId, chooseOneRewardItemId);
			return true;
		}

		// Token: 0x0600BEC5 RID: 48837 RVA: 0x003A230C File Offset: 0x003A050C
		public void AckAchievement(int achievementId)
		{
			PlayerAchievementState state = this.m_playerState.GetState(achievementId);
			if (state.Status != 3)
			{
				return;
			}
			state.Status = 4;
			this.m_playerState.UpdateState(achievementId, state);
			Network.Get().AckAchievement(achievementId);
		}

		// Token: 0x0600BEC6 RID: 48838 RVA: 0x003A234F File Offset: 0x003A054F
		public float GetNotificationPauseBufferSeconds()
		{
			return this.m_endOfTurnToastPauseBufferSecs;
		}

		// Token: 0x0600BEC7 RID: 48839 RVA: 0x003A2357 File Offset: 0x003A0557
		public bool IsShowingToast()
		{
			return this.m_achievementToast != null;
		}

		// Token: 0x0600BEC8 RID: 48840 RVA: 0x003A2368 File Offset: 0x003A0568
		public bool CheckToastQueue()
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject != null && netObject.AchievementToastDisabled)
			{
				return false;
			}
			if (this.m_completedAchievements.Count > 0)
			{
				this.ShowAchievementComplete(this.m_completedAchievements.Dequeue());
				return true;
			}
			return false;
		}

		// Token: 0x0600BEC9 RID: 48841 RVA: 0x003A23AF File Offset: 0x003A05AF
		public void PauseToastNotifications()
		{
			this.m_toastNotificationSuppressionCount++;
		}

		// Token: 0x0600BECA RID: 48842 RVA: 0x003A23BF File Offset: 0x003A05BF
		public void UnpauseToastNotifications()
		{
			if (this.m_toastNotificationSuppressionCount == 0)
			{
				return;
			}
			this.m_toastNotificationSuppressionCount--;
			if (!this.ToastNotificationsPaused)
			{
				this.CheckToastQueue();
			}
		}

		// Token: 0x0600BECB RID: 48843 RVA: 0x003A23E8 File Offset: 0x003A05E8
		public void ShowAchievementComplete(int achievementId)
		{
			if (!this.IsSystemEnabled)
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
			string @string = record.Name.GetString(true);
			if (!this.IsShowingToast() && !this.ToastNotificationsPaused)
			{
				this.m_previousToastName = @string;
				this.m_achievementToast = WidgetInstance.Create(AchievementManager.ACHIEVEMENT_TOAST_PREFAB, false);
				this.m_achievementToast.RegisterReadyListener(delegate(object _)
				{
					AchievementToast componentInChildren = this.m_achievementToast.GetComponentInChildren<AchievementToast>();
					componentInChildren.Initialize(this.GetAchievementDataModel(achievementId));
					componentInChildren.Show();
				}, null, true);
				this.m_achievementToast.RegisterDeactivatedListener(delegate(object _)
				{
					this.m_achievementToast = null;
					if (!this.CheckToastQueue())
					{
						this.m_previousToastName = string.Empty;
						SocialToastMgr.Get().CheckToastQueue();
					}
				}, null);
				return;
			}
			if (@string.Equals(this.m_previousToastName))
			{
				return;
			}
			this.m_completedAchievements.Enqueue(achievementId);
		}

		// Token: 0x0600BECC RID: 48844 RVA: 0x003A24CA File Offset: 0x003A06CA
		public bool ShowNextReward(Action callback)
		{
			return this.m_rewardPresenter.ShowNextReward(callback);
		}

		// Token: 0x0600BECD RID: 48845 RVA: 0x003A24D8 File Offset: 0x003A06D8
		public void SelectSubcategory(AchievementCategoryDataModel category, AchievementSubcategoryDataModel subcategory)
		{
			category.SelectSubcategory(subcategory, new Func<int, PlayerAchievementState>(this.m_playerState.GetState));
		}

		// Token: 0x0600BECE RID: 48846 RVA: 0x003A24F2 File Offset: 0x003A06F2
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

		// Token: 0x0600BECF RID: 48847 RVA: 0x003A2534 File Offset: 0x003A0734
		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				this.IsSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
				this.m_endOfTurnToastPauseBufferSecs = initialClientState.GuardianVars.EndOfTurnToastPauseBufferSecs;
			}
		}

		// Token: 0x0600BED0 RID: 48848 RVA: 0x003A257C File Offset: 0x003A077C
		private void ReceiveAchievementStateUpdateMessage()
		{
			PlayerAchievementStateUpdate playerAchievementStateUpdate = Network.Get().GetPlayerAchievementStateUpdate();
			if (playerAchievementStateUpdate == null)
			{
				return;
			}
			foreach (PlayerAchievementState playerAchievementState in playerAchievementStateUpdate.Achievement)
			{
				this.m_playerState.UpdateState(playerAchievementState.AchievementId, playerAchievementState);
			}
			this.UpdateStats();
		}

		// Token: 0x0600BED1 RID: 48849 RVA: 0x003A25F0 File Offset: 0x003A07F0
		private void OnGameSetup()
		{
			this.m_achievementInGameProgress.Clear();
			foreach (object obj in this.m_playerState)
			{
				PlayerAchievementState playerAchievementState = (PlayerAchievementState)obj;
				this.m_achievementInGameProgress[playerAchievementState.AchievementId] = playerAchievementState.Progress;
			}
		}

		// Token: 0x0600BED2 RID: 48850 RVA: 0x003A2664 File Offset: 0x003A0864
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
			int num;
			this.m_achievementInGameProgress.TryGetValue(achievementId, out num);
			if (num >= record.Quota)
			{
				return;
			}
			int num2 = num;
			ProgOpType opType = achievementInGameProgress.OpType;
			if (opType != ProgOpType.PROG_OP_ADD)
			{
				if (opType == ProgOpType.PROG_OP_SET)
				{
					num = achievementInGameProgress.Amount;
				}
			}
			else
			{
				num += achievementInGameProgress.Amount;
			}
			if (num == num2)
			{
				return;
			}
			this.m_achievementInGameProgress[achievementId] = num;
			if (num2 < record.Quota && num >= record.Quota)
			{
				this.ShowAchievementComplete(achievementId);
			}
		}

		// Token: 0x0600BED3 RID: 48851 RVA: 0x003A272C File Offset: 0x003A092C
		private void OnAchievementComplete()
		{
			AchievementComplete achievementComplete = Network.Get().GetAchievementComplete();
			if (achievementComplete == null)
			{
				return;
			}
			foreach (int achievementId in achievementComplete.AchievementIds)
			{
				this.ShowAchievementComplete(achievementId);
			}
		}

		// Token: 0x0600BED4 RID: 48852 RVA: 0x003A2790 File Offset: 0x003A0990
		private void OnStateChanged(PlayerAchievementState oldState, PlayerAchievementState newState)
		{
			if (!this.IsSystemEnabled)
			{
				return;
			}
			if (newState == null)
			{
				return;
			}
			if (oldState.Status != newState.Status)
			{
				this.UpdateStatus(newState.AchievementId, (AchievementManager.AchievementStatus)oldState.Status, (AchievementManager.AchievementStatus)newState.Status, newState.RewardItemOutput);
				this.OnStatusChanged(newState.AchievementId, (AchievementManager.AchievementStatus)newState.Status);
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

		// Token: 0x0600BED5 RID: 48853 RVA: 0x003A2838 File Offset: 0x003A0A38
		private void UpdateStatus(int achievementId, AchievementManager.AchievementStatus oldStatus, AchievementManager.AchievementStatus newStatus, List<RewardItemOutput> rewardItemOutput = null)
		{
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achievementId);
			if (record == null)
			{
				return;
			}
			switch (newStatus)
			{
			case AchievementManager.AchievementStatus.ACTIVE:
			case AchievementManager.AchievementStatus.COMPLETED:
			case AchievementManager.AchievementStatus.REWARD_ACKED:
				break;
			case AchievementManager.AchievementStatus.REWARD_GRANTED:
				if (record.AchievementVisibility != Assets.Achievement.AchievementVisibility.HIDDEN)
				{
					int chooseOneRewardItemId = 0;
					if (this.m_claimingAchievements.ContainsKey(achievementId))
					{
						chooseOneRewardItemId = this.m_claimingAchievements[achievementId];
						this.m_claimingAchievements.Remove(achievementId);
					}
					RewardListDbfRecord record2 = GameDbf.RewardList.GetRecord(record.RewardList);
					int? num;
					if (record2 == null)
					{
						num = null;
					}
					else
					{
						List<RewardItemDbfRecord> rewardItems = record2.RewardItems;
						num = ((rewardItems != null) ? new int?(rewardItems.Count) : null);
					}
					if ((num ?? 0) > 0)
					{
						this.m_rewardPresenter.EnqueueReward(AchievementFactory.CreateRewardScrollDataModel(achievementId, chooseOneRewardItemId, rewardItemOutput), delegate
						{
							this.AckAchievement(achievementId);
						});
					}
					else
					{
						this.AckAchievement(achievementId);
					}
				}
				break;
			default:
				Debug.LogWarning(string.Format("AchievementManager: unknown status {0} for achievement id {1}", newStatus, achievementId));
				break;
			}
			if (record.AchievementSectionRecord != null)
			{
				this.m_dirtySections.Add(record.AchievementSectionRecord);
			}
		}

		// Token: 0x0600BED6 RID: 48854 RVA: 0x003A2998 File Offset: 0x003A0B98
		private void UpdateStats()
		{
			if (this.m_dirtySections.Count == 0)
			{
				return;
			}
			foreach (AchievementSectionDbfRecord section in this.m_dirtySections)
			{
				this.m_stats.InvalidateSection(section);
			}
			Func<AchievementSectionItemDbfRecord, bool> <>9__11;
			List<AchievementSubcategoryDbfRecord> dirtySubcategoryRecords = (from record in GameDbf.AchievementSubcategory.GetRecords(delegate(AchievementSubcategoryDbfRecord record)
			{
				IEnumerable<AchievementSectionItemDbfRecord> sections = record.Sections;
				Func<AchievementSectionItemDbfRecord, bool> predicate;
				if ((predicate = <>9__11) == null)
				{
					predicate = (<>9__11 = ((AchievementSectionItemDbfRecord sectionItem) => this.m_dirtySections.Contains(sectionItem.AchievementSectionRecord)));
				}
				return sections.Any(predicate);
			}, -1)
			orderby record.ID
			select record).ToList<AchievementSubcategoryDbfRecord>();
			foreach (AchievementSubcategoryDbfRecord subcategory in dirtySubcategoryRecords)
			{
				this.m_stats.InvalidSubcategory(subcategory);
			}
			List<AchievementCategoryDbfRecord> dirtyCategoryRecords = (from record in GameDbf.AchievementCategory.GetRecords((AchievementCategoryDbfRecord record) => record.Subcategories.Intersect(dirtySubcategoryRecords).Any<AchievementSubcategoryDbfRecord>(), -1)
			orderby record.ID
			select record).ToList<AchievementCategoryDbfRecord>();
			foreach (AchievementCategoryDbfRecord category2 in dirtyCategoryRecords)
			{
				this.m_stats.InvalidateCategory(category2);
			}
			List<AchievementCategoryDataModel> list = (from dataModel in this.Categories.Categories
			where dirtyCategoryRecords.Any((AchievementCategoryDbfRecord record) => record.ID == dataModel.ID)
			orderby dataModel.ID
			select dataModel).ToList<AchievementCategoryDataModel>();
			List<AchievementSubcategoryDataModel> collection = (from dataModel in list.SelectMany((AchievementCategoryDataModel category) => category.Subcategories.Subcategories)
			where dirtySubcategoryRecords.Any((AchievementSubcategoryDbfRecord record) => record.ID == dataModel.ID)
			select dataModel into datamodel
			orderby datamodel.ID
			select datamodel).ToList<AchievementSubcategoryDataModel>();
			dirtySubcategoryRecords.Zip(collection, delegate(AchievementSubcategoryDbfRecord record, AchievementSubcategoryDataModel dataModel)
			{
				dataModel.Stats.Points = this.m_stats.GetSubcategoryPoints(record);
				dataModel.Stats.Unclaimed = this.m_stats.GetSubcategoryUnclaimed(record);
				dataModel.Stats.CompletedAchievements = this.m_stats.GetSubcategoryCompleted(record);
				dataModel.Stats.UpdateCompletionPercentage();
			});
			dirtyCategoryRecords.Zip(list, delegate(AchievementCategoryDbfRecord record, AchievementCategoryDataModel dataModel)
			{
				dataModel.Stats.Points = this.m_stats.GetCategoryPoints(record);
				dataModel.Stats.Unclaimed = this.m_stats.GetCategoryUnclaimed(record);
				dataModel.Stats.CompletedAchievements = this.m_stats.GetCategoryCompleted(record);
				dataModel.Stats.UpdateCompletionPercentage();
			});
			this.Categories.Stats.Points = this.m_stats.GetTotalPoints();
			this.Categories.Stats.Unclaimed = this.m_stats.GetTotalUnclaimed();
			this.Categories.Stats.CompletedAchievements = this.m_stats.GetTotalCompleted();
			this.Categories.Stats.UpdateCompletionPercentage();
			this.m_dirtySections.Clear();
		}

		// Token: 0x0600BED7 RID: 48855 RVA: 0x003A2C70 File Offset: 0x003A0E70
		private AchievementCategoryListDataModel InitializeCategories()
		{
			return AchievementFactory.CreateAchievementListDataModel(this.m_stats, new Func<int, PlayerAchievementState>(this.m_playerState.GetState));
		}

		// Token: 0x0600BED8 RID: 48856 RVA: 0x003A2C8E File Offset: 0x003A0E8E
		public AchievementDataModel Debug_GetAchievementDataModel(int achievementId)
		{
			return this.GetAchievementDataModel(achievementId);
		}

		// Token: 0x0600BED9 RID: 48857 RVA: 0x003A2C98 File Offset: 0x003A0E98
		public string Debug_GetAchievementHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsSystemEnabled)
			{
				stringBuilder.AppendLine("SYSTEM DISABLED");
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendFormat("Score: {0} / {1}", this.m_stats.GetTotalPoints(), CategoryList.CountAvailablePoints());
			stringBuilder.AppendLine();
			foreach (PlayerAchievementState achieveState in from a in this.m_playerState
			orderby a.Status, a.AchievementId
			select a)
			{
				stringBuilder.AppendLine(this.Debug_AchievementStateToString(achieveState));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600BEDA RID: 48858 RVA: 0x003A2D88 File Offset: 0x003A0F88
		private string Debug_AchievementStateToString(PlayerAchievementState achieveState)
		{
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(achieveState.AchievementId);
			if (record == null)
			{
				return string.Format("id={0} INVALID", achieveState.AchievementId);
			}
			string text = "";
			if (this.IsAchievementComplete(achieveState.AchievementId))
			{
				text = TimeUtils.UnixTimeStampToDateTimeLocal(achieveState.CompletedDate).ToString();
			}
			string format = "id={0} {1} [{2}/{3}] \"{4}\" {5}";
			object[] array = new object[6];
			array[0] = achieveState.AchievementId;
			array[1] = Enum.GetName(typeof(AchievementManager.AchievementStatus), achieveState.Status);
			array[2] = achieveState.Progress;
			array[3] = record.Quota;
			int num = 4;
			DbfLocValue name = record.Name;
			array[num] = (((name != null) ? name.GetString(true) : null) ?? "<no name>");
			array[5] = text;
			return string.Format(format, array);
		}

		// Token: 0x04009B11 RID: 39697
		public const long DefaultCompletedDate = 0L;

		// Token: 0x04009B12 RID: 39698
		public const int DefaultProgress = 0;

		// Token: 0x04009B13 RID: 39699
		public const AchievementManager.AchievementStatus DefaultStatus = AchievementManager.AchievementStatus.ACTIVE;

		// Token: 0x04009B14 RID: 39700
		public static readonly AssetReference ACHIEVEMENT_TOAST_PREFAB = new AssetReference("AchievementToast.prefab:9fa72c338c657d54fb9f63de21c1e4f8");

		// Token: 0x04009B19 RID: 39705
		private readonly PlayerState<PlayerAchievementState> m_playerState = new PlayerState<PlayerAchievementState>(new Func<int, PlayerAchievementState>(AchievementManager.CreateDefaultPlayerState));

		// Token: 0x04009B1A RID: 39706
		private readonly Map<int, int> m_claimingAchievements = new Map<int, int>();

		// Token: 0x04009B1B RID: 39707
		private readonly Map<int, int> m_achievementInGameProgress = new Map<int, int>();

		// Token: 0x04009B1C RID: 39708
		private Lazy<AchievementCategoryListDataModel> m_categories;

		// Token: 0x04009B1D RID: 39709
		private Queue<int> m_completedAchievements;

		// Token: 0x04009B1E RID: 39710
		private readonly RewardPresenter m_rewardPresenter = new RewardPresenter();

		// Token: 0x04009B1F RID: 39711
		private Widget m_achievementToast;

		// Token: 0x04009B20 RID: 39712
		private string m_previousToastName;

		// Token: 0x04009B21 RID: 39713
		private int m_toastNotificationSuppressionCount;

		// Token: 0x04009B22 RID: 39714
		private float m_endOfTurnToastPauseBufferSecs = 1.5f;

		// Token: 0x04009B23 RID: 39715
		private readonly AchievementStats m_stats;

		// Token: 0x04009B24 RID: 39716
		private readonly HashSet<AchievementSectionDbfRecord> m_dirtySections = new HashSet<AchievementSectionDbfRecord>();

		// Token: 0x020028C1 RID: 10433
		public enum AchievementStatus
		{
			// Token: 0x0400FACE RID: 64206
			UNKNOWN,
			// Token: 0x0400FACF RID: 64207
			ACTIVE,
			// Token: 0x0400FAD0 RID: 64208
			COMPLETED,
			// Token: 0x0400FAD1 RID: 64209
			REWARD_GRANTED,
			// Token: 0x0400FAD2 RID: 64210
			REWARD_ACKED,
			// Token: 0x0400FAD3 RID: 64211
			RESET
		}

		// Token: 0x020028C2 RID: 10434
		// (Invoke) Token: 0x06013CC3 RID: 81091
		public delegate void StatusChangedDelegate(int achievementId, AchievementManager.AchievementStatus status);

		// Token: 0x020028C3 RID: 10435
		// (Invoke) Token: 0x06013CC7 RID: 81095
		public delegate void ProgressChangedDelegate(int achievementId, int progress);

		// Token: 0x020028C4 RID: 10436
		// (Invoke) Token: 0x06013CCB RID: 81099
		public delegate void CompletedDateChangedDelegate(int achievementId, long completedDate);
	}
}
