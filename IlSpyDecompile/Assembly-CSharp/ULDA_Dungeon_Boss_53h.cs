using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_53h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01.prefab:2637615ee3e2bd54bb27e0147bbb0d55");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01.prefab:deb4537b4785f7945a5604fe1a186582");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01.prefab:9afeaaf30bed4254084edb0e263ebb90");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Death_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Death_01.prefab:ed3772c2829703b439df74f56395b95c");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_DefeatPlayer_01.prefab:457fdc2378c5dc04788a66fac2e41d5e");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01.prefab:e9b9f218764cab74998c8b91cd5a1ae2");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01.prefab:886812266f013f2449ee10f150b76770");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03.prefab:a374d46474d47fc4d88150288d825da8");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04.prefab:eb39a55924edbd046a1d89bfac59fd17");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05.prefab:d9725f3a83ca4d2459a9d603381691b3");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01.prefab:cdf31f93b1d7c3e4eae1fbe12fdef9f2");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02.prefab:330978ec179b6d14a887cf703cdff70e");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03.prefab:281d51bf27743f54c8dfb4499fbf30c0");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01.prefab:a1479a14ba655d74d96f31de2b39b809");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01.prefab:694aa7613497d3d47ba7ff5189039063");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01.prefab:cddd130c437994045a53f31f504bdf4f");

	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01.prefab:7e927df9d85d9c946ad5785da33bcc84");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01, VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02, VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01, VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01, VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01, VO_ULDA_BOSS_53h_Female_Vulpera_Death_01, VO_ULDA_BOSS_53h_Female_Vulpera_DefeatPlayer_01, VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04, VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05,
			VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01, VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02, VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03, VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01, VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01, VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01, VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01
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
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01;
		m_deathLine = VO_ULDA_BOSS_53h_Female_Vulpera_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor));
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

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01);
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
		if (!(cardId == "ULD_191"))
		{
			if (cardId == "ULD_170")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01);
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
		if (!(cardId == "DAL_728"))
		{
			if (cardId == "ULD_282")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01);
		}
	}
}
