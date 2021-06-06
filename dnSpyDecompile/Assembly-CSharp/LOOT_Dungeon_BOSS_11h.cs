using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003B5 RID: 949
public class LOOT_Dungeon_BOSS_11h : LOOT_Dungeon
{
	// Token: 0x0600360E RID: 13838 RVA: 0x00113844 File Offset: 0x00111A44
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_11h_Male_Trogg_Intro_01.prefab:54f6017f84e6d824e93a04abd27f6f01",
			"VO_LOOTA_BOSS_11h_Male_Trogg_EmoteResponse_01.prefab:ed6cf5a2a048fea4e98c50eff13cd707",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower1_01.prefab:dcb9b31148865ad428982e408330d33c",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower2_01.prefab:c07216b69bb2611419c50914f76f44c6",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower3_01.prefab:e679dfe45da0bb74891a11037790eb93",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower4_01.prefab:5856b86e997271a4fa2b1118dd2ef534",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower5_01.prefab:b300a890ae8c7d3458cf37bf1723357e",
			"VO_LOOTA_BOSS_11h_Male_Trogg_Death_01.prefab:5563abda7ad8407499b671cd4b63b3f8",
			"VO_LOOTA_BOSS_11h_Male_Trogg_DefeatPlayer_01.prefab:b09d58b2cb5875a4eba888ff6bcb6da5",
			"VO_LOOTA_BOSS_11h_Male_Trogg_EventEquality_01.prefab:6b570bed497bd2e458e70d929695c99c"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x0011390C File Offset: 0x00111B0C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003610 RID: 13840 RVA: 0x00113922 File Offset: 0x00111B22
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower1_01.prefab:dcb9b31148865ad428982e408330d33c",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower2_01.prefab:c07216b69bb2611419c50914f76f44c6",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower3_01.prefab:e679dfe45da0bb74891a11037790eb93",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower4_01.prefab:5856b86e997271a4fa2b1118dd2ef534",
			"VO_LOOTA_BOSS_11h_Male_Trogg_HeroPower5_01.prefab:b300a890ae8c7d3458cf37bf1723357e"
		};
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x00113960 File Offset: 0x00111B60
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_11h_Male_Trogg_Death_01.prefab:5563abda7ad8407499b671cd4b63b3f8";
	}

	// Token: 0x06003612 RID: 13842 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003613 RID: 13843 RVA: 0x00113968 File Offset: 0x00111B68
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_11h_Male_Trogg_Intro_01.prefab:54f6017f84e6d824e93a04abd27f6f01", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_11h_Male_Trogg_EmoteResponse_01.prefab:ed6cf5a2a048fea4e98c50eff13cd707", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003614 RID: 13844 RVA: 0x001139EF File Offset: 0x00111BEF
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
		if (cardId == "EX1_619")
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_11h_Male_Trogg_EventEquality_01.prefab:6b570bed497bd2e458e70d929695c99c", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003615 RID: 13845 RVA: 0x00113A05 File Offset: 0x00111C05
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D24 RID: 7460
	private HashSet<string> m_playedLines = new HashSet<string>();
}
