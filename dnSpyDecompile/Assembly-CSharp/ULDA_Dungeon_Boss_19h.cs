using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000490 RID: 1168
public class ULDA_Dungeon_Boss_19h : ULDA_Dungeon
{
	// Token: 0x06003F0C RID: 16140 RVA: 0x0014E70C File Offset: 0x0014C90C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_DefeatPlayer_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01,
			ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F0D RID: 16141 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F0E RID: 16142 RVA: 0x0014E890 File Offset: 0x0014CA90
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F0F RID: 16143 RVA: 0x0014E898 File Offset: 0x0014CA98
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F10 RID: 16144 RVA: 0x0014E8A0 File Offset: 0x0014CAA0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01;
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x0014E8D8 File Offset: 0x0014CAD8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06003F12 RID: 16146 RVA: 0x0014E9B2 File Offset: 0x0014CBB2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003F13 RID: 16147 RVA: 0x0014E9C8 File Offset: 0x0014CBC8
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
		if (!(cardId == "ULD_721"))
		{
			if (cardId == "ULDA_208")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F14 RID: 16148 RVA: 0x0014E9DE File Offset: 0x0014CBDE
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
		if (!(cardId == "CS2_108"))
		{
			if (cardId == "ULD_256")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002BAC RID: 11180
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerExecute_01.prefab:cebf1bb739a6c764e9cd48d1bfcf141c");

	// Token: 0x04002BAD RID: 11181
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_BossTriggerReinforcements_01.prefab:66c6c292af112c549a6316cbd04cbd2e");

	// Token: 0x04002BAE RID: 11182
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Death_01.prefab:32e580ea611e5894f85d6128723b7ae7");

	// Token: 0x04002BAF RID: 11183
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_DefeatPlayer_01.prefab:1a06f04cb515ed142a290b92d321f00f");

	// Token: 0x04002BB0 RID: 11184
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_EmoteResponse_01.prefab:680e09754c351c3418b3fb0c25e06cef");

	// Token: 0x04002BB1 RID: 11185
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01.prefab:012cc97423a43e74da3f6f296e2b7e62");

	// Token: 0x04002BB2 RID: 11186
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02.prefab:0feae56b20341204c855efeb9a477ef7");

	// Token: 0x04002BB3 RID: 11187
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03.prefab:660057087869d6746b2107610923cf4e");

	// Token: 0x04002BB4 RID: 11188
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04.prefab:4bebbef940b0c2745ad0db14fd93ea75");

	// Token: 0x04002BB5 RID: 11189
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05.prefab:94b0aaae1c4e3454dbd0cc137daac275");

	// Token: 0x04002BB6 RID: 11190
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01.prefab:5a504f320224e904da7fd12d66cb0cce");

	// Token: 0x04002BB7 RID: 11191
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02.prefab:ad60ac67236aa974981677c64a821818");

	// Token: 0x04002BB8 RID: 11192
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03.prefab:c60c1ec98b4519c418db8a278aab6385");

	// Token: 0x04002BB9 RID: 11193
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_Intro_01.prefab:0d74a9d90334d2649bf487249de05b09");

	// Token: 0x04002BBA RID: 11194
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_IntroBrannResponse_01.prefab:817ec92be64226e43a857da98a5e845c");

	// Token: 0x04002BBB RID: 11195
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_5AtkorGreaterWeapon_01.prefab:a8bc6c0ea13be4147aab13ee492c1ddc");

	// Token: 0x04002BBC RID: 11196
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTrigger_Colossus_of_the_Moon_01.prefab:55e6203596a44ce45a61bcc532c917ad");

	// Token: 0x04002BBD RID: 11197
	private static readonly AssetReference VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01 = new AssetReference("VO_ULDA_BOSS_19h_Male_TitanWatcher_PlayerTriggerTitanRingTreasure_01.prefab:3173b835a49f72746ba9f06422346e69");

	// Token: 0x04002BBE RID: 11198
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_01,
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_02,
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_03,
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_04,
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_HeroPower_05
	};

	// Token: 0x04002BBF RID: 11199
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_01,
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_02,
		ULDA_Dungeon_Boss_19h.VO_ULDA_BOSS_19h_Male_TitanWatcher_Idle_03
	};

	// Token: 0x04002BC0 RID: 11200
	private HashSet<string> m_playedLines = new HashSet<string>();
}
