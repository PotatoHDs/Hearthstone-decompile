using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Good_Fight_04 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01.prefab:69810d20104aa2542bb3edd88608dbd0");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01.prefab:a47af84dbc2216c42b8b3fef7338870b");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01.prefab:48c9bee7aa305a040af19d5baece49b4");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01.prefab:757d9027ca998734fb7bec71dc3d3061");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01.prefab:df5ee5dc38227e845a94e23b95ddc48a");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01.prefab:885a72732e1bb274d9a029e9f8331db6");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01.prefab:b5a573bc5147690419a1f7372f724732");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01.prefab:e0dbe86245f17b640aec423e8e8a812f");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01.prefab:4ae3feef3d7e68d43883dda605245139");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01.prefab:2d6785adc93516942acaf838f508107c");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_PlayerStart_01.prefab:8224ce413ab32b647950f0d31d9b3940");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01.prefab:8236a017ec144f34eacb62a97003856e");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01.prefab:34a248c7f9163a94e97b15597319d46d");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01.prefab:7705d8f93dd73d2408d99c783ccefbfb");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01.prefab:ec94ed7f21163a9469fdba513ed0159e");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01.prefab:40c1776c2068e0643b3cd268f676ff2b");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01.prefab:87a472deab5cefc43ac6678321db273c");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01.prefab:dc0a46965c9592f49bdf5aa7ad5f0ff7");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01.prefab:1b757fe11d7cdcf4ba767d6e1c21112e");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01.prefab:86d1ac0437efe9a44822a98f609f8c4d");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01.prefab:77c46ee8d317d2945a3f150269277379");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01.prefab:8b259bcb432f5b744a40658e76b87e3f");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01.prefab:a487db55467fbd24aa5b727cdda5e9ef");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01.prefab:ed3cf66281223164b99f1528b36deb74");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01.prefab:b19755eeee34b3f4d97548af6e03644f");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01.prefab:0aad0f9d90133724681f6c8c5476fc73");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01.prefab:5bd6f11fef0ac574a96655f1a56c89bd");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01.prefab:70e3d1f1ead929549b85bf1417f5b91e");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01.prefab:4a7782f41958fbc4fa7fd9241ae32573");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01.prefab:ad5daff9579e2ad47a5e8bf4869489de");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01.prefab:498abc279c875a644a6cb292e6d459a9");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01.prefab:d0418048f8e472643adbe94328e6d01a");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01.prefab:c3dda8ac07b82f146bc7cbc7e3d0e306");

	private static readonly AssetReference VO_DRGA_BOSS_21h_Death = new AssetReference("VO_DRGA_BOSS_21h_Death.prefab:0e2b57b395058e849a2c28126fca70cb");

	private static readonly AssetReference VO_DRGA_BOSS_21h_EmoteResponse = new AssetReference("VO_DRGA_BOSS_21h_EmoteResponse.prefab:39fe53c83cb639a46a1fc58d281fd3c3");

	private List<string> m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_PuppeteerLines = new List<string> { VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01 };

	private List<string> m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_IdleLines = new List<string> { VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01 };

	private List<string> VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths = new List<string> { VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01,
			VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_PlayerStart_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01,
			VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01,
			VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01, VO_DRGA_BOSS_21h_Death, VO_DRGA_BOSS_21h_EmoteResponse
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
		return m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_21h_Death;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_09b")
			{
				Gameplay.Get().StartCoroutine(PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
			}
			else if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId() == "DRGA_BOSS_21h")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_21h_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01);
		yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01);
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
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01);
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01);
			break;
		case 105:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01);
			break;
		case 106:
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01);
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01);
			}
			break;
		case 108:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01);
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 110:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01);
			}
			break;
		case 111:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01);
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "DRGA_BOSS_03t2":
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor2, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths);
			}
			break;
		case "OG_133":
		case "OG_280":
		case "OG_042":
		case "OG_134":
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "DRGA_BOSS_09t":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_PuppeteerLines);
				break;
			case "DRGA_BOSS_09t2":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01);
				break;
			case "DAL_011":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01);
				break;
			}
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (thinkEmoteBossThinkChancePercentage > num && m_BossIdleLines != null && m_BossIdleLines.Count != 0 && cardId == "DRGA_BOSS_09h")
		{
			string line = PopRandomLine(m_BossIdleLinesCopy);
			if (m_BossIdleLinesCopy.Count == 0)
			{
				m_BossIdleLinesCopy = new List<string>(GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard()
			.PlayEmote(emoteType);
	}
}
