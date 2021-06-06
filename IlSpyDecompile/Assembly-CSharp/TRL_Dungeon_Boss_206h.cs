using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_206h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Death_Long_Alt_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Death_Long_Alt_01.prefab:3ea25ade7d1d4fc4e96bd6445db6c609");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01.prefab:39763531d0c53f84ea24dbf11e2be9e0");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01.prefab:b45a7f2c1a8531a48b9765ac4a07d4d8");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01.prefab:c7f8bf85f654048469dcc463abeaf406");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01.prefab:6c4b8943a9d967b4f845ffc6f8199432");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01.prefab:81b28005ca7086d428d5a54516b546d8");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01.prefab:b879d8b9421a58f43802d90d2c4fe533");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01.prefab:6f30d3f1de17e57499c3e72a2a483924");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01.prefab:b30c13862fe16d649af8bc8c7b6e0e1a");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01.prefab:99d2ba3259ce13243a0a19f35be707de");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01.prefab:3864bab2de40ebb4f8ff44ceab7cd8b1");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01.prefab:8f2d2b0548ea46a4ea544e7d19b50d1d");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01.prefab:b7952122697a7334b872ab7a5721a609");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01.prefab:30ce14582e70fea41864e71d37a6c827");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_Killed_01.prefab:4310c78a6dc412a47b5f9d472b2f4814");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_Killed_02.prefab:e2e5232ba8a781b4480c42aab7a25471");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_Killed_03.prefab:9c4d525738864ef4094e311ee22e241d");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_HakkarReponse_01 = new AssetReference("VO_TRLA_206h_Female_Troll_HakkarReponse_01.prefab:2ba29514d41edd74da23357cba8141e7");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Play_Hireek_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Play_Hireek_01.prefab:d966e4cf87d0f0e42aa57e8ac3d02b66");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Play_Lanathel_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Play_Lanathel_01.prefab:3ba3fcd1cf47caf44bb28c1d0831f634");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01.prefab:1843cafd27cacf24eb44f8910a6a3b01");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01.prefab:85696055c9ddcb247bc55441fea4ab19");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02.prefab:bf3e331837a7eb0448633c466a5e7019");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03.prefab:6b340e10524f2e041864ef980ff60d51");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01.prefab:fe9b1c7da40891d4ab42e8617b169d64");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02.prefab:e06cd0b9f59d75b4481664a262e2bc09");

	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_General_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_General_01.prefab:2411c9e50130d704a9e7f0b68e4a04c4");

	private List<string> m_BounceLines = new List<string> { VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01, VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_206h_Female_Troll_Death_Long_Alt_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01, VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01, VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01, VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01,
			VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01, VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01, VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01, VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01, VO_TRLA_206h_Female_Troll_Shrine_Killed_01, VO_TRLA_206h_Female_Troll_Shrine_Killed_02, VO_TRLA_206h_Female_Troll_Shrine_Killed_03, VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01, VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02, VO_TRLA_206h_Female_Troll_Shrine_General_01,
			VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01, VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02, VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03, VO_TRLA_206h_Female_Troll_HakkarReponse_01, VO_TRLA_206h_Female_Troll_Play_Lanathel_01, VO_TRLA_206h_Female_Troll_Play_Hireek_01, VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_206h_Female_Troll_Death_Long_Alt_01;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Shrine_Killed_01, VO_TRLA_206h_Female_Troll_Shrine_Killed_02, VO_TRLA_206h_Female_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01 };
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01 };
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01 };
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01 };
		TRL_Dungeon.s_hunterShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01 };
		TRL_Dungeon.s_priestShrineDeathLines = new List<string> { VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01 };
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Shrine_General_01);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03);
			break;
		case 1004:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BounceLines);
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
			if (cardId == "TRL_541")
			{
				yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_HakkarReponse_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "TRLA_181":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01);
				break;
			case "ICC_841":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Play_Lanathel_01);
				break;
			case "TRL_253":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Play_Hireek_01);
				break;
			case "TRL_251":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01);
				break;
			}
		}
	}
}
