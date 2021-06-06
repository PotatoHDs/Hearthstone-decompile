using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_33h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01.prefab:641e0356fb933da43a0863ff3f4cb97b");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Death_02 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Death_02.prefab:3cefb8526164bc44db41c9b7e32506d8");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_DefeatPlayer_01.prefab:238d5ed4853c5b64c9abd46b0019a25a");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01.prefab:cc15f3b773696994483b1e1e643cd50a");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01.prefab:301fc70e924dc5e42bb93f221682c37a");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02.prefab:e57975c4cda39ad4995879f2dc10fc91");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03.prefab:f74b4be1f30a53c4ba84dd2259dc4747");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_01.prefab:0ddebf7b4b38c8b49b9157047a086582");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_02 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_02.prefab:af8af91debd524943aa16f3c20280887");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_03 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_03.prefab:a97b231d4e03e7a49abe64fef00435b9");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_04 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_04.prefab:c0552dd3a2757a440a8a0b0479d26b30");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Intro_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Intro_01.prefab:cedda85793eb1d74cab0e8ce5d3e3bef");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01.prefab:ab526e51b7c1be7458bc211bb1fc5024");

	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01.prefab:765931e3128a256468ea3c0a4ef07246");

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01, VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02, VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_33h_Male_Elemental_Idle_01, VO_DALA_BOSS_33h_Male_Elemental_Idle_02, VO_DALA_BOSS_33h_Male_Elemental_Idle_03, VO_DALA_BOSS_33h_Male_Elemental_Idle_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01, VO_DALA_BOSS_33h_Male_Elemental_Death_02, VO_DALA_BOSS_33h_Male_Elemental_DefeatPlayer_01, VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01, VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01, VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02, VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03, VO_DALA_BOSS_33h_Male_Elemental_Idle_01, VO_DALA_BOSS_33h_Male_Elemental_Idle_02, VO_DALA_BOSS_33h_Male_Elemental_Idle_03,
			VO_DALA_BOSS_33h_Male_Elemental_Idle_04, VO_DALA_BOSS_33h_Male_Elemental_Intro_01, VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01, VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_33h_Male_Elemental_Intro_01;
		m_deathLine = VO_DALA_BOSS_33h_Male_Elemental_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora" && cardId != "DALA_Rakanishu")
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
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
			case "GAME_005":
			case "GVG_028t":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01);
				break;
			case "LOOT_998k":
			case "DALA_709":
			case "LOE_019t2":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			if (cardId == "GAME_005" || cardId == "GVG_028t")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01);
			}
		}
	}
}
