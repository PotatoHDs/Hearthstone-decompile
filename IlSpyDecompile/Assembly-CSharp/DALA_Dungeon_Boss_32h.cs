using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_32h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01.prefab:828757ed7ab447a438e69cb9ef811c47");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Death_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Death_02.prefab:96afa188faa4aaa4ba5a2c009f31bedf");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_DefeatPlayer_01.prefab:ff7d5b7b50f0eba43a0a5b5f3725527f");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01.prefab:a4100c545b9a0a848b1c915c2680cd38");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01.prefab:8688fa21c89f08145b0e70ca27ba8dec");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02.prefab:672e74d253d03f44c959d2979d9d28ed");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03.prefab:e658e6c6866676a46bd152c0c3918d7a");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04.prefab:0a7205994ddbb2347a4359a8ce94ee9f");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01.prefab:7638e2f3bf88dbb428f13952512791b1");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02.prefab:04f99f14fb9e148458654cf03b07c5b6");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01.prefab:e53663687771726459b9041a68685244");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02.prefab:9e3f2d87cc729f24b9935303a9fd1415");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01.prefab:d0a9c3f2ea1c5af4ba71cc17585a8683");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02.prefab:8b703e12d0c771444906f6d38426f982");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01.prefab:6ab7d7a10d5bf6d4db3947456896ccea");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02.prefab:7c26b30be2fafe24da261fce511a06ab");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01.prefab:0c97196de5768d04c97700790cecaf01");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Idle_01.prefab:9b93b558194d5dd408af722e7e87a345");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Idle_02.prefab:e94aa27e2e842e142a52bfcd7174ba96");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Idle_03.prefab:ce78c7593e7482845862a8976178ab5a");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Intro_01.prefab:055e362bca70b384d92a2cd9194f7ef6");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01.prefab:fe8c572e3d46bd54e9e11f23928aa894");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01.prefab:64c5f421192531244b6b49cfbe79ab1f");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Misc_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Misc_01.prefab:7c2f969c11847e64abd89b4dd0cb1f30");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01.prefab:c0084f200ca37e049b0555f84e7fcfe3");

	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01.prefab:c33ee0b3d8457104692cd17c3ba0ab13");

	private static List<string> m_HeroPowerFade = new List<string> { VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02 };

	private static List<string> m_HeroPowerMohawk = new List<string> { VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02 };

	private static List<string> m_HeroPowerPompadour = new List<string> { VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02 };

	private static List<string> m_HeroPowerQuiff = new List<string> { VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_32h_Female_Goblin_Idle_01, VO_DALA_BOSS_32h_Female_Goblin_Idle_02, VO_DALA_BOSS_32h_Female_Goblin_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01, VO_DALA_BOSS_32h_Female_Goblin_Death_02, VO_DALA_BOSS_32h_Female_Goblin_DefeatPlayer_01, VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02,
			VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01, VO_DALA_BOSS_32h_Female_Goblin_Idle_01, VO_DALA_BOSS_32h_Female_Goblin_Idle_02, VO_DALA_BOSS_32h_Female_Goblin_Idle_03,
			VO_DALA_BOSS_32h_Female_Goblin_Intro_01, VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01, VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01, VO_DALA_BOSS_32h_Female_Goblin_Misc_01, VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01, VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01
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
		m_introLine = VO_DALA_BOSS_32h_Female_Goblin_Intro_01;
		m_deathLine = VO_DALA_BOSS_32h_Female_Goblin_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03, VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04 };
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
			if (cardId == "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_32h_Female_Goblin_Misc_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerFade);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerMohawk);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerPompadour);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerQuiff);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "ICC_064"))
		{
			if (cardId == "CS2_077")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01);
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
			if (cardId == "GVG_023")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01);
			}
		}
	}
}
