using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E6 RID: 1254
public class DRGA_Good_Fight_12 : DRGA_Dungeon
{
	// Token: 0x0600432E RID: 17198 RVA: 0x0016BA08 File Offset: 0x00169C08
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_PlayerStart_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01,
			DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01,
			DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01,
			DRGA_Good_Fight_12.VO_ULDA_Reno_Male_Human_Thinking_02,
			DRGA_Good_Fight_12.VO_ULDA_Reno_Male_Human_Threaten_01,
			DRGA_Good_Fight_12.VO_DRG_610_Male_Dragon_Threaten_01,
			DRGA_Good_Fight_12.VO_DRG_650_Male_Dragon_Threaten_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600432F RID: 17199 RVA: 0x0016BFCC File Offset: 0x0016A1CC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGLOEBoss);
	}

	// Token: 0x06004330 RID: 17200 RVA: 0x0016BFDE File Offset: 0x0016A1DE
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_IdleLines;
	}

	// Token: 0x06004331 RID: 17201 RVA: 0x0016BFE6 File Offset: 0x0016A1E6
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01;
	}

	// Token: 0x06004332 RID: 17202 RVA: 0x0016C000 File Offset: 0x0016A200
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				return;
			}
			if (this.m_Galakrond)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (!this.m_Galakrond)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
	}

	// Token: 0x06004333 RID: 17203 RVA: 0x0016C110 File Offset: 0x0016A310
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 199)
		{
			switch (missionEvent)
			{
			case 100:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.BrannBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 101:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.BrannBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 102:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 103:
			case 105:
			case 108:
			case 109:
			case 111:
			case 112:
			case 113:
			case 114:
			case 116:
			case 117:
			case 118:
			case 119:
			case 125:
			case 129:
			case 131:
			case 132:
			case 138:
			case 139:
			case 142:
			case 143:
			case 144:
				break;
			case 104:
				if (this.m_Galakrond && !this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01, 2.5f);
				}
				if (!this.m_Galakrond && !this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 106:
				this.m_InvokeTracker++;
				GameState.Get().SetBusy(true);
				yield return base.PlayLineInOrderOnce(enemyActor, this.m_missionEventTrigger106Lines);
				if (!this.m_Heroic)
				{
					if (this.m_InvokeTracker == 2)
					{
						yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_ULDA_Reno_Male_Human_Thinking_02, 2.5f);
					}
					else if (this.m_InvokeTracker == 4)
					{
						yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_ULDA_Reno_Male_Human_Threaten_01, 2.5f);
					}
				}
				GameState.Get().SetBusy(false);
				goto IL_1289;
			case 107:
				if (this.m_Galakrond)
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01, 2.5f);
				}
				if (!this.m_Galakrond)
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 110:
				if (this.m_Galakrond)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_VO_BossHasGalakrond);
					goto IL_1289;
				}
				goto IL_1289;
			case 115:
				yield return base.PlayLineInOrderOnce(enemyActor, this.m_VO_PlayerPlaysGalakrond);
				goto IL_1289;
			case 120:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 121:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 122:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 123:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 124:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01, 2.5f);
					yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 126:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 127:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.BrannBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 128:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 130:
				if (this.m_Heroic)
				{
					goto IL_1289;
				}
				if (this.m_Galakrond)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01, 2.5f);
				}
				if (!this.m_Galakrond)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 133:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 134:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, DRGA_Good_Fight_12.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			case 135:
				GameState.Get().SetBusy(true);
				this.m_Galakrond = true;
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_1289;
			case 136:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineInOrderOnce(enemyActor, this.m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_ExpositionLines);
				GameState.Get().SetBusy(false);
				goto IL_1289;
			case 137:
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_GalakrondHeroPowerTrigger);
				goto IL_1289;
			case 140:
				GameState.Get().SetBusy(true);
				this.m_Galakrond = true;
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_1289;
			case 141:
				GameState.Get().SetBusy(true);
				this.m_Galakrond = true;
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01, 2.5f);
				}
				GameState.Get().SetBusy(false);
				goto IL_1289;
			case 145:
				if (!this.m_Heroic)
				{
					yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01, 2.5f);
					goto IL_1289;
				}
				goto IL_1289;
			default:
				if (missionEvent == 199)
				{
					if (!this.m_Heroic)
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionEventHeroPowerTrigger);
						goto IL_1289;
					}
					goto IL_1289;
				}
				break;
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 505:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, 2.5f);
				goto IL_1289;
			case 506:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, 2.5f);
				goto IL_1289;
			case 507:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, 2.5f);
				goto IL_1289;
			case 508:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, 2.5f);
				goto IL_1289;
			case 509:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, 2.5f);
				goto IL_1289;
			case 510:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, 2.5f);
				goto IL_1289;
			case 511:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, 2.5f);
				goto IL_1289;
			case 512:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, 2.5f);
				goto IL_1289;
			case 513:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, 2.5f);
				goto IL_1289;
			case 514:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, 2.5f);
				goto IL_1289;
			case 515:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, 2.5f);
				goto IL_1289;
			case 516:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, 2.5f);
				goto IL_1289;
			case 517:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, 2.5f);
				goto IL_1289;
			case 518:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, 2.5f);
				goto IL_1289;
			case 519:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, 2.5f);
				goto IL_1289;
			case 520:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, 2.5f);
				goto IL_1289;
			case 521:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, 2.5f);
				goto IL_1289;
			case 522:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, 2.5f);
				goto IL_1289;
			case 523:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, 2.5f);
				goto IL_1289;
			case 524:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, 2.5f);
				goto IL_1289;
			case 525:
				yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, 2.5f);
				goto IL_1289;
			default:
				switch (missionEvent)
				{
				case 555:
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, 2.5f);
					GameState.Get().SetBusy(false);
					goto IL_1289;
				case 556:
					yield return new WaitForSeconds(4f);
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, 2.5f);
					GameState.Get().SetBusy(false);
					goto IL_1289;
				case 557:
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(DRGA_Good_Fight_12.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_12.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, 2.5f);
					GameState.Get().SetBusy(false);
					goto IL_1289;
				}
				break;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1289:
		yield break;
	}

	// Token: 0x06004334 RID: 17204 RVA: 0x0016C126 File Offset: 0x0016A326
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
		yield break;
	}

	// Token: 0x06004335 RID: 17205 RVA: 0x0016C13C File Offset: 0x0016A33C
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
		yield break;
	}

	// Token: 0x040034C4 RID: 13508
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01.prefab:3c2f60746e4c84e429d1e608a606107a");

	// Token: 0x040034C5 RID: 13509
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_PlayerStart_01.prefab:39a35257a0ac6f8418dd100315960f57");

	// Token: 0x040034C6 RID: 13510
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01.prefab:fc90d2ee33a2a2b4ab809aa1c5c6fd46");

	// Token: 0x040034C7 RID: 13511
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01.prefab:5ac9f0f4046c7c842afeaf23cfba5ee3");

	// Token: 0x040034C8 RID: 13512
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01.prefab:4ea2f7e5f984e124cb274b112f2e38de");

	// Token: 0x040034C9 RID: 13513
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01.prefab:f93f7bf3762653649b5ebc71b4fa7321");

	// Token: 0x040034CA RID: 13514
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01.prefab:5da54e0334c37f74ea6b614c2a33a22f");

	// Token: 0x040034CB RID: 13515
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01.prefab:9acc290eb6f7adf44bcd496eeaa86d41");

	// Token: 0x040034CC RID: 13516
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01.prefab:a7344630bb25eb0469113101a5c88d04");

	// Token: 0x040034CD RID: 13517
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01.prefab:3abf06c829e34644f8cbcd32cedd3129");

	// Token: 0x040034CE RID: 13518
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01.prefab:ba38ff15f9ef02e4987ed6b78badea97");

	// Token: 0x040034CF RID: 13519
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01.prefab:b929a990e505a7d4e8963bafe8d49f56");

	// Token: 0x040034D0 RID: 13520
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01.prefab:d8c0ab1b6f4cb9148818e6e5f2eb6b19");

	// Token: 0x040034D1 RID: 13521
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01.prefab:c96499ffe468deb4982201ef8b8869ae");

	// Token: 0x040034D2 RID: 13522
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01.prefab:55446bcd85245c24b8c9b34f59967196");

	// Token: 0x040034D3 RID: 13523
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01.prefab:cf8f041483c58494dbebe2d168f8acb0");

	// Token: 0x040034D4 RID: 13524
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01.prefab:9ed0c0a7908fe494bbb1959203571498");

	// Token: 0x040034D5 RID: 13525
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01.prefab:276c23be1c85aec4db99aca53c052cce");

	// Token: 0x040034D6 RID: 13526
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01.prefab:5e6bedd3c5de0d0438b2a993ddd5c721");

	// Token: 0x040034D7 RID: 13527
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01.prefab:b8a15af7e3009e246b9f5eebf7f66906");

	// Token: 0x040034D8 RID: 13528
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01.prefab:9c6a0ba03296f8f4e9041372185aec69");

	// Token: 0x040034D9 RID: 13529
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01.prefab:b1975961e4f2b6e429b682ea9139318a");

	// Token: 0x040034DA RID: 13530
	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01.prefab:51f7943dfb3b8704897068e7f5cc0867");

	// Token: 0x040034DB RID: 13531
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01.prefab:2e50bc22d063aec46952652e0e6ece7a");

	// Token: 0x040034DC RID: 13532
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01.prefab:eb9c22f23e619604b9de2e8cbf35486f");

	// Token: 0x040034DD RID: 13533
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01.prefab:f13d2d8751b90254bab7a94dd70d67db");

	// Token: 0x040034DE RID: 13534
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01.prefab:f9b97fd400ffca44db629c80efcb3895");

	// Token: 0x040034DF RID: 13535
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01.prefab:32a4b7c99f3aae145b2a414c984f355b");

	// Token: 0x040034E0 RID: 13536
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01.prefab:3b0a5793ab112f54599e953587948acf");

	// Token: 0x040034E1 RID: 13537
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01.prefab:ca6c35bf0c1d5d140aafc25c11c78cc7");

	// Token: 0x040034E2 RID: 13538
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01.prefab:861be9550341a2b47b1383d91e916507");

	// Token: 0x040034E3 RID: 13539
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01.prefab:4500792ba86ca0143b8d0040571c2d35");

	// Token: 0x040034E4 RID: 13540
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01.prefab:c8f6ae7cc5b081b48aad03997c35db79");

	// Token: 0x040034E5 RID: 13541
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01.prefab:f7b745602c378f7418804298f2eb7837");

	// Token: 0x040034E6 RID: 13542
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01.prefab:8908a649ff3bb784a9dcdeb6a0539604");

	// Token: 0x040034E7 RID: 13543
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01.prefab:048c458ecdb657d48a1048d779251599");

	// Token: 0x040034E8 RID: 13544
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01.prefab:42615239b9fb58b4c8b518c764b63137");

	// Token: 0x040034E9 RID: 13545
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01.prefab:d13f509fccbb2474bbf7bc5a276d7a4d");

	// Token: 0x040034EA RID: 13546
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01.prefab:a998f80b9b268df4fb86df39a37f992e");

	// Token: 0x040034EB RID: 13547
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01.prefab:a998749fb50c6264a9f178edb21d5b7f");

	// Token: 0x040034EC RID: 13548
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01.prefab:fef0bd9eb9481db43bc33e8639f61bc6");

	// Token: 0x040034ED RID: 13549
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01.prefab:ce67c7d371bd22d4b9e346aa1583df7f");

	// Token: 0x040034EE RID: 13550
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01.prefab:7ac14feda2477984aa143219f855979f");

	// Token: 0x040034EF RID: 13551
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01.prefab:194b03d8ed0c58f4b9c685c397dc6965");

	// Token: 0x040034F0 RID: 13552
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01.prefab:b3b2591977dbf4f47a76584b055f4869");

	// Token: 0x040034F1 RID: 13553
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01.prefab:d02b7e2b1c022ea41863bdcfab331b79");

	// Token: 0x040034F2 RID: 13554
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01.prefab:f51fddd3bf8701a468de80c17972bf63");

	// Token: 0x040034F3 RID: 13555
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01.prefab:9d551762e7ecc1845842217663425566");

	// Token: 0x040034F4 RID: 13556
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01.prefab:69d52f06ee6700d4dad52f6830adf9c3");

	// Token: 0x040034F5 RID: 13557
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01.prefab:97d4176bf290fb64a9f7aba668c6d013");

	// Token: 0x040034F6 RID: 13558
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01.prefab:d8f1f38e062c3d3419d4932421a3f434");

	// Token: 0x040034F7 RID: 13559
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01.prefab:620fb641515f72842b1c56912e35b4f2");

	// Token: 0x040034F8 RID: 13560
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01.prefab:43898a028b442c049a67a6fd8cdc71e7");

	// Token: 0x040034F9 RID: 13561
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01.prefab:43e889f7e3762aa43b89a80d4a09fd59");

	// Token: 0x040034FA RID: 13562
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01.prefab:675aa4e590ffda2459d2d314f9e6cf93");

	// Token: 0x040034FB RID: 13563
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01.prefab:e8918e27e9c288e4eb6049c347f6e08e");

	// Token: 0x040034FC RID: 13564
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01.prefab:c1442324e17668d4abfbd46d6eed287a");

	// Token: 0x040034FD RID: 13565
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01.prefab:3ef89842ff6bafa44b6f5eee23690dfd");

	// Token: 0x040034FE RID: 13566
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01.prefab:e40782b698eb1fb49abc18fc5b1b4eca");

	// Token: 0x040034FF RID: 13567
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01.prefab:0bb9093215198014fb9f69a097a6234a");

	// Token: 0x04003500 RID: 13568
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	// Token: 0x04003501 RID: 13569
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	// Token: 0x04003502 RID: 13570
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	// Token: 0x04003503 RID: 13571
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	// Token: 0x04003504 RID: 13572
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	// Token: 0x04003505 RID: 13573
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	// Token: 0x04003506 RID: 13574
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	// Token: 0x04003507 RID: 13575
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	// Token: 0x04003508 RID: 13576
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	// Token: 0x04003509 RID: 13577
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	// Token: 0x0400350A RID: 13578
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	// Token: 0x0400350B RID: 13579
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	// Token: 0x0400350C RID: 13580
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	// Token: 0x0400350D RID: 13581
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	// Token: 0x0400350E RID: 13582
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	// Token: 0x0400350F RID: 13583
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	// Token: 0x04003510 RID: 13584
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	// Token: 0x04003511 RID: 13585
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	// Token: 0x04003512 RID: 13586
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	// Token: 0x04003513 RID: 13587
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	// Token: 0x04003514 RID: 13588
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	// Token: 0x04003515 RID: 13589
	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	// Token: 0x04003516 RID: 13590
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01.prefab:fbfe70bab9271e746adc673ebe4e8ab4");

	// Token: 0x04003517 RID: 13591
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Thinking_02 = new AssetReference("VO_ULDA_Reno_Male_Human_Thinking_02.prefab:f17889dac17a5554d9c34316c4323b35");

	// Token: 0x04003518 RID: 13592
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Threaten_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Threaten_01.prefab:aa2839ab336aa254faf86c33e8a63b0f");

	// Token: 0x04003519 RID: 13593
	private static readonly AssetReference VO_DRG_610_Male_Dragon_Threaten_01 = new AssetReference("VO_DRG_610_Male_Dragon_Threaten_01.prefab:7c5e7f29cef55ea489b0393e9fb5a27d");

	// Token: 0x0400351A RID: 13594
	private static readonly AssetReference VO_DRG_650_Male_Dragon_Threaten_01 = new AssetReference("VO_DRG_650_Male_Dragon_Threaten_01.prefab:ab3985b6f0a1b86488c9f959cd57f51c");

	// Token: 0x0400351B RID: 13595
	private List<string> m_missionEventTrigger106Lines = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01
	};

	// Token: 0x0400351C RID: 13596
	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_ExpositionLines = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01
	};

	// Token: 0x0400351D RID: 13597
	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_IdleLines = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01
	};

	// Token: 0x0400351E RID: 13598
	private List<string> m_VO_PlayerPlaysGalakrond = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01
	};

	// Token: 0x0400351F RID: 13599
	private List<string> m_VO_BossHasGalakrond = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01
	};

	// Token: 0x04003520 RID: 13600
	private List<string> m_missionEventHeroPowerTrigger = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01,
		DRGA_Good_Fight_12.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01
	};

	// Token: 0x04003521 RID: 13601
	private List<string> m_GalakrondHeroPowerTrigger = new List<string>
	{
		DRGA_Good_Fight_12.VO_DRG_610_Male_Dragon_Threaten_01,
		DRGA_Good_Fight_12.VO_DRG_650_Male_Dragon_Threaten_01
	};

	// Token: 0x04003522 RID: 13602
	private int m_InvokeTracker;

	// Token: 0x04003523 RID: 13603
	private HashSet<string> m_playedLines = new HashSet<string>();
}
