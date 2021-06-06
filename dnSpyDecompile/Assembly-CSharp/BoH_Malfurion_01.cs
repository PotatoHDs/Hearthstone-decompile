using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200052C RID: 1324
public class BoH_Malfurion_01 : BoH_Malfurion_Dungeon
{
	// Token: 0x0600480C RID: 18444 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600480D RID: 18445 RVA: 0x00182428 File Offset: 0x00180628
	public BoH_Malfurion_01()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_01.s_booleanOptions);
	}

	// Token: 0x0600480E RID: 18446 RVA: 0x00182500 File Offset: 0x00180700
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_01.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01,
			BoH_Malfurion_01.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03,
			BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01,
			BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02,
			BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02,
			BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02,
			BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600480F RID: 18447 RVA: 0x001826A4 File Offset: 0x001808A4
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004810 RID: 18448 RVA: 0x001826B8 File Offset: 0x001808B8
	private void SetPopupPosition()
	{
		if (this.friendlySidePlayer.IsCurrentPlayer())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = -66f;
				return;
			}
			this.popUpPos.z = -44f;
			return;
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.popUpPos.z = 66f;
				return;
			}
			this.popUpPos.z = 44f;
			return;
		}
	}

	// Token: 0x06004811 RID: 18449 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004812 RID: 18450 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004813 RID: 18451 RVA: 0x0018272D File Offset: 0x0018092D
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02);
		yield return base.MissionPlayVO(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004814 RID: 18452 RVA: 0x0018273C File Offset: 0x0018093C
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004815 RID: 18453 RVA: 0x00182744 File Offset: 0x00180944
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004816 RID: 18454 RVA: 0x0018274C File Offset: 0x0018094C
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologue;
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01;
	}

	// Token: 0x06004817 RID: 18455 RVA: 0x00182770 File Offset: 0x00180970
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

	// Token: 0x06004818 RID: 18456 RVA: 0x001827F4 File Offset: 0x001809F4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		this.popUpPos = new Vector3(0f, 0f, -40f);
		if (missionEvent != 228)
		{
			if (missionEvent != 504)
			{
				if (missionEvent == 507)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01, 2.5f);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(this.m_popUpInfo[missionEvent][0]), false, NotificationManager.PopupTextType.FANCY);
			yield return new WaitForSeconds(3.5f);
			NotificationManager.Get().DestroyNotification(popup, 0f);
			popup = null;
		}
		yield break;
	}

	// Token: 0x06004819 RID: 18457 RVA: 0x0018280A File Offset: 0x00180A0A
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

	// Token: 0x0600481A RID: 18458 RVA: 0x00182820 File Offset: 0x00180A20
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

	// Token: 0x0600481B RID: 18459 RVA: 0x00182836 File Offset: 0x00180A36
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_01.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x0600481C RID: 18460 RVA: 0x0018284C File Offset: 0x00180A4C
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x0600481D RID: 18461 RVA: 0x0018285C File Offset: 0x00180A5C
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x0600481E RID: 18462 RVA: 0x00182878 File Offset: 0x00180A78
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x0600481F RID: 18463 RVA: 0x001828B4 File Offset: 0x00180AB4
	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22", AssetLoadingOptions.None);
		this.m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = this.m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = true;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = false;
		component.FsmVariables.GetFsmBool("Destroyer").Value = false;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_turnCounter.transform.parent = actor.gameObject.transform;
		this.m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		this.m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x06004820 RID: 18464 RVA: 0x001829CA File Offset: 0x00180BCA
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x06004821 RID: 18465 RVA: 0x001829D4 File Offset: 0x00180BD4
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x06004822 RID: 18466 RVA: 0x00182A02 File Offset: 0x00180C02
	private void UpdateTurnCounter(int cost)
	{
		this.m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		if (cost <= 0)
		{
			UnityEngine.Object.Destroy(this.m_turnCounter.gameObject);
			return;
		}
		this.UpdateTurnCounterText(cost);
	}

	// Token: 0x06004823 RID: 18467 RVA: 0x00182A38 File Offset: 0x00180C38
	private void UpdateTurnCounterText(int cost)
	{
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = cost
			}
		};
		string headlineString = GameStrings.FormatPlurals("BOH_MALFURION_01", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x06004824 RID: 18468 RVA: 0x00182A90 File Offset: 0x00180C90
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04003B98 RID: 15256
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_01.InitBooleanOptions();

	// Token: 0x04003B99 RID: 15257
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01.prefab:3af07d5f3716a8b4d90be31675c25b10");

	// Token: 0x04003B9A RID: 15258
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01.prefab:2f605dc353e7ab04b85e86976a8152f4");

	// Token: 0x04003B9B RID: 15259
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01.prefab:1ebf159a3a7e0284298406e09f72cdbc");

	// Token: 0x04003B9C RID: 15260
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03.prefab:3892ddfe34a5ed34ca00fa1b1aa6a58d");

	// Token: 0x04003B9D RID: 15261
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01.prefab:16b6e5701e26b404aa3c99508099f757");

	// Token: 0x04003B9E RID: 15262
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01.prefab:6a03c6fbd426db54b8ed7e21b2b780f6");

	// Token: 0x04003B9F RID: 15263
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01.prefab:cba0a850f450a7c4d8c6dbc049b19a97");

	// Token: 0x04003BA0 RID: 15264
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02.prefab:ab0bfc8cf92454a48bf937ec5469bd46");

	// Token: 0x04003BA1 RID: 15265
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01.prefab:e6addb8546ea48146bbb0c15c7dfd43d");

	// Token: 0x04003BA2 RID: 15266
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03.prefab:b6e8025a1c97eea4fbad55ad3e21db25");

	// Token: 0x04003BA3 RID: 15267
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01.prefab:bdc1efbb05ad3094c9e266dd49fe4585");

	// Token: 0x04003BA4 RID: 15268
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02.prefab:3c4b57d3041d3bb4882097b5a17eb26c");

	// Token: 0x04003BA5 RID: 15269
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03.prefab:6a38e59a6926c804b8759fb355b03763");

	// Token: 0x04003BA6 RID: 15270
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01.prefab:f50cf649fa8fd5f43be4d78d68b5822d");

	// Token: 0x04003BA7 RID: 15271
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03.prefab:481d6ae805e2d8d47b5fdaa4af791883");

	// Token: 0x04003BA8 RID: 15272
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01.prefab:866fe7090dd3dcb428ab1d6983c06775");

	// Token: 0x04003BA9 RID: 15273
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02.prefab:209fdfafeffdbe34fa8ed8dd7a3848fc");

	// Token: 0x04003BAA RID: 15274
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02.prefab:3f6bdcf911d778a4ba709f0ef48c7b9c");

	// Token: 0x04003BAB RID: 15275
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02.prefab:d8d1eaa6b86441741b7ddadee6059cdc");

	// Token: 0x04003BAC RID: 15276
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02.prefab:76b8579081c17a043b936d9dc1126931");

	// Token: 0x04003BAD RID: 15277
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[]
			{
				"BOH_MALFURION_01b"
			}
		}
	};

	// Token: 0x04003BAE RID: 15278
	private Player friendlySidePlayer;

	// Token: 0x04003BAF RID: 15279
	private Entity playerEntity;

	// Token: 0x04003BB0 RID: 15280
	private float popUpScale = 1.25f;

	// Token: 0x04003BB1 RID: 15281
	private Vector3 popUpPos;

	// Token: 0x04003BB2 RID: 15282
	private Notification StartPopup;

	// Token: 0x04003BB3 RID: 15283
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_01.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01,
		BoH_Malfurion_01.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01,
		BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03
	};

	// Token: 0x04003BB4 RID: 15284
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01,
		BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02,
		BoH_Malfurion_01.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03
	};

	// Token: 0x04003BB5 RID: 15285
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04003BB6 RID: 15286
	private Notification m_turnCounter;

	// Token: 0x04003BB7 RID: 15287
	private MineCartRushArt m_mineCartArt;
}
