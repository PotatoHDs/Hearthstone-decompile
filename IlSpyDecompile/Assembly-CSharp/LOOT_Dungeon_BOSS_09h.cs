using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_09h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_FrostLines = new List<string> { "VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic1_01.prefab:9173fbf2da9c2954e86a889ddfb1ee83", "VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic2_01.prefab:aff1ca3d359706243b7a2c678e4ddbca", "VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic3_01.prefab:f9ca28db284511b40ad78ed168e90d03" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_09h_Female_Furbolg_Intro_01.prefab:29cd999b72b06e0468d7bf599c86bd0b", "VO_LOOTA_BOSS_09h_Female_Furbolg_EmoteResponse_01.prefab:21a1c7420686dbd4789868f49a5949e7", "VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower1_01.prefab:7ecdadc75755ce84bb13cdeaead21310", "VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower2_01.prefab:500be17bf66199247a02ab77b8798eff", "VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower3_01.prefab:45f7242c90e66c148946f4225399f143", "VO_LOOTA_BOSS_09h_Female_Furbolg_Death_01.prefab:d7d94e2e925474b4bbcb7a61c0c6fdaf", "VO_LOOTA_BOSS_09h_Female_Furbolg_DefeatPlayer_01.prefab:6391d6c1a88c90f4ab7fee2c553e700f", "VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic1_01.prefab:9173fbf2da9c2954e86a889ddfb1ee83", "VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic2_01.prefab:aff1ca3d359706243b7a2c678e4ddbca", "VO_LOOTA_BOSS_09h_Female_Furbolg_EventFrostMagic3_01.prefab:f9ca28db284511b40ad78ed168e90d03" })
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower1_01.prefab:7ecdadc75755ce84bb13cdeaead21310", "VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower2_01.prefab:500be17bf66199247a02ab77b8798eff", "VO_LOOTA_BOSS_09h_Female_Furbolg_HeroPower3_01.prefab:45f7242c90e66c148946f4225399f143" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_09h_Female_Furbolg_Death_01.prefab:d7d94e2e925474b4bbcb7a61c0c6fdaf";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_09h_Female_Furbolg_Intro_01.prefab:29cd999b72b06e0468d7bf599c86bd0b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_09h_Female_Furbolg_EmoteResponse_01.prefab:21a1c7420686dbd4789868f49a5949e7", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "LOOTA_840":
		case "CS2_028":
		case "CFM_021":
		case "ICC_836":
		case "CS2_024":
		case "CS2_026":
		case "EX1_275":
		case "CS2_037":
		case "ICC_078":
		case "ICC_056":
			if (m_FrostLines.Count != 0)
			{
				string randomLine = m_FrostLines[Random.Range(0, m_FrostLines.Count)];
				yield return PlayEasterEggLine(actor, randomLine);
				m_FrostLines.Remove(randomLine);
				yield return null;
			}
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
	}
}
