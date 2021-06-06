using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_10h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01.prefab:cdcb64b82cfe35e4a8c58b6dc785472c");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01.prefab:efc3fe713f5dab1469dc8f824bf8a1d5");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Death_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Death_01.prefab:16293dd1aed2d2144bf41bae0a8d041b");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_DefeatPlayer_01.prefab:8dcf1dfbddb0a2f47b911882aabe7d14");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01.prefab:3e0b5bc9484bdad4ca2d17790882871f");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01.prefab:73802bf2bf77cb1468923fdf87dcfe79");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02.prefab:a20f211350f961845912d9521b06b8ce");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03.prefab:5de59bf0bd060a543b753f9882acb5f3");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04.prefab:ac6af50bd75b88c4cb53bdb3927551e7");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05.prefab:28c58ee87c5cae84c9c8d97f9685babf");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Idle_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Idle_01.prefab:f660bf4d2ac975348b161b0b64bc7fef");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Idle_02 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Idle_02.prefab:5a770e18424209e428b3e4f942daa9bd");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Idle_04 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Idle_04.prefab:c8c1bcd6f607d2f41a18f52e808e7c33");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_Intro_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_Intro_01.prefab:98ab83ba93e213f429b2d5fcd8dbc174");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_IntroSqueamlish_01.prefab:04e7f88bca0568b42bc978eeb265f2bc");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01.prefab:57ed1b7fa1b145146af66216263d2c9d");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01.prefab:484bad869ef760648bba09c0cf6d48de");

	private static readonly AssetReference VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01 = new AssetReference("VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01.prefab:6c7497e1b64e59945a96dcda503382ed");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_10h_Male_Kobold_Idle_01, VO_DALA_BOSS_10h_Male_Kobold_Idle_02, VO_DALA_BOSS_10h_Male_Kobold_Idle_04 };

	private static List<string> m_HeroPowerLines = new List<string> { VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01, VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01, VO_DALA_BOSS_10h_Male_Kobold_Death_01, VO_DALA_BOSS_10h_Male_Kobold_DefeatPlayer_01, VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_01, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_02, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_03, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_04, VO_DALA_BOSS_10h_Male_Kobold_HeroPowerTrigger_05,
			VO_DALA_BOSS_10h_Male_Kobold_Idle_01, VO_DALA_BOSS_10h_Male_Kobold_Idle_02, VO_DALA_BOSS_10h_Male_Kobold_Idle_04, VO_DALA_BOSS_10h_Male_Kobold_Intro_01, VO_DALA_BOSS_10h_Male_Kobold_IntroSqueamlish_01, VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01, VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01, VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01
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
		m_introLine = VO_DALA_BOSS_10h_Male_Kobold_Intro_01;
		m_deathLine = VO_DALA_BOSS_10h_Male_Kobold_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_10h_Male_Kobold_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 101)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Squeamlish" && cardId != "DALA_Eudora" && cardId != "DALA_Rakanishu" && cardId != "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
			case "DAL_771":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_10h_Male_Kobold_PlayerSoldierofFortune_01);
				break;
			case "LOOT_541":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_10h_Male_Kobold_PlayerKingTogwaggle_01);
				break;
			case "CS2_142":
			case "DAL_614":
			case "LOOT_347":
			case "LOOT_382":
			case "LOOT_389":
			case "LOOT_531":
			case "OG_082":
			case "TOT_033":
			case "LOOT_412":
			case "LOOT_062":
			case "LOOT_014":
			case "LOOT_041":
			case "LOOT_042":
			case "LOOT_367":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_10h_Male_Kobold_PlayerKobold_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "AT_031"))
		{
			if (cardId == "CFM_669")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_10h_Male_Kobold_BossBurglyBully_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_10h_Male_Kobold_BossCutpurse_01);
		}
	}
}
