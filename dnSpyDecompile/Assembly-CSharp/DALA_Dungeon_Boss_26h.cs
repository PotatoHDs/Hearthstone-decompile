using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000447 RID: 1095
public class DALA_Dungeon_Boss_26h : DALA_Dungeon
{
	// Token: 0x06003B90 RID: 15248 RVA: 0x00135ABC File Offset: 0x00133CBC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_Death,
			DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_DefeatPlayer,
			DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse,
			DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B91 RID: 15249 RVA: 0x00135B60 File Offset: 0x00133D60
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_Intro;
		this.m_deathLine = DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_Death;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_26h.VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse;
	}

	// Token: 0x06003B92 RID: 15250 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B93 RID: 15251 RVA: 0x00135B98 File Offset: 0x00133D98
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003B94 RID: 15252 RVA: 0x00135BAE File Offset: 0x00133DAE
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

	// Token: 0x06003B95 RID: 15253 RVA: 0x00135BC4 File Offset: 0x00133DC4
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

	// Token: 0x04002471 RID: 9329
	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_Death = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_Death.prefab:96a8fe4578eb0f24db266fc11ce39e56");

	// Token: 0x04002472 RID: 9330
	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_DefeatPlayer = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_DefeatPlayer.prefab:f2401c70f688a5b4d8938932beaebee4");

	// Token: 0x04002473 RID: 9331
	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_EmoteResponse.prefab:f0c832b4e45f0db4ab850fc140c1f9f3");

	// Token: 0x04002474 RID: 9332
	private static readonly AssetReference VO_DALA_BOSS_26h_DalaranFountainGolem_Intro = new AssetReference("VO_DALA_BOSS_26h_DalaranFountainGolem_Intro.prefab:06e0e9a059c18d64dbbaa86404a76315");

	// Token: 0x04002475 RID: 9333
	private HashSet<string> m_playedLines = new HashSet<string>();
}
