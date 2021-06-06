using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005CB RID: 1483
public class TB05_GiftExchange : MissionEntity
{
	// Token: 0x0600516D RID: 20845 RVA: 0x001AC32C File Offset: 0x001AA52C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT1.prefab:4ae447402c1a6044584b4c310ec9ac54");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT2.prefab:03e080e95e177f448b422251a62b3534");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT3.prefab:83f8d60f0a3375845a3d3d8256c77998");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT4.prefab:0696e29df4374fe44998942aa3b98fdd");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_LONG1.prefab:3f1fe4e70cbaf3a40960431138ef961e");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_LONG2.prefab:3229f3d88d7ef9f45b3b5e5314f59f62");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_LONG3.prefab:7e768808510998e48b5df809cbf33c0c");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_LONG4.prefab:b10dc2bd2c73ce44e9d5dca4d4bba2c1");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_LONG5.prefab:b2bb544f1972fd546bd6fdba7f0b8aab");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_LONG6.prefab:ef76d11be6f4e4244905e125d5064438");
		base.PreloadSound("VO_TB_1503_FATHER_WINTER_START.prefab:71392d50932d2bb4f93ad5f687f229dd");
	}

	// Token: 0x0600516E RID: 20846 RVA: 0x001AC3B2 File Offset: 0x001AA5B2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.VOChoice = "";
		this.delayTime = 0f;
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent <= 2)
		{
			if (missionEvent != 1)
			{
				if (missionEvent == 2)
				{
					this.VOChoice = this.PissedVOList[UnityEngine.Random.Range(1, this.PissedVOList.Length)];
					this.delayTime = 2f;
				}
			}
			else if (this.FirstGiftVO.Length > 0)
			{
				this.VOChoice = this.FirstGiftVO;
				this.FirstGiftVO = "";
				this.delayTime = 4f;
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(0.5f);
				GameState.Get().SetBusy(false);
				this.textID = "TB_GIFTEXCHANGE_GIFTSPAWNED";
				this.popUpPos = new Vector3(1.27f, 0f, -9.32f);
				if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
				{
					this.popUpPos.z = 19f;
				}
				float d = 1.25f;
				if (UniversalInputManager.UsePhoneUI)
				{
					d = 1.75f;
				}
				this.GiftSpawnedPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * d, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
				NotificationManager.Get().DestroyNotification(this.GiftSpawnedPopup, 4f);
			}
			else
			{
				this.VOChoice = this.GiftVOList[UnityEngine.Random.Range(1, this.GiftVOList.Length)];
				this.delayTime = 3f;
			}
		}
		else if (missionEvent != 10)
		{
			if (missionEvent == 11)
			{
				if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
				{
					if (this.FirstStolenVO.Length > 0)
					{
						this.VOChoice = this.FirstStolenVO;
						this.FirstStolenVO = "";
						yield return new WaitForSeconds(1.5f);
						this.delayTime = 4f;
						this.textID = "TB_GIFTEXCHANGE_GIFTSTOLEN";
						this.popUpPos = new Vector3(22.2f, 0f, -44.6f);
						if (UniversalInputManager.UsePhoneUI)
						{
							this.popUpPos.x = 61f;
							this.popUpPos.z = -29f;
						}
						this.GiftStolenPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.25f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
						NotificationManager.Get().DestroyNotification(this.GiftStolenPopup, 4f);
					}
				}
				else if (this.NextStolenVO.Length > 0)
				{
					this.VOChoice = this.NextStolenVO;
					this.NextStolenVO = "";
				}
			}
		}
		else
		{
			this.VOChoice = this.StartVO;
			this.delayTime = 5f;
			this.textID = "TB_GIFTEXCHANGE_START";
			this.popUpPos = new Vector3(22.2f, 0f, -44.6f);
			this.popUpPos = new Vector3(0f, 0f, 0f);
			this.GameStartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * 1.75f, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.GameStartPopup, 3f);
		}
		base.PlaySound(this.VOChoice, 1f, true, false);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(this.delayTime);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040048D4 RID: 18644
	private string[] GiftVOList = new string[]
	{
		"VO_TB_1503_FATHER_WINTER_GIFT1.prefab:4ae447402c1a6044584b4c310ec9ac54",
		"VO_TB_1503_FATHER_WINTER_GIFT2.prefab:03e080e95e177f448b422251a62b3534",
		"VO_TB_1503_FATHER_WINTER_GIFT3.prefab:83f8d60f0a3375845a3d3d8256c77998",
		"VO_TB_1503_FATHER_WINTER_GIFT4.prefab:0696e29df4374fe44998942aa3b98fdd"
	};

	// Token: 0x040048D5 RID: 18645
	private string[] PissedVOList = new string[]
	{
		"VO_TB_1503_FATHER_WINTER_LONG2.prefab:3229f3d88d7ef9f45b3b5e5314f59f62",
		"VO_TB_1503_FATHER_WINTER_LONG3.prefab:7e768808510998e48b5df809cbf33c0c",
		"VO_TB_1503_FATHER_WINTER_LONG4.prefab:b10dc2bd2c73ce44e9d5dca4d4bba2c1",
		"VO_TB_1503_FATHER_WINTER_LONG5.prefab:b2bb544f1972fd546bd6fdba7f0b8aab",
		"VO_TB_1503_FATHER_WINTER_LONG6.prefab:ef76d11be6f4e4244905e125d5064438"
	};

	// Token: 0x040048D6 RID: 18646
	private string FirstGiftVO = "VO_TB_1503_FATHER_WINTER_GIFT1.prefab:4ae447402c1a6044584b4c310ec9ac54";

	// Token: 0x040048D7 RID: 18647
	private string StartVO = "VO_TB_1503_FATHER_WINTER_LONG6.prefab:ef76d11be6f4e4244905e125d5064438";

	// Token: 0x040048D8 RID: 18648
	private string FirstStolenVO = "VO_TB_1503_FATHER_WINTER_START.prefab:71392d50932d2bb4f93ad5f687f229dd";

	// Token: 0x040048D9 RID: 18649
	private string NextStolenVO = "VO_TB_1503_FATHER_WINTER_LONG1.prefab:3f1fe4e70cbaf3a40960431138ef961e";

	// Token: 0x040048DA RID: 18650
	private string VOChoice;

	// Token: 0x040048DB RID: 18651
	private float delayTime;

	// Token: 0x040048DC RID: 18652
	private Notification GiftStolenPopup;

	// Token: 0x040048DD RID: 18653
	private Notification GiftSpawnedPopup;

	// Token: 0x040048DE RID: 18654
	private Notification GameStartPopup;

	// Token: 0x040048DF RID: 18655
	private string textID;

	// Token: 0x040048E0 RID: 18656
	private Vector3 popUpPos;
}
