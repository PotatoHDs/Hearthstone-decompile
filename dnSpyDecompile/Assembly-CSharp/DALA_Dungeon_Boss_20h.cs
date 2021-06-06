using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000441 RID: 1089
public class DALA_Dungeon_Boss_20h : DALA_Dungeon
{
	// Token: 0x06003B46 RID: 15174 RVA: 0x00133BDC File Offset: 0x00131DDC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_03,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossLavaBurst_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossLightningSpell_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Death_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_DefeatPlayer_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_EmoteResponse_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Idle_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Idle_02,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Idle_03,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Intro_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_OverloadPass_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_OverloadPass_02,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_OverloadPass_03,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_PlayerRainOfToads_01,
			DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_PlayerShamanSpell_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B47 RID: 15175 RVA: 0x00133D70 File Offset: 0x00131F70
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_EmoteResponse_01;
	}

	// Token: 0x06003B48 RID: 15176 RVA: 0x00133DA8 File Offset: 0x00131FA8
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_20h.m_IdleLines;
	}

	// Token: 0x06003B49 RID: 15177 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x00133DB0 File Offset: 0x00131FB0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Vessina")
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

	// Token: 0x06003B4B RID: 15179 RVA: 0x00133E5B File Offset: 0x0013205B
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_20h.m_HeropowerTrigger);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_20h.m_BossClearOverload);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_20h.m_OverloadPass);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_PlayerShamanSpell_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003B4C RID: 15180 RVA: 0x00133E71 File Offset: 0x00132071
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "TRL_351")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_PlayerRainOfToads_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x00133E87 File Offset: 0x00132087
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "EX1_241"))
		{
			if (cardId == "BOT_246" || cardId == "EX1_238" || cardId == "EX1_251" || cardId == "EX1_259" || cardId == "GIL_600")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossLightningSpell_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossLavaBurst_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040023D4 RID: 9172
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_01.prefab:3870ef7ed8015664b9221cc70a5aaf45");

	// Token: 0x040023D5 RID: 9173
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_03 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_03.prefab:6707d7fba33bf6f409f500aab44ed52c");

	// Token: 0x040023D6 RID: 9174
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_BossLavaBurst_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_BossLavaBurst_01.prefab:0be790314f82fb1479612f57da169bd2");

	// Token: 0x040023D7 RID: 9175
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_BossLightningSpell_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_BossLightningSpell_01.prefab:ef2ff2d39dcdaae41b8d29d5f5e0dc12");

	// Token: 0x040023D8 RID: 9176
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_Death_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_Death_01.prefab:424af123c04fbaf46b1091ddd942c1c8");

	// Token: 0x040023D9 RID: 9177
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_DefeatPlayer_01.prefab:c40f584115e7c79448c12e02f6034338");

	// Token: 0x040023DA RID: 9178
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_EmoteResponse_01.prefab:ab116ca948492c64097a6e485fbc4d4d");

	// Token: 0x040023DB RID: 9179
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_01.prefab:10e14eec3cbd6ee43b1c5fd7af9772d3");

	// Token: 0x040023DC RID: 9180
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_02.prefab:a4abb8934bf564145ac03e6c8715921d");

	// Token: 0x040023DD RID: 9181
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_03.prefab:6c4c62f57b38b84409743bf02d3607cf");

	// Token: 0x040023DE RID: 9182
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_Idle_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_Idle_01.prefab:46a57a908826f11458c04e1006b8e87b");

	// Token: 0x040023DF RID: 9183
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_Idle_02 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_Idle_02.prefab:144dbf8c61c7e4840b036835f94283ab");

	// Token: 0x040023E0 RID: 9184
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_Idle_03 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_Idle_03.prefab:c4aa34caca890e34c84592b18e262e66");

	// Token: 0x040023E1 RID: 9185
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_Intro_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_Intro_01.prefab:ed29208739cf12c459e1b60bda01b0ab");

	// Token: 0x040023E2 RID: 9186
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_OverloadPass_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_OverloadPass_01.prefab:9d5ab9e0eb86ad6458d6b1fdd721e9fc");

	// Token: 0x040023E3 RID: 9187
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_OverloadPass_02 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_OverloadPass_02.prefab:c1ffe8083fd219a44a4cfb51aa80dfd1");

	// Token: 0x040023E4 RID: 9188
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_OverloadPass_03 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_OverloadPass_03.prefab:bf68200fba0024a408642e1dc4f5b10a");

	// Token: 0x040023E5 RID: 9189
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_PlayerRainOfToads_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_PlayerRainOfToads_01.prefab:c53f52cb79e63a14eb869bde05cdb48d");

	// Token: 0x040023E6 RID: 9190
	private static readonly AssetReference VO_DALA_BOSS_20h_Male_Troll_PlayerShamanSpell_01 = new AssetReference("VO_DALA_BOSS_20h_Male_Troll_PlayerShamanSpell_01.prefab:af4bd121c9205ee4eb6ec09f913dba2d");

	// Token: 0x040023E7 RID: 9191
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Idle_01,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Idle_02,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_Idle_03
	};

	// Token: 0x040023E8 RID: 9192
	private static List<string> m_OverloadPass = new List<string>
	{
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_OverloadPass_01,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_OverloadPass_02,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_OverloadPass_03
	};

	// Token: 0x040023E9 RID: 9193
	private static List<string> m_BossClearOverload = new List<string>
	{
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_01,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_BossClearOverload_03
	};

	// Token: 0x040023EA RID: 9194
	private static List<string> m_HeropowerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_20h.VO_DALA_BOSS_20h_Male_Troll_HeroPowerTrigger_03
	};

	// Token: 0x040023EB RID: 9195
	private HashSet<string> m_playedLines = new HashSet<string>();
}
