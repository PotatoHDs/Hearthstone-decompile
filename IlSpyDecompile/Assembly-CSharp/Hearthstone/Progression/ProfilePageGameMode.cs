using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class ProfilePageGameMode : MonoBehaviour
	{
		private enum GameModeIconID
		{
			ARENA,
			BATTLEGROUNDS,
			PVPDR_HEROIC,
			PVPDR_NORMAL
		}

		public static readonly AssetReference PROFILE_PAGE_GAME_MODE_PREFAB = new AssetReference("ProfilePageGameMode.prefab:f475550e593d4ac4d927f215596ff43d");

		private Widget m_widget;

		private RankedPlayListDataModel m_rankedPlayDataModels;

		private ProfileGameModeStatDataModel m_arenaDataModel;

		private ProfileGameModeStatDataModel m_bgDataModel;

		private ProfileGameModeStatDataModel m_pvpdrDataModel;

		private ProfileGameModeStatListDataModel m_gameModeStatDataModels;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_rankedPlayDataModels = new RankedPlayListDataModel();
			m_arenaDataModel = new ProfileGameModeStatDataModel();
			m_bgDataModel = new ProfileGameModeStatDataModel();
			m_pvpdrDataModel = new ProfileGameModeStatDataModel();
			m_gameModeStatDataModels = new ProfileGameModeStatListDataModel();
			if (NetCache.Get().GetNetObject<NetCache.NetCacheBaconRatingInfo>() == null)
			{
				Network.Get().RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, OnBattlegroundsRatingInfo);
				Network.Get().RequestBaconRatingInfo();
			}
			if (NetCache.Get().GetNetObject<NetCache.NetCachePVPDRStatsInfo>() == null)
			{
				Network.Get().RegisterNetHandler(PVPDRStatsInfoResponse.PacketID.ID, OnPVPDungeonRunRatingInfo);
				Network.Get().RequestPVPDRStatsInfo();
			}
			InitRankAndStats();
		}

		public void Show()
		{
			m_widget.Show();
			UpdateData();
		}

		public void Hide()
		{
			m_widget.Hide();
		}

		private void OnBattlegroundsRatingInfo()
		{
			Network.Get().RemoveNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, OnBattlegroundsRatingInfo);
			GetTotalWins(out var _, out var _, out var totalBgWins, out var _);
			UpdateBgStatDataModel(totalBgWins);
		}

		private void OnPVPDungeonRunRatingInfo()
		{
			Network.Get().RemoveNetHandler(PVPDRStatsInfoResponse.PacketID.ID, OnPVPDungeonRunRatingInfo);
			GetTotalWins(out var _, out var _, out var _, out var totalDuelsWins);
			UpdateDuelsStatDataModel(totalDuelsWins);
		}

		private void UpdateData()
		{
			GetTotalWins(out var totalRankedWins, out var totalArenaWins, out var totalBgWins, out var totalDuelsWins);
			UpdateRankedMedals(totalRankedWins);
			UpdateGameModeStats(totalArenaWins, totalBgWins, totalDuelsWins);
		}

		private void UpdateRankedMedals(int totalRankedWins)
		{
			m_rankedPlayDataModels.Items.Clear();
			MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
			foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
			{
				if (value != 0 && RankMgr.Get().IsFormatAllowedInLeague(value))
				{
					localPlayerMedalInfo.CreateDataModel(value, RankedMedal.DisplayMode.Default, isTooltipEnabled: true, hasEarnedCardBack: false, delegate(RankedPlayDataModel dm)
					{
						m_rankedPlayDataModels.Items.Add(dm);
					});
				}
			}
			m_rankedPlayDataModels.TotalWins = totalRankedWins;
		}

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
			foreach (NetCache.PlayerRecord record in netObject.Records)
			{
				if (record != null && record.Data == 0)
				{
					switch (record.RecordType)
					{
					case GameType.GT_ARENA:
						totalArenaWins += record.Wins;
						break;
					case GameType.GT_RANKED:
						totalRankedWins += record.Wins;
						break;
					case GameType.GT_BATTLEGROUNDS:
						totalBgWins += record.Wins;
						break;
					case GameType.GT_PVPDR_PAID:
					case GameType.GT_PVPDR:
						totalDuelsWins += record.Wins;
						break;
					}
				}
			}
		}

		private void InitRankAndStats()
		{
			GetTotalWins(out var totalRankedWins, out var totalArenaWins, out var totalBgWins, out var totalDuelsWins);
			UpdateRankedMedals(totalRankedWins);
			InitArenaStatDataModel(totalArenaWins);
			m_gameModeStatDataModels.Items.Add(m_arenaDataModel);
			InitBgStatDataModel(totalBgWins);
			m_gameModeStatDataModels.Items.Add(m_bgDataModel);
			InitDuelsStatDataModel(totalDuelsWins);
			m_gameModeStatDataModels.Items.Add(m_pvpdrDataModel);
			m_widget.BindDataModel(m_rankedPlayDataModels);
			m_widget.BindDataModel(m_gameModeStatDataModels);
		}

		private void InitArenaStatDataModel(int totalArenaWins)
		{
			m_arenaDataModel.ModeIcon = 0;
			m_arenaDataModel.ModeName = GameStrings.Format("GLUE_QUEST_LOG_ARENA");
			m_arenaDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_ARENA_STAT_TOOLTIP_TITLE");
			m_arenaDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_ARENA_STAT_TOOLTIP");
			UpdateArenaStatDataModel(totalArenaWins);
		}

		private void UpdateArenaStatDataModel(int totalArenaWins)
		{
			NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
			m_arenaDataModel.StatValue.Add(netObject?.BestForgeWins ?? 0);
			m_arenaDataModel.StatValue.Add(totalArenaWins);
		}

		private void InitBgStatDataModel(int totalBgWins)
		{
			m_bgDataModel.ModeIcon = 1;
			m_bgDataModel.ModeName = GameStrings.Format("GLOBAL_BATTLEGROUNDS");
			m_bgDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_BATTLEGROUNDS_STAT_TOOLTIP_TITLE");
			m_bgDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_BATTLEGROUNDS_STAT_TOOLTIP");
			UpdateBgStatDataModel(totalBgWins);
		}

		private void UpdateBgStatDataModel(int totalBgWins)
		{
			NetCache.NetCacheBaconRatingInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBaconRatingInfo>();
			m_bgDataModel.StatValue.Add(netObject?.Rating ?? 0);
			m_bgDataModel.StatValue.Add(totalBgWins);
		}

		private void InitDuelsStatDataModel(int totalDuelsWins)
		{
			m_pvpdrDataModel.ModeName = GameStrings.Get("GLUE_PVPDR");
			UpdateBgStatDataModel(totalDuelsWins);
		}

		private void UpdateDuelsStatDataModel(int totalDuelsWins)
		{
			NetCache.NetCachePVPDRStatsInfo netObject = NetCache.Get().GetNetObject<NetCache.NetCachePVPDRStatsInfo>();
			if (netObject != null)
			{
				if (netObject.Rating >= netObject.PaidRating)
				{
					m_pvpdrDataModel.ModeIcon = 3;
					m_pvpdrDataModel.StatValue.Add(netObject.Rating);
					m_pvpdrDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP_TITLE");
					m_pvpdrDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP");
				}
				else
				{
					m_pvpdrDataModel.ModeIcon = 2;
					m_pvpdrDataModel.StatValue.Add(netObject.PaidRating);
					m_pvpdrDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_HEROIC_STAT_TOOLTIP_TITLE");
					m_pvpdrDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_HEROIC_STAT_TOOLTIP");
				}
			}
			else
			{
				m_pvpdrDataModel.StatValue.Add(0);
				m_pvpdrDataModel.StatName = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP_TITLE");
				m_pvpdrDataModel.StatDesc = GameStrings.Get("GLOBAL_PROGRESSION_PROFILE_PVPDR_CASUAL_STAT_TOOLTIP");
			}
			m_pvpdrDataModel.StatValue.Add(totalDuelsWins);
		}

		private void UpdateGameModeStats(int totalArenaWins, int totalBgWins, int totalDuelsWins)
		{
			m_arenaDataModel.StatValue.Clear();
			m_bgDataModel.StatValue.Clear();
			m_pvpdrDataModel.StatValue.Clear();
			UpdateArenaStatDataModel(totalArenaWins);
			UpdateBgStatDataModel(totalBgWins);
			UpdateDuelsStatDataModel(totalDuelsWins);
		}
	}
}
