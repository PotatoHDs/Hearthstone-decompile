using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
public class TB_KoboldGifts : MissionEntity
{
	// Token: 0x060050D0 RID: 20688 RVA: 0x001A8BC5 File Offset: 0x001A6DC5
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060050D1 RID: 20689 RVA: 0x001A8BD7 File Offset: 0x001A6DD7
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOOT_384_Male_Kobold_Event_01.prefab:5caf2a56bda8b96418925f1f08c99f53");
	}

	// Token: 0x060050D2 RID: 20690 RVA: 0x001A8BE4 File Offset: 0x001A6DE4
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x060050D3 RID: 20691 RVA: 0x001A8C59 File Offset: 0x001A6E59
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
			yield return new WaitForSeconds(3.5f);
			NotificationManager.Get().DestroyNotification(popup, 0f);
			if (missionEvent == 10)
			{
				yield return new WaitForSeconds(0.15f);
				base.PlaySound("VO_LOOT_384_Male_Kobold_Event_01.prefab:5caf2a56bda8b96418925f1f08c99f53", 1f, true, false);
			}
			popup = null;
		}
		yield break;
	}

	// Token: 0x060050D4 RID: 20692 RVA: 0x001A8C6F File Offset: 0x001A6E6F
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04004771 RID: 18289
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_KOBOLDGIFTS_01"
			}
		},
		{
			11,
			new string[]
			{
				"TB_KOBOLDGIFTS_02"
			}
		}
	};

	// Token: 0x04004772 RID: 18290
	private Player friendlySidePlayer;

	// Token: 0x04004773 RID: 18291
	private Entity playerEntity;

	// Token: 0x04004774 RID: 18292
	private int isPlayerHorseman;

	// Token: 0x04004775 RID: 18293
	private float popUpScale = 1.25f;

	// Token: 0x04004776 RID: 18294
	private Vector3 popUpPos;

	// Token: 0x04004777 RID: 18295
	private Notification StartPopup;
}
