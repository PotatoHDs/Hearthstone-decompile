using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_29h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01.prefab:1737ca79607b52143a5e0ce678551c69");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01.prefab:68af1f9daf588bb428aa1e77c354c6b2");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01.prefab:8ec53badcc5f10b4dad88c236ea45227");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Death_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Death_01.prefab:d94c0c40b1c86df4eb19a72680c22c51");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_DefeatPlayer_01.prefab:31885d45b3629ae42a957b1f34866e71");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01.prefab:b063b02d74f94344aa9f3a53f22b05de");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01.prefab:eafb2c89983c98a46b55fb9ee4728c68");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02.prefab:80a545b6bc1353d4a90f2dbd5d212328");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03.prefab:a5086e35131768140b3cea2af5139af4");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04.prefab:7d043c8cc0f5bd146a42de87e52f18d5");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05.prefab:bf79b544b8f941e42bbc78639e914c98");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Idle_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Idle_01.prefab:2a5cfeb91b1aca8499db12548ce24e2a");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Idle_02 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Idle_02.prefab:238da7ea3d616fd4dbe60e1054d744b7");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Intro_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Intro_01.prefab:f6fe799000fd32848acb5c874bf4479b");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01.prefab:b452ab01c32bf6e41aca8dfad5814a10");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01.prefab:26cc182f24c505e49b51a1a889ae6587");

	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01.prefab:cbd6fa51be7caa34ab6c8c407f861d19");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_29h_Female_Elemental_Idle_01, VO_ULDA_BOSS_29h_Female_Elemental_Idle_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01, VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01, VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01, VO_ULDA_BOSS_29h_Female_Elemental_Death_01, VO_ULDA_BOSS_29h_Female_Elemental_DefeatPlayer_01, VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03, VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04,
			VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05, VO_ULDA_BOSS_29h_Female_Elemental_Idle_01, VO_ULDA_BOSS_29h_Female_Elemental_Idle_02, VO_ULDA_BOSS_29h_Female_Elemental_Intro_01, VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01, VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01, VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01
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

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_29h_Female_Elemental_Intro_01;
		m_deathLine = VO_ULDA_BOSS_29h_Female_Elemental_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
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
		if (!(cardId == "ULD_158"))
		{
			if (cardId == "ULD_137")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01);
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
			case "EX1_250":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01);
				break;
			case "ULD_197":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01);
				break;
			case "ULD_158":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01);
				break;
			}
		}
	}
}
