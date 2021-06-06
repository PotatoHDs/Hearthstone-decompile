using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005CD RID: 1485
public class TB09_ShadowTowers : MissionEntity
{
	// Token: 0x06005173 RID: 20851 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06005174 RID: 20852 RVA: 0x001AC47D File Offset: 0x001AA67D
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
		if (missionEvent - 1 > 1)
		{
			if (missionEvent - 3 > 1)
			{
				if (missionEvent == 11)
				{
					NotificationManager.Get().DestroyNotification(this.ShadowTowerPopup, 0f);
					this.doPopup = false;
				}
			}
			else
			{
				this.doPopup = true;
				this.textID = TB09_ShadowTowers.minionMsgs[missionEvent];
				this.delayTime = 0f;
				this.popUpPos.x = 0f;
				this.popUpPos.z = 20f;
				this.popupDuration = 3f;
				if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
				{
					this.popUpPos.z = -11f;
					if (missionEvent == 3)
					{
						this.doDownArrow = true;
					}
				}
				else if (missionEvent == 3)
				{
					this.doUpArrow = true;
				}
				if (UniversalInputManager.UsePhoneUI)
				{
				}
			}
		}
		else
		{
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				yield break;
			}
			this.doPopup = true;
			this.textID = TB09_ShadowTowers.minionMsgs[missionEvent];
			this.doLeftArrow = true;
			this.delayTime = 3f;
			this.popUpPos.x = 46f;
			this.popUpPos.z = -9f;
			this.popupDuration = 4f;
			if (UniversalInputManager.UsePhoneUI)
			{
			}
		}
		if (this.doPopup)
		{
			yield return new WaitForSeconds(this.delayTime);
			float d = 1.5f;
			this.ShadowTowerPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * d, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			if (this.doLeftArrow)
			{
				this.ShadowTowerPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			if (this.doUpArrow)
			{
				this.ShadowTowerPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			}
			if (this.doDownArrow)
			{
				this.ShadowTowerPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.ShadowTowerPopup, this.popupDuration);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x040048E4 RID: 18660
	private Notification ShadowTowerPopup;

	// Token: 0x040048E5 RID: 18661
	private Notification MinionPopup;

	// Token: 0x040048E6 RID: 18662
	private Vector3 popUpPos;

	// Token: 0x040048E7 RID: 18663
	private string textID;

	// Token: 0x040048E8 RID: 18664
	private bool doPopup;

	// Token: 0x040048E9 RID: 18665
	private bool doLeftArrow;

	// Token: 0x040048EA RID: 18666
	private bool doUpArrow;

	// Token: 0x040048EB RID: 18667
	private bool doDownArrow;

	// Token: 0x040048EC RID: 18668
	private float delayTime;

	// Token: 0x040048ED RID: 18669
	private float popupDuration;

	// Token: 0x040048EE RID: 18670
	private HashSet<int> seen = new HashSet<int>();

	// Token: 0x040048EF RID: 18671
	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{
			1,
			"TB_SHADOWTOWERS_SHADOWSPAWNED"
		},
		{
			2,
			"TB_SHADOWTOWERS_SHADOWSPAWNED"
		},
		{
			3,
			"TB_SHADOWTOWERS_ADJACENTMINIONS"
		},
		{
			4,
			"TB_SHADOWTOWERS_SHADOWSPAWNEDNEXT"
		}
	};
}
