using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DF RID: 1247
public class DRGA_Good_Fight_05 : DRGA_Dungeon
{
	// Token: 0x060042D2 RID: 17106 RVA: 0x001685E0 File Offset: 0x001667E0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01,
			DRGA_Good_Fight_05.VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_PlayerStart_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042D3 RID: 17107 RVA: 0x00168804 File Offset: 0x00166A04
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01;
	}

	// Token: 0x060042D4 RID: 17108 RVA: 0x0016881C File Offset: 0x00166A1C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060042D5 RID: 17109 RVA: 0x001688AD File Offset: 0x00166AAD
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger100Lines);
			goto IL_27E;
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger101Lines);
			goto IL_27E;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01, 2.5f);
			goto IL_27E;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_05.VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01, 2.5f);
				goto IL_27E;
			}
			goto IL_27E;
		case 105:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01, 2.5f);
			goto IL_27E;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_Player_Dragon_01_01);
			goto IL_27E;
		case 110:
			this.m_Galakrond = true;
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01, 2.5f);
			goto IL_27E;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_27E:
		yield break;
	}

	// Token: 0x060042D6 RID: 17110 RVA: 0x001688C3 File Offset: 0x00166AC3
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "YOD_013"))
		{
			if (!(cardId == "DRG_235"))
			{
				if (cardId == "TRL_302")
				{
					yield return base.PlayLineOnlyOnce(actor2, DRGA_Good_Fight_05.VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060042D7 RID: 17111 RVA: 0x001688D9 File Offset: 0x00166AD9
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
		if (cardId == "ICC_027")
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060042D8 RID: 17112 RVA: 0x001688F0 File Offset: 0x00166AF0
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
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
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage <= num)
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (UnityEngine.Random.Range(1, 4))
			{
			case 1:
				emoteType = EmoteType.THINK1;
				break;
			case 2:
				emoteType = EmoteType.THINK2;
				break;
			case 3:
				emoteType = EmoteType.THINK3;
				break;
			}
			GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (!this.m_Galakrond)
		{
			string line = base.PopRandomLine(this.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy);
			if (this.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy.Count == 0)
			{
				this.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy = new List<string>(DRGA_Good_Fight_05.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrond);
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
		string line2 = base.PopRandomLine(this.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy);
		if (this.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy.Count == 0)
		{
			this.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy = new List<string>(DRGA_Good_Fight_05.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrond);
		}
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line2, 2.5f));
	}

	// Token: 0x040033A3 RID: 13219
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_BoneDrake_01.prefab:6b1c03ee04f65fa45a52bc750928972a");

	// Token: 0x040033A4 RID: 13220
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_Death_01.prefab:795fa85396a97d645915a178ddf4d9ab");

	// Token: 0x040033A5 RID: 13221
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01.prefab:f6b748b0336dafd4a8d7262400abb69a");

	// Token: 0x040033A6 RID: 13222
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01.prefab:2bfab2af5d2ab4444b01de94083c7359");

	// Token: 0x040033A7 RID: 13223
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01.prefab:41ecc41ce2f5a6440baf49cb5ea2859f");

	// Token: 0x040033A8 RID: 13224
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01.prefab:25c50c83185f8b342aac98d6633d2da2");

	// Token: 0x040033A9 RID: 13225
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01.prefab:6fe20513ca1655d41bcd5d97fe8286c0");

	// Token: 0x040033AA RID: 13226
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01.prefab:13ed3a03a6b70c74a9764c2185860d87");

	// Token: 0x040033AB RID: 13227
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossAttack_01.prefab:eaf74a16b14a52a41806c98b816a9040");

	// Token: 0x040033AC RID: 13228
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_BossStart_01.prefab:4a09dac33a99ace488a5ad2fff3c1e8b");

	// Token: 0x040033AD RID: 13229
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_EmoteResponse_01.prefab:50f7d8f4491fbe44fb9857699ca22aaa");

	// Token: 0x040033AE RID: 13230
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01.prefab:d4c16850319e8be48b0f4b6bfffbd78b");

	// Token: 0x040033AF RID: 13231
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01.prefab:fc71e710d621b744d91ff2bca3eb1c7e");

	// Token: 0x040033B0 RID: 13232
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01.prefab:e594c172775313a4d98d0cead7bf27a2");

	// Token: 0x040033B1 RID: 13233
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01.prefab:1199239b067273645804d12696ab7150");

	// Token: 0x040033B2 RID: 13234
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01.prefab:2ba5a96a6dfdc4e40bb2c07ce1ec6292");

	// Token: 0x040033B3 RID: 13235
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01.prefab:42cfddb3cc8fd764a93e0d433a237e90");

	// Token: 0x040033B4 RID: 13236
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_01_01.prefab:eda9bdca870e15547b0dfdec81f464c6");

	// Token: 0x040033B5 RID: 13237
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Misc_03_01.prefab:7220336bbc1278948a24377e9433b01b");

	// Token: 0x040033B6 RID: 13238
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_ClericOfScales_01.prefab:04ecf74a86f70654abe5752dcb6cd855");

	// Token: 0x040033B7 RID: 13239
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01.prefab:d07d6b31df27f494698be08a04c27252");

	// Token: 0x040033B8 RID: 13240
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01.prefab:9c7ca64e4201b7b4190301c5c5276d78");

	// Token: 0x040033B9 RID: 13241
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01.prefab:6ae3a7e92c0fce744abf0513e6824062");

	// Token: 0x040033BA RID: 13242
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_DragonriderTalritha_01.prefab:c42d78f52e2c59941b012a51ee69a19d");

	// Token: 0x040033BB RID: 13243
	private static readonly AssetReference VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Galakrond_01.prefab:39ad410ddd1a5b5458804cd3b7d3bd2f");

	// Token: 0x040033BC RID: 13244
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_02_01.prefab:f95d0ebd33f847f49bf4cf60158c1bf9");

	// Token: 0x040033BD RID: 13245
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_Misc_04_01.prefab:f4746670ef04a7e479c939403b16a164");

	// Token: 0x040033BE RID: 13246
	private static readonly AssetReference VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_23h_Female_Forsaken_Good_Fight_05_PlayerStart_01.prefab:eb681105b9f05cf4199771701ab33bbe");

	// Token: 0x040033BF RID: 13247
	private List<string> m_missionEventTrigger100Lines = new List<string>
	{
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_01_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_02_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonDies_03_01
	};

	// Token: 0x040033C0 RID: 13248
	private List<string> m_missionEventTrigger101Lines = new List<string>
	{
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_01_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_02_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Boss_DragonReturn_03_01
	};

	// Token: 0x040033C1 RID: 13249
	private List<string> m_Player_Dragon_01_01 = new List<string>
	{
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_01_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_02_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Player_Dragon_03_01
	};

	// Token: 0x040033C2 RID: 13250
	private static List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrond = new List<string>
	{
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03_01
	};

	// Token: 0x040033C3 RID: 13251
	private static List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrond = new List<string>
	{
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_01b_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_02b_01,
		DRGA_Good_Fight_05.VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_Idle_03b_01
	};

	// Token: 0x040033C4 RID: 13252
	private List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrondCopy = new List<string>(DRGA_Good_Fight_05.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesPostGalakrond);

	// Token: 0x040033C5 RID: 13253
	private List<string> m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrondCopy = new List<string>(DRGA_Good_Fight_05.m_VO_DRGA_BOSS_18h_Female_BloodElf_Good_Fight_05_IdleLinesNoGalakrond);

	// Token: 0x040033C6 RID: 13254
	private HashSet<string> m_playedLines = new HashSet<string>();
}
