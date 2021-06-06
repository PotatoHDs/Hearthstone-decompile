using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Evil_Fight_03 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01.prefab:7cd908c62e238d945a0d3880736e5421");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01.prefab:a7ca4be725f2d3d42abf56dc852494e1");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01.prefab:d390bce5be0368c4986ce095f709e840");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_ALT_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_ALT_01.prefab:79be1f3e7cab0ce40905ddc2a846b886");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01.prefab:d8f1f38e062c3d3419d4932421a3f434");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01.prefab:620fb641515f72842b1c56912e35b4f2");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01.prefab:43898a028b442c049a67a6fd8cdc71e7");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01.prefab:5e71aa682bec7304597e3bd7b619d3fd");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01.prefab:02fea32c6fda97c4c952cb76e6387ff7");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01.prefab:90a7d1f7d7e64eb4bb2eb7247686af35");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01.prefab:659d57c7389ab52448ac1a90cb4b47a6");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01.prefab:ae4098952b0ba95479e77d10b81b2bca");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01.prefab:8fa4c3834bbd3a744adf06e2f1b8f417");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01.prefab:5f395a434ba858c4884c523c110160c9");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01.prefab:302410326e7b25b4baaee34aeab9ffc6");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01.prefab:7915ed9b45f767b40ae2bf314a46c752");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01.prefab:94eb8d42e80b8b545825241962239e58");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01.prefab:76dbdae839c0a944abee890120f6e41d");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01.prefab:3a6a3380385797a43940cb82d2abfacb");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01.prefab:a14c4cae34d6fe64dbdff511465b0791");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01.prefab:4701ca333ef721d45ae99aaa860e1ec7");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01.prefab:dff47c58319837d479094f9233a6ebd6");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01.prefab:8df4b068b4329fe47b680f6ba836e890");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01.prefab:2e5400dca2ca8bc479969fd2e4ae0f0a");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01.prefab:af48615fd4ff6f84b89d7bddebe2e870");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01.prefab:cd6ad095dd4b3174bacd848b2740216c");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01.prefab:d1edc6c04cc215540b3433a3daf46988");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01.prefab:c499f8336b39f904fa00c3bbf78d3210");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01.prefab:5ec0f084ce43d3848a5dbab3c3b47ef7");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01.prefab:573d202a24637ff418d0d823c691cccf");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01.prefab:46df1344f2e44ab41af76ddc48d56420");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01.prefab:7d5e73641e1f2c24e8fd10d04dfc8559");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_04_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_04_Hero_01.prefab:8b8c1754de37b88449785c075e344fe6");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_PlayerStart_01.prefab:d7872f57f8ef4414fa2c5169fbcf737c");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01.prefab:c615497291aad6d4e9c78ad885519bdd");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01.prefab:40008b7fc1a38b8448aa4f562b342a57");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01.prefab:661d71cae3c252d4cb2333e23c8016eb");

	private List<string> m_missionEventHeroPowerTrigger = new List<string> { VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01 };

	private List<string> m_VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_IdleLines = new List<string> { VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_ALT_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01,
			VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01,
			VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01,
			VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_04_Hero_01, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_PlayerStart_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01);
			break;
		case 101:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01);
			break;
		case 199:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_missionEventHeroPowerTrigger);
			break;
		case 103:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01);
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01);
			}
			break;
		case 106:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01);
			}
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01);
			}
			break;
		case 109:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01);
			}
			break;
		case 110:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01);
			}
			break;
		case 111:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01);
			break;
		case 112:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01);
			}
			break;
		case 113:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayCriticalLine(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01);
				yield return new WaitForSeconds(2f);
				yield return PlayCriticalLine(enemyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01);
				yield return PlayCriticalLine(friendlyActor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 114:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01);
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
		switch (cardId)
		{
		case "LOE_077":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01);
			break;
		case "LOE_079":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01);
			break;
		case "LOE_076":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01);
			break;
		case "ULD_139":
		case "ULD_500":
		case "ULD_156":
		case "ULD_238":
			if (m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01);
			}
			break;
		case "EX1_182":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01);
			break;
		case "DRGA_001":
		case "LOE_011":
			if (m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01);
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("LOE_011"), null, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01);
				yield return PlayLineAlwaysWithBrassRing(GetEnemyActorByCardId("LOE_011"), null, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01);
			}
			break;
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
		}
	}
}
