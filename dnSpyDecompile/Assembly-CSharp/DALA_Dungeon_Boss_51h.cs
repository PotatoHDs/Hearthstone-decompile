using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000460 RID: 1120
public class DALA_Dungeon_Boss_51h : DALA_Dungeon
{
	// Token: 0x06003CC7 RID: 15559 RVA: 0x0013D1BC File Offset: 0x0013B3BC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Death_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_DefeatPlayer_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Idle_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Idle_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Idle_03,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Intro_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_IntroA_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_IntroA_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_IntroB_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x0013D450 File Offset: 0x0013B650
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05,
			DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06
		};
	}

	// Token: 0x06003CC9 RID: 15561 RVA: 0x0013D4C2 File Offset: 0x0013B6C2
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003CCA RID: 15562 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003CCB RID: 15563 RVA: 0x0013D4FA File Offset: 0x0013B6FA
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(true);
			this.m_introLinesPlaying = true;
			ZoneMgr.Get().FindZoneOfType<ZonePlay>(Player.Side.FRIENDLY).UpdateLayout();
			yield return base.PlayRandomLineAlways(DALA_Dungeon_Boss_51h.Millhouse_BrassRing_Quote, DALA_Dungeon_Boss_51h.m_MillhouseIntroA);
			GameState.Get().SetBusy(false);
			goto IL_488;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(DALA_Dungeon_Boss_51h.Millhouse_BrassRing_Quote, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_IntroB_01, 2.5f);
			GameState.Get().SetBusy(false);
			this.m_introLinesPlaying = false;
			goto IL_488;
		case 104:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_488;
		case 105:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_488;
		case 106:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_488;
		case 107:
			GameState.Get().SetBusy(true);
			yield return base.PlayRandomLineAlways(DALA_Dungeon_Boss_51h.Millhouse_BrassRing_Quote, DALA_Dungeon_Boss_51h.m_PlaysMillhouse);
			GameState.Get().SetBusy(false);
			goto IL_488;
		case 108:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(DALA_Dungeon_Boss_51h.Millhouse_BrassRing_Quote, DALA_Dungeon_Boss_51h.m_PlayerDrawMillhouse);
			goto IL_488;
		case 109:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01, 2.5f);
			goto IL_488;
		case 110:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01, 2.5f);
			goto IL_488;
		case 111:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01, 2.5f);
			goto IL_488;
		case 112:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_51h.m_PlayerMillhouseManaStorm);
			goto IL_488;
		case 113:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01, 2.5f);
			goto IL_488;
		case 114:
			GameState.Get().SetBusy(true);
			yield return base.PlayRandomLineAlways(DALA_Dungeon_Boss_51h.Millhouse_BrassRing_Quote, DALA_Dungeon_Boss_51h.m_DefeatWithMilhouseInPlay);
			GameState.Get().SetBusy(false);
			goto IL_488;
		case 115:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_488;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_488:
		yield break;
	}

	// Token: 0x06003CCC RID: 15564 RVA: 0x0013D510 File Offset: 0x0013B710
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

	// Token: 0x06003CCD RID: 15565 RVA: 0x0013D526 File Offset: 0x0013B726
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

	// Token: 0x06003CCE RID: 15566 RVA: 0x0013D53C File Offset: 0x0013B73C
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (this.m_introLinesPlaying)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string line = base.PopRandomLine(this.m_IdleLinesCopy);
		if (this.m_IdleLinesCopy.Count == 0)
		{
			this.m_IdleLinesCopy = new List<string>(DALA_Dungeon_Boss_51h.m_IdleLines);
		}
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
	}

	// Token: 0x0400269B RID: 9883
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_BossMillhouse_01.prefab:902c69955c35bf944a6cd10191920c38");

	// Token: 0x0400269C RID: 9884
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Death_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Death_02.prefab:d116280bede237d4dba75d1bf742d017");

	// Token: 0x0400269D RID: 9885
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_DefeatPlayer_01.prefab:b7b527b716e18f045979213e1f5a3da3");

	// Token: 0x0400269E RID: 9886
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_EmoteResponse_01.prefab:71cb02d9bfdcbf04bba9c66a6b3c4d1b");

	// Token: 0x0400269F RID: 9887
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_01.prefab:61529b1bf9c6da44f84826833d7f81f6");

	// Token: 0x040026A0 RID: 9888
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_02.prefab:774bf5d0eea789e41bbb2ba817774e86");

	// Token: 0x040026A1 RID: 9889
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_03.prefab:da1321e86d64a4049bb54657c1a9a484");

	// Token: 0x040026A2 RID: 9890
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_04.prefab:c7ca85e3db2e8844aac7fa22ff551b80");

	// Token: 0x040026A3 RID: 9891
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_05.prefab:cb4bdbb872d53454296851cdc648e1c3");

	// Token: 0x040026A4 RID: 9892
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_HeroPower_06.prefab:5aef9ce42c7f31a45a5ab40f45344eec");

	// Token: 0x040026A5 RID: 9893
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Idle_01.prefab:b12153bc9aaa2874a9bf9ea633a2fa07");

	// Token: 0x040026A6 RID: 9894
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Idle_02.prefab:1df5dd1fe1c55b945ba9797e11e999eb");

	// Token: 0x040026A7 RID: 9895
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Idle_03.prefab:370fdfe6c0f8d7f4c9599a43f1203c89");

	// Token: 0x040026A8 RID: 9896
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_Intro_01.prefab:35e0c050d31cc934ab0bd8ad0f36a651");

	// Token: 0x040026A9 RID: 9897
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_MillhouseFullBoard_01.prefab:8c5c335b826eeed4abe785234492cc04");

	// Token: 0x040026AA RID: 9898
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_MillhouseSecond_01.prefab:863a24787ab908944af66b322345aec0");

	// Token: 0x040026AB RID: 9899
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_MillhouseShuffle_01.prefab:43a58b271a6a1d34a99d6f48d33a8f0d");

	// Token: 0x040026AC RID: 9900
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01.prefab:cd954e939fbb7ba4e8a75069bd4137a3");

	// Token: 0x040026AD RID: 9901
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02.prefab:24693a1f0214d6a4b90143ce6bab52be");

	// Token: 0x040026AE RID: 9902
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03.prefab:ed85e2850d5fba2499cc106dfe513b7b");

	// Token: 0x040026AF RID: 9903
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastormDies_01.prefab:c6c8d4a827188c04290e8de2ada52dbd");

	// Token: 0x040026B0 RID: 9904
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_TurnOneA_01.prefab:48409dd673a24c043a1d9edda49b157f");

	// Token: 0x040026B1 RID: 9905
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_TurnOneB_01.prefab:abb42e2131175cd40b99fe83aea3c5a4");

	// Token: 0x040026B2 RID: 9906
	private static readonly AssetReference VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_51h_Female_Gnome_TurnTwo_01.prefab:cad1cbdead731ed4dbf6b8db4758e8ab");

	// Token: 0x040026B3 RID: 9907
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01.prefab:424ae2d26e9c0e8449ea1dbec73cea84");

	// Token: 0x040026B4 RID: 9908
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_02.prefab:3fcf163fdacf69f4c820550e9198ae67");

	// Token: 0x040026B5 RID: 9909
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01.prefab:be513a2b0e9972e40aae962d10f34f77");

	// Token: 0x040026B6 RID: 9910
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02.prefab:b5f65c26ea96cd749b81613e0fc1155e");

	// Token: 0x040026B7 RID: 9911
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03.prefab:bda675ba2aa888c4b87d22a0e403fae5");

	// Token: 0x040026B8 RID: 9912
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_IntroA_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_IntroA_01.prefab:b32aa81844e93e045b0194e8b703e67a");

	// Token: 0x040026B9 RID: 9913
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_IntroA_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_IntroA_02.prefab:91746117746f78049a9618a845756456");

	// Token: 0x040026BA RID: 9914
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_IntroB_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_IntroB_01.prefab:6426df8a683c16942a3e77f7cdb2d69c");

	// Token: 0x040026BB RID: 9915
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01.prefab:9b2f090f78249d949845e2525c59f0f9");

	// Token: 0x040026BC RID: 9916
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02.prefab:b8f5dc5567747f146a775ae3d080c12b");

	// Token: 0x040026BD RID: 9917
	private static readonly AssetReference VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03 = new AssetReference("VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03.prefab:68f7eaa589a430948ab215bdc3fdcd60");

	// Token: 0x040026BE RID: 9918
	private static readonly AssetReference Millhouse_BrassRing_Quote = new AssetReference("Millhouse_BrassRing_Quote.prefab:8e3a3b2cb7811ba42b6dbccaafd61fe3");

	// Token: 0x040026BF RID: 9919
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Idle_01,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Idle_02,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_Idle_03
	};

	// Token: 0x040026C0 RID: 9920
	private List<string> m_IdleLinesCopy = new List<string>(DALA_Dungeon_Boss_51h.m_IdleLines);

	// Token: 0x040026C1 RID: 9921
	private static List<string> m_PlaysMillhouse = new List<string>
	{
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_01,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_02,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_PlayMillhouse_03
	};

	// Token: 0x040026C2 RID: 9922
	private static List<string> m_MillhouseIntroA = new List<string>
	{
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_IntroA_01,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_IntroA_02
	};

	// Token: 0x040026C3 RID: 9923
	private static List<string> m_PlayerMillhouseManaStorm = new List<string>
	{
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_01,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_02,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Female_Gnome_PlayerMillhouseManastorm_03
	};

	// Token: 0x040026C4 RID: 9924
	private static List<string> m_PlayerDrawMillhouse = new List<string>
	{
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_01,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_02,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DrawMillhouse_03
	};

	// Token: 0x040026C5 RID: 9925
	private static List<string> m_DefeatWithMilhouseInPlay = new List<string>
	{
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01,
		DALA_Dungeon_Boss_51h.VO_DALA_BOSS_51h_Male_Gnome_DefeatInPlay_01
	};

	// Token: 0x040026C6 RID: 9926
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040026C7 RID: 9927
	private bool m_introLinesPlaying;
}
