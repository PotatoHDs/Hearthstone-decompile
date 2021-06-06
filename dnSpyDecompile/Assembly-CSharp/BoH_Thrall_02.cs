using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000541 RID: 1345
public class BoH_Thrall_02 : BoH_Thrall_Dungeon
{
	// Token: 0x060049BB RID: 18875 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060049BC RID: 18876 RVA: 0x00189218 File Offset: 0x00187418
	public BoH_Thrall_02()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_02.s_booleanOptions);
	}

	// Token: 0x060049BD RID: 18877 RVA: 0x00189288 File Offset: 0x00187488
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01,
			BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02,
			BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060049BE RID: 18878 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060049BF RID: 18879 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060049C0 RID: 18880 RVA: 0x0018948C File Offset: 0x0018768C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060049C1 RID: 18881 RVA: 0x0018949B File Offset: 0x0018769B
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060049C2 RID: 18882 RVA: 0x001894A3 File Offset: 0x001876A3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_standardEmoteResponseLine = BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01;
	}

	// Token: 0x060049C3 RID: 18883 RVA: 0x001894C8 File Offset: 0x001876C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060049C4 RID: 18884 RVA: 0x0018954C File Offset: 0x0018774C
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
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			case 102:
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02, 2.5f);
				goto IL_589;
			case 103:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			case 104:
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03, 2.5f);
				goto IL_589;
			case 105:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			case 106:
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03, 2.5f);
				goto IL_589;
			case 107:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			case 108:
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03, 2.5f);
				goto IL_589;
			case 109:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			default:
				if (missionEvent == 228)
				{
					GameState.Get().SetBusy(true);
					this.ShowMinionMoveTutorial();
					yield return new WaitForSeconds(3f);
					this.HideNotification(this.m_minionMoveTutorialNotification, false);
					GameState.Get().SetBusy(false);
					goto IL_589;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_589;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_589:
		yield break;
	}

	// Token: 0x060049C5 RID: 18885 RVA: 0x00189562 File Offset: 0x00187762
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

	// Token: 0x060049C6 RID: 18886 RVA: 0x00189578 File Offset: 0x00187778
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

	// Token: 0x060049C7 RID: 18887 RVA: 0x0018958E File Offset: 0x0018778E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn == 3)
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_02.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03, 2.5f);
		}
		yield break;
	}

	// Token: 0x060049C8 RID: 18888 RVA: 0x001895A4 File Offset: 0x001877A4
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
		string key = "BOH_THRALL_02";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x060049C9 RID: 18889 RVA: 0x00189661 File Offset: 0x00187861
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

	// Token: 0x060049CA RID: 18890 RVA: 0x00189670 File Offset: 0x00187870
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

	// Token: 0x060049CB RID: 18891 RVA: 0x001896E4 File Offset: 0x001878E4
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

	// Token: 0x060049CC RID: 18892 RVA: 0x001784A8 File Offset: 0x001766A8
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

	// Token: 0x04003DD0 RID: 15824
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_02.InitBooleanOptions();

	// Token: 0x04003DD1 RID: 15825
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01.prefab:45760608f8527b44eaffc7c3faaaf645");

	// Token: 0x04003DD2 RID: 15826
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01.prefab:936e2824cc25be743951005073859f9e");

	// Token: 0x04003DD3 RID: 15827
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03.prefab:65e484b5c767b0d49a73e4bbe6469c18");

	// Token: 0x04003DD4 RID: 15828
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01.prefab:063ac3d9fa58c9d4eb360b6df3b7b375");

	// Token: 0x04003DD5 RID: 15829
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02.prefab:d6ef8be8d8d9d3147be4b65b3f848a1d");

	// Token: 0x04003DD6 RID: 15830
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01.prefab:e35ef0b4d834dca4da8fd27c714ea277");

	// Token: 0x04003DD7 RID: 15831
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02.prefab:92697f3486be0c64c95deaa568cd0280");

	// Token: 0x04003DD8 RID: 15832
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01.prefab:181be3f0c1202754ba8e27c61a0562a7");

	// Token: 0x04003DD9 RID: 15833
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02.prefab:b2316c97595cb0549883b788db5509d8");

	// Token: 0x04003DDA RID: 15834
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01.prefab:b54f6b82db25df94aa2bf91e17884ba5");

	// Token: 0x04003DDB RID: 15835
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02.prefab:1a8315c158c145647ac941bc95d586a8");

	// Token: 0x04003DDC RID: 15836
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01.prefab:8d3ab17683c604647adc54ef4e9fb34f");

	// Token: 0x04003DDD RID: 15837
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02.prefab:a3f20b58278fca146a736092ca4a41cb");

	// Token: 0x04003DDE RID: 15838
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03.prefab:7f513ccea20b1a74ebb23bc7455bee96");

	// Token: 0x04003DDF RID: 15839
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01.prefab:188b6f7ad7eeef24885f0eb5924b91c6");

	// Token: 0x04003DE0 RID: 15840
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01.prefab:f671bb3824222b74db29e0fef66ea72f");

	// Token: 0x04003DE1 RID: 15841
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01.prefab:bd3c5e203a7071045afe78cf9bba65f9");

	// Token: 0x04003DE2 RID: 15842
	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03.prefab:b3a179713af44824f860e7ec024ff9b7");

	// Token: 0x04003DE3 RID: 15843
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02.prefab:9eff29277873c7941a83c9587cf6f336");

	// Token: 0x04003DE4 RID: 15844
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04.prefab:2cf50b221d22a4b4c9ee3435ef4a1326");

	// Token: 0x04003DE5 RID: 15845
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02.prefab:7e5c28b8d4d0e254db0b5bfe7242be80");

	// Token: 0x04003DE6 RID: 15846
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03.prefab:d39e182c2780fb842aeb985bc41c8f6e");

	// Token: 0x04003DE7 RID: 15847
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03.prefab:be67a8a29a8660a4481306ce675c8723");

	// Token: 0x04003DE8 RID: 15848
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03.prefab:d0f7fca9d962d9e4494c4dc21561a9f4");

	// Token: 0x04003DE9 RID: 15849
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02.prefab:636c73e79e935a3498db0ab839058dac");

	// Token: 0x04003DEA RID: 15850
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02.prefab:6c73bf5960acf1841a264f6b4966acde");

	// Token: 0x04003DEB RID: 15851
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x04003DEC RID: 15852
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x04003DED RID: 15853
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01,
		BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02,
		BoH_Thrall_02.VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03
	};

	// Token: 0x04003DEE RID: 15854
	private HashSet<string> m_playedLines = new HashSet<string>();
}
