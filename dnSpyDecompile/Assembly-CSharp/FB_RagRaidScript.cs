using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class FB_RagRaidScript : MissionEntity
{
	// Token: 0x06004FAC RID: 20396 RVA: 0x001A25D7 File Offset: 0x001A07D7
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004FAD RID: 20397 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void PreloadAssets()
	{
	}

	// Token: 0x06004FAE RID: 20398 RVA: 0x001A25EC File Offset: 0x001A07EC
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

	// Token: 0x06004FAF RID: 20399 RVA: 0x001A2661 File Offset: 0x001A0861
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 14 && this.m_popUpInfo.ContainsKey(missionEvent))
		{
			yield return this.ShowPopup(this.m_popUpInfo[missionEvent][0]);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(6f);
			GameState.Get().SetBusy(false);
			yield return this.ShowPopup(this.m_popUpInfo[15][0]);
		}
		yield break;
	}

	// Token: 0x06004FB0 RID: 20400 RVA: 0x001A2677 File Offset: 0x001A0877
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040045C6 RID: 17862
	private Notification StartPopup;

	// Token: 0x040045C7 RID: 17863
	private Vector3 popUpPos;

	// Token: 0x040045C8 RID: 17864
	private Player friendlySidePlayer;

	// Token: 0x040045C9 RID: 17865
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			14,
			new string[]
			{
				"FB_RAGRAID_01"
			}
		},
		{
			15,
			new string[]
			{
				"FB_LK_DEAD_02"
			}
		}
	};
}
