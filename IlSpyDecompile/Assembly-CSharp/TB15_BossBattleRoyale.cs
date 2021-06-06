using System.Collections;

public class TB15_BossBattleRoyale : MissionEntity
{
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
	}
}
