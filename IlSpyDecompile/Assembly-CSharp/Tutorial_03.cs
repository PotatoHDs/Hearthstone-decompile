using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_03 : TutorialEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private int numTauntGorillasPlayed;

	private bool enemyPlayedBigBrother;

	private bool needATaunterVOPlayed;

	private bool monkeyLinePlayedOnce;

	private bool defenselessVoPlayed;

	private bool seenTheBrother;

	private bool warnedAgainstAttackingGorilla;

	private bool victory;

	private Notification thatsABadPlayPopup;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN,
				false
			},
			{
				GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN,
				true
			},
			{
				GameEntityOption.SHOW_CRAZY_KEYWORD_TOOLTIP,
				true
			}
		};
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public Tutorial_03()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_TUTORIAL_03_JAINA_17_33.prefab:b96f78fff7ab94a42894930d51bd45bd");
		PreloadSound("VO_TUTORIAL_03_JAINA_18_34.prefab:b9a2a99d30893804790829b3ceabc9b8");
		PreloadSound("VO_TUTORIAL_03_JAINA_01_24.prefab:b9515cf173f876a458202c6092055709");
		PreloadSound("VO_TUTORIAL_03_JAINA_05_25.prefab:38e2d64610e757245877b8f8e2f68584");
		PreloadSound("VO_TUTORIAL_03_JAINA_07_26.prefab:e93d67263c3d99740aaa4acc4b7d87a4");
		PreloadSound("VO_TUTORIAL_03_JAINA_12_28.prefab:d30f0c732643aa74aba9ec4cf2c2e6dd");
		PreloadSound("VO_TUTORIAL_03_JAINA_13_29.prefab:efca9c5305a101e4d968d08e58061cda");
		PreloadSound("VO_TUTORIAL_03_JAINA_16_32.prefab:b05bea699e2f897478c81a485a7d1a1a");
		PreloadSound("VO_TUTORIAL_03_JAINA_14_30.prefab:0787881bd0a25a342ba06f566f16051b");
		PreloadSound("VO_TUTORIAL_03_JAINA_15_31.prefab:4e0f1eaa19e283a4cac77219e1f10fe3");
		PreloadSound("VO_TUTORIAL_03_JAINA_20_36.prefab:79671f155307aa24a89b0581e4c5c4b2");
		PreloadSound("VO_TUTORIAL_03_MUKLA_01_01.prefab:3f6638f7f0d96da4ca422a290035c97a");
		PreloadSound("VO_TUTORIAL_03_MUKLA_03_03.prefab:5018131495f68c247bac073424fab700");
		PreloadSound("VO_TUTORIAL_03_MUKLA_04_04.prefab:0e4a4c87ac994c845b06230a34b168f9");
		PreloadSound("VO_TUTORIAL_03_MUKLA_05_05.prefab:8c12c75976cdfe044ad8ff3dd14ae5b8");
		PreloadSound("VO_TUTORIAL_03_MUKLA_06_06.prefab:8ed0c9ff5d18314469821d5be3d62dc7");
		PreloadSound("VO_TUTORIAL_03_MUKLA_07_07.prefab:c7b7dc3589c10c94bb3b9c0c6c08e3f6");
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			SetTutorialProgress(TutorialProgress.MUKLA_COMPLETE);
			PlaySound("VO_TUTORIAL_03_MUKLA_07_07.prefab:c7b7dc3589c10c94bb3b9c0c6c08e3f6");
			break;
		case TAG_PLAYSTATE.TIED:
			PlaySound("VO_TUTORIAL_03_MUKLA_07_07.prefab:c7b7dc3589c10c94bb3b9c0c6c08e3f6");
			break;
		case TAG_PLAYSTATE.LOST:
			SetTutorialLostProgress(TutorialProgress.MUKLA_COMPLETE);
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (enemyPlayedBigBrother)
		{
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				if (GameState.Get().GetNumEnemyMinionsInPlay(includeUntouchables: false) > 0)
				{
					if (!needATaunterVOPlayed)
					{
						if (!GameState.Get().GetFriendlySidePlayer().HasATauntMinion())
						{
							needATaunterVOPlayed = true;
							Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_17_33.prefab:b96f78fff7ab94a42894930d51bd45bd", "TUTORIAL03_JAINA_17", Notification.SpeechBubbleDirection.BottomLeft, actor));
						}
						yield break;
					}
					if (!defenselessVoPlayed)
					{
						bool flag = true;
						foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
							.GetCards())
						{
							if (card.GetEntity().HasTaunt())
							{
								flag = false;
							}
						}
						if (flag)
						{
							defenselessVoPlayed = true;
							Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_18_34.prefab:b9a2a99d30893804790829b3ceabc9b8", "TUTORIAL03_JAINA_18", Notification.SpeechBubbleDirection.BottomLeft, actor));
						}
					}
				}
			}
			else if (!seenTheBrother)
			{
				Gameplay.Get().StartCoroutine(GetReadyForBro());
			}
		}
		switch (turn)
		{
		case 1:
			if (!DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_01_24.prefab:b9515cf173f876a458202c6092055709", "TUTORIAL03_JAINA_01", Notification.SpeechBubbleDirection.BottomLeft, actor));
			}
			break;
		case 5:
			if (!DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_05_25.prefab:38e2d64610e757245877b8f8e2f68584", "TUTORIAL03_JAINA_05", Notification.SpeechBubbleDirection.BottomLeft, actor));
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_03_03.prefab:5018131495f68c247bac073424fab700", "TUTORIAL03_MUKLA_03", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			break;
		case 6:
			if (!DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_04_04.prefab:0e4a4c87ac994c845b06230a34b168f9", "TUTORIAL03_MUKLA_04", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 9:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_07_26.prefab:e93d67263c3d99740aaa4acc4b7d87a4", "TUTORIAL03_JAINA_07", Notification.SpeechBubbleDirection.BottomLeft, actor));
			break;
		case 14:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_05_05.prefab:8c12c75976cdfe044ad8ff3dd14ae5b8", "TUTORIAL03_MUKLA_05", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		}
	}

	private IEnumerator GetReadyForBro()
	{
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		seenTheBrother = true;
		GameState.Get().SetBusy(busy: true);
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_12_28.prefab:d30f0c732643aa74aba9ec4cf2c2e6dd", "TUTORIAL03_JAINA_12", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
		GameState.Get().SetBusy(busy: false);
		yield return new WaitForSeconds(3.2f);
		Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_13_29.prefab:efca9c5305a101e4d968d08e58061cda", "TUTORIAL03_JAINA_13", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			HandleGameStartEvent();
			AssetLoader.Get().InstantiatePrefab("TutorialKeywordManager.prefab:c1276fda3e1df594990295731f80c9c2", AssetLoadingOptions.IgnorePrefabPosition);
			break;
		case 4:
			numTauntGorillasPlayed++;
			if (numTauntGorillasPlayed == 1)
			{
				Gameplay.Get().StartCoroutine(ShowTauntPopup());
			}
			else if (numTauntGorillasPlayed == 2 && !DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_06_06.prefab:8ed0c9ff5d18314469821d5be3d62dc7", "TUTORIAL03_MUKLA_06", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 10:
			enemyPlayedBigBrother = true;
			Gameplay.Get().StartCoroutine(AdjustBigBrotherTransform());
			if (!GameState.Get().IsFriendlySidePlayerTurn())
			{
				Gameplay.Get().StartCoroutine(GetReadyForBro());
			}
			break;
		case 11:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_16_32.prefab:b05bea699e2f897478c81a485a7d1a1a", "TUTORIAL03_JAINA_16", Notification.SpeechBubbleDirection.BottomLeft, actor));
			break;
		case 12:
			if (!monkeyLinePlayedOnce)
			{
				monkeyLinePlayedOnce = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_14_30.prefab:0787881bd0a25a342ba06f566f16051b", "TUTORIAL03_JAINA_14", Notification.SpeechBubbleDirection.BottomLeft, actor));
			}
			else if (!DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_JAINA_15_31.prefab:4e0f1eaa19e283a4cac77219e1f10fe3", "TUTORIAL03_JAINA_15", Notification.SpeechBubbleDirection.BottomLeft, actor));
			}
			break;
		case 54:
		{
			yield return new WaitForSeconds(2f);
			string bodyTextGameString = "TUTORIAL03_HELP_03";
			if (UniversalInputManager.Get().IsTouchMode())
			{
				bodyTextGameString = "TUTORIAL03_HELP_03_TOUCH";
			}
			ShowTutorialDialog("TUTORIAL03_HELP_02", bodyTextGameString, "TUTORIAL01_HELP_16", new Vector2(0.5f, 0.5f));
			break;
		}
		case 55:
			FadeInHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_01_01.prefab:3f6638f7f0d96da4ca422a290035c97a", "TUTORIAL03_MUKLA_01", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			MulliganManager.Get().BeginMulligan();
			FadeOutHeroActor(enemyActor);
			break;
		}
	}

	private IEnumerator ShowTauntPopup()
	{
		Card gorillaCard = null;
		while (gorillaCard == null)
		{
			gorillaCard = FindCardInEnemyBattlefield("TU5_CS2_127");
			if (gorillaCard != null)
			{
				break;
			}
			yield return null;
		}
		while (!gorillaCard.IsActorReady())
		{
			yield return null;
		}
		Vector3 position = gorillaCard.transform.position - new Vector3(3f, 0f, 0f);
		Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL03_HELP_01"));
		notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		NotificationManager.Get().DestroyNotification(notification, 6f);
	}

	private IEnumerator AdjustBigBrotherTransform()
	{
		ZonePlay enemyBattlefield = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone();
		Vector3 prevBattlefieldScale = enemyBattlefield.transform.localScale;
		enemyBattlefield.transform.localScale = 1.6f * enemyBattlefield.transform.localScale;
		Vector3 position = enemyBattlefield.transform.position;
		enemyBattlefield.transform.position = new Vector3(position.x + 2.39316368f, position.y, position.z + 0.7f);
		Card bigBrotherCard = null;
		while (bigBrotherCard == null)
		{
			bigBrotherCard = FindCardInEnemyBattlefield("TU4c_007");
			if (bigBrotherCard != null)
			{
				break;
			}
			yield return null;
		}
		while (!bigBrotherCard.IsActorReady())
		{
			yield return null;
		}
		Actor actor = bigBrotherCard.GetActor();
		Transform parent = actor.transform.parent;
		Vector3 localScale = actor.transform.localScale;
		actor.transform.parent = null;
		bigBrotherCard.transform.localScale = prevBattlefieldScale;
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = parent;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
		actor.transform.parent = gameObject.transform;
		actor.transform.localScale = localScale;
		enemyBattlefield.transform.localScale = prevBattlefieldScale;
	}

	private Card FindCardInEnemyBattlefield(string cardId)
	{
		ZonePlay battlefieldZone = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone();
		for (int i = 0; i < battlefieldZone.GetCardCount(); i++)
		{
			Card cardAtIndex = battlefieldZone.GetCardAtIndex(i);
			if (!(cardAtIndex.GetEntity().GetCardId() != cardId))
			{
				return cardAtIndex;
			}
		}
		return null;
	}

	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		GetGameOptions().SetBooleanOption(GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN, value: false);
	}

	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (mousedOverEntity.HasTaunt())
		{
			GetGameOptions().SetBooleanOption(GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN, value: true);
		}
	}

	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (!base.NotifyOfBattlefieldCardClicked(clickedEntity, wasInTargetMode))
		{
			return false;
		}
		if (wasInTargetMode && clickedEntity.GetCardId() == "TU4c_007" && !warnedAgainstAttackingGorilla)
		{
			warnedAgainstAttackingGorilla = true;
			HandleMissionEvent(11);
			return false;
		}
		return true;
	}

	private void ShowDontPolymorphYourselfPopup(Vector3 origin)
	{
		if (thatsABadPlayPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(thatsABadPlayPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		thatsABadPlayPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"));
		NotificationManager.Get().DestroyNotification(thatsABadPlayPopup, 2.5f);
	}

	private void ShowDontFireballYourselfPopup(Vector3 origin)
	{
		if (thatsABadPlayPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(thatsABadPlayPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		thatsABadPlayPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"));
		NotificationManager.Get().DestroyNotification(thatsABadPlayPopup, 2.5f);
	}

	public override void NotifyOfDefeatCoinAnimation()
	{
		if (victory)
		{
			PlaySound("VO_TUTORIAL_03_JAINA_20_36.prefab:79671f155307aa24a89b0581e4c5c4b2");
		}
	}

	public override List<RewardData> GetCustomRewards()
	{
		if (!victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_022", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}
}
