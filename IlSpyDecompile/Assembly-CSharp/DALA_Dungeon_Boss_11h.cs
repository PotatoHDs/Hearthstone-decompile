using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_11h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Death_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Death_01.prefab:91c27ea464e733a479831e55f0e10d94");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_DefeatPlayer_02.prefab:b0adb823eb8cc7c408c7f3a651dcf44e");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01.prefab:3b9e1c829fbdb1d4db2c267db716ad46");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_01.prefab:45a2f62281c4c7d44b9aaa7b758de255");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_02.prefab:9a2eb335a1e90164eb829c4aec0c8949");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_03 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_03.prefab:2596c8a227f61584a9be27aeccc264d9");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_04 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_04.prefab:ad800a6f15ebec2489e757d69d627133");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Idle_05 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Idle_05.prefab:607e7297eb0cf7d40ae1ff028019f0c6");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_Intro_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_Intro_01.prefab:be53259682365ee42a19fde978a89ed4");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_IntroEudora_01.prefab:d5d95e5fda7479144b839b4678467a62");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01.prefab:3c2e3913a836c594686eea862f63ee89");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_IntroSqueamlish_01.prefab:e74828d8ab059744596941c49237bcf8");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01.prefab:815aa996010aa3e4c93d500f799a791c");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02.prefab:e254e19b1dd157b48a36351baa4c38c7");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03.prefab:b8b6471a4898b3c4082a54382e059f2b");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01.prefab:7854bb59dfa41df4c855705af84062aa");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01.prefab:ab01ceedf80d1e34291a19662d0dc15a");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01.prefab:7d9e124b25400b54bb0453326c9e758b");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02.prefab:cba80c7914deb5547b3ba75277ddde21");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04.prefab:c10e67261473f4e4a873960abc4aa224");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01.prefab:406b5b0fbb732ef4c859f6308a86150b");

	private static readonly AssetReference VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01 = new AssetReference("VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01.prefab:9121f92817c98fe4a819d63eb039937e");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_11h_Male_Kobold_Idle_01, VO_DALA_BOSS_11h_Male_Kobold_Idle_02, VO_DALA_BOSS_11h_Male_Kobold_Idle_03, VO_DALA_BOSS_11h_Male_Kobold_Idle_04, VO_DALA_BOSS_11h_Male_Kobold_Idle_05 };

	private static List<string> m_PlayerMinionDies = new List<string> { VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02, VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04 };

	private static List<string> m_PlayerAOE = new List<string> { VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02, VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03 };

	private static List<string> m_PlayerPirate = new List<string> { VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_11h_Male_Kobold_Death_01, VO_DALA_BOSS_11h_Male_Kobold_DefeatPlayer_02, VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01, VO_DALA_BOSS_11h_Male_Kobold_Idle_01, VO_DALA_BOSS_11h_Male_Kobold_Idle_02, VO_DALA_BOSS_11h_Male_Kobold_Idle_03, VO_DALA_BOSS_11h_Male_Kobold_Idle_04, VO_DALA_BOSS_11h_Male_Kobold_Idle_05, VO_DALA_BOSS_11h_Male_Kobold_Intro_01, VO_DALA_BOSS_11h_Male_Kobold_IntroEudora_01,
			VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01, VO_DALA_BOSS_11h_Male_Kobold_IntroSqueamlish_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_02, VO_DALA_BOSS_11h_Male_Kobold_PlayerAOE_03, VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerKobold_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_02, VO_DALA_BOSS_11h_Male_Kobold_PlayerMinionDies_04,
			VO_DALA_BOSS_11h_Male_Kobold_PlayerPirate_01, VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01
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
		m_introLine = VO_DALA_BOSS_11h_Male_Kobold_Intro_01;
		m_deathLine = VO_DALA_BOSS_11h_Male_Kobold_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_11h_Male_Kobold_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_11h_Male_Kobold_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Eudora" && cardId != "DALA_Squeamlish")
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerMinionDies);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerAOE);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerPirate);
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
		if (!(cardId == "LOOT_541"))
		{
			if (cardId == "NEW1_004")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_11h_Male_Kobold_PlayerVanish_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_11h_Male_Kobold_PlayerKingTogwaggle_01);
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
