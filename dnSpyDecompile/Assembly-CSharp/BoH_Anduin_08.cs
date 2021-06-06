using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000515 RID: 1301
public class BoH_Anduin_08 : BoH_Anduin_Dungeon
{
	// Token: 0x0600464B RID: 17995 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600464C RID: 17996 RVA: 0x0017B4C0 File Offset: 0x001796C0
	public BoH_Anduin_08()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_08.s_booleanOptions);
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x0017B694 File Offset: 0x00179894
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Attack_01,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Attack_02,
			BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Attack_03,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01,
			BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01,
			BoH_Anduin_08.VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02,
			BoH_Anduin_08.VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03,
			BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01,
			BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01,
			BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02,
			BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03,
			BoH_Anduin_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02,
			BoH_Anduin_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03,
			BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02,
			BoH_Anduin_08.VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01,
			BoH_Anduin_08.VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01,
			BoH_Anduin_08.VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02,
			BoH_Anduin_08.VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02,
			BoH_Anduin_08.VO_Story_Minion_Nathanos_Male_Undead_Trigger_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x0017B988 File Offset: 0x00179B88
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x0017B997 File Offset: 0x00179B97
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01;
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_DRG;
	}

	// Token: 0x06004650 RID: 18000 RVA: 0x0017B9BA File Offset: 0x00179BBA
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
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
			case 101:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01);
				yield return base.MissionPlayVO(BoH_Anduin_08.SaurfangBrassRing, BoH_Anduin_08.VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03);
				GameState.Get().SetBusy(false);
				goto IL_768;
			case 102:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(BoH_Anduin_08.GennBrassRing, BoH_Anduin_08.VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01);
				GameState.Get().SetBusy(false);
				goto IL_768;
			case 103:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(BoH_Anduin_08.BfAJainaBrassRing, BoH_Anduin_08.VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02);
				GameState.Get().SetBusy(false);
				goto IL_768;
			case 104:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01);
				GameState.Get().SetBusy(false);
				goto IL_768;
			case 105:
				yield return base.MissionPlayVOOnce(base.GetEnemyActorByCardId("Story_05_Nathanos"), BoH_Anduin_08.VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02);
				yield return base.MissionPlayVOOnce(friendlyActor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02);
				goto IL_768;
			case 106:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01);
				GameState.Get().SetBusy(false);
				goto IL_768;
			case 107:
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03);
				goto IL_768;
			case 108:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04);
				GameState.Get().SetBusy(false);
				goto IL_768;
			case 109:
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02);
				goto IL_768;
			case 110:
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02);
				goto IL_768;
			case 111:
				yield return base.MissionPlayVOOnce(base.GetEnemyActorByCardId("Story_05_Nathanos"), BoH_Anduin_08.VO_Story_Minion_Nathanos_Male_Undead_Trigger_01);
				goto IL_768;
			case 112:
				yield return base.MissionPlayVOOnce(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01);
				goto IL_768;
			case 113:
				yield return base.MissionPlayVOOnce(enemyActor, this.m_BossSylvanasUsesHeroPowerLines);
				goto IL_768;
			case 114:
				yield return base.MissionPlayVOOnce(enemyActor, this.m_BossSaurfangUsesHeroPowerLines);
				goto IL_768;
			case 115:
				yield return base.MissionPlayVOOnce(friendlyActor, this.m_AnduinAttacksLines);
				goto IL_768;
			default:
				if (missionEvent == 228)
				{
					GameState.Get().SetBusy(true);
					this.ShowMinionMoveTutorial();
					yield return new WaitForSeconds(3f);
					this.HideNotification(this.m_minionMoveTutorialNotification, false);
					GameState.Get().SetBusy(false);
					goto IL_768;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01);
				GameState.Get().SetBusy(false);
				goto IL_768;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_768;
			}
			if (missionEvent == 515)
			{
				if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() == "Story_05_Saurfang")
				{
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01);
					goto IL_768;
				}
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01);
				goto IL_768;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_768:
		yield break;
	}

	// Token: 0x06004651 RID: 18001 RVA: 0x0017B9D0 File Offset: 0x00179BD0
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

	// Token: 0x06004652 RID: 18002 RVA: 0x0017B9E6 File Offset: 0x00179BE6
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

	// Token: 0x06004653 RID: 18003 RVA: 0x0017B9FC File Offset: 0x00179BFC
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.MissionPlayVO(BoH_Anduin_08.GennBrassRing, BoH_Anduin_08.VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x06004654 RID: 18004 RVA: 0x0017BA12 File Offset: 0x00179C12
	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			yield break;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			yield break;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		float thinkEmoteBossIdleChancePercentage = this.GetThinkEmoteBossIdleChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossIdleChancePercentage > num || (!this.m_Mission_FriendlyPlayIdleLines && this.m_Mission_EnemyPlayIdleLines))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			if (cardId == "Story_05_Saurfang")
			{
				string line = base.PopRandomLine(this.m_BossSaurfangIdleLinesCopy, true);
				if (this.m_BossSaurfangIdleLinesCopy.Count == 0)
				{
					this.m_BossSaurfangIdleLinesCopy = new List<string>(this.m_BossSaurfangIdleLines);
				}
				yield return base.MissionPlayVO(actor, line);
			}
			else if (cardId == "Story_05_Sylvanas")
			{
				string line2 = base.PopRandomLine(this.m_BossSylvanasIdleLinesCopy, true);
				if (this.m_BossSylvanasIdleLinesCopy.Count == 0)
				{
					this.m_BossSylvanasIdleLinesCopy = new List<string>(this.m_BossSylvanasIdleLines);
				}
				yield return base.MissionPlayVO(actor, line2);
			}
		}
		else
		{
			if (!this.m_Mission_FriendlyPlayIdleLines)
			{
				yield break;
			}
			EmoteType emoteType = EmoteType.THINK1;
			switch (UnityEngine.Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType).GetActiveAudioSource();
		}
		yield break;
	}

	// Token: 0x06004655 RID: 18005 RVA: 0x0017BA24 File Offset: 0x00179C24
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
		string key = "BOH_ANDUIN_08";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06004656 RID: 18006 RVA: 0x0017BAE1 File Offset: 0x00179CE1
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

	// Token: 0x06004657 RID: 18007 RVA: 0x0017BAF0 File Offset: 0x00179CF0
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

	// Token: 0x06004658 RID: 18008 RVA: 0x0017BB64 File Offset: 0x00179D64
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

	// Token: 0x06004659 RID: 18009 RVA: 0x001784A8 File Offset: 0x001766A8
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

	// Token: 0x0400397A RID: 14714
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_08.InitBooleanOptions();

	// Token: 0x0400397B RID: 14715
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeA_02.prefab:5dd89aa2dd336a04eabb7e8e4f884b78");

	// Token: 0x0400397C RID: 14716
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeD_02.prefab:a8c54c1388002c548b6572e91e2b981d");

	// Token: 0x0400397D RID: 14717
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_02.prefab:67bb51b156a9e0e40a41ca5213d17003");

	// Token: 0x0400397E RID: 14718
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeE_04.prefab:821ac1c0781c6ee4f99474417906b36e");

	// Token: 0x0400397F RID: 14719
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeF_02.prefab:2e066ba484e25de46a9a9c1acbf1e15e");

	// Token: 0x04003980 RID: 14720
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8ExchangeG_02.prefab:eebdb8b1fec7d194caa60dd5d546e0cd");

	// Token: 0x04003981 RID: 14721
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Intro_01.prefab:26360fac3ef28534c9d7bb4cc3f59cea");

	// Token: 0x04003982 RID: 14722
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_01.prefab:a5e21394425b9f2499a376f68d0988df");

	// Token: 0x04003983 RID: 14723
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_02.prefab:83ab43dc30e88ae469b9b273999c04f7");

	// Token: 0x04003984 RID: 14724
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission8Victory_03.prefab:a5f1bad8391f7cd40bcf3faca7b79ec5");

	// Token: 0x04003985 RID: 14725
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Attack_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Attack_01.prefab:0618de36d93d420448287a75805971aa");

	// Token: 0x04003986 RID: 14726
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Attack_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Attack_02.prefab:18417e8d916916b47a9ccbf977535752");

	// Token: 0x04003987 RID: 14727
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Attack_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Attack_03.prefab:f2a3f88ee3b6a504298588ecf781ef2b");

	// Token: 0x04003988 RID: 14728
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8EmoteResponse_01.prefab:c48d17a01cadc7b489c6579467417c9a");

	// Token: 0x04003989 RID: 14729
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_01.prefab:34af598c27985724b9802106067a0a34");

	// Token: 0x0400398A RID: 14730
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeB_03.prefab:33e15931d5d64304da221cc698836ce4");

	// Token: 0x0400398B RID: 14731
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeD_01.prefab:c36afcabe96ea41478d70e30a2658447");

	// Token: 0x0400398C RID: 14732
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeE_01.prefab:dda0e6287f7935b4a8caa601c158c4e4");

	// Token: 0x0400398D RID: 14733
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeF_01.prefab:d35a4ad888b1f26499615770293635fa");

	// Token: 0x0400398E RID: 14734
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeG_01.prefab:e0d61e2b1a6bd35438866e29f4633c6e");

	// Token: 0x0400398F RID: 14735
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8ExchangeH_01.prefab:57c84baaf74b7a7428725fc6e84c44a6");

	// Token: 0x04003990 RID: 14736
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01.prefab:202a4e9c6167d5e4abe01e90012e6570");

	// Token: 0x04003991 RID: 14737
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02.prefab:8ac74d3549f32814ba00125de843a822");

	// Token: 0x04003992 RID: 14738
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03.prefab:60782074f6475304ba1df74000699841");

	// Token: 0x04003993 RID: 14739
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01.prefab:ba0c9c5ea6581b245b25388afdad922d");

	// Token: 0x04003994 RID: 14740
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02.prefab:95f08ae6c3f395346adbf541617cb3f1");

	// Token: 0x04003995 RID: 14741
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03.prefab:387a96afb0b076a44bac24146d5edd31");

	// Token: 0x04003996 RID: 14742
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Intro_02.prefab:713f2a93a6594524496d9cfa17783000");

	// Token: 0x04003997 RID: 14743
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Loss_01.prefab:a7eb114b2260d174ebdd571ce1248ee5");

	// Token: 0x04003998 RID: 14744
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_TriggerAlleria_01.prefab:118c9eb2fb7d71f4da228f18cc5609b6");

	// Token: 0x04003999 RID: 14745
	private static readonly AssetReference VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeB_02.prefab:88d6046346107e84a805ed3667755afc");

	// Token: 0x0400399A RID: 14746
	private static readonly AssetReference VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03 = new AssetReference("VO_Story_Hero_Varok_Male_Orc_Story_Anduin_Mission8ExchangeE_03.prefab:139a593d14a4b2b449e04e33de4b0a72");

	// Token: 0x0400399B RID: 14747
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02.prefab:fef0afedde775fb49ba5961be8c186c8");

	// Token: 0x0400399C RID: 14748
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03.prefab:b7f7552a80cdce043b9515514d9ba572");

	// Token: 0x0400399D RID: 14749
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02.prefab:7435c8fa6c168dd42abcb1c9c49f1fff");

	// Token: 0x0400399E RID: 14750
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01.prefab:809e5cbeff5ffd04ca2e31417ec96f8a");

	// Token: 0x0400399F RID: 14751
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01.prefab:be5cdbf6a229ffe4fbfeccd06a3c00ff");

	// Token: 0x040039A0 RID: 14752
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02.prefab:483f8441b6adbae4ea4fc9d0cbed163c");

	// Token: 0x040039A1 RID: 14753
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03.prefab:68363df5f2110ae42a646f7b6a79ca8f");

	// Token: 0x040039A2 RID: 14754
	private static readonly AssetReference VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01 = new AssetReference("VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeA_01.prefab:a038cb757f2fb224aadf938dcb12531d");

	// Token: 0x040039A3 RID: 14755
	private static readonly AssetReference VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01 = new AssetReference("VO_Story_Minion_Genn_Male_Worgen_Story_Anduin_Mission8ExchangeC_01.prefab:d307c74375749974abcef19e066fee10");

	// Token: 0x040039A4 RID: 14756
	private static readonly AssetReference VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02 = new AssetReference("VO_Story_Minion_Jaina_Female_Human_Story_Anduin_Mission8ExchangeC_02.prefab:078c477f5f264f12aaf341c9e250febc");

	// Token: 0x040039A5 RID: 14757
	private static readonly AssetReference VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02 = new AssetReference("VO_Story_Minion_Nathanos_Male_Undead_Story_Anduin_Mission8ExchangeD_02.prefab:ae190b055f911da4db4323c273a26c06");

	// Token: 0x040039A6 RID: 14758
	private static readonly AssetReference VO_Story_Minion_Nathanos_Male_Undead_Trigger_01 = new AssetReference("VO_Story_Minion_Nathanos_Male_Undead_Trigger_01.prefab:ca382153375ac274380ee30ed4b5e03c");

	// Token: 0x040039A7 RID: 14759
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x040039A8 RID: 14760
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x040039A9 RID: 14761
	public static readonly AssetReference GennBrassRing = new AssetReference("Greymane_BrassRing_Quote.prefab:3e16b31a3b009ad468fa76462c5eda3b");

	// Token: 0x040039AA RID: 14762
	public static readonly AssetReference SaurfangBrassRing = new AssetReference("Saurfang_BrassRing_Quote.prefab:727d1e09f5a40f649afa7ed2f3e70564");

	// Token: 0x040039AB RID: 14763
	public static readonly AssetReference BfAJainaBrassRing = new AssetReference("JainaBfA_BrassRing_Quote.prefab:7c966a1e09f056e43b799d1e9f19da82");

	// Token: 0x040039AC RID: 14764
	private List<string> m_BossSylvanasUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_01,
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_02,
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8HeroPower_03
	};

	// Token: 0x040039AD RID: 14765
	private List<string> m_BossSaurfangUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01,
		BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02,
		BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03
	};

	// Token: 0x040039AE RID: 14766
	private List<string> m_BossSylvanasIdleLines = new List<string>
	{
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01,
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02,
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03
	};

	// Token: 0x040039AF RID: 14767
	private List<string> m_BossSylvanasIdleLinesCopy = new List<string>
	{
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_01,
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_02,
		BoH_Anduin_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Anduin_Mission8Idle_03
	};

	// Token: 0x040039B0 RID: 14768
	private List<string> m_BossSaurfangIdleLines = new List<string>
	{
		BoH_Anduin_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02,
		BoH_Anduin_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03,
		BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02
	};

	// Token: 0x040039B1 RID: 14769
	private List<string> m_BossSaurfangIdleLinesCopy = new List<string>
	{
		BoH_Anduin_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_02,
		BoH_Anduin_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_03,
		BoH_Anduin_08.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02
	};

	// Token: 0x040039B2 RID: 14770
	private List<string> m_AnduinAttacksLines = new List<string>
	{
		BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Attack_01,
		BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Attack_02,
		BoH_Anduin_08.VO_Story_Hero_Anduin_Male_Human_Attack_03
	};

	// Token: 0x040039B3 RID: 14771
	private HashSet<string> m_playedLines = new HashSet<string>();
}
