using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003F2 RID: 1010
public class GIL_Dungeon_Boss_41h : GIL_Dungeon
{
	// Token: 0x06003831 RID: 14385 RVA: 0x0011BCB0 File Offset: 0x00119EB0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_41h_Male_NightElf_Intro_01.prefab:2ee1227d69533f24197e3d948f21f858",
			"VO_GILA_BOSS_41h_Male_NightElf_EmoteResponse_02.prefab:b6265a8078217254181079a5796fcc1f",
			"VO_GILA_BOSS_41h_Male_NightElf_Death_01.prefab:4a86b581b0c06f54f8566bd5def843a8",
			"VO_GILA_BOSS_41h_Male_NightElf_DefeatPlayer_01.prefab:d3365287238e41943b4c19866d607360",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_01.prefab:ccd2d7a8e82b40847a0823442c3321c9",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_02.prefab:014065c5aef92e24bbc3be7ffeb691c4",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_03.prefab:f629177909f9356428aad20b0156172b",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_04.prefab:16828aa7f71bed1479c4e0532c4eb1ba",
			"VO_GILA_BOSS_41h_Male_NightElf_EventHack_01.prefab:2345227d6fc812f4eb951c5fdef805cf",
			"VO_GILA_BOSS_41h_Male_NightElf_EventHack_02.prefab:60157a9729d33b445956b74435784dae",
			"VO_GILA_BOSS_41h_Male_NightElf_EventHack_03.prefab:3ab3c3a3da9f0bf4fac82f53c868851b"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003832 RID: 14386 RVA: 0x0011BD84 File Offset: 0x00119F84
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GILA_BOSS_41t")
		{
			string text = base.PopRandomLineWithChance(this.m_HackLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x0011BD9A File Offset: 0x00119F9A
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_01.prefab:ccd2d7a8e82b40847a0823442c3321c9",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_02.prefab:014065c5aef92e24bbc3be7ffeb691c4",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_03.prefab:f629177909f9356428aad20b0156172b",
			"VO_GILA_BOSS_41h_Male_NightElf_HeroPower_04.prefab:16828aa7f71bed1479c4e0532c4eb1ba"
		};
	}

	// Token: 0x06003834 RID: 14388 RVA: 0x0011BDCD File Offset: 0x00119FCD
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_41h_Male_NightElf_Death_01.prefab:4a86b581b0c06f54f8566bd5def843a8";
	}

	// Token: 0x06003835 RID: 14389 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003836 RID: 14390 RVA: 0x0011BDD4 File Offset: 0x00119FD4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Male_NightElf_Intro_01.prefab:2ee1227d69533f24197e3d948f21f858", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_41h_Male_NightElf_EmoteResponse_02.prefab:b6265a8078217254181079a5796fcc1f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x04001DAC RID: 7596
	private List<string> m_HackLines = new List<string>
	{
		"VO_GILA_BOSS_41h_Male_NightElf_EventHack_01.prefab:2345227d6fc812f4eb951c5fdef805cf",
		"VO_GILA_BOSS_41h_Male_NightElf_EventHack_02.prefab:60157a9729d33b445956b74435784dae",
		"VO_GILA_BOSS_41h_Male_NightElf_EventHack_03.prefab:3ab3c3a3da9f0bf4fac82f53c868851b"
	};
}
