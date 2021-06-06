using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005C3 RID: 1475
public class TB_SPT_DALA : MissionEntity
{
	// Token: 0x06005141 RID: 20801 RVA: 0x001ABA00 File Offset: 0x001A9C00
	public override void PreloadAssets()
	{
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_05h_Male_Goblin_WinterEVIL_Exchange6_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_06h_Female_Orc_WinterEVIL_Exchange10_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Exchange1_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Loss_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_02);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_03);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Victory_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_08h_Male_Kobold_WinterEVIL_Exchange4_01);
		base.PreloadSound(TB_SPT_DALA.VO_DRGA_BOSS_09h_Female_Troll_WinterEVIL_Exchange8_01);
	}

	// Token: 0x06005142 RID: 20802 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06005143 RID: 20803 RVA: 0x001ABAAD File Offset: 0x001A9CAD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 101:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.RAFAAM, TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_01, false);
			break;
		case 102:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.RAFAAM, TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_02, false);
			yield return new WaitForSeconds(1f);
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.RAFAAM, TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_03, false);
			break;
		case 103:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.RAFAAM, TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Exchange1_01, false);
			break;
		case 104:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.BOOM, TB_SPT_DALA.VO_DRGA_BOSS_05h_Male_Goblin_WinterEVIL_Exchange6_01, false);
			break;
		case 105:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.HAGATHA, TB_SPT_DALA.VO_DRGA_BOSS_06h_Female_Orc_WinterEVIL_Exchange10_01, false);
			break;
		case 107:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.LAZUL, TB_SPT_DALA.VO_DRGA_BOSS_09h_Female_Troll_WinterEVIL_Exchange8_01, false);
			break;
		case 108:
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.TOGWAGGLE, TB_SPT_DALA.VO_DRGA_BOSS_08h_Male_Kobold_WinterEVIL_Exchange4_01, false);
			break;
		}
		yield break;
	}

	// Token: 0x06005144 RID: 20804 RVA: 0x001ABAC3 File Offset: 0x001A9CC3
	private IEnumerator PlayBossLine(string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		yield return base.PlayMissionFlavorLine("Rastakhan_BrassRing_Quote:179bfad1464576448aeabfe5c3eff601", line, TB_SPT_DALA.LEFT_OF_FRIENDLY_HERO, direction, 2.5f, persistCharacter);
		yield break;
	}

	// Token: 0x06005145 RID: 20805 RVA: 0x001ABAE0 File Offset: 0x001A9CE0
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			this.matchResult = TB_SPT_DALA.VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			this.matchResult = TB_SPT_DALA.VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			this.matchResult = TB_SPT_DALA.VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x06005146 RID: 20806 RVA: 0x001ABB2D File Offset: 0x001A9D2D
	private IEnumerator PlayBossLineLeft(TB_SPT_DALA.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopLeft;
		switch (boss)
		{
		case TB_SPT_DALA.BOSS.BOOM:
			yield return base.PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, TB_SPT_DALA.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.HAGATHA:
			yield return base.PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, TB_SPT_DALA.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.TOGWAGGLE:
			yield return base.PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, TB_SPT_DALA.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.LAZUL:
			yield return base.PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, TB_SPT_DALA.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.RAFAAM:
			yield return base.PlayMissionFlavorLine("Rafaam_popup_BrassRing_Quote.prefab:187724fae6d64cf49acf11aa53ca2087", line, TB_SPT_DALA.LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
		yield break;
	}

	// Token: 0x06005147 RID: 20807 RVA: 0x001ABB51 File Offset: 0x001A9D51
	private IEnumerator PlayBossLineRight(TB_SPT_DALA.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
		switch (boss)
		{
		case TB_SPT_DALA.BOSS.BOOM:
			yield return base.PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, TB_SPT_DALA.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.HAGATHA:
			yield return base.PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, TB_SPT_DALA.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.TOGWAGGLE:
			yield return base.PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, TB_SPT_DALA.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.LAZUL:
			yield return base.PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, TB_SPT_DALA.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case TB_SPT_DALA.BOSS.RAFAAM:
			yield return base.PlayMissionFlavorLine("Rafaam_popup_BrassRing_Quote.prefab:187724fae6d64cf49acf11aa53ca2087", line, TB_SPT_DALA.RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
		yield break;
	}

	// Token: 0x06005148 RID: 20808 RVA: 0x001ABB75 File Offset: 0x001A9D75
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(false);
		switch (this.matchResult)
		{
		case TB_SPT_DALA.VICTOR.PLAYERLOST:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.RAFAAM, TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Loss_01, false);
			GameState.Get().SetBusy(false);
			break;
		case TB_SPT_DALA.VICTOR.PLAYERWIN:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLineLeft(TB_SPT_DALA.BOSS.RAFAAM, TB_SPT_DALA.VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Victory_01, false);
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x04004889 RID: 18569
	private TB_SPT_DALA.VICTOR matchResult;

	// Token: 0x0400488A RID: 18570
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_WinterEVIL_Exchange6_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_WinterEVIL_Exchange6_01.prefab:334f3d6a44555ea45badbedcf4f49248");

	// Token: 0x0400488B RID: 18571
	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_WinterEVIL_Exchange10_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_WinterEVIL_Exchange10_01.prefab:68676ec9aa6516246ada918a62545dfc");

	// Token: 0x0400488C RID: 18572
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Exchange1_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Exchange1_01.prefab:f1b2b04920a7b7041ba167c3e8f387f5");

	// Token: 0x0400488D RID: 18573
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Loss_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Loss_01.prefab:dedb812e87f1b4c49b972140a35db48c");

	// Token: 0x0400488E RID: 18574
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_01.prefab:6b860d24c5fc93344971ace72547d60b");

	// Token: 0x0400488F RID: 18575
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_02 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_02.prefab:6f65ddd984b97d94a93a5ae316fe0bf5");

	// Token: 0x04004890 RID: 18576
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_03 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Start_03.prefab:2d3ab2f215843064c8b7aea4dd6d0f89");

	// Token: 0x04004891 RID: 18577
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Victory_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_WinterEVIL_Victory_01.prefab:33e07132a9c1ea741b5fd8b807e799b9");

	// Token: 0x04004892 RID: 18578
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_WinterEVIL_Exchange4_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_WinterEVIL_Exchange4_01.prefab:1ad7915c429c71645a840c5b39e127db");

	// Token: 0x04004893 RID: 18579
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_WinterEVIL_Exchange8_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_WinterEVIL_Exchange8_01.prefab:da1b10361658a1341b1fa8f97d041a26");

	// Token: 0x04004894 RID: 18580
	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	// Token: 0x04004895 RID: 18581
	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -2.8f);

	// Token: 0x04004896 RID: 18582
	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -2.8f);

	// Token: 0x02001FDD RID: 8157
	private enum VICTOR
	{
		// Token: 0x0400DAFF RID: 56063
		PLAYERLOST,
		// Token: 0x0400DB00 RID: 56064
		PLAYERWIN,
		// Token: 0x0400DB01 RID: 56065
		ERROR
	}

	// Token: 0x02001FDE RID: 8158
	private enum BOSS
	{
		// Token: 0x0400DB03 RID: 56067
		BOOM,
		// Token: 0x0400DB04 RID: 56068
		HAGATHA,
		// Token: 0x0400DB05 RID: 56069
		TOGWAGGLE,
		// Token: 0x0400DB06 RID: 56070
		LAZUL,
		// Token: 0x0400DB07 RID: 56071
		RAFAAM
	}
}
