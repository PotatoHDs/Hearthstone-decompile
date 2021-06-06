using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004FF RID: 1279
public class BTA_Fight_21 : BTA_Dungeon_Heroic
{
	// Token: 0x060044D8 RID: 17624 RVA: 0x00174E84 File Offset: 0x00173084
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_21.BTA_BOSS_21h_Supremus_Play,
			BTA_Fight_21.BTA_BOSS_21h_Supremus_EmoteResponse,
			BTA_Fight_21.BTA_BOSS_21h_Supremus_Death
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x00174F18 File Offset: 0x00173118
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_21.BTA_BOSS_21h_Supremus_Death;
		this.m_standardEmoteResponseLine = BTA_Fight_21.BTA_BOSS_21h_Supremus_EmoteResponse;
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x00174F40 File Offset: 0x00173140
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_21.BTA_BOSS_21h_Supremus_Play, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x00174FF7 File Offset: 0x001731F7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x060044DD RID: 17629 RVA: 0x0017500D File Offset: 0x0017320D
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060044DE RID: 17630 RVA: 0x00175023 File Offset: 0x00173223
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060044DF RID: 17631 RVA: 0x00175039 File Offset: 0x00173239
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x040037B4 RID: 14260
	private static readonly AssetReference BTA_BOSS_21h_Supremus_Play = new AssetReference("BTA_BOSS_21h_Supremus_Play.prefab:e267c28d7ceda6442adfc092a8f825a1");

	// Token: 0x040037B5 RID: 14261
	private static readonly AssetReference BTA_BOSS_21h_Supremus_EmoteResponse = new AssetReference("BTA_BOSS_21h_Supremus_EmoteResponse.prefab:7878bf624db617843952fd7b939287ec");

	// Token: 0x040037B6 RID: 14262
	private static readonly AssetReference BTA_BOSS_21h_Supremus_Death = new AssetReference("BTA_BOSS_21h_Supremus_Death.prefab:a432f18252006904eaffffee747f5647");

	// Token: 0x040037B7 RID: 14263
	private HashSet<string> m_playedLines = new HashSet<string>();
}
