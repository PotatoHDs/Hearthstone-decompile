using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_27h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01.prefab:a0e92a26ed675e64e8ba8bd8496808b8");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01.prefab:08b367b7748041741983b215559e352f");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01.prefab:e26267b4645fd38458eedd88e7b8b492");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Death_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Death_01.prefab:4d4e776a143b560458db00eaf822a775");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_DefeatPlayer_01.prefab:59d4e06534fcb1a4cad81d8ca7a891d2");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01.prefab:16976ba8280b2d94e98ad8bdc8407f2d");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01.prefab:97b1ad0e6f060fc4587c006b00ee462a");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02.prefab:db88a8eed1eaf444f8035ef5f3543d32");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03.prefab:2dec94d9616c3b74c82891a3f82b2d5d");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04.prefab:c15f4f63560cb24409fd03673e94b673");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05.prefab:9367762c641a5144087506c659b2b2bb");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Idle_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Idle_01.prefab:bea1a957007cb064bb71fe6dcb6a49d2");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Idle_02 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Idle_02.prefab:a5be605353dbde6419d49d9f0cbeb77f");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Idle_03 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Idle_03.prefab:c206e7eb72e943f47bbf15cfd538e5ad");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Intro_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Intro_01.prefab:13f3e889f97e3db4488c6b274d2d3707");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01.prefab:1e9b7ee341f08bf4380638d01dc1e3ba");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01.prefab:e553865a95475d14f90aac0bb6a27177");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01.prefab:e29144619d9e37945b9f7efb5d8b852e");

	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_TurnOne_01.prefab:2a2cc3f2a11909d4797aa4f3f54aee6f");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_27h_Male_Troll_Idle_01, VO_ULDA_BOSS_27h_Male_Troll_Idle_02, VO_ULDA_BOSS_27h_Male_Troll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01, VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01, VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01, VO_ULDA_BOSS_27h_Male_Troll_Death_01, VO_ULDA_BOSS_27h_Male_Troll_DefeatPlayer_01, VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03, VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04,
			VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05, VO_ULDA_BOSS_27h_Male_Troll_Idle_01, VO_ULDA_BOSS_27h_Male_Troll_Idle_02, VO_ULDA_BOSS_27h_Male_Troll_Idle_03, VO_ULDA_BOSS_27h_Male_Troll_Intro_01, VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01, VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01, VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01, VO_ULDA_BOSS_27h_Male_Troll_TurnOne_01
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
		m_introLine = VO_ULDA_BOSS_27h_Male_Troll_Intro_01;
		m_deathLine = VO_ULDA_BOSS_27h_Male_Troll_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01);
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ULD_280"))
		{
			if (cardId == "ULD_003")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01);
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
			if (cardId == "ULD_328")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01);
			}
		}
	}
}
