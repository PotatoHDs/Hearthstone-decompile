using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C9 RID: 1481
public class TB_ChooseYourFateRandom : MissionEntity
{
	// Token: 0x06005163 RID: 20835 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06005164 RID: 20836 RVA: 0x001AC28C File Offset: 0x001AA48C
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
		this.popUpPos = new Vector3(-46f, 0f, 0f);
		int entityId = GameState.Get().GetFriendlySidePlayer().GetEntityId();
		int entityId2 = GameState.Get().GetOpposingSidePlayer().GetEntityId();
		if (missionEvent > 1000)
		{
			int num = missionEvent - 1000;
			missionEvent -= num;
			if (num == entityId)
			{
				this.popUpPos.z = -44f;
				this.textID = this.newFate;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.x = -51f;
					this.popUpPos.z = -62f;
				}
			}
			if (num == entityId2)
			{
				this.popUpPos.z = 44f;
				this.textID = this.opponentFate;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.x = -51f;
					this.popUpPos.z = 53f;
				}
			}
		}
		int num2 = missionEvent;
		if (num2 - 1 > 19)
		{
			if (num2 == 1000)
			{
				this.ChooseYourFatePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
				base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
				NotificationManager.Get().DestroyNotification(this.ChooseYourFatePopup, 5f);
				this.ChooseYourFatePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			}
		}
		else
		{
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				this.textID = this.firstFate;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.x = -77f;
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
				this.textID = this.firstOpponenentFate;
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

	// Token: 0x040048C4 RID: 18628
	private Notification ChooseYourFatePopup;

	// Token: 0x040048C5 RID: 18629
	private Vector3 popUpPos;

	// Token: 0x040048C6 RID: 18630
	private string textID;

	// Token: 0x040048C7 RID: 18631
	private string newFate = "TB_PICKYOURFATE_RANDOM_NEWFATE";

	// Token: 0x040048C8 RID: 18632
	private string opponentFate = "TB_PICKYOURFATE_RANDOM_OPPONENTFATE";

	// Token: 0x040048C9 RID: 18633
	private string firstFate = "TB_PICKYOURFATE_RANDOM_FIRSTFATE";

	// Token: 0x040048CA RID: 18634
	private string firstOpponenentFate = "TB_PICKYOURFATE_BUILDAROUND_OPPONENT_FIRSTFATE";

	// Token: 0x040048CB RID: 18635
	private HashSet<int> seen = new HashSet<int>();
}
