using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_TempleOutrun : MissionEntity
{
	public struct PopupMessage
	{
		public string Message;

		public float Delay;
	}

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHIntro_02 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHIntro_02.prefab:0dc446d089c1c6142819ecd89009e9bf");

	private static readonly AssetReference VO_HeadlessHorseman_Male_Human_HHReaction1_01 = new AssetReference("VO_HeadlessHorseman_Male_Human_HHReaction1_01.prefab:8443a7874cc9cbb48a30f57d69e1b431");

	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	private Notification m_popup;

	private float popupScale = 1.4f;

	private Entity playerEntity;

	private static readonly Dictionary<int, PopupMessage> popupMsgs;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, VO_HeadlessHorseman_Male_Human_HHIntro_02, VO_HeadlessHorseman_Male_Human_HHReaction1_01
		})
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 1000:
		{
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			string msgString;
			if (tag == 0)
			{
				msgString = GameStrings.Get(popupMsgs[2000].Message);
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
				msgString = GameStrings.Get(popupMsgs[missionEvent].Message) + "\n" + text + num + ":" + text2 + num2 + ":" + text3 + num3;
				popupScale = 1.7f;
			}
			Vector3 popUpPos = default(Vector3);
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
			}
			else
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? (-40f) : (-40f));
			}
			yield return new WaitForSeconds(4f);
			yield return ShowPopup(msgString, popupMsgs[missionEvent].Delay, popUpPos, popupScale);
			popUpPos = default(Vector3);
			break;
		}
		case 100:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HHIntro_02, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 101:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_HeadlessHorseman_Male_Human_HHReaction1_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 505:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01);
			break;
		case 506:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01);
			break;
		case 507:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01);
			break;
		case 508:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01);
			break;
		case 509:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01);
			break;
		case 510:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01);
			break;
		case 511:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01);
			break;
		case 512:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01);
			break;
		case 513:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01);
			break;
		case 514:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01);
			break;
		case 515:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01);
			break;
		case 516:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01);
			break;
		case 517:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01);
			break;
		case 518:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01);
			break;
		case 519:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01);
			break;
		case 520:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01);
			break;
		case 521:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01);
			break;
		case 522:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01);
			break;
		case 523:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01);
			break;
		case 524:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01);
			break;
		case 525:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01);
			break;
		}
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
	}

	static TB_TempleOutrun()
	{
		Dictionary<int, PopupMessage> dictionary = new Dictionary<int, PopupMessage>();
		PopupMessage value = new PopupMessage
		{
			Message = "TB_EVILBRM_CURRENT_BEST_SCORE",
			Delay = 5f
		};
		dictionary.Add(1000, value);
		value = new PopupMessage
		{
			Message = "TB_EVILBRM_NEW_BEST_SCORE",
			Delay = 5f
		};
		dictionary.Add(2000, value);
		popupMsgs = dictionary;
	}
}
