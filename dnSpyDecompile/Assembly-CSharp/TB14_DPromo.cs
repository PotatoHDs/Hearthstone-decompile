using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D3 RID: 1491
public class TB14_DPromo : MissionEntity
{
	// Token: 0x06005194 RID: 20884 RVA: 0x001ACC94 File Offset: 0x001AAE94
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
		base.PreloadSound("CowKing_TB_SPT_DPromo_Hero2_Play.wav:d4884afd0de894f618c37a00901e0258");
		base.PreloadSound("CowKing_TB_SPT_DPromo_Hero2_Death.wav:96e41f1a7ed1747e0b8ca7feb8312585");
		base.PreloadSound("HellBovine_TB_SPT_DPromoMinion_Attack.wav:e0b94995a3c774aaf86c35c2f6f9968f");
		base.PreloadSound("HellBovine_TB_SPT_DPromoMinion_Death.wav:7c64102817d15435a9319ca137fb4d5a");
		base.PreloadSound("HellBovine_TB_SPT_DPromoMinion_Play.wav:22be52fa77e13486ab76a4266aa1a815");
	}

	// Token: 0x06005195 RID: 20885 RVA: 0x001ACCE3 File Offset: 0x001AAEE3
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
		if (missionEvent == 100)
		{
			NotificationManager.Get().DestroyNotification(this.MyPopup, 0f);
			this.doPopup = false;
		}
		else
		{
			this.doPopup = true;
			if (TB14_DPromo.minionMsgs.ContainsKey(missionEvent))
			{
				this.textID = TB14_DPromo.minionMsgs[missionEvent];
			}
			else
			{
				this.textID = TB14_DPromo.minionMsgs[2];
			}
			this.popUpPos.x = 0f;
			this.popUpPos.z = 4f;
			UniversalInputManager.UsePhoneUI;
			if (missionEvent == 10)
			{
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(GameStrings.Get("TB_DPROMO_2NDHERO"));
			}
			if (missionEvent == 11)
			{
				Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(GameStrings.Get("TB_DPROMO_2NDHERO"));
			}
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

	// Token: 0x0400490B RID: 18699
	private Notification MyPopup;

	// Token: 0x0400490C RID: 18700
	private Vector3 popUpPos;

	// Token: 0x0400490D RID: 18701
	private string textID;

	// Token: 0x0400490E RID: 18702
	private bool doPopup;

	// Token: 0x0400490F RID: 18703
	private bool doLeftArrow;

	// Token: 0x04004910 RID: 18704
	private bool doUpArrow;

	// Token: 0x04004911 RID: 18705
	private bool doDownArrow;

	// Token: 0x04004912 RID: 18706
	private float delayTime;

	// Token: 0x04004913 RID: 18707
	private float popupDuration = 2.5f;

	// Token: 0x04004914 RID: 18708
	private float popupScale = 2.5f;

	// Token: 0x04004915 RID: 18709
	private HashSet<int> seen = new HashSet<int>();

	// Token: 0x04004916 RID: 18710
	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{
			10,
			"TB_DPROMO_2NDHEROPOPUP"
		},
		{
			11,
			"TB_DPROMO_2NDHEROPOPUP"
		}
	};
}
