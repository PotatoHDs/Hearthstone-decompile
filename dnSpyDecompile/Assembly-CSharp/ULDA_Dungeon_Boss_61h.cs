using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004BA RID: 1210
public class ULDA_Dungeon_Boss_61h : ULDA_Dungeon
{
	// Token: 0x060040F0 RID: 16624 RVA: 0x0015AA28 File Offset: 0x00158C28
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Death_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_DefeatPlayer_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Idle_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Idle_02,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Idle_03,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Intro_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01,
			ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040F1 RID: 16625 RVA: 0x0015ABAC File Offset: 0x00158DAC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x060040F2 RID: 16626 RVA: 0x0015ABB4 File Offset: 0x00158DB4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x060040F3 RID: 16627 RVA: 0x0015ABBC File Offset: 0x00158DBC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01;
	}

	// Token: 0x060040F4 RID: 16628 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040F5 RID: 16629 RVA: 0x0015ABF4 File Offset: 0x00158DF4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060040F6 RID: 16630 RVA: 0x0015ACCE File Offset: 0x00158ECE
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060040F7 RID: 16631 RVA: 0x0015ACE4 File Offset: 0x00158EE4
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
		if (cardId == "EX1_251" || cardId == "CFM_707" || cardId == "EX1_238" || cardId == "EX1_259" || cardId == "OG_206")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040F8 RID: 16632 RVA: 0x0015ACFA File Offset: 0x00158EFA
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
		if (!(cardId == "TRL_569"))
		{
			if (cardId == "TRL_362")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002F45 RID: 12101
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrackleDestroyMinion_01.prefab:5bc6075829115144bbb96ab57e30f76e");

	// Token: 0x04002F46 RID: 12102
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerCrowdRoaster_01.prefab:1c06497909716e542b140e741a337ae2");

	// Token: 0x04002F47 RID: 12103
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_BossTriggerDragonRoar_01.prefab:5e0399e358d6c6d41a01683ea59d1b72");

	// Token: 0x04002F48 RID: 12104
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Death_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Death_01.prefab:fe169976ca10faa429dbbca88f32c74c");

	// Token: 0x04002F49 RID: 12105
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_DefeatPlayer_01.prefab:4107a059264ebe14080c27d33ccf7698");

	// Token: 0x04002F4A RID: 12106
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_EmoteResponse_01.prefab:5f7e079bd698eae40b6f54e4aa12049b");

	// Token: 0x04002F4B RID: 12107
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01.prefab:7248c20cbfddbc643b53b2b69bb64936");

	// Token: 0x04002F4C RID: 12108
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03.prefab:94af211a6c463f14e9598b703bfa2930");

	// Token: 0x04002F4D RID: 12109
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04.prefab:3941fdfc471deab478c247119c1a243e");

	// Token: 0x04002F4E RID: 12110
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_HeroPowerRare_01.prefab:b91d4aa412f1a404b87385f0a930e6bb");

	// Token: 0x04002F4F RID: 12111
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Idle_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Idle_01.prefab:0ba028108f7067042a49fa48ce394793");

	// Token: 0x04002F50 RID: 12112
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Idle_02 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Idle_02.prefab:8740a575c7a413c4c8b114e6819e36cf");

	// Token: 0x04002F51 RID: 12113
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Idle_03 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Idle_03.prefab:e1104dc2e694c0f4ba7b921322f1943e");

	// Token: 0x04002F52 RID: 12114
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_Intro_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_Intro_01.prefab:ba61452640398c040ac0291e228dc5fe");

	// Token: 0x04002F53 RID: 12115
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_IntroResponseBrann_01.prefab:b8da20929ac465b4680fb17ac71c29b7");

	// Token: 0x04002F54 RID: 12116
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_PlayerDragon_01.prefab:e1ced7aaba56be44abf3eaa034774d49");

	// Token: 0x04002F55 RID: 12117
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_PlayerLightningSpell_01.prefab:224a62861ff08ad4ea789dc65194d6aa");

	// Token: 0x04002F56 RID: 12118
	private static readonly AssetReference VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_61h_Female_Dragon_TurnOne_01.prefab:83c124071c0e8a943ba105757e000d6f");

	// Token: 0x04002F57 RID: 12119
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_01,
		ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_03,
		ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_HeroPower_04
	};

	// Token: 0x04002F58 RID: 12120
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Idle_01,
		ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Idle_02,
		ULDA_Dungeon_Boss_61h.VO_ULDA_BOSS_61h_Female_Dragon_Idle_03
	};

	// Token: 0x04002F59 RID: 12121
	private HashSet<string> m_playedLines = new HashSet<string>();
}
