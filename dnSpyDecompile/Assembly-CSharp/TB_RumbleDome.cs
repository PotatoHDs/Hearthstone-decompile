using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C0 RID: 1472
public class TB_RumbleDome : TB_RumbleDome_Dungeon
{
	// Token: 0x06005115 RID: 20757 RVA: 0x001AB110 File Offset: 0x001A9310
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			TB_RumbleDome.VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01,
			TB_RumbleDome.VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01,
			TB_RumbleDome.VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Loss_01,
			TB_RumbleDome.VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06005116 RID: 20758 RVA: 0x001AB1B4 File Offset: 0x001A93B4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		foreach (MissionEntity.EmoteResponseGroup emoteResponseGroup in this.m_emoteResponseGroups)
		{
			if (emoteResponseGroup.m_responses.Count != 0 && emoteResponseGroup.m_triggers.Contains(emoteType))
			{
				base.PlayNextEmoteResponse(emoteResponseGroup, actor);
				this.CycleNextResponseGroupIndex(emoteResponseGroup);
			}
		}
	}

	// Token: 0x06005117 RID: 20759 RVA: 0x001AB240 File Offset: 0x001A9440
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 100)
		{
			this.TB_Thunderdome_Discover_Counter++;
			if (this.TB_Thunderdome_Discover_Counter < 2)
			{
				yield return new WaitForSeconds(2f);
				if (UnityEngine.Random.Range(1, 3) == 1)
				{
					yield return base.PlayLineOnlyOnce(TB_RumbleDome.Felfire_Ragnaros_Popup_BrassRing, TB_RumbleDome.VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01, 2.5f);
				}
				else
				{
					yield return base.PlayLineOnlyOnce(TB_RumbleDome.Felfire_Ragnaros_Popup_BrassRing, TB_RumbleDome.VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01, 2.5f);
				}
			}
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x04004877 RID: 18551
	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01.prefab:6c0893c10d1c16e49930baceece0bc73");

	// Token: 0x04004878 RID: 18552
	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01.prefab:aea0e8479b5f925458578de8cf60a1fd");

	// Token: 0x04004879 RID: 18553
	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Loss_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Loss_01.prefab:f6d508e26f96afb4880fbc20f83260a6");

	// Token: 0x0400487A RID: 18554
	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Victory_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Victory_01.prefab:8eda98546fa846147ac60318c0868fa1");

	// Token: 0x0400487B RID: 18555
	private static readonly AssetReference Felfire_Ragnaros_Popup_BrassRing = new AssetReference("Felfire_Ragnaros_Popup_BrassRing.prefab:74c40717ec5d2d14a81f07545903b022");

	// Token: 0x0400487C RID: 18556
	private int TB_Thunderdome_Discover_Counter;
}
