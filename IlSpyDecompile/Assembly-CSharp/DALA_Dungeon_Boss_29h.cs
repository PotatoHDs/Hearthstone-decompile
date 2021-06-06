using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_29h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Death_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Death_01.prefab:94c9f11b845b3ab4780730bb735f7caa");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_DefeatPlayer_01.prefab:4e5ff81e78229794d990ca9042e6a589");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01.prefab:4582b3e9dcdffe34b8421018884d6f74");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01.prefab:9e35fc7f8fc6f0b4a9721971b787465a");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02.prefab:c026f7d12961ddf469a8dfc368f6381f");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03.prefab:ac262729856029d4981645428564161c");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04.prefab:4b2a11130918cc440b4fb06edd7ca083");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01.prefab:3596b6ff6a67ebf41a92c35036fde4ba");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02.prefab:6f026439664bf2a459d3c8f9f99ea78c");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Idle_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Idle_01.prefab:16ae9ef60444a1241acde3d6ea81099c");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Idle_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Idle_02.prefab:f826221137cae434984b543e9ac5506b");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Idle_03 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Idle_03.prefab:414bbf0c5737b434fbad16e8167ef250");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_Intro_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_Intro_01.prefab:5f689cf34bb792340bc7903e34ef1ba9");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01.prefab:4d981f3d19de5284699c385313e5e347");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01.prefab:ba36cc9555cfd204db5ec7d13ec4b42b");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01.prefab:d545a3a4c99a17d4dbcfe0a396405c97");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01.prefab:20580efab6735ff498ab1f7b69c2d421");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01.prefab:ca8980c823164bc44bf1cd28bafd7f20");

	private static readonly AssetReference VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02 = new AssetReference("VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02.prefab:ce6f05aa9e4b90241a5a70a5df9505e5");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_29h_Female_Dwarf_Idle_01, VO_DALA_BOSS_29h_Female_Dwarf_Idle_02, VO_DALA_BOSS_29h_Female_Dwarf_Idle_03 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04 };

	private static List<string> m_HeroPowerRock = new List<string> { VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01, VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02 };

	private static List<string> m_PlayerSmallElemental = new List<string> { VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01, VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_29h_Female_Dwarf_Death_01, VO_DALA_BOSS_29h_Female_Dwarf_DefeatPlayer_01, VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_01, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_02, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_03, VO_DALA_BOSS_29h_Female_Dwarf_HeroPower_04, VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_01, VO_DALA_BOSS_29h_Female_Dwarf_HeroPowerRock_02, VO_DALA_BOSS_29h_Female_Dwarf_Idle_01,
			VO_DALA_BOSS_29h_Female_Dwarf_Idle_02, VO_DALA_BOSS_29h_Female_Dwarf_Idle_03, VO_DALA_BOSS_29h_Female_Dwarf_Intro_01, VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01, VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01, VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01, VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01, VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_01, VO_DALA_BOSS_29h_Female_Dwarf_PlayerSmallElemental_02
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
		m_introLine = VO_DALA_BOSS_29h_Female_Dwarf_Intro_01;
		m_deathLine = VO_DALA_BOSS_29h_Female_Dwarf_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_29h_Female_Dwarf_EmoteResponse_01;
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
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_29h_Female_Dwarf_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_29h_Female_Dwarf_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerRock);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerSmallElemental);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_29h_Female_Dwarf_PlayerElemental_01);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			if (cardId == "LOOTA_835")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_29h_Female_Dwarf_PlayerPickaxe_01);
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
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
}
