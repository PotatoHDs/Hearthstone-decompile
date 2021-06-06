using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000424 RID: 1060
public class TRL_Dungeon_Boss_201h : TRL_Dungeon
{
	// Token: 0x060039F2 RID: 14834 RVA: 0x001274E8 File Offset: 0x001256E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Death_Long_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Killed_03,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Returns_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Returns_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Returns_04,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Event_General_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Event_General_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Likkem_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x001277A0 File Offset: 0x001259A0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		TRL_Dungeon.s_deathLine = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Death_Long_01;
		TRL_Dungeon.s_responseLineGreeting = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02;
		TRL_Dungeon.s_responseLineOops = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01;
		TRL_Dungeon.s_responseLineSorry = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01;
		TRL_Dungeon.s_responseLineThanks = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01;
		TRL_Dungeon.s_responseLineThreaten = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01;
		TRL_Dungeon.s_responseLineWellPlayed = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02;
		TRL_Dungeon.s_responseLineWow = TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01;
		TRL_Dungeon.s_bossShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Killed_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Killed_02,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Killed_03
		};
		TRL_Dungeon.s_genericShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03,
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04
		};
		TRL_Dungeon.s_warriorShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01
		};
		TRL_Dungeon.s_rogueShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01
		};
		TRL_Dungeon.s_paladinShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01
		};
		TRL_Dungeon.s_warlockShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01
		};
		TRL_Dungeon.s_mageShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01
		};
		TRL_Dungeon.s_priestShrineDeathLines = new List<string>
		{
			TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02
		};
	}

	// Token: 0x060039F4 RID: 14836 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060039F5 RID: 14837 RVA: 0x0012793B File Offset: 0x00125B3B
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
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Returns_01, 2.5f);
			break;
		case 1002:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Returns_02, 2.5f);
			break;
		case 1003:
			yield return base.PlayLineOnlyOnce(actor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Returns_04, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060039F6 RID: 14838 RVA: 0x00127951 File Offset: 0x00125B51
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
		if (!(cardId == "EX1_246"))
		{
			if (!(cardId == "TRL_345"))
			{
				if (!(cardId == "TRL_352"))
				{
					if (!(cardId == "TRL_351"))
					{
						if (!(cardId == "CS2_053"))
						{
							if (cardId == "TRLA_160")
							{
								yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Likkem_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_HexLines);
		}
		if (entity.HasBattlecry())
		{
			yield return base.PlayLineOnlyOnce(enemyActor, TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02, 2.5f);
		}
		else if (entity.GetCardType() == TAG_CARDTYPE.SPELL && entity.GetCost() >= 4)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_BigSpellLines);
		}
		else if (entity.HasTag(GAME_TAG.OVERLOAD_OWED))
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_OverloadLines);
		}
		yield break;
	}

	// Token: 0x0400204F RID: 8271
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Death_Long_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Death_Long_01.prefab:953b7472d1dabfc4eb7e277757675d99");

	// Token: 0x04002050 RID: 8272
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Greetings_02.prefab:579e0b780368892488f18837971efed8");

	// Token: 0x04002051 RID: 8273
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Oops_01.prefab:e9aa5feeca073f44ca6cd80e6d13f214");

	// Token: 0x04002052 RID: 8274
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Sorry_01.prefab:48cdedc7ab856d343bd7a69fc5ce279d");

	// Token: 0x04002053 RID: 8275
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Thanks_01.prefab:1dbeac2bb2993b141a986882e0dc8388");

	// Token: 0x04002054 RID: 8276
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Threaten_01.prefab:4295bde2f7cc62444a14273e5b859921");

	// Token: 0x04002055 RID: 8277
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Well_Played_02.prefab:59d86b46d36b6f44eb54385b80542ec2");

	// Token: 0x04002056 RID: 8278
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Emote_Respond_Wow_01.prefab:4b8a95da666ac314b99be2bb6aa073b2");

	// Token: 0x04002057 RID: 8279
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_01.prefab:356917207ff8c1044b6d1b3de3be6001");

	// Token: 0x04002058 RID: 8280
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_03.prefab:4d80d6b060ac0144eb9ba46b5822c51f");

	// Token: 0x04002059 RID: 8281
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Generic_04.prefab:4869605d576a7d04d96806673d1d5ebb");

	// Token: 0x0400205A RID: 8282
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Mage_01.prefab:432a0d79736c8204d994e7e54c9342b2");

	// Token: 0x0400205B RID: 8283
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Paladin_01.prefab:0243cb6470c20c44aaedcfbc2885f70b");

	// Token: 0x0400205C RID: 8284
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Priest_02.prefab:e87a7216a675f604c9584a0eb921c816");

	// Token: 0x0400205D RID: 8285
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Rogue_01.prefab:cbe771299daf04c4fa268806ac2cacbe");

	// Token: 0x0400205E RID: 8286
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Warlock_01.prefab:01ce07578f57e2f438057c3b89066ffc");

	// Token: 0x0400205F RID: 8287
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Kill_Shrine_Warrior_01.prefab:03414093c8a7a334c8e9fe34afa12201");

	// Token: 0x04002060 RID: 8288
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Killed_01.prefab:fc94bcc5f46b2a14da0203447b4c070f");

	// Token: 0x04002061 RID: 8289
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Killed_02.prefab:665ec6be71c172b4e93d9c6ea83dd04d");

	// Token: 0x04002062 RID: 8290
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Killed_03.prefab:1d2fd3a1c5a61034492873aebc753791");

	// Token: 0x04002063 RID: 8291
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01.prefab:5de109a3c5788a34798a36642e1ea927");

	// Token: 0x04002064 RID: 8292
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02.prefab:587b8ece22b71e040a1be15d9c0d10c0");

	// Token: 0x04002065 RID: 8293
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03.prefab:bb4c5bcc7d3086c4bbab31b20197993f");

	// Token: 0x04002066 RID: 8294
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01.prefab:daba602c2c774ac478f2e6043d0b72bb");

	// Token: 0x04002067 RID: 8295
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02.prefab:705e521881bf7824cab860cf7d8e46a9");

	// Token: 0x04002068 RID: 8296
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03.prefab:f23acdc8aefe68f499d7a2958c979a10");

	// Token: 0x04002069 RID: 8297
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01.prefab:f96999bf3e8230d4aad03f0968086615");

	// Token: 0x0400206A RID: 8298
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_HauntingVisions_01.prefab:b0d57bccd3ce6d04489657e95320d254");

	// Token: 0x0400206B RID: 8299
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Kragwa_01.prefab:a26c9dcd2f4636943a52ebb3336296f8");

	// Token: 0x0400206C RID: 8300
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_Likkem_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_Likkem_01.prefab:d76d49675d024e049b0ffb21268f7ff6");

	// Token: 0x0400206D RID: 8301
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_NagaSummon_01.prefab:7fac1120660f57748aaa31228052dd9d");

	// Token: 0x0400206E RID: 8302
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01 = new AssetReference("VO_TRLA_201h_Male_Troll_EVENT_RainOfToads_01.prefab:f724160d015de814c829315ac16c7303");

	// Token: 0x0400206F RID: 8303
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Event_Battlecry_02.prefab:152ed522cc0102d46bfd3033263c6ff6");

	// Token: 0x04002070 RID: 8304
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Event_General_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Event_General_01.prefab:b26e94e06048fd448bdbad4acf556291");

	// Token: 0x04002071 RID: 8305
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Event_General_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Event_General_02.prefab:3d25a4c2f4d148c48b86bae2910fd07e");

	// Token: 0x04002072 RID: 8306
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Returns_01 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Returns_01.prefab:283f0b0396c1eba49a3ffb43a9629372");

	// Token: 0x04002073 RID: 8307
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Returns_02 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Returns_02.prefab:41b03f7e9ca5e654b85f8a4096802025");

	// Token: 0x04002074 RID: 8308
	private static readonly AssetReference VO_TRLA_201h_Male_Troll_Shrine_Returns_04 = new AssetReference("VO_TRLA_201h_Male_Troll_Shrine_Returns_04.prefab:561b605fedc991944a39644c4104ba97");

	// Token: 0x04002075 RID: 8309
	private List<string> m_HexLines = new List<string>
	{
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Event_General_01,
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_Shrine_Event_General_02,
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Damage_All_01
	};

	// Token: 0x04002076 RID: 8310
	private List<string> m_OverloadLines = new List<string>
	{
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_01,
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_02,
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_Card_Draw_03
	};

	// Token: 0x04002077 RID: 8311
	private List<string> m_BigSpellLines = new List<string>
	{
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_BigSpell_01,
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_BigSpell_02,
		TRL_Dungeon_Boss_201h.VO_TRLA_201h_Male_Troll_EVENT_BigSpell_03
	};

	// Token: 0x04002078 RID: 8312
	private HashSet<string> m_playedLines = new HashSet<string>();
}
