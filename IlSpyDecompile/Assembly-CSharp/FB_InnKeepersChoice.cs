using System.Collections;
using UnityEngine;

public class FB_InnKeepersChoice : MissionEntity
{
	private Notification m_popup;

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Vector3 popUpPos = default(Vector3);
		popUpPos.z = (UniversalInputManager.UsePhoneUI ? (-66f) : (-44f));
		if (missionEvent == 2)
		{
			yield return ShowPopup("FB_IKC_CHOSE_SETUP", 3f, popUpPos);
		}
		if (missionEvent == 1)
		{
			yield return ShowPopup("FB_IKC_ENDGAME", 4f, popUpPos);
		}
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(stringID), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(busy: false);
	}
}
