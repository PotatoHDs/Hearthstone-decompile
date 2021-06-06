using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_37h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_BossElise_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_BossElise_01.prefab:5c468a8ec467bb542aceffda168ace08");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Death_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Death_01.prefab:371ee3d52a8568e4e9a70a857a735297");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_DefeatPlayer_01.prefab:73c96cf139eef5b4a8e8286a538ca678");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01.prefab:ae98256975eeaf94ebc2aad3893748a3");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01.prefab:969447fd1a358b845b45ff7e2e9efb3e");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02.prefab:fb9ae5ff67d1c73408558da24ca27d03");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03.prefab:f43fd61454f5e6f43a977a9001ff913a");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01.prefab:f6296e9981f5dbd43b9b0ee8aabf079d");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02.prefab:39d1b61811283a545ae5ef749acbd2f5");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03.prefab:04597d44342c0694ba0afd7a147b1973");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Idle_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Idle_01.prefab:46fbf502dcdfb8940893ee6d7e3390a0");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Idle_02 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Idle_02.prefab:a0409eb808894a8418675f56a1437857");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Idle_03 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Idle_03.prefab:9425a99cdaadc75459271211254bdedb");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_Intro_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_Intro_01.prefab:f3c1ebebc05c02b468dece4cf314197d");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01.prefab:e9638b80b0cea714ab3810c8ddbd2bda");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01.prefab:899736e4a366c1b4899d544e29f9f76c");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01.prefab:68519ad2124ec564e89962461b2d623a");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01.prefab:f9730a6e7130b1e4c81514847449c3f5");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01.prefab:fc19f07efbc879c4cbe732ab8e604388");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01.prefab:e6723d3ee8c3a3e43bd683be75993b97");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01.prefab:159ca9bd10be79d4faf6550a92815f90");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01.prefab:6dfc72f20d5a7444e8d03a27d1b30463");

	private static readonly AssetReference VO_DALA_BOSS_37h_Male_Troll_TurnStart_01 = new AssetReference("VO_DALA_BOSS_37h_Male_Troll_TurnStart_01.prefab:fcc8e955884693746b28a6e14302a492");

	private static List<string> m_HeroPowerDraw = new List<string> { VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01, VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02, VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_37h_Male_Troll_Idle_01, VO_DALA_BOSS_37h_Male_Troll_Idle_02, VO_DALA_BOSS_37h_Male_Troll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_37h_Male_Troll_BossElise_01, VO_DALA_BOSS_37h_Male_Troll_Death_01, VO_DALA_BOSS_37h_Male_Troll_DefeatPlayer_01, VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01, VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_01, VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_02, VO_DALA_BOSS_37h_Male_Troll_HeroPowerDraw_03, VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01, VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02, VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03,
			VO_DALA_BOSS_37h_Male_Troll_Idle_01, VO_DALA_BOSS_37h_Male_Troll_Idle_02, VO_DALA_BOSS_37h_Male_Troll_Idle_03, VO_DALA_BOSS_37h_Male_Troll_Intro_01, VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01, VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01, VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01, VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01, VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01, VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01,
			VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01, VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01, VO_DALA_BOSS_37h_Male_Troll_TurnStart_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_01, VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_02, VO_DALA_BOSS_37h_Male_Troll_HeroPowerShuffle_03 };
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_37h_Male_Troll_Intro_01;
		m_deathLine = VO_DALA_BOSS_37h_Male_Troll_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_37h_Male_Troll_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Rakanishu" && cardId != "DALA_Tekahn" && cardId != "DALA_Squeamlish" && cardId != "DALA_Eudora" && cardId != "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerDraw);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_37h_Male_Troll_PlayerDestroyMarinTreasure_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_37h_Male_Troll_PlayerPirate_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_37h_Male_Troll_TurnStart_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "DS1_184":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_37h_Male_Troll_PlayerTracking_01);
				break;
			case "DALA_701":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_37h_Male_Troll_PlayerTogglewagglesChest_01);
				break;
			case "LOOT_357":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_37h_Male_Troll_PlayerMarin_01);
				break;
			case "DALA_709":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_37h_Male_Troll_PlayerGoldenCandle_01);
				break;
			case "LOOT_542":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_37h_Male_Troll_PlayerKingsbane_01);
				break;
			case "ICC_828":
			case "ICC_831":
			case "ICC_833":
			case "ICC_832":
			case "ICC_830":
			case "ICC_481":
			case "ICC_834":
			case "ICC_827":
			case "ICC_829":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_37h_Male_Troll_PlayerDeathKnight_01);
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
			if (cardId == "LOE_079")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_37h_Male_Troll_BossElise_01);
			}
		}
	}
}
