using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200042E RID: 1070
public class DALA_Dungeon_Boss_01h : DALA_Dungeon
{
	// Token: 0x06003A56 RID: 14934 RVA: 0x0012D220 File Offset: 0x0012B420
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_Death,
			DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_DefeatPlayer,
			DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_EmoteResponse,
			DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x0012D2C4 File Offset: 0x0012B4C4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_Intro;
		this.m_deathLine = DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_Death;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_01h.VO_DALA_BOSS_01h_Chomper_EmoteResponse;
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x0012D2FC File Offset: 0x0012B4FC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003A5A RID: 14938 RVA: 0x0012D312 File Offset: 0x0012B512
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

	// Token: 0x06003A5B RID: 14939 RVA: 0x0012D328 File Offset: 0x0012B528
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

	// Token: 0x04002201 RID: 8705
	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_Death = new AssetReference("VO_DALA_BOSS_01h_Chomper_Death.prefab:a67617e34bd46ad4b86ce38b27538336");

	// Token: 0x04002202 RID: 8706
	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_DefeatPlayer = new AssetReference("VO_DALA_BOSS_01h_Chomper_DefeatPlayer.prefab:7b9e096137b452c4bb0122120a526089");

	// Token: 0x04002203 RID: 8707
	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_EmoteResponse = new AssetReference("VO_DALA_BOSS_01h_Chomper_EmoteResponse.prefab:a3805142083d27642ab9ace616499a88");

	// Token: 0x04002204 RID: 8708
	private static readonly AssetReference VO_DALA_BOSS_01h_Chomper_Intro = new AssetReference("VO_DALA_BOSS_01h_Chomper_Intro.prefab:a4808c11753e77b43947a481f0fa7f43");

	// Token: 0x04002205 RID: 8709
	private HashSet<string> m_playedLines = new HashSet<string>();
}
