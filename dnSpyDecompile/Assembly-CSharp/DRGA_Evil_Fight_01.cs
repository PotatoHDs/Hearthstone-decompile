using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004CF RID: 1231
public class DRGA_Evil_Fight_01 : DRGA_Dungeon
{
	// Token: 0x060041F1 RID: 16881 RVA: 0x00161178 File Offset: 0x0015F378
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01,
			DRGA_Evil_Fight_01.VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060041F2 RID: 16882 RVA: 0x0016132C File Offset: 0x0015F52C
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_IdleLines;
	}

	// Token: 0x060041F3 RID: 16883 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060041F4 RID: 16884 RVA: 0x00161334 File Offset: 0x0015F534
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPowerLines;
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x0016133C File Offset: 0x0015F53C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x060041F6 RID: 16886 RVA: 0x00161344 File Offset: 0x0015F544
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x060041F7 RID: 16887 RVA: 0x00161455 File Offset: 0x0015F655
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
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_01.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01, 2.5f);
				goto IL_399;
			}
			goto IL_399;
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01, 2.5f);
			goto IL_399;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_399;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01, 2.5f);
				goto IL_399;
			}
			goto IL_399;
		case 107:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01, 2.5f);
			goto IL_399;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01, 2.5f);
				goto IL_399;
			}
			goto IL_399;
		case 109:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01, 2.5f);
			goto IL_399;
		case 110:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01, 2.5f);
			goto IL_399;
		case 111:
			yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01, 2.5f);
			goto IL_399;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_399:
		yield break;
	}

	// Token: 0x060041F8 RID: 16888 RVA: 0x0016146B File Offset: 0x0015F66B
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

	// Token: 0x060041F9 RID: 16889 RVA: 0x00161481 File Offset: 0x0015F681
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

	// Token: 0x0400312C RID: 12588
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_Death_01.prefab:c0b6294a7f3565e46a0bcf6ba465690d");

	// Token: 0x0400312D RID: 12589
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01.prefab:3f451d515cd3d7141bd33a354a3a21bb");

	// Token: 0x0400312E RID: 12590
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01.prefab:3f57520d4e98fe64cadbcbee11cc78f7");

	// Token: 0x0400312F RID: 12591
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01.prefab:2dfaf6f49d934a943ae1ceda9a6edb95");

	// Token: 0x04003130 RID: 12592
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossAttack_01.prefab:c939a890e0ca0b841afb7e621d18ea16");

	// Token: 0x04003131 RID: 12593
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStart_01.prefab:e0fb30ba5b7c8f143b73db19ae80d81b");

	// Token: 0x04003132 RID: 12594
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_BossStartHeroic_01.prefab:656e8f68d1e94a9439305c82f54f0fb4");

	// Token: 0x04003133 RID: 12595
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponse_01.prefab:caf7ced67c43a26438ceacf5b1dc51b2");

	// Token: 0x04003134 RID: 12596
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_EmoteResponseHeroic_01.prefab:72fad61b9efeab74d9eea748d0386474");

	// Token: 0x04003135 RID: 12597
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_01_01.prefab:601f126cecc8c004389d25f7499e3247");

	// Token: 0x04003136 RID: 12598
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Finely_Bubblehearth_02_01.prefab:47163ff23d2c5414389b6ba9c704d91a");

	// Token: 0x04003137 RID: 12599
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01.prefab:9c51fdf75b4ca1e418b859479a7bbf3f");

	// Token: 0x04003138 RID: 12600
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01.prefab:2bb7f600b5e3a694d88dfa5703366bfe");

	// Token: 0x04003139 RID: 12601
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01.prefab:b0a5d0ccc1642f743b35ee475eb9cc48");

	// Token: 0x0400313A RID: 12602
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_01a_01.prefab:c0f6dc96e459df14d905836244f6184f");

	// Token: 0x0400313B RID: 12603
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_02_01.prefab:9623c241e058e764fb882ff123d04d96");

	// Token: 0x0400313C RID: 12604
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_03_01.prefab:397f10ad117297f46961de5b10413eb6");

	// Token: 0x0400313D RID: 12605
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_04_01.prefab:50dc292c739a2d14e96c8a90a50c59df");

	// Token: 0x0400313E RID: 12606
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Misc_05_01.prefab:d94140d723c2f2a48999c7b268ec21c8");

	// Token: 0x0400313F RID: 12607
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_01_Turn_02_01.prefab:b1c4c16dc36290a49be1d0a399b2c0df");

	// Token: 0x04003140 RID: 12608
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Evil_Fight_01_Misc_01b_01.prefab:c2596741bfce6a647bb8a77529798d0b");

	// Token: 0x04003141 RID: 12609
	private List<string> m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_01_01,
		DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_02_01,
		DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Boss_HeroPower_03_01
	};

	// Token: 0x04003142 RID: 12610
	private List<string> m_VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_01_01,
		DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_02_01,
		DRGA_Evil_Fight_01.VO_DRGA_BOSS_03h_Male_Murloc_Evil_Fight_01_Idle_03_01
	};

	// Token: 0x04003143 RID: 12611
	private HashSet<string> m_playedLines = new HashSet<string>();
}
