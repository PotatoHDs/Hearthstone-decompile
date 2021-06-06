using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200044B RID: 1099
public class DALA_Dungeon_Boss_30h : DALA_Dungeon
{
	// Token: 0x06003BBF RID: 15295 RVA: 0x00136D34 File Offset: 0x00134F34
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_Death,
			DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_DefeatPlayer,
			DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_EmoteResponse,
			DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x00136DD8 File Offset: 0x00134FD8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_Intro;
		this.m_deathLine = DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_Death;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_30h.VO_DALA_BOSS_30h_Male_Rat_EmoteResponse;
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x00136E10 File Offset: 0x00135010
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Vessina" && cardId != "DALA_Barkeye" && cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x00136ED5 File Offset: 0x001350D5
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x00136EEB File Offset: 0x001350EB
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
		yield break;
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x00136F01 File Offset: 0x00135101
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x040024C6 RID: 9414
	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_Death = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_Death.prefab:fb52c37daa34db346a08f66d8c66b8ce");

	// Token: 0x040024C7 RID: 9415
	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_DefeatPlayer = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_DefeatPlayer.prefab:04d6baeac2746274185b97dfbccc033c");

	// Token: 0x040024C8 RID: 9416
	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_EmoteResponse = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_EmoteResponse.prefab:5b16504a10dbb4f45af73cb1b4da7553");

	// Token: 0x040024C9 RID: 9417
	private static readonly AssetReference VO_DALA_BOSS_30h_Male_Rat_Intro = new AssetReference("VO_DALA_BOSS_30h_Male_Rat_Intro.prefab:fb83d73d78d4ce64eaae1db5f7715261");

	// Token: 0x040024CA RID: 9418
	private HashSet<string> m_playedLines = new HashSet<string>();
}
