using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000572 RID: 1394
public class BOM_03_Guff_Dungeon : BOM_03_Guff_MissionEntity
{
	// Token: 0x06004D93 RID: 19859 RVA: 0x00199ED8 File Offset: 0x001980D8
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

	// Token: 0x06004D94 RID: 19860 RVA: 0x00199F54 File Offset: 0x00198154
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05,
			BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D95 RID: 19861 RVA: 0x00195570 File Offset: 0x00193770
	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOM;
	}

	// Token: 0x06004D96 RID: 19862 RVA: 0x0019A118 File Offset: 0x00198318
	public static BOM_03_Guff_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BOM_03_Guff_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BOM_03_Guff_Dungeon();
	}

	// Token: 0x06004D97 RID: 19863 RVA: 0x0019A14B File Offset: 0x0019834B
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

	// Token: 0x06004D98 RID: 19864 RVA: 0x0019A161 File Offset: 0x00198361
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		base.MissionPause(true);
		yield return this.HandleMissionEventWithTiming(514);
		base.MissionPause(false);
		yield break;
	}

	// Token: 0x06004D99 RID: 19865 RVA: 0x0019A170 File Offset: 0x00198370
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
		UnityEngine.Random.Range(0f, 1f);
		if (missionEvent <= 516)
		{
			if (missionEvent != 508)
			{
				if (missionEvent == 516)
				{
					if (this.m_SupressEnemyDeathTextBubble)
					{
						yield return base.MissionPlaySound(enemyActor, this.m_deathLine);
						goto IL_928;
					}
					yield return base.MissionPlayVO(enemyActor, this.m_deathLine);
					goto IL_928;
				}
			}
			else
			{
				if (this.HeroPowerBrukan)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Brukan_HeroPowerLines);
				}
				if (this.HeroPowerRokara)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Rokara_HeroPowerLines);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Tamsin_HeroPowerLines);
				}
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlaySound(friendlyHeroPowerActor, this.m_Dawngrasp_HeroPowerLines);
					goto IL_928;
				}
				goto IL_928;
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 600:
				this.m_Mission_EnemyHeroShouldExplodeOnDefeat = false;
				goto IL_928;
			case 601:
				this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = false;
				goto IL_928;
			case 602:
				this.m_MissionDisableAutomaticVO = true;
				goto IL_928;
			case 603:
				this.m_MissionDisableAutomaticVO = false;
				goto IL_928;
			case 604:
			case 605:
			case 606:
			case 607:
			case 608:
			case 609:
				break;
			case 610:
				this.m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
				goto IL_928;
			case 611:
				this.m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
				goto IL_928;
			case 612:
				this.m_DoEmoteDrivenStart = true;
				goto IL_928;
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
					goto IL_928;
				case 1001:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
					yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
					goto IL_928;
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
					goto IL_928;
				case 1003:
					GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
					SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
					yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
					goto IL_928;
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
						goto IL_928;
					}
					this.m_forceAlwaysPlayLine = true;
					goto IL_928;
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
					goto IL_928;
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
					goto IL_928;
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
					goto IL_928;
				}
				default:
					switch (missionEvent)
					{
					case 58023:
					{
						SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
						GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
						SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
						goto IL_928;
					}
					case 58024:
						this.HeroPowerBrukan = true;
						this.HeroPowerRokara = false;
						this.HeroPowerTamsin = false;
						this.HeroPowerDawngrasp = false;
						goto IL_928;
					case 58025:
						this.HeroPowerBrukan = false;
						this.HeroPowerRokara = true;
						this.HeroPowerTamsin = false;
						this.HeroPowerDawngrasp = false;
						goto IL_928;
					case 58026:
						this.HeroPowerBrukan = false;
						this.HeroPowerRokara = false;
						this.HeroPowerTamsin = true;
						this.HeroPowerDawngrasp = false;
						goto IL_928;
					case 58027:
						this.HeroPowerBrukan = false;
						this.HeroPowerRokara = false;
						this.HeroPowerTamsin = false;
						this.HeroPowerDawngrasp = true;
						goto IL_928;
					}
					break;
				}
				break;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_928:
		yield break;
		yield break;
	}

	// Token: 0x04004394 RID: 17300
	public readonly AssetReference Brukan_BrassRing = new AssetReference("Brukan_BrassRing_Quote.prefab:16aa2801dfe06db489bd2259944af32b");

	// Token: 0x04004395 RID: 17301
	public readonly AssetReference Rokara_B_BrassRing = new AssetReference("Rokara_B_BrassRing_Quote.prefab:301c3d7a32636944884d6fa120099950");

	// Token: 0x04004396 RID: 17302
	public readonly AssetReference Tamsin_BrassRing = new AssetReference("Tamsin_BrassRing_Quote.prefab:62964357f9958d64f9346685fc1f87f5");

	// Token: 0x04004397 RID: 17303
	public readonly AssetReference Dawngrasp_BrassRing = new AssetReference("Dawngrasp_BrassRing_Quote.prefab:45d9ad7c018bcf7429f8ff3d10e2aaf0");

	// Token: 0x04004398 RID: 17304
	public readonly AssetReference Hamuul_20_4_BrassRing_Quote = new AssetReference("Hamuul_20_4_BrassRing_Quote.prefab:54c037c90dc48994b8db6374e72f32ab");

	// Token: 0x04004399 RID: 17305
	public readonly AssetReference Naralex_BrassRing = new AssetReference("Naralex_BrassRing_Quote.prefab:6bbc6ac031d7ccf48a6e7edd7933d248");

	// Token: 0x0400439A RID: 17306
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01.prefab:e68a254459535874c93976f6f44c2612");

	// Token: 0x0400439B RID: 17307
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02.prefab:86eb7ab46c12a0f45b99589387128a14");

	// Token: 0x0400439C RID: 17308
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03.prefab:94d244024e2844648b14650966ef2b6f");

	// Token: 0x0400439D RID: 17309
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04.prefab:c675321f29a32224e816755609b3d64e");

	// Token: 0x0400439E RID: 17310
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05.prefab:27b889a954e6d444f8bd91fe7b5fb7f9");

	// Token: 0x0400439F RID: 17311
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06.prefab:3dbe08c856d9df94b8612e68e6438357");

	// Token: 0x040043A0 RID: 17312
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01.prefab:25487958973e1c44b8420788fb3ef1dd");

	// Token: 0x040043A1 RID: 17313
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02.prefab:3d9fa43f0094f3744bcd268400aa1158");

	// Token: 0x040043A2 RID: 17314
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03.prefab:7728604172e2de14194b7ce46cbf27c4");

	// Token: 0x040043A3 RID: 17315
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04.prefab:36aa36379df63984a8ce0679d1ad4d33");

	// Token: 0x040043A4 RID: 17316
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05.prefab:59a194a3be06a5c4eba09d6caad103fd");

	// Token: 0x040043A5 RID: 17317
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06.prefab:243169e212bd7464493d04a1772f4894");

	// Token: 0x040043A6 RID: 17318
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01.prefab:a9e2702de8692584b8122089162dfaca");

	// Token: 0x040043A7 RID: 17319
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02.prefab:e61c4b0290c531e4089794f1ee41bb37");

	// Token: 0x040043A8 RID: 17320
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03.prefab:1b193b2362a255c41965142cb1aa3e32");

	// Token: 0x040043A9 RID: 17321
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04.prefab:4778ed0a3df0e444a90a136bf75637d0");

	// Token: 0x040043AA RID: 17322
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05.prefab:e8345dd582c4d8a4d8fdb0b29f5594e6");

	// Token: 0x040043AB RID: 17323
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06.prefab:868f935cd5262d7478b304db1fc9097c");

	// Token: 0x040043AC RID: 17324
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02.prefab:5c59fe1390006ac47893c626407cfeb4");

	// Token: 0x040043AD RID: 17325
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04.prefab:14232e12bd04f984094b465344d47a4f");

	// Token: 0x040043AE RID: 17326
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05.prefab:d54d0e82d394b754ab824a1abf586137");

	// Token: 0x040043AF RID: 17327
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06.prefab:f5da77d66fd1f594c8bbb082353258ce");

	// Token: 0x040043B0 RID: 17328
	private List<string> m_Tamsin_HeroPowerLines = new List<string>
	{
		BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_02,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_04,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_05,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission2HeroPower_06
	};

	// Token: 0x040043B1 RID: 17329
	private List<string> m_Rokara_HeroPowerLines = new List<string>
	{
		BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_01,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_02,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_03,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_04,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_05,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission2HeroPower_06
	};

	// Token: 0x040043B2 RID: 17330
	private List<string> m_Dawngrasp_HeroPowerLines = new List<string>
	{
		BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_01,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_02,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_03,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_04,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_05,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission2HeroPower_06
	};

	// Token: 0x040043B3 RID: 17331
	private List<string> m_Brukan_HeroPowerLines = new List<string>
	{
		BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_01,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_02,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_03,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_04,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_05,
		BOM_03_Guff_Dungeon.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission2HeroPower_06
	};

	// Token: 0x040043B4 RID: 17332
	public bool HeroPowerBrukan;

	// Token: 0x040043B5 RID: 17333
	public bool HeroPowerTamsin;

	// Token: 0x040043B6 RID: 17334
	public bool HeroPowerDawngrasp;

	// Token: 0x040043B7 RID: 17335
	public bool HeroPowerRokara;

	// Token: 0x040043B8 RID: 17336
	public const int Tavish_TriggerLine = 58032;

	// Token: 0x040043B9 RID: 17337
	public const int Tavish_DeathLines = 58033;

	// Token: 0x040043BA RID: 17338
	public const int Tavish_HealLines = 58034;

	// Token: 0x040043BB RID: 17339
	public const int Tavish_IsDeadLines = 58035;

	// Token: 0x040043BC RID: 17340
	public const int Tavish_RezLines = 58036;

	// Token: 0x040043BD RID: 17341
	public const int Tavish_Attack = 58042;

	// Token: 0x040043BE RID: 17342
	public const int Scabbs_RezLines = 58037;

	// Token: 0x040043BF RID: 17343
	public const int Scabbs_DeathLines = 58038;

	// Token: 0x040043C0 RID: 17344
	public const int Scabbs_TriggerLines = 58039;

	// Token: 0x040043C1 RID: 17345
	public const int Scabbs_HealLines = 58040;

	// Token: 0x040043C2 RID: 17346
	public const int Scabbs_isDeadLines = 58041;

	// Token: 0x040043C3 RID: 17347
	public const int Scabbs_Attack = 58043;

	// Token: 0x040043C4 RID: 17348
	public bool m_Scabbs_isDead;

	// Token: 0x040043C5 RID: 17349
	public bool m_Tavish_isDead;

	// Token: 0x040043C6 RID: 17350
	public const int XyrellaCustomIdle = 58042;

	// Token: 0x040043C7 RID: 17351
	public const int SetHeroPowerBrukan = 58024;

	// Token: 0x040043C8 RID: 17352
	public const int SetHeroPowerRokara = 58025;

	// Token: 0x040043C9 RID: 17353
	public const int SetHeroPowerTamsin = 58026;

	// Token: 0x040043CA RID: 17354
	public const int SetHeroPowerDawngrasp = 58027;

	// Token: 0x040043CB RID: 17355
	public const float m_Xyrella_HP_Speaking_Chance = 0.5f;

	// Token: 0x040043CC RID: 17356
	public const float m_Xyrella_HP_Speaking_Delay = 20f;

	// Token: 0x040043CD RID: 17357
	public float m_Xyrella_HP_Seconds_Since_Action;
}
