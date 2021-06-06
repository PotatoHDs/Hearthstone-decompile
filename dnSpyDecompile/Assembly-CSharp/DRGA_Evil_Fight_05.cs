using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public class DRGA_Evil_Fight_05 : DRGA_Dungeon
{
	// Token: 0x06004226 RID: 16934 RVA: 0x0016295C File Offset: 0x00160B5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01,
			DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_PlayerStart_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004227 RID: 16935 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004228 RID: 16936 RVA: 0x00162C20 File Offset: 0x00160E20
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_Combined_IdleLines;
	}

	// Token: 0x06004229 RID: 16937 RVA: 0x00162C28 File Offset: 0x00160E28
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_Combined_HeroPowerLines;
	}

	// Token: 0x0600422A RID: 16938 RVA: 0x00162C30 File Offset: 0x00160E30
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		int num = UnityEngine.Random.Range(1, 5);
		if (num == 1)
		{
			this.m_deathLine = DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01;
		}
		if (num == 2)
		{
			this.m_deathLine = DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01;
		}
		if (num == 3)
		{
			this.m_deathLine = DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01;
		}
		if (num == 4)
		{
			this.m_deathLine = DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01;
		}
	}

	// Token: 0x0600422B RID: 16939 RVA: 0x00162C9C File Offset: 0x00160E9C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		int num = UnityEngine.Random.Range(1, 5);
		if (emoteType == EmoteType.START)
		{
			if (num < 3)
			{
				if (!this.m_Heroic)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				}
				if (this.m_Heroic)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				}
			}
			else
			{
				if (!this.m_Heroic)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				}
				if (this.m_Heroic)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					return;
				}
			}
			return;
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (num == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (num == 2)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (num == 3)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (num == 4)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600422C RID: 16940 RVA: 0x00162E8E File Offset: 0x0016108E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01, 2.5f);
			goto IL_425;
		case 101:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01, 2.5f);
			goto IL_425;
		case 102:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01, 2.5f);
			goto IL_425;
		case 103:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01, 2.5f);
			goto IL_425;
		case 104:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01, 2.5f);
			goto IL_425;
		case 105:
			yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01, 2.5f);
			goto IL_425;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.RafaamBrassRing, DRGA_Evil_Fight_05.VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 109:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 110:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 111:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 112:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 113:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor2, DRGA_Evil_Fight_05.VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01, 2.5f);
				goto IL_425;
			}
			goto IL_425;
		case 114:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_Combined_Attack);
			goto IL_425;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_425:
		yield break;
	}

	// Token: 0x0600422D RID: 16941 RVA: 0x00162EA4 File Offset: 0x001610A4
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

	// Token: 0x0600422E RID: 16942 RVA: 0x00162EBA File Offset: 0x001610BA
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "DRGA_BOSS_27t")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01, 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040031AC RID: 12716
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_05_Turn_03_01.prefab:c8d6161eb39d39443b4e04f409f63ace");

	// Token: 0x040031AD RID: 12717
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_Death_01_01.prefab:d3edee70b27ef54408e2f583dfd1caf9");

	// Token: 0x040031AE RID: 12718
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01.prefab:b4821b4dee594e8439b7ddaf796e9ffe");

	// Token: 0x040031AF RID: 12719
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01.prefab:7c349a914815fda498636e03968aa936");

	// Token: 0x040031B0 RID: 12720
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStart_01_01.prefab:6d05e6848f8a64f4ab32ec9827fb5f8c");

	// Token: 0x040031B1 RID: 12721
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossStartHeroic_01_01.prefab:3438f2c7ebbea2348a158926ec35b0c5");

	// Token: 0x040031B2 RID: 12722
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_EmoteResponse_01_01.prefab:755db052f14bf6f4c99eb4b068c05fc9");

	// Token: 0x040031B3 RID: 12723
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01.prefab:35b229bd11396ab4aa206ea3b4f50d96");

	// Token: 0x040031B4 RID: 12724
	private static readonly AssetReference VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Misc_01_01.prefab:21f98641b434c4842a86867c5569073d");

	// Token: 0x040031B5 RID: 12725
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_Death_02_01.prefab:ecc5728c307b8914b853f93921ce9ac6");

	// Token: 0x040031B6 RID: 12726
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01.prefab:39d4fef637c1f404a8d708341d77d398");

	// Token: 0x040031B7 RID: 12727
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01.prefab:ae4ab22150cd7cc47ab20b41756c354a");

	// Token: 0x040031B8 RID: 12728
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStart_02_01.prefab:13679765a17e3da478c10bd6c877ab94");

	// Token: 0x040031B9 RID: 12729
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossStartHeroic_02_01.prefab:9e024b3bb535b8341a04ba5babcda4d7");

	// Token: 0x040031BA RID: 12730
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_EmoteResponse_02_01.prefab:7241af47027c0e44599d6c8a05ed80c1");

	// Token: 0x040031BB RID: 12731
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01.prefab:6828b7cee0c8c49489b8309f8ecdeea0");

	// Token: 0x040031BC RID: 12732
	private static readonly AssetReference VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Misc_02_01.prefab:52e409e0cba2bb749b92a06a39106dea");

	// Token: 0x040031BD RID: 12733
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_Death_03_01.prefab:9155b3651c495f845a826d0814ae6857");

	// Token: 0x040031BE RID: 12734
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01.prefab:56890e0c54f4f9647a11d2b381ac015d");

	// Token: 0x040031BF RID: 12735
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01.prefab:c1621584e1977b244ac3bae1769d4bbb");

	// Token: 0x040031C0 RID: 12736
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_EmoteResponse_03_01.prefab:0bc7f0b937072c846b072bdecde39a96");

	// Token: 0x040031C1 RID: 12737
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01.prefab:4d8e09af6f8403d418764ad44c8bb1e0");

	// Token: 0x040031C2 RID: 12738
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_03_01.prefab:6e753897c88077242992c60441126620");

	// Token: 0x040031C3 RID: 12739
	private static readonly AssetReference VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Misc_05_01.prefab:0928fae526e8a214fa129f0866ab3022");

	// Token: 0x040031C4 RID: 12740
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_Death_04_01.prefab:35d6fd187c2613c409be58a5db67c0f0");

	// Token: 0x040031C5 RID: 12741
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01.prefab:8a2227b10408fb74fa8d8b9213a97ddc");

	// Token: 0x040031C6 RID: 12742
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01.prefab:5f1caf82ee6af944d91aafd8e99be0a4");

	// Token: 0x040031C7 RID: 12743
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_EmoteResponse_04_01.prefab:59b4e16350a790d47ae92cf59cf12bcd");

	// Token: 0x040031C8 RID: 12744
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01.prefab:f81e9686a73ec8f41a1170061b8608d5");

	// Token: 0x040031C9 RID: 12745
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_04_01.prefab:90c4ad759d620ab4ba76d91cbddcc304");

	// Token: 0x040031CA RID: 12746
	private static readonly AssetReference VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Misc_06_01.prefab:ec474c4bdd8b9b244983c4afdd665ed0");

	// Token: 0x040031CB RID: 12747
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_01_01.prefab:6b9e049b82a6edd409bf01c4ff21fc70");

	// Token: 0x040031CC RID: 12748
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_02_01.prefab:c9c164090d0e08a4e894069fdd225339");

	// Token: 0x040031CD RID: 12749
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_03_01.prefab:31bec908ca45f7741ad178c2e7e39a6f");

	// Token: 0x040031CE RID: 12750
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_04_01.prefab:4b983edb3a288db4b9cca2aad5566e5e");

	// Token: 0x040031CF RID: 12751
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_05_01.prefab:8e2b0e2f04fe6604f97f40733974d945");

	// Token: 0x040031D0 RID: 12752
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_Misc_06_01.prefab:0a952a233bfcbe640afe9cd7a3908152");

	// Token: 0x040031D1 RID: 12753
	private static readonly AssetReference VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_28h_Male_Tauren_Evil_Fight_05_PlayerStart_01.prefab:48f3e06e887eeee46843b3b6a198a165");

	// Token: 0x040031D2 RID: 12754
	private List<string> m_VO_DRGA_BOSS_Combined_HeroPowerLines = new List<string>
	{
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Boss_HeroPower_01_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Boss_HeroPower_02_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Boss_HeroPower_03_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Boss_HeroPower_04_01
	};

	// Token: 0x040031D3 RID: 12755
	private List<string> m_VO_DRGA_BOSS_Combined_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_Idle_01_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_Idle_02_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_Idle_03_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_Idle_04_01
	};

	// Token: 0x040031D4 RID: 12756
	private List<string> m_VO_DRGA_BOSS_Combined_Attack = new List<string>
	{
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27ha_Male_Dwarf_Evil_Fight_05_BossAttack_01_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hb_Female_NightElf_Evil_Fight_05_BossAttack_02_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hc_Female_Human_Evil_Fight_05_BossAttack_03_01,
		DRGA_Evil_Fight_05.VO_DRGA_BOSS_27hd_Male_Gnome_Evil_Fight_05_BossAttack_04_01
	};

	// Token: 0x040031D5 RID: 12757
	private HashSet<string> m_playedLines = new HashSet<string>();
}
