using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Anduin_08 : BoH_Anduin_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02.prefab:5dd89aa2dd336a04eabb7e8e4f884b78");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02.prefab:a8c54c1388002c548b6572e91e2b981d");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02.prefab:67bb51b156a9e0e40a41ca5213d17003");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04.prefab:821ac1c0781c6ee4f99474417906b36e");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02.prefab:2e066ba484e25de46a9a9c1acbf1e15e");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02.prefab:eebdb8b1fec7d194caa60dd5d546e0cd");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01.prefab:26360fac3ef28534c9d7bb4cc3f59cea");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_01.prefab:a5e21394425b9f2499a376f68d0988df");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_02.prefab:83ab43dc30e88ae469b9b273999c04f7");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_03.prefab:a5f1bad8391f7cd40bcf3faca7b79ec5");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Attack_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Attack_01.prefab:0618de36d93d420448287a75805971aa");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Attack_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Attack_02.prefab:18417e8d916916b47a9ccbf977535752");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Attack_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Attack_03.prefab:f2a3f88ee3b6a504298588ecf781ef2b");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01.prefab:c48d17a01cadc7b489c6579467417c9a");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01.prefab:34af598c27985724b9802106067a0a34");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03.prefab:33e15931d5d64304da221cc698836ce4");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01.prefab:c36afcabe96ea41478d70e30a2658447");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01.prefab:dda0e6287f7935b4a8caa601c158c4e4");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01.prefab:d35a4ad888b1f26499615770293635fa");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01.prefab:e0d61e2b1a6bd35438866e29f4633c6e");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01.prefab:57c84baaf74b7a7428725fc6e84c44a6");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01.prefab:202a4e9c6167d5e4abe01e90012e6570");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02.prefab:8ac74d3549f32814ba00125de843a822");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03.prefab:60782074f6475304ba1df74000699841");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01.prefab:ba0c9c5ea6581b245b25388afdad922d");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02.prefab:95f08ae6c3f395346adbf541617cb3f1");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03.prefab:387a96afb0b076a44bac24146d5edd31");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02.prefab:713f2a93a6594524496d9cfa17783000");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01.prefab:a7eb114b2260d174ebdd571ce1248ee5");

	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01.prefab:118c9eb2fb7d71f4da228f18cc5609b6");

	private static readonly AssetReference VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02.prefab:88d6046346107e84a805ed3667755afc");

	private static readonly AssetReference VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03 = new AssetReference("VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03.prefab:139a593d14a4b2b449e04e33de4b0a72");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02.prefab:fef0afedde775fb49ba5961be8c186c8");

	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03.prefab:b7f7552a80cdce043b9515514d9ba572");

	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02.prefab:7435c8fa6c168dd42abcb1c9c49f1fff");

	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01.prefab:809e5cbeff5ffd04ca2e31417ec96f8a");

	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01.prefab:be5cdbf6a229ffe4fbfeccd06a3c00ff");

	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02.prefab:483f8441b6adbae4ea4fc9d0cbed163c");

	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03.prefab:68363df5f2110ae42a646f7b6a79ca8f");

	private static readonly AssetReference VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01 = new AssetReference("VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01.prefab:a038cb757f2fb224aadf938dcb12531d");

	private static readonly AssetReference VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01 = new AssetReference("VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01.prefab:d307c74375749974abcef19e066fee10");

	private static readonly AssetReference VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02 = new AssetReference("VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02.prefab:078c477f5f264f12aaf341c9e250febc");

	private static readonly AssetReference VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02 = new AssetReference("VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02.prefab:ae190b055f911da4db4323c273a26c06");

	private static readonly AssetReference VO_Story_Minion_Nathanos_Male_Undead_Trigger_01 = new AssetReference("VO_Story_Minion_Nathanos_Male_Undead_Trigger_01.prefab:ca382153375ac274380ee30ed4b5e03c");

	protected Notification m_minionMoveTutorialNotification;

	protected bool m_shouldPlayMinionMoveTutorial = true;

	public static readonly AssetReference GennBrassRing = new AssetReference("Greymane_BrassRing_Quote.prefab:3e16b31a3b009ad468fa76462c5eda3b");

	public static readonly AssetReference SaurfangBrassRing = new AssetReference("Saurfang_BrassRing_Quote.prefab:727d1e09f5a40f649afa7ed2f3e70564");

	public static readonly AssetReference BfAJainaBrassRing = new AssetReference("JainaBfA_BrassRing_Quote.prefab:7c966a1e09f056e43b799d1e9f19da82");

	private List<string> m_BossSylvanasUsesHeroPowerLines = new List<string> { VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03 };

	private List<string> m_BossSaurfangUsesHeroPowerLines = new List<string> { VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03 };

	private List<string> m_BossSylvanasIdleLines = new List<string> { VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03 };

	private List<string> m_BossSylvanasIdleLinesCopy = new List<string> { VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03 };

	private List<string> m_BossSaurfangIdleLines = new List<string> { VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02 };

	private List<string> m_BossSaurfangIdleLinesCopy = new List<string> { VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02 };

	private List<string> m_AnduinAttacksLines = new List<string> { VO_Story_Hero_Anduin_Male_Human_Attack_01, VO_Story_Hero_Anduin_Male_Human_Attack_02, VO_Story_Hero_Anduin_Male_Human_Attack_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Anduin_08()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01, VO_Story_Hero_Anduin_Male_Human_Attack_01, VO_Story_Hero_Anduin_Male_Human_Attack_02, VO_Story_Hero_Anduin_Male_Human_Attack_03,
			VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02,
			VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01, VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01, VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02, VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01,
			VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02, VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02, VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01, VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01, VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02, VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02,
			VO_Story_Minion_Nathanos_Male_Undead_Trigger_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01;
		m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_DRG;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 113:
			yield return MissionPlayVOOnce(enemyActor, m_BossSylvanasUsesHeroPowerLines);
			break;
		case 115:
			yield return MissionPlayVOOnce(friendlyActor, m_AnduinAttacksLines);
			break;
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01);
			yield return MissionPlayVO(SaurfangBrassRing, VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(GennBrassRing, VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(BfAJainaBrassRing, VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 104:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			yield return MissionPlayVOOnce(GetEnemyActorByCardId("Story_05_Nathanos"), VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02);
			yield return MissionPlayVOOnce(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02);
			break;
		case 111:
			yield return MissionPlayVOOnce(GetEnemyActorByCardId("Story_05_Nathanos"), VO_Story_Minion_Nathanos_Male_Undead_Trigger_01);
			break;
		case 112:
			yield return MissionPlayVOOnce(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01);
			break;
		case 106:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 107:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03);
			break;
		case 114:
			yield return MissionPlayVOOnce(enemyActor, m_BossSaurfangUsesHeroPowerLines);
			break;
		case 108:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 109:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02);
			break;
		case 110:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02);
			break;
		case 515:
			if (GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId() == "Story_05_Saurfang")
			{
				yield return MissionPlayVO(enemyActor, VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01);
			}
			else
			{
				yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01);
			}
			break;
		case 228:
			GameState.Get().SetBusy(busy: true);
			ShowMinionMoveTutorial();
			yield return new WaitForSeconds(3f);
			HideNotification(m_minionMoveTutorialNotification);
			GameState.Get().SetBusy(busy: false);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (turn == 1)
		{
			yield return MissionPlayVO(GennBrassRing, VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02);
		}
	}

	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (m_enemySpeaking)
		{
			yield break;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			yield break;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		float thinkEmoteBossIdleChancePercentage = GetThinkEmoteBossIdleChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossIdleChancePercentage > num || (!m_Mission_FriendlyPlayIdleLines && m_Mission_EnemyPlayIdleLines))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (cardId == "Story_05_Saurfang")
			{
				string line = PopRandomLine(m_BossSaurfangIdleLinesCopy);
				if (m_BossSaurfangIdleLinesCopy.Count == 0)
				{
					m_BossSaurfangIdleLinesCopy = new List<string>(m_BossSaurfangIdleLines);
				}
				yield return MissionPlayVO(actor, line);
			}
			else if (cardId == "Story_05_Sylvanas")
			{
				string line2 = PopRandomLine(m_BossSylvanasIdleLinesCopy);
				if (m_BossSylvanasIdleLinesCopy.Count == 0)
				{
					m_BossSylvanasIdleLinesCopy = new List<string>(m_BossSylvanasIdleLines);
				}
				yield return MissionPlayVO(actor, line2);
			}
		}
		else if (m_Mission_FriendlyPlayIdleLines)
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType)
				.GetActiveAudioSource();
		}
	}

	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInOpponentPlay = GetLeftMostMinionInOpponentPlay();
		if (!(leftMostMinionInOpponentPlay == null))
		{
			Vector3 position = leftMostMinionInOpponentPlay.transform.position;
			Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2.5f) : new Vector3(position.x + 0.05f, position.y, position.z + 2.6f));
			string key = "BOH_ANDUIN_08";
			m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
			m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
		}
	}

	private IEnumerator ShowOrHideMoveMinionTutorial()
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		HideNotification(m_minionMoveTutorialNotification);
		while ((bool)InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		if ((GetLeftMostMinionInOpponentPlay() != null || (bool)InputManager.Get().GetHeldCard()) && m_shouldPlayMinionMoveTutorial)
		{
			ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(ShowOrHideMoveMinionTutorial());
		}
	}

	protected Card GetLeftMostMinionInFriendlyPlay()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	protected Card GetLeftMostMinionInOpponentPlay()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	protected void HideNotification(Notification notification, bool hideImmediately = false)
	{
		if (notification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(notification);
			}
			else
			{
				NotificationManager.Get().DestroyNotification(notification, 0f);
			}
		}
	}
}
