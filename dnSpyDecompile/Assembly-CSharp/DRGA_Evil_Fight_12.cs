using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004DA RID: 1242
public class DRGA_Evil_Fight_12 : DRGA_Dungeon
{
	// Token: 0x0600428C RID: 17036 RVA: 0x00166258 File Offset: 0x00164458
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Greetings_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Oops_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Thanks_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Threaten_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Wow_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01,
			DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01,
			DRGA_Evil_Fight_12.VO_DRG_650_Male_Dragon_Start_01,
			DRGA_Evil_Fight_12.VO_DRG_610_Male_Dragon_Threaten_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600428D RID: 17037 RVA: 0x0016646C File Offset: 0x0016466C
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGEVILBoss);
	}

	// Token: 0x0600428E RID: 17038 RVA: 0x0016647E File Offset: 0x0016467E
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_IdleLines;
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x00166486 File Offset: 0x00164686
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPowerLines;
	}

	// Token: 0x06004290 RID: 17040 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004291 RID: 17041 RVA: 0x0016648E File Offset: 0x0016468E
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01;
	}

	// Token: 0x06004292 RID: 17042 RVA: 0x001664A8 File Offset: 0x001646A8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (!this.m_Heroic)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_12.VO_DRG_610_Male_Dragon_Threaten_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x00166542 File Offset: 0x00164742
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPowerLines);
			goto IL_3BD;
		case 102:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01, 2.5f);
				goto IL_3BD;
			}
			goto IL_3BD;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(friendlyActor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01, 2.5f);
				goto IL_3BD;
			}
			goto IL_3BD;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01, 2.5f);
				goto IL_3BD;
			}
			goto IL_3BD;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01, 2.5f);
			goto IL_3BD;
		case 108:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPowerLines);
			goto IL_3BD;
		case 109:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPowerLines);
			goto IL_3BD;
		case 110:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01, 2.5f);
				goto IL_3BD;
			}
			goto IL_3BD;
		case 113:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_12.VO_DRG_650_Male_Dragon_Start_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_3BD;
		case 114:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01, 2.5f);
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01, 2.5f);
				goto IL_3BD;
			}
			goto IL_3BD;
		case 115:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_VO_DRGA_BOSS_07h_Male_Ethereal_UntoldSplendorLines);
			goto IL_3BD;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3BD:
		yield break;
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x00166558 File Offset: 0x00164758
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "DALA_708" && !this.m_Heroic)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_07h_Male_Ethereal_UntoldSplendorLines);
		}
		yield break;
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x0016656E File Offset: 0x0016476E
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
		if (cardId == "LOEA_01" || cardId == "LOEA_01H")
		{
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_12.VO_DRG_650_Male_Dragon_Start_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040032D7 RID: 13015
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Greetings_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Greetings_01.prefab:63623827e70758e4bb3536bb68291f07");

	// Token: 0x040032D8 RID: 13016
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Oops_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Oops_01.prefab:08333d528d7a5654eab207c998d9f1cc");

	// Token: 0x040032D9 RID: 13017
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Thanks_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Thanks_01.prefab:7f7b4729e415f14439a2b96008a03d8d");

	// Token: 0x040032DA RID: 13018
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Threaten_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Threaten_01.prefab:e7b35bdec0fdb8146b8f4fa9c8697908");

	// Token: 0x040032DB RID: 13019
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Wow_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Hero_Emote_Wow_01.prefab:5250b32a80e11534fa977024dccc8e2d");

	// Token: 0x040032DC RID: 13020
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01.prefab:4cca2f37ba1588840abe59fac3629346");

	// Token: 0x040032DD RID: 13021
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01.prefab:c507a04b02b7d444db5989aed139b1ff");

	// Token: 0x040032DE RID: 13022
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01.prefab:6aca3eda17845e748a986c328af5ac55");

	// Token: 0x040032DF RID: 13023
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01.prefab:d38517d0b88b9424cbbd7c772518becb");

	// Token: 0x040032E0 RID: 13024
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01.prefab:4f3928959ac118f4eb290f184ba3f3fc");

	// Token: 0x040032E1 RID: 13025
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_01_01.prefab:435c57689933a204bb80c1d05e16c534");

	// Token: 0x040032E2 RID: 13026
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_02_01.prefab:bc74d8739dcec1b4897587a410149bd2");

	// Token: 0x040032E3 RID: 13027
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_Misc_03_01.prefab:42dcde5183d62ab4989325e518183aea");

	// Token: 0x040032E4 RID: 13028
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_01.prefab:afa209d3b4dfa184ba08b7e03a91b129");

	// Token: 0x040032E5 RID: 13029
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_PlayerStart_b_01.prefab:9d3d1c803c607264f88b5c71e5b7f3bb");

	// Token: 0x040032E6 RID: 13030
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_Death_01.prefab:1293dd73c1fe811458d88cb13114eacd");

	// Token: 0x040032E7 RID: 13031
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01.prefab:a60ff4ac653d29d4aaa283131c7424e3");

	// Token: 0x040032E8 RID: 13032
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01.prefab:76009e7b08ac2414a885b15b03901408");

	// Token: 0x040032E9 RID: 13033
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01.prefab:c107dd155cf1fb2409f9bc203d836dd7");

	// Token: 0x040032EA RID: 13034
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossAttack_01.prefab:34902d6a4ea52644790e091f44a007b4");

	// Token: 0x040032EB RID: 13035
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01.prefab:3ef89842ff6bafa44b6f5eee23690dfd");

	// Token: 0x040032EC RID: 13036
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_EmoteResponse_01.prefab:7a420cb2b253bb147a5d49fd4470f499");

	// Token: 0x040032ED RID: 13037
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01.prefab:e40782b698eb1fb49abc18fc5b1b4eca");

	// Token: 0x040032EE RID: 13038
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01.prefab:c1442324e17668d4abfbd46d6eed287a");

	// Token: 0x040032EF RID: 13039
	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01.prefab:0bb9093215198014fb9f69a097a6234a");

	// Token: 0x040032F0 RID: 13040
	private static readonly AssetReference VO_DRG_650_Male_Dragon_Start_01 = new AssetReference("VO_DRG_650_Male_Dragon_Start_01.prefab:c25bea315252ae848acf738d59eb7f87");

	// Token: 0x040032F1 RID: 13041
	private static readonly AssetReference VO_DRG_610_Male_Dragon_Threaten_01 = new AssetReference("VO_DRG_610_Male_Dragon_Threaten_01.prefab:7c5e7f29cef55ea489b0393e9fb5a27d");

	// Token: 0x040032F2 RID: 13042
	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_01_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_03_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_04_01
	};

	// Token: 0x040032F3 RID: 13043
	private List<string> m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_01_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_02_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Boss_HeroPower_03_01
	};

	// Token: 0x040032F4 RID: 13044
	private List<string> m_VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01
	};

	// Token: 0x040032F5 RID: 13045
	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_UntoldSplendorLines = new List<string>
	{
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_02_01,
		DRGA_Evil_Fight_12.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_12_HeroPower_FirstTime_01
	};

	// Token: 0x040032F6 RID: 13046
	private HashSet<string> m_playedLines = new HashSet<string>();
}
