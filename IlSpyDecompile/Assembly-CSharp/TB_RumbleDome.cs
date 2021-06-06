using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_RumbleDome : TB_RumbleDome_Dungeon
{
	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01.prefab:6c0893c10d1c16e49930baceece0bc73");

	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01.prefab:aea0e8479b5f925458578de8cf60a1fd");

	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Loss_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Loss_01.prefab:f6d508e26f96afb4880fbc20f83260a6");

	private static readonly AssetReference VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Victory_01 = new AssetReference("VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Victory_01.prefab:8eda98546fa846147ac60318c0868fa1");

	private static readonly AssetReference Felfire_Ragnaros_Popup_BrassRing = new AssetReference("Felfire_Ragnaros_Popup_BrassRing.prefab:74c40717ec5d2d14a81f07545903b022");

	private int TB_Thunderdome_Discover_Counter;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01, VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01, VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Loss_01, VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Victory_01 };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		foreach (EmoteResponseGroup emoteResponseGroup in m_emoteResponseGroups)
		{
			if (emoteResponseGroup.m_responses.Count != 0 && emoteResponseGroup.m_triggers.Contains(emoteType))
			{
				PlayNextEmoteResponse(emoteResponseGroup, actor);
				CycleNextResponseGroupIndex(emoteResponseGroup);
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 100)
		{
			TB_Thunderdome_Discover_Counter++;
			if (TB_Thunderdome_Discover_Counter < 2)
			{
				yield return new WaitForSeconds(2f);
				if (Random.Range(1, 3) == 1)
				{
					yield return PlayLineOnlyOnce(Felfire_Ragnaros_Popup_BrassRing, VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro03_01);
				}
				else
				{
					yield return PlayLineOnlyOnce(Felfire_Ragnaros_Popup_BrassRing, VO_TB_FelFirefest_FelRagnaros_Male_Elemental_FelfireFest_Intro02_01);
				}
			}
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
	}
}
