using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003CB RID: 971
public class LOOT_Dungeon_BOSS_40h : LOOT_Dungeon
{
	// Token: 0x060036CF RID: 14031 RVA: 0x0011605C File Offset: 0x0011425C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_LOOTA_BOSS_40h_Female_Dragon_Intro_01.prefab:ab52d86f1d6df354cabcec7aa0c1d42a",
			"VO_LOOTA_BOSS_40h_Female_Dragon_EmoteResponse_01.prefab:652c16537ece0504592c667443bdd5af",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower1_01.prefab:e00015e116756b84f9096d8c31f8f82c",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower2_01.prefab:7174ac501266c4b468a4ab50bdeace74",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower3_01.prefab:189f71527781ccb4b989b4f7c379c90b",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower4_01.prefab:83833ba71f55f4a43b35406f6109c06b",
			"VO_LOOTA_BOSS_40h_Female_Dragon_Death_01.prefab:589c4cf8f4789df43a44f335a75a04ca",
			"VO_LOOTA_BOSS_40h_Female_Dragon_DefeatPlayer_01.prefab:d8327e1cd34b37d418c9650e73236ba4",
			"VO_LOOTA_BOSS_40h_Female_Dragon_EventPlayTwilightDrake_01.prefab:c246a514be5600b43a55849f5b024932",
			"VO_LOOTA_BOSS_40h_Female_Dragon_EventPlayTwilightWhelp_01.prefab:452f06d0867a0ef4dbcb4811d9c5f852"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x00116124 File Offset: 0x00114324
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060036D1 RID: 14033 RVA: 0x0011613A File Offset: 0x0011433A
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower1_01.prefab:e00015e116756b84f9096d8c31f8f82c",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower2_01.prefab:7174ac501266c4b468a4ab50bdeace74",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower3_01.prefab:189f71527781ccb4b989b4f7c379c90b",
			"VO_LOOTA_BOSS_40h_Female_Dragon_HeroPower4_01.prefab:83833ba71f55f4a43b35406f6109c06b"
		};
	}

	// Token: 0x060036D2 RID: 14034 RVA: 0x0011616D File Offset: 0x0011436D
	protected override string GetBossDeathLine()
	{
		return "VO_LOOTA_BOSS_40h_Female_Dragon_Death_01.prefab:589c4cf8f4789df43a44f335a75a04ca";
	}

	// Token: 0x060036D3 RID: 14035 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x00116174 File Offset: 0x00114374
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_40h_Female_Dragon_Intro_01.prefab:ab52d86f1d6df354cabcec7aa0c1d42a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOOTA_BOSS_40h_Female_Dragon_EmoteResponse_01.prefab:652c16537ece0504592c667443bdd5af", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x001161FB File Offset: 0x001143FB
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
		if (!(cardId == "EX1_043"))
		{
			if (cardId == "BRM_004")
			{
				yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_40h_Female_Dragon_EventPlayTwilightWhelp_01.prefab:452f06d0867a0ef4dbcb4811d9c5f852", 2.5f);
				yield return null;
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_LOOTA_BOSS_40h_Female_Dragon_EventPlayTwilightDrake_01.prefab:c246a514be5600b43a55849f5b024932", 2.5f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x00116211 File Offset: 0x00114411
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D49 RID: 7497
	private HashSet<string> m_playedLines = new HashSet<string>();
}
