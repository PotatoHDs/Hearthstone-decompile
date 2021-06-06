using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059C RID: 1436
public class FB_ELObrawl : MissionEntity
{
	// Token: 0x06004FA2 RID: 20386 RVA: 0x001A2493 File Offset: 0x001A0693
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004FA3 RID: 20387 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void PreloadAssets()
	{
	}

	// Token: 0x06004FA4 RID: 20388 RVA: 0x001A24A8 File Offset: 0x001A06A8
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

	// Token: 0x06004FA5 RID: 20389 RVA: 0x001A251D File Offset: 0x001A071D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		int isPlayerUnderdog = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			if (isPlayerUnderdog == 3)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][2]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			else if (isPlayerUnderdog == 1)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][1]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			else
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
		}
		yield break;
	}

	// Token: 0x06004FA6 RID: 20390 RVA: 0x001A2533 File Offset: 0x001A0733
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040045BE RID: 17854
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"FB_ELO_FAVORED",
				"FB_ELO_UNDERDOG",
				"FB_ELO_EVEN"
			}
		}
	};

	// Token: 0x040045BF RID: 17855
	private Player friendlySidePlayer;

	// Token: 0x040045C0 RID: 17856
	private Entity playerEntity;

	// Token: 0x040045C1 RID: 17857
	private int isPlayerHorseman;

	// Token: 0x040045C2 RID: 17858
	private float popUpScale = 1f;

	// Token: 0x040045C3 RID: 17859
	private Vector3 popUpPos;

	// Token: 0x040045C4 RID: 17860
	private Notification StartPopup;
}
