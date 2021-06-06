using System;
using System.Collections;

// Token: 0x02000582 RID: 1410
public class NAX_MissionEntity : MissionEntity
{
	// Token: 0x06004ED1 RID: 20177 RVA: 0x0019EF01 File Offset: 0x0019D101
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		switch (missionEvent)
		{
		case 1001:
			NotificationManager.Get().CreateKTQuote("VO_KT_ANTI_CHEESE1_65", "VO_KT_ANTI_CHEESE1_65.prefab:51ed3d983ebd14f48a297c8544ca665c", true);
			break;
		case 1002:
			NotificationManager.Get().CreateKTQuote("VO_KT_ANTI_CHEESE2_66", "VO_KT_ANTI_CHEESE2_66.prefab:58cc578984c0e6c42813763a44b0be4f", true);
			break;
		case 1003:
			NotificationManager.Get().CreateKTQuote("VO_KT_ANTI_CHEESE3_67", "VO_KT_ANTI_CHEESE3_67.prefab:8305e1f38de470346bc1b5a4dbcc286c", true);
			break;
		}
		yield break;
	}
}
