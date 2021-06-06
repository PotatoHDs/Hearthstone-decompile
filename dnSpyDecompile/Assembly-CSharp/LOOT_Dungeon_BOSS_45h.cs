using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003D0 RID: 976
public class LOOT_Dungeon_BOSS_45h : LOOT_Dungeon
{
	// Token: 0x060036FD RID: 14077 RVA: 0x001168C0 File Offset: 0x00114AC0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_45h_Male_Gnome_Intro_01.prefab:bfdb9f8e2c3f0494083ce5aa230c7f56",
			"VO_LOOTA_BOSS_45h_Male_Gnome_EmoteResponse_01.prefab:77e7d3d50d4efce4b87b1cad34b67048",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower1_01.prefab:ddfb713f6b7cd284daf2a96f21f731d8",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower2_01.prefab:005a46770cc21444cbc8c05853a88481",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower3_01.prefab:35be5eca8eae03342a5018b40c7741ba",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower4_01.prefab:43f0b3d950d0bce4281f30f83c11cae2",
			"VO_LOOTA_BOSS_45h_Male_Gnome_Death_01.prefab:84f3bb48aefd92846aadc833a1928aeb",
			"VO_LOOTA_BOSS_45h_Male_Gnome_DefeatPlayer_01.prefab:bad2261cb6735c145a71f6fe50cf427a"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036FE RID: 14078 RVA: 0x00116970 File Offset: 0x00114B70
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036FF RID: 14079 RVA: 0x00116986 File Offset: 0x00114B86
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower1_01.prefab:ddfb713f6b7cd284daf2a96f21f731d8",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower2_01.prefab:005a46770cc21444cbc8c05853a88481",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower3_01.prefab:35be5eca8eae03342a5018b40c7741ba",
			"VO_LOOTA_BOSS_45h_Male_Gnome_HeroPower4_01.prefab:43f0b3d950d0bce4281f30f83c11cae2"
		};
	}

	// Token: 0x06003700 RID: 14080 RVA: 0x001169B9 File Offset: 0x00114BB9
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_45h_Male_Gnome_Death_01.prefab:84f3bb48aefd92846aadc833a1928aeb";
	}

	// Token: 0x06003701 RID: 14081 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003702 RID: 14082 RVA: 0x001169C0 File Offset: 0x00114BC0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_45h_Male_Gnome_Intro_01.prefab:bfdb9f8e2c3f0494083ce5aa230c7f56", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_45h_Male_Gnome_EmoteResponse_01.prefab:77e7d3d50d4efce4b87b1cad34b67048", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x00116A47 File Offset: 0x00114C47
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
