using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005B7 RID: 1463
public class TB_MammothParty : MissionEntity
{
	// Token: 0x060050E7 RID: 20711 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060050E8 RID: 20712 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x060050E9 RID: 20713 RVA: 0x001A9427 File Offset: 0x001A7627
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, 0f);
		switch (missionEvent)
		{
		case 10:
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID10), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			break;
		case 11:
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
			}
			else
			{
				this.popUpPos.z = -44f;
			}
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID11), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			break;
		case 12:
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
			}
			else
			{
				this.popUpPos.z = -44f;
			}
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get(this.textID12), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 5f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(6.5f);
			GameState.Get().SetBusy(false);
			break;
		case 13:
			if (!this.hasCrasherBeenDiscarded)
			{
				this.hasCrasherBeenDiscarded = true;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.popUpPos.z = -66f;
				}
				else
				{
					this.popUpPos.z = -44f;
				}
				this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID13), false, NotificationManager.PopupTextType.BASIC);
				NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 14:
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get(this.textID14), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(7f);
			GameState.Get().SetBusy(false);
			break;
		case 15:
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
			}
			else
			{
				this.popUpPos.z = -44f;
			}
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID15), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			break;
		case 16:
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
			}
			else
			{
				this.popUpPos.z = -44f;
			}
			this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.5f, GameStrings.Get(this.textID16), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x060050EA RID: 20714 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x040047A4 RID: 18340
	private Notification StartPopup;

	// Token: 0x040047A5 RID: 18341
	private bool hasCrasherBeenDiscarded;

	// Token: 0x040047A6 RID: 18342
	private string textID10 = "TB_MP_COOP_TEXT_START";

	// Token: 0x040047A7 RID: 18343
	private string textID11 = "TB_MP_COOP_FIRST_CRASHER";

	// Token: 0x040047A8 RID: 18344
	private string textID12 = "TB_MP_COOP_PINATA";

	// Token: 0x040047A9 RID: 18345
	private string textID13 = "TB_MP_COOP_CRASHER_DISCARD";

	// Token: 0x040047AA RID: 18346
	private string textID14 = "TB_MP_COOP_ENDING";

	// Token: 0x040047AB RID: 18347
	private string textID15 = "TB_MP_COOP_1STSPELL";

	// Token: 0x040047AC RID: 18348
	private string textID16 = "TB_MP_COOP_2NDSPELL";

	// Token: 0x040047AD RID: 18349
	private Vector3 popUpPos;
}
