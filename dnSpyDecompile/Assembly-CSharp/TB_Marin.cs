using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
public class TB_Marin : MissionEntity
{
	// Token: 0x060050EC RID: 20716 RVA: 0x001A94A0 File Offset: 0x001A76A0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, 0f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
			yield return new WaitForSeconds(4f);
			NotificationManager.Get().DestroyNotification(popup, 0f);
			popup = null;
		}
		yield break;
	}

	// Token: 0x040047AE RID: 18350
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_MARIN_QUEST"
			}
		}
	};

	// Token: 0x040047AF RID: 18351
	private Entity playerEntity;

	// Token: 0x040047B0 RID: 18352
	private float popUpScale = 1f;

	// Token: 0x040047B1 RID: 18353
	private Vector3 popUpPos;

	// Token: 0x040047B2 RID: 18354
	private Notification StartPopup;
}
