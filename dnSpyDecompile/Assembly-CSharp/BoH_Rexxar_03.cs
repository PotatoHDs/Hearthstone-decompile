using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000538 RID: 1336
public class BoH_Rexxar_03 : BoH_Rexxar_Dungeon
{
	// Token: 0x0600490E RID: 18702 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x0600490F RID: 18703 RVA: 0x0018662A File Offset: 0x0018482A
	public BoH_Rexxar_03()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_03.s_booleanOptions);
	}

	// Token: 0x06004910 RID: 18704 RVA: 0x00186654 File Offset: 0x00184854
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Death_01,
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3EmoteResponse_01,
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_01,
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_02,
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_03,
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Intro_01,
			BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Loss_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeA_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeB_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeC_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeD_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeE_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_01,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_02,
			BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004911 RID: 18705 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004912 RID: 18706 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004913 RID: 18707 RVA: 0x001867A8 File Offset: 0x001849A8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x001867B7 File Offset: 0x001849B7
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3EmoteResponse_01;
	}

	// Token: 0x06004915 RID: 18709 RVA: 0x001867D0 File Offset: 0x001849D0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004916 RID: 18710 RVA: 0x00186859 File Offset: 0x00184A59
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 228)
		{
			if (missionEvent != 501)
			{
				if (missionEvent != 504)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Rexxar_03.VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Loss_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor2, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			this.ShowMinionMoveTutorial();
			yield return new WaitForSeconds(3f);
			this.HideNotification(this.m_minionMoveTutorialNotification, false);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004917 RID: 18711 RVA: 0x0018686F File Offset: 0x00184A6F
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004918 RID: 18712 RVA: 0x00186885 File Offset: 0x00184A85
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "CS2_012"))
		{
			if (cardId == "LOOT_314")
			{
				yield return base.PlayLineOnlyOnce(actor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeE_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeD_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004919 RID: 18713 RVA: 0x0018689B File Offset: 0x00184A9B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 9)
				{
					yield return base.PlayLineAlways(actor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Rexxar_03.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600491A RID: 18714 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x0600491B RID: 18715 RVA: 0x001868B4 File Offset: 0x00184AB4
	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInOpponentPlay = this.GetLeftMostMinionInOpponentPlay();
		if (leftMostMinionInOpponentPlay == null)
		{
			return;
		}
		Vector3 position = leftMostMinionInOpponentPlay.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.6f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2.5f);
		}
		string key = "BOH_REXXAR_02";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x0600491C RID: 18716 RVA: 0x00186971 File Offset: 0x00184B71
	private IEnumerator ShowOrHideMoveMinionTutorial()
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		this.HideNotification(this.m_minionMoveTutorialNotification, false);
		while (InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		if ((this.GetLeftMostMinionInOpponentPlay() != null || InputManager.Get().GetHeldCard()) && this.m_shouldPlayMinionMoveTutorial)
		{
			this.ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideMoveMinionTutorial());
		}
		yield break;
	}

	// Token: 0x0600491D RID: 18717 RVA: 0x00186980 File Offset: 0x00184B80
	protected Card GetLeftMostMinionInFriendlyPlay()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x0600491E RID: 18718 RVA: 0x001869F4 File Offset: 0x00184BF4
	protected Card GetLeftMostMinionInOpponentPlay()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x0600491F RID: 18719 RVA: 0x001784A8 File Offset: 0x001766A8
	protected void HideNotification(Notification notification, bool hideImmediately = false)
	{
		if (notification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(notification);
				return;
			}
			NotificationManager.Get().DestroyNotification(notification, 0f);
		}
	}

	// Token: 0x04003D05 RID: 15621
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_03.InitBooleanOptions();

	// Token: 0x04003D06 RID: 15622
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Death_01 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Death_01.prefab:566174dbe9ef429887501ebc140f4f2a");

	// Token: 0x04003D07 RID: 15623
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3EmoteResponse_01.prefab:59fd122df5164b69abe23f085e586d65");

	// Token: 0x04003D08 RID: 15624
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_01.prefab:6e3a504a826044bbb96b49bc91455f9c");

	// Token: 0x04003D09 RID: 15625
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_02.prefab:9a7d4eff6de64775b381aa66d6a44664");

	// Token: 0x04003D0A RID: 15626
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3HeroPower_03.prefab:a45450453ee5402d9ca301d00a32b301");

	// Token: 0x04003D0B RID: 15627
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Intro_01.prefab:f2474a3935334b11bc02bb0e54cdd893");

	// Token: 0x04003D0C RID: 15628
	private static readonly AssetReference VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Misha_Female_Bear_Story_Rexxar_Mission3Loss_01.prefab:7d0ad7ff8ef74817b9e50e9d5a37e390");

	// Token: 0x04003D0D RID: 15629
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeA_01.prefab:7f907c72e23854845ae2acb64b9e3d4c");

	// Token: 0x04003D0E RID: 15630
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeB_01.prefab:85fedca340d633244a550d25b6901436");

	// Token: 0x04003D0F RID: 15631
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeC_01.prefab:31151640d943ecf4ab956f31b21a7edf");

	// Token: 0x04003D10 RID: 15632
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeD_01.prefab:c30108161449b3849ae9abfec48b2e38");

	// Token: 0x04003D11 RID: 15633
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3ExchangeE_01.prefab:ee8683e8c73356f419b230905ea18c8c");

	// Token: 0x04003D12 RID: 15634
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_01.prefab:d0ffecd2bd207084e9e923f34c817b3a");

	// Token: 0x04003D13 RID: 15635
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Intro_02.prefab:d604c2b5d871dcb4399f3a985d962864");

	// Token: 0x04003D14 RID: 15636
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission3Victory_01.prefab:337f228655730674aa797ec48f8a79e0");

	// Token: 0x04003D15 RID: 15637
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x04003D16 RID: 15638
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x04003D17 RID: 15639
	private HashSet<string> m_playedLines = new HashSet<string>();
}
