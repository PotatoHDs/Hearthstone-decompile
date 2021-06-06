using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoM_01_Rokara_Dungeon : BoM_01_Rokara_MissionEntity
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01.prefab:11a175183db47a2479e85d896681ace2");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02.prefab:0f5afd55abea8414ab0666ad2d554856");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03.prefab:76aeab237ec8c5249afe67a0818dfd27");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_04.prefab:4d14e36f878dc0b4d942948de1889d6e");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05.prefab:2e39f62db6c882e4f995500a61a10d05");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06.prefab:380b013abcc400940967646bf74d1b8c");

	private List<string> m_Brukan_HeroPowerLines = new List<string> { VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06 };

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01.prefab:76d1d6750f4df5f42b9be52198b91d26");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_02.prefab:7a343d0c99bbcba40a35d7da20c4e63a");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03.prefab:f3fc710cf6c0f77459668006ee89ff24");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04.prefab:43e03a31a84886344bbddf022687ca4b");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06.prefab:a318145d17c8edf4f8558d02cdac8bfd");

	private List<string> m_Guff_HeroPowerLines = new List<string> { VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06 };

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01.prefab:a087f78e1d15dd14f8a9ea9161837d12");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02.prefab:a4b7a5b42073a5345ae27e15ec092f67");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_03.prefab:f72faaa261994144c9e392a8b623b21c");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04.prefab:ac3868d46ef9a834c8230c4b9fa26e8c");

	private List<string> m_Tamsin_HeroPowerLines = new List<string> { VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04 };

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01.prefab:18a6357110fe5f143ace127d06ea34ba");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02.prefab:0c2f296e8512e684680879cdac1cc46b");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03.prefab:c72eda15e76cfc44fae7e09af9993f42");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04.prefab:17f4aa15f1baa8f42b4ebef24fb8c799");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05.prefab:13846171e1eb4824288706cb62c3a910");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06.prefab:959a3b20fe5da1f4c8675fbd0d8a6dca");

	private List<string> m_Dawngrasp_HeroPowerLines = new List<string> { VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06 };

	public bool HeroPowerIsBrukan;

	public bool HeroPowerIsGuff;

	public bool HeroPowerIsTamsin;

	public bool HeroPowerIsDawngrasp;

	public readonly AssetReference Garrosh_BrassRing = new AssetReference("Garrosh_BrassRing_Quote.prefab:9c911310fb2bf7246ae78ef14a1b4dc5");

	public readonly AssetReference Brukan_BrassRing = new AssetReference("Brukan_BrassRing_Quote.prefab:16aa2801dfe06db489bd2259944af32b");

	public readonly AssetReference Guff_BrassRing = new AssetReference("Guff_BrassRing_Quote.prefab:2b02f1e9a212d7e41ace41f997923b8a");

	public readonly AssetReference Rokara_B_BrassRing = new AssetReference("Rokara_B_BrassRing_Quote.prefab:301c3d7a32636944884d6fa120099950");

	public readonly AssetReference Tamsin_BrassRing = new AssetReference("Tamsin_BrassRing_Quote.prefab:62964357f9958d64f9346685fc1f87f5");

	public readonly AssetReference Dawngrasp_BrassRing = new AssetReference("Dawngrasp_BrassRing_Quote.prefab:45d9ad7c018bcf7429f8ff3d10e2aaf0");

	public readonly AssetReference Kazakus_BrassRing = new AssetReference("Kazakus_BrassRing_Quote.prefab:74f40b18119e73f4fb7b8bc9c3f9b70f");

	public const int SetHeroPowerBrukan = 58024;

	public const int SetHeroPowerGuff = 58025;

	public const int SetHeroPowerTamsin = 58026;

	public const int SetHeroPowerDawngrasp = 58027;

	public const int InGame_BrukanEOT = 58028;

	public const int InGame_GuffEOT = 58029;

	public const int InGame_TamsinEOT = 58030;

	public const int InGame_DawngraspEOT = 58031;

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = null;
		m_deathLine = null;
		m_standardEmoteResponseLine = null;
		m_BossIdleLines = new List<string>(GetBossIdleLines());
		m_BossIdleLinesCopy = new List<string>(GetBossIdleLines());
		m_OverrideMusicTrack = MusicPlaylistType.Invalid;
		m_OverrideMulliganMusicTrack = MusicPlaylistType.Invalid;
		m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
		m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
		m_OverrideBossSubtext = null;
		m_OverridePlayerSubtext = null;
		m_SupressEnemyDeathTextBubble = true;
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_03, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04,
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_04, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_02, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04,
			VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOM;
	}

	public static BoM_01_Rokara_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BoM_01_Rokara_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
		return new BoM_01_Rokara_Dungeon();
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		entity.GetCardId();
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		MissionPause(pause: true);
		yield return HandleMissionEventWithTiming(514);
		MissionPause(pause: false);
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		Random.Range(0f, 1f);
		GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		switch (missionEvent)
		{
		case 1000:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (m_PlayPlayerVOLineIndex + 1 >= m_PlayerVOLines.Count)
			{
				m_PlayPlayerVOLineIndex = 0;
			}
			else
			{
				m_PlayPlayerVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			yield return PlayBossLine(actor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			break;
		case 1001:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			yield return PlayBossLine(actor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			break;
		case 1002:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (m_PlayBossVOLineIndex + 1 >= m_BossVOLines.Count)
			{
				m_PlayBossVOLineIndex = 0;
			}
			else
			{
				m_PlayBossVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(enemyActor, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(enemyActor, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1011:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string bossVOLine in m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(bossVOLine);
				yield return MissionPlayVO(enemyActor, bossVOLine);
			}
			foreach (string playerVOLine in m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(playerVOLine);
				yield return MissionPlayVO(enemyActor, playerVOLine);
			}
			break;
		case 1012:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string bossVOLine2 in m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(bossVOLine2);
				yield return MissionPlayVO(enemyActor, bossVOLine2);
			}
			break;
		case 1013:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string playerVOLine2 in m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(playerVOLine2);
				yield return MissionPlayVO(enemyActor, playerVOLine2);
			}
			break;
		case 1010:
			if (m_forceAlwaysPlayLine)
			{
				m_forceAlwaysPlayLine = false;
			}
			else
			{
				m_forceAlwaysPlayLine = true;
			}
			break;
		case 58023:
		{
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
			SceneMgr.Get().SetNextMode(postGameSceneMode);
			break;
		}
		case 600:
			m_Mission_EnemyHeroShouldExplodeOnDefeat = false;
			break;
		case 610:
			m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
			break;
		case 601:
			m_Mission_FriendlyHeroShouldExplodeOnDefeat = false;
			break;
		case 611:
			m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
			break;
		case 603:
			m_MissionDisableAutomaticVO = false;
			break;
		case 602:
			m_MissionDisableAutomaticVO = true;
			break;
		case 612:
			m_DoEmoteDrivenStart = true;
			break;
		case 516:
			if (m_SupressEnemyDeathTextBubble)
			{
				yield return MissionPlaySound(enemyActor, m_deathLine);
			}
			else
			{
				yield return MissionPlayVO(enemyActor, m_deathLine);
			}
			break;
		case 58024:
			HeroPowerIsBrukan = true;
			HeroPowerIsGuff = false;
			HeroPowerIsTamsin = false;
			HeroPowerIsDawngrasp = false;
			break;
		case 58025:
			HeroPowerIsBrukan = false;
			HeroPowerIsGuff = true;
			HeroPowerIsTamsin = false;
			HeroPowerIsDawngrasp = false;
			break;
		case 58026:
			HeroPowerIsBrukan = false;
			HeroPowerIsGuff = false;
			HeroPowerIsTamsin = true;
			HeroPowerIsDawngrasp = false;
			break;
		case 58027:
			HeroPowerIsBrukan = false;
			HeroPowerIsGuff = false;
			HeroPowerIsTamsin = false;
			HeroPowerIsDawngrasp = true;
			break;
		case 508:
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Brukan_HeroPowerLines);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Guff_HeroPowerLines);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Tamsin_HeroPowerLines);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Dawngrasp_HeroPowerLines);
			}
			break;
		case 58028:
			if (!m_MissionDisableAutomaticVO)
			{
				GameState.Get().SetBusy(busy: true);
				yield return MissionPlayVO("BOM_01_Brukan_08t", m_Brukan_HeroPowerLines);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 58029:
			if (!m_MissionDisableAutomaticVO)
			{
				GameState.Get().SetBusy(busy: true);
				yield return MissionPlayVOOnce("BOM_01_Guff_02t", m_Guff_HeroPowerLines);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 58030:
			if (!m_MissionDisableAutomaticVO)
			{
				GameState.Get().SetBusy(busy: true);
				yield return MissionPlayVOOnce("BOM_01_Tamsin_03t", m_Tamsin_HeroPowerLines);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 58031:
			if (!m_MissionDisableAutomaticVO)
			{
				GameState.Get().SetBusy(busy: true);
				yield return MissionPlayVOOnce("BOM_01_Dawngrasp_04t", m_Dawngrasp_HeroPowerLines);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}
}
