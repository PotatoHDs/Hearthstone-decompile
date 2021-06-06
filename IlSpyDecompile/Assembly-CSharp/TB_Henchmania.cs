using System.Collections;
using UnityEngine;

public class TB_Henchmania : MissionEntity
{
	private enum VICTOR
	{
		PLAYERLOST,
		PLAYERWIN,
		ERROR
	}

	private enum BOSS
	{
		BOOM,
		HAGATHA,
		TOGWAGGLE,
		LAZUL
	}

	private VICTOR matchResult;

	private Notification StartPopup;

	private int shouldShowVictory;

	private int shouldShowIntro;

	private int isPlayerActive;

	private int currentSelectedBoss;

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_ChooseBoss_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_ChooseBoss_01:92ef377b2a9229949a68044f60194a99");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_FightBegins_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_FightBegins_01:10a8a2a12a3d6954882ea96d0e02708b");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_HalfHealth_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_HalfHealth_01:e0241fb915083b24e91bbd7945911eb7");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_Loss_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_Loss_01:0b1fb3c932b7f6a4eac2b69971591681");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_RejectBoss_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_RejectBoss_01:be9d6e2dc6f12224b98dea7432b418bf");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_T1End_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_T1End_01:f95bc84b6be39124386249b008cca987");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_T2End_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_T2End_01:aa5d0d2be4820e94caf11bcf4f863baf");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_HM_Victory_01 = new AssetReference("VO_DrBoom_Male_Goblin_HM_Victory_01:0ae5db5ecc98fd3479aa6c0e214f9e25");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_ChooseBoss_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_ChooseBoss_01:1a3ede76502370448bcbb0f55021c0aa");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_FightBegins_02 = new AssetReference("VO_Hagatha_Female_Orc_HM_FightBegins_02:05349b4988034804687636f4b5bbcccc");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_HalfHealth_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_HalfHealth_01:04d78ee0f6475aa418f9b4ae1433b2d5");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_Loss_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_Loss_01:c9db327aabb710844b2392268e074e40");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_RejectBoss_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_RejectBoss_01:bd611323cc855bf48a3a59345806b1d1");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_T1End_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_T1End_01:22787559cc2b31f46a31aad519378170");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_T2End_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_T2End_01:e2bcc4eba0b883f43a2f18f207045a33");

	private static readonly AssetReference VO_Hagatha_Female_Orc_HM_Victory_01 = new AssetReference("VO_Hagatha_Female_Orc_HM_Victory_01:262178b640b15264a941504759e19e50");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_ChooseBoss_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_ChooseBoss_01:893315ab675d7ef4ba71ee9ba93cf37b");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_FightBegins_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_FightBegins_01:7929131f3bb0c00408780e7a1309ea5a");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_HalfHealth_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_HalfHealth_01:cef25d4d7a371f047a198c9da03b794d");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_Loss_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_Loss_01:2edeb04408e19814fbcfbec666f8a5ad");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_RejectBoss_02 = new AssetReference("VO_MadameLazul_Female_Troll_HM_RejectBoss_02:ec8aa2b89e5a4c743b19b8d0f68d270d");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_T1End_02 = new AssetReference("VO_MadameLazul_Female_Troll_HM_T1End_02:24bedd607de58bd46a72dfc8a8ac5d36");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_T2End_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_T2End_01:ab1cfda93ef83a249baa60d59be21162");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_HM_Victory_01 = new AssetReference("VO_MadameLazul_Female_Troll_HM_Victory_01:186d71a5b0da6ec4f82adbed3e7a658d");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01:b2430d97d1938654e9f6e6dc2fe2d4bb");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_FightBegins_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_FightBegins_01:a36fd84a374fce44380c00d237a3f3dd");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_HalfHealth_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_HalfHealth_01:51a2331053b354941a176c275fa33a6e");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_Loss_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_Loss_01:ed6c79dcf1bf32641ae434b83954c519");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_RejectBoss_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_RejectBoss_01:1f8dc750ed805974eb91b7ef2daf6873");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_T1End_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_T1End_01:d507d155b3604174cbf8fa865cb9fcf3");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_T2End_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_T2End_01:d78708c07cd1c4f4da11e23605055abd");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_HM_Victory_01 = new AssetReference("VO_Togwaggle_Male_Kobold_HM_Victory_01:81e2b781d0274db498488a86cb26ad11");

	private static readonly Vector3 LEFT_OF_FRIENDLY_HERO = new Vector3(-1f, 0f, 1f);

	private static readonly Vector3 LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -2.8f);

	private static readonly Vector3 RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -2.8f);

	private Player friendlySidePlayer;

	public override void PreloadAssets()
	{
		PreloadSound(VO_DrBoom_Male_Goblin_HM_ChooseBoss_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_FightBegins_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_HalfHealth_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_Loss_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_RejectBoss_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_T1End_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_T2End_01);
		PreloadSound(VO_DrBoom_Male_Goblin_HM_Victory_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_ChooseBoss_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_FightBegins_02);
		PreloadSound(VO_Hagatha_Female_Orc_HM_HalfHealth_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_Loss_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_RejectBoss_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_T1End_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_T2End_01);
		PreloadSound(VO_Hagatha_Female_Orc_HM_Victory_01);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_ChooseBoss_01);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_FightBegins_01);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_HalfHealth_01);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_Loss_01);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_RejectBoss_02);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_T1End_02);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_T2End_01);
		PreloadSound(VO_MadameLazul_Female_Troll_HM_Victory_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_FightBegins_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_HalfHealth_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_Loss_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_RejectBoss_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_T1End_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_T2End_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_HM_Victory_01);
	}

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		isPlayerActive = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 10:
			if (isPlayerActive == 1)
			{
				yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_ChooseBoss_01);
				yield return new WaitForSeconds(1f);
				yield return PlayBossLineRight(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_RejectBoss_02);
			}
			break;
		case 11:
			if (isPlayerActive == 1)
			{
				yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_ChooseBoss_01);
				yield return new WaitForSeconds(1f);
				yield return PlayBossLineRight(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_RejectBoss_01);
			}
			break;
		case 12:
			if (isPlayerActive == 1)
			{
				yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_ChooseBoss_01);
				yield return new WaitForSeconds(1f);
				yield return PlayBossLineRight(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_RejectBoss_01);
			}
			break;
		case 13:
			if (isPlayerActive == 1)
			{
				yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_ChooseBoss_01);
				yield return new WaitForSeconds(1f);
				yield return PlayBossLineRight(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_RejectBoss_01);
			}
			break;
		case 14:
			if (isPlayerActive == 1)
			{
				currentSelectedBoss = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				yield return new WaitForSeconds(3f);
				if (currentSelectedBoss == 1)
				{
					yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_FightBegins_02);
				}
				if (currentSelectedBoss == 2)
				{
					yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_FightBegins_01);
				}
				if (currentSelectedBoss == 3)
				{
					yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_FightBegins_01);
				}
				if (currentSelectedBoss == 4)
				{
					yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_FightBegins_01);
				}
			}
			break;
		case 15:
			if (isPlayerActive == 1)
			{
				yield return new WaitForSeconds(3f);
				currentSelectedBoss = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				if (currentSelectedBoss == 1)
				{
					yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_T1End_01);
				}
				if (currentSelectedBoss == 2)
				{
					yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_T1End_01);
				}
				if (currentSelectedBoss == 3)
				{
					yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_T1End_01);
				}
				if (currentSelectedBoss == 4)
				{
					yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_T1End_02);
				}
			}
			break;
		case 16:
			if (isPlayerActive == 1)
			{
				currentSelectedBoss = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				yield return new WaitForSeconds(3f);
				if (currentSelectedBoss == 1)
				{
					yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_T2End_01);
				}
				if (currentSelectedBoss == 2)
				{
					yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_T2End_01);
				}
				if (currentSelectedBoss == 3)
				{
					yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_T2End_01);
				}
				if (currentSelectedBoss == 4)
				{
					yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_T2End_01);
				}
			}
			break;
		case 17:
			if (isPlayerActive == 1)
			{
				currentSelectedBoss = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
				yield return new WaitForSeconds(3f);
				if (currentSelectedBoss == 1)
				{
					yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_HalfHealth_01);
				}
				if (currentSelectedBoss == 2)
				{
					yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_HalfHealth_01);
				}
				if (currentSelectedBoss == 3)
				{
					yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_HalfHealth_01);
				}
				if (currentSelectedBoss == 4)
				{
					yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_HalfHealth_01);
				}
			}
			break;
		}
	}

	private IEnumerator PlayBossLine(string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.BottomLeft;
		yield return PlayMissionFlavorLine("Rastakhan_BrassRing_Quote:179bfad1464576448aeabfe5c3eff601", line, LEFT_OF_FRIENDLY_HERO, direction, 2.5f, persistCharacter);
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			matchResult = VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			Debug.Log("Made it to Playstate:Lost");
			matchResult = VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			matchResult = VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	private IEnumerator PlayBossLineLeft(BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopLeft;
		switch (boss)
		{
		case BOSS.BOOM:
			yield return PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.HAGATHA:
			yield return PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.TOGWAGGLE:
			yield return PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.LAZUL:
			yield return PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
	}

	private IEnumerator PlayBossLineRight(BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
		switch (boss)
		{
		case BOSS.BOOM:
			yield return PlayMissionFlavorLine("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.HAGATHA:
			yield return PlayMissionFlavorLine("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.TOGWAGGLE:
			yield return PlayMissionFlavorLine("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		case BOSS.LAZUL:
			yield return PlayMissionFlavorLine("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		currentSelectedBoss = friendlySidePlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(busy: false);
		switch (matchResult)
		{
		case VICTOR.PLAYERLOST:
			GameState.Get().SetBusy(busy: true);
			if (currentSelectedBoss == 1)
			{
				yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_Loss_01);
			}
			if (currentSelectedBoss == 2)
			{
				yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_Loss_01);
			}
			if (currentSelectedBoss == 3)
			{
				yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_Loss_01);
			}
			if (currentSelectedBoss == 4)
			{
				yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_Loss_01);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case VICTOR.PLAYERWIN:
			if (currentSelectedBoss == 1)
			{
				yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_HM_Victory_01);
			}
			if (currentSelectedBoss == 2)
			{
				yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_HM_Victory_01);
			}
			if (currentSelectedBoss == 3)
			{
				yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_HM_Victory_01);
			}
			if (currentSelectedBoss == 4)
			{
				yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_HM_Victory_01);
			}
			break;
		}
	}
}
