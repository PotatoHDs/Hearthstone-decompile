using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB14_DPromo : MissionEntity
{
	private Notification MyPopup;

	private Vector3 popUpPos;

	private string textID;

	private bool doPopup;

	private bool doLeftArrow;

	private bool doUpArrow;

	private bool doDownArrow;

	private float delayTime;

	private float popupDuration = 2.5f;

	private float popupScale = 2.5f;

	private HashSet<int> seen = new HashSet<int>();

	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{ 10, "TB_DPROMO_2NDHEROPOPUP" },
		{ 11, "TB_DPROMO_2NDHEROPOPUP" }
	};

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
		PreloadSound("CowKing_TB_SPT_DPromo_Hero2_Play.wav:d4884afd0de894f618c37a00901e0258");
		PreloadSound("CowKing_TB_SPT_DPromo_Hero2_Death.wav:96e41f1a7ed1747e0b8ca7feb8312585");
		PreloadSound("HellBovine_TB_SPT_DPromoMinion_Attack.wav:e0b94995a3c774aaf86c35c2f6f9968f");
		PreloadSound("HellBovine_TB_SPT_DPromoMinion_Death.wav:7c64102817d15435a9319ca137fb4d5a");
		PreloadSound("HellBovine_TB_SPT_DPromoMinion_Play.wav:22be52fa77e13486ab76a4266aa1a815");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (seen.Contains(missionEvent))
		{
			yield break;
		}
		seen.Add(missionEvent);
		doPopup = false;
		doLeftArrow = false;
		doUpArrow = false;
		doDownArrow = false;
		delayTime = 0f;
		popupDuration = 2.5f;
		if (missionEvent == 100)
		{
			NotificationManager.Get().DestroyNotification(MyPopup, 0f);
			doPopup = false;
		}
		else
		{
			doPopup = true;
			if (minionMsgs.ContainsKey(missionEvent))
			{
				textID = minionMsgs[missionEvent];
			}
			else
			{
				textID = minionMsgs[2];
			}
			popUpPos.x = 0f;
			popUpPos.z = 4f;
			_ = (bool)UniversalInputManager.UsePhoneUI;
			if (missionEvent == 10)
			{
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(GameStrings.Get("TB_DPROMO_2NDHERO"));
			}
			if (missionEvent == 11)
			{
				Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(GameStrings.Get("TB_DPROMO_2NDHERO"));
			}
		}
		if (doPopup)
		{
			if (missionEvent == 1)
			{
				delayTime = 5f;
				popupDuration = 5f;
			}
			yield return new WaitForSeconds(delayTime);
			MyPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(textID), convertLegacyPosition: false);
			if (doLeftArrow)
			{
				MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			if (doUpArrow)
			{
				MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			}
			if (doDownArrow)
			{
				MyPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
			NotificationManager.Get().DestroyNotification(MyPopup, popupDuration);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
