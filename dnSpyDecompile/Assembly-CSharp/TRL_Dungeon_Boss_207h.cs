using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200042A RID: 1066
public class TRL_Dungeon_Boss_207h : TRL_Dungeon
{
	// Token: 0x06003A24 RID: 14884 RVA: 0x00129E94 File Offset: 0x00128094
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Death_Long_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_Killed_04,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_EVENT_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_EVENT_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_EVENT_03,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_FirePlume_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Antonidas_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Janalai_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Pyroblast_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Fireball_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Flamestrike_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Opponent_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Trigger_Meteor_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x0012A11C File Offset: 0x0012831C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Death_Long_01;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_Killed_04
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06
		};
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01
		};
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01
		};
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003A27 RID: 14887 RVA: 0x0012A24F File Offset: 0x0012844F
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_EVENT_01, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_EVENT_02, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_EVENT_03, 2.5f);
			break;
		case 1004:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A28 RID: 14888 RVA: 0x0012A265 File Offset: 0x00128465
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
		if (num <= 2056677053U)
		{
			if (num <= 1659401902U)
			{
				if (num != 611277770U)
				{
					if (num != 1440752425U)
					{
						if (num == 1659401902U)
						{
							if (cardId == "TRLA_133")
							{
								yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Kill_Opponent_01, 2.5f);
							}
						}
					}
					else if (cardId == "EX1_559")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Antonidas_01, 2.5f);
					}
				}
				else if (cardId == "TRLA_129s")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01, 2.5f);
				}
			}
			else if (num <= 2006344196U)
			{
				if (num != 1692957140U)
				{
					if (num == 2006344196U)
					{
						if (cardId == "TRL_316")
						{
							yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Janalai_01, 2.5f);
						}
					}
				}
				else if (cardId == "TRLA_135")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01, 2.5f);
				}
			}
			else if (num != 2023121815U)
			{
				if (num == 2056677053U)
				{
					if (cardId == "TRL_315")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01, 2.5f);
					}
				}
			}
			else if (cardId == "TRL_317")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01, 2.5f);
			}
		}
		else if (num <= 3945181129U)
		{
			if (num != 3159778034U)
			{
				if (num != 3623069510U)
				{
					if (num == 3945181129U)
					{
						if (cardId == "CS2_029")
						{
							yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Fireball_01, 2.5f);
						}
					}
				}
				else if (cardId == "CS2_118")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01, 2.5f);
				}
			}
			else if (cardId == "GIL_147")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01, 2.5f);
			}
		}
		else if (num <= 4196992509U)
		{
			if (num != 4039880748U)
			{
				if (num == 4196992509U)
				{
					if (cardId == "CS2_032")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Flamestrike_01, 2.5f);
					}
				}
			}
			else if (cardId == "UNG_084")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_FirePlume_01, 2.5f);
			}
		}
		else if (num != 4221058965U)
		{
			if (num == 4257410314U)
			{
				if (cardId == "EX1_279")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Play_Pyroblast_01, 2.5f);
				}
			}
		}
		else if (cardId == "UNG_955")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Trigger_Meteor_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002138 RID: 8504
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Death_Long_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Death_Long_01.prefab:bc8b65ed675f5e64cbba0999d87f40a3");

	// Token: 0x04002139 RID: 8505
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Greetings_01.prefab:96283758d1899b24390678b7abb72f70");

	// Token: 0x0400213A RID: 8506
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Oops_01.prefab:78ebc7d0c76e8244c9fd46fc27389eaf");

	// Token: 0x0400213B RID: 8507
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Sorry_01.prefab:bdcf2acc88e78b1488ec32fe19ee22fb");

	// Token: 0x0400213C RID: 8508
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Thanks_01.prefab:b2fe442969ca8a04cae99f02da49ed3a");

	// Token: 0x0400213D RID: 8509
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Threaten_02.prefab:b28587853451b0845b453b865ccc8287");

	// Token: 0x0400213E RID: 8510
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Well_Played_01.prefab:5e6e398c52cc12f43b9082044c704b6e");

	// Token: 0x0400213F RID: 8511
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Emote_Respond_Wow_01.prefab:ea26d56e3a26b7f499758b21d1fb7eae");

	// Token: 0x04002140 RID: 8512
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_01.prefab:120e30b322f7c414aaa1ebae6943b3cf");

	// Token: 0x04002141 RID: 8513
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_02.prefab:88924bd3d4866cc4c99e2d0f636bf927");

	// Token: 0x04002142 RID: 8514
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Generic_06.prefab:c35704f96906a0943826f17e1fd7bd47");

	// Token: 0x04002143 RID: 8515
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Paladin_01.prefab:c8679b5ad3b83d44d81ba2d274c022f7");

	// Token: 0x04002144 RID: 8516
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Shrine_Shaman_01.prefab:af248fdd112479c43aec097e8a3aafe7");

	// Token: 0x04002145 RID: 8517
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_Killed_02.prefab:951230a7e4e8f43489baa8e6aa4cdd1e");

	// Token: 0x04002146 RID: 8518
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_Killed_03.prefab:6ef4189287a81864d91e7cb1053ff0c2");

	// Token: 0x04002147 RID: 8519
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_Killed_04 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_Killed_04.prefab:3def3ba7d51db304e98a904efa230e0d");

	// Token: 0x04002148 RID: 8520
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_EVENT_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_EVENT_01.prefab:e699568563b9bde4fbb55fe1eed55a4f");

	// Token: 0x04002149 RID: 8521
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_EVENT_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_EVENT_02.prefab:004297da355f7c84ebc31481913de1e4");

	// Token: 0x0400214A RID: 8522
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_EVENT_03 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_EVENT_03.prefab:8a600e0ab04979e41829dbfb50995f02");

	// Token: 0x0400214B RID: 8523
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Kill_Opponent_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Kill_Opponent_01.prefab:a946177a47b732d4ebcbcdf4acf2f25f");

	// Token: 0x0400214C RID: 8524
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Antonidas_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Antonidas_01.prefab:c6d71428280ac1e49b13d32ec86d3bae");

	// Token: 0x0400214D RID: 8525
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Blast_Wave_01.prefab:f8b9d0c8330b19640a0c1bc6f6784ec1");

	// Token: 0x0400214E RID: 8526
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Fan_of_Flames_01.prefab:bcceb9f377808a945923b9febd0afa4d");

	// Token: 0x0400214F RID: 8527
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Fireball_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Fireball_01.prefab:d49c30de5a10a7141a0e158a99b24580");

	// Token: 0x04002150 RID: 8528
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Flamestrike_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Flamestrike_01.prefab:9c4199b67866fab4faed66c4df6288ff");

	// Token: 0x04002151 RID: 8529
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Janalai_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Janalai_01.prefab:3976a9f385b557f48915611660ecc6ff");

	// Token: 0x04002152 RID: 8530
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Magma_Rager_01.prefab:382a52eeec0c0db4c967f89193f2ab01");

	// Token: 0x04002153 RID: 8531
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Pyroblast_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Pyroblast_01.prefab:da68f6a1a92ff6748b6dc919dd1d72a3");

	// Token: 0x04002154 RID: 8532
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_Pyromaniac_01.prefab:2b865e8769a80c049b8979c36c418c55");

	// Token: 0x04002155 RID: 8533
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Play_FirePlume_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Play_FirePlume_01.prefab:ad7e903289effe14fb1ef56802c9dfcb");

	// Token: 0x04002156 RID: 8534
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01.prefab:cf280a70e937695439e47ef09b4c9681");

	// Token: 0x04002157 RID: 8535
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02 = new AssetReference("VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02.prefab:ca9e17a35fd72484d8f67aaf8f5b1e2f");

	// Token: 0x04002158 RID: 8536
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Trigger_Cinderstorm_01.prefab:1a73f9709282a9942a262f0a86673e3e");

	// Token: 0x04002159 RID: 8537
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Trigger_Frostfire_01.prefab:8d48c00adb96adb46970cc5a6c080356");

	// Token: 0x0400215A RID: 8538
	private static readonly AssetReference VO_TRLA_207h_Male_Troll_Trigger_Meteor_01 = new AssetReference("VO_TRLA_207h_Male_Troll_Trigger_Meteor_01.prefab:8e5af4adfe5990340b5df77c887a1fe0");

	// Token: 0x0400215B RID: 8539
	private List<string> m_HeroPowerLines = new List<string>
	{
		TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_01,
		TRL_Dungeon_Boss_207h.VO_TRLA_207h_Male_Troll_Shrine_ACTIVATE_02
	};

	// Token: 0x0400215C RID: 8540
	private HashSet<string> m_playedLines = new HashSet<string>();
}
