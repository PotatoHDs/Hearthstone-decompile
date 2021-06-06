using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E2 RID: 1250
public class DRGA_Good_Fight_08 : DRGA_Dungeon
{
	// Token: 0x060042F8 RID: 17144 RVA: 0x001698A8 File Offset: 0x00167AA8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_08.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_08_Victory_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round1_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round2_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DragonslayerGreatbow_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DrawCard_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Heartseeker_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_01_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_02_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_03_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossAttack_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossStart_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_EmoteResponse_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_01_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_02_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_03_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_01_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_02_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_03_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_04_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_05_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_06_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_DragonHero_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Gruul_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_NemsyHero_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_RenoHero_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Toki_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_07_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_08_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_09_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_10_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_11_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_12_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_13_01,
			DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_14_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x00169B2C File Offset: 0x00167D2C
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_IdleLines;
	}

	// Token: 0x060042FA RID: 17146 RVA: 0x00169B34 File Offset: 0x00167D34
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPowerLines;
	}

	// Token: 0x060042FB RID: 17147 RVA: 0x00169B3C File Offset: 0x00167D3C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round1_01;
	}

	// Token: 0x060042FC RID: 17148 RVA: 0x00169B54 File Offset: 0x00167D54
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_01c")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_DragonHero_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "NEW1_038")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Gruul_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "HERO_07a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_NemsyHero_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "YOD_009")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_RenoHero_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Toki_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x060042FD RID: 17149 RVA: 0x00169D29 File Offset: 0x00167F29
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_01_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 101:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_02_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 102:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_03_01, 2.5f);
			goto IL_554;
		case 103:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_04_01, 2.5f);
			goto IL_554;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_05_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_06_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_07_01, 2.5f);
				this.m_deathLine = DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round2_01;
				goto IL_554;
			}
			goto IL_554;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor2, this.m_ChromieTransform);
				goto IL_554;
			}
			goto IL_554;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(actor2, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Heartseeker_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 110:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_11_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 111:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_12_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 112:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_13_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 113:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_14_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		case 114:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round1_01, 2.5f);
			goto IL_554;
		case 115:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round2_01, 2.5f);
			goto IL_554;
		case 116:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DrawCard_01, 2.5f);
			goto IL_554;
		case 117:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossAttack_01, 2.5f);
			goto IL_554;
		case 118:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, DRGA_Good_Fight_08.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_08_Victory_01, 2.5f);
				goto IL_554;
			}
			goto IL_554;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_554:
		yield break;
	}

	// Token: 0x060042FE RID: 17150 RVA: 0x00169D3F File Offset: 0x00167F3F
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
		if (cardId == "NEW1_038")
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Gruul_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060042FF RID: 17151 RVA: 0x00169D55 File Offset: 0x00167F55
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
		if (cardId == "DRGA_BOSS_22t" || cardId == "DRGA_BOSS_22t4")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossEquipDragonslayer);
		}
		yield break;
	}

	// Token: 0x04003405 RID: 13317
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_08_Victory_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_08_Victory_01.prefab:4592b8df0e5dc8e4181c0e1cd9a908cb");

	// Token: 0x04003406 RID: 13318
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round1_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round1_01.prefab:922408d89638efe41b99343b9d945603");

	// Token: 0x04003407 RID: 13319
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round2_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Death_Round2_01.prefab:9aa0f0c617cc3894d80673068d200190");

	// Token: 0x04003408 RID: 13320
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DragonslayerGreatbow_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DragonslayerGreatbow_01.prefab:40b22d6704363aa44a961b846ef6c468");

	// Token: 0x04003409 RID: 13321
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DrawCard_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DrawCard_01.prefab:49a7684d76f21994ba5d18c554dca495");

	// Token: 0x0400340A RID: 13322
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Heartseeker_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_Heartseeker_01.prefab:e80eefdf2e7a5f941948d58dfb2c91cd");

	// Token: 0x0400340B RID: 13323
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_01_01.prefab:97fc0167fd11ba6448e3077524996078");

	// Token: 0x0400340C RID: 13324
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_02_01.prefab:7c1c3ffc262648e459011cf156741014");

	// Token: 0x0400340D RID: 13325
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_03_01.prefab:36e7bbf9fc01e9f4b8539a2c85ab7bbc");

	// Token: 0x0400340E RID: 13326
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossAttack_01.prefab:dacfac7175aaa374691c9226827b6e98");

	// Token: 0x0400340F RID: 13327
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossStart_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_BossStart_01.prefab:b6df9f87a8ab3424e916d504b2cf9a70");

	// Token: 0x04003410 RID: 13328
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_EmoteResponse_01.prefab:24d104072c731834c90765784aa9ac13");

	// Token: 0x04003411 RID: 13329
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_01_01.prefab:b1acd087bba2a1f4884af74f8a3c824f");

	// Token: 0x04003412 RID: 13330
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_02_01.prefab:a9ba6eadaf6324641bccd665106cd2a6");

	// Token: 0x04003413 RID: 13331
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_03_01.prefab:7fec6c5f112ee884fac6232913ca710c");

	// Token: 0x04003414 RID: 13332
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_01_01.prefab:0ae0effc3066b9747b0943922a00fbd1");

	// Token: 0x04003415 RID: 13333
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_02_01.prefab:ad216649b44a83044966b6f92ca90897");

	// Token: 0x04003416 RID: 13334
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_03_01.prefab:3e5364c9c1c270840afd6313122267bb");

	// Token: 0x04003417 RID: 13335
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_04_01.prefab:cba75782ea131674c9442e103d8f4940");

	// Token: 0x04003418 RID: 13336
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_05_01.prefab:c1befb2ed5fb9f0479558efebff953d7");

	// Token: 0x04003419 RID: 13337
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_06_01.prefab:6c5a3b1050e63154cbed4c2a568b4d3e");

	// Token: 0x0400341A RID: 13338
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_DragonHero_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_DragonHero_01.prefab:af8e516d6757b0043884fa58cace2d9d");

	// Token: 0x0400341B RID: 13339
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Gruul_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Gruul_01.prefab:cd4fc6d5b2201454295a889928358ef8");

	// Token: 0x0400341C RID: 13340
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_NemsyHero_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_NemsyHero_01.prefab:a1ded04d3fec7b643af39c01e78af54c");

	// Token: 0x0400341D RID: 13341
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_RenoHero_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_RenoHero_01.prefab:71e57e35373546646913400f0485598a");

	// Token: 0x0400341E RID: 13342
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Toki_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Player_Toki_01.prefab:3695445bfdba2d4438974859bc85b2d8");

	// Token: 0x0400341F RID: 13343
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_07_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_07_01.prefab:285cea29098d3df49b44a2d76fe878fc");

	// Token: 0x04003420 RID: 13344
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_08_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_08_01.prefab:73b28feb6dad5eb40a5f955e40f52479");

	// Token: 0x04003421 RID: 13345
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_09_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_09_01.prefab:416bb4f5279714d49bb09b5ecfbdad21");

	// Token: 0x04003422 RID: 13346
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_10_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_10_01.prefab:0b823edf48a6ec7498aeccb5b00443e9");

	// Token: 0x04003423 RID: 13347
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_11_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_11_01.prefab:1ea2737b4b9337f45b1226f40fe27567");

	// Token: 0x04003424 RID: 13348
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_12_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_12_01.prefab:d85b347383555074c90426cd37056612");

	// Token: 0x04003425 RID: 13349
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_13_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_13_01.prefab:7d513f8eda27c994a9dc733dac53066e");

	// Token: 0x04003426 RID: 13350
	private static readonly AssetReference VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_14_01 = new AssetReference("VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_14_01.prefab:6183ebefc40fd9941997dae74d81310e");

	// Token: 0x04003427 RID: 13351
	private List<string> m_VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_01_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_02_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_HeroPower_03_01
	};

	// Token: 0x04003428 RID: 13352
	private List<string> m_VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_IdleLines = new List<string>
	{
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_01_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_02_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Idle_03_01
	};

	// Token: 0x04003429 RID: 13353
	private List<string> m_ChromieTransform = new List<string>
	{
		DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_08_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_09_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_36h_Female_Gnome_Good_Fight_08_Misc_10_01
	};

	// Token: 0x0400342A RID: 13354
	private List<string> m_BossEquipDragonslayer = new List<string>
	{
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Misc_05_01,
		DRGA_Good_Fight_08.VO_DRGA_BOSS_22h_Male_Gronn_Good_Fight_08_Boss_DragonslayerGreatbow_01
	};

	// Token: 0x0400342B RID: 13355
	private HashSet<string> m_playedLines = new HashSet<string>();
}
