using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_203h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Death_Long_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Death_Long_03.prefab:7aa8f69c09ec99645a71f59cc8571311");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01.prefab:3eaa9cc85ccb2904bb6aeff695287d61");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01.prefab:2c8e3c29f48c78b4293a6c36eb2a4148");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01.prefab:95cabd67b5fc86e49a5fab1e356d04e8");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01.prefab:36b35d2e8b7f5a34fbb8c5ba736df4c7");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01.prefab:9397bf55c1b8ed74f823001ef683d97c");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01.prefab:bc676941846eaf64c8d4f0bb625ed924");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Healing_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Healing_01.prefab:8c4451a82771c934f9ada26f59d3e486");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01.prefab:af5453375dbd89a459988a789f10388a");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01.prefab:d833eebea8085824aa817a4324336131");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02.prefab:484a62e1592b5a1448df8c1788164932");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03.prefab:e6981f4dafa7aab40b0db66c57ef7d21");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01.prefab:4652414f10c997341a46b03d7fcf05e1");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01.prefab:31ac6326a7f12b54ab78698d982f40b4");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01.prefab:62325a5be04d0504f98e92fea07bb7c2");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02.prefab:80b2a6d55da8d23479f75454b4d7e14c");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01.prefab:2896eb702707c6646a8f12a62650ac7d");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01.prefab:33fa48ff7b162064dae41cbdd8f4ab7d");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01.prefab:aba4fbd2fdd50524cb778af23f781f62");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Killed_01.prefab:af4d62cba97102442afd786ad15dd1c5");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Killed_02.prefab:4a6bf4df648be43469f5170244e4e807");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Killed_03.prefab:d36975d6744af9045ac6ff9199a6a888");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01.prefab:e64e4aac87bb183439df9b329d29f70a");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02.prefab:48e3d425862fc7e42bcecbcf66a3a1bc");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03.prefab:bd5b5a590c9bf7d4f82ccd6b1eb95608");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01.prefab:73fb57eb54477f743a53b0d46b28eca0");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Rush_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Rush_01.prefab:0d1831e4d4e582341b82f192be50da38");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Rush_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Rush_02.prefab:7339e8681806ee142ba724b94b843b0a");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Shirvallah_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Shirvallah_01.prefab:2398594799ae6bd4ab2ca1cd5063de74");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Spirit_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Spirit_01.prefab:b99d2bb4675f1124289835713878e88a");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_TimeOut_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_TimeOut_01.prefab:b40a0f5826a7ac642a132eedf8d49375");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_TimeOut_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_TimeOut_02.prefab:8f8abb1e352d6594f86e0a5b657eb532");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Buff_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Buff_01.prefab:9ea8cd21c1c933b46b4739a600bdcbf1");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Buff_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Buff_02.prefab:19c243f13420336438df4318a9f7a944");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Buff_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Buff_03.prefab:ce951ea2bf2c75c4bbfad4e1553d2089");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_01.prefab:2988c6e3b011969449b5aa2ad69e9640");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_02.prefab:d50afb5d0e5dd5f4b9658ffc194cab32");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_03.prefab:85095f3a35868ad448217ead72cec4cb");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_04 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_04.prefab:8c858928a5db4a94b9ff6751386bb6ee");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01.prefab:2cf8111ba9b68294f96c8da0cb054039");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02.prefab:cbb2562f68a1e8b4cbdc3ba674b2dcf3");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03.prefab:fa9002a0287d93d4f8c02c1fd74f55de");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Return_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Return_01.prefab:13d861b07c771434d91f676f69c23e20");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Return_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Return_02.prefab:c83ba3e021a14ec4abaa0be9be378a42");

	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Return_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Return_03.prefab:8a1c17a87a9294246a0533eeef3430fd");

	private List<string> m_ShieldLines = new List<string> { VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01, VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02, VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03 };

	private List<string> m_BuffLines = new List<string> { VO_TRLA_203h_Male_Troll_Shrine_Buff_01, VO_TRLA_203h_Male_Troll_Shrine_Buff_02, VO_TRLA_203h_Male_Troll_Shrine_Buff_03 };

	private List<string> m_DamageLines = new List<string> { VO_TRLA_203h_Male_Troll_Shrine_Damage_01, VO_TRLA_203h_Male_Troll_Shrine_Damage_02, VO_TRLA_203h_Male_Troll_Shrine_Damage_03, VO_TRLA_203h_Male_Troll_Shrine_Damage_04 };

	private List<string> m_RushLines = new List<string> { VO_TRLA_203h_Male_Troll_Play_Rush_01, VO_TRLA_203h_Male_Troll_Play_Rush_02 };

	private List<string> m_TimeOutLines = new List<string> { VO_TRLA_203h_Male_Troll_Play_TimeOut_01, VO_TRLA_203h_Male_Troll_Play_TimeOut_02 };

	private List<string> m_ChallengerLines = new List<string> { VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01, VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02, VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_203h_Male_Troll_Death_Long_03, VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01, VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01, VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01, VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01, VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01, VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01, VO_TRLA_203h_Male_Troll_Play_Healing_01, VO_TRLA_203h_Male_Troll_Shrine_Killed_01, VO_TRLA_203h_Male_Troll_Shrine_Killed_02,
			VO_TRLA_203h_Male_Troll_Shrine_Killed_03, VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02, VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03, VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02, VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01,
			VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01, VO_TRLA_203h_Male_Troll_Shrine_Return_01, VO_TRLA_203h_Male_Troll_Shrine_Return_02, VO_TRLA_203h_Male_Troll_Shrine_Return_03, VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01, VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02, VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03, VO_TRLA_203h_Male_Troll_Shrine_Buff_01, VO_TRLA_203h_Male_Troll_Shrine_Buff_02,
			VO_TRLA_203h_Male_Troll_Shrine_Buff_03, VO_TRLA_203h_Male_Troll_Shrine_Damage_01, VO_TRLA_203h_Male_Troll_Shrine_Damage_02, VO_TRLA_203h_Male_Troll_Shrine_Damage_03, VO_TRLA_203h_Male_Troll_Shrine_Damage_04, VO_TRLA_203h_Male_Troll_Play_Shirvallah_01, VO_TRLA_203h_Male_Troll_Play_Rush_01, VO_TRLA_203h_Male_Troll_Play_Rush_02, VO_TRLA_203h_Male_Troll_Play_Spirit_01, VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01,
			VO_TRLA_203h_Male_Troll_Play_TimeOut_01, VO_TRLA_203h_Male_Troll_Play_TimeOut_02, VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01, VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02, VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_203h_Male_Troll_Death_Long_03;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_203h_Male_Troll_Play_Healing_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Shrine_Killed_01, VO_TRLA_203h_Male_Troll_Shrine_Killed_02, VO_TRLA_203h_Male_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02, VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03 };
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01 };
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01 };
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02 };
		TRL_Dungeon.s_hunterShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01 };
		TRL_Dungeon.s_druidShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01 };
		TRL_Dungeon.s_warlockShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01 };
		TRL_Dungeon.s_mageShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01 };
		TRL_Dungeon.s_priestShrineDeathLines = new List<string> { VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01 };
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_203h_Male_Troll_Shrine_Return_01);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_203h_Male_Troll_Shrine_Return_02);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_203h_Male_Troll_Shrine_Return_03);
			break;
		case 1004:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_ShieldLines);
			break;
		case 1005:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BuffLines);
			break;
		case 1006:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_DamageLines);
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "TRL_309":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_203h_Male_Troll_Play_Spirit_01);
			yield break;
		case "TRL_300":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_203h_Male_Troll_Play_Shirvallah_01);
			yield break;
		case "TRL_307":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01);
			yield break;
		case "TRL_302":
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_TimeOutLines);
			yield break;
		case "TRL_305":
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_ChallengerLines);
			yield break;
		}
		if (entity.HasTag(GAME_TAG.RUSH))
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_RushLines);
		}
	}
}
