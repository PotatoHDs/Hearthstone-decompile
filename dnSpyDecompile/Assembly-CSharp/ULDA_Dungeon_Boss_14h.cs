using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200048B RID: 1163
public class ULDA_Dungeon_Boss_14h : ULDA_Dungeon
{
	// Token: 0x06003ED1 RID: 16081 RVA: 0x0014D394 File Offset: 0x0014B594
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_BossPoisonWeapon_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_BossStolenSteel_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Death_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_DeathALT_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_DefeatPlayer_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_EmoteResponse_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_02,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_02,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_03,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Idle_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Idle_02,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Idle_03,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Intro_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_PlayerDestroyWeapon_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_PlayerKingsbane_01,
			ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_PlayerStolenSteel_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003ED2 RID: 16082 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003ED3 RID: 16083 RVA: 0x0014D518 File Offset: 0x0014B718
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003ED4 RID: 16084 RVA: 0x0014D520 File Offset: 0x0014B720
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_EmoteResponse_01;
	}

	// Token: 0x06003ED5 RID: 16085 RVA: 0x0014D558 File Offset: 0x0014B758
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

	// Token: 0x06003ED6 RID: 16086 RVA: 0x0014D5E1 File Offset: 0x0014B7E1
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerTriggerLines);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerPlayShuffledWeaponsLines);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_PlayerDestroyWeapon_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003ED7 RID: 16087 RVA: 0x0014D5F7 File Offset: 0x0014B7F7
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
		if (!(cardId == "LOOT_542"))
		{
			if (cardId == "TRL_156")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_PlayerStolenSteel_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_PlayerKingsbane_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003ED8 RID: 16088 RVA: 0x0014D60D File Offset: 0x0014B80D
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
		if (!(cardId == "CS2_074"))
		{
			if (cardId == "TRL_156")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_BossStolenSteel_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_BossPoisonWeapon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B55 RID: 11093
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_BossPoisonWeapon_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_BossPoisonWeapon_01.prefab:f4027680ee899b149b43d5db73bd9b43");

	// Token: 0x04002B56 RID: 11094
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_BossStolenSteel_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_BossStolenSteel_01.prefab:33a3a3bd1a15fca49a97649753868ac5");

	// Token: 0x04002B57 RID: 11095
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_Death_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_Death_01.prefab:607940c64b781ca4a89437e6c9ad888b");

	// Token: 0x04002B58 RID: 11096
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_DeathALT_01.prefab:5b05801540a9fd4428d3447edab589e4");

	// Token: 0x04002B59 RID: 11097
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_DefeatPlayer_01.prefab:bb91a3b9dffc0424096097091d23232b");

	// Token: 0x04002B5A RID: 11098
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_EmoteResponse_01.prefab:d9891ebde053f3a42a9deb6393f52cce");

	// Token: 0x04002B5B RID: 11099
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_01.prefab:e16f2345bbd61ee439b44aefdc111026");

	// Token: 0x04002B5C RID: 11100
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_02 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_02.prefab:5b153a9dc205b764196d2cc93f777f93");

	// Token: 0x04002B5D RID: 11101
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_01.prefab:734128a7941a0fe4c8be600a6858f2b3");

	// Token: 0x04002B5E RID: 11102
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_02 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_02.prefab:11a5db4d6f820b241ad8570f2fc3e3e2");

	// Token: 0x04002B5F RID: 11103
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_03 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_03.prefab:e35b47ef5762d1845baf42cdb479d4ee");

	// Token: 0x04002B60 RID: 11104
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_Idle_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_Idle_01.prefab:0b4433893f0165c479d109835b61dc7e");

	// Token: 0x04002B61 RID: 11105
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_Idle_02 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_Idle_02.prefab:a5a6f0372a1612e49a5d4d00c1118ea5");

	// Token: 0x04002B62 RID: 11106
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_Idle_03 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_Idle_03.prefab:61840672e1922d44e96c5211bfcc1ffe");

	// Token: 0x04002B63 RID: 11107
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_Intro_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_Intro_01.prefab:fa9042398dc64744a87ebbf9ef9e2fa8");

	// Token: 0x04002B64 RID: 11108
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_PlayerDestroyWeapon_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_PlayerDestroyWeapon_01.prefab:a0e91f0f7e0e3654e817defda411a009");

	// Token: 0x04002B65 RID: 11109
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_PlayerKingsbane_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_PlayerKingsbane_01.prefab:3e5552f032facd5459fd184fa78e35c5");

	// Token: 0x04002B66 RID: 11110
	private static readonly AssetReference VO_ULDA_BOSS_14h_Female_Sethrak_PlayerStolenSteel_01 = new AssetReference("VO_ULDA_BOSS_14h_Female_Sethrak_PlayerStolenSteel_01.prefab:78c415b4ee63479488fae89fd2f047cb");

	// Token: 0x04002B67 RID: 11111
	private List<string> m_HeroPowerPlayShuffledWeaponsLines = new List<string>
	{
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_01,
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerPlayShuffledWeapons_02
	};

	// Token: 0x04002B68 RID: 11112
	private List<string> m_HeroPowerTriggerLines = new List<string>
	{
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_01,
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_02,
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_HeroPowerTrigger_03
	};

	// Token: 0x04002B69 RID: 11113
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Idle_01,
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Idle_02,
		ULDA_Dungeon_Boss_14h.VO_ULDA_BOSS_14h_Female_Sethrak_Idle_03
	};

	// Token: 0x04002B6A RID: 11114
	private HashSet<string> m_playedLines = new HashSet<string>();
}
