using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B6 RID: 950
public class LOOT_Dungeon_BOSS_12h : LOOT_Dungeon
{
	// Token: 0x06003618 RID: 13848 RVA: 0x00113A30 File Offset: 0x00111C30
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_12h_Male_Kobold_Intro1_01.prefab:46cb2187ee29a324580ee8107c7c3c0a",
			"VO_LOOTA_BOSS_12h_Male_Kobold_Intro2_01.prefab:0ead7cf01e448f24aae1b25b7d633d99",
			"VO_LOOTA_BOSS_12h_Male_Kobold_Intro3_01.prefab:f52f03fbf2a9b4542aafb456236240ac",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EmoteResponse_01.prefab:8114296dd0a4eb742837f7147c34e37a",
			"VO_LOOTA_BOSS_12h_Male_Kobold_Death1_01.prefab:ed87ca80573e2ab46afa1b7ecd055682",
			"VO_LOOTA_BOSS_12h_Male_Kobold_Death2_01.prefab:ed9c247de5783ca4ba252280da1ec02b",
			"VO_LOOTA_BOSS_12h_Male_Kobold_Death3_01.prefab:cc139c64563141a4299d25958b9e8af4",
			"VO_LOOTA_BOSS_12h_Male_Kobold_DefeatPlayer_01.prefab:51813afc620f6db4cafa3e8bc65aef20",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventChargeBigMinion_01.prefab:9029d30fc0c80ef419c1b763e46356db",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysPatches_01.prefab:25aab388198063f47b882b311169cece",
			"VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysCrab_01.prefab:1e4208a2ac0cc584292fe3375d0145c7"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003619 RID: 13849 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600361A RID: 13850 RVA: 0x00113B04 File Offset: 0x00111D04
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600361B RID: 13851 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600361C RID: 13852 RVA: 0x00113B1C File Offset: 0x00111D1C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			string soundPath = this.m_IntroLines[UnityEngine.Random.Range(0, this.m_IntroLines.Count)];
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(soundPath, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_12h_Male_Kobold_EmoteResponse_01.prefab:8114296dd0a4eb742837f7147c34e37a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600361D RID: 13853 RVA: 0x00113BBC File Offset: 0x00111DBC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		switch (missionEvent)
		{
		case 101:
		{
			string line = this.m_DeathLines[UnityEngine.Random.Range(0, this.m_DeathLines.Count)];
			yield return base.PlayBossLine(enemyActor, line, 2.5f);
			break;
		}
		case 102:
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventChargeBigMinion_01.prefab:9029d30fc0c80ef419c1b763e46356db", 2.5f);
			break;
		case 103:
			yield return new WaitForSeconds(4.5f);
			yield return base.PlayBossLine(enemyActor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysPatches_01.prefab:25aab388198063f47b882b311169cece", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x0600361E RID: 13854 RVA: 0x00113BD2 File Offset: 0x00111DD2
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
		if (cardId == "UNG_807")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_12h_Male_Kobold_EventPlayerPlaysCrab_01.prefab:1e4208a2ac0cc584292fe3375d0145c7", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D25 RID: 7461
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D26 RID: 7462
	private List<string> m_IntroLines = new List<string>
	{
		"VO_LOOTA_BOSS_12h_Male_Kobold_Intro1_01.prefab:46cb2187ee29a324580ee8107c7c3c0a",
		"VO_LOOTA_BOSS_12h_Male_Kobold_Intro2_01.prefab:0ead7cf01e448f24aae1b25b7d633d99",
		"VO_LOOTA_BOSS_12h_Male_Kobold_Intro3_01.prefab:f52f03fbf2a9b4542aafb456236240ac"
	};

	// Token: 0x04001D27 RID: 7463
	private List<string> m_DeathLines = new List<string>
	{
		"VO_LOOTA_BOSS_12h_Male_Kobold_Death1_01.prefab:ed87ca80573e2ab46afa1b7ecd055682",
		"VO_LOOTA_BOSS_12h_Male_Kobold_Death2_01.prefab:ed9c247de5783ca4ba252280da1ec02b",
		"VO_LOOTA_BOSS_12h_Male_Kobold_Death3_01.prefab:cc139c64563141a4299d25958b9e8af4"
	};
}
