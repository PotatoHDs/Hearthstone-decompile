using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_NewYearRaven : MissionEntity
{
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02.prefab:d0725dde1600fb945a3ff082fbf63d66");

	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03.prefab:f32ae61fac251044a98bb410b40098a2");

	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05.prefab:ac73d74050101514ba8a96cc1acb69e4");

	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08.prefab:e4a9d91e20a222b4bbb557db67e8fb4c");

	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09.prefab:2f60b28058016a346adebc684daf562c");

	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10.prefab:d87eff5930c85e544aaedfa1d7b273db");

	private List<int> usedMissionsEvents;

	private bool linePlayedThisTurn;

	public TB_NewYearRaven()
	{
		usedMissionsEvents = new List<int>();
	}

	public override void PreloadAssets()
	{
		PreloadSound(VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02);
		PreloadSound(VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03);
		PreloadSound(VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05);
		PreloadSound(VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08);
		PreloadSound(VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09);
		PreloadSound(VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10);
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 99)
		{
			linePlayedThisTurn = false;
		}
		else if (!usedMissionsEvents.Contains(missionEvent) && (missionEvent == 12 || missionEvent == 13 || GameState.Get().GetGameEntity().GetTag(GAME_TAG.TURN) > 3) && !linePlayedThisTurn)
		{
			switch (missionEvent)
			{
			case 12:
				usedMissionsEvents.Add(missionEvent);
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02"), VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02", "", Notification.SpeechBubbleDirection.None, null));
				linePlayedThisTurn = true;
				break;
			case 13:
				usedMissionsEvents.Add(missionEvent);
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03"), VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03", "", Notification.SpeechBubbleDirection.None, null));
				linePlayedThisTurn = true;
				break;
			case 15:
				usedMissionsEvents.Add(missionEvent);
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05"), VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05", "", Notification.SpeechBubbleDirection.None, null));
				linePlayedThisTurn = true;
				break;
			case 18:
				usedMissionsEvents.Add(missionEvent);
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08"), VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08", "", Notification.SpeechBubbleDirection.None, null));
				linePlayedThisTurn = true;
				break;
			case 19:
				usedMissionsEvents.Add(missionEvent);
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09"), VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09", "", Notification.SpeechBubbleDirection.None, null));
				linePlayedThisTurn = true;
				break;
			case 20:
				usedMissionsEvents.Add(missionEvent);
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10"), VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10", "", Notification.SpeechBubbleDirection.None, null));
				linePlayedThisTurn = true;
				break;
			}
		}
	}
}
