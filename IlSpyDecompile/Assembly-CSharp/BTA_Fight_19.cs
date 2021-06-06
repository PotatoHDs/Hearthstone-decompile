using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_19 : BTA_Dungeon_Heroic
{
	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01.prefab:b8ab368d1c2fc7444bf66912c16822b8");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01.prefab:f14339031c01bdd40bf7365431f0d8d3");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01.prefab:bf4e0c7b747120c47ad156475523488f");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01.prefab:d6aa49fc61b2cc84ab88bb9ba5dc7dad");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01.prefab:0e692b6708edb9d4bbd6e03e05d97bc6");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01.prefab:3dc62a0eec39fb64ab0ff3e1d24131bd");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01.prefab:7a407e6d58c4c1d4ab1a84649280fc2e");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01.prefab:08852d382e735da45af9527547c0b4e1");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02.prefab:afaaeb3b05b9b1b4492b7204aca2dec4");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03.prefab:4955e26177e712c47b6fedfbdbbec69f");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01.prefab:2b1a1eae46be9db44b6e2a3bf00d241d");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01.prefab:95210bc2fcc850b44bd3f8d297aa3b59");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01.prefab:e640de0c77e7fae43b3c3ba5744d5feb");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01.prefab:d6a7e55471dc27342a140237eadc2e99");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01.prefab:9a10c9750f5c0284e9e4cbdbabdacffe");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01.prefab:843d7f27a3f25c742b661b42aeaaaac3");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02.prefab:50c5f7a0c973881439081382107d7753");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03.prefab:520ed1631f9a375408a89e5ba105abd5");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01.prefab:e9a60b5e9970d9f4096b374ca1ac8548");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01.prefab:848ed1bbb818fae448f4d32bee656a7a");

	private static readonly AssetReference VO_BTA_BOSS_19h_Male_Demon_UI_Mission_Fight_19_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_19h_Male_Demon_UI_Mission_Fight_19_CoinSelect_01.prefab:0a14a62d2f5320844889bca7e653df89");

	private List<string> m_missionEventTrigger115Lines = new List<string> { VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03 };

	private List<string> m_missionEventTrigger507Lines = new List<string> { VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03 };

	private List<string> m_VO_BTA_BOSS_19h_IdleLines = new List<string> { VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_SummonCube_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_02, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_CubeDestroyed_03,
			VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_02, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_HeroPower_03, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleA_01, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_IdleB_01,
			VO_BTA_BOSS_19h_Male_Demon_UI_Mission_Fight_19_CoinSelect_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_19h_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossDeath_01;
		m_standardEmoteResponseLine = VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Emote_Response_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 115:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_missionEventTrigger115Lines);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Attack_01);
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
			case "BT_196":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Kelidan_01);
				break;
			case "BT_730":
			case "BT_140":
			case "BT_715":
			case "BT_716":
			case "BT_120":
			case "BT_123":
			case "BT_123t":
			case "BT_138":
			case "BT_256":
			case "DRG_063":
			case "BT_262":
			case "BT_726":
			case "CFM_609":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_FelOrc_01);
				break;
			case "BT_850":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Magtheridon_01);
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
			case "BT_737":
			case "BT_729":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Hero_Warden_01);
				break;
			case "EX1_301":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_Riftcleaver_01);
				break;
			case "ULD_258":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_19h_Male_Demon_Mission_Fight_19_Boss_GrimRally_01);
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
