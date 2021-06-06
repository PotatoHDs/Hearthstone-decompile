using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000530 RID: 1328
public class BoH_Malfurion_05 : BoH_Malfurion_Dungeon
{
	// Token: 0x0600485F RID: 18527 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004860 RID: 18528 RVA: 0x00183DA0 File Offset: 0x00181FA0
	public BoH_Malfurion_05()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_05.s_booleanOptions);
	}

	// Token: 0x06004861 RID: 18529 RVA: 0x00183E44 File Offset: 0x00182044
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01,
			BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01,
			BoH_Malfurion_05.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02,
			BoH_Malfurion_05.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01,
			BoH_Malfurion_05.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01,
			BoH_Malfurion_05.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02,
			BoH_Malfurion_05.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004862 RID: 18530 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004863 RID: 18531 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004864 RID: 18532 RVA: 0x00183FE8 File Offset: 0x001821E8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_05.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004865 RID: 18533 RVA: 0x00183FF7 File Offset: 0x001821F7
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004866 RID: 18534 RVA: 0x00183FFF File Offset: 0x001821FF
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004867 RID: 18535 RVA: 0x00184007 File Offset: 0x00182207
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologueBoss;
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01;
	}

	// Token: 0x06004868 RID: 18536 RVA: 0x0018402C File Offset: 0x0018222C
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

	// Token: 0x06004869 RID: 18537 RVA: 0x001840B0 File Offset: 0x001822B0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Malfurion_05.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600486A RID: 18538 RVA: 0x001840C6 File Offset: 0x001822C6
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

	// Token: 0x0600486B RID: 18539 RVA: 0x001840DC File Offset: 0x001822DC
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

	// Token: 0x0600486C RID: 18540 RVA: 0x001840F2 File Offset: 0x001822F2
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(BoH_Malfurion_05.MaievBrassRing, BoH_Malfurion_05.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Malfurion_05.TyrandeBrassRing, BoH_Malfurion_05.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Malfurion_05.TyrandeBrassRing, BoH_Malfurion_05.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04003C23 RID: 15395
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_05.InitBooleanOptions();

	// Token: 0x04003C24 RID: 15396
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_02.prefab:baa16b2933ca0b24eb3a568dfba5eaae");

	// Token: 0x04003C25 RID: 15397
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5_Victory_03.prefab:4ac0355b281527b479cb53ae2866e4cd");

	// Token: 0x04003C26 RID: 15398
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5EmoteResponse_01.prefab:f4b3f9005369ae149a6e2f2c9aeba8aa");

	// Token: 0x04003C27 RID: 15399
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_01.prefab:be68fd4770d4fc54c9e6e166c36240c2");

	// Token: 0x04003C28 RID: 15400
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_03.prefab:7e79f922d4688ef40821adbb6e0d1e38");

	// Token: 0x04003C29 RID: 15401
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeB_04.prefab:b084f69490e5c3b49a4cb4aa312d4264");

	// Token: 0x04003C2A RID: 15402
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5ExchangeC_01.prefab:83f2bcbc02944e94091739d2bd5b04b8");

	// Token: 0x04003C2B RID: 15403
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01.prefab:7a12755030c032b4f981a51f9693c755");

	// Token: 0x04003C2C RID: 15404
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02.prefab:51f3204ff69bbd343970a5d7dc33c952");

	// Token: 0x04003C2D RID: 15405
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03.prefab:7fa22da573b746547aafdb82eed5b60f");

	// Token: 0x04003C2E RID: 15406
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01.prefab:ec582d94ab2d40a4a9c8e6f765795c95");

	// Token: 0x04003C2F RID: 15407
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02.prefab:bbb6fcd38143b4846b6f8515e3ab7761");

	// Token: 0x04003C30 RID: 15408
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03.prefab:43b670310ca68374fa69076f7a490948");

	// Token: 0x04003C31 RID: 15409
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Intro_01.prefab:4c7b9ebafd81a3644b03a997f7ae85aa");

	// Token: 0x04003C32 RID: 15410
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Loss_01.prefab:2cdcfabacb3976941bf27b533f063283");

	// Token: 0x04003C33 RID: 15411
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5ExchangeA_02.prefab:21bc5da3d9486f546a26c5775282fa42");

	// Token: 0x04003C34 RID: 15412
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission5Victory_01.prefab:bb73c8acd0807e7419a913dbd0688f40");

	// Token: 0x04003C35 RID: 15413
	private static readonly AssetReference VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01 = new AssetReference("VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission5ExchangeA_01.prefab:35c46dfe3a887ed44832ef4113aa1921");

	// Token: 0x04003C36 RID: 15414
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeA_02.prefab:e08c6437e199cc342bb3d20a3d24e060");

	// Token: 0x04003C37 RID: 15415
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission5ExchangeB_02.prefab:9cc110946978bc64db3797a8e746d649");

	// Token: 0x04003C38 RID: 15416
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x04003C39 RID: 15417
	public static readonly AssetReference MaievBrassRing = new AssetReference("Maiev_BrassRing_Quote.prefab:32a15dc6f5ca637499225d598df88188");

	// Token: 0x04003C3A RID: 15418
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_01,
		BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_02,
		BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5HeroPower_03
	};

	// Token: 0x04003C3B RID: 15419
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_01,
		BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_02,
		BoH_Malfurion_05.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission5Idle_03
	};

	// Token: 0x04003C3C RID: 15420
	private HashSet<string> m_playedLines = new HashSet<string>();
}
