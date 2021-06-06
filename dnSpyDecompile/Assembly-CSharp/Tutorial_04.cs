using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DB RID: 1499
public class Tutorial_04 : TutorialEntity
{
	// Token: 0x0600520F RID: 21007 RVA: 0x001AF66C File Offset: 0x001AD86C
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

	// Token: 0x06005210 RID: 21008 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06005211 RID: 21009 RVA: 0x001AF67C File Offset: 0x001AD87C
	public Tutorial_04()
	{
		this.m_gameOptions.AddOptions(Tutorial_04.s_booleanOptions, Tutorial_04.s_stringOptions);
	}

	// Token: 0x06005212 RID: 21010 RVA: 0x001AF69C File Offset: 0x001AD89C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TUTORIAL04_HEMET_23_21.prefab:86859f5cb798f304395a63f446fe9d00");
		base.PreloadSound("VO_TUTORIAL04_HEMET_15_13.prefab:c0da1267215708947a954e9c0ea1b061");
		base.PreloadSound("VO_TUTORIAL04_HEMET_20_18.prefab:5d49a0bac4c03b94e9e13945624a581b");
		base.PreloadSound("VO_TUTORIAL04_HEMET_16_14.prefab:df368c7075e4a2649803729f7b86601e");
		base.PreloadSound("VO_TUTORIAL04_HEMET_13_12.prefab:fe14ab273aa4b7e4491f30310a7d0eca");
		base.PreloadSound("VO_TUTORIAL04_HEMET_19_17.prefab:b9d5bd30659aae84b8a1380cbdba0398");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_09_43.prefab:1ee05d74948aba04ebd7065e44813921");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_10_44.prefab:6f5921db1071ead4585c8cc9689d22ea");
		base.PreloadSound("VO_TUTORIAL04_HEMET_06_05.prefab:2527939914db3e543941a13266e88a01");
		base.PreloadSound("VO_TUTORIAL04_HEMET_07_06_ALT.prefab:c19475ec3c3b0e648a97f423e0e86143");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_04_40.prefab:5bfc80c6184279140878a51eb1fa3469");
		base.PreloadSound("VO_TUTORIAL04_HEMET_08_07.prefab:68207d2681a60c84d840d37c4b90740f");
		base.PreloadSound("VO_TUTORIAL04_HEMET_09_08.prefab:2994b6b35f2e5f54782b6100ea92f40e");
		base.PreloadSound("VO_TUTORIAL04_HEMET_10_09.prefab:3282099b41c7ab94aa99e84c20dd7db7");
		base.PreloadSound("VO_TUTORIAL04_HEMET_11_10.prefab:db8c8cea0db51d14fbd5d4c782b8b160");
		base.PreloadSound("VO_TUTORIAL04_HEMET_12_11.prefab:b0ea652d6f1ec6845845226680ade252");
		base.PreloadSound("VO_TUTORIAL04_HEMET_12_11_ALT.prefab:b59f55e14876bee43a0d9ab4b7317f84");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_08_42.prefab:36763b11766e2b64198719d44b0fa8bf");
		base.PreloadSound("VO_TUTORIAL04_HEMET_01_01.prefab:89be0839b16c1244a9221b373fd8fb61");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_01_37.prefab:c7fc40d1595ca3c49b524b9929264477");
		base.PreloadSound("VO_TUTORIAL04_HEMET_02_02.prefab:c3ca1337cb01efe4194899d42918f80c");
		base.PreloadSound("VO_TUTORIAL04_HEMET_03_03.prefab:b014c684c85f1c440bed5560c6b6dbf5");
		base.PreloadSound("VO_TUTORIAL_04_JAINA_02_38.prefab:83b64d5eeb884db43b9fa5f20316da2c");
		base.PreloadSound("VO_TUTORIAL04_HEMET_04_04_ALT.prefab:bb3fadd78adce274993862115f3c5137");
	}

	// Token: 0x06005213 RID: 21011 RVA: 0x001AF7B4 File Offset: 0x001AD9B4
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			this.victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		if (this.m_heroPowerCostLabel != null)
		{
			UnityEngine.Object.Destroy(this.m_heroPowerCostLabel);
		}
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.SetTutorialProgress(TutorialProgress.NESINGWARY_COMPLETE);
			base.PlaySound("VO_TUTORIAL04_HEMET_23_21.prefab:86859f5cb798f304395a63f446fe9d00", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.TIED)
		{
			base.PlaySound("VO_TUTORIAL04_HEMET_23_21.prefab:86859f5cb798f304395a63f446fe9d00", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			base.SetTutorialLostProgress(TutorialProgress.NESINGWARY_COMPLETE);
		}
	}

	// Token: 0x06005214 RID: 21012 RVA: 0x001AF82C File Offset: 0x001ADA2C
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		this.m_shouldSignalPolymorph = false;
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (this.m_playOneHealthCommentNextTurn)
		{
			this.m_playOneHealthCommentNextTurn = false;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_04_JAINA_08_42.prefab:36763b11766e2b64198719d44b0fa8bf", "TUTORIAL04_JAINA_08", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
		}
		switch (turn)
		{
		case 1:
			if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_15_13.prefab:c0da1267215708947a954e9c0ea1b061", "TUTORIAL04_HEMET_15", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 4:
		{
			yield return new WaitForSeconds(1f);
			Vector3 position = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard().transform.position;
			if (UniversalInputManager.UsePhoneUI)
			{
				Vector3 position2 = new Vector3(position.x, position.y, position.z + 2.3f);
				this.heroPowerHelp = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_01"), true, NotificationManager.PopupTextType.BASIC);
				this.heroPowerHelp.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			else
			{
				Vector3 position3 = new Vector3(position.x + 3f, position.y, position.z);
				this.heroPowerHelp = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position3, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_01"), true, NotificationManager.PopupTextType.BASIC);
				this.heroPowerHelp.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
				AssetLoader.Get().InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", new PrefabCallback<GameObject>(this.ManaLabelLoadedCallback), GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard(), AssetLoadingOptions.IgnorePrefabPosition);
			}
			break;
		}
		case 5:
			NotificationManager.Get().DestroyNotification(this.heroPowerHelp, 0f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_20_18.prefab:5d49a0bac4c03b94e9e13945624a581b", "TUTORIAL04_HEMET_20", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			break;
		case 7:
			if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_16_14.prefab:df368c7075e4a2649803729f7b86601e", "TUTORIAL04_HEMET_16", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 9:
			if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_13_12.prefab:fe14ab273aa4b7e4491f30310a7d0eca", "TUTORIAL04_HEMET_13", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 11:
			GameState.Get().SetBusy(true);
			Gameplay.Get().SetGameStateBusy(false, 3f);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_19_17.prefab:b9d5bd30659aae84b8a1380cbdba0398", "TUTORIAL04_HEMET_19", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			yield return new WaitForSeconds(0.7f);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_04_JAINA_09_43.prefab:1ee05d74948aba04ebd7065e44813921", "TUTORIAL04_JAINA_09", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			break;
		case 12:
			if (base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				this.m_shouldSignalPolymorph = true;
				List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
				if (InputManager.Get().GetHeldCard() == null)
				{
					Card card = null;
					foreach (Card card2 in cards)
					{
						if (card2.GetEntity().GetCardId() == "TU5_CS2_022")
						{
							card = card2;
						}
					}
					if (!(card == null) && !card.IsMousedOver())
					{
						Gameplay.Get().StartCoroutine(this.ShowArrowInSeconds(0f));
					}
				}
			}
			break;
		}
		yield break;
	}

	// Token: 0x06005215 RID: 21013 RVA: 0x001AF842 File Offset: 0x001ADA42
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
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_06_05.prefab:2527939914db3e543941a13266e88a01", "TUTORIAL04_HEMET_06", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_07_06_ALT.prefab:c19475ec3c3b0e648a97f423e0e86143", "TUTORIAL04_HEMET_07", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			yield return Gameplay.Get().StartCoroutine(this.Wait(1f));
			GameState.Get().SetBusy(false);
			break;
		case 3:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(this.Wait(2f));
			GameState.Get().SetBusy(false);
			break;
		case 4:
			if (UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().GetFriendlyHand().SetHandEnlarged(false);
			}
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_04_JAINA_04_40.prefab:5bfc80c6184279140878a51eb1fa3469", "TUTORIAL04_JAINA_04", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 5:
			if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
			{
				switch (this.numBeastsPlayed)
				{
				case 0:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_08_07.prefab:68207d2681a60c84d840d37c4b90740f", "TUTORIAL04_HEMET_08", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					break;
				case 1:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_09_08.prefab:2994b6b35f2e5f54782b6100ea92f40e", "TUTORIAL04_HEMET_09", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					break;
				case 2:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_10_09.prefab:3282099b41c7ab94aa99e84c20dd7db7", "TUTORIAL04_HEMET_10", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					break;
				case 3:
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_11_10.prefab:db8c8cea0db51d14fbd5d4c782b8b160", "TUTORIAL04_HEMET_11", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					break;
				}
				this.numBeastsPlayed++;
			}
			break;
		case 6:
			if (this.numComplaintsMade == 0)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_12_11.prefab:b0ea652d6f1ec6845845226680ade252", "TUTORIAL04_HEMET_12a", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				this.numComplaintsMade++;
			}
			else
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_12_11_ALT.prefab:b59f55e14876bee43a0d9ab4b7317f84", "TUTORIAL04_HEMET_12b", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			}
			break;
		case 7:
			this.m_playOneHealthCommentNextTurn = true;
			break;
		default:
			if (missionEvent != 54)
			{
				if (missionEvent == 55)
				{
					if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
					{
						base.FadeInHeroActor(enemyActor);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_01_01.prefab:89be0839b16c1244a9221b373fd8fb61", "TUTORIAL04_HEMET_01", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						base.FadeOutHeroActor(enemyActor);
						base.FadeInHeroActor(jainaActor);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_04_JAINA_01_37.prefab:c7fc40d1595ca3c49b524b9929264477", "TUTORIAL04_JAINA_01", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
						base.FadeOutHeroActor(jainaActor);
						yield return new WaitForSeconds(0.5f);
					}
					MulliganManager.Get().BeginMulligan();
					if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
					{
						this.m_hemetSpeaking = true;
						base.FadeInHeroActor(enemyActor);
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_02_02.prefab:c3ca1337cb01efe4194899d42918f80c", "TUTORIAL04_HEMET_02", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						base.FadeOutHeroActor(enemyActor);
						this.m_hemetSpeaking = false;
					}
				}
			}
			else
			{
				yield return new WaitForSeconds(2f);
				string bodyTextGameString = (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE)) ? "TUTORIAL04_HELP_15" : "TUTORIAL04_HELP_16";
				base.ShowTutorialDialog("TUTORIAL04_HELP_14", bodyTextGameString, "TUTORIAL01_HELP_16", Vector2.zero, true);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06005216 RID: 21014 RVA: 0x001AF858 File Offset: 0x001ADA58
	public override void NotifyOfCoinFlipResult()
	{
		Gameplay.Get().StartCoroutine(this.HandleCoinFlip());
	}

	// Token: 0x06005217 RID: 21015 RVA: 0x001AF86B File Offset: 0x001ADA6B
	private IEnumerator HandleCoinFlip()
	{
		GameState.Get().SetBusy(true);
		yield return Gameplay.Get().StartCoroutine(this.Wait(1f));
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		while (this.m_hemetSpeaking)
		{
			yield return null;
		}
		base.FadeInHeroActor(enemyActor);
		yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_03_03.prefab:b014c684c85f1c440bed5560c6b6dbf5", "TUTORIAL04_HEMET_03", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
		base.FadeOutHeroActor(enemyActor);
		yield return new WaitForSeconds(0.3f);
		base.FadeInHeroActor(jainaActor);
		yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_04_JAINA_02_38.prefab:83b64d5eeb884db43b9fa5f20316da2c", "TUTORIAL04_JAINA_02", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
		base.FadeOutHeroActor(jainaActor);
		yield return new WaitForSeconds(0.25f);
		if (!base.DidLoseTutorial(TutorialProgress.NESINGWARY_COMPLETE))
		{
			base.FadeInHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL04_HEMET_04_04_ALT.prefab:bb3fadd78adce274993862115f3c5137", "TUTORIAL04_HEMET_04", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			base.FadeOutHeroActor(enemyActor);
			yield return new WaitForSeconds(0.4f);
		}
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06005218 RID: 21016 RVA: 0x001AF87A File Offset: 0x001ADA7A
	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		yield break;
	}

	// Token: 0x06005219 RID: 21017 RVA: 0x001AF88C File Offset: 0x001ADA8C
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

	// Token: 0x0600521A RID: 21018 RVA: 0x001AF8F6 File Offset: 0x001ADAF6
	private bool AllowEndTurn()
	{
		return !this.m_shouldSignalPolymorph || (this.m_shouldSignalPolymorph && this.m_isBogSheeped);
	}

	// Token: 0x0600521B RID: 21019 RVA: 0x001AF914 File Offset: 0x001ADB14
	public override bool NotifyOfEndTurnButtonPushed()
	{
		if (base.GetTag(GAME_TAG.TURN) != 4 && this.AllowEndTurn())
		{
			NotificationManager.Get().DestroyAllArrows();
			return true;
		}
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket != null && !optionsPacket.HasValidOption())
		{
			NotificationManager.Get().DestroyAllArrows();
			return true;
		}
		if (this.endTurnNotifier != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.endTurnNotifier);
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
		string key = "TUTORIAL_NO_ENDTURN_HP";
		if (GameState.Get().GetFriendlySidePlayer().HasReadyAttackers())
		{
			key = "TUTORIAL_NO_ENDTURN_ATK";
		}
		if (this.m_shouldSignalPolymorph && !this.m_isBogSheeped)
		{
			key = "TUTORIAL_NO_ENDTURN";
		}
		this.endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.endTurnNotifier, 2.5f);
		return false;
	}

	// Token: 0x0600521C RID: 21020 RVA: 0x001AFA17 File Offset: 0x001ADC17
	public override void NotifyOfTargetModeCancelled()
	{
		if (this.sheepTheBog == null)
		{
			return;
		}
		NotificationManager.Get().DestroyAllPopUps();
	}

	// Token: 0x0600521D RID: 21021 RVA: 0x001AFA34 File Offset: 0x001ADC34
	public override void NotifyOfCardGrabbed(Entity entity)
	{
		if (this.m_shouldSignalPolymorph)
		{
			if (entity.GetCardId() == "TU5_CS2_022")
			{
				this.m_isPolymorphGrabbed = true;
				if (this.sheepTheBog != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.sheepTheBog);
				}
				if (this.handBounceArrow != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.handBounceArrow);
				}
				NotificationManager.Get().DestroyAllPopUps();
				Vector3 position = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetFirstCard().transform.position;
				Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
				this.sheepTheBog = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_02"), true, NotificationManager.PopupTextType.BASIC);
				this.sheepTheBog.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				return;
			}
			if (this.sheepTheBog != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.sheepTheBog);
			}
			NotificationManager.Get().DestroyAllPopUps();
			if (UniversalInputManager.UsePhoneUI)
			{
				InputManager.Get().ReturnHeldCardToHand();
				return;
			}
			InputManager.Get().DropHeldCard();
		}
	}

	// Token: 0x0600521E RID: 21022 RVA: 0x001AFB68 File Offset: 0x001ADD68
	public override void NotifyOfCardDropped(Entity entity)
	{
		this.m_isPolymorphGrabbed = false;
		if (this.m_shouldSignalPolymorph)
		{
			if (this.sheepTheBog != null)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.sheepTheBog);
			}
			NotificationManager.Get().DestroyAllPopUps();
			if (this.ShouldShowArrowOnCardInHand(entity))
			{
				Gameplay.Get().StartCoroutine(this.ShowArrowInSeconds(0.5f));
			}
		}
	}

	// Token: 0x0600521F RID: 21023 RVA: 0x001AFBCC File Offset: 0x001ADDCC
	public override bool NotifyOfBattlefieldCardClicked(Entity clickedEntity, bool wasInTargetMode)
	{
		if (this.m_shouldSignalPolymorph)
		{
			if (clickedEntity.GetCardId() == "TU5_CS1_069" && wasInTargetMode)
			{
				if (this.sheepTheBog != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.sheepTheBog);
				}
				NotificationManager.Get().DestroyAllPopUps();
				this.m_shouldSignalPolymorph = false;
				this.m_isBogSheeped = true;
			}
			else
			{
				if (this.m_isPolymorphGrabbed && wasInTargetMode)
				{
					if (this.noSheepPopup != null)
					{
						NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.noSheepPopup);
					}
					Vector3 position = clickedEntity.GetCard().transform.position;
					Vector3 position2 = new Vector3(position.x + 2.5f, position.y, position.z);
					this.noSheepPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL04_HELP_03"), true, NotificationManager.PopupTextType.BASIC);
					NotificationManager.Get().DestroyNotification(this.noSheepPopup, 3f);
					return false;
				}
				return false;
			}
		}
		return true;
	}

	// Token: 0x06005220 RID: 21024 RVA: 0x001AFCCB File Offset: 0x001ADECB
	public override bool ShouldAllowCardGrab(Entity entity)
	{
		return !this.m_shouldSignalPolymorph || !(entity.GetCardId() != "TU5_CS2_022");
	}

	// Token: 0x06005221 RID: 21025 RVA: 0x001AFCEC File Offset: 0x001ADEEC
	private void ManaLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		GameObject costTextObject = ((Card)callbackData).GetActor().GetCostTextObject();
		if (costTextObject == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		this.m_heroPowerCostLabel = go;
		SceneUtils.SetLayer(go, GameLayer.Default);
		go.transform.parent = costTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(-0.02f, 0.38f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.x, go.transform.localScale.x);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_COST");
	}

	// Token: 0x06005222 RID: 21026 RVA: 0x001AFDEB File Offset: 0x001ADFEB
	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (this.ShouldShowArrowOnCardInHand(mousedOverEntity))
		{
			NotificationManager.Get().DestroyAllArrows();
		}
	}

	// Token: 0x06005223 RID: 21027 RVA: 0x001AFE00 File Offset: 0x001AE000
	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (this.ShouldShowArrowOnCardInHand(mousedOffEntity))
		{
			Gameplay.Get().StartCoroutine(this.ShowArrowInSeconds(0.5f));
		}
	}

	// Token: 0x06005224 RID: 21028 RVA: 0x001AFE21 File Offset: 0x001AE021
	private bool ShouldShowArrowOnCardInHand(Entity entity)
	{
		return entity.GetZone() == TAG_ZONE.HAND && (this.m_shouldSignalPolymorph && entity.GetCardId() == "TU5_CS2_022");
	}

	// Token: 0x06005225 RID: 21029 RVA: 0x001AFE4B File Offset: 0x001AE04B
	private IEnumerator ShowArrowInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0 || this.m_isPolymorphGrabbed)
		{
			yield break;
		}
		Card polyMorph = null;
		foreach (Card card in cards)
		{
			if (card.GetEntity().GetCardId() == "TU5_CS2_022")
			{
				polyMorph = card;
			}
		}
		if (polyMorph == null)
		{
			yield break;
		}
		while (iTween.Count(polyMorph.gameObject) > 0)
		{
			yield return null;
		}
		if (polyMorph.IsMousedOver())
		{
			yield break;
		}
		if (InputManager.Get().GetHeldCard() == polyMorph)
		{
			yield break;
		}
		this.ShowHandBouncingArrow();
		yield break;
	}

	// Token: 0x06005226 RID: 21030 RVA: 0x001AFE64 File Offset: 0x001AE064
	private void ShowHandBouncingArrow()
	{
		if (this.handBounceArrow != null)
		{
			return;
		}
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0)
		{
			return;
		}
		Card card = null;
		foreach (Card card2 in cards)
		{
			if (card2.GetEntity().GetCardId() == "TU5_CS2_022")
			{
				card = card2;
			}
		}
		if (card == null)
		{
			return;
		}
		if (this.m_isPolymorphGrabbed)
		{
			return;
		}
		Vector3 position = card.transform.position;
		Vector3 position2 = new Vector3(position.x, position.y, position.z + 2f);
		this.handBounceArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, 0f, 0f));
		this.handBounceArrow.transform.parent = card.transform;
	}

	// Token: 0x06005227 RID: 21031 RVA: 0x001AFF74 File Offset: 0x001AE174
	public override void NotifyOfDefeatCoinAnimation()
	{
		if (!this.victory)
		{
			return;
		}
		base.PlaySound("VO_TUTORIAL_04_JAINA_10_44.prefab:6f5921db1071ead4585c8cc9689d22ea", 1f, true, false);
	}

	// Token: 0x06005228 RID: 21032 RVA: 0x001AFF94 File Offset: 0x001AE194
	public override List<RewardData> GetCustomRewards()
	{
		if (!this.victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_213", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}

	// Token: 0x0400495E RID: 18782
	private static Map<GameEntityOption, bool> s_booleanOptions = Tutorial_04.InitBooleanOptions();

	// Token: 0x0400495F RID: 18783
	private static Map<GameEntityOption, string> s_stringOptions = Tutorial_04.InitStringOptions();

	// Token: 0x04004960 RID: 18784
	private Notification endTurnNotifier;

	// Token: 0x04004961 RID: 18785
	private Notification thatsABadPlayPopup;

	// Token: 0x04004962 RID: 18786
	private Notification handBounceArrow;

	// Token: 0x04004963 RID: 18787
	private Notification sheepTheBog;

	// Token: 0x04004964 RID: 18788
	private Notification noSheepPopup;

	// Token: 0x04004965 RID: 18789
	private int numBeastsPlayed;

	// Token: 0x04004966 RID: 18790
	private GameObject m_heroPowerCostLabel;

	// Token: 0x04004967 RID: 18791
	private Notification heroPowerHelp;

	// Token: 0x04004968 RID: 18792
	private bool victory;

	// Token: 0x04004969 RID: 18793
	private bool m_hemetSpeaking;

	// Token: 0x0400496A RID: 18794
	private int numComplaintsMade;

	// Token: 0x0400496B RID: 18795
	private bool m_shouldSignalPolymorph;

	// Token: 0x0400496C RID: 18796
	private bool m_isPolymorphGrabbed;

	// Token: 0x0400496D RID: 18797
	private bool m_isBogSheeped;

	// Token: 0x0400496E RID: 18798
	private bool m_playOneHealthCommentNextTurn;
}
