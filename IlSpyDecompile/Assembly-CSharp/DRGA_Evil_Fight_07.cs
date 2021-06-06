using System.Collections;
using System.Collections.Generic;

public class DRGA_Evil_Fight_07 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01.prefab:952a81bfdab47284994a01faa45590ce");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01.prefab:148c6825171d8cd41bb624f2cff122ca");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01.prefab:6cd702754cada6740b1edfe85ff66cbd");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01.prefab:ecfc1e70bff9db048a6c78da1592aca7");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01.prefab:128a69a740c056949b3b5c74e9e6d6d7");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01.prefab:149a30c34f666b54d9e6e3575a21a6c7");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01.prefab:bdb9ac51e8a91d241878865afeee5913");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01.prefab:4afb1fc74492b304e990584ff1d69d49");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01.prefab:3eb5f7ea36b24f24ba737f6eb82b2125");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01.prefab:6d4e17b4f71c9d0468a6bbc77dcbfe32");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01.prefab:f0d5700f148c49543b6377448242ff00");

	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01.prefab:c66d382d2faa5d340ae55d0e9da4f7cc");

	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01.prefab:85b01537507184842adce860f2de1ef1");

	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01.prefab:50510f94bd9f8d043ac97bdbe15284dd");

	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01.prefab:7df8ee0d6492d3c40bdf4fd1a8985e3f");

	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01.prefab:b803192afa357e64b9c0a98b07341c15");

	private static readonly AssetReference VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02 = new AssetReference("VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02.prefab:b728a370c2350a440933191ef9c5f34b");

	private List<string> m_missionEventTrigger100Lines = new List<string> { VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01 };

	private List<string> m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsakenLines = new List<string> { VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01 };

	private List<string> m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_IdleLines = new List<string> { VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01 };

	private List<string> m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPowerLines = new List<string> { VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreathLines = new List<string> { VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_01_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_02_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Dragondies_03_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_01_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsaken_02_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_01_01,
			VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_02_01, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Idle_03_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_01_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_02_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPower_03_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_01, VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreath_02
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger100Lines);
			break;
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_BossAttack_01);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor2, m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Hero_HeroPowerLines);
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
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "DRG_033" && !m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_29h_Male_Dragon_Evil_Fight_07_Waxadred_CandleBreathLines);
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
			if (cardId == "DRGA_BOSS_23t")
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_23h_Female_Forsaken_Evil_Fight_07_Boss_WillOfForsakenLines);
			}
		}
	}
}
