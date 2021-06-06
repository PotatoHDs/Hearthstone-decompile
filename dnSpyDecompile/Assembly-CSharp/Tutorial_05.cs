using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DC RID: 1500
public class Tutorial_05 : TutorialEntity
{
	// Token: 0x0600522A RID: 21034 RVA: 0x001AF66C File Offset: 0x001AD86C
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.KEYWORD_HELP_DELAY_OVERRIDDEN,
				true
			}
		};
	}

	// Token: 0x0600522B RID: 21035 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x0600522C RID: 21036 RVA: 0x001AFFE0 File Offset: 0x001AE1E0
	public Tutorial_05()
	{
		this.m_gameOptions.AddOptions(Tutorial_05.s_booleanOptions, Tutorial_05.s_stringOptions);
	}

	// Token: 0x0600522D RID: 21037 RVA: 0x001B0004 File Offset: 0x001AE204
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_12_12.prefab:dacd7715ffe4d38458679bd5cac593d1");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_03_39.prefab:ef84060011610064abeee5d2d526bf85");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_11_11.prefab:8cd68956e13f8ee43bb816a92c56ab7e");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_02_03.prefab:00cdf773e524ae548a31d82db5bb35c2");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_04_05.prefab:eb68b53ffa7195841a18d4c50516ce35");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_08_08.prefab:32281bee676aa6d4e9c590dfb9e03cb6");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_03_04.prefab:38739c8e8bb7eba42a94afe8bce981f3");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_05_06.prefab:30bf89624d8c3df4b9f776218c7300ad");
		base.PreloadSound("VO_TUTORIAL_05_JAINA_02_46.prefab:4daa9f9fc9fc730429c198b9a7212521");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_06_07.prefab:f8e57e165a11de047a2fcaa95e22457b");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_09_09.prefab:1ca9806eebcb0e841be971e486199833");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_10_10.prefab:30c0266100fcd714e804006040c241ad");
		base.PreloadSound("VO_TUTORIAL_05_JAINA_05_47.prefab:8caf0051fc3c91c48852eed53e886e4b");
		base.PreloadSound("VO_TUTORIAL_05_JAINA_06_48.prefab:fbfba7282a8cb334ba40699cab0524fd");
		base.PreloadSound("VO_TUTORIAL_05_ILLIDAN_01_02.prefab:d4b65ea6366e7a64d8833321001590f1");
		base.PreloadSound("VO_TUTORIAL_05_JAINA_01_45.prefab:d5b645ea8a95c0e44a90838ab77b2564");
		base.PreloadSound("VO_INNKEEPER_TUT_COMPLETE_05.prefab:c8d19a552e18c7c429946f62102c9460");
	}

	// Token: 0x0600522E RID: 21038 RVA: 0x001B00CC File Offset: 0x001AE2CC
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			this.victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.SetTutorialProgress(TutorialProgress.ILLIDAN_COMPLETE);
			FixedRewardsMgr.Get().CheckForTutorialComplete();
			if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn() && !GameMgr.Get().IsSpectator())
			{
				BnetPresenceMgr.Get().SetGameField(15U, 1);
			}
			base.ResetTutorialLostProgress();
			base.PlaySound("VO_TUTORIAL_05_ILLIDAN_12_12.prefab:dacd7715ffe4d38458679bd5cac593d1", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.TIED)
		{
			base.PlaySound("VO_TUTORIAL_05_ILLIDAN_12_12.prefab:dacd7715ffe4d38458679bd5cac593d1", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			base.SetTutorialLostProgress(TutorialProgress.ILLIDAN_COMPLETE);
		}
	}

	// Token: 0x0600522F RID: 21039 RVA: 0x001B0163 File Offset: 0x001AE363
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (GameState.Get().GetOpposingSidePlayer().HasWeapon())
		{
			GameState.Get().GetOpposingSidePlayer().GetWeaponCard().GetActorSpell(SpellType.DEATH, true).m_BlockServerEvents = true;
		}
		if (turn == 2)
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_04_JAINA_03_39.prefab:ef84060011610064abeee5d2d526bf85", "TUTORIAL04_JAINA_03", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
			if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_11_11.prefab:8cd68956e13f8ee43bb816a92c56ab7e", "TUTORIAL05_ILLIDAN_11", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
			if (base.GetTag(GAME_TAG.TURN) == 2 && EndTurnButton.Get().IsInNMPState())
			{
				this.ShowEndTurnBouncingArrow();
			}
		}
		yield break;
	}

	// Token: 0x06005230 RID: 21040 RVA: 0x001B0179 File Offset: 0x001AE379
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 1:
			base.HandleGameStartEvent();
			break;
		case 2:
			if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				this.weaponsPlayed++;
				if (this.weaponsPlayed == 1)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_02_03.prefab:00cdf773e524ae548a31d82db5bb35c2", "TUTORIAL05_ILLIDAN_02", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
				}
				else if (this.weaponsPlayed == 2)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_04_05.prefab:eb68b53ffa7195841a18d4c50516ce35", "TUTORIAL05_ILLIDAN_04", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_08_08.prefab:32281bee676aa6d4e9c590dfb9e03cb6", "TUTORIAL05_ILLIDAN_08", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
				}
				GameState.Get().SetBusy(false);
			}
			break;
		case 3:
			if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_03_04.prefab:38739c8e8bb7eba42a94afe8bce981f3", "TUTORIAL05_ILLIDAN_03", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
			}
			break;
		case 4:
			if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_05_06.prefab:30bf89624d8c3df4b9f776218c7300ad", "TUTORIAL05_ILLIDAN_05", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 5:
			if (this.heroPowerHasNotBeenUsed)
			{
				this.heroPowerHasNotBeenUsed = false;
				GameState.Get().SetBusy(true);
				yield return new WaitForSeconds(2f);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_JAINA_02_46.prefab:4daa9f9fc9fc730429c198b9a7212521", "TUTORIAL05_JAINA_02", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_06_07.prefab:f8e57e165a11de047a2fcaa95e22457b", "TUTORIAL05_ILLIDAN_06", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 6:
		case 7:
		case 11:
			break;
		case 8:
			if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_09_09.prefab:1ca9806eebcb0e841be971e486199833", "TUTORIAL05_ILLIDAN_09", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			}
			break;
		case 9:
			if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_10_10.prefab:30c0266100fcd714e804006040c241ad", "TUTORIAL05_ILLIDAN_10", Notification.SpeechBubbleDirection.TopLeft, enemyActor, 1f, true, false, 3f, 0f));
			}
			break;
		case 10:
			if (this.numTimesRemindedAboutGoal == 0)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_JAINA_05_47.prefab:8caf0051fc3c91c48852eed53e886e4b", "TUTORIAL05_JAINA_05", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
			}
			else if (this.numTimesRemindedAboutGoal == 1)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_JAINA_06_48.prefab:fbfba7282a8cb334ba40699cab0524fd", "TUTORIAL05_JAINA_06", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
			}
			this.numTimesRemindedAboutGoal++;
			break;
		case 12:
		{
			GameState.Get().SetBusy(true);
			Vector3 position = GameState.Get().GetOpposingSidePlayer().GetHeroCard().transform.position;
			Vector3 position2 = new Vector3(position.x - 1.55f, position.y, position.z - 2.721f);
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL05_HELP_01"), true, NotificationManager.PopupTextType.BASIC);
			notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			yield return new WaitForSeconds(5.5f);
			GameState.Get().SetBusy(false);
			break;
		}
		default:
			if (missionEvent != 54)
			{
				if (missionEvent == 55)
				{
					if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
					{
						base.FadeInHeroActor(enemyActor);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_ILLIDAN_01_02.prefab:d4b65ea6366e7a64d8833321001590f1", "TUTORIAL05_ILLIDAN_01", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						base.FadeOutHeroActor(enemyActor);
						yield return new WaitForSeconds(0.5f);
						base.FadeInHeroActor(jainaActor);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_05_JAINA_01_45.prefab:d5b645ea8a95c0e44a90838ab77b2564", "TUTORIAL05_JAINA_01", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
					}
					MulliganManager.Get().BeginMulligan();
					if (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE))
					{
						yield return new WaitForSeconds(2.3f);
						base.FadeOutHeroActor(jainaActor);
					}
				}
			}
			else
			{
				yield return new WaitForSeconds(2f);
				string bodyTextGameString = (!base.DidLoseTutorial(TutorialProgress.ILLIDAN_COMPLETE)) ? "TUTORIAL05_HELP_03" : "TUTORIAL05_HELP_04";
				base.ShowTutorialDialog("TUTORIAL05_HELP_02", bodyTextGameString, "TUTORIAL01_HELP_16", new Vector2(0.5f, 0f), true);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06005231 RID: 21041 RVA: 0x001B018F File Offset: 0x001AE38F
	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		yield break;
	}

	// Token: 0x06005232 RID: 21042 RVA: 0x001B01A0 File Offset: 0x001AE3A0
	private void ShowEndTurnBouncingArrow()
	{
		if (EndTurnButton.Get().IsInWaitingState())
		{
			return;
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 2f, position.y, position.z);
		NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, -90f, 0f));
	}

	// Token: 0x06005233 RID: 21043 RVA: 0x001B020A File Offset: 0x001AE40A
	public override bool NotifyOfEndTurnButtonPushed()
	{
		NotificationManager.Get().DestroyAllArrows();
		return true;
	}

	// Token: 0x06005234 RID: 21044 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool NotifyOfTooltipDisplay(TooltipZone specificZone)
	{
		return false;
	}

	// Token: 0x06005235 RID: 21045 RVA: 0x001B0218 File Offset: 0x001AE418
	public override string[] NotifyOfKeywordHelpPanelDisplay(Entity entity)
	{
		if (entity.GetCardId() == "TU4e_004" || entity.GetCardId() == "TU4e_007")
		{
			return new string[]
			{
				GameStrings.Get("TUTORIAL05_WEAPON_HEADLINE"),
				GameStrings.Get("TUTORIAL05_WEAPON_DESC")
			};
		}
		return null;
	}

	// Token: 0x06005236 RID: 21046 RVA: 0x001B026C File Offset: 0x001AE46C
	public override List<RewardData> GetCustomRewards()
	{
		if (!this.victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("EX1_277", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}

	// Token: 0x0400496F RID: 18799
	private static Map<GameEntityOption, bool> s_booleanOptions = Tutorial_05.InitBooleanOptions();

	// Token: 0x04004970 RID: 18800
	private static Map<GameEntityOption, string> s_stringOptions = Tutorial_05.InitStringOptions();

	// Token: 0x04004971 RID: 18801
	private int weaponsPlayed;

	// Token: 0x04004972 RID: 18802
	private int numTimesRemindedAboutGoal;

	// Token: 0x04004973 RID: 18803
	private bool heroPowerHasNotBeenUsed = true;

	// Token: 0x04004974 RID: 18804
	private bool victory;
}
