using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005C5 RID: 1477
public class TB_TrollsWeek1 : MissionEntity
{
	// Token: 0x06005150 RID: 20816 RVA: 0x001AC06B File Offset: 0x001AA26B
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_11);
		base.PreloadSound(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_03);
		base.PreloadSound(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_09);
	}

	// Token: 0x06005151 RID: 20817 RVA: 0x001AC09D File Offset: 0x001AA29D
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06005152 RID: 20818 RVA: 0x001AC0AF File Offset: 0x001AA2AF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 10:
			this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			this.shouldShowIntro = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			if (this.shouldShowIntro == 1)
			{
				yield return this.PlayBossLine(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_11, false);
				yield return new WaitForSeconds(4f);
			}
			break;
		case 11:
			yield return this.PlayBossLine(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_03, false);
			yield return new WaitForSeconds(4f);
			break;
		case 12:
			yield return this.PlayBossLine(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_09, false);
			yield return new WaitForSeconds(4f);
			break;
		}
		yield break;
	}

	// Token: 0x06005153 RID: 20819 RVA: 0x001AC0C5 File Offset: 0x001AA2C5
	private IEnumerator PlayBossLine(string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		yield return base.PlayMissionFlavorLine("Rastakhan_BrassRing_Quote:179bfad1464576448aeabfe5c3eff601", line, TB_TrollsWeek1.LEFT_OF_FRIENDLY_HERO, direction, 2.5f, persistCharacter);
		yield break;
	}

	// Token: 0x06005154 RID: 20820 RVA: 0x001AC0E4 File Offset: 0x001AA2E4
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			this.matchResult = TB_TrollsWeek1.VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			this.matchResult = TB_TrollsWeek1.VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			this.matchResult = TB_TrollsWeek1.VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x06005155 RID: 20821 RVA: 0x001AC131 File Offset: 0x001AA331
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		this.shouldShowVictory = this.friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		yield return new WaitForSeconds(2f);
		switch (this.matchResult)
		{
		case TB_TrollsWeek1.VICTOR.PLAYERLOST:
			GameState.Get().SetBusy(true);
			GameState.Get().SetBusy(false);
			break;
		case TB_TrollsWeek1.VICTOR.PLAYERWIN:
			if (this.shouldShowVictory == 1)
			{
				yield return new WaitForSeconds(3f);
				GameState.Get().SetBusy(true);
				yield return this.PlayBossLine(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_03, false);
				yield return this.PlayBossLine(TB_TrollsWeek1.VO_HERO_02b_Male_Troll_Event_09, false);
				GameState.Get().SetBusy(false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x040048B3 RID: 18611
	private TB_TrollsWeek1.VICTOR matchResult;

	// Token: 0x040048B4 RID: 18612
	private Notification StartPopup;

	// Token: 0x040048B5 RID: 18613
	private int shouldShowVictory;

	// Token: 0x040048B6 RID: 18614
	private int shouldShowIntro;

	// Token: 0x040048B7 RID: 18615
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_11 = new AssetReference("VO_HERO_02b_Male_Troll_Event_11:e360856574d463247960068d89134791");

	// Token: 0x040048B8 RID: 18616
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_03 = new AssetReference("VO_HERO_02b_Male_Troll_Event_03:7a6078498a8e2dc4284fc70f8d37faf4");

	// Token: 0x040048B9 RID: 18617
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Event_09 = new AssetReference("VO_HERO_02b_Male_Troll_Event_09:2b061472d8f0e4549801ff0c25d8d686");

	// Token: 0x040048BA RID: 18618
	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	// Token: 0x040048BB RID: 18619
	private Player friendlySidePlayer;

	// Token: 0x02001FE7 RID: 8167
	private enum VICTOR
	{
		// Token: 0x0400DB31 RID: 56113
		PLAYERLOST,
		// Token: 0x0400DB32 RID: 56114
		PLAYERWIN,
		// Token: 0x0400DB33 RID: 56115
		ERROR
	}
}
