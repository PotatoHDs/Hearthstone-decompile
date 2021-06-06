using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200053A RID: 1338
public class BoH_Rexxar_05 : BoH_Rexxar_Dungeon
{
	// Token: 0x06004936 RID: 18742 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004937 RID: 18743 RVA: 0x00187024 File Offset: 0x00185224
	public BoH_Rexxar_05()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_05.s_booleanOptions);
	}

	// Token: 0x06004938 RID: 18744 RVA: 0x001870C8 File Offset: 0x001852C8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_05.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01,
			BoH_Rexxar_05.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Death_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01,
			BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01,
			BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02,
			BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01,
			BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01,
			BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01,
			BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01,
			BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01,
			BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01,
			BoH_Rexxar_05.VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004939 RID: 18745 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600493A RID: 18746 RVA: 0x0018730C File Offset: 0x0018550C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01, 2.5f);
		yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600493B RID: 18747 RVA: 0x0018731B File Offset: 0x0018551B
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5IdleLines;
	}

	// Token: 0x0600493C RID: 18748 RVA: 0x00187323 File Offset: 0x00185523
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPowerLines;
	}

	// Token: 0x0600493D RID: 18749 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600493E RID: 18750 RVA: 0x0018732B File Offset: 0x0018552B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01;
	}

	// Token: 0x0600493F RID: 18751 RVA: 0x00187344 File Offset: 0x00185544
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

	// Token: 0x06004940 RID: 18752 RVA: 0x001873CD File Offset: 0x001855CD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 204)
		{
			if (missionEvent == 101)
			{
				GameState.Get().SetBusy(true);
				Actor enemyActorByCardId = base.GetEnemyActorByCardId("Story_02_WoundedFootman");
				if (enemyActorByCardId != null)
				{
					yield return base.PlayLineAlways(enemyActorByCardId, BoH_Rexxar_05.VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01, 2.5f);
				}
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_51A;
			}
			switch (missionEvent)
			{
			case 201:
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01, 2.5f);
				goto IL_51A;
			case 202:
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01, 2.5f);
				goto IL_51A;
			case 203:
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01, 2.5f);
				goto IL_51A;
			case 204:
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01, 2.5f);
				goto IL_51A;
			}
		}
		else
		{
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_05.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_05.DaelinBrassRing, BoH_Rexxar_05.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_05.DaelinBrassRing, BoH_Rexxar_05.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_05.JainaBrassRing, BoH_Rexxar_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_51A;
			}
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_51A;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_51A:
		yield break;
	}

	// Token: 0x06004941 RID: 18753 RVA: 0x001873E3 File Offset: 0x001855E3
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

	// Token: 0x06004942 RID: 18754 RVA: 0x001873F9 File Offset: 0x001855F9
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

	// Token: 0x06004943 RID: 18755 RVA: 0x0018740F File Offset: 0x0018560F
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 17)
		{
			if (turn == 19)
			{
				yield return base.PlayLineAlways(actor, BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004944 RID: 18756 RVA: 0x0016110D File Offset: 0x0015F30D
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}

	// Token: 0x04003D33 RID: 15667
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_05.InitBooleanOptions();

	// Token: 0x04003D34 RID: 15668
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_01.prefab:997fcc78b0cb3fe4fbec72709bcee854");

	// Token: 0x04003D35 RID: 15669
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission5Victory_02.prefab:b70268c69b6fcfe44a884a2332281f05");

	// Token: 0x04003D36 RID: 15670
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Death_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Death_01.prefab:2f1d78ced905b924a8b7e095df3072f6");

	// Token: 0x04003D37 RID: 15671
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5EmoteResponse_01.prefab:728c0896ee6c3ae43991f81dd9cd21fa");

	// Token: 0x04003D38 RID: 15672
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeF_01.prefab:6fb03e885e5140a47a705acd75232b89");

	// Token: 0x04003D39 RID: 15673
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5ExchangeG_01.prefab:6d2ecdfadf3bce347878adecd0b587d0");

	// Token: 0x04003D3A RID: 15674
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01.prefab:87e8d2cb6f399ee46a2b5bf1d9500eb0");

	// Token: 0x04003D3B RID: 15675
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02.prefab:a1a24e27312657741b588fe92ac39f86");

	// Token: 0x04003D3C RID: 15676
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03.prefab:30be6009789d5af41996148c83158ad3");

	// Token: 0x04003D3D RID: 15677
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01.prefab:d3baf9d334cc64c4291fdc03ca7082c3");

	// Token: 0x04003D3E RID: 15678
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02.prefab:ddc3334a445dc6d49bb758de4924bad4");

	// Token: 0x04003D3F RID: 15679
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03.prefab:132abd92e75104748b9f0e53df4e4eb7");

	// Token: 0x04003D40 RID: 15680
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Intro_01.prefab:94bd06ace9e7b2446829a7a780a7f9f0");

	// Token: 0x04003D41 RID: 15681
	private static readonly AssetReference VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Loss_01.prefab:6679e11bb7f9d704dbdbd23961d754ef");

	// Token: 0x04003D42 RID: 15682
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01.prefab:fa0da701f910f7c4b8405a9da4992d55");

	// Token: 0x04003D43 RID: 15683
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01.prefab:b240c16eba247d248b5d5e1a3e295422");

	// Token: 0x04003D44 RID: 15684
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeC_01.prefab:3b7fab7a81b8ee941931847b27517c20");

	// Token: 0x04003D45 RID: 15685
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeD_01.prefab:1432f5c6ded7ab64aa0b200dd057e7f2");

	// Token: 0x04003D46 RID: 15686
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_01.prefab:0228474dc52500d4bb83de6d1295a2c0");

	// Token: 0x04003D47 RID: 15687
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeE_02.prefab:8bc801ed88bd6bf40bfe597daae5f6a0");

	// Token: 0x04003D48 RID: 15688
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Intro_01.prefab:bc321e0aa483b2b4bb0cdcddb89f3ff4");

	// Token: 0x04003D49 RID: 15689
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_01.prefab:1a6e39f4d9cd0b849904b3f0e974bb46");

	// Token: 0x04003D4A RID: 15690
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5Victory_02.prefab:a320a54cbadebf44db61d3a51ebbde8b");

	// Token: 0x04003D4B RID: 15691
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeB_01.prefab:8fe4e45a1df13dc4f94043292557f3b2");

	// Token: 0x04003D4C RID: 15692
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeC_01.prefab:4da7e895d72989e459030bacd0263096");

	// Token: 0x04003D4D RID: 15693
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeD_01.prefab:09f65539eb4190a4eb5517a7df375319");

	// Token: 0x04003D4E RID: 15694
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5ExchangeE_01.prefab:a281acc43a6ef4d4a9eb83996b93fc28");

	// Token: 0x04003D4F RID: 15695
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Intro_01.prefab:8f4adabd70a55eb4aaf239e3d5616081");

	// Token: 0x04003D50 RID: 15696
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission5Victory_01.prefab:5f8dcfef5a39fa945bef952cc027b04d");

	// Token: 0x04003D51 RID: 15697
	private static readonly AssetReference VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01 = new AssetReference("VO_Story_Minion_Footman_Male_Human_Story_Footman_Play_01.prefab:82d107a16b46519499058165c6d0f7f6");

	// Token: 0x04003D52 RID: 15698
	public static readonly AssetReference DaelinBrassRing = new AssetReference("Daelin_BrassRing_Quote.prefab:8553800b28758a44da69e1cd9bdacf07");

	// Token: 0x04003D53 RID: 15699
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04003D54 RID: 15700
	private List<string> m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPowerLines = new List<string>
	{
		BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_01,
		BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_02,
		BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5HeroPower_03
	};

	// Token: 0x04003D55 RID: 15701
	private List<string> m_VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5IdleLines = new List<string>
	{
		BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_01,
		BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_02,
		BoH_Rexxar_05.VO_Story_Hero_Darkscale_Female_Naga_Story_Rexxar_Mission5Idle_03
	};

	// Token: 0x04003D56 RID: 15702
	private HashSet<string> m_playedLines = new HashSet<string>();
}
