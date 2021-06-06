using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_Marin : MissionEntity
{
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]> { 
	{
		10,
		new string[1] { "TB_MARIN_QUEST" }
	} };

	private Entity playerEntity;

	private float popUpScale = 1f;

	private Vector3 popUpPos;

	private Notification StartPopup;

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(0f, 0f, 0f);
		if (m_popUpInfo.ContainsKey(missionEvent))
		{
			Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
			yield return new WaitForSeconds(4f);
			NotificationManager.Get().DestroyNotification(popup, 0f);
		}
	}
}
