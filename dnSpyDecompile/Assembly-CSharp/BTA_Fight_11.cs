using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004F5 RID: 1269
public class BTA_Fight_11 : BTA_Dungeon
{
	// Token: 0x06004438 RID: 17464 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004439 RID: 17465 RVA: 0x0017190C File Offset: 0x0016FB0C
	public BTA_Fight_11()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_11.s_booleanOptions);
	}

	// Token: 0x0600443A RID: 17466 RVA: 0x001719C0 File Offset: 0x0016FBC0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_04B_01,
			BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_05A_01,
			BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_06A_01,
			BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_PlayerStart_01,
			BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_VictoryA_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_AmbushTrigger_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Attack_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Penance_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_RustswornCultist_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossDeath_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossStart_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Emote_Response_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_CommandtheIllidari_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_UrzulHorror_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_02,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_03,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_04,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleA_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleB_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleC_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Misc_01,
			BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_RustedLegionGanarg_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04A_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04C_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05B_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05C_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_06B_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_VictoryB_01,
			BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_UI_Mission_Fight_11_Turn_1_Flavor_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600443B RID: 17467 RVA: 0x00171C04 File Offset: 0x0016FE04
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_11h_IdleLines;
	}

	// Token: 0x0600443C RID: 17468 RVA: 0x00171C0C File Offset: 0x0016FE0C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossDeath_01;
	}

	// Token: 0x0600443D RID: 17469 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600443E RID: 17470 RVA: 0x00171C24 File Offset: 0x0016FE24
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600443F RID: 17471 RVA: 0x00171C34 File Offset: 0x0016FE34
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004440 RID: 17472 RVA: 0x00171CBC File Offset: 0x0016FEBC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 500)
		{
			if (missionEvent == 100)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_AmbushTrigger_01, 2.5f);
				goto IL_1DD;
			}
			if (missionEvent == 500)
			{
				base.PlaySound(BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Attack_01, 1f, true, false);
				goto IL_1DD;
			}
		}
		else
		{
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor2, BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_VictoryA_01, 2.5f);
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_VictoryB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_1DD;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayRandomLineAlways(actor, this.m_missionEventTrigger105_Lines);
				goto IL_1DD;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1DD:
		yield break;
	}

	// Token: 0x06004441 RID: 17473 RVA: 0x00171CD2 File Offset: 0x0016FED2
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_173"))
		{
			if (!(cardId == "BT_407"))
			{
				if (cardId == "BTA_06")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Misc_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_UrzulHorror_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_CommandtheIllidari_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004442 RID: 17474 RVA: 0x00171CE8 File Offset: 0x0016FEE8
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_160"))
		{
			if (!(cardId == "BTA_12"))
			{
				if (cardId == "CFM_781")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Penance_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_RustedLegionGanarg_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_RustswornCultist_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004443 RID: 17475 RVA: 0x00171CFE File Offset: 0x0016FEFE
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 3)
		{
			if (turn != 1)
			{
				if (turn == 3)
				{
					yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04A_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_04B_01, 2.5f);
					yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04C_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_UI_Mission_Fight_11_Turn_1_Flavor_01, 2.5f);
			}
		}
		else if (turn != 7)
		{
			if (turn == 11)
			{
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_06A_01, 2.5f);
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_06B_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_11.VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_05A_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05B_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_11.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05C_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040036A3 RID: 13987
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_11.InitBooleanOptions();

	// Token: 0x040036A4 RID: 13988
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_04B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_04B_01.prefab:571fe638b871c094a84d087df3b0616f");

	// Token: 0x040036A5 RID: 13989
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_05A_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_05A_01.prefab:f05bc37056f694744bbfd2a6e9ed0107");

	// Token: 0x040036A6 RID: 13990
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_06A_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_11_Bonding_06A_01.prefab:9057d1ffe2dcffa4590cded80a1a8196");

	// Token: 0x040036A7 RID: 13991
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_11_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_11_PlayerStart_01.prefab:97096cf61f5264e42a214b2d6f64959e");

	// Token: 0x040036A8 RID: 13992
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_11_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_11_VictoryA_01.prefab:d3b69ef6c17b61743866e563e62596c2");

	// Token: 0x040036A9 RID: 13993
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_AmbushTrigger_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_AmbushTrigger_01.prefab:5b7de994202af1448aa9decedeb90799");

	// Token: 0x040036AA RID: 13994
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Attack_01.prefab:c24f1a153b2184540a57d92ab853af5b");

	// Token: 0x040036AB RID: 13995
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Penance_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_Penance_01.prefab:0f5ecc3ebd6e68442b87ebd1ff697ba9");

	// Token: 0x040036AC RID: 13996
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_RustswornCultist_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Boss_RustswornCultist_01.prefab:e36b7ad491a62c645adef3aafd3433a3");

	// Token: 0x040036AD RID: 13997
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossDeath_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossDeath_01.prefab:39b22c8b10b36d44f976a827d8a47b59");

	// Token: 0x040036AE RID: 13998
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossStart_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_BossStart_01.prefab:c62508be2248a834aaf8c57fafc68623");

	// Token: 0x040036AF RID: 13999
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Emote_Response_01.prefab:8df04a710d3a8f349b6ffe348b7a4606");

	// Token: 0x040036B0 RID: 14000
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_CommandtheIllidari_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_CommandtheIllidari_01.prefab:8b948e5abf2b2fa47b25f452a851ee19");

	// Token: 0x040036B1 RID: 14001
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_UrzulHorror_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Hero_UrzulHorror_01.prefab:5acb38a9e6e22b541a11678f40d8a09a");

	// Token: 0x040036B2 RID: 14002
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_01.prefab:bf9f5a59be91f0642b76116021045f5b");

	// Token: 0x040036B3 RID: 14003
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_02.prefab:668490af1baa90e4eaa65be304c4e17e");

	// Token: 0x040036B4 RID: 14004
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_03.prefab:bb582781ee6b72142ad06a6cb08784a7");

	// Token: 0x040036B5 RID: 14005
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_04.prefab:3b5daacb133e2a74d8b5258b60c9bf08");

	// Token: 0x040036B6 RID: 14006
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleA_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleA_01.prefab:111d31e0c9f912e469388935d361ed84");

	// Token: 0x040036B7 RID: 14007
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleB_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleB_01.prefab:5bf6c40e21857d147939033f6a467bf3");

	// Token: 0x040036B8 RID: 14008
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleC_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleC_01.prefab:58a585790e6c16844baa37ecd986ade8");

	// Token: 0x040036B9 RID: 14009
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Misc_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_Misc_01.prefab:b89ba069b9017364c880ee8df315d254");

	// Token: 0x040036BA RID: 14010
	private static readonly AssetReference VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_RustedLegionGanarg_01 = new AssetReference("VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_RustedLegionGanarg_01.prefab:49d6fd66d60aabb4fa5fa50a8c0c7ffc");

	// Token: 0x040036BB RID: 14011
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04A_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04A_01.prefab:e53b8d31a63d53e49a4f8010817cc667");

	// Token: 0x040036BC RID: 14012
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_04C_01.prefab:36a44b8818a5d164b982ab1e349ded91");

	// Token: 0x040036BD RID: 14013
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05B_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05B_01.prefab:7b232f545b1d4ea46a615f3a4ef539da");

	// Token: 0x040036BE RID: 14014
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_05C_01.prefab:ad777afec9f9c4d41b935f92cd7fb197");

	// Token: 0x040036BF RID: 14015
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_06B_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_Bonding_06B_01.prefab:2c051bd8f3e3854488d0e9ce5e560c9c");

	// Token: 0x040036C0 RID: 14016
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_VictoryB_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_11_VictoryB_01.prefab:b1a421d657ab60b438e76b459a08d19e");

	// Token: 0x040036C1 RID: 14017
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_UI_Mission_Fight_11_Turn_1_Flavor_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_UI_Mission_Fight_11_Turn_1_Flavor_01.prefab:12e7a0d0526302c4a9fd1a2dca6760dd");

	// Token: 0x040036C2 RID: 14018
	private List<string> m_VO_BTA_BOSS_11h_IdleLines = new List<string>
	{
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleA_01,
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleB_01,
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_IdleC_01
	};

	// Token: 0x040036C3 RID: 14019
	private List<string> m_missionEventTrigger105_Lines = new List<string>
	{
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_01,
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_02,
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_03,
		BTA_Fight_11.VO_BTA_BOSS_11h_Female_Arakkoa_Mission_Fight_11_HeroPowerTrigger_04
	};

	// Token: 0x040036C4 RID: 14020
	private HashSet<string> m_playedLines = new HashSet<string>();
}
