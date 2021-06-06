using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DE RID: 1246
public class DRGA_Good_Fight_04 : DRGA_Dungeon
{
	// Token: 0x060042C2 RID: 17090 RVA: 0x00167D1C File Offset: 0x00165F1C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_04.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_PlayerStart_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_21h_Death,
			DRGA_Good_Fight_04.VO_DRGA_BOSS_21h_EmoteResponse
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042C3 RID: 17091 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060042C4 RID: 17092 RVA: 0x00167FB0 File Offset: 0x001661B0
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_IdleLines;
	}

	// Token: 0x060042C5 RID: 17093 RVA: 0x00167FB8 File Offset: 0x001661B8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPowerLines;
	}

	// Token: 0x060042C6 RID: 17094 RVA: 0x00167FC0 File Offset: 0x001661C0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_04.VO_DRGA_BOSS_21h_Death;
	}

	// Token: 0x060042C7 RID: 17095 RVA: 0x00167FD8 File Offset: 0x001661D8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_09b")
			{
				Gameplay.Get().StartCoroutine(this.PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
				return;
			}
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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
			if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() == "DRGA_BOSS_21h")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_04.VO_DRGA_BOSS_21h_EmoteResponse, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060042C8 RID: 17096 RVA: 0x00168131 File Offset: 0x00166331
	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		yield return base.PlayLineAlways(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01, 2.5f);
		yield break;
	}

	// Token: 0x060042C9 RID: 17097 RVA: 0x00168140 File Offset: 0x00166340
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_04.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3DA;
			}
			goto IL_3DA;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01, 2.5f);
			goto IL_3DA;
		case 105:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01, 2.5f);
			goto IL_3DA;
		case 106:
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01, 2.5f);
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01, 2.5f);
				goto IL_3DA;
			}
			goto IL_3DA;
		case 108:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01, 2.5f);
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01, 2.5f);
			}
			GameState.Get().SetBusy(false);
			goto IL_3DA;
		case 110:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_04.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01, 2.5f);
				goto IL_3DA;
			}
			goto IL_3DA;
		case 111:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01, 2.5f);
				goto IL_3DA;
			}
			goto IL_3DA;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3DA:
		yield break;
	}

	// Token: 0x060042CA RID: 17098 RVA: 0x00168156 File Offset: 0x00166356
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "DRGA_BOSS_03t2"))
		{
			if (cardId == "OG_133" || cardId == "OG_280" || cardId == "OG_042" || cardId == "OG_134")
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01, 2.5f);
			}
		}
		else if (!this.m_Heroic)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor2, this.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths);
		}
		yield break;
	}

	// Token: 0x060042CB RID: 17099 RVA: 0x0016816C File Offset: 0x0016636C
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
		if (!(cardId == "DRGA_BOSS_09t"))
		{
			if (!(cardId == "DRGA_BOSS_09t2"))
			{
				if (cardId == "DAL_011")
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_PuppeteerLines);
		}
		yield break;
	}

	// Token: 0x060042CC RID: 17100 RVA: 0x00168184 File Offset: 0x00166384
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (thinkEmoteBossThinkChancePercentage > num && this.m_BossIdleLines != null && this.m_BossIdleLines.Count != 0 && cardId == "DRGA_BOSS_09h")
		{
			string line = base.PopRandomLine(this.m_BossIdleLinesCopy);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x0400337B RID: 13179
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Backstory_01a_01.prefab:69810d20104aa2542bb3edd88608dbd0");

	// Token: 0x0400337C RID: 13180
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_04_Misc_01b_01.prefab:a47af84dbc2216c42b8b3fef7338870b");

	// Token: 0x0400337D RID: 13181
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Backstory_01b_01.prefab:48c9bee7aa305a040af19d5baece49b4");

	// Token: 0x0400337E RID: 13182
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01.prefab:757d9027ca998734fb7bec71dc3d3061");

	// Token: 0x0400337F RID: 13183
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01.prefab:df5ee5dc38227e845a94e23b95ddc48a");

	// Token: 0x04003380 RID: 13184
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning03_01.prefab:885a72732e1bb274d9a029e9f8331db6");

	// Token: 0x04003381 RID: 13185
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_Summoning05_01.prefab:b5a573bc5147690419a1f7372f724732");

	// Token: 0x04003382 RID: 13186
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01a_01.prefab:e0dbe86245f17b640aec423e8e8a812f");

	// Token: 0x04003383 RID: 13187
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_01c_01.prefab:4ae3feef3d7e68d43883dda605245139");

	// Token: 0x04003384 RID: 13188
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_Misc_02_01.prefab:2d6785adc93516942acaf838f508107c");

	// Token: 0x04003385 RID: 13189
	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_PlayerStart_01.prefab:8224ce413ab32b647950f0d31d9b3940");

	// Token: 0x04003386 RID: 13190
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01.prefab:8236a017ec144f34eacb62a97003856e");

	// Token: 0x04003387 RID: 13191
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01.prefab:34a248c7f9163a94e97b15597319d46d");

	// Token: 0x04003388 RID: 13192
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01.prefab:7705d8f93dd73d2408d99c783ccefbfb");

	// Token: 0x04003389 RID: 13193
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01.prefab:ec94ed7f21163a9469fdba513ed0159e");

	// Token: 0x0400338A RID: 13194
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01.prefab:40c1776c2068e0643b3cd268f676ff2b");

	// Token: 0x0400338B RID: 13195
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01.prefab:87a472deab5cefc43ac6678321db273c");

	// Token: 0x0400338C RID: 13196
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01.prefab:dc0a46965c9592f49bdf5aa7ad5f0ff7");

	// Token: 0x0400338D RID: 13197
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossAttack_01.prefab:1b757fe11d7cdcf4ba767d6e1c21112e");

	// Token: 0x0400338E RID: 13198
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStart_01.prefab:86d1ac0437efe9a44822a98f609f8c4d");

	// Token: 0x0400338F RID: 13199
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartHeroic_01.prefab:77c46ee8d317d2945a3f150269277379");

	// Token: 0x04003390 RID: 13200
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_BossStartLazul_01.prefab:8b259bcb432f5b744a40658e76b87e3f");

	// Token: 0x04003391 RID: 13201
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EmoteResponse_01.prefab:a487db55467fbd24aa5b727cdda5e9ef");

	// Token: 0x04003392 RID: 13202
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_OldGodExperiments_01.prefab:ed3cf66281223164b99f1528b36deb74");

	// Token: 0x04003393 RID: 13203
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning01_01.prefab:b19755eeee34b3f4d97548af6e03644f");

	// Token: 0x04003394 RID: 13204
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning02_01.prefab:0aad0f9d90133724681f6c8c5476fc73");

	// Token: 0x04003395 RID: 13205
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_EVENT_Summoning04_01.prefab:5bd6f11fef0ac574a96655f1a56c89bd");

	// Token: 0x04003396 RID: 13206
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01.prefab:70e3d1f1ead929549b85bf1417f5b91e");

	// Token: 0x04003397 RID: 13207
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01.prefab:4a7782f41958fbc4fa7fd9241ae32573");

	// Token: 0x04003398 RID: 13208
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01.prefab:ad5daff9579e2ad47a5e8bf4869489de");

	// Token: 0x04003399 RID: 13209
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_LazulScheme_01.prefab:498abc279c875a644a6cb292e6d459a9");

	// Token: 0x0400339A RID: 13210
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Player_OldGod_01.prefab:d0418048f8e472643adbe94328e6d01a");

	// Token: 0x0400339B RID: 13211
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_PlayerStartLazul_01.prefab:c3dda8ac07b82f146bc7cbc7e3d0e306");

	// Token: 0x0400339C RID: 13212
	private static readonly AssetReference VO_DRGA_BOSS_21h_Death = new AssetReference("VO_DRGA_BOSS_21h_Death.prefab:0e2b57b395058e849a2c28126fca70cb");

	// Token: 0x0400339D RID: 13213
	private static readonly AssetReference VO_DRGA_BOSS_21h_EmoteResponse = new AssetReference("VO_DRGA_BOSS_21h_EmoteResponse.prefab:39fe53c83cb639a46a1fc58d281fd3c3");

	// Token: 0x0400339E RID: 13214
	private List<string> m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_01_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_02_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_HeroPower_03_01
	};

	// Token: 0x0400339F RID: 13215
	private List<string> m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_PuppeteerLines = new List<string>
	{
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_01_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_02_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_03_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Boss_Puppeteer_04_01
	};

	// Token: 0x040033A0 RID: 13216
	private List<string> m_VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_IdleLines = new List<string>
	{
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_01_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_02_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_09h_Female_Troll_Good_Fight_04_Idle_03_01
	};

	// Token: 0x040033A1 RID: 13217
	private List<string> VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths = new List<string>
	{
		DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths01_01,
		DRGA_Good_Fight_04.VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_04_EVENT_FromTheDepths02_01
	};

	// Token: 0x040033A2 RID: 13218
	private HashSet<string> m_playedLines = new HashSet<string>();
}
