using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class BoH_Anduin_06 : BoH_Anduin_Dungeon
{
	// Token: 0x06004624 RID: 17956 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004625 RID: 17957 RVA: 0x0017A9E4 File Offset: 0x00178BE4
	public BoH_Anduin_06()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_06.s_booleanOptions);
	}

	// Token: 0x06004626 RID: 17958 RVA: 0x0017AA90 File Offset: 0x00178C90
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02,
			BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03,
			BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01,
			BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02,
			BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01,
			BoH_Anduin_06.VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004627 RID: 17959 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004628 RID: 17960 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004629 RID: 17961 RVA: 0x0017AC64 File Offset: 0x00178E64
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600462A RID: 17962 RVA: 0x0017AC73 File Offset: 0x00178E73
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x0600462B RID: 17963 RVA: 0x0017AC7B File Offset: 0x00178E7B
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x0600462C RID: 17964 RVA: 0x0017AC83 File Offset: 0x00178E83
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_LOOT;
		this.m_standardEmoteResponseLine = BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01;
	}

	// Token: 0x0600462D RID: 17965 RVA: 0x0017ACA6 File Offset: 0x00178EA6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 228)
		{
			switch (missionEvent)
			{
			case 102:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01);
				GameState.Get().SetBusy(false);
				goto IL_51D;
			case 103:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01);
				GameState.Get().SetBusy(false);
				goto IL_51D;
			case 104:
				break;
			case 105:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(base.GetEnemyActorByCardId("Story_05_SamtheWise"), BoH_Anduin_06.VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01);
				GameState.Get().SetBusy(false);
				goto IL_51D;
			case 106:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01);
				GameState.Get().SetBusy(false);
				goto IL_51D;
			default:
				if (missionEvent == 228)
				{
					GameState.Get().SetBusy(true);
					this.ShowMinionMoveTutorial();
					yield return new WaitForSeconds(3f);
					this.HideNotification(this.m_minionMoveTutorialNotification, false);
					GameState.Get().SetBusy(false);
					goto IL_51D;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_06.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02);
				GameState.Get().SetBusy(false);
				goto IL_51D;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_51D;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_51D;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_51D:
		yield break;
	}

	// Token: 0x0600462E RID: 17966 RVA: 0x0017ACBC File Offset: 0x00178EBC
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

	// Token: 0x0600462F RID: 17967 RVA: 0x0017ACD2 File Offset: 0x00178ED2
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004630 RID: 17968 RVA: 0x0017ACE8 File Offset: 0x00178EE8
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004631 RID: 17969 RVA: 0x0017ACF8 File Offset: 0x00178EF8
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
		string key = "BOH_ANDUIN_06";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06004632 RID: 17970 RVA: 0x0017ADB5 File Offset: 0x00178FB5
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

	// Token: 0x06004633 RID: 17971 RVA: 0x0017ADC4 File Offset: 0x00178FC4
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

	// Token: 0x06004634 RID: 17972 RVA: 0x0017AE38 File Offset: 0x00179038
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

	// Token: 0x06004635 RID: 17973 RVA: 0x001784A8 File Offset: 0x001766A8
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

	// Token: 0x04003942 RID: 14658
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_06.InitBooleanOptions();

	// Token: 0x04003943 RID: 14659
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02.prefab:51c5ebd53930a8d4e9a6c3b6d5b9876a");

	// Token: 0x04003944 RID: 14660
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01.prefab:217eba39f8ba076449a05458abf8c1a0");

	// Token: 0x04003945 RID: 14661
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02.prefab:7730d01779261ef44a932ca6624afa99");

	// Token: 0x04003946 RID: 14662
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01.prefab:4eea07bd68b1e9540b3f498f6ede1f0a");

	// Token: 0x04003947 RID: 14663
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02.prefab:3bcbffa0845cb714ea9247bcbc5cb55e");

	// Token: 0x04003948 RID: 14664
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03.prefab:7617c1faf3c22a74e8f2204e9b918292");

	// Token: 0x04003949 RID: 14665
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04.prefab:611a9d36f6d6dea439f234e8fcbf1b0f");

	// Token: 0x0400394A RID: 14666
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01.prefab:27a63b711947c674fa0ce9b4a209a1dd");

	// Token: 0x0400394B RID: 14667
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01.prefab:ab61b46eea485a34a93c0319294c77c5");

	// Token: 0x0400394C RID: 14668
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03.prefab:8714bda28aab0dd43ae0aa0eac022300");

	// Token: 0x0400394D RID: 14669
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01.prefab:d8a909b386dcb464dba538622af9e864");

	// Token: 0x0400394E RID: 14670
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01.prefab:4ecd2cf7956034d4e896660a6d55ce0d");

	// Token: 0x0400394F RID: 14671
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01.prefab:e9fffd02eecbf9a4caf63a75526df23e");

	// Token: 0x04003950 RID: 14672
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01.prefab:bd6c783406465b040bfed07196ec335c");

	// Token: 0x04003951 RID: 14673
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01.prefab:a9f67b63767f9e6449fbf30679d58a72");

	// Token: 0x04003952 RID: 14674
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02.prefab:511cbe78cde72ab4e979b594b27c3a36");

	// Token: 0x04003953 RID: 14675
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03.prefab:e17cbdf2c68126649bfa5d6167130acc");

	// Token: 0x04003954 RID: 14676
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01.prefab:5d10a608bdaf72747bb7ee755864cd7a");

	// Token: 0x04003955 RID: 14677
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02.prefab:0c9ab276b34d2e14b9ec4cc4c84b5cc2");

	// Token: 0x04003956 RID: 14678
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03.prefab:885fe8fe862376441aa6c7fe6f461015");

	// Token: 0x04003957 RID: 14679
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02.prefab:7bd5b55d2aeb8014da6a4a049cdd1c25");

	// Token: 0x04003958 RID: 14680
	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01.prefab:dcfd078f359979d4b94d5449d53d0eb7");

	// Token: 0x04003959 RID: 14681
	private static readonly AssetReference VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01 = new AssetReference("VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01.prefab:7d385f828f2fbd34c8b5765605c54154");

	// Token: 0x0400395A RID: 14682
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x0400395B RID: 14683
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x0400395C RID: 14684
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01,
		BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02,
		BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03
	};

	// Token: 0x0400395D RID: 14685
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01,
		BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02,
		BoH_Anduin_06.VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03
	};

	// Token: 0x0400395E RID: 14686
	private HashSet<string> m_playedLines = new HashSet<string>();
}
