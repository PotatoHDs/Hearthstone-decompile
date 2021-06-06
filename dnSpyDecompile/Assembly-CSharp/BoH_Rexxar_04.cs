using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000539 RID: 1337
public class BoH_Rexxar_04 : BoH_Rexxar_Dungeon
{
	// Token: 0x06004924 RID: 18724 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004925 RID: 18725 RVA: 0x00186B60 File Offset: 0x00184D60
	public BoH_Rexxar_04()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_04.s_booleanOptions);
	}

	// Token: 0x06004926 RID: 18726 RVA: 0x00186BCC File Offset: 0x00184DCC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Death_01,
			BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01,
			BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01,
			BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02,
			BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03,
			BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01,
			BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02,
			BoH_Rexxar_04.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01,
			BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01,
			BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01,
			BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01,
			BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02,
			BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01,
			BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004927 RID: 18727 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004928 RID: 18728 RVA: 0x00186D90 File Offset: 0x00184F90
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01, 2.5f);
		yield return base.PlayLineAlways(BoH_Rexxar_04.MogrinBrassRing, BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004929 RID: 18729 RVA: 0x00186D9F File Offset: 0x00184F9F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPowerLines;
	}

	// Token: 0x0600492A RID: 18730 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600492B RID: 18731 RVA: 0x00186DA7 File Offset: 0x00184FA7
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01;
	}

	// Token: 0x0600492C RID: 18732 RVA: 0x00186DC0 File Offset: 0x00184FC0
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

	// Token: 0x0600492D RID: 18733 RVA: 0x00186E49 File Offset: 0x00185049
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Rexxar_04.ThrallBrassRing, BoH_Rexxar_04.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_28F;
		case 502:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Rexxar_04.MogrinBrassRing, BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_28F;
		case 504:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_28F;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_28F:
		yield break;
	}

	// Token: 0x0600492E RID: 18734 RVA: 0x00186E5F File Offset: 0x0018505F
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

	// Token: 0x0600492F RID: 18735 RVA: 0x00186E75 File Offset: 0x00185075
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

	// Token: 0x06004930 RID: 18736 RVA: 0x00186E8B File Offset: 0x0018508B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(BoH_Rexxar_04.MogrinBrassRing, BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Rexxar_04.MogrinBrassRing, BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01, 2.5f);
			break;
		case 4:
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_04.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Rexxar_04.MogrinBrassRing, BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Rexxar_04.MogrinBrassRing, BoH_Rexxar_04.VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x06004931 RID: 18737 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x04003D18 RID: 15640
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_04.InitBooleanOptions();

	// Token: 0x04003D19 RID: 15641
	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Death_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Death_01.prefab:360502f78bbb40d4b66dffc00d88620f");

	// Token: 0x04003D1A RID: 15642
	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01.prefab:21be207809e8454488283ee3034b15a0");

	// Token: 0x04003D1B RID: 15643
	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01.prefab:bea44d02563343bc88aac8eeb164edb6");

	// Token: 0x04003D1C RID: 15644
	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02.prefab:0b3385edc0ef43bea51732452138d73a");

	// Token: 0x04003D1D RID: 15645
	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03.prefab:80a4e2d96e044b3d9dee1b7b8c261d38");

	// Token: 0x04003D1E RID: 15646
	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01.prefab:e09871da9f6540228ef22387eef08672");

	// Token: 0x04003D1F RID: 15647
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01.prefab:c7c455d853c3ccc46a8efd8c2b2938ac");

	// Token: 0x04003D20 RID: 15648
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01.prefab:3b367750f86ea6a4cb6fb327fa0850e0");

	// Token: 0x04003D21 RID: 15649
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02.prefab:f6f55776113c2074796d9a84ba9ab200");

	// Token: 0x04003D22 RID: 15650
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01.prefab:887c44a315d32b04d8959966eead35ca");

	// Token: 0x04003D23 RID: 15651
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02.prefab:ea44beb27502b6047b9c773d1890c8d8");

	// Token: 0x04003D24 RID: 15652
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01.prefab:0f42d5c8bca12714697ccf9fdac306ba");

	// Token: 0x04003D25 RID: 15653
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02.prefab:716f1c8db14f5de47a80dacb52db60ac");

	// Token: 0x04003D26 RID: 15654
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01.prefab:b928d9fa52037ac4b9aff0a3219783aa");

	// Token: 0x04003D27 RID: 15655
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02.prefab:f5642d0ae8a0dc64187beefbf4e2b122");

	// Token: 0x04003D28 RID: 15656
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01.prefab:6572d5e1fae41f7419a45d90ba1de569");

	// Token: 0x04003D29 RID: 15657
	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01.prefab:8fbbe66497f0c2444ace675879852a0d");

	// Token: 0x04003D2A RID: 15658
	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01.prefab:e3bffeeb027339743bf02c29405b3d90");

	// Token: 0x04003D2B RID: 15659
	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01.prefab:4da71e2a2ff621f439e1a6474870b989");

	// Token: 0x04003D2C RID: 15660
	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02.prefab:36854aa8c6879c34ba95886be636ac26");

	// Token: 0x04003D2D RID: 15661
	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01.prefab:e745a4332f1760a4a90e1fc019fd2f80");

	// Token: 0x04003D2E RID: 15662
	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01.prefab:caa54a11af9c75049942a4d139fe06ff");

	// Token: 0x04003D2F RID: 15663
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003D30 RID: 15664
	public static readonly AssetReference MogrinBrassRing = new AssetReference("Mogrin_BrassRing_Quote.prefab:952755d4adf3dcb42900b423823b1c00");

	// Token: 0x04003D31 RID: 15665
	private List<string> m_VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPowerLines = new List<string>
	{
		BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01,
		BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02,
		BoH_Rexxar_04.VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03
	};

	// Token: 0x04003D32 RID: 15666
	private HashSet<string> m_playedLines = new HashSet<string>();
}
