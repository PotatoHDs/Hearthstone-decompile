using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_75h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01.prefab:491b1d1633064b44ba4a511090155e3c");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01.prefab:e94e4e7b982144240a94256d1cbdd21b");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01.prefab:1c6773a7874e4ae42b6bf1c6f4e9656d");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01.prefab:340a64243a4a72449b0bc9b0107322a1");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01.prefab:f3a81f48488ba174eada659e4628135a");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_DefeatPlayer_01.prefab:c19e57d305dbfbd4d9ba2f2441cb69f7");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01.prefab:7255cc75cecbad84298ad2ce7c71461a");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02.prefab:55497f08bbd61ef4fb8874afc8c6973d");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03.prefab:42c848f589a15a842b9d3fc6b2cc59e6");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04.prefab:2883497e2dbda1e46bf8559e033287d8");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05.prefab:a4c2c8264e48f8d44833d3be8f5d0f03");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01.prefab:27304034fdcc65346a666ac4e6a1a289");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02.prefab:53c305e0e1365c847a3c3691e726d5fd");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03.prefab:c1223879320e4544cbb13ecc654b3b3a");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01.prefab:28766bacda9490448aa113df3a8296ff");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01.prefab:682d0bd60e694854b9e4b039ebb11899");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01.prefab:17d50b6331e346d46abae7fe2cb7722a");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01.prefab:d620619241697704bbd4a68f16121449");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01.prefab:3d2cff3f5aa90564f926633b8c81fa11");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01.prefab:5f0e9bb4dacd41947b09e5a048309e7c");

	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01.prefab:bee07d18a9f80584abe5e5529dd4e006");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04, VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_DefeatPlayer_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02, VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03, VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04,
			VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03, VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01, VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01,
			VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01
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
		m_introLine = VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01;
		m_deathLine = VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
			case "ULDA_Reno":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "ULDA_Elise":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "ULDA_Brann":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "ULDA_Finley":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ULD_719"))
		{
			if (cardId == "ULD_292")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01);
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
			case "ULD_430":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01);
				break;
			case "EX1_539":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01);
				break;
			case "ULD_194":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01);
				break;
			case "ULD_429":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01);
				break;
			}
		}
	}
}
