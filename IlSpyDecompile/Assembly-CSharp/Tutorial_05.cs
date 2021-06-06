using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_05 : TutorialEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private int weaponsPlayed;

	private int numTimesRemindedAboutGoal;

	private bool heroPowerHasNotBeenUsed = true;

	private bool victory;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN,
			true
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public Tutorial_05()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_12_12.prefab:dacd7715ffe4d38458679bd5cac593d1");
		PreloadSound("VO_TUTORIAL_04_JAINA_03_39.prefab:ef84060011610064abeee5d2d526bf85");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_11_11.prefab:8cd68956e13f8ee43bb816a92c56ab7e");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_02_03.prefab:00cdf773e524ae548a31d82db5bb35c2");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_04_05.prefab:eb68b53ffa7195841a18d4c50516ce35");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_08_08.prefab:32281bee676aa6d4e9c590dfb9e03cb6");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_03_04.prefab:38739c8e8bb7eba42a94afe8bce981f3");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_05_06.prefab:30bf89624d8c3df4b9f776218c7300ad");
		PreloadSound("VO_TUTORIAL_05_JAINA_02_46.prefab:4daa9f9fc9fc730429c198b9a7212521");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_06_07.prefab:f8e57e165a11de047a2fcaa95e22457b");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_09_09.prefab:1ca9806eebcb0e841be971e486199833");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_10_10.prefab:30c0266100fcd714e804006040c241ad");
		PreloadSound("VO_TUTORIAL_05_JAINA_05_47.prefab:8caf0051fc3c91c48852eed53e886e4b");
		PreloadSound("VO_TUTORIAL_05_JAINA_06_48.prefab:fbfba7282a8cb334ba40699cab0524fd");
		PreloadSound("VO_TUTORIAL_05_ILLIDAN_01_02.prefab:d4b65ea6366e7a64d8833321001590f1");
		PreloadSound("VO_TUTORIAL_05_JAINA_01_45.prefab:d5b645ea8a95c0e44a90838ab77b2564");
		PreloadSound("VO_INNKEEPER_TUT_COMPLETE_05.prefab:c8d19a552e18c7c429946f62102c9460");
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
			SetTutorialProgress(TutorialProgress.ILLIDAN_COMPLETE);
			FixedRewardsMgr.Get().CheckForTutorialComplete();
			if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn() && !GameMgr.Get().IsSpectator())
			{
				BnetPresenceMgr.Get().SetGameField(15u, 1);
			}
			ResetTutorialLostProgress();
			PlaySound("VO_TUTORIAL_05_ILLIDAN_12_12.prefab:dacd7715ffe4d38458679bd5cac593d1");
			break;
		case TAG_PLAYSTATE.TIED:
			PlaySound("VO_TUTORIAL_05_ILLIDAN_12_12.prefab:dacd7715ffe4d38458679bd5cac593d1");
			break;
		case TAG_PLAYSTATE.LOST:
			SetTutorialLostProgress(TutorialProgress.ILLIDAN_COMPLETE);
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
		if (GameState.Get().GetOpposingSidePlayer().HasWeapon())
		{
			GameState.Get().GetOpposingSidePlayer().GetWeaponCard()
				.GetActorSpell(SpellType.DEATH)
				.m_BlockServerEvents = true;
		}
		if (turn == 2)
		{
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_04_JAINA_03_39.prefab:ef84060011610064abeee5d2d526bf85", "TUTORIAL04_JAINA_03", Notification.SpeechBubbleDirection.BottomLeft, actor));
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_11_11.prefab:8cd68956e13f8ee43bb816a92c56ab7e", "TUTORIAL05_ILLIDAN_11", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			if (GetTag(GAME_TAG.TURN) == 2 && EndTurnButton.Get().IsInNMPState())
			{
				ShowEndTurnBouncingArrow();
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			HandleGameStartEvent();
			break;
		case 2:
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				weaponsPlayed++;
				if (weaponsPlayed == 1)
				{
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_02_03.prefab:00cdf773e524ae548a31d82db5bb35c2", "TUTORIAL05_ILLIDAN_02", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
				}
				else if (weaponsPlayed == 2)
				{
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_04_05.prefab:eb68b53ffa7195841a18d4c50516ce35", "TUTORIAL05_ILLIDAN_04", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_08_08.prefab:32281bee676aa6d4e9c590dfb9e03cb6", "TUTORIAL05_ILLIDAN_08", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
				}
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 3:
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_03_04.prefab:38739c8e8bb7eba42a94afe8bce981f3", "TUTORIAL05_ILLIDAN_03", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
			}
			break;
		case 4:
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_05_06.prefab:30bf89624d8c3df4b9f776218c7300ad", "TUTORIAL05_ILLIDAN_05", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 5:
			if (heroPowerHasNotBeenUsed)
			{
				heroPowerHasNotBeenUsed = false;
				GameState.Get().SetBusy(busy: true);
				yield return new WaitForSeconds(2f);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_JAINA_02_46.prefab:4daa9f9fc9fc730429c198b9a7212521", "TUTORIAL05_JAINA_02", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_06_07.prefab:f8e57e165a11de047a2fcaa95e22457b", "TUTORIAL05_ILLIDAN_06", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 8:
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_09_09.prefab:1ca9806eebcb0e841be971e486199833", "TUTORIAL05_ILLIDAN_09", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			break;
		case 9:
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_10_10.prefab:30c0266100fcd714e804006040c241ad", "TUTORIAL05_ILLIDAN_10", Notification.SpeechBubbleDirection.TopLeft, enemyActor));
			}
			break;
		case 10:
			if (numTimesRemindedAboutGoal == 0)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_JAINA_05_47.prefab:8caf0051fc3c91c48852eed53e886e4b", "TUTORIAL05_JAINA_05", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			}
			else if (numTimesRemindedAboutGoal == 1)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_JAINA_06_48.prefab:fbfba7282a8cb334ba40699cab0524fd", "TUTORIAL05_JAINA_06", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			}
			numTimesRemindedAboutGoal++;
			break;
		case 12:
		{
			GameState.Get().SetBusy(busy: true);
			Vector3 position = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.transform.position;
			Vector3 position2 = new Vector3(position.x - 1.55f, position.y, position.z - 2.721f);
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL05_HELP_01"));
			notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			yield return new WaitForSeconds(5.5f);
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 54:
		{
			yield return new WaitForSeconds(2f);
			string bodyTextGameString = ((!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE)) ? "TUTORIAL05_HELP_03" : "TUTORIAL05_HELP_04");
			ShowTutorialDialog("TUTORIAL05_HELP_02", bodyTextGameString, "TUTORIAL01_HELP_16", new Vector2(0.5f, 0f), swapMaterial: true);
			break;
		}
		case 55:
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				FadeInHeroActor(enemyActor);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_01_02.prefab:d4b65ea6366e7a64d8833321001590f1", "TUTORIAL05_ILLIDAN_01", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				FadeOutHeroActor(enemyActor);
				yield return new WaitForSeconds(0.5f);
				FadeInHeroActor(jainaActor);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_05_JAINA_01_45.prefab:d5b645ea8a95c0e44a90838ab77b2564", "TUTORIAL05_JAINA_01", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			}
			MulliganManager.Get().BeginMulligan();
			if (!DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				yield return new WaitForSeconds(2.3f);
				FadeOutHeroActor(jainaActor);
			}
			break;
		}
	}

	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

	private void ShowEndTurnBouncingArrow()
	{
		if (!EndTurnButton.Get().IsInWaitingState())
		{
			Vector3 position = EndTurnButton.Get().transform.position;
			Vector3 position2 = new Vector3(position.x - 2f, position.y, position.z);
			NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, -90f, 0f));
		}
	}

	public override bool NotifyOfEndTurnButtonPushed()
	{
		NotificationManager.Get().DestroyAllArrows();
		return true;
	}

	public override bool NotifyOfTooltipDisplay(TooltipZone specificZone)
	{
		return false;
	}

	public override string[] NotifyOfKeywordHelpPanelDisplay(Entity entity)
	{
		if (entity.GetCardId() == "TU4e_004" || entity.GetCardId() == "TU4e_007")
		{
			return new string[2]
			{
				GameStrings.Get("TUTORIAL05_WEAPON_HEADLINE"),
				GameStrings.Get("TUTORIAL05_WEAPON_DESC")
			};
		}
		return null;
	}

	public override List<RewardData> GetCustomRewards()
	{
		if (!victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("EX1_277", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}
}
