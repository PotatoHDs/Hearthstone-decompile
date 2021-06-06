using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_26h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01.prefab:3d5a271fdd5626142a9881567ce6ad94");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01.prefab:43be4359ee4bc454d83bb067c67e0ea4");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01.prefab:6d892f81258e93f4badfa482a0665765");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_DefeatPlayer_01.prefab:fedcf1cb64a6a3946b33e0a55a21a0ee");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01.prefab:1116f04f4c037884ca39889f23525075");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01.prefab:8ab9b9f60d05b24409a10c00de2a070e");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03.prefab:8fd09491add94f348aab77c79bcde414");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04.prefab:ca9ad22dc7628d64f8c1fd4c026ed67c");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05.prefab:58693bb4d55776d418c6c5c7914ea8ea");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01.prefab:d02ab5b480f82f942aa531f4ddce0793");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02.prefab:62cb5b2f5510d6045afcabfc3535613d");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03.prefab:0d6dca33078df13489fdb7664abeac72");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01.prefab:9996636d831af5a479cfd29816b7dd4c");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01.prefab:1ea6a18afc9e9b4499aaa98e23c793f2");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01.prefab:e5bc7c740bc4fab44b3a15c0426a765d");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGatlingWandTreasure_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGatlingWandTreasure_01.prefab:9538562cf71a9564b8f05a14eaac5d8e");

	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01.prefab:4ca5ab7a76339ac4b876e1eebb5ded0e");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01, VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02, VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01, VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01, VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01, VO_ULDA_BOSS_26h_Female_Mechagnome_DefeatPlayer_01, VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04, VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05, VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01,
			VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02, VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03, VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01, VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01, VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01, VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGatlingWandTreasure_01, VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01;
		m_deathLine = VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "GVG_119"))
		{
			if (cardId == "ULDA_115")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01);
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
		if (!(cardId == "GVG_082"))
		{
			if (cardId == "ULDA_207")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01);
		}
	}
}
