using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_02 : TutorialEntity
{
	private Notification endTurnNotifier;

	private Notification manaNotifier;

	private Notification manaNotifier2;

	private Notification handBounceArrow;

	private GameObject costLabel;

	private int numManaThisTurn = 1;

	public override void PreloadAssets()
	{
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_02_05.prefab:d1334881818e67d4c85216afa56226d6");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_01_04.prefab:5b48a6d28da46464ea99c7b278f63226");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_04_07.prefab:a804332a9a314af49b35d1c6d4a1f306");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_05_08.prefab:946dc71f989978844af5222d4342df4c");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_07_10.prefab:ffdc387467735484390ee8545698c57e");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_11_14.prefab:ada9c4aef7cd8dc418005c0a4c5f578d");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_13_16.prefab:80757414dc5a3b54b9cfc328ce2b7f6c");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_15_17.prefab:973e26c00c354b24595965035e8efba7");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_06_09.prefab:04bd4efe66a93bb438327216a4254560");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_03_06.prefab:c509f7e0eca4fb84dbf9be77a7ed5823");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_17_19.prefab:a7aab1a8c3e6d304a9b6f451187fdb00");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_08_11.prefab:21d83afbda98c8844b0ba771b14833e7");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_09_12.prefab:a050db78c641ba04d88382e2b759dbac");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_10_13.prefab:a22defa2f9b5ec242a1f4e502d9349eb");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_16_18.prefab:2493cb5abcdbf45468d74ab4ab4c10f8");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962");
		PreloadSound("VO_TUTORIAL_02_JAINA_08_22.prefab:52cd86a7a20daeb4b8d1f3fd2647e9ea");
		PreloadSound("VO_TUTORIAL_02_JAINA_03_18.prefab:4942e6b39e0bf0747b0ad09944cf9ad2");
		PreloadSound("VO_TUTORIAL02_MILLHOUSE_19_21.prefab:bc8b4236bf74f1244afa49a8195c7f74");
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			SetTutorialProgress(TutorialProgress.MILLHOUSE_COMPLETE);
			PlaySound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962");
			break;
		case TAG_PLAYSTATE.TIED:
			PlaySound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962");
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (GameState.Get().IsFriendlySidePlayerTurn())
		{
			numManaThisTurn++;
		}
		Actor millhouseActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
		{
			Vector3 manaCrystalSpawnPosition2 = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
			Vector3 position2;
			Notification.PopUpArrowDirection direction2;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(manaCrystalSpawnPosition2.x - 0.7f, manaCrystalSpawnPosition2.y + 1.14f, manaCrystalSpawnPosition2.z + 4.33f);
				direction2 = Notification.PopUpArrowDirection.RightDown;
			}
			else
			{
				position2 = new Vector3(manaCrystalSpawnPosition2.x - 0.02f, manaCrystalSpawnPosition2.y + 0.2f, manaCrystalSpawnPosition2.z + 1.8f);
				direction2 = Notification.PopUpArrowDirection.Down;
			}
			manaNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL02_HELP_01"));
			manaNotifier.ShowPopUpArrow(direction2);
			yield return new WaitForSeconds(4.5f);
			if (!(manaNotifier != null))
			{
				break;
			}
			iTween.PunchScale(manaNotifier.gameObject, iTween.Hash("amount", new Vector3(1f, 1f, 1f), "time", 1f));
			yield return new WaitForSeconds(4.5f);
			if (manaNotifier != null)
			{
				iTween.PunchScale(manaNotifier.gameObject, iTween.Hash("amount", new Vector3(1f, 1f, 1f), "time", 1f));
				yield return new WaitForSeconds(4.5f);
				if (manaNotifier != null)
				{
					NotificationManager.Get().DestroyNotification(manaNotifier, 0f);
				}
			}
			break;
		}
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(0.5f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_04_07.prefab:a804332a9a314af49b35d1c6d4a1f306", "TUTORIAL02_MILLHOUSE_04", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			yield return new WaitForSeconds(0.3f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_05_08.prefab:946dc71f989978844af5222d4342df4c", "TUTORIAL02_MILLHOUSE_05", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 3:
		{
			Vector3 manaCrystalSpawnPosition = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
			Vector3 position;
			Notification.PopUpArrowDirection direction;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position = new Vector3(manaCrystalSpawnPosition.x - 0.7f, manaCrystalSpawnPosition.y + 1.14f, manaCrystalSpawnPosition.z + 4.33f);
				direction = Notification.PopUpArrowDirection.RightDown;
			}
			else
			{
				position = new Vector3(manaCrystalSpawnPosition.x - 0.02f, manaCrystalSpawnPosition.y + 0.2f, manaCrystalSpawnPosition.z + 1.7f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			manaNotifier2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL02_HELP_03"));
			manaNotifier2.ShowPopUpArrow(direction);
			yield return new WaitForSeconds(4.5f);
			if (manaNotifier2 != null)
			{
				iTween.PunchScale(manaNotifier2.gameObject, iTween.Hash("amount", new Vector3(1f, 1f, 1f), "time", 1f));
				yield return new WaitForSeconds(4.5f);
				if (manaNotifier2 != null)
				{
					iTween.PunchScale(manaNotifier2.gameObject, iTween.Hash("amount", new Vector3(1f, 1f, 1f), "time", 1f));
				}
			}
			break;
		}
		case 4:
		{
			if (manaNotifier2 != null)
			{
				NotificationManager.Get().DestroyNotification(manaNotifier2, 0f);
			}
			GameState.Get().SetBusy(busy: true);
			AudioSource previousLine = GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_17_19.prefab:a7aab1a8c3e6d304a9b6f451187fdb00");
			while (SoundManager.Get().IsPlaying(previousLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_07_10.prefab:ffdc387467735484390ee8545698c57e", "TUTORIAL02_MILLHOUSE_07", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 6:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_11_14.prefab:ada9c4aef7cd8dc418005c0a4c5f578d", "TUTORIAL02_MILLHOUSE_11", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_13_16.prefab:80757414dc5a3b54b9cfc328ce2b7f6c", "TUTORIAL02_MILLHOUSE_13", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 9:
			yield return new WaitForSeconds(0.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_15_17.prefab:973e26c00c354b24595965035e8efba7", "TUTORIAL02_MILLHOUSE_15", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			break;
		case 10:
		{
			GameState.Get().SetBusy(busy: true);
			AudioSource comeOnLine = GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_16_18.prefab:2493cb5abcdbf45468d74ab4ab4c10f8");
			while (SoundManager.Get().IsPlaying(comeOnLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_06_09.prefab:04bd4efe66a93bb438327216a4254560", "TUTORIAL02_MILLHOUSE_06", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			break;
		}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor millhouseActor = GameState.Get().GetOpposingSidePlayer().GetHero()
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
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(1.5f);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_03_06.prefab:c509f7e0eca4fb84dbf9be77a7ed5823", "TUTORIAL02_MILLHOUSE_03", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			yield return new WaitForSeconds(4f);
			if (GetTag(GAME_TAG.TURN) == 1 && !EndTurnButton.Get().IsInWaitingState())
			{
				ShowEndTurnBouncingArrow();
			}
			break;
		case 3:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_17_19.prefab:a7aab1a8c3e6d304a9b6f451187fdb00", "TUTORIAL02_MILLHOUSE_17", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			break;
		case 4:
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_08_11.prefab:21d83afbda98c8844b0ba771b14833e7", "TUTORIAL02_MILLHOUSE_08", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_02_JAINA_03_18.prefab:4942e6b39e0bf0747b0ad09944cf9ad2", "TUTORIAL02_JAINA_03", Notification.SpeechBubbleDirection.BottomLeft, jainaActor));
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_09_12.prefab:a050db78c641ba04d88382e2b759dbac", "TUTORIAL02_MILLHOUSE_09", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			break;
		case 5:
		{
			GameState.Get().SetBusy(busy: true);
			AudioSource feelslikeLine = GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_08_11.prefab:21d83afbda98c8844b0ba771b14833e7");
			while (SoundManager.Get().IsPlaying(feelslikeLine))
			{
				yield return null;
			}
			AudioSource whatLine = GetPreloadedSound("VO_TUTORIAL_02_JAINA_03_18.prefab:4942e6b39e0bf0747b0ad09944cf9ad2");
			while (SoundManager.Get().IsPlaying(whatLine))
			{
				yield return null;
			}
			AudioSource winngingLine = GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_09_12.prefab:a050db78c641ba04d88382e2b759dbac");
			while (SoundManager.Get().IsPlaying(winngingLine))
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_10_13.prefab:a22defa2f9b5ec242a1f4e502d9349eb", "TUTORIAL02_MILLHOUSE_10", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 6:
			if (EndTurnButton.Get().IsInNMPState())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_16_18.prefab:2493cb5abcdbf45468d74ab4ab4c10f8", "TUTORIAL02_MILLHOUSE_16", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			}
			break;
		case 54:
			yield return new WaitForSeconds(2f);
			ShowTutorialDialog("TUTORIAL02_HELP_06", "TUTORIAL02_HELP_07", "TUTORIAL01_HELP_16", new Vector2(0.5f, 0f));
			break;
		case 55:
			FadeInHeroActor(millhouseActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_02_05.prefab:d1334881818e67d4c85216afa56226d6", "TUTORIAL02_MILLHOUSE_02", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
			HistoryManager.Get().DisableHistory();
			MulliganManager.Get().BeginMulligan();
			yield return new WaitForSeconds(1.1f);
			FadeOutHeroActor(millhouseActor);
			break;
		}
	}

	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (mousedOverEntity.GetZone() == TAG_ZONE.HAND && GetTag(GAME_TAG.TURN) <= 7)
		{
			AssetLoader.Get().InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", ManaLabelLoadedCallback, mousedOverEntity.GetCard(), AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (costLabel != null)
		{
			Object.Destroy(costLabel);
		}
	}

	public override void NotifyOfCoinFlipResult()
	{
		Gameplay.Get().StartCoroutine(HandleCoinFlip());
	}

	private IEnumerator HandleCoinFlip()
	{
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(3.5f);
		Actor millhouseActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		FadeInHeroActor(millhouseActor);
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL02_MILLHOUSE_01_04.prefab:5b48a6d28da46464ea99c7b278f63226", "TUTORIAL02_MILLHOUSE_01", Notification.SpeechBubbleDirection.TopRight, millhouseActor));
		GameState.Get().SetBusy(busy: false);
		yield return new WaitForSeconds(0.175f);
		FadeOutHeroActor(millhouseActor);
	}

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
		if (endTurnNotifier != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(endTurnNotifier);
		}
		Vector3 position = EndTurnButton.Get().transform.position;
		Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
		string key = "TUTORIAL_NO_ENDTURN";
		if (GameState.Get().GetFriendlySidePlayer().HasReadyAttackers())
		{
			key = "TUTORIAL_NO_ENDTURN_ATK";
		}
		endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		NotificationManager.Get().DestroyNotification(endTurnNotifier, 2.5f);
		return false;
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

	public override string[] NotifyOfKeywordHelpPanelDisplay(Entity entity)
	{
		if (entity.GetCardId() == "CS2_122")
		{
			return new string[2]
			{
				GameStrings.Get("TUTORIAL_RAID_LEADER_HEADLINE"),
				GameStrings.Get("TUTORIAL_RAID_LEADER_DESCRIPTION")
			};
		}
		if (entity.GetCardId() == "TU5_CS2_023")
		{
			return new string[2]
			{
				GameStrings.Get("TUTORIAL_ARCANE_INTELLECT_HEADLINE"),
				GameStrings.Get("TUTORIAL_ARCANE_INTELLECT_DESCRIPTION")
			};
		}
		return null;
	}

	public override void NotifyOfCardGrabbed(Entity entity)
	{
		if (entity.GetCardId() == "TU5_CS2_023" && GameState.Get().GetFriendlySidePlayer().GetNumAvailableResources() >= entity.GetCost())
		{
			BoardTutorial.Get().EnableFullHighlight(enable: true);
		}
		if (costLabel != null)
		{
			Object.Destroy(costLabel);
		}
	}

	public override void NotifyOfCardDropped(Entity entity)
	{
		if (entity.GetCardId() == "TU5_CS2_023")
		{
			BoardTutorial.Get().EnableFullHighlight(enable: false);
		}
	}

	public override void NotifyOfManaCrystalSpawned()
	{
		AssetLoader.Get().InstantiatePrefab("plus1.prefab:7427d28c07eea8645a3308e04398ee30", Plus1ActorLoadedCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
		if (GetTag(GAME_TAG.TURN) == 3)
		{
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_02_JAINA_08_22.prefab:52cd86a7a20daeb4b8d1f3fd2647e9ea", "TUTORIAL02_JAINA_08", Notification.SpeechBubbleDirection.BottomLeft, actor));
		}
		FadeInManaSpotlight();
	}

	private void FadeInManaSpotlight()
	{
		Gameplay.Get().StartCoroutine(StartManaSpotFade());
	}

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
	}

	private void Plus1ActorLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		Vector3 position = SceneUtils.FindChildBySubstring(Board.Get().gameObject, "ManaCounter_Friendly").transform.position;
		Vector3 position2 = new Vector3(position.x - 0.02f, position.y + 0.2f, position.z);
		go.transform.position = position2;
		go.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		Vector3 localScale = go.transform.localScale;
		go.transform.localScale = new Vector3(1f, 1f, 1f);
		iTween.MoveTo(go, new Vector3(position2.x, position2.y, position2.z + 2f), 3f);
		float num = 2.5f;
		iTween.ScaleTo(go, new Vector3(localScale.x * num, localScale.y * num, localScale.z * num), 3f);
		iTween.RotateTo(go, new Vector3(0f, 170f, 0f), 3f);
		iTween.FadeTo(go, 0f, 2.75f);
	}

	public override void NotifyOfEnemyManaCrystalSpawned()
	{
		AssetLoader.Get().InstantiatePrefab("plus1.prefab:7427d28c07eea8645a3308e04398ee30", Plus1ActorLoadedCallbackEnemy, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void Plus1ActorLoadedCallbackEnemy(AssetReference assetRef, GameObject go, object callbackData)
	{
		Vector3 position = SceneUtils.FindChildBySubstring(Board.Get().gameObject, "ManaCounter_Opposing").transform.position;
		Vector3 position2 = new Vector3(position.x, position.y + 0.2f, position.z);
		go.transform.position = position2;
		go.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		Vector3 localScale = go.transform.localScale;
		go.transform.localScale = new Vector3(1f, 1f, 1f);
		iTween.MoveTo(go, new Vector3(position2.x, position2.y, position2.z - 2f), 3f);
		float num = 2.5f;
		iTween.ScaleTo(go, new Vector3(localScale.x * num, localScale.y * num, localScale.z * num), 3f);
		iTween.RotateTo(go, new Vector3(0f, 170f, 0f), 3f);
		iTween.FadeTo(go, 0f, 2.75f);
	}

	private void ManaLabelLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		GameObject costTextObject = ((Card)callbackData).GetActor().GetCostTextObject();
		if (costTextObject == null)
		{
			Object.Destroy(go);
			return;
		}
		if (costLabel != null)
		{
			Object.Destroy(costLabel);
		}
		costLabel = go;
		go.transform.parent = costTextObject.transform;
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.transform.localPosition = new Vector3(-0.025f, 0.28f, 0f);
		go.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		go.GetComponent<UberText>().Text = GameStrings.Get("GLOBAL_COST");
	}

	public override void NotifyOfTooltipZoneMouseOver(TooltipZone tooltip)
	{
		if (tooltip.targetObject.GetComponent<ManaCrystalMgr>() != null)
		{
			if (manaNotifier != null)
			{
				Object.Destroy(manaNotifier.gameObject);
			}
			if (manaNotifier2 != null)
			{
				Object.Destroy(manaNotifier2.gameObject);
			}
		}
	}

	public override string GetTurnStartReminderText()
	{
		return GameStrings.Format("TUTORIAL02_HELP_04", numManaThisTurn);
	}

	public override void NotifyOfDefeatCoinAnimation()
	{
		Gameplay.Get().StartCoroutine(PlayGoingOutSound());
	}

	private IEnumerator PlayGoingOutSound()
	{
		AudioSource deathLine = GetPreloadedSound("VO_TUTORIAL02_MILLHOUSE_20_22_ALT.prefab:79394b29df25e894085524bdad538962");
		while (deathLine != null && deathLine.isPlaying)
		{
			yield return null;
		}
		PlaySound("VO_TUTORIAL02_MILLHOUSE_19_21.prefab:bc8b4236bf74f1244afa49a8195c7f74");
	}

	protected override void NotifyOfManaError()
	{
		NotificationManager.Get().DestroyNotificationNowWithNoAnim(manaNotifier);
		NotificationManager.Get().DestroyNotificationNowWithNoAnim(manaNotifier2);
	}

	public override List<RewardData> GetCustomRewards()
	{
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("EX1_015", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}
}
