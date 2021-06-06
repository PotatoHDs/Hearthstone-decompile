using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_23 : BTA_Dungeon_Heroic
{
	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01.prefab:0ab275c1a9c09324c90c9f473801367d");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01.prefab:bb35237313dfd7d488cb61ca0aff4640");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01.prefab:e7c86907f2c517a4b92c1b4dddcaff05");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01.prefab:ce4f2428ca4b0ad45b52cd05492403bf");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01.prefab:2d0d469f9e1076a41a9618ffaafd542d");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01.prefab:7259cd5165ef06840b83cf49afa4b7b2");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01.prefab:c14def875a76cc14faaead0534261c29");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01.prefab:b72cd9ac8ca920745aecb99ab99ea920");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01.prefab:757a239fd530b664f8a30160ebe84160");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02.prefab:65e625c6021c3b040b04891fb5ad1254");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03.prefab:63c3f3a9c20770847a240f7d0da8e467");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04.prefab:6e7f6f9d93f52804295821f8db9c7943");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01.prefab:df0639a438208ce4990b599d1c5a24de");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01.prefab:caa9d5ffa48b0ff47aeb88df0b875996");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01.prefab:b9c83c40dfdc537468ad758526fa05d7");

	private static readonly AssetReference VO_BTA_BOSS_08hx_Female_Demon_UI_Mission_Fight_23_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_08hx_Female_Demon_UI_Mission_Fight_23_CoinSelect_01.prefab:e4e2166f87a9bd04e9eb417285950635");

	private List<string> m_VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_WeaponDestroy = new List<string> { VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01 };

	private List<string> m_missionEventTrigger507Lines = new List<string> { VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04 };

	private List<string> m_VO_BTA_BOSS_23h_IdleLines = new List<string> { VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_LightsChampion_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_StolenSteel_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_02,
			VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_03, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_HeroPower_04, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleA_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleB_01, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_IdleC_01, VO_BTA_BOSS_08hx_Female_Demon_UI_Mission_Fight_23_CoinSelect_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_23h_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossDeath_01;
		m_standardEmoteResponseLine = VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Emote_Response_01;
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
			switch (cardId)
			{
			case "HERO_10":
			case "HERO_10a":
			case "HERO_10b":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStartDemonHunter_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			default:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
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
		case 110:
			yield return PlayLineInOrderOnce(actor, m_VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_WeaponDestroy);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Boss_Attack_01);
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
			if (cardId == "BT_117")
			{
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_08hx_Female_Demon_Mission_Fight_23_Hero_Bladestorm_01);
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
