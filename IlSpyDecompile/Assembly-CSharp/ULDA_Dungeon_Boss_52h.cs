using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_52h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01.prefab:159103d42df8b3b489c0e40cebbca7fc");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01.prefab:fbc9363a6cf9a3c46b15dd45d3874fa5");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01.prefab:eb728c7f61b17ad44a66b145fbf4fc1b");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01.prefab:1bd3a2230ec022841970069845dab07e");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01.prefab:de1fa7d300be606439a9668b59a167a8");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_DefeatPlayer_01.prefab:43e2f24e3185162459d0d113059f263b");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01.prefab:588679b16be5a654b8ea21ffb5d8b360");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01.prefab:e79b5719260d06a46a0a3c0e2552304c");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03.prefab:c7d1cc7d5df059f46813b1c0c1f1b28d");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05.prefab:8e1ae81702419cc498e8225e65bea36d");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01.prefab:9a22082301cb4a04abeaa9f5b902202e");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02.prefab:4486e499b77779e459d882ec683d7908");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03.prefab:af72c89b9d5ae21429ec804375d0cefc");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01.prefab:04ec01ffc764867469bc10aa1e86035e");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_IntroResponseReno_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_IntroResponseReno_01.prefab:a5da3c562d4e1e04b85298332f33834e");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01.prefab:be198bfd9ec5b38438b0d22f642441d7");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01.prefab:bb36b791d9954b8479ca6482a2273c07");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01.prefab:5690869bd409deb4cb006b55ef7dd03b");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01.prefab:c6515a9d41705494891a103ab2a27bed");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01.prefab:34191563ddc4a664982a1e96e4357c44");

	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01.prefab:2a6782f37af2f3444a2b81f9e43e697c");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01, VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03, VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01, VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02, VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01, VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01, VO_ULDA_BOSS_52h_Female_UndeadLich_DefeatPlayer_01, VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01, VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01, VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03, VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05,
			VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01, VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02, VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03, VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01, VO_ULDA_BOSS_52h_Female_UndeadLich_IntroResponseReno_01, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01,
			VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01
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
		m_introLine = VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01;
		m_deathLine = VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START && cardId != "ULDA_Reno")
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01);
			break;
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ICC_854":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01);
				break;
			case "ULD_282":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01);
				break;
			case "FP1_013":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01);
				break;
			case "ICC_314":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01);
				break;
			case "ULD_268":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01);
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
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "CS2_026":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01);
				break;
			case "DAL_577":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01);
				break;
			case "CS2_033":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01);
				break;
			}
		}
	}
}
