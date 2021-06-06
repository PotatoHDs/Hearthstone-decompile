using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_22 : BTA_Dungeon_Heroic
{
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01.prefab:5e2ee0089e6d2da49b73f3207c236af9");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01.prefab:ff2f65b86ac9ba7409a577c212efd9c5");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01.prefab:fc70bc1102656c74b8003626be6cf1bb");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01.prefab:eae6b8b3e2aab6041a96172296d697c0");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01.prefab:f0d128f0675fe1b47b6460e96a4d682e");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01.prefab:ce8940cb3601ab848a4e57ee44c23185");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01.prefab:6d92097ccf49b8841b802599db8cf03a");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01.prefab:24f7e4daae17ce349ac74edf6857220d");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01.prefab:6a5f4f9cbae1f084089954a22ebbbaec");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01.prefab:8977f253905fd80479ddca63cd047ff4");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01.prefab:a9395cd9495bf9444ba1875c33ba1d9f");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02.prefab:0d8d0d883a997ca4bba2b3eb6e7672c0");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03.prefab:531f90851e82b15449e6b3a94fdcdcb2");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04.prefab:3931c237faf965f458570832c5a93c14");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01.prefab:69a93c8681b5a0044ae3cdfeb65322c3");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01.prefab:b7834b0c5235adf4f9b97e4259dc400c");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01.prefab:0f46df05c85bf7a43a346b3e85a5fd50");

	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_UI_Mission_Fight_22_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_UI_Mission_Fight_22_CoinSelect_01.prefab:d67906a7a549b754e99c79a963f502e1");

	private List<string> m_missionEventTrigger507Lines = new List<string> { VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04 };

	private List<string> m_VO_BTA_BOSS_22h_IdleLines = new List<string> { VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01,
			VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01, VO_BTA_BOSS_22h_Male_Human_UI_Mission_Fight_22_CoinSelect_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_22h_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01;
		m_standardEmoteResponseLine = VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_10" || cardId == "HERO_10a")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 500:
			PlaySound(VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01);
			break;
		case 507:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger507Lines);
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
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ICC_314":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01);
				break;
			case "ICC_481":
			case "ICC_827":
			case "ICC_828":
			case "ICC_830":
			case "ICC_829":
			case "ICC_831":
			case "ICC_832":
			case "ICC_833":
			case "ICC_834":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01);
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
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ICC_314t4":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01);
				break;
			case "ICC_314t5":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01);
				break;
			case "ICC_314t8":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01);
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		_ = turn;
	}
}
