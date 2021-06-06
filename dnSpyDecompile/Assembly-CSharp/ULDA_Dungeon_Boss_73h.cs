using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C5 RID: 1221
public class ULDA_Dungeon_Boss_73h : ULDA_Dungeon
{
	// Token: 0x0600416D RID: 16749 RVA: 0x0015DED8 File Offset: 0x0015C0D8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerExplosiveRunes_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerHeroicStrike_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerPlayExplosiveRunes_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Death_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_02,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_03,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_04,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_05,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_02,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_03,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Intro_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Cursed_Lieutenant_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_King_Phaoris_01,
			ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Siamat_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600416E RID: 16750 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600416F RID: 16751 RVA: 0x0015E06C File Offset: 0x0015C26C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004170 RID: 16752 RVA: 0x0015E074 File Offset: 0x0015C274
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004171 RID: 16753 RVA: 0x0015E07C File Offset: 0x0015C27C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_EmoteResponse_01;
	}

	// Token: 0x06004172 RID: 16754 RVA: 0x0015E0B4 File Offset: 0x0015C2B4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_IntroSpecialFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004173 RID: 16755 RVA: 0x0015E18E File Offset: 0x0015C38E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerExplosiveRunes_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x0015E1A4 File Offset: 0x0015C3A4
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
		if (!(cardId == "ULD_189"))
		{
			if (!(cardId == "ULD_304"))
			{
				if (cardId == "ULD_178")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Siamat_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_King_Phaoris_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Cursed_Lieutenant_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004175 RID: 16757 RVA: 0x0015E1BA File Offset: 0x0015C3BA
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
		if (!(cardId == "CS2_105"))
		{
			if (cardId == "LOOT_101")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerPlayExplosiveRunes_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerHeroicStrike_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003051 RID: 12369
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerExplosiveRunes_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerExplosiveRunes_01.prefab:ee2b49761bb03b54bac04eec04d52fc9");

	// Token: 0x04003052 RID: 12370
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerHeroicStrike_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerHeroicStrike_01.prefab:002ff346bfa849848abf40df1a0ed52c");

	// Token: 0x04003053 RID: 12371
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerPlayExplosiveRunes_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_BossTriggerPlayExplosiveRunes_01.prefab:06afcc5a0c94cc64b8deb309daf5dfa7");

	// Token: 0x04003054 RID: 12372
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_Death_01.prefab:734e0529efa8cf3429a1262cec87d5ef");

	// Token: 0x04003055 RID: 12373
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_DefeatPlayer_01.prefab:1b2aae4f91b64a548b9232e789f7bfa5");

	// Token: 0x04003056 RID: 12374
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_EmoteResponse_01.prefab:b5e892b811ea91c47860068dd1cd52ec");

	// Token: 0x04003057 RID: 12375
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_01.prefab:62af478601389a246b024c8c58b3619e");

	// Token: 0x04003058 RID: 12376
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_02.prefab:a103d60ac2181ad4cbe43849cd55846b");

	// Token: 0x04003059 RID: 12377
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_03.prefab:3436c65cd5de29749b2ca9af4d9bac5e");

	// Token: 0x0400305A RID: 12378
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_04.prefab:cf78703d684c45b4cbc92ce8b445a1b2");

	// Token: 0x0400305B RID: 12379
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_05.prefab:4930f45660b454348b9d36cf41444737");

	// Token: 0x0400305C RID: 12380
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_01.prefab:c4ac919798ee45d4a89329690d8b1fbf");

	// Token: 0x0400305D RID: 12381
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_02.prefab:1c64211ff601b794d93512706564a45b");

	// Token: 0x0400305E RID: 12382
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_03.prefab:44c14962e4fd80a4c844f7255b159e24");

	// Token: 0x0400305F RID: 12383
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_Intro_01.prefab:f087080c34f6261438b8052d9a1a5fba");

	// Token: 0x04003060 RID: 12384
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_IntroSpecialFinley_01.prefab:07f425bdaca013a4eb2edf5ce59cec7e");

	// Token: 0x04003061 RID: 12385
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Cursed_Lieutenant_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Cursed_Lieutenant_01.prefab:4d29ba82c2bfd7b4e9b364383228e8e6");

	// Token: 0x04003062 RID: 12386
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_King_Phaoris_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_King_Phaoris_01.prefab:57be555af5fe6004e927eda18d537a1d");

	// Token: 0x04003063 RID: 12387
	private static readonly AssetReference VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Siamat_01 = new AssetReference("VO_ULDA_BOSS_73h_Male_NefersetTolvir_PlayerTrigger_Siamat_01.prefab:2db985ecf9ff3db43bec397d9e71652d");

	// Token: 0x04003064 RID: 12388
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_01,
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_02,
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_03,
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_04,
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_HeroPower_05
	};

	// Token: 0x04003065 RID: 12389
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_01,
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_02,
		ULDA_Dungeon_Boss_73h.VO_ULDA_BOSS_73h_Male_NefersetTolvir_Idle_03
	};

	// Token: 0x04003066 RID: 12390
	private HashSet<string> m_playedLines = new HashSet<string>();
}
