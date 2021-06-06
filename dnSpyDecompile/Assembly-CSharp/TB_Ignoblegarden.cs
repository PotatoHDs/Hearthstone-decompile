using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B2 RID: 1458
public class TB_Ignoblegarden : MissionEntity
{
	// Token: 0x060050C7 RID: 20679 RVA: 0x001A8A40 File Offset: 0x001A6C40
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_01);
		base.PreloadSound(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_02);
		base.PreloadSound(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_03);
		base.PreloadSound(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_04);
		base.PreloadSound(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_05);
		base.PreloadSound(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_06);
		base.PreloadSound(TB_Ignoblegarden.VO_HERO_02b_Male_Troll_Event_11);
	}

	// Token: 0x060050C8 RID: 20680 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060050C9 RID: 20681 RVA: 0x001A8ABD File Offset: 0x001A6CBD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (this.seen.Contains(missionEvent))
		{
			yield break;
		}
		this.seen.Add(missionEvent);
		switch (missionEvent)
		{
		case 1:
			yield return this.PlayBossLine(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_01, false);
			break;
		case 2:
			yield return this.PlayBossLine(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_02, false);
			break;
		case 3:
			yield return this.PlayBossLine(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_03, false);
			break;
		case 4:
			yield return this.PlayBossLine(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_04, false);
			break;
		case 5:
			yield return this.PlayBossLine(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_05, false);
			break;
		case 6:
			yield return this.PlayBossLine(TB_Ignoblegarden.VO_DrBoom_Male_Goblin_Ignoblegarden_06, false);
			break;
		}
		yield return new WaitForSeconds(4f);
		yield break;
	}

	// Token: 0x060050CA RID: 20682 RVA: 0x001A8AD3 File Offset: 0x001A6CD3
	private IEnumerator PlayBossLine(string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		yield return base.PlayMissionFlavorLine("DrBoom_BrassRing_Quote:01c6f6e5b12fb0e4cbb9adde214ac8dc", line, TB_Ignoblegarden.LEFT_OF_FRIENDLY_HERO, direction, 2.5f, persistCharacter);
		yield break;
	}

	// Token: 0x04004761 RID: 18273
	private HashSet<int> seen = new HashSet<int>();

	// Token: 0x04004762 RID: 18274
	private TB_Ignoblegarden.VICTOR matchResult;

	// Token: 0x04004763 RID: 18275
	private Notification StartPopup;

	// Token: 0x04004764 RID: 18276
	private int shouldShowVictory;

	// Token: 0x04004765 RID: 18277
	private int shouldShowIntro;

	// Token: 0x04004766 RID: 18278
	private static readonly AssetReference VO_DrBoom_Male_Goblin_Ignoblegarden_01 = new AssetReference("VO_DrBoom_Male_Goblin_Ignoblegarden_01:d5352ba39a24f7e40b490823b44249a2");

	// Token: 0x04004767 RID: 18279
	private static readonly AssetReference VO_DrBoom_Male_Goblin_Ignoblegarden_02 = new AssetReference("VO_DrBoom_Male_Goblin_Ignoblegarden_02:1cd6b63958bd93845a4339e493869ef2");

	// Token: 0x04004768 RID: 18280
	private static readonly AssetReference VO_DrBoom_Male_Goblin_Ignoblegarden_03 = new AssetReference("VO_DrBoom_Male_Goblin_Ignoblegarden_03:5bd75d7de52abed44a1cbb07a2d5d65b");

	// Token: 0x04004769 RID: 18281
	private static readonly AssetReference VO_DrBoom_Male_Goblin_Ignoblegarden_04 = new AssetReference("VO_DrBoom_Male_Goblin_Ignoblegarden_04:86b663815d9d8fc43806cb55c41df1dc");

	// Token: 0x0400476A RID: 18282
	private static readonly AssetReference VO_DrBoom_Male_Goblin_Ignoblegarden_05 = new AssetReference("VO_DrBoom_Male_Goblin_Ignoblegarden_05:bf12e1c7bd80d724d8050188a7ea4ec3");

	// Token: 0x0400476B RID: 18283
	private static readonly AssetReference VO_DrBoom_Male_Goblin_Ignoblegarden_06 = new AssetReference("VO_DrBoom_Male_Goblin_Ignoblegarden_06:2e155dd569b89f145967acb09a2d59a7");

	// Token: 0x0400476C RID: 18284
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_11 = new AssetReference("VO_HERO_02b_Male_Troll_Event_11:e360856574d463247960068d89134791");

	// Token: 0x0400476D RID: 18285
	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	// Token: 0x02001FBD RID: 8125
	private enum VICTOR
	{
		// Token: 0x0400DA6E RID: 55918
		PLAYERLOST,
		// Token: 0x0400DA6F RID: 55919
		PLAYERWIN,
		// Token: 0x0400DA70 RID: 55920
		ERROR
	}
}
