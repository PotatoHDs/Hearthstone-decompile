using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000427 RID: 1063
public class TRL_Dungeon_Boss_204h : TRL_Dungeon
{
	// Token: 0x06003A0A RID: 14858 RVA: 0x00128E10 File Offset: 0x00127010
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Death_Long_03,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Panther_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Lynx_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Harbinger_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_RandomSpell_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Reducecost_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Hatchet_02
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x00128FE8 File Offset: 0x001271E8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Death_Long_03;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01
		};
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01
		};
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003A0D RID: 14861 RVA: 0x001290E1 File Offset: 0x001272E1
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_RandomSpell_01, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Reducecost_01, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02, 2.5f);
			break;
		case 1004:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02, 2.5f);
			break;
		case 1005:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A0E RID: 14862 RVA: 0x001290F7 File Offset: 0x001272F7
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
		if (!(cardId == "TRL_901"))
		{
			if (!(cardId == "TRL_900"))
			{
				if (!(cardId == "TRLA_165"))
				{
					if (!(cardId == "TRL_348"))
					{
						if (!(cardId == "TRLA_166"))
						{
							if (cardId == "TRL_111")
							{
								yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Hatchet_02, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Harbinger_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Lynx_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_204h.VO_TRLA_204h_Male_Troll_Event_Panther_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040020DF RID: 8415
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Death_Long_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Death_Long_03.prefab:fda150aeab916c247b5e7849cf7c17fd");

	// Token: 0x040020E0 RID: 8416
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Greetings_01.prefab:002210047b2026043aca9bec75914b70");

	// Token: 0x040020E1 RID: 8417
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Oops_01.prefab:55792cf328c80294c9a70143ab1c55f9");

	// Token: 0x040020E2 RID: 8418
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Sorry_01.prefab:210dc50662e3c564b8ab24b060c72f46");

	// Token: 0x040020E3 RID: 8419
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Thanks_01.prefab:3e1457e272054124ca4a2cb85dad89ff");

	// Token: 0x040020E4 RID: 8420
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Threaten_01.prefab:10d05366788d54a43a674d80e1edcb8e");

	// Token: 0x040020E5 RID: 8421
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Well_Played_01.prefab:fda94620074f94441834d158801217d8");

	// Token: 0x040020E6 RID: 8422
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Emote_Respond_Wow_01.prefab:6ef33f7420a5de64090e2d303730d6db");

	// Token: 0x040020E7 RID: 8423
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Kill_Shrine_Generic_01.prefab:f4dbe7aab706692498ab873e8882a460");

	// Token: 0x040020E8 RID: 8424
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Kill_Shrine_Shaman_01.prefab:922a0ade13b61c847a4d5d3c1136eda5");

	// Token: 0x040020E9 RID: 8425
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_Killed_01.prefab:198f62dc268ac6941b005a9cacb82508");

	// Token: 0x040020EA RID: 8426
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_Killed_02.prefab:759e5249d671ece43a543a2eef6daad1");

	// Token: 0x040020EB RID: 8427
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_Killed_03.prefab:ac9f78056deea734496a44a18e3804a0");

	// Token: 0x040020EC RID: 8428
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Harbinger_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Harbinger_01.prefab:bf83f97a09381e1428294b3027d3ea7a");

	// Token: 0x040020ED RID: 8429
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Hatchet_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Hatchet_02.prefab:247c8868743663d41ad2c6ff3a75d61e");

	// Token: 0x040020EE RID: 8430
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Lynx_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Lynx_01.prefab:9a3ced7509c29bb4eb8e7adbee37a0fc");

	// Token: 0x040020EF RID: 8431
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Panther_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Panther_01.prefab:186cc9daf641ff145ad4b5aa0dc8cb53");

	// Token: 0x040020F0 RID: 8432
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_RandomSpell_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_RandomSpell_01.prefab:5926f66071a55a748afab3b1949bfd78");

	// Token: 0x040020F1 RID: 8433
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Event_Reducecost_01 = new AssetReference("VO_TRLA_204h_Male_Troll_Event_Reducecost_01.prefab:4642293430bc30e4cbabfb995e800fa6");

	// Token: 0x040020F2 RID: 8434
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_BigDamage_02.prefab:5a397fcc9b97d3f41a9f5f64b9ba98b5");

	// Token: 0x040020F3 RID: 8435
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_BigDamage_03.prefab:f9973fb2d7005754098083cf8383eeb8");

	// Token: 0x040020F4 RID: 8436
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_BigDamage_04.prefab:cf30b9449d5e2854bb2ea1a8685069a2");

	// Token: 0x040020F5 RID: 8437
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_DrawCards_02.prefab:350fbe7120c47dd489e7012b4da2c184");

	// Token: 0x040020F6 RID: 8438
	private static readonly AssetReference VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03 = new AssetReference("VO_TRLA_204h_Male_Troll_Shrine_DrawCards_03.prefab:b543906e2e89a3144b918d16ed0dbe22");

	// Token: 0x040020F7 RID: 8439
	private HashSet<string> m_playedLines = new HashSet<string>();
}
