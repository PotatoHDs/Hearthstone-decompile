using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004D0 RID: 1232
public class DRGA_Evil_Fight_02 : DRGA_Dungeon
{
	// Token: 0x060041FF RID: 16895 RVA: 0x00161690 File Offset: 0x0015F890
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_PlayerStart_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01,
			DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004200 RID: 16896 RVA: 0x001618C4 File Offset: 0x0015FAC4
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_IdleLines;
	}

	// Token: 0x06004201 RID: 16897 RVA: 0x001618CC File Offset: 0x0015FACC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPowerLines;
	}

	// Token: 0x06004202 RID: 16898 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004203 RID: 16899 RVA: 0x001618D4 File Offset: 0x0015FAD4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		if (!this.m_Heroic)
		{
			this.m_deathLine = DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01;
		}
		if (this.m_Heroic)
		{
			this.m_deathLine = DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01;
		}
	}

	// Token: 0x06004204 RID: 16900 RVA: 0x0016190C File Offset: 0x0015FB0C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06004205 RID: 16901 RVA: 0x001619DD File Offset: 0x0015FBDD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01, 2.5f);
			goto IL_3DE;
		case 101:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01, 2.5f);
			goto IL_3DE;
		case 102:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01, 2.5f);
				goto IL_3DE;
			}
			goto IL_3DE;
		case 104:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01, 2.5f);
			goto IL_3DE;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01, 2.5f);
				goto IL_3DE;
			}
			goto IL_3DE;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01, 2.5f);
				goto IL_3DE;
			}
			goto IL_3DE;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01, 2.5f);
				goto IL_3DE;
			}
			goto IL_3DE;
		case 113:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01, 2.5f);
			goto IL_3DE;
		case 114:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlwaysWithBrassRing(base.GetEnemyActorByCardId("DRGA_BOSS_25t"), null, DRGA_Evil_Fight_02.VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_3DE;
		case 115:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01, 2.5f);
				GameState.Get().SetBusy(false);
				base.PlaySound(DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01, 1f, true, false);
				goto IL_3DE;
			}
			goto IL_3DE;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3DE:
		yield break;
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x001619F3 File Offset: 0x0015FBF3
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
		if (!(cardId == "DRGA_BOSS_09t"))
		{
			if (cardId == "ICC_833")
			{
				if (this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01, 2.5f);
				}
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x00161A09 File Offset: 0x0015FC09
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
		if (!(cardId == "ICC_289"))
		{
			if (cardId == "ICC_090")
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003144 RID: 12612
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_PlayerStart_01.prefab:c291a4def8311994e9be1b03396f92cb");

	// Token: 0x04003145 RID: 12613
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01.prefab:c8d372d5455a3bd48850c64518c30c09");

	// Token: 0x04003146 RID: 12614
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01.prefab:4f2c978adbfece8479247c2306a361fa");

	// Token: 0x04003147 RID: 12615
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01.prefab:e5b1dcbbdcd6b414c9235c49a606c78d");

	// Token: 0x04003148 RID: 12616
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01.prefab:7c3e7aaccb8cd204e8537bf11ae68f15");

	// Token: 0x04003149 RID: 12617
	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01.prefab:92af804b9e3c7ea48888b7ce2025dd9e");

	// Token: 0x0400314A RID: 12618
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01.prefab:a73b40e951f2ba243bbaa5b5c8816363");

	// Token: 0x0400314B RID: 12619
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01.prefab:7ac56b867eb84064989e416a5f62c068");

	// Token: 0x0400314C RID: 12620
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01.prefab:8271ab3a381e4b740bb2631bffb7ebc0");

	// Token: 0x0400314D RID: 12621
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01.prefab:084a3324541241749b6d095237845635");

	// Token: 0x0400314E RID: 12622
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01.prefab:d452ccf89651e4c4793224d4abf163f4");

	// Token: 0x0400314F RID: 12623
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01.prefab:d2c3ffcb44544c94ba5d858ea4ddb8e7");

	// Token: 0x04003150 RID: 12624
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01.prefab:a5ce63dda652e1e42898e380f1de3850");

	// Token: 0x04003151 RID: 12625
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01.prefab:52cc26fe46c42594cac95c18c5c7012b");

	// Token: 0x04003152 RID: 12626
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01.prefab:97a7947075b77f5458bfd5d7cbe6b644");

	// Token: 0x04003153 RID: 12627
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01.prefab:4204dcf8e3898964292fe047284a0f1f");

	// Token: 0x04003154 RID: 12628
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01.prefab:df04e2a3ab57a714d87294fa9bf54cf6");

	// Token: 0x04003155 RID: 12629
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01.prefab:c8690c4abb1e8b6408365bf6981b1c67");

	// Token: 0x04003156 RID: 12630
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01.prefab:e0cfb9a1a25e84d47875a1fda0f5210a");

	// Token: 0x04003157 RID: 12631
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01.prefab:742add37f038de741a1cf363737c3f0c");

	// Token: 0x04003158 RID: 12632
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01.prefab:3976109773f78414db2dfcaec5de4df6");

	// Token: 0x04003159 RID: 12633
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01.prefab:71979a371cc97964fbc4ab7dfd5023fc");

	// Token: 0x0400315A RID: 12634
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01.prefab:6ce27385ff31b6b4d98a0633527295d1");

	// Token: 0x0400315B RID: 12635
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01.prefab:b4abc210c97a8ac4daf409febce546c6");

	// Token: 0x0400315C RID: 12636
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01.prefab:ec3c5b67bae912d42a294918a964e8ee");

	// Token: 0x0400315D RID: 12637
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01.prefab:28a05e97f0e563646b8724eeecf540fa");

	// Token: 0x0400315E RID: 12638
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01.prefab:9106889a7877a9c40901a2c3b3083d14");

	// Token: 0x0400315F RID: 12639
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01.prefab:ffbfe2c550cfb2941aa8640d9483bc2f");

	// Token: 0x04003160 RID: 12640
	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01.prefab:092644dd1cf9cb84aa78d8616bdb7146");

	// Token: 0x04003161 RID: 12641
	private List<string> m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01
	};

	// Token: 0x04003162 RID: 12642
	private List<string> m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01,
		DRGA_Evil_Fight_02.VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01
	};

	// Token: 0x04003163 RID: 12643
	private HashSet<string> m_playedLines = new HashSet<string>();
}
