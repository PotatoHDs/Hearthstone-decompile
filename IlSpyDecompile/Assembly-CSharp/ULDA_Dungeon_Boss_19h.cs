using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_19h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01.prefab:cebf1bb739a6c764e9cd48d1bfcf141c");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01.prefab:66c6c292af112c549a6316cbd04cbd2e");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01.prefab:32e580ea611e5894f85d6128723b7ae7");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_DefeatPlayer_01.prefab:1a06f04cb515ed142a290b92d321f00f");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01.prefab:680e09754c351c3418b3fb0c25e06cef");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01.prefab:012cc97423a43e74da3f6f296e2b7e62");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02.prefab:0feae56b20341204c855efeb9a477ef7");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03.prefab:660057087869d6746b2107610923cf4e");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04.prefab:4bebbef940b0c2745ad0db14fd93ea75");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05.prefab:94b0aaae1c4e3454dbd0cc137daac275");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01.prefab:5a504f320224e904da7fd12d66cb0cce");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02.prefab:ad60ac67236aa974981677c64a821818");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03.prefab:c60c1ec98b4519c418db8a278aab6385");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01.prefab:0d74a9d90334d2649bf487249de05b09");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01.prefab:817ec92be64226e43a857da98a5e845c");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01.prefab:a8bc6c0ea13be4147aab13ee492c1ddc");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01.prefab:55e6203596a44ce45a61bcc532c917ad");

	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01.prefab:3173b835a49f72746ba9f06422346e69");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02, VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_DefeatPlayer_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04, VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05,
			VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02, VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03, VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01, VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01
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
		m_introLine = VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01;
		m_deathLine = VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01);
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
		if (!(cardId == "ULD_721"))
		{
			if (cardId == "ULDA_208")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01);
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
		if (!(cardId == "CS2_108"))
		{
			if (cardId == "ULD_256")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01);
		}
	}
}
