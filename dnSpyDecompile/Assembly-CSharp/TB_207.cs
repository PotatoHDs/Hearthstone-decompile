using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A1 RID: 1441
public class TB_207 : MissionEntity
{
	// Token: 0x06004FBD RID: 20413 RVA: 0x001A2BFD File Offset: 0x001A0DFD
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
	}

	// Token: 0x06004FBE RID: 20414 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void PreloadAssets()
	{
	}

	// Token: 0x06004FBF RID: 20415 RVA: 0x001A2C20 File Offset: 0x001A0E20
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

	// Token: 0x06004FC0 RID: 20416 RVA: 0x001A2C95 File Offset: 0x001A0E95
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		this.brawl = this.opposingSidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		this.yourBrawl = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		Debug.Log("Brawl # " + this.brawl);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (this.m_popUpInfo.ContainsKey(missionEvent))
		{
			if (missionEvent == 10)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][1]) + "\n" + GameStrings.Get(this.m_popUpInfo[1][this.yourBrawl]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				yield return new WaitForSeconds(0.5f);
				popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]) + "\n" + GameStrings.Get(this.m_popUpInfo[1][this.brawl]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(3.5f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
			if (missionEvent == 11)
			{
				Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]) + "\n" + GameStrings.Get(this.m_popUpInfo[1][this.yourBrawl]), false, NotificationManager.PopupTextType.FANCY);
				yield return new WaitForSeconds(4f);
				NotificationManager.Get().DestroyNotification(popup, 0f);
				popup = null;
			}
		}
		yield break;
	}

	// Token: 0x06004FC1 RID: 20417 RVA: 0x001A2CAB File Offset: 0x001A0EAB
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040045D1 RID: 17873
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			10,
			new string[]
			{
				"TB_207_INTRO_02",
				"TB_207_INTRO_01"
			}
		},
		{
			11,
			new string[]
			{
				"TB_207_INTRO_01"
			}
		},
		{
			1,
			new string[]
			{
				"",
				"TB_207_01",
				"TB_207_02",
				"TB_207_03",
				"TB_207_04",
				"TB_207_05",
				"TB_207_06",
				"TB_207_07",
				"TB_207_08",
				"TB_207_09"
			}
		}
	};

	// Token: 0x040045D2 RID: 17874
	private Player friendlySidePlayer;

	// Token: 0x040045D3 RID: 17875
	private Player opposingSidePlayer;

	// Token: 0x040045D4 RID: 17876
	private Entity playerEntity;

	// Token: 0x040045D5 RID: 17877
	private float popUpScale = 1.25f;

	// Token: 0x040045D6 RID: 17878
	private Vector3 popUpPos;

	// Token: 0x040045D7 RID: 17879
	private Notification StartPopup;

	// Token: 0x040045D8 RID: 17880
	private int brawl;

	// Token: 0x040045D9 RID: 17881
	private int yourBrawl;
}
