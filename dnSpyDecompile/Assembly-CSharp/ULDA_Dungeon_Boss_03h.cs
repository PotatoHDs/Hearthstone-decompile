using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000480 RID: 1152
public class ULDA_Dungeon_Boss_03h : ULDA_Dungeon
{
	// Token: 0x06003E64 RID: 15972 RVA: 0x0014AEE4 File Offset: 0x001490E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Death_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_DefeatPlayer_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_HeroPower_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_HeroPower_02,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_HeroPower_05,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Idle_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Idle_02,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Idle_03,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Intro_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01,
			ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E65 RID: 15973 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E66 RID: 15974 RVA: 0x0014B048 File Offset: 0x00149248
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E67 RID: 15975 RVA: 0x0014B050 File Offset: 0x00149250
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01;
	}

	// Token: 0x06003E68 RID: 15976 RVA: 0x0014B088 File Offset: 0x00149288
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "ULDA_Finley")
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

	// Token: 0x06003E69 RID: 15977 RVA: 0x0014B1AE File Offset: 0x001493AE
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003E6A RID: 15978 RVA: 0x0014B1C4 File Offset: 0x001493C4
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
		if (!(cardId == "ULD_195"))
		{
			if (cardId == "ULD_163")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E6B RID: 15979 RVA: 0x0014B1DA File Offset: 0x001493DA
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
		if (!(cardId == "ULD_160"))
		{
			if (cardId == "ULD_170")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002AB6 RID: 10934
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_BossTriggerSinisterDeal_01.prefab:a51a01532d42ad146b2aa33565be4b34");

	// Token: 0x04002AB7 RID: 10935
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_BossTriggerWeaponizedWasp_01.prefab:d010cf5ae2f4dfc4898b4ed186806fa7");

	// Token: 0x04002AB8 RID: 10936
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Death_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Death_01.prefab:a6327c291f470f949bbf8cb5104218be");

	// Token: 0x04002AB9 RID: 10937
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_DefeatPlayer_01.prefab:213adc426d7f1474a9deef70ff873cd9");

	// Token: 0x04002ABA RID: 10938
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_EmoteResponse_01.prefab:82f4117350cc42447b084876b0992418");

	// Token: 0x04002ABB RID: 10939
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_HeroPower_01.prefab:8a85ecb0a49cf0d42a5c7f78f095334d");

	// Token: 0x04002ABC RID: 10940
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_HeroPower_02.prefab:c0fc67769fe151048b3d0e594ae978df");

	// Token: 0x04002ABD RID: 10941
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_HeroPower_05.prefab:e467b975f9ed44347945d9ec4cd5320c");

	// Token: 0x04002ABE RID: 10942
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Idle_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Idle_01.prefab:eee83ede7cb5c234d920db60baa9ce35");

	// Token: 0x04002ABF RID: 10943
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Idle_02 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Idle_02.prefab:c785f9f3685821447af9360560687b3b");

	// Token: 0x04002AC0 RID: 10944
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Idle_03 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Idle_03.prefab:da256f8fe6d5dad4ba4597194bf33d57");

	// Token: 0x04002AC1 RID: 10945
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_Intro_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_Intro_01.prefab:d2328bc0879cd1f408381a0aba8822ab");

	// Token: 0x04002AC2 RID: 10946
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_IntroBrannResponse_01.prefab:00517485c0759c445b311efde42de66e");

	// Token: 0x04002AC3 RID: 10947
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_IntroEliseResponse_01.prefab:e980a990444f5f44798d5d123f42846c");

	// Token: 0x04002AC4 RID: 10948
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Expired_Merchant_01.prefab:081efa092c272794883e1217241e80da");

	// Token: 0x04002AC5 RID: 10949
	private static readonly AssetReference VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01 = new AssetReference("VO_ULDA_BOSS_03h_Female_Human_PlayerTrigger_Frightened_Flunky_01.prefab:187596b96e402164fbe57fd39100233d");

	// Token: 0x04002AC6 RID: 10950
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_HeroPower_01,
		ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_HeroPower_02,
		ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_HeroPower_05
	};

	// Token: 0x04002AC7 RID: 10951
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Idle_01,
		ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Idle_02,
		ULDA_Dungeon_Boss_03h.VO_ULDA_BOSS_03h_Female_Human_Idle_03
	};

	// Token: 0x04002AC8 RID: 10952
	private HashSet<string> m_playedLines = new HashSet<string>();
}
