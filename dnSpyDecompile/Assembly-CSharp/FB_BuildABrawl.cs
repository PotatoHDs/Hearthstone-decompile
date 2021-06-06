using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000599 RID: 1433
public class FB_BuildABrawl : MissionEntity
{
	// Token: 0x06004F8E RID: 20366 RVA: 0x001A1C08 File Offset: 0x0019FE08
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004F8F RID: 20367 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void PreloadAssets()
	{
	}

	// Token: 0x06004F90 RID: 20368 RVA: 0x001A1C1C File Offset: 0x0019FE1C
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

	// Token: 0x06004F91 RID: 20369 RVA: 0x001A1C91 File Offset: 0x0019FE91
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.brawl = this.friendlySidePlayer.GetTag(GAME_TAG.SCORE_VALUE_3);
		Debug.Log("Brawl # " + this.brawl);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			if (missionEvent == 10)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]) + "\n" + GameStrings.Get(this.m_popUpInfo[1][this.brawl]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			if (missionEvent == 11)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
		}
		yield break;
	}

	// Token: 0x06004F92 RID: 20370 RVA: 0x001A1CA7 File Offset: 0x0019FEA7
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040045B0 RID: 17840
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"FB_BUILDABRAWL_INTRO"
			}
		},
		{
			11,
			new string[]
			{
				"FB_BUILDABRAWL_NOTSTARTED"
			}
		},
		{
			1,
			new string[]
			{
				"",
				"FB_BUILDABRAWL_001",
				"FB_BUILDABRAWL_002",
				"FB_BUILDABRAWL_003",
				"FB_BUILDABRAWL_004",
				"FB_BUILDABRAWL_005",
				"FB_BUILDABRAWL_006",
				"FB_BUILDABRAWL_007",
				"FB_BUILDABRAWL_008",
				"FB_BUILDABRAWL_009",
				"FB_BUILDABRAWL_010",
				"FB_BUILDABRAWL_011",
				"FB_BUILDABRAWL_012",
				"FB_BUILDABRAWL_013",
				"FB_BUILDABRAWL_014",
				"FB_BUILDABRAWL_015",
				"FB_BUILDABRAWL_016"
			}
		}
	};

	// Token: 0x040045B1 RID: 17841
	private Player friendlySidePlayer;

	// Token: 0x040045B2 RID: 17842
	private Entity playerEntity;

	// Token: 0x040045B3 RID: 17843
	private float popUpScale = 1.25f;

	// Token: 0x040045B4 RID: 17844
	private Vector3 popUpPos;

	// Token: 0x040045B5 RID: 17845
	private Notification StartPopup;

	// Token: 0x040045B6 RID: 17846
	private int brawl;
}
