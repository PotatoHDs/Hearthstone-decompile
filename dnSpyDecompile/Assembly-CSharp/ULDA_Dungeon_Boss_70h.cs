using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C2 RID: 1218
public class ULDA_Dungeon_Boss_70h : ULDA_Dungeon
{
	// Token: 0x06004146 RID: 16710 RVA: 0x0015D090 File Offset: 0x0015B290
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_BossTriggerArcaneMissilesSpellPower_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_BossVaporizeTrigger_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_DeathALT_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_DefeatPlayer_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_EmoteResponse_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_02,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_03,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_04,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_05,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPowerRare_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_02,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_03,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Intro_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_IntroSpecialBrann_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFreezeBoss_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFrostArmorTrigger_01,
			ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerPyroblastFace_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004147 RID: 16711 RVA: 0x0015D224 File Offset: 0x0015B424
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004148 RID: 16712 RVA: 0x0015D22C File Offset: 0x0015B42C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004149 RID: 16713 RVA: 0x0015D234 File Offset: 0x0015B434
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_EmoteResponse_01;
	}

	// Token: 0x0600414A RID: 16714 RVA: 0x0015D26C File Offset: 0x0015B46C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_IntroSpecialBrann_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x0600414B RID: 16715 RVA: 0x0015D346 File Offset: 0x0015B546
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
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFreezeBoss_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFrostArmorTrigger_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerPyroblastFace_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_BossVaporizeTrigger_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_BossTriggerArcaneMissilesSpellPower_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x0600414C RID: 16716 RVA: 0x0015D35C File Offset: 0x0015B55C
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
		yield break;
	}

	// Token: 0x0600414D RID: 16717 RVA: 0x0015D372 File Offset: 0x0015B572
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
		yield break;
	}

	// Token: 0x04003010 RID: 12304
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_BossTriggerArcaneMissilesSpellPower_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_BossTriggerArcaneMissilesSpellPower_01.prefab:c9540a9ec64f44f409bed15ad68e83da");

	// Token: 0x04003011 RID: 12305
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_BossVaporizeTrigger_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_BossVaporizeTrigger_01.prefab:89a0bfc086c730441954de3ab364d4e1");

	// Token: 0x04003012 RID: 12306
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_DeathALT_01.prefab:3dc608dccd0ddfb4ead688a9e2604514");

	// Token: 0x04003013 RID: 12307
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_DefeatPlayer_01.prefab:eb97396b15ba2bc4da33f03c84e76ca1");

	// Token: 0x04003014 RID: 12308
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_EmoteResponse_01.prefab:698b1ac4aada5db4f84f112ca995c232");

	// Token: 0x04003015 RID: 12309
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_01.prefab:e3ef9e9f28778bd418c4f45e97ae942e");

	// Token: 0x04003016 RID: 12310
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_02.prefab:dee59a73cd6617d45b225bf510e504e3");

	// Token: 0x04003017 RID: 12311
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_03.prefab:75f80a9bd9ad36d4d831764b03a5326c");

	// Token: 0x04003018 RID: 12312
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_04.prefab:3aabfd692db3f34428c3031bd4c2a083");

	// Token: 0x04003019 RID: 12313
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_05.prefab:8bd7a7f82b3b9f447a482cbec4b62b7c");

	// Token: 0x0400301A RID: 12314
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPowerRare_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPowerRare_01.prefab:c697707f48711bd448a54f6d0932c107");

	// Token: 0x0400301B RID: 12315
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_01.prefab:f982fe45ee4762b40a9e322e762e05b4");

	// Token: 0x0400301C RID: 12316
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_02 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_02.prefab:6d059ca0b84dee844aad3249b8e3bb52");

	// Token: 0x0400301D RID: 12317
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_03 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_03.prefab:fe899f346fe0fbb4e9bf35e4de50e77e");

	// Token: 0x0400301E RID: 12318
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_Intro_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_Intro_01.prefab:278d7147969332f42915201a4bb3709b");

	// Token: 0x0400301F RID: 12319
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_IntroSpecialBrann_01.prefab:cf32fac4c04ad2044b663e3bdfb2094d");

	// Token: 0x04003020 RID: 12320
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFreezeBoss_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFreezeBoss_01.prefab:1455fbb598786ac4a80d2069b36312fb");

	// Token: 0x04003021 RID: 12321
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFrostArmorTrigger_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerFrostArmorTrigger_01.prefab:5301ecb285849a841838217c2ffa01dc");

	// Token: 0x04003022 RID: 12322
	private static readonly AssetReference VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerPyroblastFace_01 = new AssetReference("VO_ULDA_BOSS_70h_Male_TitanConstruct_PlayerPyroblastFace_01.prefab:f9806b9ed78cb734585133632b7751ff");

	// Token: 0x04003023 RID: 12323
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_01,
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_02,
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_03,
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_04,
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_HeroPower_05
	};

	// Token: 0x04003024 RID: 12324
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_01,
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_02,
		ULDA_Dungeon_Boss_70h.VO_ULDA_BOSS_70h_Male_TitanConstruct_Idle_03
	};

	// Token: 0x04003025 RID: 12325
	private HashSet<string> m_playedLines = new HashSet<string>();
}
