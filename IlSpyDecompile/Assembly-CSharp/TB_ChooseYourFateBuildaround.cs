using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_ChooseYourFateBuildaround : MissionEntity
{
	private Notification ChooseYourFatePopup;

	private Vector3 popUpPos;

	private string textID;

	private string friendlyFate = "TB_PICKYOURFATE_BUILDAROUND_NEWFATE";

	private string opposingFate = "TB_PICKYOURFATE_BUILDAROUND_OPPONENTFATE";

	private HashSet<int> seen = new HashSet<int>();

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
		if (seen.Contains(missionEvent))
		{
			yield break;
		}
		seen.Add(missionEvent);
		popUpPos = new Vector3(0f, 0f, 0f);
		if ((uint)(missionEvent - 1) > 2u)
		{
			yield break;
		}
		if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
		{
			textID = friendlyFate;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.x = -75f;
				popUpPos.z = 30.5f;
			}
			else
			{
				popUpPos.x = -50.5f;
				popUpPos.z = 29f;
			}
		}
		else
		{
			textID = opposingFate;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.x = -34f;
				popUpPos.z = 12f;
			}
			else
			{
				popUpPos.x = -7f;
				popUpPos.z = 9f;
			}
		}
		yield return new WaitForSeconds(1f);
		ChooseYourFatePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID), convertLegacyPosition: false);
		PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
		NotificationManager.Get().DestroyNotification(ChooseYourFatePopup, 3f);
		ChooseYourFatePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
	}
}
