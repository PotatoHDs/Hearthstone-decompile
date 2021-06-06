using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D1 RID: 977
public class LOOT_Dungeon_BOSS_46h : LOOT_Dungeon
{
	// Token: 0x06003706 RID: 14086 RVA: 0x00116A60 File Offset: 0x00114C60
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_46h_Male_Murloc_Intro_01.prefab:71c2c439e7f1f3e42be72c6b02d4a013",
			"VO_LOOTA_BOSS_46h_Male_Murloc_EmoteResponse_01.prefab:00a42aa4cdea4954f968e90a4c2f2758",
			"VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower1_01.prefab:1d626b91bc1b5e7498592d47eb2a66ed",
			"VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower2_01.prefab:dfb57d97d355c9242945f43abc4ce81d",
			"VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower3_01.prefab:b1e1098bc4695444bbfabf587395db28",
			"VO_LOOTA_BOSS_46h_Male_Murloc_Death_01.prefab:3cc732e6f7fd04b459e5dece964b7eba",
			"VO_LOOTA_BOSS_46h_Male_Murloc_DefeatPlayer_01.prefab:e24d2745d8495864a83101211a805f6b"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003707 RID: 14087 RVA: 0x00116B08 File Offset: 0x00114D08
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x00116B1E File Offset: 0x00114D1E
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower1_01.prefab:1d626b91bc1b5e7498592d47eb2a66ed",
			"VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower2_01.prefab:dfb57d97d355c9242945f43abc4ce81d",
			"VO_LOOTA_BOSS_46h_Male_Murloc_HeroPower3_01.prefab:b1e1098bc4695444bbfabf587395db28"
		};
	}

	// Token: 0x06003709 RID: 14089 RVA: 0x00116B46 File Offset: 0x00114D46
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_46h_Male_Murloc_Death_01.prefab:3cc732e6f7fd04b459e5dece964b7eba";
	}

	// Token: 0x0600370A RID: 14090 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x00116B50 File Offset: 0x00114D50
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_46h_Male_Murloc_Intro_01.prefab:71c2c439e7f1f3e42be72c6b02d4a013", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_46h_Male_Murloc_EmoteResponse_01.prefab:00a42aa4cdea4954f968e90a4c2f2758", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600370C RID: 14092 RVA: 0x00116BD7 File Offset: 0x00114DD7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}
}
