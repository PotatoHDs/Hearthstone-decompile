using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_68h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01.prefab:a873bb5e7365cdc4c915b4b650a01dbc");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Death_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Death_01.prefab:76c49765d2c812643bcb00d7a92d6112");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_DefeatPlayer_01.prefab:d39518f20db49a745a80013ad9b4139b");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01.prefab:0ec964c2034842a479fa06e9e81ce122");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02.prefab:9d9a010736697b44db997de8e1f6c930");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03.prefab:6be21abc0ed74f443a19711cd2a3a1b4");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04.prefab:586f7f44c7675b342b58b9a9d7055bd7");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05.prefab:598761b5a02cc504da2f14a8deae30ff");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07.prefab:6aae75afaf0b63f48b206406a3f6eda1");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08.prefab:43908666d428c294aaf5469ac22883e8");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Idle_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Idle_01.prefab:c6f0ed7e5efa54b4ea0faeaed6907ce2");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Idle_02 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Idle_02.prefab:7ed2e1ee86dff144386d2a50733e8cef");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Idle_03 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Idle_03.prefab:149ce7a0eaaeb4c4391212f44f091749");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Intro_01.prefab:02a6b55bd379de848aa4e2271fab4fe3");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01.prefab:ac364d1574d7fb34ba153514c9ed047d");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01.prefab:501bbfe0d99f0f544b74712260f9d650");

	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01.prefab:73d103d2c3238064eaa756bc8e48f62e");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_68h_Female_BloodElf_Idle_01, VO_DALA_BOSS_68h_Female_BloodElf_Idle_02, VO_DALA_BOSS_68h_Female_BloodElf_Idle_03 };

	private static List<string> m_HeroPowerTrigger = new List<string> { VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01, VO_DALA_BOSS_68h_Female_BloodElf_Death_01, VO_DALA_BOSS_68h_Female_BloodElf_DefeatPlayer_01, VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07, VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08,
			VO_DALA_BOSS_68h_Female_BloodElf_Idle_01, VO_DALA_BOSS_68h_Female_BloodElf_Idle_02, VO_DALA_BOSS_68h_Female_BloodElf_Idle_03, VO_DALA_BOSS_68h_Female_BloodElf_Intro_01, VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01, VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01, VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_68h_Female_BloodElf_Intro_01;
		m_deathLine = VO_DALA_BOSS_68h_Female_BloodElf_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTrigger);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "GVG_003":
			case "KAR_073":
			case "KAR_075":
			case "KAR_076":
			case "KAR_077":
			case "KAR_091":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01);
				break;
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
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "GVG_003":
			case "KAR_073":
			case "KAR_075":
			case "KAR_076":
			case "KAR_077":
			case "KAR_091":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01);
				break;
			}
		}
	}
}
