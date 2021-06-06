using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200048E RID: 1166
public class ULDA_Dungeon_Boss_17h : ULDA_Dungeon
{
	// Token: 0x06003EF0 RID: 16112 RVA: 0x0014DDFC File Offset: 0x0014BFFC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Death_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_DefeatPlayer_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Idle_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Idle_02,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Idle_03,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Intro_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01,
			ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EF1 RID: 16113 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x0014DF70 File Offset: 0x0014C170
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x0014DF78 File Offset: 0x0014C178
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003EF4 RID: 16116 RVA: 0x0014DF80 File Offset: 0x0014C180
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003EF5 RID: 16117 RVA: 0x0014DFB8 File Offset: 0x0014C1B8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Elise" && cardId != "ULDA_Finley")
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

	// Token: 0x06003EF6 RID: 16118 RVA: 0x0014E070 File Offset: 0x0014C270
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003EF7 RID: 16119 RVA: 0x0014E086 File Offset: 0x0014C286
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
		if (!(cardId == "ULDA_115"))
		{
			if (cardId == "ULD_167")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003EF8 RID: 16120 RVA: 0x0014E09C File Offset: 0x0014C29C
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
		if (!(cardId == "BOT_286"))
		{
			if (cardId == "ICC_809")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B84 RID: 11140
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_BossCheatDeathTriggerLeperGnome_01.prefab:0fc92e47dd133fe4a8034a891f2e00c3");

	// Token: 0x04002B85 RID: 11141
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_BossNecriumBlade_01.prefab:c1ee367016a08d241ade51fbcf12847a");

	// Token: 0x04002B86 RID: 11142
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_BossPlagueScientist_01.prefab:4effaa1d5f8968f4b9dd315e84e79dd5");

	// Token: 0x04002B87 RID: 11143
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Death_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Death_01.prefab:9f23f72e9732ae44da916dc8509b3458");

	// Token: 0x04002B88 RID: 11144
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_DefeatPlayer_01.prefab:25b70b6297e4f2942b078d06f6d257b9");

	// Token: 0x04002B89 RID: 11145
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_EmoteResponse_01.prefab:810df3ccfc55ced408ba3c3ee15da5bc");

	// Token: 0x04002B8A RID: 11146
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01.prefab:e1d43ec3083d57e44ba87b754237405d");

	// Token: 0x04002B8B RID: 11147
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02.prefab:13c19d1da951a384d9bc42e8963f988c");

	// Token: 0x04002B8C RID: 11148
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03.prefab:8b8a6cf7ce5c9b84fa56f8f136e05d1d");

	// Token: 0x04002B8D RID: 11149
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04.prefab:19392619422ffcb4dbaa13117ea5908f");

	// Token: 0x04002B8E RID: 11150
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05.prefab:a6df2692510fb494b8abe58d97535f31");

	// Token: 0x04002B8F RID: 11151
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Idle_01.prefab:6f9522927c1b061409aa51557f222f57");

	// Token: 0x04002B90 RID: 11152
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Idle_02.prefab:ae0accc3c5ad15c44b9fb0d61cd0231c");

	// Token: 0x04002B91 RID: 11153
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Idle_03.prefab:64be5b3354f97f04cb94ebc513a9a38b");

	// Token: 0x04002B92 RID: 11154
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_Intro_01.prefab:5e307745ef7a8dc478c8b4d8ece7b43c");

	// Token: 0x04002B93 RID: 11155
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Diseased_Vulture_01.prefab:7be040567d5cb0442a8b06580c513f38");

	// Token: 0x04002B94 RID: 11156
	private static readonly AssetReference VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01 = new AssetReference("VO_ULDA_BOSS_17h_Male_Gnome_PlayerTrigger_Gnomebliterator_01.prefab:cb22f7b71be483545a93975944e57548");

	// Token: 0x04002B95 RID: 11157
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_01,
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_02,
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_03,
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_04,
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_HeroPower_05
	};

	// Token: 0x04002B96 RID: 11158
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Idle_01,
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Idle_02,
		ULDA_Dungeon_Boss_17h.VO_ULDA_BOSS_17h_Male_Gnome_Idle_03
	};

	// Token: 0x04002B97 RID: 11159
	private HashSet<string> m_playedLines = new HashSet<string>();
}
