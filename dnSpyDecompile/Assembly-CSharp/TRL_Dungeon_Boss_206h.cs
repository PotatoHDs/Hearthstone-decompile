using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000429 RID: 1065
public class TRL_Dungeon_Boss_206h : TRL_Dungeon
{
	// Token: 0x06003A1B RID: 14875 RVA: 0x00129904 File Offset: 0x00127B04
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Death_Long_Alt_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_General_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_HakkarReponse_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Play_Lanathel_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Play_Hireek_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A1C RID: 14876 RVA: 0x00129B0C File Offset: 0x00127D0C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Death_Long_Alt_01;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01
		};
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01
		};
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01
		};
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01
		};
		TRL_Dungeon.s_hunterShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01
		};
		TRL_Dungeon.s_priestShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01
		};
	}

	// Token: 0x06003A1D RID: 14877 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003A1E RID: 14878 RVA: 0x00129C6D File Offset: 0x00127E6D
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_General_01, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03, 2.5f);
			break;
		case 1004:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BounceLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x00129C83 File Offset: 0x00127E83
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
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
		if (cardId == "TRL_541")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_HakkarReponse_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x00129C99 File Offset: 0x00127E99
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
		if (!(cardId == "TRLA_181"))
		{
			if (!(cardId == "ICC_841"))
			{
				if (!(cardId == "TRL_253"))
				{
					if (cardId == "TRL_251")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Play_Hireek_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Play_Lanathel_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400211B RID: 8475
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Death_Long_Alt_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Death_Long_Alt_01.prefab:3ea25ade7d1d4fc4e96bd6445db6c609");

	// Token: 0x0400211C RID: 8476
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Greetings_01.prefab:39763531d0c53f84ea24dbf11e2be9e0");

	// Token: 0x0400211D RID: 8477
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Oops_01.prefab:b45a7f2c1a8531a48b9765ac4a07d4d8");

	// Token: 0x0400211E RID: 8478
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Sorry_01.prefab:c7f8bf85f654048469dcc463abeaf406");

	// Token: 0x0400211F RID: 8479
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Thanks_01.prefab:6c4b8943a9d967b4f845ffc6f8199432");

	// Token: 0x04002120 RID: 8480
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Threaten_01.prefab:81b28005ca7086d428d5a54516b546d8");

	// Token: 0x04002121 RID: 8481
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Well_Played_01.prefab:b879d8b9421a58f43802d90d2c4fe533");

	// Token: 0x04002122 RID: 8482
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Emote_Respond_Wow_01.prefab:6f30d3f1de17e57499c3e72a2a483924");

	// Token: 0x04002123 RID: 8483
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Generic_01.prefab:b30c13862fe16d649af8bc8c7b6e0e1a");

	// Token: 0x04002124 RID: 8484
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Hunter_01.prefab:99d2ba3259ce13243a0a19f35be707de");

	// Token: 0x04002125 RID: 8485
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Priest_01.prefab:3864bab2de40ebb4f8ff44ceab7cd8b1");

	// Token: 0x04002126 RID: 8486
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Rogue_01.prefab:8f2d2b0548ea46a4ea544e7d19b50d1d");

	// Token: 0x04002127 RID: 8487
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Shaman_01.prefab:b7952122697a7334b872ab7a5721a609");

	// Token: 0x04002128 RID: 8488
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Kill_Shrine_Warrior_01.prefab:30ce14582e70fea41864e71d37a6c827");

	// Token: 0x04002129 RID: 8489
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_Killed_01.prefab:4310c78a6dc412a47b5f9d472b2f4814");

	// Token: 0x0400212A RID: 8490
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_Killed_02.prefab:e2e5232ba8a781b4480c42aab7a25471");

	// Token: 0x0400212B RID: 8491
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_Killed_03.prefab:9c4d525738864ef4094e311ee22e241d");

	// Token: 0x0400212C RID: 8492
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_HakkarReponse_01 = new AssetReference("VO_TRLA_206h_Female_Troll_HakkarReponse_01.prefab:2ba29514d41edd74da23357cba8141e7");

	// Token: 0x0400212D RID: 8493
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Play_Hireek_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Play_Hireek_01.prefab:d966e4cf87d0f0e42aa57e8ac3d02b66");

	// Token: 0x0400212E RID: 8494
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Play_Lanathel_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Play_Lanathel_01.prefab:3ba3fcd1cf47caf44bb28c1d0831f634");

	// Token: 0x0400212F RID: 8495
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Play_SpiritofBat_01.prefab:1843cafd27cacf24eb44f8910a6a3b01");

	// Token: 0x04002130 RID: 8496
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01.prefab:85696055c9ddcb247bc55441fea4ab19");

	// Token: 0x04002131 RID: 8497
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02.prefab:bf3e331837a7eb0448633c466a5e7019");

	// Token: 0x04002132 RID: 8498
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_03.prefab:6b340e10524f2e041864ef980ff60d51");

	// Token: 0x04002133 RID: 8499
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_01.prefab:fe9b1c7da40891d4ab42e8617b169d64");

	// Token: 0x04002134 RID: 8500
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_CheapSpell_02.prefab:e06cd0b9f59d75b4481664a262e2bc09");

	// Token: 0x04002135 RID: 8501
	private static readonly AssetReference VO_TRLA_206h_Female_Troll_Shrine_General_01 = new AssetReference("VO_TRLA_206h_Female_Troll_Shrine_General_01.prefab:2411c9e50130d704a9e7f0b68e4a04c4");

	// Token: 0x04002136 RID: 8502
	private List<string> m_BounceLines = new List<string>
	{
		TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_01,
		TRL_Dungeon_Boss_206h.VO_TRLA_206h_Female_Troll_Shrine_BounceDamage_02
	};

	// Token: 0x04002137 RID: 8503
	private HashSet<string> m_playedLines = new HashSet<string>();
}
