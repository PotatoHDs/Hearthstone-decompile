using System.Collections;
using System.Collections.Generic;

public class DRGA_Evil_Fight_12 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Greetings_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Greetings_01.prefab:63623827e70758e4bb3536bb68291f07");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Oops_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Oops_01.prefab:08333d528d7a5654eab207c998d9f1cc");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Thanks_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Thanks_01.prefab:7f7b4729e415f14439a2b96008a03d8d");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Threaten_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Threaten_01.prefab:e7b35bdec0fdb8146b8f4fa9c8697908");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Wow_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Wow_01.prefab:5250b32a80e11534fa977024dccc8e2d");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01.prefab:4cca2f37ba1588840abe59fac3629346");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01.prefab:c507a04b02b7d444db5989aed139b1ff");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01.prefab:6aca3eda17845e748a986c328af5ac55");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01.prefab:d38517d0b88b9424cbbd7c772518becb");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01.prefab:4f3928959ac118f4eb290f184ba3f3fc");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01.prefab:435c57689933a204bb80c1d05e16c534");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01.prefab:bc74d8739dcec1b4897587a410149bd2");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01.prefab:42dcde5183d62ab4989325e518183aea");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01.prefab:afa209d3b4dfa184ba08b7e03a91b129");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01.prefab:9d3d1c803c607264f88b5c71e5b7f3bb");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01.prefab:1293dd73c1fe811458d88cb13114eacd");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01.prefab:a60ff4ac653d29d4aaa283131c7424e3");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01.prefab:76009e7b08ac2414a885b15b03901408");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01.prefab:c107dd155cf1fb2409f9bc203d836dd7");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01.prefab:34902d6a4ea52644790e091f44a007b4");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01.prefab:3ef89842ff6bafa44b6f5eee23690dfd");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01.prefab:7a420cb2b253bb147a5d49fd4470f499");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01.prefab:e40782b698eb1fb49abc18fc5b1b4eca");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01.prefab:c1442324e17668d4abfbd46d6eed287a");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01.prefab:0bb9093215198014fb9f69a097a6234a");

	private static readonly AssetReference VO_DRG_650_Male_Dragon_Start_01 = new AssetReference("VO_DRG_650_Male_Dragon_Start_01.prefab:c25bea315252ae848acf738d59eb7f87");

	private static readonly AssetReference VO_DRG_610_Male_Dragon_Threaten_01 = new AssetReference("VO_DRG_610_Male_Dragon_Threaten_01.prefab:7c5e7f29cef55ea489b0393e9fb5a27d");

	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPowerLines = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01 };

	private List<string> m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_IdleLines = new List<string> { VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01 };

	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_UntoldSplendorLines = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Greetings_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Oops_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Thanks_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Threaten_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Wow_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01,
			VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01,
			VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01, VO_DRG_650_Male_Dragon_Start_01, VO_DRG_610_Male_Dragon_Threaten_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGEVILBoss);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPowerLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType != EmoteType.START && MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRG_610_Male_Dragon_Threaten_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
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
			yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPowerLines);
			break;
		case 102:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01);
			}
			break;
		case 103:
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(friendlyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01);
			}
			break;
		case 104:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01);
			}
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01);
			break;
		case 108:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPowerLines);
			break;
		case 109:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPowerLines);
			break;
		case 110:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01);
			}
			break;
		case 113:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_DRG_650_Male_Dragon_Start_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 114:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01);
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01);
			}
			break;
		case 115:
			yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_VO_DRGA_BOSS_07h_Male_Ethereal_UntoldSplendorLines);
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
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "DALA_708" && !m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_07h_Male_Ethereal_UntoldSplendorLines);
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
			if (cardId == "LOEA_01" || cardId == "LOEA_01H")
			{
				yield return PlayLineAlways(actor, VO_DRG_650_Male_Dragon_Start_01);
			}
		}
	}
}
