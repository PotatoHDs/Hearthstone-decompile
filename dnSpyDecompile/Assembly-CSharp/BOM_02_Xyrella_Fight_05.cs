using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200056D RID: 1389
public class BOM_02_Xyrella_Fight_05 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004D13 RID: 19731 RVA: 0x00197E94 File Offset: 0x00196094
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01,
			BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02,
			BOM_02_Xyrella_Fight_05.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D14 RID: 19732 RVA: 0x00198068 File Offset: 0x00196268
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGameBossIdleLines;
	}

	// Token: 0x06004D15 RID: 19733 RVA: 0x00198070 File Offset: 0x00196270
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004D16 RID: 19734 RVA: 0x00198078 File Offset: 0x00196278
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BOT;
		base.OnCreateGame();
	}

	// Token: 0x06004D17 RID: 19735 RVA: 0x0019808B File Offset: 0x0019628B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 505)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01);
					break;
				case 517:
					yield return base.MissionPlayVO(enemyActor, this.m_InGameBossIdleLines);
					break;
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(this.Tavish_BrassRing, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004D18 RID: 19736 RVA: 0x001980A1 File Offset: 0x001962A1
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

	// Token: 0x06004D19 RID: 19737 RVA: 0x001980B7 File Offset: 0x001962B7
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

	// Token: 0x06004D1A RID: 19738 RVA: 0x001980CD File Offset: 0x001962CD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04);
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02);
			}
		}
		else if (turn != 13)
		{
			if (turn == 17)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(this.Tavish_BrassRing, BOM_02_Xyrella_Fight_05.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01);
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02);
		}
		yield break;
	}

	// Token: 0x040042DA RID: 17114
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission5Victory_01.prefab:5cb01fd878f58254094c172c34c20a5e");

	// Token: 0x040042DB RID: 17115
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Death_01.prefab:d510827255cd01d4bb98a1546c7a52d3");

	// Token: 0x040042DC RID: 17116
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5EmoteResponse_01.prefab:6932cac834076ae41a6ea20ad360ba40");

	// Token: 0x040042DD RID: 17117
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeA_01.prefab:c380c6e983f083b448d8ea298b5a27ea");

	// Token: 0x040042DE RID: 17118
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeB_04.prefab:a04e1c633b15ce9428cd2c1face02304");

	// Token: 0x040042DF RID: 17119
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeC_02.prefab:d6780a350f3fabd439e6fb5a2fa8a464");

	// Token: 0x040042E0 RID: 17120
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5ExchangeD_01.prefab:1f58765e81ee4f744bd32a63099fb6c7");

	// Token: 0x040042E1 RID: 17121
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01.prefab:0de7906fec741ea488d4d36b01862c45");

	// Token: 0x040042E2 RID: 17122
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02.prefab:a90438929cfcded4d85726c665da6d57");

	// Token: 0x040042E3 RID: 17123
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03.prefab:0f6c3ac3ac678d8459d6f199b69ee9ea");

	// Token: 0x040042E4 RID: 17124
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01.prefab:c60bda170d386dc43ad3f5710e2defd3");

	// Token: 0x040042E5 RID: 17125
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02.prefab:9cf5237501fd07345b1a766c3ab751f2");

	// Token: 0x040042E6 RID: 17126
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03.prefab:95d19b58a884a9249bf2946e6cc3a473");

	// Token: 0x040042E7 RID: 17127
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Intro_02.prefab:e598517e555b83e4092d2d996cf9d50f");

	// Token: 0x040042E8 RID: 17128
	private static readonly AssetReference VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Loss_01.prefab:03c0e7a1d0a3f654ebf2adfc8b4dc8f3");

	// Token: 0x040042E9 RID: 17129
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeA_02.prefab:fbfc4fa99208be440962e10bd592ac5e");

	// Token: 0x040042EA RID: 17130
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_01.prefab:4b7acc29e927e9a4d8bf496ccdfeb189");

	// Token: 0x040042EB RID: 17131
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_02.prefab:213536285a7d20442b4d3f3350dace87");

	// Token: 0x040042EC RID: 17132
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeB_03.prefab:cd01ba250041c1241bc408c35c0d6b73");

	// Token: 0x040042ED RID: 17133
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5ExchangeD_02.prefab:69a736ed759eaec41af9d6ca9d53f7e4");

	// Token: 0x040042EE RID: 17134
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Intro_01.prefab:468c06e51078f81408f3dad5b93147fd");

	// Token: 0x040042EF RID: 17135
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission5Victory_02.prefab:07e8412050bbc344abdf35c50ddc2332");

	// Token: 0x040042F0 RID: 17136
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission5ExchangeC_01.prefab:6b942914f84dfea42b371cde18b5ec62");

	// Token: 0x040042F1 RID: 17137
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_01,
		BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_02,
		BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5HeroPower_03
	};

	// Token: 0x040042F2 RID: 17138
	private List<string> m_InGameBossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_01,
		BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_02,
		BOM_02_Xyrella_Fight_05.VO_Story_Hero_Trixie_Female_Goblin_Story_Xyrella_Mission5Idle_03
	};

	// Token: 0x040042F3 RID: 17139
	private HashSet<string> m_playedLines = new HashSet<string>();
}
