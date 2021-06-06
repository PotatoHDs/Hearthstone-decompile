using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003CF RID: 975
public class LOOT_Dungeon_BOSS_44h : LOOT_Dungeon
{
	// Token: 0x060036F4 RID: 14068 RVA: 0x0011677C File Offset: 0x0011497C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_44h_Female_Dragon_Intro_01.prefab:336704b53c1d9ea48a9f0d6e1e6356d5",
			"VO_LOOTA_BOSS_44h_Female_Dragon_EmoteResponse_01.prefab:a710ab86ebc0c1040af7104fae088d07",
			"VO_LOOTA_BOSS_44h_Female_Dragon_Death_01.prefab:a94e33d6a82d725469a2255d2320090f",
			"VO_LOOTA_BOSS_44h_Female_Dragon_DefeatPlayer_01.prefab:66671023c97390245b92c577396d0975"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036F5 RID: 14069 RVA: 0x00116800 File Offset: 0x00114A00
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036F6 RID: 14070 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060036F7 RID: 14071 RVA: 0x00116816 File Offset: 0x00114A16
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_44h_Female_Dragon_Death_01.prefab:a94e33d6a82d725469a2255d2320090f";
	}

	// Token: 0x060036F8 RID: 14072 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060036F9 RID: 14073 RVA: 0x00116820 File Offset: 0x00114A20
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_44h_Female_Dragon_Intro_01.prefab:336704b53c1d9ea48a9f0d6e1e6356d5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_44h_Female_Dragon_EmoteResponse_01.prefab:a710ab86ebc0c1040af7104fae088d07", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036FA RID: 14074 RVA: 0x001168A7 File Offset: 0x00114AA7
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
