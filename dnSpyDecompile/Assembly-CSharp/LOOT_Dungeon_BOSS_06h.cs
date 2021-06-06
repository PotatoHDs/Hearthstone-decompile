using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003B2 RID: 946
public class LOOT_Dungeon_BOSS_06h : LOOT_Dungeon
{
	// Token: 0x060035F3 RID: 13811 RVA: 0x00113280 File Offset: 0x00111480
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_06h_Male_Furbolg_Intro_01.prefab:80aabdb2e58804e4b820fe25fd706f58",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_EmoteResponse_01.prefab:8c00812afbfab854b9cc0fa40ad7a250",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower1_01.prefab:e9120cfb5a02e7443af0c631596271ce",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower2_01.prefab:cf086888d51f3f24498f700bfd11b059",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower3_01.prefab:f778efbe26021884db88c723f6c1f1db",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower4_01.prefab:9a25d1db59d12784b834f4b9910571e9",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower5_01.prefab:8b5680e4dde84494e83ff10c79e407ca",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_Death_01.prefab:982a68851c0d43642a94fab4b3b448d2",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_DefeatPlayer_01.prefab:981056a42cf99dc478f30111338bb2b0",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_EventPlayerEvolves_01.prefab:990e6ac0bad905549bead65facc02ab8"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035F4 RID: 13812 RVA: 0x00113348 File Offset: 0x00111548
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060035F5 RID: 13813 RVA: 0x0011335E File Offset: 0x0011155E
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower1_01.prefab:e9120cfb5a02e7443af0c631596271ce",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower2_01.prefab:cf086888d51f3f24498f700bfd11b059",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower3_01.prefab:f778efbe26021884db88c723f6c1f1db",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower4_01.prefab:9a25d1db59d12784b834f4b9910571e9",
			"VO_LOOTA_BOSS_06h_Male_Furbolg_HeroPower5_01.prefab:8b5680e4dde84494e83ff10c79e407ca"
		};
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x0011339C File Offset: 0x0011159C
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_06h_Male_Furbolg_Death_01.prefab:982a68851c0d43642a94fab4b3b448d2";
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x001133A4 File Offset: 0x001115A4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_06h_Male_Furbolg_Intro_01.prefab:80aabdb2e58804e4b820fe25fd706f58", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_06h_Male_Furbolg_EmoteResponse_01.prefab:8c00812afbfab854b9cc0fa40ad7a250", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x0011342B File Offset: 0x0011162B
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
		if (!(cardId == "OG_027"))
		{
			if (cardId == "ICC_481")
			{
				yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_06h_Male_Furbolg_EventPlayerEvolves_01.prefab:990e6ac0bad905549bead65facc02ab8", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_06h_Male_Furbolg_EventPlayerEvolves_01.prefab:990e6ac0bad905549bead65facc02ab8", 2.5f);
		}
		yield break;
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x00113441 File Offset: 0x00111641
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D20 RID: 7456
	private HashSet<string> m_playedLines = new HashSet<string>();
}
