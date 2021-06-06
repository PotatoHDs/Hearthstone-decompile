using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000426 RID: 1062
public class TRL_Dungeon_Boss_203h : TRL_Dungeon
{
	// Token: 0x06003A02 RID: 14850 RVA: 0x001284CC File Offset: 0x001266CC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Death_Long_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Healing_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Return_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Return_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Return_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Buff_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Buff_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Buff_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_03,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_04,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Shirvallah_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Rush_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Rush_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Spirit_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_TimeOut_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_TimeOut_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A03 RID: 14851 RVA: 0x001287F4 File Offset: 0x001269F4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Death_Long_03;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Healing_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03
		};
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01
		};
		TRL_Dungeon.s_shamanShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01
		};
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02
		};
		TRL_Dungeon.s_hunterShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01
		};
		TRL_Dungeon.s_druidShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01
		};
		TRL_Dungeon.s_warlockShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01
		};
		TRL_Dungeon.s_mageShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01
		};
		TRL_Dungeon.s_priestShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01
		};
	}

	// Token: 0x06003A04 RID: 14852 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003A05 RID: 14853 RVA: 0x001289C3 File Offset: 0x00126BC3
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Return_01, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Return_02, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Return_03, 2.5f);
			break;
		case 1004:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_ShieldLines);
			break;
		case 1005:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BuffLines);
			break;
		case 1006:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_DamageLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A06 RID: 14854 RVA: 0x001289D9 File Offset: 0x00126BD9
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
		if (cardId == "TRL_309")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Spirit_01, 2.5f);
			yield break;
		}
		if (cardId == "TRL_300")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Shirvallah_01, 2.5f);
			yield break;
		}
		if (cardId == "TRL_307")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01, 2.5f);
			yield break;
		}
		if (cardId == "TRL_302")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_TimeOutLines);
			yield break;
		}
		if (!(cardId == "TRL_305"))
		{
			if (entity.HasTag(GAME_TAG.RUSH))
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_RushLines);
			}
			yield break;
		}
		yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_ChallengerLines);
		yield break;
	}

	// Token: 0x040020AB RID: 8363
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Death_Long_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Death_Long_03.prefab:7aa8f69c09ec99645a71f59cc8571311");

	// Token: 0x040020AC RID: 8364
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Greetings_01.prefab:3eaa9cc85ccb2904bb6aeff695287d61");

	// Token: 0x040020AD RID: 8365
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Oops_01.prefab:2c8e3c29f48c78b4293a6c36eb2a4148");

	// Token: 0x040020AE RID: 8366
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Thanks_01.prefab:95cabd67b5fc86e49a5fab1e356d04e8");

	// Token: 0x040020AF RID: 8367
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Threaten_01.prefab:36b35d2e8b7f5a34fbb8c5ba736df4c7");

	// Token: 0x040020B0 RID: 8368
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Well_Played_01.prefab:9397bf55c1b8ed74f823001ef683d97c");

	// Token: 0x040020B1 RID: 8369
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Emote_Respond_Wow_01.prefab:bc676941846eaf64c8d4f0bb625ed924");

	// Token: 0x040020B2 RID: 8370
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Healing_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Healing_01.prefab:8c4451a82771c934f9ada26f59d3e486");

	// Token: 0x040020B3 RID: 8371
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Druid_01.prefab:af5453375dbd89a459988a789f10388a");

	// Token: 0x040020B4 RID: 8372
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_01.prefab:d833eebea8085824aa817a4324336131");

	// Token: 0x040020B5 RID: 8373
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_02.prefab:484a62e1592b5a1448df8c1788164932");

	// Token: 0x040020B6 RID: 8374
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Generic_03.prefab:e6981f4dafa7aab40b0db66c57ef7d21");

	// Token: 0x040020B7 RID: 8375
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Hunter_01.prefab:4652414f10c997341a46b03d7fcf05e1");

	// Token: 0x040020B8 RID: 8376
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Mage_01.prefab:31ac6326a7f12b54ab78698d982f40b4");

	// Token: 0x040020B9 RID: 8377
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Priest_01.prefab:62325a5be04d0504f98e92fea07bb7c2");

	// Token: 0x040020BA RID: 8378
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Rogue_02.prefab:80b2a6d55da8d23479f75454b4d7e14c");

	// Token: 0x040020BB RID: 8379
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Shaman_01.prefab:2896eb702707c6646a8f12a62650ac7d");

	// Token: 0x040020BC RID: 8380
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Warlock_01.prefab:33fa48ff7b162064dae41cbdd8f4ab7d");

	// Token: 0x040020BD RID: 8381
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Kill_Shrine_Warrior_01.prefab:aba4fbd2fdd50524cb778af23f781f62");

	// Token: 0x040020BE RID: 8382
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Killed_01.prefab:af4d62cba97102442afd786ad15dd1c5");

	// Token: 0x040020BF RID: 8383
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Killed_02.prefab:4a6bf4df648be43469f5170244e4e807");

	// Token: 0x040020C0 RID: 8384
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Killed_03.prefab:d36975d6744af9045ac6ff9199a6a888");

	// Token: 0x040020C1 RID: 8385
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01.prefab:e64e4aac87bb183439df9b329d29f70a");

	// Token: 0x040020C2 RID: 8386
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02.prefab:48e3d425862fc7e42bcecbcf66a3a1bc");

	// Token: 0x040020C3 RID: 8387
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03.prefab:bd5b5a590c9bf7d4f82ccd6b1eb95608");

	// Token: 0x040020C4 RID: 8388
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_FlashOfLight_01.prefab:73fb57eb54477f743a53b0d46b28eca0");

	// Token: 0x040020C5 RID: 8389
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Rush_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Rush_01.prefab:0d1831e4d4e582341b82f192be50da38");

	// Token: 0x040020C6 RID: 8390
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Rush_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Rush_02.prefab:7339e8681806ee142ba724b94b843b0a");

	// Token: 0x040020C7 RID: 8391
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Shirvallah_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Shirvallah_01.prefab:2398594799ae6bd4ab2ca1cd5063de74");

	// Token: 0x040020C8 RID: 8392
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_Spirit_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_Spirit_01.prefab:b99d2bb4675f1124289835713878e88a");

	// Token: 0x040020C9 RID: 8393
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_TimeOut_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_TimeOut_01.prefab:b40a0f5826a7ac642a132eedf8d49375");

	// Token: 0x040020CA RID: 8394
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Play_TimeOut_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Play_TimeOut_02.prefab:8f8abb1e352d6594f86e0a5b657eb532");

	// Token: 0x040020CB RID: 8395
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Buff_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Buff_01.prefab:9ea8cd21c1c933b46b4739a600bdcbf1");

	// Token: 0x040020CC RID: 8396
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Buff_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Buff_02.prefab:19c243f13420336438df4318a9f7a944");

	// Token: 0x040020CD RID: 8397
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Buff_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Buff_03.prefab:ce951ea2bf2c75c4bbfad4e1553d2089");

	// Token: 0x040020CE RID: 8398
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_01.prefab:2988c6e3b011969449b5aa2ad69e9640");

	// Token: 0x040020CF RID: 8399
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_02.prefab:d50afb5d0e5dd5f4b9658ffc194cab32");

	// Token: 0x040020D0 RID: 8400
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_03.prefab:85095f3a35868ad448217ead72cec4cb");

	// Token: 0x040020D1 RID: 8401
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Damage_04 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Damage_04.prefab:8c858928a5db4a94b9ff6751386bb6ee");

	// Token: 0x040020D2 RID: 8402
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01.prefab:2cf8111ba9b68294f96c8da0cb054039");

	// Token: 0x040020D3 RID: 8403
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02.prefab:cbb2562f68a1e8b4cbdc3ba674b2dcf3");

	// Token: 0x040020D4 RID: 8404
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03.prefab:fa9002a0287d93d4f8c02c1fd74f55de");

	// Token: 0x040020D5 RID: 8405
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Return_01 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Return_01.prefab:13d861b07c771434d91f676f69c23e20");

	// Token: 0x040020D6 RID: 8406
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Return_02 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Return_02.prefab:c83ba3e021a14ec4abaa0be9be378a42");

	// Token: 0x040020D7 RID: 8407
	private static readonly AssetReference VO_TRLA_203h_Male_Troll_Shrine_Return_03 = new AssetReference("VO_TRLA_203h_Male_Troll_Shrine_Return_03.prefab:8a1c17a87a9294246a0533eeef3430fd");

	// Token: 0x040020D8 RID: 8408
	private List<string> m_ShieldLines = new List<string>
	{
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_DivineShield_01,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_DivineShield_02,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_DivineShield_03
	};

	// Token: 0x040020D9 RID: 8409
	private List<string> m_BuffLines = new List<string>
	{
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Buff_01,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Buff_02,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Buff_03
	};

	// Token: 0x040020DA RID: 8410
	private List<string> m_DamageLines = new List<string>
	{
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_01,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_02,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_03,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Shrine_Damage_04
	};

	// Token: 0x040020DB RID: 8411
	private List<string> m_RushLines = new List<string>
	{
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Rush_01,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_Rush_02
	};

	// Token: 0x040020DC RID: 8412
	private List<string> m_TimeOutLines = new List<string>
	{
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_TimeOut_01,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_TimeOut_02
	};

	// Token: 0x040020DD RID: 8413
	private List<string> m_ChallengerLines = new List<string>
	{
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_ANewChallenger_01,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_ANewChallenger_02,
		TRL_Dungeon_Boss_203h.VO_TRLA_203h_Male_Troll_Play_ANewChallenger_03
	};

	// Token: 0x040020DE RID: 8414
	private HashSet<string> m_playedLines = new HashSet<string>();
}
