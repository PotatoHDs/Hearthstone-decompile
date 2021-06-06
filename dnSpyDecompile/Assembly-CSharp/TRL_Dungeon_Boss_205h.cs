using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000428 RID: 1064
public class TRL_Dungeon_Boss_205h : TRL_Dungeon
{
	// Token: 0x06003A12 RID: 14866 RVA: 0x00129298 File Offset: 0x00127498
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Death_Long_02,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineComesBack_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineComesBack_02,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_Gonk_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_Hakkar_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A13 RID: 14867 RVA: 0x001294E0 File Offset: 0x001276E0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Death_Long_02;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01
		};
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01
		};
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01
		};
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01
		};
		TRL_Dungeon.s_mageShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01
		};
		TRL_Dungeon.s_priestShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01
		};
	}

	// Token: 0x06003A14 RID: 14868 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003A15 RID: 14869 RVA: 0x00129641 File Offset: 0x00127841
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineComesBack_01, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineComesBack_02, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01, 2.5f);
			break;
		case 1004:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_WildLines);
			break;
		case 1005:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BondLines);
			break;
		case 1006:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_ArmorLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A16 RID: 14870 RVA: 0x00129657 File Offset: 0x00127857
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
		if (cardId == "UNG_926")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003A17 RID: 14871 RVA: 0x0012966D File Offset: 0x0012786D
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
		if (!(cardId == "TRL_241"))
		{
			if (!(cardId == "TRL_223"))
			{
				if (!(cardId == "TRL_254"))
				{
					if (cardId == "TRL_541")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_Hakkar_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_Play_Gonk_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040020F8 RID: 8440
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Death_Long_02 = new AssetReference("VO_TRLA_205h_Female_Troll_Death_Long_02.prefab:1e3d6128dc8d05e4f8804b051fdf4159");

	// Token: 0x040020F9 RID: 8441
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Greetings_01.prefab:7e5e71e8c00c6cc4f9ed0fb975d94019");

	// Token: 0x040020FA RID: 8442
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Oops_01.prefab:43f03d5b2700bae49bfd062a5f77133c");

	// Token: 0x040020FB RID: 8443
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Sorry_01.prefab:6ed062c3df246f6448d4f297d4bc1052");

	// Token: 0x040020FC RID: 8444
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Thanks_01.prefab:3b70d01781b015c4ea71b880cf3e0188");

	// Token: 0x040020FD RID: 8445
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Threaten_01.prefab:553d27a7d2db38e48958f023ed167b8f");

	// Token: 0x040020FE RID: 8446
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Well_Played_01.prefab:c8e87b6f20b54124e939a03e5bba39c1");

	// Token: 0x040020FF RID: 8447
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Emote_Respond_Wow_01.prefab:48ebae750b56c4641a6bbf7702b85ab8");

	// Token: 0x04002100 RID: 8448
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Generic_01.prefab:671effc28625f404084248879dc5ac40");

	// Token: 0x04002101 RID: 8449
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Mage_01.prefab:9e3d3f2dbed2c3841b938e87bce6acbc");

	// Token: 0x04002102 RID: 8450
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Paladin_01.prefab:11eecb1ee68145543b0ad2b3db23f233");

	// Token: 0x04002103 RID: 8451
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Priest_01.prefab:89911ca0d25ca5d44a9f541a1b76a5e8");

	// Token: 0x04002104 RID: 8452
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Rogue_01.prefab:a715d11664536d041b6d0babdc29a3bf");

	// Token: 0x04002105 RID: 8453
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Kill_Shrine_Shaman_01.prefab:349e60e93eb13ea4785fcd9c8b468193");

	// Token: 0x04002106 RID: 8454
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Shrine_Killed_01.prefab:42217f5ff6cce874d999458c3a5271c2");

	// Token: 0x04002107 RID: 8455
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_205h_Female_Troll_Shrine_Killed_02.prefab:ef91a55cf28f6894aab7d5b42e3f9d45");

	// Token: 0x04002108 RID: 8456
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_205h_Female_Troll_Shrine_Killed_03.prefab:fa2d345416b80e84ea3b26d4248e0292");

	// Token: 0x04002109 RID: 8457
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_CorneredSentry_01.prefab:973d98684ef295a4fb82a0f3220d9c8c");

	// Token: 0x0400210A RID: 8458
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_Gonk_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_Gonk_01.prefab:a8259579db0e8f644925030253b40a9a");

	// Token: 0x0400210B RID: 8459
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_Hakkar_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_Hakkar_01.prefab:9e96e91a3b25b4741bd3d7f48b8e3f0f");

	// Token: 0x0400210C RID: 8460
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_MarkOfTheLoa_01.prefab:3848c58c94822f54e8d09f06ceedb8b3");

	// Token: 0x0400210D RID: 8461
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01 = new AssetReference("VO_TRLA_205h_Female_Troll_Play_SpiritOfTheRaptor_01.prefab:f28666e4c74c27c4f84709e233ed52db");

	// Token: 0x0400210E RID: 8462
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineComesBack_01 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineComesBack_01.prefab:34f65f3e6faa29544b3b4a345328af1f");

	// Token: 0x0400210F RID: 8463
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineComesBack_02 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineComesBack_02.prefab:bd173f0ef30b29a4d8135a3c4d76a20b");

	// Token: 0x04002110 RID: 8464
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01.prefab:66cb0553e4afc754392c4a4a3ee74c88");

	// Token: 0x04002111 RID: 8465
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02.prefab:053146bd504cf8349bb0f5aea18c10a3");

	// Token: 0x04002112 RID: 8466
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03.prefab:7682b5f4647bf6d46809e9696344ce42");

	// Token: 0x04002113 RID: 8467
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04.prefab:8ee9bf8fc1711cd49b6e9f1cbcbb4d93");

	// Token: 0x04002114 RID: 8468
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_01.prefab:8b3b2a9face2d7e44a098be4be56296b");

	// Token: 0x04002115 RID: 8469
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02.prefab:38a9c3168b7eee9408ec24a1e2280c92");

	// Token: 0x04002116 RID: 8470
	private static readonly AssetReference VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03 = new AssetReference("VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03.prefab:80310a7386e0642418616804ad44038c");

	// Token: 0x04002117 RID: 8471
	private List<string> m_WildLines = new List<string>
	{
		TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_MinionBuff_04
	};

	// Token: 0x04002118 RID: 8472
	private List<string> m_BondLines = new List<string>
	{
		TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_02,
		TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_PlusAttack_03
	};

	// Token: 0x04002119 RID: 8473
	private List<string> m_ArmorLines = new List<string>
	{
		TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_01,
		TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_02,
		TRL_Dungeon_Boss_205h.VO_TRLA_205h_Female_Troll_ShrineEvent_Mana_03
	};

	// Token: 0x0400211A RID: 8474
	private HashSet<string> m_playedLines = new HashSet<string>();
}
