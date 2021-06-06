using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005BE RID: 1470
public class TB_RoadToNR : MissionEntity
{
	// Token: 0x060050FF RID: 20735 RVA: 0x001A96FE File Offset: 0x001A78FE
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06005100 RID: 20736 RVA: 0x001A970C File Offset: 0x001A790C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange1Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange1Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss1_Exchange2_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss1_Exchange2Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange3Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange3Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Loss_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Victory_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss2_StartResponse_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss2_Exchange1_01,
			TB_RoadToNR.VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Exchange1Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange2_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange3_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Victory_01,
			TB_RoadToNR.VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Loss_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_HostLine_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Exchange1_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss3_Exchange1Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_Exchange2_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss3_Victory_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_HostLine_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss4_HostLineResponse_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_Exchange1_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss4_Exchange2_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange2Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange3_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Loss_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Victory_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_VictoryResponse_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_HostLine_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_HostLineResponse_01,
			TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Start_02,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss5_Exchange1_01,
			TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange1Response_01,
			TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange2_01,
			TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss5_Exchange3Response1_01,
			TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3Response2_01,
			TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange4_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_Victory_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_VictoryResponse_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_HostLine_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_HostLineResponse_01,
			TB_RoadToNR.VO_DAL_719_Male_Kobold_Northrend_Boss6_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_Exchange1_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss6_Exchange1Response_01,
			TB_RoadToNR.VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange2_01,
			TB_RoadToNR.VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange3_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory1_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory2_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_HostLine_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss7_StartResponse_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss7_Exchange1_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange1Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Exchange2_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange2Response_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Victory_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Start_01,
			TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Loss_01,
			TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Loss_01,
			TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss8_Victory1_01,
			TB_RoadToNR.VO_DALA_900h_Male_Human_PlayerBrood_01
		};
		this.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06005101 RID: 20737 RVA: 0x001A9BA0 File Offset: 0x001A7DA0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (this.seen.Contains(missionEvent))
		{
			yield break;
		}
		this.seen.Add(missionEvent);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent <= 259998)
		{
			if (missionEvent <= 159933)
			{
				switch (missionEvent)
				{
				case 1000:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					if (this.m_PlayPlayerVOLineIndex + 1 >= this.m_PlayerVOLines.Count)
					{
						this.m_PlayPlayerVOLineIndex = 0;
					}
					else
					{
						this.m_PlayPlayerVOLineIndex++;
					}
					SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
					yield return base.PlayBossLine(playerActor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
					break;
				case 1001:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
					yield return base.PlayBossLine(playerActor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
					break;
				case 1002:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					if (this.m_PlayBossVOLineIndex + 1 >= this.m_BossVOLines.Count)
					{
						this.m_PlayBossVOLineIndex = 0;
					}
					else
					{
						this.m_PlayBossVOLineIndex++;
					}
					SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
					yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
					break;
				case 1003:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
					yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
					break;
				default:
					if (missionEvent != 1234)
					{
						switch (missionEvent)
						{
						case 159924:
							GameState.Get().SetBusy(true);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Start_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
							GameState.Get().SetBusy(false);
							break;
						case 159925:
							GameState.Get().SetBusy(true);
							Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Loss_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
							yield return new WaitForSeconds(4.5f);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss2_StartResponse_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 0f, false);
							GameState.Get().SetBusy(false);
							break;
						case 159926:
							GameState.Get().SetBusy(true);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_HostLine_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 4.5f, false);
							yield return new WaitForSeconds(1f);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss4_HostLineResponse_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3.5f, false);
							GameState.Get().SetBusy(false);
							break;
						case 159927:
							GameState.Get().SetBusy(true);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_HostLine_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3.5f, false);
							yield return new WaitForSeconds(3.5f);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Start_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 1.5f, false);
							GameState.Get().SetBusy(false);
							break;
						case 159928:
							GameState.Get().SetBusy(true);
							Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Start_02, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
							GameState.Get().SetBusy(false);
							break;
						case 159929:
							GameState.Get().SetBusy(true);
							yield return new WaitForSeconds(2f);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_HostLine_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3.5f, false);
							yield return new WaitForSeconds(1f);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_HostLineResponse_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2.5f, false);
							GameState.Get().SetBusy(false);
							break;
						case 159930:
							GameState.Get().SetBusy(true);
							yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_HostLine_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3f, false);
							GameState.Get().SetBusy(false);
							break;
						case 159933:
							GameState.Get().SetBusy(true);
							Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Start_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
							GameState.Get().SetBusy(false);
							break;
						}
					}
					else
					{
						int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
						string msgString;
						if (tag == 0)
						{
							msgString = GameStrings.Get(TB_RoadToNR.popupMsgs[2000].Message);
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
								GameStrings.Get(TB_RoadToNR.popupMsgs[missionEvent].Message),
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
						yield return this.ShowPopup(msgString, TB_RoadToNR.popupMsgs[missionEvent].Delay, popUpPos, this.popupScale);
						msgString = null;
						popUpPos = default(Vector3);
					}
					break;
				}
			}
			else if (missionEvent <= 160313)
			{
				if (missionEvent != 159998)
				{
					if (missionEvent == 160313)
					{
						GameState.Get().SetBusy(true);
						yield return new WaitForSeconds(2.5f);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Bob_BrassRing_Quote, TB_RoadToNR.VO_DALA_900h_Male_Human_PlayerBrood_01, TB_RoadToNR.LEFT_OF_FRIENDLY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3f, false);
						GameState.Get().SetBusy(false);
					}
				}
				else
				{
					GameState.Get().SetBusy(true);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Start_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				switch (missionEvent)
				{
				case 259924:
					GameState.Get().SetBusy(true);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange1Response_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 0f, false);
					yield return new WaitForSeconds(3f);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange1Response_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
					GameState.Get().SetBusy(false);
					break;
				case 259925:
					GameState.Get().SetBusy(true);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss2_Exchange1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
					yield return new WaitForSeconds(2.5f);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Exchange1Response_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
					yield return new WaitForSeconds(2.5f);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange2_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 4f, false);
					GameState.Get().SetBusy(false);
					break;
				case 259926:
					GameState.Get().SetBusy(true);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Start_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 3.5f, false);
					GameState.Get().SetBusy(false);
					break;
				case 259927:
				case 259931:
				case 259932:
					break;
				case 259928:
					GameState.Get().SetBusy(true);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_HostLine_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2.5f, false);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_HostLineResponse_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 3.5f, false);
					GameState.Get().SetBusy(false);
					break;
				case 259929:
					GameState.Get().SetBusy(true);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DAL_719_Male_Kobold_Northrend_Boss6_Start_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
					yield return new WaitForSeconds(2.5f);
					GameState.Get().SetBusy(false);
					break;
				case 259930:
					GameState.Get().SetBusy(true);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Start_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 4f, false);
					yield return new WaitForSeconds(1f);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss7_StartResponse_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2.5f, false);
					GameState.Get().SetBusy(false);
					break;
				case 259933:
					GameState.Get().SetBusy(true);
					yield return new WaitForSeconds(0.5f);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Loss_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
					yield return new WaitForSeconds(2.5f);
					GameState.Get().SetBusy(false);
					break;
				default:
					if (missionEvent == 259998)
					{
						GameState.Get().SetBusy(true);
						yield return new WaitForSeconds(0.5f);
						Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Loss_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
						yield return new WaitForSeconds(2.5f);
						GameState.Get().SetBusy(false);
					}
					break;
				}
			}
		}
		else if (missionEvent <= 459930)
		{
			switch (missionEvent)
			{
			case 359924:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss1_Exchange2_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 0f, false);
				yield return new WaitForSeconds(2.5f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss1_Exchange2Response_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359925:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange3_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3f, false);
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359926:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_Exchange1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3.5f, false);
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359927:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Exchange1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2f, false);
				yield return new WaitForSeconds(2f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss3_Exchange1Response_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2f, false);
				yield return new WaitForSeconds(2f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_Exchange2_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3.5f, false);
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359928:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss5_Exchange1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 4.5f, false);
				yield return new WaitForSeconds(1f);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange1Response_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
				yield return new WaitForSeconds(2.5f);
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359929:
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(2f);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange2_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359930:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss7_Exchange1_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 4f, false);
				yield return new WaitForSeconds(1f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange1Response_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2.5f, false);
				GameState.Get().SetBusy(false);
				goto IL_2179;
			case 359931:
			case 359932:
				goto IL_2179;
			case 359933:
				break;
			default:
				if (missionEvent != 359998)
				{
					switch (missionEvent)
					{
					case 459924:
						GameState.Get().SetBusy(true);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange3Response_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 0f, false);
						yield return new WaitForSeconds(3f);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange3Response_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
						GameState.Get().SetBusy(false);
						goto IL_2179;
					case 459925:
						GameState.Get().SetBusy(true);
						yield return new WaitForSeconds(0.5f);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Victory_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 5f, false);
						GameState.Get().SetBusy(false);
						goto IL_2179;
					case 459926:
						GameState.Get().SetBusy(true);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss4_Exchange2_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3.5f, false);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange2Response_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 3.5f, false);
						GameState.Get().SetBusy(false);
						goto IL_2179;
					case 459927:
						GameState.Get().SetBusy(true);
						yield return new WaitForSeconds(0.5f);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss3_Victory_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 4f, false);
						GameState.Get().SetBusy(false);
						goto IL_2179;
					case 459928:
						GameState.Get().SetBusy(true);
						Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange2_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
						GameState.Get().SetBusy(false);
						goto IL_2179;
					case 459929:
						GameState.Get().SetBusy(true);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_Exchange1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 4f, false);
						yield return new WaitForSeconds(1f);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss6_Exchange1Response_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2.5f, false);
						GameState.Get().SetBusy(false);
						goto IL_2179;
					case 459930:
						GameState.Get().SetBusy(true);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Exchange2_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 4f, false);
						yield return new WaitForSeconds(1f);
						yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange2Response_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 2.5f, false);
						GameState.Get().SetBusy(false);
						goto IL_2179;
					default:
						goto IL_2179;
					}
				}
				break;
			}
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss8_Victory1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 5f, false);
			GameState.Get().SetBusy(false);
		}
		else if (missionEvent <= 659929)
		{
			switch (missionEvent)
			{
			case 559924:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Loss_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
				GameState.Get().SetBusy(false);
				break;
			case 559925:
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Loss_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
				break;
			case 559926:
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange3_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 3.5f, false);
				GameState.Get().SetBusy(false);
				break;
			case 559927:
				break;
			case 559928:
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(1f);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
				yield return new WaitForSeconds(3.5f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss5_Exchange3Response1_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 3f, false);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3Response2_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
				break;
			case 559929:
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(1f);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange3_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
				break;
			case 559930:
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(0.5f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Victory_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 3f, false);
				GameState.Get().SetBusy(false);
				break;
			default:
				switch (missionEvent)
				{
				case 659924:
					GameState.Get().SetBusy(true);
					yield return new WaitForSeconds(0.5f);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Victory_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 0f, false);
					yield return new WaitForSeconds(1f);
					GameState.Get().SetBusy(false);
					break;
				case 659926:
					GameState.Get().SetBusy(true);
					yield return new WaitForSeconds(0.5f);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Loss_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 4f, false);
					yield return new WaitForSeconds(3f);
					GameState.Get().SetBusy(false);
					break;
				case 659928:
					GameState.Get().SetBusy(true);
					yield return new WaitForSeconds(2f);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_RoadToNR.VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange4_01, Notification.SpeechBubbleDirection.TopLeft, enemyActor, 3f, 1f, true, false, 0f));
					GameState.Get().SetBusy(false);
					break;
				case 659929:
					GameState.Get().SetBusy(true);
					yield return new WaitForSeconds(0.5f);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory1_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 8f, false);
					yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory2_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2f, false);
					GameState.Get().SetBusy(false);
					break;
				}
				break;
			}
		}
		else if (missionEvent != 759926)
		{
			if (missionEvent == 759928)
			{
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(0.5f);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Brann_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_Victory_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2.5f, false);
				yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Elise_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_VictoryResponse_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 6f, false);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Finley_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Victory_01, TB_RoadToNR.RIGHT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopRight, 4f, false);
			yield return new WaitForSeconds(0.5f);
			yield return base.PlayMissionFlavorLine(TB_RoadToNR.Tombs_of_Terror_Reno_BrassRing_Quote, TB_RoadToNR.VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_VictoryResponse_01, TB_RoadToNR.LEFT_OF_ENEMY_HERO, Notification.SpeechBubbleDirection.TopLeft, 2f, false);
			GameState.Get().SetBusy(false);
		}
		IL_2179:
		yield break;
	}

	// Token: 0x06005102 RID: 20738 RVA: 0x001A9BB6 File Offset: 0x001A7DB6
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
		yield break;
	}

	// Token: 0x06005103 RID: 20739 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x040047C1 RID: 18369
	private static readonly AssetReference Tombs_of_Terror_Reno_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Reno_BrassRing_Quote.prefab:4c0b79d4f597c464baabf02e06cf8ae7");

	// Token: 0x040047C2 RID: 18370
	private static readonly AssetReference Tombs_of_Terror_Finley_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Finley_BrassRing_Quote.prefab:547ebc970764ec64da6eb3de26ed4698");

	// Token: 0x040047C3 RID: 18371
	private static readonly AssetReference Tombs_of_Terror_Brann_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Brann_BrassRing_Quote.prefab:d521a1fe41518e24da6e4252b97fbeb7");

	// Token: 0x040047C4 RID: 18372
	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	// Token: 0x040047C5 RID: 18373
	private static readonly AssetReference Bob_BrassRing_Quote = new AssetReference("Bob_BrassRing_Quote.prefab:89385ff7d67aa1e49bcf25bc15ca61f6");

	// Token: 0x040047C6 RID: 18374
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Start_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Start_01.prefab:057649bc2d715e54c90bc2eaf0c06cd0");

	// Token: 0x040047C7 RID: 18375
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange1Response_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange1Response_01.prefab:5a9b0920bb977d648a7ba8adc4ffe899");

	// Token: 0x040047C8 RID: 18376
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange1Response_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange1Response_01.prefab:09c3998f6955b9f419db61a586e81079");

	// Token: 0x040047C9 RID: 18377
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss1_Exchange2_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss1_Exchange2_01.prefab:5c3914c6ba61fa744b48603a37ebf66f");

	// Token: 0x040047CA RID: 18378
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss1_Exchange2Response_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss1_Exchange2Response_01.prefab:cc216a5c5160757469b6da3a933f5b40");

	// Token: 0x040047CB RID: 18379
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange3Response_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss1_Exchange3Response_01.prefab:7caffbd5bbef1c94c9b30e753e3c92b4");

	// Token: 0x040047CC RID: 18380
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange3Response_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Exchange3Response_01.prefab:2e78ea75e1d23b048bd6f6102d49b65d");

	// Token: 0x040047CD RID: 18381
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Loss_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Loss_01.prefab:3a17d9b891c780745b4e06cea06464a6");

	// Token: 0x040047CE RID: 18382
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Victory_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss1_Victory_01.prefab:14695e3b456b366468d89a5ba157d81c");

	// Token: 0x040047CF RID: 18383
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss2_StartResponse_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss2_StartResponse_01.prefab:a1451d0fedf18cf4c8f746a3e545e428");

	// Token: 0x040047D0 RID: 18384
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss2_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss2_Exchange1_01.prefab:37f8afe94af1ca243ad4a5ecca061c9d");

	// Token: 0x040047D1 RID: 18385
	private static readonly AssetReference VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Exchange1Response_01 = new AssetReference("VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Exchange1Response_01.prefab:dfbf26920132d64449d71ed0b44abea2");

	// Token: 0x040047D2 RID: 18386
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange2_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange2_01.prefab:8783ace3dd5add64d98f1d40d0a34f12");

	// Token: 0x040047D3 RID: 18387
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange3_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Exchange3_01.prefab:18b1bdb432448094e820521b4d9177b9");

	// Token: 0x040047D4 RID: 18388
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Victory_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss2_Victory_01.prefab:240d7f1e901556444a06dd3746e52f98");

	// Token: 0x040047D5 RID: 18389
	private static readonly AssetReference VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Loss_01 = new AssetReference("VO_TB_GALAPortals_OrgGuardH_Male_Orc_Northrend_Boss2_Loss_01.prefab:124dbe4154e5b23448fa034cb0b3da59");

	// Token: 0x040047D6 RID: 18390
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_HostLine_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_HostLine_01.prefab:42bcec03b7d533b41bbc388e7825d52f");

	// Token: 0x040047D7 RID: 18391
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Start_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Start_01.prefab:d23e1e7caf2455940bdc602de739e051");

	// Token: 0x040047D8 RID: 18392
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss3_Exchange1_01.prefab:fca58ee9a17805648a7160d39885ce92");

	// Token: 0x040047D9 RID: 18393
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss3_Exchange1Response_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss3_Exchange1Response_01.prefab:1c0bfa679ffa1ae489bb360eaa8c846f");

	// Token: 0x040047DA RID: 18394
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_Exchange2_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss3_Exchange2_01.prefab:610173ade6d3aa04e94d575b2238ff8e");

	// Token: 0x040047DB RID: 18395
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss3_Victory_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss3_Victory_01.prefab:1b8ee3bcf30f25340943c8ef75a75dfb");

	// Token: 0x040047DC RID: 18396
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_HostLine_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_HostLine_01.prefab:6ecc3b3012db61f4b947c7c8697174ea");

	// Token: 0x040047DD RID: 18397
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss4_HostLineResponse_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss4_HostLineResponse_01.prefab:d2aa424df7329f84b9c9e9f8ee42d967");

	// Token: 0x040047DE RID: 18398
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Start_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Start_01.prefab:89c73eb7bd94d2046ad9fc22d41ac434");

	// Token: 0x040047DF RID: 18399
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_Exchange1_01.prefab:5928ddb577a71924bbcb4edfae86b761");

	// Token: 0x040047E0 RID: 18400
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss4_Exchange2_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss4_Exchange2_01.prefab:6b476b6d90a675b4f988a659ec17af31");

	// Token: 0x040047E1 RID: 18401
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange2Response_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange2Response_01.prefab:84096eb707e0fbe4aa3b9f53803fb2b4");

	// Token: 0x040047E2 RID: 18402
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange3_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Exchange3_01.prefab:374a9663c1348b447b6c9759d28aee4f");

	// Token: 0x040047E3 RID: 18403
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Loss_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Loss_01.prefab:aa3c5c10d6a7a9444b7878fe5f19cd88");

	// Token: 0x040047E4 RID: 18404
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Victory_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss4_Victory_01.prefab:9e7b9fd0980e83b40870712e0a38182b");

	// Token: 0x040047E5 RID: 18405
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_VictoryResponse_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss4_VictoryResponse_01.prefab:bda4ef726431a7648992924367f2cb34");

	// Token: 0x040047E6 RID: 18406
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_HostLine_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_HostLine_01.prefab:06e6b3d43ba630f4595858fecdb3a175");

	// Token: 0x040047E7 RID: 18407
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_HostLineResponse_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_HostLineResponse_01.prefab:f7e40439e8c1372498485e68745b637c");

	// Token: 0x040047E8 RID: 18408
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Start_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Start_02.prefab:5abc6795f97b49e4abb6d20b1bd94af4");

	// Token: 0x040047E9 RID: 18409
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss5_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss5_Exchange1_01.prefab:f4f41f31cbd6eb3479d4eed327360a5e");

	// Token: 0x040047EA RID: 18410
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange1Response_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange1Response_01.prefab:e1c12b5789973b24eb89f341f6813e0b");

	// Token: 0x040047EB RID: 18411
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange2_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange2_01.prefab:d7f12649fd3a45d4793def83a3bf4513");

	// Token: 0x040047EC RID: 18412
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3_01.prefab:a383f5fb67af80c449320d2e5ba5eb9c");

	// Token: 0x040047ED RID: 18413
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss5_Exchange3Response1_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss5_Exchange3Response1_01.prefab:d07c98f264a7abb459f8e454f3b5e1d6");

	// Token: 0x040047EE RID: 18414
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3Response2_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange3Response2_01.prefab:e47388e8b2daa18428faa644b1bf3fd9");

	// Token: 0x040047EF RID: 18415
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange4_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Northrend_Boss5_Exchange4_01.prefab:aad73eb715845014c96129f7198f120e");

	// Token: 0x040047F0 RID: 18416
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_Victory_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss5_Victory_01.prefab:840ef4dd806d5724a90f04a479dfea2c");

	// Token: 0x040047F1 RID: 18417
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_VictoryResponse_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss5_VictoryResponse_01.prefab:13e14a1a628e9764483d4922a97515fa");

	// Token: 0x040047F2 RID: 18418
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_HostLine_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_HostLine_01.prefab:27ea455959d060c4589b311500fbb3fb");

	// Token: 0x040047F3 RID: 18419
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_HostLineResponse_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_HostLineResponse_01.prefab:733c2d96d4859174aafc294fa001e572");

	// Token: 0x040047F4 RID: 18420
	private static readonly AssetReference VO_DAL_719_Male_Kobold_Northrend_Boss6_Start_01 = new AssetReference("VO_DAL_719_Male_Kobold_Northrend_Boss6_Start_01.prefab:28c4a1bb916b3c346bba998d31097341");

	// Token: 0x040047F5 RID: 18421
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss6_Exchange1_01.prefab:be63fedec4ef8df448204c4d43be6c33");

	// Token: 0x040047F6 RID: 18422
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss6_Exchange1Response_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss6_Exchange1Response_01.prefab:f98a0ae34c0916e4c80d428d06a57b9a");

	// Token: 0x040047F7 RID: 18423
	private static readonly AssetReference VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange2_01 = new AssetReference("VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange2_01.prefab:2c14109e147f0844c97df0e2e0d24265");

	// Token: 0x040047F8 RID: 18424
	private static readonly AssetReference VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange3_01 = new AssetReference("VO_DAL_719_Male_Kobold_Northrend_Boss6_Exchange3_01.prefab:41544ce2729bf9d41974b604d70060df");

	// Token: 0x040047F9 RID: 18425
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory1_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory1_01.prefab:9dc21085ed648c8458fae6040c8213a8");

	// Token: 0x040047FA RID: 18426
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory2_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss6_Victory2_01.prefab:585a13de5c099a1428fd038c482150c6");

	// Token: 0x040047FB RID: 18427
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_HostLine_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_HostLine_01.prefab:85b4cd41119500e4a9d1fbe439c51b3e");

	// Token: 0x040047FC RID: 18428
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Start_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Start_01.prefab:b4ec9ab6a5cef8844ada36c4db428c8a");

	// Token: 0x040047FD RID: 18429
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss7_StartResponse_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Northrend_Boss7_StartResponse_01.prefab:b78bcadb4150612439896840737ce0c1");

	// Token: 0x040047FE RID: 18430
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss7_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss7_Exchange1_01.prefab:85c141a0c9c42894aa60d8c95e3265d3");

	// Token: 0x040047FF RID: 18431
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange1Response_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange1Response_01.prefab:5df5394abd7f91d4496cd61a8b348133");

	// Token: 0x04004800 RID: 18432
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Exchange2_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Exchange2_01.prefab:1ecc65440ee806d458f2fb146da958c5");

	// Token: 0x04004801 RID: 18433
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange2Response_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss7_Exchange2Response_01.prefab:cca9f91532be6f2468af1e4ce96a771c");

	// Token: 0x04004802 RID: 18434
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Victory_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss7_Victory_01.prefab:39953f9661708234a8ccf94d6f35cd22");

	// Token: 0x04004803 RID: 18435
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Start_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Start_01.prefab:5ad5b4d2a50aac1499b1f1868e2b84f7");

	// Token: 0x04004804 RID: 18436
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Start_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Start_01.prefab:3fa81cc157bb74a42ad56ab662f79edd");

	// Token: 0x04004805 RID: 18437
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Loss_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Northrend_Boss8_Loss_01.prefab:f01d31b97c6c47640bfaae1d407037f0");

	// Token: 0x04004806 RID: 18438
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Loss_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Northrend_Boss8_Loss_01.prefab:435120e5f0bc0784cbc143a30a9a78d9");

	// Token: 0x04004807 RID: 18439
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss8_Victory1_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Northrend_Boss8_Victory1_01.prefab:63cee9eae5ff3b74cbb55a39d5061422");

	// Token: 0x04004808 RID: 18440
	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerBrood_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerBrood_01.prefab:456313fb3b1046a47a8313a452bb6415");

	// Token: 0x04004809 RID: 18441
	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(1.3f, 0f, 1f);

	// Token: 0x0400480A RID: 18442
	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(1.3f, 0f, -1.8f);

	// Token: 0x0400480B RID: 18443
	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6.3f, 0f, -1.8f);

	// Token: 0x0400480C RID: 18444
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x0400480D RID: 18445
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x0400480E RID: 18446
	private HashSet<int> seen = new HashSet<int>();

	// Token: 0x0400480F RID: 18447
	private static readonly Dictionary<int, TB_RoadToNR.PopupMessage> popupMsgs = new Dictionary<int, TB_RoadToNR.PopupMessage>
	{
		{
			1001,
			new TB_RoadToNR.PopupMessage
			{
				Message = "VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01"
			}
		},
		{
			2002,
			new TB_RoadToNR.PopupMessage
			{
				Message = "VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01"
			}
		}
	};

	// Token: 0x04004810 RID: 18448
	private Notification m_popup;

	// Token: 0x04004811 RID: 18449
	private float popupScale = 1.4f;

	// Token: 0x04004812 RID: 18450
	private Entity playerEntity;

	// Token: 0x04004813 RID: 18451
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04004814 RID: 18452
	private int m_PlayBossVOLineIndex;

	// Token: 0x02001FCB RID: 8139
	public struct PopupMessage
	{
		// Token: 0x0400DAAB RID: 55979
		public string Message;

		// Token: 0x0400DAAC RID: 55980
		public float Delay;
	}
}
