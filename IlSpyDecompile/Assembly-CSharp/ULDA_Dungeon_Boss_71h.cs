using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_71h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01.prefab:9ab702164bdf9fd40a61ad7f55cf3cad");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01.prefab:ab112c9ee3a74c54db65ece8076b3680");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01.prefab:33a05918d3451e940bd69b07439a2928");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01.prefab:3402a6d59faa1a2449c313a293455e8a");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_DefeatPlayer_01.prefab:d32314c7acbedfa46b959bd109eb52be");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01.prefab:0d45150628fcddf458ddf04468e7523c");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01.prefab:27e653f08ea5cc84e99b66d5fa984e92");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02.prefab:8613b0c830e8f03458c9f2915b0a21bc");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03.prefab:2b72d37ad26a81141a85120af4d5b25b");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04.prefab:4b76c1d2bf312f441ab03a37a6774723");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01.prefab:a75591ba5a2d49d4e9176ab048af0a3b");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02.prefab:da27890ccd9a93d4daa3c48518d5ccdc");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03.prefab:ed04416d6572afe4cb8483d34752e659");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01.prefab:65aa6b7b76277f343b80c00531b53863");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01.prefab:1091dccdaf4cc4342b79a377e428d641");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01.prefab:baffab126fabf3c4e92d42b6fb52132b");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01.prefab:8a0ae762684df394d9cc121866f1e34d");

	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01.prefab:2bdc3ac2476557546923a49511ed6010");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02, VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_DefeatPlayer_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03, VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04,
			VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02, VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03, VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01, VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01
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

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01;
		m_deathLine = VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
			case "EX1_158":
			case "EX1_571":
			case "GIL_663t":
			case "EX1_tk9":
			case "EX1_573t":
			case "ULD_137t":
			case "BOT_420":
			case "DAL_256":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01);
				break;
			case "GVG_033":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01);
				break;
			case "EX1_622":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01);
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
			case "ULD_135":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01);
				break;
			case "EX1_161":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01);
				break;
			case "EX1_164a":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01);
				break;
			}
		}
	}
}
