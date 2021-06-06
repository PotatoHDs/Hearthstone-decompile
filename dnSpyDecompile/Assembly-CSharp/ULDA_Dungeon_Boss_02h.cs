using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200047F RID: 1151
public class ULDA_Dungeon_Boss_02h : ULDA_Dungeon
{
	// Token: 0x06003E56 RID: 15958 RVA: 0x0014AA78 File Offset: 0x00148C78
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01,
			ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x0014ABEC File Offset: 0x00148DEC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E59 RID: 15961 RVA: 0x0014ABF4 File Offset: 0x00148DF4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003E5A RID: 15962 RVA: 0x0014ABFC File Offset: 0x00148DFC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01;
	}

	// Token: 0x06003E5B RID: 15963 RVA: 0x0014AC34 File Offset: 0x00148E34
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Brann")
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

	// Token: 0x06003E5C RID: 15964 RVA: 0x0014ACDF File Offset: 0x00148EDF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003E5D RID: 15965 RVA: 0x0014ACF5 File Offset: 0x00148EF5
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
		if (!(cardId == "ULD_198"))
		{
			if (cardId == "ULD_265")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E5E RID: 15966 RVA: 0x0014AD0B File Offset: 0x00148F0B
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
		if (!(cardId == "ULD_208"))
		{
			if (!(cardId == "ULD_723"))
			{
				if (cardId == "ULD_185")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002AA2 RID: 10914
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossKhartutDefender_01.prefab:9aefc1756b18c3243bcd1fc9b7ddba95");

	// Token: 0x04002AA3 RID: 10915
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerMurmy_01.prefab:440f45fe4088d9644a2a7bdf3957eb1e");

	// Token: 0x04002AA4 RID: 10916
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_BossTriggerTempleBerserker_01.prefab:55bfc25fbbd181246b758b80647f1fbb");

	// Token: 0x04002AA5 RID: 10917
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Death_01.prefab:1f805fefe136f9749b33bcd30d3d5a18");

	// Token: 0x04002AA6 RID: 10918
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_DefeatPlayer_01.prefab:9e7eff7b8d4c8ff4baa68bad4c6f7d0a");

	// Token: 0x04002AA7 RID: 10919
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_EmoteResponse_01.prefab:3345597ffd2600c4abef8ec08b66c8b4");

	// Token: 0x04002AA8 RID: 10920
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01.prefab:8bdfee6ba1794e646a801a7547f795e9");

	// Token: 0x04002AA9 RID: 10921
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02.prefab:ea6491933eac5e6489a327d066a9ffe3");

	// Token: 0x04002AAA RID: 10922
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03.prefab:af3da69402a52e94ca677423ac3f0dd8");

	// Token: 0x04002AAB RID: 10923
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04.prefab:8421ff98e4c1ac746ac4789c26d320a1");

	// Token: 0x04002AAC RID: 10924
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05.prefab:bbe26dbaa8c01b949915c809920b4b86");

	// Token: 0x04002AAD RID: 10925
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01.prefab:216e7b15265cb5047b1fac95ad0df5cc");

	// Token: 0x04002AAE RID: 10926
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02.prefab:69155876b0008274a801a5326256b2e4");

	// Token: 0x04002AAF RID: 10927
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03.prefab:f513c26c122f736499228e5f41e4b610");

	// Token: 0x04002AB0 RID: 10928
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_Intro_01.prefab:47b82e642ae7cc242aa9b5388a956488");

	// Token: 0x04002AB1 RID: 10929
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Conjured_Mirage_01.prefab:8ce518a633961124da0d79bc08561d6e");

	// Token: 0x04002AB2 RID: 10930
	private static readonly AssetReference VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01 = new AssetReference("VO_ULDA_BOSS_02h_Male_NefersetTolvir_PlayerTrigger_Embalming_Ritual_01.prefab:3b8d27c284dc924468518b254e182440");

	// Token: 0x04002AB3 RID: 10931
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_01,
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_02,
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_03,
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_04,
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_HeroPower_05
	};

	// Token: 0x04002AB4 RID: 10932
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_01,
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_02,
		ULDA_Dungeon_Boss_02h.VO_ULDA_BOSS_02h_Male_NefersetTolvir_Idle_03
	};

	// Token: 0x04002AB5 RID: 10933
	private HashSet<string> m_playedLines = new HashSet<string>();
}
