using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_21h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01.prefab:cb6475cddb4628644b0e4dc901289b06");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01.prefab:bfa6a8f5f8d99ba46b87e382b66cbe77");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01.prefab:46613392d861162408cea8ff8688303b");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Death_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Death_01.prefab:fd2f2d4c8ee359f4e8dad7a4cfca0f42");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_DefeatPlayer_01.prefab:504f16bec31ace54f85d82e8a3fb0623");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01.prefab:f622470889b1a6d4e98248078bc73de9");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02.prefab:e18274e0c85c91647a42ac78ea8287ba");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03.prefab:0b224351c92010e4f8066503fadff1fd");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04.prefab:fd300f3e9d5b84a4ead1f9d8f90f4405");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05.prefab:5798ec62d65a7804c81a9e9ee6dc7f4c");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01.prefab:a93a456bbb3f04b44b9bfec33c6b5b7e");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02.prefab:36761bf96493ddf49ba7a65d1a2c3f54");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03.prefab:099dd12705cf9304996760a35cd23283");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01.prefab:ee4b5860061bc2145a2ab9829b2d1dac");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01.prefab:4321de4c6f34b3a4c966ceefaf000107");

	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01.prefab:e5128c7afd634f745871e3c047a00a73");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01, VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02, VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01, VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01, VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01, VO_ULDA_BOSS_21h_Female_BloodElf_Death_01, VO_ULDA_BOSS_21h_Female_BloodElf_DefeatPlayer_01, VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04, VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05,
			VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01, VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02, VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03, VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01, VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01, VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01
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
		m_introLine = VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01;
		m_deathLine = VO_ULDA_BOSS_21h_Female_BloodElf_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Elise" && cardId != "ULDA_Reno")
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ULD_276"))
		{
			if (cardId == "ULD_155")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01);
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
			case "DAL_727":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01);
				break;
			case "EX1_080":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01);
				break;
			case "ULD_728":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01);
				break;
			}
		}
	}
}
