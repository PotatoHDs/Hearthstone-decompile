using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000573 RID: 1395
public class BOM_03_Guff_Fight_01 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004D9D RID: 19869 RVA: 0x0019A2E8 File Offset: 0x001984E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02,
			BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01,
			BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D9E RID: 19870 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004D9F RID: 19871 RVA: 0x0019A4AF File Offset: 0x001986AF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01);
			break;
		case 101:
			yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01);
			break;
		case 102:
			yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01);
			break;
		case 103:
			yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01);
			break;
		default:
			switch (missionEvent)
			{
			case 505:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01);
				yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02);
				GameState.Get().SetBusy(false);
				goto IL_3D4;
			case 506:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_3D4;
			case 510:
				yield return base.MissionPlaySound(enemyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01);
				goto IL_3D4;
			case 514:
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01);
				goto IL_3D4;
			case 515:
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01);
				goto IL_3D4;
			case 516:
				yield return base.MissionPlaySound(enemyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01);
				goto IL_3D4;
			case 517:
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01);
				goto IL_3D4;
			}
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		IL_3D4:
		yield break;
	}

	// Token: 0x06004DA0 RID: 19872 RVA: 0x0019A4C5 File Offset: 0x001986C5
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004DA1 RID: 19873 RVA: 0x0019A4DB File Offset: 0x001986DB
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004DA2 RID: 19874 RVA: 0x0019A4F1 File Offset: 0x001986F1
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			switch (turn)
			{
			case 5:
				yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01);
				break;
			case 6:
			case 8:
				break;
			case 7:
				yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01);
				break;
			case 9:
				yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02);
				break;
			default:
				if (turn == 13)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01);
					yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02);
				}
				break;
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01);
			yield return base.MissionPlayVO(this.Hamuul_20_4_BrassRing_Quote, BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x040043CE RID: 17358
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01.prefab:386bffea3459a7547a25a673235e3578");

	// Token: 0x040043CF RID: 17359
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01.prefab:6e3c3fc142cb56343b598465cc1cf4b6");

	// Token: 0x040043D0 RID: 17360
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01.prefab:4ae2228bfa9439c4ab97658f23b54ce8");

	// Token: 0x040043D1 RID: 17361
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01.prefab:34166ad121ef7ce468e7c05a938f4d87");

	// Token: 0x040043D2 RID: 17362
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01.prefab:5a1b2de9babe078448562118b0e5c313");

	// Token: 0x040043D3 RID: 17363
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01.prefab:89c999f92557cf34a93c91c32c45fb5d");

	// Token: 0x040043D4 RID: 17364
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01.prefab:d5638acd9df669d4ab955006ce578974");

	// Token: 0x040043D5 RID: 17365
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02.prefab:c740e74c569331a43afa54efd6bffa9e");

	// Token: 0x040043D6 RID: 17366
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01.prefab:22dc5eb0ebd705a4299b19340ee03eee");

	// Token: 0x040043D7 RID: 17367
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01.prefab:1cce85f0a921c724f861e4edc1b0a41d");

	// Token: 0x040043D8 RID: 17368
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01.prefab:1c3bc5184090af4419cbebbd7ad83d23");

	// Token: 0x040043D9 RID: 17369
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02.prefab:9f08d480ec0c23447be5d7376fe71022");

	// Token: 0x040043DA RID: 17370
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01.prefab:52586fe50f2b6324fb6071c8e3de4878");

	// Token: 0x040043DB RID: 17371
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01.prefab:d77ccd44b150d0342a7b92e43b8a90d3");

	// Token: 0x040043DC RID: 17372
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01.prefab:c378aacd545101e41ab289e662418e73");

	// Token: 0x040043DD RID: 17373
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02.prefab:f0de5b7fe61e94742aab18e02e79aabc");

	// Token: 0x040043DE RID: 17374
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01.prefab:6e6ab33262971dd47832035800074100");

	// Token: 0x040043DF RID: 17375
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01.prefab:01e68067f62062a4e9aee7ca4ff559e4");

	// Token: 0x040043E0 RID: 17376
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01.prefab:8019e26487d4f40409bffee3c40b087a");

	// Token: 0x040043E1 RID: 17377
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01.prefab:db3ed0bb4a34e18489ed2592ec92b2b3");

	// Token: 0x040043E2 RID: 17378
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02.prefab:3671b0bc221dec043bbe8aeb78015a75");

	// Token: 0x040043E3 RID: 17379
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission8Victory_02.prefab:d6dbd7597ceb080499e2dcb862bc11f9");

	// Token: 0x040043E4 RID: 17380
	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_PreMission1_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_PreMission1_01.prefab:0da641ea5cc9d0d4eac18b364039c00f");

	// Token: 0x040043E5 RID: 17381
	private List<string> m_missionEventTriggerInGame_VictoryPreExplosionLines = new List<string>
	{
		BOM_03_Guff_Fight_01.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01,
		BOM_03_Guff_Fight_01.VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02
	};

	// Token: 0x040043E6 RID: 17382
	private HashSet<string> m_playedLines = new HashSet<string>();
}
