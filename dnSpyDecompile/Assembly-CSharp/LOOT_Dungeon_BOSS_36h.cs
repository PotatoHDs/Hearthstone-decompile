using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003C7 RID: 967
public class LOOT_Dungeon_BOSS_36h : LOOT_Dungeon
{
	// Token: 0x060036AE RID: 13998 RVA: 0x00115944 File Offset: 0x00113B44
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			LOOT_Dungeon_BOSS_36h.VO_LOOT_329_Male_Elemental_Attack_01,
			LOOT_Dungeon_BOSS_36h.VO_LOOT_329_Male_Elemental_Death_01,
			LOOT_Dungeon_BOSS_36h.VO_LOOT_329_Male_Elemental_Play_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060036AF RID: 13999 RVA: 0x001159CC File Offset: 0x00113BCC
	protected override string GetBossDeathLine()
	{
		return LOOT_Dungeon_BOSS_36h.VO_LOOT_329_Male_Elemental_Death_01;
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x001159D8 File Offset: 0x00113BD8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_36h.VO_LOOT_329_Male_Elemental_Play_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(LOOT_Dungeon_BOSS_36h.VO_LOOT_329_Male_Elemental_Attack_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x00115A69 File Offset: 0x00113C69
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.PlayLoyalSideKickBetrayal(missionEvent);
		yield break;
	}

	// Token: 0x04001D42 RID: 7490
	private static readonly AssetReference VO_LOOT_329_Male_Elemental_Attack_01 = new AssetReference("VO_LOOT_329_Male_Elemental_Attack_01.prefab:c263e413027e1c3419330793de1a9b83");

	// Token: 0x04001D43 RID: 7491
	private static readonly AssetReference VO_LOOT_329_Male_Elemental_Death_01 = new AssetReference("VO_LOOT_329_Male_Elemental_Death_01.prefab:7fda354ba88198d4992562d4c9b51373");

	// Token: 0x04001D44 RID: 7492
	private static readonly AssetReference VO_LOOT_329_Male_Elemental_Play_01 = new AssetReference("VO_LOOT_329_Male_Elemental_Play_01.prefab:acccd0bdaf7b3964d8c782e6191599c5");
}
