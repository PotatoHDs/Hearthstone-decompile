using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005B1 RID: 1457
public class TB_Henchmania : MissionEntity
{
	// Token: 0x060050BD RID: 20669 RVA: 0x001A8510 File Offset: 0x001A6710
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_ChooseBoss_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_FightBegins_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_HalfHealth_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_Loss_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_RejectBoss_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_T1End_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_T2End_01);
		base.PreloadSound(TB_Henchmania.VO_DrBoom_Male_Goblin_HM_Victory_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_ChooseBoss_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_FightBegins_02);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_HalfHealth_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_Loss_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_RejectBoss_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_T1End_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_T2End_01);
		base.PreloadSound(TB_Henchmania.VO_Hagatha_Female_Orc_HM_Victory_01);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_ChooseBoss_01);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_FightBegins_01);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_HalfHealth_01);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_Loss_01);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_RejectBoss_02);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_T1End_02);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_T2End_01);
		base.PreloadSound(TB_Henchmania.VO_MadameLazul_Female_Troll_HM_Victory_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_FightBegins_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_HalfHealth_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_Loss_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_RejectBoss_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_T1End_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_T2End_01);
		base.PreloadSound(TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_Victory_01);
	}

	// Token: 0x060050BE RID: 20670 RVA: 0x001A871D File Offset: 0x001A691D
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x060050BF RID: 20671 RVA: 0x001A872F File Offset: 0x001A692F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.isPlayerActive = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 10:
			if (this.isPlayerActive == 1)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_ChooseBoss_01, false);
				yield return new WaitForSeconds(1f);
				yield return this.PlayBossLineRight(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_RejectBoss_02, false);
			}
			break;
		case 11:
			if (this.isPlayerActive == 1)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_ChooseBoss_01, false);
				yield return new WaitForSeconds(1f);
				yield return this.PlayBossLineRight(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_RejectBoss_01, false);
			}
			break;
		case 12:
			if (this.isPlayerActive == 1)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01, false);
				yield return new WaitForSeconds(1f);
				yield return this.PlayBossLineRight(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_RejectBoss_01, false);
			}
			break;
		case 13:
			if (this.isPlayerActive == 1)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_ChooseBoss_01, false);
				yield return new WaitForSeconds(1f);
				yield return this.PlayBossLineRight(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_RejectBoss_01, false);
			}
			break;
		case 14:
			if (this.isPlayerActive == 1)
			{
				this.currentSelectedBoss = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				yield return new WaitForSeconds(3f);
				if (this.currentSelectedBoss == 1)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_FightBegins_02, false);
				}
				if (this.currentSelectedBoss == 2)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_FightBegins_01, false);
				}
				if (this.currentSelectedBoss == 3)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_FightBegins_01, false);
				}
				if (this.currentSelectedBoss == 4)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_FightBegins_01, false);
				}
			}
			break;
		case 15:
			if (this.isPlayerActive == 1)
			{
				yield return new WaitForSeconds(3f);
				this.currentSelectedBoss = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				if (this.currentSelectedBoss == 1)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_T1End_01, false);
				}
				if (this.currentSelectedBoss == 2)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_T1End_01, false);
				}
				if (this.currentSelectedBoss == 3)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_T1End_01, false);
				}
				if (this.currentSelectedBoss == 4)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_T1End_02, false);
				}
			}
			break;
		case 16:
			if (this.isPlayerActive == 1)
			{
				this.currentSelectedBoss = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				yield return new WaitForSeconds(3f);
				if (this.currentSelectedBoss == 1)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_T2End_01, false);
				}
				if (this.currentSelectedBoss == 2)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_T2End_01, false);
				}
				if (this.currentSelectedBoss == 3)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_T2End_01, false);
				}
				if (this.currentSelectedBoss == 4)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_T2End_01, false);
				}
			}
			break;
		case 17:
			if (this.isPlayerActive == 1)
			{
				this.currentSelectedBoss = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				yield return new WaitForSeconds(3f);
				if (this.currentSelectedBoss == 1)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_HalfHealth_01, false);
				}
				if (this.currentSelectedBoss == 2)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_HalfHealth_01, false);
				}
				if (this.currentSelectedBoss == 3)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_HalfHealth_01, false);
				}
				if (this.currentSelectedBoss == 4)
				{
					yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_HalfHealth_01, false);
				}
			}
			break;
		}
		yield break;
	}

	// Token: 0x060050C0 RID: 20672 RVA: 0x001A8745 File Offset: 0x001A6945
	private IEnumerator PlayBossLine(string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		yield return base.PlayMissionFlavorLine("Rastakhan_BrassRing_Quote:179bfad1464576448aeabfe5c3eff601", line, TB_Henchmania.LEFT_OF_FRIENDLY_HERO, direction, 2.5f, persistCharacter);
		yield break;
	}

	// Token: 0x060050C1 RID: 20673 RVA: 0x001A8764 File Offset: 0x001A6964
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			this.matchResult = TB_Henchmania.VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			this.matchResult = TB_Henchmania.VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			this.matchResult = TB_Henchmania.VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x060050C2 RID: 20674 RVA: 0x001A87B1 File Offset: 0x001A69B1
	private IEnumerator PlayBossLineLeft(TB_Henchmania.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopLeft;
		switch (boss)
		{
		case TB_Henchmania.BOSS.BOOM:
			yield return base.PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, TB_Henchmania.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_Henchmania.BOSS.HAGATHA:
			yield return base.PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, TB_Henchmania.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_Henchmania.BOSS.TOGWAGGLE:
			yield return base.PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, TB_Henchmania.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_Henchmania.BOSS.LAZUL:
			yield return base.PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, TB_Henchmania.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
		yield break;
	}

	// Token: 0x060050C3 RID: 20675 RVA: 0x001A87D5 File Offset: 0x001A69D5
	private IEnumerator PlayBossLineRight(TB_Henchmania.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
		switch (boss)
		{
		case TB_Henchmania.BOSS.BOOM:
			yield return base.PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, TB_Henchmania.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_Henchmania.BOSS.HAGATHA:
			yield return base.PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, TB_Henchmania.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_Henchmania.BOSS.TOGWAGGLE:
			yield return base.PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, TB_Henchmania.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_Henchmania.BOSS.LAZUL:
			yield return base.PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, TB_Henchmania.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
		yield break;
	}

	// Token: 0x060050C4 RID: 20676 RVA: 0x001A87F9 File Offset: 0x001A69F9
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.currentSelectedBoss = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(false);
		switch (this.matchResult)
		{
		case TB_Henchmania.VICTOR.PLAYERLOST:
			GameState.Get().SetBusy(true);
			if (this.currentSelectedBoss == 1)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_Loss_01, false);
			}
			if (this.currentSelectedBoss == 2)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_Loss_01, false);
			}
			if (this.currentSelectedBoss == 3)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_Loss_01, false);
			}
			if (this.currentSelectedBoss == 4)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_Loss_01, false);
			}
			GameState.Get().SetBusy(false);
			break;
		case TB_Henchmania.VICTOR.PLAYERWIN:
			if (this.currentSelectedBoss == 1)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.HAGATHA, TB_Henchmania.VO_Hagatha_Female_Orc_HM_Victory_01, false);
			}
			if (this.currentSelectedBoss == 2)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.BOOM, TB_Henchmania.VO_DrBoom_Male_Goblin_HM_Victory_01, false);
			}
			if (this.currentSelectedBoss == 3)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.TOGWAGGLE, TB_Henchmania.VO_Togwaggle_Male_Kobold_HM_Victory_01, false);
			}
			if (this.currentSelectedBoss == 4)
			{
				yield return this.PlayBossLineLeft(TB_Henchmania.BOSS.LAZUL, TB_Henchmania.VO_MadameLazul_Female_Troll_HM_Victory_01, false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x04004737 RID: 18231
	private TB_Henchmania.VICTOR matchResult;

	// Token: 0x04004738 RID: 18232
	private Notification StartPopup;

	// Token: 0x04004739 RID: 18233
	private int shouldShowVictory;

	// Token: 0x0400473A RID: 18234
	private int shouldShowIntro;

	// Token: 0x0400473B RID: 18235
	private int isPlayerActive;

	// Token: 0x0400473C RID: 18236
	private int currentSelectedBoss;

	// Token: 0x0400473D RID: 18237
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_ChooseBoss_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_ChooseBoss_01:92ef377b2a9229949a68044f60194a99");

	// Token: 0x0400473E RID: 18238
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_FightBegins_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_FightBegins_01:10a8a2a12a3d6954882ea96d0e02708b");

	// Token: 0x0400473F RID: 18239
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_HalfHealth_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_HalfHealth_01:e0241fb915083b24e91bbd7945911eb7");

	// Token: 0x04004740 RID: 18240
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_Loss_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_Loss_01:0b1fb3c932b7f6a4eac2b69971591681");

	// Token: 0x04004741 RID: 18241
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_RejectBoss_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_RejectBoss_01:be9d6e2dc6f12224b98dea7432b418bf");

	// Token: 0x04004742 RID: 18242
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_T1End_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_T1End_01:f95bc84b6be39124386249b008cca987");

	// Token: 0x04004743 RID: 18243
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_T2End_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_T2End_01:aa5d0d2be4820e94caf11bcf4f863baf");

	// Token: 0x04004744 RID: 18244
	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_Victory_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_Victory_01:0ae5db5ecc98fd3479aa6c0e214f9e25");

	// Token: 0x04004745 RID: 18245
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_ChooseBoss_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_ChooseBoss_01:1a3ede76502370448bcbb0f55021c0aa");

	// Token: 0x04004746 RID: 18246
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_FightBegins_02 = new AssetReference("VO_Hagatha_Female_Orc_HM_FightBegins_02:05349b4988034804687636f4b5bbcccc");

	// Token: 0x04004747 RID: 18247
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_HalfHealth_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_HalfHealth_01:04d78ee0f6475aa418f9b4ae1433b2d5");

	// Token: 0x04004748 RID: 18248
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_Loss_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_Loss_01:c9db327aabb710844b2392268e074e40");

	// Token: 0x04004749 RID: 18249
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_RejectBoss_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_RejectBoss_01:bd611323cc855bf48a3a59345806b1d1");

	// Token: 0x0400474A RID: 18250
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_T1End_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_T1End_01:22787559cc2b31f46a31aad519378170");

	// Token: 0x0400474B RID: 18251
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_T2End_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_T2End_01:e2bcc4eba0b883f43a2f18f207045a33");

	// Token: 0x0400474C RID: 18252
	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_Victory_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_Victory_01:262178b640b15264a941504759e19e50");

	// Token: 0x0400474D RID: 18253
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_ChooseBoss_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_ChooseBoss_01:893315ab675d7ef4ba71ee9ba93cf37b");

	// Token: 0x0400474E RID: 18254
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_FightBegins_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_FightBegins_01:7929131f3bb0c00408780e7a1309ea5a");

	// Token: 0x0400474F RID: 18255
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_HalfHealth_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_HalfHealth_01:cef25d4d7a371f047a198c9da03b794d");

	// Token: 0x04004750 RID: 18256
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_Loss_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_Loss_01:2edeb04408e19814fbcfbec666f8a5ad");

	// Token: 0x04004751 RID: 18257
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_RejectBoss_02 = new AssetReference("VO_MadameLazul_Female_Troll_HM_RejectBoss_02:ec8aa2b89e5a4c743b19b8d0f68d270d");

	// Token: 0x04004752 RID: 18258
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_T1End_02 = new AssetReference("VO_MadameLazul_Female_Troll_HM_T1End_02:24bedd607de58bd46a72dfc8a8ac5d36");

	// Token: 0x04004753 RID: 18259
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_T2End_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_T2End_01:ab1cfda93ef83a249baa60d59be21162");

	// Token: 0x04004754 RID: 18260
	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_Victory_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_Victory_01:186d71a5b0da6ec4f82adbed3e7a658d");

	// Token: 0x04004755 RID: 18261
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01:b2430d97d1938654e9f6e6dc2fe2d4bb");

	// Token: 0x04004756 RID: 18262
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_FightBegins_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_FightBegins_01:a36fd84a374fce44380c00d237a3f3dd");

	// Token: 0x04004757 RID: 18263
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_HalfHealth_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_HalfHealth_01:51a2331053b354941a176c275fa33a6e");

	// Token: 0x04004758 RID: 18264
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_Loss_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_Loss_01:ed6c79dcf1bf32641ae434b83954c519");

	// Token: 0x04004759 RID: 18265
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_RejectBoss_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_RejectBoss_01:1f8dc750ed805974eb91b7ef2daf6873");

	// Token: 0x0400475A RID: 18266
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_T1End_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_T1End_01:d507d155b3604174cbf8fa865cb9fcf3");

	// Token: 0x0400475B RID: 18267
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_T2End_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_T2End_01:d78708c07cd1c4f4da11e23605055abd");

	// Token: 0x0400475C RID: 18268
	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_Victory_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_Victory_01:81e2b781d0274db498488a86cb26ad11");

	// Token: 0x0400475D RID: 18269
	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	// Token: 0x0400475E RID: 18270
	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -2.8f);

	// Token: 0x0400475F RID: 18271
	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -2.8f);

	// Token: 0x04004760 RID: 18272
	private Player friendlySidePlayer;

	// Token: 0x02001FB6 RID: 8118
	private enum VICTOR
	{
		// Token: 0x0400DA4D RID: 55885
		PLAYERLOST,
		// Token: 0x0400DA4E RID: 55886
		PLAYERWIN,
		// Token: 0x0400DA4F RID: 55887
		ERROR
	}

	// Token: 0x02001FB7 RID: 8119
	private enum BOSS
	{
		// Token: 0x0400DA51 RID: 55889
		BOOM,
		// Token: 0x0400DA52 RID: 55890
		HAGATHA,
		// Token: 0x0400DA53 RID: 55891
		TOGWAGGLE,
		// Token: 0x0400DA54 RID: 55892
		LAZUL
	}
}
