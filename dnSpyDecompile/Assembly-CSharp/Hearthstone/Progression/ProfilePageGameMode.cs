using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001113 RID: 4371
	[RequireComponent(typeof(WidgetTemplate))]
	public class ProfilePageGameMode : MonoBehaviour
	{
		// Token: 0x0600BF5D RID: 48989 RVA: 0x003A4C64 File Offset: 0x003A2E64
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_rankedPlayDataModels = new RankedPlayListDataModel();
			this.m_arenaDataModel = new ProfileGameModeStatDataModel();
			this.m_bgDataModel = new ProfileGameModeStatDataModel();
			this.m_pvpdrDataModel = new ProfileGameModeStatDataModel();
			this.m_gameModeStatDataModels = new ProfileGameModeStatListDataModel();
			if (NetCache.Get().GetNetObject<NetCache.NetCacheBaconRatingInfo>() == null)
			{
				Network.Get().RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.OnBattlegroundsRatingInfo), null);
				Network.Get().RequestBaconRatingInfo();
			}
			if (NetCache.Get().GetNetObject<NetCache.NetCachePVPDRStatsInfo>() == null)
			{
				Network.Get().RegisterNetHandler(PVPDRStatsInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDungeonRunRatingInfo), null);
				Network.Get().RequestPVPDRStatsInfo();
			}
			this.InitRankAndStats();
		}

		// Token: 0x0600BF5E RID: 48990 RVA: 0x003A4D2A File Offset: 0x003A2F2A
		public void Show()
		{
			this.m_widget.Show();
			this.UpdateData();
		}

		// Token: 0x0600BF5F RID: 48991 RVA: 0x003A4D3D File Offset: 0x003A2F3D
		public void Hide()
		{
			this.m_widget.Hide();
		}

		// Token: 0x0600BF60 RID: 48992 RVA: 0x003A4D4C File Offset: 0x003A2F4C
		private void OnBattlegroundsRatingInfo()
		{
			Network.Get().RemoveNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.OnBattlegroundsRatingInfo));
			int num;
			int num2;
			int totalBgWins;
			int num3;
			this.GetTotalWins(out num, out num2, out totalBgWins, out num3);
			this.UpdateBgStatDataModel(totalBgWins);
		}

		// Token: 0x0600BF61 RID: 48993 RVA: 0x003A4D90 File Offset: 0x003A2F90
		private void OnPVPDungeonRunRatingInfo()
		{
			Network.Get().RemoveNetHandler(PVPDRStatsInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDungeonRunRatingInfo));
			int num;
			int num2;
			int num3;
			int totalDuelsWins;
			this.GetTotalWins(out num, out num2, out num3, out totalDuelsWins);
			this.UpdateDuelsStatDataModel(totalDuelsWins);
		}

		// Token: 0x0600BF62 RID: 48994 RVA: 0x003A4DD4 File Offset: 0x003A2FD4
		private void UpdateData()
		{
			int totalRankedWins;
			int totalArenaWins;
			int totalBgWins;
			int totalDuelsWins;
			this.GetTotalWins(out totalRankedWins, out totalArenaWins, out totalBgWins, out totalDuelsWins);
			this.UpdateRankedMedals(totalRankedWins);
			this.UpdateGameModeStats(totalArenaWins, totalBgWins, totalDuelsWins);
		}

		// Token: 0x0600BF63 RID: 48995 RVA: 0x003A4E00 File Offset: 0x003A3000
		private void UpdateRankedMedals(int totalRankedWins)
		{
			this.m_rankedPlayDataModels.Items.Clear();
			MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
			foreach (object obj in Enum.GetValues(typeof(FormatType)))
			{
				FormatType formatType = (FormatType)obj;
				if (formatType != FormatType.FT_UNKNOWN && RankMgr.Get().IsFormatAllowedInLeague(formatType))
				{
					localPlayerMedalInfo.CreateDataModel(formatType, RankedMedal.DisplayMode.Default, true, false, delegate(RankedPlayDataModel dm)
					{
						this.m_rankedPlayDataModels.Items.Add(dm);
					});
				}
			}
			this.m_rankedPlayDataModels.TotalWins = totalRankedWins;
		}

		// Token: 0x0600BF64 RID: 48996 RVA: 0x003A4EAC File Offset: 0x003A30AC
		private void GetTotalWins(out int totalRankedWins, out int totalArenaWins, out int totalBgWins, out int totalDuelsWins)
		{
			totalRankedWins = 0;
			totalArenaWins = 0;
			totalBgWins = 0;
			totalDuelsWins = 0;
			NetCache.NetCachePlayerRecords netObject = NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>();
			if (netObject == null)
			{
				return;
			}
			foreach (NetCache.PlayerRecord playerRecord in netObject.Records)
			{
				if (playerRecord != null && playerRecord.Data == 0)
				{
					GameType recordType = playerRecord.RecordType;
					if (recordType <= GameType.GT_RANKED)
					{
						if (recordType != GameType.GT_ARENA)
						{
							if (recordType == GameType.GT_RANKED)
							{
								totalRankedWins += playerRecord.Wins;
							}
						}
						else
						{
							totalArenaWins += playerRecord.Wins;
						}
					}
					else if (recordType != GameType.GT_BATTLEGROUNDS)
					{
						if (recordType - GameType.GT_PVPDR_PAID <= 1)
						{
							totalDuelsWins += playerRecord.Wins;
						}
					}
					else
					{
						totalBgWins += playerRecord.Wins;
					}
				}
			}
		}

		// Token: 0x0600BF65 RID: 48997 RVA: 0x003A4F78 File Offset: 0x003A3178
		private void InitRankAndStats()
		{
			int totalRankedWins;
			int totalArenaWins;
			int totalBgWins;
			int totalDuelsWins;
			this.GetTotalWins(out totalRankedWins, out totalArenaWins, out totalBgWins, out totalDuelsWins);
			this.UpdateRankedMedals(totalRankedWins);
			this.InitArenaStatDataModel(totalArenaWins);
			this.m_gameModeStatDataModels.Items.Add(this.m_arenaDataModel);
			this.InitBgStatDataModel(totalBgWins);
			this.m_gameModeStatDataModels.Items.Add(this.m_bgDataModel);
			this.InitDuelsStatDataModel(totalDuelsWins);
			this.m_gameModeStatDataModels.Items.Add(this.m_pvpdrDataModel);
			this.m_widget.BindDataModel(this.m_rankedPlayDataModels, false);
			this.m_widget.BindDataModel(this.m_gameModeStatDataModels, false);
		}

		// Token: 0x0600BF66 RID: 48998 RVA: 0x003A5018 File Offset: 0x003A3218
		private void InitArenaStatDataModel(int totalArenaWins)
		{
			this.m_arenaDataModel.ModeIcon = 0;
			this.m_arenaDataModel.ModeName = GameStrings.Format("GLUE_QUEST_LOG_ARENA", Array.Empty<object>());
			this.m_arenaDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_ARENA_STAT_TOOLTIP_TITLE");
			this.m_arenaDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_ARENA_STAT_TOOLTIP");
			this.UpdateArenaStatDataModel(totalArenaWins);
		}

		// Token: 0x0600BF67 RID: 48999 RVA: 0x003A507C File Offset: 0x003A327C
		private void UpdateArenaStatDataModel(int totalArenaWins)
		{
			NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
			this.m_arenaDataModel.StatValue.Add((netObject != null) ? netObject.BestForgeWins : 0);
			this.m_arenaDataModel.StatValue.Add(totalArenaWins);
		}

		// Token: 0x0600BF68 RID: 49000 RVA: 0x003A50C4 File Offset: 0x003A32C4
		private void InitBgStatDataModel(int totalBgWins)
		{
			this.m_bgDataModel.ModeIcon = 1;
			this.m_bgDataModel.ModeName = GameStrings.Format("GLOBAL_BATTLEGROUNDS", Array.Empty<object>());
			this.m_bgDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_BATTLEGROUNDS_STAT_TOOLTIP_TITLE");
			this.m_bgDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_BATTLEGROUNDS_STAT_TOOLTIP");
			this.UpdateBgStatDataModel(totalBgWins);
		}

		// Token: 0x0600BF69 RID: 49001 RVA: 0x003A5128 File Offset: 0x003A3328
		private void UpdateBgStatDataModel(int totalBgWins)
		{
			NetCache.NetCacheBaconRatingInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBaconRatingInfo>();
			this.m_bgDataModel.StatValue.Add((netObject != null) ? netObject.Rating : 0);
			this.m_bgDataModel.StatValue.Add(totalBgWins);
		}

		// Token: 0x0600BF6A RID: 49002 RVA: 0x003A516D File Offset: 0x003A336D
		private void InitDuelsStatDataModel(int totalDuelsWins)
		{
			this.m_pvpdrDataModel.ModeName = GameStrings.Get("GLUE_PVPDR");
			this.UpdateBgStatDataModel(totalDuelsWins);
		}

		// Token: 0x0600BF6B RID: 49003 RVA: 0x003A518C File Offset: 0x003A338C
		private void UpdateDuelsStatDataModel(int totalDuelsWins)
		{
			NetCache.NetCachePVPDRStatsInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCachePVPDRStatsInfo>();
			if (netObject != null)
			{
				if (netObject.Rating >= netObject.PaidRating)
				{
					this.m_pvpdrDataModel.ModeIcon = 3;
					this.m_pvpdrDataModel.StatValue.Add(netObject.Rating);
					this.m_pvpdrDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP_TITLE");
					this.m_pvpdrDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP");
				}
				else
				{
					this.m_pvpdrDataModel.ModeIcon = 2;
					this.m_pvpdrDataModel.StatValue.Add(netObject.PaidRating);
					this.m_pvpdrDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_HEROIC_STAT_TOOLTIP_TITLE");
					this.m_pvpdrDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_HEROIC_STAT_TOOLTIP");
				}
			}
			else
			{
				this.m_pvpdrDataModel.StatValue.Add(0);
				this.m_pvpdrDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP_TITLE");
				this.m_pvpdrDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP");
			}
			this.m_pvpdrDataModel.StatValue.Add(totalDuelsWins);
		}

		// Token: 0x0600BF6C RID: 49004 RVA: 0x003A52A4 File Offset: 0x003A34A4
		private void UpdateGameModeStats(int totalArenaWins, int totalBgWins, int totalDuelsWins)
		{
			this.m_arenaDataModel.StatValue.Clear();
			this.m_bgDataModel.StatValue.Clear();
			this.m_pvpdrDataModel.StatValue.Clear();
			this.UpdateArenaStatDataModel(totalArenaWins);
			this.UpdateBgStatDataModel(totalBgWins);
			this.UpdateDuelsStatDataModel(totalDuelsWins);
		}

		// Token: 0x04009B6B RID: 39787
		public static readonly AssetReference PROFILE_PAGE_GAME_MODE_PREFAB = new AssetReference("ProfilePageGameMode.prefab:f475550e593d4ac4d927f215596ff43d");

		// Token: 0x04009B6C RID: 39788
		private Widget m_widget;

		// Token: 0x04009B6D RID: 39789
		private RankedPlayListDataModel m_rankedPlayDataModels;

		// Token: 0x04009B6E RID: 39790
		private ProfileGameModeStatDataModel m_arenaDataModel;

		// Token: 0x04009B6F RID: 39791
		private ProfileGameModeStatDataModel m_bgDataModel;

		// Token: 0x04009B70 RID: 39792
		private ProfileGameModeStatDataModel m_pvpdrDataModel;

		// Token: 0x04009B71 RID: 39793
		private ProfileGameModeStatListDataModel m_gameModeStatDataModels;

		// Token: 0x020028DC RID: 10460
		private enum GameModeIconID
		{
			// Token: 0x0400FB0E RID: 64270
			ARENA,
			// Token: 0x0400FB0F RID: 64271
			BATTLEGROUNDS,
			// Token: 0x0400FB10 RID: 64272
			PVPDR_HEROIC,
			// Token: 0x0400FB11 RID: 64273
			PVPDR_NORMAL
		}
	}
}
