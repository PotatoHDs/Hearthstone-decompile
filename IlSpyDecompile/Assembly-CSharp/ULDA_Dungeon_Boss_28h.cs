using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_28h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01.prefab:2170c1c365e9bd34bb5fe00a59990557");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01.prefab:87d63cbce23f44a4ea88f8c067d8b7dd");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Death_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Death_01.prefab:60bfa7953200b27418356cb4317499ba");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_DefeatPlayer_01.prefab:d49d15d9a47db8d479fdc664e42cfe85");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01.prefab:a09a7ebf7b3c6a8419cb06ef9ee33da3");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01.prefab:3f0befd3bfb9126488242c1fb5b9df42");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02.prefab:59b79fa742b1d8c4aa7a60d26f4dc92b");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03.prefab:96255a10992be1f4ca765472dc954096");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04.prefab:91ff1f35bc33f99448740d632e880aae");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05.prefab:07a2b666d72066a409c9b4907a3e27ad");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Idle_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Idle_01.prefab:e1ee77c7cbde6cc4f900edc843d40333");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Idle_03 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Idle_03.prefab:647f4eee0f573ba4ab66d5d4e5ab8e1c");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Intro_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Intro_01.prefab:b2d4fa03f5cd4ca44889408bd45ce45e");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01.prefab:2cfeb12312e30f04d9e69faeb0ecc1cb");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01.prefab:3c29e7ed5727eb34e884af5158994141");

	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01.prefab:c8bee67a877784d46869eb20a987f669");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_28h_Female_Revenant_Idle_01, VO_ULDA_BOSS_28h_Female_Revenant_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01, VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01, VO_ULDA_BOSS_28h_Female_Revenant_Death_01, VO_ULDA_BOSS_28h_Female_Revenant_DefeatPlayer_01, VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04, VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05,
			VO_ULDA_BOSS_28h_Female_Revenant_Idle_01, VO_ULDA_BOSS_28h_Female_Revenant_Idle_03, VO_ULDA_BOSS_28h_Female_Revenant_Intro_01, VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01, VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01, VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01
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
		m_introLine = VO_ULDA_BOSS_28h_Female_Revenant_Intro_01;
		m_deathLine = VO_ULDA_BOSS_28h_Female_Revenant_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULD_158":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01);
				break;
			case "ULD_713":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01);
				break;
			case "ULD_178":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01);
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
		if (!(cardId == "NEW1_010"))
		{
			if (cardId == "LOOT_517")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01);
		}
	}
}
