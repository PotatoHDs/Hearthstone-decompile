using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200059D RID: 1437
public class FB_InnKeepersChoice : MissionEntity
{
	// Token: 0x06004FA8 RID: 20392 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06004FA9 RID: 20393 RVA: 0x001A259D File Offset: 0x001A079D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Vector3 popUpPos = default(Vector3);
		popUpPos.z = (UniversalInputManager.UsePhoneUI ? -66f : -44f);
		if (missionEvent == 2)
		{
			yield return this.ShowPopup("FB_IKC_CHOSE_SETUP", 3f, popUpPos);
		}
		if (missionEvent == 1)
		{
			yield return this.ShowPopup("FB_IKC_ENDGAME", 4f, popUpPos);
		}
		yield break;
	}

	// Token: 0x06004FAA RID: 20394 RVA: 0x001A25B3 File Offset: 0x001A07B3
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.FANCY);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040045C5 RID: 17861
	private Notification m_popup;
}
