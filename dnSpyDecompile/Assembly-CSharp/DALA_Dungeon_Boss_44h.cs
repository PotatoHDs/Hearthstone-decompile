using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000459 RID: 1113
public class DALA_Dungeon_Boss_44h : DALA_Dungeon
{
	// Token: 0x06003C70 RID: 15472 RVA: 0x0013B168 File Offset: 0x00139368
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_BossArakoaPlayed_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_BossShamanDamageSpell_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Death_02,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_DefeatPlayer_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_EmoteResponse_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_02,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_03,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_02,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_03,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_04,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_05,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Idle_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Idle_02,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Idle_03,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Intro_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_IntroKriziki_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C71 RID: 15473 RVA: 0x0013B30C File Offset: 0x0013950C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_EmoteResponse_01;
	}

	// Token: 0x06003C72 RID: 15474 RVA: 0x0013B344 File Offset: 0x00139544
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_44h.m_IdleLines;
	}

	// Token: 0x06003C73 RID: 15475 RVA: 0x0013B34C File Offset: 0x0013954C
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_01,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_02,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_03,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_04,
			DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_05
		};
	}

	// Token: 0x06003C74 RID: 15476 RVA: 0x0013B3B0 File Offset: 0x001395B0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Kriziki")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003C75 RID: 15477 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003C76 RID: 15478 RVA: 0x0013B45B File Offset: 0x0013965B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_44h.m_BossBurnsCardOnDraw);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003C77 RID: 15479 RVA: 0x0013B471 File Offset: 0x00139671
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "ICC_407")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_44h.m_Gnomeferatu);
		}
		yield break;
	}

	// Token: 0x06003C78 RID: 15480 RVA: 0x0013B487 File Offset: 0x00139687
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "GIL_600") && !(cardId == "EX1_238") && !(cardId == "EX1_245"))
		{
			if (cardId == "OG_293" || cardId == "CFM_626")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_BossArakoaPlayed_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_BossShamanDamageSpell_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040025FE RID: 9726
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_BossArakoaPlayed_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_BossArakoaPlayed_01.prefab:2b8a991f05085af49939c9b32477bae8");

	// Token: 0x040025FF RID: 9727
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_BossShamanDamageSpell_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_BossShamanDamageSpell_01.prefab:bc022a1bc6a6b0946b9e93cdec79ef40");

	// Token: 0x04002600 RID: 9728
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_Death_02 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_Death_02.prefab:f5bf9ffbaf7275e498664c5cff721bb5");

	// Token: 0x04002601 RID: 9729
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_DefeatPlayer_01.prefab:5fdb529ce1bc8da4f9c974cc87012514");

	// Token: 0x04002602 RID: 9730
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_EmoteResponse_01.prefab:95b9a2a8c81bcc6499926001d31b5939");

	// Token: 0x04002603 RID: 9731
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_01.prefab:3e9b2a4b9c944fe4ebcbc4022a6b2be8");

	// Token: 0x04002604 RID: 9732
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_02 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_02.prefab:beb97e4e5a22cfe418b259ef1310383f");

	// Token: 0x04002605 RID: 9733
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_03 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_03.prefab:339a3bfab079bc947a2ca8500f9330e8");

	// Token: 0x04002606 RID: 9734
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_01.prefab:2b2253ffb72c06248a2205b113a5d865");

	// Token: 0x04002607 RID: 9735
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_02 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_02.prefab:38a4df2dcfffa634480f81d82bae3dff");

	// Token: 0x04002608 RID: 9736
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_03 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_03.prefab:ae2a338cc382bb1428ba11f2a726f84f");

	// Token: 0x04002609 RID: 9737
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_04 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_04.prefab:69a8371277b25a74499783548d407298");

	// Token: 0x0400260A RID: 9738
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_05 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_HeroPower_05.prefab:821a0aafb777f2143851e65b2836af23");

	// Token: 0x0400260B RID: 9739
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_Idle_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_Idle_01.prefab:b0e87c0523eaed745a705994ab101c3a");

	// Token: 0x0400260C RID: 9740
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_Idle_02 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_Idle_02.prefab:40783cb4a8355b847ba7ab57f1b7e7ad");

	// Token: 0x0400260D RID: 9741
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_Idle_03 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_Idle_03.prefab:f4e01ba448bdb5d46b50f636375b5d13");

	// Token: 0x0400260E RID: 9742
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_Intro_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_Intro_01.prefab:9c67a65d3460aef42bf1096904403de9");

	// Token: 0x0400260F RID: 9743
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_IntroKriziki_01.prefab:befc9255c7514a648b3cbf6f72cd78a0");

	// Token: 0x04002610 RID: 9744
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_01 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_01.prefab:1578e4a57d072234f977b69ff9b35ca6");

	// Token: 0x04002611 RID: 9745
	private static readonly AssetReference VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_02 = new AssetReference("VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_02.prefab:7b823ebad582c4346972e779258d1854");

	// Token: 0x04002612 RID: 9746
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Idle_01,
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Idle_02,
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_Idle_03
	};

	// Token: 0x04002613 RID: 9747
	private static List<string> m_Gnomeferatu = new List<string>
	{
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_01,
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_PlayerGnomeferatu_02
	};

	// Token: 0x04002614 RID: 9748
	private static List<string> m_BossBurnsCardOnDraw = new List<string>
	{
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_01,
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_02,
		DALA_Dungeon_Boss_44h.VO_DALA_BOSS_44h_Male_Arakkoa_HandFull_03
	};

	// Token: 0x04002615 RID: 9749
	private HashSet<string> m_playedLines = new HashSet<string>();
}
