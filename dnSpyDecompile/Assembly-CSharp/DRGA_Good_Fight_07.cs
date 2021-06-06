using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E1 RID: 1249
public class DRGA_Good_Fight_07 : DRGA_Dungeon
{
	// Token: 0x060042EB RID: 17131 RVA: 0x0016935C File Offset: 0x0016755C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_07.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_07_Backstory_01a_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Backstory_01b_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_02,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Misc_01_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_PlayerStart_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_RotwingCaptured_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_TurnOne_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Victory_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_Death_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_01_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_02_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_03_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossAttack_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossStart_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BrannIdle_01_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_EmoteResponse_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_01_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_02_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_03_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_02_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_03_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_04_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_05_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_06_01,
			DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Player_Rotwing_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042EC RID: 17132 RVA: 0x00169560 File Offset: 0x00167760
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_IdleLines;
	}

	// Token: 0x060042ED RID: 17133 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060042EE RID: 17134 RVA: 0x00169568 File Offset: 0x00167768
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_Death_01;
	}

	// Token: 0x060042EF RID: 17135 RVA: 0x00169580 File Offset: 0x00167780
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060042F0 RID: 17136 RVA: 0x00169611 File Offset: 0x00167811
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_07.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_07_Backstory_01a_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Backstory_01b_01, 2.5f);
				goto IL_420;
			}
			goto IL_420;
		case 102:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Misc_01_01, 2.5f);
				goto IL_420;
			}
			goto IL_420;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_RotwingCaptured_01, 2.5f);
				goto IL_420;
			}
			goto IL_420;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_TurnOne_01, 2.5f);
				goto IL_420;
			}
			goto IL_420;
		case 105:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Victory_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_06_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_420;
			}
			goto IL_420;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventTrigger106Lines);
			goto IL_420;
		case 107:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossAttack_01, 2.5f);
			goto IL_420;
		case 108:
			yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_02_01, 2.5f);
			goto IL_420;
		case 109:
			yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_03_01, 2.5f);
			goto IL_420;
		case 110:
			yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_04_01, 2.5f);
			goto IL_420;
		case 111:
			yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_05_01, 2.5f);
			goto IL_420;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_420:
		yield break;
	}

	// Token: 0x060042F1 RID: 17137 RVA: 0x00169627 File Offset: 0x00167827
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "DRGA_BOSS_04t"))
		{
			if (cardId == "YOD_036")
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Player_Rotwing_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor2, this.m_VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_DragonbreathLines);
		}
		yield break;
	}

	// Token: 0x060042F2 RID: 17138 RVA: 0x0016963D File Offset: 0x0016783D
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
		yield break;
	}

	// Token: 0x040033E7 RID: 13287
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_07_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_07_Backstory_01a_01.prefab:7033ca23bb5b3384d8f3fdca08b55488");

	// Token: 0x040033E8 RID: 13288
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Backstory_01b_01.prefab:280a2851162529f40aedfbebdaf24145");

	// Token: 0x040033E9 RID: 13289
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_01.prefab:f51c135e7a81a904dacfd433f35b1274");

	// Token: 0x040033EA RID: 13290
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_02 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_02.prefab:240c24a6aa1d0df47aee6af9d9ea7b2f");

	// Token: 0x040033EB RID: 13291
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Misc_01_01.prefab:776a9f12df22e7441904a3285c0c403f");

	// Token: 0x040033EC RID: 13292
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_PlayerStart_01.prefab:6618c7c7da1bb354388dbdfa7d68f6a8");

	// Token: 0x040033ED RID: 13293
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_RotwingCaptured_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_RotwingCaptured_01.prefab:ff7d3fbc2412adb4cb702195549219ed");

	// Token: 0x040033EE RID: 13294
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_TurnOne_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_TurnOne_01.prefab:f3406880fcab36c42abca7246646fa14");

	// Token: 0x040033EF RID: 13295
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Victory_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Victory_01.prefab:b2e35fdf67c238c409ae31f8ce3df139");

	// Token: 0x040033F0 RID: 13296
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_Death_01.prefab:4d00dd974d1496d4a9eaaa95c4cc99aa");

	// Token: 0x040033F1 RID: 13297
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_01_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_01_01.prefab:d4991e5db14d6464db6032a4fc30a95f");

	// Token: 0x040033F2 RID: 13298
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_02_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_02_01.prefab:7eb0b4f79c563514c89fb2a7607814d5");

	// Token: 0x040033F3 RID: 13299
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_03_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_03_01.prefab:5eef3d52f04244544aaa4911b2c9a35c");

	// Token: 0x040033F4 RID: 13300
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossAttack_01.prefab:326aada7a3277244e946724ed8f8d25b");

	// Token: 0x040033F5 RID: 13301
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossStart_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BossStart_01.prefab:eaf1cd45f0b9d2b4bbebf455b402283d");

	// Token: 0x040033F6 RID: 13302
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BrannIdle_01_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_BrannIdle_01_01.prefab:04b1e548994fd2d47bc351f5f308dd69");

	// Token: 0x040033F7 RID: 13303
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_EmoteResponse_01.prefab:dc524f3a9ce93e941bc12c0cf1c227e6");

	// Token: 0x040033F8 RID: 13304
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_01_01.prefab:ecef7a29e9f13a948a33bfbb88f34985");

	// Token: 0x040033F9 RID: 13305
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_02_01.prefab:14dc271f0e6ea08498f0fb965618c782");

	// Token: 0x040033FA RID: 13306
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_03_01.prefab:6ed8c497fc1538648ac05162c5f5c589");

	// Token: 0x040033FB RID: 13307
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_02_01.prefab:dd6457b68fdf7ed468a1c92c2e264429");

	// Token: 0x040033FC RID: 13308
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_03_01.prefab:de1341dcf88c1e7469a6e742cdcd3a9d");

	// Token: 0x040033FD RID: 13309
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_04_01.prefab:a9f14b3f910dcc2489af329df073d599");

	// Token: 0x040033FE RID: 13310
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_05_01.prefab:900435e8f499da04391a2b07373de8b2");

	// Token: 0x040033FF RID: 13311
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Misc_06_01.prefab:d617ccd45c4ffb447a97f1d259550b40");

	// Token: 0x04003400 RID: 13312
	private static readonly AssetReference VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Player_Rotwing_01 = new AssetReference("VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Player_Rotwing_01.prefab:3b670cff8181a5e4f9ba9302e49b7a9c");

	// Token: 0x04003401 RID: 13313
	private List<string> m_missionEventTrigger106Lines = new List<string>
	{
		DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_01_01,
		DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_02_01,
		DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Boss_HeroPowerTrigger_03_01
	};

	// Token: 0x04003402 RID: 13314
	private List<string> m_VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_DragonbreathLines = new List<string>
	{
		DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_01,
		DRGA_Good_Fight_07.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_07_Brann_Dragonbreath_02
	};

	// Token: 0x04003403 RID: 13315
	private List<string> m_VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_IdleLines = new List<string>
	{
		DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_01_01,
		DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_02_01,
		DRGA_Good_Fight_07.VO_DRGA_BOSS_19h_Male_Dragon_Good_Fight_07_Idle_03_01
	};

	// Token: 0x04003404 RID: 13316
	private HashSet<string> m_playedLines = new HashSet<string>();
}
