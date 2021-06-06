using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_207h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Death_Long_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Death_Long_01.prefab:bc8b65ed675f5e64cbba0999d87f40a3");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01.prefab:96283758d1899b24390678b7abb72f70");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01.prefab:78ebc7d0c76e8244c9fd46fc27389eaf");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01.prefab:bdcf2acc88e78b1488ec32fe19ee22fb");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01.prefab:b2fe442969ca8a04cae99f02da49ed3a");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02.prefab:b28587853451b0845b453b865ccc8287");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01.prefab:5e6e398c52cc12f43b9082044c704b6e");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01.prefab:ea26d56e3a26b7f499758b21d1fb7eae");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01.prefab:120e30b322f7c414aaa1ebae6943b3cf");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02.prefab:88924bd3d4866cc4c99e2d0f636bf927");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06.prefab:c35704f96906a0943826f17e1fd7bd47");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01.prefab:c8679b5ad3b83d44d81ba2d274c022f7");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01.prefab:af248fdd112479c43aec097e8a3aafe7");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_Killed_02.prefab:951230a7e4e8f43489baa8e6aa4cdd1e");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_Killed_03.prefab:6ef4189287a81864d91e7cb1053ff0c2");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_Killed_04 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_Killed_04.prefab:3def3ba7d51db304e98a904efa230e0d");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_EVENT_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_EVENT_01.prefab:e699568563b9bde4fbb55fe1eed55a4f");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_EVENT_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_EVENT_02.prefab:004297da355f7c84ebc31481913de1e4");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_EVENT_03 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_EVENT_03.prefab:8a600e0ab04979e41829dbfb50995f02");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Opponent_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Opponent_01.prefab:a946177a47b732d4ebcbcdf4acf2f25f");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Antonidas_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Antonidas_01.prefab:c6d71428280ac1e49b13d32ec86d3bae");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01.prefab:f8b9d0c8330b19640a0c1bc6f6784ec1");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01.prefab:bcceb9f377808a945923b9febd0afa4d");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Fireball_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Fireball_01.prefab:d49c30de5a10a7141a0e158a99b24580");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Flamestrike_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Flamestrike_01.prefab:9c4199b67866fab4faed66c4df6288ff");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Janalai_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Janalai_01.prefab:3976a9f385b557f48915611660ecc6ff");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01.prefab:382a52eeec0c0db4c967f89193f2ab01");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Pyroblast_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Pyroblast_01.prefab:da68f6a1a92ff6748b6dc919dd1d72a3");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01.prefab:2b865e8769a80c049b8979c36c418c55");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_FirePlume_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_FirePlume_01.prefab:ad7e903289effe14fb1ef56802c9dfcb");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01.prefab:cf280a70e937695439e47ef09b4c9681");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02.prefab:ca9e17a35fd72484d8f67aaf8f5b1e2f");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01.prefab:1a73f9709282a9942a262f0a86673e3e");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01.prefab:8d48c00adb96adb46970cc5a6c080356");

	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Trigger_Meteor_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Trigger_Meteor_01.prefab:8e5af4adfe5990340b5df77c887a1fe0");

	private List<string> m_HeroPowerLines = new List<string> { VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01, VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_207h_Male_Troll_Death_Long_01, VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01, VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01, VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02, VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01, VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01, VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01, VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01, VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02,
			VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06, VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01, VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01, VO_TRLA_207h_Male_Troll_Shrine_Killed_02, VO_TRLA_207h_Male_Troll_Shrine_Killed_03, VO_TRLA_207h_Male_Troll_Shrine_Killed_04, VO_TRLA_207h_Male_Troll_Shrine_EVENT_01, VO_TRLA_207h_Male_Troll_Shrine_EVENT_02, VO_TRLA_207h_Male_Troll_Shrine_EVENT_03, VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01,
			VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02, VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01, VO_TRLA_207h_Male_Troll_Play_FirePlume_01, VO_TRLA_207h_Male_Troll_Play_Antonidas_01, VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01, VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01, VO_TRLA_207h_Male_Troll_Play_Janalai_01, VO_TRLA_207h_Male_Troll_Play_Pyroblast_01, VO_TRLA_207h_Male_Troll_Play_Fireball_01, VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01,
			VO_TRLA_207h_Male_Troll_Play_Flamestrike_01, VO_TRLA_207h_Male_Troll_Kill_Opponent_01, VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01, VO_TRLA_207h_Male_Troll_Trigger_Meteor_01, VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_207h_Male_Troll_Death_Long_01;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_207h_Male_Troll_Shrine_Killed_02, VO_TRLA_207h_Male_Troll_Shrine_Killed_03, VO_TRLA_207h_Male_Troll_Shrine_Killed_04 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02, VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06 };
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string> { VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01 };
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string> { VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01 };
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1001:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Shrine_EVENT_01);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Shrine_EVENT_02);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Shrine_EVENT_03);
			break;
		case 1004:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "TRL_315":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01);
				break;
			case "UNG_084":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_FirePlume_01);
				break;
			case "EX1_559":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Antonidas_01);
				break;
			case "CS2_118":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01);
				break;
			case "TRLA_135":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01);
				break;
			case "TRL_316":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Janalai_01);
				break;
			case "EX1_279":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Pyroblast_01);
				break;
			case "CS2_029":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Fireball_01);
				break;
			case "TRL_317":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01);
				break;
			case "CS2_032":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Play_Flamestrike_01);
				break;
			case "TRLA_133":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Kill_Opponent_01);
				break;
			case "TRLA_129s":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01);
				break;
			case "UNG_955":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Trigger_Meteor_01);
				break;
			case "GIL_147":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01);
				break;
			}
		}
	}
}
