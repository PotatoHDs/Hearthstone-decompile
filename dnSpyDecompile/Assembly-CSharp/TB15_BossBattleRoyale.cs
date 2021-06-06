using System;
using System.Collections;

// Token: 0x020005D4 RID: 1492
public class TB15_BossBattleRoyale : MissionEntity
{
	// Token: 0x06005198 RID: 20888 RVA: 0x001ACD48 File Offset: 0x001AAF48
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield break;
	}
}
