using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_12 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01.prefab:6207b28dfe2d2dd489bddb216c9598bc");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01.prefab:d5c8917a4cb3401499ecd86c42801133");

	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01.prefab:9ee0d92e24cef7840b5bf44e3dd19b09");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01.prefab:dbd37b82d84594e499da15674822603e");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01.prefab:7c97c474478831c479dafff631800ca3");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01.prefab:8679ea5398ff9bd4099f5a620571ea31");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01.prefab:2961d0b9b354e684a9f522f100b12494");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01.prefab:aa316ccb5f534e546b70dd1124c96ed4");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathA_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathA_01.prefab:2de2f95d4acf4f94d91022f3c956bd40");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01.prefab:567321497cafb6b4ba8cbba14d7ca737");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01.prefab:bb154eb15195c304db7ce2c69f43c2ac");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01.prefab:0b372717b0e66d64f9015a3e6ddd4cab");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01.prefab:59c3aa5627b9e804ca17b67f4c4c5cba");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01.prefab:98d6cdd652a551b4f9d2a854c790630d");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01.prefab:f62ffa26019811a4eaa176e993130198");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01.prefab:4e0d3012425089f49b88e6fce4eaa82d");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02.prefab:2003ba4e664e63549983cfc1d8bb7936");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03.prefab:bf3ecd2fbc8e26840a45a12a378e4d9c");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04.prefab:346f319a2dd82e54a88897feee4ad529");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01.prefab:8012dbfe8fce27947a8cfe559c91f909");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01.prefab:b969da525c961e943be2fd9e0b2f7ca9");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01.prefab:4be27b6c5038b534f955d6c048843293");

	private static readonly AssetReference VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01 = new AssetReference("VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01.prefab:58583e48a54a77346bb7115ba47638b0");

	private List<string> m_VO_BTA_BOSS_12h_IdleLines = new List<string> { VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01 };

	private List<string> m_VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_Lines = new List<string> { VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_12()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01, VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathA_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01,
			VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_02, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_03, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_04, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleA_01,
			VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleB_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_IdleC_01, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_12h_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossDeathB_01;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_12_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_BossStart_01);
		GameState.Get().SetBusy(busy: false);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 500:
			PlaySound(VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor2, VO_BTA_01_Female_NightElf_Mission_Fight_12_VictoryA_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 507:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_HeroPowerTrigger_Lines);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BT_235":
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_ChaosNova_01);
				break;
			case "BT_801":
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_EyeBeam_01);
				break;
			case "BT_850":
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Hero_Magtheridon_01);
				break;
			case "BTA_08":
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, VO_BTA_08_Male_Orc_Mission_Fight_12_MiscA_01);
				yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_MiscB_01);
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
			case "BTA_15":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_EndlessLegion_01);
				break;
			case "EX1_308":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Soulfire_01);
				break;
			case "GVG_052":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_Crush_01);
				break;
			case "OG_276":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_12h_Male_Demon_Mission_Fight_12_Boss_BloodWarriors_01);
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
		_ = turn;
	}
}
