using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_24 : BTA_Dungeon_Heroic
{
	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01.prefab:e3d63675b22f79e4c8b479023969a0bc");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01.prefab:2e67226b64f44a847bde99c46719a105");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01.prefab:48db091dfe3a80f4694150e900f0330c");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02.prefab:0339aab82e9580d4296dc6ec75e2e1b0");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01.prefab:53006bfee7cdaf04bb87b85ce1d6b516");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01.prefab:9297d3bddef53524eb26bc31b73bd2f1");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01.prefab:46233fcd1e7d4914d9312c9379b7a9e6");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01.prefab:e81575e9996a764498922f6e29c78e01");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01.prefab:536317df38e083046845246a41109d6b");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01.prefab:2d32c35040548d547ad9fc19b5b8b864");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01.prefab:c3f902a050d2a07488944749521f06a6");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01.prefab:c12ab414324a5564da2632ebce568b36");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02.prefab:a89fb81b96e5cb843953afaeb436c4c4");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03.prefab:507050813e0a8ef43b2cdb5fa881ace3");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04.prefab:8820cce8136fa6e46bf555d29dbb01d0");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01.prefab:08d3b3877a3f9d246851bab5420c00a0");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01.prefab:f3ec2682d3af4464ebbe35bf81559f79");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01.prefab:e0fce4f07262b234289bcf55676aef12");

	private static readonly AssetReference VO_BTA_BOSS_24h_Female_Naga_UI_Mission_Fight_24_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_24h_Female_Naga_UI_Mission_Fight_24_CoinSelect_01.prefab:253dabbfd2c19f8488345380a6953f76");

	private List<string> m_VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangEliteLines = new List<string> { VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02 };

	private List<string> m_missionEventTrigger507Lines = new List<string> { VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04 };

	private List<string> m_VO_BTA_BOSS_24h_IdleLines = new List<string> { VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangElite_02, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01,
			VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_02, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_03, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_HeroPower_04, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleA_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleB_01, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_IdleC_01, VO_BTA_BOSS_24h_Female_Naga_UI_Mission_Fight_24_CoinSelect_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_24h_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossDeath_01;
		m_standardEmoteResponseLine = VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Emote_Response_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_Attack_01);
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
			if (cardId == "BT_109")
			{
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Hero_LadyVashj_01);
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
			case "BTA_BOSS_24t":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangEliteLines);
				break;
			case "BT_110":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_BowEquip_01);
				break;
			case "BT_230":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_CoilfangWarlord_01);
				break;
			case "EX1_238":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_24h_Female_Naga_Mission_Fight_24_Boss_DeadlyShot_01);
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
