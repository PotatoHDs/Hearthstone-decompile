using System.Collections;
using UnityEngine;

public class TB_MP_Crossroads : MissionEntity
{
	private Notification CrasherPopup;

	private string crasherText = "TB_MP_CROSSROADS";

	private Vector3 popUpPos;

	private float popupScale = 1.25f;

	private void Start()
	{
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(-55f, 0f, -10f);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			popUpPos.x = -74f;
			popUpPos.z = -21f;
			popupScale = 1.75f;
		}
		if (missionEvent != 99)
		{
			yield break;
		}
		yield return new WaitForSeconds(2f);
		if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
		{
			popUpPos.x = 55f;
			popUpPos.z = 19f;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.x = 75f;
				popUpPos.z = 17f;
			}
		}
		GameState.Get().SetBusy(busy: true);
		CrasherPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(crasherText), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(CrasherPopup, 3f);
		yield return new WaitForSeconds(1f);
		GameState.Get().SetBusy(busy: false);
	}
}
