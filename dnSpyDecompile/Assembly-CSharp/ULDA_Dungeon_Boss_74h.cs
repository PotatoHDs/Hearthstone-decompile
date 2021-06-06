using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C6 RID: 1222
public class ULDA_Dungeon_Boss_74h : ULDA_Dungeon
{
	// Token: 0x0600417B RID: 16763 RVA: 0x0015E3B0 File Offset: 0x0015C5B0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01,
			ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600417C RID: 16764 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600417D RID: 16765 RVA: 0x0015E524 File Offset: 0x0015C724
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600417E RID: 16766 RVA: 0x0015E52C File Offset: 0x0015C72C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600417F RID: 16767 RVA: 0x0015E534 File Offset: 0x0015C734
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01;
	}

	// Token: 0x06004180 RID: 16768 RVA: 0x0015E56C File Offset: 0x0015C76C
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

	// Token: 0x06004181 RID: 16769 RVA: 0x0015E5F5 File Offset: 0x0015C7F5
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004182 RID: 16770 RVA: 0x0015E60B File Offset: 0x0015C80B
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
		if (!(cardId == "ULD_262"))
		{
			if (!(cardId == "ULD_705"))
			{
				if (cardId == "ULD_193")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004183 RID: 16771 RVA: 0x0015E621 File Offset: 0x0015C821
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
		if (!(cardId == "ULD_145"))
		{
			if (!(cardId == "DAL_141"))
			{
				if (cardId == "ULD_714")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003067 RID: 12391
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerBrazenZealot_01.prefab:4ddef806194558c4bad1f0f6cda87b40");

	// Token: 0x04003068 RID: 12392
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerDesperateMeasures_01.prefab:10b6300309d799a4ca3bd33cffa6d536");

	// Token: 0x04003069 RID: 12393
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_BossTriggerPenance_01.prefab:ba5866db8f497404fbcdbf6b1dc9f2d5");

	// Token: 0x0400306A RID: 12394
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Death_01.prefab:a8743560fe64e17478acd7fef0788774");

	// Token: 0x0400306B RID: 12395
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_DefeatPlayer_01.prefab:434dba6b805689141bfcd4c065dc2c6b");

	// Token: 0x0400306C RID: 12396
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_EmoteResponse_01.prefab:15fee89c4c3068b40a4abb6ee7e1d0f1");

	// Token: 0x0400306D RID: 12397
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01.prefab:21ee719686c9556479e167bf56c79415");

	// Token: 0x0400306E RID: 12398
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02.prefab:6a7dac9f196114c42acc563b5c67f114");

	// Token: 0x0400306F RID: 12399
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04.prefab:25e6a7304a976c14fa8ba326ffbebe5f");

	// Token: 0x04003070 RID: 12400
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05.prefab:6f71f32b1a87a7a4c85e7e09924f7088");

	// Token: 0x04003071 RID: 12401
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01.prefab:8ebb64f437aaf31489ba8471cae04fb1");

	// Token: 0x04003072 RID: 12402
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02.prefab:22a4b041347a41a46af780d35761e93f");

	// Token: 0x04003073 RID: 12403
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03.prefab:4482bb176cacb6d41af0a5f144051f7c");

	// Token: 0x04003074 RID: 12404
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_Intro_01.prefab:b1476815603ebc944a393d4f15901024");

	// Token: 0x04003075 RID: 12405
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_High_Priest_Amet_01.prefab:77016998e60f11a4a92be46558f2c45b");

	// Token: 0x04003076 RID: 12406
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Living_Monument_01.prefab:f3046fb234083d14786c4129883a9745");

	// Token: 0x04003077 RID: 12407
	private static readonly AssetReference VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01 = new AssetReference("VO_ULDA_BOSS_74h_Male_NefersetTolvir_PlayerTrigger_Mogu_Cultist_01.prefab:a06f53a896bb4384b9259e93c706c62b");

	// Token: 0x04003078 RID: 12408
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_01,
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_02,
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_04,
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_HeroPower_05
	};

	// Token: 0x04003079 RID: 12409
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_01,
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_02,
		ULDA_Dungeon_Boss_74h.VO_ULDA_BOSS_74h_Male_NefersetTolvir_Idle_03
	};

	// Token: 0x0400307A RID: 12410
	private HashSet<string> m_playedLines = new HashSet<string>();
}
