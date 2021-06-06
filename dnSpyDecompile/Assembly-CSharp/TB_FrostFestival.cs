using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005AE RID: 1454
public class TB_FrostFestival : MissionEntity
{
	// Token: 0x060050A9 RID: 20649 RVA: 0x001A7FBC File Offset: 0x001A61BC
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_01.prefab:e90eb948480f3794da67e53ab145c36e");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_02.prefab:d22af95d6e42bd14583ae80f65688618");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_03.prefab:39a4f6504daf0744ebd50da61db59cf7");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_04.prefab:0a0b4cc180b513941932bf24fa43f7f3");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_05.prefab:768e13c9a0e21cb498e21bb9e7c9ed78");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_07.prefab:5d98feb185dab5248bfbe2a008315f85");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_08.prefab:eeeb522e0b5a0424d9bc61cc6c9b8bcf");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_11.prefab:4f5f974b86eda8a438caf16ca10e8873");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_15.prefab:f465336dfab2e924fa20576596ed3775");
		base.PreloadSound("VO_Ahune_Male_Elemental_Brawl_16.prefab:db0ea5a0571b23d41843004bfb04c2a5");
	}

	// Token: 0x060050AA RID: 20650 RVA: 0x001A8037 File Offset: 0x001A6237
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Vector3 popUpPos = new Vector3(0f, 0f, 0f);
		if (missionEvent <= 9)
		{
			if (missionEvent != 1)
			{
				if (missionEvent == 9)
				{
					this.hasPlayedDropLine = false;
				}
			}
			else
			{
				yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_08.prefab:eeeb522e0b5a0424d9bc61cc6c9b8bcf", 3f, 1f, false, false);
			}
		}
		else if (missionEvent != 10)
		{
			if (missionEvent != 20)
			{
				if (missionEvent == 21)
				{
					if (!this.hasPlayedArmorLine)
					{
						this.hasPlayedArmorLine = true;
						Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get("POPUP_FROST_BRAWL_ARMOR"), false, NotificationManager.PopupTextType.BASIC);
						NotificationManager.Get().DestroyNotification(notification, 7f);
						GameState.Get().SetBusy(true);
						yield return new WaitForSeconds(2f);
						GameState.Get().SetBusy(false);
					}
				}
			}
			else
			{
				yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_07.prefab:5d98feb185dab5248bfbe2a008315f85", 3f, 1f, false, false);
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get("POPUP_FROST_BRAWL"), false, NotificationManager.PopupTextType.BASIC);
				NotificationManager.Get().DestroyNotification(notification, 7f);
			}
		}
		else if (!this.hasPlayedDropLine)
		{
			this.hasPlayedDropLine = true;
			yield return this.HandleRagDummyDeath();
		}
		yield break;
	}

	// Token: 0x060050AB RID: 20651 RVA: 0x001A804D File Offset: 0x001A624D
	private IEnumerator HandleRagDummyDeath()
	{
		switch (UnityEngine.Random.Range(0, 11))
		{
		case 0:
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_01.prefab:e90eb948480f3794da67e53ab145c36e", 3f, 1f, false, false);
			break;
		case 1:
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_02.prefab:d22af95d6e42bd14583ae80f65688618", 3f, 1f, false, false);
			break;
		case 2:
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_03.prefab:39a4f6504daf0744ebd50da61db59cf7", 3f, 1f, false, false);
			break;
		case 3:
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_04.prefab:0a0b4cc180b513941932bf24fa43f7f3", 3f, 1f, false, false);
			break;
		case 4:
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_05.prefab:768e13c9a0e21cb498e21bb9e7c9ed78", 3f, 1f, false, false);
			break;
		case 5:
		case 6:
		case 7:
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_11.prefab:4f5f974b86eda8a438caf16ca10e8873", 3f, 1f, false, false);
			break;
		}
		yield break;
	}

	// Token: 0x060050AC RID: 20652 RVA: 0x001A805C File Offset: 0x001A625C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield return new WaitForSeconds(2f);
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_15.prefab:f465336dfab2e924fa20576596ed3775", 3f, 1f, false, false);
		}
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return base.PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_16.prefab:db0ea5a0571b23d41843004bfb04c2a5", 3f, 1f, false, false);
		}
		yield break;
	}

	// Token: 0x040046FD RID: 18173
	private bool hasPlayedDropLine;

	// Token: 0x040046FE RID: 18174
	private bool hasPlayedArmorLine;
}
