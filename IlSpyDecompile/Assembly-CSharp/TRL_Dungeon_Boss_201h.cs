using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_201h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Death_Long_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Death_Long_01.prefab:953b7472d1dabfc4eb7e277757675d99");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02.prefab:579e0b780368892488f18837971efed8");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01.prefab:e9aa5feeca073f44ca6cd80e6d13f214");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01.prefab:48cdedc7ab856d343bd7a69fc5ce279d");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01.prefab:1dbeac2bb2993b141a986882e0dc8388");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01.prefab:4295bde2f7cc62444a14273e5b859921");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02.prefab:59d86b46d36b6f44eb54385b80542ec2");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01.prefab:4b8a95da666ac314b99be2bb6aa073b2");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01.prefab:356917207ff8c1044b6d1b3de3be6001");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03.prefab:4d80d6b060ac0144eb9ba46b5822c51f");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04.prefab:4869605d576a7d04d96806673d1d5ebb");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01.prefab:432a0d79736c8204d994e7e54c9342b2");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01.prefab:0243cb6470c20c44aaedcfbc2885f70b");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02.prefab:e87a7216a675f604c9584a0eb921c816");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01.prefab:cbe771299daf04c4fa268806ac2cacbe");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01.prefab:01ce07578f57e2f438057c3b89066ffc");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01.prefab:03414093c8a7a334c8e9fe34afa12201");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Killed_01.prefab:fc94bcc5f46b2a14da0203447b4c070f");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Killed_02.prefab:665ec6be71c172b4e93d9c6ea83dd04d");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Killed_03.prefab:1d2fd3a1c5a61034492873aebc753791");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01.prefab:5de109a3c5788a34798a36642e1ea927");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02.prefab:587b8ece22b71e040a1be15d9c0d10c0");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03.prefab:bb4c5bcc7d3086c4bbab31b20197993f");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01.prefab:daba602c2c774ac478f2e6043d0b72bb");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02.prefab:705e521881bf7824cab860cf7d8e46a9");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03.prefab:f23acdc8aefe68f499d7a2958c979a10");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01.prefab:f96999bf3e8230d4aad03f0968086615");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01.prefab:b0d57bccd3ce6d04489657e95320d254");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01.prefab:a26c9dcd2f4636943a52ebb3336296f8");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Likkem_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Likkem_01.prefab:d76d49675d024e049b0ffb21268f7ff6");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01.prefab:7fac1120660f57748aaa31228052dd9d");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01.prefab:f724160d015de814c829315ac16c7303");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02.prefab:152ed522cc0102d46bfd3033263c6ff6");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Event_General_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Event_General_01.prefab:b26e94e06048fd448bdbad4acf556291");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Event_General_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Event_General_02.prefab:3d25a4c2f4d148c48b86bae2910fd07e");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Returns_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Returns_01.prefab:283f0b0396c1eba49a3ffb43a9629372");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Returns_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Returns_02.prefab:41b03f7e9ca5e654b85f8a4096802025");

	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Returns_04 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Returns_04.prefab:561b605fedc991944a39644c4104ba97");

	private List<string> m_HexLines = new List<string> { VO_TRLA_201h_Male_Troll_Shrine_Event_General_01, VO_TRLA_201h_Male_Troll_Shrine_Event_General_02, VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01 };

	private List<string> m_OverloadLines = new List<string> { VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01, VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02, VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03 };

	private List<string> m_BigSpellLines = new List<string> { VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01, VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02, VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_201h_Male_Troll_Death_Long_01, VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02, VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01, VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01, VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02, VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01, VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01, VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03,
			VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04, VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02, VO_TRLA_201h_Male_Troll_Shrine_Killed_01, VO_TRLA_201h_Male_Troll_Shrine_Killed_02, VO_TRLA_201h_Male_Troll_Shrine_Killed_03,
			VO_TRLA_201h_Male_Troll_Shrine_Returns_01, VO_TRLA_201h_Male_Troll_Shrine_Returns_02, VO_TRLA_201h_Male_Troll_Shrine_Returns_04, VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02, VO_TRLA_201h_Male_Troll_Shrine_Event_General_01, VO_TRLA_201h_Male_Troll_Shrine_Event_General_02, VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01, VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02, VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03, VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01,
			VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01, VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02, VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03, VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01, VO_TRLA_201h_Male_Troll_EVENT_Likkem_01, VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01, VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01, VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_201h_Male_Troll_Death_Long_01;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Shrine_Killed_01, VO_TRLA_201h_Male_Troll_Shrine_Killed_02, VO_TRLA_201h_Male_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03, VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04 };
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01 };
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01 };
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01 };
		TRL_Dungeon.s_warlockShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01 };
		TRL_Dungeon.s_mageShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01 };
		TRL_Dungeon.s_priestShrineDeathLines = new List<string> { VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02 };
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_201h_Male_Troll_Shrine_Returns_01);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_201h_Male_Troll_Shrine_Returns_02);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_201h_Male_Troll_Shrine_Returns_04);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "EX1_246":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HexLines);
				break;
			case "TRL_345":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01);
				break;
			case "TRL_352":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_201h_Male_Troll_EVENT_Likkem_01);
				break;
			case "TRL_351":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01);
				break;
			case "CS2_053":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01);
				break;
			case "TRLA_160":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01);
				break;
			}
			if (entity.HasBattlecry())
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02);
			}
			else if (entity.GetCardType() == TAG_CARDTYPE.SPELL && entity.GetCost() >= 4)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_BigSpellLines);
			}
			else if (entity.HasTag(GAME_TAG.OVERLOAD_OWED))
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_OverloadLines);
			}
		}
	}
}
