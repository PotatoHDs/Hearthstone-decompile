using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_RagRaidScript : MissionEntity
{
	private Notification StartPopup;

	private Vector3 popUpPos;

	private Player friendlySidePlayer;

	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			14,
			new string[1] { "FB_RAGRAID_01" }
		},
		{
			15,
			new string[1] { "FB_LK_DEAD_02" }
		}
	};

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
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
		if (missionEvent == 14 && m_popUpInfo.ContainsKey(missionEvent))
		{
			yield return ShowPopup(m_popUpInfo[missionEvent][0]);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(6f);
			GameState.Get().SetBusy(busy: false);
			yield return ShowPopup(m_popUpInfo[15][0]);
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
