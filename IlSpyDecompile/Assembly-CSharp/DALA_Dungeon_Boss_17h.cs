using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_17h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01.prefab:f816b776da34ae049bbd8c821c9cba18");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01.prefab:eedfb8ad0daaa6a4685e981ed9cf9b6b");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01.prefab:101d24d581760ac4fa3054168b69b413");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Death_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Death_01.prefab:84cf1ed3a4bf89e4eb349ba16ba89c12");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_DefeatPlayer_01.prefab:7706c0cbc2fb7894986e32412de2d0eb");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01.prefab:eeb81a6f93411df4686ccc4b2c4f4299");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01.prefab:eb0f6e31b2aaf0048b3d384fd6d533a1");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02.prefab:4b74d0dab742b0f438fbc428de581fad");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03.prefab:c8a67e4db1dc8fc4db5968b108f9efb2");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04.prefab:4aaa07f800ad2c742bb2c65c57833b6e");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Idle_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Idle_01.prefab:7713b0bee3e8c994190fab9e47c65368");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Idle_02 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Idle_02.prefab:47dd7a4bcf701b240ba537d51d9fceb1");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Idle_03 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Idle_03.prefab:27ccac1585bb7f84789946cf4f7a5a0b");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Intro_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Intro_01.prefab:f4b9aa6b5351e274ba3266e1d31f6990");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01.prefab:f7c7036e6164bab4abdbfc8e109b7aaf");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01.prefab:9c7936fca250f0940b21f8e9d2403a85");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01.prefab:51fdcd9e20919344a88a6d323fe418de");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01.prefab:0e457f95dacc78144a795b24e121b8dc");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01.prefab:99c85ed702fb28c41826195abfda5246");

	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01.prefab:2069dd3394216d7428d7c2d2121b8944");

	private List<string> m_BossHeroPowerTrigger = new List<string> { VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_17h_Female_Tauren_Idle_01, VO_DALA_BOSS_17h_Female_Tauren_Idle_02, VO_DALA_BOSS_17h_Female_Tauren_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01, VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01, VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01, VO_DALA_BOSS_17h_Female_Tauren_Death_01, VO_DALA_BOSS_17h_Female_Tauren_DefeatPlayer_01, VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03, VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04,
			VO_DALA_BOSS_17h_Female_Tauren_Idle_01, VO_DALA_BOSS_17h_Female_Tauren_Idle_02, VO_DALA_BOSS_17h_Female_Tauren_Idle_03, VO_DALA_BOSS_17h_Female_Tauren_Intro_01, VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01, VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01, VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01, VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01, VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01, VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01
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
		m_introLine = VO_DALA_BOSS_17h_Female_Tauren_Intro_01;
		m_deathLine = VO_DALA_BOSS_17h_Female_Tauren_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPowerTrigger);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01);
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
			case "DAL_732":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01);
				break;
			case "OG_044":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01);
				break;
			case "GIL_663t":
			case "FP1_019t":
			case "EX1_158t":
			case "DAL_256t2":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01);
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
			case "AT_042":
			case "EX1_165":
			case "BRM_010":
			case "GIL_188":
			case "GVG_080":
			case "ICC_051":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01);
				break;
			case "EX1_164":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01);
				break;
			case "TRL_343":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01);
				break;
			}
		}
	}
}
