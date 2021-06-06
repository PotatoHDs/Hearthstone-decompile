using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005AB RID: 1451
public class TB_FireFest : MissionEntity
{
	// Token: 0x06005094 RID: 20628 RVA: 0x001A6C84 File Offset: 0x001A4E84
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06005095 RID: 20629 RVA: 0x001A7B42 File Offset: 0x001A5D42
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
		if (missionEvent == 99)
		{
			this.m_deaths++;
			if (this.m_deaths == 1)
			{
				yield return this.ShowPopup("TB_FIREFEST_FIRST", 7f, popUpPos);
			}
			else if (this.m_deaths == 7)
			{
				yield return this.ShowPopup("TB_FIREFEST_SECOND", 2.5f, popUpPos);
			}
		}
		yield break;
	}

	// Token: 0x06005096 RID: 20630 RVA: 0x001A7B58 File Offset: 0x001A5D58
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2.5f, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(4f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040046DD RID: 18141
	private Notification m_popup;

	// Token: 0x040046DE RID: 18142
	private int m_deaths;
}
