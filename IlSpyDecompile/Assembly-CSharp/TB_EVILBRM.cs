using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_EVILBRM : MissionEntity
{
	public struct PopupMessage
	{
		public string Message;

		public float Delay;
	}

	private enum BOSS
	{
		BOOM,
		HAGATHA,
		TOGWAGGLE,
		LAZUL,
		RAFAAM
	}

	private enum VICTOR
	{
		PLAYERLOST,
		PLAYERWIN,
		ERROR
	}

	private static readonly AssetReference VO_Rafaam_Male_Ethereal_BRM_Start_01 = new AssetReference("VO_Rafaam_Male_Ethereal_BRM_Start_01:840b30444d3dcac419d14454b31ef534");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_BRM_T1End_01 = new AssetReference("VO_DrBoom_Male_Goblin_BRM_T1End_01:ba94239568242a04db30dcf8fc6be837");

	private static readonly AssetReference VO_DrBoom_Male_Goblin_BRM_Victory_01 = new AssetReference("VO_DrBoom_Male_Goblin_BRM_Victory_01:0b3187eb9664d9f4ca73ff246baa6463");

	private static readonly AssetReference VO_Hagatha_Female_Orc_BRM_Victory_01 = new AssetReference("VO_Hagatha_Female_Orc_BRM_Victory_01:2832c50d764531d4794545901326adac");

	private static readonly AssetReference VO_MadameLazul_Female_Troll_BRM_Victory_01 = new AssetReference("VO_MadameLazul_Female_Troll_BRM_Victory_01:1d1c9015c5e6cd34892e179df49768e2");

	private static readonly AssetReference VO_Togwaggle_Male_Kobold_BRM_Victory_01 = new AssetReference("VO_Togwaggle_Male_Kobold_BRM_Victory_01:849eb420629bf2a41aaf192489366f8d");

	private static readonly AssetReference VO_Rafaam_Male_Ethereal_HM_Victory_01 = new AssetReference("VO_Rafaam_Male_Ethereal_HM_Victory_01:04141be712eae134b85f869c50056efa");

	private Notification m_popup;

	private float popupScale = 1.4f;

	private static readonly Dictionary<int, PopupMessage> popupMsgs;

	private static readonly Vector3 LEFT_OF_ENEMY_HERO;

	private static readonly Vector3 RIGHT_OF_ENEMY_HERO;

	private VICTOR matchResult;

	private int currentSelectedBoss;

	private int isOnRagnaros;

	private Player enemyPlayer;

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
		PreloadSound(VO_Rafaam_Male_Ethereal_BRM_Start_01);
		PreloadSound(VO_DrBoom_Male_Goblin_BRM_T1End_01);
		PreloadSound(VO_DrBoom_Male_Goblin_BRM_Victory_01);
		PreloadSound(VO_Hagatha_Female_Orc_BRM_Victory_01);
		PreloadSound(VO_MadameLazul_Female_Troll_BRM_Victory_01);
		PreloadSound(VO_Togwaggle_Male_Kobold_BRM_Victory_01);
		PreloadSound(VO_Rafaam_Male_Ethereal_HM_Victory_01);
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 1000)
		{
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			string msgString;
			if (tag == 0)
			{
				msgString = GameStrings.Get(popupMsgs[2000].Message);
			}
			else
			{
				string text = "";
				string text2 = "";
				string text3 = "";
				int num = tag / 3600;
				int num2 = tag % 3600 / 60;
				int num3 = tag % 60;
				if (num < 10)
				{
					text = "0";
				}
				if (num2 < 10)
				{
					text2 = "0";
				}
				if (num3 < 10)
				{
					text3 = "0";
				}
				msgString = GameStrings.Get(popupMsgs[missionEvent].Message) + "\n" + text + num + ":" + text2 + num2 + ":" + text3 + num3;
				popupScale = 1.7f;
			}
			Vector3 popUpPos = default(Vector3);
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
			}
			else
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? (-40f) : (-40f));
			}
			yield return new WaitForSeconds(4f);
			yield return ShowPopup(msgString, popupMsgs[missionEvent].Delay, popUpPos, popupScale);
			popUpPos = default(Vector3);
		}
		if (missionEvent == 10)
		{
			yield return new WaitForSeconds(1f);
			yield return PlayBossLineLeft(BOSS.RAFAAM, VO_Rafaam_Male_Ethereal_BRM_Start_01);
			yield return new WaitForSeconds(0.5f);
			yield return PlayBossLineRight(BOSS.BOOM, VO_DrBoom_Male_Goblin_BRM_T1End_01);
		}
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
		case BOSS.RAFAAM:
			yield return PlayMissionFlavorLine("Rafaam_popup_BrassRing_Quote:187724fae6d64cf49acf11aa53ca2087", line, LEFT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
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
		case BOSS.RAFAAM:
			yield return PlayMissionFlavorLine("Rafaam_popup_BrassRing_Quote:187724fae6d64cf49acf11aa53ca2087", line, RIGHT_OF_ENEMY_HERO, direction, 2.5f, persistCharacter);
			break;
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			matchResult = VICTOR.PLAYERWIN;
			break;
		case TAG_PLAYSTATE.LOST:
			matchResult = VICTOR.PLAYERLOST;
			break;
		case TAG_PLAYSTATE.TIED:
			matchResult = VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		enemyPlayer = GameState.Get().GetOpposingSidePlayer();
		currentSelectedBoss = enemyPlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
		isOnRagnaros = enemyPlayer.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		Debug.Log("isRagnaros returns " + isOnRagnaros);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(busy: false);
		if (isOnRagnaros != 1)
		{
			yield break;
		}
		switch (matchResult)
		{
		case VICTOR.PLAYERWIN:
			if (currentSelectedBoss == 1)
			{
				yield return new WaitForSeconds(1.5f);
				yield return PlayBossLineLeft(BOSS.BOOM, VO_DrBoom_Male_Goblin_BRM_Victory_01);
			}
			if (currentSelectedBoss == 2)
			{
				yield return new WaitForSeconds(1.5f);
				yield return PlayBossLineLeft(BOSS.HAGATHA, VO_Hagatha_Female_Orc_BRM_Victory_01);
			}
			if (currentSelectedBoss == 3)
			{
				yield return new WaitForSeconds(1.5f);
				yield return PlayBossLineLeft(BOSS.LAZUL, VO_MadameLazul_Female_Troll_BRM_Victory_01);
			}
			if (currentSelectedBoss == 4)
			{
				yield return new WaitForSeconds(1.5f);
				yield return PlayBossLineLeft(BOSS.TOGWAGGLE, VO_Togwaggle_Male_Kobold_BRM_Victory_01);
			}
			if (currentSelectedBoss == 5)
			{
				yield return new WaitForSeconds(1.5f);
				yield return PlayBossLineLeft(BOSS.RAFAAM, VO_Rafaam_Male_Ethereal_HM_Victory_01);
			}
			break;
		}
	}

	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos, float popupScale)
	{
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(stringID), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		yield return new WaitForSeconds(0f);
	}

	static TB_EVILBRM()
	{
		Dictionary<int, PopupMessage> dictionary = new Dictionary<int, PopupMessage>();
		PopupMessage value = new PopupMessage
		{
			Message = "TB_EVILBRM_CURRENT_BEST_SCORE",
			Delay = 5f
		};
		dictionary.Add(1000, value);
		value = new PopupMessage
		{
			Message = "TB_EVILBRM_NEW_BEST_SCORE",
			Delay = 5f
		};
		dictionary.Add(2000, value);
		popupMsgs = dictionary;
		LEFT_OF_ENEMY_HERO = new Vector3(-1f, 0f, -1.8f);
		RIGHT_OF_ENEMY_HERO = new Vector3(-6f, 0f, -1.8f);
	}
}
