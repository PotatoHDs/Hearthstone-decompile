using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_66h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_66h_Female_Undead_Intro_01.prefab:a5215794eefe1114d9ffd4cc13cc7d1e", "VO_GILA_BOSS_66h_Female_Undead_EmoteResponse_02.prefab:d3150f2caf430a44ba2ff7eee0dc7a8a", "VO_GILA_BOSS_66h_Female_Undead_Death_02.prefab:1580a674aefbc4e459cbb59d1001fa95", "VO_GILA_BOSS_66h_Female_Undead_HeroPower_01.prefab:710a76b29da400648a62052df042614d", "VO_GILA_BOSS_66h_Female_Undead_HeroPower_02.prefab:03e8bf23eba1f7a49957b1dc5fba2e6f", "VO_GILA_BOSS_66h_Female_Undead_HeroPower_03.prefab:1442e3289ff370f4ca908faf4be06c50", "VO_GILA_BOSS_66h_Female_Undead_EventPlayHorn_02.prefab:8c24650aeffa2f142963239d8a560e7d" })
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
		return new List<string> { "VO_GILA_BOSS_66h_Female_Undead_HeroPower_01.prefab:710a76b29da400648a62052df042614d", "VO_GILA_BOSS_66h_Female_Undead_HeroPower_02.prefab:03e8bf23eba1f7a49957b1dc5fba2e6f", "VO_GILA_BOSS_66h_Female_Undead_HeroPower_03.prefab:1442e3289ff370f4ca908faf4be06c50" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_66h_Female_Undead_Death_02.prefab:1580a674aefbc4e459cbb59d1001fa95";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_66h_Female_Undead_Intro_01.prefab:a5215794eefe1114d9ffd4cc13cc7d1e", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_66h_Female_Undead_EmoteResponse_02.prefab:d3150f2caf430a44ba2ff7eee0dc7a8a", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "GILA_852a")
			{
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_66h_Female_Undead_EventPlayHorn_02.prefab:8c24650aeffa2f142963239d8a560e7d");
			}
		}
	}
}
