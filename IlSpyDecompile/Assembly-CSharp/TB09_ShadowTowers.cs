using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB09_ShadowTowers : MissionEntity
{
	private Notification ShadowTowerPopup;

	private Notification MinionPopup;

	private Vector3 popUpPos;

	private string textID;

	private bool doPopup;

	private bool doLeftArrow;

	private bool doUpArrow;

	private bool doDownArrow;

	private float delayTime;

	private float popupDuration;

	private HashSet<int> seen = new HashSet<int>();

	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{ 1, "TB_SHADOWTOWERS_SHADOWSPAWNED" },
		{ 2, "TB_SHADOWTOWERS_SHADOWSPAWNED" },
		{ 3, "TB_SHADOWTOWERS_ADJACENTMINIONS" },
		{ 4, "TB_SHADOWTOWERS_SHADOWSPAWNEDNEXT" }
	};

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
		doPopup = false;
		doLeftArrow = false;
		doUpArrow = false;
		doDownArrow = false;
		switch (missionEvent)
		{
		case 1:
		case 2:
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				yield break;
			}
			doPopup = true;
			textID = minionMsgs[missionEvent];
			doLeftArrow = true;
			delayTime = 3f;
			popUpPos.x = 46f;
			popUpPos.z = -9f;
			popupDuration = 4f;
			if (!UniversalInputManager.UsePhoneUI)
			{
			}
			break;
		case 3:
		case 4:
			doPopup = true;
			textID = minionMsgs[missionEvent];
			delayTime = 0f;
			popUpPos.x = 0f;
			popUpPos.z = 20f;
			popupDuration = 3f;
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				popUpPos.z = -11f;
				if (missionEvent == 3)
				{
					doDownArrow = true;
				}
			}
			else if (missionEvent == 3)
			{
				doUpArrow = true;
			}
			if (!UniversalInputManager.UsePhoneUI)
			{
			}
			break;
		case 11:
			NotificationManager.Get().DestroyNotification(ShadowTowerPopup, 0f);
			doPopup = false;
			break;
		}
		if (doPopup)
		{
			yield return new WaitForSeconds(delayTime);
			float num = 1.5f;
			ShadowTowerPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * num, GameStrings.Get(textID), convertLegacyPosition: false);
			if (doLeftArrow)
			{
				ShadowTowerPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			if (doUpArrow)
			{
				ShadowTowerPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			}
			if (doDownArrow)
			{
				ShadowTowerPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
			NotificationManager.Get().DestroyNotification(ShadowTowerPopup, popupDuration);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
