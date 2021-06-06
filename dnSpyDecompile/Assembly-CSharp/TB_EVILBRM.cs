using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005AA RID: 1450
public class TB_EVILBRM : MissionEntity
{
	// Token: 0x0600508B RID: 20619 RVA: 0x001A78CC File Offset: 0x001A5ACC
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
		base.PreloadSound(TB_EVILBRM.VO_Rafaam_Male_Ethereal_BRM_Start_01);
		base.PreloadSound(TB_EVILBRM.VO_DrBoom_Male_Goblin_BRM_T1End_01);
		base.PreloadSound(TB_EVILBRM.VO_DrBoom_Male_Goblin_BRM_Victory_01);
		base.PreloadSound(TB_EVILBRM.VO_Hagatha_Female_Orc_BRM_Victory_01);
		base.PreloadSound(TB_EVILBRM.VO_MadameLazul_Female_Troll_BRM_Victory_01);
		base.PreloadSound(TB_EVILBRM.VO_Togwaggle_Male_Kobold_BRM_Victory_01);
		base.PreloadSound(TB_EVILBRM.VO_Rafaam_Male_Ethereal_HM_Victory_01);
	}

	// Token: 0x0600508C RID: 20620 RVA: 0x001A7954 File Offset: 0x001A5B54
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 1000)
		{
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			string msgString;
			if (tag == 0)
			{
				msgString = GameStrings.Get(TB_EVILBRM.popupMsgs[2000].Message);
			}
			else
			{
				string text = "";
				string text2 = "";
				string text3 = "";
				int num = tag / 3600;
				int num2 = tag % 3600 / 60;
				int num3 = tag % 60;
				if (num < 10)
				{
					text = "0";
				}
				if (num2 < 10)
				{
					text2 = "0";
				}
				if (num3 < 10)
				{
					text3 = "0";
				}
				msgString = string.Concat(new object[]
				{
					GameStrings.Get(TB_EVILBRM.popupMsgs[missionEvent].Message),
					"\n",
					text,
					num,
					":",
					text2,
					num2,
					":",
					text3,
					num3
				});
				this.popupScale = 1.7f;
			}
			Vector3 popUpPos = default(Vector3);
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
			}
			else
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? -40f : -40f);
			}
			yield return new WaitForSeconds(4f);
			yield return this.ShowPopup(msgString, TB_EVILBRM.popupMsgs[missionEvent].Delay, popUpPos, this.popupScale);
			msgString = null;
			popUpPos = default(Vector3);
		}
		if (missionEvent == 10)
		{
			yield return new WaitForSeconds(1f);
			yield return this.PlayBossLineLeft(TB_EVILBRM.BOSS.RAFAAM, TB_EVILBRM.VO_Rafaam_Male_Ethereal_BRM_Start_01, false);
			yield return new WaitForSeconds(0.5f);
			yield return this.PlayBossLineRight(TB_EVILBRM.BOSS.BOOM, TB_EVILBRM.VO_DrBoom_Male_Goblin_BRM_T1End_01, false);
		}
		yield break;
	}

	// Token: 0x0600508D RID: 20621 RVA: 0x001A796A File Offset: 0x001A5B6A
	private IEnumerator PlayBossLineLeft(TB_EVILBRM.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopLeft;
		switch (boss)
		{
		case TB_EVILBRM.BOSS.BOOM:
			yield return base.PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, TB_EVILBRM.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.HAGATHA:
			yield return base.PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, TB_EVILBRM.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.TOGWAGGLE:
			yield return base.PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, TB_EVILBRM.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.LAZUL:
			yield return base.PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, TB_EVILBRM.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.RAFAAM:
			yield return base.PlayMissionFlavorLine("Rafaam_popup_BrassRing_Quote:187724fae6d64cf49acf11aa53ca2087", line, TB_EVILBRM.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
		yield break;
	}

	// Token: 0x0600508E RID: 20622 RVA: 0x001A798E File Offset: 0x001A5B8E
	private IEnumerator PlayBossLineRight(TB_EVILBRM.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
		switch (boss)
		{
		case TB_EVILBRM.BOSS.BOOM:
			yield return base.PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, TB_EVILBRM.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.HAGATHA:
			yield return base.PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, TB_EVILBRM.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.TOGWAGGLE:
			yield return base.PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, TB_EVILBRM.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.LAZUL:
			yield return base.PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, TB_EVILBRM.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_EVILBRM.BOSS.RAFAAM:
			yield return base.PlayMissionFlavorLine("Rafaam_popup_BrassRing_Quote:187724fae6d64cf49acf11aa53ca2087", line, TB_EVILBRM.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
		yield break;
	}

	// Token: 0x0600508F RID: 20623 RVA: 0x001A79B2 File Offset: 0x001A5BB2
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			this.matchResult = TB_EVILBRM.VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			this.matchResult = TB_EVILBRM.VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			this.matchResult = TB_EVILBRM.VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x06005090 RID: 20624 RVA: 0x001A79EA File Offset: 0x001A5BEA
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		this.enemyPlayer = GameState.Get().GetOpposingSidePlayer();
		this.currentSelectedBoss = this.enemyPlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		this.isOnRagnaros = this.enemyPlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		Debug.Log("isRagnaros returns " + this.isOnRagnaros);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(false);
		if (this.isOnRagnaros == 1)
		{
			switch (this.matchResult)
			{
			case TB_EVILBRM.VICTOR.PLAYERWIN:
				if (this.currentSelectedBoss == 1)
				{
					yield return new WaitForSeconds(1.5f);
					yield return this.PlayBossLineLeft(TB_EVILBRM.BOSS.BOOM, TB_EVILBRM.VO_DrBoom_Male_Goblin_BRM_Victory_01, false);
				}
				if (this.currentSelectedBoss == 2)
				{
					yield return new WaitForSeconds(1.5f);
					yield return this.PlayBossLineLeft(TB_EVILBRM.BOSS.HAGATHA, TB_EVILBRM.VO_Hagatha_Female_Orc_BRM_Victory_01, false);
				}
				if (this.currentSelectedBoss == 3)
				{
					yield return new WaitForSeconds(1.5f);
					yield return this.PlayBossLineLeft(TB_EVILBRM.BOSS.LAZUL, TB_EVILBRM.VO_MadameLazul_Female_Troll_BRM_Victory_01, false);
				}
				if (this.currentSelectedBoss == 4)
				{
					yield return new WaitForSeconds(1.5f);
					yield return this.PlayBossLineLeft(TB_EVILBRM.BOSS.TOGWAGGLE, TB_EVILBRM.VO_Togwaggle_Male_Kobold_BRM_Victory_01, false);
				}
				if (this.currentSelectedBoss == 5)
				{
					yield return new WaitForSeconds(1.5f);
					yield return this.PlayBossLineLeft(TB_EVILBRM.BOSS.RAFAAM, TB_EVILBRM.VO_Rafaam_Male_Ethereal_HM_Victory_01, false);
				}
				break;
			}
		}
		yield break;
	}

	// Token: 0x06005091 RID: 20625 RVA: 0x001A79F9 File Offset: 0x001A5BF9
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
		yield break;
	}

	// Token: 0x040046CD RID: 18125
	private static readonly AssetReference VO_Rafaam_Male_Ethereal_BRM_Start_01 = new AssetReference("VO_Rafaam_Male_Ethereal_BRM_Start_01:840b30444d3dcac419d14454b31ef534");

	// Token: 0x040046CE RID: 18126
	private static readonly AssetReference VO_DrBoom_Male_Goblin_BRM_T1End_01 = new AssetReference("VO_DrBoom_Male_Goblin_BRM_T1End_01:ba94239568242a04db30dcf8fc6be837");

	// Token: 0x040046CF RID: 18127
	private static readonly AssetReference VO_DrBoom_Male_Goblin_BRM_Victory_01 = new AssetReference("VO_DrBoom_Male_Goblin_BRM_Victory_01:0b3187eb9664d9f4ca73ff246baa6463");

	// Token: 0x040046D0 RID: 18128
	private static readonly AssetReference VO_Hagatha_Female_Orc_BRM_Victory_01 = new AssetReference("VO_Hagatha_Female_Orc_BRM_Victory_01:2832c50d764531d4794545901326adac");

	// Token: 0x040046D1 RID: 18129
	private static readonly AssetReference VO_MadameLazul_Female_Troll_BRM_Victory_01 = new AssetReference("VO_MadameLazul_Female_Troll_BRM_Victory_01:1d1c9015c5e6cd34892e179df49768e2");

	// Token: 0x040046D2 RID: 18130
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_BRM_Victory_01 = new AssetReference("VO_Togwaggle_Male_Kobold_BRM_Victory_01:849eb420629bf2a41aaf192489366f8d");

	// Token: 0x040046D3 RID: 18131
	private static readonly AssetReference VO_Rafaam_Male_Ethereal_HM_Victory_01 = new AssetReference("VO_Rafaam_Male_Ethereal_HM_Victory_01:04141be712eae134b85f869c50056efa");

	// Token: 0x040046D4 RID: 18132
	private Notification m_popup;

	// Token: 0x040046D5 RID: 18133
	private float popupScale = 1.4f;

	// Token: 0x040046D6 RID: 18134
	private static readonly Dictionary<int, TB_EVILBRM.PopupMessage> popupMsgs = new Dictionary<int, TB_EVILBRM.PopupMessage>
	{
		{
			1000,
			new TB_EVILBRM.PopupMessage
			{
				Message = "TB_EVILBRM_CURRENT_BEST_SCORE",
				Delay = 5f
			}
		},
		{
			2000,
			new TB_EVILBRM.PopupMessage
			{
				Message = "TB_EVILBRM_NEW_BEST_SCORE",
				Delay = 5f
			}
		}
	};

	// Token: 0x040046D7 RID: 18135
	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -1.8f);

	// Token: 0x040046D8 RID: 18136
	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -1.8f);

	// Token: 0x040046D9 RID: 18137
	private TB_EVILBRM.VICTOR matchResult;

	// Token: 0x040046DA RID: 18138
	private int currentSelectedBoss;

	// Token: 0x040046DB RID: 18139
	private int isOnRagnaros;

	// Token: 0x040046DC RID: 18140
	private Player enemyPlayer;

	// Token: 0x02001F9E RID: 8094
	public struct PopupMessage
	{
		// Token: 0x0400D9DE RID: 55774
		public string Message;

		// Token: 0x0400D9DF RID: 55775
		public float Delay;
	}

	// Token: 0x02001F9F RID: 8095
	private enum BOSS
	{
		// Token: 0x0400D9E1 RID: 55777
		BOOM,
		// Token: 0x0400D9E2 RID: 55778
		HAGATHA,
		// Token: 0x0400D9E3 RID: 55779
		TOGWAGGLE,
		// Token: 0x0400D9E4 RID: 55780
		LAZUL,
		// Token: 0x0400D9E5 RID: 55781
		RAFAAM
	}

	// Token: 0x02001FA0 RID: 8096
	private enum VICTOR
	{
		// Token: 0x0400D9E7 RID: 55783
		PLAYERLOST,
		// Token: 0x0400D9E8 RID: 55784
		PLAYERWIN,
		// Token: 0x0400D9E9 RID: 55785
		ERROR
	}
}
