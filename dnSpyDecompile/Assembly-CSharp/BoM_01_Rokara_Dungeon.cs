using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000566 RID: 1382
public class BoM_01_Rokara_Dungeon : BoM_01_Rokara_MissionEntity
{
	// Token: 0x06004C89 RID: 19593 RVA: 0x00195340 File Offset: 0x00193540
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
		this.m_SupressEnemyDeathTextBubble = true;
	}

	// Token: 0x06004C8A RID: 19594 RVA: 0x001953BC File Offset: 0x001935BC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_03,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_04,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_02,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04,
			BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C8B RID: 19595 RVA: 0x00195570 File Offset: 0x00193770
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOM;
	}

	// Token: 0x06004C8C RID: 19596 RVA: 0x00195578 File Offset: 0x00193778
	public static BoM_01_Rokara_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BoM_01_Rokara_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BoM_01_Rokara_Dungeon();
	}

	// Token: 0x06004C8D RID: 19597 RVA: 0x001955AB File Offset: 0x001937AB
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

	// Token: 0x06004C8E RID: 19598 RVA: 0x001955C1 File Offset: 0x001937C1
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		base.MissionPause(true);
		yield return this.HandleMissionEventWithTiming(514);
		base.MissionPause(false);
		yield break;
	}

	// Token: 0x06004C8F RID: 19599 RVA: 0x001955D0 File Offset: 0x001937D0
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		UnityEngine.Random.Range(0f, 1f);
		base.GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		if (missionEvent <= 516)
		{
			if (missionEvent != 508)
			{
				if (missionEvent == 516)
				{
					if (this.m_SupressEnemyDeathTextBubble)
					{
						yield return base.MissionPlaySound(enemyActor, this.m_deathLine);
						goto IL_A84;
					}
					yield return base.MissionPlayVO(enemyActor, this.m_deathLine);
					goto IL_A84;
				}
			}
			else
			{
				if (this.HeroPowerIsBrukan)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Brukan_HeroPowerLines);
				}
				if (this.HeroPowerIsGuff)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Guff_HeroPowerLines);
				}
				if (this.HeroPowerIsTamsin)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Tamsin_HeroPowerLines);
				}
				if (this.HeroPowerIsDawngrasp)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Dawngrasp_HeroPowerLines);
					goto IL_A84;
				}
				goto IL_A84;
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 600:
				this.m_Mission_EnemyHeroShouldExplodeOnDefeat = false;
				goto IL_A84;
			case 601:
				this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = false;
				goto IL_A84;
			case 602:
				this.m_MissionDisableAutomaticVO = true;
				goto IL_A84;
			case 603:
				this.m_MissionDisableAutomaticVO = false;
				goto IL_A84;
			case 604:
			case 605:
			case 606:
			case 607:
			case 608:
			case 609:
				break;
			case 610:
				this.m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
				goto IL_A84;
			case 611:
				this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
				goto IL_A84;
			case 612:
				this.m_DoEmoteDrivenStart = true;
				goto IL_A84;
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
					goto IL_A84;
				case 1001:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
					yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
					goto IL_A84;
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
					goto IL_A84;
				case 1003:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
					yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
					goto IL_A84;
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
						goto IL_A84;
					}
					this.m_forceAlwaysPlayLine = true;
					goto IL_A84;
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
					goto IL_A84;
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
					goto IL_A84;
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
					goto IL_A84;
				}
				default:
					switch (missionEvent)
					{
					case 58023:
					{
						SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
						GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
						SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
						goto IL_A84;
					}
					case 58024:
						this.HeroPowerIsBrukan = true;
						this.HeroPowerIsGuff = false;
						this.HeroPowerIsTamsin = false;
						this.HeroPowerIsDawngrasp = false;
						goto IL_A84;
					case 58025:
						this.HeroPowerIsBrukan = false;
						this.HeroPowerIsGuff = true;
						this.HeroPowerIsTamsin = false;
						this.HeroPowerIsDawngrasp = false;
						goto IL_A84;
					case 58026:
						this.HeroPowerIsBrukan = false;
						this.HeroPowerIsGuff = false;
						this.HeroPowerIsTamsin = true;
						this.HeroPowerIsDawngrasp = false;
						goto IL_A84;
					case 58027:
						this.HeroPowerIsBrukan = false;
						this.HeroPowerIsGuff = false;
						this.HeroPowerIsTamsin = false;
						this.HeroPowerIsDawngrasp = true;
						goto IL_A84;
					case 58028:
						if (!this.m_MissionDisableAutomaticVO)
						{
							GameState.Get().SetBusy(true);
							yield return base.MissionPlayVO("BOM_01_Brukan_08t", this.m_Brukan_HeroPowerLines);
							GameState.Get().SetBusy(false);
							goto IL_A84;
						}
						goto IL_A84;
					case 58029:
						if (!this.m_MissionDisableAutomaticVO)
						{
							GameState.Get().SetBusy(true);
							yield return base.MissionPlayVOOnce("BOM_01_Guff_02t", this.m_Guff_HeroPowerLines);
							GameState.Get().SetBusy(false);
							goto IL_A84;
						}
						goto IL_A84;
					case 58030:
						if (!this.m_MissionDisableAutomaticVO)
						{
							GameState.Get().SetBusy(true);
							yield return base.MissionPlayVOOnce("BOM_01_Tamsin_03t", this.m_Tamsin_HeroPowerLines);
							GameState.Get().SetBusy(false);
							goto IL_A84;
						}
						goto IL_A84;
					case 58031:
						if (!this.m_MissionDisableAutomaticVO)
						{
							GameState.Get().SetBusy(true);
							yield return base.MissionPlayVOOnce("BOM_01_Dawngrasp_04t", this.m_Dawngrasp_HeroPowerLines);
							GameState.Get().SetBusy(false);
							goto IL_A84;
						}
						goto IL_A84;
					}
					break;
				}
				break;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_A84:
		yield break;
		yield break;
	}

	// Token: 0x040041BA RID: 16826
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01.prefab:11a175183db47a2479e85d896681ace2");

	// Token: 0x040041BB RID: 16827
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02.prefab:0f5afd55abea8414ab0666ad2d554856");

	// Token: 0x040041BC RID: 16828
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03.prefab:76aeab237ec8c5249afe67a0818dfd27");

	// Token: 0x040041BD RID: 16829
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_04.prefab:4d14e36f878dc0b4d942948de1889d6e");

	// Token: 0x040041BE RID: 16830
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05.prefab:2e39f62db6c882e4f995500a61a10d05");

	// Token: 0x040041BF RID: 16831
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06.prefab:380b013abcc400940967646bf74d1b8c");

	// Token: 0x040041C0 RID: 16832
	private List<string> m_Brukan_HeroPowerLines = new List<string>
	{
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_01,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_02,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_03,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_05,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2HeroPower_06
	};

	// Token: 0x040041C1 RID: 16833
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01.prefab:76d1d6750f4df5f42b9be52198b91d26");

	// Token: 0x040041C2 RID: 16834
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_02.prefab:7a343d0c99bbcba40a35d7da20c4e63a");

	// Token: 0x040041C3 RID: 16835
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03.prefab:f3fc710cf6c0f77459668006ee89ff24");

	// Token: 0x040041C4 RID: 16836
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04.prefab:43e03a31a84886344bbddf022687ca4b");

	// Token: 0x040041C5 RID: 16837
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06.prefab:a318145d17c8edf4f8558d02cdac8bfd");

	// Token: 0x040041C6 RID: 16838
	private List<string> m_Guff_HeroPowerLines = new List<string>
	{
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_01,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_03,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_04,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3HeroPower_06
	};

	// Token: 0x040041C7 RID: 16839
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01.prefab:a087f78e1d15dd14f8a9ea9161837d12");

	// Token: 0x040041C8 RID: 16840
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02.prefab:a4b7a5b42073a5345ae27e15ec092f67");

	// Token: 0x040041C9 RID: 16841
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_03.prefab:f72faaa261994144c9e392a8b623b21c");

	// Token: 0x040041CA RID: 16842
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04.prefab:ac3868d46ef9a834c8230c4b9fa26e8c");

	// Token: 0x040041CB RID: 16843
	private List<string> m_Tamsin_HeroPowerLines = new List<string>
	{
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_01,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_02,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4HeroPower_04
	};

	// Token: 0x040041CC RID: 16844
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01.prefab:18a6357110fe5f143ace127d06ea34ba");

	// Token: 0x040041CD RID: 16845
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02.prefab:0c2f296e8512e684680879cdac1cc46b");

	// Token: 0x040041CE RID: 16846
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03.prefab:c72eda15e76cfc44fae7e09af9993f42");

	// Token: 0x040041CF RID: 16847
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04.prefab:17f4aa15f1baa8f42b4ebef24fb8c799");

	// Token: 0x040041D0 RID: 16848
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05.prefab:13846171e1eb4824288706cb62c3a910");

	// Token: 0x040041D1 RID: 16849
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06.prefab:959a3b20fe5da1f4c8675fbd0d8a6dca");

	// Token: 0x040041D2 RID: 16850
	private List<string> m_Dawngrasp_HeroPowerLines = new List<string>
	{
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_01,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_02,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_03,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_04,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_05,
		BoM_01_Rokara_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5HeroPower_06
	};

	// Token: 0x040041D3 RID: 16851
	public bool HeroPowerIsBrukan;

	// Token: 0x040041D4 RID: 16852
	public bool HeroPowerIsGuff;

	// Token: 0x040041D5 RID: 16853
	public bool HeroPowerIsTamsin;

	// Token: 0x040041D6 RID: 16854
	public bool HeroPowerIsDawngrasp;

	// Token: 0x040041D7 RID: 16855
	public readonly AssetReference Garrosh_BrassRing = new AssetReference("Garrosh_BrassRing_Quote.prefab:9c911310fb2bf7246ae78ef14a1b4dc5");

	// Token: 0x040041D8 RID: 16856
	public readonly AssetReference Brukan_BrassRing = new AssetReference("Brukan_BrassRing_Quote.prefab:16aa2801dfe06db489bd2259944af32b");

	// Token: 0x040041D9 RID: 16857
	public readonly AssetReference Guff_BrassRing = new AssetReference("Guff_BrassRing_Quote.prefab:2b02f1e9a212d7e41ace41f997923b8a");

	// Token: 0x040041DA RID: 16858
	public readonly AssetReference Rokara_B_BrassRing = new AssetReference("Rokara_B_BrassRing_Quote.prefab:301c3d7a32636944884d6fa120099950");

	// Token: 0x040041DB RID: 16859
	public readonly AssetReference Tamsin_BrassRing = new AssetReference("Tamsin_BrassRing_Quote.prefab:62964357f9958d64f9346685fc1f87f5");

	// Token: 0x040041DC RID: 16860
	public readonly AssetReference Dawngrasp_BrassRing = new AssetReference("Dawngrasp_BrassRing_Quote.prefab:45d9ad7c018bcf7429f8ff3d10e2aaf0");

	// Token: 0x040041DD RID: 16861
	public readonly AssetReference Kazakus_BrassRing = new AssetReference("Kazakus_BrassRing_Quote.prefab:74f40b18119e73f4fb7b8bc9c3f9b70f");

	// Token: 0x040041DE RID: 16862
	public const int SetHeroPowerBrukan = 58024;

	// Token: 0x040041DF RID: 16863
	public const int SetHeroPowerGuff = 58025;

	// Token: 0x040041E0 RID: 16864
	public const int SetHeroPowerTamsin = 58026;

	// Token: 0x040041E1 RID: 16865
	public const int SetHeroPowerDawngrasp = 58027;

	// Token: 0x040041E2 RID: 16866
	public const int InGame_BrukanEOT = 58028;

	// Token: 0x040041E3 RID: 16867
	public const int InGame_GuffEOT = 58029;

	// Token: 0x040041E4 RID: 16868
	public const int InGame_TamsinEOT = 58030;

	// Token: 0x040041E5 RID: 16869
	public const int InGame_DawngraspEOT = 58031;
}
