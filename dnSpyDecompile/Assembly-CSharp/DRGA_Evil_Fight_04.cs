using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004D2 RID: 1234
public class DRGA_Evil_Fight_04 : DRGA_Dungeon
{
	// Token: 0x0600421A RID: 16922 RVA: 0x00162340 File Offset: 0x00160540
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_ALT_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01,
			DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600421B RID: 16923 RVA: 0x00162564 File Offset: 0x00160764
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_IdleLines;
	}

	// Token: 0x0600421C RID: 16924 RVA: 0x0016256C File Offset: 0x0016076C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01;
	}

	// Token: 0x0600421D RID: 16925 RVA: 0x00162584 File Offset: 0x00160784
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				return;
			}
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x0600421E RID: 16926 RVA: 0x00162695 File Offset: 0x00160895
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventTrigger101Lines);
			goto IL_3E4;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventTrigger102Lines);
			goto IL_3E4;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01, 2.5f);
				goto IL_3E4;
			}
			goto IL_3E4;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01, 2.5f);
				goto IL_3E4;
			}
			goto IL_3E4;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01, 2.5f);
				goto IL_3E4;
			}
			goto IL_3E4;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01, 2.5f);
				goto IL_3E4;
			}
			goto IL_3E4;
		case 109:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01, 2.5f);
			goto IL_3E4;
		case 110:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01, 2.5f);
			goto IL_3E4;
		case 111:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01, 2.5f);
			goto IL_3E4;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3E4:
		yield break;
	}

	// Token: 0x0600421F RID: 16927 RVA: 0x001626AB File Offset: 0x001608AB
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
		if (!(cardId == "DRG_026") && !(cardId == "DRG_270"))
		{
			if (cardId == "DRG_036")
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004220 RID: 16928 RVA: 0x001626C1 File Offset: 0x001608C1
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

	// Token: 0x0400318C RID: 12684
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01.prefab:fb59050624bb4f742af20725bb5dac8d");

	// Token: 0x0400318D RID: 12685
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_ALT_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_ALT_01.prefab:ff9296d899b70494184c097abdf7fc0b");

	// Token: 0x0400318E RID: 12686
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01.prefab:6b91b6060fd1cec4daa78557b3cc98e8");

	// Token: 0x0400318F RID: 12687
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01.prefab:f92d24bfb2dde784cad973743444a8db");

	// Token: 0x04003190 RID: 12688
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01.prefab:2338504811083cf4b940a6d1294eb318");

	// Token: 0x04003191 RID: 12689
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01.prefab:9d8b703966c80d64cb401e71f276023d");

	// Token: 0x04003192 RID: 12690
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01.prefab:11538486800e08d40a55dfd99a07c8fc");

	// Token: 0x04003193 RID: 12691
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01.prefab:85b63efb2a5351b47aca246dc7d8758b");

	// Token: 0x04003194 RID: 12692
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01.prefab:5d6ca739d5747e346857be286eea46e7");

	// Token: 0x04003195 RID: 12693
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01.prefab:2f92b2564c37e6f4f94b7ed6d4f4a6ae");

	// Token: 0x04003196 RID: 12694
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01.prefab:78448df9d383f1847957f14931ce7d0b");

	// Token: 0x04003197 RID: 12695
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01.prefab:20f6f5ab55241504bb078b133c26ba81");

	// Token: 0x04003198 RID: 12696
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01.prefab:4c0459040ff59c74c87221af8f45754b");

	// Token: 0x04003199 RID: 12697
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01.prefab:b12f3c72ae450fd49bf2e0ab2264ac49");

	// Token: 0x0400319A RID: 12698
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01.prefab:694ddc3e96ccec14eb4f8cf86704362a");

	// Token: 0x0400319B RID: 12699
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01.prefab:1f65c83f9b6ed4d4391720e59038e51c");

	// Token: 0x0400319C RID: 12700
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01.prefab:edfe8ae778bebe94faafaa55e7a7e908");

	// Token: 0x0400319D RID: 12701
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01.prefab:e0cbe4545aa8c4c46b864e7e42481d7f");

	// Token: 0x0400319E RID: 12702
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01.prefab:474ffaae173517d478c2ae2365c92e9c");

	// Token: 0x0400319F RID: 12703
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01.prefab:6066b6c1f9654d54cad48eaea607e252");

	// Token: 0x040031A0 RID: 12704
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01.prefab:441ecb1e7b084a542aacb7ba0836105c");

	// Token: 0x040031A1 RID: 12705
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01.prefab:74e206bc3bec69747b13f8e968d0021a");

	// Token: 0x040031A2 RID: 12706
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01.prefab:3962774ee11d62a4b822973849ebef77");

	// Token: 0x040031A3 RID: 12707
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01.prefab:e0745cb1fb7a4664588bcd85e2b38629");

	// Token: 0x040031A4 RID: 12708
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01.prefab:2c6a55e0d68f9f644abea2b11541f4ff");

	// Token: 0x040031A5 RID: 12709
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01.prefab:25b255118f60f2a4796f76e1c31a10d1");

	// Token: 0x040031A6 RID: 12710
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01.prefab:4d875c0bf7cf2f94ebf2602176c042f9");

	// Token: 0x040031A7 RID: 12711
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01.prefab:5fff2f88775e8e6489959428dfc16f31");

	// Token: 0x040031A8 RID: 12712
	private List<string> m_missionEventTrigger101Lines = new List<string>
	{
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01,
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01,
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01
	};

	// Token: 0x040031A9 RID: 12713
	private List<string> m_missionEventTrigger102Lines = new List<string>
	{
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01,
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01,
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01
	};

	// Token: 0x040031AA RID: 12714
	private List<string> m_VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01,
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01,
		DRGA_Evil_Fight_04.VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01
	};

	// Token: 0x040031AB RID: 12715
	private HashSet<string> m_playedLines = new HashSet<string>();
}
