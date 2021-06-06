using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005BB RID: 1467
public class TB_NewYearRaven : MissionEntity
{
	// Token: 0x060050F6 RID: 20726 RVA: 0x001A9560 File Offset: 0x001A7760
	public TB_NewYearRaven()
	{
		this.usedMissionsEvents = new List<int>();
	}

	// Token: 0x060050F7 RID: 20727 RVA: 0x001A9574 File Offset: 0x001A7774
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02);
		base.PreloadSound(TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03);
		base.PreloadSound(TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05);
		base.PreloadSound(TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08);
		base.PreloadSound(TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09);
		base.PreloadSound(TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10);
	}

	// Token: 0x060050F8 RID: 20728 RVA: 0x001A95E1 File Offset: 0x001A77E1
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 99)
		{
			this.linePlayedThisTurn = false;
			yield break;
		}
		if (this.usedMissionsEvents.Contains(missionEvent))
		{
			yield break;
		}
		if (missionEvent != 12 && missionEvent != 13 && GameState.Get().GetGameEntity().GetTag(GAME_TAG.TURN) <= 3)
		{
			yield break;
		}
		if (this.linePlayedThisTurn)
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 12:
			this.usedMissionsEvents.Add(missionEvent);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02"), TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02, 0f, null, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			this.linePlayedThisTurn = true;
			break;
		case 13:
			this.usedMissionsEvents.Add(missionEvent);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03"), TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03, 0f, null, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			this.linePlayedThisTurn = true;
			break;
		case 15:
			this.usedMissionsEvents.Add(missionEvent);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05"), TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05, 0f, null, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			this.linePlayedThisTurn = true;
			break;
		case 18:
			this.usedMissionsEvents.Add(missionEvent);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08"), TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08, 0f, null, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			this.linePlayedThisTurn = true;
			break;
		case 19:
			this.usedMissionsEvents.Add(missionEvent);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09"), TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09, 0f, null, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			this.linePlayedThisTurn = true;
			break;
		case 20:
			this.usedMissionsEvents.Add(missionEvent);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10"), TB_NewYearRaven.VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10, 0f, null, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			this.linePlayedThisTurn = true;
			break;
		}
		yield break;
	}

	// Token: 0x040047B9 RID: 18361
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_02.prefab:d0725dde1600fb945a3ff082fbf63d66");

	// Token: 0x040047BA RID: 18362
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_03.prefab:f32ae61fac251044a98bb410b40098a2");

	// Token: 0x040047BB RID: 18363
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_05.prefab:ac73d74050101514ba8a96cc1acb69e4");

	// Token: 0x040047BC RID: 18364
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_08.prefab:e4a9d91e20a222b4bbb557db67e8fb4c");

	// Token: 0x040047BD RID: 18365
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_09.prefab:2f60b28058016a346adebc684daf562c");

	// Token: 0x040047BE RID: 18366
	private static readonly AssetReference VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10 = new AssetReference("VO_INNKEEPER_Male_Dwarf_HSNewYearBrawl_10.prefab:d87eff5930c85e544aaedfa1d7b273db");

	// Token: 0x040047BF RID: 18367
	private List<int> usedMissionsEvents;

	// Token: 0x040047C0 RID: 18368
	private bool linePlayedThisTurn;
}
