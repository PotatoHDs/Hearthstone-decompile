using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_62h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01.prefab:7e0c6049a9f1a824d9ef770f28b84baa");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01.prefab:d19e6e93910fe994f83a1d6a18f1dd2e");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01.prefab:cc256eba57b4b5b4dbeea00ecef8cb8b");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02.prefab:718f6d2fcfb74fd439257088f8c1a628");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Death_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Death_01.prefab:0342ea44ad37295468518b25c89afb28");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_DefeatPlayer_01.prefab:379907e56504d784aba61da071f80fbb");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01.prefab:e371d0bc8542d9f4da88845135ec44f4");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01.prefab:b7e7e961893de844da4b1de329cdcd11");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02.prefab:3ff60493cc8e98d4b814a5194f9f9ef8");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03.prefab:81b7c5e0b1eb17d4da5fafefdfc87d40");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04.prefab:26b73cba9899d9743987b46befd8713b");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05.prefab:b14cda8388a313f4a80e9f5456c57dc3");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Idle_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Idle_01.prefab:c2afeb46d5739294da9ae3f4492ce2cd");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Idle_02 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Idle_02.prefab:b01df7a9456a5654abdef992e359290b");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Idle_03 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Idle_03.prefab:83d2359ec32509d4c883ff0d50434c8a");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Intro_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Intro_01.prefab:e2a046ae0d82015438e85d7016cde65d");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01.prefab:d40dfbefe3e3d2d4bae000f7cd864b9a");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_IntroTekahn_01.prefab:81dbb4f41b35f0641b9dabffbb5f70ef");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01.prefab:2d9dafe828fc4dd418d02cf4d9db7153");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01.prefab:825cfd89a17aa3d47aff0b7f0ed426f9");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01.prefab:8809b2ce463f6424ca925e951521be9d");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01.prefab:68a809f9fffa2e2429d43128d54e3da1");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01.prefab:c8bb0bd9761ce084dbc5b3738f95270c");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01.prefab:8636b6c4366dff74482da32a49834328");

	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01.prefab:d3465a93e9e406b4e94e88d83440f89d");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_62h_Male_Tauren_Idle_01, VO_DALA_BOSS_62h_Male_Tauren_Idle_02, VO_DALA_BOSS_62h_Male_Tauren_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_BossStealMinion = new List<string> { VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01, VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01, VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01, VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01, VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02, VO_DALA_BOSS_62h_Male_Tauren_Death_01, VO_DALA_BOSS_62h_Male_Tauren_DefeatPlayer_01, VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03,
			VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05, VO_DALA_BOSS_62h_Male_Tauren_Idle_01, VO_DALA_BOSS_62h_Male_Tauren_Idle_02, VO_DALA_BOSS_62h_Male_Tauren_Idle_03, VO_DALA_BOSS_62h_Male_Tauren_Intro_01, VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01, VO_DALA_BOSS_62h_Male_Tauren_IntroTekahn_01, VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01, VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01,
			VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01, VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01, VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01, VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01, VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_62h_Male_Tauren_Intro_01;
		m_deathLine = VO_DALA_BOSS_62h_Male_Tauren_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04, VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Tekahn")
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "OG_104":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01);
				break;
			case "ICC_314":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01);
				break;
			case "GIL_685":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01);
				break;
			case "OG_118":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01);
				break;
			case "EX1_625":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01);
				break;
			case "LOOT_526":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01);
				break;
			case "EX1_032":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01);
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
			case "GIL_124":
			case "OG_142":
			case "OG_337":
			case "TRL_408":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01);
				break;
			case "CFM_603":
			case "CS1_113":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossStealMinion);
				break;
			}
		}
	}
}
