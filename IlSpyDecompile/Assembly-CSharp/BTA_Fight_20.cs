using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_20 : BTA_Dungeon_Heroic
{
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01.prefab:2e3344c8e992e054cb034fa0b44d2dae");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01.prefab:f8ebadd413bb1194fb4f39b86e07789a");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01.prefab:b03478e1287853d498904c0aa36a5396");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01.prefab:ec8f114e8919257498374d3a3dcba9a8");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01.prefab:dee945938b4e0c4489484b8e73645d0a");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeathDragon_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeathDragon_01.prefab:6c70dc3791edd4e4895d7038e5f19cb5");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01.prefab:09a5af2cb9d66274093e0dcea7aa0d9d");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01.prefab:24b4c506085f9e04d8b997d56c444535");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01.prefab:ecc9151a00d836a4fbd3dbc057c2f138");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01.prefab:38ad3021b7d80c749b60438a3f25383d");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01.prefab:fb8b5d236c24c7f4282da2cdf98e6a76");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01.prefab:d7341681689f9014ab6bbe2ab3c879b2");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01.prefab:d590a42e009e2c043b6786e78c7a23f8");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02.prefab:e794e8203efa85a4e93d557b60c60529");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03.prefab:a62807cd851fd3a4f8d2eadcb461b2dd");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04.prefab:ca379cf0d9bdb0146a9f6f764bf08009");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01.prefab:a5eb8ee200f7f14488277b951875f6c4");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01.prefab:4d5834477e914054e8207dcd2740dd83");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01.prefab:51f910f0c1881d8438c3439e5be16fb3");

	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_UI_Mission_Fight_20_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_UI_Mission_Fight_20_CoinSelect_01.prefab:4f01fe794b87e7546aaac1c98be049d4");

	private List<string> m_missionEventTrigger507Lines = new List<string> { VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04 };

	private List<string> m_VO_BTA_BOSS_20h_IdleLines = new List<string> { VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeathDragon_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01,
			VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01, VO_BTA_BOSS_20h_Male_Gronn_UI_Mission_Fight_20_CoinSelect_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_20h_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01;
		m_standardEmoteResponseLine = VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 110:
			yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01);
			break;
		case 115:
			yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01);
			break;
		case 116:
			yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01);
			break;
		case 500:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01);
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
			case "NEW1_030":
			case "DRG_026":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01);
				break;
			case "DAL_575":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01);
				break;
			case "NEW1_038":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01);
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
			if (cardId == "CS2_108")
			{
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01);
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
