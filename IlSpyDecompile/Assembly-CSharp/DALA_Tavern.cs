using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DALA_Tavern : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_900h_Male_Human_Idle_01 = new AssetReference("VO_DALA_900h_Male_Human_Idle_01.prefab:dbb905dd9ace1b640902d08aa66182d1");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Idle_02 = new AssetReference("VO_DALA_900h_Male_Human_Idle_02.prefab:3cf9566a837907a4cb8c1d0e39362c9a");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Idle_03 = new AssetReference("VO_DALA_900h_Male_Human_Idle_03.prefab:bafa81859a8927049a740a1b588fd113");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_01 = new AssetReference("VO_DALA_900h_Male_Human_Intro_01.prefab:9449f0b4c1b7d4a4282e3fbe7a4d0066");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_02 = new AssetReference("VO_DALA_900h_Male_Human_Intro_02.prefab:b3d2d7fc342eaf7408dc9947c7cdaa53");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_03 = new AssetReference("VO_DALA_900h_Male_Human_Intro_03.prefab:10d8ce68d0c9a7b4db783bad35362bf1");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_04 = new AssetReference("VO_DALA_900h_Male_Human_Intro_04.prefab:76f5856098d17a24ba4dc1ce80716f7e");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Intro_05 = new AssetReference("VO_DALA_900h_Male_Human_Intro_05.prefab:3ced63167e3a40e4c88a46daeada9620");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroChu_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroChu_01.prefab:134d9cc8075977a47a22fa78abd264af");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroEudora_02 = new AssetReference("VO_DALA_900h_Male_Human_IntroEudora_02.prefab:9475700429a8c624ab33b9b9b3a49339");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroGeorge_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroGeorge_01.prefab:f150d96a7f351e747844e4a453d6a6cc");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroKriziki_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroKriziki_01.prefab:94f757d0bddea8b4cbd612783bbdf01f");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroOlBarkeye_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroOlBarkeye_01.prefab:1785f482c8f4ae34c87f1873f8ad9524");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroRakanishu_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroRakanishu_01.prefab:a74a7d981b5a08a499dc9dd48eec153d");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroSqueamlish_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroSqueamlish_01.prefab:15c66b1ecd6382d4691117223da64058");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroTekahn_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroTekahn_01.prefab:efa24b64d3be70a46b7197fd01fd6b41");

	private static readonly AssetReference VO_DALA_900h_Male_Human_IntroVessina_01 = new AssetReference("VO_DALA_900h_Male_Human_IntroVessina_01.prefab:34908c49072198c45ac3669014c81c45");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_01 = new AssetReference("VO_DALA_900h_Male_Human_Outro_01.prefab:500f3e8787af1c9489f10617f89f30c5");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_02 = new AssetReference("VO_DALA_900h_Male_Human_Outro_02.prefab:4da33b7a627c3794d922b351514801e3");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_03 = new AssetReference("VO_DALA_900h_Male_Human_Outro_03.prefab:6253c613cd1653f489a9f9b44a88a297");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_04 = new AssetReference("VO_DALA_900h_Male_Human_Outro_04.prefab:9b0f7be66c854c345bd2cbb5e439952c");

	private static readonly AssetReference VO_DALA_900h_Male_Human_Outro_05 = new AssetReference("VO_DALA_900h_Male_Human_Outro_05.prefab:4c204ac82e7ded14e9dae3940f9a8dd6");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerBrood_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerBrood_01.prefab:456313fb3b1046a47a8313a452bb6415");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerBrood_02 = new AssetReference("VO_DALA_900h_Male_Human_PlayerBrood_02.prefab:87bdd7dd4ed293b4aa951322c7fcd523");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerDismiss_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerDismiss_01.prefab:42f71bfccfd097d4dbc0fbe4f5058639");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerGoodFood_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerGoodFood_01.prefab:3c8d6db526d2ace43b737f26347a99f7");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerKindle_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerKindle_01.prefab:17ea2f82c22e95e4c912e07b77027d31");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRecruit_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRecruit_01.prefab:adfe05e034ce3d14bb4d6fa37d00a5a1");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01.prefab:67afc87e17c97a744b9c8b72b59043d6");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRightHandMan_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRightHandMan_01.prefab:0009033bd405db74cb50a95a226230d9");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01.prefab:2cac7a36c24b2404ca7c866a9a3c64a2");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTakeAChance_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTakeAChance_01.prefab:50ed7a2465d6a5f4790e3193342c6583");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTallTales_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTallTales_01.prefab:2180fc84653ed864ca5877374c146e17");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTellAStory_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTellAStory_01.prefab:f2d640c4155e1054ea9e00ec808adbba");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01.prefab:82ae89ac0b8edcf458093421a2eb54cc");

	private static readonly AssetReference VO_DALA_900h_Male_Human_PlayerYoureAllFired_01 = new AssetReference("VO_DALA_900h_Male_Human_PlayerYoureAllFired_01.prefab:dbc3069fd98fdbd41bb555cb7eb13f54");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Death_01 = new AssetReference("VO_DALA_901h_Male_Mech_Death_01.prefab:9fe4235dcd6f963479e28fa1625fe1af");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Idle_01 = new AssetReference("VO_DALA_901h_Male_Mech_Idle_01.prefab:158422eed9e14fe4c813b76ae7482f06");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Idle_02 = new AssetReference("VO_DALA_901h_Male_Mech_Idle_02.prefab:e5029757a8fbf964cbdf2c1ed6c2ec1a");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Idle_03 = new AssetReference("VO_DALA_901h_Male_Mech_Idle_03.prefab:1386f2b7fe103a049b2fa65db88c1ab5");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_01 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_01.prefab:5678393cc999bb142b38330e1ef94dbd");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_02 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_02.prefab:e323453876c6a0147a286b8cbb1d5930");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_03 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_03.prefab:a8f66126889493f4583a448c39694f00");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_04 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_04.prefab:434888aec49048149852ba382bda95c0");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_05 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_05.prefab:bafa9206894d60f4089b2d3638489549");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Intro_06 = new AssetReference("VO_DALA_901h_Male_Mech_Intro_06.prefab:74b222aa30ec2bf47b9bbd8bde74708f");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroChu_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroChu_01.prefab:ecd36b7d5b6b47248825c5034dd2901c");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroEudora_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroEudora_01.prefab:43ac2c5eeb8a5b648a1d6b15cd0b2dbe");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroGeorge_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroGeorge_01.prefab:94452ed7ae3d677479c85ca032b2dfa6");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroKriziki_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroKriziki_01.prefab:ced09968469c30049982057db41c0eaf");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroOlBarkeye_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroOlBarkeye_01.prefab:bc3fce38befd28a438363481245d13a2");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroRakanishu_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroRakanishu_01.prefab:521a52551a4f4b1459cbae7c8c0d306c");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroSqueamlish_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroSqueamlish_01.prefab:474837621fc9ed340ae1097f6b419b1e");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroTekahn_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroTekahn_01.prefab:c07db9a51b36f064894ab7e1cbcec09a");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_IntroVessina_01 = new AssetReference("VO_DALA_901h_Male_Mech_IntroVessina_01.prefab:d2da83f427149d440b6b395c49bbea19");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Outro_01 = new AssetReference("VO_DALA_901h_Male_Mech_Outro_01.prefab:806d5323d81326149bb3982dba145114");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Outro_02 = new AssetReference("VO_DALA_901h_Male_Mech_Outro_02.prefab:953b85d08c6945a479916401a33f3638");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_Outro_03 = new AssetReference("VO_DALA_901h_Male_Mech_Outro_03.prefab:ec27ba35d9003d74d843ef754ec9911d");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerBrood_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerBrood_01.prefab:511bf126da14f124e9610f4c68caa95d");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerBrood_02 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerBrood_02.prefab:5724a6477ee0f7147adaeca3b29fdbbb");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerDismiss_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerDismiss_01.prefab:f7e9245aedfa70e4c8fa7354ee29ad77");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerGoodFood_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerGoodFood_01.prefab:4cfcc28f63a76724cbcc0f309cc2ee31");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerKindle_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerKindle_01.prefab:af41bd38f2a8498438cdc2f504673e8e");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRecruit_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRecruit_01.prefab:efb4f80235646e74e974007b58e0369e");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01.prefab:5c6ae624000940648ac87c574ab5f963");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRightHandMan_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRightHandMan_01.prefab:61177d7c5129bac4f9243493a128a4a9");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRightHandMan_02 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRightHandMan_02.prefab:5e7dedc2026f31e47b7066f289acb4b2");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01.prefab:a35fb1d03e06217488f91b6ba6bec037");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTakeAChance_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTakeAChance_01.prefab:b3afb8901d6ae6c41aaab5124fbc0531");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTallTales_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTallTales_01.prefab:942537c8075b3a64ca5a4936f2a0bae1");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTellAStory_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTellAStory_01.prefab:cc13e37231d58694a86846223ec7b75c");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTheChallenge_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTheChallenge_01.prefab:becce32e9a04e0b4aaa28b9a71cffe50");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01.prefab:088705a54b564a64bb5f2b9fb8c9e3ac");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01 = new AssetReference("VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01.prefab:0acaaae59417b49468ac19b44c0c23cd");

	private static readonly AssetReference VO_DALA_901h_Male_Mech_TurnOne_01 = new AssetReference("VO_DALA_901h_Male_Mech_TurnOne_01.prefab:3c551789dd2875e48aac37174c6fc972");

	private static readonly string BARTENDERBOB_CARDID = "DALA_BOSS_99h";

	private static readonly string BARTENDOTRON_CARDID = "DALA_BOSS_98h";

	private static List<string> m_IdleLines = new List<string> { VO_DALA_900h_Male_Human_Idle_01, VO_DALA_900h_Male_Human_Idle_02, VO_DALA_900h_Male_Human_Idle_03 };

	private static List<string> m_Bartendortron_IdleLines = new List<string> { VO_DALA_901h_Male_Mech_Idle_01, VO_DALA_901h_Male_Mech_Idle_02, VO_DALA_901h_Male_Mech_Idle_03 };

	private static List<string> m_OutroLinesCopy = new List<string>();

	private static List<string> m_OutroLines = new List<string> { VO_DALA_900h_Male_Human_Outro_01, VO_DALA_900h_Male_Human_Outro_02, VO_DALA_900h_Male_Human_Outro_03, VO_DALA_900h_Male_Human_Outro_04, VO_DALA_900h_Male_Human_Outro_05 };

	private static List<string> m_Bartendortron_OutroLinesCopy = new List<string>();

	private static List<string> m_Bartendortron_OutroLines = new List<string> { VO_DALA_901h_Male_Mech_Outro_01, VO_DALA_901h_Male_Mech_Outro_02, VO_DALA_901h_Male_Mech_Outro_03 };

	private List<string> m_IntroLines = new List<string> { VO_DALA_900h_Male_Human_Intro_02, VO_DALA_900h_Male_Human_Intro_03, VO_DALA_900h_Male_Human_Intro_04 };

	private List<string> m_Bartendortron_IntroLines = new List<string> { VO_DALA_901h_Male_Mech_Intro_01, VO_DALA_901h_Male_Mech_Intro_02, VO_DALA_901h_Male_Mech_Intro_03, VO_DALA_901h_Male_Mech_Intro_04, VO_DALA_901h_Male_Mech_Intro_05, VO_DALA_901h_Male_Mech_Intro_06 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public bool m_thinkEmoteFirstRun = true;

	public DALA_Tavern()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(OnGameplaySceneLoaded);
	}

	~DALA_Tavern()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
		}
	}

	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
			ManaCrystalMgr.Get().SetEnemyManaCounterActive(active: false);
		}
	}

	protected override void HandleMainReadyStep()
	{
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("DALA_Tavern.HandleMainReadyStep(): GameState is null.");
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			Log.Gameplay.PrintError("DALA_Tavern.HandleMainReadyStep(): GameEntity is null.");
		}
		else if (gameEntity.GetTag(GAME_TAG.TURN) == 1)
		{
			GameState.Get().SetMulliganBusy(busy: true);
		}
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	public override bool DoAlternateMulliganIntro()
	{
		new TavernMulliganIntro().Show(GameEntity.Coroutines);
		return true;
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Network.Get().DisconnectFromGameServer();
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		SceneMgr.Get().SetNextMode(postGameSceneMode);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_900h_Male_Human_Idle_01, VO_DALA_900h_Male_Human_Idle_02, VO_DALA_900h_Male_Human_Idle_03, VO_DALA_900h_Male_Human_Intro_01, VO_DALA_900h_Male_Human_Intro_02, VO_DALA_900h_Male_Human_Intro_03, VO_DALA_900h_Male_Human_Intro_04, VO_DALA_900h_Male_Human_Intro_05, VO_DALA_900h_Male_Human_IntroChu_01, VO_DALA_900h_Male_Human_IntroEudora_02,
			VO_DALA_900h_Male_Human_IntroGeorge_01, VO_DALA_900h_Male_Human_IntroKriziki_01, VO_DALA_900h_Male_Human_IntroOlBarkeye_01, VO_DALA_900h_Male_Human_IntroRakanishu_01, VO_DALA_900h_Male_Human_IntroSqueamlish_01, VO_DALA_900h_Male_Human_IntroTekahn_01, VO_DALA_900h_Male_Human_IntroVessina_01, VO_DALA_900h_Male_Human_Outro_01, VO_DALA_900h_Male_Human_Outro_02, VO_DALA_900h_Male_Human_Outro_03,
			VO_DALA_900h_Male_Human_Outro_04, VO_DALA_900h_Male_Human_Outro_05, VO_DALA_900h_Male_Human_PlayerBrood_01, VO_DALA_900h_Male_Human_PlayerBrood_02, VO_DALA_900h_Male_Human_PlayerDismiss_01, VO_DALA_900h_Male_Human_PlayerGoodFood_01, VO_DALA_900h_Male_Human_PlayerKindle_01, VO_DALA_900h_Male_Human_PlayerRecruit_01, VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01, VO_DALA_900h_Male_Human_PlayerRightHandMan_01,
			VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01, VO_DALA_900h_Male_Human_PlayerTakeAChance_01, VO_DALA_900h_Male_Human_PlayerTallTales_01, VO_DALA_900h_Male_Human_PlayerTellAStory_01, VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01, VO_DALA_900h_Male_Human_PlayerYoureAllFired_01, VO_DALA_901h_Male_Mech_Death_01, VO_DALA_901h_Male_Mech_Idle_01, VO_DALA_901h_Male_Mech_Idle_02, VO_DALA_901h_Male_Mech_Idle_03,
			VO_DALA_901h_Male_Mech_Intro_01, VO_DALA_901h_Male_Mech_Intro_02, VO_DALA_901h_Male_Mech_Intro_03, VO_DALA_901h_Male_Mech_Intro_04, VO_DALA_901h_Male_Mech_Intro_05, VO_DALA_901h_Male_Mech_Intro_06, VO_DALA_901h_Male_Mech_IntroChu_01, VO_DALA_901h_Male_Mech_IntroEudora_01, VO_DALA_901h_Male_Mech_IntroGeorge_01, VO_DALA_901h_Male_Mech_IntroKriziki_01,
			VO_DALA_901h_Male_Mech_IntroOlBarkeye_01, VO_DALA_901h_Male_Mech_IntroRakanishu_01, VO_DALA_901h_Male_Mech_IntroSqueamlish_01, VO_DALA_901h_Male_Mech_IntroTekahn_01, VO_DALA_901h_Male_Mech_IntroVessina_01, VO_DALA_901h_Male_Mech_Outro_01, VO_DALA_901h_Male_Mech_Outro_02, VO_DALA_901h_Male_Mech_Outro_03, VO_DALA_901h_Male_Mech_PlayerBrood_01, VO_DALA_901h_Male_Mech_PlayerBrood_02,
			VO_DALA_901h_Male_Mech_PlayerDismiss_01, VO_DALA_901h_Male_Mech_PlayerGoodFood_01, VO_DALA_901h_Male_Mech_PlayerKindle_01, VO_DALA_901h_Male_Mech_PlayerRecruit_01, VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01, VO_DALA_901h_Male_Mech_PlayerRightHandMan_01, VO_DALA_901h_Male_Mech_PlayerRightHandMan_02, VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01, VO_DALA_901h_Male_Mech_PlayerTakeAChance_01, VO_DALA_901h_Male_Mech_PlayerTallTales_01,
			VO_DALA_901h_Male_Mech_PlayerTellAStory_01, VO_DALA_901h_Male_Mech_PlayerTheChallenge_01, VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01, VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01, VO_DALA_901h_Male_Mech_TurnOne_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
		VOPool value = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_DRUID_VO);
		m_VOPools.Add(909, value);
		VOPool value2 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_HUNTER_VO);
		m_VOPools.Add(910, value2);
		VOPool value3 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_MAGE_VO);
		m_VOPools.Add(911, value3);
		VOPool value4 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_PALADIN_VO);
		m_VOPools.Add(912, value4);
		VOPool value5 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_PRIEST_VO);
		m_VOPools.Add(913, value5);
		VOPool value6 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_ROGUE_VO);
		m_VOPools.Add(914, value6);
		VOPool value7 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_SHAMAN_VO);
		m_VOPools.Add(915, value7);
		VOPool value8 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_WARLOCK_VO);
		m_VOPools.Add(916, value8);
		VOPool value9 = new VOPool(new List<string> { VO_DALA_900h_Male_Human_Intro_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.OPPONENT_HERO, null, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_TAVERN_WARRIOR_VO);
		m_VOPools.Add(917, value9);
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_DALA_900h_Male_Human_Intro_05;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (cardId == BARTENDOTRON_CARDID)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_901h_Male_Mech_TurnOne_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
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
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		VOPool vOPool = null;
		int num = 0;
		string item = null;
		string cardId2 = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (cardId2 == BARTENDERBOB_CARDID)
		{
			switch (missionEvent)
			{
			case 101:
			{
				if (cardId == "DALA_Squeamlish")
				{
					num = 909;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroSqueamlish_01;
				}
				if (cardId == "DALA_Barkeye")
				{
					num = 910;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroOlBarkeye_01;
				}
				if (cardId == "DALA_Rakanishu")
				{
					num = 911;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroRakanishu_01;
				}
				if (cardId == "DALA_George")
				{
					num = 912;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroGeorge_01;
				}
				if (cardId == "DALA_Kriziki")
				{
					num = 913;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroKriziki_01;
				}
				if (cardId == "DALA_Eudora")
				{
					num = 913;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroEudora_02;
				}
				if (cardId == "DALA_Vessina")
				{
					num = 915;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroVessina_01;
				}
				if (cardId == "DALA_Tekahn")
				{
					num = 916;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroTekahn_01;
				}
				if (cardId == "DALA_Chu")
				{
					num = 917;
					vOPool = m_VOPools[num];
					item = VO_DALA_900h_Male_Human_IntroChu_01;
				}
				GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.ADVENTURE_DATA_CLIENT_DALARAN, vOPool.m_oncePerAccountGameSaveSubkey, out long value);
				if (value > 0)
				{
					for (int i = 0; i < 3; i++)
					{
						m_IntroLines.Add(item);
					}
					string soundPath2 = PopRandomLine(m_IntroLines);
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(soundPath2, Notification.SpeechBubbleDirection.TopRight, actor));
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(num));
				}
				break;
			}
			case 102:
			{
				GameState.Get().SetBusy(busy: true);
				if (m_OutroLinesCopy.Count == 0)
				{
					m_OutroLinesCopy = new List<string>(m_OutroLines);
				}
				string soundPath = PopRandomLine(m_OutroLinesCopy);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(soundPath, Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			}
			default:
				yield return base.HandleMissionEventWithTiming(missionEvent);
				break;
			}
		}
		else
		{
			if (!(cardId2 == BARTENDOTRON_CARDID))
			{
				yield break;
			}
			switch (missionEvent)
			{
			case 101:
			{
				if (cardId == "DALA_Squeamlish")
				{
					num = 909;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroSqueamlish_01;
				}
				if (cardId == "DALA_Barkeye")
				{
					num = 910;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroOlBarkeye_01;
				}
				if (cardId == "DALA_Rakanishu")
				{
					num = 911;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroRakanishu_01;
				}
				if (cardId == "DALA_George")
				{
					num = 912;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroGeorge_01;
				}
				if (cardId == "DALA_Kriziki")
				{
					num = 913;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroKriziki_01;
				}
				if (cardId == "DALA_Eudora")
				{
					num = 913;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroEudora_01;
				}
				if (cardId == "DALA_Vessina")
				{
					num = 915;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroVessina_01;
				}
				if (cardId == "DALA_Tekahn")
				{
					num = 916;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroTekahn_01;
				}
				if (cardId == "DALA_Chu")
				{
					num = 917;
					_ = m_VOPools[num];
					item = VO_DALA_901h_Male_Mech_IntroChu_01;
				}
				for (int j = 0; j < 3; j++)
				{
					m_Bartendortron_IntroLines.Add(item);
				}
				string soundPath4 = PopRandomLine(m_Bartendortron_IntroLines);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(soundPath4, Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
			case 102:
			{
				GameState.Get().SetBusy(busy: true);
				if (m_Bartendortron_OutroLinesCopy.Count == 0)
				{
					m_Bartendortron_OutroLinesCopy = new List<string>(m_Bartendortron_OutroLines);
				}
				string soundPath3 = PopRandomLine(m_Bartendortron_OutroLinesCopy);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(soundPath3, Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
				break;
			}
			default:
				yield return base.HandleMissionEventWithTiming(missionEvent);
				break;
			}
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string enemyHeroCardID = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (enemyHeroCardID == BARTENDERBOB_CARDID)
		{
			switch (cardId)
			{
			case "DALA_902":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerDismiss_01);
				break;
			case "DALA_904":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerGoodFood_01);
				break;
			case "DALA_911":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerKindle_01);
				break;
			case "DALA_901":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerRecruit_01);
				break;
			case "DALA_907":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerRecruitAVeteran_01);
				break;
			case "DALA_905":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerRightHandMan_01);
				break;
			case "DALA_906":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerRoundOfDrinks_01);
				break;
			case "DALA_903":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerTakeAChance_01);
				break;
			case "DALA_913":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerTallTales_01);
				break;
			case "DALA_908":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerTellAStory_01);
				break;
			case "DALA_910":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerTheGangsAllHere_01);
				break;
			case "DALA_909":
				yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerYoureAllFired_01);
				break;
			case "DALA_912":
				if (Random.Range(1, 11) == 1)
				{
					yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerBrood_02);
				}
				else
				{
					yield return PlayLineOnlyOnce(actor, VO_DALA_900h_Male_Human_PlayerBrood_01);
				}
				break;
			}
		}
		else
		{
			if (!(enemyHeroCardID == BARTENDOTRON_CARDID))
			{
				yield break;
			}
			switch (cardId)
			{
			case "DALA_902":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerDismiss_01);
				break;
			case "DALA_904":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerGoodFood_01);
				break;
			case "DALA_911":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerKindle_01);
				break;
			case "DALA_901":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerRecruit_01);
				break;
			case "DALA_907":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerRecruitAVeteran_01);
				break;
			case "DALA_905":
				if (Random.Range(1, 3) == 1)
				{
					yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerRightHandMan_01);
				}
				else
				{
					yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerRightHandMan_02);
				}
				break;
			case "DALA_906":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerRoundOfDrinks_01);
				break;
			case "DALA_903":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerTakeAChance_01);
				break;
			case "DALA_913":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerTallTales_01);
				break;
			case "DALA_908":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerTellAStory_01);
				break;
			case "DALA_910":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerTheGangsAllHere_01);
				break;
			case "DALA_909":
				yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerYoureAllFired_01);
				break;
			case "DALA_912":
				if (Random.Range(1, 11) == 1)
				{
					yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerBrood_02);
				}
				else
				{
					yield return PlayLineOnlyOnce(actor, VO_DALA_901h_Male_Mech_PlayerBrood_01);
				}
				break;
			}
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (m_thinkEmoteFirstRun)
		{
			if (cardId == BARTENDOTRON_CARDID)
			{
				m_BossIdleLines = new List<string>(m_Bartendortron_IdleLines);
				m_BossIdleLinesCopy = new List<string>(m_Bartendortron_IdleLines);
			}
			else
			{
				m_BossIdleLines = new List<string>(m_IdleLines);
				m_BossIdleLinesCopy = new List<string>(m_IdleLines);
			}
			m_thinkEmoteFirstRun = false;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && m_BossIdleLines != null && m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string line = PopRandomLine(m_BossIdleLinesCopy);
			if (m_BossIdleLinesCopy.Count == 0)
			{
				if (cardId == BARTENDOTRON_CARDID)
				{
					m_BossIdleLinesCopy = new List<string>(m_Bartendortron_IdleLines);
				}
				else
				{
					m_BossIdleLinesCopy = new List<string>(m_IdleLines);
				}
			}
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			return;
		}
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
			.PlayEmote(emoteType);
	}

	public override string[] GetOverrideBoardClickSounds()
	{
		return new string[4] { "TavernBoard_WoodPoke_Creak_1.prefab:ab4c166deba73ef4c80d46dca53aaf14", "TavernBoard_WoodPoke_Creak_2.prefab:272821729af020e46acb68d4e3f29e8e", "TavernBoard_WoodPoke_Creak_3.prefab:80220d29df0ae4849be9a9029b33088a", "TavernBoard_WoodPoke_Creak_4.prefab:0a478645bd054864c9ac52804f3be118" };
	}
}
