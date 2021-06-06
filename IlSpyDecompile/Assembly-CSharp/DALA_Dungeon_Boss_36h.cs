using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_36h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02.prefab:ba3fe70ad6249b54d888062bd52ed302");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03.prefab:3cfd1a53963a591409e33c8b84c37d15");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Death_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Death_01.prefab:ab4af967e35860b46b347facdc1e5383");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_DefeatPlayer_01.prefab:f2ed950056a666c4cbfb4bb5bf94655c");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01.prefab:926422888dfd13f48b1213ea18ee5801");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01.prefab:95eb4c4cacf9d9c4a8c644820ed244c2");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02.prefab:ddfa2c324fdc3a64eb919a25395b74cd");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03.prefab:8f8469192fdc0214bb42ae50200edff0");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04.prefab:f3b703916bb731a498cdf3358074a8fa");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05.prefab:8740c8361ac74d7419173a88384535cb");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06.prefab:19b5762e9cea0fd41a0109f73d510cf8");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Idle_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Idle_01.prefab:f0801adc980a4fc47ac6b84c889ad2c6");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Idle_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Idle_02.prefab:50448994fab7409419ad17dd5c601dce");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Idle_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Idle_03.prefab:036f8783eb6537c45ad6eaae2264fb9a");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Intro_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Intro_01.prefab:dfb744fdbf73d124dacb5779514f0a22");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01.prefab:99a90329256d8c9479af79d0c03f5915");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01.prefab:a77b9142eab443d4ba6e2d714b01c3ff");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01.prefab:4c8d0d11d5b89db468a0d74d659e8f95");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01.prefab:008b595d390cde04ab817bd4fbf555ad");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Misc_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Misc_01.prefab:bd476ea3e50613d4b900150d74b13f38");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01.prefab:6981b902fcab85c42a0ac7c089160c32");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02.prefab:0008ebc950ad2814abb328bba0b94383");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03.prefab:14d2f1246194b3347883e2c5293d7dd2");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01.prefab:199e6c7bf11f1eb4b8bb187cb261c5cd");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01.prefab:3cc67530d93cc3f4b8b96a00803441b1");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02.prefab:e97c1df1cd5bac5459646311b2603d8b");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01.prefab:60ae5b903f68ffa4a885055bb2a7e960");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01.prefab:352c3703e2428df4596948d5ff9cb142");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02.prefab:81d67454d7b508d469b95d731cacd401");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01.prefab:de8533db12fb958489a24b7e17113462");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02.prefab:eba045b47e60a434086c1285cf9e890a");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01.prefab:55e45142174377945b37e922591e1534");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02.prefab:a26a8e004b300724885132d1123e4b31");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04.prefab:01cb43de3a7012f409e923542311b376");

	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01.prefab:b453c7fa9eacf8745a5ceb10a0c571f8");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_Idle_01, VO_DALA_BOSS_36h_Male_Dwarf_Idle_02, VO_DALA_BOSS_36h_Male_Dwarf_Idle_03 };

	private static List<string> m_BossBeast = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02, VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03 };

	private static List<string> m_PlayerBeast = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03 };

	private static List<string> m_PlayerBigBeast = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02 };

	private static List<string> m_PlayerSmallBeast = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02 };

	private static List<string> m_PlayerSSS = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02 };

	private static List<string> m_PlayerZombeast = new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02, VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03, VO_DALA_BOSS_36h_Male_Dwarf_Death_01, VO_DALA_BOSS_36h_Male_Dwarf_DefeatPlayer_01, VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05,
			VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06, VO_DALA_BOSS_36h_Male_Dwarf_Idle_01, VO_DALA_BOSS_36h_Male_Dwarf_Idle_02, VO_DALA_BOSS_36h_Male_Dwarf_Idle_03, VO_DALA_BOSS_36h_Male_Dwarf_Intro_01, VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01, VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01, VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01, VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01, VO_DALA_BOSS_36h_Male_Dwarf_Misc_01,
			VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01,
			VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01, VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02, VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04, VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05, VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06 };
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_36h_Male_Dwarf_Intro_01;
		m_deathLine = VO_DALA_BOSS_36h_Male_Dwarf_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
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
			case "DALA_Eudora":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Kriziki":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Squeamlish":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Barkeye":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerSmallBeast);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerBeast);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerBigBeast);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerZombeast);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossBeast);
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01);
			break;
		case 108:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_36h_Male_Dwarf_Misc_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "NEW1_034"))
		{
			if (cardId == "DALA_704")
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerSSS);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01);
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
		}
	}
}
