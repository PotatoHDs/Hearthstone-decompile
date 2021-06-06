using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_ChooseYourFateRandom : MissionEntity
{
	private Notification ChooseYourFatePopup;

	private Vector3 popUpPos;

	private string textID;

	private string newFate = "TB_PICKYOURFATE_RANDOM_NEWFATE";

	private string opponentFate = "TB_PICKYOURFATE_RANDOM_OPPONENTFATE";

	private string firstFate = "TB_PICKYOURFATE_RANDOM_FIRSTFATE";

	private string firstOpponenentFate = "TB_PICKYOURFATE_BUILDAROUND_OPPONENT_FIRSTFATE";

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
		popUpPos = new Vector3(-46f, 0f, 0f);
		int entityId = GameState.Get().GetFriendlySidePlayer().GetEntityId();
		int entityId2 = GameState.Get().GetOpposingSidePlayer().GetEntityId();
		if (missionEvent > 1000)
		{
			int num = missionEvent - 1000;
			missionEvent -= num;
			if (num == entityId)
			{
				popUpPos.z = -44f;
				textID = newFate;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.x = -51f;
					popUpPos.z = -62f;
				}
			}
			if (num == entityId2)
			{
				popUpPos.z = 44f;
				textID = opponentFate;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.x = -51f;
					popUpPos.z = 53f;
				}
			}
		}
		switch (missionEvent)
		{
		case 1000:
			ChooseYourFatePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID), convertLegacyPosition: false);
			PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
			NotificationManager.Get().DestroyNotification(ChooseYourFatePopup, 5f);
			ChooseYourFatePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			break;
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 19:
		case 20:
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				textID = firstFate;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.x = -77f;
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
				textID = firstOpponenentFate;
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
			break;
		}
	}
}
