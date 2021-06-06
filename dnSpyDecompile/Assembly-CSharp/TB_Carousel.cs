using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020005A8 RID: 1448
public class TB_Carousel : MissionEntity
{
	// Token: 0x06005083 RID: 20611 RVA: 0x001A77BE File Offset: 0x001A59BE
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06005084 RID: 20612 RVA: 0x001A77D0 File Offset: 0x001A59D0
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x06005085 RID: 20613 RVA: 0x001A7845 File Offset: 0x001A5A45
	private IEnumerator ShowArrow()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.playerNum = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		WidgetInstance arrow = new WidgetInstance();
		if (this.playerNum == 1)
		{
			arrow = WidgetInstance.Create("CarouselAArrows.prefab:1eb2af643b42e904bb83957e95320ba6", false);
		}
		else
		{
			arrow = WidgetInstance.Create("CarouselBArrows.prefab:5718b27c261c6654d887b62a406da354", false);
		}
		while (!arrow.IsReady)
		{
			yield return null;
		}
		arrow.transform.position = new Vector3(-12.78f, 0.67f, -10.9f);
		yield break;
	}

	// Token: 0x06005086 RID: 20614 RVA: 0x001A7854 File Offset: 0x001A5A54
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.playerNum = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, 0f);
		if (missionEvent == 11)
		{
			Debug.Log("Reached Mission Event 11");
			yield return this.ShowArrow();
		}
		if (this.m_popUpInfo.ContainsKey(missionEvent) && missionEvent == 10)
		{
			if (this.playerNum == 1)
			{
				GameState.Get().SetBusy(true);
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
			else
			{
				GameState.Get().SetBusy(true);
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][1]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				GameState.Get().SetBusy(false);
				popup = null;
			}
		}
		yield break;
	}

	// Token: 0x06005087 RID: 20615 RVA: 0x001A786A File Offset: 0x001A5A6A
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040046C6 RID: 18118
	private Notification StartPopup;

	// Token: 0x040046C7 RID: 18119
	private Notification ArrowPopup;

	// Token: 0x040046C8 RID: 18120
	private Vector3 popUpPos;

	// Token: 0x040046C9 RID: 18121
	private Player friendlySidePlayer;

	// Token: 0x040046CA RID: 18122
	private float popUpScale = 1.5f;

	// Token: 0x040046CB RID: 18123
	private int playerNum;

	// Token: 0x040046CC RID: 18124
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_CAROUSEL_A",
				"TB_CAROUSEL_B"
			}
		}
	};
}
