using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D5 RID: 1493
public class TB16_MP_Stormwind : MissionEntity
{
	// Token: 0x0600519A RID: 20890 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x0600519B RID: 20891 RVA: 0x001ACD57 File Offset: 0x001AAF57
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (this.seen.Contains(missionEvent))
		{
			yield break;
		}
		this.seen.Add(missionEvent);
		this.doPopup = false;
		this.doLeftArrow = false;
		this.doUpArrow = false;
		this.doDownArrow = false;
		this.delayTime = 0f;
		this.popupDuration = 2.5f;
		this.textID = GameStrings.Get(TB16_MP_Stormwind.minionMsgs[missionEvent]);
		if (missionEvent == 10)
		{
			this.doPopup = true;
			this.popUpPos.x = 0f;
			this.popUpPos.z = 4f;
			if (UniversalInputManager.UsePhoneUI)
			{
			}
		}
		else
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(this.textID);
		}
		if (this.doPopup)
		{
			if (missionEvent == 1)
			{
				this.delayTime = 5f;
				this.popupDuration = 5f;
			}
			yield return new WaitForSeconds(this.delayTime);
			this.MyPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popupScale, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			if (this.doLeftArrow)
			{
				this.MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			if (this.doUpArrow)
			{
				this.MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			}
			if (this.doDownArrow)
			{
				this.MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.MyPopup, this.popupDuration);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04004917 RID: 18711
	private Notification MyPopup;

	// Token: 0x04004918 RID: 18712
	private Vector3 popUpPos;

	// Token: 0x04004919 RID: 18713
	private string textID;

	// Token: 0x0400491A RID: 18714
	private bool doPopup;

	// Token: 0x0400491B RID: 18715
	private bool doLeftArrow;

	// Token: 0x0400491C RID: 18716
	private bool doUpArrow;

	// Token: 0x0400491D RID: 18717
	private bool doDownArrow;

	// Token: 0x0400491E RID: 18718
	private float delayTime;

	// Token: 0x0400491F RID: 18719
	private float popupDuration = 2.5f;

	// Token: 0x04004920 RID: 18720
	private float popupScale = 2.5f;

	// Token: 0x04004921 RID: 18721
	private HashSet<int> seen = new HashSet<int>();

	// Token: 0x04004922 RID: 18722
	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{
			10,
			"TB_MP_STORMWIND_SUCCESS"
		},
		{
			11,
			"TB_MP_BOSS2"
		},
		{
			12,
			"TB_MP_BOSS3"
		},
		{
			13,
			"TB_MP_BOSS"
		}
	};
}
