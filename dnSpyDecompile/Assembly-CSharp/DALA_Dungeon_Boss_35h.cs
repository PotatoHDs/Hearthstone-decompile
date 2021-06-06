using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000450 RID: 1104
public class DALA_Dungeon_Boss_35h : DALA_Dungeon
{
	// Token: 0x06003BFD RID: 15357 RVA: 0x0013820C File Offset: 0x0013640C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Death_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPower_02,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPower_03,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPower_04,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Idle_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Idle_02,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Idle_03,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Intro_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01,
			DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x00138390 File Offset: 0x00136590
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02;
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x001383C8 File Offset: 0x001365C8
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_35h.m_IdleLines;
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C01 RID: 15361 RVA: 0x001383D0 File Offset: 0x001365D0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
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

	// Token: 0x06003C02 RID: 15362 RVA: 0x001384B7 File Offset: 0x001366B7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_35h.m_HeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_35h.m_HeroPowerFull);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C03 RID: 15363 RVA: 0x001384CD File Offset: 0x001366CD
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "CS1_112"))
		{
			if (cardId == "EX1_625")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003C04 RID: 15364 RVA: 0x001384E3 File Offset: 0x001366E3
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
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
		if (!(cardId == "LOOT_388"))
		{
			if (cardId == "LOOT_278t1" || cardId == "LOOT_278t2" || cardId == "LOOT_278t3" || cardId == "LOOT_278t4")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400251E RID: 9502
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_BossFungalEnchanter_02.prefab:25c05fb426d9fb544a0ebc10562d2573");

	// Token: 0x0400251F RID: 9503
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_BossSpellNotHeal_01.prefab:0ec1d3d0a1f8e0b4a950d383359e6c29");

	// Token: 0x04002520 RID: 9504
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Death_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Death_01.prefab:3eefb2ed7c30d8b4e9d0df6753e3dee1");

	// Token: 0x04002521 RID: 9505
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_DefeatPlayer_01.prefab:bc8ffc05d0477a74d81bd9ca0438e357");

	// Token: 0x04002522 RID: 9506
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_EmoteResponse_02.prefab:ca986dbc64768f54f8a017f3f128f226");

	// Token: 0x04002523 RID: 9507
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPower_02.prefab:dc3d97af82fdd72459e432738740006b");

	// Token: 0x04002524 RID: 9508
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPower_03.prefab:b3c85457720f0f94784965cccc08eb81");

	// Token: 0x04002525 RID: 9509
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPower_04.prefab:302e1e07f01bcfa49a513eba2f9e87a1");

	// Token: 0x04002526 RID: 9510
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01.prefab:cd257dd25f7049349bfa5baeab0ec1e1");

	// Token: 0x04002527 RID: 9511
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02.prefab:2968be61389f10748919cee312354716");

	// Token: 0x04002528 RID: 9512
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03.prefab:ca2cb80e1d64fcc4cab8ea6a6b6d5c01");

	// Token: 0x04002529 RID: 9513
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Idle_01.prefab:52b31b78da5a0814795b9aafc811e307");

	// Token: 0x0400252A RID: 9514
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Idle_02.prefab:8b9870e311ec6b54d9a92e51581d973f");

	// Token: 0x0400252B RID: 9515
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Idle_03.prefab:9384798e42b21054e82d4ffa1729729e");

	// Token: 0x0400252C RID: 9516
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_Intro_01.prefab:a16c116a544085549b710c7eaf71aac9");

	// Token: 0x0400252D RID: 9517
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_IntroGeorge_02.prefab:f8688b867b2d6f04eb3b0ff2709e5cb0");

	// Token: 0x0400252E RID: 9518
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_PlayerHolyNova_01.prefab:c2bb5c4f04f66ba4b9260a154448f350");

	// Token: 0x0400252F RID: 9519
	private static readonly AssetReference VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01 = new AssetReference("VO_DALA_BOSS_35h_Female_Human_PlayerShadowForm_01.prefab:2c45b907579c33c4ba68fc0b80ce2e8e");

	// Token: 0x04002530 RID: 9520
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Idle_01,
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Idle_02,
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_Idle_03
	};

	// Token: 0x04002531 RID: 9521
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPower_02,
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPower_03,
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPower_04
	};

	// Token: 0x04002532 RID: 9522
	private static List<string> m_HeroPowerFull = new List<string>
	{
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_01,
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_02,
		DALA_Dungeon_Boss_35h.VO_DALA_BOSS_35h_Female_Human_HeroPowerFull_03
	};

	// Token: 0x04002533 RID: 9523
	private HashSet<string> m_playedLines = new HashSet<string>();
}
