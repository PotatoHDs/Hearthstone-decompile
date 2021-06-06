using System.Collections;
using System.Collections.Generic;

public class TRL_Dungeon_Boss_204h : TRL_Dungeon
{
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Death_Long_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Death_Long_03.prefab:fda150aeab916c247b5e7849cf7c17fd");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01.prefab:002210047b2026043aca9bec75914b70");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01.prefab:55792cf328c80294c9a70143ab1c55f9");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01.prefab:210dc50662e3c564b8ab24b060c72f46");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01.prefab:3e1457e272054124ca4a2cb85dad89ff");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01.prefab:10d05366788d54a43a674d80e1edcb8e");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01.prefab:fda94620074f94441834d158801217d8");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01.prefab:6ef33f7420a5de64090e2d303730d6db");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01.prefab:f4dbe7aab706692498ab873e8882a460");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01.prefab:922a0ade13b61c847a4d5d3c1136eda5");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_Killed_01.prefab:198f62dc268ac6941b005a9cacb82508");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_Killed_02.prefab:759e5249d671ece43a543a2eef6daad1");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_Killed_03.prefab:ac9f78056deea734496a44a18e3804a0");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Harbinger_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Harbinger_01.prefab:bf83f97a09381e1428294b3027d3ea7a");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Hatchet_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Hatchet_02.prefab:247c8868743663d41ad2c6ff3a75d61e");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Lynx_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Lynx_01.prefab:9a3ced7509c29bb4eb8e7adbee37a0fc");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Panther_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Panther_01.prefab:186cc9daf641ff145ad4b5aa0dc8cb53");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_RandomSpell_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_RandomSpell_01.prefab:5926f66071a55a748afab3b1949bfd78");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Reducecost_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Reducecost_01.prefab:4642293430bc30e4cbabfb995e800fa6");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02.prefab:5a397fcc9b97d3f41a9f5f64b9ba98b5");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03.prefab:f9973fb2d7005754098083cf8383eeb8");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04.prefab:cf30b9449d5e2854bb2ea1a8685069a2");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02.prefab:350fbe7120c47dd489e7012b4da2c184");

	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03.prefab:b543906e2e89a3144b918d16ed0dbe22");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_TRLA_204h_Male_Troll_Death_Long_03, VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01, VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01, VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01, VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01, VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01, VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01, VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01, VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01, VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01,
			VO_TRLA_204h_Male_Troll_Shrine_Killed_01, VO_TRLA_204h_Male_Troll_Shrine_Killed_02, VO_TRLA_204h_Male_Troll_Shrine_Killed_03, VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02, VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03, VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04, VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02, VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03, VO_TRLA_204h_Male_Troll_Event_Panther_01, VO_TRLA_204h_Male_Troll_Event_Lynx_01,
			VO_TRLA_204h_Male_Troll_Event_Harbinger_01, VO_TRLA_204h_Male_Troll_Event_RandomSpell_01, VO_TRLA_204h_Male_Troll_Event_Reducecost_01, VO_TRLA_204h_Male_Troll_Event_Hatchet_02
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = VO_TRLA_204h_Male_Troll_Death_Long_03;
		TRL_Dungeon.s_responseLineGreeting = VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string> { VO_TRLA_204h_Male_Troll_Shrine_Killed_01, VO_TRLA_204h_Male_Troll_Shrine_Killed_02, VO_TRLA_204h_Male_Troll_Shrine_Killed_03 };
		TRL_Dungeon.s_genericShrineDeathLines = new List<string> { VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01 };
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string> { VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01 };
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
			yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Event_RandomSpell_01);
			break;
		case 1002:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Event_Reducecost_01);
			break;
		case 1003:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02);
			break;
		case 1004:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02);
			break;
		case 1005:
			yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03);
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
			case "TRL_901":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Event_Panther_01);
				break;
			case "TRL_900":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04);
				break;
			case "TRLA_165":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03);
				break;
			case "TRL_348":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Event_Lynx_01);
				break;
			case "TRLA_166":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Event_Harbinger_01);
				break;
			case "TRL_111":
				yield return PlayLineOnlyOnce(actor, VO_TRLA_204h_Male_Troll_Event_Hatchet_02);
				break;
			}
		}
	}
}
