using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_45h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01.prefab:dff001d7e7f662b44ae169a2143123c4");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01.prefab:8aea94f72b105934bb226ff0bd2d9908");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01.prefab:0ff5fb95711d2244f9c22f50fe618571");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01.prefab:89d0a59f276fe774e824e6e2358e8486");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_DefeatPlayer_01.prefab:d4ca6d27238242f4b8dffaca9df0324f");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01.prefab:0108c06139d601e4394686211c2f76b4");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01.prefab:f6ea52763d07084478095e57f63bd74b");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02.prefab:5f886a597e174964289e04deb6d4145b");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03.prefab:3c597914ab6be4c43a6534d2d5e23d42");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04.prefab:8267c8f61a94f264482bdf47501e21cd");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01.prefab:f477c776226a69d449301e476ed9fcf0");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02.prefab:50e82ccc9e84b844b8c15834420b8029");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01.prefab:3e2ceb3badbb1504c8c13a3484767d69");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02.prefab:18bc370fe10fab5489d37f30c795cfb1");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03.prefab:70002a8c8817a5d47b1c5c46d012ee77");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04.prefab:b2335794ca49968458d75a7dcd9b8157");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01.prefab:a97a1d17445e2ff4bbe9fb512b6c6489");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01.prefab:bac0437383f783343a47a995a0cfe861");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01.prefab:331e705c15eb171428f5a0ae9454dcbc");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01.prefab:d08cf2b6ced6ba34b9c8a6cc4f4fa202");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01.prefab:7e4af84ed70d03c4cb4c13091457e167");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01.prefab:bcae8eb60177c3c4985cc24d242063b9");

	private static readonly AssetReference VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01 = new AssetReference("VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01.prefab:75799adf78bc5e54880a548ea9e6de45");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04 };

	private List<string> m_HeroPowerTreasureLines = new List<string> { VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01, VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01, VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01, VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01, VO_ULDA_BOSS_45h_Male_BloodElf_DefeatPlayer_01, VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_01, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_02, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_03, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPower_04,
			VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_01, VO_ULDA_BOSS_45h_Male_BloodElf_HeroPowerTreasure_02, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_01, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_02, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_03, VO_ULDA_BOSS_45h_Male_BloodElf_Idle_04, VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01, VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01, VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01, VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01,
			VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01, VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01, VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01
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
		m_introLine = VO_ULDA_BOSS_45h_Male_BloodElf_Intro_01;
		m_deathLine = VO_ULDA_BOSS_45h_Male_BloodElf_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_45h_Male_BloodElf_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			switch (cardId)
			{
			case "ULDA_Brann":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_45h_Male_BloodElf_IntroBrann_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "ULDA_Elise":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_45h_Male_BloodElf_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "ULDA_Finley":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_45h_Male_BloodElf_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "ULDA_Reno":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_45h_Male_BloodElf_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			default:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTreasureLines);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "AT_033":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_45h_Male_BloodElf_PlayerBurgle_01);
				break;
			case "ULD_172":
			case "ULD_707":
			case "ULD_715":
			case "ULD_717":
			case "ULD_718":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_45h_Male_BloodElf_PlayerPlagueSpell_01);
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
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "EX1_590":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_45h_Male_BloodElf_BossBloodKnight_01);
				break;
			case "ULD_145":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_45h_Male_BloodElf_BossBrazenZealout_01);
				break;
			case "LOE_116":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_45h_Male_BloodElf_BossReliquarySeeker_01);
				break;
			}
		}
	}
}
