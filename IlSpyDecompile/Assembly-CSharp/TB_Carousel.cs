using System.Collections;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class TB_Carousel : MissionEntity
{
	private Notification StartPopup;

	private Notification ArrowPopup;

	private Vector3 popUpPos;

	private Player friendlySidePlayer;

	private float popUpScale = 1.5f;

	private int playerNum;

	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]> { 
	{
		10,
		new string[2] { "TB_CAROUSEL_A", "TB_CAROUSEL_B" }
	} };

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	private void SetPopupPosition()
	{
		if (friendlySidePlayer.IsCurrentPlayer())
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
		}
		else if ((bool)UniversalInputManager.UsePhoneUI)
		{
			popUpPos.z = 66f;
		}
		else
		{
			popUpPos.z = 44f;
		}
	}

	private IEnumerator ShowArrow()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		playerNum = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		new WidgetInstance();
		WidgetInstance arrow = ((playerNum != 1) ? WidgetInstance.Create("CarouselBArrows.prefab:5718b27c261c6654d887b62a406da354") : WidgetInstance.Create("CarouselAArrows.prefab:1eb2af643b42e904bb83957e95320ba6"));
		while (!arrow.IsReady)
		{
			yield return null;
		}
		arrow.transform.position = new Vector3(-12.78f, 0.67f, -10.9f);
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		playerNum = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(0f, 0f, 0f);
		if (missionEvent == 11)
		{
			Debug.Log("Reached Mission Event 11");
			yield return ShowArrow();
		}
		if (m_popUpInfo.ContainsKey(missionEvent) && missionEvent == 10)
		{
			if (playerNum == 1)
			{
				GameState.Get().SetBusy(busy: true);
				Notification popup2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(5f);
				NotificationManager.Get().DestroyNotification(popup2, 0f);
				GameState.Get().SetBusy(busy: false);
			}
			else
			{
				GameState.Get().SetBusy(busy: true);
				Notification popup2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][1]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup2, 0f);
				GameState.Get().SetBusy(busy: false);
			}
		}
	}

	private IEnumerator ShowPopup(string displayString)
	{
		StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(displayString), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(StartPopup, 7f);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(busy: false);
	}
}
