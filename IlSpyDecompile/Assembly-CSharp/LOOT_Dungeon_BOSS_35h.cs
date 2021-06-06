using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon_BOSS_35h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_CounteredLines = new List<string> { "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered1_01.prefab:25a7fb7a7b8b3814daf55fb23ea9413e", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered2_01.prefab:8b79ec33b480b80449e68711c8d26a03", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered3_01.prefab:347ceccfb0763f944998fef97df721c7", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered4_01.prefab:ec23b2e7ba01cb441a3ce3add0361e72", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered5_01.prefab:c39f53ea68577a74099dace6e5fb7f69" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_35h_Male_Furbolg_Intro_01.prefab:f6f44292c06355449ab33c49b87fd051", "VO_LOOTA_BOSS_35h_Male_Furbolg_EmoteResponse_01.prefab:c01bc9067117b8142b96c8b60fe96b9d", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered1_01.prefab:25a7fb7a7b8b3814daf55fb23ea9413e", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered2_01.prefab:8b79ec33b480b80449e68711c8d26a03", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered3_01.prefab:347ceccfb0763f944998fef97df721c7", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered4_01.prefab:ec23b2e7ba01cb441a3ce3add0361e72", "VO_LOOTA_BOSS_35h_Male_Furbolg_SpellCountered5_01.prefab:c39f53ea68577a74099dace6e5fb7f69", "VO_LOOTA_BOSS_35h_Male_Furbolg_Death_01.prefab:044f0aa3de369464db7a3577780cf268", "VO_LOOTA_BOSS_35h_Male_Furbolg_DefeatPlayer_01.prefab:3ceb3d9d755224f4e917d571e6a93943" })
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
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_35h_Male_Furbolg_Death_01.prefab:044f0aa3de369464db7a3577780cf268";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_35h_Male_Furbolg_Intro_01.prefab:f6f44292c06355449ab33c49b87fd051", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_35h_Male_Furbolg_EmoteResponse_01.prefab:c01bc9067117b8142b96c8b60fe96b9d", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		yield return PlayLoyalSideKickBetrayal(missionEvent);
		if (missionEvent == 101)
		{
			int num = 30;
			int num2 = Random.Range(0, 100);
			if (m_CounteredLines.Count != 0 && num >= num2)
			{
				string randomLine = m_CounteredLines[Random.Range(0, m_CounteredLines.Count)];
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
				m_CounteredLines.Remove(randomLine);
				yield return null;
			}
		}
	}
}
