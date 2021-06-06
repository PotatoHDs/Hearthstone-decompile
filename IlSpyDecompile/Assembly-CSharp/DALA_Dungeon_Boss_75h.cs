using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_75h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Death_01.prefab:5719452f3c2d7bd4da146446835cd4f4");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_DefeatPlayer_01.prefab:35ff8e0d084bc784e9daa822eeaac10e");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01.prefab:6bb27181428e1b148b39d58d33468e9c");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Idle_01.prefab:d60e88c764584a4408d52a2f184753f0");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Idle_02.prefab:537fd87c6991a694b94b07d96c5b6752");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Idle_03.prefab:336c5699a9cd64f4d988e753fdf9ded7");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Intro_01.prefab:0c090bb43d823e04392ab3885909c11e");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroChu_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroChu_01.prefab:9b198f36d4a787748bcb0cef5159981e");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroEudora_01.prefab:accb4417721d47a4087f4eca36f280bc");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01.prefab:aaa240338c0f8eb4cb6d264b5016da0a");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01.prefab:638cbe32be800d94394ff04a6e2e6a1c");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01.prefab:20504d0dbbc0f6942b568bcdaf1e9acf");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01.prefab:456f2d610c003d44585f063decddca7f");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01.prefab:545e847ffd649274c88d0babffc19486");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01.prefab:9cb7974f75507fd4e9b5cb00d5e5f52b");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroVessina_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroVessina_01.prefab:8425ab15488fcd244a42e191c8bdb2b7");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Outro_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Outro_01.prefab:6056ab8f83c7919499e8a47eabf0020e");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Outro_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Outro_02.prefab:b07cae75e6605094cb0f77453e49120c");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestA_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestA_01.prefab:b7b4c811b8cc77547994b22668d3dce1");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestA_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestA_02.prefab:d2f593dfd5c4a284086fc045ae9443ba");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestA_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestA_03.prefab:023b7a0d6fb73024496632317d626713");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestB_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestB_01.prefab:757a724cc13ac484689f50590075c45d");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestB_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestB_02.prefab:a0406fc414d662740b6043f4cc1780ab");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestB_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestB_03.prefab:9910dba6afb66bd489ac065792b8d3a6");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestC_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestC_01.prefab:9196247b7aa967a4d8a2919645458c10");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestC_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestC_02.prefab:6838bebba6e7f9940afabbc3c53bc758");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestC_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestC_03.prefab:91bde431a5340df4591ae52a7fd88d8b");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestCFail_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestCFail_01.prefab:94f2ead6d267cd54580569b870a714e9");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_01.prefab:8d8937377f62e894c9d5d1a798b32ab6");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_02.prefab:54f09f467b692084b9acc6978c2300db");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_03.prefab:5a6607d7f67cc4a4c8327a4dce91d1ca");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_04 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_04.prefab:b60b417679da7a84ab9988c14be6a776");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestE_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestE_01.prefab:efe9dc47ca872cf40bd94f688e925103");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestE_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestE_02.prefab:0c8f85429f2a648489f6f7d5f1f08777");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestE_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestE_03.prefab:e32b7bb05b7b55949a935d85767e9e58");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_TurnOne_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_TurnOne_01.prefab:c91c87bc6f91a504d81980729a7e6b56");

	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_TurnOne_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_TurnOne_02.prefab:3c1abd2c1234e234c9104c1985f30b6e");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_75h_Male_Human_Idle_01, VO_DALA_BOSS_75h_Male_Human_Idle_02, VO_DALA_BOSS_75h_Male_Human_Idle_03 };

	private static List<string> m_BossOutro = new List<string> { VO_DALA_BOSS_75h_Male_Human_QuestB_03, VO_DALA_BOSS_75h_Male_Human_Outro_01, VO_DALA_BOSS_75h_Male_Human_Outro_02 };

	private static List<string> m_BossTurnOne = new List<string> { VO_DALA_BOSS_75h_Male_Human_TurnOne_01, VO_DALA_BOSS_75h_Male_Human_TurnOne_02 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_75h_Male_Human_Death_01, VO_DALA_BOSS_75h_Male_Human_DefeatPlayer_01, VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01, VO_DALA_BOSS_75h_Male_Human_Idle_01, VO_DALA_BOSS_75h_Male_Human_Idle_02, VO_DALA_BOSS_75h_Male_Human_Idle_03, VO_DALA_BOSS_75h_Male_Human_Intro_01, VO_DALA_BOSS_75h_Male_Human_IntroChu_01, VO_DALA_BOSS_75h_Male_Human_IntroEudora_01, VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01,
			VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01, VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01, VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01, VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01, VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01, VO_DALA_BOSS_75h_Male_Human_IntroVessina_01, VO_DALA_BOSS_75h_Male_Human_Outro_01, VO_DALA_BOSS_75h_Male_Human_Outro_02, VO_DALA_BOSS_75h_Male_Human_QuestA_01, VO_DALA_BOSS_75h_Male_Human_QuestA_02,
			VO_DALA_BOSS_75h_Male_Human_QuestA_03, VO_DALA_BOSS_75h_Male_Human_QuestB_01, VO_DALA_BOSS_75h_Male_Human_QuestB_02, VO_DALA_BOSS_75h_Male_Human_QuestB_03, VO_DALA_BOSS_75h_Male_Human_QuestC_01, VO_DALA_BOSS_75h_Male_Human_QuestC_02, VO_DALA_BOSS_75h_Male_Human_QuestC_03, VO_DALA_BOSS_75h_Male_Human_QuestCFail_01, VO_DALA_BOSS_75h_Male_Human_QuestD_01, VO_DALA_BOSS_75h_Male_Human_QuestD_02,
			VO_DALA_BOSS_75h_Male_Human_QuestD_03, VO_DALA_BOSS_75h_Male_Human_QuestD_04, VO_DALA_BOSS_75h_Male_Human_QuestE_01, VO_DALA_BOSS_75h_Male_Human_QuestE_02, VO_DALA_BOSS_75h_Male_Human_QuestE_03, VO_DALA_BOSS_75h_Male_Human_TurnOne_01, VO_DALA_BOSS_75h_Male_Human_TurnOne_02
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
		m_introLine = VO_DALA_BOSS_75h_Male_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_75h_Male_Human_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
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
			case "DALA_Chu":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Eudora":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_George":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Kriziki":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Barkeye":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Rakanishu":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Squemlish":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Tekahn":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "DALA_Vessina":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_75h_Male_Human_IntroVessina_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestA_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestA_02);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestA_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestB_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestB_02);
			break;
		case 106:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestB_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestC_01);
			break;
		case 108:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestC_02);
			break;
		case 109:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestC_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 110:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestCFail_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 111:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestD_01);
			break;
		case 112:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestD_02);
			break;
		case 113:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestD_03);
			break;
		case 114:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestD_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 115:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestE_01);
			break;
		case 116:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestE_02);
			break;
		case 117:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_75h_Male_Human_QuestE_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 118:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossTurnOne);
			GameState.Get().SetBusy(busy: false);
			break;
		case 120:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossOutro);
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
		}
	}
}
