using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000496 RID: 1174
public class ULDA_Dungeon_Boss_25h : ULDA_Dungeon
{
	// Token: 0x06003F4D RID: 16205 RVA: 0x0014FA9C File Offset: 0x0014DC9C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Death_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_DefeatPlayer_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_IntroBrann_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_IntroFinley_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01,
			ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F4E RID: 16206 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F4F RID: 16207 RVA: 0x0014FC10 File Offset: 0x0014DE10
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F50 RID: 16208 RVA: 0x0014FC18 File Offset: 0x0014DE18
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F51 RID: 16209 RVA: 0x0014FC20 File Offset: 0x0014DE20
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01;
	}

	// Token: 0x06003F52 RID: 16210 RVA: 0x0014FC58 File Offset: 0x0014DE58
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003F53 RID: 16211 RVA: 0x0014FCE1 File Offset: 0x0014DEE1
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003F54 RID: 16212 RVA: 0x0014FCF7 File Offset: 0x0014DEF7
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
		if (!(cardId == "ULD_712"))
		{
			if (cardId == "ULDA_501")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F55 RID: 16213 RVA: 0x0014FD0D File Offset: 0x0014DF0D
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
		if (!(cardId == "ULD_133"))
		{
			if (cardId == "ULD_231")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C02 RID: 11266
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_BossCrystalMerchant_01.prefab:3eb5a7a9990665148b470ca6be88e106");

	// Token: 0x04002C03 RID: 11267
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_BossTriggerPlatedBeetleDeathrattle_01.prefab:8e96b66d6319f3748a1354b0876583ad");

	// Token: 0x04002C04 RID: 11268
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_BossWhirlkickMaster_01.prefab:9c5569a166b7d04449ef78ed63115d6b");

	// Token: 0x04002C05 RID: 11269
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Death_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Death_01.prefab:79d5bad257ecaa041982253d28c8cc8a");

	// Token: 0x04002C06 RID: 11270
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_DefeatPlayer_01.prefab:c62d0bc8daf02eb4bbf933e2965b6132");

	// Token: 0x04002C07 RID: 11271
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_EmoteResponse_01.prefab:a8128f4f1643d0143ae4d364b3ac8264");

	// Token: 0x04002C08 RID: 11272
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01.prefab:1bb99fa2cc2ab9649a3e2861611e7948");

	// Token: 0x04002C09 RID: 11273
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02.prefab:fa60da7f193e6a840ba6803927c6c1ee");

	// Token: 0x04002C0A RID: 11274
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03.prefab:02fc7d8992482844981059a2644342cf");

	// Token: 0x04002C0B RID: 11275
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01.prefab:36473927d33c6aa4281fbc530568b65b");

	// Token: 0x04002C0C RID: 11276
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02.prefab:1b04883e394e7e9419bfff09303f020e");

	// Token: 0x04002C0D RID: 11277
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03.prefab:90e35723200f64e4183ce1af6b407072");

	// Token: 0x04002C0E RID: 11278
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_Intro_01.prefab:6b9d4e2b8f8264146923e435ac904355");

	// Token: 0x04002C0F RID: 11279
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_IntroBrann_01.prefab:20e80b7557f2726408240ee2d058e9bd");

	// Token: 0x04002C10 RID: 11280
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_IntroFinley_01.prefab:81f88d793350cdb45b5a85a3e400b30c");

	// Token: 0x04002C11 RID: 11281
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_PlayerBugCollector_01.prefab:565195085f4af444dbf89d5aa96b5e11");

	// Token: 0x04002C12 RID: 11282
	private static readonly AssetReference VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01 = new AssetReference("VO_ULDA_BOSS_25h_Female_SandTroll_PlayerFinleysMount_01.prefab:1f534cab6dc1fb249a6d841fa11af2a8");

	// Token: 0x04002C13 RID: 11283
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_01,
		ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_02,
		ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_HeroPower_03
	};

	// Token: 0x04002C14 RID: 11284
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Idle_01,
		ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Idle_02,
		ULDA_Dungeon_Boss_25h.VO_ULDA_BOSS_25h_Female_SandTroll_Idle_03
	};

	// Token: 0x04002C15 RID: 11285
	private HashSet<string> m_playedLines = new HashSet<string>();
}
