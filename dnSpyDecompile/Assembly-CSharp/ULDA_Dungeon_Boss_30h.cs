using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200049B RID: 1179
public class ULDA_Dungeon_Boss_30h : ULDA_Dungeon
{
	// Token: 0x06003F92 RID: 16274 RVA: 0x001510C0 File Offset: 0x0014F2C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01,
			ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F93 RID: 16275 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F94 RID: 16276 RVA: 0x00151224 File Offset: 0x0014F424
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F95 RID: 16277 RVA: 0x0015122C File Offset: 0x0014F42C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01;
	}

	// Token: 0x06003F96 RID: 16278 RVA: 0x00151264 File Offset: 0x0014F464
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

	// Token: 0x06003F97 RID: 16279 RVA: 0x001512ED File Offset: 0x0014F4ED
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003F98 RID: 16280 RVA: 0x00151303 File Offset: 0x0014F503
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
			if (cardId == "CS2_029")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F99 RID: 16281 RVA: 0x00151319 File Offset: 0x0014F519
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
		if (!(cardId == "UNG_809"))
		{
			if (!(cardId == "CS2_032"))
			{
				if (cardId == "BRM_002")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C67 RID: 11367
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFirefly_01.prefab:7f4b2bcd0a897154781b45da0945666f");

	// Token: 0x04002C68 RID: 11368
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamestrike_01.prefab:90d1ffbe274c5c74d896cf53c8cc4e8b");

	// Token: 0x04002C69 RID: 11369
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_BossTriggerFlamewaker_01.prefab:be690996d7ba7114893560caf4a89f4b");

	// Token: 0x04002C6A RID: 11370
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Death_01.prefab:786ab936edab4ca4f9ec25ef1043ad5f");

	// Token: 0x04002C6B RID: 11371
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_DefeatPlayer_01.prefab:195ab34934b1e4c4190b00a57628d8b4");

	// Token: 0x04002C6C RID: 11372
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_EmoteResponse_01.prefab:388d6dc2e5873f34baef5edbf2f4e45c");

	// Token: 0x04002C6D RID: 11373
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01.prefab:d3da526643216d3469cb4affd279b486");

	// Token: 0x04002C6E RID: 11374
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03.prefab:4d51832d05d371e47b6858dac38b0b7b");

	// Token: 0x04002C6F RID: 11375
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04.prefab:efc05fffb80f51c4bb1d0e9422cbeb9e");

	// Token: 0x04002C70 RID: 11376
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05.prefab:6b91551cde887824a88ad5bb7d0339ae");

	// Token: 0x04002C71 RID: 11377
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01.prefab:8564c53c5c5ded248b8f960b770edc3d");

	// Token: 0x04002C72 RID: 11378
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02.prefab:ae9737b3f3f15444c9bf8aace76dc766");

	// Token: 0x04002C73 RID: 11379
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03.prefab:4e5103210c9e69140a919c27c17ed2ad");

	// Token: 0x04002C74 RID: 11380
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_Intro_01.prefab:c0cb28dc94ff7af4a8d1a72181307912");

	// Token: 0x04002C75 RID: 11381
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Fireball_01.prefab:8dd701ead2a23324ab6cb65105829b13");

	// Token: 0x04002C76 RID: 11382
	private static readonly AssetReference VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01 = new AssetReference("VO_ULDA_BOSS_30h_Female_NefersetTolvir_PlayerTrigger_Pharaoh_Cat_01.prefab:cbadc943d6f8d4847869588fc6b42dd9");

	// Token: 0x04002C77 RID: 11383
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_01,
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_03,
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_04,
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_HeroPower_05
	};

	// Token: 0x04002C78 RID: 11384
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_01,
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_02,
		ULDA_Dungeon_Boss_30h.VO_ULDA_BOSS_30h_Female_NefersetTolvir_Idle_03
	};

	// Token: 0x04002C79 RID: 11385
	private HashSet<string> m_playedLines = new HashSet<string>();
}
