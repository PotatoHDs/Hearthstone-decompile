using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class TRL_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06003A33 RID: 14899 RVA: 0x0012AADE File Offset: 0x00128CDE
	public override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.TRL;
	}

	// Token: 0x06003A34 RID: 14900 RVA: 0x0012AAE8 File Offset: 0x00128CE8
	public override void PreloadAssets()
	{
		GenericDungeonMissionEntity.VOPool value = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Generic_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(100, value);
		GenericDungeonMissionEntity.VOPool value2 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_Game1Begin_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_SHRINE_TUTORIAL_1_VO);
		this.m_VOPools.Add(700, value2);
		GenericDungeonMissionEntity.VOPool value3 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_Game2Begin_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_SHRINE_TUTORIAL_2_VO);
		this.m_VOPools.Add(701, value3);
		GenericDungeonMissionEntity.VOPool value4 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_Defeat_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO);
		this.m_VOPools.Add(709, value4);
		GenericDungeonMissionEntity.VOPool value5 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_EnemyShrineDead_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_ENEMY_SHRINE_DIES_TUTORIAL_VO);
		this.m_VOPools.Add(702, value5);
		GenericDungeonMissionEntity.VOPool value6 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_EnemyShrineReturn_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_ENEMY_SHRINE_REVIVES_TUTORIAL_VO);
		this.m_VOPools.Add(703, value6);
		GenericDungeonMissionEntity.VOPool value7 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDead_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_DIES_TUTORIAL_VO);
		this.m_VOPools.Add(704, value7);
		GenericDungeonMissionEntity.VOPool value8 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDisabled_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_TIMER_TICK_TUTORIAL_VO);
		this.m_VOPools.Add(705, value8);
		GenericDungeonMissionEntity.VOPool value9 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_ShrineDestroy_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_LOST_TUTORIAL_VO);
		this.m_VOPools.Add(706, value9);
		GenericDungeonMissionEntity.VOPool value10 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_ShrineTransform_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_TRANSFORMED_TUTORIAL_VO);
		this.m_VOPools.Add(707, value10);
		GenericDungeonMissionEntity.VOPool value11 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_HERO_02b_Male_Troll_Troll_ShrineBounce_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, TRL_MissionEntity.Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_BOUNCED_TUTORIAL_VO);
		this.m_VOPools.Add(708, value11);
		GenericDungeonMissionEntity.VOPool value12 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Returns_01,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Returns_02,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Returns_03,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Returns_04,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Returns_05
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(300, value12);
		GenericDungeonMissionEntity.VOPool value13 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_01,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_02,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_03,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_04,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_05
		}, 1f, MissionEntity.ShouldPlayValue.Always, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(500, value13);
		GenericDungeonMissionEntity.VOPool value14 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_01,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_02,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_03,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_04,
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_05
		}, 1f, MissionEntity.ShouldPlayValue.Always, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(600, value14);
		GenericDungeonMissionEntity.VOPool value15 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Druid_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(102, value15);
		GenericDungeonMissionEntity.VOPool value16 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Hunter_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(103, value16);
		GenericDungeonMissionEntity.VOPool value17 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Mage_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(104, value17);
		GenericDungeonMissionEntity.VOPool value18 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Paladin_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(105, value18);
		GenericDungeonMissionEntity.VOPool value19 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Priest_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(106, value19);
		GenericDungeonMissionEntity.VOPool value20 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Rogue_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(107, value20);
		GenericDungeonMissionEntity.VOPool value21 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Shaman_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(108, value21);
		GenericDungeonMissionEntity.VOPool value22 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Warlock_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(109, value22);
		GenericDungeonMissionEntity.VOPool value23 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Kill_Shrine_Warrior_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(110, value23);
		GenericDungeonMissionEntity.VOPool value24 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Druid_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(402, value24);
		GenericDungeonMissionEntity.VOPool value25 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Hunter_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(403, value25);
		GenericDungeonMissionEntity.VOPool value26 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Mage_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(404, value26);
		GenericDungeonMissionEntity.VOPool value27 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Paladin_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(405, value27);
		GenericDungeonMissionEntity.VOPool value28 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Priest_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(406, value28);
		GenericDungeonMissionEntity.VOPool value29 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Rogue_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(407, value29);
		GenericDungeonMissionEntity.VOPool value30 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Shaman_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(408, value30);
		GenericDungeonMissionEntity.VOPool value31 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warlock_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(409, value31);
		GenericDungeonMissionEntity.VOPool value32 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			TRL_MissionEntity.VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warrior_01
		}, 0.2f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO, "", GameSaveKeySubkeyId.INVALID);
		this.m_VOPools.Add(410, value32);
		base.PreloadAssets();
	}

	// Token: 0x06003A35 RID: 14901 RVA: 0x0012B31D File Offset: 0x0012951D
	protected override bool CanPlayVOLines(Entity speakerEntity, GenericDungeonMissionEntity.VOSpeaker speaker)
	{
		if (speaker == GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO)
		{
			return speakerEntity.GetCardId().Contains("TRLA_209h_");
		}
		return base.CanPlayVOLines(speakerEntity, speaker);
	}

	// Token: 0x06003A36 RID: 14902 RVA: 0x0012B33C File Offset: 0x0012953C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(709));
			yield break;
		}
		yield break;
	}

	// Token: 0x06003A37 RID: 14903 RVA: 0x0012B352 File Offset: 0x00129552
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_TRLMulligan);
		}
	}

	// Token: 0x06003A38 RID: 14904 RVA: 0x0012B368 File Offset: 0x00129568
	public override void StartGameplaySoundtracks()
	{
		bool flag = GameUtils.GetDefeatedBossCount() == 7;
		MusicManager.Get().StartPlaylist(flag ? MusicPlaylistType.InGame_TRLFinalBoss : MusicPlaylistType.InGame_TRLAdventure);
	}

	// Token: 0x06003A39 RID: 14905 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x04002180 RID: 8576
	private static readonly string Rastakhan_BrassRing_Quote = "Rastakhan_BrassRing_Quote.prefab:179bfad1464576448aeabfe5c3eff601";

	// Token: 0x04002181 RID: 8577
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_Game1Begin_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_Game1Begin_01.prefab:afd9eaf8855bf874a826f364c38da31b");

	// Token: 0x04002182 RID: 8578
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_Game2Begin_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_Game2Begin_01.prefab:7bde3c2ed1d9a0b4798fd276e266afff");

	// Token: 0x04002183 RID: 8579
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_Defeat_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_Defeat_01.prefab:29800eb6bdd34bc4b9cdde83840bf51e");

	// Token: 0x04002184 RID: 8580
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_EnemyShrineDead_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_EnemyShrineDead_01.prefab:69cb28e5779dc554b84be3fa4e5a880e");

	// Token: 0x04002185 RID: 8581
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_EnemyShrineReturn_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_EnemyShrineReturn_01.prefab:ccecbea6798e0584fba482201ed9032c");

	// Token: 0x04002186 RID: 8582
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDead_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDead_01.prefab:cf30dbf19d496fd4d86ef16c68ba3e96");

	// Token: 0x04002187 RID: 8583
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDisabled_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDisabled_01.prefab:be54488096969d241a24ce2331c25843");

	// Token: 0x04002188 RID: 8584
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_ShrineDestroy_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_ShrineDestroy_01.prefab:992f9169c14340649bd658c32fa1d266");

	// Token: 0x04002189 RID: 8585
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_ShrineTransform_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_ShrineTransform_01.prefab:b36edf0591d4c554f8a37ab1eb33a8cd");

	// Token: 0x0400218A RID: 8586
	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_ShrineBounce_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_ShrineBounce_01.prefab:f2213b57ae9a85642bf2a912602b3892");

	// Token: 0x0400218B RID: 8587
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_01.prefab:5b53352bae2cbee4e877af95624c6e23");

	// Token: 0x0400218C RID: 8588
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_02.prefab:f3f1211782e014d44be2266b48fe841a");

	// Token: 0x0400218D RID: 8589
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_03.prefab:d73d5740c7ae19d47af77a61ee4413cd");

	// Token: 0x0400218E RID: 8590
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_04.prefab:41039197a4433464c8d06ca949846885");

	// Token: 0x0400218F RID: 8591
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_05 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_05.prefab:8cf9bae702a69f84892ee05e1a929ddc");

	// Token: 0x04002190 RID: 8592
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Generic_01.prefab:7b09cf3c50ff5ac4198d93a4d2de9fee");

	// Token: 0x04002191 RID: 8593
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Druid_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Druid_01.prefab:12dfbd67dd429fd4b856063118fe3f36");

	// Token: 0x04002192 RID: 8594
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Hunter_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Hunter_01.prefab:0b7d821dd44e7384f88412f506cee1a3");

	// Token: 0x04002193 RID: 8595
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Mage_01.prefab:ebcc4b264896f7c4db4b844304302a50");

	// Token: 0x04002194 RID: 8596
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Paladin_01.prefab:6a2513f3d19f7bc42bdcb0b6699ac829");

	// Token: 0x04002195 RID: 8597
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Priest_01.prefab:06f5ea33962b9d542a73dd7d4663e3a3");

	// Token: 0x04002196 RID: 8598
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Rogue_01.prefab:1b81524dcbb16494ea2f3564ac4354f7");

	// Token: 0x04002197 RID: 8599
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Shaman_01.prefab:8aa3a7365444fc345bf8f5e06af2c347");

	// Token: 0x04002198 RID: 8600
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Warlock_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Warlock_01.prefab:7d1f3535a0e597946bbd822ac13c3729");

	// Token: 0x04002199 RID: 8601
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Warrior_01.prefab:59e8110726205e749b266739ecb4209c");

	// Token: 0x0400219A RID: 8602
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Druid_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Druid_01.prefab:9e50e9b615d01b1409af733af9d1faf4");

	// Token: 0x0400219B RID: 8603
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Hunter_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Hunter_01.prefab:813de6f1b0767194b81009af23b277cd");

	// Token: 0x0400219C RID: 8604
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Mage_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Mage_01.prefab:0203c3c020550aa4f876e8d72a872786");

	// Token: 0x0400219D RID: 8605
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Paladin_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Paladin_01.prefab:52a41492e9ad12e48b5083345d1f4437");

	// Token: 0x0400219E RID: 8606
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Priest_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Priest_01.prefab:ac45a45b89023bb4e812f815dc59effb");

	// Token: 0x0400219F RID: 8607
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Rogue_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Rogue_01.prefab:525db2978fc2cb9408f5d6dee20e9b8a");

	// Token: 0x040021A0 RID: 8608
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Shaman_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Shaman_01.prefab:61c3325aeb1e83a4686d9a91c407e2ba");

	// Token: 0x040021A1 RID: 8609
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warlock_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warlock_01.prefab:5feb37f49c1eda44b817db7a4355a7ae");

	// Token: 0x040021A2 RID: 8610
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warrior_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warrior_01.prefab:7f1010243156aca419753f8ca6cbf13f");

	// Token: 0x040021A3 RID: 8611
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_01.prefab:f2b32d78a5bcf6448a5c43f5d944579b");

	// Token: 0x040021A4 RID: 8612
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_02.prefab:72b5f01d510ff4d488c6179be0af9219");

	// Token: 0x040021A5 RID: 8613
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_03.prefab:580a820958714974dad73b2fc29b310e");

	// Token: 0x040021A6 RID: 8614
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_04.prefab:bfa4b0fbdea57214689961cda0257de3");

	// Token: 0x040021A7 RID: 8615
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_05 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_05.prefab:93ecacd6d46db714b94b6b5de38c233e");

	// Token: 0x040021A8 RID: 8616
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_01.prefab:7b02ae02563c9ee40b7d39ed2fbe17e9");

	// Token: 0x040021A9 RID: 8617
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_02.prefab:f3619b48393189240a8c857a6a9f4ee7");

	// Token: 0x040021AA RID: 8618
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_03.prefab:399382b0101a43143bec43b136879a83");

	// Token: 0x040021AB RID: 8619
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_04.prefab:77f7baee64e324844b1b9306ed1a42ac");

	// Token: 0x040021AC RID: 8620
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_05 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_05.prefab:26bed8961b6b7344ca6208211cb2d1bb");

	// Token: 0x02001913 RID: 6419
	public enum TRL_VOPoolType
	{
		// Token: 0x0400BBB9 RID: 48057
		INVALID,
		// Token: 0x0400BBBA RID: 48058
		KILL_SHRINE_GENERIC = 100,
		// Token: 0x0400BBBB RID: 48059
		KILL_SHRINE_DRUID = 102,
		// Token: 0x0400BBBC RID: 48060
		KILL_SHRINE_HUNTER,
		// Token: 0x0400BBBD RID: 48061
		KILL_SHRINE_MAGE,
		// Token: 0x0400BBBE RID: 48062
		KILL_SHRINE_PALADIN,
		// Token: 0x0400BBBF RID: 48063
		KILL_SHRINE_PRIEST,
		// Token: 0x0400BBC0 RID: 48064
		KILL_SHRINE_ROGUE,
		// Token: 0x0400BBC1 RID: 48065
		KILL_SHRINE_SHAMAN,
		// Token: 0x0400BBC2 RID: 48066
		KILL_SHRINE_WARLOCK,
		// Token: 0x0400BBC3 RID: 48067
		KILL_SHRINE_WARRIOR,
		// Token: 0x0400BBC4 RID: 48068
		SHRINE_KILLED = 200,
		// Token: 0x0400BBC5 RID: 48069
		SHRINE_RETURNS = 300,
		// Token: 0x0400BBC6 RID: 48070
		SHRINE_FLAVOR_DRUID = 402,
		// Token: 0x0400BBC7 RID: 48071
		SHRINE_FLAVOR_HUNTER,
		// Token: 0x0400BBC8 RID: 48072
		SHRINE_FLAVOR_MAGE,
		// Token: 0x0400BBC9 RID: 48073
		SHRINE_FLAVOR_PALADIN,
		// Token: 0x0400BBCA RID: 48074
		SHRINE_FLAVOR_PRIEST,
		// Token: 0x0400BBCB RID: 48075
		SHRINE_FLAVOR_ROGUE,
		// Token: 0x0400BBCC RID: 48076
		SHRINE_FLAVOR_SHAMAN,
		// Token: 0x0400BBCD RID: 48077
		SHRINE_FLAVOR_WARLOCK,
		// Token: 0x0400BBCE RID: 48078
		SHRINE_FLAVOR_WARRIOR,
		// Token: 0x0400BBCF RID: 48079
		NEARING_WIN = 500,
		// Token: 0x0400BBD0 RID: 48080
		NEARING_LOSS = 600,
		// Token: 0x0400BBD1 RID: 48081
		TUTORIAL_SHRINE_1 = 700,
		// Token: 0x0400BBD2 RID: 48082
		TUTORIAL_SHRINE_2,
		// Token: 0x0400BBD3 RID: 48083
		TUTORIAL_ENEMY_SHRINE_DIES,
		// Token: 0x0400BBD4 RID: 48084
		TUTORIAL_ENEMY_SHRINE_REVIVES,
		// Token: 0x0400BBD5 RID: 48085
		TUTORIAL_PLAYER_SHRINE_DIES,
		// Token: 0x0400BBD6 RID: 48086
		TUTORIAL_PLAYER_SHRINE_TIMER_TICK,
		// Token: 0x0400BBD7 RID: 48087
		TUTORIAL_PLAYER_SHRINE_LOST,
		// Token: 0x0400BBD8 RID: 48088
		TUTORIAL_PLAYER_SHRINE_TRANSFORMED,
		// Token: 0x0400BBD9 RID: 48089
		TUTORIAL_PLAYER_SHRINE_BOUNCED,
		// Token: 0x0400BBDA RID: 48090
		TUTORIAL_PLAYER_FIRST_LOST
	}
}
