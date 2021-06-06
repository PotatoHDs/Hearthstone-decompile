using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_200h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Death_Long_02 = new AssetReference("VO_TRLA_200h_Male_Troll_Death_Long_02.prefab:617d2f10a0b784f41996ca4470a3b521");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01.prefab:a78b045849c256541bd980732271ea96");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01.prefab:010e8c52c50b01e4e963038b87ce5a06");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01.prefab:754e3f0ca927cd042920033008c98206");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01.prefab:4bd9201d7208452459dcc8ecaa4dab4d");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01.prefab:0194f93848bfc1a45a0cd1b5bfc0e4d4");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01.prefab:fb6d2284183033b4a8ecbabc20ecc488");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01.prefab:8ec038795f003814ea0c18144989eb38");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01.prefab:3ef5d34b96e5caf49b4f527acce0b782");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02.prefab:3f858ff6189b4ff4da08f37ed7693e72");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03.prefab:f1467c267e3b63e4d83985c4a55ef7cd");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04.prefab:463a9bbc835d60c4fb427ed4e738e84c");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Shrine_Killed_01.prefab:84df71a8c42ef874e81998ab5396ec1e");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_200h_Male_Troll_Shrine_Killed_02.prefab:93adfb21b6f069f43bbd56fbfae92c38");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_200h_Male_Troll_Shrine_Killed_03.prefab:c0228fb20edb1bb4db78e11399956514");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Armor_01 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Armor_01.prefab:1984bf68e32bb5f44a1419209c8fe516");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Armor_02 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Armor_02.prefab:7438d447e34cc464281b7fd3c79142b4");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Armor_03 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Armor_03.prefab:31b7276ace6acd747a09a2935ee46f8f");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Dragon_01 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Dragon_01.prefab:6503010125cd3974c9facd16bf43d365");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Dragon_02 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Dragon_02.prefab:36a0484b6bb4b6b43af3f925025bc2fd");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Dragon_03 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Dragon_03.prefab:6e11197568e0e31458099b94a362d8c1");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Weapon_01 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Weapon_01.prefab:23ef3880e2e8569459e4583eff1bbdfa");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Weapon_02 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Weapon_02.prefab:a36227bbca3702d488e32d305f3b0ac9");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Weapon_03 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Weapon_03.prefab:5ac1fc559e791bb4894d196446cd1dc9");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Akali_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Akali_01.prefab:cbe43a80594c86043a3cef8fd6b81105");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Hakkar_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Hakkar_01.prefab:cf429cb885fae4344a2f52e723d62a2b");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Janalai_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Janalai_01.prefab:d6ccfa30ea5bad242b857438d2a397d7");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Ragewing_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Ragewing_01.prefab:bb4d1f60660d16c4f9cd7c24c4e5f2d2");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01.prefab:c72bcf5a7e7e2cb44b303b4dc9509d1c");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineComesBack_01 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineComesBack_01.prefab:69bdad0df848747499b0b3b748a764dd");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineComesBack_02 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineComesBack_02.prefab:16e8ecc16fe8e624fb9d9c3004bd4646");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineComesBack_03 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineComesBack_03.prefab:27e4ef847fc7c3d4aa98b4819f30951b");

	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineDoesThing_01 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineDoesThing_01.prefab:98e761a9605c28f45877c5aca006fd42");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_DragonLines = new List<string> { VO_TRLA_200h_Male_Troll_EVENT_Dragon_01, VO_TRLA_200h_Male_Troll_EVENT_Dragon_02, VO_TRLA_200h_Male_Troll_EVENT_Dragon_03 };

	private List<string> m_WeaponLines = new List<string> { VO_TRLA_200h_Male_Troll_EVENT_Weapon_01, VO_TRLA_200h_Male_Troll_EVENT_Weapon_02, VO_TRLA_200h_Male_Troll_EVENT_Weapon_03 };

	private List<string> m_ArmorLines = new List<string> { VO_TRLA_200h_Male_Troll_EVENT_Armor_01, VO_TRLA_200h_Male_Troll_EVENT_Armor_02, VO_TRLA_200h_Male_Troll_EVENT_Armor_03 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_200h_Male_Troll_Death_Long_02, VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01, VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01, VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01, VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01, VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01, VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01, VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01, VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02,
			VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03, VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04, VO_TRLA_200h_Male_Troll_Shrine_Killed_01, VO_TRLA_200h_Male_Troll_Shrine_Killed_02, VO_TRLA_200h_Male_Troll_Shrine_Killed_03, VO_TRLA_200h_Male_Troll_ShrineComesBack_01, VO_TRLA_200h_Male_Troll_ShrineComesBack_02, VO_TRLA_200h_Male_Troll_ShrineComesBack_03, VO_TRLA_200h_Male_Troll_ShrineDoesThing_01, VO_TRLA_200h_Male_Troll_Play_Akali_01,
			VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01, VO_TRLA_200h_Male_Troll_Play_Hakkar_01, VO_TRLA_200h_Male_Troll_Play_Janalai_01, VO_TRLA_200h_Male_Troll_Play_Ragewing_01, VO_TRLA_200h_Male_Troll_EVENT_Armor_01, VO_TRLA_200h_Male_Troll_EVENT_Armor_02, VO_TRLA_200h_Male_Troll_EVENT_Armor_03, VO_TRLA_200h_Male_Troll_EVENT_Dragon_01, VO_TRLA_200h_Male_Troll_EVENT_Dragon_02, VO_TRLA_200h_Male_Troll_EVENT_Dragon_03,
			VO_TRLA_200h_Male_Troll_EVENT_Weapon_01, VO_TRLA_200h_Male_Troll_EVENT_Weapon_02, VO_TRLA_200h_Male_Troll_EVENT_Weapon_03
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_200h_Male_Troll_Death_Long_02;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_200h_Male_Troll_Shrine_Killed_01, VO_TRLA_200h_Male_Troll_Shrine_Killed_02, VO_TRLA_200h_Male_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02, VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03, VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04 };
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_200h_Male_Troll_ShrineComesBack_01);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_200h_Male_Troll_ShrineComesBack_02);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_200h_Male_Troll_ShrineComesBack_03);
			break;
		case 1004:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_200h_Male_Troll_ShrineDoesThing_01);
			break;
		case 1005:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_ArmorLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
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
			if (cardId == "TRL_316")
			{
				yield return PlayLineOnlyOnce(actor, VO_TRLA_200h_Male_Troll_Play_Janalai_01);
			}
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
			case "TRL_329":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_200h_Male_Troll_Play_Akali_01);
				break;
			case "TRL_327":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01);
				break;
			case "TRL_541":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_200h_Male_Troll_Play_Hakkar_01);
				break;
			case "TRL_548":
				yield return PlayLineOnlyOnce(enemyActor, VO_TRLA_200h_Male_Troll_Play_Ragewing_01);
				break;
			}
			if (entity.HasRace(TAG_RACE.DRAGON))
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_DragonLines);
			}
			if (entity.GetCardType() == TAG_CARDTYPE.WEAPON)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_WeaponLines);
			}
		}
	}
}
