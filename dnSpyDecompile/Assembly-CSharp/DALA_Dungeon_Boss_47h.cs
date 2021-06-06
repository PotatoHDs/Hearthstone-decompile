using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200045C RID: 1116
public class DALA_Dungeon_Boss_47h : DALA_Dungeon
{
	// Token: 0x06003C98 RID: 15512 RVA: 0x0013C1C0 File Offset: 0x0013A3C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_Death,
			DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_DefeatPlayer,
			DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_EmoteResponse,
			DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C99 RID: 15513 RVA: 0x0013C264 File Offset: 0x0013A464
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_Intro;
		this.m_deathLine = DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_Death;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_47h.VO_DALA_BOSS_47h_Lavanthor_EmoteResponse;
	}

	// Token: 0x06003C9A RID: 15514 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C9B RID: 15515 RVA: 0x0013C29C File Offset: 0x0013A49C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003C9C RID: 15516 RVA: 0x0013C2B2 File Offset: 0x0013A4B2
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

	// Token: 0x06003C9D RID: 15517 RVA: 0x0013C2C8 File Offset: 0x0013A4C8
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

	// Token: 0x0400264F RID: 9807
	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_Death = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_Death.prefab:7e208dd4154b0654a870063a4d336090");

	// Token: 0x04002650 RID: 9808
	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_DefeatPlayer = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_DefeatPlayer.prefab:c200bd9d856ebfd4e8c6469989200ec1");

	// Token: 0x04002651 RID: 9809
	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_EmoteResponse = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_EmoteResponse.prefab:15154e48f0b0d1e4dbc63ccfe61b0284");

	// Token: 0x04002652 RID: 9810
	private static readonly AssetReference VO_DALA_BOSS_47h_Lavanthor_Intro = new AssetReference("VO_DALA_BOSS_47h_Lavanthor_Intro.prefab:b2e242f69f7a7e44c8fc14ed07d35736");

	// Token: 0x04002653 RID: 9811
	private HashSet<string> m_playedLines = new HashSet<string>();
}
