using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_66h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01.prefab:341aacf7352bf024188831e4ab716f45");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01.prefab:b7b7df2179a59b3418e88ba4e11acdb3");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01.prefab:af97b4b2bc6b77f4eb3980fbac310e58");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01.prefab:64a68f83742b9634bac6edc7854dbc6e");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_DefeatPlayer_01.prefab:3fbc7019ec1d7c843b5403a727edfd13");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01.prefab:ac9e6e3eea022b44b902efd6d70ae1a8");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02.prefab:fe361a03a2671384c9ebe4725f49ebca");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03.prefab:7ff78a906890abc4d8cbf4ae191b2831");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04.prefab:320c88ba420ade145a3be54220f9a62d");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05.prefab:2767250aaae90114487ef3f34a240431");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01.prefab:e6a8452fb2ca1fa4692bdeade843a628");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02.prefab:fcded398091442c4ea930bc1bc016792");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03.prefab:818c1915a901a5f43b5ffbe479f52757");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04.prefab:23826da75864bb14c88332332926e3b0");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Intro_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Intro_01.prefab:3ba6f3d90baae2f4eb5a270f3414a1e8");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01.prefab:0e3aa5202fdd39c44b168590f8813f6d");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01.prefab:ecf4efdb0c1d6504da43ee67eaee854d");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01.prefab:8c507084f7c11ef43969f0688e07bb05");

	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01.prefab:685670079405c9347bc2e10681759c03");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01, VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01, VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01, VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01, VO_ULDA_BOSS_66h_Male_Djinn_DefeatPlayer_01, VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04, VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05,
			VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01, VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02, VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03, VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04, VO_ULDA_BOSS_66h_Male_Djinn_Intro_01, VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01, VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01, VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01, VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_66h_Male_Djinn_Intro_01;
		m_deathLine = VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01);
			break;
		case 101:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01);
			break;
		case 102:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02);
			break;
		case 103:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03);
			break;
		case 104:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04);
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
			case "ULD_178":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01);
				break;
			case "LOOTA_814":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01);
				break;
			case "ULD_003":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01);
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
			case "EX1_251":
			case "CFM_707":
			case "EX1_238":
			case "EX1_259":
			case "OG_206":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01);
				break;
			case "CS2_022":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01);
				break;
			}
		}
	}
}
