using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D9 RID: 1497
public class Tutorial_02 : TutorialEntity
{
	// Token: 0x060051E1 RID: 20961 RVA: 0x001AE92C File Offset: 0x001ACB2C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_02_05.prefab:d1334881818e67d4c85216afa56226d6");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_01_04.prefab:5b48a6d28da46464ea99c7b278f63226");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_04_07.prefab:a804332a9a314af49b35d1c6d4a1f306");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_05_08.prefab:946dc71f989978844af5222d4342df4c");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_07_10.prefab:ffdc387467735484390ee8545698c57e");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_11_14.prefab:ada9c4aef7cd8dc418005c0a4c5f578d");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_13_16.prefab:80757414dc5a3b54b9cfc328ce2b7f6c");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_15_17.prefab:973e26c00c354b24595965035e8efba7");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_06_09.prefab:04bd4efe66a93bb438327216a4254560");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_03_06.prefab:c509f7e0eca4fb84dbf9be77a7ed5823");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_17_19.prefab:a7aab1a8c3e6d304a9b6f451187fdb00");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_08_11.prefab:21d83afbda98c8844b0ba771b14833e7");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_09_12.prefab:a050db78c641ba04d88382e2b759dbac");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_10_13.prefab:a22defa2f9b5ec242a1f4e502d9349eb");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_16_18.prefab:2493cb5abcdbf45468d74ab4ab4c10f8");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962");
		base.PreloadSound("VO_TUTORIAL_02_JAINA_08_22.prefab:52cd86a7a20daeb4b8d1f3fd2647e9ea");
		base.PreloadSound("VO_TUTORIAL_02_JAINA_03_18.prefab:4942e6b39e0bf0747b0ad09944cf9ad2");
		base.PreloadSound("VO_TUTORIAL02_MILLHOUSE_19_21.prefab:bc8b4236bf74f1244afa49a8195c7f74");
	}

	// Token: 0x060051E2 RID: 20962 RVA: 0x001AEA0A File Offset: 0x001ACC0A
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.SetTutorialProgress(TutorialProgress.MILLHOUSE_COMPLETE);
			base.PlaySound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.TIED)
		{
			base.PlaySound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962", 1f, true, false);
		}
	}

	// Token: 0x060051E3 RID: 20963 RVA: 0x001AEA47 File Offset: 0x001ACC47
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (GameState.Get().IsFriendlySidePlayerTurn())
		{
			this.numManaThisTurn++;
		}
		Actor millhouseActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		AudioSource previousLine;
		AudioSource comeOnLine;
		switch (turn)
		{
		case 1:
		{
			Vector3 manaCrystalSpawnPosition = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
			Vector3 position;
			Notification.PopUpArrowDirection direction;
			if (UniversalInputManager.UsePhoneUI)
			{
				position = new Vector3(manaCrystalSpawnPosition.x - 0.7f, manaCrystalSpawnPosition.y + 1.14f, manaCrystalSpawnPosition.z + 4.33f);
				direction = Notification.PopUpArrowDirection.RightDown;
			}
			else
			{
				position = new Vector3(manaCrystalSpawnPosition.x - 0.02f, manaCrystalSpawnPosition.y + 0.2f, manaCrystalSpawnPosition.z + 1.8f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			this.manaNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL02_HELP_01"), true, NotificationManager.PopupTextType.BASIC);
			this.manaNotifier.ShowPopUpArrow(direction);
			yield return new WaitForSeconds(4.5f);
			if (this.manaNotifier != null)
			{
				iTween.PunchScale(this.manaNotifier.gameObject, iTween.Hash(new object[]
				{
					"amount",
					new Vector3(1f, 1f, 1f),
					"time",
					1f
				}));
				yield return new WaitForSeconds(4.5f);
				if (this.manaNotifier != null)
				{
					iTween.PunchScale(this.manaNotifier.gameObject, iTween.Hash(new object[]
					{
						"amount",
						new Vector3(1f, 1f, 1f),
						"time",
						1f
					}));
					yield return new WaitForSeconds(4.5f);
					if (this.manaNotifier != null)
					{
						NotificationManager.Get().DestroyNotification(this.manaNotifier, 0f);
					}
				}
			}
			break;
		}
		case 2:
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_04_07.prefab:a804332a9a314af49b35d1c6d4a1f306", "TUTORIAL02_MILLHOUSE_04", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			yield return new WaitForSeconds(0.3f);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_05_08.prefab:946dc71f989978844af5222d4342df4c", "TUTORIAL02_MILLHOUSE_05", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 3:
		{
			Vector3 manaCrystalSpawnPosition2 = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
			Vector3 position2;
			Notification.PopUpArrowDirection direction2;
			if (UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(manaCrystalSpawnPosition2.x - 0.7f, manaCrystalSpawnPosition2.y + 1.14f, manaCrystalSpawnPosition2.z + 4.33f);
				direction2 = Notification.PopUpArrowDirection.RightDown;
			}
			else
			{
				position2 = new Vector3(manaCrystalSpawnPosition2.x - 0.02f, manaCrystalSpawnPosition2.y + 0.2f, manaCrystalSpawnPosition2.z + 1.7f);
				direction2 = Notification.PopUpArrowDirection.Down;
			}
			this.manaNotifier2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL02_HELP_03"), true, NotificationManager.PopupTextType.BASIC);
			this.manaNotifier2.ShowPopUpArrow(direction2);
			yield return new WaitForSeconds(4.5f);
			if (this.manaNotifier2 != null)
			{
				iTween.PunchScale(this.manaNotifier2.gameObject, iTween.Hash(new object[]
				{
					"amount",
					new Vector3(1f, 1f, 1f),
					"time",
					1f
				}));
				yield return new WaitForSeconds(4.5f);
				if (this.manaNotifier2 != null)
				{
					iTween.PunchScale(this.manaNotifier2.gameObject, iTween.Hash(new object[]
					{
						"amount",
						new Vector3(1f, 1f, 1f),
						"time",
						1f
					}));
				}
			}
			break;
		}
		case 4:
			if (this.manaNotifier2 != null)
			{
				NotificationManager.Get().DestroyNotification(this.manaNotifier2, 0f);
			}
			GameState.Get().SetBusy(true);
			previousLine = base.GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_17_19.prefab:a7aab1a8c3e6d304a9b6f451187fdb00");
			while (SoundManager.Get().IsPlaying(previousLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_07_10.prefab:ffdc387467735484390ee8545698c57e", "TUTORIAL02_MILLHOUSE_07", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 6:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_11_14.prefab:ada9c4aef7cd8dc418005c0a4c5f578d", "TUTORIAL02_MILLHOUSE_11", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 8:
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_13_16.prefab:80757414dc5a3b54b9cfc328ce2b7f6c", "TUTORIAL02_MILLHOUSE_13", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 9:
			yield return new WaitForSeconds(0.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_15_17.prefab:973e26c00c354b24595965035e8efba7", "TUTORIAL02_MILLHOUSE_15", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			break;
		case 10:
			GameState.Get().SetBusy(true);
			comeOnLine = base.GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_16_18.prefab:2493cb5abcdbf45468d74ab4ab4c10f8");
			while (SoundManager.Get().IsPlaying(comeOnLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_06_09.prefab:04bd4efe66a93bb438327216a4254560", "TUTORIAL02_MILLHOUSE_06", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		}
		previousLine = null;
		comeOnLine = null;
		yield break;
	}

	// Token: 0x060051E4 RID: 20964 RVA: 0x001AEA5D File Offset: 0x001ACC5D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor millhouseActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		AudioSource feelslikeLine;
		AudioSource whatLine;
		AudioSource winngingLine;
		switch (missionEvent)
		{
		case 1:
			base.HandleGameStartEvent();
			break;
		case 2:
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1.5f);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_03_06.prefab:c509f7e0eca4fb84dbf9be77a7ed5823", "TUTORIAL02_MILLHOUSE_03", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			yield return new WaitForSeconds(4f);
			if (base.GetTag(GAME_TAG.TURN) == 1 && !EndTurnButton.Get().IsInWaitingState())
			{
				this.ShowEndTurnBouncingArrow();
			}
			break;
		case 3:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_17_19.prefab:a7aab1a8c3e6d304a9b6f451187fdb00", "TUTORIAL02_MILLHOUSE_17", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			break;
		case 4:
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_08_11.prefab:21d83afbda98c8844b0ba771b14833e7", "TUTORIAL02_MILLHOUSE_08", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_02_JAINA_03_18.prefab:4942e6b39e0bf0747b0ad09944cf9ad2", "TUTORIAL02_JAINA_03", Notification.SpeechBubbleDirection.BottomLeft, jainaActor, 1f, true, false, 3f, 0f));
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_09_12.prefab:a050db78c641ba04d88382e2b759dbac", "TUTORIAL02_MILLHOUSE_09", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			break;
		case 5:
			GameState.Get().SetBusy(true);
			feelslikeLine = base.GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_08_11.prefab:21d83afbda98c8844b0ba771b14833e7");
			while (SoundManager.Get().IsPlaying(feelslikeLine))
			{
				yield return null;
			}
			whatLine = base.GetPreloadedSound("VO_TUTORIAL_02_JAINA_03_18.prefab:4942e6b39e0bf0747b0ad09944cf9ad2");
			while (SoundManager.Get().IsPlaying(whatLine))
			{
				yield return null;
			}
			winngingLine = base.GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_09_12.prefab:a050db78c641ba04d88382e2b759dbac");
			while (SoundManager.Get().IsPlaying(winngingLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_10_13.prefab:a22defa2f9b5ec242a1f4e502d9349eb", "TUTORIAL02_MILLHOUSE_10", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 6:
			if (EndTurnButton.Get().IsInNMPState())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_16_18.prefab:2493cb5abcdbf45468d74ab4ab4c10f8", "TUTORIAL02_MILLHOUSE_16", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
			}
			break;
		default:
			if (missionEvent != 54)
			{
				if (missionEvent == 55)
				{
					base.FadeInHeroActor(millhouseActor);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_02_05.prefab:d1334881818e67d4c85216afa56226d6", "TUTORIAL02_MILLHOUSE_02", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
					HistoryManager.Get().DisableHistory();
					MulliganManager.Get().BeginMulligan();
					yield return new WaitForSeconds(1.1f);
					base.FadeOutHeroActor(millhouseActor);
				}
			}
			else
			{
				yield return new WaitForSeconds(2f);
				base.ShowTutorialDialog("TUTORIAL02_HELP_06", "TUTORIAL02_HELP_07", "TUTORIAL01_HELP_16", new Vector2(0.5f, 0f), false);
			}
			break;
		}
		feelslikeLine = null;
		whatLine = null;
		winngingLine = null;
		yield break;
	}

	// Token: 0x060051E5 RID: 20965 RVA: 0x001AEA73 File Offset: 0x001ACC73
	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (mousedOverEntity.GetZone() == TAG_ZONE.HAND && base.GetTag(GAME_TAG.TURN) <= 7)
		{
			AssetLoader.Get().InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", new PrefabCallback<GameObject>(this.ManaLabelLoadedCallback), mousedOverEntity.GetCard(), AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	// Token: 0x060051E6 RID: 20966 RVA: 0x001AEAB1 File Offset: 0x001ACCB1
	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (this.costLabel != null)
		{
			UnityEngine.Object.Destroy(this.costLabel);
		}
	}

	// Token: 0x060051E7 RID: 20967 RVA: 0x001AEACC File Offset: 0x001ACCCC
	public override void NotifyOfCoinFlipResult()
	{
		Gameplay.Get().StartCoroutine(this.HandleCoinFlip());
	}

	// Token: 0x060051E8 RID: 20968 RVA: 0x001AEADF File Offset: 0x001ACCDF
	private IEnumerator HandleCoinFlip()
	{
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(3.5f);
		Actor millhouseActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		base.FadeInHeroActor(millhouseActor);
		yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_01_04.prefab:5b48a6d28da46464ea99c7b278f63226", "TUTORIAL02_MILLHOUSE_01", Notification.SpeechBubbleDirection.TopRight, millhouseActor, 1f, true, false, 3f, 0f));
		GameState.Get().SetBusy(false);
		yield return new WaitForSeconds(0.175f);
		base.FadeOutHeroActor(millhouseActor);
		yield break;
	}

	// Token: 0x060051E9 RID: 20969 RVA: 0x001AEAF0 File Offset: 0x001ACCF0
	public override bool NotifyOfEndTurnButtonPushed()
	{
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket != null)
		{
			if (!optionsPacket.HasValidOption())
			{
				NotificationManager.Get().DestroyAllArrows();
				return true;
			}
			bool flag = false;
			for (int i = 0; i < optionsPacket.List.Count; i++)
			{
				Network.Options.Option option = optionsPacket.List[i];
				if (option.Main.PlayErrorInfo.IsValid() && option.Type == Network.Options.Option.OptionType.POWER && !(GameState.Get().GetEntity(option.Main.ID).GetCardId() == "TU5_CS2_025"))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return true;
			}
		}
		if (this.endTurnNotifier != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.endTurnNotifier);
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
		string key = "TUTORIAL_NO_ENDTURN";
		if (GameState.Get().GetFriendlySidePlayer().HasReadyAttackers())
		{
			key = "TUTORIAL_NO_ENDTURN_ATK";
		}
		this.endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.endTurnNotifier, 2.5f);
		return false;
	}

	// Token: 0x060051EA RID: 20970 RVA: 0x001AEC3C File Offset: 0x001ACE3C
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

	// Token: 0x060051EB RID: 20971 RVA: 0x001AECA8 File Offset: 0x001ACEA8
	public override string[] NotifyOfKeywordHelpPanelDisplay(Entity entity)
	{
		if (entity.GetCardId() == "CS2_122")
		{
			return new string[]
			{
				GameStrings.Get("TUTORIAL_RAID_LEADER_HEADLINE"),
				GameStrings.Get("TUTORIAL_RAID_LEADER_DESCRIPTION")
			};
		}
		if (entity.GetCardId() == "TU5_CS2_023")
		{
			return new string[]
			{
				GameStrings.Get("TUTORIAL_ARCANE_INTELLECT_HEADLINE"),
				GameStrings.Get("TUTORIAL_ARCANE_INTELLECT_DESCRIPTION")
			};
		}
		return null;
	}

	// Token: 0x060051EC RID: 20972 RVA: 0x001AED1C File Offset: 0x001ACF1C
	public override void NotifyOfCardGrabbed(Entity entity)
	{
		if (entity.GetCardId() == "TU5_CS2_023" && GameState.Get().GetFriendlySidePlayer().GetNumAvailableResources() >= entity.GetCost())
		{
			BoardTutorial.Get().EnableFullHighlight(true);
		}
		if (this.costLabel != null)
		{
			UnityEngine.Object.Destroy(this.costLabel);
		}
	}

	// Token: 0x060051ED RID: 20973 RVA: 0x001AED76 File Offset: 0x001ACF76
	public override void NotifyOfCardDropped(Entity entity)
	{
		if (entity.GetCardId() == "TU5_CS2_023")
		{
			BoardTutorial.Get().EnableFullHighlight(false);
		}
	}

	// Token: 0x060051EE RID: 20974 RVA: 0x001AED98 File Offset: 0x001ACF98
	public override void NotifyOfManaCrystalSpawned()
	{
		AssetLoader.Get().InstantiatePrefab("plus1.prefab:7427d28c07eea8645a3308e04398ee30", new PrefabCallback<GameObject>(this.Plus1ActorLoadedCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		if (base.GetTag(GAME_TAG.TURN) == 3)
		{
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_02_JAINA_08_22.prefab:52cd86a7a20daeb4b8d1f3fd2647e9ea", "TUTORIAL02_JAINA_08", Notification.SpeechBubbleDirection.BottomLeft, actor, 1f, true, false, 3f, 0f));
		}
		this.FadeInManaSpotlight();
	}

	// Token: 0x060051EF RID: 20975 RVA: 0x001AEE21 File Offset: 0x001AD021
	private void FadeInManaSpotlight()
	{
		Gameplay.Get().StartCoroutine(this.StartManaSpotFade());
	}

	// Token: 0x060051F0 RID: 20976 RVA: 0x001AEE34 File Offset: 0x001AD034
	private IEnumerator StartManaSpotFade()
	{
		Light manaSpot = BoardTutorial.Get().m_ManaSpotlight;
		manaSpot.enabled = true;
		manaSpot.spotAngle = 179f;
		manaSpot.intensity = 0f;
		float TARGET_INTENSITY = 0.6f;
		while (manaSpot.intensity < TARGET_INTENSITY * 0.95f)
		{
			manaSpot.intensity = Mathf.Lerp(manaSpot.intensity, TARGET_INTENSITY, Time.deltaTime * 5f);
			manaSpot.spotAngle = Mathf.Lerp(manaSpot.spotAngle, 80f, Time.deltaTime * 5f);
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		while (manaSpot.intensity > 0.05f)
		{
			manaSpot.intensity = Mathf.Lerp(manaSpot.intensity, 0f, Time.deltaTime * 10f);
			yield return null;
		}
		manaSpot.enabled = false;
		yield break;
	}

	// Token: 0x060051F1 RID: 20977 RVA: 0x001AEE3C File Offset: 0x001AD03C
	private void Plus1ActorLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		Vector3 position = SceneUtils.FindChildBySubstring(Board.Get().gameObject, "ManaCounter_Friendly").transform.position;
		Vector3 vector = new Vector3(position.x - 0.02f, position.y + 0.2f, position.z);
		go.transform.position = vector;
		go.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		Vector3 localScale = go.transform.localScale;
		go.transform.localScale = new Vector3(1f, 1f, 1f);
		iTween.MoveTo(go, new Vector3(vector.x, vector.y, vector.z + 2f), 3f);
		float num = 2.5f;
		iTween.ScaleTo(go, new Vector3(localScale.x * num, localScale.y * num, localScale.z * num), 3f);
		iTween.RotateTo(go, new Vector3(0f, 170f, 0f), 3f);
		iTween.FadeTo(go, 0f, 2.75f);
	}

	// Token: 0x060051F2 RID: 20978 RVA: 0x001AEF68 File Offset: 0x001AD168
	public override void NotifyOfEnemyManaCrystalSpawned()
	{
		AssetLoader.Get().InstantiatePrefab("plus1.prefab:7427d28c07eea8645a3308e04398ee30", new PrefabCallback<GameObject>(this.Plus1ActorLoadedCallbackEnemy), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060051F3 RID: 20979 RVA: 0x001AEF90 File Offset: 0x001AD190
	private void Plus1ActorLoadedCallbackEnemy(AssetReference assetRef, GameObject go, object callbackData)
	{
		Vector3 position = SceneUtils.FindChildBySubstring(Board.Get().gameObject, "ManaCounter_Opposing").transform.position;
		Vector3 vector = new Vector3(position.x, position.y + 0.2f, position.z);
		go.transform.position = vector;
		go.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		Vector3 localScale = go.transform.localScale;
		go.transform.localScale = new Vector3(1f, 1f, 1f);
		iTween.MoveTo(go, new Vector3(vector.x, vector.y, vector.z - 2f), 3f);
		float num = 2.5f;
		iTween.ScaleTo(go, new Vector3(localScale.x * num, localScale.y * num, localScale.z * num), 3f);
		iTween.RotateTo(go, new Vector3(0f, 170f, 0f), 3f);
		iTween.FadeTo(go, 0f, 2.75f);
	}

	// Token: 0x060051F4 RID: 20980 RVA: 0x001AF0B8 File Offset: 0x001AD2B8
	private void ManaLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		GameObject costTextObject = ((Card)callbackData).GetActor().GetCostTextObject();
		if (costTextObject == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		if (this.costLabel != null)
		{
			UnityEngine.Object.Destroy(this.costLabel);
		}
		this.costLabel = go;
		go.transform.parent = costTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(-0.025f, 0.28f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_COST");
	}

	// Token: 0x060051F5 RID: 20981 RVA: 0x001AF18C File Offset: 0x001AD38C
	public override void NotifyOfTooltipZoneMouseOver(TooltipZone tooltip)
	{
		if (tooltip.targetObject.GetComponent<ManaCrystalMgr>() != null)
		{
			if (this.manaNotifier != null)
			{
				UnityEngine.Object.Destroy(this.manaNotifier.gameObject);
			}
			if (this.manaNotifier2 != null)
			{
				UnityEngine.Object.Destroy(this.manaNotifier2.gameObject);
			}
		}
	}

	// Token: 0x060051F6 RID: 20982 RVA: 0x001AF1E8 File Offset: 0x001AD3E8
	public override string GetTurnStartReminderText()
	{
		return GameStrings.Format("TUTORIAL02_HELP_04", new object[]
		{
			this.numManaThisTurn
		});
	}

	// Token: 0x060051F7 RID: 20983 RVA: 0x001AF208 File Offset: 0x001AD408
	public override void NotifyOfDefeatCoinAnimation()
	{
		Gameplay.Get().StartCoroutine(this.PlayGoingOutSound());
	}

	// Token: 0x060051F8 RID: 20984 RVA: 0x001AF21B File Offset: 0x001AD41B
	private IEnumerator PlayGoingOutSound()
	{
		AudioSource deathLine = base.GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962");
		while (deathLine != null && deathLine.isPlaying)
		{
			yield return null;
		}
		base.PlaySound("VO_TUTORIAL02_MILLHOUSE_19_21.prefab:bc8b4236bf74f1244afa49a8195c7f74", 1f, true, false);
		yield break;
	}

	// Token: 0x060051F9 RID: 20985 RVA: 0x001AF22A File Offset: 0x001AD42A
	protected override void NotifyOfManaError()
	{
		NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.manaNotifier);
		NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.manaNotifier2);
	}

	// Token: 0x060051FA RID: 20986 RVA: 0x001AF24C File Offset: 0x001AD44C
	public override List<RewardData> GetCustomRewards()
	{
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("EX1_015", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}

	// Token: 0x0400494D RID: 18765
	private Notification endTurnNotifier;

	// Token: 0x0400494E RID: 18766
	private Notification manaNotifier;

	// Token: 0x0400494F RID: 18767
	private Notification manaNotifier2;

	// Token: 0x04004950 RID: 18768
	private Notification handBounceArrow;

	// Token: 0x04004951 RID: 18769
	private GameObject costLabel;

	// Token: 0x04004952 RID: 18770
	private int numManaThisTurn = 1;
}
