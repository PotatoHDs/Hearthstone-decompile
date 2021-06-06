using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004B0 RID: 1200
public class ULDA_Dungeon_Boss_51h : ULDA_Dungeon
{
	// Token: 0x0600408A RID: 16522 RVA: 0x001587F8 File Offset: 0x001569F8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Death_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_DefeatPlayer_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_IntroSpecial_Elise_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01,
			ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600408B RID: 16523 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600408C RID: 16524 RVA: 0x0015896C File Offset: 0x00156B6C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600408D RID: 16525 RVA: 0x00158974 File Offset: 0x00156B74
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600408E RID: 16526 RVA: 0x0015897C File Offset: 0x00156B7C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01;
	}

	// Token: 0x0600408F RID: 16527 RVA: 0x001589B4 File Offset: 0x00156BB4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START && cardId != "ULDA_Elise")
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

	// Token: 0x06004090 RID: 16528 RVA: 0x00158A5F File Offset: 0x00156C5F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004091 RID: 16529 RVA: 0x00158A75 File Offset: 0x00156C75
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
		if (!(cardId == "ULD_186"))
		{
			if (!(cardId == "ULD_138"))
			{
				if (cardId == "ULD_271")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004092 RID: 16530 RVA: 0x00158A8B File Offset: 0x00156C8B
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
		if (!(cardId == "ULD_138"))
		{
			if (!(cardId == "ULD_265"))
			{
				if (cardId == "ULD_268")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002EA7 RID: 11943
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerAnubisathDefender_01.prefab:3e2dfe63028792941a30fd3c818caba6");

	// Token: 0x04002EA8 RID: 11944
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerEmbalmingRitual_01.prefab:08aef91a3a8b66244bd306daea060474");

	// Token: 0x04002EA9 RID: 11945
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_BossTriggerPsychopomp_01.prefab:2b2bc6b4297687f43bde99e100bb5ceb");

	// Token: 0x04002EAA RID: 11946
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Death_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Death_01.prefab:60891177533b5934ea4d499152eef7c9");

	// Token: 0x04002EAB RID: 11947
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_DefeatPlayer_01.prefab:c34d561cc66605d4b95fe836ccc523b0");

	// Token: 0x04002EAC RID: 11948
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_EmoteResponse_01.prefab:32ba78c9a0364de48b8abf823ef603aa");

	// Token: 0x04002EAD RID: 11949
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01.prefab:49940cd11cadb5041b6431367c43d164");

	// Token: 0x04002EAE RID: 11950
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02.prefab:a384f835f507a0349b2ac39ef70e2e34");

	// Token: 0x04002EAF RID: 11951
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03.prefab:a146aca8a334e684c873d0f3da19a73e");

	// Token: 0x04002EB0 RID: 11952
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05.prefab:587a74d18825d9a4bbdc6b0349cd9210");

	// Token: 0x04002EB1 RID: 11953
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01.prefab:49fd34f7c4ba8f54e91406a4a38ac49c");

	// Token: 0x04002EB2 RID: 11954
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03.prefab:5d0d8db1387ffdd458f6fad072550935");

	// Token: 0x04002EB3 RID: 11955
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_Intro_01.prefab:ecb6ded326712904889601681e0a5f60");

	// Token: 0x04002EB4 RID: 11956
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_IntroSpecial_Elise_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_IntroSpecial_Elise_01.prefab:54ea30eff03a410458187db325b3ba34");

	// Token: 0x04002EB5 RID: 11957
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Anubisath_Defender_01.prefab:ba414294e85df514990c7a501dbb0ed7");

	// Token: 0x04002EB6 RID: 11958
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Enslaved_Guardian_01.prefab:12e1b3782b0072843858d63157d2b12a");

	// Token: 0x04002EB7 RID: 11959
	private static readonly AssetReference VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01 = new AssetReference("VO_ULDA_BOSS_51h_Female_Anubisath_PlayerTrigger_Pharaoh_Cat_01.prefab:0c73fa01c1b00d14f82f99c0e09039d3");

	// Token: 0x04002EB8 RID: 11960
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_01,
		ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_02,
		ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_03,
		ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_HeroPower_05
	};

	// Token: 0x04002EB9 RID: 11961
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Idle_01,
		ULDA_Dungeon_Boss_51h.VO_ULDA_BOSS_51h_Female_Anubisath_Idle_03
	};

	// Token: 0x04002EBA RID: 11962
	private HashSet<string> m_playedLines = new HashSet<string>();
}
