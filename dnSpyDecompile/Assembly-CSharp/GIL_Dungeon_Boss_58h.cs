using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000402 RID: 1026
public class GIL_Dungeon_Boss_58h : GIL_Dungeon
{
	// Token: 0x060038C8 RID: 14536 RVA: 0x0011E064 File Offset: 0x0011C264
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_58h_Male_Giant_Intro_01.prefab:71e5135d4f6d84245abc1ce40bba77fb",
			"VO_GILA_BOSS_58h_Male_Giant_EmoteResponse_01.prefab:7f1980710cf3f024caacacd2331e0679",
			"VO_GILA_BOSS_58h_Male_Giant_Death_01.prefab:6524b213834bf93469b5d634cb8cad4b",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_01.prefab:2fee4d07c72264943b0a4cdd196c31bd",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_02.prefab:ae8591110faf4b040b1bd80e505679b3",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_03.prefab:6b64a8cd7461b184eab9e3aaf3ef6f99",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_04.prefab:776a6a1998c124042a2323ef8e9bc3c2",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_05.prefab:88f499a3b1b959242942beedd7fb1fb3",
			"VO_GILA_BOSS_58h_Male_Giant_EventMinionReturn_01.prefab:9282d72c43fbcfb4a9cd17819832d169",
			"VO_GILA_BOSS_58h_Male_Giant_EventMinionReturn_02.prefab:0cdb21c0f611fbf4f8661813101a36f1",
			"VO_GILA_BOSS_58h_Male_Giant_EventMinionReturn_03.prefab:85f475740861cb141b213e744d910d00",
			"VO_GILA_BOSS_58h_Male_Giant_EventBigWeapon_01.prefab:d57a95e52bab4ae46874b9d08736ba7d"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038C9 RID: 14537 RVA: 0x0011E140 File Offset: 0x0011C340
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060038CA RID: 14538 RVA: 0x0011E156 File Offset: 0x0011C356
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_01.prefab:2fee4d07c72264943b0a4cdd196c31bd",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_02.prefab:ae8591110faf4b040b1bd80e505679b3",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_03.prefab:6b64a8cd7461b184eab9e3aaf3ef6f99",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_04.prefab:776a6a1998c124042a2323ef8e9bc3c2",
			"VO_GILA_BOSS_58h_Male_Giant_HeroPower_05.prefab:88f499a3b1b959242942beedd7fb1fb3"
		};
	}

	// Token: 0x060038CB RID: 14539 RVA: 0x0011E194 File Offset: 0x0011C394
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_58h_Male_Giant_Death_01.prefab:6524b213834bf93469b5d634cb8cad4b";
	}

	// Token: 0x060038CC RID: 14540 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060038CD RID: 14541 RVA: 0x0011E19C File Offset: 0x0011C39C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_58h_Male_Giant_Intro_01.prefab:71e5135d4f6d84245abc1ce40bba77fb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_58h_Male_Giant_EmoteResponse_01.prefab:7f1980710cf3f024caacacd2331e0679", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060038CE RID: 14542 RVA: 0x0011E223 File Offset: 0x0011C423
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_58h_Male_Giant_EventBigWeapon_01.prefab:d57a95e52bab4ae46874b9d08736ba7d", 2.5f);
			}
		}
		else
		{
			string text = base.PopRandomLineWithChance(this.m_MinionReturn);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001DD8 RID: 7640
	private List<string> m_MinionReturn = new List<string>
	{
		"VO_GILA_BOSS_58h_Male_Giant_EventMinionReturn_01.prefab:9282d72c43fbcfb4a9cd17819832d169",
		"VO_GILA_BOSS_58h_Male_Giant_EventMinionReturn_02.prefab:0cdb21c0f611fbf4f8661813101a36f1",
		"VO_GILA_BOSS_58h_Male_Giant_EventMinionReturn_03.prefab:85f475740861cb141b213e744d910d00"
	};
}
