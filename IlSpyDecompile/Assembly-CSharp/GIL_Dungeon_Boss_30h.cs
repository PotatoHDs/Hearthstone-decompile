using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_30h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomPlayerLines = new List<string> { "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_01.prefab:c9780cba16cd06445bd6e7e2e0efa1e8", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_02.prefab:e1de4eca234ede04abc7dceb210b2862", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_06.prefab:7229b20970f6a404c915407bec978e01" };

	private List<string> m_RandomHeroLines = new List<string> { "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_04.prefab:7743ea933ef269646a76c8890e2f6bea", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_05.prefab:abb18a13f759d1f469c411f336f8745f" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_30h_Female_Gnome_Intro_01.prefab:8299ef8185f5d9545af06156ce8b6910", "VO_GILA_BOSS_30h_Female_Gnome_EmoteResponse_01.prefab:ddb12b11b028f9a478e49fde4d26b989", "VO_GILA_BOSS_30h_Female_Gnome_Death_01.prefab:e5758ee593be0dd428b09d57d53b6e16", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_01.prefab:c9780cba16cd06445bd6e7e2e0efa1e8", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_02.prefab:e1de4eca234ede04abc7dceb210b2862", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_04.prefab:7743ea933ef269646a76c8890e2f6bea", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_05.prefab:abb18a13f759d1f469c411f336f8745f", "VO_GILA_BOSS_30h_Female_Gnome_EventBloodMagic_06.prefab:7229b20970f6a404c915407bec978e01" })
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_30h_Female_Gnome_Intro_01.prefab:8299ef8185f5d9545af06156ce8b6910", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_30h_Female_Gnome_EmoteResponse_01.prefab:ddb12b11b028f9a478e49fde4d26b989", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_30h_Female_Gnome_Death_01.prefab:e5758ee593be0dd428b09d57d53b6e16";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		if (entity.IsSpell())
		{
			string text = PopRandomLineWithChance(m_RandomHeroLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
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
		if (entity.IsSpell())
		{
			string text = PopRandomLineWithChance(m_RandomPlayerLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}
}
