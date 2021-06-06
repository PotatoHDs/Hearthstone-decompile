using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000568 RID: 1384
public class BOM_02_Xyrella_Dungeon : BOM_02_Xyrella_MissionEntity
{
	// Token: 0x06004CD8 RID: 19672 RVA: 0x001966FC File Offset: 0x001948FC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetBossIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetBossIdleLines());
		this.m_OverrideMusicTrack = MusicPlaylistType.Invalid;
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.Invalid;
		this.m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
		this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
		this.m_OverrideBossSubtext = null;
		this.m_OverridePlayerSubtext = null;
		this.m_SupressEnemyDeathTextBubble = false;
	}

	// Token: 0x06004CD9 RID: 19673 RVA: 0x00196778 File Offset: 0x00194978
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05,
			BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004CDA RID: 19674 RVA: 0x00195570 File Offset: 0x00193770
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOM;
	}

	// Token: 0x06004CDB RID: 19675 RVA: 0x00196B3C File Offset: 0x00194D3C
	public static BOM_02_Xyrella_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BOM_02_Xyrella_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BOM_02_Xyrella_Dungeon();
	}

	// Token: 0x06004CDC RID: 19676 RVA: 0x00196B6F File Offset: 0x00194D6F
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		entity.GetCardId();
		yield break;
	}

	// Token: 0x06004CDD RID: 19677 RVA: 0x00196B85 File Offset: 0x00194D85
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		base.MissionPause(true);
		yield return this.HandleMissionEventWithTiming(514);
		base.MissionPause(false);
		yield break;
	}

	// Token: 0x06004CDE RID: 19678 RVA: 0x00196B94 File Offset: 0x00194D94
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		UnityEngine.Random.Range(0f, 1f);
		base.GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		float num = UnityEngine.Random.Range(0f, 1f);
		if (missionEvent <= 518)
		{
			if (missionEvent != 516)
			{
				if (missionEvent == 518)
				{
					if (!base.shouldPlayBanterVO())
					{
						goto IL_CF0;
					}
					if (this.m_Scabbs_isDead && this.m_Tavish_isDead)
					{
						yield return base.MissionPlayVO("BOM_02_Scabbs_06t", this.m_Scabbs_isDeadLines);
						yield return base.MissionPlayVO("BOM_02_Tavish_01t", this.m_Tavish_IsDeadLines);
						goto IL_CF0;
					}
					if (this.m_Scabbs_isDead)
					{
						yield return base.MissionPlayVO("BOM_02_Scabbs_06t", this.m_Scabbs_isDeadLines);
						goto IL_CF0;
					}
					if (this.m_Tavish_isDead)
					{
						yield return base.MissionPlayVO("BOM_02_Tavish_01t", this.m_Tavish_IsDeadLines);
						goto IL_CF0;
					}
					yield return base.MissionPlayThinkEmote(actor);
					goto IL_CF0;
				}
			}
			else
			{
				if (this.m_SupressEnemyDeathTextBubble)
				{
					yield return base.MissionPlaySound(enemyActor, this.m_deathLine);
					goto IL_CF0;
				}
				yield return base.MissionPlayVO(enemyActor, this.m_deathLine);
				goto IL_CF0;
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 600:
				this.m_Mission_EnemyHeroShouldExplodeOnDefeat = false;
				goto IL_CF0;
			case 601:
				this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = false;
				goto IL_CF0;
			case 602:
				this.m_MissionDisableAutomaticVO = true;
				goto IL_CF0;
			case 603:
				this.m_MissionDisableAutomaticVO = false;
				goto IL_CF0;
			case 604:
			case 605:
			case 606:
			case 607:
			case 608:
			case 609:
				break;
			case 610:
				this.m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
				goto IL_CF0;
			case 611:
				this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
				goto IL_CF0;
			case 612:
				this.m_DoEmoteDrivenStart = true;
				goto IL_CF0;
			default:
				switch (missionEvent)
				{
				case 1000:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					if (this.m_PlayPlayerVOLineIndex + 1 >= this.m_PlayerVOLines.Count)
					{
						this.m_PlayPlayerVOLineIndex = 0;
					}
					else
					{
						this.m_PlayPlayerVOLineIndex++;
					}
					SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
					yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
					goto IL_CF0;
				case 1001:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
					yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
					goto IL_CF0;
				case 1002:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					if (this.m_PlayBossVOLineIndex + 1 >= this.m_BossVOLines.Count)
					{
						this.m_PlayBossVOLineIndex = 0;
					}
					else
					{
						this.m_PlayBossVOLineIndex++;
					}
					SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
					yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
					goto IL_CF0;
				case 1003:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
					yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
					goto IL_CF0;
				case 1004:
				case 1005:
				case 1006:
				case 1007:
				case 1008:
				case 1009:
					break;
				case 1010:
					if (this.m_forceAlwaysPlayLine)
					{
						this.m_forceAlwaysPlayLine = false;
						goto IL_CF0;
					}
					this.m_forceAlwaysPlayLine = true;
					goto IL_CF0;
				case 1011:
				{
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					foreach (string text in this.m_BossVOLines)
					{
						SceneDebugger.Get().AddMessage(text);
						yield return base.MissionPlayVO(enemyActor, text);
					}
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					foreach (string text2 in this.m_PlayerVOLines)
					{
						SceneDebugger.Get().AddMessage(text2);
						yield return base.MissionPlayVO(enemyActor, text2);
					}
					enumerator = default(List<string>.Enumerator);
					goto IL_CF0;
				}
				case 1012:
				{
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					foreach (string text3 in this.m_BossVOLines)
					{
						SceneDebugger.Get().AddMessage(text3);
						yield return base.MissionPlayVO(enemyActor, text3);
					}
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					goto IL_CF0;
				}
				case 1013:
				{
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					foreach (string text4 in this.m_PlayerVOLines)
					{
						SceneDebugger.Get().AddMessage(text4);
						yield return base.MissionPlayVO(enemyActor, text4);
					}
					List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					goto IL_CF0;
				}
				default:
					switch (missionEvent)
					{
					case 58023:
					{
						SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
						GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
						SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
						goto IL_CF0;
					}
					case 58032:
						yield return base.MissionPlayVO("BOM_02_Tavish_01t", this.m_Tavish_TriggerLines);
						goto IL_CF0;
					case 58033:
						this.m_Tavish_isDead = true;
						yield return base.MissionPlaySound("BOM_02_Tavish_01t", this.m_Tavish_DeathLines);
						goto IL_CF0;
					case 58034:
						if (!base.shouldPlayBanterVO())
						{
							goto IL_CF0;
						}
						if (num > 0.5f)
						{
							yield return base.MissionPlayVO(actor, this.m_Xyrella_HPHeal);
							goto IL_CF0;
						}
						yield return base.MissionPlayVO("BOM_02_Tavish_01t", this.m_Tavish_HealLines);
						goto IL_CF0;
					case 58035:
						this.m_Tavish_isDead = true;
						yield return base.MissionPlayVO("BOM_02_Tavish_01t", this.m_Tavish_IsDeadLines);
						goto IL_CF0;
					case 58036:
						this.m_Tavish_isDead = false;
						if (!base.shouldPlayBanterVO())
						{
							goto IL_CF0;
						}
						if (num > 0.5f)
						{
							yield return base.MissionPlayVO(actor, this.m_Xyrella_HPRez);
							goto IL_CF0;
						}
						yield return base.MissionPlayVO("BOM_02_Tavish_01t", this.m_Tavish_RezLines);
						goto IL_CF0;
					case 58037:
						this.m_Scabbs_isDead = false;
						if (!base.shouldPlayBanterVO())
						{
							goto IL_CF0;
						}
						if (num > 0.5f)
						{
							yield return base.MissionPlayVO(actor, this.m_Xyrella_HPRez);
							goto IL_CF0;
						}
						yield return base.MissionPlayVO("BOM_02_Scabbs_06t", this.m_Scabbs_RezLines);
						goto IL_CF0;
					case 58038:
						this.m_Tavish_isDead = true;
						yield return base.MissionPlaySound("BOM_02_Scabbs_06t", this.m_Scabbs_DeathLines);
						goto IL_CF0;
					case 58039:
						yield return base.MissionPlayVO("BOM_02_Scabbs_06t", this.m_Scabbs_TriggerLines);
						goto IL_CF0;
					case 58040:
						if (!base.shouldPlayBanterVO())
						{
							goto IL_CF0;
						}
						if (num > 0.5f)
						{
							yield return base.MissionPlayVO(actor, this.m_Xyrella_HPHeal);
							goto IL_CF0;
						}
						yield return base.MissionPlayVO("BOM_02_Scabbs_06t", this.m_Scabbs_HealLines);
						goto IL_CF0;
					case 58041:
						yield return base.MissionPlayVO("BOM_02_Scabbs_06t", this.m_Scabbs_isDeadLines);
						goto IL_CF0;
					case 58042:
						if (base.shouldPlayBanterVO())
						{
							yield return base.MissionPlaySound("BOM_02_Tavish_01t", this.m_Tavish_AttackLines);
							goto IL_CF0;
						}
						goto IL_CF0;
					case 58043:
						if (base.shouldPlayBanterVO())
						{
							yield return base.MissionPlaySound("BOM_02_Scabbs_06t", this.m_Scabbs_AttackLines);
							goto IL_CF0;
						}
						goto IL_CF0;
					}
					break;
				}
				break;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_CF0:
		yield break;
		yield break;
	}

	// Token: 0x04004229 RID: 16937
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01.prefab:bdc7cdc8f439b7742b90d090b0a68ac6");

	// Token: 0x0400422A RID: 16938
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02.prefab:60004d0fb7385e547a8224910590ae8e");

	// Token: 0x0400422B RID: 16939
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03.prefab:5c82e945fe93bc241bd3e0e8e7a24dea");

	// Token: 0x0400422C RID: 16940
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04.prefab:137db0ac137e1904db25fbedef06220a");

	// Token: 0x0400422D RID: 16941
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05.prefab:7396e59074a787842b10d41833e31e0a");

	// Token: 0x0400422E RID: 16942
	private List<string> m_Tavish_TriggerLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05
	};

	// Token: 0x0400422F RID: 16943
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01.prefab:1987a509960661a49a9e90b9e184bf76");

	// Token: 0x04004230 RID: 16944
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02.prefab:740f8883faa6d66498d13c7bc4538e99");

	// Token: 0x04004231 RID: 16945
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03.prefab:f9e4dc77e90ef984caecf46604c7e981");

	// Token: 0x04004232 RID: 16946
	private List<string> m_Tavish_DeathLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03
	};

	// Token: 0x04004233 RID: 16947
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01.prefab:e72b6cdb10b7559408a53af9972b163e");

	// Token: 0x04004234 RID: 16948
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02.prefab:385813dd835f47040963445651d6ecbf");

	// Token: 0x04004235 RID: 16949
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03.prefab:99784d26fdf7a1f4bb30610f3adeccc1");

	// Token: 0x04004236 RID: 16950
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04.prefab:74a7a948eb3a8c1458fefffce4ee6625");

	// Token: 0x04004237 RID: 16951
	private List<string> m_Tavish_HealLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04
	};

	// Token: 0x04004238 RID: 16952
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01.prefab:0faf2869c5fe23142bdc18cc709b209d");

	// Token: 0x04004239 RID: 16953
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02.prefab:79cdd1ddc7fdb494ca990a33e07ba8f4");

	// Token: 0x0400423A RID: 16954
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03.prefab:693af485ff9a431469f253a98277e58d");

	// Token: 0x0400423B RID: 16955
	private List<string> m_Tavish_IsDeadLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03
	};

	// Token: 0x0400423C RID: 16956
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01.prefab:a7368390318b5264a96defe9fa21da43");

	// Token: 0x0400423D RID: 16957
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02.prefab:6cb1c9d46351b7043ab57b30330fad7e");

	// Token: 0x0400423E RID: 16958
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03.prefab:80fb4434ee751714ab5dbfda2c3ac467");

	// Token: 0x0400423F RID: 16959
	private List<string> m_Tavish_RezLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03
	};

	// Token: 0x04004240 RID: 16960
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01.prefab:6505ae2ddb7e6c84cad08b7d665d27cc");

	// Token: 0x04004241 RID: 16961
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02.prefab:ffaf1d03355a5584fbb58bf5d5fd4ff4");

	// Token: 0x04004242 RID: 16962
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03.prefab:7ca800208e669934389c7ffc29b8cefc");

	// Token: 0x04004243 RID: 16963
	private List<string> m_Tavish_AttackLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03
	};

	// Token: 0x04004244 RID: 16964
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01.prefab:3931eef6f40244e46822ab4c9522297e");

	// Token: 0x04004245 RID: 16965
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02.prefab:1192a9d9a7982594e979ccc257172026");

	// Token: 0x04004246 RID: 16966
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03.prefab:74264db1dc70a194a9b801750beded5e");

	// Token: 0x04004247 RID: 16967
	private List<string> m_Scabbs_RezLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03
	};

	// Token: 0x04004248 RID: 16968
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01.prefab:359d4bbea22e8ef49bb81bb5224dda87");

	// Token: 0x04004249 RID: 16969
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02.prefab:c28119a2c173320489d49fba69e7dbb0");

	// Token: 0x0400424A RID: 16970
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03.prefab:1a305fda837bbfa4980242b29660e280");

	// Token: 0x0400424B RID: 16971
	private List<string> m_Scabbs_DeathLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03
	};

	// Token: 0x0400424C RID: 16972
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01.prefab:ba9ea9cc6632a5e44883797e594f9b66");

	// Token: 0x0400424D RID: 16973
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02.prefab:42ba2603fb3d75b499fdcab3041574d8");

	// Token: 0x0400424E RID: 16974
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03.prefab:da8b38b924a7a1347894c088d9cb4584");

	// Token: 0x0400424F RID: 16975
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04.prefab:63a368c752c74d343ad61f4ec3b38642");

	// Token: 0x04004250 RID: 16976
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05.prefab:32bb44d8c8f2726449c436effa0b439f");

	// Token: 0x04004251 RID: 16977
	private List<string> m_Scabbs_TriggerLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05
	};

	// Token: 0x04004252 RID: 16978
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01.prefab:21f7df62e12f70942a57b8fddebb0d11");

	// Token: 0x04004253 RID: 16979
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02.prefab:7a0b9cf0bbf9eff4f89751b024585bdb");

	// Token: 0x04004254 RID: 16980
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03.prefab:3113cf4b0a979434ba8bdbde4dee786e");

	// Token: 0x04004255 RID: 16981
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04.prefab:4e6ef9170e7b8eb44b31e9d200fcc6a2");

	// Token: 0x04004256 RID: 16982
	private List<string> m_Scabbs_HealLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04
	};

	// Token: 0x04004257 RID: 16983
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01.prefab:62f5d5bc3c07e2843970a4d226583687");

	// Token: 0x04004258 RID: 16984
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02.prefab:03a1158d578a0da43bee54ba64997f80");

	// Token: 0x04004259 RID: 16985
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03.prefab:41325e4422e2a93478602fcdc42901db");

	// Token: 0x0400425A RID: 16986
	private List<string> m_Scabbs_isDeadLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03
	};

	// Token: 0x0400425B RID: 16987
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01.prefab:b13d79013fab8aa42887ea962872dbd6");

	// Token: 0x0400425C RID: 16988
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02.prefab:9a7a93b7a3d3d1e42bfe32e1ba7288ca");

	// Token: 0x0400425D RID: 16989
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03.prefab:84d0a22dbc608874c8360efb6aea6216");

	// Token: 0x0400425E RID: 16990
	private List<string> m_Scabbs_AttackLines = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04
	};

	// Token: 0x0400425F RID: 16991
	public readonly AssetReference Tavish_BrassRing = new AssetReference("Tavish_BrassRing_Quote.prefab:ad6adae48f4bfba4da53b7138111c1e3");

	// Token: 0x04004260 RID: 16992
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01.prefab:53f58e50aac9ccc41a764aff34c50340");

	// Token: 0x04004261 RID: 16993
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02.prefab:e5614706798e2cf43ac1fca0e2581af8");

	// Token: 0x04004262 RID: 16994
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03.prefab:e1577bcbb62807e45aa6c808714db2e7");

	// Token: 0x04004263 RID: 16995
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04.prefab:97404acfc3770224fb1d352abadce4fa");

	// Token: 0x04004264 RID: 16996
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05.prefab:ba5c601009dc72f4da982fa94abd7e7c");

	// Token: 0x04004265 RID: 16997
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06.prefab:6246f9c7f3340b14f89b396dc3cc05fe");

	// Token: 0x04004266 RID: 16998
	private List<string> m_Xyrella_HPHeal = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06
	};

	// Token: 0x04004267 RID: 16999
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01.prefab:7f83089cafcb0ac4f8d06e61b1e7d50c");

	// Token: 0x04004268 RID: 17000
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02.prefab:f726954dc0742054dbb14ab45690b46d");

	// Token: 0x04004269 RID: 17001
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03.prefab:cd8028c5856c251449a90814bd67cbdf");

	// Token: 0x0400426A RID: 17002
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04.prefab:11383f944ada27b4d95bf89b246a82cc");

	// Token: 0x0400426B RID: 17003
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05.prefab:54e7707836df865498dd5fc41552d4c4");

	// Token: 0x0400426C RID: 17004
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06.prefab:075634794bf6d3e4199382d60ef66067");

	// Token: 0x0400426D RID: 17005
	private List<string> m_Xyrella_HPRez = new List<string>
	{
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05,
		BOM_02_Xyrella_Dungeon.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06
	};

	// Token: 0x0400426E RID: 17006
	public const int Tavish_TriggerLine = 58032;

	// Token: 0x0400426F RID: 17007
	public const int Tavish_DeathLines = 58033;

	// Token: 0x04004270 RID: 17008
	public const int Tavish_HealLines = 58034;

	// Token: 0x04004271 RID: 17009
	public const int Tavish_IsDeadLines = 58035;

	// Token: 0x04004272 RID: 17010
	public const int Tavish_RezLines = 58036;

	// Token: 0x04004273 RID: 17011
	public const int Tavish_Attack = 58042;

	// Token: 0x04004274 RID: 17012
	public const int Scabbs_RezLines = 58037;

	// Token: 0x04004275 RID: 17013
	public const int Scabbs_DeathLines = 58038;

	// Token: 0x04004276 RID: 17014
	public const int Scabbs_TriggerLines = 58039;

	// Token: 0x04004277 RID: 17015
	public const int Scabbs_HealLines = 58040;

	// Token: 0x04004278 RID: 17016
	public const int Scabbs_isDeadLines = 58041;

	// Token: 0x04004279 RID: 17017
	public const int Scabbs_Attack = 58043;

	// Token: 0x0400427A RID: 17018
	public bool m_Scabbs_isDead;

	// Token: 0x0400427B RID: 17019
	public bool m_Tavish_isDead;

	// Token: 0x0400427C RID: 17020
	public const int XyrellaCustomIdle = 58042;

	// Token: 0x0400427D RID: 17021
	public const float m_Xyrella_HP_Speaking_Chance = 0.5f;

	// Token: 0x0400427E RID: 17022
	public float m_Xyrella_HP_Seconds_Since_Action;
}
