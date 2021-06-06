using System.Collections;
using UnityEngine;

public class TB_FrostFestival : MissionEntity
{
	private bool hasPlayedDropLine;

	private bool hasPlayedArmorLine;

	public override void PreloadAssets()
	{
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_01.prefab:e90eb948480f3794da67e53ab145c36e");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_02.prefab:d22af95d6e42bd14583ae80f65688618");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_03.prefab:39a4f6504daf0744ebd50da61db59cf7");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_04.prefab:0a0b4cc180b513941932bf24fa43f7f3");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_05.prefab:768e13c9a0e21cb498e21bb9e7c9ed78");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_07.prefab:5d98feb185dab5248bfbe2a008315f85");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_08.prefab:eeeb522e0b5a0424d9bc61cc6c9b8bcf");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_11.prefab:4f5f974b86eda8a438caf16ca10e8873");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_15.prefab:f465336dfab2e924fa20576596ed3775");
		PreloadSound("VO_Ahune_Male_Elemental_Brawl_16.prefab:db0ea5a0571b23d41843004bfb04c2a5");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Vector3 popUpPos = new Vector3(0f, 0f, 0f);
		switch (missionEvent)
		{
		case 1:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_08.prefab:eeeb522e0b5a0424d9bc61cc6c9b8bcf");
			break;
		case 9:
			hasPlayedDropLine = false;
			break;
		case 10:
			if (!hasPlayedDropLine)
			{
				hasPlayedDropLine = true;
				yield return HandleRagDummyDeath();
			}
			break;
		case 20:
		{
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_07.prefab:5d98feb185dab5248bfbe2a008315f85");
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get("POPUP_FROST_BRAWL"), convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(notification, 7f);
			break;
		}
		case 21:
			if (!hasPlayedArmorLine)
			{
				hasPlayedArmorLine = true;
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 2f, GameStrings.Get("POPUP_FROST_BRAWL_ARMOR"), convertLegacyPosition: false);
				NotificationManager.Get().DestroyNotification(notification, 7f);
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(2f);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
	}

	private IEnumerator HandleRagDummyDeath()
	{
		switch (Random.Range(0, 11))
		{
		case 0:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_01.prefab:e90eb948480f3794da67e53ab145c36e");
			break;
		case 1:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_02.prefab:d22af95d6e42bd14583ae80f65688618");
			break;
		case 2:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_03.prefab:39a4f6504daf0744ebd50da61db59cf7");
			break;
		case 3:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_04.prefab:0a0b4cc180b513941932bf24fa43f7f3");
			break;
		case 4:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_05.prefab:768e13c9a0e21cb498e21bb9e7c9ed78");
			break;
		case 5:
		case 6:
		case 7:
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_11.prefab:4f5f974b86eda8a438caf16ca10e8873");
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield return new WaitForSeconds(2f);
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_15.prefab:f465336dfab2e924fa20576596ed3775");
		}
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return PlayBigCharacterQuoteAndWaitOnce("Ahune_BigQuote.prefab:00dd8f83adda33345ac291cc76241482", "VO_Ahune_Male_Elemental_Brawl_16.prefab:db0ea5a0571b23d41843004bfb04c2a5");
		}
	}
}
