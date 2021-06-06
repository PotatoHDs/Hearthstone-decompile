using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003CC RID: 972
public class LOOT_Dungeon_BOSS_41h : LOOT_Dungeon
{
	// Token: 0x060036D9 RID: 14041 RVA: 0x0011623C File Offset: 0x0011443C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_41h_Male_Kobold_Intro_01.prefab:ce71aae59b0a07a4199833b254d82c66",
			"VO_LOOTA_BOSS_41h_Male_Kobold_EmoteResponse_01.prefab:09755fb2908eb1d4c8e93ae50ac2ee40",
			"VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower1_01.prefab:6647456515d002a41a5a75a7f0192a3d",
			"VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower2_01.prefab:f079bea51d0fa8d4e8c90da653fc948e",
			"VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower3_01.prefab:dc15ef4312d010b488aeb221f16e8c54",
			"VO_LOOTA_BOSS_41h_Male_Kobold_Death_01.prefab:a4488b2ad903b134d9493263fc696fc5",
			"VO_LOOTA_BOSS_41h_Male_Kobold_DefeatPlayer_01.prefab:30615acf7942fda49abf98714fac530e",
			"VO_LOOTA_BOSS_41h_Male_Kobold_EventHornOfCenarius_01.prefab:332370e137aa1144faf79fda6290edbd"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036DA RID: 14042 RVA: 0x001162EC File Offset: 0x001144EC
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036DB RID: 14043 RVA: 0x00116302 File Offset: 0x00114502
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower1_01.prefab:6647456515d002a41a5a75a7f0192a3d",
			"VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower2_01.prefab:f079bea51d0fa8d4e8c90da653fc948e",
			"VO_LOOTA_BOSS_41h_Male_Kobold_HeroPower3_01.prefab:dc15ef4312d010b488aeb221f16e8c54"
		};
	}

	// Token: 0x060036DC RID: 14044 RVA: 0x0011632A File Offset: 0x0011452A
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_41h_Male_Kobold_Death_01.prefab:a4488b2ad903b134d9493263fc696fc5";
	}

	// Token: 0x060036DD RID: 14045 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060036DE RID: 14046 RVA: 0x00116334 File Offset: 0x00114534
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_41h_Male_Kobold_Intro_01.prefab:ce71aae59b0a07a4199833b254d82c66", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_41h_Male_Kobold_EmoteResponse_01.prefab:09755fb2908eb1d4c8e93ae50ac2ee40", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036DF RID: 14047 RVA: 0x001163BB File Offset: 0x001145BB
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "LOOTA_837")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_41h_Male_Kobold_EventHornOfCenarius_01.prefab:332370e137aa1144faf79fda6290edbd", 2.5f);
		}
		yield break;
	}

	// Token: 0x060036E0 RID: 14048 RVA: 0x001163D1 File Offset: 0x001145D1
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D4A RID: 7498
	private HashSet<string> m_playedLines = new HashSet<string>();
}
