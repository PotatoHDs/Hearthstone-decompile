using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_18h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01.prefab:cb50cfe393848cf46a3f00f569b1727f");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01.prefab:014dc19dfaddbe047b4cb98356b51351");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Death_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Death_01.prefab:9d0a745a6f8a71548ae3f7f1fb7bd144");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_DefeatPlayer_01.prefab:d25caa7bf184f5942a4f807ea5fdeed3");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01.prefab:d0a61d9788efbd846b597d4b96f52a7e");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01.prefab:7b354bb68ea38014490ca7e4addd32ba");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02.prefab:b535e9b0290069e4dbde8f165a376462");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04.prefab:bd0f3eb73d546a741b059ebd6b0a96c6");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Idle_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Idle_01.prefab:330f0c6b5580aa84cbc914cd2a5490b2");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Idle_02 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Idle_02.prefab:0e0bf35478d59f842aa7c4dcc14812e3");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Idle_03 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Idle_03.prefab:f50f5918509b42740855b84f2daa09dd");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Intro_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Intro_01.prefab:6f158bba08ff3fa42b2490c10c128571");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01.prefab:c88814d382ae33544b467635dc914b61");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01.prefab:1722bf17e3dd1774cbc3ce74d8c17787");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01.prefab:b202ec9a27f03594781ed49759413fb4");

	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01.prefab:9a81ea28775d05347ad2dae910770238");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_18h_Female_Draenei_Idle_01, VO_DALA_BOSS_18h_Female_Draenei_Idle_02, VO_DALA_BOSS_18h_Female_Draenei_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01, VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01, VO_DALA_BOSS_18h_Female_Draenei_Death_01, VO_DALA_BOSS_18h_Female_Draenei_DefeatPlayer_01, VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01, VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01, VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02, VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04, VO_DALA_BOSS_18h_Female_Draenei_Idle_01, VO_DALA_BOSS_18h_Female_Draenei_Idle_02,
			VO_DALA_BOSS_18h_Female_Draenei_Idle_03, VO_DALA_BOSS_18h_Female_Draenei_Intro_01, VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01, VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01, VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01, VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01, VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02, VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04 };
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_18h_Female_Draenei_Intro_01;
		m_deathLine = VO_DALA_BOSS_18h_Female_Draenei_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
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
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		_ = missionEvent;
		yield return base.HandleMissionEventWithTiming(missionEvent);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "LOOT_344"))
		{
			if (cardId == "GIL_583")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "EX1_244"))
		{
			if (cardId == "LOOT_518")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01);
		}
	}
}
