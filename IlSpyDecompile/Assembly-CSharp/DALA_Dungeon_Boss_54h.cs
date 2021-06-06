using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_54h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01.prefab:2ad92defe613eb044a3ff8783d7409fb");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01.prefab:b1b6892cd8914ba4489118cd65b62f33");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02.prefab:70f6d82bab328ae4797463d8a8bd64ad");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01.prefab:96b2d24132b421c4d83cd5dfaf8ee0ea");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02.prefab:ea9058ed4596a8946af2dde75df44432");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Death_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Death_01.prefab:6405e6724f466b44ab720a2cefc7ca81");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_DefeatPlayer_01.prefab:5ef8cae2dfe289144bba2d0bee96ddcf");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01.prefab:469889c1cb4db2c4499b9bc5ac9688bb");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Exposition_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Exposition_01.prefab:64b51fbcb58d23047a25770de356e32a");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01.prefab:86fcc403ff4a6234aaaa1940883ddbb6");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02.prefab:64ea229c4e4e1b8498bc84abd219351c");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01.prefab:b2e7e9e06e551b4479687281e49daa4f");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02.prefab:191edf8126d85904bb1b9d561c6a01e9");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03.prefab:3d773d9012ec5184b88e839c6b4217bb");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Idle_02.prefab:d49961c4d1eddb0459e0f79d0fc04eb8");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Idle_03.prefab:2bd18c7f92b00c44b8a6d86f8657a4d2");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Idle_04.prefab:87463035dbf5d2745bd7965359f20edd");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Intro_01.prefab:884a25bceb1f1db44bb516787dd7ef82");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01.prefab:7d9970dedeb8112498bf49609af5eea5");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01.prefab:de3b4da57d39de3469fb7c334f0b0132");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01.prefab:1a32183f1da1ab347b299692e7550bd8");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01.prefab:29be779eeea6c5e45962ceb0679e8a3b");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02.prefab:d883d936d67bbc945b0cfa9992af98ec");

	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01.prefab:49b0c84d18cd3c24a848117799bd428b");

	private static List<string> m_BossLavaShock = new List<string> { VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01, VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02 };

	private static List<string> m_BossOverloadPass = new List<string> { VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01, VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02 };

	private static List<string> m_BossHeroPowerTriggers = new List<string> { VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02 };

	private static List<string> m_PlayerHeroPowerTriggers = new List<string> { VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03 };

	private static List<string> m_PlayerUnlockMana = new List<string> { VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01, VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_54h_Male_Goblin_Idle_02, VO_DALA_BOSS_54h_Male_Goblin_Idle_03, VO_DALA_BOSS_54h_Male_Goblin_Idle_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01, VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01, VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02, VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01, VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02, VO_DALA_BOSS_54h_Male_Goblin_Death_01, VO_DALA_BOSS_54h_Male_Goblin_DefeatPlayer_01, VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01, VO_DALA_BOSS_54h_Male_Goblin_Exposition_01, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01,
			VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02, VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03, VO_DALA_BOSS_54h_Male_Goblin_Idle_02, VO_DALA_BOSS_54h_Male_Goblin_Idle_03, VO_DALA_BOSS_54h_Male_Goblin_Idle_04, VO_DALA_BOSS_54h_Male_Goblin_Intro_01, VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01, VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01,
			VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01, VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01, VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02, VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_54h_Male_Goblin_Intro_01;
		m_deathLine = VO_DALA_BOSS_54h_Male_Goblin_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_54h_Male_Goblin_Exposition_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerUnlockMana);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerHeroPowerTriggers);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPowerTriggers);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossOverloadPass);
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "DAL_088":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01);
				break;
			case "DAL_720":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01);
				break;
			case "DAL_739":
			case "DAL_741":
			case "DAL_613":
			case "DAL_614":
			case "DAL_615":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01);
				break;
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "BRM_011")
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossLavaShock);
			}
		}
	}
}
