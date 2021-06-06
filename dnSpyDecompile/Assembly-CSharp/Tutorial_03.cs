using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DA RID: 1498
public class Tutorial_03 : TutorialEntity
{
	// Token: 0x060051FC RID: 20988 RVA: 0x001AF287 File Offset: 0x001AD487
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

	// Token: 0x060051FD RID: 20989 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x060051FE RID: 20990 RVA: 0x001AF2A9 File Offset: 0x001AD4A9
	public Tutorial_03()
	{
		this.m_gameOptions.AddOptions(Tutorial_03.s_booleanOptions, Tutorial_03.s_stringOptions);
	}

	// Token: 0x060051FF RID: 20991 RVA: 0x001AF2C8 File Offset: 0x001AD4C8
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TUTORIAL_03_JAINA_17_33.prefab:b96f78fff7ab94a42894930d51bd45bd");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_18_34.prefab:b9a2a99d30893804790829b3ceabc9b8");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_01_24.prefab:b9515cf173f876a458202c6092055709");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_05_25.prefab:38e2d64610e757245877b8f8e2f68584");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_07_26.prefab:e93d67263c3d99740aaa4acc4b7d87a4");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_12_28.prefab:d30f0c732643aa74aba9ec4cf2c2e6dd");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_13_29.prefab:efca9c5305a101e4d968d08e58061cda");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_16_32.prefab:b05bea699e2f897478c81a485a7d1a1a");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_14_30.prefab:0787881bd0a25a342ba06f566f16051b");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_15_31.prefab:4e0f1eaa19e283a4cac77219e1f10fe3");
		base.PreloadSound("VO_TUTORIAL_03_JAINA_20_36.prefab:79671f155307aa24a89b0581e4c5c4b2");
		base.PreloadSound("VO_TUTORIAL_03_MUKLA_01_01.prefab:3f6638f7f0d96da4ca422a290035c97a");
		base.PreloadSound("VO_TUTORIAL_03_MUKLA_03_03.prefab:5018131495f68c247bac073424fab700");
		base.PreloadSound("VO_TUTORIAL_03_MUKLA_04_04.prefab:0e4a4c87ac994c845b06230a34b168f9");
		base.PreloadSound("VO_TUTORIAL_03_MUKLA_05_05.prefab:8c12c75976cdfe044ad8ff3dd14ae5b8");
		base.PreloadSound("VO_TUTORIAL_03_MUKLA_06_06.prefab:8ed0c9ff5d18314469821d5be3d62dc7");
		base.PreloadSound("VO_TUTORIAL_03_MUKLA_07_07.prefab:c7b7dc3589c10c94bb3b9c0c6c08e3f6");
	}

	// Token: 0x06005200 RID: 20992 RVA: 0x001AF390 File Offset: 0x001AD590
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			this.victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.SetTutorialProgress(TutorialProgress.MUKLA_COMPLETE);
			base.PlaySound("VO_TUTORIAL_03_MUKLA_07_07.prefab:c7b7dc3589c10c94bb3b9c0c6c08e3f6", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.TIED)
		{
			base.PlaySound("VO_TUTORIAL_03_MUKLA_07_07.prefab:c7b7dc3589c10c94bb3b9c0c6c08e3f6", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			base.SetTutorialLostProgress(TutorialProgress.MUKLA_COMPLETE);
		}
	}

	// Token: 0x06005201 RID: 20993 RVA: 0x001AF3EF File Offset: 0x001AD5EF
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (this.enemyPlayedBigBrother)
		{
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				if (GameState.Get().GetNumEnemyMinionsInPlay(false) > 0)
				{
					if (!this.needATaunterVOPlayed)
					{
						if (!GameState.Get().GetFriendlySidePlayer().HasATauntMinion())
						{
							this.needATaunterVOPlayed = true;
							Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_17_33.prefab:b96f78fff7ab94a42894930d51bd45bd", "TUTORIAL03_JAINA_17", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
						}
						yield break;
					}
					if (!this.defenselessVoPlayed)
					{
						bool flag = true;
						using (List<Card>.Enumerator enumerator = GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.GetEntity().HasTaunt())
								{
									flag = false;
								}
							}
						}
						if (flag)
						{
							this.defenselessVoPlayed = true;
							Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_18_34.prefab:b9a2a99d30893804790829b3ceabc9b8", "TUTORIAL03_JAINA_18", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
						}
					}
				}
			}
			else if (!this.seenTheBrother)
			{
				Gameplay.Get().StartCoroutine(this.GetReadyForBro());
			}
		}
		if (turn != 1)
		{
			switch (turn)
			{
			case 5:
				if (!base.DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
				{
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_05_25.prefab:38e2d64610e757245877b8f8e2f68584", "TUTORIAL03_JAINA_05", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_03_03.prefab:5018131495f68c247bac073424fab700", "TUTORIAL03_MUKLA_03", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				}
				break;
			case 6:
				if (!base.DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
				{
					GameState.Get().SetBusy(true);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_04_04.prefab:0e4a4c87ac994c845b06230a34b168f9", "TUTORIAL03_MUKLA_04", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					GameState.Get().SetBusy(false);
				}
				break;
			case 7:
			case 8:
				break;
			case 9:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_07_26.prefab:e93d67263c3d99740aaa4acc4b7d87a4", "TUTORIAL03_JAINA_07", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
				break;
			default:
				if (turn == 14)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_05_05.prefab:8c12c75976cdfe044ad8ff3dd14ae5b8", "TUTORIAL03_MUKLA_05", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				}
				break;
			}
		}
		else if (!base.DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_01_24.prefab:b9515cf173f876a458202c6092055709", "TUTORIAL03_JAINA_01", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
		}
		yield break;
	}

	// Token: 0x06005202 RID: 20994 RVA: 0x001AF405 File Offset: 0x001AD605
	private IEnumerator GetReadyForBro()
	{
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		this.seenTheBrother = true;
		GameState.Get().SetBusy(true);
		yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_12_28.prefab:d30f0c732643aa74aba9ec4cf2c2e6dd", "TUTORIAL03_JAINA_12", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
		GameState.Get().SetBusy(false);
		yield return new WaitForSeconds(3.2f);
		Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_13_29.prefab:efca9c5305a101e4d968d08e58061cda", "TUTORIAL03_JAINA_13", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
		yield break;
	}

	// Token: 0x06005203 RID: 20995 RVA: 0x001AF414 File Offset: 0x001AD614
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 4)
		{
			if (missionEvent != 1)
			{
				if (missionEvent == 4)
				{
					this.numTauntGorillasPlayed++;
					if (this.numTauntGorillasPlayed == 1)
					{
						Gameplay.Get().StartCoroutine(this.ShowTauntPopup());
					}
					else if (this.numTauntGorillasPlayed == 2 && !base.DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
					{
						GameState.Get().SetBusy(true);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_06_06.prefab:8ed0c9ff5d18314469821d5be3d62dc7", "TUTORIAL03_MUKLA_06", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						GameState.Get().SetBusy(false);
					}
				}
			}
			else
			{
				base.HandleGameStartEvent();
				AssetLoader.Get().InstantiatePrefab("TutorialKeywordManager.prefab:c1276fda3e1df594990295731f80c9c2", AssetLoadingOptions.IgnorePrefabPosition);
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 10:
				this.enemyPlayedBigBrother = true;
				Gameplay.Get().StartCoroutine(this.AdjustBigBrotherTransform());
				if (!GameState.Get().IsFriendlySidePlayerTurn())
				{
					Gameplay.Get().StartCoroutine(this.GetReadyForBro());
				}
				break;
			case 11:
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_16_32.prefab:b05bea699e2f897478c81a485a7d1a1a", "TUTORIAL03_JAINA_16", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
				break;
			case 12:
				if (!this.monkeyLinePlayedOnce)
				{
					this.monkeyLinePlayedOnce = true;
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_14_30.prefab:0787881bd0a25a342ba06f566f16051b", "TUTORIAL03_JAINA_14", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
				}
				else if (!base.DidLoseTutorial(TutorialProgress.MUKLA_COMPLETE))
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_JAINA_15_31.prefab:4e0f1eaa19e283a4cac77219e1f10fe3", "TUTORIAL03_JAINA_15", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
				}
				break;
			default:
				if (missionEvent != 54)
				{
					if (missionEvent == 55)
					{
						base.FadeInHeroActor(enemyActor);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_03_MUKLA_01_01.prefab:3f6638f7f0d96da4ca422a290035c97a", "TUTORIAL03_MUKLA_01", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						MulliganManager.Get().BeginMulligan();
						base.FadeOutHeroActor(enemyActor);
					}
				}
				else
				{
					yield return new WaitForSeconds(2f);
					string bodyTextGameString = "TUTORIAL03_HELP_03";
					if (UniversalInputManager.Get().IsTouchMode())
					{
						bodyTextGameString = "TUTORIAL03_HELP_03_TOUCH";
					}
					base.ShowTutorialDialog("TUTORIAL03_HELP_02", bodyTextGameString, "TUTORIAL01_HELP_16", new Vector2(0.5f, 0.5f), false);
				}
				break;
			}
		}
		yield break;
	}

	// Token: 0x06005204 RID: 20996 RVA: 0x001AF42A File Offset: 0x001AD62A
	private IEnumerator ShowTauntPopup()
	{
		Card gorillaCard = null;
		while (gorillaCard == null)
		{
			gorillaCard = this.FindCardInEnemyBattlefield("TU5_CS2_127");
			if (gorillaCard != null)
			{
				IL_8F:
				while (!gorillaCard.IsActorReady())
				{
					yield return null;
				}
				Vector3 position = gorillaCard.transform.position - new Vector3(3f, 0f, 0f);
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL03_HELP_01"), true, NotificationManager.PopupTextType.BASIC);
				notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				NotificationManager.Get().DestroyNotification(notification, 6f);
				yield break;
			}
			yield return null;
		}
		goto IL_8F;
	}

	// Token: 0x06005205 RID: 20997 RVA: 0x001AF439 File Offset: 0x001AD639
	private IEnumerator AdjustBigBrotherTransform()
	{
		ZonePlay enemyBattlefield = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone();
		Vector3 prevBattlefieldScale = enemyBattlefield.transform.localScale;
		enemyBattlefield.transform.localScale = 1.6f * enemyBattlefield.transform.localScale;
		Vector3 position = enemyBattlefield.transform.position;
		enemyBattlefield.transform.position = new Vector3(position.x + 2.3931637f, position.y, position.z + 0.7f);
		Card bigBrotherCard = null;
		while (bigBrotherCard == null)
		{
			bigBrotherCard = this.FindCardInEnemyBattlefield("TU4c_007");
			if (bigBrotherCard != null)
			{
				IL_128:
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
				yield break;
			}
			yield return null;
		}
		goto IL_128;
	}

	// Token: 0x06005206 RID: 20998 RVA: 0x001AF448 File Offset: 0x001AD648
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

	// Token: 0x06005207 RID: 20999 RVA: 0x001AF494 File Offset: 0x001AD694
	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		base.GetGameOptions().SetBooleanOption(GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN, false);
	}

	// Token: 0x06005208 RID: 21000 RVA: 0x001AF4A4 File Offset: 0x001AD6A4
	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (mousedOverEntity.HasTaunt())
		{
			base.GetGameOptions().SetBooleanOption(GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN, true);
		}
	}

	// Token: 0x06005209 RID: 21001 RVA: 0x001AF4BC File Offset: 0x001AD6BC
	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (!base.NotifyOfBattlefieldCardClicked(clickedEntity, wasInTargetMode))
		{
			return false;
		}
		if (wasInTargetMode && clickedEntity.GetCardId() == "TU4c_007" && !this.warnedAgainstAttackingGorilla)
		{
			this.warnedAgainstAttackingGorilla = true;
			base.HandleMissionEvent(11);
			return false;
		}
		return true;
	}

	// Token: 0x0600520A RID: 21002 RVA: 0x001AF4FC File Offset: 0x001AD6FC
	private void ShowDontPolymorphYourselfPopup(Vector3 origin)
	{
		if (this.thatsABadPlayPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.thatsABadPlayPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		this.thatsABadPlayPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.thatsABadPlayPopup, 2.5f);
	}

	// Token: 0x0600520B RID: 21003 RVA: 0x001AF580 File Offset: 0x001AD780
	private void ShowDontFireballYourselfPopup(Vector3 origin)
	{
		if (this.thatsABadPlayPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.thatsABadPlayPopup);
		}
		Vector3 position = new Vector3(origin.x - 3f, origin.y, origin.z);
		this.thatsABadPlayPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL01_HELP_07"), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.thatsABadPlayPopup, 2.5f);
	}

	// Token: 0x0600520C RID: 21004 RVA: 0x001AF602 File Offset: 0x001AD802
	public override void NotifyOfDefeatCoinAnimation()
	{
		if (!this.victory)
		{
			return;
		}
		base.PlaySound("VO_TUTORIAL_03_JAINA_20_36.prefab:79671f155307aa24a89b0581e4c5c4b2", 1f, true, false);
	}

	// Token: 0x0600520D RID: 21005 RVA: 0x001AF620 File Offset: 0x001AD820
	public override List<RewardData> GetCustomRewards()
	{
		if (!this.victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_022", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}

	// Token: 0x04004953 RID: 18771
	private static Map<GameEntityOption, bool> s_booleanOptions = Tutorial_03.InitBooleanOptions();

	// Token: 0x04004954 RID: 18772
	private static Map<GameEntityOption, string> s_stringOptions = Tutorial_03.InitStringOptions();

	// Token: 0x04004955 RID: 18773
	private int numTauntGorillasPlayed;

	// Token: 0x04004956 RID: 18774
	private bool enemyPlayedBigBrother;

	// Token: 0x04004957 RID: 18775
	private bool needATaunterVOPlayed;

	// Token: 0x04004958 RID: 18776
	private bool monkeyLinePlayedOnce;

	// Token: 0x04004959 RID: 18777
	private bool defenselessVoPlayed;

	// Token: 0x0400495A RID: 18778
	private bool seenTheBrother;

	// Token: 0x0400495B RID: 18779
	private bool warnedAgainstAttackingGorilla;

	// Token: 0x0400495C RID: 18780
	private bool victory;

	// Token: 0x0400495D RID: 18781
	private Notification thatsABadPlayPopup;
}
