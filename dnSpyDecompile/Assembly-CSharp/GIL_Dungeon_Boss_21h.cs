using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003DF RID: 991
public class GIL_Dungeon_Boss_21h : GIL_Dungeon
{
	// Token: 0x06003787 RID: 14215 RVA: 0x0011951C File Offset: 0x0011771C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_21h_Rottooth_Intro.prefab:a7bfe727e3a77ef4faa4e1151a670e87",
			"VO_GILA_BOSS_21h_Rottooth_Emote.prefab:01a3a0ce7aa408347910735b9416dfde",
			"VO_GILA_BOSS_21h_Rottooth_Death.prefab:4e73b15125c45344088be9decbe0c07f"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003788 RID: 14216 RVA: 0x00119598 File Offset: 0x00117798
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003789 RID: 14217 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600378A RID: 14218 RVA: 0x001195AE File Offset: 0x001177AE
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_21h_Rottooth_Death.prefab:4e73b15125c45344088be9decbe0c07f";
	}

	// Token: 0x0600378B RID: 14219 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600378C RID: 14220 RVA: 0x001195B8 File Offset: 0x001177B8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_21h_Rottooth_Intro.prefab:a7bfe727e3a77ef4faa4e1151a670e87", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_21h_Rottooth_Emote.prefab:01a3a0ce7aa408347910735b9416dfde", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}
}
