using System.Collections;
using System.Collections.Generic;

public class LOOT_Dungeon_BOSS_42h : LOOT_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_LOOTA_BOSS_42h_Female_Furbolg_Intro_01.prefab:e6a0b52c962983c489ab786bdd7b79f4", "VO_LOOTA_BOSS_42h_Female_Furbolg_EmoteResponse_01.prefab:1d5578d063a77c041ba0c134c626aced", "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor5_01.prefab:fd5a2bdaea56f7b4b883ef7334881533", "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor10_01.prefab:6fdf043dd2afda0468139c7de643902c", "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor15_01.prefab:df9cb4bc288d2f04fb2c31a63b98d136", "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor20_01.prefab:5024ee61e4f4e31468d1617148af5c76", "VO_LOOTA_BOSS_42h_Female_Furbolg_Death_01.prefab:7a7171be3063c424bacaae1f4986d0b1", "VO_LOOTA_BOSS_42h_Female_Furbolg_DefeatPlayer_01.prefab:43a63a4ab1d3d8f4d9a9df823f882209" })
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
		return "VO_LOOTA_BOSS_42h_Female_Furbolg_Death_01.prefab:7a7171be3063c424bacaae1f4986d0b1";
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_42h_Female_Furbolg_Intro_01.prefab:e6a0b52c962983c489ab786bdd7b79f4", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_42h_Female_Furbolg_EmoteResponse_01.prefab:1d5578d063a77c041ba0c134c626aced", Notification.SpeechBubbleDirection.TopRight, actor));
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
		string missionEventName = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(missionEventName))
		{
			yield return PlayLoyalSideKickBetrayal(missionEvent);
			switch (missionEvent)
			{
			case 101:
				m_playedLines.Add(missionEventName);
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor5_01.prefab:fd5a2bdaea56f7b4b883ef7334881533");
				break;
			case 102:
				m_playedLines.Add(missionEventName);
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor10_01.prefab:6fdf043dd2afda0468139c7de643902c");
				break;
			case 103:
				m_playedLines.Add(missionEventName);
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor15_01.prefab:df9cb4bc288d2f04fb2c31a63b98d136");
				break;
			case 104:
				m_playedLines.Add(missionEventName);
				yield return PlayLineOnlyOnce(enemyActor, "VO_LOOTA_BOSS_42h_Female_Furbolg_Armor20_01.prefab:5024ee61e4f4e31468d1617148af5c76");
				break;
			}
		}
	}
}
