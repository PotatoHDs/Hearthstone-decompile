using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB16_MP_Stormwind : MissionEntity
{
	private Notification MyPopup;

	private Vector3 popUpPos;

	private string textID;

	private bool doPopup;

	private bool doLeftArrow;

	private bool doUpArrow;

	private bool doDownArrow;

	private float delayTime;

	private float popupDuration = 2.5f;

	private float popupScale = 2.5f;

	private HashSet<int> seen = new HashSet<int>();

	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{ 10, "TB_MP_STORMWIND_SUCCESS" },
		{ 11, "TB_MP_BOSS2" },
		{ 12, "TB_MP_BOSS3" },
		{ 13, "TB_MP_BOSS" }
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
		delayTime = 0f;
		popupDuration = 2.5f;
		textID = GameStrings.Get(minionMsgs[missionEvent]);
		if (missionEvent == 10)
		{
			doPopup = true;
			popUpPos.x = 0f;
			popUpPos.z = 4f;
			if (!UniversalInputManager.UsePhoneUI)
			{
			}
		}
		else
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(textID);
		}
		if (doPopup)
		{
			if (missionEvent == 1)
			{
				delayTime = 5f;
				popupDuration = 5f;
			}
			yield return new WaitForSeconds(delayTime);
			MyPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(textID), convertLegacyPosition: false);
			if (doLeftArrow)
			{
				MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			if (doUpArrow)
			{
				MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			}
			if (doDownArrow)
			{
				MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
			NotificationManager.Get().DestroyNotification(MyPopup, popupDuration);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
