using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005BA RID: 1466
public class TB_MP_Crossroads : MissionEntity
{
	// Token: 0x060050F3 RID: 20723 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060050F4 RID: 20724 RVA: 0x001A952C File Offset: 0x001A772C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(-55f, 0f, -10f);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.popUpPos.x = -74f;
			this.popUpPos.z = -21f;
			this.popupScale = 1.75f;
		}
		if (missionEvent == 99)
		{
			yield return new WaitForSeconds(2f);
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				this.popUpPos.x = 55f;
				this.popUpPos.z = 19f;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.x = 75f;
					this.popUpPos.z = 17f;
				}
			}
			GameState.Get().SetBusy(true);
			this.CrasherPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popupScale, GameStrings.Get(this.crasherText), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.CrasherPopup, 3f);
			yield return new WaitForSeconds(1f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x040047B5 RID: 18357
	private Notification CrasherPopup;

	// Token: 0x040047B6 RID: 18358
	private string crasherText = "TB_MP_CROSSROADS";

	// Token: 0x040047B7 RID: 18359
	private Vector3 popUpPos;

	// Token: 0x040047B8 RID: 18360
	private float popupScale = 1.25f;
}
