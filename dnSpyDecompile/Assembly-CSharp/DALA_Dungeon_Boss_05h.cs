using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000432 RID: 1074
public class DALA_Dungeon_Boss_05h : DALA_Dungeon
{
	// Token: 0x06003A86 RID: 14982 RVA: 0x0012E578 File Offset: 0x0012C778
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Death_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_DefeatPlayer_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Idle_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Idle_02,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Idle_03,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Intro_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A87 RID: 14983 RVA: 0x0012E6EC File Offset: 0x0012C8EC
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03,
			DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04
		};
	}

	// Token: 0x06003A88 RID: 14984 RVA: 0x0012E73E File Offset: 0x0012C93E
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x0012E746 File Offset: 0x0012C946
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01;
	}

	// Token: 0x06003A8A RID: 14986 RVA: 0x0012E780 File Offset: 0x0012C980
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish" && cardId != "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
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

	// Token: 0x06003A8B RID: 14987 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003A8C RID: 14988 RVA: 0x0012E8B3 File Offset: 0x0012CAB3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossBigSpells);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerBigSpells);
		}
		yield break;
	}

	// Token: 0x06003A8D RID: 14989 RVA: 0x0012E8C9 File Offset: 0x0012CAC9
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

	// Token: 0x06003A8E RID: 14990 RVA: 0x0012E8DF File Offset: 0x0012CADF
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

	// Token: 0x0400225F RID: 8799
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01.prefab:e933cf5fc48346d43b8b7173ee8330e4");

	// Token: 0x04002260 RID: 8800
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02.prefab:5e263a60c6b8b9e4cb9af33a0d785fef");

	// Token: 0x04002261 RID: 8801
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Death_01.prefab:17ec26e483d12d54e819ea3a462f7df8");

	// Token: 0x04002262 RID: 8802
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_DefeatPlayer_01.prefab:4bcb35aad3ffc744bbdf32332ae01018");

	// Token: 0x04002263 RID: 8803
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_EmoteResponse_01.prefab:93730ba56c731f741a95a957acf59553");

	// Token: 0x04002264 RID: 8804
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_01.prefab:054838c9417dfc046878fc064a8a7157");

	// Token: 0x04002265 RID: 8805
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_02.prefab:32d12a2d9070dd84d9372fd11c9bab7f");

	// Token: 0x04002266 RID: 8806
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_03.prefab:0510798abcee96845a0ba543afbb8005");

	// Token: 0x04002267 RID: 8807
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_HeroPower_04.prefab:89b9a7236aa55cf4ea9b14401beb29d5");

	// Token: 0x04002268 RID: 8808
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Idle_01.prefab:9fa4a74ff20dda247a9ad5b1392be813");

	// Token: 0x04002269 RID: 8809
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Idle_02 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Idle_02.prefab:24d6483a3608bd04e8a647638af21b63");

	// Token: 0x0400226A RID: 8810
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Idle_03.prefab:4576b87a20ace514cb5c83250093e3f9");

	// Token: 0x0400226B RID: 8811
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_Intro_01.prefab:fe467453afc2d7342bcb1ce9df93f975");

	// Token: 0x0400226C RID: 8812
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_IntroGeorge_01.prefab:ce8f52baa057be24f834c9911002f2a6");

	// Token: 0x0400226D RID: 8813
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_IntroOlBarkeye_01.prefab:2940e5f61feaf4540846764efa5fc0fa");

	// Token: 0x0400226E RID: 8814
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01.prefab:237152cff25430f4898e8b6972aa2dd1");

	// Token: 0x0400226F RID: 8815
	private static readonly AssetReference VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03 = new AssetReference("VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03.prefab:6bc46825284fdb9459e135e616510045");

	// Token: 0x04002270 RID: 8816
	private List<string> m_PlayerBigSpells = new List<string>
	{
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_01,
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_PlayerBigSpell_03
	};

	// Token: 0x04002271 RID: 8817
	private List<string> m_BossBigSpells = new List<string>
	{
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_01,
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_BossBigSpell_02
	};

	// Token: 0x04002272 RID: 8818
	private List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Idle_01,
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Idle_02,
		DALA_Dungeon_Boss_05h.VO_DALA_BOSS_05h_Male_Gnome_Idle_03
	};

	// Token: 0x04002273 RID: 8819
	private HashSet<string> m_playedLines = new HashSet<string>();
}
