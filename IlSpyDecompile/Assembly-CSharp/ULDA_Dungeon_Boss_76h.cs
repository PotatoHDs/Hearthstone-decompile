using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_76h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01.prefab:9b4c06729e6039c4780c734741b37d74");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01.prefab:3bf7905f1cbcd4141b8f1e59da315c15");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01.prefab:fa86f12201d231046833af91c1860bd4");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01.prefab:8de52c1facb71234a9e3b3ee08a846ae");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01.prefab:dd7819bb7c633554d9215357c51c01ed");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01.prefab:a3f1d2d0d18732147b57a6de8bd7f49c");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_DefeatPlayer_01.prefab:62c754f4349233140bb7ce0b59a35645");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01.prefab:df4d2803cef85a94eb991c35fbdab650");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01.prefab:c7ab1d95a90160546b01bce377f11cd1");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02.prefab:47f6e36096b69e44a9a0b9dd98d83cee");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03.prefab:a036c2e85ae2fa54ea8df93102fe8578");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05.prefab:f9aa8eb50f2f46542a91932685b20e49");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01.prefab:41ef35d280ac8db419c8d0a5840a111c");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02.prefab:05e5998d4dfa6ad40af674e41707574a");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03.prefab:3d3ee22babf316c4999a4b644a4f6258");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01.prefab:a30c11b19fd170f4e82d06fcd4cd7343");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroBrann_01.prefab:756d9a9b80d8c91448de51e1b67c3cf4");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01.prefab:7d486b9e0447d0e44a25fe90bac28290");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroFinley_01.prefab:9397da5a8dd81e04ebf9283da03f7c63");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroReno_01.prefab:e0c8919a5ab02814299c0e13f0604394");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01.prefab:a170e3c8678052540a514672a6006ccb");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01.prefab:a52a618cd4899e446858892f4aea6510");

	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01.prefab:99471727957a2d04689c26ce214916a6");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_DefeatPlayer_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02,
			VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03, VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroBrann_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroFinley_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroReno_01,
			VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01, VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01;
		m_deathLine = VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
			case "ULD_179":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01);
				break;
			case "ULD_152":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01);
				break;
			case "EX1_407":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01);
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
			case "DAL_731":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01);
				break;
			case "ULD_143":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01);
				break;
			case "ULD_728":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01);
				break;
			case "CS2_097":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01);
				break;
			}
		}
	}
}
