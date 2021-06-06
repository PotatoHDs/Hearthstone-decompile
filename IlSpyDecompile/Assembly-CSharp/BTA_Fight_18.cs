using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_18 : BTA_Dungeon_Heroic
{
	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01.prefab:2729a0c015a55274b891ae8277c95b0a");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01.prefab:9cd0a99019fbb9d459a8c0c6f55e1531");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01.prefab:2f2888b711f125849b2f5db9d0b84c2f");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01.prefab:a5de9613feb01ae49b61cb49a1790fa2");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01.prefab:a1ebbc7b04832254aba457dc07dbbc0e");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01.prefab:bd6ffec198d8a534daa7ea496ce7622e");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01.prefab:2dde42aa99035b84a87fe231fc9958d4");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01.prefab:6aa175905aaeb224fa558a2601f935bc");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01.prefab:8a7d80ff30e579246b292953df7114c4");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01.prefab:7915a7abac1d8bb4488208ee2d4f3b82");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01.prefab:e1f2814555dd181499a920c1f5754778");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02.prefab:621950d7bfeccb8468b796482864d6ca");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03.prefab:7f153a6f89399ae42ac06bb3b1071c91");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04.prefab:b342b44aae612854d845079d88b02b20");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01.prefab:2092f9498d5a2a045ae5ba7859716689");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01.prefab:dae654dff87dadc418bce653feb3eeb0");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01.prefab:3ea279c040760a940bc9139ec5ba674c");

	private static readonly AssetReference VO_BTA_BOSS_18h_Male_Demon_UI_Mission_Fight_18_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_18h_Male_Demon_UI_Mission_Fight_18_CoinSelect_01.prefab:618497dc5902b37498570cf7c9795ac7");

	private List<string> m_missionEventTrigger507Lines = new List<string> { VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04 };

	private List<string> m_VO_BTA_BOSS_18h_IdleLines = new List<string> { VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01,
			VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_02, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_03, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_HeroPower_04, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleA_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleB_01, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_IdleC_01, VO_BTA_BOSS_18h_Male_Demon_UI_Mission_Fight_18_CoinSelect_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_18h_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossDeath_01;
		m_standardEmoteResponseLine = VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Emote_Response_01;
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
			case "HERO_04":
			case "HERO_04a":
			case "HERO_04b":
			case "HERO_04c":
			case "HERO_04d":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartPaladin_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "HERO_10":
			case "HERO_10a":
			case "HERO_10b":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStartDemonHunter_01, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			default:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineAlways(actor, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_SummonDemon_01);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_Attack_01);
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
			if (cardId == "TRL_302")
			{
				yield return PlayLineAlways(actor, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Hero_TimeOut_01);
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
			case "CS2_062":
			case "CFM_094":
			case "OG_239":
				yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_WarlockAOEDamageSpell_01);
				break;
			case "GVG_021":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_18h_Male_Demon_Mission_Fight_18_Boss_DarkPortal_01);
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
