using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRL_Dungeon_Boss_202h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Death_Long_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Death_Long_04.prefab:6c810d6eefcc25643abe5b7c10392e80");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01.prefab:3fca7d65892112648a2786cf2d80d7b4");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01.prefab:1b9f72498ad26e043a8f0999464ba244");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01.prefab:579290639e90a3e42b1d6ceda51a3e84");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01.prefab:ebbe2b7b2aa9c6943b1c679c9c430bfc");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01.prefab:9478297c91161c543ad0b8db6d028f41");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01.prefab:d18e4368aba99dc4faa9f0b6f22d00ec");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01.prefab:4a9177d622dc90a44bd03403061545e3");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01.prefab:a269888d3fc5a05489bc6e83ec50e3e4");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Killed_01.prefab:8c410236da214e649b505938e56138b0");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02.prefab:e913e5f4071d23046960c773b5089879");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03.prefab:33417ca00c7c8534889e55a23f8f4373");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01.prefab:92e4b6e930aeb884c8e6e74b6592cea0");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Killed_02.prefab:47103f73babb8914699834c02e28b8b0");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Killed_03.prefab:a294ee6ef84248c49bcc7810b9dea5ef");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_01.prefab:f5f4b0f3349574b45b1af2b72d80420b");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_02.prefab:1c840dd62251b2341a81e82f003ca70f");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_03.prefab:bd6b92e07cb06744e9d2183a27e9f98c");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_04.prefab:5c74df4036a1547409d1eeff65225caf");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Spell_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Spell_01.prefab:914f1ff6cde36b34e9744584ad6f7a8e");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Spell_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Spell_02.prefab:16bd2fee248cc90469f4de43bff1a0cd");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Spell_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Spell_03.prefab:7635ec176e350fc4896134038353040e");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Gral_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Play _Gral_02.prefab:758863504113a2c48b4ca790bf508e8a");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_02.prefab:1ef85ead71e9726409e1d6fb90ba4b36");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_03.prefab:1e63b9743fd9e2347a4c67ddf699b67a");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_04.prefab:41b322266f1df6540902613f8e2a4cc3");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_06 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_06.prefab:f41338d68c6cac8479308fb842384845");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01.prefab:bd5675d93357f7a498b24d5c5e5dcb3e");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Patches_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Patches_01.prefab:b62d2f9e4cd3c04408f4291879d90aa5");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Pirate_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Pirate_01.prefab:d1547cdbbcaba37459935001cf32553b");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Pirate_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Pirate_03.prefab:b949411bbd0c5f643b2f5b25fe61b919");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Pirate_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Pirate_04.prefab:9ede01f601c9379458b8feb4dcf84989");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_RaidingParty_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_RaidingParty_01.prefab:c11541abb0ff4cb469e434894abd83ca");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01.prefab:281efc0ac2b8ece4793771ec0b8f9528");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Spirit_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Spirit_01.prefab:dc4403f41f790954280488b5f0be61d3");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01.prefab:3e803912a9d1a4e4a9c83d5889b7415a");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Mana_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Mana_02.prefab:99ae18eb40bac4a4e827c5d26902fd9b");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Mana_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Mana_04.prefab:9a3d43cde04993045be2cc177170ce9f");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Return_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Return_02.prefab:a0b599b5392ceb0488281692a99a23c5");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Return_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Return_03.prefab:3ccee2a5dc2e13943b58be2f6113f860");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Return_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Return_04.prefab:c88118d6c6fec7c4c8281714c2530d09");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Stealth_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Stealth_01.prefab:693c580ac4b1dd647802a086a0335122");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Stealth_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Stealth_02.prefab:9c56a25f2eda0d949b987bc24f9e3f6a");

	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Stealth_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Stealth_03.prefab:957a66ff65b19394191925372c24429f");

	private List<string> m_StealthShrineDeathLines = new List<string> { VO_TRLA_202h_Female_Troll_Shrine_Stealth_01, VO_TRLA_202h_Female_Troll_Shrine_Stealth_02, VO_TRLA_202h_Female_Troll_Shrine_Stealth_03 };

	private List<string> m_StealLines = new List<string> { VO_TRLA_202h_Female_Troll_Shrine_Mana_02, VO_TRLA_202h_Female_Troll_Shrine_Mana_04, VO_TRLA_202h_Female_Troll_Play_Burgle_01, VO_TRLA_202h_Female_Troll_Play_Burgle_02, VO_TRLA_202h_Female_Troll_Play_Burgle_03, VO_TRLA_202h_Female_Troll_Play_Burgle_04 };

	private List<string> m_SpellLines = new List<string> { VO_TRLA_202h_Female_Troll_Shrine_Spell_01, VO_TRLA_202h_Female_Troll_Shrine_Spell_02, VO_TRLA_202h_Female_Troll_Shrine_Spell_03 };

	private List<string> m_PirateLines = new List<string> { VO_TRLA_202h_Female_Troll_Play_Pirate_01, VO_TRLA_202h_Female_Troll_Play_Pirate_03, VO_TRLA_202h_Female_Troll_Play_Pirate_04 };

	private float chanceToPlayVO = 0.2f;

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_202h_Female_Troll_Death_Long_04, VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01, VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01, VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01, VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01, VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01, VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01, VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01, VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01, VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02,
			VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03, VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01, VO_TRLA_202h_Female_Troll_Shrine_Killed_01, VO_TRLA_202h_Female_Troll_Shrine_Killed_02, VO_TRLA_202h_Female_Troll_Shrine_Killed_03, VO_TRLA_202h_Female_Troll_Shrine_Return_02, VO_TRLA_202h_Female_Troll_Shrine_Return_03, VO_TRLA_202h_Female_Troll_Shrine_Return_04, VO_TRLA_202h_Female_Troll_Shrine_Stealth_01, VO_TRLA_202h_Female_Troll_Shrine_Stealth_02,
			VO_TRLA_202h_Female_Troll_Shrine_Stealth_03, VO_TRLA_202h_Female_Troll_Shrine_Mana_02, VO_TRLA_202h_Female_Troll_Shrine_Mana_04, VO_TRLA_202h_Female_Troll_Play_Burgle_01, VO_TRLA_202h_Female_Troll_Play_Burgle_02, VO_TRLA_202h_Female_Troll_Play_Burgle_03, VO_TRLA_202h_Female_Troll_Play_Burgle_04, VO_TRLA_202h_Female_Troll_Shrine_Spell_01, VO_TRLA_202h_Female_Troll_Shrine_Spell_02, VO_TRLA_202h_Female_Troll_Shrine_Spell_03,
			VO_TRLA_202h_Female_Troll_Play_Gral_02, VO_TRLA_202h_Female_Troll_Play_Spirit_01, VO_TRLA_202h_Female_Troll_Play_Pirate_01, VO_TRLA_202h_Female_Troll_Play_Pirate_03, VO_TRLA_202h_Female_Troll_Play_Pirate_04, VO_TRLA_202h_Female_Troll_Play_CannonBarrage_02, VO_TRLA_202h_Female_Troll_Play_CannonBarrage_03, VO_TRLA_202h_Female_Troll_Play_CannonBarrage_04, VO_TRLA_202h_Female_Troll_Play_CannonBarrage_06, VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01,
			VO_TRLA_202h_Female_Troll_Play_RaidingParty_01, VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01, VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01, VO_TRLA_202h_Female_Troll_Play_Patches_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_202h_Female_Troll_Death_Long_04;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_202h_Female_Troll_Shrine_Killed_01, VO_TRLA_202h_Female_Troll_Shrine_Killed_02, VO_TRLA_202h_Female_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01, VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02, VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03 };
		TRL_Dungeon.s_mageShrineDeathLines = new List<string> { VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01 };
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Shrine_Return_02);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Shrine_Return_03);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Shrine_Return_04);
			break;
		case 1004:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_StealthShrineDeathLines);
			break;
		case 1005:
			if (Random.Range(0f, 1f) < chanceToPlayVO)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_StealLines);
			}
			break;
		case 1006:
			if (Random.Range(0f, 1f) < chanceToPlayVO)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_SpellLines);
			}
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
		case "TRL_092":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_Spirit_01);
			yield break;
		case "TRL_409":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_Gral_02);
			yield break;
		case "TRL_157":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01);
			yield break;
		case "TRL_124":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_RaidingParty_01);
			yield break;
		case "GVG_025":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01);
			yield break;
		case "AT_032":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01);
			yield break;
		case "CFM_637":
			yield return PlayLineOnlyOnce(actor, VO_TRLA_202h_Female_Troll_Play_Patches_01);
			yield break;
		}
		if (entity.HasRace(TAG_RACE.PIRATE) && Random.Range(0f, 1f) < 0.5f)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PirateLines);
		}
	}
}
