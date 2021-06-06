using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000497 RID: 1175
public class ULDA_Dungeon_Boss_26h : ULDA_Dungeon
{
	// Token: 0x06003F5B RID: 16219 RVA: 0x0014FEC4 File Offset: 0x0014E0C4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_DefeatPlayer_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGatlingWandTreasure_01,
			ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F5C RID: 16220 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F5D RID: 16221 RVA: 0x00150038 File Offset: 0x0014E238
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F5E RID: 16222 RVA: 0x00150040 File Offset: 0x0014E240
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F5F RID: 16223 RVA: 0x00150048 File Offset: 0x0014E248
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01;
	}

	// Token: 0x06003F60 RID: 16224 RVA: 0x00150080 File Offset: 0x0014E280
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003F61 RID: 16225 RVA: 0x0015015A File Offset: 0x0014E35A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003F62 RID: 16226 RVA: 0x00150170 File Offset: 0x0014E370
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
		if (!(cardId == "GVG_119"))
		{
			if (cardId == "ULDA_115")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F63 RID: 16227 RVA: 0x00150186 File Offset: 0x0014E386
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
		if (!(cardId == "GVG_082"))
		{
			if (cardId == "ULDA_207")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C16 RID: 11286
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_BossClockworkGnome_01.prefab:3d5a271fdd5626142a9881567ce6ad94");

	// Token: 0x04002C17 RID: 11287
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_BossGatlingWandTreasure_01.prefab:43be4359ee4bc454d83bb067c67e0ea4");

	// Token: 0x04002C18 RID: 11288
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Death_01.prefab:6d892f81258e93f4badfa482a0665765");

	// Token: 0x04002C19 RID: 11289
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_DefeatPlayer_01.prefab:fedcf1cb64a6a3946b33e0a55a21a0ee");

	// Token: 0x04002C1A RID: 11290
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_EmoteResponse_01.prefab:1116f04f4c037884ca39889f23525075");

	// Token: 0x04002C1B RID: 11291
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01.prefab:8ab9b9f60d05b24409a10c00de2a070e");

	// Token: 0x04002C1C RID: 11292
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03.prefab:8fd09491add94f348aab77c79bcde414");

	// Token: 0x04002C1D RID: 11293
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04.prefab:ca9ad22dc7628d64f8c1fd4c026ed67c");

	// Token: 0x04002C1E RID: 11294
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05.prefab:58693bb4d55776d418c6c5c7914ea8ea");

	// Token: 0x04002C1F RID: 11295
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01.prefab:d02ab5b480f82f942aa531f4ddce0793");

	// Token: 0x04002C20 RID: 11296
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02.prefab:62cb5b2f5510d6045afcabfc3535613d");

	// Token: 0x04002C21 RID: 11297
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03.prefab:0d6dca33078df13489fdb7664abeac72");

	// Token: 0x04002C22 RID: 11298
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_Intro_01.prefab:9996636d831af5a479cfd29816b7dd4c");

	// Token: 0x04002C23 RID: 11299
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_IntroReno_01.prefab:1ea6a18afc9e9b4499aaa98e23c793f2");

	// Token: 0x04002C24 RID: 11300
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerBlingtron_01.prefab:e5bc7c740bc4fab44b3a15c0426a765d");

	// Token: 0x04002C25 RID: 11301
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGatlingWandTreasure_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGatlingWandTreasure_01.prefab:9538562cf71a9564b8f05a14eaac5d8e");

	// Token: 0x04002C26 RID: 11302
	private static readonly AssetReference VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01 = new AssetReference("VO_ULDA_BOSS_26h_Female_Mechagnome_PlayerGnomebliterator_01.prefab:4ca5ab7a76339ac4b876e1eebb5ded0e");

	// Token: 0x04002C27 RID: 11303
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_01,
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_03,
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_04,
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_HeroPower_05
	};

	// Token: 0x04002C28 RID: 11304
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_01,
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_02,
		ULDA_Dungeon_Boss_26h.VO_ULDA_BOSS_26h_Female_Mechagnome_Idle_03
	};

	// Token: 0x04002C29 RID: 11305
	private HashSet<string> m_playedLines = new HashSet<string>();
}
