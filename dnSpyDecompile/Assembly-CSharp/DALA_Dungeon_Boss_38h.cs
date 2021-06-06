using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000453 RID: 1107
public class DALA_Dungeon_Boss_38h : DALA_Dungeon
{
	// Token: 0x06003C25 RID: 15397 RVA: 0x001394D4 File Offset: 0x001376D4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Death_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_DefeatPlayer_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Idle_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Idle_02,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Idle_03,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Intro_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C26 RID: 15398 RVA: 0x001396A8 File Offset: 0x001378A8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003C27 RID: 15399 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C28 RID: 15400 RVA: 0x001396E0 File Offset: 0x001378E0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05,
			DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06
		};
	}

	// Token: 0x06003C29 RID: 15401 RVA: 0x00139742 File Offset: 0x00137942
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_38h.m_IdleLines;
	}

	// Token: 0x06003C2A RID: 15402 RVA: 0x00139749 File Offset: 0x00137949
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003C2B RID: 15403 RVA: 0x0013975F File Offset: 0x0013795F
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 735099362U)
		{
			if (num <= 634433648U)
			{
				if (num != 54306029U)
				{
					if (num != 634433648U)
					{
						goto IL_361;
					}
					if (!(cardId == "KAR_073"))
					{
						goto IL_361;
					}
				}
				else if (!(cardId == "DALA_BOSS_38t"))
				{
					goto IL_361;
				}
			}
			else if (num != 701544124U)
			{
				if (num != 718321743U)
				{
					if (num != 735099362U)
					{
						goto IL_361;
					}
					if (!(cardId == "KAR_075"))
					{
						goto IL_361;
					}
				}
				else if (!(cardId == "KAR_076"))
				{
					goto IL_361;
				}
			}
			else if (!(cardId == "KAR_077"))
			{
				goto IL_361;
			}
		}
		else if (num <= 1034316038U)
		{
			if (num != 1005806932U)
			{
				if (num != 1034316038U)
				{
					goto IL_361;
				}
				if (!(cardId == "LOOT_370"))
				{
					goto IL_361;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01, 2.5f);
				goto IL_361;
			}
			else if (!(cardId == "KAR_091"))
			{
				goto IL_361;
			}
		}
		else if (num != 3155791879U)
		{
			if (num != 3755629543U)
			{
				if (num != 3767375410U)
				{
					goto IL_361;
				}
				if (!(cardId == "EX1_312"))
				{
					goto IL_361;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01, 2.5f);
				goto IL_361;
			}
			else if (!(cardId == "GVG_003"))
			{
				goto IL_361;
			}
		}
		else
		{
			if (!(cardId == "ICC_903"))
			{
				goto IL_361;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01, 2.5f);
			goto IL_361;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02, 2.5f);
		IL_361:
		yield break;
	}

	// Token: 0x06003C2C RID: 15404 RVA: 0x00139775 File Offset: 0x00137975
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 701544124U)
		{
			if (num != 54306029U)
			{
				if (num != 634433648U)
				{
					if (num != 701544124U)
					{
						goto IL_24E;
					}
					if (!(cardId == "KAR_077"))
					{
						goto IL_24E;
					}
				}
				else if (!(cardId == "KAR_073"))
				{
					goto IL_24E;
				}
			}
			else if (!(cardId == "DALA_BOSS_38t"))
			{
				goto IL_24E;
			}
		}
		else if (num <= 735099362U)
		{
			if (num != 718321743U)
			{
				if (num != 735099362U)
				{
					goto IL_24E;
				}
				if (!(cardId == "KAR_075"))
				{
					goto IL_24E;
				}
			}
			else
			{
				if (!(cardId == "KAR_076"))
				{
					goto IL_24E;
				}
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01, 2.5f);
				goto IL_24E;
			}
		}
		else if (num != 1005806932U)
		{
			if (num != 3755629543U)
			{
				goto IL_24E;
			}
			if (!(cardId == "GVG_003"))
			{
				goto IL_24E;
			}
		}
		else if (!(cardId == "KAR_091"))
		{
			goto IL_24E;
		}
		yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_38h.m_BossPortal);
		IL_24E:
		yield break;
	}

	// Token: 0x04002579 RID: 9593
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01.prefab:5b826b60e67152b4885635c113d05b4c");

	// Token: 0x0400257A RID: 9594
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02.prefab:e39f74d9a0036024e9d01860b69a79c5");

	// Token: 0x0400257B RID: 9595
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03.prefab:4d566e2c93f4c9a45862f379911165d6");

	// Token: 0x0400257C RID: 9596
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04.prefab:d0444d1589adbdb41b7da76fa85fcc26");

	// Token: 0x0400257D RID: 9597
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05.prefab:9e86f21294ea754469acb5e3c043fb08");

	// Token: 0x0400257E RID: 9598
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06.prefab:c940ade1ed7b524449c124a1e87e99bb");

	// Token: 0x0400257F RID: 9599
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Death_01.prefab:e8650f9cf9569ef4eaf429f7a60bc578");

	// Token: 0x04002580 RID: 9600
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_DefeatPlayer_01.prefab:933653975b69d2b438891b976067f5d4");

	// Token: 0x04002581 RID: 9601
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_EmoteResponse_01.prefab:e6297f146486096408c1e2381b634a5e");

	// Token: 0x04002582 RID: 9602
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_02.prefab:c58df3a37ad4e154cac7d1562c12caca");

	// Token: 0x04002583 RID: 9603
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_03.prefab:bf66eea2f3e114b4784b0a435b9f966a");

	// Token: 0x04002584 RID: 9604
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_04.prefab:c883ef4410295824e9925ae8a6b2cc8e");

	// Token: 0x04002585 RID: 9605
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_05.prefab:140641b94f9b2b84f947ad6b216f29ed");

	// Token: 0x04002586 RID: 9606
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPower_06.prefab:b69d3d582b850974ab91915d390de1b1");

	// Token: 0x04002587 RID: 9607
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_HeroPowerPlayFirelandsPortal_01.prefab:8d130e514087e0d499d469db1392bf91");

	// Token: 0x04002588 RID: 9608
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Idle_01.prefab:c3e13e097b1b6d948ba2bf3d20f594dd");

	// Token: 0x04002589 RID: 9609
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Idle_02.prefab:b569d23be124c0f4586891fd62f4a7bc");

	// Token: 0x0400258A RID: 9610
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Idle_03.prefab:56a6d2785a3acab44ac6ae7e58a2a5ba");

	// Token: 0x0400258B RID: 9611
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_Intro_01.prefab:7edcdc4307049924a955327b72093264");

	// Token: 0x0400258C RID: 9612
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerGatherYourParty_01.prefab:e106e003d3d797e42a83a25e9f900f91");

	// Token: 0x0400258D RID: 9613
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerPortal_02.prefab:6712c015107aa9d4c93f0b7db0f7cff7");

	// Token: 0x0400258E RID: 9614
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerSanguineReveler_01.prefab:7c03cf81502beab40b9072c7d891e6a9");

	// Token: 0x0400258F RID: 9615
	private static readonly AssetReference VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01 = new AssetReference("VO_DALA_BOSS_38h_Female_Gnome_PlayerTwistingNether_01.prefab:3358cc7d1df59444ab2ef4f1e62a820e");

	// Token: 0x04002590 RID: 9616
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002591 RID: 9617
	private static List<string> m_BossPortal = new List<string>
	{
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_01,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_02,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_03,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_04,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_05,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_BossPortal_06
	};

	// Token: 0x04002592 RID: 9618
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Idle_01,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Idle_02,
		DALA_Dungeon_Boss_38h.VO_DALA_BOSS_38h_Female_Gnome_Idle_03
	};
}
