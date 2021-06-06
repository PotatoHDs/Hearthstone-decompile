using System.Collections;
using UnityEngine;

public class TB07_FactionWars : MissionEntity
{
	private Notification GameOverPopup;

	private string textID;

	private Vector3 popUpPos;

	public override void PreloadAssets()
	{
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		textID = "";
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 1)
		{
			textID = "TB_SINGLEPLAYERTRIAL_PLAYERDIED";
		}
		if (textID.Length > 0)
		{
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(busy: false);
			popUpPos = new Vector3(1.27f, 0f, 19f);
			GameOverPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.25f, GameStrings.Get(textID), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(GameOverPopup, 4f);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
