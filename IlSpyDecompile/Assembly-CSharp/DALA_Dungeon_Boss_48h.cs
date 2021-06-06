using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_48h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01.prefab:540608faa9951d0438c1072432f3101f");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01.prefab:8b729ca0f3d81fa41ac740067e0b6a79");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01.prefab:32e4f1e234cf2e14aa46a0bff14cec79");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01.prefab:0f426d7c9d114e54baf7a93ba04128de");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02.prefab:ab8366f4da6552046b2cb39c1c973c75");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03.prefab:72e216b521811f4468a80469404271d5");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Death_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Death_01.prefab:e31e7fc2bff9d0340aca7a9b7bbb6cdf");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_DefeatPlayer_01.prefab:95da793bc82ba6d4ab851daf7fed9aa4");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01.prefab:bd98f103e89e3f848aa078e4e470d2fd");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_01.prefab:2746848f84ede83439c4e33a9949e72c");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_02.prefab:646b1065715e34447a0588b9e4f478cc");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_03 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_03.prefab:6a540e539e081644c822b83072ee633c");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_04 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_04.prefab:0fd7034e9bff5d04dbeb9c19db4d6d92");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPower_05 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPower_05.prefab:fcd54861bab56ad449bd23c4b7871eb0");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01.prefab:d83636839190ef44d91b1c3f82768340");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02.prefab:c3841504c31aec94a9282ce005fb9b8b");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Idle_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Idle_01.prefab:bdba1379ecac54746b11124e07f916cd");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Idle_02 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Idle_02.prefab:eba030fd7f2f91a40b78e1befa150837");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Idle_03 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Idle_03.prefab:63ac32bf12427b94695650e5db376401");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_Intro_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_Intro_01.prefab:1a003f3c97d47ea4b927ef393a0b6f43");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01.prefab:e1199523d54583846a515a81b7bffef7");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01.prefab:35fd955540a09624d976ec2a7db8a603");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01.prefab:100ee9cac794a78488efe09158a0a45b");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01.prefab:4a336fab02ace6145bce4cde6641339a");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01.prefab:8db7671dc135c8f4c9aeecb029f70000");

	private static readonly AssetReference VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01 = new AssetReference("VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01.prefab:a134616a98fd3f94998ce2b53fa74396");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_48h_Male_Demon_Idle_01, VO_DALA_BOSS_48h_Male_Demon_Idle_02, VO_DALA_BOSS_48h_Male_Demon_Idle_03 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_48h_Male_Demon_HeroPower_01, VO_DALA_BOSS_48h_Male_Demon_HeroPower_02, VO_DALA_BOSS_48h_Male_Demon_HeroPower_03, VO_DALA_BOSS_48h_Male_Demon_HeroPower_04, VO_DALA_BOSS_48h_Male_Demon_HeroPower_05 };

	private static List<string> m_HeroPowerBig = new List<string> { VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01, VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02 };

	private static List<string> m_BossVoidShift = new List<string> { VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01, VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02, VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01, VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01, VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01, VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_01, VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_02, VO_DALA_BOSS_48h_Male_Demon_BossVoidShift_03, VO_DALA_BOSS_48h_Male_Demon_Death_01, VO_DALA_BOSS_48h_Male_Demon_DefeatPlayer_01, VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01, VO_DALA_BOSS_48h_Male_Demon_HeroPower_01,
			VO_DALA_BOSS_48h_Male_Demon_HeroPower_02, VO_DALA_BOSS_48h_Male_Demon_HeroPower_03, VO_DALA_BOSS_48h_Male_Demon_HeroPower_04, VO_DALA_BOSS_48h_Male_Demon_HeroPower_05, VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_01, VO_DALA_BOSS_48h_Male_Demon_HeroPowerBig_02, VO_DALA_BOSS_48h_Male_Demon_Idle_01, VO_DALA_BOSS_48h_Male_Demon_Idle_02, VO_DALA_BOSS_48h_Male_Demon_Idle_03, VO_DALA_BOSS_48h_Male_Demon_Intro_01,
			VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01, VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01, VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01, VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01, VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01, VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01
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
		m_introLine = VO_DALA_BOSS_48h_Male_Demon_Intro_01;
		m_deathLine = VO_DALA_BOSS_48h_Male_Demon_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_48h_Male_Demon_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_48h_Male_Demon_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_48h_Male_Demon_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish")
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_48h_Male_Demon_BossBigDemon_01);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerBig);
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
			case "OG_118":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_48h_Male_Demon_PlayerRenounceDarkness_01);
				break;
			case "EX1_312":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_48h_Male_Demon_PlayerTwistingNether_01);
				break;
			case "LOOT_368":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_48h_Male_Demon_PlayerVoidLord_01);
				break;
			case "DALA_BOSS_48t":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_48h_Male_Demon_PlayerVoidShift_01);
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
			switch (cardId)
			{
			case "EX1_181":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_48h_Male_Demon_BossCalloftheVoid_01);
				break;
			case "FP1_022":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_48h_Male_Demon_BossVoidcaller_01);
				break;
			case "DALA_BOSS_48t":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossVoidShift);
				break;
			}
		}
	}
}
