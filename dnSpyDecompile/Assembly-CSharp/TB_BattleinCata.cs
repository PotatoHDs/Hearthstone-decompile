using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005A6 RID: 1446
public class TB_BattleinCata : MissionEntity
{
	// Token: 0x06005072 RID: 20594 RVA: 0x001A6C84 File Offset: 0x001A4E84
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06005073 RID: 20595 RVA: 0x001A6C91 File Offset: 0x001A4E91
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (this.m_deaths > 7)
		{
			yield break;
		}
		Vector3 popUpPos = default(Vector3);
		popUpPos.z = (UniversalInputManager.UsePhoneUI ? -66f : -44f);
		if (missionEvent == 199)
		{
			this.m_deaths++;
			if (this.m_deaths == 1)
			{
				yield return this.ShowPopup("TB_BATTLEINCATA_PALLADIN", 7f, popUpPos);
			}
		}
		yield break;
	}

	// Token: 0x06005074 RID: 20596 RVA: 0x001A6CA7 File Offset: 0x001A4EA7
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2.5f, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(4f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040046B0 RID: 18096
	private Notification m_popup;

	// Token: 0x040046B1 RID: 18097
	private int m_deaths;
}
