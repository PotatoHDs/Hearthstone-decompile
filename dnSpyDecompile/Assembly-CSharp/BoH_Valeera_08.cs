using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class BoH_Valeera_08 : BoH_Valeera_Dungeon
{
	// Token: 0x06004BC6 RID: 19398 RVA: 0x001914A8 File Offset: 0x0018F6A8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8EmoteResponse_01,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeA_01,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeB_01,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeC_02,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_01,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_02,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_03,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Intro_02,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Loss_01,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_01,
			BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_03,
			BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeA_02,
			BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeC_01,
			BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Intro_01,
			BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004BC7 RID: 19399 RVA: 0x001915FC File Offset: 0x0018F7FC
	private void Start()
	{
		this.friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	// Token: 0x06004BC8 RID: 19400 RVA: 0x00191610 File Offset: 0x0018F810
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

	// Token: 0x06004BC9 RID: 19401 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004BCA RID: 19402 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004BCB RID: 19403 RVA: 0x00191685 File Offset: 0x0018F885
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004BCC RID: 19404 RVA: 0x00191694 File Offset: 0x0018F894
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004BCD RID: 19405 RVA: 0x0019169C File Offset: 0x0018F89C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_GILFinalBoss;
		this.m_standardEmoteResponseLine = BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8EmoteResponse_01;
	}

	// Token: 0x06004BCE RID: 19406 RVA: 0x001916C0 File Offset: 0x0018F8C0
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

	// Token: 0x06004BCF RID: 19407 RVA: 0x00191744 File Offset: 0x0018F944
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
					yield return base.PlayLineAlways(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Loss_01, 2.5f);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Victory_02, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_03, 2.5f);
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

	// Token: 0x06004BD0 RID: 19408 RVA: 0x0019175A File Offset: 0x0018F95A
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

	// Token: 0x06004BD1 RID: 19409 RVA: 0x00191770 File Offset: 0x0018F970
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

	// Token: 0x06004BD2 RID: 19410 RVA: 0x00191786 File Offset: 0x0018F986
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
			if (turn != 7)
			{
				if (turn == 11)
				{
					yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeC_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_08.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004BD3 RID: 19411 RVA: 0x0019179C File Offset: 0x0018F99C
	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		this.InitVisuals();
	}

	// Token: 0x06004BD4 RID: 19412 RVA: 0x001917AC File Offset: 0x0018F9AC
	private void InitVisuals()
	{
		int cost = base.GetCost();
		this.InitTurnCounter(cost);
	}

	// Token: 0x06004BD5 RID: 19413 RVA: 0x001917C8 File Offset: 0x0018F9C8
	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			this.UpdateVisuals(change.newValue);
		}
	}

	// Token: 0x06004BD6 RID: 19414 RVA: 0x00191804 File Offset: 0x0018FA04
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

	// Token: 0x06004BD7 RID: 19415 RVA: 0x0019191A File Offset: 0x0018FB1A
	private void UpdateVisuals(int cost)
	{
		this.UpdateTurnCounter(cost);
	}

	// Token: 0x06004BD8 RID: 19416 RVA: 0x00191924 File Offset: 0x0018FB24
	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		this.m_mineCartArt.DoPortraitSwap(actor);
	}

	// Token: 0x06004BD9 RID: 19417 RVA: 0x00191952 File Offset: 0x0018FB52
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

	// Token: 0x06004BDA RID: 19418 RVA: 0x00191988 File Offset: 0x0018FB88
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
		string headlineString = GameStrings.FormatPlurals("BOH_VALEERA_08", pluralNumbers, Array.Empty<object>());
		this.m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	// Token: 0x06004BDB RID: 19419 RVA: 0x001919E0 File Offset: 0x0018FBE0
	private IEnumerator ShowPopup(string displayString)
	{
		this.StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), false, NotificationManager.PopupTextType.BASIC);
		NotificationManager.Get().DestroyNotification(this.StartPopup, 7f);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0400405C RID: 16476
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8EmoteResponse_01.prefab:0723307cf76586348911e4261e586673");

	// Token: 0x0400405D RID: 16477
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeA_01.prefab:ef35cbf633307724cbe2f7ddebcf54da");

	// Token: 0x0400405E RID: 16478
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeB_01.prefab:eab885d89886d8446919a714a40d10d4");

	// Token: 0x0400405F RID: 16479
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8ExchangeC_02.prefab:6b2374883d397c444a18a82f36e812a0");

	// Token: 0x04004060 RID: 16480
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_01.prefab:deb8713c125651d409bf412e056dd506");

	// Token: 0x04004061 RID: 16481
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_02.prefab:190187b6bb313974bbfedd59cc012f72");

	// Token: 0x04004062 RID: 16482
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_03.prefab:0bc78b3600f7e0347b9490d7f3948600");

	// Token: 0x04004063 RID: 16483
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Intro_02.prefab:709bd338ef18f964bbcb5f827e252c62");

	// Token: 0x04004064 RID: 16484
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Loss_01.prefab:9c67722a24f784d4baaa83bd706f1fe2");

	// Token: 0x04004065 RID: 16485
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_01.prefab:f813048b715c4c642b80960a3c7dd6b9");

	// Token: 0x04004066 RID: 16486
	private static readonly AssetReference VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Victory_03.prefab:692bd7580a9819d468287265230f8b95");

	// Token: 0x04004067 RID: 16487
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeA_02.prefab:5bad61ef61d7b9e4eb89daee515e1b60");

	// Token: 0x04004068 RID: 16488
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8ExchangeC_01.prefab:13aaf782442e9d640bbff9b51220295c");

	// Token: 0x04004069 RID: 16489
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Intro_01.prefab:a7233807d400b874cbef3a042fb0993c");

	// Token: 0x0400406A RID: 16490
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission8Victory_02.prefab:480a3cbda32cba048a7b9f84a022c433");

	// Token: 0x0400406B RID: 16491
	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[]
			{
				"BOH_VALEERA_08b"
			}
		}
	};

	// Token: 0x0400406C RID: 16492
	private Player friendlySidePlayer;

	// Token: 0x0400406D RID: 16493
	private Entity playerEntity;

	// Token: 0x0400406E RID: 16494
	private float popUpScale = 1.25f;

	// Token: 0x0400406F RID: 16495
	private Vector3 popUpPos;

	// Token: 0x04004070 RID: 16496
	private Notification StartPopup;

	// Token: 0x04004071 RID: 16497
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_01,
		BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_02,
		BoH_Valeera_08.VO_Story_Hero_Jorach_Male_Human_Story_Valeera_Mission8Idle_03
	};

	// Token: 0x04004072 RID: 16498
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04004073 RID: 16499
	private Notification m_turnCounter;

	// Token: 0x04004074 RID: 16500
	private MineCartRushArt m_mineCartArt;
}
