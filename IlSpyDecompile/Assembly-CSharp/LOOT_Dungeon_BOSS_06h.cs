using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_06h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_06h_Male_Furbolg_Intro_01.prefab:80aabdb2e58804e4b820fe25fd706f58", "VO_LOOTA_BOSS_06h_Male_Furbolg_EmoteResponse_01.prefab:8c00812afbfab854b9cc0fa40ad7a250", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower1_01.prefab:e9120cfb5a02e7443af0c631596271ce", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower2_01.prefab:cf086888d51f3f24498f700bfd11b059", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower3_01.prefab:f778efbe26021884db88c723f6c1f1db", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower4_01.prefab:9a25d1db59d12784b834f4b9910571e9", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower5_01.prefab:8b5680e4dde84494e83ff10c79e407ca", "VO_LOOTA_BOSS_06h_Male_Furbolg_Death_01.prefab:982a68851c0d43642a94fab4b3b448d2", "VO_LOOTA_BOSS_06h_Male_Furbolg_DefeatPlayer_01.prefab:981056a42cf99dc478f30111338bb2b0", "VO_LOOTA_BOSS_06h_Male_Furbolg_EventPlayerEvolves_01.prefab:990e6ac0bad905549bead65facc02ab8" })
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
		return new List<string> { "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower1_01.prefab:e9120cfb5a02e7443af0c631596271ce", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower2_01.prefab:cf086888d51f3f24498f700bfd11b059", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower3_01.prefab:f778efbe26021884db88c723f6c1f1db", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower4_01.prefab:9a25d1db59d12784b834f4b9910571e9", "VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower5_01.prefab:8b5680e4dde84494e83ff10c79e407ca" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_06h_Male_Furbolg_Death_01.prefab:982a68851c0d43642a94fab4b3b448d2";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_06h_Male_Furbolg_Intro_01.prefab:80aabdb2e58804e4b820fe25fd706f58", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_06h_Male_Furbolg_EmoteResponse_01.prefab:8c00812afbfab854b9cc0fa40ad7a250", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (!(cardId == "OG_027"))
		{
			if (cardId == "ICC_481")
			{
				yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_06h_Male_Furbolg_EventPlayerEvolves_01.prefab:990e6ac0bad905549bead65facc02ab8");
			}
		}
		else
		{
			yield return PlayEasterEggLine(actor, "VO_LOOTA_BOSS_06h_Male_Furbolg_EventPlayerEvolves_01.prefab:990e6ac0bad905549bead65facc02ab8");
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
