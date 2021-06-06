using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003CA RID: 970
public class LOOT_Dungeon_BOSS_39h : LOOT_Dungeon
{
	// Token: 0x060036C7 RID: 14023 RVA: 0x00115EA4 File Offset: 0x001140A4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_39h_Male_Golem_Intro_01.prefab:7066bdb186d303d4f9aed5cf13c26591",
			"VO_LOOTA_BOSS_39h_Male_Golem_EmoteResponse_01.prefab:219ec10b3ba61b64397e546a152cabf2",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower1_01.prefab:adeb79587bf3bd942b9bbaa35c491eef",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower2_01.prefab:315f80280dc2d0c4f8017444f74a7cd3",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower3_01.prefab:0a75b9b340c4a8d46970d421e27b90b1",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower4_01.prefab:bded90c518b2f5543a1fb55c02be4367",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower5_01.prefab:bc9562ea729826543990d0550804a39a",
			"VO_LOOTA_BOSS_39h_Male_Golem_Death_01.prefab:431be0113aaeb59419bbd6d7c9f49635",
			"VO_LOOTA_BOSS_39h_Male_Golem_DefeatPlayer_01.prefab:5a3abf72a5240bc4098c9b85a77ed957"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036C8 RID: 14024 RVA: 0x00115F60 File Offset: 0x00114160
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036C9 RID: 14025 RVA: 0x00115F76 File Offset: 0x00114176
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower1_01.prefab:adeb79587bf3bd942b9bbaa35c491eef",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower2_01.prefab:315f80280dc2d0c4f8017444f74a7cd3",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower3_01.prefab:0a75b9b340c4a8d46970d421e27b90b1",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower4_01.prefab:bded90c518b2f5543a1fb55c02be4367",
			"VO_LOOTA_BOSS_39h_Male_Golem_HeroPower5_01.prefab:bc9562ea729826543990d0550804a39a"
		};
	}

	// Token: 0x060036CA RID: 14026 RVA: 0x00115FB4 File Offset: 0x001141B4
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_39h_Male_Golem_Death_01.prefab:431be0113aaeb59419bbd6d7c9f49635";
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x00115FBC File Offset: 0x001141BC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_39h_Male_Golem_Intro_01.prefab:7066bdb186d303d4f9aed5cf13c26591", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_39h_Male_Golem_EmoteResponse_01.prefab:219ec10b3ba61b64397e546a152cabf2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036CC RID: 14028 RVA: 0x00116043 File Offset: 0x00114243
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
