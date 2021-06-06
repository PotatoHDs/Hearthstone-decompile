using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A0 RID: 1184
public class ULDA_Dungeon_Boss_35h : ULDA_Dungeon
{
	// Token: 0x06003FD4 RID: 16340 RVA: 0x00152840 File Offset: 0x00150A40
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_DefeatPlayer_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01,
			ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FD5 RID: 16341 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003FD6 RID: 16342 RVA: 0x001529D4 File Offset: 0x00150BD4
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FD7 RID: 16343 RVA: 0x001529DC File Offset: 0x00150BDC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003FD8 RID: 16344 RVA: 0x001529E4 File Offset: 0x00150BE4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01;
	}

	// Token: 0x06003FD9 RID: 16345 RVA: 0x00152A1C File Offset: 0x00150C1C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Reno" && cardId != "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003FDA RID: 16346 RVA: 0x00152AD4 File Offset: 0x00150CD4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003FDB RID: 16347 RVA: 0x00152AEA File Offset: 0x00150CEA
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
		if (!(cardId == "NEW1_010"))
		{
			if (!(cardId == "LOE_020") && !(cardId == "ULD_182"))
			{
				if (cardId == "ICC_201")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003FDC RID: 16348 RVA: 0x00152B00 File Offset: 0x00150D00
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
		if (!(cardId == "ULD_275"))
		{
			if (!(cardId == "ULD_181"))
			{
				if (cardId == "BRM_011")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002CD3 RID: 11475
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_BossBoneWraith_01.prefab:efefe1e6ba6cdad43968953f3a5fd9c1");

	// Token: 0x04002CD4 RID: 11476
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_BossEarthquake_01.prefab:d9ad5d85626b4d74ea0617a1a4572cfc");

	// Token: 0x04002CD5 RID: 11477
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_BossLavaShock_01.prefab:b7ec166690a7ab948898bf70e4ec89a1");

	// Token: 0x04002CD6 RID: 11478
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Death_01.prefab:45bfeb4623a61db4bbaaf7ebe29bd753");

	// Token: 0x04002CD7 RID: 11479
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_DefeatPlayer_01.prefab:0e750a0c209070c4b890997d3dbae173");

	// Token: 0x04002CD8 RID: 11480
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_EmoteResponse_01.prefab:8837d6f82d9ecb84dacddf76ea1890a6");

	// Token: 0x04002CD9 RID: 11481
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01.prefab:c1ec17c9051f45941853200dcc24db77");

	// Token: 0x04002CDA RID: 11482
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02.prefab:1d3f70dd36eeb884988eedcc4e2cc8e5");

	// Token: 0x04002CDB RID: 11483
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03.prefab:8b03b5b72de0d514ab2b127dfcd91156");

	// Token: 0x04002CDC RID: 11484
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04.prefab:89f57723c2679fd4683df3686b335201");

	// Token: 0x04002CDD RID: 11485
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01.prefab:8273e9e21c1ce5a489bf52716577048b");

	// Token: 0x04002CDE RID: 11486
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01.prefab:c3ae169a36043894783b8ddaece563c4");

	// Token: 0x04002CDF RID: 11487
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02.prefab:013a2830b46c0654a9ec36a987574cad");

	// Token: 0x04002CE0 RID: 11488
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01.prefab:104d12a7571c8f14899ef769d83ebb3b");

	// Token: 0x04002CE1 RID: 11489
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Intro_01.prefab:69e8dbb2a1090d0468b7c2f5692f420a");

	// Token: 0x04002CE2 RID: 11490
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_Misc_01.prefab:a49e3b90561852f438676adb6c76713a");

	// Token: 0x04002CE3 RID: 11491
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerAlAkir_01.prefab:fa56605e9c15a3c47834ca318eda3a2c");

	// Token: 0x04002CE4 RID: 11492
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerCamel_01.prefab:1d335fd6adf9ed240b6b106f58dd4954");

	// Token: 0x04002CE5 RID: 11493
	private static readonly AssetReference VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01 = new AssetReference("VO_ULDA_BOSS_35h_Male_BoneWraith_PlayerRolltheBones_01.prefab:c201eb6b42bbbf2479b0b8f4e2ac6259");

	// Token: 0x04002CE6 RID: 11494
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_01,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_02,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_03,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPower_04,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_HeroPowerRare_01
	};

	// Token: 0x04002CE7 RID: 11495
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_01,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_Idle_02,
		ULDA_Dungeon_Boss_35h.VO_ULDA_BOSS_35h_Male_BoneWraith_IdleRare_01
	};

	// Token: 0x04002CE8 RID: 11496
	private HashSet<string> m_playedLines = new HashSet<string>();
}
