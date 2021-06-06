using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000444 RID: 1092
public class DALA_Dungeon_Boss_23h : DALA_Dungeon
{
	// Token: 0x06003B6C RID: 15212 RVA: 0x00134CB8 File Offset: 0x00132EB8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_Death,
			DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_DefeatPlayer,
			DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse,
			DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B6D RID: 15213 RVA: 0x00134D5C File Offset: 0x00132F5C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_Intro;
		this.m_deathLine = DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_Death;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_23h.VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse;
	}

	// Token: 0x06003B6E RID: 15214 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B6F RID: 15215 RVA: 0x00134D94 File Offset: 0x00132F94
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003B70 RID: 15216 RVA: 0x00134DAA File Offset: 0x00132FAA
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

	// Token: 0x06003B71 RID: 15217 RVA: 0x00134DC0 File Offset: 0x00132FC0
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

	// Token: 0x0400242B RID: 9259
	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_Death = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_Death.prefab:19fffba6da6499444a0c0895b3e26307");

	// Token: 0x0400242C RID: 9260
	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_DefeatPlayer = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_DefeatPlayer.prefab:78db84ca05e10a749b5653311fe40572");

	// Token: 0x0400242D RID: 9261
	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_EmoteResponse.prefab:d8ccfc2635d6b3d4e9b0a26b2ac3bc41");

	// Token: 0x0400242E RID: 9262
	private static readonly AssetReference VO_DALA_BOSS_23h_SharkyMcFin_Intro = new AssetReference("VO_DALA_BOSS_23h_SharkyMcFin_Intro.prefab:0ef7854270498b843825a831374135e7");

	// Token: 0x0400242F RID: 9263
	private HashSet<string> m_playedLines = new HashSet<string>();
}
