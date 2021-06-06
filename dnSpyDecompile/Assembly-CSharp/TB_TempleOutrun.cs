using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C4 RID: 1476
public class TB_TempleOutrun : MissionEntity
{
	// Token: 0x0600514B RID: 20811 RVA: 0x001ABC74 File Offset: 0x001A9E74
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01,
			TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01,
			TB_TempleOutrun.VO_HeadlessHorseman_Male_Human_HHIntro_02,
			TB_TempleOutrun.VO_HeadlessHorseman_Male_Human_HHReaction1_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600514C RID: 20812 RVA: 0x001ABE3C File Offset: 0x001AA03C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent <= 101)
		{
			if (missionEvent != 100)
			{
				if (missionEvent == 101)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_TempleOutrun.VO_HeadlessHorseman_Male_Human_HHReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
				}
			}
			else
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(TB_TempleOutrun.VO_HeadlessHorseman_Male_Human_HHIntro_02, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 505:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, 2.5f);
				break;
			case 506:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, 2.5f);
				break;
			case 507:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, 2.5f);
				break;
			case 508:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, 2.5f);
				break;
			case 509:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, 2.5f);
				break;
			case 510:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, 2.5f);
				break;
			case 511:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, 2.5f);
				break;
			case 512:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, 2.5f);
				break;
			case 513:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, 2.5f);
				break;
			case 514:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, 2.5f);
				break;
			case 515:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, 2.5f);
				break;
			case 516:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, 2.5f);
				break;
			case 517:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, 2.5f);
				break;
			case 518:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, 2.5f);
				break;
			case 519:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, 2.5f);
				break;
			case 520:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, 2.5f);
				break;
			case 521:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, 2.5f);
				break;
			case 522:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, 2.5f);
				break;
			case 523:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, 2.5f);
				break;
			case 524:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, 2.5f);
				break;
			case 525:
				yield return base.PlayBossLine(TB_TempleOutrun.Wisdomball_Pop_up_BrassRing_Quote, TB_TempleOutrun.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, 2.5f);
				break;
			default:
				if (missionEvent == 1000)
				{
					int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
					string msgString;
					if (tag == 0)
					{
						msgString = GameStrings.Get(TB_TempleOutrun.popupMsgs[2000].Message);
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
							GameStrings.Get(TB_TempleOutrun.popupMsgs[missionEvent].Message),
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
					yield return this.ShowPopup(msgString, TB_TempleOutrun.popupMsgs[missionEvent].Delay, popUpPos, this.popupScale);
					msgString = null;
					popUpPos = default(Vector3);
				}
				break;
			}
		}
		yield break;
	}

	// Token: 0x0600514D RID: 20813 RVA: 0x001ABE52 File Offset: 0x001AA052
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
		yield break;
	}

	// Token: 0x04004897 RID: 18583
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	// Token: 0x04004898 RID: 18584
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	// Token: 0x04004899 RID: 18585
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	// Token: 0x0400489A RID: 18586
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	// Token: 0x0400489B RID: 18587
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	// Token: 0x0400489C RID: 18588
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	// Token: 0x0400489D RID: 18589
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	// Token: 0x0400489E RID: 18590
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	// Token: 0x0400489F RID: 18591
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	// Token: 0x040048A0 RID: 18592
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	// Token: 0x040048A1 RID: 18593
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	// Token: 0x040048A2 RID: 18594
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	// Token: 0x040048A3 RID: 18595
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	// Token: 0x040048A4 RID: 18596
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	// Token: 0x040048A5 RID: 18597
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	// Token: 0x040048A6 RID: 18598
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	// Token: 0x040048A7 RID: 18599
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	// Token: 0x040048A8 RID: 18600
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	// Token: 0x040048A9 RID: 18601
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	// Token: 0x040048AA RID: 18602
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	// Token: 0x040048AB RID: 18603
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	// Token: 0x040048AC RID: 18604
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHIntro_02 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHIntro_02.prefab:0dc446d089c1c6142819ecd89009e9bf");

	// Token: 0x040048AD RID: 18605
	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHReaction1_01 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHReaction1_01.prefab:8443a7874cc9cbb48a30f57d69e1b431");

	// Token: 0x040048AE RID: 18606
	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	// Token: 0x040048AF RID: 18607
	private Notification m_popup;

	// Token: 0x040048B0 RID: 18608
	private float popupScale = 1.4f;

	// Token: 0x040048B1 RID: 18609
	private Entity playerEntity;

	// Token: 0x040048B2 RID: 18610
	private static readonly Dictionary<int, TB_TempleOutrun.PopupMessage> popupMsgs = new Dictionary<int, TB_TempleOutrun.PopupMessage>
	{
		{
			1000,
			new TB_TempleOutrun.PopupMessage
			{
				Message = "TB_EVILBRM_CURRENT_BEST_SCORE",
				Delay = 5f
			}
		},
		{
			2000,
			new TB_TempleOutrun.PopupMessage
			{
				Message = "TB_EVILBRM_NEW_BEST_SCORE",
				Delay = 5f
			}
		}
	};

	// Token: 0x02001FE4 RID: 8164
	public struct PopupMessage
	{
		// Token: 0x0400DB20 RID: 56096
		public string Message;

		// Token: 0x0400DB21 RID: 56097
		public float Delay;
	}
}
