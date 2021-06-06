using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_06 : TutorialEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification m_endTurnNotifier;

	private bool m_victory;

	private bool m_choSpeaking;

	private Spell m_choFloatSpell;

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

	public Tutorial_06()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_TUTORIAL_06_CHO_15_15.prefab:5f0d0a2d3c6884a47aeadcf29b3d802d");
		PreloadSound("VO_TUTORIAL_06_CHO_09_13.prefab:99a983ceaa6615848a8bea922e428a2d");
		PreloadSound("VO_TUTORIAL_06_CHO_17_16.prefab:d337628cbe1422e4ca21dbe174ddef2e");
		PreloadSound("VO_TUTORIAL_06_CHO_05_09.prefab:ef06af76837b9ff4c8ac27ee18516291");
		PreloadSound("VO_TUTORIAL_06_JAINA_03_51.prefab:06bd40a237dd0674e8d377240de40e65");
		PreloadSound("VO_TUTORIAL_06_CHO_06_10.prefab:cd28a9685f46936409d5300001540558");
		PreloadSound("VO_TUTORIAL_06_CHO_21_18.prefab:48c935e7ec96a104ab04d185382898a4");
		PreloadSound("VO_TUTORIAL_06_CHO_20_17.prefab:dfc795a107caddb42b3d131d6a627fd8");
		PreloadSound("VO_TUTORIAL_06_CHO_07_11.prefab:b691c4acfee6c5342a727189de686b6d");
		PreloadSound("VO_TUTORIAL_06_JAINA_04_52.prefab:5d75f42502bc99b4c84704bedf553ba5");
		PreloadSound("VO_TUTORIAL_06_CHO_04_08.prefab:8164c968ccb1be44d9dfc01c1668b014");
		PreloadSound("VO_TUTORIAL_06_CHO_12_14.prefab:13ee98fef9d3e6746a69c385c889dc3a");
		PreloadSound("VO_TUTORIAL_06_CHO_01_05.prefab:10097a4886a24384d8e8f6dd668bb1c7");
		PreloadSound("VO_TUTORIAL_06_JAINA_01_49.prefab:b9513645100911741b9bda379bc27a75");
		PreloadSound("VO_TUTORIAL_06_CHO_02_06.prefab:a9c29883676f21d4e932dccc0f92feca");
		PreloadSound("VO_TUTORIAL_06_JAINA_02_50.prefab:b97fe840305cae04f8486ac1770b126f");
		PreloadSound("VO_TUTORIAL_06_CHO_03_07.prefab:c71aaff381cdbd346a9bcf54fa5d7db9");
		PreloadSound("VO_TUTORIAL_06_CHO_22_19.prefab:8c70f69b5da1f9c43accca95c1854ddf");
		PreloadSound("VO_TUTORIAL_06_JAINA_05_53.prefab:6fb71de610db1234887f6d6c948f5174");
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		CancelChoFloating();
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			m_victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			SetTutorialProgress(TutorialProgress.CHO_COMPLETE);
			PlaySound("VO_TUTORIAL_06_CHO_22_19.prefab:8c70f69b5da1f9c43accca95c1854ddf");
			break;
		case TAG_PLAYSTATE.TIED:
			PlaySound("VO_TUTORIAL_06_CHO_22_19.prefab:8c70f69b5da1f9c43accca95c1854ddf");
			break;
		case TAG_PLAYSTATE.LOST:
			SetTutorialLostProgress(TutorialProgress.CHO_COMPLETE);
			break;
		}
	}

	protected override Spell BlowUpHero(Card card, SpellType spellType)
	{
		if (card.GetEntity().GetCardId() != "TU4f_001")
		{
			return base.BlowUpHero(card, spellType);
		}
		Spell result = card.ActivateActorSpell(SpellType.CHODEATH);
		Gameplay.Get().StartCoroutine(HideOtherElements(card));
		return result;
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 2:
			if (!DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_15_15.prefab:5f0d0a2d3c6884a47aeadcf29b3d802d", "TUTORIAL06_CHO_15", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 4:
			if (!DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
			{
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_09_13.prefab:99a983ceaa6615848a8bea922e428a2d", "TUTORIAL06_CHO_09", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 15:
			if (!DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
			{
				while (m_choSpeaking)
				{
					yield return null;
				}
				yield return new WaitForSeconds(0.5f);
				m_choSpeaking = true;
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_05_09.prefab:ef06af76837b9ff4c8ac27ee18516291", "TUTORIAL06_CHO_05", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				m_choSpeaking = false;
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_JAINA_03_51.prefab:06bd40a237dd0674e8d377240de40e65", "TUTORIAL06_JAINA_03", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
				m_choSpeaking = true;
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_06_10.prefab:cd28a9685f46936409d5300001540558", "TUTORIAL06_CHO_06", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				m_choSpeaking = false;
			}
			break;
		case 14:
			if (!DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
			{
				while (m_choSpeaking)
				{
					yield return null;
				}
				m_choSpeaking = true;
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_21_18.prefab:48c935e7ec96a104ab04d185382898a4", "TUTORIAL06_CHO_21", Notification.SpeechBubbleDirection.TopRight, enemyActor));
				m_choSpeaking = false;
			}
			break;
		case 16:
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_20_17.prefab:dfc795a107caddb42b3d131d6a627fd8", "TUTORIAL06_CHO_20", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
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
			GameState.Get().SetBusy(busy: true);
			while (m_choSpeaking)
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_17_16.prefab:d337628cbe1422e4ca21dbe174ddef2e", "TUTORIAL06_CHO_17", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 6:
		{
			GameState.Get().SetBusy(busy: true);
			Card card = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard();
			m_choFloatSpell = card.GetActorSpell(SpellType.CHOFLOAT);
			m_choFloatSpell.ActivateState(SpellStateType.BIRTH);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_07_11.prefab:b691c4acfee6c5342a727189de686b6d", "TUTORIAL06_CHO_07", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_JAINA_04_52.prefab:5d75f42502bc99b4c84704bedf553ba5", "TUTORIAL06_JAINA_04", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			break;
		}
		case 8:
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_04_08.prefab:8164c968ccb1be44d9dfc01c1668b014", "TUTORIAL06_CHO_04", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 9:
			CancelChoFloating();
			m_choSpeaking = true;
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_12_14.prefab:13ee98fef9d3e6746a69c385c889dc3a", "TUTORIAL06_CHO_12", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			m_choSpeaking = false;
			break;
		case 10:
		{
			Card card2 = FindVoodooDoctorInOpposingSide();
			if (!(card2 == null))
			{
				GameState.Get().SetBusy(busy: true);
				Vector3 position = card2.transform.position;
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, new Vector3(position.x + 3f, position.y, position.z), TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL06_HELP_03"));
				notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
				NotificationManager.Get().DestroyNotification(notification, 5f);
				yield return new WaitForSeconds(5f);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
		case 54:
		{
			yield return new WaitForSeconds(2f);
			string bodyTextGameString = ((!DidLoseTutorial(TutorialProgress.CHO_COMPLETE)) ? "TUTORIAL06_HELP_02" : "TUTORIAL06_HELP_04");
			ShowTutorialDialog("TUTORIAL06_HELP_01", bodyTextGameString, "TUTORIAL01_HELP_16", new Vector2(0f, 0.5f));
			break;
		}
		case 55:
			MulliganManager.Get().BeginMulligan();
			FadeInHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_01_05.prefab:10097a4886a24384d8e8f6dd668bb1c7", "TUTORIAL06_CHO_01", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			FadeOutHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(Wait(0.5f));
			FadeInHeroActor(jainaActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_JAINA_01_49.prefab:b9513645100911741b9bda379bc27a75", "TUTORIAL06_JAINA_01", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			FadeOutHeroActor(jainaActor);
			yield return Gameplay.Get().StartCoroutine(Wait(0.5f));
			FadeInHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_02_06.prefab:a9c29883676f21d4e932dccc0f92feca", "TUTORIAL06_CHO_02", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			FadeOutHeroActor(enemyActor);
			yield return Gameplay.Get().StartCoroutine(Wait(0.25f));
			FadeInHeroActor(jainaActor);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_JAINA_02_50.prefab:b97fe840305cae04f8486ac1770b126f", "TUTORIAL06_JAINA_02", Notification.SpeechBubbleDirection.BottomRight, jainaActor));
			FadeOutHeroActor(jainaActor);
			yield return Gameplay.Get().StartCoroutine(Wait(0.25f));
			Gameplay.Get().StartCoroutine(PlaySoundAndWait("VO_TUTORIAL_06_CHO_03_07.prefab:c71aaff381cdbd346a9bcf54fa5d7db9", "TUTORIAL06_CHO_03", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		}
	}

	private void CancelChoFloating()
	{
		if (!(m_choFloatSpell == null) && m_choFloatSpell.GetActiveState() != 0)
		{
			m_choFloatSpell.ActivateState(SpellStateType.CANCEL);
		}
	}

	private Card FindVoodooDoctorInOpposingSide()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetCardId() == "EX1_011")
			{
				return card;
			}
		}
		return null;
	}

	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

	public override float GetAdditionalTimeToWaitForSpells()
	{
		return 1.5f;
	}

	public override bool NotifyOfEndTurnButtonPushed()
	{
		Network.Options optionsPacket = GameState.Get().GetOptionsPacket();
		if (optionsPacket != null && !optionsPacket.HasValidOption())
		{
			NotificationManager.Get().DestroyAllArrows();
			return true;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Main.PlayErrorInfo.IsValid() && option.Type == Network.Options.Option.OptionType.POWER && GameState.Get().GetEntity(option.Main.ID).GetZone() == TAG_ZONE.PLAY)
			{
				if (m_endTurnNotifier != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_endTurnNotifier);
				}
				Vector3 position = EndTurnButton.Get().transform.position;
				Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
				m_endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL_NO_ENDTURN_ATK"));
				NotificationManager.Get().DestroyNotification(m_endTurnNotifier, 2.5f);
				return false;
			}
		}
		return true;
	}

	public override void NotifyOfDefeatCoinAnimation()
	{
		if (m_victory)
		{
			PlaySound("VO_TUTORIAL_06_JAINA_05_53.prefab:6fb71de610db1234887f6d6c948f5174");
		}
	}

	public override List<RewardData> GetCustomRewards()
	{
		if (!m_victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_124", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}
}
