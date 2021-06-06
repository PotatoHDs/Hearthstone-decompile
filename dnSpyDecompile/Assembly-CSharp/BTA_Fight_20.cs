using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004FE RID: 1278
public class BTA_Fight_20 : BTA_Dungeon_Heroic
{
	// Token: 0x060044CA RID: 17610 RVA: 0x001749C0 File Offset: 0x00172BC0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeathDragon_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01,
			BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_UI_Mission_Fight_20_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060044CB RID: 17611 RVA: 0x00174B64 File Offset: 0x00172D64
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_20h_IdleLines;
	}

	// Token: 0x060044CC RID: 17612 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060044CD RID: 17613 RVA: 0x00174B6C File Offset: 0x00172D6C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01;
	}

	// Token: 0x060044CE RID: 17614 RVA: 0x00174B94 File Offset: 0x00172D94
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060044CF RID: 17615 RVA: 0x00174C4B File Offset: 0x00172E4B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 115)
		{
			if (missionEvent == 110)
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01, 2.5f);
				goto IL_1D2;
			}
			if (missionEvent == 115)
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01, 2.5f);
				goto IL_1D2;
			}
		}
		else
		{
			if (missionEvent == 116)
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01, 2.5f);
				goto IL_1D2;
			}
			if (missionEvent == 500)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01, 2.5f);
				goto IL_1D2;
			}
			if (missionEvent == 507)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger507Lines);
				goto IL_1D2;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1D2:
		yield break;
	}

	// Token: 0x060044D0 RID: 17616 RVA: 0x00174C61 File Offset: 0x00172E61
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "NEW1_030") && !(cardId == "DRG_026"))
		{
			if (!(cardId == "DAL_575"))
			{
				if (cardId == "NEW1_038")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044D1 RID: 17617 RVA: 0x00174C77 File Offset: 0x00172E77
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "CS2_108")
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044D2 RID: 17618 RVA: 0x00174C8D File Offset: 0x00172E8D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x0400379D RID: 14237
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Bladestorm_01.prefab:2e3344c8e992e054cb034fa0b44d2dae");

	// Token: 0x0400379E RID: 14238
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_Attack_01.prefab:f8ebadd413bb1194fb4f39b86e07789a");

	// Token: 0x0400379F RID: 14239
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMighty_01.prefab:b03478e1287853d498904c0aa36a5396");

	// Token: 0x040037A0 RID: 14240
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Boss_SlayeroftheMightyDragon_01.prefab:ec8f114e8919257498374d3a3dcba9a8");

	// Token: 0x040037A1 RID: 14241
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeath_01.prefab:dee945938b4e0c4489484b8e73645d0a");

	// Token: 0x040037A2 RID: 14242
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeathDragon_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossDeathDragon_01.prefab:6c70dc3791edd4e4895d7038e5f19cb5");

	// Token: 0x040037A3 RID: 14243
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_BossStart_01.prefab:09a5af2cb9d66274093e0dcea7aa0d9d");

	// Token: 0x040037A4 RID: 14244
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Emote_Response_01.prefab:24b4c506085f9e04d8b997d56c444535");

	// Token: 0x040037A5 RID: 14245
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Deathwing_01.prefab:ecc9151a00d836a4fbd3dbc057c2f138");

	// Token: 0x040037A6 RID: 14246
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Dragon_01.prefab:38ad3021b7d80c749b60438a3f25383d");

	// Token: 0x040037A7 RID: 14247
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_FighT_20_Hero_Gruul_01.prefab:fb8b5d236c24c7f4282da2cdf98e6a76");

	// Token: 0x040037A8 RID: 14248
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_Hero_Khadgar_01.prefab:d7341681689f9014ab6bbe2ab3c879b2");

	// Token: 0x040037A9 RID: 14249
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01.prefab:d590a42e009e2c043b6786e78c7a23f8");

	// Token: 0x040037AA RID: 14250
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02.prefab:e794e8203efa85a4e93d557b60c60529");

	// Token: 0x040037AB RID: 14251
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03.prefab:a62807cd851fd3a4f8d2eadcb461b2dd");

	// Token: 0x040037AC RID: 14252
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04.prefab:ca379cf0d9bdb0146a9f6f764bf08009");

	// Token: 0x040037AD RID: 14253
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01.prefab:a5eb8ee200f7f14488277b951875f6c4");

	// Token: 0x040037AE RID: 14254
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01.prefab:4d5834477e914054e8207dcd2740dd83");

	// Token: 0x040037AF RID: 14255
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01.prefab:51f910f0c1881d8438c3439e5be16fb3");

	// Token: 0x040037B0 RID: 14256
	private static readonly AssetReference VO_BTA_BOSS_20h_Male_Gronn_UI_Mission_Fight_20_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_20h_Male_Gronn_UI_Mission_Fight_20_CoinSelect_01.prefab:4f01fe794b87e7546aaac1c98be049d4");

	// Token: 0x040037B1 RID: 14257
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_01,
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_02,
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_03,
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_HeroPower_04
	};

	// Token: 0x040037B2 RID: 14258
	private List<string> m_VO_BTA_BOSS_20h_IdleLines = new List<string>
	{
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleA_01,
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleB_01,
		BTA_Fight_20.VO_BTA_BOSS_20h_Male_Gronn_Mission_Fight_20_IdleC_01
	};

	// Token: 0x040037B3 RID: 14259
	private HashSet<string> m_playedLines = new HashSet<string>();
}
