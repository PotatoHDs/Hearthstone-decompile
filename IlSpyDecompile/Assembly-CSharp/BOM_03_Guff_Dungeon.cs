using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOM_03_Guff_Dungeon : BOM_03_Guff_MissionEntity
{
	public readonly AssetReference Brukan_BrassRing = new AssetReference("Brukan_BrassRing_Quote.prefab:16aa2801dfe06db489bd2259944af32b");

	public readonly AssetReference Rokara_B_BrassRing = new AssetReference("Rokara_B_BrassRing_Quote.prefab:301c3d7a32636944884d6fa120099950");

	public readonly AssetReference Tamsin_BrassRing = new AssetReference("Tamsin_BrassRing_Quote.prefab:62964357f9958d64f9346685fc1f87f5");

	public readonly AssetReference Dawngrasp_BrassRing = new AssetReference("Dawngrasp_BrassRing_Quote.prefab:45d9ad7c018bcf7429f8ff3d10e2aaf0");

	public readonly AssetReference Hamuul_20_4_BrassRing_Quote = new AssetReference("Hamuul_20_4_BrassRing_Quote.prefab:54c037c90dc48994b8db6374e72f32ab");

	public readonly AssetReference Naralex_BrassRing = new AssetReference("Naralex_BrassRing_Quote.prefab:6bbc6ac031d7ccf48a6e7edd7933d248");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01.prefab:e68a254459535874c93976f6f44c2612");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02.prefab:86eb7ab46c12a0f45b99589387128a14");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03.prefab:94d244024e2844648b14650966ef2b6f");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04.prefab:c675321f29a32224e816755609b3d64e");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05.prefab:27b889a954e6d444f8bd91fe7b5fb7f9");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06.prefab:3dbe08c856d9df94b8612e68e6438357");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01.prefab:25487958973e1c44b8420788fb3ef1dd");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02.prefab:3d9fa43f0094f3744bcd268400aa1158");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03.prefab:7728604172e2de14194b7ce46cbf27c4");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04.prefab:36aa36379df63984a8ce0679d1ad4d33");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05.prefab:59a194a3be06a5c4eba09d6caad103fd");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06.prefab:243169e212bd7464493d04a1772f4894");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01.prefab:a9e2702de8692584b8122089162dfaca");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02.prefab:e61c4b0290c531e4089794f1ee41bb37");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03.prefab:1b193b2362a255c41965142cb1aa3e32");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04.prefab:4778ed0a3df0e444a90a136bf75637d0");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05.prefab:e8345dd582c4d8a4d8fdb0b29f5594e6");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06.prefab:868f935cd5262d7478b304db1fc9097c");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02.prefab:5c59fe1390006ac47893c626407cfeb4");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04.prefab:14232e12bd04f984094b465344d47a4f");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05.prefab:d54d0e82d394b754ab824a1abf586137");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06.prefab:f5da77d66fd1f594c8bbb082353258ce");

	private List<string> m_Tamsin_HeroPowerLines = new List<string> { VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06 };

	private List<string> m_Rokara_HeroPowerLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06 };

	private List<string> m_Dawngrasp_HeroPowerLines = new List<string> { VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06 };

	private List<string> m_Brukan_HeroPowerLines = new List<string> { VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06 };

	public bool HeroPowerBrukan;

	public bool HeroPowerTamsin;

	public bool HeroPowerDawngrasp;

	public bool HeroPowerRokara;

	public const int Tavish_TriggerLine = 58032;

	public const int Tavish_DeathLines = 58033;

	public const int Tavish_HealLines = 58034;

	public const int Tavish_IsDeadLines = 58035;

	public const int Tavish_RezLines = 58036;

	public const int Tavish_Attack = 58042;

	public const int Scabbs_RezLines = 58037;

	public const int Scabbs_DeathLines = 58038;

	public const int Scabbs_TriggerLines = 58039;

	public const int Scabbs_HealLines = 58040;

	public const int Scabbs_isDeadLines = 58041;

	public const int Scabbs_Attack = 58043;

	public bool m_Scabbs_isDead;

	public bool m_Tavish_isDead;

	public const int XyrellaCustomIdle = 58042;

	public const int SetHeroPowerBrukan = 58024;

	public const int SetHeroPowerRokara = 58025;

	public const int SetHeroPowerTamsin = 58026;

	public const int SetHeroPowerDawngrasp = 58027;

	public const float m_Xyrella_HP_Speaking_Chance = 0.5f;

	public const float m_Xyrella_HP_Speaking_Delay = 20f;

	public float m_Xyrella_HP_Seconds_Since_Action;

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
		m_SupressEnemyDeathTextBubble = false;
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06,
			VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04,
			VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06
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

	public static BOM_03_Guff_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BOM_03_Guff_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
		return new BOM_03_Guff_Dungeon();
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
		Random.Range(0f, 1f);
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
			HeroPowerBrukan = true;
			HeroPowerRokara = false;
			HeroPowerTamsin = false;
			HeroPowerDawngrasp = false;
			break;
		case 58025:
			HeroPowerBrukan = false;
			HeroPowerRokara = true;
			HeroPowerTamsin = false;
			HeroPowerDawngrasp = false;
			break;
		case 58026:
			HeroPowerBrukan = false;
			HeroPowerRokara = false;
			HeroPowerTamsin = true;
			HeroPowerDawngrasp = false;
			break;
		case 58027:
			HeroPowerBrukan = false;
			HeroPowerRokara = false;
			HeroPowerTamsin = false;
			HeroPowerDawngrasp = true;
			break;
		case 508:
			if (HeroPowerBrukan)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Brukan_HeroPowerLines);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Rokara_HeroPowerLines);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Tamsin_HeroPowerLines);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlaySound(friendlyHeroPowerActor, m_Dawngrasp_HeroPowerLines);
			}
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}
}
