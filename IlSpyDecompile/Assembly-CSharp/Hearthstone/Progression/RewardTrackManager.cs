using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class RewardTrackManager : IService
	{
		public enum RewardStatus
		{
			UNKNOWN,
			GRANTED,
			ACKED,
			RESET
		}

		private const RewardStatus DefaultStatus = RewardStatus.UNKNOWN;

		private readonly PlayerState<PlayerRewardTrackLevelState> m_rewardTrackLevelState = new PlayerState<PlayerRewardTrackLevelState>(CreateDefaultLevelState);

		private readonly RewardPresenter m_rewardPresenter = new RewardPresenter();

		private readonly Map<(int, int, bool), int> m_pendingRewardClaimRequests = new Map<(int, int, bool), int>();

		private int m_rewardTrackPageStartLevel;

		private int m_rewardTrackPageEndLevel;

		private bool m_isShowingSeasonRoll;

		private RewardTrackDataModel m_prevSeasonDataModel;

		private RewardTrackNodeRewardsDataModel m_prevSeasonChooseOneReward;

		private Queue<RewardTrackUnclaimedRewards> m_rewardTrackUnclaimedNotifications = new Queue<RewardTrackUnclaimedRewards>();

		public bool IsSystemEnabled { get; private set; }

		public RewardTrackDataModel TrackDataModel { get; private set; }

		public RewardTrackNodeListDataModel NodesDataModel { get; private set; }

		public PageInfoDataModel PageDataModel { get; private set; }

		public RewardTrackDbfRecord RewardTrackAsset => GameDbf.RewardTrack.GetRecord(TrackDataModel?.RewardTrackId ?? 0);

		public RewardTrackLevelDbfRecord RewardTrackLevelAsset => RewardTrackAsset?.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == TrackDataModel?.Level);

		public bool HasPremiumRewardsUnlocked => AccountLicenseMgr.Get().OwnsAccountLicense(RewardTrackAsset?.AccountLicenseRecord?.LicenseId ?? 0);

		public int CurrentPageNumber => PageDataModel?.PageNumber ?? 1;

		public int TotalPages => PageDataModel?.TotalPages ?? 1;

		public int ItemsPerPage => PageDataModel?.ItemsPerPage ?? 0;

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			TrackDataModel = new RewardTrackDataModel();
			NodesDataModel = new RewardTrackNodeListDataModel();
			PageDataModel = new PageInfoDataModel();
			HearthstoneApplication.Get().WillReset += WillReset;
			Network network = serviceLocator.Get<Network>();
			network.RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
			network.RegisterNetHandler(PlayerRewardTrackStateUpdate.PacketID.ID, ReceiveRewardTrackStateUpdateMessage);
			network.RegisterNetHandler(RewardTrackUnclaimedNotification.PacketID.ID, OnRewardTrackUnclaimedNotification);
			AccountLicenseMgr.Get().RegisterAccountLicensesChangedListener(OnAccountLicensesChanged);
			serviceLocator.Get<NetCache>().RegisterUpdatedListener(typeof(NetCache.NetCacheAccountLicenses), OnAccountLicensesUpdated);
			m_rewardTrackLevelState.OnStateChanged += OnStateChanged;
			yield break;
		}

		public Type[] GetDependencies()
		{
			return new Type[3]
			{
				typeof(Network),
				typeof(NetCache),
				typeof(AccountLicenseMgr)
			};
		}

		public void Shutdown()
		{
		}

		private void WillReset()
		{
			IsSystemEnabled = false;
			TrackDataModel = new RewardTrackDataModel();
			NodesDataModel = new RewardTrackNodeListDataModel();
			PageDataModel = new PageInfoDataModel();
			m_rewardTrackLevelState.Reset();
			m_rewardTrackUnclaimedNotifications.Clear();
		}

		public static RewardTrackManager Get()
		{
			return HearthstoneServices.Get<RewardTrackManager>();
		}

		public void SetRewardTrackNodePage(int pageNum, int itemsPerPage)
		{
			if (RewardTrackAsset?.Levels == null)
			{
				Debug.LogError("SetRewardTrackNodePage: RewardTrackAsset is missing or incomplete!");
				return;
			}
			int levelCapSoft = RewardTrackAsset.LevelCapSoft;
			int count = RewardTrackAsset.Levels.Count;
			int num = Mathf.Min(levelCapSoft + 1, count);
			int num2 = ((count <= 0) ? 1 : Mathf.Max(1, Mathf.CeilToInt((float)num / (float)itemsPerPage)));
			pageNum = Mathf.Clamp(pageNum, 1, num2);
			m_rewardTrackPageStartLevel = itemsPerPage * (pageNum - 1) + 1;
			m_rewardTrackPageStartLevel = Mathf.Clamp(m_rewardTrackPageStartLevel, 1, count);
			m_rewardTrackPageEndLevel = m_rewardTrackPageStartLevel + itemsPerPage - 1;
			m_rewardTrackPageEndLevel = Mathf.Clamp(m_rewardTrackPageEndLevel, 1, count);
			TrackDataModel.LevelSoftCap = num;
			TrackDataModel.LevelHardCap = count;
			PageDataModel.PageNumber = pageNum;
			PageDataModel.TotalPages = num2;
			PageDataModel.ItemsPerPage = itemsPerPage;
			PageDataModel.InfoText = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_PAGE_NUMBER", pageNum, num2);
			CreateRewardTrackNodes();
			ApplyRewardTrackStateToNodes();
		}

		public bool ClaimRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack, int chooseOneRewardItemId = 0)
		{
			if (rewardTrackId == TrackDataModel.RewardTrackId)
			{
				if (!RewardExistsAtLevel(level, forPaidTrack))
				{
					return false;
				}
				if (TrackDataModel.Level < level)
				{
					return false;
				}
				if (ProgressUtils.HasClaimedRewardTrackReward(GetRewardStatus(level, forPaidTrack)))
				{
					return false;
				}
				if (forPaidTrack && !HasPremiumRewardsUnlocked)
				{
					return false;
				}
			}
			if (m_pendingRewardClaimRequests.ContainsKey((rewardTrackId, level, forPaidTrack)))
			{
				return false;
			}
			m_pendingRewardClaimRequests[(rewardTrackId, level, forPaidTrack)] = chooseOneRewardItemId;
			Network.Get().ClaimRewardTrackReward(rewardTrackId, level, forPaidTrack, chooseOneRewardItemId);
			return true;
		}

		public bool AckRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack)
		{
			if (TrackDataModel.RewardTrackId == rewardTrackId)
			{
				if (!RewardExistsAtLevel(level, forPaidTrack))
				{
					return false;
				}
				if (GetRewardStatus(level, forPaidTrack) != RewardStatus.GRANTED)
				{
					return false;
				}
				PlayerRewardTrackLevelState state = m_rewardTrackLevelState.GetState(level);
				if (state == null)
				{
					return false;
				}
				if (forPaidTrack)
				{
					state.PaidRewardStatus = 2;
				}
				else
				{
					state.FreeRewardStatus = 2;
				}
			}
			Network.Get().AckRewardTrackReward(rewardTrackId, level, forPaidTrack);
			return true;
		}

		public bool HasUnclaimedRewardsForLevel(RewardTrackLevelDbfRecord record)
		{
			PlayerRewardTrackLevelState state = m_rewardTrackLevelState.GetState(record.Level);
			bool premiumRewardsUnlocked = TrackDataModel.PremiumRewardsUnlocked;
			RewardListDbfRecord freeRewardListRecord = record.FreeRewardListRecord;
			bool flag = freeRewardListRecord != null && freeRewardListRecord.RewardItems?.Count > 0;
			int num;
			if (premiumRewardsUnlocked)
			{
				RewardListDbfRecord paidRewardListRecord = record.PaidRewardListRecord;
				num = ((paidRewardListRecord != null && paidRewardListRecord.RewardItems?.Count > 0) ? 1 : 0);
			}
			else
			{
				num = 0;
			}
			bool flag2 = (byte)num != 0;
			if (!flag || ProgressUtils.HasClaimedRewardTrackReward((RewardStatus)state.FreeRewardStatus))
			{
				if (flag2)
				{
					return !ProgressUtils.HasClaimedRewardTrackReward((RewardStatus)state.PaidRewardStatus);
				}
				return false;
			}
			return true;
		}

		public bool ShowNextReward(Action callback)
		{
			return m_rewardPresenter.ShowNextReward(callback);
		}

		public RewardTrackLevelDbfRecord GetRewardTrackLevelRecord(int level)
		{
			return RewardTrackAsset?.Levels?.Find((RewardTrackLevelDbfRecord cur) => cur.Level == level);
		}

		public bool ShowUnclaimedTrackRewardsPopup(Action callback)
		{
			if (m_isShowingSeasonRoll || m_rewardTrackUnclaimedNotifications.Count == 0)
			{
				return false;
			}
			RewardTrackUnclaimedRewards rewardTrackUnclaimedNotif = m_rewardTrackUnclaimedNotifications.Dequeue();
			Widget widget = WidgetInstance.Create(RewardTrackSeasonRoll.REWARD_TRACK_SEASON_ROLL_PREFAB);
			m_isShowingSeasonRoll = true;
			widget.RegisterReadyListener(delegate
			{
				RewardTrackSeasonRoll componentInChildren = widget.GetComponentInChildren<RewardTrackSeasonRoll>();
				componentInChildren.Initialize(callback, rewardTrackUnclaimedNotif);
				componentInChildren.Show();
			});
			return true;
		}

		public bool SetRewardTrackSeasonLastSeen(int seasonLastSeen)
		{
			if (GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PROGRESSION, GameSaveKeySubkeyId.PROGRESSION_REWARD_TRACK_SEASON_LAST_SEEN, seasonLastSeen)))
			{
				TrackDataModel.SeasonLastSeen = seasonLastSeen;
				return true;
			}
			return false;
		}

		public int GetLastRewardTrackSeasonSeen()
		{
			long value = 0L;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PROGRESSION, GameSaveKeySubkeyId.PROGRESSION_REWARD_TRACK_SEASON_LAST_SEEN, out value);
			return (int)value;
		}

		public int ApplyXpBonusPercent(int xp)
		{
			float num = 1f + (float)TrackDataModel.XpBonusPercent / 100f;
			return (int)Math.Round((float)xp * num, MidpointRounding.AwayFromZero);
		}

		private static PlayerRewardTrackLevelState CreateDefaultLevelState(int id)
		{
			return new PlayerRewardTrackLevelState
			{
				Level = id,
				FreeRewardStatus = 0,
				PaidRewardStatus = 0
			};
		}

		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				IsSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
			}
			int lastRewardTrackSeasonSeen = GetLastRewardTrackSeasonSeen();
			if (lastRewardTrackSeasonSeen <= 0)
			{
				TrackDataModel.SeasonLastSeen = 1;
			}
			else
			{
				TrackDataModel.SeasonLastSeen = lastRewardTrackSeasonSeen;
			}
		}

		private void OnAccountLicensesChanged(List<AccountLicenseInfo> changedLicensesInfo, object userData)
		{
			OnAccountLicensesUpdated();
		}

		private void OnAccountLicensesUpdated()
		{
			UpdatePremiumRewardsUnlocked();
			UpdateXpBonusPercent();
			UpdateUnclaimedRewards();
		}

		private void UpdatePremiumRewardsUnlocked()
		{
			TrackDataModel.PremiumRewardsUnlocked = HasPremiumRewardsUnlocked;
		}

		private void UpdateXpBonusPercent()
		{
			float num = CalcRewardTrackXpMult();
			TrackDataModel.XpBonusPercent = (int)Math.Round((num - 1f) * 100f);
			TrackDataModel.XpBoostText = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_XP_TOOLTIP_BOOST_BODY", TrackDataModel.XpBonusPercent);
		}

		private void CreateRewardTrackNodes()
		{
			NodesDataModel.Nodes = RewardTrackFactory.CreateRewardTrackNodeDataModelList(RewardTrackAsset, TrackDataModel, m_rewardTrackLevelState.GetState, m_rewardTrackPageStartLevel, m_rewardTrackPageEndLevel);
		}

		private void SetChooseOneRewardItemAsOwned(int level, bool forPaidTrack, int chooseOneRewardItemId)
		{
			if (chooseOneRewardItemId > 0)
			{
				RewardItemDataModel rewardItemDataModel = (from item in NodesDataModel.Nodes.Where((RewardTrackNodeDataModel node) => node.Level == level).SelectMany((RewardTrackNodeDataModel node) => (!forPaidTrack) ? node.FreeRewards?.Items?.Items : node.PremiumRewards?.Items?.Items)
					where item.AssetId == chooseOneRewardItemId
					select item).FirstOrDefault();
				if (rewardItemDataModel?.Card != null)
				{
					rewardItemDataModel.Card.Owned = true;
				}
			}
		}

		private void ApplyRewardTrackStateToNodes()
		{
			foreach (RewardTrackNodeDataModel node in NodesDataModel.Nodes)
			{
				PlayerRewardTrackLevelState state = m_rewardTrackLevelState.GetState(node.Level);
				node.FreeRewards.IsClaimed = ProgressUtils.HasClaimedRewardTrackReward((RewardStatus)state.FreeRewardStatus);
				node.PremiumRewards.IsClaimed = ProgressUtils.HasClaimedRewardTrackReward((RewardStatus)state.PaidRewardStatus);
			}
		}

		private void ReceiveRewardTrackStateUpdateMessage()
		{
			PlayerRewardTrackStateUpdate playerRewardTrackStateUpdate = Network.Get().GetPlayerRewardTrackStateUpdate();
			if (playerRewardTrackStateUpdate == null)
			{
				return;
			}
			foreach (PlayerRewardTrackState item in playerRewardTrackStateUpdate.State)
			{
				HandleRewardTrackStateUpdate(item);
			}
		}

		private void HandleRewardTrackStateUpdate(PlayerRewardTrackState stateUpdate)
		{
			if (stateUpdate.HasIsActiveRewardTrack && !stateUpdate.IsActiveRewardTrack)
			{
				foreach (PlayerRewardTrackLevelState item in stateUpdate.TrackLevel)
				{
					UpdateStatus(stateUpdate.RewardTrackId, item.Level, (RewardStatus)item.FreeRewardStatus, forPaidTrack: false, item.RewardItemOutput);
					UpdateStatus(stateUpdate.RewardTrackId, item.Level, (RewardStatus)item.PaidRewardStatus, forPaidTrack: true, item.RewardItemOutput);
				}
				return;
			}
			bool flag = false;
			if (TrackDataModel.RewardTrackId != stateUpdate.RewardTrackId)
			{
				flag = true;
				TrackDataModel.RewardTrackId = stateUpdate.RewardTrackId;
				TrackDataModel.Season = RewardTrackAsset.Season;
				m_rewardTrackLevelState.Reset();
				UpdatePremiumRewardsUnlocked();
				CreateRewardTrackNodes();
			}
			if (stateUpdate.HasLevel)
			{
				if (TrackDataModel.Level != stateUpdate.Level)
				{
					TrackDataModel.Level = stateUpdate.Level;
					UpdateXpBonusPercent();
					UpdateUnclaimedRewards();
				}
				TrackDataModel.XpNeeded = RewardTrackLevelAsset?.XpNeeded ?? 0;
			}
			if (stateUpdate.HasXp)
			{
				TrackDataModel.Xp = stateUpdate.Xp;
			}
			if (stateUpdate.HasLevel || stateUpdate.HasXp)
			{
				TrackDataModel.XpProgress = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_XP_PROGRESS", TrackDataModel.Xp, TrackDataModel.XpNeeded);
			}
			if (stateUpdate.TrackLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState item2 in stateUpdate.TrackLevel)
				{
					m_rewardTrackLevelState.UpdateState(item2.Level, item2);
				}
				ApplyRewardTrackStateToNodes();
			}
			if (stateUpdate.TrackLevel.Count > 0 || flag)
			{
				UpdateUnclaimedRewards();
			}
		}

		private void UpdateUnclaimedRewards()
		{
			if (RewardTrackAsset?.Levels == null)
			{
				Debug.LogError("UpdateUnclaimedRewards: RewardTrackAsset is missing or incomplete!");
			}
			else
			{
				TrackDataModel.Unclaimed = ProgressUtils.CountUnclaimedRewards(RewardTrackAsset, TrackDataModel.Level, HasPremiumRewardsUnlocked, m_rewardTrackLevelState.GetState);
			}
		}

		private void OnStateChanged(PlayerRewardTrackLevelState oldState, PlayerRewardTrackLevelState newState)
		{
			if (IsSystemEnabled && newState != null)
			{
				if (oldState == null || oldState.FreeRewardStatus != newState.FreeRewardStatus)
				{
					UpdateStatus(TrackDataModel.RewardTrackId, newState.Level, (RewardStatus)newState.FreeRewardStatus, forPaidTrack: false, newState.RewardItemOutput);
				}
				if (oldState == null || oldState.PaidRewardStatus != newState.PaidRewardStatus)
				{
					UpdateStatus(TrackDataModel.RewardTrackId, newState.Level, (RewardStatus)newState.PaidRewardStatus, forPaidTrack: true, newState.RewardItemOutput);
				}
			}
		}

		private void UpdateStatus(int rewardTrackId, int level, RewardStatus status, bool forPaidTrack, List<RewardItemOutput> rewardItemOutput)
		{
			RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = GameDbf.RewardTrack.GetRecord(rewardTrackId)?.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == level);
			if (rewardTrackLevelDbfRecord == null)
			{
				return;
			}
			switch (status)
			{
			case RewardStatus.GRANTED:
			{
				int rewardListId = (forPaidTrack ? rewardTrackLevelDbfRecord.PaidRewardList : rewardTrackLevelDbfRecord.FreeRewardList);
				if (m_pendingRewardClaimRequests.TryGetValue((rewardTrackLevelDbfRecord.RewardTrackId, level, forPaidTrack), out var value))
				{
					m_pendingRewardClaimRequests.Remove((rewardTrackLevelDbfRecord.RewardTrackId, level, forPaidTrack));
				}
				m_rewardPresenter.EnqueueReward(RewardTrackFactory.CreateRewardScrollDataModel(rewardListId, level, value, rewardItemOutput), delegate
				{
					AckRewardTrackReward(rewardTrackId, level, forPaidTrack);
				});
				if (TrackDataModel.RewardTrackId == rewardTrackId)
				{
					SetChooseOneRewardItemAsOwned(level, forPaidTrack, value);
				}
				break;
			}
			default:
				Debug.LogWarning($"RewardTrackManager: unknown status {status} for level {level} for reward track id {rewardTrackId}");
				break;
			case RewardStatus.UNKNOWN:
			case RewardStatus.ACKED:
			case RewardStatus.RESET:
				break;
			}
		}

		private void OnRewardTrackUnclaimedNotification()
		{
			RewardTrackUnclaimedNotification rewardTrackUnclaimedNotification = Network.Get().GetRewardTrackUnclaimedNotification();
			if (rewardTrackUnclaimedNotification == null)
			{
				return;
			}
			foreach (RewardTrackUnclaimedRewards item in rewardTrackUnclaimedNotification.Notif)
			{
				m_rewardTrackUnclaimedNotifications.Enqueue(item);
			}
		}

		private RewardStatus GetRewardStatus(int level, bool forPaidTrack)
		{
			PlayerRewardTrackLevelState state = m_rewardTrackLevelState.GetState(level);
			if (!forPaidTrack)
			{
				return (RewardStatus)state.FreeRewardStatus;
			}
			return (RewardStatus)state.PaidRewardStatus;
		}

		private bool RewardExistsAtLevel(int level, bool forPaidTrack)
		{
			RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = RewardTrackAsset?.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == level);
			if (rewardTrackLevelDbfRecord == null)
			{
				return false;
			}
			if (!forPaidTrack)
			{
				return rewardTrackLevelDbfRecord.FreeRewardList != 0;
			}
			return rewardTrackLevelDbfRecord.PaidRewardList != 0;
		}

		private float CalcRewardTrackXpMult()
		{
			float num = 1f;
			if (HasPremiumRewardsUnlocked)
			{
				List<RewardTrackLevelDbfRecord> list = RewardTrackAsset?.Levels;
				if (list != null)
				{
					foreach (RewardTrackLevelDbfRecord item in list)
					{
						if (item.Level <= TrackDataModel.Level && item.PaidRewardListRecord != null && item.PaidRewardListRecord.RewardItems != null)
						{
							foreach (RewardItemDbfRecord rewardItem in item.PaidRewardListRecord.RewardItems)
							{
								if (rewardItem.RewardType == RewardItem.RewardType.REWARD_TRACK_XP_BOOST)
								{
									float num2 = 1f + (float)rewardItem.Quantity / 100f;
									if (num2 > num)
									{
										num = num2;
									}
								}
							}
						}
					}
					return num;
				}
			}
			return num;
		}

		public string GetRewardTrackDebugHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!IsSystemEnabled)
			{
				stringBuilder.AppendLine("SYSTEM DISABLED");
				stringBuilder.AppendLine();
			}
			string arg = RewardTrackAsset?.Event ?? "unknown";
			int num = RewardTrackAsset?.Season ?? 0;
			int num2 = RewardTrackLevelAsset?.XpNeeded ?? 0;
			stringBuilder.AppendLine($"ID={TrackDataModel.RewardTrackId} SEASON={num} EVENT={arg}");
			stringBuilder.AppendLine($"LEVEL: {TrackDataModel.Level}");
			stringBuilder.AppendLine($"XP: {TrackDataModel.Xp} / {num2}  (BOOST: {TrackDataModel.XpBonusPercent}%)");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- XP Gains ----------");
			stringBuilder.AppendLine(RewardXpNotificationManager.Get().GetRewardTrackDebugHudString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Level States ----------");
			stringBuilder.AppendLine($"Unclaimed: {TrackDataModel.Unclaimed}");
			foreach (PlayerRewardTrackLevelState item in m_rewardTrackLevelState.OrderBy((PlayerRewardTrackLevelState r) => r.Level))
			{
				stringBuilder.AppendFormat("{0} Free={1} Paid={2}", item.Level, GetRewardStatusString(item, forPaidTrack: false), GetRewardStatusString(item, forPaidTrack: true));
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		private string GetRewardStatusString(PlayerRewardTrackLevelState rewardTrackLevelState, bool forPaidTrack)
		{
			if (RewardExistsAtLevel(rewardTrackLevelState.Level, forPaidTrack))
			{
				return Enum.GetName(typeof(RewardStatus), forPaidTrack ? rewardTrackLevelState.PaidRewardStatus : rewardTrackLevelState.FreeRewardStatus);
			}
			return "n/a";
		}
	}
}
