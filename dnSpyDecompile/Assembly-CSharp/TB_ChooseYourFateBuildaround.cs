using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C8 RID: 1480
public class TB_ChooseYourFateBuildaround : MissionEntity
{
	// Token: 0x06005160 RID: 20832 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06005161 RID: 20833 RVA: 0x001AC24D File Offset: 0x001AA44D
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
		this.popUpPos = new Vector3(0f, 0f, 0f);
		if (missionEvent - 1 <= 2)
		{
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				this.textID = this.friendlyFate;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.x = -75f;
					this.popUpPos.z = 30.5f;
				}
				else
				{
					this.popUpPos.x = -50.5f;
					this.popUpPos.z = 29f;
				}
			}
			else
			{
				this.textID = this.opposingFate;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.x = -34f;
					this.popUpPos.z = 12f;
				}
				else
				{
					this.popUpPos.x = -7f;
					this.popUpPos.z = 9f;
				}
			}
			yield return new WaitForSeconds(1f);
			this.ChooseYourFatePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.ChooseYourFatePopup, 3f);
			this.ChooseYourFatePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
		}
		yield break;
	}

	// Token: 0x040048BE RID: 18622
	private Notification ChooseYourFatePopup;

	// Token: 0x040048BF RID: 18623
	private Vector3 popUpPos;

	// Token: 0x040048C0 RID: 18624
	private string textID;

	// Token: 0x040048C1 RID: 18625
	private string friendlyFate = "TB_PICKYOURFATE_BUILDAROUND_NEWFATE";

	// Token: 0x040048C2 RID: 18626
	private string opposingFate = "TB_PICKYOURFATE_BUILDAROUND_OPPONENTFATE";

	// Token: 0x040048C3 RID: 18627
	private HashSet<int> seen = new HashSet<int>();
}
