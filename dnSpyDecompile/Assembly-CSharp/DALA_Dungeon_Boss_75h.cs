using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000478 RID: 1144
public class DALA_Dungeon_Boss_75h : DALA_Dungeon
{
	// Token: 0x06003DED RID: 15853 RVA: 0x00145A8C File Offset: 0x00143C8C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Death_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroChu_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroEudora_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroVessina_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Outro_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Outro_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestA_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestA_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestA_03,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_03,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestC_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestC_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestC_03,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestCFail_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_03,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_04,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestE_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestE_02,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestE_03,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_TurnOne_01,
			DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_TurnOne_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DEE RID: 15854 RVA: 0x00145D40 File Offset: 0x00143F40
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003DEF RID: 15855 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003DF0 RID: 15856 RVA: 0x00145D78 File Offset: 0x00143F78
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_75h.m_IdleLines;
	}

	// Token: 0x06003DF1 RID: 15857 RVA: 0x00145D80 File Offset: 0x00143F80
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Kriziki")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Squemlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_IntroVessina_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003DF2 RID: 15858 RVA: 0x0014603D File Offset: 0x0014423D
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestA_01, 2.5f);
			goto IL_567;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestA_02, 2.5f);
			goto IL_567;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestA_03, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_01, 2.5f);
			goto IL_567;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_02, 2.5f);
			goto IL_567;
		case 106:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_03, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestC_01, 2.5f);
			goto IL_567;
		case 108:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestC_02, 2.5f);
			goto IL_567;
		case 109:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestC_03, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 110:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestCFail_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 111:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_01, 2.5f);
			goto IL_567;
		case 112:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_02, 2.5f);
			goto IL_567;
		case 113:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_03, 2.5f);
			goto IL_567;
		case 114:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestD_04, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 115:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestE_01, 2.5f);
			goto IL_567;
		case 116:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestE_02, 2.5f);
			goto IL_567;
		case 117:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestE_03, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 118:
			GameState.Get().SetBusy(true);
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.m_BossTurnOne);
			GameState.Get().SetBusy(false);
			goto IL_567;
		case 120:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_75h.m_BossOutro);
			goto IL_567;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_567:
		yield break;
	}

	// Token: 0x06003DF3 RID: 15859 RVA: 0x00146053 File Offset: 0x00144253
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

	// Token: 0x06003DF4 RID: 15860 RVA: 0x00146069 File Offset: 0x00144269
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x04002969 RID: 10601
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Death_01.prefab:5719452f3c2d7bd4da146446835cd4f4");

	// Token: 0x0400296A RID: 10602
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_DefeatPlayer_01.prefab:35ff8e0d084bc784e9daa822eeaac10e");

	// Token: 0x0400296B RID: 10603
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_EmoteResponse_01.prefab:6bb27181428e1b148b39d58d33468e9c");

	// Token: 0x0400296C RID: 10604
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Idle_01.prefab:d60e88c764584a4408d52a2f184753f0");

	// Token: 0x0400296D RID: 10605
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Idle_02.prefab:537fd87c6991a694b94b07d96c5b6752");

	// Token: 0x0400296E RID: 10606
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Idle_03.prefab:336c5699a9cd64f4d988e753fdf9ded7");

	// Token: 0x0400296F RID: 10607
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Intro_01.prefab:0c090bb43d823e04392ab3885909c11e");

	// Token: 0x04002970 RID: 10608
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroChu_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroChu_01.prefab:9b198f36d4a787748bcb0cef5159981e");

	// Token: 0x04002971 RID: 10609
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroEudora_01.prefab:accb4417721d47a4087f4eca36f280bc");

	// Token: 0x04002972 RID: 10610
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroGeorge_01.prefab:aaa240338c0f8eb4cb6d264b5016da0a");

	// Token: 0x04002973 RID: 10611
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroKriziki_01.prefab:638cbe32be800d94394ff04a6e2e6a1c");

	// Token: 0x04002974 RID: 10612
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroOlBarkeye_01.prefab:20504d0dbbc0f6942b568bcdaf1e9acf");

	// Token: 0x04002975 RID: 10613
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroRakanishu_01.prefab:456f2d610c003d44585f063decddca7f");

	// Token: 0x04002976 RID: 10614
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroSqueamlish_01.prefab:545e847ffd649274c88d0babffc19486");

	// Token: 0x04002977 RID: 10615
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroTekahn_01.prefab:9cb7974f75507fd4e9b5cb00d5e5f52b");

	// Token: 0x04002978 RID: 10616
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_IntroVessina_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_IntroVessina_01.prefab:8425ab15488fcd244a42e191c8bdb2b7");

	// Token: 0x04002979 RID: 10617
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Outro_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Outro_01.prefab:6056ab8f83c7919499e8a47eabf0020e");

	// Token: 0x0400297A RID: 10618
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_Outro_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_Outro_02.prefab:b07cae75e6605094cb0f77453e49120c");

	// Token: 0x0400297B RID: 10619
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestA_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestA_01.prefab:b7b4c811b8cc77547994b22668d3dce1");

	// Token: 0x0400297C RID: 10620
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestA_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestA_02.prefab:d2f593dfd5c4a284086fc045ae9443ba");

	// Token: 0x0400297D RID: 10621
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestA_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestA_03.prefab:023b7a0d6fb73024496632317d626713");

	// Token: 0x0400297E RID: 10622
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestB_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestB_01.prefab:757a724cc13ac484689f50590075c45d");

	// Token: 0x0400297F RID: 10623
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestB_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestB_02.prefab:a0406fc414d662740b6043f4cc1780ab");

	// Token: 0x04002980 RID: 10624
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestB_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestB_03.prefab:9910dba6afb66bd489ac065792b8d3a6");

	// Token: 0x04002981 RID: 10625
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestC_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestC_01.prefab:9196247b7aa967a4d8a2919645458c10");

	// Token: 0x04002982 RID: 10626
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestC_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestC_02.prefab:6838bebba6e7f9940afabbc3c53bc758");

	// Token: 0x04002983 RID: 10627
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestC_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestC_03.prefab:91bde431a5340df4591ae52a7fd88d8b");

	// Token: 0x04002984 RID: 10628
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestCFail_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestCFail_01.prefab:94f2ead6d267cd54580569b870a714e9");

	// Token: 0x04002985 RID: 10629
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_01.prefab:8d8937377f62e894c9d5d1a798b32ab6");

	// Token: 0x04002986 RID: 10630
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_02.prefab:54f09f467b692084b9acc6978c2300db");

	// Token: 0x04002987 RID: 10631
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_03.prefab:5a6607d7f67cc4a4c8327a4dce91d1ca");

	// Token: 0x04002988 RID: 10632
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestD_04 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestD_04.prefab:b60b417679da7a84ab9988c14be6a776");

	// Token: 0x04002989 RID: 10633
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestE_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestE_01.prefab:efe9dc47ca872cf40bd94f688e925103");

	// Token: 0x0400298A RID: 10634
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestE_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestE_02.prefab:0c8f85429f2a648489f6f7d5f1f08777");

	// Token: 0x0400298B RID: 10635
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_QuestE_03 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_QuestE_03.prefab:e32b7bb05b7b55949a935d85767e9e58");

	// Token: 0x0400298C RID: 10636
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_TurnOne_01 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_TurnOne_01.prefab:c91c87bc6f91a504d81980729a7e6b56");

	// Token: 0x0400298D RID: 10637
	private static readonly AssetReference VO_DALA_BOSS_75h_Male_Human_TurnOne_02 = new AssetReference("VO_DALA_BOSS_75h_Male_Human_TurnOne_02.prefab:3c1abd2c1234e234c9104c1985f30b6e");

	// Token: 0x0400298E RID: 10638
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x0400298F RID: 10639
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Idle_03
	};

	// Token: 0x04002990 RID: 10640
	private static List<string> m_BossOutro = new List<string>
	{
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_QuestB_03,
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Outro_01,
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_Outro_02
	};

	// Token: 0x04002991 RID: 10641
	private static List<string> m_BossTurnOne = new List<string>
	{
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_TurnOne_01,
		DALA_Dungeon_Boss_75h.VO_DALA_BOSS_75h_Male_Human_TurnOne_02
	};
}
