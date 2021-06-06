using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_02 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01.prefab:e898b0501f4775946b45e36df0866d2c");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01.prefab:b138bd177c46bff408d35429f20225b1");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01.prefab:ebf5ee5fa99b59346a22f06c02259d59");

	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01.prefab:2216e4de765f8934d828057e33368a80");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01.prefab:f7036b4d57a6ab841a3f7c435ae9aa71");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01.prefab:e20013fc04519e247a6565196df2a807");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01.prefab:c5cce735e02941443b4bba89e4b6ca41");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01.prefab:51455cd023371ba4cbef5b1d7b6922be");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01.prefab:e1be88dd306bb62428226b4036abd8f8");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01.prefab:32f3fccae85cdda439d8c51176e0d56b");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01.prefab:d7025c683be7ad846938c3064d76d94c");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01.prefab:290f1670e64f3bf43b9747159fb87dce");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01.prefab:de3d15bd885801a43bf61272476c90b3");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01.prefab:03210da823f75eb47b94594386315846");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01.prefab:35e8ecb028ff8ac45b53de4c71df333c");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02.prefab:3dafb16092279f54e972b371de877630");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03.prefab:121cc3da7ebfd20418590b6fae0c82aa");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04.prefab:95cb66ddfad77604db12084d6e354cf0");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01.prefab:f9cc9fd6f2f032945a9a967611bcfbbd");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01.prefab:4a195d306e1d1f448a2930fdfef61a68");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01.prefab:5bcc1a551dfcd1f4f9ef580b66ae9422");

	private static readonly AssetReference VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01 = new AssetReference("VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01.prefab:868ebb9c850bd6b4cb8f6b01cbb11f71");

	private static readonly AssetReference VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01 = new AssetReference("VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01.prefab:bbdf5fec759200b40a01c3a1e8da80cd");

	private List<string> m_VO_BTA_BOSS_02h_IdleLines = new List<string> { VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01 };

	private List<string> m_VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_Lines = new List<string> { VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_02()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01, VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01, VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01,
			VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_02, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_03, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_04, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleA_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleB_01,
			VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_IdleC_01, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01, VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_02h_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossDeath_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_02_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_BossStart_01);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ReplicatotronTransform_01);
			break;
		case 101:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_StealthMinion_01);
			break;
		case 103:
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, VO_BTA_BOSS_02t_Female_Naga_Mission_Fight_02_Boss_DormantNagaAwakens_01);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_HeroPowerTrigger_Lines);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, VO_BTA_09_Female_Naga_Mission_Fight_02_Minion_VictoryA_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_02_Minion_VictoryB_01);
			GameState.Get().SetBusy(busy: false);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "BT_205"))
		{
			if (cardId == "BT_213")
			{
				yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_ImprisonedFelmaw_01);
			}
		}
		else
		{
			yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Hero_Scrapshot_01);
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
			case "BT_008":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_RustswornInitiate_01);
				break;
			case "BTA_13":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_Deteriorate_01);
				break;
			case "BT_156":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_Boss_ImprsonedVilefiend_01);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (turn == 1)
		{
			yield return PlayLineAlways(actor, VO_BTA_BOSS_02h_Female_Demon_Mission_Fight_02_TurnOneResponse_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_02_TurnOne_01);
		}
	}
}
