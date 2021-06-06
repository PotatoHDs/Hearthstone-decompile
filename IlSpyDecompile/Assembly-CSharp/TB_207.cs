using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_207 : MissionEntity
{
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[2] { "TB_207_INTRO_02", "TB_207_INTRO_01" }
		},
		{
			11,
			new string[1] { "TB_207_INTRO_01" }
		},
		{
			1,
			new string[10] { "", "TB_207_01", "TB_207_02", "TB_207_03", "TB_207_04", "TB_207_05", "TB_207_06", "TB_207_07", "TB_207_08", "TB_207_09" }
		}
	};

	private Player friendlySidePlayer;

	private Player opposingSidePlayer;

	private Entity playerEntity;

	private float popUpScale = 1.25f;

	private Vector3 popUpPos;

	private Notification StartPopup;

	private int brawl;

	private int yourBrawl;

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
	}

	public override void PreloadAssets()
	{
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

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		brawl = opposingSidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		yourBrawl = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		Debug.Log("Brawl # " + brawl);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		popUpPos = new Vector3(0f, 0f, -40f);
		if (m_popUpInfo.ContainsKey(missionEvent))
		{
			if (missionEvent == 10)
			{
				Notification popup3 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][1]) + "\n" + GameStrings.Get(m_popUpInfo[1][yourBrawl]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup3, 0f);
				yield return new WaitForSeconds(0.5f);
				popup3 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]) + "\n" + GameStrings.Get(m_popUpInfo[1][brawl]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup3, 0f);
			}
			if (missionEvent == 11)
			{
				Notification popup3 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]) + "\n" + GameStrings.Get(m_popUpInfo[1][yourBrawl]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup3, 0f);
			}
		}
	}

	private IEnumerator ShowPopup(string displayString)
	{
		StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(StartPopup, 7f);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(busy: false);
	}
}
