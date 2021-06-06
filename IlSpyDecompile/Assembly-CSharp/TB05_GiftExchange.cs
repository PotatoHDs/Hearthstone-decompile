using System.Collections;
using UnityEngine;

public class TB05_GiftExchange : MissionEntity
{
	private string[] GiftVOList = new string[4] { "VO_TB_1503_FATHER_WINTER_GIFT1.prefab:4ae447402c1a6044584b4c310ec9ac54", "VO_TB_1503_FATHER_WINTER_GIFT2.prefab:03e080e95e177f448b422251a62b3534", "VO_TB_1503_FATHER_WINTER_GIFT3.prefab:83f8d60f0a3375845a3d3d8256c77998", "VO_TB_1503_FATHER_WINTER_GIFT4.prefab:0696e29df4374fe44998942aa3b98fdd" };

	private string[] PissedVOList = new string[5] { "VO_TB_1503_FATHER_WINTER_LONG2.prefab:3229f3d88d7ef9f45b3b5e5314f59f62", "VO_TB_1503_FATHER_WINTER_LONG3.prefab:7e768808510998e48b5df809cbf33c0c", "VO_TB_1503_FATHER_WINTER_LONG4.prefab:b10dc2bd2c73ce44e9d5dca4d4bba2c1", "VO_TB_1503_FATHER_WINTER_LONG5.prefab:b2bb544f1972fd546bd6fdba7f0b8aab", "VO_TB_1503_FATHER_WINTER_LONG6.prefab:ef76d11be6f4e4244905e125d5064438" };

	private string FirstGiftVO = "VO_TB_1503_FATHER_WINTER_GIFT1.prefab:4ae447402c1a6044584b4c310ec9ac54";

	private string StartVO = "VO_TB_1503_FATHER_WINTER_LONG6.prefab:ef76d11be6f4e4244905e125d5064438";

	private string FirstStolenVO = "VO_TB_1503_FATHER_WINTER_START.prefab:71392d50932d2bb4f93ad5f687f229dd";

	private string NextStolenVO = "VO_TB_1503_FATHER_WINTER_LONG1.prefab:3f1fe4e70cbaf3a40960431138ef961e";

	private string VOChoice;

	private float delayTime;

	private Notification GiftStolenPopup;

	private Notification GiftSpawnedPopup;

	private Notification GameStartPopup;

	private string textID;

	private Vector3 popUpPos;

	public override void PreloadAssets()
	{
		PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT1.prefab:4ae447402c1a6044584b4c310ec9ac54");
		PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT2.prefab:03e080e95e177f448b422251a62b3534");
		PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT3.prefab:83f8d60f0a3375845a3d3d8256c77998");
		PreloadSound("VO_TB_1503_FATHER_WINTER_GIFT4.prefab:0696e29df4374fe44998942aa3b98fdd");
		PreloadSound("VO_TB_1503_FATHER_WINTER_LONG1.prefab:3f1fe4e70cbaf3a40960431138ef961e");
		PreloadSound("VO_TB_1503_FATHER_WINTER_LONG2.prefab:3229f3d88d7ef9f45b3b5e5314f59f62");
		PreloadSound("VO_TB_1503_FATHER_WINTER_LONG3.prefab:7e768808510998e48b5df809cbf33c0c");
		PreloadSound("VO_TB_1503_FATHER_WINTER_LONG4.prefab:b10dc2bd2c73ce44e9d5dca4d4bba2c1");
		PreloadSound("VO_TB_1503_FATHER_WINTER_LONG5.prefab:b2bb544f1972fd546bd6fdba7f0b8aab");
		PreloadSound("VO_TB_1503_FATHER_WINTER_LONG6.prefab:ef76d11be6f4e4244905e125d5064438");
		PreloadSound("VO_TB_1503_FATHER_WINTER_START.prefab:71392d50932d2bb4f93ad5f687f229dd");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		VOChoice = "";
		delayTime = 0f;
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 1:
			if (FirstGiftVO.Length > 0)
			{
				VOChoice = FirstGiftVO;
				FirstGiftVO = "";
				delayTime = 4f;
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(0.5f);
				GameState.Get().SetBusy(busy: false);
				textID = "TB_GIFTEXCHANGE_GIFTSPAWNED";
				popUpPos = new Vector3(1.27f, 0f, -9.32f);
				if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
				{
					popUpPos.z = 19f;
				}
				float num = 1.25f;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					num = 1.75f;
				}
				GiftSpawnedPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * num, GameStrings.Get(textID), convertLegacyPosition: false);
				NotificationManager.Get().DestroyNotification(GiftSpawnedPopup, 4f);
			}
			else
			{
				VOChoice = GiftVOList[Random.Range(1, GiftVOList.Length)];
				delayTime = 3f;
			}
			break;
		case 2:
			VOChoice = PissedVOList[Random.Range(1, PissedVOList.Length)];
			delayTime = 2f;
			break;
		case 10:
			VOChoice = StartVO;
			delayTime = 5f;
			textID = "TB_GIFTEXCHANGE_START";
			popUpPos = new Vector3(22.2f, 0f, -44.6f);
			popUpPos = new Vector3(0f, 0f, 0f);
			GameStartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.75f, GameStrings.Get(textID), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(GameStartPopup, 3f);
			break;
		case 11:
			if (GameState.Get().GetFriendlySidePlayer() == GameState.Get().GetCurrentPlayer())
			{
				if (FirstStolenVO.Length > 0)
				{
					VOChoice = FirstStolenVO;
					FirstStolenVO = "";
					yield return new WaitForSeconds(1.5f);
					delayTime = 4f;
					textID = "TB_GIFTEXCHANGE_GIFTSTOLEN";
					popUpPos = new Vector3(22.2f, 0f, -44.6f);
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						popUpPos.x = 61f;
						popUpPos.z = -29f;
					}
					GiftStolenPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.25f, GameStrings.Get(textID), convertLegacyPosition: false);
					NotificationManager.Get().DestroyNotification(GiftStolenPopup, 4f);
				}
			}
			else if (NextStolenVO.Length > 0)
			{
				VOChoice = NextStolenVO;
				NextStolenVO = "";
			}
			break;
		}
		PlaySound(VOChoice);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(delayTime);
		GameState.Get().SetBusy(busy: false);
	}
}
