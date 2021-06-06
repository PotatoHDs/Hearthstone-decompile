using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004AD RID: 1197
public class ULDA_Dungeon_Boss_48h : ULDA_Dungeon
{
	// Token: 0x06004063 RID: 16483 RVA: 0x00157978 File Offset: 0x00155B78
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_DefeatPlayer_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Idle_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Idle_02,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Idle_03,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Intro_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01,
			ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004064 RID: 16484 RVA: 0x00157AEC File Offset: 0x00155CEC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004065 RID: 16485 RVA: 0x00157AF4 File Offset: 0x00155CF4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004066 RID: 16486 RVA: 0x00157AFC File Offset: 0x00155CFC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01;
	}

	// Token: 0x06004067 RID: 16487 RVA: 0x00157B34 File Offset: 0x00155D34
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004068 RID: 16488 RVA: 0x00157C0E File Offset: 0x00155E0E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06004069 RID: 16489 RVA: 0x00157C24 File Offset: 0x00155E24
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
		if (!(cardId == "ULD_229"))
		{
			if (!(cardId == "EX1_407"))
			{
				if (cardId == "ULD_280")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600406A RID: 16490 RVA: 0x00157C3A File Offset: 0x00155E3A
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
		if (!(cardId == "EX1_407"))
		{
			if (cardId == "CS2_124")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002E66 RID: 11878
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerBrawl_01.prefab:1b3d9aa090490084cb5c69b5d6e7d911");

	// Token: 0x04002E67 RID: 11879
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWarriorDamageDestroySpell_01.prefab:57241ec4c761c5c47aac98f4efd46c45");

	// Token: 0x04002E68 RID: 11880
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_BossTriggerWolfrider_01.prefab:dc0224756d9ed0a4ba83d16021db6ac1");

	// Token: 0x04002E69 RID: 11881
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_DeathALT_01.prefab:4b012baa630fc644e8d0dab6253c2a39");

	// Token: 0x04002E6A RID: 11882
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_DefeatPlayer_01.prefab:47871a3b50561e2459b80724e45dd027");

	// Token: 0x04002E6B RID: 11883
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_EmoteResponse_01.prefab:1ff3d71246673cf4d9042798c40bcb0f");

	// Token: 0x04002E6C RID: 11884
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01.prefab:8b2b23e5c9ecb4445925d949680861cd");

	// Token: 0x04002E6D RID: 11885
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02.prefab:69ef1dfe547c9934788dd4a50e3b193b");

	// Token: 0x04002E6E RID: 11886
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03.prefab:99cfa9ca66006244a9302dc6556d49a6");

	// Token: 0x04002E6F RID: 11887
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Idle_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Idle_01.prefab:d2a4403d971a58b4780bf479c36cb186");

	// Token: 0x04002E70 RID: 11888
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Idle_02 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Idle_02.prefab:a33b5cdcd6876d54c9109533f887c84d");

	// Token: 0x04002E71 RID: 11889
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Idle_03 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Idle_03.prefab:52d9faf2ab95f4244ad180726bcf3622");

	// Token: 0x04002E72 RID: 11890
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_Intro_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_Intro_01.prefab:7fbbd04bfd86a014db7feba3d49717ff");

	// Token: 0x04002E73 RID: 11891
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_IntroSpecial_Finley_01.prefab:b29c74c6cc52c9b43b2b128e16d69fc6");

	// Token: 0x04002E74 RID: 11892
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Mischief_Maker_01.prefab:1c940300f467aec4296e23d5ff09ae78");

	// Token: 0x04002E75 RID: 11893
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_PlayerTrigger_Wasteland_Scorpid_01.prefab:0b21f1b9465901f48aa8bf0a1b1675f5");

	// Token: 0x04002E76 RID: 11894
	private static readonly AssetReference VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01 = new AssetReference("VO_ULDA_BOSS_48h_Female_Maghar_PlayerTriggerBrawl_01.prefab:450a9127f0e56be47a40dc22f06a648a");

	// Token: 0x04002E77 RID: 11895
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_01,
		ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_02,
		ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_HeroPower_03
	};

	// Token: 0x04002E78 RID: 11896
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Idle_01,
		ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Idle_02,
		ULDA_Dungeon_Boss_48h.VO_ULDA_BOSS_48h_Female_Maghar_Idle_03
	};

	// Token: 0x04002E79 RID: 11897
	private HashSet<string> m_playedLines = new HashSet<string>();
}
