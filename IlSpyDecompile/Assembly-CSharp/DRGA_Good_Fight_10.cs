using System.Collections;
using System.Collections.Generic;

public class DRGA_Good_Fight_10 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01.prefab:50ad3af71651dee48b1c5238599d6df4");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01.prefab:08019cbc4a328ed4f9d558b9ecb00481");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01.prefab:ef13e0c9db32dd34d8cb7a622fff2e3d");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01.prefab:9f21066db9f25ff46b1c65f88754a3e2");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01.prefab:40510a55066c2f74699c40021a91efcc");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01.prefab:1d7e42cf31e31e24983e5b9dbdecdf22");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01.prefab:3f8c74f77cc11f643b2c74cfdeb4d1f9");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01.prefab:2cc5ca550881eb841964d51ffe02e244");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01.prefab:468a554b1b975f04eaece5bf0e35d56a");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01.prefab:0ab6b9d5f4387b04194855ba3a63d8ae");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01.prefab:3bb9683d5c54a1644a7fc5cdb2b969d6");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01.prefab:6fefb79902929c64487b54f940fa130a");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01.prefab:52f6e165a24a0634cadfcacab3f6022d");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01.prefab:606c56ef66b98e74a9be873b52d8ae3c");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01.prefab:1ae53f0a3148ebf45a0b65355469bf97");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01.prefab:566a52eddf171aa42ae3c0d1efda0c4e");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01.prefab:f9c49e71ac3db36429fb193c1f706f09");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01.prefab:7c56039b14fd28047bed95cc571dd27f");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01.prefab:10019f7b141216b4e97a1838e337ef44");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01.prefab:510e7486be1dee44ea7fe5701740e2fb");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01.prefab:7bd4e45f65533a44caf048675c453d9c");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01.prefab:f4efc27bc8698a343b3c264fa9887228");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01.prefab:8f0b2837e2a397c4ea842b914427da02");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01.prefab:7d66c4d38a6f07d41bd0bbfe64518580");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01.prefab:b13e078bb0706754e9c85ccd4448a369");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01.prefab:d7ec92988cada1343b672f2349ef9f44");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01.prefab:6b86b69b7f5d6054c97330f9f80a45d3");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01.prefab:3c36c9b338e28ab47a1c4793ee228c3b");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01.prefab:66d27281b0b79f546a411ad1d1f42237");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01.prefab:ecda67edf924f9144b1fee415c1a5cd3");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01.prefab:147a9e873b024cd46ba2542b5d506b71");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01.prefab:5dc482fb65804e642b98ea2097cda4a4");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01.prefab:95dd01aabfee8154e9b8e6c637395c47");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01.prefab:c170b366458989f49bd736c3b00db00d");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01.prefab:038cf01d36976394cb45c66da9ba7fa5");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01.prefab:77abd879f3c394643940e0227ffeadbd");

	private List<string> m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines = new List<string> { VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01 };

	private List<string> m_missionEventHeroPowerKarl = new List<string> { VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01,
			VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01,
			VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01,
			VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		if (!m_Heroic)
		{
			m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines.Add(VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01);
		}
		if (m_Heroic)
		{
			m_deathLine = VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01;
			m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines.Add(VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
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
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01);
			break;
		case 101:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01);
			break;
		case 104:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01);
			break;
		case 111:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01);
			}
			break;
		case 112:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01);
			}
			break;
		case 114:
			if (m_Heroic)
			{
			}
			break;
		case 115:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01);
			}
			break;
		case 116:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01);
			}
			break;
		case 117:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01);
			}
			break;
		case 109:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01);
			}
			break;
		case 120:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 121:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 122:
			yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionEventHeroPowerKarl);
			break;
		case 123:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01);
				GameState.Get().SetBusy(busy: false);
			}
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
			switch (cardId)
			{
			case "UNG_960":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01);
				break;
			case "LOOT_541":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01);
				break;
			case "UNG_950":
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01);
				GameState.Get().SetBusy(busy: false);
				break;
			case "CS2_142":
			case "DAL_614":
			case "DRG_082":
			case "LOOT_014":
			case "LOOT_041":
			case "LOOT_042":
			case "LOOT_062":
			case "LOOT_347":
			case "LOOT_382":
			case "LOOT_389":
			case "LOOT_412":
			case "LOOT_531":
			case "LOOT_998k":
			case "OG_082":
			case "TOT_033":
			case "ULD_184":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01);
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
		}
	}
}
