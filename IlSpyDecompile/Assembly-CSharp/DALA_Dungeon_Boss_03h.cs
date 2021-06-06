using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_03h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01.prefab:16976478bdca1bb41a4cd3903a2e502e");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02.prefab:87b79e63418b6f24e827268842a28e2f");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03.prefab:b8211fca6a3cd614a84cd09b6044449d");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01.prefab:708c0acd0b75eef43b67a7f1efe0f43f");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01.prefab:b069918550be0a24e8a85dd04fe847b3");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02.prefab:b4ebbcbf9f919984590b27ef49262623");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03.prefab:f44f15adb95d7cb4c812671be1cc1c7a");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04.prefab:ed5f44d3a5b90764f8bfa3fbb5926eeb");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01.prefab:5c0002137d68f17468f48f752e4cae6e");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02.prefab:8297447eaf8711845ac24407433e3e2e");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03.prefab:0f0347be7d6966c4d9877b8d42912837");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01.prefab:a7b9ab72aa814264b913486991e71b10");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02.prefab:cc6765c289562e7478459df57a36dddc");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01.prefab:6b4ecb20342992c49a31676700e13988");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02.prefab:bdf4e0ddd4e025645ac57c780b747e8e");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03.prefab:cd6099343fd020544968c54145c893e8");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Death_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Death_03.prefab:62438176772786e4383069d604e42766");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_DefeatPlayer_02.prefab:7194f7ddc4463524984926fe6e687a0f");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01.prefab:209c31fdaf50f4949871aed09e282485");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02.prefab:8c0d04d7904b2024096f83ee6ced3091");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03.prefab:5559e68af0ac45e46a14efb8d91c970c");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04.prefab:38fb3ef64c2e2b94cbc73a5a4cee61eb");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Idle_01.prefab:f93ca7e9021783d42a38c42d5a07a094");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Idle_02.prefab:99d3be1976028a8439a4bddba50c0b6d");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Idle_04.prefab:99c4f5047113f9247be1ce60fff837fa");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_Intro_01.prefab:75457983cd77c824d8fefa9b5196ee9e");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01.prefab:8dd82ee0ffe129a4aa4ae109391ba793");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01.prefab:94fce89c59818ef4d85fbf51b1bdf314");

	private static readonly AssetReference VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02 = new AssetReference("VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02.prefab:ff5ccb4f4aa2b35499b0419a47610fa6");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_Idle_01, VO_DALA_BOSS_03h_Male_Goblin_Idle_02, VO_DALA_BOSS_03h_Male_Goblin_Idle_04 };

	private List<string> m_BossReductomara = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01, VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02, VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03 };

	private List<string> m_BossBunnifitronus = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01, VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02, VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03 };

	private List<string> m_BossPresto = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03 };

	private List<string> m_BossYoggers = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01, VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02, VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03 };

	private List<string> m_BossSpell = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01, VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02 };

	private List<string> m_PlayerBossSpell = new List<string> { VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01, VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_01, VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_02, VO_DALA_BOSS_03h_Male_Goblin_BossBunnifitronus_03, VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_01, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_02, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_03, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04, VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_01, VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_02,
			VO_DALA_BOSS_03h_Male_Goblin_BossReductomara_03, VO_DALA_BOSS_03h_Male_Goblin_BossSpell_01, VO_DALA_BOSS_03h_Male_Goblin_BossSpell_02, VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_01, VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_02, VO_DALA_BOSS_03h_Male_Goblin_BossYoggers_03, VO_DALA_BOSS_03h_Male_Goblin_Death_03, VO_DALA_BOSS_03h_Male_Goblin_DefeatPlayer_02, VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01, VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02,
			VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03, VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04, VO_DALA_BOSS_03h_Male_Goblin_Idle_01, VO_DALA_BOSS_03h_Male_Goblin_Idle_02, VO_DALA_BOSS_03h_Male_Goblin_Idle_04, VO_DALA_BOSS_03h_Male_Goblin_Intro_01, VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01, VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_01, VO_DALA_BOSS_03h_Male_Goblin_PlayerBossSpell_02
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_03h_Male_Goblin_HeroPower_02, VO_DALA_BOSS_03h_Male_Goblin_HeroPower_03, VO_DALA_BOSS_03h_Male_Goblin_HeroPower_04 };
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_03h_Male_Goblin_Intro_01;
		m_deathLine = VO_DALA_BOSS_03h_Male_Goblin_Death_03;
		m_standardEmoteResponseLine = VO_DALA_BOSS_03h_Male_Goblin_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_03h_Male_Goblin_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Chu" && cardId != "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossSpell);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_03h_Male_Goblin_BossPresto_04);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
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
			switch (cardId)
			{
			case "DALA_BOSS_03t":
			case "DALA_BOSS_03t2":
			case "DALA_BOSS_03t3":
			case "DALA_BOSS_03t4":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerBossSpell);
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
			switch (cardId)
			{
			case "DALA_BOSS_03t":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossReductomara);
				break;
			case "DALA_BOSS_03t2":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossBunnifitronus);
				break;
			case "DALA_BOSS_03t3":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossPresto);
				break;
			case "DALA_BOSS_03t4":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossYoggers);
				break;
			case "GIL_147":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_03h_Male_Goblin_BossFireSpell_01);
				break;
			}
		}
	}
}
