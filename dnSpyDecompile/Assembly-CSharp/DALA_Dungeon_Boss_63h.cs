using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200046C RID: 1132
public class DALA_Dungeon_Boss_63h : DALA_Dungeon
{
	// Token: 0x06003D5C RID: 15708 RVA: 0x00141E30 File Offset: 0x00140030
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Death_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_DefeatPlayer_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_02,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_03,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_04,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_06,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_07,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_08,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Idle_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Idle_02,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Idle_03,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Intro_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01,
			DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D5D RID: 15709 RVA: 0x00142004 File Offset: 0x00140204
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01;
	}

	// Token: 0x06003D5E RID: 15710 RVA: 0x0014203C File Offset: 0x0014023C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_63h.m_IdleLines;
	}

	// Token: 0x06003D5F RID: 15711 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D60 RID: 15712 RVA: 0x00142044 File Offset: 0x00140244
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora" && cardId != "DALA_Chu" && cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003D61 RID: 15713 RVA: 0x00142109 File Offset: 0x00140309
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_63h.m_HeroPower);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_63h.m_HeroPowerRare);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D62 RID: 15714 RVA: 0x0014211F File Offset: 0x0014031F
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "CFM_669"))
		{
			if (!(cardId == "TRL_321"))
			{
				if (cardId == "EX1_607")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D63 RID: 15715 RVA: 0x00142135 File Offset: 0x00140335
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "EX1_407")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002831 RID: 10289
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_BossBigWeapon_01.prefab:9784661d7e311d849bf1835ef8179ad7");

	// Token: 0x04002832 RID: 10290
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_BossBrawl_01.prefab:8dff18608f17a894b8cb74d0c039e9e4");

	// Token: 0x04002833 RID: 10291
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Death_01.prefab:1a4a883ab3a4fe04d95aabd6d04bf895");

	// Token: 0x04002834 RID: 10292
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_DefeatPlayer_01.prefab:bbd0d481210cce443bd5039f7d45678d");

	// Token: 0x04002835 RID: 10293
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_EmoteResponse_01.prefab:9de198d6fd5a14f4caa17ca7a28e87ea");

	// Token: 0x04002836 RID: 10294
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_01.prefab:cdbef254005d58e4f90ddc35bfb49d51");

	// Token: 0x04002837 RID: 10295
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_02 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_02.prefab:056b67e688cc12e4ca622361ab47b20f");

	// Token: 0x04002838 RID: 10296
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_03 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_03.prefab:05bf56009309d504a977a4ad167583f1");

	// Token: 0x04002839 RID: 10297
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_04 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_04.prefab:488c9e1d211e6844ba412e8534832820");

	// Token: 0x0400283A RID: 10298
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_06 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_06.prefab:00d17bdb4f9ea7948af3055891fba90b");

	// Token: 0x0400283B RID: 10299
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_07 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_07.prefab:f8a993b2f7342074a8437ae9a20b53fa");

	// Token: 0x0400283C RID: 10300
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPower_08 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPower_08.prefab:440cdcfdab0e5a3449c10a29ad26361b");

	// Token: 0x0400283D RID: 10301
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01.prefab:8e1bdf0f90b1cce45a05f3cc8a997d14");

	// Token: 0x0400283E RID: 10302
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02.prefab:98b3e8862b840154d809e0ae7a33b855");

	// Token: 0x0400283F RID: 10303
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03.prefab:8cff32844530f6042ad345db5dc2ccec");

	// Token: 0x04002840 RID: 10304
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Idle_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Idle_01.prefab:0ef683245d33d974cacc58065fcb6e0b");

	// Token: 0x04002841 RID: 10305
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Idle_02.prefab:14a8caa11d7b0c640a74fe8307987266");

	// Token: 0x04002842 RID: 10306
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Idle_03.prefab:391b87c4109fbf74699041f93c5b1107");

	// Token: 0x04002843 RID: 10307
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_Intro_01.prefab:656ea1eb30a094f448ef27cb5f474710");

	// Token: 0x04002844 RID: 10308
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerBurglyBully_01.prefab:e6dc51ef6513a0545a7fcb58e286d892");

	// Token: 0x04002845 RID: 10309
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerDevestate_01.prefab:17171b8a17cf96641ae127666f5a6d64");

	// Token: 0x04002846 RID: 10310
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerDragon_01.prefab:19d54d88746302f4091eece73b5092b3");

	// Token: 0x04002847 RID: 10311
	private static readonly AssetReference VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01 = new AssetReference("VO_DALA_BOSS_63h_Male_Orc_PlayerInnerRage_01.prefab:4aafbb8a6461e3f41b1e55d5dfb993d4");

	// Token: 0x04002848 RID: 10312
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Idle_01,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Idle_02,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_Idle_03
	};

	// Token: 0x04002849 RID: 10313
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_01,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_02,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_03,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_04,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_06,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_07,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPower_08
	};

	// Token: 0x0400284A RID: 10314
	private static List<string> m_HeroPowerRare = new List<string>
	{
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_01,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_02,
		DALA_Dungeon_Boss_63h.VO_DALA_BOSS_63h_Male_Orc_HeroPowerRare_03
	};

	// Token: 0x0400284B RID: 10315
	private HashSet<string> m_playedLines = new HashSet<string>();
}
