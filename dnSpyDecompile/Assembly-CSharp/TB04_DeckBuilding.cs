using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005CA RID: 1482
public class TB04_DeckBuilding : MissionEntity
{
	// Token: 0x06005166 RID: 20838 RVA: 0x0010C851 File Offset: 0x0010AA51
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			}
		};
	}

	// Token: 0x06005167 RID: 20839 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06005168 RID: 20840 RVA: 0x001AC2E1 File Offset: 0x001AA4E1
	public TB04_DeckBuilding()
	{
		this.m_gameOptions.AddOptions(TB04_DeckBuilding.s_booleanOptions, TB04_DeckBuilding.s_stringOptions);
	}

	// Token: 0x06005169 RID: 20841 RVA: 0x001A6C84 File Offset: 0x001A4E84
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x0600516A RID: 20842 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x0600516B RID: 20843 RVA: 0x001AC2FE File Offset: 0x001AA4FE
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, 0f);
		switch (missionEvent)
		{
		case 1:
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				yield break;
			}
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			this.textID = "TB_DECKBUILDING_FIRSTPICKTHREE";
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
			}
			else
			{
				this.popUpPos.z = -44f;
			}
			this.PickThreePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.25f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.PickThreePopup, 12f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1f);
			GameState.Get().SetBusy(false);
			break;
		case 2:
			if (this.PickThreePopup)
			{
				NotificationManager.Get().DestroyNotification(this.PickThreePopup, 0.25f);
			}
			break;
		case 3:
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				yield break;
			}
			this.textID = "TB_DECKBUILDING_FIRSTENDTURN";
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.x = 82f;
				this.popUpPos.z = -28f;
			}
			else
			{
				this.popUpPos.z = -36f;
			}
			GameState.Get().SetBusy(true);
			this.EndOfTurnPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.EndOfTurnPopup, 5f);
			this.EndOfTurnPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			NotificationManager.Get().DestroyNotification(this.CardPlayedPopup, 0f);
			yield return new WaitForSeconds(3.5f);
			GameState.Get().SetBusy(false);
			break;
		case 4:
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				yield break;
			}
			this.textID = "TB_DECKBUILDING_FIRSTCARDPLAYED";
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.x = 82f;
				this.popUpPos.y = 0f;
				this.popUpPos.z = -28f;
			}
			else
			{
				this.popUpPos.x = 51f;
				this.popUpPos.y = 0f;
				this.popUpPos.z = -15.5f;
			}
			GameState.Get().SetBusy(true);
			this.CardPlayedPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			this.CardPlayedPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.CardPlayedPopup, 10f);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(false);
			if (this.CardPlayedPopup != null)
			{
				iTween.PunchScale(this.CardPlayedPopup.gameObject, iTween.Hash(new object[]
				{
					"amount",
					new Vector3(2f, 2f, 2f),
					"time",
					1f
				}));
			}
			break;
		case 10:
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents(false);
			}
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			this.textID = "TB_DECKBUILDING_STARTOFGAME";
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.x = 0f;
				this.popUpPos.y = 0f;
				this.popUpPos.z = 0f;
			}
			else
			{
				this.popUpPos.x = 0f;
				this.popUpPos.y = 0f;
				this.popUpPos.z = 0f;
			}
			GameState.Get().SetBusy(true);
			this.StartOfTurnPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.StartOfTurnPopup, 3f);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(false);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(3f);
			GameState.Get().SetBusy(false);
			break;
		case 11:
			NotificationManager.Get().DestroyNotification(this.StartOfTurnPopup, 0f);
			NotificationManager.Get().DestroyNotification(this.EndOfTurnPopup, 0f);
			NotificationManager.Get().DestroyNotification(this.CardPlayedPopup, 0f);
			NotificationManager.Get().DestroyNotification(this.PickThreePopup, 0f);
			break;
		}
		yield break;
	}

	// Token: 0x040048CC RID: 18636
	private static Map<GameEntityOption, bool> s_booleanOptions = TB04_DeckBuilding.InitBooleanOptions();

	// Token: 0x040048CD RID: 18637
	private static Map<GameEntityOption, string> s_stringOptions = TB04_DeckBuilding.InitStringOptions();

	// Token: 0x040048CE RID: 18638
	private Notification PickThreePopup;

	// Token: 0x040048CF RID: 18639
	private Notification EndOfTurnPopup;

	// Token: 0x040048D0 RID: 18640
	private Notification StartOfTurnPopup;

	// Token: 0x040048D1 RID: 18641
	private Notification CardPlayedPopup;

	// Token: 0x040048D2 RID: 18642
	private Vector3 popUpPos;

	// Token: 0x040048D3 RID: 18643
	private string textID;
}
