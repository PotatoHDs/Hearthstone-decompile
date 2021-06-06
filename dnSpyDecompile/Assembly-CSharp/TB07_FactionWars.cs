using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005CC RID: 1484
public class TB07_FactionWars : MissionEntity
{
	// Token: 0x06005170 RID: 20848 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void PreloadAssets()
	{
	}

	// Token: 0x06005171 RID: 20849 RVA: 0x001AC467 File Offset: 0x001AA667
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.textID = "";
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 1)
		{
			this.textID = "TB_SINGLEPLAYERTRIAL_PLAYERDIED";
		}
		if (this.textID.Length > 0)
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(false);
			this.popUpPos = new Vector3(1.27f, 0f, 19f);
			this.GameOverPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.25f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.GameOverPopup, 4f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x040048E1 RID: 18657
	private Notification GameOverPopup;

	// Token: 0x040048E2 RID: 18658
	private string textID;

	// Token: 0x040048E3 RID: 18659
	private Vector3 popUpPos;
}
