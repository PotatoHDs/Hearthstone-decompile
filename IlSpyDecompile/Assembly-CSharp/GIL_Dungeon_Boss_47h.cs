using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_47h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomLines = new List<string> { "VO_GILA_BOSS_47h_Male_Monster_HeroPower_01.prefab:b26250c73e5df194fa2cd34852999952", "VO_GILA_BOSS_47h_Male_Monster_HeroPower_02.prefab:ac052081d4ffc8940830be182ae0a5e1", "VO_GILA_BOSS_47h_Male_Monster_HeroPower_03.prefab:fcd5d6d4a4d62fa4f99bc2592b23a573" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_47h_Male_Monster_IntroALT_01.prefab:e5a8c96c1c0518a4db0b59172a471a3b", "VO_GILA_BOSS_47h_Male_Monster_EmoteResponse_01.prefab:0dd0da0712e288b4bbb226eca3eb8e3f", "VO_GILA_BOSS_47h_Male_Monster_Death_01.prefab:fd4b005d9ff09134ba3f48da2397786d", "VO_GILA_BOSS_47h_Male_Monster_HeroPower_01.prefab:b26250c73e5df194fa2cd34852999952", "VO_GILA_BOSS_47h_Male_Monster_HeroPower_02.prefab:ac052081d4ffc8940830be182ae0a5e1", "VO_GILA_BOSS_47h_Male_Monster_HeroPower_03.prefab:fcd5d6d4a4d62fa4f99bc2592b23a573" })
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_47h_Male_Monster_IntroALT_01.prefab:e5a8c96c1c0518a4db0b59172a471a3b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_47h_Male_Monster_EmoteResponse_01.prefab:0dd0da0712e288b4bbb226eca3eb8e3f", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_47h_Male_Monster_Death_01.prefab:fd4b005d9ff09134ba3f48da2397786d";
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
		if (entity.HasTag(GAME_TAG.BATTLECRY))
		{
			string text = PopRandomLineWithChance(m_RandomLines);
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
		if (entity.HasTag(GAME_TAG.BATTLECRY))
		{
			string text = PopRandomLineWithChance(m_RandomLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}
}
