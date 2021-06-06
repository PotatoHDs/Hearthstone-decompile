using System.Collections;
using UnityEngine;

public class TB_FireFest : MissionEntity
{
	private Notification m_popup;

	private int m_deaths;

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
		if (m_deaths > 7)
		{
			yield break;
		}
		Vector3 popUpPos = default(Vector3);
		popUpPos.z = (UniversalInputManager.UsePhoneUI ? (-66f) : (-44f));
		if (missionEvent == 99)
		{
			m_deaths++;
			if (m_deaths == 1)
			{
				yield return ShowPopup("TB_FIREFEST_FIRST", 7f, popUpPos);
			}
			else if (m_deaths == 7)
			{
				yield return ShowPopup("TB_FIREFEST_SECOND", 2.5f, popUpPos);
			}
		}
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2.5f, GameStrings.Get(stringID), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(4f);
		GameState.Get().SetBusy(busy: false);
	}
}
