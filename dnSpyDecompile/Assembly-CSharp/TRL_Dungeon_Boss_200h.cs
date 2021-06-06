using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000423 RID: 1059
public class TRL_Dungeon_Boss_200h : TRL_Dungeon
{
	// Token: 0x060039E9 RID: 14825 RVA: 0x00126E54 File Offset: 0x00125054
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Death_Long_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineComesBack_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineComesBack_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineComesBack_03,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineDoesThing_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Akali_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Hakkar_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Janalai_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Ragewing_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Armor_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Armor_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Armor_03,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Dragon_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Dragon_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Dragon_03,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Weapon_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Weapon_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Weapon_03
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x001270BC File Offset: 0x001252BC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Death_Long_02;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03,
			TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04
		};
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x001271CB File Offset: 0x001253CB
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineComesBack_01, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineComesBack_02, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineComesBack_03, 2.5f);
			break;
		case 1004:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_ShrineDoesThing_01, 2.5f);
			break;
		case 1005:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_ArmorLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060039ED RID: 14829 RVA: 0x001271E1 File Offset: 0x001253E1
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
		if (cardId == "TRL_316")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Janalai_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060039EE RID: 14830 RVA: 0x001271F7 File Offset: 0x001253F7
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "TRL_329"))
		{
			if (!(cardId == "TRL_327"))
			{
				if (!(cardId == "TRL_541"))
				{
					if (cardId == "TRL_548")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Ragewing_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Hakkar_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_Play_Akali_01, 2.5f);
		}
		if (entity.HasRace(TAG_RACE.DRAGON))
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_DragonLines);
		}
		if (entity.GetCardType() == TAG_CARDTYPE.WEAPON)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_WeaponLines);
		}
		yield break;
	}

	// Token: 0x0400202A RID: 8234
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Death_Long_02 = new AssetReference("VO_TRLA_200h_Male_Troll_Death_Long_02.prefab:617d2f10a0b784f41996ca4470a3b521");

	// Token: 0x0400202B RID: 8235
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Greetings_01.prefab:a78b045849c256541bd980732271ea96");

	// Token: 0x0400202C RID: 8236
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Oops_01.prefab:010e8c52c50b01e4e963038b87ce5a06");

	// Token: 0x0400202D RID: 8237
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Sorry_01.prefab:754e3f0ca927cd042920033008c98206");

	// Token: 0x0400202E RID: 8238
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Thanks_01.prefab:4bd9201d7208452459dcc8ecaa4dab4d");

	// Token: 0x0400202F RID: 8239
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Threaten_01.prefab:0194f93848bfc1a45a0cd1b5bfc0e4d4");

	// Token: 0x04002030 RID: 8240
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Well_Played_01.prefab:fb6d2284183033b4a8ecbabc20ecc488");

	// Token: 0x04002031 RID: 8241
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Emote_Respond_Wow_01.prefab:8ec038795f003814ea0c18144989eb38");

	// Token: 0x04002032 RID: 8242
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_01.prefab:3ef5d34b96e5caf49b4f527acce0b782");

	// Token: 0x04002033 RID: 8243
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_02.prefab:3f858ff6189b4ff4da08f37ed7693e72");

	// Token: 0x04002034 RID: 8244
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_03.prefab:f1467c267e3b63e4d83985c4a55ef7cd");

	// Token: 0x04002035 RID: 8245
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04 = new AssetReference("VO_TRLA_200h_Male_Troll_Kill_Shrine_Generic_04.prefab:463a9bbc835d60c4fb427ed4e738e84c");

	// Token: 0x04002036 RID: 8246
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Shrine_Killed_01.prefab:84df71a8c42ef874e81998ab5396ec1e");

	// Token: 0x04002037 RID: 8247
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_200h_Male_Troll_Shrine_Killed_02.prefab:93adfb21b6f069f43bbd56fbfae92c38");

	// Token: 0x04002038 RID: 8248
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_200h_Male_Troll_Shrine_Killed_03.prefab:c0228fb20edb1bb4db78e11399956514");

	// Token: 0x04002039 RID: 8249
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Armor_01 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Armor_01.prefab:1984bf68e32bb5f44a1419209c8fe516");

	// Token: 0x0400203A RID: 8250
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Armor_02 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Armor_02.prefab:7438d447e34cc464281b7fd3c79142b4");

	// Token: 0x0400203B RID: 8251
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Armor_03 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Armor_03.prefab:31b7276ace6acd747a09a2935ee46f8f");

	// Token: 0x0400203C RID: 8252
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Dragon_01 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Dragon_01.prefab:6503010125cd3974c9facd16bf43d365");

	// Token: 0x0400203D RID: 8253
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Dragon_02 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Dragon_02.prefab:36a0484b6bb4b6b43af3f925025bc2fd");

	// Token: 0x0400203E RID: 8254
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Dragon_03 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Dragon_03.prefab:6e11197568e0e31458099b94a362d8c1");

	// Token: 0x0400203F RID: 8255
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Weapon_01 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Weapon_01.prefab:23ef3880e2e8569459e4583eff1bbdfa");

	// Token: 0x04002040 RID: 8256
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Weapon_02 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Weapon_02.prefab:a36227bbca3702d488e32d305f3b0ac9");

	// Token: 0x04002041 RID: 8257
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_EVENT_Weapon_03 = new AssetReference("VO_TRLA_200h_Male_Troll_EVENT_Weapon_03.prefab:5ac1fc559e791bb4894d196446cd1dc9");

	// Token: 0x04002042 RID: 8258
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Akali_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Akali_01.prefab:cbe43a80594c86043a3cef8fd6b81105");

	// Token: 0x04002043 RID: 8259
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Hakkar_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Hakkar_01.prefab:cf429cb885fae4344a2f52e723d62a2b");

	// Token: 0x04002044 RID: 8260
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Janalai_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Janalai_01.prefab:d6ccfa30ea5bad242b857438d2a397d7");

	// Token: 0x04002045 RID: 8261
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_Ragewing_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_Ragewing_01.prefab:bb4d1f60660d16c4f9cd7c24c4e5f2d2");

	// Token: 0x04002046 RID: 8262
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01 = new AssetReference("VO_TRLA_200h_Male_Troll_Play_SpiritOfTheRhino_01.prefab:c72bcf5a7e7e2cb44b303b4dc9509d1c");

	// Token: 0x04002047 RID: 8263
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineComesBack_01 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineComesBack_01.prefab:69bdad0df848747499b0b3b748a764dd");

	// Token: 0x04002048 RID: 8264
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineComesBack_02 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineComesBack_02.prefab:16e8ecc16fe8e624fb9d9c3004bd4646");

	// Token: 0x04002049 RID: 8265
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineComesBack_03 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineComesBack_03.prefab:27e4ef847fc7c3d4aa98b4819f30951b");

	// Token: 0x0400204A RID: 8266
	private static readonly AssetReference VO_TRLA_200h_Male_Troll_ShrineDoesThing_01 = new AssetReference("VO_TRLA_200h_Male_Troll_ShrineDoesThing_01.prefab:98e761a9605c28f45877c5aca006fd42");

	// Token: 0x0400204B RID: 8267
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x0400204C RID: 8268
	private List<string> m_DragonLines = new List<string>
	{
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Dragon_01,
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Dragon_02,
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Dragon_03
	};

	// Token: 0x0400204D RID: 8269
	private List<string> m_WeaponLines = new List<string>
	{
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Weapon_01,
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Weapon_02,
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Weapon_03
	};

	// Token: 0x0400204E RID: 8270
	private List<string> m_ArmorLines = new List<string>
	{
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Armor_01,
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Armor_02,
		TRL_Dungeon_Boss_200h.VO_TRLA_200h_Male_Troll_EVENT_Armor_03
	};
}
