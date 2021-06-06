using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005DD RID: 1501
public class Tutorial_06 : TutorialEntity
{
	// Token: 0x06005238 RID: 21048 RVA: 0x001AF66C File Offset: 0x001AD86C
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

	// Token: 0x06005239 RID: 21049 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x0600523A RID: 21050 RVA: 0x001B02B8 File Offset: 0x001AE4B8
	public Tutorial_06()
	{
		this.m_gameOptions.AddOptions(Tutorial_06.s_booleanOptions, Tutorial_06.s_stringOptions);
	}

	// Token: 0x0600523B RID: 21051 RVA: 0x001B02D8 File Offset: 0x001AE4D8
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_TUTORIAL_06_CHO_15_15.prefab:5f0d0a2d3c6884a47aeadcf29b3d802d");
		base.PreloadSound("VO_TUTORIAL_06_CHO_09_13.prefab:99a983ceaa6615848a8bea922e428a2d");
		base.PreloadSound("VO_TUTORIAL_06_CHO_17_16.prefab:d337628cbe1422e4ca21dbe174ddef2e");
		base.PreloadSound("VO_TUTORIAL_06_CHO_05_09.prefab:ef06af76837b9ff4c8ac27ee18516291");
		base.PreloadSound("VO_TUTORIAL_06_JAINA_03_51.prefab:06bd40a237dd0674e8d377240de40e65");
		base.PreloadSound("VO_TUTORIAL_06_CHO_06_10.prefab:cd28a9685f46936409d5300001540558");
		base.PreloadSound("VO_TUTORIAL_06_CHO_21_18.prefab:48c935e7ec96a104ab04d185382898a4");
		base.PreloadSound("VO_TUTORIAL_06_CHO_20_17.prefab:dfc795a107caddb42b3d131d6a627fd8");
		base.PreloadSound("VO_TUTORIAL_06_CHO_07_11.prefab:b691c4acfee6c5342a727189de686b6d");
		base.PreloadSound("VO_TUTORIAL_06_JAINA_04_52.prefab:5d75f42502bc99b4c84704bedf553ba5");
		base.PreloadSound("VO_TUTORIAL_06_CHO_04_08.prefab:8164c968ccb1be44d9dfc01c1668b014");
		base.PreloadSound("VO_TUTORIAL_06_CHO_12_14.prefab:13ee98fef9d3e6746a69c385c889dc3a");
		base.PreloadSound("VO_TUTORIAL_06_CHO_01_05.prefab:10097a4886a24384d8e8f6dd668bb1c7");
		base.PreloadSound("VO_TUTORIAL_06_JAINA_01_49.prefab:b9513645100911741b9bda379bc27a75");
		base.PreloadSound("VO_TUTORIAL_06_CHO_02_06.prefab:a9c29883676f21d4e932dccc0f92feca");
		base.PreloadSound("VO_TUTORIAL_06_JAINA_02_50.prefab:b97fe840305cae04f8486ac1770b126f");
		base.PreloadSound("VO_TUTORIAL_06_CHO_03_07.prefab:c71aaff381cdbd346a9bcf54fa5d7db9");
		base.PreloadSound("VO_TUTORIAL_06_CHO_22_19.prefab:8c70f69b5da1f9c43accca95c1854ddf");
		base.PreloadSound("VO_TUTORIAL_06_JAINA_05_53.prefab:6fb71de610db1234887f6d6c948f5174");
	}

	// Token: 0x0600523C RID: 21052 RVA: 0x001B03B8 File Offset: 0x001AE5B8
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		this.CancelChoFloating();
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			this.m_victory = true;
		}
		base.NotifyOfGameOver(gameResult);
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.SetTutorialProgress(TutorialProgress.CHO_COMPLETE);
			base.PlaySound("VO_TUTORIAL_06_CHO_22_19.prefab:8c70f69b5da1f9c43accca95c1854ddf", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.TIED)
		{
			base.PlaySound("VO_TUTORIAL_06_CHO_22_19.prefab:8c70f69b5da1f9c43accca95c1854ddf", 1f, true, false);
			return;
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			base.SetTutorialLostProgress(TutorialProgress.CHO_COMPLETE);
		}
	}

	// Token: 0x0600523D RID: 21053 RVA: 0x001B041D File Offset: 0x001AE61D
	protected override Spell BlowUpHero(Card card, SpellType spellType)
	{
		if (card.GetEntity().GetCardId() != "TU4f_001")
		{
			return base.BlowUpHero(card, spellType);
		}
		Spell result = card.ActivateActorSpell(SpellType.CHODEATH);
		Gameplay.Get().StartCoroutine(base.HideOtherElements(card));
		return result;
	}

	// Token: 0x0600523E RID: 21054 RVA: 0x001B0459 File Offset: 0x001AE659
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor jainaActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 2)
		{
			if (turn != 4)
			{
				switch (turn)
				{
				case 14:
					if (!base.DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
					{
						while (this.m_choSpeaking)
						{
							yield return null;
						}
						this.m_choSpeaking = true;
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_21_18.prefab:48c935e7ec96a104ab04d185382898a4", "TUTORIAL06_CHO_21", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						this.m_choSpeaking = false;
					}
					break;
				case 15:
					if (!base.DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
					{
						while (this.m_choSpeaking)
						{
							yield return null;
						}
						yield return new WaitForSeconds(0.5f);
						this.m_choSpeaking = true;
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_05_09.prefab:ef06af76837b9ff4c8ac27ee18516291", "TUTORIAL06_CHO_05", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						this.m_choSpeaking = false;
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_JAINA_03_51.prefab:06bd40a237dd0674e8d377240de40e65", "TUTORIAL06_JAINA_03", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
						this.m_choSpeaking = true;
						yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_06_10.prefab:cd28a9685f46936409d5300001540558", "TUTORIAL06_CHO_06", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
						this.m_choSpeaking = false;
					}
					break;
				case 16:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_20_17.prefab:dfc795a107caddb42b3d131d6a627fd8", "TUTORIAL06_CHO_20", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					break;
				}
			}
			else if (!base.DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
			{
				GameState.Get().SetBusy(true);
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_09_13.prefab:99a983ceaa6615848a8bea922e428a2d", "TUTORIAL06_CHO_09", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				GameState.Get().SetBusy(false);
			}
		}
		else if (!base.DidLoseTutorial(TutorialProgress.CHO_COMPLETE))
		{
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_15_15.prefab:5f0d0a2d3c6884a47aeadcf29b3d802d", "TUTORIAL06_CHO_15", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600523F RID: 21055 RVA: 0x001B046F File Offset: 0x001AE66F
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
			while (this.m_choSpeaking)
			{
				yield return null;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_17_16.prefab:d337628cbe1422e4ca21dbe174ddef2e", "TUTORIAL06_CHO_17", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			break;
		case 3:
		case 4:
		case 5:
		case 7:
			break;
		case 6:
		{
			GameState.Get().SetBusy(true);
			Card card = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard();
			this.m_choFloatSpell = card.GetActorSpell(SpellType.CHOFLOAT, true);
			this.m_choFloatSpell.ActivateState(SpellStateType.BIRTH);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_07_11.prefab:b691c4acfee6c5342a727189de686b6d", "TUTORIAL06_CHO_07", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			GameState.Get().SetBusy(false);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_JAINA_04_52.prefab:5d75f42502bc99b4c84704bedf553ba5", "TUTORIAL06_JAINA_04", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
			break;
		}
		case 8:
			Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_04_08.prefab:8164c968ccb1be44d9dfc01c1668b014", "TUTORIAL06_CHO_04", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			break;
		case 9:
			this.CancelChoFloating();
			this.m_choSpeaking = true;
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_12_14.prefab:13ee98fef9d3e6746a69c385c889dc3a", "TUTORIAL06_CHO_12", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
			this.m_choSpeaking = false;
			break;
		case 10:
		{
			Card card2 = this.FindVoodooDoctorInOpposingSide();
			if (card2 == null)
			{
				yield break;
			}
			GameState.Get().SetBusy(true);
			Vector3 position = card2.transform.position;
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, new Vector3(position.x + 3f, position.y, position.z), TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL06_HELP_03"), true, NotificationManager.PopupTextType.BASIC);
			notification.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			NotificationManager.Get().DestroyNotification(notification, 5f);
			yield return new WaitForSeconds(5f);
			GameState.Get().SetBusy(false);
			break;
		}
		default:
			if (missionEvent != 54)
			{
				if (missionEvent == 55)
				{
					MulliganManager.Get().BeginMulligan();
					base.FadeInHeroActor(enemyActor);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_01_05.prefab:10097a4886a24384d8e8f6dd668bb1c7", "TUTORIAL06_CHO_01", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					base.FadeOutHeroActor(enemyActor);
					yield return Gameplay.Get().StartCoroutine(this.Wait(0.5f));
					base.FadeInHeroActor(jainaActor);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_JAINA_01_49.prefab:b9513645100911741b9bda379bc27a75", "TUTORIAL06_JAINA_01", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
					base.FadeOutHeroActor(jainaActor);
					yield return Gameplay.Get().StartCoroutine(this.Wait(0.5f));
					base.FadeInHeroActor(enemyActor);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_02_06.prefab:a9c29883676f21d4e932dccc0f92feca", "TUTORIAL06_CHO_02", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
					base.FadeOutHeroActor(enemyActor);
					yield return Gameplay.Get().StartCoroutine(this.Wait(0.25f));
					base.FadeInHeroActor(jainaActor);
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_JAINA_02_50.prefab:b97fe840305cae04f8486ac1770b126f", "TUTORIAL06_JAINA_02", Notification.SpeechBubbleDirection.BottomRight, jainaActor, 1f, true, false, 3f, 0f));
					base.FadeOutHeroActor(jainaActor);
					yield return Gameplay.Get().StartCoroutine(this.Wait(0.25f));
					Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_TUTORIAL_06_CHO_03_07.prefab:c71aaff381cdbd346a9bcf54fa5d7db9", "TUTORIAL06_CHO_03", Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false, 3f, 0f));
				}
			}
			else
			{
				yield return new WaitForSeconds(2f);
				string bodyTextGameString = (!base.DidLoseTutorial(TutorialProgress.CHO_COMPLETE)) ? "TUTORIAL06_HELP_02" : "TUTORIAL06_HELP_04";
				base.ShowTutorialDialog("TUTORIAL06_HELP_01", bodyTextGameString, "TUTORIAL01_HELP_16", new Vector2(0f, 0.5f), false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06005240 RID: 21056 RVA: 0x001B0485 File Offset: 0x001AE685
	private void CancelChoFloating()
	{
		if (this.m_choFloatSpell == null)
		{
			return;
		}
		if (this.m_choFloatSpell.GetActiveState() != SpellStateType.NONE)
		{
			this.m_choFloatSpell.ActivateState(SpellStateType.CANCEL);
		}
	}

	// Token: 0x06005241 RID: 21057 RVA: 0x001B04B0 File Offset: 0x001AE6B0
	private Card FindVoodooDoctorInOpposingSide()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetCardId() == "EX1_011")
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x06005242 RID: 21058 RVA: 0x001B0528 File Offset: 0x001AE728
	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		yield break;
	}

	// Token: 0x06005243 RID: 21059 RVA: 0x001B0537 File Offset: 0x001AE737
	public override float GetAdditionalTimeToWaitForSpells()
	{
		return 1.5f;
	}

	// Token: 0x06005244 RID: 21060 RVA: 0x001B0540 File Offset: 0x001AE740
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
				if (this.m_endTurnNotifier != null)
				{
					NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_endTurnNotifier);
				}
				Vector3 position = EndTurnButton.Get().transform.position;
				Vector3 position2 = new Vector3(position.x - 3f, position.y, position.z);
				this.m_endTurnNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get("TUTORIAL_NO_ENDTURN_ATK"), true, NotificationManager.PopupTextType.BASIC);
				NotificationManager.Get().DestroyNotification(this.m_endTurnNotifier, 2.5f);
				return false;
			}
		}
		return true;
	}

	// Token: 0x06005245 RID: 21061 RVA: 0x001B0662 File Offset: 0x001AE862
	public override void NotifyOfDefeatCoinAnimation()
	{
		if (!this.m_victory)
		{
			return;
		}
		base.PlaySound("VO_TUTORIAL_06_JAINA_05_53.prefab:6fb71de610db1234887f6d6c948f5174", 1f, true, false);
	}

	// Token: 0x06005246 RID: 21062 RVA: 0x001B0680 File Offset: 0x001AE880
	public override List<RewardData> GetCustomRewards()
	{
		if (!this.m_victory)
		{
			return null;
		}
		List<RewardData> list = new List<RewardData>();
		CardRewardData cardRewardData = new CardRewardData("CS2_124", TAG_PREMIUM.NORMAL, 2);
		cardRewardData.MarkAsDummyReward();
		list.Add(cardRewardData);
		return list;
	}

	// Token: 0x04004975 RID: 18805
	private static Map<GameEntityOption, bool> s_booleanOptions = Tutorial_06.InitBooleanOptions();

	// Token: 0x04004976 RID: 18806
	private static Map<GameEntityOption, string> s_stringOptions = Tutorial_06.InitStringOptions();

	// Token: 0x04004977 RID: 18807
	private Notification m_endTurnNotifier;

	// Token: 0x04004978 RID: 18808
	private bool m_victory;

	// Token: 0x04004979 RID: 18809
	private bool m_choSpeaking;

	// Token: 0x0400497A RID: 18810
	private Spell m_choFloatSpell;
}
