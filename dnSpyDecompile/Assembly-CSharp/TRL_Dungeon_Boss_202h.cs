using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class TRL_Dungeon_Boss_202h : TRL_Dungeon
{
	// Token: 0x060039FA RID: 14842 RVA: 0x00127C80 File Offset: 0x00125E80
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Death_Long_04,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Return_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Return_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Return_04,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Stealth_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Stealth_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Stealth_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Mana_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Mana_04,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_04,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Spell_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Spell_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Spell_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Gral_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Spirit_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Pirate_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Pirate_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Pirate_04,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_CannonBarrage_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_CannonBarrage_03,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_CannonBarrage_04,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_CannonBarrage_06,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_RaidingParty_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Patches_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039FB RID: 14843 RVA: 0x00127F98 File Offset: 0x00126198
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Death_Long_04;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02,
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03
		};
		TRL_Dungeon.s_mageShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01
		};
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x001280B1 File Offset: 0x001262B1
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Return_02, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Return_03, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Return_04, 2.5f);
			break;
		case 1004:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_StealthShrineDeathLines);
			break;
		case 1005:
			if (UnityEngine.Random.Range(0f, 1f) < this.chanceToPlayVO)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_StealLines);
			}
			break;
		case 1006:
			if (UnityEngine.Random.Range(0f, 1f) < this.chanceToPlayVO)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_SpellLines);
			}
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x001280C7 File Offset: 0x001262C7
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
		if (num <= 1081964899U)
		{
			if (num != 269472642U)
			{
				if (num != 664109169U)
				{
					if (num == 1081964899U)
					{
						if (cardId == "TRL_124")
						{
							yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_RaidingParty_01, 2.5f);
							yield break;
						}
					}
				}
				else if (cardId == "TRL_409")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Gral_02, 2.5f);
					yield break;
				}
			}
			else if (cardId == "AT_032")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01, 2.5f);
				yield break;
			}
		}
		else if (num <= 2997423496U)
		{
			if (num != 1097609685U)
			{
				if (num == 2997423496U)
				{
					if (cardId == "CFM_637")
					{
						yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Patches_01, 2.5f);
						yield break;
					}
				}
			}
			else if (cardId == "TRL_157")
			{
				yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01, 2.5f);
				yield break;
			}
		}
		else if (num != 3522655625U)
		{
			if (num == 3654669639U)
			{
				if (cardId == "GVG_025")
				{
					yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01, 2.5f);
					yield break;
				}
			}
		}
		else if (cardId == "TRL_092")
		{
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Spirit_01, 2.5f);
			yield break;
		}
		if (entity.HasRace(TAG_RACE.PIRATE) && UnityEngine.Random.Range(0f, 1f) < 0.5f)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PirateLines);
		}
		yield break;
	}

	// Token: 0x04002079 RID: 8313
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Death_Long_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Death_Long_04.prefab:6c810d6eefcc25643abe5b7c10392e80");

	// Token: 0x0400207A RID: 8314
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Greetings_01.prefab:3fca7d65892112648a2786cf2d80d7b4");

	// Token: 0x0400207B RID: 8315
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Oops_01.prefab:1b9f72498ad26e043a8f0999464ba244");

	// Token: 0x0400207C RID: 8316
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Sorry_01.prefab:579290639e90a3e42b1d6ceda51a3e84");

	// Token: 0x0400207D RID: 8317
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Thanks_01.prefab:ebbe2b7b2aa9c6943b1c679c9c430bfc");

	// Token: 0x0400207E RID: 8318
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Threaten_01.prefab:9478297c91161c543ad0b8db6d028f41");

	// Token: 0x0400207F RID: 8319
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Well_Played_01.prefab:d18e4368aba99dc4faa9f0b6f22d00ec");

	// Token: 0x04002080 RID: 8320
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Emote_Respond_Wow_01.prefab:4a9177d622dc90a44bd03403061545e3");

	// Token: 0x04002081 RID: 8321
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_01.prefab:a269888d3fc5a05489bc6e83ec50e3e4");

	// Token: 0x04002082 RID: 8322
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Killed_01.prefab:8c410236da214e649b505938e56138b0");

	// Token: 0x04002083 RID: 8323
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_02.prefab:e913e5f4071d23046960c773b5089879");

	// Token: 0x04002084 RID: 8324
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Generic_03.prefab:33417ca00c7c8534889e55a23f8f4373");

	// Token: 0x04002085 RID: 8325
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Kill_Shrine_Mage_01.prefab:92e4b6e930aeb884c8e6e74b6592cea0");

	// Token: 0x04002086 RID: 8326
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Killed_02.prefab:47103f73babb8914699834c02e28b8b0");

	// Token: 0x04002087 RID: 8327
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Killed_03.prefab:a294ee6ef84248c49bcc7810b9dea5ef");

	// Token: 0x04002088 RID: 8328
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_01.prefab:f5f4b0f3349574b45b1af2b72d80420b");

	// Token: 0x04002089 RID: 8329
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_02.prefab:1c840dd62251b2341a81e82f003ca70f");

	// Token: 0x0400208A RID: 8330
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_03.prefab:bd6b92e07cb06744e9d2183a27e9f98c");

	// Token: 0x0400208B RID: 8331
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Burgle_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Burgle_04.prefab:5c74df4036a1547409d1eeff65225caf");

	// Token: 0x0400208C RID: 8332
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Spell_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Spell_01.prefab:914f1ff6cde36b34e9744584ad6f7a8e");

	// Token: 0x0400208D RID: 8333
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Spell_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Spell_02.prefab:16bd2fee248cc90469f4de43bff1a0cd");

	// Token: 0x0400208E RID: 8334
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Spell_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Spell_03.prefab:7635ec176e350fc4896134038353040e");

	// Token: 0x0400208F RID: 8335
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Gral_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Play _Gral_02.prefab:758863504113a2c48b4ca790bf508e8a");

	// Token: 0x04002090 RID: 8336
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_02.prefab:1ef85ead71e9726409e1d6fb90ba4b36");

	// Token: 0x04002091 RID: 8337
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_03.prefab:1e63b9743fd9e2347a4c67ddf699b67a");

	// Token: 0x04002092 RID: 8338
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_04.prefab:41b322266f1df6540902613f8e2a4cc3");

	// Token: 0x04002093 RID: 8339
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_CannonBarrage_06 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_CannonBarrage_06.prefab:f41338d68c6cac8479308fb842384845");

	// Token: 0x04002094 RID: 8340
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_OneEyedCheat_01.prefab:bd5675d93357f7a498b24d5c5e5dcb3e");

	// Token: 0x04002095 RID: 8341
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Patches_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Patches_01.prefab:b62d2f9e4cd3c04408f4291879d90aa5");

	// Token: 0x04002096 RID: 8342
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Pirate_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Pirate_01.prefab:d1547cdbbcaba37459935001cf32553b");

	// Token: 0x04002097 RID: 8343
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Pirate_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Pirate_03.prefab:b949411bbd0c5f643b2f5b25fe61b919");

	// Token: 0x04002098 RID: 8344
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Pirate_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Pirate_04.prefab:9ede01f601c9379458b8feb4dcf84989");

	// Token: 0x04002099 RID: 8345
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_RaidingParty_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_RaidingParty_01.prefab:c11541abb0ff4cb469e434894abd83ca");

	// Token: 0x0400209A RID: 8346
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_ShadyDealer_01.prefab:281efc0ac2b8ece4793771ec0b8f9528");

	// Token: 0x0400209B RID: 8347
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_Spirit_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_Spirit_01.prefab:dc4403f41f790954280488b5f0be61d3");

	// Token: 0x0400209C RID: 8348
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Play_WalkThePlank_01.prefab:3e803912a9d1a4e4a9c83d5889b7415a");

	// Token: 0x0400209D RID: 8349
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Mana_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Mana_02.prefab:99ae18eb40bac4a4e827c5d26902fd9b");

	// Token: 0x0400209E RID: 8350
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Mana_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Mana_04.prefab:9a3d43cde04993045be2cc177170ce9f");

	// Token: 0x0400209F RID: 8351
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Return_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Return_02.prefab:a0b599b5392ceb0488281692a99a23c5");

	// Token: 0x040020A0 RID: 8352
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Return_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Return_03.prefab:3ccee2a5dc2e13943b58be2f6113f860");

	// Token: 0x040020A1 RID: 8353
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Return_04 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Return_04.prefab:c88118d6c6fec7c4c8281714c2530d09");

	// Token: 0x040020A2 RID: 8354
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Stealth_01 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Stealth_01.prefab:693c580ac4b1dd647802a086a0335122");

	// Token: 0x040020A3 RID: 8355
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Stealth_02 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Stealth_02.prefab:9c56a25f2eda0d949b987bc24f9e3f6a");

	// Token: 0x040020A4 RID: 8356
	private static readonly AssetReference VO_TRLA_202h_Female_Troll_Shrine_Stealth_03 = new AssetReference("VO_TRLA_202h_Female_Troll_Shrine_Stealth_03.prefab:957a66ff65b19394191925372c24429f");

	// Token: 0x040020A5 RID: 8357
	private List<string> m_StealthShrineDeathLines = new List<string>
	{
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Stealth_01,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Stealth_02,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Stealth_03
	};

	// Token: 0x040020A6 RID: 8358
	private List<string> m_StealLines = new List<string>
	{
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Mana_02,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Mana_04,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_01,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_02,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_03,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Burgle_04
	};

	// Token: 0x040020A7 RID: 8359
	private List<string> m_SpellLines = new List<string>
	{
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Spell_01,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Spell_02,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Shrine_Spell_03
	};

	// Token: 0x040020A8 RID: 8360
	private List<string> m_PirateLines = new List<string>
	{
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Pirate_01,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Pirate_03,
		TRL_Dungeon_Boss_202h.VO_TRLA_202h_Female_Troll_Play_Pirate_04
	};

	// Token: 0x040020A9 RID: 8361
	private float chanceToPlayVO = 0.2f;

	// Token: 0x040020AA RID: 8362
	private HashSet<string> m_playedLines = new HashSet<string>();
}
