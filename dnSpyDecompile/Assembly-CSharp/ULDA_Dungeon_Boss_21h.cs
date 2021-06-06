using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000492 RID: 1170
public class ULDA_Dungeon_Boss_21h : ULDA_Dungeon
{
	// Token: 0x06003F27 RID: 16167 RVA: 0x0014EFFC File Offset: 0x0014D1FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Death_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_DefeatPlayer_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01,
			ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F28 RID: 16168 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F29 RID: 16169 RVA: 0x0014F160 File Offset: 0x0014D360
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F2A RID: 16170 RVA: 0x0014F168 File Offset: 0x0014D368
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F2B RID: 16171 RVA: 0x0014F170 File Offset: 0x0014D370
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01;
	}

	// Token: 0x06003F2C RID: 16172 RVA: 0x0014F1A8 File Offset: 0x0014D3A8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "ULDA_Elise" && cardId != "ULDA_Reno")
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

	// Token: 0x06003F2D RID: 16173 RVA: 0x0014F260 File Offset: 0x0014D460
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003F2E RID: 16174 RVA: 0x0014F276 File Offset: 0x0014D476
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
		if (!(cardId == "ULD_276"))
		{
			if (cardId == "ULD_155")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F2F RID: 16175 RVA: 0x0014F28C File Offset: 0x0014D48C
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
		if (!(cardId == "DAL_727"))
		{
			if (!(cardId == "EX1_080"))
			{
				if (cardId == "ULD_728")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002BD4 RID: 11220
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_BossCalltoAdventure_01.prefab:cb6475cddb4628644b0e4dc901289b06");

	// Token: 0x04002BD5 RID: 11221
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_BossSecretkeeper_01.prefab:bfa6a8f5f8d99ba46b87e382b66cbe77");

	// Token: 0x04002BD6 RID: 11222
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_BossSubdue_01.prefab:46613392d861162408cea8ff8688303b");

	// Token: 0x04002BD7 RID: 11223
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Death_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Death_01.prefab:fd2f2d4c8ee359f4e8dad7a4cfca0f42");

	// Token: 0x04002BD8 RID: 11224
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_DefeatPlayer_01.prefab:504f16bec31ace54f85d82e8a3fb0623");

	// Token: 0x04002BD9 RID: 11225
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_EmoteResponse_01.prefab:f622470889b1a6d4e98248078bc73de9");

	// Token: 0x04002BDA RID: 11226
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02.prefab:e18274e0c85c91647a42ac78ea8287ba");

	// Token: 0x04002BDB RID: 11227
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03.prefab:0b224351c92010e4f8066503fadff1fd");

	// Token: 0x04002BDC RID: 11228
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04.prefab:fd300f3e9d5b84a4ead1f9d8f90f4405");

	// Token: 0x04002BDD RID: 11229
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05.prefab:5798ec62d65a7804c81a9e9ee6dc7f4c");

	// Token: 0x04002BDE RID: 11230
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01.prefab:a93a456bbb3f04b44b9bfec33c6b5b7e");

	// Token: 0x04002BDF RID: 11231
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02.prefab:36761bf96493ddf49ba7a65d1a2c3f54");

	// Token: 0x04002BE0 RID: 11232
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03.prefab:099dd12705cf9304996760a35cd23283");

	// Token: 0x04002BE1 RID: 11233
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_Intro_01.prefab:ee4b5860061bc2145a2ab9829b2d1dac");

	// Token: 0x04002BE2 RID: 11234
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_PlayerTranslatingHieroglyphics_01.prefab:4321de4c6f34b3a4c966ceefaf000107");

	// Token: 0x04002BE3 RID: 11235
	private static readonly AssetReference VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01 = new AssetReference("VO_ULDA_BOSS_21h_Female_BloodElf_PlayerUnsealtheVault_01.prefab:e5128c7afd634f745871e3c047a00a73");

	// Token: 0x04002BE4 RID: 11236
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_02,
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_03,
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_04,
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_HeroPower_05
	};

	// Token: 0x04002BE5 RID: 11237
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Idle_01,
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Idle_02,
		ULDA_Dungeon_Boss_21h.VO_ULDA_BOSS_21h_Female_BloodElf_Idle_03
	};

	// Token: 0x04002BE6 RID: 11238
	private HashSet<string> m_playedLines = new HashSet<string>();
}
