using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public class DRGA_Good_Fight_01 : DRGA_Dungeon
{
	// Token: 0x0600429B RID: 17051 RVA: 0x00166824 File Offset: 0x00164A24
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_01.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_PlayerStart_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01,
			DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600429C RID: 17052 RVA: 0x00166A98 File Offset: 0x00164C98
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_IdleLines;
	}

	// Token: 0x0600429D RID: 17053 RVA: 0x00166AA0 File Offset: 0x00164CA0
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPowerLines;
	}

	// Token: 0x0600429E RID: 17054 RVA: 0x00166AA8 File Offset: 0x00164CA8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01;
	}

	// Token: 0x0600429F RID: 17055 RVA: 0x00166AC0 File Offset: 0x00164CC0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x060042A0 RID: 17056 RVA: 0x00166B91 File Offset: 0x00164D91
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_01.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_01.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_01.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01, 2.5f);
				goto IL_375;
			}
			goto IL_375;
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01, 2.5f);
			goto IL_375;
		case 104:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_01.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_375;
			}
			goto IL_375;
		case 105:
			yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01, 2.5f);
			goto IL_375;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_01.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01, 2.5f);
				goto IL_375;
			}
			goto IL_375;
		case 107:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_01.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01, 2.5f);
				yield return new WaitForSeconds(3.25f);
				GameState.Get().SetBusy(false);
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01, 2.5f);
				goto IL_375;
			}
			goto IL_375;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_375:
		yield break;
	}

	// Token: 0x060042A1 RID: 17057 RVA: 0x00166BA7 File Offset: 0x00164DA7
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 33902556U)
		{
			if (num != 347318U)
			{
				if (num != 17124937U)
				{
					if (num != 33902556U)
					{
						goto IL_2FA;
					}
					if (!(cardId == "DRGA_BOSS_05t3"))
					{
						goto IL_2FA;
					}
				}
				else if (!(cardId == "DRGA_BOSS_05t4"))
				{
					goto IL_2FA;
				}
			}
			else if (!(cardId == "DRGA_BOSS_05t5"))
			{
				goto IL_2FA;
			}
		}
		else if (num <= 2045944872U)
		{
			if (num != 963594556U)
			{
				if (num != 2045944872U)
				{
					goto IL_2FA;
				}
				if (!(cardId == "BOT_238"))
				{
					goto IL_2FA;
				}
				if (this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01, 2.5f);
					goto IL_2FA;
				}
				goto IL_2FA;
			}
			else
			{
				if (!(cardId == "GVG_110"))
				{
					goto IL_2FA;
				}
				if (this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01, 2.5f);
					goto IL_2FA;
				}
				goto IL_2FA;
			}
		}
		else if (num != 3632122375U)
		{
			if (num != 4061565529U)
			{
				goto IL_2FA;
			}
			if (!(cardId == "DAL_008"))
			{
				goto IL_2FA;
			}
			if (this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01, 2.5f);
				goto IL_2FA;
			}
			goto IL_2FA;
		}
		else if (!(cardId == "DRGA_BOSS_05t"))
		{
			goto IL_2FA;
		}
		yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_InventionLines);
		IL_2FA:
		yield break;
	}

	// Token: 0x060042A2 RID: 17058 RVA: 0x00166BBD File Offset: 0x00164DBD
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
		if (!(cardId == "DRGA_BOSS_05t2"))
		{
			if (!(cardId == "DRGA_BOSS_05t4"))
			{
				if (!(cardId == "DRGA_BOSS_05t3"))
				{
					if (!(cardId == "DRGA_BOSS_05t"))
					{
						if (cardId == "DRGA_BOSS_05t5")
						{
							yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrageLines);
		}
		yield break;
	}

	// Token: 0x040032F7 RID: 13047
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01.prefab:c615497291aad6d4e9c78ad885519bdd");

	// Token: 0x040032F8 RID: 13048
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01.prefab:661d71cae3c252d4cb2333e23c8016eb");

	// Token: 0x040032F9 RID: 13049
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01.prefab:2752a53a5dba7bc4cb4274ed6674bc85");

	// Token: 0x040032FA RID: 13050
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01.prefab:5933275eed2b8364ba93e68322fbb7d9");

	// Token: 0x040032FB RID: 13051
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_PlayerStart_01.prefab:a4da2de66b2a4ec4eaaccf9e699bdcd5");

	// Token: 0x040032FC RID: 13052
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01.prefab:679fd7eb47e528b4d8e4c5fa423851db");

	// Token: 0x040032FD RID: 13053
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01.prefab:7035ad03ffa34b2478c33b7ade3081d5");

	// Token: 0x040032FE RID: 13054
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01.prefab:949b7d9d2eedbc94685abfd8bb1ac887");

	// Token: 0x040032FF RID: 13055
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01.prefab:6c03f8b373447e24a86aaac882f9ffcc");

	// Token: 0x04003300 RID: 13056
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01.prefab:c79fb8b74a7371f428cb86590835ccd2");

	// Token: 0x04003301 RID: 13057
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01.prefab:d20c69d50f0d36945909da3a456c24bf");

	// Token: 0x04003302 RID: 13058
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01.prefab:0d6bb9e30d011484795a00216360088f");

	// Token: 0x04003303 RID: 13059
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01.prefab:90e1edceee3731044b9ebb3c692da15c");

	// Token: 0x04003304 RID: 13060
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01.prefab:41df9a4d0353265428a7778e293cae54");

	// Token: 0x04003305 RID: 13061
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01.prefab:66bc198ee1c355a41a8cda959d55af41");

	// Token: 0x04003306 RID: 13062
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01.prefab:7a89c126984d2184e8b40b0e7aa2b62e");

	// Token: 0x04003307 RID: 13063
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01.prefab:8e70ce6185c2ac648bc6e411c7d92e26");

	// Token: 0x04003308 RID: 13064
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01.prefab:75ddc30cfe66cfe49a2713bbb17319cb");

	// Token: 0x04003309 RID: 13065
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01.prefab:8af1072e5db259d43a8fb5d111ae5a4f");

	// Token: 0x0400330A RID: 13066
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01.prefab:a21d84910671b0648a22318c8a6167c5");

	// Token: 0x0400330B RID: 13067
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01.prefab:6383705f81805114995dbfc3de4c3aa8");

	// Token: 0x0400330C RID: 13068
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01.prefab:f1a78d627e1963840836335c864deae5");

	// Token: 0x0400330D RID: 13069
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01.prefab:83a99a93a71c1e8478348296c39cb81b");

	// Token: 0x0400330E RID: 13070
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01.prefab:7389b40ea8f8ac14d870f73a64d53004");

	// Token: 0x0400330F RID: 13071
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01.prefab:a434d952a1164d841954781a05ab3e44");

	// Token: 0x04003310 RID: 13072
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01.prefab:690120117c943cd4f9062677f30385a8");

	// Token: 0x04003311 RID: 13073
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01.prefab:378b56173f071864aba84ee64881b267");

	// Token: 0x04003312 RID: 13074
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01.prefab:f8d2c419c67f7114a81a7ae25c25e969");

	// Token: 0x04003313 RID: 13075
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01.prefab:9fcf1b8156baf7944b6faad9359da4cc");

	// Token: 0x04003314 RID: 13076
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01.prefab:eb4bbd196f5ff5643bcc35f31a5072c2");

	// Token: 0x04003315 RID: 13077
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01.prefab:03087bc3567ccc04793ae412293c2cfc");

	// Token: 0x04003316 RID: 13078
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01.prefab:58644d40b7142e146a4382ace99a7d84");

	// Token: 0x04003317 RID: 13079
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01.prefab:40008b7fc1a38b8448aa4f562b342a57");

	// Token: 0x04003318 RID: 13080
	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01
	};

	// Token: 0x04003319 RID: 13081
	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrageLines = new List<string>
	{
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01
	};

	// Token: 0x0400331A RID: 13082
	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_IdleLines = new List<string>
	{
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01
	};

	// Token: 0x0400331B RID: 13083
	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_InventionLines = new List<string>
	{
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01,
		DRGA_Good_Fight_01.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01
	};

	// Token: 0x0400331C RID: 13084
	private HashSet<string> m_playedLines = new HashSet<string>();
}
