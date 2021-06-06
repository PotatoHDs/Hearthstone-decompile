using System.Collections;
using UnityEngine;

public class TB04_DeckBuilding : MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification PickThreePopup;

	private Notification EndOfTurnPopup;

	private Notification StartOfTurnPopup;

	private Notification CardPlayedPopup;

	private Vector3 popUpPos;

	private string textID;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.HANDLE_COIN,
			false
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public TB04_DeckBuilding()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(0f, 0f, 0f);
		switch (missionEvent)
		{
		case 1:
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(busy: false);
				textID = "TB_DECKBUILDING_FIRSTPICKTHREE";
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.z = -66f;
				}
				else
				{
					popUpPos.z = -44f;
				}
				PickThreePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.25f, GameStrings.Get(textID), convertLegacyPosition: false);
				PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
				NotificationManager.Get().DestroyNotification(PickThreePopup, 12f);
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(1f);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 2:
			if ((bool)PickThreePopup)
			{
				NotificationManager.Get().DestroyNotification(PickThreePopup, 0.25f);
			}
			break;
		case 3:
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				textID = "TB_DECKBUILDING_FIRSTENDTURN";
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.x = 82f;
					popUpPos.z = -28f;
				}
				else
				{
					popUpPos.z = -36f;
				}
				GameState.Get().SetBusy(busy: true);
				EndOfTurnPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID), convertLegacyPosition: false);
				PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
				NotificationManager.Get().DestroyNotification(EndOfTurnPopup, 5f);
				EndOfTurnPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
				NotificationManager.Get().DestroyNotification(CardPlayedPopup, 0f);
				yield return new WaitForSeconds(3.5f);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 4:
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				textID = "TB_DECKBUILDING_FIRSTCARDPLAYED";
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					popUpPos.x = 82f;
					popUpPos.y = 0f;
					popUpPos.z = -28f;
				}
				else
				{
					popUpPos.x = 51f;
					popUpPos.y = 0f;
					popUpPos.z = -15.5f;
				}
				GameState.Get().SetBusy(busy: true);
				CardPlayedPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(textID), convertLegacyPosition: false);
				CardPlayedPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
				NotificationManager.Get().DestroyNotification(CardPlayedPopup, 10f);
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(busy: false);
				if (CardPlayedPopup != null)
				{
					iTween.PunchScale(CardPlayedPopup.gameObject, iTween.Hash("amount", new Vector3(2f, 2f, 2f), "time", 1f));
				}
			}
			break;
		case 10:
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents();
			}
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			textID = "TB_DECKBUILDING_STARTOFGAME";
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.x = 0f;
				popUpPos.y = 0f;
				popUpPos.z = 0f;
			}
			else
			{
				popUpPos.x = 0f;
				popUpPos.y = 0f;
				popUpPos.z = 0f;
			}
			GameState.Get().SetBusy(busy: true);
			StartOfTurnPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get(textID), convertLegacyPosition: false);
			PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
			NotificationManager.Get().DestroyNotification(StartOfTurnPopup, 3f);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(busy: false);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(busy: false);
			break;
		case 11:
			NotificationManager.Get().DestroyNotification(StartOfTurnPopup, 0f);
			NotificationManager.Get().DestroyNotification(EndOfTurnPopup, 0f);
			NotificationManager.Get().DestroyNotification(CardPlayedPopup, 0f);
			NotificationManager.Get().DestroyNotification(PickThreePopup, 0f);
			break;
		}
	}
}
