using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_64h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01.prefab:b2a2a58d21a00cf4dab4d71a2f244492");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01.prefab:6a5496cc4ff8c504ab22bef0db913acc");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01.prefab:003303c1bb2f0da46887539f2cb966e1");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Death_01.prefab:a5c9bb6c02f5c6f45917b1f922f896a0");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_DefeatPlayer_01.prefab:646ce5edac52d0640b930fd3ea828943");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01.prefab:e48ccde61bd17914f998e285ce9cfa26");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Exposition_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Exposition_01.prefab:6995faf495156174689ab4f0d438641f");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Exposition_02 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Exposition_02.prefab:13057285d08166c4fa482e80427706e5");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_01.prefab:f15093db68319854a87c1ec81ee7d545");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_04 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_04.prefab:095c39082802db844b5e8a31d82391c2");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_05 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_05.prefab:10354dd7df73fa74bb9f9f13ef590079");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_06 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_06.prefab:8d3549d8cfc0904459793166752ad979");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_07 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_07.prefab:25aa90ac5b84d444493e5bb9266f3bd1");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_HeroPower_08 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_HeroPower_08.prefab:05fb49e541e6b2944a646864fcbc9526");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Idle_02.prefab:a62396e00d1a4a14fbccb7f6b12fd02d");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Idle_03.prefab:fb458f82444a32c43826f0ba39095357");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Idle_04 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Idle_04.prefab:123a0be21529dd949be25e52f6f0c30e");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_Intro_01.prefab:eaf0436940f1d5a4caf1458b31474545");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_IntroChu_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_IntroChu_01.prefab:0bb7f4c0bab9ef548a2c4fdd4d384286");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01.prefab:b948e4420698bfb459e7c35538d061f1");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01.prefab:f595a759222325f45bd13a25b7def66f");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01.prefab:9cb4494da4713604481a80d5a94810a4");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01.prefab:21844d3d1c16d154583a72e786263375");

	private static readonly AssetReference VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01 = new AssetReference("VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01.prefab:1484edf934d4d70458d9ff65f1257010");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_64h_Male_Orc_Idle_02, VO_DALA_BOSS_64h_Male_Orc_Idle_03, VO_DALA_BOSS_64h_Male_Orc_Idle_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01, VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01, VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01, VO_DALA_BOSS_64h_Male_Orc_Death_01, VO_DALA_BOSS_64h_Male_Orc_DefeatPlayer_01, VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01, VO_DALA_BOSS_64h_Male_Orc_Exposition_01, VO_DALA_BOSS_64h_Male_Orc_Exposition_02, VO_DALA_BOSS_64h_Male_Orc_HeroPower_01, VO_DALA_BOSS_64h_Male_Orc_HeroPower_04,
			VO_DALA_BOSS_64h_Male_Orc_HeroPower_05, VO_DALA_BOSS_64h_Male_Orc_HeroPower_06, VO_DALA_BOSS_64h_Male_Orc_HeroPower_07, VO_DALA_BOSS_64h_Male_Orc_HeroPower_08, VO_DALA_BOSS_64h_Male_Orc_Idle_02, VO_DALA_BOSS_64h_Male_Orc_Idle_03, VO_DALA_BOSS_64h_Male_Orc_Idle_04, VO_DALA_BOSS_64h_Male_Orc_Intro_01, VO_DALA_BOSS_64h_Male_Orc_IntroChu_01, VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01,
			VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01, VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01, VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01, VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01
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
		m_introLine = VO_DALA_BOSS_64h_Male_Orc_Intro_01;
		m_deathLine = VO_DALA_BOSS_64h_Male_Orc_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_64h_Male_Orc_EmoteResponse_01;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_64h_Male_Orc_HeroPower_01, VO_DALA_BOSS_64h_Male_Orc_HeroPower_04, VO_DALA_BOSS_64h_Male_Orc_HeroPower_05, VO_DALA_BOSS_64h_Male_Orc_HeroPower_06, VO_DALA_BOSS_64h_Male_Orc_HeroPower_07, VO_DALA_BOSS_64h_Male_Orc_HeroPower_08 };
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_64h_Male_Orc_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_64h_Male_Orc_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_64h_Male_Orc_Exposition_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_64h_Male_Orc_Exposition_02);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_64h_Male_Orc_BossBuffedMinionDies_01);
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
			case "AT_033":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_64h_Male_Orc_PlayerBurgle_01);
				break;
			case "GAME_005":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_64h_Male_Orc_PlayerCoin_01);
				break;
			case "DAL_088":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_64h_Male_Orc_PlayerSafeguard_01);
				break;
			case "DAL_771":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_64h_Male_Orc_PlayerSoldierofFortune_01);
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
		if (!(cardId == "DAL_088"))
		{
			if (cardId == "CFM_940")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_64h_Male_Orc_BossIKnowAGuy_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_64h_Male_Orc_BossSafeguard_01);
		}
	}
}
