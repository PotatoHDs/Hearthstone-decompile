using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_208h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Death_Long_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Death_Long_03.prefab:84be873500689e24ab4107904dd1f040");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01.prefab:df0c3a54ae4b8ff4e98a23600f2a9615");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01.prefab:c810f9ab2a4ed1b4fac70563c95c379e");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01.prefab:faf5a3cbcb0dfc64b8a5207227a2ce65");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01.prefab:f767120d831ffee498b8ddcd00c775fc");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01.prefab:26662358917d41c459f9152115f0b291");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01.prefab:790ee61f6034ea6428e564431fd81aaf");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01.prefab:a98626c6cb1556741a48e7d830140a18");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01.prefab:5cd92c3cc3d1af1418c0b005a77a7cfc");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01.prefab:60b57a978dca99142b61e78fc081840d");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01.prefab:159cadedae03c3545a5396480afabcc0");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_Killed_01.prefab:f82fd58230a531241be4c4928bffcde8");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_Killed_02.prefab:ef44e03a5e60997468893296d1c28398");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_Killed_03.prefab:2490eaf1df774e045ab8292b7f9b5816");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventCopies_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventCopies_01.prefab:aa03548831424464685ca0964845647a");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventMedic_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventMedic_01.prefab:21f65fa764d7eeb4291896f75e7f7145");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventMender_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventMender_01.prefab:b85bf9754c422ea449157e8046cfe17c");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventSoulbreaker_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventSoulbreaker_01.prefab:047f167bd32a715468bc0f4f7b0e3b38");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01.prefab:79232de0686997d4b8e6f99390185cd6");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_Seance_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_Seance_01.prefab:05b23f6b543b88e4b9e82358ea00d36b");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_Seance_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_Seance_02.prefab:47732cbd20c21fa44b6c1b37690a9577");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_Surrender_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_Surrender_01.prefab:49b82c7abac913949bba1d34de4c0a77");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01.prefab:6f9e02b6505e8bb4d83841a6a6dd99f4");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02.prefab:3c783dfb2874be7418cac42d00db11f1");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03.prefab:9c01a87608c61174eac276a5e19ae340");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigRez_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigRez_01.prefab:0533bb6f24b1a1f469f0ebf91babdbd1");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigRez_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigRez_02.prefab:bd481b0579944f44ba9d7d5fcfdc250a");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigRez_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigRez_03.prefab:b899e5b1275cb0e4a8ed38c2e0a4b135");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01.prefab:9a90fea561ab3f14ba48452918be21bf");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02.prefab:ec8e756fd7344be4987f68661a639f45");

	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03.prefab:4b31d0e515164d54d941b8b45dc5a3fe");

	private List<string> m_DeathrattleLines = new List<string> { VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01, VO_TRLA_208h_Female_Troll_Play_Seance_02 };

	private List<string> m_HealingLines = new List<string> { VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02, VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03 };

	private List<string> m_SpellLines = new List<string> { VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01, VO_TRLA_208h_Female_Troll_EventCopies_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_208h_Female_Troll_Death_Long_03, VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01, VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01, VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01, VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01, VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01, VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01, VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01, VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01, VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01,
			VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01, VO_TRLA_208h_Female_Troll_Shrine_Killed_01, VO_TRLA_208h_Female_Troll_Shrine_Killed_02, VO_TRLA_208h_Female_Troll_Shrine_Killed_03, VO_TRLA_208h_Female_Troll_Shrine_BigRez_01, VO_TRLA_208h_Female_Troll_Shrine_BigRez_02, VO_TRLA_208h_Female_Troll_Shrine_BigRez_03, VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01, VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02, VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03,
			VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01, VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02, VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03, VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01, VO_TRLA_208h_Female_Troll_Play_Seance_01, VO_TRLA_208h_Female_Troll_Play_Seance_02, VO_TRLA_208h_Female_Troll_Play_Surrender_01, VO_TRLA_208h_Female_Troll_EventMedic_01, VO_TRLA_208h_Female_Troll_EventMender_01, VO_TRLA_208h_Female_Troll_EventSoulbreaker_01,
			VO_TRLA_208h_Female_Troll_EventCopies_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_208h_Female_Troll_Death_Long_03;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_208h_Female_Troll_Shrine_Killed_01, VO_TRLA_208h_Female_Troll_Shrine_Killed_02, VO_TRLA_208h_Female_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01 };
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string> { VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01 };
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string> { VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01 };
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Shrine_BigRez_02);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03);
			break;
		case 1004:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_DeathrattleLines);
			break;
		case 1005:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HealingLines);
			break;
		case 1006:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_SpellLines);
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
			case "TRL_260":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Shrine_BigRez_01);
				break;
			case "TRL_502":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Shrine_BigRez_03);
				break;
			case "TRL_501":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01);
				break;
			case "TRL_097":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Play_Seance_01);
				break;
			case "TRL_500":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_Play_Surrender_01);
				break;
			case "TRLA_150":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_EventMedic_01);
				break;
			case "TRLA_151":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_EventMender_01);
				break;
			case "TRLA_152":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_208h_Female_Troll_EventSoulbreaker_01);
				break;
			}
		}
	}
}
