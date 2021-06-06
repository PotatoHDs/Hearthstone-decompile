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
	// Token: 0x02001123 RID: 4387
	public class RewardTrackManager : IService
	{
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x0600C028 RID: 49192 RVA: 0x003A9004 File Offset: 0x003A7204
		// (set) Token: 0x0600C029 RID: 49193 RVA: 0x003A900C File Offset: 0x003A720C
		public bool IsSystemEnabled { get; private set; }

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600C02A RID: 49194 RVA: 0x003A9015 File Offset: 0x003A7215
		// (set) Token: 0x0600C02B RID: 49195 RVA: 0x003A901D File Offset: 0x003A721D
		public RewardTrackDataModel TrackDataModel { get; private set; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x0600C02C RID: 49196 RVA: 0x003A9026 File Offset: 0x003A7226
		// (set) Token: 0x0600C02D RID: 49197 RVA: 0x003A902E File Offset: 0x003A722E
		public RewardTrackNodeListDataModel NodesDataModel { get; private set; }

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600C02E RID: 49198 RVA: 0x003A9037 File Offset: 0x003A7237
		// (set) Token: 0x0600C02F RID: 49199 RVA: 0x003A903F File Offset: 0x003A723F
		public PageInfoDataModel PageDataModel { get; private set; }

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600C030 RID: 49200 RVA: 0x003A9048 File Offset: 0x003A7248
		public RewardTrackDbfRecord RewardTrackAsset
		{
			get
			{
				Dbf<RewardTrackDbfRecord> rewardTrack = GameDbf.RewardTrack;
				RewardTrackDataModel trackDataModel = this.TrackDataModel;
				return rewardTrack.GetRecord((trackDataModel != null) ? trackDataModel.RewardTrackId : 0);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600C031 RID: 49201 RVA: 0x003A9066 File Offset: 0x003A7266
		public RewardTrackLevelDbfRecord RewardTrackLevelAsset
		{
			get
			{
				RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
				if (rewardTrackAsset == null)
				{
					return null;
				}
				return rewardTrackAsset.Levels.Find(delegate(RewardTrackLevelDbfRecord r)
				{
					int level = r.Level;
					RewardTrackDataModel trackDataModel = this.TrackDataModel;
					int? num = (trackDataModel != null) ? new int?(trackDataModel.Level) : null;
					return level == num.GetValueOrDefault() & num != null;
				});
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600C032 RID: 49202 RVA: 0x003A908C File Offset: 0x003A728C
		public bool HasPremiumRewardsUnlocked
		{
			get
			{
				AccountLicenseMgr accountLicenseMgr = AccountLicenseMgr.Get();
				RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
				long? num;
				if (rewardTrackAsset == null)
				{
					num = null;
				}
				else
				{
					AccountLicenseDbfRecord accountLicenseRecord = rewardTrackAsset.AccountLicenseRecord;
					num = ((accountLicenseRecord != null) ? new long?(accountLicenseRecord.LicenseId) : null);
				}
				return accountLicenseMgr.OwnsAccountLicense(num ?? 0L);
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600C033 RID: 49203 RVA: 0x003A90EB File Offset: 0x003A72EB
		public int CurrentPageNumber
		{
			get
			{
				PageInfoDataModel pageDataModel = this.PageDataModel;
				if (pageDataModel == null)
				{
					return 1;
				}
				return pageDataModel.PageNumber;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600C034 RID: 49204 RVA: 0x003A90FE File Offset: 0x003A72FE
		public int TotalPages
		{
			get
			{
				PageInfoDataModel pageDataModel = this.PageDataModel;
				if (pageDataModel == null)
				{
					return 1;
				}
				return pageDataModel.TotalPages;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600C035 RID: 49205 RVA: 0x003A9111 File Offset: 0x003A7311
		public int ItemsPerPage
		{
			get
			{
				PageInfoDataModel pageDataModel = this.PageDataModel;
				if (pageDataModel == null)
				{
					return 0;
				}
				return pageDataModel.ItemsPerPage;
			}
		}

		// Token: 0x0600C036 RID: 49206 RVA: 0x003A9124 File Offset: 0x003A7324
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			this.TrackDataModel = new RewardTrackDataModel();
			this.NodesDataModel = new RewardTrackNodeListDataModel();
			this.PageDataModel = new PageInfoDataModel();
			HearthstoneApplication.Get().WillReset += this.WillReset;
			Network network = serviceLocator.Get<Network>();
			network.RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState), null);
			network.RegisterNetHandler(PlayerRewardTrackStateUpdate.PacketID.ID, new Network.NetHandler(this.ReceiveRewardTrackStateUpdateMessage), null);
			network.RegisterNetHandler(RewardTrackUnclaimedNotification.PacketID.ID, new Network.NetHandler(this.OnRewardTrackUnclaimedNotification), null);
			AccountLicenseMgr.Get().RegisterAccountLicensesChangedListener(new AccountLicenseMgr.AccountLicensesChangedCallback(this.OnAccountLicensesChanged));
			serviceLocator.Get<NetCache>().RegisterUpdatedListener(typeof(NetCache.NetCacheAccountLicenses), new Action(this.OnAccountLicensesUpdated));
			this.m_rewardTrackLevelState.OnStateChanged += this.OnStateChanged;
			yield break;
		}

		// Token: 0x0600C037 RID: 49207 RVA: 0x003A913A File Offset: 0x003A733A
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(Network),
				typeof(NetCache),
				typeof(AccountLicenseMgr)
			};
		}

		// Token: 0x0600C038 RID: 49208 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Shutdown()
		{
		}

		// Token: 0x0600C039 RID: 49209 RVA: 0x003A9169 File Offset: 0x003A7369
		private void WillReset()
		{
			this.IsSystemEnabled = false;
			this.TrackDataModel = new RewardTrackDataModel();
			this.NodesDataModel = new RewardTrackNodeListDataModel();
			this.PageDataModel = new PageInfoDataModel();
			this.m_rewardTrackLevelState.Reset();
			this.m_rewardTrackUnclaimedNotifications.Clear();
		}

		// Token: 0x0600C03A RID: 49210 RVA: 0x003A91A9 File Offset: 0x003A73A9
		public static RewardTrackManager Get()
		{
			return HearthstoneServices.Get<RewardTrackManager>();
		}

		// Token: 0x0600C03B RID: 49211 RVA: 0x003A91B0 File Offset: 0x003A73B0
		public void SetRewardTrackNodePage(int pageNum, int itemsPerPage)
		{
			RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
			if (((rewardTrackAsset != null) ? rewardTrackAsset.Levels : null) == null)
			{
				Debug.LogError("SetRewardTrackNodePage: RewardTrackAsset is missing or incomplete!");
				return;
			}
			int levelCapSoft = this.RewardTrackAsset.LevelCapSoft;
			int count = this.RewardTrackAsset.Levels.Count;
			int num = Mathf.Min(levelCapSoft + 1, count);
			int num2 = (count > 0) ? Mathf.Max(1, Mathf.CeilToInt((float)num / (float)itemsPerPage)) : 1;
			pageNum = Mathf.Clamp(pageNum, 1, num2);
			this.m_rewardTrackPageStartLevel = itemsPerPage * (pageNum - 1) + 1;
			this.m_rewardTrackPageStartLevel = Mathf.Clamp(this.m_rewardTrackPageStartLevel, 1, count);
			this.m_rewardTrackPageEndLevel = this.m_rewardTrackPageStartLevel + itemsPerPage - 1;
			this.m_rewardTrackPageEndLevel = Mathf.Clamp(this.m_rewardTrackPageEndLevel, 1, count);
			this.TrackDataModel.LevelSoftCap = num;
			this.TrackDataModel.LevelHardCap = count;
			this.PageDataModel.PageNumber = pageNum;
			this.PageDataModel.TotalPages = num2;
			this.PageDataModel.ItemsPerPage = itemsPerPage;
			this.PageDataModel.InfoText = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_PAGE_NUMBER", new object[]
			{
				pageNum,
				num2
			});
			this.CreateRewardTrackNodes();
			this.ApplyRewardTrackStateToNodes();
		}

		// Token: 0x0600C03C RID: 49212 RVA: 0x003A92DC File Offset: 0x003A74DC
		public bool ClaimRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack, int chooseOneRewardItemId = 0)
		{
			if (rewardTrackId == this.TrackDataModel.RewardTrackId)
			{
				if (!this.RewardExistsAtLevel(level, forPaidTrack))
				{
					return false;
				}
				if (this.TrackDataModel.Level < level)
				{
					return false;
				}
				if (ProgressUtils.HasClaimedRewardTrackReward(this.GetRewardStatus(level, forPaidTrack)))
				{
					return false;
				}
				if (forPaidTrack && !this.HasPremiumRewardsUnlocked)
				{
					return false;
				}
			}
			if (this.m_pendingRewardClaimRequests.ContainsKey(new ValueTuple<int, int, bool>(rewardTrackId, level, forPaidTrack)))
			{
				return false;
			}
			this.m_pendingRewardClaimRequests[new ValueTuple<int, int, bool>(rewardTrackId, level, forPaidTrack)] = chooseOneRewardItemId;
			Network.Get().ClaimRewardTrackReward(rewardTrackId, level, forPaidTrack, chooseOneRewardItemId);
			return true;
		}

		// Token: 0x0600C03D RID: 49213 RVA: 0x003A9370 File Offset: 0x003A7570
		public bool AckRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack)
		{
			if (this.TrackDataModel.RewardTrackId == rewardTrackId)
			{
				if (!this.RewardExistsAtLevel(level, forPaidTrack))
				{
					return false;
				}
				if (this.GetRewardStatus(level, forPaidTrack) != RewardTrackManager.RewardStatus.GRANTED)
				{
					return false;
				}
				PlayerRewardTrackLevelState state = this.m_rewardTrackLevelState.GetState(level);
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

		// Token: 0x0600C03E RID: 49214 RVA: 0x003A93D8 File Offset: 0x003A75D8
		public bool HasUnclaimedRewardsForLevel(RewardTrackLevelDbfRecord record)
		{
			PlayerRewardTrackLevelState state = this.m_rewardTrackLevelState.GetState(record.Level);
			bool premiumRewardsUnlocked = this.TrackDataModel.PremiumRewardsUnlocked;
			RewardListDbfRecord freeRewardListRecord = record.FreeRewardListRecord;
			bool flag;
			if (freeRewardListRecord == null)
			{
				flag = false;
			}
			else
			{
				List<RewardItemDbfRecord> rewardItems = freeRewardListRecord.RewardItems;
				int? num = (rewardItems != null) ? new int?(rewardItems.Count) : null;
				int num2 = 0;
				flag = (num.GetValueOrDefault() > num2 & num != null);
			}
			bool flag2 = flag;
			bool flag3;
			if (premiumRewardsUnlocked)
			{
				RewardListDbfRecord paidRewardListRecord = record.PaidRewardListRecord;
				if (paidRewardListRecord == null)
				{
					flag3 = false;
				}
				else
				{
					List<RewardItemDbfRecord> rewardItems2 = paidRewardListRecord.RewardItems;
					int? num = (rewardItems2 != null) ? new int?(rewardItems2.Count) : null;
					int num2 = 0;
					flag3 = (num.GetValueOrDefault() > num2 & num != null);
				}
			}
			else
			{
				flag3 = false;
			}
			bool flag4 = flag3;
			return (flag2 && !ProgressUtils.HasClaimedRewardTrackReward((RewardTrackManager.RewardStatus)state.FreeRewardStatus)) || (flag4 && !ProgressUtils.HasClaimedRewardTrackReward((RewardTrackManager.RewardStatus)state.PaidRewardStatus));
		}

		// Token: 0x0600C03F RID: 49215 RVA: 0x003A94B4 File Offset: 0x003A76B4
		public bool ShowNextReward(Action callback)
		{
			return this.m_rewardPresenter.ShowNextReward(callback);
		}

		// Token: 0x0600C040 RID: 49216 RVA: 0x003A94C4 File Offset: 0x003A76C4
		public RewardTrackLevelDbfRecord GetRewardTrackLevelRecord(int level)
		{
			RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
			if (rewardTrackAsset == null)
			{
				return null;
			}
			List<RewardTrackLevelDbfRecord> levels = rewardTrackAsset.Levels;
			if (levels == null)
			{
				return null;
			}
			return levels.Find((RewardTrackLevelDbfRecord cur) => cur.Level == level);
		}

		// Token: 0x0600C041 RID: 49217 RVA: 0x003A9508 File Offset: 0x003A7708
		public bool ShowUnclaimedTrackRewardsPopup(Action callback)
		{
			if (this.m_isShowingSeasonRoll || this.m_rewardTrackUnclaimedNotifications.Count == 0)
			{
				return false;
			}
			RewardTrackUnclaimedRewards rewardTrackUnclaimedNotif = this.m_rewardTrackUnclaimedNotifications.Dequeue();
			Widget widget = WidgetInstance.Create(RewardTrackSeasonRoll.REWARD_TRACK_SEASON_ROLL_PREFAB, false);
			this.m_isShowingSeasonRoll = true;
			widget.RegisterReadyListener(delegate(object _)
			{
				RewardTrackSeasonRoll componentInChildren = widget.GetComponentInChildren<RewardTrackSeasonRoll>();
				componentInChildren.Initialize(callback, rewardTrackUnclaimedNotif);
				componentInChildren.Show();
			}, null, true);
			return true;
		}

		// Token: 0x0600C042 RID: 49218 RVA: 0x003A9581 File Offset: 0x003A7781
		public bool SetRewardTrackSeasonLastSeen(int seasonLastSeen)
		{
			if (GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PROGRESSION, GameSaveKeySubkeyId.PROGRESSION_REWARD_TRACK_SEASON_LAST_SEEN, new long[]
			{
				(long)seasonLastSeen
			}), null))
			{
				this.TrackDataModel.SeasonLastSeen = seasonLastSeen;
				return true;
			}
			return false;
		}

		// Token: 0x0600C043 RID: 49219 RVA: 0x003A95BC File Offset: 0x003A77BC
		public int GetLastRewardTrackSeasonSeen()
		{
			long num = 0L;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PROGRESSION, GameSaveKeySubkeyId.PROGRESSION_REWARD_TRACK_SEASON_LAST_SEEN, out num);
			return (int)num;
		}

		// Token: 0x0600C044 RID: 49220 RVA: 0x003A95E8 File Offset: 0x003A77E8
		public int ApplyXpBonusPercent(int xp)
		{
			float num = 1f + (float)this.TrackDataModel.XpBonusPercent / 100f;
			return (int)Math.Round((double)((float)xp * num), MidpointRounding.AwayFromZero);
		}

		// Token: 0x0600C045 RID: 49221 RVA: 0x003A961A File Offset: 0x003A781A
		private static PlayerRewardTrackLevelState CreateDefaultLevelState(int id)
		{
			return new PlayerRewardTrackLevelState
			{
				Level = id,
				FreeRewardStatus = 0,
				PaidRewardStatus = 0
			};
		}

		// Token: 0x0600C046 RID: 49222 RVA: 0x003A9638 File Offset: 0x003A7838
		private void OnInitialClientState()
		{
			InitialClientState initialClientState = Network.Get().GetInitialClientState();
			if (initialClientState != null && initialClientState.HasGuardianVars)
			{
				this.IsSystemEnabled = initialClientState.GuardianVars.ProgressionEnabled;
			}
			int lastRewardTrackSeasonSeen = this.GetLastRewardTrackSeasonSeen();
			if (lastRewardTrackSeasonSeen <= 0)
			{
				this.TrackDataModel.SeasonLastSeen = 1;
				return;
			}
			this.TrackDataModel.SeasonLastSeen = lastRewardTrackSeasonSeen;
		}

		// Token: 0x0600C047 RID: 49223 RVA: 0x003A9690 File Offset: 0x003A7890
		private void OnAccountLicensesChanged(List<AccountLicenseInfo> changedLicensesInfo, object userData)
		{
			this.OnAccountLicensesUpdated();
		}

		// Token: 0x0600C048 RID: 49224 RVA: 0x003A9698 File Offset: 0x003A7898
		private void OnAccountLicensesUpdated()
		{
			this.UpdatePremiumRewardsUnlocked();
			this.UpdateXpBonusPercent();
			this.UpdateUnclaimedRewards();
		}

		// Token: 0x0600C049 RID: 49225 RVA: 0x003A96AC File Offset: 0x003A78AC
		private void UpdatePremiumRewardsUnlocked()
		{
			this.TrackDataModel.PremiumRewardsUnlocked = this.HasPremiumRewardsUnlocked;
		}

		// Token: 0x0600C04A RID: 49226 RVA: 0x003A96C0 File Offset: 0x003A78C0
		private void UpdateXpBonusPercent()
		{
			float num = this.CalcRewardTrackXpMult();
			this.TrackDataModel.XpBonusPercent = (int)Math.Round((double)((num - 1f) * 100f));
			this.TrackDataModel.XpBoostText = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_XP_TOOLTIP_BOOST_BODY", new object[]
			{
				this.TrackDataModel.XpBonusPercent
			});
		}

		// Token: 0x0600C04B RID: 49227 RVA: 0x003A9721 File Offset: 0x003A7921
		private void CreateRewardTrackNodes()
		{
			this.NodesDataModel.Nodes = RewardTrackFactory.CreateRewardTrackNodeDataModelList(this.RewardTrackAsset, this.TrackDataModel, new Func<int, PlayerRewardTrackLevelState>(this.m_rewardTrackLevelState.GetState), this.m_rewardTrackPageStartLevel, this.m_rewardTrackPageEndLevel);
		}

		// Token: 0x0600C04C RID: 49228 RVA: 0x003A975C File Offset: 0x003A795C
		private void SetChooseOneRewardItemAsOwned(int level, bool forPaidTrack, int chooseOneRewardItemId)
		{
			if (chooseOneRewardItemId <= 0)
			{
				return;
			}
			RewardItemDataModel rewardItemDataModel = (from item in (from node in this.NodesDataModel.Nodes
			where node.Level == level
			select node).SelectMany(delegate(RewardTrackNodeDataModel node)
			{
				if (!forPaidTrack)
				{
					RewardTrackNodeRewardsDataModel freeRewards = node.FreeRewards;
					if (freeRewards == null)
					{
						return null;
					}
					RewardListDataModel items = freeRewards.Items;
					if (items == null)
					{
						return null;
					}
					return items.Items;
				}
				else
				{
					RewardTrackNodeRewardsDataModel premiumRewards = node.PremiumRewards;
					if (premiumRewards == null)
					{
						return null;
					}
					RewardListDataModel items2 = premiumRewards.Items;
					if (items2 == null)
					{
						return null;
					}
					return items2.Items;
				}
			})
			where item.AssetId == chooseOneRewardItemId
			select item).FirstOrDefault<RewardItemDataModel>();
			if (((rewardItemDataModel != null) ? rewardItemDataModel.Card : null) != null)
			{
				rewardItemDataModel.Card.Owned = true;
			}
		}

		// Token: 0x0600C04D RID: 49229 RVA: 0x003A97EC File Offset: 0x003A79EC
		private void ApplyRewardTrackStateToNodes()
		{
			foreach (RewardTrackNodeDataModel rewardTrackNodeDataModel in this.NodesDataModel.Nodes)
			{
				PlayerRewardTrackLevelState state = this.m_rewardTrackLevelState.GetState(rewardTrackNodeDataModel.Level);
				rewardTrackNodeDataModel.FreeRewards.IsClaimed = ProgressUtils.HasClaimedRewardTrackReward((RewardTrackManager.RewardStatus)state.FreeRewardStatus);
				rewardTrackNodeDataModel.PremiumRewards.IsClaimed = ProgressUtils.HasClaimedRewardTrackReward((RewardTrackManager.RewardStatus)state.PaidRewardStatus);
			}
		}

		// Token: 0x0600C04E RID: 49230 RVA: 0x003A9878 File Offset: 0x003A7A78
		private void ReceiveRewardTrackStateUpdateMessage()
		{
			PlayerRewardTrackStateUpdate playerRewardTrackStateUpdate = Network.Get().GetPlayerRewardTrackStateUpdate();
			if (playerRewardTrackStateUpdate == null)
			{
				return;
			}
			foreach (PlayerRewardTrackState stateUpdate in playerRewardTrackStateUpdate.State)
			{
				this.HandleRewardTrackStateUpdate(stateUpdate);
			}
		}

		// Token: 0x0600C04F RID: 49231 RVA: 0x003A98DC File Offset: 0x003A7ADC
		private void HandleRewardTrackStateUpdate(PlayerRewardTrackState stateUpdate)
		{
			if (stateUpdate.HasIsActiveRewardTrack && !stateUpdate.IsActiveRewardTrack)
			{
				foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in stateUpdate.TrackLevel)
				{
					this.UpdateStatus(stateUpdate.RewardTrackId, playerRewardTrackLevelState.Level, (RewardTrackManager.RewardStatus)playerRewardTrackLevelState.FreeRewardStatus, false, playerRewardTrackLevelState.RewardItemOutput);
					this.UpdateStatus(stateUpdate.RewardTrackId, playerRewardTrackLevelState.Level, (RewardTrackManager.RewardStatus)playerRewardTrackLevelState.PaidRewardStatus, true, playerRewardTrackLevelState.RewardItemOutput);
				}
				return;
			}
			bool flag = false;
			if (this.TrackDataModel.RewardTrackId != stateUpdate.RewardTrackId)
			{
				flag = true;
				this.TrackDataModel.RewardTrackId = stateUpdate.RewardTrackId;
				this.TrackDataModel.Season = this.RewardTrackAsset.Season;
				this.m_rewardTrackLevelState.Reset();
				this.UpdatePremiumRewardsUnlocked();
				this.CreateRewardTrackNodes();
			}
			if (stateUpdate.HasLevel)
			{
				if (this.TrackDataModel.Level != stateUpdate.Level)
				{
					this.TrackDataModel.Level = stateUpdate.Level;
					this.UpdateXpBonusPercent();
					this.UpdateUnclaimedRewards();
				}
				RewardTrackDataModel trackDataModel = this.TrackDataModel;
				RewardTrackLevelDbfRecord rewardTrackLevelAsset = this.RewardTrackLevelAsset;
				trackDataModel.XpNeeded = ((rewardTrackLevelAsset != null) ? rewardTrackLevelAsset.XpNeeded : 0);
			}
			if (stateUpdate.HasXp)
			{
				this.TrackDataModel.Xp = stateUpdate.Xp;
			}
			if (stateUpdate.HasLevel || stateUpdate.HasXp)
			{
				this.TrackDataModel.XpProgress = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_XP_PROGRESS", new object[]
				{
					this.TrackDataModel.Xp,
					this.TrackDataModel.XpNeeded
				});
			}
			if (stateUpdate.TrackLevel.Count > 0)
			{
				foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState2 in stateUpdate.TrackLevel)
				{
					this.m_rewardTrackLevelState.UpdateState(playerRewardTrackLevelState2.Level, playerRewardTrackLevelState2);
				}
				this.ApplyRewardTrackStateToNodes();
			}
			if (stateUpdate.TrackLevel.Count > 0 || flag)
			{
				this.UpdateUnclaimedRewards();
			}
		}

		// Token: 0x0600C050 RID: 49232 RVA: 0x003A9B04 File Offset: 0x003A7D04
		private void UpdateUnclaimedRewards()
		{
			RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
			if (((rewardTrackAsset != null) ? rewardTrackAsset.Levels : null) == null)
			{
				Debug.LogError("UpdateUnclaimedRewards: RewardTrackAsset is missing or incomplete!");
				return;
			}
			this.TrackDataModel.Unclaimed = ProgressUtils.CountUnclaimedRewards(this.RewardTrackAsset, this.TrackDataModel.Level, this.HasPremiumRewardsUnlocked, new Func<int, PlayerRewardTrackLevelState>(this.m_rewardTrackLevelState.GetState));
		}

		// Token: 0x0600C051 RID: 49233 RVA: 0x003A9B68 File Offset: 0x003A7D68
		private void OnStateChanged(PlayerRewardTrackLevelState oldState, PlayerRewardTrackLevelState newState)
		{
			if (!this.IsSystemEnabled)
			{
				return;
			}
			if (newState == null)
			{
				return;
			}
			if (oldState == null || oldState.FreeRewardStatus != newState.FreeRewardStatus)
			{
				this.UpdateStatus(this.TrackDataModel.RewardTrackId, newState.Level, (RewardTrackManager.RewardStatus)newState.FreeRewardStatus, false, newState.RewardItemOutput);
			}
			if (oldState == null || oldState.PaidRewardStatus != newState.PaidRewardStatus)
			{
				this.UpdateStatus(this.TrackDataModel.RewardTrackId, newState.Level, (RewardTrackManager.RewardStatus)newState.PaidRewardStatus, true, newState.RewardItemOutput);
			}
		}

		// Token: 0x0600C052 RID: 49234 RVA: 0x003A9BEC File Offset: 0x003A7DEC
		private void UpdateStatus(int rewardTrackId, int level, RewardTrackManager.RewardStatus status, bool forPaidTrack, List<RewardItemOutput> rewardItemOutput)
		{
			RewardTrackDbfRecord record = GameDbf.RewardTrack.GetRecord(rewardTrackId);
			RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = (record != null) ? record.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == level) : null;
			if (rewardTrackLevelDbfRecord == null)
			{
				return;
			}
			switch (status)
			{
			case RewardTrackManager.RewardStatus.UNKNOWN:
			case RewardTrackManager.RewardStatus.ACKED:
			case RewardTrackManager.RewardStatus.RESET:
				break;
			case RewardTrackManager.RewardStatus.GRANTED:
			{
				int rewardListId = forPaidTrack ? rewardTrackLevelDbfRecord.PaidRewardList : rewardTrackLevelDbfRecord.FreeRewardList;
				int chooseOneRewardItemId;
				if (this.m_pendingRewardClaimRequests.TryGetValue(new ValueTuple<int, int, bool>(rewardTrackLevelDbfRecord.RewardTrackId, level, forPaidTrack), out chooseOneRewardItemId))
				{
					this.m_pendingRewardClaimRequests.Remove(new ValueTuple<int, int, bool>(rewardTrackLevelDbfRecord.RewardTrackId, level, forPaidTrack));
				}
				this.m_rewardPresenter.EnqueueReward(RewardTrackFactory.CreateRewardScrollDataModel(rewardListId, level, chooseOneRewardItemId, rewardItemOutput), delegate
				{
					this.AckRewardTrackReward(rewardTrackId, level, forPaidTrack);
				});
				if (this.TrackDataModel.RewardTrackId == rewardTrackId)
				{
					this.SetChooseOneRewardItemAsOwned(level, forPaidTrack, chooseOneRewardItemId);
					return;
				}
				break;
			}
			default:
				Debug.LogWarning(string.Format("RewardTrackManager: unknown status {0} for level {1} for reward track id {2}", status, level, rewardTrackId));
				break;
			}
		}

		// Token: 0x0600C053 RID: 49235 RVA: 0x003A9D44 File Offset: 0x003A7F44
		private void OnRewardTrackUnclaimedNotification()
		{
			RewardTrackUnclaimedNotification rewardTrackUnclaimedNotification = Network.Get().GetRewardTrackUnclaimedNotification();
			if (rewardTrackUnclaimedNotification == null)
			{
				return;
			}
			foreach (RewardTrackUnclaimedRewards item in rewardTrackUnclaimedNotification.Notif)
			{
				this.m_rewardTrackUnclaimedNotifications.Enqueue(item);
			}
		}

		// Token: 0x0600C054 RID: 49236 RVA: 0x003A9DAC File Offset: 0x003A7FAC
		private RewardTrackManager.RewardStatus GetRewardStatus(int level, bool forPaidTrack)
		{
			PlayerRewardTrackLevelState state = this.m_rewardTrackLevelState.GetState(level);
			if (!forPaidTrack)
			{
				return (RewardTrackManager.RewardStatus)state.FreeRewardStatus;
			}
			return (RewardTrackManager.RewardStatus)state.PaidRewardStatus;
		}

		// Token: 0x0600C055 RID: 49237 RVA: 0x003A9DD8 File Offset: 0x003A7FD8
		private bool RewardExistsAtLevel(int level, bool forPaidTrack)
		{
			RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
			RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = (rewardTrackAsset != null) ? rewardTrackAsset.Levels.Find((RewardTrackLevelDbfRecord r) => r.Level == level) : null;
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

		// Token: 0x0600C056 RID: 49238 RVA: 0x003A9E34 File Offset: 0x003A8034
		private float CalcRewardTrackXpMult()
		{
			float num = 1f;
			if (this.HasPremiumRewardsUnlocked)
			{
				RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
				List<RewardTrackLevelDbfRecord> list = (rewardTrackAsset != null) ? rewardTrackAsset.Levels : null;
				if (list != null)
				{
					foreach (RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord in list)
					{
						if (rewardTrackLevelDbfRecord.Level <= this.TrackDataModel.Level && rewardTrackLevelDbfRecord.PaidRewardListRecord != null && rewardTrackLevelDbfRecord.PaidRewardListRecord.RewardItems != null)
						{
							foreach (RewardItemDbfRecord rewardItemDbfRecord in rewardTrackLevelDbfRecord.PaidRewardListRecord.RewardItems)
							{
								if (rewardItemDbfRecord.RewardType == RewardItem.RewardType.REWARD_TRACK_XP_BOOST)
								{
									float num2 = 1f + (float)rewardItemDbfRecord.Quantity / 100f;
									if (num2 > num)
									{
										num = num2;
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600C057 RID: 49239 RVA: 0x003A9F40 File Offset: 0x003A8140
		public string GetRewardTrackDebugHudString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.IsSystemEnabled)
			{
				stringBuilder.AppendLine("SYSTEM DISABLED");
				stringBuilder.AppendLine();
			}
			RewardTrackDbfRecord rewardTrackAsset = this.RewardTrackAsset;
			string arg = ((rewardTrackAsset != null) ? rewardTrackAsset.Event : null) ?? "unknown";
			RewardTrackDbfRecord rewardTrackAsset2 = this.RewardTrackAsset;
			int num = (rewardTrackAsset2 != null) ? rewardTrackAsset2.Season : 0;
			RewardTrackLevelDbfRecord rewardTrackLevelAsset = this.RewardTrackLevelAsset;
			int num2 = (rewardTrackLevelAsset != null) ? rewardTrackLevelAsset.XpNeeded : 0;
			stringBuilder.AppendLine(string.Format("ID={0} SEASON={1} EVENT={2}", this.TrackDataModel.RewardTrackId, num, arg));
			stringBuilder.AppendLine(string.Format("LEVEL: {0}", this.TrackDataModel.Level));
			stringBuilder.AppendLine(string.Format("XP: {0} / {1}  (BOOST: {2}%)", this.TrackDataModel.Xp, num2, this.TrackDataModel.XpBonusPercent));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- XP Gains ----------");
			stringBuilder.AppendLine(RewardXpNotificationManager.Get().GetRewardTrackDebugHudString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---------- Level States ----------");
			stringBuilder.AppendLine(string.Format("Unclaimed: {0}", this.TrackDataModel.Unclaimed));
			foreach (PlayerRewardTrackLevelState playerRewardTrackLevelState in from r in this.m_rewardTrackLevelState
			orderby r.Level
			select r)
			{
				stringBuilder.AppendFormat("{0} Free={1} Paid={2}", playerRewardTrackLevelState.Level, this.GetRewardStatusString(playerRewardTrackLevelState, false), this.GetRewardStatusString(playerRewardTrackLevelState, true));
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600C058 RID: 49240 RVA: 0x003AA124 File Offset: 0x003A8324
		private string GetRewardStatusString(PlayerRewardTrackLevelState rewardTrackLevelState, bool forPaidTrack)
		{
			if (this.RewardExistsAtLevel(rewardTrackLevelState.Level, forPaidTrack))
			{
				return Enum.GetName(typeof(RewardTrackManager.RewardStatus), forPaidTrack ? rewardTrackLevelState.PaidRewardStatus : rewardTrackLevelState.FreeRewardStatus);
			}
			return "n/a";
		}

		// Token: 0x04009BDC RID: 39900
		private const RewardTrackManager.RewardStatus DefaultStatus = RewardTrackManager.RewardStatus.UNKNOWN;

		// Token: 0x04009BDD RID: 39901
		private readonly PlayerState<PlayerRewardTrackLevelState> m_rewardTrackLevelState = new PlayerState<PlayerRewardTrackLevelState>(new Func<int, PlayerRewardTrackLevelState>(RewardTrackManager.CreateDefaultLevelState));

		// Token: 0x04009BDE RID: 39902
		private readonly RewardPresenter m_rewardPresenter = new RewardPresenter();

		// Token: 0x04009BDF RID: 39903
		private readonly Map<ValueTuple<int, int, bool>, int> m_pendingRewardClaimRequests = new Map<ValueTuple<int, int, bool>, int>();

		// Token: 0x04009BE0 RID: 39904
		private int m_rewardTrackPageStartLevel;

		// Token: 0x04009BE1 RID: 39905
		private int m_rewardTrackPageEndLevel;

		// Token: 0x04009BE2 RID: 39906
		private bool m_isShowingSeasonRoll;

		// Token: 0x04009BE3 RID: 39907
		private RewardTrackDataModel m_prevSeasonDataModel;

		// Token: 0x04009BE4 RID: 39908
		private RewardTrackNodeRewardsDataModel m_prevSeasonChooseOneReward;

		// Token: 0x04009BE5 RID: 39909
		private Queue<RewardTrackUnclaimedRewards> m_rewardTrackUnclaimedNotifications = new Queue<RewardTrackUnclaimedRewards>();

		// Token: 0x020028FD RID: 10493
		public enum RewardStatus
		{
			// Token: 0x0400FB76 RID: 64374
			UNKNOWN,
			// Token: 0x0400FB77 RID: 64375
			GRANTED,
			// Token: 0x0400FB78 RID: 64376
			ACKED,
			// Token: 0x0400FB79 RID: 64377
			RESET
		}
	}
}
