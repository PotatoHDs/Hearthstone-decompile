using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200042B RID: 1067
public class TRL_Dungeon_Boss_208h : TRL_Dungeon
{
	// Token: 0x06003A2C RID: 14892 RVA: 0x0012A4D8 File Offset: 0x001286D8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Death_Long_03,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigRez_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigRez_02,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigRez_03,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_Seance_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_Seance_02,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_Surrender_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventMedic_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventMender_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventSoulbreaker_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventCopies_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A2D RID: 14893 RVA: 0x0012A720 File Offset: 0x00128920
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Death_Long_03;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01
		};
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01
		};
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01
		};
	}

	// Token: 0x06003A2E RID: 14894 RVA: 0x0012A833 File Offset: 0x00128A33
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 1001:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigRez_02, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03, 2.5f);
			break;
		case 1004:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_DeathrattleLines);
			break;
		case 1005:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HealingLines);
			break;
		case 1006:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_SpellLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A2F RID: 14895 RVA: 0x0012A849 File Offset: 0x00128A49
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
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 2636961905U)
		{
			if (num <= 2603406667U)
			{
				if (num != 2411652362U)
				{
					if (num == 2603406667U)
					{
						if (cardId == "TRL_502")
						{
							yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigRez_03, 2.5f);
						}
					}
				}
				else if (cardId == "TRL_260")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigRez_01, 2.5f);
				}
			}
			else if (num != 2620184286U)
			{
				if (num == 2636961905U)
				{
					if (cardId == "TRL_500")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_Surrender_01, 2.5f);
					}
				}
			}
			else if (cardId == "TRL_501")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01, 2.5f);
			}
		}
		else if (num <= 3958229895U)
		{
			if (num != 3572988482U)
			{
				if (num == 3958229895U)
				{
					if (cardId == "TRLA_152")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventSoulbreaker_01, 2.5f);
					}
				}
			}
			else if (cardId == "TRL_097")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_Seance_01, 2.5f);
			}
		}
		else if (num != 3975007514U)
		{
			if (num == 3991785133U)
			{
				if (cardId == "TRLA_150")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventMedic_01, 2.5f);
				}
			}
		}
		else if (cardId == "TRLA_151")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventMender_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400215D RID: 8541
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Death_Long_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Death_Long_03.prefab:84be873500689e24ab4107904dd1f040");

	// Token: 0x0400215E RID: 8542
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Greetings_01.prefab:df0c3a54ae4b8ff4e98a23600f2a9615");

	// Token: 0x0400215F RID: 8543
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Oops_01.prefab:c810f9ab2a4ed1b4fac70563c95c379e");

	// Token: 0x04002160 RID: 8544
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Sorry_01.prefab:faf5a3cbcb0dfc64b8a5207227a2ce65");

	// Token: 0x04002161 RID: 8545
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Thanks_01.prefab:f767120d831ffee498b8ddcd00c775fc");

	// Token: 0x04002162 RID: 8546
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Threaten_01.prefab:26662358917d41c459f9152115f0b291");

	// Token: 0x04002163 RID: 8547
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Well_Played_01.prefab:790ee61f6034ea6428e564431fd81aaf");

	// Token: 0x04002164 RID: 8548
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Emote_Respond_Wow_01.prefab:a98626c6cb1556741a48e7d830140a18");

	// Token: 0x04002165 RID: 8549
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Kill_Shrine_Generic_01.prefab:5cd92c3cc3d1af1418c0b005a77a7cfc");

	// Token: 0x04002166 RID: 8550
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Kill_Shrine_Rogue_01.prefab:60b57a978dca99142b61e78fc081840d");

	// Token: 0x04002167 RID: 8551
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Kill_Shrine_Warrior_01.prefab:159cadedae03c3545a5396480afabcc0");

	// Token: 0x04002168 RID: 8552
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_Killed_01.prefab:f82fd58230a531241be4c4928bffcde8");

	// Token: 0x04002169 RID: 8553
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_Killed_02.prefab:ef44e03a5e60997468893296d1c28398");

	// Token: 0x0400216A RID: 8554
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_Killed_03.prefab:2490eaf1df774e045ab8292b7f9b5816");

	// Token: 0x0400216B RID: 8555
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventCopies_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventCopies_01.prefab:aa03548831424464685ca0964845647a");

	// Token: 0x0400216C RID: 8556
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventMedic_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventMedic_01.prefab:21f65fa764d7eeb4291896f75e7f7145");

	// Token: 0x0400216D RID: 8557
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventMender_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventMender_01.prefab:b85bf9754c422ea449157e8046cfe17c");

	// Token: 0x0400216E RID: 8558
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_EventSoulbreaker_01 = new AssetReference("VO_TRLA_208h_Female_Troll_EventSoulbreaker_01.prefab:047f167bd32a715468bc0f4f7b0e3b38");

	// Token: 0x0400216F RID: 8559
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_AuchenaiPhantasm_01.prefab:79232de0686997d4b8e6f99390185cd6");

	// Token: 0x04002170 RID: 8560
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_Seance_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_Seance_01.prefab:05b23f6b543b88e4b9e82358ea00d36b");

	// Token: 0x04002171 RID: 8561
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_Seance_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_Seance_02.prefab:47732cbd20c21fa44b6c1b37690a9577");

	// Token: 0x04002172 RID: 8562
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Play_Surrender_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Play_Surrender_01.prefab:49b82c7abac913949bba1d34de4c0a77");

	// Token: 0x04002173 RID: 8563
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01.prefab:6f9e02b6505e8bb4d83841a6a6dd99f4");

	// Token: 0x04002174 RID: 8564
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02.prefab:3c783dfb2874be7418cac42d00db11f1");

	// Token: 0x04002175 RID: 8565
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03.prefab:9c01a87608c61174eac276a5e19ae340");

	// Token: 0x04002176 RID: 8566
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigRez_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigRez_01.prefab:0533bb6f24b1a1f469f0ebf91babdbd1");

	// Token: 0x04002177 RID: 8567
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigRez_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigRez_02.prefab:bd481b0579944f44ba9d7d5fcfdc250a");

	// Token: 0x04002178 RID: 8568
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigRez_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigRez_03.prefab:b899e5b1275cb0e4a8ed38c2e0a4b135");

	// Token: 0x04002179 RID: 8569
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01.prefab:9a90fea561ab3f14ba48452918be21bf");

	// Token: 0x0400217A RID: 8570
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigSummon_02.prefab:ec8e756fd7344be4987f68661a639f45");

	// Token: 0x0400217B RID: 8571
	private static readonly AssetReference VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03 = new AssetReference("VO_TRLA_208h_Female_Troll_Shrine_BigSummon_03.prefab:4b31d0e515164d54d941b8b45dc5a3fe");

	// Token: 0x0400217C RID: 8572
	private List<string> m_DeathrattleLines = new List<string>
	{
		TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigDamage_01,
		TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Play_Seance_02
	};

	// Token: 0x0400217D RID: 8573
	private List<string> m_HealingLines = new List<string>
	{
		TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigDamage_02,
		TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigDamage_03
	};

	// Token: 0x0400217E RID: 8574
	private List<string> m_SpellLines = new List<string>
	{
		TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_Shrine_BigSummon_01,
		TRL_Dungeon_Boss_208h.VO_TRLA_208h_Female_Troll_EventCopies_01
	};

	// Token: 0x0400217F RID: 8575
	private HashSet<string> m_playedLines = new HashSet<string>();
}
