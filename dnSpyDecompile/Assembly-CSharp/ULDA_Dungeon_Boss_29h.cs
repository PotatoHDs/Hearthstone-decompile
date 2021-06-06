using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200049A RID: 1178
public class ULDA_Dungeon_Boss_29h : ULDA_Dungeon
{
	// Token: 0x06003F85 RID: 16261 RVA: 0x00150C40 File Offset: 0x0014EE40
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Death_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_DefeatPlayer_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Idle_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Idle_02,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Intro_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01,
			ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F86 RID: 16262 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F87 RID: 16263 RVA: 0x00150DB4 File Offset: 0x0014EFB4
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F88 RID: 16264 RVA: 0x00150DBC File Offset: 0x0014EFBC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01;
	}

	// Token: 0x06003F89 RID: 16265 RVA: 0x00150DF4 File Offset: 0x0014EFF4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003F8A RID: 16266 RVA: 0x00150ECE File Offset: 0x0014F0CE
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

	// Token: 0x06003F8B RID: 16267 RVA: 0x00150EE4 File Offset: 0x0014F0E4
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
		if (!(cardId == "ULD_158"))
		{
			if (cardId == "ULD_137")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F8C RID: 16268 RVA: 0x00150EFA File Offset: 0x0014F0FA
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
		if (!(cardId == "EX1_250"))
		{
			if (!(cardId == "ULD_197"))
			{
				if (cardId == "ULD_158")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C53 RID: 11347
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerEarthElemental_01.prefab:1737ca79607b52143a5e0ce678551c69");

	// Token: 0x04002C54 RID: 11348
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerQuicksandElemental_01.prefab:68af1f9daf588bb428aa1e77c354c6b2");

	// Token: 0x04002C55 RID: 11349
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_BossTriggerSandstormElemental_01.prefab:8ec53badcc5f10b4dad88c236ea45227");

	// Token: 0x04002C56 RID: 11350
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Death_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Death_01.prefab:d94c0c40b1c86df4eb19a72680c22c51");

	// Token: 0x04002C57 RID: 11351
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_DefeatPlayer_01.prefab:31885d45b3629ae42a957b1f34866e71");

	// Token: 0x04002C58 RID: 11352
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_EmoteResponse_01.prefab:b063b02d74f94344aa9f3a53f22b05de");

	// Token: 0x04002C59 RID: 11353
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01.prefab:eafb2c89983c98a46b55fb9ee4728c68");

	// Token: 0x04002C5A RID: 11354
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02.prefab:80a545b6bc1353d4a90f2dbd5d212328");

	// Token: 0x04002C5B RID: 11355
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03.prefab:a5086e35131768140b3cea2af5139af4");

	// Token: 0x04002C5C RID: 11356
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04.prefab:7d043c8cc0f5bd146a42de87e52f18d5");

	// Token: 0x04002C5D RID: 11357
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05.prefab:bf79b544b8f941e42bbc78639e914c98");

	// Token: 0x04002C5E RID: 11358
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Idle_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Idle_01.prefab:2a5cfeb91b1aca8499db12548ce24e2a");

	// Token: 0x04002C5F RID: 11359
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Idle_02 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Idle_02.prefab:238da7ea3d616fd4dbe60e1054d744b7");

	// Token: 0x04002C60 RID: 11360
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_Intro_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_Intro_01.prefab:f6fe799000fd32848acb5c874bf4479b");

	// Token: 0x04002C61 RID: 11361
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_IntroFinley_01.prefab:b452ab01c32bf6e41aca8dfad5814a10");

	// Token: 0x04002C62 RID: 11362
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Garden_Gnome_01.prefab:26cc182f24c505e49b51a1a889ae6587");

	// Token: 0x04002C63 RID: 11363
	private static readonly AssetReference VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01 = new AssetReference("VO_ULDA_BOSS_29h_Female_Elemental_PlayerTrigger_Sandstorm_Elemental_01.prefab:cbd6fa51be7caa34ab6c8c407f861d19");

	// Token: 0x04002C64 RID: 11364
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_01,
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_02,
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_03,
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_04,
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_HeroPower_05
	};

	// Token: 0x04002C65 RID: 11365
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Idle_01,
		ULDA_Dungeon_Boss_29h.VO_ULDA_BOSS_29h_Female_Elemental_Idle_02
	};

	// Token: 0x04002C66 RID: 11366
	private HashSet<string> m_playedLines = new HashSet<string>();
}
